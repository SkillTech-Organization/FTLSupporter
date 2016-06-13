using PMap.Common;
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
using System.Windows.Shapes;


namespace PMAdmin
{
    /// <summary>
    /// Interaction logic for dlgValidationErrors.xaml
    /// </summary>
    public partial class dlgValidationErrors : Window
    {
        List<ObjectValidator.ValidationError> m_errList;
        public dlgValidationErrors(List<ObjectValidator.ValidationError> p_errList, string p_title = "")
        {
            InitializeComponent();
            m_errList = p_errList;
            lstErr.DataContext = m_errList;
            DataGridTitle.Text = p_title;
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
