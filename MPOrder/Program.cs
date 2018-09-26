using MPOrder.Forms;
using PMapCore.Common;
using PMapCore.Licence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPOrder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Többszörös indítás kivédése
            if (ProcessUtils.ThisProcessIsAlreadyRunning())
            {
                ProcessUtils.SetFocusToPreviousInstance(Application.ProductName);
                Application.Exit();
                return;
            }
            frmMPOrder MainForm = null;
            try
            {
                PMapIniParams.Instance.ReadParams("", "DB0", "MPOrder.ini");
                PMapCommonVars.Instance.ConnectToDB();

                ChkLic.Check(PMapIniParams.Instance.IDFile);


                MainForm = new frmMPOrder();
                Application.Run(MainForm);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);

                Application.Exit();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            Util.ExceptionLog(ex);
            UI.Error(ex.Message);
            Application.Exit();
        }
    }
}
