using PMAdmin.Model;
using PMap.Common;
using PMap.Licence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class MyClass : INotifyPropertyChanged
    {
        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                UpdateProperty("Text");
                UpdateProperty("HasContent");
            }
        }

        public bool HasContent
        {
            get { return !string.IsNullOrEmpty(text); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void UpdateProperty(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    /// <summary>
    /// Interaction logic for uscTest.xaml
    /// </summary>
    public partial class uscTest : UserControl
    {
        public uscTest()
        {
            InitializeComponent();
             var   pml = new PMapLicence();
            pml.SetObjState(PMap.Common.Azure.AzureTableObjBase.enObjectState.Stored);
            var md = new mdlLicences();
            md.EditedItem.SetObjState(PMap.Common.Azure.AzureTableObjBase.enObjectState.Stored);
            this.DataContext = md;
        }
    }
}
