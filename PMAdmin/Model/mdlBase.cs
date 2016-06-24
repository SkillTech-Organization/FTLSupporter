using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PMAdmin.Model
{
    public class mdlBase : INotifyPropertyChanged
    {
        private bool m_dirty;
        public mdlBase() { m_dirty = false; }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        public void NotifyPropertyChanged(String p_propertyName = "", bool p_setDirty = true)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
            }
            if (p_setDirty)
                m_dirty = true;
        }

        public bool Dirty { get { return m_dirty; } set { m_dirty = value; } }
    }
}
