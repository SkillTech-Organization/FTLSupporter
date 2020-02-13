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
using PMapCore.MapProvider;
using System.Drawing.Drawing2D;
using PMapCore.Route;
using PMapCore.BO;
using PMapCore.Strings;
using PMapCore.BLL;
using PMapCore.Common;
using System.Globalization;
using System.Runtime.ExceptionServices;
using PMapUI.Common;

namespace PMapTestApp
{
    public partial class frmRouteCheck : Form
    {
        private GMapMarker CurrentPos;
        private GMapMarker MarkerFrom;
        private GMapMarker MarkerTo;
        private Dictionary<CRoutePars, boRoute> m_routes = new Dictionary<CRoutePars, boRoute>();

        // layers
        private GMapOverlay m_selectorLayer;
        private GMapOverlay m_routeLayer;
        private GMapOverlay m_directRouteLayer;
        private GMapOverlay m_boundaryLayer;
        private GMapOverlay m_edgesLayer;
        private GMapOverlay m_edgesMarkerLayer;

        private bool m_isMouseDown = false;
        private int m_currZoom = Global.DefZoom;
        private const int grpShowMarkersShrink = 15;

        private List<int> m_boundNodes = new List<int>();

        private bllRoute m_bllRoute;
        private bllDepot m_bllDepot;

        public frmRouteCheck()
        {
            InitializeComponent();
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

                    gMapControl.CacheLocation = PMapIniParams.Instance.MapCacheDB;
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

                    m_edgesLayer = new GMapOverlay("Edges");
                    gMapControl.Overlays.Add(m_edgesLayer);

                    m_edgesMarkerLayer = new GMapOverlay("EdgesMarker");
                    gMapControl.Overlays.Add(m_edgesMarkerLayer);


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
                    ckhShowEdges.Checked = false;

                   var from = new PointLatLng(46.2416455, 20.1600394);
                    var to  = new PointLatLng(46.2425501, 20.2815699);
                    numLatFrom.Value = Convert.ToDecimal(from.Lat);
                    numLngFrom.Value = Convert.ToDecimal(from.Lng);
                    numLatTo.Value = Convert.ToDecimal(to.Lat);
                    numLngTo.Value = Convert.ToDecimal(to.Lng);
                    //chgFrom();
                    //chgTo();

                    CurrentPos.Position = from;
                    lblCurrLat.Text = CurrentPos.Position.Lat.ToString("0.0000000");
                    lblCurrLng.Text = CurrentPos.Position.Lng.ToString("0.0000000");
                    lblDst.Text = Util.GetDistanceOfTwoPoints_Meter(from.Lng, from.Lat, to.Lng, to.Lat).ToString(Global.NUMFORMAT);

                    MarkerFrom.Position = from;
                    MarkerTo.Position = to;
                    numFromNOD_ID.Value = 0;
                    numToNOD_ID.Value = 0;

                    gMapControl.ZoomAndCenterMarkers(m_selectorLayer.Id);

                    UpdateControls();
                    /*
                    numFromNOD_ID.Value =46087;
                    numToNOD_ID.Value = 15;
                    */

                }
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }

        }

        private void UpdateControls()
        {
            tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
            if (tbZoom.Value != m_currZoom)
                tbZoom.Value = m_currZoom;
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
            button1.Enabled = numToNOD_ID.Value > 0 && numFromNOD_ID.Value > 0;
            button2.Enabled = numToNOD_ID.Value > 0 && numFromNOD_ID.Value > 0;
            cmbRST_ID_LIST.Enabled = numToNOD_ID.Value > 0 && numFromNOD_ID.Value > 0 && m_routes != null && m_routes.Count > 0;

            m_boundaryLayer.Polygons.Clear();
            m_boundNodes = new int[] { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) }.ToList();

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
            lblDst.Text = Util.GetDistanceOfTwoPoints_Meter(MarkerFrom.Position.Lng, MarkerFrom.Position.Lat, MarkerTo.Position.Lng, MarkerTo.Position.Lat).ToString(Global.NUMFORMAT);


        }

        void gMapControl_OnMapZoomChanged()
        {
            tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
            tbZoom.Value = (int)(gMapControl.Zoom);
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
        }

        void gMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng pt = gMapControl.FromLocalToLatLng(e.X, e.Y);

                setFiledTriggersOff();
                if (rdbFrom.Checked)
                {
                    MarkerFrom.Position = pt;

                    numLatFrom.Value = Convert.ToDecimal(pt.Lat);
                    numLngFrom.Value = Convert.ToDecimal(pt.Lng);
                }
                else
                {
                    MarkerTo.Position = pt;
                    numLatTo.Value = Convert.ToDecimal(pt.Lat);
                    numLngTo.Value = Convert.ToDecimal(pt.Lng);
                }
                setFiledTriggersOn();
                //    gMapControl.ZoomAndCenterMarkers(m_selectorLayer.Id);
            }
            if (e.Button == MouseButtons.Middle)
            {

                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                PointLatLng pt2 = gMapControl.FromLocalToLatLng(e.X, e.Y);
                var res = bllDepot.GetWeightsNear(pt2.Lng * Global.LatLngDivider, pt2.Lat * Global.LatLngDivider);
                UI.Message("Közeli korlátozások:" + res);
            }
        }

        private void setFromToMap()
        {

            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

            //      int NOD_ID = m_bllRoute.GetNearestNOD_ID(MarkerFrom.Position);
            int diff = 0;
         //   int NOD_ID = RouteData.Instance.GetNearestNOD_ID(MarkerFrom.Position, out diff);
            int NOD_ID = RouteData.Instance.GetNearestReachableNOD_IDForTruck(MarkerFrom.Position, (string)cmbRST_ID_LIST.SelectedValue,  (int)numWeigtht.Value, 0, 0, out diff);
            if (NOD_ID > 0)
            {

                setFiledTriggersOff();

                numFromNOD_ID.Value = NOD_ID;

                boNode nd = m_bllRoute.GetNode(NOD_ID);
                MarkerFrom.Position = new PointLatLng(nd.NOD_YPOS/ Global.LatLngDivider, nd.NOD_XPOS / Global.LatLngDivider);

                numLatFrom.Value = Convert.ToDecimal(MarkerFrom.Position.Lat);
                numLngFrom.Value = Convert.ToDecimal(MarkerFrom.Position.Lng);
                setFiledTriggersOn();
                UpdateControls();
           //     UI.Message("Diff:"+ diff.ToString());

            }
            else
            {
                UI.Message("Nincs igazítás !");
            }

        }
        private void setToToMap()
        {
            int NOD_ID = RouteData.Instance.GetNearestReachableNOD_IDForTruck(MarkerTo.Position, (string)cmbRST_ID_LIST.SelectedValue, (int)numWeigtht.Value, 0, 0);
      //      int NOD_ID = m_bllRoute.GetNearestNOD_ID(MarkerTo.Position);
            if (NOD_ID > 0)
            {
                setFiledTriggersOff();
                numToNOD_ID.Value = NOD_ID;

                boNode nd = m_bllRoute.GetNode(NOD_ID);
                MarkerTo.Position = new PointLatLng(nd.NOD_YPOS / Global.LatLngDivider, nd.NOD_XPOS / Global.LatLngDivider);

                numLatTo.Value = Convert.ToDecimal(MarkerTo.Position.Lat);
                numLngTo.Value = Convert.ToDecimal(MarkerTo.Position.Lng);

                setFiledTriggersOn();
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
            CurrentPos.Position = gMapControl.FromLocalToLatLng(e.X, e.Y);

            lblCurrLat.Text = CurrentPos.Position.Lat.ToString("0.0000000");
            lblCurrLng.Text = CurrentPos.Position.Lng.ToString("0.0000000");

            if (e.Button == MouseButtons.Left && m_isMouseDown)
            {
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
            chgFrom();
          }

        private void chgFrom ()
        {
            this.numFromNOD_ID.ValueChanged -= new System.EventHandler(this.numFromNOD_ID_ValueChanged);

            MarkerFrom.Position = new PointLatLng(Convert.ToDouble(numLatFrom.Value, CultureInfo.InvariantCulture), Convert.ToDouble(numLngFrom.Value, CultureInfo.InvariantCulture));
            numFromNOD_ID.Value = 0;
            /*  
            int NOD_ID = m_bllRoute.GetNearestNOD_ID(MarkerFrom.Position);
            if (NOD_ID > 0)
                numFromNOD_ID.Value = NOD_ID;
            else
                numFromNOD_ID.Value = 0;
            */
            this.numFromNOD_ID.ValueChanged += new System.EventHandler(this.numFromNOD_ID_ValueChanged);
            gMapControl.ZoomAndCenterMarkers(m_selectorLayer.Id);
            UpdateControls();

        }

        private void numLatTo_ValueChanged(object sender, EventArgs e)
        {
            chgTo();
        }
        private void chgTo()
        {
            this.numToNOD_ID.ValueChanged -= new System.EventHandler(this.numToNOD_ID_ValueChanged);
            MarkerTo.Position = new PointLatLng(Convert.ToDouble(numLatTo.Value, CultureInfo.InvariantCulture), Convert.ToDouble(numLngTo.Value, CultureInfo.InvariantCulture));
            numToNOD_ID.Value = 0;
            /*
            int NOD_ID = m_bllRoute.GetNearestNOD_ID(MarkerTo.Position);
            if (NOD_ID > 0)
                numToNOD_ID.Value = NOD_ID;
            else
                numToNOD_ID.Value = 0;
            */
            this.numToNOD_ID.ValueChanged += new System.EventHandler(this.numToNOD_ID_ValueChanged);
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
                dlgCheckRouteDetails rd = new dlgCheckRouteDetails(m_routes, (int)numWeigtht.Value);
                rd.ShowDialog(this);

            }


        }

        private void CalcAndShowRoute()
        {
            setFromToMap();
            setToToMap();

            PMapRoutingProvider provider = new PMapRoutingProvider();
            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);
            if (numFromNOD_ID.Value > 0 && numToNOD_ID.Value > 0)
            {
                RectLatLng boundary = new RectLatLng();
                List<int> nodes = new List<int>() { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) };
                boundary = m_bllRoute.getBoundary(nodes);

                DataTable dtRZN_ID_LIST = (DataTable)cmbRST_ID_LIST.DataSource;

                //A TestApp-ban egyelőre csak a súlykorlátozásokkal foglalkozunk
                //
                List<CRoutePars> lstRoutePars = dtRZN_ID_LIST.AsEnumerable().Select(x => new CRoutePars() { RZN_ID_LIST = Util.getFieldValue<string>(x, "RESTZONE_IDS") }).ToList();

                foreach (var rp in lstRoutePars)
                {

                    rp.Weight = (int)numWeigtht.Value;
                }

                Dictionary<string, List<int>[]> NeighborsFull = null;
                Dictionary<string, List<int>[]> NeighborsCut = null;
                RouteData.Instance.getNeigboursByBound(lstRoutePars, ref NeighborsFull, ref NeighborsCut, boundary, null);

                //összes RZN_ID_LIST-ra elkérjük az útvonalakat
                m_routes.Clear();
                foreach (var routePar in lstRoutePars)
                {
                    boRoute result = provider.GetRoute(Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value), routePar,
                        NeighborsFull[routePar.Hash],
                        PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
                        rdShortestPath.Checked ? ECalcMode.ShortestPath : ECalcMode.FastestPath);
                    m_routes.Add(routePar, result);
                }
                UpdateControls();
                showRoute();
            }

        }

        private void showRoute()
        {

            if (m_routes != null && m_routes.Count > 0)
            {
                m_routeLayer.Routes.Clear();
                m_routeLayer.Markers.Clear();

                //A TestApp-ban egyelőre csak a súlykorlátozásokkal foglalkozunk
                //
                var routePar = new CRoutePars() { RZN_ID_LIST = (string)cmbRST_ID_LIST.SelectedValue, Weight= (int)numWeigtht.Value};

                 boRoute route;


                if (m_routes.TryGetValue(routePar, out route) && route.DST_DISTANCE > 0)
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
            else
            {
                m_routeLayer.Routes.Clear();
                m_routeLayer.Markers.Clear();
            }

        }


        private void frmRouteCheck_Load(object sender, EventArgs e)
        {
            DataTable dt = m_bllRoute.GetRestZonesToDT();
            cmbRST_ID_LIST.ValueMember = "RESTZONE_IDS";
            cmbRST_ID_LIST.DisplayMember = "RESTZONE_NAMES";
            cmbRST_ID_LIST.DataSource = dt;

        }

        private void numFromNOD_ID_ValueChanged(object sender, EventArgs e)
        {
            this.numLatFrom.ValueChanged -= new System.EventHandler(this.numLatFrom_ValueChanged);
            this.numLngFrom.ValueChanged -= new System.EventHandler(this.numLngFrom_ValueChanged);

            MarkerFrom.Position = m_bllRoute.GetPointLatLng(Convert.ToInt32(numFromNOD_ID.Value));
            numLatFrom.Value = Convert.ToDecimal(MarkerFrom.Position.Lat);
            numLngFrom.Value = Convert.ToDecimal(MarkerFrom.Position.Lng);

            if (numFromNOD_ID.Value > 0 && numToNOD_ID.Value > 0)
            {
                m_boundNodes = new int[] { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) }.ToList();
            }
            this.numLatFrom.ValueChanged += new System.EventHandler(this.numLatFrom_ValueChanged);
            this.numLngFrom.ValueChanged += new System.EventHandler(this.numLngFrom_ValueChanged);

            UpdateControls();
        }

        private void numToNOD_ID_ValueChanged(object sender, EventArgs e)
        {
            this.numLatTo.ValueChanged -= new System.EventHandler(this.numLatTo_ValueChanged);
            this.numLngTo.ValueChanged -= new System.EventHandler(this.numLngTo_ValueChanged);
            MarkerTo.Position = m_bllRoute.GetPointLatLng(Convert.ToInt32(numToNOD_ID.Value));
            numLatTo.Value = Convert.ToDecimal(MarkerTo.Position.Lat);
            numLngTo.Value = Convert.ToDecimal(MarkerTo.Position.Lng);
            if (numFromNOD_ID.Value > 0 && numToNOD_ID.Value > 0)
            {
                m_boundNodes = new int[] { Convert.ToInt32(numFromNOD_ID.Value), Convert.ToInt32(numToNOD_ID.Value) }.ToList();
            }
            this.numLatTo.ValueChanged += new System.EventHandler(this.numLatTo_ValueChanged);
            this.numLngTo.ValueChanged += new System.EventHandler(this.numLngTo_ValueChanged);
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
                    m_boundNodes.Add(Convert.ToInt32(point));
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
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                setFiledTriggersOff();
                lblFrom.Text = d.m_XNAME;
                numFromNOD_ID.Value = d.m_NOD_ID;
                numLatFrom.Value = d.m_NOD_XPOS/Global.LatLngDivider;
                numLngFrom.Value = d.m_NOD_YPOS / Global.LatLngDivider;
                MarkerFrom.Position = new PointLatLng((double)numLngFrom.Value, (double)numLatFrom.Value);
                setFiledTriggersOn();

            }
        }
        private void btnTo_Click(object sender, EventArgs e)
        {
            dlgSelWHSDEP d = new dlgSelWHSDEP();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                setFiledTriggersOff();
                lblTo.Text = d.m_XNAME;
                numToNOD_ID.Value = d.m_NOD_ID ;
                numLatTo.Value = d.m_NOD_XPOS / Global.LatLngDivider;
                numLngTo.Value = d.m_NOD_YPOS / Global.LatLngDivider;
                MarkerTo.Position = new PointLatLng((double)numLngTo.Value, (double)numLatTo.Value);
                setFiledTriggersOn();

            }
        }

        private void txtDierctRoute_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFrom_Click(object sender, EventArgs e)
        {

        }

  

        private void cmbRST_ID_LIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            showRoute();
        }

        private void btnShowEdges_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                ckhShowEdges.Checked = false;
                PointLatLng p_pt = MarkerTo.Position;

                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                m_edgesLayer.Routes.Clear();
                m_edgesLayer.Markers.Clear();
                m_edgesMarkerLayer.Markers.Clear();
                ckhShowMarkers.Checked = false;

                m_edgesMarkerLayer.IsVisibile = false;

                HashSet<PointLatLng> markersPts = new HashSet<PointLatLng>();

                //                        foreach (var edg in RouteData.Instance.Edges.Where(x => new int[] { 413679 }.Contains(x.Value.ID)))
                //               foreach (var edg in RouteData.Instance.Edges.Where(x => new int[] { 383360 }.Contains(x.Value.ID)))
                //              foreach (var edg in RouteData.Instance.Edges.Where(x => x.Value.RDT_VALUE >= 3 && x.Value.EDG_ETLCODE == ""))
                // foreach (var edg in RouteData.Instance.Edges.Where(x => x.Value.RDT_VALUE == 5 && 
                //              (x.Value.EDG_STRNUM1 == "0" && x.Value.EDG_STRNUM2 == "0" && x.Value.EDG_STRNUM3 == "0" && x.Value.EDG_STRNUM4 == "0")
                //              /*&& (x.Value.ZIP_NUM_FROM == 0  && x.Value.ZIP_NUM_TO == 0)*/ ))

                //                   foreach (var edg in RouteData.Instance.Edges)

                //                foreach (var edg in RouteData.Instance.Edges.Where(x =>  x.Value.EDG_MAXWEIGHT > 0 ||  x.Value.EDG_MAXHEIGHT > 0 /*|| x.Value.EDG_MAXWIDTH > 0*/))
                // foreach (var edg in RouteData.Instance.Edges.Where(x => x.Value.ID == 507536 && x.Value.EDG_LENGTH > 15000))

  //              foreach (var edg in RouteData.Instance.Edges.Where(
  //                               w => (Math.Abs(w.Value.fromLatLng.Lng - MarkerTo.Position.Lng) + Math.Abs(w.Value.fromLatLng.Lat - MarkerTo.Position.Lat) < (double)Global.EdgeApproachHighway/2)))
             /*       foreach (var edg in RouteData.Instance.Edges.Where(
                                    w => (Math.Abs(w.Value.toLatLng.Lng - MarkerTo.Position.Lng) + Math.Abs(w.Value.toLatLng.Lat - MarkerTo.Position.Lat) < (double)Global.EdgeApproachHighway / 2)))
*/
/*
                    foreach (var edg in RouteData.Instance.Edges.Where(
                                     w => int.Parse( w.Value.EDG_STRNUM4 ) > 1000))
*/
                foreach (var edg in RouteData.Instance.Edges.Where(
                            w =>(Math.Abs(w.Value.fromLatLng.Lng - p_pt.Lng) + Math.Abs(w.Value.fromLatLng.Lat - p_pt.Lat) <
                                (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                                ((double)Global.EdgeApproachCity*4 / Global.LatLngDivider) : ((double)Global.EdgeApproachHighway*2 / Global.LatLngDivider)) ||
                                Math.Abs(w.Value.toLatLng.Lng - p_pt.Lng) + Math.Abs(w.Value.toLatLng.Lat - p_pt.Lat) <
                                (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                                ((double)Global.EdgeApproachCity*4 / Global.LatLngDivider) : ((double)Global.EdgeApproachHighway*2 / Global.LatLngDivider)))
                                ))
                 
                //              (x.Value.EDG_STRNUM1 != "0" || x.Value.EDG_STRNUM2 != "0" || x.Value.EDG_STRNUM3 != "0" || x.Value.EDG_STRNUM4 != "0")))
                    {

                        var edge = edg.Value;

                    GMapMarker gm = null;
                    if (markersPts.Contains(edge.fromLatLng))
                    {
                        gm = m_edgesMarkerLayer.Markers.FirstOrDefault(x => x.Position == edge.fromLatLng);
                        gm.ToolTipText += "\n";

                    }
                    else
                    {
                        gm = new GMarkerGoogle(edge.fromLatLng, GMarkerGoogleType.blue_small);
                        m_edgesMarkerLayer.Markers.Add(gm);
                    }
                    gm.ToolTipText += String.Format("ID:{0} weight{1}, height:{2}, width:{3}\nname:{4}, fromNOD:{5}, toNOD:{6}", edge.ID, edge.EDG_MAXWEIGHT, edge.EDG_MAXHEIGHT, edge.EDG_MAXWIDTH, edge.EDG_NAME, edge.NOD_ID_FROM, edge.NOD_ID_TO);
                    Pen p;
                    switch ( edge.RDT_VALUE)
                    {
                        case 1:
                            p = new Pen(Color.Red, 1);
                            break;
                        case 2:
                            p = new Pen(Color.Orange, 1);
                            break;
                        case 3:
                            p = new Pen(Color.HotPink, 1);
                            break;
                        case 4:
                            p = new Pen(Color.Blue, 1);
                            break;
                        case 5:
                            p = new Pen(Color.Green, 1);
                            break;
                        case 6:
                            p = new Pen(Color.Brown, 1);
                            break;
                        case 7:
                            p = new Pen(Color.Yellow, 1);
                            break;
                        default:
                            p = new Pen(Color.Black, 1);
                            break;
                    }

                    GMapRoute r = new GMapRoute(new List<PointLatLng> { edge.fromLatLng, edge.toLatLng }, "xx");

                    r.Stroke = p;

                    m_edgesLayer.Routes.Add(r);
                }
                ckhShowEdges.Checked = true;
            }

        }

        private void ckhShowEdges_CheckedChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                m_edgesLayer.IsVisibile = ckhShowEdges.Checked;
                gMapControl.Refresh();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            setFromToMap();
            setToToMap();
        }

        private void ckhShowMarkers_CheckedChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                m_edgesMarkerLayer.IsVisibile = ckhShowMarkers.Checked;
                gMapControl.Refresh();
            }

        }

        private void numLngFrom_ValueChanged(object sender, EventArgs e)
        {
            chgFrom();
         }

        private void numLngTo_ValueChanged(object sender, EventArgs e)
        {
            chgTo();

        }

        private void frmRouteCheck_Shown(object sender, EventArgs e)
        {
            chgFrom();
            chgTo();

        }

        private void chkBoundary_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateControls();

        }

        private void setFiledTriggersOff()
        {
            this.numLatFrom.ValueChanged -= new System.EventHandler(this.numLatFrom_ValueChanged);
            this.numLngFrom.ValueChanged -= new System.EventHandler(this.numLngFrom_ValueChanged);
            this.numFromNOD_ID.ValueChanged -= new System.EventHandler(this.numFromNOD_ID_ValueChanged);

            this.numLatTo.ValueChanged -= new System.EventHandler(this.numLatTo_ValueChanged);
            this.numLngTo.ValueChanged -= new System.EventHandler(this.numLngTo_ValueChanged);
            this.numToNOD_ID.ValueChanged -= new System.EventHandler(this.numToNOD_ID_ValueChanged);
        }

        private void setFiledTriggersOn()
        {
            this.numLatFrom.ValueChanged += new System.EventHandler(this.numLatFrom_ValueChanged);
            this.numLngFrom.ValueChanged += new System.EventHandler(this.numLngFrom_ValueChanged);
            this.numFromNOD_ID.ValueChanged += new System.EventHandler(this.numFromNOD_ID_ValueChanged);

            this.numLatTo.ValueChanged += new System.EventHandler(this.numLatTo_ValueChanged);
            this.numLngTo.ValueChanged += new System.EventHandler(this.numLngTo_ValueChanged);
            this.numToNOD_ID.ValueChanged += new System.EventHandler(this.numToNOD_ID_ValueChanged);
        }

        private void btnWeight_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                ckhShowEdges.Checked = false;
                PointLatLng p_pt = MarkerTo.Position;

                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                m_edgesLayer.Routes.Clear();
                m_edgesLayer.Polygons.Clear();
                m_edgesLayer.Markers.Clear();
                m_edgesMarkerLayer.Markers.Clear();

                m_edgesMarkerLayer.IsVisibile = true;
                
                HashSet<PointLatLng> markersPts = new HashSet<PointLatLng>();

                foreach (var edg in RouteData.Instance.Edges.Where(
                            w => w.Value.EDG_MAXWEIGHT > 0 ))
                {

                    var edge = edg.Value;

                    Pen p;
                    switch (edge.EDG_MAXWEIGHT)
                    {
                        case 0:
                            p = new Pen(Color.Green, 1);
                            break;
                        case Global.RST_WEIGHT35:
                            p = new Pen(Color.Red, 1);
                            break;
                        case Global.RST_WEIGHT75:
                            p = new Pen(Color.HotPink, 1);
                            break;
                        case Global.RST_WEIGHT120:
                            p = new Pen(Color.Yellow, 1);
                            break;
                       
                        default:
                            p = new Pen(Color.Tomato, 1);
                            break;
                    }

                    /*
                    GMapRoute r = new GMapRoute(new List<PointLatLng> { edge.fromLatLng, edge.toLatLng }, "xx");

                    r.Stroke = p;

                    m_edgesLayer.Routes.Add(r);
                    */
                    GMapPolygon px = new GMapPolygon(new List<PointLatLng> { edge.fromLatLng, edge.toLatLng }, "xx");
                    px.Stroke = p;
                    m_edgesLayer.Polygons.Add(px);

                    GMapMarker gm = null;
                    if (markersPts.Contains(edge.fromLatLng))
                    {
                        gm = m_edgesMarkerLayer.Markers.FirstOrDefault(x => x.Position == edge.fromLatLng);
                        gm.ToolTipText += "\n";

                    }
                    else
                    {
                        gm = new GMarkerGoogle(edge.fromLatLng, GMarkerGoogleType.blue_small);
                        m_edgesMarkerLayer.Markers.Add(gm);
                    }
                    gm.ToolTipText += String.Format("ID:{0} weight{1}, height:{2}, width:{3}\nname:{4}, fromNOD:{5}, toNOD:{6}", edge.ID, edge.EDG_MAXWEIGHT, edge.EDG_MAXHEIGHT, edge.EDG_MAXWIDTH, edge.EDG_NAME, edge.NOD_ID_FROM, edge.NOD_ID_TO);

                }
                ckhShowEdges.Checked = true;
            }

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void gMapControl_Load(object sender, EventArgs e)
        {

        }
    }
}
