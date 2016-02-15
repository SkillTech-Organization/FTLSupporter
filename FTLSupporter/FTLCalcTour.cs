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
        public FTLCalcTour()
        {
            Rank = 0;
            T1Km = 0;
            T1Toll = 0;
            T1Cost = 0;
            T1Route = "";

            RelKm = 0;
            RelToll = 0;
            RelCost = 0;
            RelRoute = "";

            T2Km = 0;
            T2Toll = 0;
            T2Cost = 0;
            T2Route = "";

        }

        [DisplayNameAttributeX(Name = "Helyezés", Order = 1)]
        public int Rank { get; set; }

        [DisplayNameAttributeX(Name = "Rendszám", Order = 2)]
        public string RegNo { get; set; }

        [DisplayNameAttributeX(Name = "Szállítási feladat azonosítója", Order = 3)]
        public string CurrTaskID { get; set; }

        [DisplayNameAttributeX(Name = "Beosztott szállítási feladat azonosítója", Order = 4)]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Futó szállítási feladat befejezés (tervezett)időpontja", Order = 5)]
        public DateTime TimeCurrFinish { get; set; }


        [DisplayNameAttributeX(Name = "Felrakás kezdete", Order = 6)]
        public DateTime StartFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás vége", Order = 7)]
        public DateTime EndFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás (megérkezés)kezdete", Order = 8)]
        public DateTime StartTo { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás vége/befejezés", Order = 9)]
        public DateTime EndTo { get; set; }

        [DisplayNameAttributeX(Name = "I.túra KM", Order = 10)]
        public double T1Km { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útdíj", Order = 11)]
        public double T1Toll { get; set; }

        [DisplayNameAttributeX(Name = "I.túra költség", Order = 12)]
        public double T1Cost { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útvonal", Order = 13)]
        public string T1Route { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 14)]
        public double RelKm { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útdíj", Order = 15)]
        public double RelToll { get; set; }

        [DisplayNameAttributeX(Name = "Átállás költség", Order = 16)]
        public double RelCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útvonal", Order = 17)]
        public string RelRoute { get; set; }

        [DisplayNameAttributeX(Name = "II.túra KM", Order = 18)]
        public double T2Km { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útdíj", Order = 19)]
        public double T2Toll { get; set; }

        [DisplayNameAttributeX(Name = "II.túra költség", Order = 20)]
        public double T2Cost { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útvonal", Order = 21)]
        public string T2Route { get; set; }

    }
}
