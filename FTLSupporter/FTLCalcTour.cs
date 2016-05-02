using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{

    /// <summary>
    /// Túraajánlat
    /// </summary>
    public class FTLCalcTour
    {
        public FTLCalcTour()
        {
            Rank = 0;
            T1Km = 0;
            T1Toll = 0;
            T1Cost = 0;
            T1CalcRoute = new List<FTLCalcRoute>();

            RelKm = 0;
            RelToll = 0;
            RelCost = 0;
            RelCalcRoute = new List<FTLCalcRoute>();

            T2Km = 0;
            T2Toll = 0;
            T2Cost = 0;
            T2CalcRoute = new List<FTLCalcRoute>();

            RetKm = 0;
            RetToll = 0;
            RetCost = 0;
            RetCalcRoute = new List<FTLCalcRoute>();
        }

        [DisplayNameAttributeX(Name = "Beosztott szállítási feladat", Order = 1)]
        public FTLTask Task { get; set; }

        [DisplayNameAttributeX(Name = "Helyezés", Order = 2)]
        public int Rank { get; set; }


        [DisplayNameAttributeX(Name = "Jármű", Order = 3)]
        public FTLTruck Truck { get; set; }

        [DisplayNameAttributeX(Name = "Futó szállítási feladat befejezés (tervezett)időpontja lerakodással", Order = 4)]
        public DateTime TimeCurrTourFinish { get; set; }

        #region beosztandó szállítási feladat mezői
        [DisplayNameAttributeX(Name = "Felrakás kezdete", Order = 5)]
        public DateTime TimeStartFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás vége", Order = 6)]
        public DateTime TimeEndFrom { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás (megérkezés)kezdete", Order = 7)]
        public DateTime TimeStartTo { get; set; }

        [DisplayNameAttributeX(Name = "Lerakás vége/befejezés", Order = 8)]
        public DateTime TimeEndTo { get; set; }

        [DisplayNameAttributeX(Name = "I.túra KM", Order = 9)]
        public double T1Km { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útdíj", Order = 10)]
        public double T1Toll { get; set; }

        [DisplayNameAttributeX(Name = "I.túra költség", Order = 11)]
        public double T1Cost { get; set; }

        [DisplayNameAttributeX(Name = "I.túra időtartam", Order = 12)]
        public double T1Duration { get; set; }

        [DisplayNameAttributeX(Name = "I.túra részletek", Order = 13)]
        public List<FTLCalcRoute> T1CalcRoute { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 14)]
        public double RelKm { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útdíj", Order = 15)]
        public double RelToll { get; set; }

        [DisplayNameAttributeX(Name = "Átállás költség", Order = 16)]
        public double RelCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás időtartam", Order = 17)]
        public double RelDuration { get; set; }

        [DisplayNameAttributeX(Name = "Átállás részletek", Order = 18)]
        public List<FTLCalcRoute> RelCalcRoute { get; set; }

        [DisplayNameAttributeX(Name = "II.túra KM", Order = 19)]
        public double T2Km { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útdíj", Order = 20)]
        public double T2Toll { get; set; }

        [DisplayNameAttributeX(Name = "II.túra költség", Order = 21)]
        public double T2Cost { get; set; }

        [DisplayNameAttributeX(Name = "II.túra időtartam", Order = 22)]
        public double T2Duration { get; set; }

        [DisplayNameAttributeX(Name = "II.túra részletek", Order = 23)]
        public List<FTLCalcRoute> T2CalcRoute { get; set; }


        [DisplayNameAttributeX(Name = "Visszatérés KM", Order = 24)]
        public double RetKm { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés  útdíj", Order = 25)]
        public double RetToll { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés  költség", Order = 26)]
        public double RetCost { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés időtartam", Order = 27)]
        public double RetDuration { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés részletek", Order = 28)]
        public List<FTLCalcRoute> RetCalcRoute { get; set; }

        #endregion

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
