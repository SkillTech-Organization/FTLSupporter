using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PMapCore.Common;

namespace PMapTestApp
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

            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
      
                       //Többszörös indítás kivédése
            if (ProcessUtils.ThisProcessIsAlreadyRunning())
            {
                ProcessUtils.SetFocusToPreviousInstance(Application.ProductName);
                Application.Exit();
                return;
            }
            frmPMapTest MainForm = null;
            try
            {

                /*
                 * inicializálás, adatbázis, program update
                 */
                MainForm = new frmPMapTest();
                Application.Run(MainForm);
            }
            catch (Exception e)
            {
                Util.ExceptionLog( e);
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
