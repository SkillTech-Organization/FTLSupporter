using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.Forms.Base;

namespace PMap.Forms
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
