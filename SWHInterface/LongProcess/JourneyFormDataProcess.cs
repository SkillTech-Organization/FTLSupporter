using GMap.NET;
using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.BO.DataXChange;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.LongProcess.Base;
using PMapCore.MapProvider;
using PMapCore.Route;
using PMapCore.Strings;
using SWHInterface.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWHInterface.LongProcess
{
    /*
PMap menetlevelek feldolgozása funkció (egyszerűsített verzió).
Feladat: Egy lerakó-listára egy járműre meg kell jeleníteni a legrövidebb és  leggyorsabb  útvonalat.  A kiszámolt útvonalakat táblázatban és térképen is meg kell jeleníteni.
Megvalósítás
A menetlevelek feldolgozása egy új képernyőn megjelenő PMap funkció. A képernyő  minden induláskor a kapott lerakó lista alapján elvégzi az útvonalszámítást.
•	Input adatok:
•	Lerakó ID-k listája. A megadott adatokat az ellenőrző-képernyőn módosítani, törölni nem lehet.
•	Jármű ID
•	A képernyő három részre van osztva:
•	Lerakógrid: Az útvonal pontjait tartalmazó grid.
•	Eredménygrid: 2 db sora lesz (számítástípusonként egy-egy sor).  A sorok oszlopaiban az összes km, menetidő, költség van megjelenítve. A grid tartalma excelbe exportálható.
•	Térkép: Ki-be kapcsolható módon az egyes számítástípusok alapján meghatározott útvonal. A lerakógrid egyes pontjaira kattintva a térképen a megfelelő pont kijelölésre kerül.
•	Minden útvonalról kérhető egy táblázatos részletező, amelyik excelbe exportálható.
Feladat lépései:
•	PMap:
•	Menetlevelek feldolgozása képernyő 
*/
    internal class JourneyFormDataProcess : BaseLongProcess
    {


        private List<boXRouteSection> m_lstRouteSection;
        private boXTruck m_XTruck;
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private bllRoute m_bllRoute = null;
        private Dictionary<int, string> m_rdt = null;   //Úttípusok


        //ebből kell kiolvasni az eredményt
        public boXRouteSummary Result { get; set; } = null;

        public JourneyFormDataProcess(ProcessNotifyIcon p_NotifyIcon, List<boXRouteSection> p_lstRouteSection, boXTruck p_XTruck)
            : base(p_NotifyIcon, ThreadPriority.Normal)
        {
            m_lstRouteSection = p_lstRouteSection;
            m_XTruck = p_XTruck;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_bllRoute = new bllRoute(m_DB);
        }
        protected override void DoWork()
        {
            try
            {
                //Súly alapján megállapítjuk a behajtási övezeteket (RZN_ID_LIST)
                //
                string RZN_ID_LIST = "";
                if (m_XTruck.TRK_WEIGHT <= Global.RST_WEIGHT35)
                    RZN_ID_LIST = GetRestZonesByRST_ID(Global.RST_MAX35T);
                else if (m_XTruck.TRK_WEIGHT <= Global.RST_WEIGHT75)
                    RZN_ID_LIST = GetRestZonesByRST_ID(Global.RST_MAX75T);
                else if (m_XTruck.TRK_WEIGHT <= Global.RST_WEIGHT120)
                    RZN_ID_LIST = GetRestZonesByRST_ID(Global.RST_MAX12T);
                else if (m_XTruck.TRK_WEIGHT > Global.RST_WEIGHT120)
                    RZN_ID_LIST = GetRestZonesByRST_ID(Global.RST_BIGGER12T);
                else
                    RZN_ID_LIST = GetRestZonesByRST_ID(Global.RST_NORESTRICT);

                m_rdt = m_bllRoute.GetRoadTypesToDict();

                RectLatLng boundary = new RectLatLng();
                List<int> nodes = m_lstRouteSection.Select(i => i.NOD_ID).ToList();
                boundary = m_bllRoute.getBoundary(nodes);



                Dictionary<string, List<int>[]> NeighborsFull = null;
                Dictionary<string, List<int>[]> NeighborsCut = null;

                var routePar = new CRoutePars() { RZN_ID_LIST = RZN_ID_LIST, Weight = m_XTruck.TRK_WEIGHT, Height = m_XTruck.TRK_XHEIGHT, Width = m_XTruck.TRK_XWIDTH };

                RouteData.Instance.getNeigboursByBound(routePar, ref NeighborsFull, ref NeighborsCut, boundary, null);

                PMapRoutingProvider provider = new PMapRoutingProvider();
                var RouteVisS = new XRoute(XRoute.TY_SHORTEST, PMapMessages.M_PATH_SHORTEST);
                var RouteVisF = new XRoute(XRoute.TY_FASTEST, PMapMessages.M_PATH_FASTEST);


                string lastETLCODE_S = "", lastETLCODE_F = "";
                for (int i = 0; i < m_lstRouteSection.Count - 1; i++)
                {
Console.WriteLine($"{i} --> {m_lstRouteSection[i].DEP_NAME}");

                    //Legrövidebb út
                    boRoute routeS = provider.GetRoute(m_lstRouteSection[i].NOD_ID, m_lstRouteSection[i + 1].NOD_ID, routePar,
                                    NeighborsFull[routePar.Hash],
                                    PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
                                    ECalcMode.ShortestPath);

                    if (routeS != null)
                    {
                        genData(RouteVisS, routeS,
                            m_lstRouteSection[i].NOD_ID, m_lstRouteSection[i + 1].NOD_ID,
                            m_XTruck, m_lstRouteSection[i].RouteSectionType, ref lastETLCODE_S);
                    }

                    //Leggyorsabb út
                    boRoute routeF = provider.GetRoute(m_lstRouteSection[i].NOD_ID, m_lstRouteSection[i + 1].NOD_ID, routePar,
                                    NeighborsFull[routePar.Hash],
                                    PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
                                    ECalcMode.FastestPath);

                    if (routeF != null)
                    {
                        genData(RouteVisF, routeF,
                            m_lstRouteSection[i].NOD_ID, m_lstRouteSection[i + 1].NOD_ID,
                            m_XTruck, m_lstRouteSection[i].RouteSectionType, ref lastETLCODE_F);
                    }


                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {
                        EventStopped.Set();
                        return;
                    }
                }

                //Az eredmény összeállítsa
                Result = new boXRouteSummary();
                Result.ShortestRoute.SumDistance = RouteVisS.SumDistance;
                Result.ShortestRoute.SumDuration = RouteVisS.SumDuration;
                Result.ShortestRoute.SumToll = RouteVisS.SumToll;
                Result.ShortestRoute.SumDistanceEmpty = RouteVisS.SumDistanceEmpty;
                Result.ShortestRoute.SumDurationEmpty = RouteVisS.SumDurationEmpty;
                Result.ShortestRoute.SumTollEmpty = RouteVisS.SumTollEmpty;
                Result.ShortestRoute.SumDistanceLoaded = RouteVisS.SumDistanceLoaded;
                Result.ShortestRoute.SumDurationLoaded = RouteVisS.SumDurationLoaded;
                Result.ShortestRoute.SumTollLoaded = RouteVisS.SumTollLoaded;

                Result.FastestRoute.SumDistance = RouteVisF.SumDistance;
                Result.FastestRoute.SumDuration = RouteVisF.SumDuration;
                Result.FastestRoute.SumToll = RouteVisF.SumToll;
                Result.FastestRoute.SumDistanceEmpty = RouteVisF.SumDistanceEmpty;
                Result.FastestRoute.SumDurationEmpty = RouteVisF.SumDurationEmpty;
                Result.FastestRoute.SumTollEmpty = RouteVisF.SumTollEmpty;
                Result.FastestRoute.SumDistanceLoaded = RouteVisF.SumDistanceLoaded;
                Result.FastestRoute.SumDurationLoaded = RouteVisF.SumDurationLoaded;
                Result.FastestRoute.SumTollLoaded = RouteVisF.SumTollLoaded;


            }

            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;

            }
            finally
            {
                EventStopped.Set();
                if (m_notifyIcon != null)
                    m_notifyIcon.StopNotify(this);
            }

        }
        private void genData(XRoute p_RouteVis, boRoute p_route,
                int p_NOD_ID_FROM, int p_NOD_ID_TO, boXTruck p_Truck,
               boXRouteSection.ERouteSectionType p_RouteSectionType, ref string p_lastETLCODE)
        {
            Dictionary<int, int> speeds = new Dictionary<int, int>();
            speeds.Add(1, p_Truck.SPV_VALUE1);
            speeds.Add(2, p_Truck.SPV_VALUE2);
            speeds.Add(3, p_Truck.SPV_VALUE3);
            speeds.Add(4, p_Truck.SPV_VALUE4);
            speeds.Add(5, p_Truck.SPV_VALUE5);
            speeds.Add(6, p_Truck.SPV_VALUE6);
            speeds.Add(7, p_Truck.SPV_VALUE7);
            double dTollMultiplier = bllPlanEdit.GetTollMultiplier(p_Truck.TRK_ETOLLCAT, p_Truck.TRK_ENGINEEURO);

            p_RouteVis.SumDistance += p_route.DST_DISTANCE;

            p_RouteVis.SumDuration += bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);


            /*******/
            if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
            {
                p_RouteVis.SumDistanceEmpty += p_route.DST_DISTANCE;
                p_RouteVis.SumDurationEmpty += bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);
            }


            /*******/
            if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
            {
                p_RouteVis.SumDistanceLoaded += p_route.DST_DISTANCE;
                p_RouteVis.SumDurationLoaded += bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);
            }


            String LastEdgeName = "";
            String LastRoadType = "";
            bool LastOneWay = false;
            bool LastDestTraffic = false;
            string LastWZone = "";
            double LastSpeed = -1;
            string LastETLCODE = "";
            XRouteDetails detail = null;
            foreach (boEdge edge in p_route.Edges)
            {

                double currSpeed = 0;
                currSpeed = speeds[edge.RDT_VALUE];

                if (detail == null ||
                    LastEdgeName != edge.EDG_NAME ||
                    LastRoadType != edge.RDT_VALUE.ToString() + "-" + m_rdt[edge.RDT_VALUE] ||
                    LastOneWay != edge.EDG_ONEWAY ||
                    LastDestTraffic != edge.EDG_DESTTRAFFIC ||
                    LastWZone != edge.WZONE ||
                    LastSpeed != currSpeed ||
                    LastETLCODE != edge.EDG_ETLCODE)
                {
                    detail = new XRouteDetails(p_route, p_RouteSectionType);
                    p_RouteVis.Details.Add(detail);
                    //Részletező
                    detail.NOD_ID_FROM = p_NOD_ID_FROM;
                    detail.NOD_ID_TO = p_NOD_ID_TO;
                    detail.Text = edge.EDG_NAME;
                    detail.Speed = currSpeed;
                    detail.RoadType = edge.RDT_VALUE.ToString() + "-" + m_rdt[edge.RDT_VALUE];
                    detail.WZone = edge.WZONE;
                    detail.OneWay = edge.EDG_ONEWAY;
                    detail.DestTraffic = edge.EDG_DESTTRAFFIC;
                    detail.EDG_ETLCODE = edge.EDG_ETLCODE;
                    detail.EDG_MAXWEIGHT = edge.EDG_MAXWEIGHT;
                    detail.EDG_MAXHEIGHT = edge.EDG_MAXHEIGHT;
                    detail.EDG_MAXWIDTH = edge.EDG_MAXWIDTH;
                    detail.Dist = 0;
                    detail.Duration = 0;
                    detail.Toll = 0;


                    LastEdgeName = edge.EDG_NAME;
                    LastRoadType = edge.RDT_VALUE.ToString() + "-" + m_rdt[edge.RDT_VALUE];
                    LastOneWay = edge.EDG_ONEWAY;
                    LastDestTraffic = edge.EDG_DESTTRAFFIC;
                    LastWZone = edge.WZONE;
                    LastSpeed = currSpeed;
                    LastETLCODE = edge.EDG_ETLCODE;

                }
                detail.Dist += edge.EDG_LENGTH;
                detail.Duration += edge.EDG_LENGTH / (currSpeed / 3.6 * 60 * Global.defWeather);

                if (p_Truck.TRK_ETOLLCAT > 1 && p_lastETLCODE != edge.EDG_ETLCODE)
                {
                    //Az 
                    detail.Toll += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;

                    if (edge.EDG_ETLCODE.Length > 0)
                    {
                        p_RouteVis.SumToll += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;

                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
                            p_RouteVis.SumTollEmpty += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;
                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
                            p_RouteVis.SumTollLoaded += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;

                    }
                    p_lastETLCODE = edge.EDG_ETLCODE;
                }
            }
        }


        private string GetRestZonesByRST_ID(int p_RST)
        {
            string RZN_ID_LIST = "";
            if (PMapCommonVars.Instance.RZN_ID_LISTCahce.ContainsKey(p_RST))
            {
                RZN_ID_LIST = PMapCommonVars.Instance.RZN_ID_LISTCahce[p_RST];
            }
            else
            {
                RZN_ID_LIST = m_bllRoute.GetRestZonesByRST_ID(p_RST);
                PMapCommonVars.Instance.RZN_ID_LISTCahce.Add(p_RST, RZN_ID_LIST);
            }
            return RZN_ID_LIST;

        }
 
    }
}
