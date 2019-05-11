using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapUI.Forms.Base;
using PMapCore.BO;

namespace PMapTestApp
{
    public partial class dlgTestInput : BaseDialog
    {

        public dlgTestInput()
        {
            InitializeComponent();
            base.InitDialog();

            propertyGridCtrl1.RetrieveFields();
            propertyGridCtrl1.SignedControl = buttonOK;
            propertyGridCtrl1.Dialog = this;
            propertyGridCtrl1.Refresh();

        }

        public override bool OKPressed()
        {
            return base.OKPressed();
        }
    }
}
