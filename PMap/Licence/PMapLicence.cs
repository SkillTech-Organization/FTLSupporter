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
        public PMapLicence() { m_ID = Guid.NewGuid(); Expired = DateTime.Now.Date; }
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

        private string m_AppInstance;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Példány megnevezés", Order = 1, NoPrefix = true)]
        [Required(ErrorMessage = "Az alkalamzáspéldány megnevezést kötelező kitölteni !")]
        public string AppInstance 
        {
            get { return m_AppInstance; }
            set {
                m_AppInstance = value;
                NotifyPropertyChanged("AppInstance");
            }
        }


        private DateTime m_Expired;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Lejárati dátum", Order = 2, NoPrefix = true)]
        [Required(ErrorMessage = "Lejárati dátumot kötelező kitölteni !")]
        public DateTime Expired 
        {
            get { return m_Expired; }
            set { m_Expired = value; NotifyPropertyChanged("Expired"); }
        }

        private string m_Comment;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Megjegyzés", Order = 3, NoPrefix = true)]
        public string Comment  
        {
            get { return m_Comment; }
            set { m_Comment = value; NotifyPropertyChanged("Comment"); }
        }

        private string m_MachineID;
        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Legutolsó bejelentkezés számítógép", Order = 4, NoPrefix = true)]
        public string MachineID
        {
            get { return m_MachineID; }
            set { m_MachineID = value; NotifyPropertyChanged("MachineID"); }
        }

        [DisplayNameAttributeX(Name = "State", Order = 5, NoPrefix = true)]
        public string xState { get{ return this.State; } }


    }
}
