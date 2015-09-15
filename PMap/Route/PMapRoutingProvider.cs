using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.MapProviders;
using GMap.NET;
using PMap.DB.Base;
using System.Data;
using PMap.Route;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Odbc;
using PMap.DB;
using PMap.BO;
using PMap.Common;

namespace PMap.MapProvider
{

    //Üzemmódok
    //
    public enum ECalcMode
    {
        ShortestPath,
        FastestPath
    }



    /// <summary>
    /// A multhread miatt példányosíthatónak kell lennie az osztálynak!!!
    /// </summary>
    public class PMapRoutingProvider
    {


        private List<int>[] m_computedNeighborsArr = null;              //A sebességprofilhoz tartozó korlátozásokkal figyelembe vett csomópontkapcsolatok


        private HashSet<int> m_targetNodes;
        public PMapRoutingProvider()
        {
        }

        ///
        /// Egy p_NOD_ID_FROM pontból az összes p_ListNOD_ID_TO pontba számított legrövidebb út számítása
        //
        public List<boRoute> GetAllRoutes(string p_RZN_ID_LIST, int p_NOD_ID_FROM, List<int> p_ListNOD_ID_TO, List<int>[] p_neighborsArrFull, List<int>[] p_neighborsArrCut, ECalcMode p_calcMode)
        {

            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            List<boRoute> result = new List<boRoute>();         //útvonal leíró az összes target-re
            DateTime dtStart = DateTime.Now;
            RouteCalculator calcEngine;

            if (p_calcMode == ECalcMode.ShortestPath)
                calcEngine = new RouteCalculator(RouteData.Instance.NodeCount,
                                                 new RouteCalculator.GetInternodeCost(getShortestPath),
                                                 new RouteCalculator.GetNodeNeigbors(getNeigbors));
            else
                calcEngine = new RouteCalculator(RouteData.Instance.NodeCount,
                                                 new RouteCalculator.GetInternodeCost(getFastestPath),
                                                 new RouteCalculator.GetNodeNeigbors(getNeigbors));




            m_targetNodes = new HashSet<int>(p_ListNOD_ID_TO);
            RouteCalculator.RouteCalcResult optimizedPathsForAllDest = null;
            RouteCalculator.RouteCalcResult optimizedPathsForAllDestNOCUT = null;

            if (PMapIniParams.Instance.CutMapForRouting && p_neighborsArrCut.Length > 0)
                m_computedNeighborsArr = p_neighborsArrCut;
            else
                m_computedNeighborsArr = p_neighborsArrFull;

            optimizedPathsForAllDest = calcEngine.CalcAllOptimizedPaths(p_NOD_ID_FROM, IsComputeAllRoutesFinish);


            foreach (int NOD_ID_TO in p_ListNOD_ID_TO)
            {

                if (p_NOD_ID_FROM == 299396 && NOD_ID_TO == 360892)
                    Console.WriteLine("x");

                int[] optimizedPath = new int[0];
                if (optimizedPathsForAllDest != null)
                {
                    optimizedPath = calcEngine.GetOptimizedPath(p_NOD_ID_FROM, NOD_ID_TO, optimizedPathsForAllDest.MinimumPath);
                    
                    //kivágott térképen nem találtunk útvonalat, próbálkozunk a teljessel
                    if (optimizedPath.Count() <= 1 && PMapIniParams.Instance.CutMapForRouting)
                    {
                        //ha még nem számoltunk útvonalat a teljes térképpel, megtesszük
                        if (optimizedPathsForAllDestNOCUT == null)
                        {
                            m_computedNeighborsArr = p_neighborsArrFull;
                            m_targetNodes = new HashSet<int>(p_ListNOD_ID_TO);
                            optimizedPathsForAllDestNOCUT = calcEngine.CalcAllOptimizedPaths(p_NOD_ID_FROM, IsComputeAllRoutesFinish);
                        }

                        //lekérdezzük 
                        optimizedPath = calcEngine.GetOptimizedPath(p_NOD_ID_FROM, NOD_ID_TO, optimizedPathsForAllDestNOCUT.MinimumPath);

                    }
                }
                else
                {
             //       Util.Log2File("Null optimizedPaths NOD_ID_FROM=" + p_NOD_ID_FROM.ToString() + ", NOD_ID_TO=" + NOD_ID_TO.ToString() + ", RZN_ID_LIST=" + p_RZN_ID_LIST);
                }
                result.Add(getRouteInfo(p_RZN_ID_LIST, p_NOD_ID_FROM, NOD_ID_TO, optimizedPath));
            }
            Console.WriteLine("GetAllRoutes " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());
            return result;
        }


        private bool IsComputeAllRoutesFinish(int p_computedNode)
        {
            if (m_targetNodes.Contains(p_computedNode))
            {
                m_targetNodes.Remove(p_computedNode);
            }
            return m_targetNodes.Count == 0;
        }

        /// <summary>
        /// Két node közötti legrövidebb útvonal. 
        /// Részletesebb lekérdezéshez a GetRouteDetails() metódus használandó
        /// </summary>
        /// <param name="p_NOD_ID_FROM"></param>
        /// <param name="p_NOD_ID_TO"></param>
        /// <returns></returns>
        public boRoute GetRoute(string p_RZN_ID_LIST, int p_NOD_ID_FROM, int p_NOD_ID_TO, List<int>[] p_neighborsArrFull, List<int>[] p_neighborsArrCut, ECalcMode p_calcMode)
        {
            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            DateTime dtStart = DateTime.Now;
            RouteCalculator calcEngine;

            if (p_calcMode == ECalcMode.ShortestPath)
                calcEngine = new RouteCalculator(RouteData.Instance.NodeCount,
                                                 new RouteCalculator.GetInternodeCost(getShortestPath),
                                                 new RouteCalculator.GetNodeNeigbors(getNeigbors));
            else
                calcEngine = new RouteCalculator(RouteData.Instance.NodeCount,
                                                 new RouteCalculator.GetInternodeCost(getFastestPath),
                                                 new RouteCalculator.GetNodeNeigbors(getNeigbors));


            int[] optimizedPath = new int[0];

            if (PMapIniParams.Instance.CutMapForRouting && p_neighborsArrCut.Length > 0)
            {
                m_computedNeighborsArr = p_neighborsArrCut;
                optimizedPath = calcEngine.CalcOneOptimizedPath(p_NOD_ID_FROM, p_NOD_ID_TO);
            }
            //ha kivágott térképen nem találunk útvonalat, megpróbáljuk a teljes térképpel
            if (optimizedPath.Count() <= 1)
            {

                m_computedNeighborsArr = p_neighborsArrFull;
                optimizedPath = calcEngine.CalcOneOptimizedPath(p_NOD_ID_FROM, p_NOD_ID_TO);
            }
            var result = getRouteInfo(p_RZN_ID_LIST, p_NOD_ID_FROM, p_NOD_ID_TO, optimizedPath);
            Console.WriteLine("GetRoute " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());


            return result;
        }


        /// <summary>
        /// Útvonal koordináták összeállítása az eredménytömbből
        /// </summary>
        /// <param name="p_optimizedPath"></param>
        /// <returns></returns>
        private MapRoute getMapRoute(int[] p_optimizedPath, int p_NOD_ID_FROM)
        {

            List<PointLatLng> pathPoints = new List<PointLatLng>();

            pathPoints.Add(RouteData.Instance.NodePositions[p_NOD_ID_FROM]);                        //legelső pont nincs a p_shortestPath-ban!
            pathPoints.AddRange(p_optimizedPath.Select(x => RouteData.Instance.NodePositions[x]));


            return new MapRoute(pathPoints, "");
        }

        private boRoute getRouteInfo(string p_RZN_ID_LIST, int p_NOD_ID_FROM, int p_NOD_ID_TO, int[] p_optimizedPath)
        {
            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            boRoute routeInfo = new boRoute()
            {
                NOD_ID_FROM = p_NOD_ID_FROM,
                RZN_ID_LIST = p_RZN_ID_LIST,
                NOD_ID_TO = p_NOD_ID_TO,
                Route = getMapRoute(p_optimizedPath, p_NOD_ID_FROM),
                Edges = new List<boEdge>()
            };
            
            try
            {
                // edge-k összevadászása
                if (p_optimizedPath.Count() > 0)
                {
                    //Megj.: p_shortestPath nem tartalmazza a kiindulási node-ot. Emiatt van a variálás
                    int NodeFrom = p_NOD_ID_FROM;               //Legelső elem
                    int NodeTo = p_optimizedPath[0];            //A végelemet mindig tartalmazza, azaaz p_optimizedPath.Count()==1  esetén nincs eredmény
                    boEdge edge = null;

                    if (RouteData.Instance.Edges.TryGetValue(NodeFrom.ToString() + "," + NodeTo.ToString(), out edge))
                    {
                        routeInfo.Edges.Add(edge);

                        //A többi elem
                        for (int i = 0; i < p_optimizedPath.Count() - 1; i++)
                        {
                            NodeFrom = p_optimizedPath[i];
                            NodeTo = p_optimizedPath[i + 1];
                            if (!RouteData.Instance.Edges.TryGetValue(NodeFrom.ToString() + "," + NodeTo.ToString(), out edge))
                                throw new NullReferenceException("null edge:" + NodeFrom.ToString() + "," + NodeTo.ToString());

                            routeInfo.Edges.Add(edge);
                        }

                    }
                    else
                    {
                        //az p_optimizedPath mindig tartalmazza a végelemet. 
                        //Ha nincs útvonal a két pont közözz, akkor p_optimizedPath értéke 1,
                        //Ha 1-nél nagyobb a p_optimizedPath mérete és nem találunk edge-t, gebasz van
                        if (p_optimizedPath.Count() > 1)
                            throw new NullReferenceException("null edge!");
                    }


                }

                //Távolság kiszámolása
                routeInfo.DST_DISTANCE= routeInfo.Edges.Sum(e => e.EDG_LENGTH);
    
                return routeInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }

        }

        //node-k közötti távolságok lekérdezése
        /// Külső változók (gyorsítás miatt vannak külön tárolva)
        //      m_computedEdges       :A sebességprofilhoz tartozó korlátozásokkal figyelembe vett élek
        private float getShortestPath(int nodeFrom, int nodeTo)
        {
            string sKey = nodeFrom.ToString() + "," + nodeTo.ToString();
            boEdge retval;
            if (RouteData.Instance.Edges.TryGetValue(sKey, out retval))
            {
                return retval.EDG_LENGTH;
            }
            return float.MaxValue;
        }

        /// Külső változók (gyorsítás miatt vannak külön tárolva)
        //      m_computedEdges       :A sebességprofilhoz tartozó korlátozásokkal figyelembe vett élek
        private float getFastestPath(int nodeFrom, int nodeTo)
        {
            string sKey = nodeFrom.ToString() + "," + nodeTo.ToString();
            try
            {
                boEdge retval;
                if (RouteData.Instance.Edges.TryGetValue(sKey, out retval))
                {
                    return retval.CalcDuration;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return float.MaxValue;
        }

        //Egy node-ból elérhető node-ok visszaadása
        /// <summary>
        // Külső változók (gyorsítás miatt vannak külön tárolva)
        //     m_computedNeighborsArr:A sebességprofilhoz tartozó korlátozásokkal figyelembe vett csomópontkapcsolatok
        /// </summary>
        /// <param name="nodeFrom"></param>
        /// <returns></returns>
        private List<int> getNeigbors(int nodeFrom)
        {
            return m_computedNeighborsArr[nodeFrom];
        }


    }
}