using PMap.Common.Attrib;
using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PMap.Licence
{


    [Serializable]
    [DataContract(Namespace = "")]
    public class PMapLicWarn : AzureTableObjBase
    {
        public PMapLicWarn() { m_ID = Guid.NewGuid(); }
        public PMapLicWarn ShallowCopy()
        {
            return (PMapLicWarn)this.MemberwiseClone();
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


        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Alkalmazás példány", Order = 1, NoPrefix = true)]
        public string AppInstance { get; set; }

        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Eredeti számítógép ID", Order = 2, NoPrefix = true)]
        public string OldMachineID { get; set; }

        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Új számítógép ID", Order = 3, NoPrefix = true)]
        public string NewMachineID { get; set; }

        [DataMember]
        [AzurePartitionAttr]
        [DisplayNameAttributeX(Name = "Időpont", Order = 4, NoPrefix = true)]
        public string PMapTimestamp { get; set; }
    }
}
