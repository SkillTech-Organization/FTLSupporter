using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    //Eredmény útvonalak
    public class FTLCalcRoute
    {
        [DisplayNameAttributeX(Name = "Túrapont", Order = 1)]
        public FTLPoint TPoint { get; set; }

        [DisplayNameAttributeX(Name = "Érkezés", Order = 2)]
        public DateTime Arrival { get; set; }

        [DisplayNameAttributeX(Name = "Indulás", Order = 3)]
        public DateTime Departure { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítve", Order = 4)]
        public bool Completed { get; set; }

        [DisplayNameAttributeX(Name = "Útvonal", Order = 4)]
        public string RouteX { get; set; }


        public int Duration { get; set; }
        public double Distance { get; set; }
        public double Toll { get; set; }

        public bool Current { get; set; }


        internal FTLPMapRoute PMapRoute { get; set; }
    
    }
}
