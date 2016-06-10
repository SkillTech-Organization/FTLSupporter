﻿using PMAdmin.Common;
using PMAdmin.Model;
using PMap.Common;
using PMap.Common.Azure;
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
    /// Interaction logic for uscLicences.xaml
    /// </summary>
    public partial class uscLicences : UserControl
    {
        private mdlLicences m_dataContext = new mdlLicences();
        public uscLicences()
        {
            InitializeComponent();
            refreshFromDB();
            this.DataContext = m_dataContext;
        }

        private void refreshFromDB()
        {
            using (new WaitCursor())
            {
                m_dataContext.PMapLicenceList = AzureTableStore.Instance.RetrieveList<PMapLicence>();
            }
        }

        private void dgrLicences_AutoGeneratedColumns(object sender, EventArgs e)
        {
            PMAUtils.SetDataGridColums<PMapLog>((DataGrid)sender);

        }

        private void dgrLicences_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor pd = (PropertyDescriptor)e.PropertyDescriptor;
            if (pd.PropertyType == typeof(System.Windows.Media.ImageSource))
            {
                e.Column = PMAUtils.SetDataGridImageColums(pd);
            }
        }
    }
}
