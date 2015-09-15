using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PMap.Common.Parse
{
     [Serializable]
    [DataContract(Namespace = "")]
    public class PMapCalls
    {
        [DataMember]
        public string PMC_LICENCE { get; set; }
        [DataMember]
        public string PMC_IP { get; set; }
        [DataMember]
        public string PMC_VERSION { get; set; }
        [DataMember]
        public string PMC_FUNCTION { get; set; }
        [DataMember]
        public string PMC_TIMESTAMP { get; set; }
        [DataMember]
        public string PMC_RESULT { get; set; }
        [DataMember]
        public string PMC_DURATION { get; set; }
        [DataMember]
        public string PMC_VALUE1 { get; set; }
        [DataMember]
        public string PMC_VALUE2 { get; set; }
        [DataMember]
        public string PMC_VALUE3 { get; set; }
        [DataMember]
        public string PMC_VALUE4 { get; set; }
        [DataMember]
        public string PMC_VALUE5 { get; set; }
        [DataMember]
        public string PMC_VALUE6 { get; set; }
        [DataMember]
        public string PMC_VALUE7 { get; set; }
        [DataMember]
        public string PMC_VALUE8 { get; set; }
        [DataMember]
        public string PMC_VALUE9 { get; set; }
        [DataMember]
        public string PMC_VALUE10 { get; set; }
    }
}
