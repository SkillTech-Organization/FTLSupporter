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

            RelKm = 0;
            RelToll = 0;
            RelCost = 0;

            T2Km = 0;
            T2Toll = 0;
            T2Cost = 0;

            RetKm = 0;
            RetToll = 0;
            RetCost = 0;
        }

        [DisplayNameAttributeX(Name = "Helyezés", Order = 1)]
        public int Rank { get; set; }

        [DisplayNameAttributeX(Name = "Rendszám", Order = 2)]
        public string RegNo { get; set; }

        [DisplayNameAttributeX(Name = "Szállítási feladat azonosítója", Order = 3)]
        public string CurrTaskID { get; set; }

        [DisplayNameAttributeX(Name = "Beosztott szállítási feladat azonosítója", Order = 4)]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Futó szállítási feladat befejezés (tervezett)időpontja lerakodással", Order = 5)]
        public DateTime TimeCurrTourFinish { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás kezdete", Order = 6)]
        public DateTime TimeStartFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás vége", Order = 7)]
        public DateTime TimeEndFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás (megérkezés)kezdete", Order = 8)]
        public DateTime TimeStartTo { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás vége/befejezés", Order = 9)]
        public DateTime TimeEndTo { get; set; }

        [DisplayNameAttributeX(Name = "I.túra KM", Order = 10)]
        public double T1Km { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útdíj", Order = 11)]
        public double T1Toll { get; set; }

        [DisplayNameAttributeX(Name = "I.túra költség", Order = 12)]
        public double T1Cost { get; set; }
 
        [DisplayNameAttributeX(Name = "I.túra időtartam", Order = 13)]
        public double T1Duration { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 14)]
        public double RelKm { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útdíj", Order = 15)]
        public double RelToll { get; set; }

        [DisplayNameAttributeX(Name = "Átállás költség", Order = 16)]
        public double RelCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás időtartam", Order = 17)]
        public double RelDuration { get; set; }

        [DisplayNameAttributeX(Name = "II.túra KM", Order = 18)]
        public double T2Km { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útdíj", Order = 19)]
        public double T2Toll { get; set; }

        [DisplayNameAttributeX(Name = "II.túra költség", Order = 20)]
        public double T2Cost { get; set; }

        [DisplayNameAttributeX(Name = "II.túra időtartam", Order = 21)]
        public double T2Duration { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés KM", Order = 22)]
        public double RetKm { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés  útdíj", Order = 23)]
        public double RetToll { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés  költség", Order = 24)]
        public double RetCost { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés időtartam", Order = 25)]
        public double RetDuration { get; set; }

        [DisplayNameAttributeX(Name = "Befejezés időpontja", Order = 26)]
        public DateTime TimeComplete { get; set; }

        [DisplayNameAttributeX(Name = "II.túra teljesítésének költsége", Order = 27)]
        public double AdditionalCost { get { return RelToll + RelCost + T2Toll + T2Cost + RetToll + RetCost; } }

        [DisplayNameAttributeX(Name = "Összes költség", Order = 28)]
        public double FullCost { get { return T1Toll + T1Cost + AdditionalCost; } }

        [DisplayNameAttributeX(Name = "Teljes KM", Order = 29)]
        public double FullKM { get { return T1Km + RelKm + T2Km + RetKm; } }

        [DisplayNameAttributeX(Name = "Teljes időtartam", Order = 30)]
        public double FullDuration { get { return T1Duration + RelDuration + T2Duration + RetDuration; } }

    }
}
