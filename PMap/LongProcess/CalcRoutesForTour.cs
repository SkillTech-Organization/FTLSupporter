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

            ProcessForm.SetInfoText(PMapMessages.T_COMPLETE_TOURROUTES2 + m_Tour.TRUCK);
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

                var routePar = new CRoutePars() { RZN_ID_LIST = p_tour.RZN_ID_LIST, Weight = p_tour.TRK_WEIGHT, Height = p_tour.TRK_XHEIGHT, Width = p_tour.TRK_XWIDTH };

                Dictionary<string, List<int>[]> neighborsFull = null;
                Dictionary<string, List<int>[]> neighborsCut = null;
                RectLatLng boundary = new RectLatLng();
                List<int> nodes = p_tour.TourPoints.Select(s => s.NOD_ID).ToList();
                boundary = m_bllRoute.getBoundary(nodes);
                RouteData.Instance.getNeigboursByBound(routePar, ref neighborsFull, ref neighborsCut, boundary, p_tour.TourPoints);

                List<boRoute> results = new List<boRoute>();

                PMapRoutingProvider provider = new PMapRoutingProvider();
                foreach (var tourPoint in p_tour.TourPoints.GroupBy(g => g.NOD_ID))
                {
                    ProcessForm.NextStep();

                    RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);

                    var toNodes = p_tour.TourPoints.GroupBy(g => g.NOD_ID).Where(w => w.Key != tourPoint.First().NOD_ID).Select(s => s.Key).ToList();
                    results.AddRange(provider.GetAllRoutes(routePar, tourPoint.First().NOD_ID, toNodes,
                                         neighborsFull[routePar.Hash], neighborsCut[routePar.Hash],
                                         PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath));

                }
                m_bllRoute.DeleteTourRoutes(p_tour);
                m_bllRoute.WriteRoutesBulk(results, true);
                results = new List<boRoute>();
                Util.Log2File("CalcRoutesForTours WriteRoutesBulk: " + GC.GetTotalMemory(false).ToString());



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
