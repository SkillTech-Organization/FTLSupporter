﻿using System;
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