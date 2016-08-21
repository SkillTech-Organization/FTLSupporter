using PMap;
using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    [Serializable]
    public class FTLTruck
    {
        public enum eTruckTaskType
        {
            Available,                  // Elérhető
            Planned,                    // Tervezett
            Running                     // Futó
        }


        [ItemIDAttr]
        [DisplayNameAttributeX(Name = "Jármű azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TruckID")]                          /* jellemzően a rendszám */
        public string TruckID { get; set; }

        [DisplayNameAttributeX(Name = "Összsúly", Order = 2)]           /* gross vehicle weight rating */
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:GVWR")]              
        public int GVWR { get; set; }

        [DisplayNameAttributeX(Name = "Kapacitás (súly)", Order = 3)]
        public int Capacity { get; set; }

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


        [DisplayNameAttributeX(Name = "motor EURO besorolás", Order = 11)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:EngineEuro")]
        public int EngineEuro { get; set; }                 // 1,2,3,4,.... ==> EURO I, EURO II, EURO III, EURO IV...

        /******************* Járműfeladat ******************************/

        [DisplayNameAttributeX(Name = "Jármű szállítási feladat típus", Order = 12)]
        [Required(ErrorMessage = "Kötelező mező:TruckTaskType")]
        public eTruckTaskType TruckTaskType { get; set; }

        [DisplayNameAttributeX(Name = "Futó szállítási feladat azonosító", Order = 13)]
        public string RunningTaskID { get; set; }


        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 14)]
        public bool CurrIsOneWay { get; set; }

        private DateTime m_CurrTime;
        [DisplayNameAttributeX(Name = "Aktuális időpont", Order = 15)]
        [Required(ErrorMessage = "Kötelező mező:TimeCurr")]
        public DateTime CurrTime
        {
            get {
                if (TruckTaskType == eTruckTaskType.Planned)        /* tervezett túra esetén az első túrapont indulás időpontja */
                {
                    FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                    if (firstPt != null)
                        return firstPt.RealDeparture;
                    else
                        return DateTime.MinValue;
                }
                else
                    return m_CurrTime; 
            
            }
            set
            {
                if (TruckTaskType != eTruckTaskType.Planned)
                {
                    m_CurrTime = value;
                }
            }
        }

        private double m_CurrLat;
        [DisplayNameAttributeX(Name = "Aktuális hosszúsági koordináta", Order = 16)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:CurrLat")]
        public double CurrLat
        {
            get
            {
                if (TruckTaskType == eTruckTaskType.Planned)        /* tervezett túra esetén az első túrapont indulás időpontja */
                {
                    FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                    if (firstPt != null)
                        return firstPt.Lat;
                    else
                        return 0;
                }
                else
                    return m_CurrLat;

            }
            set
            {
                if (TruckTaskType != eTruckTaskType.Planned)
                {
                    m_CurrLat = value;
                }
            }
        }

        private double m_CurrLng;
        [DisplayNameAttributeX(Name = "Aktuális szélességi koordináta", Order = 17)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:CurrLng")]
        public double CurrLng
        {
            get
            {
                if (TruckTaskType == eTruckTaskType.Planned)        /* tervezett túra esetén az első túrapont indulás időpontja */
                {
                    FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                    if (firstPt != null)
                        return firstPt.Lng;
                    else
                        return 0;
                }
                else
                    return m_CurrLng;

            }
            set
            {
                if (TruckTaskType != eTruckTaskType.Planned)
                {
                    m_CurrLng = value;
                }
            }
        }

        [DisplayNameAttributeX(Name = "Teljesítés alatt álló túrapontok", Order = 18)]
        [Required(ErrorMessage = "Kötelező mező:CurrTPoints")]
        public List<FTLPoint> CurrTPoints { get; set; }

        [DisplayNameAttributeX(Name = "Hány túrapont van teljesítve?", Order = 19)]
        public int TPointCompleted { get; set; }
     

        internal int RST_ID                             //Összsúly alapján a behajtási övezet ID
        {
            get
            {
                if (GVWR <= 3500)
                    return Global.RST_MAX35T;
                else if (GVWR <= 7500)
                    return Global.RST_MAX75T;
                else if (GVWR <= 12000)
                    return Global.RST_MAX12T;
                else if (GVWR > 12000)
                    return Global.RST_BIGGER12T;
                return Global.RST_NORESTRICT;
            }
        }

        // MEGJ: Behajtási övezet ID != járműkategória 

        internal int ETollCat
        {                                               //A díjszámításnál használandó járműkategória. 
            get                                         //Az FTLSupport-ban a Behajtási övezet ID és járműkategória súlyfüggő, de nem célszerű összevonni a két mezőt,
            {                                           //mert később ez változhat (spec. behajtási engedélyek, stb...)
                if (GVWR <= 3500)
                    return Global.ETOLLCAT_MAX35T;
                else if (GVWR <= 7500)
                    return Global.ETOLLCAT_MAX75T;
                else if (GVWR <= 12000)
                    return Global.ETOLLCAT_MAX12T;
                else if (GVWR > 12000)
                    return Global.ETOLLCAT_BIGGER12T;
                return Global.ETOLLCAT_MAX35T;
            }
        }

        internal string RZN_ID_LIST { get; set; }
        internal int NOD_ID_CURR { get; set; }

    }
}
