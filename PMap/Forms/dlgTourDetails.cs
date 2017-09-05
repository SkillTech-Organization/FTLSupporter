using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.DB;
using System.Globalization;
using GMap.NET;
using PMap.Printing;
using PMap.BO;
using PMap.Forms.Base;

namespace PMap.Forms
{
    public partial class dlgTourDetails : BaseDialog
    {
        public class CTourDetails
        {
            public int Type { get; set; }
            public string Text { get; set; }
            public string Dist { get; set; }
            public string Duration { get; set; }
            public string Speed { get; set; }
            public string RoadType { get; set; }
            public string WZone { get; set; }
            public bool OneWay { get; set; }
            public bool DestTraffic { get; set; }
            public string EDG_ETLCODE { get; set; }
            public int EDG_MAXWEIGHT { get; set; }
            public int EDG_MAXHEIGHT { get; set; }
            public int EDG_MAXWIDTH { get; set; }
            public double OrigToll { get; set; }
            public double Toll { get; set; }
        }

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
