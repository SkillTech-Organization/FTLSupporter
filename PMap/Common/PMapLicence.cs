using PMap.Common.Attrib;
using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PMap.Common
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class PMapLicence : AzureTableObjBase
    {
        public PMapLicence() { m_ID = Guid.NewGuid(); }
        public PMapLog ShallowCopy()
        {
            return (PMapLog)this.MemberwiseClone();
        }
        [DataMember]
        [AzurePartitionAttr]
        [AzureTablePartitionKeyAttr]
        [DisplayNameAttributeX(Name = "Instance megnevezés", Order = 1, NoPrefix = true)]
        [Required(ErrorMessage = "Instance megnevezést kötelező kitölteni !")]
        public string LIC_INSTANCE { get; set; }

        private Guid m_ID;
        [DataMember]
        [AzureRowAttr]
        [AzureTableRowKeyAttr]
        public Guid ID
        {
            get { return m_ID; }
            set { m_ID = value; NotifyPropertyChanged("ID"); }
        }

        [DataMember]
        [AzureRowAttr]
        [DisplayNameAttributeX(Name = "Lejárati dátum", Order = 2, NoPrefix = true)]
        [Required(ErrorMessage = "Lejárati dátumot kötelező kitölteni !")]
        public DateTime LIC_EXPIRED { get; set; }

        [DataMember]
        [AzureRowAttr]
        [DisplayNameAttributeX(Name = "Megjegyzés", Order = 3, NoPrefix = true)]
        public string LIC_COMMENT { get; set; }

    }
}
