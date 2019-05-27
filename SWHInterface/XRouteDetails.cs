using PMapCore.BO;
using PMapCore.BO.DataXChange;
using SWHInterface.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHInterface
{

    public class XRouteDetails
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
        public int EDG_MAXWEIGHT { get; set; }
        public int EDG_MAXHEIGHT { get; set; }
        public int EDG_MAXWIDTH { get; set; }
        public double Toll { get; set; }
        public boRoute Route { get; private set; }
        public boXRouteSection.ERouteSectionType RouteSectionType { get; private set; }
        public int RouteSectionTypeInt { get { return (int)RouteSectionType; } }
        public XRouteDetails(boRoute p_Route, boXRouteSection.ERouteSectionType p_RouteSectionType)
        {
            Route = p_Route;
            RouteSectionType = p_RouteSectionType;
        }

    }
}
