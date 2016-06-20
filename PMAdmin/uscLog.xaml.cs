﻿using PMAdmin.Common;
using PMAdmin.Model;
using PMap.Common;
using PMap.Common.Azure;
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
    /// <summary>
    /// Interaction logic for uscLog.xaml
    /// </summary>
    public partial class uscLog : UserControl
    {
        private mdlLog m_dataContext = new mdlLog();
        public uscLog()
        {
            InitializeComponent();
            refreshFromDB();
            this.DataContext = m_dataContext;
        }
        private void refreshFromDB()
        {
            using (new WaitCursor())
            {
//                m_dataContext.PMapLogList =AzureTableStore.Instance.RetrieveList<PMapLog>();
                m_dataContext.PMapLicenceList = AzureTableStore.Instance.RetrieveList<PMapLicence>();

                m_dataContext.SelLicence = m_dataContext.PMapLicenceList.FirstOrDefault();
            }
        }

        private void dgrLog_AutoGeneratedColumns(object sender, EventArgs e)
        {
            PMAUtils.SetDataGridColums<PMapLog>((DataGrid)sender);

        }

        private void dgrLog_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor pd = (PropertyDescriptor)e.PropertyDescriptor;
            if (pd.PropertyType == typeof(System.Windows.Media.ImageSource))
            {
                e.Column = PMAUtils.SetDataGridImageColums(pd);
            }

        }
    }
}
