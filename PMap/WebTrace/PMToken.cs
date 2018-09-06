using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.WebTrace
{
    [Serializable]
    public class PMToken
    {
        [DataMember]
        public string temporaryUserToken { get; set; } = "";
    }
}
