using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PMapCore.Licence
{
    public class PMapID
    {

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
