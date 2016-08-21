﻿using Microsoft.WindowsAzure.Storage.Table;
using PMAdmin.Common;
using PMAdmin.Model;
using PMap;
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
    /// Interaction logic for uscLicWarns.xaml
    /// </summary>
    public partial class uscLicWarns : UserControl
    {
        private mdlLicWarns m_dataContext = new mdlLicWarns();

        public uscLicWarns()
        {
            InitializeComponent();
            this.DataContext = m_dataContext;
            m_dataContext.PMapLicenceList = AzureTableStore.Instance.RetrieveList<PMapLicence>("", "AppInstance");
            m_dataContext.SelLicence = m_dataContext.PMapLicenceList.FirstOrDefault();
            m_dataContext.DateS = DateTime.Now.AddDays(-7).Date;
            m_dataContext.DateE = DateTime.Now.Date;
            getList();
        }

    

        private void dgrLicWarns_AutoGeneratedColumns(object sender, EventArgs e)
        {
            PMAUtils.SetDataGridColums<PMapLicWarn>((DataGrid)sender);

        }

        private void dgrLicWarns_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor pd = (PropertyDescriptor)e.PropertyDescriptor;
            if (pd.PropertyType == typeof(System.Windows.Media.ImageSource))
            {
                e.Column = PMAUtils.SetDataGridImageColums(pd);
            }


        }

        private void dgrLicWarns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PMapLicWarn sel = (PMapLicWarn)dgrLicWarns.SelectedItem;
            if (sel != null)
                m_dataContext.SelLicWarn = sel.ShallowCopy();
            else
                m_dataContext.SelLicWarn = new PMapLicWarn();

        }

        private void getList()
        {
            if (m_dataContext.SelLicence == null)
                return;
            if (!m_dataContext.Dirty)
                return;

            
            using (new WaitCursor())
            {
                if (m_dataContext.DateS > m_dataContext.DateE)
                {
                    var tmp = m_dataContext.DateS;
                    m_dataContext.DateS = m_dataContext.DateE;
                    m_dataContext.DateE = tmp;

                }

                string fltDateS = TableQuery.GenerateFilterCondition("PMapTimestamp", QueryComparisons.GreaterThanOrEqual, m_dataContext.DateS.ToString(Global.DATETIMEFORMAT));
                string fltDateE = TableQuery.GenerateFilterCondition("PMapTimestamp", QueryComparisons.LessThanOrEqual, m_dataContext.DateE.AddDays(1).AddSeconds(-1).ToString(Global.DATETIMEFORMAT));
                string fltINSTANCE = TableQuery.GenerateFilterCondition("AppInstance", QueryComparisons.Equal, m_dataContext.SelLicence.AppInstance);

                m_dataContext.PMapLicWarnList = AzureTableStore.Instance.RetrieveList<PMapLicWarn>(fltDateS + " and " + fltDateE + " and " + fltINSTANCE);
                m_dataContext.SelLicWarn = new PMapLicWarn();
                dgrLicWarns.SelectedItem = null;
                m_dataContext.Dirty = false;


            }
        }

        private void DateS_LostFocus(object sender, RoutedEventArgs e)
        {
            getList();
        }

        private void DateE_LostFocus(object sender, RoutedEventArgs e)
        {
            getList();
        }

        private void cmbAppInstance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getList();

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (!UI.Confirm("A lekérdezésben szereplő összes tétel törlése ?"))
                return;

            using (new WaitCursor())
            {
                foreach (var item in m_dataContext.PMapLicWarnList)
                {
                    AzureTableStore.Instance.Delete(item);
                }
                m_dataContext.Dirty = true;
                getList();
            }
        }
    }
}