using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMapCore.LongProcess.Base
{
    public partial class BaseMultiProgressDialog : BaseProgressDialog
    {
        public BaseMultiProgressDialog()
        {
            InitializeComponent();
        }

        public BaseMultiProgressDialog(int p_min, int p_max, string p_caption, bool p_canAbort)
            : base(p_min, p_max, p_caption, p_canAbort)
        {
            InitializeComponent();
            btnAbort.Visible = p_canAbort;
            this.Text = p_caption;
        }

        public override void _setInfoText(string p_infoText)
        {
            
            int itemsPerPage = (int)(lstInfoText.Height / lstInfoText.ItemHeight);
            if (lstInfoText.Items.Count <= itemsPerPage)
            {
                lstInfoText.Items.Add(p_infoText);
            }
            else
            {
                lstInfoText.Items.RemoveAt(0);
                lstInfoText.Items.Insert(itemsPerPage, p_infoText);
            }

            lstInfoText.TopIndex = lstInfoText.Items.Count;
            
        }

        public override void _nextProgressValue()
        {
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            StopAllThreadsAndCloseForm();
        }

      

    }
}
