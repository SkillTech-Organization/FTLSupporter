using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHInterface
{
    internal class XRoute
    {


        public const int TY_SHORTEST = 0;
        public const int TY_FASTEST = 1;
        public XRoute(int p_type, string p_Name)
        {
            Type = p_type;
            Name = p_Name;
            Visible = true;
            SumToll = 0;
            SumDistance = 0;
            SumDuration = 0;
            //Route = new List<boRoute>();
            Details = new List<XRouteDetails>();
            SumDistance = 0;
            SumDuration = 0;

            SumTollEmpty = 0;
            SumDistanceEmpty = 0;
            SumDurationEmpty = 0;

            SumTollLoaded = 0;
            SumDistanceLoaded = 0;
            SumDurationLoaded = 0;

        }



        public int Type { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public double SumToll { get; set; }
        public double SumDistance { get; set; }
        public double SumDuration { get; set; }
        //public List<boRoute> Route { get; set; }
        public List<XRouteDetails> Details { get; set; }

        public double SumTollEmpty { get; set; }
        public double SumDistanceEmpty { get; set; }
        public double SumDurationEmpty { get; set; }

        public double SumTollLoaded { get; set; }
        public double SumDistanceLoaded { get; set; }
        public double SumDurationLoaded { get; set; }
    }
}
