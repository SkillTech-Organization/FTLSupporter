using GMap.NET;
using PMap;
using PMap.BLL;
using PMap.BO;
using PMap.Common;
using PMap.DB.Base;
using PMap.LongProcess.Base;
using PMap.MapProvider;
using PMap.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLCalcRouteProcess : BaseLongProcess
    {

        public bool Completed { get; set; }
        public List<boRoute> result { get; set; }
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private bllRoute m_bllRoute;
        private bool m_cacheRoutes;

        List<FTLPMapRoute> m_lstRoutes = new List<FTLPMapRoute>();

        internal FTLCalcRouteProcess(ProcessNotifyIcon p_NotifyIcon, List<FTLPMapRoute> p_lstRoutes, bool p_cacheRoutes)
            : base(p_NotifyIcon, System.Threading.ThreadPriority.Highest)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            m_bllRoute = new bllRoute(m_DB);

            m_lstRoutes = p_lstRoutes;
            m_cacheRoutes = p_cacheRoutes;
        }


        protected override void DoWork()
        {
            try
            {
                Completed = false;

                int i = 0;
                DateTime dtStart = DateTime.Now;
                TimeSpan tspDiff;

                if (ProcessForm != null)
                {
                    ProcessForm.SetInfoText( "Inicializálás");
                }


                PMapRoutingProvider provider = new PMapRoutingProvider();
                RouteData.Instance.Init(m_DB, null);
                RectLatLng boundary = new RectLatLng();

                List<int> fromNodes = m_lstRoutes.GroupBy(g => g.fromNOD_ID).Select(x => x.Key).ToList();
                List<int> toNodes = m_lstRoutes.GroupBy(g => g.toNOD_ID).Select(x => x.Key).ToList();
                List<int> allNodes = fromNodes.Union(toNodes).ToList();
                boundary = m_bllRoute.getBoundary(allNodes);

 
                Dictionary<string, List<int>[]> NeighborsArrFull = null;
                Dictionary<string, List<int>[]> NeighborsArrCut = null;
                List<CRoutePars> routePars = m_lstRoutes.GroupBy(g => new { g.RZN_ID_LIST, g.GVWR, g.Height, g.Width })
                      .Select(s => new CRoutePars() { RZN_ID_LIST = s.Key.RZN_ID_LIST, Weight = s.Key.GVWR, Height = s.Key.Height, Width = s.Key.Width }).ToList();

                RouteData.Instance.getNeigboursByBound(routePars, ref NeighborsArrFull, ref NeighborsArrCut, boundary, null);

                var lstCalcNodes = m_lstRoutes.GroupBy(gr => new { gr.fromNOD_ID, gr.RZN_ID_LIST, gr.GVWR, gr.Height, gr.Width }).ToDictionary(gr => gr.Key, gr => gr.Select(x => x.toNOD_ID).ToList());

                DateTime dtStartX2 = DateTime.Now;
                List<boRoute> writeRoute = new List<boRoute>();
                foreach (var calcNode in lstCalcNodes.AsEnumerable())
                {

                    var routePar = routePars.Where(w => w.RZN_ID_LIST == calcNode.Key.RZN_ID_LIST &&
                                                    w.Weight == calcNode.Key.GVWR &&
                                                    w.Height == calcNode.Key.Height &&
                                                    w.Width == calcNode.Key.Width).FirstOrDefault();
                    i++;
                    dtStart = DateTime.Now;

                    List<int> lstToNodes = calcNode.Value;
                    List<boRoute> results = provider.GetAllRoutes(routePar, calcNode.Key.fromNOD_ID, lstToNodes,
                                            NeighborsArrFull[routePar.Hash], NeighborsArrCut[routePar.Hash],
                                            PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);

                    //A kiszámolt eredmények 'bedolgozása'
                    foreach (boRoute route in results)
                    {
                        //leválogatjuk, mely útvonalakra tartozik a számítás
                        List<FTLPMapRoute> lstFTLR = m_lstRoutes.Where(x => x.fromNOD_ID == route.NOD_ID_FROM && x.toNOD_ID == route.NOD_ID_TO 
                                                                    && x.RZN_ID_LIST == routePar.RZN_ID_LIST && x.GVWR==routePar.Weight && x.Height==routePar.Height && x.Width==routePar.Width).ToList();

                        foreach (FTLPMapRoute ftr in lstFTLR)
                        {

                            if (m_cacheRoutes)
                            {
                                writeRoute.Add(route);
                            }

                            ftr.route = route;
                            //                    ftr.duration_nemkell = bllPlanEdit.GetDuration(route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                        }

                    }


                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {

                        EventStopped.Set();
                        Completed = false;
                        break;
                    }
                
                    tspDiff = DateTime.Now - dtStart;
                    string infoText1 = i.ToString() + "/" + fromNodes.Count();
                    if (PMapIniParams.Instance.TestMode)
                        infoText1 += " " + tspDiff.Duration().TotalMilliseconds.ToString("#0") + " ms";
                    if (ProcessForm != null)
                    {
                        ProcessForm.SetInfoText( infoText1);
                        ProcessForm.NextStep();
                    }
                    this.SetNotifyIconText(infoText1);
                }

                m_bllRoute.WriteRoutesBulk(writeRoute, true);  //itt lehetne optimalizálni, hogy csak from-->to utak legyenek be\rva

                Completed = true;
                m_DB.Close();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
            }
        }

    }



}
