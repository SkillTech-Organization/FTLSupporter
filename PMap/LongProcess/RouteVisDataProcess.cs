using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.LongProcess.Base;
using PMapCore.DB.Base;
using PMapCore.Strings;
using PMapCore.Common;
using PMapCore.BLL;
using PMapCore.BO;
using GMap.NET;
using PMapCore.Route;
using PMapCore.MapProvider;
using PMapCore.BO.DataXChange;

namespace Map.LongProcess
{

   public class RouteVisDataProcess : BaseLongProcess
    {
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        private bllRouteVis m_bllRouteVis;
        private bllRoute m_bllRoute;
        private bllTruck m_bllTruck;
        private bllPlanEdit m_bllPlanEdit;
        private bllSpeedProf m_bllSpeedProf;

        Dictionary<string, boSpeedProfValues> m_sp;
        private Dictionary<int, string> m_rdt = null;   //Úttípusok

        public RouteVisDataProcess()
            : base(new BaseSilngleProgressDialog(0, RouteVisCommonVars.Instance.lstRouteDepots.Count - 1, PMapMessages.M_ROUTVIS_LOADDATA, false), PMapIniParams.Instance.InitRouteDataProcess)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            m_bllRouteVis = new bllRouteVis(m_DB);
            m_bllRoute = new bllRoute(m_DB);
            m_bllTruck = new bllTruck(m_DB);
            m_bllPlanEdit = new bllPlanEdit(m_DB);
            m_bllSpeedProf = new bllSpeedProf(m_DB);


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

            boTruck Truck = RouteVisCommonVars.Instance.Truck;
            var routePar = new CRoutePars() { RZN_ID_LIST = Truck.RZN_ID_LIST, Weight = Truck.TRK_WEIGHT, Height = Truck.TRK_XHEIGHT, Width = Truck.TRK_XWIDTH };

            RouteData.Instance.getNeigboursByBound(routePar, ref  NeighborsFull, ref NeighborsCut, boundary, null);

            PMapRoutingProvider provider = new PMapRoutingProvider();

            string lastETLCODE_S = "";
            string lastETLCODE_F = "";

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
                    boRoute routeS = provider.GetRoute(RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.NOD_ID, RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.NOD_ID, routePar,
                                    NeighborsFull[routePar.Hash], 
                                    PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
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
                    boRoute routeF = provider.GetRoute(RouteVisCommonVars.Instance.lstRouteDepots[i].Depot.NOD_ID, RouteVisCommonVars.Instance.lstRouteDepots[i + 1].Depot.NOD_ID, routePar,
                                    NeighborsFull[routePar.Hash], 
                                    PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
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

                if (RouteVisCommonVars.Instance.CalcTRK_ETOLLCAT > 1 && p_lastETLCODE != edge.EDG_ETLCODE)
                {
                    //Az 
                    detail.Toll += edge.Tolls[RouteVisCommonVars.Instance.CalcTRK_ETOLLCAT] * p_TollMultiplier;

                    if (edge.EDG_ETLCODE.Length > 0)
                    {
                        p_RouteVis.SumToll += edge.Tolls[p_Truck.TRK_ETOLLCAT] * p_TollMultiplier;

                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Empty)
                            p_RouteVis.SumTollEmpty += edge.Tolls[p_Truck.TRK_ETOLLCAT] * p_TollMultiplier;
                        if (p_RouteSectionType == boXRouteSection.ERouteSectionType.Loaded)
                            p_RouteVis.SumTollLoaded += edge.Tolls[p_Truck.TRK_ETOLLCAT] * p_TollMultiplier;

                    }
                    p_lastETLCODE = edge.EDG_ETLCODE;
                }


            }

        }
    }

}
