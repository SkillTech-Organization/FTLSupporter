using PMap.Common;
using PMap.Licence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAdmin.Model
{
    public class mdlLog : mdlBase
    {
        
        public mdlLog()
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
                NotifyPropertyChanged("PMapLicenceList");
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
                m_dateS = value;
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
        

        private ObservableCollection<PMapLog> m_PMapLogList = new ObservableCollection<PMapLog>();
        public ObservableCollection<PMapLog> PMapLogList
        {
            get { return m_PMapLogList; }
            set
            {
                m_PMapLogList = value;
                NotifyPropertyChanged("PMapLogList");
            }
        }


    }
}
