using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.LongProcess.Base;
using PMap.MapProvider;
using PMap.Route;
using PMap.DB;
using PMap.DB.Base;
using System.Data.SqlClient;
using GMap.NET;
using System.Threading;
using PMap.Forms;
using Map.LongProcess;
using PMap.BO;
using PMap.BLL;
using PMap.Common;

namespace PMap.LongProcess
{

    /// <summary>
    /// Útvonalszámítás process
    /// </summary>
    class CalcPMapRouteProcess : BaseLongProcess
    {
        public bool Completed { get; set; }

        private List<boRoute> m_CalcDistances = null;
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private string m_Hint = "";

        private bool m_savePoints = true;
        private bllRoute m_bllRoute;

        public CalcPMapRouteProcess(BaseProgressDialog p_Form, ThreadPriority p_ThreadPriority, string p_Hint, List<boRoute> p_CalcDistances, bool p_savePoints)
            : base(p_Form, p_ThreadPriority)
        {
            m_CalcDistances = p_CalcDistances;
            m_Hint = p_Hint;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_bllRoute = new bllRoute(m_DB);
            m_savePoints = p_savePoints;
        }

        public CalcPMapRouteProcess(ProcessNotifyIcon p_NotifyIcon, ThreadPriority p_ThreadPriority, string p_Hint, List<boRoute> p_CalcDistances, bool p_savePoints)
            : base(p_NotifyIcon, p_ThreadPriority)
        {
            m_CalcDistances = p_CalcDistances;
            m_Hint = p_Hint;

            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_bllRoute = new bllRoute(m_DB);

            m_savePoints = p_savePoints;

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
                    ProcessForm.SetInfoText(m_Hint.Trim() + " Inicializálás");
                }

                
                PMapRoutingProvider provider = new PMapRoutingProvider();
                RouteData.Instance.Init(m_DB, null);
                RectLatLng boundary = new RectLatLng();

                if (m_CalcDistances.Count > 0)
                {
                    List<int> fromNodes = m_CalcDistances.GroupBy(g => g.NOD_ID_FROM).Select(item => item.Key).ToList();
                    List<int> toNodes = m_CalcDistances.GroupBy(g => g.NOD_ID_TO).Select(item => item.Key).ToList();
                    List<int> allNodes = fromNodes.Union(toNodes).ToList();
                    boundary = m_bllRoute.getBoundary(allNodes);
                }

                List<string> aRZN_ID_LIST = m_CalcDistances.GroupBy(grp => grp.RZN_ID_LIST).Select(grp => grp.Key).ToList();
                Dictionary<string, List<int>[]> NeighborsArrFull = null;
                Dictionary<string, List<int>[]> NeighborsArrCut = null;

                RouteData.Instance.getNeigboursByBound(aRZN_ID_LIST, out NeighborsArrFull, out NeighborsArrCut, boundary);

                DateTime dtStartX2 = DateTime.Now;

                var calcNode = m_CalcDistances.GroupBy(gr => new { gr.NOD_ID_FROM, gr.RZN_ID_LIST }).ToDictionary(gr => gr.Key, gr => gr.Select(x => x.NOD_ID_TO).ToList());

                foreach (var item in calcNode.AsEnumerable())
                {

                    dtStart = DateTime.Now;

                    i++;
                    List<boRoute> results = provider.GetAllRoutes(item.Key.RZN_ID_LIST, item.Key.NOD_ID_FROM, item.Value, 
                                        NeighborsArrFull[item.Key.RZN_ID_LIST], NeighborsArrCut[item.Key.RZN_ID_LIST], 
                                        PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath);

                    //Eredmény adatbázisba írása minden csomópont kiszámolása után
                    m_bllRoute.WriteRoutes(results, m_savePoints);
                    /*
                    //Eredmény ellenőrzése Google-al
                    foreach (var ri in results)
                    {
                    
                        PointLatLng PositionFrom = m_bllRoute.GetPointLatLng(ri.Value.NOD_ID_FROM, m_conn.DB);
                        PointLatLng PositionTo = m_bllRoute.GetPointLatLng(ri.Value.NOD_ID_TO, m_conn.DB);
                        double duration = 0;

                        MapRoute route = PPlanCommonVars.Instance.RoutingProvider.GetRoute(PositionFrom, PositionTo, false, false, 10);
                        if (route != null)
                        {
                            string DurationText = "";

                            String[] sArr = route.Name.Split('/');

                            DurationText = sArr[1].Replace(")", "");
                            DurationText = DurationText.Replace("(", "");
                            DurationText = DurationText.Replace("mins", "");
                            DurationText = DurationText.Replace("min", "");
                            DurationText = DurationText.Replace("hours", ":");
                            DurationText = DurationText.Replace("hour", ":");

                            try
                            {
                                String[] sTimeArr = DurationText.Split(':');
                                if (sTimeArr.Length == 2)
                                    duration = Convert.ToDouble(sTimeArr[0]) * 60 + Convert.ToInt32(sTimeArr[1]);
                                else
                                    duration = Convert.ToDouble(sTimeArr[0]);
                            }
                            catch
                            {
                                duration = 0;
                            }

                            String sMsg = "{0}=>{1} [{2},{3}] távolság PMap:{4}, Google:{5}";

                            Util.Log2File(String.Format(sMsg, ri.Value.NOD_ID_FROM, ri.Value.NOD_ID_TO, PositionFrom, PositionTo, ri.Value.RouteDetail.First().Value.Distance / 1000, route.Distance));
 
                        }
                  
                    }
                    */


                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {

                        EventStopped.Set();
                        Completed = false;
                        break;
                    }



                    tspDiff = DateTime.Now - dtStart;
                    string infoText1 = i.ToString() + "/" + calcNode.Count();
                    if (PMapIniParams.Instance.TestMode)
                        infoText1 += " " + tspDiff.Duration().TotalMilliseconds.ToString("#0") + " ms";
                    //                ProcessForm.SetInfoText(m_Hint.Trim() + "=>" + Util.GetSysInfo().PadRight(15) + " " + infoText1.PadRight(25) + " NODE_ID:" + item.Key.ToString());
                    if (ProcessForm != null)
                    {
                        ProcessForm.SetInfoText(m_Hint.Trim() + infoText1);
                        ProcessForm.NextStep();
                    }
                    this.SetNotifyIconText(m_Hint.Trim() + infoText1);
                }

                //Eredmény adatbázisba írása
                //            m_bllRoute.WriteRoutes(results, m_conn.DB);
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
