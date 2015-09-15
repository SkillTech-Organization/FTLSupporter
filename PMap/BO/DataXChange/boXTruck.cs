using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PMap.Localize;
using System.Drawing;
using System.ComponentModel;
using PMap.Common;
using PMap.BO.Base;

namespace PMap.BO.DataXChange
{
    /*
    Megnevezés	Mezőkód	    Típus	        Megjegyzés
    Járműkód	TRK_CODE	Megnevezések	
    Rendszám	TRK_REG_NUM	Rendszám	
    Pótkocsi	TRK_TRAILER	Megnevezések	
    Hosszúság	TRK_LENGTH	Számérték	
    Szélesség	TRK_WIDTH	Számérték	
    Magasság	TRK_HEIGHT	Számérték	
    Szín	    TRK_COLOR	Számérték	    RGB szín
    GPS követés	TRK_GPS	    Logikai érték	Van-e a járműre GPS követés?
    Hátlap	    TRK_BACKPANEL	Logikai érték	
    Logo	    TRK_LOGO	Logikai érték	
    Tengelyszám	TRK_AXLENUM	Számérték	
    Útdíjkat.   TRK_ETOLLCAT	Számérték	2-4
    Motor EURO  TRK_ENGINEEURO	Számérték	EURO besorolás szám
    Alap.pihenőidő	TRK_IDLETIME	Perc	
    Aktív ?	    TRK_ACTIVE	Logikai érték	
    Megjegyzés	TRK_COMMENT	Megjegyzések	
    Fuvarozó	CRR_CODE	Megnevezések	Fuvarozó neve/szöveges kódja
    Raktár	    WHS_CODE	Megnevezések	
			
    
    Sebességprofil
    Autópálya	SPV_VALUE1	Számérték	
    Autóút	    SPV_VALUE2	Számérték	
    Főútvonal	SPV_VALUE3	Számérték	
    Egyéb (kétszámjegyű) út	SPV_VALUE4	Számérték	
    Alsóbbr.út	SPV_VALUE5	Számérték	
    Város	    SPV_VALUE6	Számérték	
    Speci.      exSPV_VALUE7	Számérték	
    
    Kapacitásprofil
    Súlyokrlát	CPP_LOADQTY	Számérték	
    Térfogat	CPP_LOADVOL	Számérték	

    Járműtípus – tarifaprofil
    Lerakóh.ktg TFP_DEPCOST	Számérték	
    Fix ktg	    TFP_FIXCOST	Számérték	
    Óradíj	    TFP_HOURCOST	Számérték	
    Km ktg	    TFP_KMCOST	Számérték	

    Szállítható árutípusok:	    Nem kell átadni, minden jármű mindent szállíthat
    Bevételi tarifaprofil       Nem kell átadni, nem számolunk külön bevételi tarifaprofillal. Értéke a tarifaprofillal megegyezik
    Kiadási tarifaprofil        Nem kell átadni, nem számolunk külön kiadási tarifaprofillal. Értéke a tarifaprofillal megegyezik
    Járműrégiók                 Nem kell átadni, az egyes járművek bármely lerakót kiszolgálhatnak
    */

    public class boXTruck : boXBase
    {
        [Required(ErrorMessage = DXMessages.RQ_TRK_CODE)]
        [DisplayNameAttributeX(Name = "Járműkód", Order = 1)]
        public string TRK_CODE { get; set; }

        [Required(ErrorMessage = DXMessages.RQ_TRK_REG_NUM)]
        [DisplayNameAttributeX(Name = "Rendszám", Order = 2)]
        public string TRK_REG_NUM { get; set; }

        [DisplayNameAttributeX(Name = "Pótkocsi", Order = 3)]
        public string TRK_TRAILER { get; set; }

        [DisplayNameAttributeX(Name = "Hosszúság", Order = 4)]
        public double TRK_LENGTH { get; set; }

        [DisplayNameAttributeX(Name = "Szélesség", Order = 5)]
        public double TRK_WIDTH { get; set; }

        [DisplayNameAttributeX(Name = "Magasság", Order = 6)]
        public double TRK_HEIGHT { get; set; }

        [DisplayNameAttributeX(Name = "Összsúly (kg)", Order = 7)]
        [Required(ErrorMessage = DXMessages.RQ_TRK_WEIGHT)]
        public double TRK_WEIGHT { get; set; }

        [DisplayNameAttributeX(Name = "Szín", Order = 8)]
        public Color TRK_COLOR { get; set; }

        [DisplayNameAttributeX(Name = "GPS követés", Order = 9)]
        public bool TRK_GPS { get; set; }

        [DisplayNameAttributeX(Name = "Hátlap", Order = 10)]
        public bool TRK_BACKPANEL { get; set; }

        [DisplayNameAttributeX(Name = "Logo", Order = 11)]
        public bool TRK_LOGO { get; set; }

        [DisplayNameAttributeX(Name = "Tengelyszám", Order = 12)]
        [Range(0, 8)]
        public int TRK_AXLENUM { get; set; }

        [DisplayNameAttributeX(Name = "Útdíjkategória", Order = 13)]
        [Range(2, 4, ErrorMessage = DXMessages.RQ_TRK_ETOLLCAT)]
        public int TRK_ETOLLCAT { get; set; }

        [DisplayNameAttributeX(Name = "Motor EURO besorolás", Order = 14)]
        [Range(1, 6, ErrorMessage = DXMessages.RQ_TRK_ENGINEEURO)]
        public int TRK_ENGINEEURO { get; set; }

        [DisplayNameAttributeX(Name = "Alapértelmezett pihenőidő", Order = 15)]
        [Range(0, 14400)]
        public int TRK_IDLETIME { get; set; }

        [DisplayNameAttributeX(Name = "Aktív", Order = 16)]
        public bool TRK_ACTIVE { get; set; }

        [DisplayNameAttributeX(Name = "Megjegyzés", Order = 17)]
        public string TRK_COMMENT { get; set; }

        [DisplayNameAttributeX(Name = "Fuvarozó", Order = 18)]
        [Required(ErrorMessage = DXMessages.RQ_CRR_CODE)]
        public string CRR_CODE { get; set; }

        [DisplayNameAttributeX(Name = "Raktár", Order = 19)]
        [Required(ErrorMessage = DXMessages.RQ_WHS_CODE)]
        public string WHS_CODE { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Autópálya", Order = 20)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE1 { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Autóút", Order = 21)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE2 { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Főútvonal", Order = 22)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE3 { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Egyéb (kétszámjegyű) út", Order = 23)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE4 { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Alsóbbrendű út", Order = 24)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE5 { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Város", Order = 25)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE6 { get; set; }

        [DisplayNameAttributeX(Name = "Sebességprofil: Speciális utak", Order = 26)]
        [Required(ErrorMessage = DXMessages.RQ_SPV_VALUE)]
        [Range(0, 130, ErrorMessage = DXMessages.RG_SPV_VALUE)]
        public int SPV_VALUE7 { get; set; }

        [DisplayNameAttributeX(Name = "Súlyokrlát (kg)", Order = 27)]
        [Required(ErrorMessage = DXMessages.RQ_CPP_LOADQTY)]
        [Range(0, 1000000)]
        public double CPP_LOADQTY { get; set; }

        [DisplayNameAttributeX(Name = "Térfogat", Order = 28)]
        [Required(ErrorMessage = DXMessages.RQ_CPP_LOADVOL)]
        [Range(0, 1000000)]
        public double CPP_LOADVOL { get; set; }

        [DisplayNameAttributeX(Name = "Fix túraköltség", Order = 29)]
        [Required(ErrorMessage = DXMessages.RQ_TFP_FIXCOST)]
        [Range(0, 10000000)]
        public double TFP_FIXCOST { get; set; }

        [DisplayNameAttributeX(Name = "Km. költség", Order = 30)]
        [Required(ErrorMessage = DXMessages.RQ_TFP_KMCOST)]
        [Range(0, 10000000)]
        public double TFP_KMCOST { get; set; }

        [DisplayNameAttributeX(Name = "Óradíj", Order = 31)]
        [Required(ErrorMessage = DXMessages.RQ_TFP_HOURCOST)]
        [Range(0, 10000000)]
        public double TFP_HOURCOST { get; set; }
    }
}
