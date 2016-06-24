using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            uscLog l = new uscLog();

            l.Width = double.NaN; ;
            l.Height = double.NaN; ;
            this.grpMain.Content = l;

        }

        private void btnLicence_Click(object sender, RoutedEventArgs e)
        {
            uscLicences l = new uscLicences();

            l.Width = double.NaN; ;
            l.Height = double.NaN; ;
            this.grpMain.Content = l;


        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            uscTest t = new uscTest();

            t.Width = double.NaN; ;
            t.Height = double.NaN; ;
            this.grpMain.Content = t;


        }

        private void btnLicWarns_Click(object sender, RoutedEventArgs e)
        {
            uscLicWarns lw = new uscLicWarns();

            lw.Width = double.NaN; ;
            lw.Height = double.NaN; ;
            this.grpMain.Content = lw;

        }
    }
}
