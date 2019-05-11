using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.DB;
using System.Globalization;
using GMap.NET;
using PMapUI.Printing;
using PMapCore.BO;
using PMapUI.Forms.Base;
using PMapCore.Common;
using PMapCore.Common.PPlan;

namespace PMapUI.Forms
{
    public partial class dlgTourDetails : BaseDialog
    {
     

        private boPlanTour m_selTour = null;
        private List<CTourDetails> m_tourDetails = null;

        public dlgTourDetails()
            : base(eEditMode.infomode)
        {
            InitializeComponent();
            InitDialog();
            AskOnExit = false;

        }

        public dlgTourDetails(boPlanTour p_selTour, List<CTourDetails> p_tourDetails)
            : base(eEditMode.infomode)
        {
            InitializeComponent();
            m_selTour = p_selTour;
            m_tourDetails = p_tourDetails;
            InitDialog();
            if (m_selTour != null)
                this.Text = string.Format("{0} jármű útvonala. Kezdés:{1}", m_selTour.TRUCK, m_selTour.START.ToString(Global.DATETIMEFORMAT));

            gridTourDetails.DataSource = m_tourDetails;
            AskOnExit = false;

        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            PMapGridPrinting pr = new PMapGridPrinting(gridTourDetails, this.Text, true);
            pr.ShowPreview();

        }

  
        private void excelToolStripButton_Click(object sender, EventArgs e)
        {

            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                bool v = gridType.Visible;
                gridType.Visible = false;
                gridTourDetails.ExportToXls(openExcel.FileName);
                gridType.Visible = v;

            }

        }

    }
}
