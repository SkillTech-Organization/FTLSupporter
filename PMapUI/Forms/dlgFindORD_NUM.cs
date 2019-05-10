using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapUI.Forms.Base;

namespace PMapUI.Forms
{
    public partial class dlgFindORD_NUM : BaseDialog
    {
        public dlgFindORD_NUM()
            : base(eEditMode.editmode)
        {
            InitializeComponent();
            InitDialog();
            AskOnExit = false;
        }
    }
}
