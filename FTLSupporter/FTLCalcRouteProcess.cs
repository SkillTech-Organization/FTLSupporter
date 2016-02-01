using GMap.NET;
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


        private List<int[]> m_lstNOD_ID;
        List<FTLTruck> m_lstTrucks;
        Dictionary<string, boSpeedProfValues> m_sp;

        public FTLCalcRouteProcess(List<int[]> p_lstNOD_ID, List<FTLTruck> p_lstTrucks)
            : base(new BaseSilngleProgressDialog(0, p_lstNOD_ID.Count - 1, FTLMessages.M_CALCROUTES, false), System.Threading.ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            m_bllRoute = new bllRoute(m_DB);
  
            m_lstNOD_ID = p_lstNOD_ID;
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

                List<int> fromNodes = m_lstNOD_ID.GroupBy(g => g[0]).Select(x => x.Key).ToList();
                List<int> toNodes = m_lstNOD_ID.GroupBy(g => g[1]).Select(x => x.Key).ToList();
                List<int> allNodes = fromNodes.Union(toNodes).ToList();
                boundary = m_bllRoute.getBoundary(allNodes);

 
                List<string> aRZN_ID_LIST = m_lstTrucks.GroupBy(grp => grp.RZN_ID_LIST).Select(grp => grp.Key).ToList();
                Dictionary<string, List<int>[]> NeighborsArrFull = null;
                Dictionary<string, List<int>[]> NeighborsArrCut = null;

                RouteData.Instance.getNeigboursByBound(aRZN_ID_LIST, out NeighborsArrFull, out NeighborsArrCut, boundary);

                DateTime dtStartX2 = DateTime.Now;
                foreach (int NOD_ID_FROM in fromNodes)
                {
                    i++;
                    dtStart = DateTime.Now;

                    foreach (string sRZN in aRZN_ID_LIST)
                    {
                        List<boRoute> results = provider.GetAllRoutes(sRZN, NOD_ID_FROM, toNodes,
                                            NeighborsArrFull[sRZN], NeighborsArrCut[sRZN],
                                            PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);

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
