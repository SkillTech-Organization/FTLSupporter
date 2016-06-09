using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using PMap.Common.Azure;
using PMap.Common.Attrib;

namespace PMap.Common
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class PMapLog : AzureTableObjBase
    {
        /*****************/
        /* partition key */
        /*****************/

        public PMapLog() { m_ID = Guid.NewGuid(); }

        public PMapLog ShallowCopy()
        {
            return (PMapLog)this.MemberwiseClone();
        }

        [DataMember]
        [AzurePartitionAttr]
        [AzureTablePartitionKeyAttr]
        [DisplayNameAttributeX(Name = "Instance", Order = 1, NoPrefix=true)]
        public string LOG_INSTANCE { get; set; }


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
        [DisplayNameAttributeX(Name = "Bejegyzés típusa", Order = 2, NoPrefix=true)]
        public string LOG_TYPE { get; set; }

        [DataMember]
        [AzureRowAttr]
        [DisplayNameAttributeX(Name = "Szöveg", Order = 3, NoPrefix=true)]
        public string LOG_TEXT { get; set; }

        [DataMember]
        [AzureRowAttr]
        [DisplayNameAttributeX(Name = "Időpont", Order = 4, NoPrefix=true)]
        public string LOG_TIMESTAMP { get; set; }

        [DataMember]
        [AzureRowAttr]
        [DisplayNameAttributeX(Name = "Érték", Order = 5, NoPrefix = true)]
        public string LOG_VALUE { get; set; }
    }
}
