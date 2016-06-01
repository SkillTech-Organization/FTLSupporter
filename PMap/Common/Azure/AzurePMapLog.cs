using PMap.Common.Attrib;
using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PMap.Common.Azure
{

    [Serializable]
    [DataContract(Namespace = "")]
    public class AzurePMapLog : AzureTableObjBase
    {
        /*****************/
        /* partition key */
        /*****************/

        public AzurePMapLog() { m_ID = Guid.NewGuid();  }

        public AzurePMapLog ShallowCopy()
        {
            return (AzurePMapLog)this.MemberwiseClone();
        }

        private Guid m_ID;                                  
        [DataMember]
        [AzureTablePartitionKeyAttr]
        [AzurePartitionAttr]
        public Guid ID
        {
            get { return m_ID; }
            set { m_ID = value; NotifyPropertyChanged( "ID"); }
        }

        [DataMember]
        [AzurePartitionAttr]
        public string LOG_IP { get; set; }
        [DataMember]
        [AzurePartitionAttr]
        public string LOG_TYPE { get; set; }
        [DataMember]
        [AzurePartitionAttr]
        public string LOG_TEXT { get; set; }
        [DataMember]
        [AzurePartitionAttr]
        public string LOG_TIMESTAMP { get; set; }
        [DataMember]
        [AzurePartitionAttr]
        public string LOG_VALUE { get; set; }
    }
}