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
        public class LogType
        {
            public string Type { get; set; }
            public string Name { get; set; }

    }
        
        public mdlLog()
        {
            m_dateS = DateTime.Now.Date;
            m_dateE = DateTime.Now.Date;
            List<LogType> lt = new List<LogType>();
            lt.Add( new LogType() { Type = "", Name = "** Minden típus **"});
            lt.Add( new LogType() { Type = "log", Name = "LOG típus"});
            lt.Add(new LogType() { Type = "msg", Name = "MSG típus" });

            m_PMapLogTypeList = new ObservableCollection<LogType>(lt);
        }

        private ObservableCollection<PMapLicence> m_PMapLicenceList = new ObservableCollection<PMapLicence>();
        public ObservableCollection<PMapLicence> PMapLicenceList
        {
            get { return m_PMapLicenceList; }
            set
            {
                m_PMapLicenceList = value;
                NotifyPropertyChanged("PMapLicenceList", false);
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

        private ObservableCollection<LogType> m_PMapLogTypeList = new ObservableCollection<LogType>();
        public ObservableCollection<LogType> PMapLogTypeList
        {
            get { return m_PMapLogTypeList; }
            set
            {
                m_PMapLogTypeList = value;
                NotifyPropertyChanged("PMapLogTypeList", false);
            }
        }

        private string m_selType;
        public string SelType
        {
            get { return m_selType; }
            set
            {
                m_selType = value;
                NotifyPropertyChanged("SelType");
            }
        }

        
        private ObservableCollection<PMapLog> m_PMapLogList = new ObservableCollection<PMapLog>();
        public ObservableCollection<PMapLog> PMapLogList
        {
            get { return m_PMapLogList; }
            set
            {
                m_PMapLogList = value;
                NotifyPropertyChanged("PMapLogList", false);
            }
        }

        private PMapLog m_selLog;
        public PMapLog SelLog
        {
            get { return m_selLog; }
            set
            {
                m_selLog = value;
                NotifyPropertyChanged("SelLog",false);
            }
        }

    }
}
