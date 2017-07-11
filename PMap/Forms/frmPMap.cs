using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.IO;
using Microsoft.Win32;
using GMap.NET.Internals;
using System.Drawing.Drawing2D;
using GMap.NET.MapProviders;
using PMap.Markers;
using PMap.MapProvider;
using PMap.DB.Base;
using PMap.DB;
using PMap.LongProcess;
using PMap.LongProcess.Base;
using PMap.Route;
using PMap.BO;
using PMap.BLL;
using PMap.Localize;
using PMap.Common;
using PMap.Forms.Base;
using PMap.Common.PPlan;

namespace PMap.Forms
{

    public partial class frmPMap : BaseForm
    {
        public GMapMarker CurrentPos { get; private set; }

        
        // layers
        private GMapOverlay m_selectorLayer;
        private GMapOverlay m_routeLayer;
        private GMapOverlay m_depotsLayer;


        private bool m_isMouseDown = false;

        private int m_currZoom = Global.DefZoom;

        private bllRoute m_bllRoute;
        private bllPlan m_bllPlan;

        private const int grpShowMarkersShrink = 15;
        private PPlanCommonVars m_PPlanCommonVars = new PPlanCommonVars();

        public frmPMap(double p_lat, double p_lng, string sHint)
        {
            InitializeComponent();
            gMapControl.Position = new PointLatLng(p_lat, p_lng);
            initPMapFrom();
            CurrentPos.ToolTipText = sHint;
            CurrentPos.ToolTipMode = MarkerTooltipMode.Always;
            rdbAlways.Checked = true;
            UpdateControls();
            InitForm();

        }

        public frmPMap()
        {
            InitializeComponent();
            gMapControl.Position = new PointLatLng(46.3, 20.1);
            initPMapFrom();
            CurrentPos.ToolTipText = "";
            UpdateControls();
            InitForm();
        }


        private void initPMapFrom()
        {
            try
            {
                if (!DesignMode)
                {

                    PMapCommonVars.Instance.ConnectToDB();
                    

                    m_bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
                    m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);

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
                    m_depotsLayer = new GMapOverlay(Global.depotsLayerName);
                    gMapControl.Overlays.Add(m_depotsLayer);

                    CurrentPos = new GMarkerCross(gMapControl.Position);
                    CurrentPos.ToolTipMode = MarkerTooltipMode.Always;


                    CurrentPos.ToolTipText = PMapMessages.M_FRMPMAP_SELPOS;
                    CurrentPos = new GMarkerCross(gMapControl.Position);
                    m_selectorLayer.Markers.Add(CurrentPos);


                    gMapControl.MouseMove += new MouseEventHandler(gMapControl_MouseMove);
                    gMapControl.MouseDown += new MouseEventHandler(gMapControl_MouseDown);
                    gMapControl.MouseUp += new MouseEventHandler(gMapControl_MouseUp);
                    gMapControl.MouseDoubleClick += new MouseEventHandler(gMapControl_MouseDoubleClick);
                    gMapControl.OnMapZoomChanged += new MapZoomChanged(gMapControl_OnMapZoomChanged);

                    rdbAlways.Checked = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        void gMapControl_OnMapZoomChanged()
        {
            tbZoom.ValueChanged -=new EventHandler(tbZoom_ValueChanged);
            tbZoom.Value = (int)(gMapControl.Zoom);
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
        }

        void gMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private void UpdateControls()
        {
            lblCurrLat.Text = CurrentPos.Position.Lat.ToString("0.0000000");
            lblCurrLng.Text = CurrentPos.Position.Lng.ToString("0.0000000");
            tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
            if (tbZoom.Value != m_currZoom)
                tbZoom.Value = m_currZoom;
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);

        }

        private void tbZoom_ValueChanged(object sender, EventArgs e)
        {
            gMapControl.OnMapZoomChanged -= new MapZoomChanged(gMapControl_OnMapZoomChanged);
            gMapControl.Zoom = (tbZoom.Value);
            gMapControl.OnMapZoomChanged += new MapZoomChanged(gMapControl_OnMapZoomChanged);

        }


        public DialogResult ShowMap()
        {
            DialogResult res = DialogResult.OK;
            try
            {
                grpMarkerTooltipMode.Visible = false;
                grpDepots.Visible = false;
                res = this.ShowDialog();
            }
            catch (Exception e)
            {
                throw e;
            }
            return res;
        }

        public bool ShowRoute(List<PMapMarker> p_points, Color p_color)
        {
            try
            {
                chkShowDepots.Visible = false;
                chkShowDepots.Checked = false;
                chkShowRouteDepots.Checked = true;
                grpDepots.Size = new System.Drawing.Size(grpDepots.Size.Width, grpDepots.Size.Height - grpShowMarkersShrink);


                m_routeLayer.Routes.Clear();
                m_routeLayer.Markers.Clear();
                CurrentPos.ToolTipText = "";
                CurrentPos.ToolTipMode = MarkerTooltipMode.Never;

                rdbAlways.Checked = true;
                grpMarkerTooltipMode.Visible = true;

                if (p_points.Count() <= 1)
                    return false;

                setRouteToLayer(p_points, p_color, true);
                CurrentPos.Position = p_points[0].Position;
                UpdateControls();
                gMapControl.ZoomAndCenterRoutes(m_routeLayer.Id);
                tbZoom.Value = Convert.ToInt32(gMapControl.Zoom);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
       }
        private void setRouteToLayer(List<PMapMarker> p_points, Color p_color, bool p_addMarker)
        {

            for (int i = 0; i < p_points.Count() - 1; i++)
            {


                MapRoute route = PMapCommonVars.Instance.RoutingProvider.GetRoute(p_points[i].Position, p_points[i + 1].Position, false, false, tbZoom.Value);
                if (route != null)
                {
                    // add route
                    GMapRoute r = new GMapRoute(route.Points, route.Name);

                    Pen p = new Pen(p_color, Global.TourLineWidthNormal);
                    p.DashStyle = DashStyle.Solid;
                    r.Stroke = p;

                    m_routeLayer.Routes.Add(r);

                    if (p_addMarker)
                    {

                        GMapMarker gm;
                        if (i == 0)
                        {
                            gm = new GMarkerGoogle(p_points[i].Position, GMarkerGoogleType.green_dot);
                            gm.ToolTipMode = MarkerTooltipMode.Always;
                            gm.ToolTipText = p_points[i].Hint;
                            m_routeLayer.Markers.Add(gm);
                        }
                        gm = new GMarkerGoogle(p_points[i + 1].Position, GMarkerGoogleType.green_dot);
                        gm.ToolTipMode = MarkerTooltipMode.Always;
                        gm.ToolTipText = p_points[i + 1].Hint;
                        m_routeLayer.Markers.Add(gm);
                    }
                }

            }
        }



        public bool ShowDepots(List<PMapMarker> p_depots)
        {
            try
            {

                chkShowRouteDepots.Visible = false;
                chkShowRouteDepots.Checked = false;
                chkShowDepots.Checked = true;
                grpDepots.Size = new System.Drawing.Size(grpDepots.Size.Width, grpDepots.Size.Height - grpShowMarkersShrink);

                m_depotsLayer.Routes.Clear();
                m_depotsLayer.Markers.Clear();
                CurrentPos.ToolTipText = "";
                CurrentPos.ToolTipMode = MarkerTooltipMode.Never;


                if (p_depots.Count() <= 0)
                    return false;

                SetDepotsToLayer(p_depots);

                CurrentPos.Position = p_depots[0].Position;
                gMapControl.ZoomAndCenterMarkers(m_depotsLayer.Id);
                UpdateControls();
                tbZoom.Value = Convert.ToInt32(gMapControl.Zoom);

                rdbAlways.Checked = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        private void SetDepotsToLayer(List<PMapMarker> p_depots)
        {
            for (int i = 0; i < p_depots.Count(); i++)
            {
                if (p_depots[i].Type == MarkerType.UnPlannedDepot)
                {
                    GMarkerGoogle gmr = new GMarkerGoogle(p_depots[i].Position, GMarkerGoogleType.red);
                    gmr.ToolTipMode = MarkerTooltipMode.Always;
                    gmr.ToolTipText = p_depots[i].Hint;
                    m_depotsLayer.Markers.Add(gmr);
                }
                else
                {
                    GMarkerGoogle gmg = new GMarkerGoogle(p_depots[i].Position, GMarkerGoogleType.green);
                    gmg.ToolTipMode = MarkerTooltipMode.Always;
                    gmg.ToolTipText = p_depots[i].Hint;
                    m_depotsLayer.Markers.Add(gmg);
                }
            }
        }




        public void ShowDepotsAndRoute(int p_PLN_ID, int p_TPL_ID, Color p_color)
        {
            try
            {

                PMapCommonVars.Instance.ConnectToDB();
                

                chkShowRouteDepots.Checked = true;
                chkShowDepots.Checked = true;

                m_routeLayer.Routes.Clear();
                m_routeLayer.Markers.Clear();
                m_depotsLayer.Routes.Clear();
                m_depotsLayer.Markers.Clear();
                CurrentPos.ToolTipText = "";
                CurrentPos.ToolTipMode = MarkerTooltipMode.Never;


                SetDepotsToLayerByID(p_PLN_ID);
                setRouteToLayerByID(p_TPL_ID, p_color, true);

                UpdateControls();
                gMapControl.ZoomAndCenterRoutes(m_routeLayer.Id);
                tbZoom.Value = Convert.ToInt32(gMapControl.Zoom);

                rdbOnMouseOver.Checked = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void SetDepotsToLayerByID(int p_PLN_ID)
        {

            try
            {
                List<boPlanOrder> UnplannedOrderList = m_bllPlan.GetPlanOrders(p_PLN_ID);

                foreach (boPlanOrder rup in UnplannedOrderList)
                {
                    PointLatLng pos = new PointLatLng(rup.NOD_YPOS / Global.LatLngDivider, rup.NOD_XPOS / Global.LatLngDivider);
                    PPlanMarkerUnPlanned gm;
                    gm = new PPlanMarkerUnPlanned(pos, rup);
                    gm.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                    gm.ToolTipText = rup.DEP_NAME + "\n" + rup.ZIP_NUM.ToString() + " " + rup.ZIP_CITY + " " + rup.DEP_ADRSTREET;
                    m_depotsLayer.Markers.Add(gm);
                    gm.IsVisible = chkShowDepots.Checked;
                    gm.ToolTipMode = MarkerTooltipMode.Always;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void setRouteToLayerByID(int p_TPL_ID, Color p_color, bool p_addMarker)
        {
            try
            {
                boPlanTour tour = m_bllPlan.GetPlanTour(p_TPL_ID);
                if (tour != null)
                {
                    for (int i = 0; i < tour.TourPoints.Count - 1; i++)
                    {

                        PointLatLng start = new PointLatLng(tour.TourPoints[i].NOD_YPOS / Global.LatLngDivider, tour.TourPoints[i].NOD_XPOS / Global.LatLngDivider);
                        PointLatLng end = new PointLatLng(tour.TourPoints[i + 1].NOD_YPOS / Global.LatLngDivider, tour.TourPoints[i + 1].NOD_XPOS / Global.LatLngDivider);

                        MapRoute result = null;
                        result = m_bllRoute.GetMapRouteFromDB(tour.TourPoints[i].NOD_ID, tour.TourPoints[i + 1].NOD_ID, tour.RZN_ID_LIST, tour.TRK_WEIGHT, tour.TRK_HEIGHT, tour.TRK_WIDTH);
                        if (result != null)
                        {
                            // add route
                            GMapRoute r = new GMapRoute(result.Points, result.Name);

                            Pen pen = new Pen(Util.GetSemiTransparentColor(tour.PCOLOR), Global.TourLineWidthNormal);
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                            r.Stroke = pen;
                            m_routeLayer.Routes.Add(r);

                            if (p_addMarker)
                            {

                                PPlanMarkerFlag mrkFlag;
                                if (tour.TourPoints[i].PTP_TYPE == Global.PTP_WHSOUT)
                                {
                                    mrkFlag = new PPlanMarkerFlag(start, tour.TourPoints[i]);
                                    mrkFlag.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                                    tour.TourPoints[i].ToolTipText = tour.TourPoints[i].TIME_AND_NAME;
                                    //                            mrkFlag.Size = new System.Drawing.Size(20, 20);

                                    tour.TourPoints[i].Marker = mrkFlag;
                                    m_routeLayer.Markers.Add(mrkFlag);
                                }

                                if (tour.TourPoints[i].PTP_TYPE == Global.PTP_WHSIN)
                                {
                                    //ide csak multit]ra esetén futhat a program !!!
                                    //
                                    mrkFlag = new PPlanMarkerFlag(start, tour.TourPoints[i]);
                                    tour.TourPoints[i].Marker = mrkFlag;
                                    m_routeLayer.Markers.Add(mrkFlag);

                                }

                                if (tour.TourPoints[i + 1].PTP_TYPE == Global.PTP_TPOINT)
                                {
                                    PPlanMarker mrkTourPoint = new PPlanMarker(end, tour.PCOLOR, tour.TourPoints[i]);
                                    mrkTourPoint.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                                    mrkTourPoint.Size = new System.Drawing.Size(20, 20);

                                    tour.TourPoints[i + 1].ToolTipText = tour.TourPoints[i + 1].DEP_CODE + "  ";
                                    tour.TourPoints[i + 1].ToolTipText += tour.TourPoints[i + 1].TIME_AND_NAME;

                                    tour.TourPoints[i + 1].Marker = mrkTourPoint;
                                    m_routeLayer.Markers.Add(mrkTourPoint);

                                }
                            }

                        }

                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void ClearAll()
        {
            m_routeLayer.Routes.Clear();
            m_routeLayer.Markers.Clear();
            m_depotsLayer.Routes.Clear();
            m_depotsLayer.Markers.Clear();
            CurrentPos.ToolTipText = "";
            CurrentPos.ToolTipMode = MarkerTooltipMode.Never;
        }


        private void rdbNever_CheckedChanged(object sender, EventArgs e)
        {
            foreach(  GMapMarker gm in m_routeLayer.Markers )
            {
                gm.ToolTipMode = MarkerTooltipMode.Never;
            }
            foreach (GMapMarker gm in m_depotsLayer.Markers)
            {
                gm.ToolTipMode = MarkerTooltipMode.Never;
            }
            gMapControl.Refresh();
        }

        private void rdbOnMouseOver_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GMapMarker gm in m_routeLayer.Markers)
            {
                gm.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            }
            foreach (GMapMarker gm in m_depotsLayer.Markers)
            {
                gm.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            }
            gMapControl.Refresh();
        }

        private void rdbAlways_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GMapMarker gm in m_routeLayer.Markers)
            {
                gm.ToolTipMode = MarkerTooltipMode.Always;
            }
            foreach (GMapMarker gm in m_depotsLayer.Markers)
            {
                gm.ToolTipMode = MarkerTooltipMode.Always;
            }
            gMapControl.Refresh();
        }



        private void chkShowRouteDepots_CheckedChanged(object sender, EventArgs e)
        {
            grpMarkerTooltipMode.Visible = chkShowRouteDepots.Checked || chkShowDepots.Checked;
            foreach (GMapMarker gm in m_routeLayer.Markers)
            {
                gm.IsVisible = chkShowRouteDepots.Checked;
            }
            gMapControl.Refresh();

        }

        private void chkShowDepots_CheckedChanged(object sender, EventArgs e)
        {
            grpMarkerTooltipMode.Visible = chkShowRouteDepots.Checked || chkShowDepots.Checked;
            foreach (GMapMarker gm in m_depotsLayer.Markers)
            {
                gm.IsVisible = chkShowDepots.Checked;
            }
            gMapControl.Refresh();
        }


    }
}
