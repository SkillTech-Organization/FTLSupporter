using PMap;
using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

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
 

        [DisplayNameAttributeX(Name = "Feladat azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TruckID")]
        public string TruckID { get; set; }

        [DisplayNameAttributeX(Name = "Összsúly", Order = 2)]           /* gross vehicle weight rating */
        [Required(ErrorMessage = "Kötelező mező:GVWR")]
        public int GVWR { get; set; }


        [DisplayNameAttributeX(Name = "Kapacitás (súly)", Order = 3)]
        [Required(ErrorMessage = "Kötelező mező:Capacity")]
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



        /******************* Járműfeladat ******************************/

        [DisplayNameAttributeX(Name = "Jármű szállítási feladat típus", Order = 11)]
        [Required(ErrorMessage = "Kötelező mező:TruckTaskType")]

        [DisplayNameAttributeX(Name = "Futó szállítási feladat azonosító", Order = 12)]
        public string RunningTaskID { get; set; }


        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 13)]
        public bool CurrIsOneWay { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális időpont", Order = 14)]
        [Required(ErrorMessage = "Kötelező mező:TimeCurr")]
        public DateTime CurrTime { get; set; }


        [DisplayNameAttributeX(Name = "Aktuális lat", Order = 23)]
        [Required(ErrorMessage = "Kötelező mező:LatCurr")]
        public double CurrLat { get; set; }

        [DisplayNameAttributeX(Name = "Aktuális lng", Order = 24)]
        [Required(ErrorMessage = "Kötelező mező:LngCurr")]
        public double CurrLng { get; set; }

        [DisplayNameAttributeX(Name = "Túrapontok", Order = 6)]
        [Required(ErrorMessage = "Kötelező mező:TPoints")]
        public List<FTLPoint> CurrTPoints { get; set; }


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

    }
}
