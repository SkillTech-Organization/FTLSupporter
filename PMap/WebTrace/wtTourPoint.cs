﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.Markers;
using GMap.NET.WindowsForms;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using PMap.Common.Attrib;

namespace PMap.WebTrace
{
    [Serializable]
    [DataContract(Namespace = "TourPoint")]
    public class wtTourPoint
    {
  
        [DataMember]
        [DisplayNameAttributeX(Name = "Túrapont sorszáma")]
        public int Order { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Távolság az előző túraponttól")]
        public double Distance { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Megérkezés")]
        public DateTime ArrTime { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Kiszolgálás kezdete")]
        public DateTime ServTime { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Távozás")]
        public DateTime DepTime { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Lerakó/felrakó kód")]
        public string DepCode { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Lerakó/felrakó kód")]

        public string DepName { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Lerakó/felrakó cím")]
        public string DepAddr { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Megrendelés száma (külső azonosító)")]
        public string OrdNum { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Útvonal-pontok")]
        public List<wtMapPoint> MapPoints { get; set; } = new List<wtMapPoint>();
    }

}
