using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using PMap.BO;
using PMap.Markers;
using PMap.BO.DataXChange;

namespace PMap.Common
{

    public sealed class RouteVisCommonVars
    {
        public const int TY_SHORTEST = 0;
        public const int TY_FASTEST = 1;


        public class CRouteVisDetails
        {
            public int NOD_ID_FROM { get; set; }
            public int NOD_ID_TO { get; set; }
            public string Text { get; set; }
            public double Dist { get; set; }
            public double Duration { get; set; }
            public double Speed { get; set; }
            public string RoadType { get; set; }
            public string WZone { get; set; }
            public bool OneWay { get; set; }
            public bool DestTraffic { get; set; }
            public string EDG_ETLCODE { get; set; }
            public double Toll { get; set; }
            public boRoute Route { get; private set; }
            public boXRouteSection.ERouteSectionType RouteSectionType { get; private set; }
            public int RouteSectionTypeInt { get { return (int)RouteSectionType; } }
            public CRouteVisDetails(boRoute p_Route, boXRouteSection.ERouteSectionType p_RouteSectionType)
            {
                Route = p_Route;
                RouteSectionType = p_RouteSectionType;
            }

        }

        public class CRouteDepots 
        {
            public boDepot Depot { get; set; }
            public int RouteSectionTypeInt { get { return (int)RouteSectionType; } }
            public boXRouteSection.ERouteSectionType RouteSectionType { get; set; }
            public int xxx { get { return 2; } }
            public bool ValidRoute { get; set; }

            public CRouteDepots()
            {
                ValidRoute = false;
            }
        }

        
        public class CRouteVis
        {

            public CRouteVis(int p_type, string p_Name)
            {
                Type = p_type;
                Name = p_Name;
                Visible = true;
                SumToll = 0;
                SumDistance = 0;
                SumDuration = 0;
                //Route = new List<boRoute>();
                Details = new List<CRouteVisDetails>();
                SumDistance = 0;
                SumDuration = 0;

                SumTollEmpty = 0;
                SumDistanceEmpty = 0;
                SumDurationEmpty = 0;

                SumTollLoaded = 0;
                SumDistanceLoaded= 0;
                SumDurationLoaded = 0;

           }



            public int Type { get; set; }
            public string Name { get; set; }
            public bool Visible { get; set; }
            public double SumToll { get; set; }
            public double SumDistance { get; set; }
            public double SumDuration { get; set; }
            //public List<boRoute> Route { get; set; }
            public List<CRouteVisDetails> Details { get; set; }

            public double SumTollEmpty { get; set; }
            public double SumDistanceEmpty { get; set; }
            public double SumDurationEmpty { get; set; }

            public double SumTollLoaded { get; set; }
            public double SumDistanceLoaded { get; set; }
            public double SumDurationLoaded { get; set; }
        }

        // private static readonly object padlock = new object();
        private static volatile object padlock = new object();



        //Singleton technika...
        static private RouteVisCommonVars instance = null;        //Mivel statikus tag a program indulásakor 
        static public RouteVisCommonVars Instance                                  //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RouteVisCommonVars();
                        instance.GetRouteWithTruckSpeeds = true;
                        instance.lstRouteDepots = new List<CRouteDepots>();
                        instance.lstDetails= new List<CRouteVis>();
                        instance.SelectedType = TY_SHORTEST;
                        instance.TooltipMode = MarkerTooltipMode.OnMouseOver;

                    }
                }
                return instance;

            }

        }

        public bool GetRouteWithTruckSpeeds { get; set; }     //true esetén a leggyorsabb út számolásához használt, ini fileban tárolt ideális sebességprofilt vesszük figyelembe a megjelenítésnél (SWH specifikum)
        public MarkerTooltipMode TooltipMode { get; set; }
        public int Zoom { get; set; }
        public PointLatLng CurrentPosition { get; set; }
        public List<CRouteDepots> lstRouteDepots { get; set; }
        public boTruck Truck { get; set; }
        public int CalcTRK_ETOLLCAT { get; set; }
        public List<CRouteVis> lstDetails { get; set; }
        public int SelectedType { get; set; }
        public int SelectedDepID { get; set; }


    }


}
