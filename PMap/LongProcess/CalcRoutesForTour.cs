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
using PMap.Cache;
using PMap.Localize;

namespace PMap.LongProcess
{
    public class CalcRoutesForTours : BaseLongProcess
    {

        public enum eCompleteCode
        {
            OK,
            UserBreak,
            NoRoute
        }

        public eCompleteCode CompleteCode { get; set; }

        private boPlanTour m_Tour;


        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private bllRoute m_bllRoute;

        private PPlanCommonVars m_PPlanCommonVars;


        public CalcRoutesForTours(BaseProgressDialog p_Form, boPlanTour p_Tour)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_Tour = p_Tour;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_bllRoute = new bllRoute(m_DB);
        }

        protected override void DoWork()
        {

            CompleteCode = eCompleteCode.OK;

            ProcessForm.SetInfoText( PMapMessages.T_COMPLETE_TOURROUTES2 + m_Tour.TRUCK);
            CompleteCode = CreateOneRoute(m_Tour);


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

        public eCompleteCode CreateOneRoute(boPlanTour p_tour)
        {
            try
            {

                int iErrCnt = 0;

                //FONTOS !!!
                //A túrák mindig visszatérnek a kiindulási raktárba, ezért a legutolsó túrapontra nem készítünk markert.
                //
                Dictionary<string, List<int>[]> neighborsFull = null;
                Dictionary<string, List<int>[]> neighborsCut = null;

                PMapRoutingProvider provider = new PMapRoutingProvider();
                for (int i = 0; i < p_tour.TourPoints.Count - 1; i++)
                {
                    ProcessForm.NextStep();

                    PointLatLng start = new PointLatLng(p_tour.TourPoints[i].NOD_YPOS / Global.LatLngDivider, p_tour.TourPoints[i].NOD_XPOS / Global.LatLngDivider);
                    PointLatLng end = new PointLatLng(p_tour.TourPoints[i + 1].NOD_YPOS / Global.LatLngDivider, p_tour.TourPoints[i + 1].NOD_XPOS / Global.LatLngDivider);

                     MapRoute result = null;
                     if (p_tour.TourPoints[i].NOD_ID != p_tour.TourPoints[i + 1].NOD_ID)
                    {

                        result = m_bllRoute.GetMapRouteFromDB(p_tour.TourPoints[i].NOD_ID, p_tour.TourPoints[i + 1].NOD_ID, p_tour.RZN_ID_LIST, p_tour.TRK_WEIGHT, p_tour.TRK_XHEIGHT, p_tour.TRK_XWIDTH);
                        if (result == null)
                        {
                            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                            var routePar = new CRoutePars() { RZN_ID_LIST = p_tour.RZN_ID_LIST, Weight = p_tour.TRK_WEIGHT, Height = p_tour.TRK_XHEIGHT, Width = p_tour.TRK_XWIDTH };

                            //Azért van itt a térkép előkészítés, hogy csak akkor fusson le, ha 
                            //kell útvonalat számítani
                            if (neighborsFull == null || neighborsCut == null)
                            {
                                RectLatLng boundary = new RectLatLng();
                                List<int> nodes = p_tour.TourPoints.Select(s => s.NOD_ID).ToList();
                                boundary = m_bllRoute.getBoundary(nodes);
                                RouteData.Instance.getNeigboursByBound(routePar, ref neighborsFull, ref neighborsCut, boundary, p_tour.TourPoints);
                            }

                            boRoute routeInf = provider.GetRoute(p_tour.TourPoints[i].NOD_ID, p_tour.TourPoints[i + 1].NOD_ID, routePar,
                                neighborsFull[routePar.Hash], neighborsCut[routePar.Hash],
                                PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);
                            result = routeInf.Route;
                            m_bllRoute.WriteOneRoute(routeInf);
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
                //throw;
                Util.ExceptionLog(e);
            }
            finally
            {
            }
            return eCompleteCode.OK;
        }

    }
}
