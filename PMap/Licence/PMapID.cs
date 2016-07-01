using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PMap.Licence
{
    internal class PMapID
    {
        public const string pw = "01EF1AEA0F433DE23F9C5BBB2A222100";
        public const string iv = "01EE23F9C5BBB2A2";

        [XmlElement(ElementName = "ID")]
        public Guid ID { get; set; }

        [XmlElement(ElementName = "AppInstance")]
        public string AppInstance { get; set; }

        [XmlElement(ElementName = "AzureAccountName")]
        public string AzureAccountName { get; set; }

        [XmlElement(ElementName = "AzureAccountKey")]
        public string AzureAccountKey { get; set; }

    }
}
