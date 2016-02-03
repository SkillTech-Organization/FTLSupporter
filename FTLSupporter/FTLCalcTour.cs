using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLCalcTour
    {

        [DisplayNameAttributeX(Name = "Helyezés", Order = 2)]
        public int Rank { get; set; }

        [DisplayNameAttributeX(Name = "Rendszám", Order = 1)]
        public string RegNo { get; set; }

        [DisplayNameAttributeX(Name = "Szállítási feladat azonosítója", Order = 3)]
        public string CurrTaskID { get; set; }


        [DisplayNameAttributeX(Name = "Futó szállítási feladat befejezés (tervezett)időpontja", Order = 3)]
        public DateTime TimeCurrFinish { get; set; }

        [DisplayNameAttributeX(Name = "Beosztott szállítási feladat azonosítója", Order = 3)]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás kezdete", Order = 3)]
        public DateTime StartFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás vége", Order = 3)]
        public DateTime EndFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás (megérkezés)kezdete", Order = 3)]
        public DateTime StartTo { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás vége/befejezés", Order = 3)]
        public DateTime EndTo { get; set; }

        [DisplayNameAttributeX(Name = "I.túra KM", Order = 3)]
        public double T1Km { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útdíj", Order = 3)]
        public double T1Toll { get; set; }

        [DisplayNameAttributeX(Name = "I.túra költség", Order = 3)]
        public double T1Cost { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útvonal", Order = 3)]
        public string T1Route { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 3)]
        public double RelKm { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útdíj", Order = 3)]
        public double RelToll { get; set; }

        [DisplayNameAttributeX(Name = "Átállás költség", Order = 3)]
        public double RelCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útvonal", Order = 3)]
        public string RelRoute { get; set; }

        [DisplayNameAttributeX(Name = "II.túra KM", Order = 3)]
        public double T2Km { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útdíj", Order = 3)]
        public double T2Toll { get; set; }

        [DisplayNameAttributeX(Name = "II.túra költség", Order = 3)]
        public double T2Cost { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útvonal", Order = 3)]
        public string T2Route { get; set; }
    }
}
