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
        [DisplayNameAttributeX(Name = "Rendszám", Order = 1)]
        public string RegNo { get; set; }

        [DisplayNameAttributeX(Name = "Helyezés", Order = 2)]
        public int Rank { get; set; }

        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 3)]
        public bool IsOneWay { get; set; }

    }
}
