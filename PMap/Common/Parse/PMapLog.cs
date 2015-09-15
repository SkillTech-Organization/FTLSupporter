using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PMap.Common.Parse
{
    [Serializable]
    [DataContract(Namespace = "")]
    public class PMapLog
    {
        [DataMember]
        public string LOG_IP { get; set; }
        [DataMember]
        public string LOG_TYPE { get; set; }
        [DataMember]
        public string LOG_TEXT { get; set; }
        [DataMember]
        public string LOG_TIMESTAMP { get; set; }
        [DataMember]
        public string LOG_VALUE { get; set; }
    }
}
