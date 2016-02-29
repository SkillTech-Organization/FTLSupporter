using PMap;
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
        

        [DisplayNameAttributeX(Name = "Raksúly (kg)", Order = 3)]
        [Required(ErrorMessage = "Kötelező mező:CapacityWeight")]
        public int CapacityWeight { get; set; }

        [DisplayNameAttributeX(Name = "Összsúly (kg)", Order = 2)]
        [Required(ErrorMessage = "Kötelező mező:TruckWeight")]
        public int TruckWeight { get; set; }

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
        

        [DisplayNameAttributeX(Name = "Teljesítés max. KM", Order = 9)]
        public double MaxKM { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítés max. idő", Order = 10)]
        public double MaxDuration { get; set; }


        [DisplayNameAttributeX(Name = "motor EURO besorolás", Order = 10)]
        [Required(ErrorMessage = "Kötelező mező:EngineEuro")]
        public int EngineEuro { get; set; }                 // 1,2,3,4,.... ==> EURO I, EURO II, EURO III, EURO IV...

        /******************* Járműfeladat ******************************/

        private eTruckTaskType m_TruckTaskType;
        [DisplayNameAttributeX(Name = "Jármű szállítási feladat típus", Order = 11)]
        [Required(ErrorMessage = "Kötelező mező:TruckTaskType")]
        public eTruckTaskType TruckTaskType
        {
            get { return m_TruckTaskType; }
            set
            {
                m_TruckTaskType = value;
                if (value == eTruckTaskType.Available)
                {
                    LatTo = LatCurr;
                    LngTo = LngCurr;
                }
            }
        }


        [DisplayNameAttributeX(Name = "Futó szállítási feladat azonosító", Order = 12)]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 13)]
        public bool IsOneWay { get; set; }

        [DisplayNameAttributeX(Name = "Indulás (tervezett) időpontja", Order = 15)]
        [Required(ErrorMessage = "Kötelező mező:TimeFrom")]
        public DateTime TimeFrom { get; set; }

        [DisplayNameAttributeX(Name = "Megérkezés (tervezett) időpontja", Order = 16)]
        [Required(ErrorMessage = "Kötelező mező:TimeTo")]
        [ErrorIfPropAttrX(EvalMode.IsSmallerThanAnother, "TimeFrom", "A megérkezés korábbi, mint a befejezés!")]
        public DateTime TimeTo { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás befejezés (tervezett) időpontja", Order = 17)]
        [Required(ErrorMessage = "Kötelező mező:TimeFinish")]
        [ErrorIfPropAttrX(EvalMode.IsSmallerThanAnother, "TimeUnload", "A lerakodás vége korábbi, mint a megérkezés!")]      //lerakás időtartama: TimeFinish-TimeTo
        public DateTime TimeUnload { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 18)]
        public double LatFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 19)]
        public double LngFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakó lat", Order = 20)]
        public double LatTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 21)]
        public double LngTo { get; set; }


        private DateTime m_TimeCurr;
        [DisplayNameAttributeX(Name = "Aktuális időpont", Order = 22)]
        [Required(ErrorMessage = "Kötelező mező:TimeCurr")]
        public DateTime TimeCurr
        {
            get { return m_TimeCurr; }
            set
            {
                m_TimeCurr = value;
                if (TruckTaskType == eTruckTaskType.Available)
                {
                    TimeFrom = value;
                    TimeTo = value;
                }
            }
        }

        private double m_LatCurr;
        [DisplayNameAttributeX(Name = "Aktuális lat", Order = 23)]
        [Required(ErrorMessage = "Kötelező mező:LatCurr")]
        public double LatCurr
        {
            get { return m_LatCurr; }
            set
            {
                m_LatCurr = value;
                if (TruckTaskType == eTruckTaskType.Available)
                {
                    LatFrom = value;
                    LatTo = value;
                }
                    
            }
        }

        private double m_LngCurr;
        [DisplayNameAttributeX(Name = "Aktuális lng", Order = 24)]
        [Required(ErrorMessage = "Kötelező mező:LngCurr")]
        public double LngCurr
        {
            get { return m_LngCurr; }
            set
            {
                m_LngCurr = value;
                if (TruckTaskType == eTruckTaskType.Available)
                {
                    LngFrom = value;
                    LngTo = value;
                }
            }
        }


        internal int RST_ID                             //Behajtási övezet ID
        {
            get
            {
                if (CapacityWeight <= 3500)
                    return Global.RST_MAX35T;
                else if (CapacityWeight <= 7500)
                    return Global.RST_MAX75T;
                else if (CapacityWeight <= 12000)
                    return Global.RST_MAX12T;
                else if (CapacityWeight > 12000)
                    return Global.RST_BIGGER12T;
                return Global.RST_NORESTRICT;
            }
        }

        // MEGJ: Behajtási övezet ID != járműkategória 

        internal int ETollCat
        {                                               //A díjszámításnál használandó járműkategória. 
            get                                         //Az FTLSupport-ban a Behajtási övezet ID és járműkategória súlyfüggő, de nem célszerű összevonni a két mezőt,
            {                                           //mert később ez változhat (spec. behajtási engedélyek, stb...)
                if (CapacityWeight <= 3500)
                    return Global.ETOLLCAT_MAX35T;
                else if (CapacityWeight <= 7500)
                    return Global.ETOLLCAT_MAX75T;
                else if (CapacityWeight <= 12000)
                    return Global.ETOLLCAT_MAX12T;
                else if (CapacityWeight > 12000)
                    return Global.ETOLLCAT_BIGGER12T;
                return Global.ETOLLCAT_MAX35T;
            }
        }

        internal int CurrUnloadDuration
        {
            get
            {
                return TimeUnload.Subtract(TimeTo).Minutes;
            }
        }

        internal string RZN_ID_LIST { get; set; }

        internal int NOD_ID_FROM { get; set; }
        internal int NOD_ID_TO { get; set; }
        internal int NOD_ID_CURR { get; set; }

    }
}
