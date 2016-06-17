using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PMap.Licence
{
    public class PMapID
    {
        [XmlElement(ElementName = "Instance")]
        public string Instance { get; set; }

        [XmlElement(ElementName = "AzureAccountName")]
        public string AzureAccountName { get; set; }

        [XmlElement(ElementName = "AzureAccountKey")]
        public string AzureAccountKey { get; set; }

    }
}
