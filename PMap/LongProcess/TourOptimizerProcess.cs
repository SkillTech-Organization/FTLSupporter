using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.LongProcess.Base;
using PMapCore.DB;
using System.Threading;
using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.Localize;
using PMapCore.DB.Base;
using System.Diagnostics;
using PMapCore.Common;
using System.Windows.Forms;

namespace PMapCore.LongProcess
{
    public class TourOptimizerProcess : BaseLongProcess
    {
        public enum eOptResult
        {
            Init = 0,
            OK = 1,
            Error = 2,
            Cancel = 3,
            IgnoredHappened = 4                 //Csak a túrára optimalizálás esetén értelmezett
        }

        public eOptResult Result { get; set; }
        public string IgnoredOrders { get; private set; } = "";


        private int m_PLN_ID;
        private int m_TPL_ID;
        private bool m_Replan;
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private bool m_silentMode = true;
        private bllOptimize m_optimize;
        private Process m_procOptimizer = new Process();

        public TourOptimizerProcess(BaseProgressDialog p_Form, int p_PLN_ID, int p_TPL_ID, bool p_Replan, bool p_silentMode)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_PLN_ID = p_PLN_ID;
            m_TPL_ID = p_TPL_ID;
            m_Replan = p_Replan;
            m_silentMode = p_silentMode;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
        }
        protected override void DoWork()
        {
            Result = eOptResult.Init;

            m_optimize = new bllOptimize(m_DB, m_PLN_ID, m_TPL_ID, m_Replan);

            m_optimize.FillOptimize(ProcessForm);

            ProcessForm.SetInfoText(PMapMessages.M_OPT_CREATEFILE);
            m_optimize.MakeOptContent();

            Util.String2File(m_optimize.boOpt.OptimizerContent, PMapIniParams.Instance.PlanFile, Encoding.GetEncoding( Global.PM_ENCODING));

            if (System.IO.File.Exists(PMapIniParams.Instance.PlanOK))
                System.IO.File.Delete(PMapIniParams.Instance.PlanOK);
            if (System.IO.File.Exists(PMapIniParams.Instance.PlanErr))
                System.IO.File.Delete(PMapIniParams.Instance.PlanErr);
            if (System.IO.File.Exists(PMapIniParams.Instance.PlanResultFile))
                System.IO.File.Delete(PMapIniParams.Instance.PlanResultFile);

            m_procOptimizer.StartInfo.FileName = PMapIniParams.Instance.PlanAppl;
            m_procOptimizer.StartInfo.Arguments = PMapIniParams.Instance.PlanArgs;
            m_procOptimizer.StartInfo.UseShellExecute = false;
            if (m_silentMode)
            {
                m_procOptimizer.StartInfo.CreateNoWindow = true;
                m_procOptimizer.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                m_procOptimizer.StartInfo.RedirectStandardOutput = true;

            }
            else
            {
                m_procOptimizer.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                m_procOptimizer.StartInfo.RedirectStandardOutput = false;
            }

            m_procOptimizer.EnableRaisingEvents = true;
            m_procOptimizer.Exited += M_procOptimizer_Exited;

            m_procOptimizer.Start();

            {

                for (int m_turn = 1; m_turn <= PMapIniParams.Instance.OptimizeTimeOutSec; m_turn++)
                {
                    ProcessForm.SetInfoText(String.Format(PMapMessages.M_OPT_OPT, m_turn, PMapIniParams.Instance.OptimizeTimeOutSec));

                    if (System.IO.File.Exists(PMapIniParams.Instance.PlanOK))
                    {
                        finalize(eOptResult.OK);
                        return;
                    }
                    if (System.IO.File.Exists(PMapIniParams.Instance.PlanErr))
                    {
                        System.IO.StreamReader file = new System.IO.StreamReader(PMapIniParams.Instance.PlanErr, Encoding.GetEncoding(Global.PM_ENCODING));
                        string content = file.ReadToEnd();
                        if (content.CompareTo(Global.OPT_NOERROR) != 0)
                        {
                            m_optimize = null;
                            finalize(eOptResult.Error);
                            ProcessForm.SetVisible( false);
                            UI.Error( String.Format( PMapMessages.E_PVRP_ERR,  content));
                            return;
                        }
                    }
                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {
                        // && UI.Confirm(PMapMessages.Q_OPT_READRESULT)
                        if (System.IO.File.Exists(PMapIniParams.Instance.PlanResultFile))
                            finalize(eOptResult.OK);
                        else
                            finalize(eOptResult.Cancel);

                        EventStopped.Set();
                        return;
                    }

                    Thread.Sleep(1000);
                    ProcessForm.NextStep();
                }

                var resultLen = new System.IO.FileInfo(PMapIniParams.Instance.PlanResultFile).Length;
                if (System.IO.File.Exists(PMapIniParams.Instance.PlanResultFile) && resultLen > 0)  ////Előrdulhat, hogy elszáll a PVRP és üres a result.dat!
                    finalize(eOptResult.OK);
                else
                    finalize(eOptResult.Cancel);
                m_DB.Close();


            }
        }

        private void M_procOptimizer_Exited(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(PMapIniParams.Instance.PlanOK))  //leállt a process és nincs OK file
            {

                bool errHappened = false;
                try
                {
                    if (System.IO.File.Exists(PMapIniParams.Instance.PlanErr))
                    {
                        System.IO.StreamReader file = new System.IO.StreamReader(PMapIniParams.Instance.PlanErr, Encoding.GetEncoding(Global.PM_ENCODING));
                        string content = file.ReadToEnd();
                        errHappened = content.CompareTo(Global.OPT_NOERROR) != 0;
                    }


                    //leállt a process és van eredményfájl, készítünk egy OK.dat-ot
                    if (System.IO.File.Exists(PMapIniParams.Instance.PlanResultFile)            //van már eredmény
                        && !errHappened)                  //nicns error   
                    {
                        Util.String2File("Optimizer process has exited!", PMapIniParams.Instance.PlanOK);
                        return;
                    }

                    //leállt a process és NINCS eredményfájl és errorfájl, készítünk egy Error.dat-ot
                    if (!System.IO.File.Exists(PMapIniParams.Instance.PlanResultFile))
                    {
                        Util.String2File("Optimizer process has stopped!", PMapIniParams.Instance.PlanErr);
                        return;
                    }
                }
                catch( Exception ex)
                {
                    //A PVRP elszállhat akkor is, amikor fogja az error.dat-ot amiben a Global.OPT_NOERROR szöveg van.
                    //Nincs még result file sem. 
                    //Jobb híjján készítünk egy OK.dat-ot és egy üres resultot, hogy az optimizerprocess be tudjon fejeződni
                    if (!System.IO.File.Exists(PMapIniParams.Instance.PlanResultFile))
                    {
                        Util.String2File("Optimizer process has an Exception:" + ex.Message, PMapIniParams.Instance.PlanResultFile);
                        Util.String2File("" + ex.Message, PMapIniParams.Instance.PlanOK);
                    }
                    return;
                }
            }
        }

        private void finalize(eOptResult p_result)
        {

            Result = p_result;
            if (!m_procOptimizer.HasExited)
                m_procOptimizer.Kill();
            if (PMapIniParams.Instance.LogVerbose >= PMapIniParams.eLogVerbose.debug && m_procOptimizer.StartInfo.RedirectStandardOutput)
                Util.Log2File(String.Format(PMapMessages.M_OPT_OPTRESULT, m_procOptimizer.StandardOutput.ReadToEnd()));


            if (p_result == eOptResult.OK && m_optimize != null)
            {
                m_optimize.ProcessResult(PMapIniParams.Instance.PlanResultFile, ProcessForm);
                if (!string.IsNullOrEmpty(m_optimize.IgnoredOrders))
                {
                    IgnoredOrders = m_optimize.IgnoredOrders;
                    Result = eOptResult.IgnoredHappened;
                }
            }
            return;
        }

        public override void Stop()
        {
            base.Stop();


        }

    }
}
