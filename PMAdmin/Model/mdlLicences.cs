using PMap.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAdmin.Model
{
    public class mdlLicences : mdlBase
    {

        private ObservableCollection<PMapLicence> m_PMapLicenceList = new ObservableCollection<PMapLicence>();
        public ObservableCollection<PMapLicence> PMapLicenceList
        {
            get { return m_PMapLicenceList; }
            set
            {
                m_PMapLicenceList = value;
                NotifyPropertyChanged("PMapLicenceList");
            }

        }

        private PMapLicence m_editedItem = new PMapLicence();
        public PMapLicence EditedItem
        {
            get { return m_editedItem; }
            set
            {
                m_editedItem = value;
                NotifyPropertyChanged("EditedItem");
            }

        }

        public bool IsNeedSave
        {
            get
            {
                if (EditedItem != null)
                    return EditedItem.UnSavedState;
                return false;
            }
        }

        /***************************/
        /*     Business logic      */
        /***************************/
        public void AddNewItem(PMapLicence p_newItem)
        {
            this.PMapLicenceList.Add(p_newItem);
            NotifyPropertyChanged("PMapLicenceList");

        }

        public void ModifyItem(PMapLicence p_modItem)
        {
            PMapLicence pml = PMapLicenceList.Where(x => x.ID == p_modItem.ID ).FirstOrDefault();
            if (pml != null)
            {
                int idx = PMapLicenceList.IndexOf(pml);
                PMapLicenceList[idx] = p_modItem;
                NotifyPropertyChanged("PMapLicenceList");
            }
        }

        public void DeleteItem(PMapLicence p_modItem)
        {
            PMapLicence pml = PMapLicenceList.Where(x => x.ID == p_modItem.ID).FirstOrDefault();
            if (pml != null)
            {
                PMapLicenceList.Remove( pml);
                EditedItem = new PMapLicence();
                NotifyPropertyChanged("PMapLicenceList");
            }
        }
       
    }
}
