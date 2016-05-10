﻿using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    //Eredmény útvonalak
    public class FTLCalcRoute
    {

        public FTLCalcRoute()
        {
            TPoint = null;
            Arrival = DateTime.MinValue;
            Departure = DateTime.MinValue;
            Completed = false;
            PMapRoute = null;
            RouteDuration = 0;
            WaitingDuration = 0;
            SrvDuration = 0;
            Distance = 0;
            Toll = 0;
            Current = false;
            RoutePoints = "";
        }

        [DisplayNameAttributeX(Name = "Túrapont", Order = 1)]
        public FTLPoint TPoint { get; set; }

        [DisplayNameAttributeX(Name = "Érkezés", Order = 2)]
        public DateTime Arrival { get; set; }

        [DisplayNameAttributeX(Name = "Indulás", Order = 3)]
        public DateTime Departure { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítve", Order = 4)]
        public bool Completed { get; set; }

        [DisplayNameAttributeX(Name = "Utazás időtartama", Order = 5)]
        public int RouteDuration { get; set; }

        [DisplayNameAttributeX(Name = "Várakozási idő", Order = 6)]
        public int WaitingDuration { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálási idő", Order = 7)]
        public int SrvDuration { get; set; }

        [DisplayNameAttributeX(Name = "Távolság (m)", Order = 8)]
        public double Distance { get; set; }

        [DisplayNameAttributeX(Name = "Útdíj", Order = 9)]
        public double Toll { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális pont?", Order = 10)]
        public bool Current { get; set; }

        [DisplayNameAttributeX(Name = "Útvonal pontok", Order = 11)]
        public string RoutePoints { get; set; }

        /* munkamező */
        internal FTLPMapRoute PMapRoute { get; set; }
    
    }
}
