using PMapCore.Common;
using PMapUI.Forms.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPOrder.Forms
{
    public partial class dlgImport : BaseDialog
    {
        public string FileName { get; private set; }
        public DateTime ShippingDateX { get; private set; }
        public dlgImport()
              : base(eEditMode.editmode)
        {
            InitializeComponent();
            InitDialog();
            buttonOK.Enabled = false;
            dtmShippingDateX.Value = Util.GetNextWorkingDay(DateTime.Now.Date).Date;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (openCSV.ShowDialog() == DialogResult.OK)
            {
                buttonOK.Enabled = true;
                lblvFile.Text = openCSV.FileName;
         
            }
        }
        public override bool OKPressed()
        {
            FileName = lblvFile.Text;
            ShippingDateX = dtmShippingDateX.Value;
            return base.OKPressed();
        }
    }
}
