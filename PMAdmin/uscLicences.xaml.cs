﻿using PMAdmin.Common;
using PMAdmin.Model;
using PMap.Common;
using PMap.Common.Azure;
using PMap.Licence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml;
using System.Xml.Serialization;

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
            PMAUtils.SetDataGridColums<PMapLicence>((DataGrid)sender);

        }

        private void dgrLicences_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Image mező generálása
            PropertyDescriptor pd = (PropertyDescriptor)e.PropertyDescriptor;
            if (pd.PropertyType == typeof(System.Windows.Media.ImageSource))
            {
                e.Column = PMAUtils.SetDataGridImageColums(pd);
            }

  
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            m_dataContext.EditedItem = new PMapLicence();
            enableControls();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            selectItem();
            enableControls();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            PMapLicence curr = m_dataContext.EditedItem;
            List<ObjectValidator.ValidationError> err = ObjectValidator.ValidateObject(curr);
            if (err.Count > 0)
            {
                dlgValidationErrors d = new dlgValidationErrors(err, "Licence felvitel");
                d.ShowDialog();

            }
            else
            {

                AzureTableObjBase.enObjectState oriState = curr.ObjState;
                writeItem(curr);
                if( oriState == AzureTableObjBase.enObjectState.New)
                    m_dataContext.AddNewItem(curr);
                else
                    m_dataContext.ModifyItem(curr);

                /*
                PMapLicence ori = m_dataContext.PMapLicenceList.Where(x => x.ID == curr.ID).FirstOrDefault();
                if (ori == null)
                {
                    m_dataContext.AddNewItem(curr);
                }
                else
                {
                    m_dataContext.ModifyItem(curr);
                }
                */

                dgrLicences.SelectedItem = curr;
                dgrLicences.ScrollIntoView(curr);
                dgrLicences.Focus();
            }

        }

        private void btnGen_Click(object sender, RoutedEventArgs e)
        {
            m_dataContext.EditedItem.LIC_INSTANCE = "ddddd";
            /*
            XmlSerializer xsSubmit = new XmlSerializer(typeof(MyObject));
            var subReq = new MyObject();
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, subReq);
                var xml = sww.ToString(); // Your XML
            }
            */
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (!UI.Confirm("Törölhető a tétel?"))
                return;
            //Elem kiválasztása
            selectItem();

            //Azure művelet
            PMapLicence curr = m_dataContext.EditedItem;
            writeItem(curr);

            //Model művelet
            m_dataContext.DeleteItem(curr);


        }

        private void dgrLicences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectItem();
        }


        private void selectItem()
        {

            PMapLicence sel = (PMapLicence)dgrLicences.SelectedItem;
            if (sel != null)
                m_dataContext.EditedItem = sel.ShallowCopy();
            else
                m_dataContext.EditedItem = new PMapLicence();
        }

        private void enableControls()
        {
            /*
            btnNew.IsEnabled = true;
            btnReset.IsEnabled = (m_dataContext.EditedItem != null);
            btnSave.IsEnabled = (m_dataContext.EditedItem != null && m_dataContext.EditedItem.UnSavedState);
            btnGen.IsEnabled = (m_dataContext.EditedItem != null && m_dataContext.EditedItem.StoredState);
            */
        }

        private void writeItem(PMapLicence p_item)
        {
            switch (p_item.ObjState)
            {
                case AzureTableObjBase.enObjectState.New:
                    p_item.SetObjState(AzureTableObjBase.enObjectState.Stored);
                    AzureTableStore.Instance.Insert(p_item);
                    break;
                case AzureTableObjBase.enObjectState.Stored:

                    break;
                case AzureTableObjBase.enObjectState.Modified:
                    p_item.SetObjState(AzureTableObjBase.enObjectState.Stored);
                    AzureTableStore.Instance.Modify(p_item);

                    break;
                case AzureTableObjBase.enObjectState.Inactive:
                    p_item.SetObjState(AzureTableObjBase.enObjectState.Inactive);
                    AzureTableStore.Instance.Delete(p_item);
                    break;
                default:
                    break;
            }
        }
    }
}
