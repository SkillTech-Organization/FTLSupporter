using PMap.Common;
using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{

  
    public class FTLTruck
    {

        public enum eTruckTaskType
        {
            Available,                  // Elérhető
            Planned,                    // Tervezett
            Running                     // Futó
        }

        [DisplayNameAttributeX(Name = "Rendszám", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:RegNo")]
        public string RegNo { get; set; }
        
        [DisplayNameAttributeX(Name = "Összsúly (kg)", Order = 2)]
        [Required(ErrorMessage = "Kötelező mező:TruckWeight")]
        public int TruckWeight { get; set; }

        [DisplayNameAttributeX(Name = "Raksúly (kg)", Order = 3)]
        [Required(ErrorMessage = "Kötelező mező:CapacityWeight")]
        public int CapacityWeight { get; set; }

        [DisplayNameAttributeX(Name = "Járműtípus", Order = 4)]
        [Required(ErrorMessage = "Kötelező mező:TruckType")]
        public string TruckType { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálható szállítási feladattípusok", Order = 5)]
        [Required(ErrorMessage = "Kötelező mező:CargoTypes")]
        public string CargoTypes { get; set; }

        [DisplayNameAttributeX(Name = "Fix költség", Order = 6)]
        public double FixCost { get; set; }

        [DisplayNameAttributeX(Name = "Túra KM költség", Order = 7)]
        public double KMCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 8)]
        public double RelocateCost { get; set; }
        
        /*
        [DisplayNameAttributeX(Name = "Rendelkezésre állás kezdőidőpontja", Order = 9)]
        public DateTime Available { get; set; }
        */

        [DisplayNameAttributeX(Name = "Teljesítés max. KM", Order = 9)]
        public double MaxKM { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítés max. idő", Order = 10)]
        public double MaxDuration { get; set; }

        /******************* Járműfeladat ******************************/

        [DisplayNameAttributeX(Name = "Jármű szállítási feladat típus", Order = 11)]
        [Required(ErrorMessage = "Kötelező mező:TruckTaskType")]
        public eTruckTaskType TruckTaskType { get; set; }

        [DisplayNameAttributeX(Name = "Futó szállítási feladat azonosító", Order = 12)]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 13)]
        public bool IsOneWay { get; set; }

        [DisplayNameAttributeX(Name = "Futó túra?", Order = 14)]
        public bool IsRunningTask { get; set; }

        [DisplayNameAttributeX(Name = "Indulás (tervezett) időpontja", Order = 15)]
        [Required(ErrorMessage = "Kötelező mező:TimeFrom")]
        public DateTime TimeFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás (tervezett) időpontja", Order = 16)]
        public DateTime TimeTo { get; set; }

        [DisplayNameAttributeX(Name = "Befejezés (tervezett) időpontja", Order = 17)]
        public DateTime TimeFinish { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 17)]
        public double LatFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 18)]
        public double LngFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakó lat", Order = 19)]
        public double LatTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 20)]
        public double LngTo { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális időpont", Order = 21)]          
        [Required(ErrorMessage = "Kötelező mező:TimeCurr")]
        public DateTime TimeCurr { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális lat", Order = 22)]
        [Required(ErrorMessage = "Kötelező mező:LatCurr")]
        public double LatCurr { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális lng", Order = 23)]
        [Required(ErrorMessage = "Kötelező mező:LngCurr")]
        public double LngCurr { get; set; }


        internal int RST_ID { get; set; }
        internal string RZN_ID_LIST { get; set; }

        internal int NOD_ID_FROM { get; set; }
        internal int NOD_ID_TO { get; set; }
        internal int NOD_ID_CURR { get; set; }

    }
}
