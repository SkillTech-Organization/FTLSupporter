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
using static SWHInterface.BO.boXRouteSummary;

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
        private string m_RZN_ID_LIST;

        //ebből kell kiolvasni az eredményt
        public boJourneyFormResult Result { get; set; } = new boJourneyFormResult();

        public JourneyFormDataProcess(ProcessNotifyIcon p_NotifyIcon, List<boXRouteSection> p_lstRouteSection, boXTruck p_XTruck, string p_RZN_ID_LIST)
            : base(p_NotifyIcon, ThreadPriority.Normal)
        {
            m_lstRouteSection = p_lstRouteSection;
            m_XTruck = p_XTruck;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_bllRoute = new bllRoute(m_DB);
            m_RZN_ID_LIST = p_RZN_ID_LIST;
        }
        protected override void DoWork()
        {
            try
            {
                //Súly alapján megállapítjuk a behajtási övezeteket (RZN_ID_LIST)
                //

                m_rdt = m_bllRoute.GetRoadTypesToDict();
                /*
                var rd = "";
                foreach (var ds in PMapIniParams.Instance.dicSpeed)
                {
                    rd += $"{ds.Key}:{ds.Value},";
                }
                Util.Log2File($"  sebességprofil:{rd}", false);
                */

                RectLatLng boundary = new RectLatLng();
                List<int> nodes = m_lstRouteSection.Select(i => i.NOD_ID).ToList();
                boundary = m_bllRoute.getBoundary(nodes);



                Dictionary<string, List<int>[]> NeighborsFull = null;
                Dictionary<string, List<int>[]> NeighborsCut = null;

                var routePar = new CRoutePars() { RZN_ID_LIST = m_RZN_ID_LIST, Weight = m_XTruck.TRK_WEIGHT, Height = m_XTruck.TRK_XHEIGHT, Width = m_XTruck.TRK_XWIDTH };
           //     Util.Log2File($"  Weight:{m_XTruck.TRK_WEIGHT}, Height:{m_XTruck.TRK_XHEIGHT}, Width:{m_XTruck.TRK_XWIDTH}", false);

                RouteData.Instance.getNeigboursByBound(routePar, ref NeighborsFull, ref NeighborsCut, boundary, null);

                PMapRoutingProvider provider = new PMapRoutingProvider();
                var RouteVisS = new XRoute(XRoute.TY_SHORTEST, PMapMessages.M_PATH_SHORTEST);
                var RouteVisF = new XRoute(XRoute.TY_FASTEST, PMapMessages.M_PATH_FASTEST);


                string lastETLCODE_S = "", lastETLCODE_F = "";
                for (int i = 0; i < m_lstRouteSection.Count - 1; i++)
                {

                    var itemResult = new boXRouteSummary();
                    itemResult.FromPoint = m_lstRouteSection[i];
                    itemResult.ToPoint = m_lstRouteSection[i+1];


                    //Legrövidebb út
                    boRoute routeS = provider.GetRoute(m_lstRouteSection[i].NOD_ID, m_lstRouteSection[i + 1].NOD_ID, routePar,
                                    NeighborsFull[routePar.Hash],
                                    PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
                                    ECalcMode.ShortestPath);

                    if (routeS != null)
                    {
                        itemResult.ShortestRoute = genData(RouteVisS, routeS,
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
                        itemResult.FastestRoute = genData(RouteVisF, routeF,
                            m_lstRouteSection[i].NOD_ID, m_lstRouteSection[i + 1].NOD_ID,
                            m_XTruck, m_lstRouteSection[i].RouteSectionType, ref lastETLCODE_F);
                    }
                    /*
                    Util.Log2File($"  {i} {itemResult.FromPoint.DEP_NAME} ({itemResult.FromPoint.ZIP_NUM} {itemResult.FromPoint.ZIP_CITY} {itemResult.FromPoint.DEP_ADRSTREET}), lat:{itemResult.FromPoint.Lat}, lng:{itemResult.FromPoint.Lng}-->{itemResult.ToPoint.DEP_NAME} ({itemResult.ToPoint.ZIP_NUM} {itemResult.ToPoint.ZIP_CITY} {itemResult.ToPoint.DEP_ADRSTREET}), lat:{itemResult.ToPoint.Lat}, lng:{itemResult.ToPoint.Lng}", false);
                    Util.Log2File($"     NOD_ID: {itemResult.FromPoint.NOD_ID} -->{itemResult.ToPoint.NOD_ID}: shortest:{routeS.CalcDistance.ToString()}, fastest:{routeF.CalcDistance.ToString()}", false);
                    Util.Log2File($"     RouteS: {string.Join(",", routeS.Edges.Select(s => s.ID).ToList())}");
                    Util.Log2File($"     RouteF: {string.Join(",", routeF.Edges.Select(s => s.ID).ToList())}");
                    Util.Log2File($"     NOD_S : {string.Join(",", routeS.Edges.Select(s => s.NOD_ID_FROM).ToList())}");
                    Util.Log2File($"     NOD_F : {string.Join(",", routeF.Edges.Select(s => s.NOD_ID_FROM).ToList())}");
                    Util.Log2File($"     LenS  : {string.Join(",", routeS.Edges.Select(s => $"{s.ID}:{s.EDG_LENGTH}").ToList())}");
                    Util.Log2File($"     LenF  : {string.Join(",", routeF.Edges.Select(s => $"{s.ID}:{s.EDG_LENGTH}").ToList())}");
                    */

                    Result.SectionSummaries.Add(itemResult);
                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {
                        EventStopped.Set();
                        return;
                    }
                }

                //Az eredmény összeállítsa
                Result.TotalSummary.ShortestRoute.SumDistance = RouteVisS.SumDistance;
                Result.TotalSummary.ShortestRoute.SumDuration = RouteVisS.SumDuration;
                Result.TotalSummary.ShortestRoute.SumToll = RouteVisS.SumToll;
                Result.TotalSummary.ShortestRoute.SumDistanceEmpty = RouteVisS.SumDistanceEmpty;
                Result.TotalSummary.ShortestRoute.SumDurationEmpty = RouteVisS.SumDurationEmpty;
                Result.TotalSummary.ShortestRoute.SumTollEmpty = RouteVisS.SumTollEmpty;
                Result.TotalSummary.ShortestRoute.SumDistanceLoaded = RouteVisS.SumDistanceLoaded;
                Result.TotalSummary.ShortestRoute.SumDurationLoaded = RouteVisS.SumDurationLoaded;
                Result.TotalSummary.ShortestRoute.SumTollLoaded = RouteVisS.SumTollLoaded;

                Result.TotalSummary.FastestRoute.SumDistance = RouteVisF.SumDistance;
                Result.TotalSummary.FastestRoute.SumDuration = RouteVisF.SumDuration;
                Result.TotalSummary.FastestRoute.SumToll = RouteVisF.SumToll;
                Result.TotalSummary.FastestRoute.SumDistanceEmpty = RouteVisF.SumDistanceEmpty;
                Result.TotalSummary.FastestRoute.SumDurationEmpty = RouteVisF.SumDurationEmpty;
                Result.TotalSummary.FastestRoute.SumTollEmpty = RouteVisF.SumTollEmpty;
                Result.TotalSummary.FastestRoute.SumDistanceLoaded = RouteVisF.SumDistanceLoaded;
                Result.TotalSummary.FastestRoute.SumDurationLoaded = RouteVisF.SumDurationLoaded;
                Result.TotalSummary.FastestRoute.SumTollLoaded = RouteVisF.SumTollLoaded;
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
        private boXRouteSummaryDetails genData(XRoute p_RouteVisSummary, boRoute p_route,
                int p_NOD_ID_FROM, int p_NOD_ID_TO, boXTruck p_Truck,
               boXRouteSection.ERouteSectionType p_RouteSectionType, ref string p_lastETLCODE)
        {
            boXRouteSummaryDetails res = new boXRouteSummaryDetails();
            Dictionary<int, int> speeds = new Dictionary<int, int>();
            speeds.Add(1, p_Truck.SPV_VALUE1);
            speeds.Add(2, p_Truck.SPV_VALUE2);
            speeds.Add(3, p_Truck.SPV_VALUE3);
            speeds.Add(4, p_Truck.SPV_VALUE4);
            speeds.Add(5, p_Truck.SPV_VALUE5);
            speeds.Add(6, p_Truck.SPV_VALUE6);
            speeds.Add(7, p_Truck.SPV_VALUE7);
            double dTollMultiplier = bllPlanEdit.GetTollMultiplier(p_Truck.TRK_ETOLLCAT, p_Truck.TRK_ENGINEEURO);

            p_RouteVisSummary.SumDistance += p_route.DST_DISTANCE;
            res.SumDistance = p_route.DST_DISTANCE;

            p_RouteVisSummary.SumDuration += bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);
            res.SumDuration = bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);


            /*******/
            if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
            {
                p_RouteVisSummary.SumDistanceEmpty += p_route.DST_DISTANCE;
                p_RouteVisSummary.SumDurationEmpty += bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);

                res.SumDistanceEmpty = p_route.DST_DISTANCE;
                res.SumDurationEmpty = bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);

            }


            /*******/
            if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
            {
                p_RouteVisSummary.SumDistanceLoaded += p_route.DST_DISTANCE;
                p_RouteVisSummary.SumDurationLoaded += bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);

                res.SumDistanceLoaded = p_route.DST_DISTANCE;
                res.SumDurationLoaded = bllPlanEdit.GetDuration(p_route.Edges, speeds, Global.defWeather);

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
                    p_RouteVisSummary.Details.Add(detail);
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
                        p_RouteVisSummary.SumToll += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;
                        res.SumToll += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;

                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
                        {
                            p_RouteVisSummary.SumTollEmpty += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;
                            res.SumTollEmpty += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;
                        }
                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
                        {
                            p_RouteVisSummary.SumTollLoaded += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;
                            res.SumTollLoaded += edge.Tolls[p_Truck.TRK_ETOLLCAT] * dTollMultiplier;
                        }
                    }
                    p_lastETLCODE = edge.EDG_ETLCODE;
                }
            }
            return res;
        }
 
    }
}
