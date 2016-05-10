using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FTLSupporter
{

    /// <summary>
    /// Túraajánlat
    /// </summary>
    public class FTLCalcTour
    {
        public enum FTLCalcTourStatus
        {
            [Description("OK")]
            OK,
            [Description("ERR")]
            ERR
        };
        public FTLCalcTour()
        {
            Status = FTLCalcTourStatus.OK;
            Msg = "";

            Rank = 0;
            T1M = 0;
            T1Toll = 0;
            T1Cost = 0;
            T1Duration = 0;
            T1Start = DateTime.MinValue;
            T1End = DateTime.MinValue;
            T1CalcRoute = new List<FTLCalcRoute>();

            RelM = 0;
            RelToll = 0;
            RelCost = 0;
            RelDuration = 0;
            RelStart = DateTime.MinValue;
            RelEnd = DateTime.MinValue;
            RelCalcRoute = new FTLCalcRoute();

            T2M = 0;
            T2Toll = 0;
            T2Cost = 0;
            T2Duration = 0;
            T2Start = DateTime.MinValue;
            T2End = DateTime.MinValue;
            T2CalcRoute = new List<FTLCalcRoute>();

            RetM = 0;
            RetToll = 0;
            RetCost = 0;
            RetDuration = 0;
            RetStart = DateTime.MinValue;
            RetEnd = DateTime.MinValue;
            RetCalcRoute = new FTLCalcRoute();
        }

        [DisplayNameAttributeX(Name = "Státusz", Order = 1)]
        public FTLCalcTourStatus Status { get; set; }

        [DisplayNameAttributeX(Name = "Üzenet", Order = 2)]
        public List<string> Msg { get; set; }

        [DisplayNameAttributeX(Name = "Beosztott szállítási feladat", Order = 3)]
        public FTLTask Task { get; set; }

        [DisplayNameAttributeX(Name = "Helyezés", Order = 2)]
        public int Rank { get; set; }


        [DisplayNameAttributeX(Name = "Jármű", Order = 3)]
        public FTLTruck Truck { get; set; }

        #region túrarészletezők
        [DisplayNameAttributeX(Name = "I.túra hossz (m)", Order = 4)]
        public double T1M { get; set; }

        [DisplayNameAttributeX(Name = "I.túra útdíj", Order = 5)]
        public double T1Toll { get; set; }

        [DisplayNameAttributeX(Name = "I.túra költség", Order = 6)]
        public double T1Cost { get; set; }

        [DisplayNameAttributeX(Name = "I.túra időtartam", Order = 7)]
        public double T1Duration { get; set; }

        [DisplayNameAttributeX(Name = "I.túra kezdete", Order = 8)]
        public DateTime T1Start { get; set; }

        [DisplayNameAttributeX(Name = "I.túra vége", Order = 9)]
        public DateTime T1End { get; set; }

        [DisplayNameAttributeX(Name = "I.túra részletek", Order = 10)]
        public List<FTLCalcRoute> T1CalcRoute { get; set; }

        [DisplayNameAttributeX(Name = "Átállás hossz (m)", Order = 11)]
        public double RelM { get; set; }

        [DisplayNameAttributeX(Name = "Átállás útdíj", Order = 12)]
        public double RelToll { get; set; }

        [DisplayNameAttributeX(Name = "Átállás költség", Order = 13)]
        public double RelCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás időtartam", Order = 14)]
        public double RelDuration { get; set; }

        [DisplayNameAttributeX(Name = "Átállás kezdete", Order = 15)]
        public DateTime RelStart { get; set; }

        [DisplayNameAttributeX(Name = "Átállás vége", Order = 16)]
        public DateTime RelEnd { get; set; }

        [DisplayNameAttributeX(Name = "Átállás részletek", Order = 17)]
        public FTLCalcRoute RelCalcRoute { get; set; }

        [DisplayNameAttributeX(Name = "II.túra hossz (m)", Order = 18)]
        public double T2M { get; set; }

        [DisplayNameAttributeX(Name = "II.túra útdíj", Order = 19)]
        public double T2Toll { get; set; }

        [DisplayNameAttributeX(Name = "II.túra költség", Order = 20)]
        public double T2Cost { get; set; }

        [DisplayNameAttributeX(Name = "II.túra időtartam", Order = 21)]
        public double T2Duration { get; set; }

        [DisplayNameAttributeX(Name = "II.túra kezdete", Order = 22)]
        public DateTime T2Start { get; set; }

        [DisplayNameAttributeX(Name = "II.túra vége", Order = 23)]
        public DateTime T2End { get; set; }

        [DisplayNameAttributeX(Name = "II.túra részletek", Order = 24)]
        public List<FTLCalcRoute> T2CalcRoute { get; set; }


        [DisplayNameAttributeX(Name = "Visszatérés hossz (m)", Order = 25)]
        public double RetM { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés  útdíj", Order = 26)]
        public double RetToll { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés  költség", Order = 27)]
        public double RetCost { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés időtartam", Order = 28)]
        public double RetDuration { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés kezdete", Order = 29)]
        public DateTime RetStart { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés vége", Order = 29)]
        public DateTime RetEnd { get; set; }

        [DisplayNameAttributeX(Name = "Visszatérés részletek", Order = 30)]
        public FTLCalcRoute RetCalcRoute { get; set; }

        #endregion

        [DisplayNameAttributeX(Name = "Befejezés időpontja", Order = 31)]
        public DateTime TimeComplete { get { return Truck.CurrIsOneWay ? T2End : RetEnd; } }

        [DisplayNameAttributeX(Name = "II.túra teljesítésének költsége", Order = 32)]
        public double AdditionalCost { get { return RelToll + RelCost + T2Toll + T2Cost + RetToll + RetCost; } }

        [DisplayNameAttributeX(Name = "Összes költség", Order = 33)]
        public double FullCost { get { return T1Toll + T1Cost + AdditionalCost; } }

        [DisplayNameAttributeX(Name = "Teljes hossz (m)", Order = 34)]
        public double FullM { get { return T1M + RelM + T2M + RetM; } }

        [DisplayNameAttributeX(Name = "Teljes időtartam", Order = 35)]
        public double FullDuration { get { return T1Duration + RelDuration + T2Duration + RetDuration; } }

    }
}
