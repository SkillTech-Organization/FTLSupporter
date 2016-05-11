using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FTLSupporter
{

    public class FTLPoint
    {
        [ItemIDAttr]
        [DisplayNameAttributeX(Name = "Túrapont azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TPID")]
        public string TPID { get; set; }

        [DisplayNameAttributeX(Name = "Megnevezés", Order = 2)]
        public string Name { get; set; }


        [DisplayNameAttributeX(Name = "Cím", Order = 3)]
        public string Addr { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálás időablak kezdete ", Order = 4)]
        [Required(ErrorMessage = "Kötelező mező:Open")]
        public DateTime Open { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálás időablak vége", Order = 5)]
        [ErrorIfPropAttrX(EvalMode.IsSmaller, "Close", "A felrakás kezdete későbbi, mint a befejezése")]
        public DateTime Close { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálás időtartama", Order = 6)]
        [ErrorIfConstAttrX(EvalMode.IsSmaller, 0, "Kötelező mező:Lng")]
        public int SrvDuration { get; set; }

        [DisplayNameAttributeX(Name = "Hosszúsági koordináta (lat)", Order = 7)]
        [ErrorIfConstAttrX(EvalMode.IsSmallerOrEqualr, 0, "Kötelező mező:Lat")]
        public double Lat { get; set; }

        [DisplayNameAttributeX(Name = "Szélességi koordináta (lng)", Order = 8)]
        [ErrorIfConstAttrX(EvalMode.IsSmallerOrEqualr, 0, "Kötelező mező:Lng")]
        public double Lng { get; set; }

        [DisplayNameAttributeX(Name = "Érkezés", Order = 9)]
//        [Required(ErrorMessage = "Kötelező mező:Arrival")]
        public DateTime Arrival { get; set; }

        [DisplayNameAttributeX(Name = "Indulás", Order = 10)]
        public DateTime Departure { get { return Arrival.AddMinutes(SrvDuration); } }


        /* local members */
        internal int NOD_ID { get; set; }

        public FTLPoint ShallowCopy()
        {
            return (FTLPoint)this.MemberwiseClone();
        }

    }
}
