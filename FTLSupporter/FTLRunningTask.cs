using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public class FTLRunningTask
    {
        [DisplayNameAttributeX(Name = "Rendszám", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:RegNo")]
        public string RegNo { get; set; }

        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 3)]
        public bool IsOneWay { get; set; }

        [DisplayNameAttributeX(Name = "Futó túra?", Order = 3)]
        public bool IsRunningTask { get; set; }

        [DisplayNameAttributeX(Name = "Indulás (tervezett) időpontja", Order = 3)]
        [Required(ErrorMessage = "Kötelező mező:TimeFrom")]
        public DateTime TimeFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás (tervezett) időpontja", Order = 3)]
        [Required(ErrorMessage = "Kötelező mező:TimeTo")]
        public DateTime TimeTo { get; set; }

        [DisplayNameAttributeX(Name = "Befejezés (tervezett) időpontja", Order = 3)]
        [Required(ErrorMessage = "Kötelező mező:Finish")]
        public DateTime TimeFinish { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 8)]
        [Required(ErrorMessage = "Kötelező mező:LatFrom")]
        public double LatFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 9)]
        [Required(ErrorMessage = "Kötelező mező:LngFrom")]
        public double LngFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakó lat", Order = 8)]
        [Required(ErrorMessage = "Kötelező mező:LatTo")]
        public double LatTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 9)]
        [Required(ErrorMessage = "Kötelező mező:LngTo")]
        public double LngTo { get; set; }
        
        [DisplayNameAttributeX(Name = "Aktuális időpont", Order = 8)]
        public DateTime TimeCurr { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális lat", Order = 8)]
        public double LatCurr { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális lng", Order = 9)]
        public double LngCurr { get; set; }

        internal int NOD_ID_FROM { get; set; }
        internal int NOD_ID_TO { get; set; }
        internal int NOD_ID_CURR { get; set; }


    }
}
