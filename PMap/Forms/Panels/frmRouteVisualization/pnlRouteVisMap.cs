using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.Forms.Base;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using PMapCore.Common;
using PMapCore.BO;
using PMapCore.Markers;
using System.Drawing.Drawing2D;
using PMapCore.Common.PPlan;
using Map.LongProcess;
using PMapCore.BLL;
using PMapCore.BO.DataXChange;

namespace PMapCore.Forms.Panels.frmRouteVisualization
{
    public partial class pnlRouteVisMap : BasePanel
    {

        private AccessMode MapAccessMode { get; set; }
        public GMapProvider MapProvider { get; set; }


        private GMapOverlay m_depotsLayer = null;
        private GMapOverlay m_fastestLayer = null;
        private GMapOverlay m_shortestLayer = null;

        private GMapMarker m_selectorMarker = null;
        private GMapMarker m_FocusedMarker = null;
        private GMapMarker m_MovedMarker = null;

        private bllRoute m_Route = null;
        private PPlanCommonVars m_PPlanCommonVars;
        public pnlRouteVisMap(PPlanCommonVars p_PPlanCommonVars)
        {
            InitializeComponent();
            gMapControl.CacheLocation = PMapIniParams.Instance.MapCacheDB;
            m_PPlanCommonVars = p_PPlanCommonVars;
            m_Route = new bllRoute(PMapCommonVars.Instance.CT_DB);

            init();
        }

        private void init()
        {
            try
            {
                InitPanel();


                if (!DesignMode)
                {


                    gMapControl.Manager.Mode = PMapIniParams.Instance.MapCacheMode;
                    gMapControl.MapProvider = PMapCommonVars.Instance.MapProvider;
                    gMapControl.CacheLocation = PMapIniParams.Instance.MapCacheDB;

                    gMapControl.MinZoom = Global.DefMinZoom;
                    gMapControl.MaxZoom = Global.DefMaxZoom;
                    gMapControl.Zoom = RouteVisCommonVars.Instance.Zoom;
                    gMapControl.Position = RouteVisCommonVars.Instance.CurrentPosition;


                    if (m_fastestLayer == null)
                    {
                        m_fastestLayer = new GMapOverlay("m_fastestLayer");
                        m_fastestLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_fastestLayer);
                    }
                    else
                        m_fastestLayer.Clear();



                    if (m_shortestLayer == null)
                    {
                        m_shortestLayer = new GMapOverlay("m_shortestLayer");
                        m_shortestLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_shortestLayer);
                    }
                    else
                        m_shortestLayer.Clear();


                    if (m_depotsLayer == null)
                    {
                        m_depotsLayer = new GMapOverlay("m_depotsLayer");
                        m_depotsLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_depotsLayer);
                    }
                    else
                        m_depotsLayer.Clear();

                    if (RouteVisCommonVars.Instance.lstDetails.Count > 0)
                    {

                        foreach (RouteVisCommonVars.CRouteVisDetails detail in RouteVisCommonVars.Instance.lstDetails[RouteVisCommonVars.TY_SHORTEST].Details)
                        {
                            GMapRoute r = new GMapRoute(detail.Route.Route.Points, detail.Route.Route.Name);

                            Pen pen = new Pen(Util.GetSemiTransparentColor(Color.Blue), Global.TourLineWidthNormal);
                            pen.DashStyle = detail.RouteSectionType == boXRouteSection.ERouteSectionType.Empty ? DashStyle.DashDot : DashStyle.Solid;
                            r.Stroke = pen;
                            m_shortestLayer.Routes.Add(r);
                        }

                        foreach (RouteVisCommonVars.CRouteVisDetails detail in RouteVisCommonVars.Instance.lstDetails[RouteVisCommonVars.TY_FASTEST].Details)
                        {
                            GMapRoute r = new GMapRoute(detail.Route.Route.Points, detail.Route.Route.Name);

                            Pen pen = new Pen(Util.GetSemiTransparentColor(Color.Red), Global.TourLineWidthNormal);
                            pen.DashStyle = detail.RouteSectionType == boXRouteSection.ERouteSectionType.Empty ? DashStyle.DashDot : DashStyle.Solid;
                            r.Stroke = pen;
                            m_fastestLayer.Routes.Add(r);
                        }

                        m_MovedMarker = null;
                    }

                    foreach (RouteVisCommonVars.CRouteDepots rtDep in RouteVisCommonVars.Instance.lstRouteDepots)
                    {

                        boPlanTourPoint tp = new boPlanTourPoint();
                        tp.NOD_NAME = rtDep.Depot.DEP_NAME;
                        tp.ID = rtDep.Depot.ID;
                        PPlanMarker mrkTourPoint = new PPlanMarker(rtDep.Depot.Position, Color.YellowGreen, tp);
                        mrkTourPoint.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                        mrkTourPoint.Size = new System.Drawing.Size(20, 20);
                        mrkTourPoint.ToolTipText = rtDep.Depot.DEP_NAME;
                        m_depotsLayer.Markers.Add(mrkTourPoint);
                    }


                    setToolTipMode();
                    gMapControl.ZoomAndCenterMarkers(m_depotsLayer.Id);

                    UpdateControls(new PointLatLng());
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }


        private void UpdateControls(PointLatLng p_mousePosition)
        {

            if (p_mousePosition != null)
            {
                lblCurrLat.Text = p_mousePosition.Lat.ToString("0.0000000");
                lblCurrLng.Text = p_mousePosition.Lng.ToString("0.0000000");
            }
        }

        public void RefreshPanel(RouteVisEventArgs p_evtArgs)
        {
            switch (p_evtArgs.EventMode)
            {
                case eRouteVisEventMode.ReInit:
                    init();
                    break;
                case eRouteVisEventMode.ChgZoom:
                    gMapControl.OnMapZoomChanged -= new MapZoomChanged(gMapControl_OnMapZoomChanged);
                    gMapControl.Zoom = RouteVisCommonVars.Instance.Zoom;
                    gMapControl.Refresh();
                    gMapControl.OnMapZoomChanged += new MapZoomChanged(gMapControl_OnMapZoomChanged);
                    break;
                case eRouteVisEventMode.ChgTooltipMode:
                    setToolTipMode();
                    gMapControl.Refresh();
                    break;
                case eRouteVisEventMode.ChgRouteVisible:

                    foreach (var rt in m_shortestLayer.Routes)
                        rt.IsVisible = RouteVisCommonVars.Instance.lstDetails[RouteVisCommonVars.TY_SHORTEST].Visible;
                    foreach (var rt in m_fastestLayer.Routes)
                        rt.IsVisible = RouteVisCommonVars.Instance.lstDetails[RouteVisCommonVars.TY_FASTEST].Visible;
                    break;
                case eRouteVisEventMode.ChgDepotSelected:
                    int ID = -1;
                    foreach (PPlanMarker mrk in m_depotsLayer.Markers)
                    {
                        if (mrk.TourPoint.ID == RouteVisCommonVars.Instance.SelectedDepID)
                        {
                            m_FocusedMarker = mrk;
                            gMapControl.Refresh();
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void zoomChanged()
        {
            RouteVisCommonVars.Instance.Zoom = (int)(gMapControl.Zoom);
            DoNotifyDataChanged(new RouteVisEventArgs(eRouteVisEventMode.ChgZoom));
        }
        private void setToolTipMode()
        {
            foreach (var mrk in m_depotsLayer.Markers)
            {
                mrk.ToolTipMode = RouteVisCommonVars.Instance.TooltipMode;
            }
        }
        private void gMapControl_OnMapZoomChanged()
        {
            zoomChanged();
        }


        private void gMapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            m_FocusedMarker = item;
            gMapControl.Refresh();

            PPlanMarker mrk = (PPlanMarker)item;
            RouteVisCommonVars.Instance.SelectedDepID = mrk.TourPoint.ID;   //az ID-ben a DEP_ID van tárolva
            DoNotifyDataChanged(new RouteVisEventArgs(eRouteVisEventMode.ChgDepotSelected));

        }

        private void gMapControl_Paint(object sender, PaintEventArgs e)
        {
            if (m_FocusedMarker != null)
                drawFocusedItem(e.Graphics, m_FocusedMarker, Color.Orange);
        }

        private void drawFocusedItem(Graphics g, GMapMarker p_focusedItem, Color p_color)
        {

            Brush Fill = new SolidBrush(Color.FromArgb(96, p_color.R, p_color.G, p_color.B));
            Pen p = new Pen(p_color, 3);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            GPoint pt = gMapControl.FromLatLngToLocal(p_focusedItem.Position);
            Rectangle rect3 = new Rectangle((int)pt.X - 10, +(int)pt.Y - 10, 20, 20);
            g.DrawEllipse(p, rect3);
            g.FillEllipse(Fill, rect3);
            p.Dispose();

        }

        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            //lerakóra kattintottunk-e?
            */

            m_MovedMarker = null;
            foreach (GMapMarker m in m_depotsLayer.Markers)
            {
                if (m.IsVisible)
                {
                    if (m.LocalAreaInControlSpace.Contains(e.X, e.Y))
                    {
                        m_MovedMarker = m;
                        break;
                    }
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                m_FocusedMarker = null;
                gMapControl.Refresh();
            }
        }

        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateControls(gMapControl.FromLocalToLatLng(e.X, e.Y));

            if (e.Button == MouseButtons.Left && m_MovedMarker != null)
            {
                if (m_fastestLayer.IsVisibile)
                    m_fastestLayer.IsVisibile = false;
                if (m_shortestLayer.IsVisibile)
                    m_shortestLayer.IsVisibile = false;
                PointLatLng msPt = gMapControl.FromLocalToLatLng(e.X, e.Y);
                m_MovedMarker.Position = msPt;
                gMapControl.Refresh();
            }


        }

        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_MovedMarker != null)
            {
                m_FocusedMarker = m_MovedMarker;

                int NOD_ID = m_Route.GetNearestNOD_ID(m_MovedMarker.Position);
                m_FocusedMarker.Position = m_Route.GetPointLatLng(NOD_ID);

                PPlanMarker mrk = (PPlanMarker)m_FocusedMarker;
                RouteVisCommonVars.Instance.SelectedDepID = mrk.TourPoint.ID;   //az ID-ben a DEP_ID van tárolva
                mrk.TourPoint.NOD_ID = NOD_ID;

                RouteVisCommonVars.CRouteDepots dep = RouteVisCommonVars.Instance.lstRouteDepots.Where(i => i.Depot.ID == mrk.TourPoint.ID).SingleOrDefault();
                if (dep != null)
                {
                    dep.Depot.NOD_ID = NOD_ID;
                    boNode nod = m_Route.GetNode( NOD_ID);
                    dep.Depot.NOD_XPOS = nod.NOD_XPOS;
                    dep.Depot.NOD_YPOS = nod.NOD_YPOS;
                }

                
                RouteVisDataProcess rvdp = new RouteVisDataProcess();
                rvdp.Run();
                rvdp.ProcessForm.ShowDialog();




/*
                
                foreach( GMapMarker gm in m_depotsLayer.Markers)
                {
                    
                    PPlanMarker tp =  (PPlanMarker) gm;
                    if( tp.TourPoint.ID == mrk.TourPoint.ID)
                    {
                        gm.Position = m_FocusedMarker.Position;
                        break;
                    }
                }
                    
  */              
                m_MovedMarker = null;

                m_fastestLayer.IsVisibile = true;
                m_shortestLayer.IsVisibile = true;
                this.RefreshPanel(new RouteVisEventArgs(eRouteVisEventMode.ReInit));
            }
        }
    }
}
