using PMap.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{

    /*
        Megnevezés	Mezőkód	 Típus 	Kötelező?	Megjegyzés	
        Rendszám		Sztring	I	Egyedi azonosító	
        Összsúly 		Szám	I	Behajtási övezetek meghatározásához	
        Raksúly		Szám	I	Teljesíthető szállítási feladatok meghatározásához	
        Típus		Sztring	I	-->szállítási feladat.Teljesítő járműtípusok	
        Kiszolgálható szállítási feladattípusok		Sztring	I	Az elemek vesszővel elválasztva (hűtős, mega, akasztós, stb...) 	
        Túra KM költség		Szám	I		
        Átállás KM költség		Szám	I		
        Fix. KM költség		Szám	N		
        Teljesítés max. KM		Szám	N	0 esetén nincs KM korlát (AETR minimál megvalósítás)	
        Teljesítés max. idő		Szám	N	percben megadva, 0 esetén nincs ilyenkorlát (AETR minimál megvalósítás)	
     */
    public class FTLTruck
    {
        [DisplayNameAttributeX(Name = "Rendszám", Order = 1)]
        public string RegNo { get; set; }
        
        [DisplayNameAttributeX(Name = "Összsúly (kg)", Order = 2)]
        public int TruckWeight { get; set; }

        [DisplayNameAttributeX(Name = "Raksúly (kg)", Order = 3)]
        public int CargoWeight { get; set; }

        [DisplayNameAttributeX(Name = "Járműtípus", Order = 4)]
        public string TruckType { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálható szállítási feladattípusok", Order = 5)]
        public string CargoTypes { get; set; }

        [DisplayNameAttributeX(Name = "Fix költség", Order = 6)]
        public double FixCost { get; set; }

        [DisplayNameAttributeX(Name = "Túra KM költség", Order = 7)]
        public double KMCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 8)]
        public double RelocateCost { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítés max. KM", Order = 9)]
        public double MaxKm { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítés max. idő", Order = 10)]
        public double MaxDuration { get; set; }

    }
}
