using PMapCore.Common.Attrib;
using PMapCore.Common.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.WebTrace
{
    [Serializable]
    [DataContract(Namespace = "User")]
    public class PMUser : AzureTableObjBase
    {

        public const string PartitonConst = "USER";

        [DataMember]
        [AzureTablePartitionKeyAttr]
        public string PartitionKey { get; set; } = PartitonConst;

        private string m_ID { get; set; }
        [DataMember]
        [AzureTableRowKeyAttr]
        public string ID
        {
            get { return m_ID; }
            set
            {
                m_ID = value;
                NotifyPropertyChanged("ID");
            }
        }

        private string m_userName { get; set; }
        [DataMember]
        public string UserName
        {
            get { return m_userName; }
            set
            {
                m_userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private string m_password { get; set; }
        [DataMember]
        public string Password
        {
            get { return m_password; }
            set
            {
                m_password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private string m_userType { get; set; }
        [DataMember]
        public string UserType
        {
            get { return m_userType; }
            set
            {
                m_userType = value;
                NotifyPropertyChanged("UserType");
            }
        }

    }
}
