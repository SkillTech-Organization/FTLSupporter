using PMap.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    /*
        ID		Sztring	I
        Típus		Sztring	I
        Irányos túra		Logikai	I
        Megbízó		Sztring	N
        Felrakó megnevezés		Sztring	N
        Felrakó cím		Szám	N
        Felrakás kezdete		Időpont	I
        Felrakás vége		Időpont	I
        Felrakó lat		Szám	I
        Felrakó lng		Szám	I
        Lerakó megnevezés		Sztring	N
        Lerakó cím		Sztring	N
        Lerakás kezdete		Időpont	I
        Lerakás vége		Időpont	I
        Lerakó lat		Szám	I
        Lerakó lng		Szám	I
        Súly		Szám	I
        Teljesítő járműtípusok		Sztring	I
     */
    public class FTLTask
    {
        [DisplayNameAttributeX(Name = "Azonosító", Order = 1)]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Típus", Order = 1)]
        public string TaskType { get; set; }

        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 1)]
        public bool IsOneWay { get; set; }

        [DisplayNameAttributeX(Name = "Megbízó", Order = 1)]
        public string Client { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó megnevezés", Order = 1)]
        public string PartnerNameFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó megnevezés", Order = 1)]
        public string PartnerNameFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás kezdete", Order = 1)]
        public string OpenStartFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás vége", Order = 1)]
        public string OpenEndFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 1)]
        public double LatFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 1)]
        public double LngFrom { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó megnevezés", Order = 1)]
        public string PartnerNameTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó megnevezés", Order = 1)]
        public string PartnerNameTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás kezdete", Order = 1)]
        public string OpenStartTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakás vége", Order = 1)]
        public string OpenEndTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lat", Order = 1)]
        public double LatTo { get; set; }

        [DisplayNameAttributeX(Name = "Felrakó lng", Order = 1)]
        public double LngTo { get; set; }

    }
}
