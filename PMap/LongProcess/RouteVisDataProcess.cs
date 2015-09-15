using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.LongProcess.Base;
using PMap.DB.Base;
using PMap.Localize;
using PMap;
using PMap.Common;
using PMap.BLL;
using PMap.BO;
using GMap.NET;
using PMap.Route;
using PMap.MapProvider;
using PMap.BO.DataXChange;

namespace Map.LongProcess
{

    class RouteVisDataProcess : BaseLongProcess
    {
        private SQLServerConnect m_conn = null;                 //A multithread miatt saját connection kell
        private bllRouteVis m_bllRouteVis;
        private bllRoute m_bllRoute;
        private bllTruck m_bllTruck;
        private bllPlanEdit m_bllPlanEdit;
        private bllSpeedProf m_bllSpeedProf;

        Dictionary<string, boSpeedProfValues> m_sp;
        private Dictionary<int, string> m_rdt = null;

        public RouteVisDataProcess()
            : base(new BaseSilngleProgressDialog(0, RouteVisCommonVars.Instance.lstRouteDepots.Count - 1, PMapMessages.M_ROUTVIS_LOADDATA, false), PMapIniParams.Instance.InitRouteDataProcess)
        {
            m_conn = new PMap.DB.Base.SQLServerConnect(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_conn.ConnectDB();
            m_bllRouteVis = new bllRouteVis(m_conn.DB);
            m_bllRoute = new bllRoute(m_conn.DB);
            m_bllTruck = new bllTruck(m_conn.DB);
            m_bllPlanEdit = new bllPlanEdit(m_conn.DB);
            m_bllSpeedProf = new bllSpeedProf(m_conn.DB);


        }


        protected override void DoWork()
        {
            m_sp = m_bllSpeedProf.GetSpeedValuesToDict();

            m_rdt = m_bllRoute.GetRoadTypesToDict();
            RectLatLng boundary = new RectLatLng();
            List<int> nodes = RouteVisCommonVars.Instance.lstRouteDepots.Select(i => i.Depot.NOD_ID).ToList();
            boundary = m_bllRoute.getBoundary(nodes);


            Dictionary<string, List<int>[]> NeighborsFull = null;
            Dictionary<string, List<int>[]> NeighborsCut = null;


            List<string> aRZN_ID_LIST = new List<string>();
            aRZN_ID_LIST.Add(RouteVisCommonVars.Instance.Truck.RZN_ID_LIST);
            RouteData.Instance.getNeigboursByBound(aRZN_ID_LIST, out  NeighborsFull, out NeighborsCut, boundary);

            PMapRoutingProvider provider = new PMapRoutingProvider();

            string lastETLCODE_S = "";
            string lastETLCODE_F = "";

            boTruck Truck = RouteVisCommonVars.Instance.Truck;
            double dTollMultiplier = bllPlanEdit.GetTollMultiplier(RouteVisCommonVars.Instance.CalcTRK_ETOLLCAT, Truck.TRK_ENGINEEURO);
            RouteVisCommonVars.CRouteVis RouteVisS = new RouteVisCommonVars.CRouteVis(RouteVisCommonVars.TY_SHORTEST, PMapMessages.M_ROUTVIS_SHORTEST);
            RouteVisCommonVars.CRouteVis RouteVisF = new RouteVisCommonVars.CRouteVis(RouteVisCommonVars.TY_FASTEST, PMapMessages.M_ROUTVIS_FASTEST);

            RouteVisCommonVars.Instance.lstDetails.Clear();

            for (int i = 0; i < RouteVisCommonVars.Instance.lstRouteDepots.Count - 1; i++)
            {
                if (!RouteVisCommonVars.Instance.lstRouteDepots[i].ValidRoute)
                {
                    ProcessForm.NextStep();
                    ProcessForm.SetInfoText(RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.DEP_NAME + " --> " + RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.DEP_NAME);

                    //Legrövidebb út
                    boRoute routeS = provider.GetRoute(Truck.RZN_ID_LIST, RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.NOD_ID, RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.NOD_ID,
                                    NeighborsFull[Truck.RZN_ID_LIST], NeighborsCut[Truck.RZN_ID_LIST],
                                    ECalcMode.ShortestPath);
                    if (routeS != null)
                    {
                        genData(RouteVisS, routeS,
                            RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.NOD_ID,
                            RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.NOD_ID,
                            Truck, dTollMultiplier, RouteVisCommonVars.Instance.GetRouteWithTruckSpeeds,
                            RouteVisCommonVars.Instance.lstRouteDepots[i].RouteSectionType, ref lastETLCODE_S);
                    }

                    //Leggyorsabb út
                    boRoute routeF = provider.GetRoute(Truck.RZN_ID_LIST, RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.NOD_ID, RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.NOD_ID,
                                    NeighborsFull[Truck.RZN_ID_LIST], NeighborsCut[Truck.RZN_ID_LIST],
                                    ECalcMode.FastestPath);

                    if (routeF != null)
                    {
                        genData(RouteVisF, routeF,
                            RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.NOD_ID,
                            RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.NOD_ID,
                            Truck, dTollMultiplier, RouteVisCommonVars.Instance.GetRouteWithTruckSpeeds,
                             RouteVisCommonVars.Instance.lstRouteDepots[i].RouteSectionType, ref lastETLCODE_F);
                    }

                    if (EventStop != null && EventStop.WaitOne(0, true))
                    {
                        EventStopped.Set();
                        return;
                    }
                }

                RouteVisCommonVars.Instance.lstRouteDepots[i].ValidRoute = false;
                
                RouteVisCommonVars.Instance.lstDetails.Clear();

//                if (i < RouteVisCommonVars.Instance.lstDetails.Count - 1)
                {
                    RouteVisCommonVars.Instance.lstDetails.Add(RouteVisS);
                    RouteVisCommonVars.Instance.lstDetails.Add(RouteVisF);
                }
                //else
                {
//                    RouteVisCommonVars.Instance.lstDetails[i] = RouteVisS;

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_RouteVis"></param>
        /// <param name="p_route"></param>
        /// <param name="p_NOD_ID_FROM"></param>
        /// <param name="p_NOD_ID_TO"></param>
        /// <param name="p_Truckx"></param>
        /// <param name="p_TollMultiplier"></param>
        /// <param name="p_CalcWithTruckSpeeds"></param>
        /// <param name="p_lastETLCODE"></param>
        private void genData(RouteVisCommonVars.CRouteVis p_RouteVis, boRoute p_route, int p_NOD_ID_FROM, int p_NOD_ID_TO, boTruck p_Truck, double p_TollMultiplier, bool p_CalcWithTruckSpeeds, boXRouteSection.ERouteSectionType p_RouteSectionType, ref string p_lastETLCODE)
        {

            p_RouteVis.SumDistance += p_route.DST_DISTANCE;
            if (p_CalcWithTruckSpeeds)
                p_RouteVis.SumDuration += bllPlanEdit.GetDuration(p_route.Edges, m_sp, p_Truck.SPP_ID, Global.defWeather);
            else
                p_RouteVis.SumDuration += bllPlanEdit.GetDuration(p_route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);


            /*******/
            if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
            {
                p_RouteVis.SumDistanceEmpty += p_route.DST_DISTANCE;
                if (p_CalcWithTruckSpeeds)
                    p_RouteVis.SumDurationEmpty += bllPlanEdit.GetDuration(p_route.Edges, m_sp, p_Truck.SPP_ID, Global.defWeather);
                else
                    p_RouteVis.SumDurationEmpty += bllPlanEdit.GetDuration(p_route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
            }


            /*******/
            if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
            {
                p_RouteVis.SumDistanceLoaded += p_route.DST_DISTANCE;
                if (p_CalcWithTruckSpeeds)
                    p_RouteVis.SumDurationLoaded += bllPlanEdit.GetDuration(p_route.Edges, m_sp, p_Truck.SPP_ID, Global.defWeather);
                else
                    p_RouteVis.SumDurationLoaded += bllPlanEdit.GetDuration(p_route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
            }


            String LastEdgeName = "";
            String LastRoadType = "";
            bool LastOneWay = false;
            bool LastDestTraffic = false;
            string LastWZone = "";
            double LastSpeed = -1;
            string LastETLCODE = "";
            RouteVisCommonVars.CRouteVisDetails detail = null;
            foreach (boEdge edge in p_route.Edges)
            {

                double currSpeed = 0;
                if (p_CalcWithTruckSpeeds)
                    currSpeed = m_sp[edge.RDT_VALUE.ToString() + Global.SEP_COORD + p_Truck.SPP_ID].SPV_VALUE;
                else
                    currSpeed = PMapIniParams.Instance.dicSpeed[edge.RDT_VALUE];

                if (detail == null ||
                    LastEdgeName != edge.EDG_NAME ||
                    LastRoadType != edge.RDT_VALUE.ToString() + "-" + m_rdt[edge.RDT_VALUE] ||
                    LastOneWay != edge.EDG_ONEWAY ||
                    LastDestTraffic != edge.EDG_DESTTRAFFIC ||
                    LastWZone != edge.WZONE ||
                    LastSpeed != currSpeed ||
                    LastETLCODE != edge.EDG_ETLCODE)
                {
                    detail = new RouteVisCommonVars.CRouteVisDetails(p_route, p_RouteSectionType);
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

                if (RouteVisCommonVars.Instance.CalcTRK_ETOLLCAT > 1 && p_lastETLCODE != edge.EDG_ETLCODE)
                {
                    //Az 
                    detail.Toll += edge.Tolls["J" + RouteVisCommonVars.Instance.CalcTRK_ETOLLCAT.ToString()] * p_TollMultiplier;

                    if (edge.EDG_ETLCODE.Length > 0)
                    {
                        p_RouteVis.SumToll += edge.Tolls["J" + p_Truck.TRK_ETOLLCAT.ToString()] * p_TollMultiplier;

                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
                            p_RouteVis.SumTollEmpty += edge.Tolls["J" + p_Truck.TRK_ETOLLCAT.ToString()] * p_TollMultiplier;
                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
                            p_RouteVis.SumTollLoaded += edge.Tolls["J" + p_Truck.TRK_ETOLLCAT.ToString()] * p_TollMultiplier;

                    }
                    p_lastETLCODE = edge.EDG_ETLCODE;
                }


            }

        }
    }

}
