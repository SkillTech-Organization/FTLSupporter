using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PMapCore.BO;
using PMapCore.BO.Base;
using PMapCore.Common.Attrib;

namespace PMapCore.BLL.DataXChange
{
    public class boXRouteSummary : boXBase
    {

        public class boXRouteSummaryDetails
        {
            [DisplayNameAttributeX(Name = "Kiválasztva", Order = 1)]
            public bool Selected { get; set; }
            [DisplayNameAttributeX(Name = "Távolság", Order = 2)]
            public double SumDistance { get; set; }
            [DisplayNameAttributeX(Name = "Menetidő", Order = 3)]
            public double SumDuration { get; set; }
            [DisplayNameAttributeX(Name = "Útdíj", Order = 4)]
            public double SumToll { get; set; }


            [DisplayNameAttributeX(Name = "Üres távolság", Order = 5)]
            public double SumDistanceEmpty { get; set; }
            [DisplayNameAttributeX(Name = "Üres menetidő", Order = 6)]
            public double SumDurationEmpty { get; set; }
            [DisplayNameAttributeX(Name = "Üres  útdíj", Order = 7)]
            public double SumTollEmpty { get; set; }

            [DisplayNameAttributeX(Name = "Rakott távolság", Order = 8)]
            public double SumDistanceLoaded { get; set; }
            [DisplayNameAttributeX(Name = "Rakott menetidő", Order = 9)]
            public double SumDurationLoaded { get; set; }
            [DisplayNameAttributeX(Name = "Rakott útdíj", Order = 10)]
            public double SumTollLoaded { get; set; }
        
        }



        [Browsable(false)]
        public boXRouteSummaryDetails ShortestRoute { get; set; }
        [Browsable(false)]
        public boXRouteSummaryDetails FastestRoute { get; set; }

        public boXRouteSummary()
        {
            ShortestRoute = new boXRouteSummaryDetails();
            ShortestRoute.Selected = false;
            FastestRoute = new boXRouteSummaryDetails();
            FastestRoute.Selected = false;
        }
    }
}
