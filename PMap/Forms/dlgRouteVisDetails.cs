using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.Common;
using PMap.BO;
using PMap.Forms.Base;
using PMap.Printing;

namespace PMap.Forms
{
    public partial class dlgRouteVisDetails : BaseDialog
    {

        private List<RouteVisCommonVars.CRouteVisDetails> m_details = new List<RouteVisCommonVars.CRouteVisDetails>();

        public dlgRouteVisDetails(List<RouteVisCommonVars.CRouteVisDetails> p_details, string p_caption)
            : base(eEditMode.infomode)

        {
            InitializeComponent();
            InitDialog();
            m_details = p_details;
            gridRouteDetails.DataSource = p_details;
            this.Text += "<<<" + p_caption + ">>>";
            AskOnExit = false;

        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            PMapGridPrinting pr = new PMapGridPrinting(gridRouteDetails, this.Text, true);
            pr.ShowPreview();

        }

        private void excelToolStripButton_Click(object sender, EventArgs e)
        {

            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                gridRouteDetails.ExportToXls(openExcel.FileName);
            }

        }

 
    }
}
