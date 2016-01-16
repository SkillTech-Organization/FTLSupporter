using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap;
using PMap.DB;
using PMap.Route;
using PMap.Printing;
using PMap.BO;
using PMap.BLL;
using PMap.Localize;
using PMap.Common;

namespace PMapTestApp
{
    public partial class dlgCheckRouteDetails : Form
    {

        private class CCheckRouteDetails
        {
            public int NOD_ID_FROM { get; set; }
            public int NOD_ID_TO { get; set; }
            public string Text { get; set; }
            public double Dist { get; set; }
            public double Duration { get; set; }
            public double Speed { get; set; }
            public string RoadType { get; set; }
            public string WZone { get; set; }
            public bool OneWay { get; set; }
            public bool DestTraffic { get; set; }
            public string EDG_ETLCODE { get; set; }
        }

        private List<CCheckRouteDetails> m_listItems = new List<CCheckRouteDetails>();
        private Dictionary<string, boRoute> m_route = null;
        private Dictionary<string, boSpeedProfValues> m_sp = null;
        private Dictionary<int, string> m_rdt = null;

        private bllRoute m_bllRoute;
        private bllSpeedProf m_bllSpeedProf;

        public dlgCheckRouteDetails(Dictionary<string, boRoute> p_route)
        {
            InitializeComponent();
            m_route = p_route;
            m_bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
            m_bllSpeedProf = new bllSpeedProf(PMapCommonVars.Instance.CT_DB);
            m_sp = m_bllSpeedProf.GetSpeedValuesToDict();
            m_rdt = m_bllRoute.GetRoadTypesToDict();
            gridRouteDetails.DataSource = m_listItems;

        }
        private void buttonClose_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbSpeedProfile_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbSpeedProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dlgCheckRouteDetails_Load(object sender, EventArgs e)
        {
            DataTable dtSp = m_bllRoute.GetSpeedProfsToDT();
            cmbSpeedProfile.DisplayMember = "SPP_NAME1";
            cmbSpeedProfile.ValueMember = "ID";
            cmbSpeedProfile.DataSource = dtSp;

            DataTable dtRs = m_bllRoute.GetRestZoneListToDT();
            cmbRST_ID_LIST.ValueMember = "RESTZONE_IDS";
            cmbRST_ID_LIST.DisplayMember = "RESTZONE_NAMES";
            cmbRST_ID_LIST.DataSource = dtRs;

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

        private void refresh()
        {
            if (cmbSpeedProfile.DataSource == null || cmbRST_ID_LIST.DataSource == null)
                return;
            m_listItems.Clear();
            int SPP_ID = (int)cmbSpeedProfile.SelectedValue;
            string sRZN_ID_LIST = (string)cmbRST_ID_LIST.SelectedValue;
            if (m_route[sRZN_ID_LIST].Route != null)
            {
                double dDuration = 0;
                foreach (boEdge edge in m_route[sRZN_ID_LIST].Edges)
                {
                    double fSpeed = m_sp[edge.RDT_VALUE.ToString() + Global.SEP_COORD + SPP_ID.ToString()].SPV_VALUE;

                    if (edge.EDG_ETLCODE.Length > 0)
                        System.Console.WriteLine("c");

                    CCheckRouteDetails item = new CCheckRouteDetails
                    {
                        NOD_ID_FROM = edge.NOD_ID_FROM,
                        NOD_ID_TO = edge.NOD_ID_TO,
                        Text = edge.EDG_NAME,
                        Dist = edge.EDG_LENGTH,
                        Duration = (edge.EDG_LENGTH / (fSpeed / 3.6 * 60)),
                        Speed = fSpeed,
                        RoadType = edge.RDT_VALUE.ToString() + "-" + m_rdt[edge.RDT_VALUE],
                        OneWay = edge.EDG_ONEWAY,
                        WZone = edge.WZONE,
                        DestTraffic = edge.EDG_DESTTRAFFIC,
                        EDG_ETLCODE = edge.EDG_ETLCODE

                    };
                    m_listItems.Add(item);
                    dDuration += (edge.EDG_LENGTH / (fSpeed / 3.6 * 60));
                }
                lblDistance.Text = m_route[sRZN_ID_LIST].DST_DISTANCE.ToString();
                lblDuration.Text = dDuration.ToString();
                gridRouteDetails.RefreshDataSource();
            }
            else
            {
                MessageBox.Show(PMapMessages.E_PCHK_NOROUTE_SPP);
            }
        }

        private void cmbRST_ID_LIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

     }
}
