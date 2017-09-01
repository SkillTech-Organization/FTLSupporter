using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using PMap.LongProcess.Base;
using PMap.LongProcess;
using GMap.NET.Internals;
using System.Threading;
using PMap.MapProvider;
using PMap.Route;
using Map.LongProcess;
using PMap.BO;
using PMap.Localize;
using PMap.Common;
using PMap.Licence;

namespace PMap.Route
{
    /// <summary>
    /// Útvonalszámítás entry point-ok. A PMap.Route névtér osztályait és metódousait csak ezen az osztályon keresztül érjük el.
    /// </summary>
    public static class PMRouteInterface
    {
        /// <summary>
        /// PMap útvonalszámítás adattömbök incializálása
        /// </summary>
        public static void StartPMRouteInitProcess()
        {
            DateTime dtStart = DateTime.Now;
/* 
            PMapIniParams.Instance.ReadParams("", "DB0");
            ChkLic.Check(PMapIniParams.Instance.IDFile);
*/
            try
            {

                InitRouteDataProcess irdp = new InitRouteDataProcess();
                irdp.Run();
                irdp.ProcessForm.ShowDialog();
                Util.Log2File("StartPMRouteInitProcess  " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Útvonalszámítás egy szálon
        /// </summary>
        /// <param name="p_CalcNodes"></param>
        /// <param name="p_boundary"></param>
        /// <param name="p_CalcInfo"></param>
        /// <param name="p_ThreadPriority"></param>
        /// <param name="p_NotifyForm"></param>
        /// <returns></returns>
        public static bool GetPMapRoutesSingle(List<boRoute> p_CalcDistances, string p_CalcInfo, ThreadPriority p_ThreadPriority, bool p_NotifyForm,  bool p_savePoints)
        {
            bool bCompleted = false;
            DateTime dtStart = DateTime.Now;
            TimeSpan tspDiff;


            Util.Log2File("GetPMapRoutes SingleThread START " + Util.GetSysInfo());
/* EZ NEM KELL 

                        PMapIniParams.Instance.ReadParams("", "DB0");
                        ChkLic.Check(PMapIniParams.Instance.IDFile);
*/
                        try
                        {

                            string sTitle = String.Format(PMapMessages.M_INTF_PMROUTES_TH, p_CalcInfo);
                            CalcPMapRouteProcess cpp = null;
                            BaseSilngleProgressDialog pd = null;
                            ProcessNotifyIcon ni = null;

                            if (p_NotifyForm)
                            {
                                pd = new BaseSilngleProgressDialog(0, p_CalcDistances.GroupBy(gr => new { gr.NOD_ID_FROM, gr.RZN_ID_LIST, gr.DST_MAXWEIGHT, gr.DST_MAXHEIGHT, gr.DST_MAXWIDTH }).Count() - 1, sTitle, true);
                                cpp = new CalcPMapRouteProcess(pd, p_ThreadPriority, "", p_CalcDistances, p_savePoints);
                            }
                            else
                            {
                                ni = new ProcessNotifyIcon();
                                cpp = new CalcPMapRouteProcess(ni, p_ThreadPriority, "", p_CalcDistances, p_savePoints);
                            }


                            if (p_NotifyForm)
                            {
                                cpp.Run();
                                pd.ShowDialog();
                            }
                            else
                            {
                                cpp.RunWait();
                            }


                            bCompleted = cpp.Completed;

                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                        tspDiff = DateTime.Now - dtStart;
                        Util.Log2File("GetPMapRoutes SingleThread END   " + Util.GetSysInfo() + " Időtartam:" + tspDiff.ToString() + " Átlag(ms):" + (tspDiff.Duration().TotalMilliseconds / p_CalcDistances.Count));
                        return bCompleted;
                    }


                    /// <summary>
                    /// Útvonalszámítás több szálon
                    /// </summary>
                    /// <param name="p_CalcNodes"></param>
                    /// <param name="p_boundary"></param>
                    /// <param name="p_CalcInfo"></param>
                    /// <param name="p_ThreadPriority"></param>
                    /// <param name="p_NotifyForm"></param>
                    /// <returns></returns>
                    public static bool GetPMapRoutesMulti(List<boRoute> p_CalcDistances, string p_CalcInfo, ThreadPriority p_ThreadPriority, bool p_NotifyForm, bool p_savePoints)
                    {
                        bool bCompleted = true;

                        DateTime dtStart = DateTime.Now;
                        TimeSpan tspDiff;


                        Util.Log2File("GetPMapRoutesMulti START " + Util.GetSysInfo());
            /* EZ NEM KELL 
                        PMapIniParams.Instance.ReadParams("", "DB0");
                        ChkLic.Check(PMapIniParams.Instance.IDFile);
            */
            bool bUseRouteCache = GMaps.Instance.UseRouteCache;
            GMaps.Instance.UseRouteCache = false;

            try
            {


                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                List<boRoute>[] calcDistances = new List<boRoute>[PMapIniParams.Instance.RouteThreadNum];
                for (int i = 0; i < PMapIniParams.Instance.RouteThreadNum; i++)
                {
                    calcDistances[i] = new List<boRoute>();
                }
                int cidx = 0;
                int lastNODE_FROM_ID = -1;
                p_CalcDistances.Sort((a, b) => a.NOD_ID_FROM.CompareTo(b.NOD_ID_FROM));
                foreach (boRoute citem in p_CalcDistances)
                {
                    calcDistances[cidx].Add(citem);
                    
                    if (lastNODE_FROM_ID != citem.NOD_ID_FROM)
                    {
                        lastNODE_FROM_ID = citem.NOD_ID_FROM;
                        cidx++;
                        if (cidx >= calcDistances.Count())
                            cidx = 0;
                    }

                }

                string sTitle = String.Format(PMapMessages.M_INTF_PMROUTES_MULTI_TH, p_CalcInfo, PMapIniParams.Instance.RouteThreadNum);


                BaseMultiProgressDialog pd = null;
                ProcessNotifyIcon ni = null;
                if (p_NotifyForm)
                {
                    pd = new BaseMultiProgressDialog(0, p_CalcDistances.GroupBy(gr => new { gr.NOD_ID_FROM, gr.RZN_ID_LIST }).Count() - 1, sTitle, true);
                }
                else
                {
                    ni = new ProcessNotifyIcon();
                }


                List<CalcPMapRouteProcess> distList = new List<CalcPMapRouteProcess>();

                for (int i = 0; i < PMapIniParams.Instance.RouteThreadNum; i++)
                {
                    CalcPMapRouteProcess gdp = null;
                    if (p_NotifyForm)
                    {

                        gdp = new CalcPMapRouteProcess(pd, p_ThreadPriority, "#" + i.ToString() + "#", calcDistances[i], p_savePoints);
                    }
                    else
                    {
                        gdp = new CalcPMapRouteProcess(ni, p_ThreadPriority, "#" + i.ToString() + "#", calcDistances[i], p_savePoints);

                    }

                    gdp.Run();

                }

                if (p_NotifyForm)
                {
                    pd.ShowDialog();
                }

                foreach (var x in distList)
                {
                    bCompleted = bCompleted && x.Completed;
                }

                tspDiff = DateTime.Now - dtStart;

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                GMaps.Instance.UseRouteCache = bUseRouteCache;
            }
            Util.Log2File("GetPMapRoutesMulti  END   " + Util.GetSysInfo() + " Időtartam:" + tspDiff.ToString() + " Átlag(ms):" + (tspDiff.Duration().TotalMilliseconds / p_CalcDistances.Count));
            return bCompleted;
        }
    }
}
