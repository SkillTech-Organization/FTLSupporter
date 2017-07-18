using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.LongProcess.Base;
using PMap.DB;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using PMap.Markers;
using GMap.NET.ObjectModel;
using PMap.Route;
using PMap.MapProvider;
using System.Threading;
using PMap.BO;
using PMap.BLL;
using PMap.DB.Base;
using PMap.Common;
using PMap.Common.PPlan;

namespace PMap.LongProcess
{
    public class GetRoutePathProcess : BaseLongProcess
    {

        public enum eCompleteCode
        {
            OK,
            UserBreak,
            NoRoute
        }

        public eCompleteCode CompleteCode { get; set; }
        public ObservableCollectionThreadSafe<GMapOverlay> Overlays = new ObservableCollectionThreadSafe<GMapOverlay>();

        private List<boPlanTour> m_TourList;
        private GMapControl m_gMapControl;
        private GMapOverlay m_baseLayer;

        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private bllRoute m_bllRoute;

        private PPlanCommonVars m_PPlanCommonVars;

        int m_genTPL_ID = -1;               // kitöltés esetén csak ezt a túrát kell térképre generálni

        public GetRoutePathProcess(BaseProgressDialog p_Form, List<boPlanTour> p_TourList, GMapControl p_gMapControl, GMapOverlay p_baseLayer, int p_genTPL_ID, PPlanCommonVars p_PPlanCommonVars)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_TourList = p_TourList;
            m_gMapControl = p_gMapControl;
            m_baseLayer = p_baseLayer;
            m_genTPL_ID = p_genTPL_ID;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_bllRoute = new bllRoute(m_DB);
            m_PPlanCommonVars = p_PPlanCommonVars;

        }
        
        protected override void DoWork()
        {

            CompleteCode = eCompleteCode.OK;
            if (m_genTPL_ID <= 0)
            {
                //az alaplayert csak teljes útvonalgenerálásnál töröljük
                m_baseLayer.Routes.Clear();
                m_baseLayer.Markers.Clear();
            }

            for (int i = 0; i < m_TourList.Count; i++)
            {
                if (m_genTPL_ID <= 0 || m_TourList[i].ID == m_genTPL_ID)
                {
                    ProcessForm.SetInfoText("Túrarészletező betöltés:" + m_TourList[i].TRUCK);
                    CompleteCode = CreateOneRoute(m_TourList[i], true);


                    if (CompleteCode != eCompleteCode.OK)
                    {
                        EventStopped.Set();
                        return;
                    }

                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {

                        EventStopped.Set();
                        CompleteCode = eCompleteCode.UserBreak;
                        return;
                    }
                    ProcessForm.NextStep();
                }
            }

        }

        public eCompleteCode CreateOneRoute(boPlanTour p_tour, bool p_addMarker)
        {
            try
            {

                int iErrCnt = 0;
                if (p_tour.Layer == null)
                {
                    p_tour.Layer = new GMapOverlay(p_tour.ID.ToString());
                    p_tour.Layer.IsVisibile = p_tour.PSelect;
                    Overlays.Add(p_tour.Layer);
                }
                else
                {
                    p_tour.Layer.Routes.Clear();
                    p_tour.Layer.Markers.Clear();
                }


                //FONTOS !!!
                //A túrák mindig visszatérnek a kiindulási raktárba, ezért a legutolsó túrapontra nem készítünk markert.
                //
                PMapRoutingProvider provider = new PMapRoutingProvider();
                System.Drawing.Drawing2D.DashStyle dashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                for (int i = 0; i < p_tour.TourPoints.Count - 1; i++)
                {

                    PointLatLng start = new PointLatLng(p_tour.TourPoints[i].NOD_YPOS / Global.LatLngDivider, p_tour.TourPoints[i].NOD_XPOS / Global.LatLngDivider);
                    PointLatLng end = new PointLatLng(p_tour.TourPoints[i + 1].NOD_YPOS / Global.LatLngDivider, p_tour.TourPoints[i + 1].NOD_XPOS / Global.LatLngDivider);

                    Dictionary<CRoutePars, List<int>[]> neighborsFull = null;
                    Dictionary<CRoutePars, List<int>[]> neighborsCut = null;

                    MapRoute result = null;
                    if (p_tour.TourPoints[i].NOD_ID != p_tour.TourPoints[i + 1].NOD_ID)
                    {

                        result = m_bllRoute.GetMapRouteFromDB(p_tour.TourPoints[i].NOD_ID, p_tour.TourPoints[i + 1].NOD_ID, p_tour.RZN_ID_LIST, p_tour.TRK_WEIGHT, p_tour.TRK_XHEIGHT, p_tour.TRK_XWIDTH);
                        if (result == null)
                        {
                            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                            var routePar = new CRoutePars() { RZN_ID_LIST = p_tour.RZN_ID_LIST, Weight = p_tour.TRK_WEIGHT, Height = p_tour.TRK_XHEIGHT, Width = p_tour.TRK_XWIDTH };
                            //TODO:lehet, hogy nem kellene térkép kivágást végeznni itt
                            if (neighborsFull == null || neighborsCut == null)
                            {
                                RectLatLng boundary = new RectLatLng();
                                List<int> nodes = new List<int>() { p_tour.TourPoints[i].NOD_ID, p_tour.TourPoints[i + 1].NOD_ID };
                                boundary = m_bllRoute.getBoundary(nodes);
                                RouteData.Instance.getNeigboursByBound(routePar, out neighborsFull, out neighborsCut, boundary);
                            }

                            boRoute routeInf = provider.GetRoute(p_tour.TourPoints[i].NOD_ID, p_tour.TourPoints[i + 1].NOD_ID, routePar,
                                neighborsFull[routePar], neighborsCut[routePar],
                                PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);
                            result = routeInf.Route;
                            m_bllRoute.WriteOneRoute(routeInf);
                        }
                    }
                    else
                    {

                        result = new MapRoute(p_tour.TourPoints[i].NOD_NAME);
                    }
                    if (result != null)
                    {

                        iErrCnt = 0;

                        // add route
                        GMapRoute r = new GMapRoute(result.Points, result.Name);

                        Pen pen = new Pen(Util.GetSemiTransparentColor(p_tour.PCOLOR), Global.TourLineWidthNormal);
                        pen.DashStyle = dashStyle;
                        r.Stroke = pen;

                        p_tour.Layer.Routes.Add(r);
                        m_baseLayer.Routes.Add(r);


                        p_tour.TourPoints[i].Route = r;
                        p_tour.TourPoints[i].NextTourPoint = p_tour.TourPoints[i + 1];

                        if (p_addMarker)
                        {

                            PPlanMarkerFlag mrkFlag;
                            if (p_tour.TourPoints[i].PTP_TYPE == Global.PTP_WHSOUT)
                            {
                                mrkFlag = new PPlanMarkerFlag(start, p_tour.TourPoints[i]);
                                mrkFlag.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                                p_tour.TourPoints[i].ToolTipText = p_tour.TourPoints[i].TIME_AND_NAME;
                                //                            mrkFlag.Size = new System.Drawing.Size(20, 20);

                                p_tour.Layer.Markers.Add(mrkFlag);
                                p_tour.TourPoints[i].Marker = mrkFlag;
                                m_baseLayer.Markers.Add(mrkFlag);
                            }

                            if (p_tour.TourPoints[i].PTP_TYPE == Global.PTP_WHSIN)
                            {
                                //ide csak multit]ra esetén futhat a program !!!
                                //
                                mrkFlag = new PPlanMarkerFlag(start, p_tour.TourPoints[i]);
                                dashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                                p_tour.Layer.Markers.Add(mrkFlag);
                                p_tour.TourPoints[i].Marker = mrkFlag;
                                m_baseLayer.Markers.Add(mrkFlag);

                            }

                            if (p_tour.TourPoints[i + 1].PTP_TYPE == Global.PTP_TPOINT)
                            {
                                PPlanMarker mrkTourPoint = new PPlanMarker(end, p_tour.PCOLOR, p_tour.TourPoints[i]);
                                mrkTourPoint.ToolTipMode = m_PPlanCommonVars.TooltipMode;
                                mrkTourPoint.Size = new System.Drawing.Size(20, 20);

                                if (PMapIniParams.Instance.DepCodeInToolTip)
                                    p_tour.TourPoints[i + 1].ToolTipText = p_tour.TourPoints[i + 1].DEP_CODE + "  ";
                                p_tour.TourPoints[i + 1].ToolTipText += p_tour.TourPoints[i + 1].TIME_AND_NAME;

                                p_tour.Layer.Markers.Add(mrkTourPoint);
                                p_tour.TourPoints[i + 1].Marker = mrkTourPoint;
                                m_baseLayer.Markers.Add(mrkTourPoint);

                            }
                        }
                    }
                    else
                    {
                        iErrCnt++;
                    }
                    if (iErrCnt >= 6)
                    {
                        return eCompleteCode.NoRoute;
                    }
                }
            }
            catch (Exception e)
            {
                //throw e;
                Util.ExceptionLog(e);
            }
            finally
            {
            }
            return eCompleteCode.OK;
        }

    }
}
