using PMap.Common.Attrib;
using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace PMap.Licence
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class PMapLicence : AzureTableObjBase
    {
        public PMapLicence() { m_ID = Guid.NewGuid(); LIC_EXPIRED = DateTime.Now.Date; }
        public PMapLicence ShallowCopy()
        {
            return (PMapLicence)this.MemberwiseClone();
        }


        private Guid m_ID;
        [DataMember]
        [AzurePartitionAttr]
        [AzureTablePartitionKeyAttr]
        public Guid ID
        {
            get { return m_ID; }
            set { m_ID = value; NotifyPropertyChanged("ID"); }
        }

        private string m_LIC_INSTANCE;

        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Instance megnevezés", Order = 1, NoPrefix = true)]
        [Required(ErrorMessage = "Instance megnevezést kötelező kitölteni !")]
        public string LIC_INSTANCE 
        {
            get { return m_LIC_INSTANCE; }
            set {
                m_LIC_INSTANCE = value;
                NotifyPropertyChanged("LIC_INSTANCE");
            }
        }


        private DateTime m_LIC_EXPIRED;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Lejárati dátum", Order = 2, NoPrefix = true)]
        [Required(ErrorMessage = "Lejárati dátumot kötelező kitölteni !")]
        public DateTime LIC_EXPIRED 
        {
            get { return m_LIC_EXPIRED; }
            set { m_LIC_EXPIRED = value; NotifyPropertyChanged("LIC_EXPIRED"); }
        }

        private string m_LIC_COMMENT;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Megjegyzés", Order = 3, NoPrefix = true)]
        public string LIC_COMMENT  
        {
            get { return m_LIC_COMMENT; }
            set { m_LIC_COMMENT = value; NotifyPropertyChanged("LIC_COMMENT"); }
        }

        private string m_LIC_COMPUTERNAME;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Legutolsó bejelentkezés számítógépnév", Order = 4, NoPrefix = true)]
        public string LIC_COMPUTERNAME
        {
            get { return m_LIC_COMPUTERNAME; }
            set { m_LIC_COMPUTERNAME = value; NotifyPropertyChanged("LIC_COMPUTERNAME"); }
        }

        [DisplayNameAttributeX(Name = "State", Order = 5, NoPrefix = true)]
        public string xState { get{ return this.State; } }


    }
}
