using PMap.Licence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAdmin.Model
{
    public class mdlLicWarns : mdlBase
    {
        public mdlLicWarns()
        {
            m_dateS = DateTime.Now.Date;
            m_dateE = DateTime.Now.Date;
        }

        private ObservableCollection<PMapLicence> m_PMapLicenceList = new ObservableCollection<PMapLicence>();
        public ObservableCollection<PMapLicence> PMapLicenceList
        {
            get { return m_PMapLicenceList; }
            set
            {
                m_PMapLicenceList = value;
                NotifyPropertyChanged("PMapLicenceList", false);
                NotifyPropertyChanged("HasItems", false);
            }
        }

        private DateTime m_dateS;
        public DateTime DateS
        {
            get { return m_dateS; }
            set
            {
                m_dateS = value;
                NotifyPropertyChanged("DateS");
            }
        }

        private DateTime m_dateE;
        public DateTime DateE
        {
            get { return m_dateE; }
            set
            {
                m_dateE = value;
                NotifyPropertyChanged("DateE");
            }
        }

        private PMapLicence m_selLicence;
        public PMapLicence SelLicence
        {
            get { return m_selLicence; }
            set
            {
                m_selLicence = value;
                NotifyPropertyChanged("SelLicence");
            }
        }


        private ObservableCollection<PMapLicWarn> m_PMapLicWarnList = new ObservableCollection<PMapLicWarn>();
        public ObservableCollection<PMapLicWarn> PMapLicWarnList
        {
            get { return m_PMapLicWarnList; }
            set
            {
                m_PMapLicWarnList = value;
                NotifyPropertyChanged("PMapLicWarnList", false);
                NotifyPropertyChanged("HasItems", false);
            }
        }


        private PMapLicWarn m_selLicWarn;
        public PMapLicWarn SelLicWarn
        {
            get { return m_selLicWarn; }
            set
            {
                m_selLicWarn = value;
                NotifyPropertyChanged("SelLicWarn", false);
            }
        }
        public bool HasItems
        {
            get { return m_PMapLicWarnList.Count > 0; }
           }
    }
}
