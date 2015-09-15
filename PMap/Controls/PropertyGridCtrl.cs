using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.Forms.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraVerticalGrid.Rows;
using System.Reflection;
using PMap.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMap.Controls
{
    /// <summary>
    /// WORK IN PROGRESS !!!
    /// </summary>
    public class PropertyGridCtrl : DevExpress.XtraVerticalGrid.PropertyGridControl
    {
        #region Constructor

        protected Dictionary<string, string> m_errMsg = new Dictionary<string, string>();

        protected Control m_signedControl = null;
        protected BaseDialog m_dialog = null;
        protected ErrorProvider m_errProvider;
        protected bool m_showMessageBox = false;
        protected PersistentRepository m_prep;


        protected bool m_isValid = true;
        protected bool m_isChanged = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyGridCtrl()
        {
            InitializeComponent();
        }

        public Control SignedControl
        {
            set { m_signedControl = value; }
        }



        public BaseDialog Dialog
        {
            set
            {
                m_dialog = value;
                m_dialog.buttonCancel.MouseDown += new MouseEventHandler(buttonCancel_MouseDown);
            }
        }


        public bool ShowMessageBox
        {
            get { return m_showMessageBox; }
            set { m_showMessageBox = value; }
        }


        public Dictionary<string, string> ErrMsg
        {
            get { return m_errMsg; }
            set { m_errMsg = value; }
        }

        public bool IsValid
        {
            set { m_isValid = value; }
            get { return m_isValid; }
        }

        public bool IsChanged
        {
            set { m_isChanged = value; }
            get { return m_isChanged; }
        }

        #endregion

        #region Methods - Private

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();


            m_prep = new PersistentRepository(this);
            this.ExternalRepository = m_prep;

            // 
            // PropertyGridControl
            // 
            this.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(PropertyGridCtrl_ValidatingEditor);
            this.ValidateRecord += new DevExpress.XtraVerticalGrid.Events.ValidateRecordEventHandler(PropertyGridCtrl_ValidateRecord);
            this.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(PropertyGridCtrl_CellValueChanged);

            // Create a repository item which represents an in-place CheckEdit control.
            RepositoryItemCheckEdit riCheckEdit = new RepositoryItemCheckEdit();
            // Represent check boxes as radio buttons.
            riCheckEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            // Associate the Boolean data type with the created repository item.
            this.DefaultEditors.Add(typeof(Boolean), riCheckEdit);

            RepositoryItemDateEdit riDateEdit = new RepositoryItemDateEdit();
            this.DefaultEditors.Add(typeof(DateTime), riDateEdit);

            RepositoryItemSpinEdit riSpinEditd = new RepositoryItemSpinEdit();

            riSpinEditd.DisplayFormat.FormatString = Global.NUMFORMAT;
            riSpinEditd.EditFormat.FormatString = Global.NUMFORMAT;
            //            riSpinEditd.DisplayFormat.FormatString = "#.00";
            riSpinEditd.EditFormat.FormatType = FormatType.Numeric;
            this.DefaultEditors.Add(typeof(double), riSpinEditd);

            RepositoryItemSpinEdit riSpinEditi = new RepositoryItemSpinEdit();
            riSpinEditi.DisplayFormat.FormatString = Global.INTFORMAT;
            riSpinEditi.EditFormat.FormatString = Global.INTFORMAT;
            riSpinEditi.EditFormat.FormatType = FormatType.Numeric;
            this.DefaultEditors.Add(typeof(int), riSpinEditi);

            m_errMsg.Clear();
            m_errProvider = new ErrorProvider();
            //            m_errProvider.ContainerControl = this;
            m_errProvider.RightToLeft = true;
            this.ResumeLayout(false);
        }

        void PropertyGridCtrl_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            IsChanged = true;
        }

        public override void RetrieveFields()
        {
            //NEM SZABAD a MEGJEGYZÉSBŐL KIVENNI !!! 	
            //           base.RetrieveFields();
            //A  RetrieveFields() helyett a SetObject-et kell hívni!!!
        }
        public void SetObject(object p_obj)
        {

            IsValid = true;
            IsChanged = false;

            this.OptionsView.ShowRootCategories = false;
            
            this.SelectedObject = p_obj;
            this.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            base.RetrieveFields();
            this.BestFit();
            if (p_obj != null)
            {
                Type classType = p_obj.GetType();

                foreach (BaseRow catRow in this.Rows)
                {
                     
                   foreach (BaseRow itemRow in catRow.ChildRows)
                    {


                        PropertyInfo propertyInfo = classType.GetProperty(itemRow.Properties.FieldName);
                        object[] attributes = propertyInfo.GetCustomAttributes(true);
                        foreach (object attribute in attributes)
                        {

                            EditorX edt = attribute as EditorX;
                            if (edt != null)
                            {

                                //Custom repository editor
                                /*
                                AppContent appContent = null;
                                if (p_obj.GetType().BaseType == typeof(BaseProperty) || p_obj.GetType().BaseType.BaseType == typeof(BaseProperty))
                                {

                                    BaseProperty bo = (BaseProperty)p_obj;
                                    appContent = bo.AppContent;
                                }
                                RepositoryItem rep = (RepositoryItem)(Activator.CreateInstance(edt.EditorType, appContent, edt.Parameterlist));
                                rep.Name = "edt" + itemRow.Properties.FieldName;
                                this.RepositoryItems.Add(rep);
                                itemRow.Properties.RowEdit = rep;
                                 */
                            }
                        }

                    }
                }
            }
        }


        void PropertyGridCtrl_ValidateRecord(object sender, DevExpress.XtraVerticalGrid.Events.ValidateRecordEventArgs e)
        {
        }



        #endregion

        #region Event Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        protected void PropertyGridCtrl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            /*
            Type classType = this.SelectedObject.GetType();
            string propertyName = getPropertyName();

            string displayName = "";

            PropertyInfo propertyInfo = classType.GetProperty(propertyName);
            object[] attributes = propertyInfo.Attributes;


            var context = new ValidationContext(e.Value, null, null);        
            var results = new List<ValidationResult>();




            
            
            var attributes = typeof(User)
            .GetProperty("EmailAddress")
            .GetCustomAttributes(false)
            .OfType<ValidationAttribute>()
            .ToArray();

        if (!Validator.TryValidateValue(value, context, results, attributes))
        {
            foreach (var result in results)
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }
        else
        {
            Console.WriteLine("{0} is valid", value);
        }

            if ((attributes != null) && (attributes.Length > 0))
            {
                foreach (object attribute in attributes)
                {

                    DisplayNameAttribute dn = attribute as DisplayNameAttribute;
                    if (dn != null)
                        displayName = dn.DisplayName;




                    List<ObjectValidator.ValidationError> validationErros = ObjectValidator.ValidateObject(xDepot);
                    if (validationErros.Count == 0)
                    {

                    Validator validator = attribute as BaseValidator;
                    if (validator != null)
                    {
                        // Validate the data using the rule
                        if (!validator.IsValid(e.Value))
                        {
                            e.Valid = false;
                            string msg = displayName + ":" + validator.ErrorMessage;
                            setError(propertyName, msg);
                            e.ErrorText = validator.ErrorMessage;

                            if (m_showMessageBox)
                            {
                                UI.Error( validator.ErrorMessage);
                            }
                        }
                        else
                        {
                            clearError(propertyName);
                        }
                        setErrProvider();
                    }
                }
            }
             */
        }

        public void clearError(string p_propName)
        {
            if (m_errMsg.ContainsKey(p_propName))
                m_errMsg.Remove(p_propName);

            IsValid = (m_errMsg.Count == 0);

        }

        public void setError(string p_propName, string p_msg)
        {
            clearError(p_propName);
            m_errMsg.Add(p_propName, p_msg);
            IsValid = false;
        }


        public void setErrProvider()
        {
            if (m_signedControl != null)
            {
                m_errProvider.RightToLeft = true;

                m_errProvider.Clear();
                string msg = "";
                m_signedControl.Enabled = (m_errMsg.Count == 0);
                foreach (KeyValuePair<string, string> kvp in m_errMsg)
                {
                    if (msg.Length > 0)
                        msg += "\n";
                    msg += kvp.Value.ToString();
                }
                m_errProvider.SetError(m_signedControl, msg);
            }
        }

        public string getPropertyName()
        {
            return this.FocusedRow.Name.Substring(3);
        }

        public string getPropertyDisplayName()
        {
            Type classType = this.SelectedObject.GetType();
            PropertyInfo propertyInfo = classType.GetProperty(getPropertyName());
            object[] attributes = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);

            if (attributes != null)
            {
                DisplayNameAttribute dn = (DisplayNameAttribute)attributes[0];
                return dn.DisplayName;
            }

            return "";
        }

        public string GetPropertyValue(string p_propName)
        {
            return this.GetCellValue(this.Rows["row" + p_propName], 0).ToString();
        }

        void buttonCancel_MouseDown(object sender, MouseEventArgs e)
        {
            m_errProvider.Clear();
            this.ClearRowErrors();
            m_dialog.CancelPressed();

        }


        #endregion
    }
}


