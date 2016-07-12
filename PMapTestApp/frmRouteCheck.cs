using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using PMap.DB;
using PMap.MapProvider;
using System.Drawing.Drawing2D;
using PMap.Route;
using PMap;
using PMap.BO;
using PMap.Localize;
using PMap.BLL;
using PMap.Common;
using System.Globalization;

namespace PMapTestApp
{
    public partial class frmRouteCheck : Form
    {
        private GMapMarker CurrentPos;
        private GMapMarker MarkerFrom;
        private GMapMarker MarkerTo;
        private Dictionary<string, boRoute> m_routes = new Dictionary<string, boRoute>();

        // layers
        private GMapOverlay m_selectorLayer;
        private GMapOverlay m_routeLayer;
        private GMapOverlay m_directRouteLayer;
        private GMapOverlay m_boundaryLayer;

        private bool m_isMouseDown = false;
        private int m_currZoom = Global.DefZoom;
        private const int grpShowMarkersShrink = 15;

        private List<int> m_boundNodes = new List<int>();

        private bllRoute m_bllRoute;
        private bllDepot m_bllDepot;

        public frmRouteCheck()
        {
            InitializeComponent();
            gMapControl.Position = new PointLatLng(46.3, 20.1);
            init();
            UpdateControls();
        }



        private void init()
        {
            try
            {
                if (!DesignMode)
                {

                    PMapCommonVars.Instance.ConnectToDB();
                    
                    m_bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
                    m_bllDepot = new bllDepot(PMapCommonVars.Instance.CT_DB);


                    tbZoom.Minimum = Global.DefMinZoom;
                    tbZoom.Maximum = Global.DefMaxZoom;
                    tbZoom.Value = m_currZoom;


                    gMapControl.MapProvider = PMapCommonVars.Instance.MapProvider;
                    gMapControl.MinZoom = Global.DefMinZoom;
                    gMapControl.MaxZoom = Global.DefMaxZoom;
                    gMapControl.Zoom = m_currZoom;

                    m_selectorLayer = new GMapOverlay(Global.selectorLayerName);
                    gMapControl.Overlays.Add(m_selectorLayer);
                    m_routeLayer = new GMapOverlay(Global.routeLayerName);
                    gMapControl.Overlays.Add(m_routeLayer);

                    m_directRouteLayer = new GMapOverlay("directRoute");
                    gMapControl.Overlays.Add(m_directRouteLayer);

                    MarkerTo = new GMarkerGoogle(gMapControl.Position, GMarkerGoogleType.red);
                    m_selectorLayer.Markers.Add(MarkerTo);
                    MarkerFrom = new GMarkerGoogle(gMapControl.Position, GMarkerGoogleType.purple);
                    m_selectorLayer.Markers.Add(MarkerFrom);

                    CurrentPos = new GMarkerCross(gMapControl.Position);
                    m_selectorLayer.Markers.Add(CurrentPos);


                    m_boundaryLayer = new GMapOverlay(Global.boundaryLayerName);
                    gMapControl.Overlays.Add(m_boundaryLayer);


                    //                    CurrentPos = new GMarkerCross(gMapControl.Position);
                    //                    CurrentPos.ToolTipMode = MarkerTooltipMode.Always;
                    //                    CurrentPos.ToolTipText = "Kiválaztott pozíció";
                    //                    CurrentPos = new GMarkerCross(gMapControl.Position);


                    gMapControl.MouseMove += new MouseEventHandler(gMapControl_MouseMove);
                    gMapControl.MouseDown += new MouseEventHandler(gMapControl_MouseDown);
                    gMapControl.MouseUp += new MouseEventHandler(gMapControl_MouseUp);
                    gMapControl.MouseDoubleClick += new MouseEventHandler(gMapControl_MouseDoubleClick);
                    gMapControl.OnMapZoomChanged += new MapZoomChanged(gMapControl_OnMapZoomChanged);

                    rdbFrom.Checked = true;
                    rdbFastestPath.Checked = true;
                    /*
                    numFromNOD_ID.Value =46087;
                    numToNOD_ID.Value = 15;
                    */

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void UpdateControls()
        {
            lblCurrLat.Text = CurrentPos.Position.Lat.ToString("0.0000000");
            lblCurrLng.Text = CurrentPos.Position.Lng.ToString("0.0000000");
            tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
            if (tbZoom.Value != m_currZoom)
                tbZoom.Value = m_currZoom;
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
            button1.Enabled = numToNOD_ID.Value > 0 && numFromNOD_ID.Value > 0;
            button2.Enabled = numToNOD_ID.Value > 0 && numFromNOD_ID.Value > 0;
            cmbRST_ID_LIST.Enabled = numToNOD_ID.Value > 0 && numFromNOD_ID.Value > 0 && m_routes != null && m_routes.Count > 0;

            m_boundaryLayer.Polygons.Clear();
            if (chkBoundary.Checked && m_boundNodes.Count > 0)
            {
                    List<int> nodes = new int[] { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) }.ToList();
                    RectLatLng r = m_bllRoute.getBoundary(m_boundNodes);

                    List<PointLatLng> p = new PointLatLng[] { new PointLatLng( r.Top ,r.Left ),
                                                           new PointLatLng( r.Top, r.Right ),
                                                           new PointLatLng( r.Bottom, r.Right ),
                                                           new PointLatLng( r.Bottom, r.Left)
                                                           }.ToList();

                    List<PointLatLng> p2 = new PointLatLng[] { new PointLatLng( (double)numLatFrom.Value, (double)numLngFrom.Value),
                                                           new PointLatLng( (double)numLatFrom.Value, (double)numLngTo.Value),
                                                           new PointLatLng( (double)numLatTo.Value, (double)numLngTo.Value),
                                                           new PointLatLng( (double)numLatTo.Value, (double)numLngFrom.Value)
                                                           }.ToList();

                    m_boundaryLayer.Polygons.Add(new GMapPolygon(p, PMapMessages.S_PCHK_BOUNDARY));
             //       m_boundaryLayer.Polygons.Add(new GMapPolygon(p2, "lehatárolás"));
            }


        }

        void gMapControl_OnMapZoomChanged()
        {
            tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
            tbZoom.Value = (int)(gMapControl.Zoom);
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
        }

        void gMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PointLatLng pt = gMapControl.FromLocalToLatLng(e.X, e.Y);

            if (rdbFrom.Checked)
            {
                setFromNode(pt);
            }
            else
            {
                setToNode(pt);
            }
            gMapControl.ZoomAndCenterMarkers(m_selectorLayer.Id);

        }

        private void setFromNode(PointLatLng p_pt)
        {
            int NOD_ID = m_bllRoute.GetNearestNOD_ID(p_pt);
            if (NOD_ID > 0)
            {
                this.numLatFrom.ValueChanged -= new System.EventHandler(this.numLatFrom_ValueChanged);
                this.numLngFrom.ValueChanged -= new System.EventHandler(this.numLatFrom_ValueChanged);
                numFromNOD_ID.Value = NOD_ID;
                MarkerFrom.Position = p_pt;
                numLatFrom.Value = Convert.ToDecimal(p_pt.Lat);
                numLngFrom.Value = Convert.ToDecimal(p_pt.Lng);
                this.numLatFrom.ValueChanged += new System.EventHandler(this.numLatFrom_ValueChanged);
                this.numLngFrom.ValueChanged += new System.EventHandler(this.numLatFrom_ValueChanged);
                UpdateControls();
            }

        }
        private void setToNode(PointLatLng p_pt)
        {
            int NOD_ID = m_bllRoute.GetNearestNOD_ID(p_pt);
            if (NOD_ID > 0)
            {
                this.numLatTo.ValueChanged -= new System.EventHandler(this.numLatTo_ValueChanged);
                this.numLngTo.ValueChanged -= new System.EventHandler(this.numLatTo_ValueChanged);

                numToNOD_ID.Value = NOD_ID;
                MarkerTo.Position = p_pt;
                numLatTo.Value = Convert.ToDecimal(p_pt.Lat);
                numLngTo.Value = Convert.ToDecimal(p_pt.Lng);
                this.numLatTo.ValueChanged += new System.EventHandler(this.numLatTo_ValueChanged);
                this.numLngTo.ValueChanged += new System.EventHandler(this.numLatTo_ValueChanged);
                UpdateControls();
            }

        }

        void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isMouseDown = false;
            }
        }

        void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isMouseDown = true;
                CurrentPos.Position = gMapControl.FromLocalToLatLng(e.X, e.Y);
                UpdateControls();
            }
        }

        void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_isMouseDown)
            {
                CurrentPos.Position = gMapControl.FromLocalToLatLng(e.X, e.Y);
                UpdateControls();
            }
        }


        private void tbZoom_ValueChanged(object sender, EventArgs e)
        {
            gMapControl.OnMapZoomChanged -= new MapZoomChanged(gMapControl_OnMapZoomChanged);
            gMapControl.Zoom = (tbZoom.Value);
            gMapControl.OnMapZoomChanged += new MapZoomChanged(gMapControl_OnMapZoomChanged);

        }

        private void numLatFrom_ValueChanged(object sender, EventArgs e)
        {
            MarkerFrom.Position = new PointLatLng(Convert.ToDouble(numLatFrom.Value, CultureInfo.InvariantCulture), Convert.ToDouble(numLngFrom.Value, CultureInfo.InvariantCulture));
            int NOD_ID = m_bllRoute.GetNearestNOD_ID(MarkerFrom.Position);
            if (NOD_ID > 0)
                numFromNOD_ID.Value = NOD_ID;
            else
                numFromNOD_ID.Value = 0;
            gMapControl.ZoomAndCenterMarkers(m_selectorLayer.Id);
            UpdateControls();
        }

        private void numLatTo_ValueChanged(object sender, EventArgs e)
        {
            MarkerTo.Position = new PointLatLng(Convert.ToDouble(numLatTo.Value, CultureInfo.InvariantCulture), Convert.ToDouble(numLngTo.Value, CultureInfo.InvariantCulture));
            int NOD_ID = m_bllRoute.GetNearestNOD_ID(MarkerTo.Position);
            if (NOD_ID > 0)
                numToNOD_ID.Value = NOD_ID;
            else
                numToNOD_ID.Value = 0;
            gMapControl.ZoomAndCenterMarkers(m_selectorLayer.Id);
            UpdateControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalcAndShowRoute();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (m_routes == null)
                CalcAndShowRoute();

            if (m_routes != null)
            {
                dlgCheckRouteDetails rd = new dlgCheckRouteDetails(m_routes);
                rd.ShowDialog(this);

            }
            

        }

        private void CalcAndShowRoute()
        {
            PMapRoutingProvider provider = new PMapRoutingProvider();
            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);
            if (numFromNOD_ID.Value > 0 && numToNOD_ID.Value > 0)
            {
                RectLatLng boundary = new RectLatLng();
                List<int> nodes = new List<int>() { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value)};
                boundary = m_bllRoute.getBoundary(nodes);

                DataTable dtRZN_ID_LIST = (DataTable)cmbRST_ID_LIST.DataSource;
                List<string> aRZN_ID_LIST = dtRZN_ID_LIST.AsEnumerable().Select( x=>Util.getFieldValue<string>(x, "RESTZONE_IDS")).ToList();
                Dictionary<string, List<int>[]> NeighborsFull = null;
                Dictionary<string, List<int>[]> NeighborsCut = null;
                RouteData.Instance.getNeigboursByBound(aRZN_ID_LIST, out  NeighborsFull, out NeighborsCut, boundary);
                
                //összes RZN_ID_LIST-ra elkérjük az útvonalakat
                m_routes.Clear();
                foreach ( string sRZN_ID_LIST in aRZN_ID_LIST)
                {
                    boRoute result = provider.GetRoute(sRZN_ID_LIST, Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value),
                        NeighborsFull[sRZN_ID_LIST], NeighborsCut[sRZN_ID_LIST],
                        rdShortestPath.Checked ? ECalcMode.ShortestPath : ECalcMode.FastestPath);
                    m_routes.Add(sRZN_ID_LIST, result);
                }
                UpdateControls();
                showRoute();
            }

        }

        private void showRoute()
        {
        
            if (m_routes != null)
            {
                m_routeLayer.Routes.Clear();
                m_routeLayer.Markers.Clear();
                
                
                string sRZN_ID_LIST = (string)cmbRST_ID_LIST.SelectedValue;
                boRoute route;
                

                if (m_routes.TryGetValue( sRZN_ID_LIST, out route) && route.DST_DISTANCE > 0)
                {

                    GMapRoute r = new GMapRoute(route.Route.Points, "");
                    Pen p = new Pen(Color.GreenYellow, Global.TourLineWidthNormal);
                    p.DashStyle = DashStyle.Solid;
                    r.Stroke = p;
                    m_routeLayer.Routes.Add(r);
                    int iEdge = 0;

                    for (int i = 0; i < route.Route.Points.Count(); i++)
                    {
                        PointLatLng pt = route.Route.Points[i];
                        GMapMarker gm = new GMarkerGoogle(pt, GMarkerGoogleType.yellow_small);

                        boEdge edg = route.Edges[iEdge];
                        if (i == 0)
                            gm.ToolTipText = edg.NOD_ID_FROM.ToString();
                        else
                        {
                            gm.ToolTipText = edg.NOD_ID_TO.ToString();
                            iEdge++;
                        }
                        gm.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        m_routeLayer.Markers.Add(gm);

                    }

                }
                else
                {
                    UI.Message(PMapMessages.E_PCHK_NOROUTE);

                }
                gMapControl.Refresh();
                gMapControl.ZoomAndCenterMarkers(m_routeLayer.Id);
            }
         
        }


        private void frmRouteCheck_Load(object sender, EventArgs e)
        {
            DataTable dt = m_bllRoute.GetRestZonesToDT();
            cmbRST_ID_LIST.ValueMember= "RESTZONE_IDS";
            cmbRST_ID_LIST.DisplayMember = "RESTZONE_NAMES";
            cmbRST_ID_LIST.DataSource = dt;

        }

        private void numFromNOD_ID_ValueChanged(object sender, EventArgs e)
        {
            MarkerFrom.Position = m_bllRoute.GetPointLatLng(Convert.ToInt32(numFromNOD_ID.Value));
            numLatFrom.Value = Convert.ToDecimal( MarkerFrom.Position.Lat);
            numLngFrom.Value = Convert.ToDecimal( MarkerFrom.Position.Lng);

            if (numFromNOD_ID.Value > 0 && numToNOD_ID.Value > 0)
            {
                m_boundNodes = new int[] { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) }.ToList();
            }

            UpdateControls();
        }

        private void numToNOD_ID_ValueChanged(object sender, EventArgs e)
        {
            MarkerTo.Position = m_bllRoute.GetPointLatLng(Convert.ToInt32(numToNOD_ID.Value));
            numLatTo.Value = Convert.ToDecimal(MarkerTo.Position.Lat);
            numLngTo.Value = Convert.ToDecimal(MarkerTo.Position.Lng);
            if (numFromNOD_ID.Value > 0 && numToNOD_ID.Value > 0)
            {
                m_boundNodes = new int[] { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) }.ToList();
            }
            UpdateControls();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string[] aRoute = txtDierctRoute.Text.Replace('\n', ',').Replace(' ', ',').Split(',');
                m_directRouteLayer.Routes.Clear();
                m_directRouteLayer.Markers.Clear();

                GMapRoute mr = new GMapRoute("");
                m_boundNodes.Clear();
                foreach (string point in aRoute)
                {
                    m_boundNodes.Add( Convert.ToInt32( point));
                    if (point.Length > 0)
                    {
                        PointLatLng pt = m_bllRoute.GetPointLatLng(Convert.ToInt32(point));
                        mr.Points.Add(pt);

                        GMapMarker gm = new GMarkerGoogle(pt, GMarkerGoogleType.blue_small);
                        gm.ToolTipText = point;
                        gm.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        m_directRouteLayer.Markers.Add(gm);
                    }
                }

                Pen p = new Pen(Color.Aquamarine, Global.TourLineWidthNormal);
                p.DashStyle = DashStyle.Solid;
                m_directRouteLayer.Routes.Add(mr);

                gMapControl.Refresh();
                gMapControl.ZoomAndCenterMarkers(m_directRouteLayer.Id);
                UpdateControls();
            }
            catch (Exception ee)
            {
                MessageBox.Show(String.Format(PMapMessages.E_PCHK_ERRINROUTE, ee.Message));
            }
        }

        private void tbZoom_Scroll(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnFrom_Click(object sender, EventArgs e)
        {
            dlgSelWHSDEP d = new dlgSelWHSDEP();
            if( d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblFrom.Text = d.m_XNAME;
                numFromNOD_ID.Value = d.m_NOD_ID;
                numLatFrom.Value = d.m_NOD_XPOS;
                numLngFrom.Value = d.m_NOD_YPOS;

            }
        }

        private void chkBoundary_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void txtDierctRoute_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFrom_Click(object sender, EventArgs e)
        {

        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            dlgSelWHSDEP d = new dlgSelWHSDEP();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblTo.Text = d.m_XNAME;
                numToNOD_ID.Value = d.m_NOD_ID;
                numLatTo.Value = d.m_NOD_XPOS;
                numLngTo.Value = d.m_NOD_YPOS;

            }
        }

        private void cmbRST_ID_LIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            showRoute();
        }

    }
}
