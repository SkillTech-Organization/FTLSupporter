using PMap.Common;
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
