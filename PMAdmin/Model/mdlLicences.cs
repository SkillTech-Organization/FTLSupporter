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

    }
}
