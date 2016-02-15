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


        List<FTLRoute> m_lstRoutes = new List<FTLRoute>();
        List<FTLTruck> m_lstTrucks;
        Dictionary<string, boSpeedProfValues> m_sp;

        public FTLCalcRouteProcess(ProcessNotifyIcon p_NotifyIcon, List<FTLRoute> p_lstRoutes, List<FTLTruck> p_lstTrucks)
            : base(p_NotifyIcon, System.Threading.ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            m_bllRoute = new bllRoute(m_DB);

            m_lstRoutes = p_lstRoutes;
            m_lstTrucks = p_lstTrucks;

        }


        protected override void DoWork()
        {
            try
            {
                Completed = true;

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

 
                Dictionary<int, string> aRZN_ID_LIST = m_lstTrucks.GroupBy(grp => grp.RST_ID).ToDictionary(grp => grp.Key, grp => grp.Max( x=>x.RZN_ID_LIST));
                Dictionary<string, List<int>[]> NeighborsArrFull = null;
                Dictionary<string, List<int>[]> NeighborsArrCut = null;

                RouteData.Instance.getNeigboursByBound(aRZN_ID_LIST.Values.ToList(), out NeighborsArrFull, out NeighborsArrCut, boundary);


                DateTime dtStartX2 = DateTime.Now;
                foreach (int NOD_ID_FROM in fromNodes)
                {
                    i++;
                    dtStart = DateTime.Now;

                    foreach (var xRZN in aRZN_ID_LIST)
                    {
                        List<boRoute> results = provider.GetAllRoutes(xRZN.Value, NOD_ID_FROM, toNodes,
                                            NeighborsArrFull[xRZN.Value], NeighborsArrCut[xRZN.Value],
                                            PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);

                           
                        //A kiszámolt eredmények 'bedolgozása'
                        foreach (boRoute route in results)
                        {
                            //leválogatjuk, mely útvonalakra tartozik a számítás
                            List<FTLRoute> lstFTLR = m_lstRoutes.Where(x => x.fromNOD_ID == route.NOD_ID_FROM && x.toNOD_ID == route.NOD_ID_TO && x.RZN_ID_LIST == xRZN.Value).ToList();
                            foreach (FTLRoute ftr in lstFTLR)
                            {
                                ftr.route = route;
                                ftr.duration = bllPlanEdit.GetDuration(route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);

                                foreach (FTLTruck trk in m_lstTrucks)
                                {
                                    //Járműkategóriánként és EURO besorolásként kiszámított útdíjak
                                    if (ftr.Toll.Where(x => x.ETollCat == trk.ETollCat && x.EngineEuro == trk.EngineEuro).FirstOrDefault() == null)
                                    {


                                        string sLastETLCode = "";           // HIBALEHETŐSÉG: az útdíjat a teljesített és a teljesítendő útvonalakra külön számítjuk. Elképzelhető, hogy az eredményben
                                        // a részútvonalakat úgy adódnak össze, hogy két fizetős útszakasz összekapcsolódik, ezért arra a szakaszra csak egy útdíjat 
                                        // kell számolni. Az eredmény útdíját újra kell számolni !!!

                                        FTLRoute.FTLToll toll = new FTLRoute.FTLToll() { ETollCat = trk.ETollCat, EngineEuro = trk.EngineEuro,
                                            Toll = bllPlanEdit.GetToll(route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode)};

                                        ftr.Toll.Add(toll);
                                    }
                                }
                            }

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
                    //                ProcessForm.SetInfoText(m_Hint.Trim() + "=>" + Util.GetSysInfo().PadRight(15) + " " + infoText1.PadRight(25) + " NODE_ID:" + item.Key.ToString());
                    if (ProcessForm != null)
                    {
                        ProcessForm.SetInfoText( infoText1);
                        ProcessForm.NextStep();
                    }
                    this.SetNotifyIconText(infoText1);
                }
                Completed = true;
                m_DB.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }

    }



}
