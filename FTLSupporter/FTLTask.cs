using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
  
    public class FTLTask
    {
        [DisplayNameAttributeX(Name = "Azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TaskID")]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Megbízó", Order = 3)]
        public string Client { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó megnevezés", Order = 4)]
        public string PartnerNameFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás időablak kezdete ", Order = 5)]
        [Required(ErrorMessage = "Kötelező mező:StartFrom")]
        public DateTime OpenFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás időablak vége", Order = 6)]
        [ErrorIfPropAttrX(EvalMode.IsSmallerThanAnother, "OpenFrom", "A felrakás kezdete későbbi, mint a befejezése")]
        public DateTime CloseFrom { get; set; }
felrakás időtartama

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 7)]
        [Required(ErrorMessage = "Kötelező mező:LatFrom")]
        public double LatFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 8)]
        [Required(ErrorMessage = "Kötelező mező:LngFrom")]
        public double LngFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó megnevezés", Order = 9)]
        public string PartnerNameTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás időablak kezdete", Order = 10)]
        [Required(ErrorMessage = "Kötelező mező:StartTo")]
        public DateTime OpenTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás időablak vége", Order = 11)]
        [Required(ErrorMessage = "Kötelező mező:EndTo")]
        public DateTime CloseTo { get; set; }
learkás időtartama

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 12)]
        [Required(ErrorMessage = "Kötelező mező:LatTo")]
        public double LatTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 13)]
        [Required(ErrorMessage = "Kötelező mező:LngTo")]
        public double LngTo { get; set; }

        [DisplayNameAttributeX(Name = "Árutpus", Order = 14)]
        [Required(ErrorMessage = "Kötelező mező:TaskType")]
        public string CargoType { get; set; }

        [DisplayNameAttributeX(Name = "Súly", Order = 15)]
        [Required(ErrorMessage = "Kötelező mező:Weight")]
        public double Weight { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítő járműtípusok", Order = 15)]
        public string TruckTypes { get; set; }


        internal int NOD_ID_FROM { get; set; }
        internal int NOD_ID_TO { get; set; }


    }
}
