using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.LongProcess.Base;
using PMapCore.MapProvider;
using PMapCore.Route;
using PMapCore.DB;
using PMapCore.DB.Base;
using System.Data.SqlClient;
using GMap.NET;
using System.Threading;
using PMapCore.Forms;
using Map.LongProcess;
using PMapCore.BO;
using PMapCore.BLL;
using PMapCore.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime;

namespace PMapCore.LongProcess
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
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut );
            m_bllRoute = new bllRoute(m_DB);
            m_savePoints = p_savePoints;
            Completed = false;
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
            Completed = false;
        }

        protected override void DoWork()
        {
            MemoryFailPoint memFailPoint = null;
            try
            {
                long availe = GC.GetTotalMemory(false);
                long allocated = (int)(availe / 1024 / 1024) < 40 ? (int)(availe / 1024 / 1024) : 60;
                memFailPoint = new MemoryFailPoint((int)allocated);
                Util.Log2File("CalcPMapRouteProcess Allocated: " + allocated.ToString());

                Completed = false;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


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

                //Térkép készítése minden behajtásiövezet-listára. Csak akkora méretű térképet használunk,
                //amelybe beleférnek (kis ráhagyással persze) a lerakók.
                
                //Dictionary<string, List<int>[]> NeighborsArrFull = null;
                //Dictionary<string, List<int>[]> NeighborsArrCut = null;
                List<CRoutePars> routePars = m_CalcDistances.GroupBy(g => new { g.RZN_ID_LIST, g.DST_MAXWEIGHT, g.DST_MAXHEIGHT, g.DST_MAXWIDTH })
                    .Select(s => new CRoutePars() { RZN_ID_LIST = s.Key.RZN_ID_LIST, Weight = s.Key.DST_MAXWEIGHT, Height = s.Key.DST_MAXHEIGHT, Width = s.Key.DST_MAXWIDTH }).ToList();


                DateTime dtStartX2 = DateTime.Now;

                var maxCnt = m_CalcDistances.GroupBy(gr => new { gr.NOD_ID_FROM, gr.RZN_ID_LIST, gr.DST_MAXWEIGHT, gr.DST_MAXHEIGHT, gr.DST_MAXWIDTH }).Count();
                Random random = new Random();
                int flushcnt = random.Next(9000, 12000);

                List<boRoute> results = new List<boRoute>();
                int itemNo = 0;


    /*
                Dictionary<string, List<int>[]> NeighborsArrFull = null;
                Dictionary<string, List<int>[]> NeighborsArrCut = null;
                RouteData.Instance.getNeigboursByBound(routePars, ref NeighborsArrFull, ref NeighborsArrCut, boundary, null);
*/

                foreach (var routePar in routePars)
                {
                    List<int>[] MapFull = null;
                    List<int>[] MapCut = null;
                    RouteData.Instance.PrepareMap(routePar, ref MapFull, ref MapCut, boundary, null);

                    var dicCalcNodes = m_CalcDistances.Where(w => w.RZN_ID_LIST == routePar.RZN_ID_LIST &&
                                                        w.DST_MAXWEIGHT == routePar.Weight &&
                                                        w.DST_MAXHEIGHT == routePar.Height &&
                                                        w.DST_MAXWIDTH == routePar.Width)
                                                        .GroupBy(g=>g.NOD_ID_FROM)
                                                        .ToDictionary(gr => gr.Key, gr => gr.Select(x => x.NOD_ID_TO).ToList());

                   foreach (var calcNode in dicCalcNodes.AsEnumerable())
                    {
 
                        dtStart = DateTime.Now;
                        itemNo++;


                        List<int> lstToNodes = calcNode.Value;

                        //megj: nins routePar null ellenőrzés, hogy szálljon el, ha valami probléma van
                        //
                        results.AddRange(provider.GetAllRoutes(routePar, calcNode.Key, lstToNodes,
                                            MapFull,
                                            PMapIniParams.Instance.CutMapForRouting && MapCut != null ? MapCut : null,
                                            PMapIniParams.Instance.FastestPath ? ECalcMode.FastestPath : ECalcMode.ShortestPath));

                        //Eredmény adatbázisba írása minden csomópont kiszámolása után -- NEM, a BULK insertet használjuk !!!
                        //m_bllRoute.WriteRoutes(results, m_savePoints);

                        if (results.Count() >= flushcnt)
                        {
                            ProcessForm.SetInfoText("Kiírás...");
                            m_bllRoute.WriteRoutesBulk(results, m_savePoints);
                            results = new List<boRoute>();
                            Util.Log2File("CalcPMapRouteProcess WriteRoutesBulk: " + GC.GetTotalMemory(false).ToString());
                        }
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
                            return;
                        }



                        tspDiff = DateTime.Now - dtStart;
                        string infoText1 = itemNo.ToString() + "/" + (maxCnt.ToString());
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
                }


                //Eredmény adatbázisba írása
                ProcessForm.SetInfoText("Kiírás...");
                m_bllRoute.WriteRoutesBulk(results, m_savePoints);
                Completed = true;
                m_DB.Close();

            }
            catch (InsufficientMemoryException ex)
            {
               Util.ExceptionLog(ex);
                Util.Log2File("CalcPMapRouteProcess InsufficientMemoryException AVAILABLE:" + GC.GetTotalMemory(false).ToString());
                throw;
            }
            catch (OutOfMemoryException ex)
            {
                Util.ExceptionLog(ex);
                Util.Log2File("CalcPMapRouteProcess OutOfMemoryException AVAILABLE:" + GC.GetTotalMemory(false).ToString());
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (memFailPoint != null)
                {
                    memFailPoint.Dispose();
                }
            }
        }

    }
}
