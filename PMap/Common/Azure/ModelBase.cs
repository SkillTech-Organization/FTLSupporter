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
    public class ModelBase :  INotifyPropertyChanged, IDataErrorInfo
    {
        public enum enObjectState
        {
            New = 'N',               //not saved
            Stored = 'S',
            Modified = 'M',         //not saved
            Inactive = 'I'          //maybe saved but selected for deleting
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
                this.m_State = enObjectState.Modified;
        }

        public ModelBase()
        {
            m_State = enObjectState.New;
        }



        [ScriptIgnoreAttribute]
        [IgnoreDataMember]
        public bool NewState
        {
            get { return m_State == enObjectState.New; }
        }

        [ScriptIgnoreAttribute]
        [IgnoreDataMember]
        public bool ActiveState
        {
            get { return m_State != enObjectState.Inactive; }
        }

        [ScriptIgnoreAttribute]
        [IgnoreDataMember]
        public bool StoredState
        {
            get { return m_State == enObjectState.Stored; }
        }

        [ScriptIgnoreAttribute]
        [IgnoreDataMember]
        public bool UnSavedState
        {
            get { return m_State != enObjectState.Stored; }
        }

        private enObjectState m_State;
        [DataMember]
        [XmlElement(ElementName = "State")]
        public string State
        {
            get { return m_State.ToString(); }
            set { m_State = (enObjectState)Enum.Parse(typeof(enObjectState), value); NotifyPropertyChanged(); }
        }

        public void SetObjState(enObjectState p_state)
        {
            m_State = p_state;
            NotifyPropertyChanged("State");
            NotifyPropertyChanged("NewState");
            NotifyPropertyChanged("ActiveState");
            NotifyPropertyChanged("StoredState");
            NotifyPropertyChanged("UnSavedState");
        }

        
        #region IDataErrorInfo Members

        [ScriptIgnoreAttribute]
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
        [ScriptIgnoreAttribute]
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
                var v = pi.GetValue(this);
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

        [ScriptIgnoreAttribute]
        [IgnoreDataMember]
        public string LocalizedDateTimeFormat
        {
            get
            {
                /* egyelőre nem kell
                if (CultureInfo.TwoLetterISOLanguageName == "fr")
                    return "MM/dd/yyyy HH:mm";
                else if (CultureInfo.TwoLetterISOLanguageName == "en")
                    return "MM/dd/yyyy HH:mm";
                */
                return "yyyy.MM.dd HH:mm";
            }
        }
    }
}
