using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Xml.Serialization;

namespace PMap.Common.Azure
{
    [DataContract(Name = "AzureTableObjBase")]
    public class AzureTableObjBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public enum enObjectState
        {
            [Description("N")]
            New,               //not saved
            [Description("S")]
            Stored,
            [Description("M")]
            Modified,         //not saved
            [Description("I")]
            Inactive              //maybe saved but selected for deleting
        }


        public event PropertyChangedEventHandler PropertyChanged;


        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        public void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            bool isDataMember = false;
            var pi = this.GetType().GetProperty(propertyName);
            if (pi != null)
                isDataMember = Attribute.IsDefined(pi, typeof(DataMemberAttribute));

            if (isDataMember && propertyName != "State" && this.m_State != enObjectState.Inactive && this.m_State != enObjectState.New)
                State = enObjectState.Modified;
        }

        public AzureTableObjBase()
        {
            m_State= enObjectState.New;
        }



        [IgnoreDataMember]
        public bool NewState
        {
            get { return m_State == enObjectState.New; }
        }

        [IgnoreDataMember]
        public bool ActiveState
        {
            get { return m_State != enObjectState.Inactive; }
        }

        [IgnoreDataMember]
        public bool StoredState
        {
            get { return m_State == enObjectState.Stored; }
        }

        [IgnoreDataMember]
        public bool UnSavedState
        {
            get { return m_State != enObjectState.Stored; }
        }

        [IgnoreDataMember]
        public bool ModifiedState
        {
            get { return m_State == enObjectState.Modified; }
        }

        [IgnoreDataMember]
        public bool ChangedState
        {
            get { return m_State == enObjectState.Modified || m_State == enObjectState.New; }
        }

        [IgnoreDataMember]
        private enObjectState m_State = enObjectState.New;
        [DataMember]
        [XmlElement(ElementName = "State")]
        [AzurePartitionAttr]
        [IgnoreDataMember]
        public enObjectState State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                NotifyPropertyChanged("State");
                NotifyPropertyChanged("NewState");
                NotifyPropertyChanged("ActiveState");
                NotifyPropertyChanged("StoredState");
                NotifyPropertyChanged("UnSavedState");
                NotifyPropertyChanged("ModifiedState");
                NotifyPropertyChanged("ChangedState");
            }
        }


        #region IDataErrorInfo Members

        [IgnoreDataMember]
        public string Error
        {
            get
            {
                return null;
            }
        }


        /// <summary>
        /// Examines the property that was changed and provides the
        /// correct error message based on some rules
        /// </summary>
        /// <param name="name">The property that changed</param>
        /// <returns>a error message string</returns>
        [IgnoreDataMember]
        public string this[string name]
        {
            get
            {

                string result = null;
                var pi = this.GetType().GetProperty(name);

                var context = new ValidationContext(this, null, null);
                context.MemberName = pi.Name;
                var results = new List<ValidationResult>();
                var v = pi.GetValue(this, null);
                var isValid = Validator.TryValidateProperty(v, context, results);
                if (!isValid)
                {

                    foreach (var validationResult in results)
                    {
                        if (result != null)
                            result += Environment.NewLine;
                        else
                            result = "";
                        result = validationResult.ErrorMessage;
                    }
                }
                return result;
            }
        }
        #endregion

        [IgnoreDataMember]
        public string LocalizedDateTimeFormat
        {
            get
            {
                /*
                if (CultureInfo.TwoLetterISOLanguageName == "fr")
                    return "MM/dd/yyyy HH:mm";
                else if (CultureInfo.TwoLetterISOLanguageName == "en")
                    return "MM/dd/yyyy HH:mm";
                    */
                return "yyyy.MM.dd HH:mm";
            }
        }

        [IgnoreDataMember]
        public DateTime m_Created;

        [DataMember]
        [XmlElement(ElementName = "Created")]
        [AzurePartitionAttr]
        public DateTime Created { get { return m_Created; } set { m_Created = value; Updated = value; } }
        [DataMember]
        [XmlElement(ElementName = "Updated")]
        [AzurePartitionAttr]
        public DateTime Updated { get; set; }
        [DataMember]
        [XmlElement(ElementName = "Creator")]
        [AzurePartitionAttr]
        public string Creator { get; set; }
        [DataMember]
        [XmlElement(ElementName = "Updater")]
        [AzurePartitionAttr]
        public string Updater { get; set; }
    }
}
