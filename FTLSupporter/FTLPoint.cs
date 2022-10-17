using PMapCore.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace FTLSupporter
{
    [Serializable]
    public class FTLPoint
    {
        [ItemIDAttr]
        [DisplayNameAttributeX(Name = "Túrapont azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TPID")]
        [JsonPropertyName("tpid")]
        public string TPID { get; set; }

        [DisplayNameAttributeX(Name = "Megnevezés", Order = 2)]
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [DisplayNameAttributeX(Name = "Cím", Order = 3)]
        [JsonPropertyName("addr")]
        public string Addr { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálás időablak kezdete ", Order = 4)]
        [Required(ErrorMessage = "Kötelező mező:Open")]
        [JsonPropertyName("open")]
        public DateTime Open { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálás időablak vége", Order = 5)]
        [ErrorIfPropAttrX(EvalMode.IsSmaller, "Close", "A felrakás kezdete későbbi, mint a befejezése")]
        [JsonPropertyName("close")]
        public DateTime Close { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálás időtartama", Order = 6)]
        [ErrorIfConstAttrX(EvalMode.IsSmaller, 0, "Kötelező mező:SrvDuration")]
        [JsonPropertyName("srvDuration")]
        public int SrvDuration { get; set; }

        [DisplayNameAttributeX(Name = "Türelmi idő", Order = 7)]
        [ErrorIfConstAttrX(EvalMode.IsSmaller, 0, "Nullánál nem lehet kisebb az értéke!")]
        [JsonPropertyName("extraPeriod")]
        public int ExtraPeriod { get; set; }

        [DisplayNameAttributeX(Name = "Hosszúsági koordináta (lat)", Order = 8)]
        [ErrorIfConstAttrX(EvalMode.IsSmallerOrEqual, 0, "Kötelező mező:Lat")]
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [DisplayNameAttributeX(Name = "Szélességi koordináta (lng)", Order = 9)]
        [ErrorIfConstAttrX(EvalMode.IsSmallerOrEqual, 0, "Kötelező mező:Lng")]
        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        [DisplayNameAttributeX(Name = "Érkezés", Order = 10)]
        //        [Required(ErrorMessage = "Kötelező mező:Arrival")]
        [JsonPropertyName("realArrival")]
        public DateTime RealArrival { get; set; }

        [DisplayNameAttributeX(Name = "Indulás", Order = 11)]
        public DateTime RealDeparture { get { return RealArrival.AddMinutes(SrvDuration); } }

        [DisplayNameAttributeX(Name = "Türelmi időben történt megérkezés?", Order = 12)]
        public bool ArrInExtraPeriod  { get { return Close < RealDeparture; } }


        /* local members */
        internal int NOD_ID { get; set; }
        internal DateTime RealClose { get { return Close.AddMinutes(ExtraPeriod); } }

        public FTLPoint ShallowCopy()
        {
            return (FTLPoint)this.MemberwiseClone();
        }

    }
}
