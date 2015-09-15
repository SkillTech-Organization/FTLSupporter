using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PMap.LongProcess.Base
{
    public partial class BaseSilngleProgressDialog : BaseProgressDialog
    {
        private Cursor m_oldCursor = Cursors.WaitCursor;
        private int m_stepVal = 0;

        public BaseSilngleProgressDialog(int p_min, int p_max, string p_caption, bool p_canAbort)
            :base(p_min, p_max, p_caption, p_canAbort)
        {
            InitializeComponent();
            progressBar.Minimum = p_min >= 0 ? p_min : 0;
            progressBar.Maximum = p_max >= 0 ? p_max : 0;
            btnAbort.Visible = p_canAbort;
            tblProgress.ColumnCount = p_canAbort ? 2 : 1;
      }

        public override  void _initFormParams( int p_min, int p_max, string p_caption)
        {
            p_min = p_min >= 0 ? p_min : 0;
            p_max = p_max >= 0 ? p_max : 0;

            base._initFormParams(p_min, p_max, p_caption);
            m_stepVal = ((p_max - p_min) / 100);      //max 100 lépés
            m_stepVal = (m_stepVal > 0 ? m_stepVal : 1);
        }

    
        public override void _setFormCaption(string p_caption)
        {
            this.Text = p_caption;
        }

        public override void _setInfoText(string p_infoText)
        {
            this.lblInfo.Text = p_infoText;
        }

        public override void _nextProgressValue()
        {
            m_value++;
            if( progressBar.Value + m_stepVal >= m_value && m_value <= progressBar.Maximum)
                progressBar.Value = m_value;

            //this.Refresh();
        }


        private void btnAbort_Click(object sender, EventArgs e)
        {
            StopAllThreadsAndCloseForm();
        }


     }
}
