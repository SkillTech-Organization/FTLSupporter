using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PMap.Common.Parse
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class AzurePMapLog : PMapLog
    {
        /*****************/
        /* partition key */
        /*****************/

        public PMapLog() { m_ID = Guid.NewGuid(); }

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
            set { m_ID = value; NotifyPropertyChanged("ID"); }
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
