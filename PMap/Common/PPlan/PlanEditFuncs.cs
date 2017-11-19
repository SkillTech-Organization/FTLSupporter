using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL;
using PMap.BO;
using GMap.NET.WindowsForms;
using PMap.Forms.Base;
using PMap.Localize;
using System.Drawing;
using PMap.DB.Base;
using System.Windows.Forms;
using PMap.Route;
using GMap.NET;
using PMap.MapProvider;
using PMap.Cache;
using GMap.NET.WindowsForms.Markers;
using PMap.Markers;

namespace PMap.Common.PPlan
{
    public class PlanEditFuncs
    {
        private bllPlan m_bllPlan;
        private bllPlanEdit m_bllPlanEdit;
        private bllRoute m_bllRoute;

        private BasePanel m_panel;
        private PPlanCommonVars m_PPlanCommonVars;

        public PlanEditFuncs(BasePanel p_panel, PPlanCommonVars p_PPlanCommonVars)
        {
            m_panel = p_panel;
            m_PPlanCommonVars = p_PPlanCommonVars;
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
            m_bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
        }

        public boPlanTour RemoveTourPoint(boPlanTourPoint p_TourPoint)
        {
            boPlanTour refreshedTour = null;
            if (p_TourPoint != null && UI.Confirm(PMapMessages.Q_PEDIT_DELDEP, p_TourPoint.CLT_NAME, p_TourPoint.Tour.TRUCK))
            {
                //Ha több túrapont is vonatkozik egy pontra, akkor mindegyiket töröljük

                var deletePoints = p_TourPoint.Tour.TourPoints.Where(w => w.NOD_ID == p_TourPoint.NOD_ID).ToList();
                foreach (var dp in deletePoints)
                {
                    m_bllPlanEdit.RemoveOrderFromTour(dp, Global.defWeather, true);
                }

                refreshedTour = RefreshToursAfterModify(p_TourPoint.Tour.ID, 0);
            }
            return refreshedTour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ReorganizedTourPoint">"Átszervezendő túrapont</param>
        /// <param name="p_Tour">Cél túra</param>
        /// <param name="p_InsertionPoint">beszúrás túrapont</param>
        public boPlanTour ReorganizeTour(boPlanTourPoint p_ReorganizedTourPoint, boPlanTour p_Tour, boPlanTourPoint p_InsertionPoint)
        {

            boPlanTour refreshedTour = null;
            if (p_InsertionPoint.PTP_TYPE == Global.PTP_TYPE_WHS_E)
            {
                UI.Error(PMapMessages.E_PEDIT_WRONGINSPOINT);
                return refreshedTour;
            }

            if (UI.Confirm(PMapMessages.Q_PEDIT_REORDER, p_ReorganizedTourPoint.Tour.TRUCK, p_ReorganizedTourPoint.CLT_NAME))
            {
                bllPlanCheck.checkOrderResult res = bllPlanCheck.checkOrderResult.OK;
                //Egyelőre nem kell kézi tervezésnél a járműellenőrzés
                //res = bllPlanCheck.CheckAll(m_EditedTourPoint.TOD_ID, m_EditedRoute.Tour.ID);
                res = bllPlanCheck.checkOrderResult.OK;

                //MEGJ:a m_EditedTourPoint.Tour t
                if (!checkInsertionPoint(p_ReorganizedTourPoint.Tour.RZN_ID_LIST, p_ReorganizedTourPoint.Tour.TRK_WEIGHT, p_ReorganizedTourPoint.Tour.TRK_XHEIGHT, p_ReorganizedTourPoint.Tour.TRK_XWIDTH, p_InsertionPoint.NOD_ID, p_ReorganizedTourPoint.NOD_ID, p_InsertionPoint.NextTourPoint.NOD_ID))
                {
                    //Megj.:Egyelőre nem kezeljük azt a helyzetet, amikor nincs távolaág számítva
                    //(ez esetben pl. egy távolságszámítást indíthatnánk a két pontra)
                    UI.Message(PMapMessages.E_PEDIT_NOTFITTEDDEP);
                    //                    resetEditMode();
                    return refreshedTour;
                }


                //Létező túrapont átszervezése
                //
                if (res == bllPlanCheck.checkOrderResult.OK && m_bllPlanEdit.RemoveOrderFromTour(p_ReorganizedTourPoint, Global.defWeather, true))
                {
                    boPlanOrder upo = m_bllPlan.GetPlanOrder(p_ReorganizedTourPoint.TOD_ID);

                    int PTP_ID = m_bllPlanEdit.AddOrderToTour(p_Tour.ID, upo, p_InsertionPoint, Global.defWeather);

                    //Túrák befrissítése

                    if (p_ReorganizedTourPoint.Tour.ID != p_Tour.ID)
                        refreshedTour = RefreshToursAfterModify(p_ReorganizedTourPoint.Tour.ID, p_Tour.ID);
                    else
                        refreshedTour = RefreshToursAfterModify(p_ReorganizedTourPoint.Tour.ID, 0);

                    //Rápozícionálunk a túrára
                    PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, p_Tour);
                    m_panel.DoNotifyDataChanged(eve);

                    //rápozícionálunk az új túrapontra
                    //
                    eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, m_PPlanCommonVars.GetTourPointByID(PTP_ID));
                    m_panel.DoNotifyDataChanged(eve);
                }
            }
            return refreshedTour;
        }

        public boPlanTour TurnTour(int p_TPL_ID)
        {
            boPlanTour refreshedTour = null;
            if (UI.Confirm(PMapMessages.Q_PEDIT_REVERSE))
            {
                m_bllPlanEdit.TurnTour(p_TPL_ID, Global.defWeather);
                refreshedTour = RefreshToursAfterModify(p_TPL_ID, 0);
            }
            return refreshedTour;
        }


        public boPlanTour AddOrderToTour(boPlanTour p_Tour, boPlanTourPoint p_InsertionPoint, boPlanOrder p_planOrder)
        {
            boPlanTour refreshedTour = null;
            if (UI.Confirm(PMapMessages.Q_PEDIT_DEPINTOTOUR, p_Tour.TRUCK, p_planOrder.DEP_NAME))
            {

                if (p_InsertionPoint.PTP_TYPE == Global.PTP_TYPE_WHS_E)
                {
                    UI.Error(PMapMessages.E_PEDIT_WRONGINSPOINT);
                    return refreshedTour;
                }

                bllPlanCheck.checkOrderResult res = bllPlanCheck.checkOrderResult.OK;

                //Egyelőre nem kell kézi tervezésnél a járműellenőrzés
                //res = bllPlanCheck.CheckAll(m_EditedUnplannedOrder.ID, m_EditedRoute.Tour.ID);


                //Új megrendelés felvétele a túrába
                //
                if (res == bllPlanCheck.checkOrderResult.OK)
                {

                    if (!checkInsertionPoint(p_Tour.RZN_ID_LIST, p_Tour.TRK_WEIGHT, p_Tour.TRK_XHEIGHT, p_Tour.TRK_XWIDTH, p_InsertionPoint.NOD_ID, p_planOrder.NOD_ID, p_InsertionPoint.NextTourPoint.NOD_ID))
                    {
                        //Megj.:Egyelőre nem kezeljük azt a helyzetet, amikor nincs távolaág számítva
                        //(ez esetben pl. egy távolságszámítást indíthatnánk a két pontra)
                        UI.Message(PMapMessages.E_PEDIT_NOINSPOINT);
                        //resetEditMode();
                        return refreshedTour;

                    }
                    int PTP_ID = m_bllPlanEdit.AddOrderToTour(p_Tour.ID, p_planOrder, p_InsertionPoint, Global.defWeather);
                    if (PTP_ID > 0)
                    {
                        refreshedTour = RefreshToursAfterModify(p_Tour.ID, 0);

                        //Rápozícionálunk a túrára
                        PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, refreshedTour);
                        m_panel.DoNotifyDataChanged(eve);

                        //rápozícionálunk az új túrapontra
                        //
                        eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, m_PPlanCommonVars.GetTourPointByID(PTP_ID));
                        m_panel.DoNotifyDataChanged(eve);
                    }
                }
            }
            return refreshedTour;
        }


        public boPlanTour CreateNewTour(int p_PLN_ID, int p_WHS_ID, int p_TPL_ID, Color p_color, DateTime p_WhsS, DateTime p_WhsE, int p_srvTime)
        {
            boPlanTour refreshedTour = null;
            if (UI.Confirm(PMapMessages.Q_PEDIT_NEWTOUR))
            {

                m_bllPlanEdit.CreateNewTour(p_PLN_ID, p_WHS_ID, p_TPL_ID, p_color, p_WhsS, p_WhsE, p_srvTime);
                refreshedTour = RefreshToursAfterModify(p_TPL_ID, 0);

                PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, refreshedTour);
                m_panel.DoNotifyDataChanged(eve);
            }
            return refreshedTour;
        }

        public void DelTour(boPlanTour p_delTour)
        {
            if (UI.Confirm(PMapMessages.Q_PEDIT_DELTOUR, p_delTour.TRUCK))
            {
                m_bllPlanEdit.DeleteTour(p_delTour.ID);
                RefreshToursAfterModify(p_delTour.ID, 0);
            }
        }

        public boPlanTour ChangeTruck(int p_PLN_ID, int p_OldTPL_ID, int p_NewTPL_ID, Color p_color)
        {

            boPlanTour refreshedTour = null;
            using (TransactionBlock transObj = new TransactionBlock(PMapCommonVars.Instance.CT_DB))
            {
                try
                {
                    m_bllPlanEdit.ChangeTruck(p_PLN_ID, p_OldTPL_ID, p_NewTPL_ID, Global.defWeather);
                    m_bllPlanEdit.UpdateTourColor(p_NewTPL_ID, p_color);
                    m_bllPlanEdit.UpdateTourSelect(p_NewTPL_ID, true);
                }
                catch (Exception exc)
                {
                    PMapCommonVars.Instance.CT_DB.Rollback();
                    Util.ExceptionLog(exc);
                    throw;
                }

            }
            refreshedTour = RefreshToursAfterModify(p_OldTPL_ID, p_NewTPL_ID);

            PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, refreshedTour);
            m_panel.DoNotifyDataChanged(eve);

            return refreshedTour;
        }

        public void RefreshOrdersFromDB()
        {
            m_PPlanCommonVars.PlanOrderList = m_bllPlan.GetPlanOrders(m_PPlanCommonVars.PLN_ID);
            m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.RefreshOrders));
            m_PPlanCommonVars.FocusedUnplannedOrder = null;
        }

        public boPlanTour RefreshToursAfterModify(int pChangedTourID1, int pChangedTourID2)
        {
            boPlanTour TourWithFreshData = null;
            using (new WaitCursor())
            {
                RefreshOrdersFromDB();

                m_PPlanCommonVars.FocusedPoint = null;

                if (pChangedTourID1 > 0)
                {
                    TourWithFreshData = RefreshTourDataFromDB(pChangedTourID1);
                }
                    if (pChangedTourID2 > 0)
                {
                    TourWithFreshData = RefreshTourDataFromDB(pChangedTourID2);
                }
            }
            return TourWithFreshData;
        }

        public bool SetPositionByOrd_NUM(string sFindORD_NUM)
        {
            string sFndText = sFindORD_NUM.ToUpper();


            boPlanOrder ord = m_PPlanCommonVars.GetOrderByORD_NUM(sFndText);
            if (ord != null)
            {

                PlanEventArgs evt = new PlanEventArgs(ePlanEventMode.ChgFocusedOrder, ord);
                m_panel.DoNotifyDataChanged(evt);

                if (ord.PTP_ID > 0)
                {
                    boPlanTourPoint ptp = m_PPlanCommonVars.GetTourPointByID(ord.PTP_ID);
                    evt = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, ptp.Tour);
                    m_panel.DoNotifyDataChanged(evt);

                    evt = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, ptp);
                    m_panel.DoNotifyDataChanged(evt);
                }
                return true;

            }
            return false;
        }

   
        private bool checkInsertionPoint(string p_RZN_ID_LIST, int DST_MAXWEIGHT, int DST_MAXHEIGHT, int DST_MAXWIDTH, int p_NOD_ID_START, int p_NOD_ID_INS, int p_NOD_ID_END)
        {

            
            //Ellenőrizni, találunk-e útvonalat a beszúrási pont és a beszúrt lerakó között
            bllPlanCheck.checkDistanceResult dresultStart =
                    bllPlanCheck.CheckDistance(p_RZN_ID_LIST, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH, p_NOD_ID_START, p_NOD_ID_INS);
            //Ellenőrizzük, van-e útvonal a lerakó és a beszúrás érkezési pontja között
            bllPlanCheck.checkDistanceResult dresultEnd =
                    bllPlanCheck.CheckDistance(p_RZN_ID_LIST, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH, p_NOD_ID_INS, p_NOD_ID_END);
            
                        return (dresultStart == bllPlanCheck.checkDistanceResult.OK &&
                                dresultEnd == bllPlanCheck.checkDistanceResult.OK);
          /* 
          EZ KELL

            DateTime dtStart = DateTime.Now;
            Dictionary<string, List<int>[]> neighborsFull = null;
            Dictionary<string, List<int>[]> neighborsCut = null;
            var routePar = new CRoutePars() { RZN_ID_LIST = p_RZN_ID_LIST, Weight = DST_MAXWEIGHT, Height = DST_MAXHEIGHT, Width = DST_MAXWIDTH };

            var resultStart = m_bllRoute.GetMapRouteFromDB(p_NOD_ID_START, p_NOD_ID_INS, p_RZN_ID_LIST, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH);
            var resultEnd = m_bllRoute.GetMapRouteFromDB(p_NOD_ID_INS, p_NOD_ID_END, p_RZN_ID_LIST, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH);
            if (resultStart == null || resultEnd == null)
            {
                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);
                if (neighborsFull == null || neighborsCut == null)
                {
                    RectLatLng boundary = new RectLatLng();
                    List<int> nodes = new List<int>() { p_NOD_ID_START, p_NOD_ID_INS, p_NOD_ID_END };
                    boundary = m_bllRoute.getBoundary(nodes);
                    RouteData.Instance.getNeigboursByBound(routePar, ref neighborsFull, ref neighborsCut, boundary);
                }
                PMapRoutingProvider provider = new PMapRoutingProvider();

                if (resultStart == null)
                {
                    boRoute routeInfStart = provider.GetRoute(p_NOD_ID_START, p_NOD_ID_INS, routePar,
                        neighborsFull[routePar.Hash], neighborsCut[routePar.Hash],
                        PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);
                    resultStart = routeInfStart.Route;
                    m_bllRoute.WriteOneRoute(routeInfStart);

                    using (LogForRouteCache lockObj = new LogForRouteCache(RouteCache.Locker))
                    {
                        RouteCache.Instance.Items.Add(routeInfStart);
                    }
                }

                if (resultEnd == null)
                {
                    boRoute routeInfEnd = provider.GetRoute(p_NOD_ID_INS, p_NOD_ID_END, routePar,
                        neighborsFull[routePar.Hash], neighborsCut[routePar.Hash],
                        PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);
                    resultEnd = routeInfEnd.Route;
                    m_bllRoute.WriteOneRoute(routeInfEnd);

                    using (LogForRouteCache lockObj = new LogForRouteCache(RouteCache.Locker))
                    {
                        RouteCache.Instance.Items.Add(routeInfEnd);
                    }
                }

            }
            Util.Log2File(String.Format("InsertionPoint Időtartam:{0}", (DateTime.Now - dtStart).ToString()), false);
            Console.WriteLine(String.Format("InsertionPoint Időtartam:{0}", (DateTime.Now - dtStart).ToString()));

            return (resultStart != null && resultEnd != null);
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_TourWithFreshData"></param>
        private boPlanTour RefreshTourDataFromDB(int p_TPL_ID)
        {

            boPlanTour TourWithOldData = m_PPlanCommonVars.GetTourByID(p_TPL_ID);
            boPlanTour TourWithFreshData = m_bllPlan.GetPlanTour(p_TPL_ID);
            if (TourWithOldData != null)
            {

                //kitöröljük a régi adatokkal rendelkező túrát és beszúrjuk a firssítést
                int index = m_PPlanCommonVars.TourList.IndexOf(TourWithOldData);
                m_PPlanCommonVars.TourList.Remove(TourWithOldData);
                m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.RemoveTour, TourWithOldData));

                if (TourWithFreshData != null)
                {
                    m_PPlanCommonVars.TourList.Insert(index, TourWithFreshData);
                    m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.AddTour, TourWithFreshData));
                }
            }
            else
            {
                //Nem volt még a listában, hozzáadás
                if (TourWithFreshData != null)
                {
                    m_PPlanCommonVars.TourList.Add(TourWithFreshData);
                    m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.AddTour, TourWithFreshData));
                }
            }
            return TourWithFreshData;
        }

        public void ShowMapEdgesForCheck(GMapOverlay p_checkMapLayer, PointLatLng p_position)
        {
            using (new WaitCursor())
            {

                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                HashSet<PointLatLng> markersPts = new HashSet<PointLatLng>(p_checkMapLayer.Markers.Select(s=>s.Position).ToList());

                foreach (var edg in RouteData.Instance.Edges.Where(
                     w => (Math.Abs(w.Value.toLatLng.Lng - p_position.Lng) + Math.Abs(w.Value.toLatLng.Lat - p_position.Lat) < (double)Global.EdgeApproachCity/Global.LatLngDivider))
                     )
                {
                    var edge = edg.Value;


                    if (p_checkMapLayer.Routes.FirstOrDefault(a => a.From == edge.fromLatLng && a.To == edge.toLatLng) == null)
                    {


                       var ToolTipText = String.Format("ID:{0} Súly:{1}, Magaság:{2}, Szélesség:{3}\nNév:{4}, fromNOD:{5}, toNOD:{6}\nBehajtási övezet:{7}",
                           edge.ID, edge.EDG_MAXWEIGHT, edge.EDG_MAXHEIGHT, edge.EDG_MAXWIDTH, edge.EDG_NAME, edge.NOD_ID_FROM, edge.NOD_ID_TO, edge.WZONE);
                        GMapMarker gm = null;

                
                           if (markersPts.Contains(edge.fromLatLng))
                           {
                               gm = p_checkMapLayer.Markers.FirstOrDefault(x => x.Position == edge.fromLatLng);
                               gm.ToolTipText += "\n" + ToolTipText;

                           }
                           else
                           {
                               gm = new GMarkerGoogle(edge.fromLatLng, GMarkerGoogleType.orange_small);
                               p_checkMapLayer.Markers.Add(gm);
                               gm.ToolTipText = ToolTipText;
                               markersPts.Add(gm.Position);
                           }
                           
                        
                        if (markersPts.Contains(edge.toLatLng))
                        {
                            gm = p_checkMapLayer.Markers.FirstOrDefault(x => x.Position == edge.toLatLng);
                            gm.ToolTipText += "\n" + ToolTipText;


                        }
                        else
                        {
                            gm = new GMarkerGoogle(edge.toLatLng, GMarkerGoogleType.orange_small);
                            p_checkMapLayer.Markers.Add(gm);
                            gm.ToolTipText = ToolTipText;
                            markersPts.Add(gm.Position);
                      }
                        
                        //    gm.ToolTipText += String.Format("ID:{0} Súly:{1}, Magaság:{2}, Szélesség:{3}\nNév:{4}, fromNOD:{5}, toNOD:{6}", edge.ID, edge.EDG_MAXWEIGHT, edge.EDG_MAXHEIGHT, edge.EDG_MAXWIDTH, edge.EDG_NAME, edge.NOD_ID_FROM, edge.NOD_ID_TO);

                        Pen p;
                        switch (edge.RDT_VALUE)
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

                        p_checkMapLayer.Routes.Add(r);

                    }
                }
            }
        }

    }
}

