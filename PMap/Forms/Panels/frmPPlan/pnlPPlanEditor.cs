using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using GMap.NET;
using GMap.NET.WindowsForms;
using PMap.DB;
using GMap.NET.WindowsForms.Markers;
using PMap.LongProcess.Base;
using PMap.LongProcess;
using System.Drawing.Drawing2D;
using PMap.Markers;
using GMap.NET.ObjectModel;
using GMap.NET.MapProviders;
using PMap.BO;
using PMap.Localize;
using PMap.BLL;
using PMap.Common;
using PMap.Forms.Base;
using PMap.Common.PPlan;
using PMap.DB.Base;
using PMap.Route;

namespace PMap.Forms.Panels.frmPPlan
{
    public partial class pnlPPlanEditor : BasePanel
    {

        private class EditedRoute
        {
            public boPlanTour Tour { get; set; }
            public boPlanTourPoint TpRouteStart { get; set; }
            public string ItemID
            {
                get { return Tour.ID.ToString("000000000000#") + TpRouteStart.ID.ToString("000000000000#"); }
            }
        }

        private PointLatLng m_mousePosition;
        private GMapOverlay m_baseLayer = null;
        private GMapOverlay m_unplannedLayer = null;
        private GMapOverlay m_editorLayer = null;
        private GMapOverlay m_checkMapLayer = null;


        private GMapMarker m_selectorMarker = null;

        private bool m_EditMode = false;
        private EditedRoute m_EditedRoute = null;
        private boPlanTourPoint m_EditedTourPoint = null;
        private boPlanOrder m_EditedUnplannedOrder = null;
        private bool m_CheckMode = false;

        private bool m_MouseMoved = false;
        private GMapMarker m_ToolTipedMarker = null;

        private bllPlanEdit m_bllPlanEdit;
        private bllPlan m_bllPlan;
        private PlanEditFuncs m_PlanEditFuncs;


        private PPlanCommonVars m_PPlanCommonVars;


        public pnlPPlanEditor(PPlanCommonVars p_PPlanCommonVars)
        {
            InitializeComponent();
            m_PPlanCommonVars = p_PPlanCommonVars;
            Init();
        }

        public bool EditMode
        {
            get { return m_EditMode; }

        }

        public boPlanTourPoint EditedTourPoint
        {
            get { return m_EditedTourPoint; }

        }


        #region inicializálás


        private void Init()
        {
            try
            {
                InitPanel();

                m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
                m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
                m_PlanEditFuncs = new PlanEditFuncs(this, m_PPlanCommonVars);

                if (!DesignMode)
                {

                    m_EditedRoute = null;
                    m_EditedTourPoint = null;
                    m_EditedUnplannedOrder = null;

                    clearMarkerTooltip();

                    m_EditMode = false;
                    m_CheckMode = false;

                    gMapControl.Manager.Mode = PMapCommonVars.Instance.MapAccessMode;
                    gMapControl.MapProvider = PMapCommonVars.Instance.MapProvider;
                    gMapControl.CacheLocation = PMapIniParams.Instance.MapCacheDB;


                    gMapControl.MinZoom = Global.DefMinZoom;
                    gMapControl.MaxZoom = Global.DefMaxZoom;
                    gMapControl.Zoom = m_PPlanCommonVars.Zoom;
                    gMapControl.Position = m_PPlanCommonVars.CurrentPosition;

                    if (m_unplannedLayer == null)
                    {
                        m_unplannedLayer = new GMapOverlay("unplannedLayer");
                        m_unplannedLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_unplannedLayer);
                    }
                    else
                    {
                        m_unplannedLayer.Markers.Clear();
                    }

                    if (m_editorLayer == null)
                    {
                        m_editorLayer = new GMapOverlay("editorLayer");
                        m_editorLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_editorLayer);
                    }
                    else
                    {
                        m_editorLayer.Markers.Clear();
                        m_editorLayer.Routes.Clear();
                        m_editorLayer.Polygons.Clear();
                    }

                    if (m_baseLayer == null)
                    {
                        m_baseLayer = new GMapOverlay("baseLayer");
                        m_baseLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_baseLayer);
                    }
                    else
                    {
                        m_baseLayer.Markers.Clear();
                        m_baseLayer.Routes.Clear();
                        m_baseLayer.Polygons.Clear();
                    }


                    if (m_checkMapLayer == null)
                    {
                        m_checkMapLayer = new GMapOverlay("checkMapLayer");
                        m_checkMapLayer.IsVisibile = true;
                        gMapControl.Overlays.Add(m_checkMapLayer);
                    }
                    else
                    {
                        m_checkMapLayer.Markers.Clear();
                        m_checkMapLayer.Routes.Clear();
                        m_checkMapLayer.Polygons.Clear();
                    }

                    if (m_selectorMarker == null)
                    {
                        m_selectorMarker = new GMarkerGoogle(gMapControl.Position, GMarkerGoogleType.red);
                    }

                    m_selectorMarker.IsVisible = false;
                    m_editorLayer.Markers.Add(m_selectorMarker);

                    if (m_PPlanCommonVars.FocusedUnplannedOrder != null)
                    {
                        m_PPlanCommonVars.FocusedUnplannedOrder.Marker.IsVisible = false;
                    }
                    m_PPlanCommonVars.FocusedUnplannedOrder = null;

                    FullRefreshMap();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        /// <summary>
        /// Egy túra útvonalának térképre rakása
        /// </summary>
        /// <param name="p_genTPL_ID"></param>
        /// <returns></returns>
        public GetRoutePathProcess.eCompleteCode GetRoutesForMap(int p_genTPL_ID)
        {
            GetRoutePathProcess.eCompleteCode CreateCompleted = GetRoutePathProcess.eCompleteCode.OK;
            try
            {


                if (m_PPlanCommonVars.TourList.Count == 0)
                {
                    //layereket alaphelyzetbe
                    gMapControl.Overlays.Clear();
                    gMapControl.Overlays.Add(m_baseLayer);
                    gMapControl.Overlays.Add(m_unplannedLayer);
                    gMapControl.Overlays.Add(m_editorLayer);
                    gMapControl.Overlays.Add(m_checkMapLayer);
                    return GetRoutePathProcess.eCompleteCode.OK;
                }

                gMapControl.SuspendLayout();
                gMapControl.HoldInvalidation = true;


                BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, m_PPlanCommonVars.TourList.Count - 1, "Túrarészletező", true);
                GetRoutePathProcess rpp = new GetRoutePathProcess(pd, m_PPlanCommonVars.TourList, gMapControl, m_baseLayer, p_genTPL_ID, m_PPlanCommonVars);

                foreach (GMapOverlay ov in gMapControl.Overlays)
                {
                    if (ov != m_unplannedLayer && ov != m_baseLayer && ov != m_editorLayer)
                    {
                        var linq = (from o in m_PPlanCommonVars.TourList
                                    where o.Layer == ov
                                    select o);
                        if (linq.Count<boPlanTour>() > 0)
                        {
                            rpp.Overlays.Add(ov);
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        rpp.Overlays.Add(ov);
                    }
                }

                gMapControl.Overlays.Clear();


                //Lehet, hogy sokáig fog tartani, ezért LongProcess-ben végezzül e feltöltést

                DateTime dtStart = DateTime.Now;
                TimeSpan tspDiff;

                Util.Log2File("PPlanEditor.GetRoutePaths() START " + Util.GetSysInfo() + " pontok száma:" + m_PPlanCommonVars.TourList.Count.ToString());

                rpp.Run();
                pd.ShowDialog();

                if (rpp.CompleteCode == GetRoutePathProcess.eCompleteCode.UserBreak)
                {
                    UI.Message(PMapMessages.M_PEDIT_ROUTECALCABORTED);
                    Application.Exit();
                }

                if (rpp.CompleteCode == GetRoutePathProcess.eCompleteCode.NoRoute)
                {
                    UI.Message(PMapMessages.E_PEDIT_NOGETROUTES);
                    Application.Exit();
                }




                foreach (GMapOverlay ov in rpp.Overlays)
                    gMapControl.Overlays.Add(ov);

                gMapControl.ResumeLayout();
                gMapControl.Refresh();


                tspDiff = DateTime.Now - dtStart;
                Util.Log2File("PPlanEditor.GetRoutePaths() " + Util.GetSysInfo() + " Időtartam:" + tspDiff.ToString() + " Átlag(ms):" + (tspDiff.Duration().TotalMilliseconds / m_PPlanCommonVars.TourList.Count));
            }
            catch (Exception e)
            {


                gMapControl.ResumeLayout();
                gMapControl.Refresh();

                throw;
            }
            return CreateCompleted;
        }

        public void CreateUnPlannedMarkers()
        {
            try
            {
                foreach (boPlanOrder rup in m_PPlanCommonVars.PlanOrderList)
                {
                    if (rup.PTP_ID == 0)            //Tervezetlen-e a megrendelés?
                    {

                        PointLatLng pos = new PointLatLng(rup.NOD_YPOS / Global.LatLngDivider, rup.NOD_XPOS / Global.LatLngDivider);

                        PPlanMarkerUnPlanned gm;
                        gm = new PPlanMarkerUnPlanned(pos, rup);
                        gm.ToolTipMode = m_PPlanCommonVars.TooltipMode;

                        //                gm.PMapToolTipText = rup.DEP_NAME + "\n" + rup.ZIP_NUM.ToString() + " " + rup.ZIP_CITY + " " + rup.DEP_ADRSTREET;
                        //                gm.ToolTipText = "";

                        //                gm.Size = new System.Drawing.Size(20, 20);
                        gm.IsVisible = m_PPlanCommonVars.ShowUnPlannedDepots;
                        m_baseLayer.Markers.Add(gm);
                        m_unplannedLayer.Markers.Add(gm);
                        rup.Marker = gm;
                    }
                }
            }
            catch (Exception e)
            {


                gMapControl.ResumeLayout();
                gMapControl.Refresh();

                throw;
            }

        }

        public void ClearUnpannedMarkers()
        {
            foreach (GMapMarker gm in m_unplannedLayer.Markers)
            {
                m_baseLayer.Markers.Remove(gm);
            }
            m_unplannedLayer.Markers.Clear();
        }


        #endregion


        #region frissítés

        public void RefreshPanel(PlanEventArgs p_planEventArgs)
        {

            Console.WriteLine("p_planEventArgs=" + p_planEventArgs.EventMode.ToString());


            try
            {
                switch (p_planEventArgs.EventMode)
                {
                    case ePlanEventMode.ReInit:
                        this.Init();
                        break;

                    case ePlanEventMode.Refresh:
                        FullRefreshMap();

                        //                        ExitEditMode(false);
                        break;

                    case ePlanEventMode.RefreshOrders:
                        ClearUnpannedMarkers();
                        CreateUnPlannedMarkers();
                        break;

                    case ePlanEventMode.RemoveTour:
                        setEditedRoute(null);
                        setFocusedTour(null);

                        m_EditedTourPoint = null;
                        m_EditedUnplannedOrder = null;

                        RemoveTour(p_planEventArgs.Tour);
                        UpdateControls();
                        clearMarkerTooltip();
                        //                        ExitEditMode(false);
                        break;

                    case ePlanEventMode.AddTour:
                        setEditedRoute(null);
                        setFocusedTour(null);

                        m_EditedTourPoint = null;
                        m_EditedUnplannedOrder = null;

                        AddTour(p_planEventArgs.Tour);
                        UpdateControls();
                        clearMarkerTooltip();
                        //                        ExitEditMode(false);
                        break;

                    case ePlanEventMode.ChgZoom:
                        gMapControl.OnMapZoomChanged -= new MapZoomChanged(gMapControl_OnMapZoomChanged);
                        gMapControl.Zoom = m_PPlanCommonVars.Zoom;
                        gMapControl.Refresh();
                        gMapControl.OnMapZoomChanged += new MapZoomChanged(gMapControl_OnMapZoomChanged);
                        break;
                    case ePlanEventMode.ChgShowPlannedFlag:
                        foreach (boPlanTour rTour in m_PPlanCommonVars.TourList)
                        {
                            foreach (GMapMarker gm in rTour.Layer.Markers)
                            {
                                gm.IsVisible = m_PPlanCommonVars.ShowPlannedDepots;
                            }
                        }
                        m_PPlanCommonVars.FocusedPoint = null;
                        gMapControl.Refresh();
                        break;
                    case ePlanEventMode.ChgShowUnPlannedFlag:
                        foreach (GMapMarker gm in m_unplannedLayer.Markers)
                        {
                            gm.IsVisible = m_PPlanCommonVars.ShowUnPlannedDepots;
                        }
                        m_PPlanCommonVars.FocusedUnplannedOrder = null;
                        gMapControl.Refresh();
                        break;

                    case ePlanEventMode.ChgTooltipMode:
                        SetToolTipMode();
                        gMapControl.Refresh();
                        break;

                    case ePlanEventMode.ChgTourSelected:
                        p_planEventArgs.Tour.Layer.IsVisibile = p_planEventArgs.IsVisible;
                        setFocusedTour(p_planEventArgs.Tour);
                        //m_FocusedPoint = null;

                        if (p_planEventArgs.IsVisible && m_PPlanCommonVars.ZoomToSelectedPlan)
                            gMapControl.ZoomAndCenterRoutes(p_planEventArgs.Tour.Layer.Id);

                        gMapControl.Refresh();
                        break;

                    case ePlanEventMode.ChgTourColor:
                        foreach (GMapMarker mark in p_planEventArgs.Tour.Layer.Markers)
                        {
                            if (mark.GetType() == typeof(PPlanMarker))
                            {
                                PPlanMarker pplm = (PPlanMarker)mark;
                                pplm.Color = p_planEventArgs.Color;
                            }
                        }
                        /*
                        foreach (GMapRoute route in p_planEventArgs.Tour.Layer.Routes)
                        {
                            route.Stroke = new Pen(Util.GetSemiTransparentColor(p_planEventArgs.Color),
                                                    p_planEventArgs.Tour == m_FocusedTour ?  Global.TourLineWidthSelected : Global.TourLineWidthNormal);
                        }
                        */
                        setFocusedTour(p_planEventArgs.Tour);
                        m_PPlanCommonVars.FocusedPoint = null;
                        gMapControl.Refresh();
                        break;

                    case ePlanEventMode.HideAllTours:
                        setFocusedTour(null);
                        m_EditedTourPoint = null;
                        m_EditedUnplannedOrder = null;
                        gMapControl.Refresh();

                        break;

                    case ePlanEventMode.ShowAllTours:
                        setFocusedTour(null);
                        m_EditedTourPoint = null;
                        m_EditedUnplannedOrder = null;
                        gMapControl.Refresh();

                        break;


                    case ePlanEventMode.ChgFocusedTour:
                        resetEditMode();
                        if (p_planEventArgs.Tour != null)
                        {
                            if (m_PPlanCommonVars.FocusedTour != p_planEventArgs.Tour)
                                if (p_planEventArgs.Tour.PSelect)
                                {
                                    setFocusedTour(p_planEventArgs.Tour);
                                    if (m_PPlanCommonVars.ZoomToSelectedPlan)
                                        gMapControl.ZoomAndCenterRoutes(p_planEventArgs.Tour.Layer.Id);
                                }
                                else
                                {
                                    setFocusedTour(null);
                                }
                        }
                        else
                        {
                            setFocusedTour(null);
                        }
                        gMapControl.Refresh();
                        break;


                    case ePlanEventMode.ChgFocusedTourPoint:
                        boPlanTour Tour = m_PPlanCommonVars.GetTourByID(p_planEventArgs.TourPoint.TPL_ID);
                        if (Tour != null && (m_PPlanCommonVars.FocusedPoint == null || m_PPlanCommonVars.FocusedPoint != p_planEventArgs.TourPoint))
                        {
                            m_PPlanCommonVars.FocusedPoint = p_planEventArgs.TourPoint;
                            m_PPlanCommonVars.FocusedUnplannedOrder = null;
                            if (m_PPlanCommonVars.ZoomToSelectedPlan)
                            {
                                gMapControl.ZoomAndCenterMarkers(Tour.Layer.Id);
                            }
                            else
                            {
                                if (m_PPlanCommonVars.FocusedPoint.Marker != null &&
                                    !gMapControl.ViewArea.Contains(m_PPlanCommonVars.FocusedPoint.Marker.Position))
                                    gMapControl.Position = m_PPlanCommonVars.FocusedPoint.Marker.Position;
                            }
                        }
                        gMapControl.Refresh();
                        break;

                    case ePlanEventMode.ChgFocusedOrder:


                        if (m_PPlanCommonVars.ShowUnPlannedDepots)
                        {
                            //     setFocusedTour(null);
                            m_PPlanCommonVars.FocusedPoint = null;
                            m_PPlanCommonVars.FocusedUnplannedOrder = p_planEventArgs.PlanOrder;
                            if (m_PPlanCommonVars.ZoomToSelectedUnPlanned)
                            {
                                gMapControl.Position = m_PPlanCommonVars.FocusedUnplannedOrder.Marker.Position;
                            }
                            else
                            {
                                if (m_PPlanCommonVars.FocusedUnplannedOrder.Marker != null &&
                                    !gMapControl.ViewArea.Contains(m_PPlanCommonVars.FocusedUnplannedOrder.Marker.Position))
                                    gMapControl.Position = m_PPlanCommonVars.FocusedUnplannedOrder.Marker.Position;
                                //                                    gMapControl.ZoomAndCenterMarkers(m_unplannedLayer.Id);
                            }


                            gMapControl.Refresh();
                        }
                        break;

                    case ePlanEventMode.EditorMode:
                        exitCheckMode();
                        enterEditMode(false);
                        break;

                    case ePlanEventMode.ViewerMode:
                        exitCheckMode();
                        exitEditMode(false);
                        break;

                    case ePlanEventMode.CheckMode:
                        exitEditMode(false);
                        enterCheckMode();

                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void RemoveTour(boPlanTour p_Tour)
        {
            try
            {
                gMapControl.HoldInvalidation = true;

                //Útvonalak leszedése az alaplayerről
                foreach (GMapRoute ro in p_Tour.Layer.Routes)
                {
                    m_baseLayer.Routes.Remove(ro);
                }

                //Túrapontok leszedése az alaplayerről
                foreach (GMapMarker gm in p_Tour.Layer.Markers)
                {
                    m_baseLayer.Markers.Remove(gm);
                }


                p_Tour.Layer.Routes.Clear();
                p_Tour.Layer.Markers.Clear();
                gMapControl.Overlays.Remove(p_Tour.Layer);         //a map kontrolrol is leszedjük a túrát layert (a ROUTE és MARKER tartalommal)

                gMapControl.HoldInvalidation = false;
            }
            catch (Exception e)
            {
                gMapControl.HoldInvalidation = false;
                throw;
            }
        }

        private void AddTour(boPlanTour p_Tour)
        {
            try
            {

                gMapControl.HoldInvalidation = true;
                GetRoutesForMap(p_Tour.ID);
                setFocusedTour(p_Tour);
                gMapControl.HoldInvalidation = false;
            }
            catch (Exception e)
            {
                gMapControl.HoldInvalidation = false;
                throw;
            }
        }

        private void FullRefreshMap()
        {
            setEditedRoute(null);
            m_EditedTourPoint = null;
            m_EditedUnplannedOrder = null;

            GetRoutesForMap(-1);

            ClearUnpannedMarkers();
            CreateUnPlannedMarkers();


            zoomToRoutes();
            UpdateControls();
            clearMarkerTooltip();
        }

        #endregion



        #region útvonal, útszakasz, túrapont keresés

        private GMapMarker FindMarkerForMousepos(int x, int y)
        {
            //először az unplanned layeren keresünk (felhasználói igény)
            //
            foreach (GMapMarker m in m_unplannedLayer.Markers)
            {
                if (m.IsVisible)
                {
                    if (m.LocalAreaInControlSpace.Contains(x, y))
                    {
                        return m;
                    }
                }
            }

            for (int i = gMapControl.Overlays.Count - 1; i >= 0; i--)
            {
                GMapOverlay o = gMapControl.Overlays[i];
                if (o != m_unplannedLayer)                          //az unplannedek között már kerestünk
                {
                    if (o != null && o.IsVisibile)
                    {
                        foreach (GMapMarker m in o.Markers)
                        {
                            if (m.IsVisible)
                            {
                                if (m.LocalAreaInControlSpace.Contains(x, y))
                                {
                                    return m;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private EditedRoute SelectNextRouteAtPos(PointLatLng p_point, double p_nearbyDistance)
        {
            List<EditedRoute> routesNearBy;
            routesNearBy = FindRoutes(p_point, p_nearbyDistance);

            if (routesNearBy.Count > 0)
            {
                if (m_EditedRoute != null)
                {
                    //volt már kijelölt túra, egy másik route megkeresése
                    var linqTours = (from o in routesNearBy
                                     orderby o.ItemID
                                     where o.ItemID.CompareTo(m_EditedRoute.ItemID) > 0
                                     select o);
                    if (linqTours.Count<EditedRoute>() > 0)
                        return linqTours.First<EditedRoute>();
                    else
                        return routesNearBy[0];
                }
                return routesNearBy[0];
            }
            return null;
        }

        private List<EditedRoute> FindRoutes(PointLatLng p_point, double p_nearbyDistance)
        {
            List<EditedRoute> retVal = new List<EditedRoute>();

            foreach (boPlanTour rTour in m_PPlanCommonVars.TourList)
            {
                if (rTour.PSelect)
                {
                    foreach (boPlanTourPoint rTourPoint in rTour.TourPoints)
                    {

                        //FONTOS !!!
                        //A túrák mindig visszatérnek a kiindulási raktárba, ezért a legutolsó túrapontra (PTP_TYPE == 1) nem készítünk markert.
                        //emiatt vizsgálni kell, van-e marker
                        if (rTourPoint.PTP_TYPE != Global.PTP_TYPE_WHS_E && rTourPoint.Route != null)
                        {
                            for (int i = 0; i < rTourPoint.Route.Points.Count - 2; i++)
                            {
                                double dst = Util.DistanceBetweenSegmentAndPoint(rTourPoint.Route.Points[i].Lat, rTourPoint.Route.Points[i].Lng,
                                                                                rTourPoint.Route.Points[i + 1].Lat, rTourPoint.Route.Points[i + 1].Lng,
                                                                                p_point.Lat, p_point.Lng);
                                if (dst <= p_nearbyDistance)
                                {
                                    EditedRoute sr = new EditedRoute();
                                    sr.Tour = rTour;
                                    sr.TpRouteStart = rTourPoint;
                                    retVal.Add(sr);
                                }
                            }
                        }
                    }
                }
            }
            return retVal;
        }


        #endregion

        #region egér, billentyűzet-kezelés

        private void gMapControl_MouseEnter(object sender, EventArgs e)
        {
            gMapControl.Focus();
        }

        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                m_MouseMoved = false;
                m_EditedTourPoint = null;
                m_EditedUnplannedOrder = null;

                if (e.Button == MouseButtons.Left)
                {

        

                    //               Console.WriteLine("gMapControl_MouseDown");


                    m_selectorMarker.Position = gMapControl.FromLocalToLatLng(e.X, e.Y);



                    //Ez a háromfajta objektum lehet kiszelektálva
                    //           m_selectedTourPoint = null;
                    //           m_selectedUnplannedOrder = null;
                    //           m_selectedRoute = null;
                    bool bRefresh = false;

                    GMapMarker marker = FindMarkerForMousepos(e.X, e.Y);


                    if (marker != null)
                    {
                        //ha már kiválasztott unplanned-re kattintunk
                        if (m_PPlanCommonVars.FocusedUnplannedOrder != null &&
                            m_PPlanCommonVars.FocusedUnplannedOrder.Marker != null &&
                            m_PPlanCommonVars.FocusedUnplannedOrder.Marker.IsVisible &&
                            m_PPlanCommonVars.FocusedUnplannedOrder.Marker.Position == marker.Position)
                        {

                            m_EditedTourPoint = null;
                            m_EditedUnplannedOrder = m_PPlanCommonVars.FocusedUnplannedOrder;
                            m_PPlanCommonVars.FocusedPoint = null;
                            return;
                        }

                        //Ha nem, akkor először az unplannedek között keresünk. 
                        foreach (boPlanOrder rUnplanned in m_PPlanCommonVars.PlanOrderList.Where(i => i.PTP_ID == 0).ToList())
                        {
                            if (rUnplanned.Marker.Position == marker.Position && rUnplanned.Marker.IsVisible)
                            {
                                m_PPlanCommonVars.FocusedUnplannedOrder = rUnplanned;
                                if (m_EditMode)
                                    m_EditedUnplannedOrder = rUnplanned;

                                bRefresh = true;
                                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedOrder, m_PPlanCommonVars.FocusedUnplannedOrder));
                                break;
                            }
                        }


                        //az előző keresés nem volt sikeres, már kiválasztott túrapontra kattintottunk-e ?
                        //
                        if (!bRefresh && m_PPlanCommonVars.FocusedPoint != null &&
                            m_PPlanCommonVars.FocusedPoint.Marker != null &&
                            m_PPlanCommonVars.FocusedPoint.Marker.Position == marker.Position)
                        {
                            m_EditedTourPoint = m_PPlanCommonVars.FocusedPoint;
                            m_EditedUnplannedOrder = null;
                            m_PPlanCommonVars.FocusedUnplannedOrder = null;
                            return;
                        }


                        //Egy kiválasztott marker vagy túrapont, vagy tervezetlen túrapont lehet.

                        m_selectorMarker.Position = marker.Position;
                        m_editorLayer.IsVisibile = true;
                        //m_selectorMarker.Visible = true;

                        if (!bRefresh)
                        {
                            foreach (boPlanTour rTour in m_PPlanCommonVars.TourList)
                            {
                                if (rTour.PSelect)
                                {
                                    foreach (boPlanTourPoint rTourPoint in rTour.TourPoints)
                                    {
                                        //FONTOS !!!
                                        //1. A túrák mindig visszatérnek a kiindulási raktárba, ezért a legutolsó túrapontra (PTP_TYPE == 1) nem készítünk markert.
                                        //emiatt vizsgálni kell, van-e marker
                                        //2. szerkesztőmódban nem kattinthatunk raktárra
                                        //                            if (rTourPoint.PTP_TYPE == Global.PTP_TPOINT && rTourPoint.Marker.Position == marker.Position)

                                        if (rTourPoint.Marker != null && rTourPoint.Marker.Position == marker.Position && (!m_EditMode || rTourPoint.PTP_TYPE == Global.PTP_TYPE_DEP))
                                        {
                                            boPlanTourPoint focusedPt = null;
                                            if (rTourPoint.PTP_TYPE == Global.PTP_TYPE_DEP)
                                            {
                                                focusedPt = rTourPoint;        //Túrapont esetén
                                                setFocusedTour(rTourPoint.Tour);
                                            }
                                            else
                                            {
                                                focusedPt = rTour.TourPoints[0];        //Raktárra kattintás esetén a kiindulási raktár lesz kiválasztva
                                                setFocusedTour(rTour);
                                            }
                                            m_PPlanCommonVars.FocusedUnplannedOrder = null;

                                            if (m_EditMode)
                                                m_EditedTourPoint = rTourPoint;

                                            bRefresh = true;
                                            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTour, rTour));
                                            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, focusedPt));
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        //                if (!m_EditMode)
                        {
                            //Útvonalra kattintottam?

                            EditedRoute selRt = SelectNextRouteAtPos(m_selectorMarker.Position, 0.002);

                            List<EditedRoute> routesNearBy;
                            routesNearBy = FindRoutes(m_selectorMarker.Position, Global.ROUTE_APPROACH);
                            if (routesNearBy.Count > 0)
                            {
                                //Egy útvonalra kattintottunk

                                if (m_PPlanCommonVars.FocusedTour == null)
                                {
                                    setFocusedTour(routesNearBy[0].Tour);
                                }
                                else
                                {
                                    //volt már kijelölt túra, egy másik megkeresése
                                    var linqTours = (from o in routesNearBy
                                                     orderby o.Tour.ID
                                                     where o.Tour.ID > m_PPlanCommonVars.FocusedTour.ID
                                                     select o);
                                    if (linqTours.Count<EditedRoute>() > 0)
                                        setFocusedTour(linqTours.First<EditedRoute>().Tour);
                                    else
                                        setFocusedTour(routesNearBy[0].Tour);
                                }
                                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTour, m_PPlanCommonVars.FocusedTour));
                            }
                            else
                            {
                                resetEditMode();
                            }

                            bRefresh = true;

                        }

                    }
                    if (bRefresh)
                    {
                        gMapControl.Refresh();
                        if (m_ToolTipedMarker != null)
                        {
                            GMapMarker oCurrMarker = m_ToolTipedMarker;
                            clearMarkerTooltip();
                            gMapControl_OnMarkerEnter(oCurrMarker);
                            gMapControl.Invalidate();

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                exitEditMode(true);


                //                throw new Exception(ex.Message);
            }

        }


        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //            clearMarkerTooltip();
                m_mousePosition = gMapControl.FromLocalToLatLng(e.X, e.Y);
                if (e.Button == MouseButtons.Left && m_EditMode)
                {
                    clearMarkerTooltip();
                    m_MouseMoved = true;
                    m_selectorMarker.Position = gMapControl.FromLocalToLatLng(e.X, e.Y);

                    EditedRoute eroute = SelectNextRouteAtPos(m_selectorMarker.Position, 0.002);


                    if (eroute == null)
                    {
                        //Ha még nem volt kiválasztva útvonal, megnézzük, hogy a fókuszált túrához vagyunk-e közel
                        if (m_PPlanCommonVars.FocusedTour != null)
                        {

                            //volt már kijelölt túra, egy másik route megkeresése
                            var linqTours = (from o in m_PPlanCommonVars.FocusedTour.TourPoints
                                             where o.Marker != null && o.Marker.LocalArea.Contains(e.X, e.Y)
                                             select o);

                            if (linqTours.Count<boPlanTourPoint>() > 0)
                            {
                                eroute = new EditedRoute();
                                eroute.Tour = m_PPlanCommonVars.FocusedTour;
                                eroute.TpRouteStart = linqTours.First<boPlanTourPoint>();
                            }
                        }
                    }

                    //nem találtunk útvonalat... esetleg egy üres túrát találunk-e?
                    if (eroute == null)
                    {
                        GMapMarker marker = FindMarkerForMousepos(e.X, e.Y);
                        if (marker != null)
                        {
                            foreach (boPlanTour rTour in m_PPlanCommonVars.TourList)
                            {
                                if (rTour.PSelect)
                                {
                                    for (int i = 0; i < rTour.TourPoints.Count - 1; i++)
                                    {
                                        boPlanTourPoint rTourPoint = rTour.TourPoints[i];
                                        if (rTourPoint.Marker != null && rTourPoint.Marker.Position == marker.Position && rTourPoint.PTP_TYPE == Global.PTP_TYPE_WHS_S && rTour.TourPoints[i + 1].PTP_TYPE == Global.PTP_TYPE_WHS_E)
                                        {
                                            eroute = new EditedRoute();
                                            eroute.Tour = rTour;
                                            eroute.TpRouteStart = rTourPoint;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    setEditedRoute(eroute);
                    gMapControl.Refresh();
                }

                UpdateControls();
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                exitEditMode(true);
                //throw;
            }
        }


        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && m_EditMode)
                {
                    clearMarkerTooltip();

                    //Egy beosztatlan megrendelésre kattintva, 
                    //ctrl lenyomása esetén, ha van fókuszált túra, 
                    //akkor automatikusan a túrába fűzzük a megrendelést

                    if (Control.ModifierKeys == Keys.Control &&
                        m_PPlanCommonVars.FocusedUnplannedOrder != null &&
                        m_PPlanCommonVars.FocusedTour != null &&
                        m_PPlanCommonVars.FocusedTour.TourPoints.Count >= 2)
                    {

                        EditedRoute eroute = new EditedRoute();
                        eroute.Tour = m_PPlanCommonVars.FocusedTour;
                        eroute.TpRouteStart = m_PPlanCommonVars.FocusedTour.TourPoints[m_PPlanCommonVars.FocusedTour.TourPoints.Count - 2];

                        setEditedRoute(eroute);
                    }

                    if (m_EditedRoute != null)
                    {
                        if (m_EditedRoute.Tour.LOCKED)
                        {
                            UI.Message(PMapMessages.E_PEDIT_LOCKEDTRUCK);
                            resetEditMode();
                            return;
                        }

                        if (m_EditedRoute.TpRouteStart.PTP_TYPE != Global.PTP_TYPE_WHS_E)
                        {

                            if (m_EditedRoute.TpRouteStart != m_EditedTourPoint && m_EditedRoute.TpRouteStart.NextTourPoint != m_EditedTourPoint)
                            {
                                if (m_EditedTourPoint != null)
                                {
                                    m_PlanEditFuncs.ReorganizeTour(m_EditedTourPoint, m_EditedRoute.Tour, m_EditedRoute.TpRouteStart);
                                }

                                if (m_EditedUnplannedOrder != null)
                                {
                                    m_PlanEditFuncs.AddOrderToTour(m_EditedRoute.Tour, m_EditedRoute.TpRouteStart, m_EditedUnplannedOrder);
                                }

                                /*
                                if (res != bllPlanCheck.checkOrderResult.OK)
                                {
                                    UI.Error(bllPlanCheck.GetOrderResultText(res));
                                }
                                 */

                            }
                            else
                            {
                                UI.Error(PMapMessages.E_PEDIT_NOMOVEPOINT, m_EditedTourPoint.Tour.TRUCK, m_EditedTourPoint.CLT_NAME);
                            }
                        }
                        else
                        {
                            UI.Error(PMapMessages.E_PEDIT_NOMOVEAFTERWHS, m_EditedTourPoint.Tour.TRUCK, m_EditedTourPoint.CLT_NAME);
                        }
                    }
                    else if (m_EditedTourPoint != null)
                    {
                        //Törlés
                        if (m_MouseMoved)
                        {
                            m_PlanEditFuncs.RemoveTourPoint(m_EditedTourPoint);
                            //resetEditMode();
                        }
                    }
                }

                resetEditMode();
                m_MouseMoved = false;
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                exitEditMode(true);
                //throw;
            }

        }


        private void gMapControl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Point pt = gMapControl.PointToClient(Cursor.Position);

                gMapControl.Position = gMapControl.FromLocalToLatLng(pt.X, pt.Y);
                gMapControl.Refresh();


                GPoint gmapPt = gMapControl.FromLatLngToLocal(gMapControl.Position);

                Cursor.Position = gMapControl.PointToScreen(new System.Drawing.Point((int)gmapPt.X, (int)gmapPt.Y));


                if (m_CheckMode)
                {
                    m_PlanEditFuncs.ShowMapEdgesForCheck(m_editorLayer, gMapControl.Position);
                    return;
                }


            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                exitEditMode(true);
                //throw;
            }

        }

        /// <summary>
        /// A formra a KeyPreview-et be kell állítani!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPPlanEditor_KeyDown(object sender, KeyEventArgs e)
        {
            //          Console.WriteLine("cWM_KEYDOWN");
            if (e.Control && !m_EditMode)
            {
                enterEditMode(true);
            }

        }

        private void pnlPPlanEditor_KeyUp(object sender, KeyEventArgs e)
        {
            //           Console.WriteLine("cWM_KEYUP");
            if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Escape)
                exitEditMode(true);
        }


        #endregion

        #region Megjelenítés
        private void gMapControl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                // túrapont, tervezetlen megrendelés esetén a kiemelés megjelenítését itt végezzük
                //
                GMapMarker pos = null;
                Color color = Color.Red;
                if (m_PPlanCommonVars.FocusedPoint != null &&
                    m_PPlanCommonVars.FocusedPoint.Marker != null &&
                    m_PPlanCommonVars.FocusedPoint.Tour.PSelect)
                {
                    pos = m_PPlanCommonVars.FocusedPoint.Marker;
                    if (m_PPlanCommonVars.FocusedPoint.Marker.GetType() == typeof(PPlanMarker))
                    {
                        //Normál túrapont
                        PPlanMarker pm = (PPlanMarker)pos;
                        color = pm.Color;
                    }
                    else
                    {
                        //raktár
                        PPlanMarkerFlag pm = (PPlanMarkerFlag)pos;
                        if (m_PPlanCommonVars.FocusedTour != null)
                            color = m_PPlanCommonVars.FocusedTour.PCOLOR;
                        else
                            color = Color.YellowGreen;
                    }

                    drawFocusedItem(e.Graphics, pos, color);
                }

                if (m_PPlanCommonVars.FocusedUnplannedOrder != null &&
                    m_PPlanCommonVars.FocusedUnplannedOrder.Marker != null)
                {
                    drawFocusedItem(e.Graphics, m_PPlanCommonVars.FocusedUnplannedOrder.Marker, Color.Red);
                }


                if (m_EditMode && (m_EditedTourPoint != null || m_EditedUnplannedOrder != null))
                {
                    GMapMarker EditedItem = null;
                    if (m_EditedTourPoint != null)
                        EditedItem = m_EditedTourPoint.Marker;
                    if (m_EditedUnplannedOrder != null)
                        EditedItem = m_EditedUnplannedOrder.Marker;

                    Pen p = new Pen(Color.Red, 8);
                    p.StartCap = LineCap.Round;
                    p.EndCap = LineCap.ArrowAnchor;
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    GPoint ptEdited = gMapControl.FromLatLngToLocal(EditedItem.Position);
                    GPoint ptSelector = gMapControl.FromLatLngToLocal(m_selectorMarker.Position);

                    g.DrawLine(p, ptEdited.X, ptEdited.Y, ptSelector.X, ptSelector.Y);


                    if (m_EditedRoute != null && m_EditedRoute.TpRouteStart != null && m_EditedRoute.TpRouteStart.Route != null)
                    {
                        PointLatLng firstRoutePoint;
                        if (m_EditedRoute.TpRouteStart.Route.Points.Count > 0)
                            firstRoutePoint = m_EditedRoute.TpRouteStart.Route.Points[0];
                        else
                            firstRoutePoint = m_EditedRoute.TpRouteStart.Marker.Position;

                        PPlanMarker firstRoutePointMarker = new PPlanMarker(firstRoutePoint, Color.Transparent, null);

                        PointLatLng lastRoutePoint;
                        if (m_EditedRoute.TpRouteStart.Route.Points.Count > 0)
                            lastRoutePoint = m_EditedRoute.TpRouteStart.Route.Points[m_EditedRoute.TpRouteStart.Route.Points.Count - 1];
                        else
                            lastRoutePoint = m_EditedRoute.TpRouteStart.Marker.Position;


                        PPlanMarker lastRoutePointMarker = new PPlanMarker(lastRoutePoint, Color.Transparent, null);


                        GPoint ptFirst = gMapControl.FromLatLngToLocal(new PointLatLng(firstRoutePoint.Lat, firstRoutePoint.Lng));
                        GPoint ptLast = gMapControl.FromLatLngToLocal(new PointLatLng(lastRoutePoint.Lat, lastRoutePoint.Lng));


                        //útvonal sematikus kirajzolása 
                        //                    Pen p2 = new Pen(m_EditedRoute.Tour.PCOLOR, 4);
                        Pen p2 = new Pen(Color.Black, 4);
                        p2.DashStyle = DashStyle.Dot;
                        g.DrawLine(p2, ptFirst.X, ptFirst.Y, ptSelector.X, ptSelector.Y);
                        g.DrawLine(p2, ptSelector.X, ptSelector.Y, ptLast.X, ptLast.Y);

                        //a sematikus útvonalra iránynyilak kirajzolása

                        //                    Pen p3 = new Pen(m_EditedRoute.Tour.PCOLOR, 6);
                        Pen p3 = new Pen(Color.Black, 6);
                        p3.StartCap = LineCap.Round;
                        p3.EndCap = LineCap.ArrowAnchor;
                        GPoint ptArrowS = getAPointOnVector_2D(ptFirst, ptSelector, 10);
                        g.DrawLine(p3, ptArrowS.X, ptArrowS.Y, ptSelector.X, ptSelector.Y);
                        GPoint ptArrowE = getAPointOnVector_2D(ptSelector, ptLast, 10);
                        g.DrawLine(p3, ptArrowE.X, ptArrowE.Y, ptLast.X, ptLast.Y);


                        /*
                        double degree = RadianToDegree(angleOfTwoLines_2D( ptEdited, 
                            ptSelector,
                            ptFirst,
                            ptLast));
                        if (degree > 180)
                            mul = -1;

                        System.Drawing.Point px = GetThirdVertexOfRightTriangleOnVector_2D(ptEdited, ptSelector, 30 * mul);


                        Brush Fill = new SolidBrush(Color.FromArgb(96, Color.AliceBlue.R, Color.AliceBlue.G, Color.AliceBlue.B));
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(px.X - 10, px.Y - 10, 19, 19);
                        g.DrawEllipse(p, rect);
                        g.FillEllipse(Fill, rect);
                        */

                    }

                    p.Dispose();
                }
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                throw;
            }

        }

        public static double RadianToDegree(double radian)
        {
            return radian * ((double)180 / Math.PI);
        }

        public static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / (double)180;
        }

        public static double AngleOfTwoLines_2D(GPoint p1, GPoint p2, GPoint p3, GPoint p4)
        {
            //Line1: (x1, y1), (x2, y2)
            //Line2: (x3, y3), (x4, y4)
            //Double Angle = Math.Atan2(y2-y1, x2-x1) - Math.Atan2(y4-y3,x4-x3);

            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) - Math.Atan2(p4.Y - p3.Y, p4.X - p3.X);

        }

        private GPoint getAPointOnVector_2D(GPoint p_ptStart, GPoint p_ptEnd, double p_dst)
        {
            //1. szakasz meredekségének meghatározása
            //
            double radSlope = Math.Atan2((p_ptEnd.Y - p_ptStart.Y), (p_ptEnd.X - p_ptStart.X));
            //2 pontkoordináta-számítás
            //
            return new GPoint(
                Convert.ToInt32(p_ptEnd.X - (Math.Cos(radSlope) * p_dst)),
                Convert.ToInt32(p_ptEnd.Y - (Math.Sin(radSlope) * p_dst)));
        }

        /// <summary>
        /// Egy vektorra fektetett derékszögű háromszög 3. csúcskoordinátájának a visszaadása
        /// </summary>
        /// <param name="p_ptStart">Vektor kezdőpontja</param>
        /// <param name="p_ptEnd">Vektor végpontka</param>
        /// <param name="p_legA">"a" oldal mérete</param> Megj:negatív érték esetén a p_ptA-p_ptB egyenes "másik" oldalára készül a C pont
        /// <returns>C csúcspont</returns>
        private Point getThirdVertexOfRightTriangleOnVector_2D(GPoint p_ptStart, GPoint p_ptEnd, double p_legA)
        {
            //1. szakasz meredekségének meghatározása
            //
            double radSlope = Math.Atan2((p_ptEnd.Y - p_ptStart.Y), (p_ptEnd.X - p_ptStart.X));
            // 2. B pont szamitasa
            Point B = new Point(
                Convert.ToInt32(p_ptEnd.X - (Math.Cos(radSlope) * Math.Abs(p_legA))),
                Convert.ToInt32(p_ptEnd.Y - (Math.Sin(radSlope) * Math.Abs(p_legA))));
            //3.C pont kiszámítása
            int X3 = Convert.ToInt32(p_ptEnd.X + (p_ptEnd.Y - B.Y) * p_legA / Math.Sqrt(Math.Pow((p_ptEnd.Y - B.Y), 2) + Math.Pow((B.X - p_ptEnd.X), 2)));
            int Y3 = Convert.ToInt32(p_ptEnd.Y + (B.X - p_ptEnd.X) * p_legA / Math.Sqrt(Math.Pow((p_ptEnd.Y - B.Y), 2) + Math.Pow((B.X - p_ptEnd.X), 2)));
            return new System.Drawing.Point(X3, Y3);

            /* 
                x3=x1+(y1-y2)*2/sqrt((y1-y2)^2+(x2-x1)^2)
                y3=y1+(x2-x1)*2/sqrt((y1-y2)^2+(x2-x1)^2)
             */
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



        #endregion

        public void zoomToRoutes()
        {
            m_baseLayer.IsVisibile = true;
            gMapControl.ZoomAndCenterRoutes(m_baseLayer.Id);
            m_baseLayer.IsVisibile = false;
            zoomChanged();
        }


        public void zoomToAllObjects()
        {
            m_baseLayer.IsVisibile = true;
            gMapControl.ZoomAndCenterMarkers(m_baseLayer.Id);
            m_baseLayer.IsVisibile = false;
            zoomChanged();
        }


        #region Tooltip megjelentés
        /// <summary>
        /// Tooltip megjelentés
        /// A GMap az egérpozíció alatti összes marker tooltipjét feldobálja. Ez kis zoom esetén nagy tooltipkavalkádot jelent. 
        /// A PMapban továbbá a tooltipben a gridben kiválasztott elemet kell elsősorban megjeleníteni. Ezért ebben a metódusban
        /// a GMaptól jövő marker pozícicója alapján öszeszedjük az összes, tooltipben megjelenítendő GMapos adatot .
        /// </summary>
        /// <param name="item"></param>
        private void gMapControl_OnMarkerEnter(GMapMarker item)
        {
            try
            {
                if (item.GetType() == typeof(GMarkerGoogle))
                    return;
                boPlanTour tooltipedTour = null;
                boPlanOrder tooltipedPlanOrder = null;
                if (m_PPlanCommonVars.TooltipMode == MarkerTooltipMode.Always)
                {
                    //Ha minden tooltipet megjelenítünk, akkor a kapott item első sora lesz megjelenítve a státuszsorban
                    string[] aToolTipText = item.ToolTipText.Split('\n');
                    if (aToolTipText != null && aToolTipText.Length > 0)
                        lblToolTip.Text = aToolTipText[0];
                    return;
                }
                if (m_ToolTipedMarker == null)
                {
                    String sToolTipText = "";

                    //fókuszált pont tooltipjének begyűjtése
                    if (m_PPlanCommonVars.FocusedPoint != null &&
                        m_PPlanCommonVars.FocusedPoint.Marker != null &&
                        m_PPlanCommonVars.FocusedPoint.Marker.Position == item.Position)
                    {
                        sToolTipText = m_PPlanCommonVars.FocusedPoint.ToolTipText.Replace("\\n", "\n");
                        tooltipedTour = m_PPlanCommonVars.FocusedPoint.Tour;
                    }

                    if (m_PPlanCommonVars.FocusedUnplannedOrder != null &&
                        m_PPlanCommonVars.FocusedUnplannedOrder.Marker != null &&
                        m_PPlanCommonVars.FocusedUnplannedOrder.Marker.Position == item.Position)
                    {
                        sToolTipText = m_PPlanCommonVars.FocusedUnplannedOrder.ToolTipText.Replace("\\n", "\n");
                        tooltipedPlanOrder = m_PPlanCommonVars.FocusedUnplannedOrder;
                    }
                    if (m_EditedTourPoint != null && m_EditedTourPoint.Marker != null && m_EditedTourPoint.Marker.Position == item.Position)
                    {
                        sToolTipText = m_EditedTourPoint.ToolTipText.Replace("\\n", "\n");
                        tooltipedTour = m_EditedTourPoint.Tour;
                    }



                    //összegyűjtjük a látható túrapontok tooltipjeit
                    foreach (boPlanTour rTour in m_PPlanCommonVars.TourList)
                    {
                        if (rTour.Layer.IsVisibile)
                        {
                            foreach (boPlanTourPoint rTourPoint in rTour.TourPoints)
                            {
                                if (rTourPoint != m_PPlanCommonVars.FocusedPoint && rTourPoint != m_EditedTourPoint)
                                {
                                    if (rTourPoint.Marker != null && rTourPoint.Marker.Position == item.Position)
                                    {
                                        if (!sToolTipText.Contains(rTourPoint.ToolTipText))
                                        {
                                            if (sToolTipText != "" && sToolTipText.Last() != '\n')
                                                sToolTipText += "\n";

                                            sToolTipText += rTourPoint.ToolTipText;
                                            sToolTipText = sToolTipText.Replace("\\n", "\n");
                                        }
                                        tooltipedTour = rTour;
                                    }
                                }
                            }
                        }
                    }

                    //Összuegyűjtjük a tervezetlen lerakók tooltipjeit
                    foreach (boPlanOrder rup in m_PPlanCommonVars.PlanOrderList)
                    {
                        if (rup.PTP_ID == 0 && rup != m_PPlanCommonVars.FocusedUnplannedOrder)
                        {
                            if (rup.Marker != null && rup.Marker.Position == item.Position)
                            {
                                if (!sToolTipText.Contains(rup.ToolTipText))
                                {
                                    if (sToolTipText != "" && sToolTipText.Last() != '\n')
                                        sToolTipText += "\n";
                                    sToolTipText += rup.ToolTipText;
                                    sToolTipText = sToolTipText.Replace("\\n", "\n");
                                }
                                tooltipedPlanOrder = rup;
                            }
                        }
                    }


                    m_ToolTipedMarker = item;

                    //Az státuszbáron csak a tooltip első sorát jelenítjük meg.
                    string[] aToolTipText = sToolTipText.Split('\n');
                    if (aToolTipText != null && aToolTipText.Length > 0)
                        lblToolTip.Text = aToolTipText[0];

                    m_ToolTipedMarker.ToolTipText = sToolTipText;

                    //Hogy a tooltip legyen legfelül, az ahhoz tarozó layer-t meg kell emelni
                    if (tooltipedPlanOrder != null)
                    {
                        gMapControl.Overlays.Remove(m_unplannedLayer);
                        gMapControl.Overlays.Add(m_unplannedLayer);
                    }
                    if (tooltipedTour != null)
                    {
                        gMapControl.Overlays.Remove(tooltipedTour.Layer);
                        //A túrák layere mindig a tervezetlen túrák layer-e alatt van. Emiatt a gMapControl.Overlays.Count - 1
                        gMapControl.Overlays.Add(tooltipedTour.Layer);
                    }

                }
                else
                {
                }
            }
            catch (Exception e)
            {

                Util.ExceptionLog(e);
                lblToolTip.Text = "?";
            }
        }

        private void gMapControl_OnMarkerLeave(GMapMarker item)
        {
            clearMarkerTooltip();
        }

        private void clearMarkerTooltip()
        {
            if (m_ToolTipedMarker != null)
            {
                m_ToolTipedMarker.ToolTipText = "";
                m_ToolTipedMarker = null;
                lblToolTip.Text = "\n";
            }

        }
        #endregion

        void gMapControl_OnMapZoomChanged()
        {
            zoomChanged();
        }

        public override void UpdateControls()
        {
            lblCurrLat.Text = m_mousePosition.Lat.ToString("0.0000000");
            lblCurrLng.Text = m_mousePosition.Lng.ToString("0.0000000");
        }

        private void zoomChanged()
        {
            m_PPlanCommonVars.Zoom = (int)(gMapControl.Zoom);
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgZoom));
        }


        private void setFocusedTour(boPlanTour p_FocusedTour)
        {
            try
            {
                if (m_PPlanCommonVars.FocusedTour != null)
                {
                    //Kiválasztott vastagságot megszüntetjük
                    foreach (GMapRoute gr in m_PPlanCommonVars.FocusedTour.Layer.Routes)
                    {
                        gr.Stroke.Color = Util.GetSemiTransparentColor(m_PPlanCommonVars.FocusedTour.PCOLOR);
                        gr.Stroke.Width = Global.TourLineWidthNormal;
                    }

                }

                if (p_FocusedTour != null && p_FocusedTour.Layer.IsVisibile)
                    m_PPlanCommonVars.FocusedTour = p_FocusedTour;
                else
                {
                    m_PPlanCommonVars.FocusedTour = null;
                    m_PPlanCommonVars.FocusedPoint = null;
                }


                if (m_PPlanCommonVars.FocusedTour != null)
                {
                    gMapControl.Overlays.Remove(m_PPlanCommonVars.FocusedTour.Layer);
                    gMapControl.Overlays.Insert(gMapControl.Overlays.Count - 1, m_PPlanCommonVars.FocusedTour.Layer);

                    //Kiválasztott túrát megvastagítjuk
                    foreach (GMapRoute gr in m_PPlanCommonVars.FocusedTour.Layer.Routes)
                    {
                        gr.Stroke.Color = Util.GetSemiTransparentColor(m_PPlanCommonVars.FocusedTour.PCOLOR);
                        gr.Stroke.Width = Global.TourLineWidthSelected;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        private void setEditedRoute(EditedRoute p_EditedRoute)
        {
            if (m_EditedRoute != null && m_EditedRoute.TpRouteStart.Route != null)
                m_EditedRoute.TpRouteStart.Route.Stroke.Width = Global.TourLineWidthNormal;

            m_EditedRoute = p_EditedRoute;

            if (m_EditedRoute != null && m_EditedRoute.TpRouteStart.Route != null)
            {
                setFocusedTour(m_EditedRoute.Tour);
                m_EditedRoute.TpRouteStart.Route.Stroke.Width = Global.TourLineWidthSelected;
            }
        }




        private void pnlPPlanEditor_Load(object sender, EventArgs e)
        {
            // When the form loads, the KeyPreview property is set to True.
            // This lets the form capture keyboard events before
            // any other element in the form.
            this.KeyPreview = true;

        }


        public void SetToolTipMode()
        {

            foreach (boPlanTour rTour in m_PPlanCommonVars.TourList)
            {
                foreach (PPlanMarker mrkTourPoint in rTour.Layer.Markers)
                {
                    mrkTourPoint.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                    if (m_PPlanCommonVars.TooltipMode == MarkerTooltipMode.Always)
                        mrkTourPoint.ToolTipText = mrkTourPoint.TourPoint.ToolTipText;
                    else
                        mrkTourPoint.ToolTipText = "";
                }
            }

            foreach (boPlanOrder rUnPlanned in m_PPlanCommonVars.PlanOrderList)
            {
                if (rUnPlanned.PTP_ID == 0)
                {
                    rUnPlanned.Marker.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                    if (m_PPlanCommonVars.TooltipMode == MarkerTooltipMode.Always)
                        rUnPlanned.Marker.ToolTipText = rUnPlanned.ToolTipText;
                    else
                        rUnPlanned.Marker.ToolTipText = "";
                }
            }
        }

        private void enterEditMode(bool p_notify)
        {
            if (!m_EditMode)
            {
                m_EditMode = true;
                //          SetFocusedTour(null);
                m_PPlanCommonVars.DraggedObj = null;

                setEditedRoute(null);
                m_EditedTourPoint = null;
                m_EditedUnplannedOrder = null;

                gMapControl.Refresh();
                statusStrip.BackColor = Color.LightCoral;

                if (p_notify)
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.EditorMode));
            }
        }

        private void exitEditMode(bool p_notify)
        {
            if (m_EditMode)
            {

                m_EditMode = false;
                setEditedRoute(null);
                m_EditedTourPoint = null;
                m_EditedUnplannedOrder = null;
                statusStrip.BackColor = SystemColors.Control;
                m_PPlanCommonVars.DraggedObj = null;
                gMapControl.Refresh();
                if (p_notify)
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ViewerMode));
            }
        }

        private void resetEditMode()
        {
            m_MouseMoved = false;
            m_EditedTourPoint = null;
            m_EditedUnplannedOrder = null;
            setEditedRoute(null);
            gMapControl.Invalidate();
            gMapControl.Refresh();
        }

        private void enterCheckMode()
        {
            m_CheckMode = true;
    //        m_checkMapLayer.IsVisibile = true;
            gMapControl.Refresh();
        }

        private void exitCheckMode()
        {
            m_CheckMode = false;
            m_editorLayer.Markers.Clear();
            m_editorLayer.Routes.Clear();
   //         m_checkMapLayer.IsVisibile = false;
            gMapControl.Refresh();
        }

     }
}