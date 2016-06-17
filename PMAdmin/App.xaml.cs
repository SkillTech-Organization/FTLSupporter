using PMAdmin.Common;
using PMAdmin.Properties;
using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace PMAdmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Magyar nyelv beállítása
            var ci = new System.Globalization.CultureInfo("hu-HU");
            Thread.CurrentThread.CurrentUICulture = ci;

            //DatePicker vízjel kikapcsolása
            EventManager.RegisterClassHandler(typeof(DatePicker),
                DatePicker.LoadedEvent,
                new RoutedEventHandler(DatePicker_Loaded));

            //FrameworkElement.Language beállítása, hogy minden kontrol
            //a megfelelő nyelvi formátummal működjön
            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            AzureTableStore.Instance.AzureAccount = PMAdmin.Properties.Settings.Default.AzureAccount;
            AzureTableStore.Instance.AzureKey = PMAdmin.Properties.Settings.Default.AzureKey;
        }

        /// <summary>
        /// Get rid of "select a date" in DatePickerTextBox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null) return;

            var tb = PMAUtils.GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null) return;

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;

            wm.Content = "";
        }

 
    }
}
