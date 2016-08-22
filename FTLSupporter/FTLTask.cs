﻿using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    [Serializable]
    public class FTLTask
    {
        public FTLTask ShallowCopy()
        {
            return (FTLTask)this.MemberwiseClone();
        }


        [ItemIDAttr]
        [DisplayNameAttributeX(Name = "Feladat azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TaskID")]
        public string TaskID { get; set; }

        [DisplayNameAttributeX(Name = "Rakománytípus", Order = 2)]
        [Required(ErrorMessage = "Kötelező mező:CargoType")]
        public string CargoType { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítő járműtípusok", Order = 3)]
        public string TruckTypes { get; set; }

        [DisplayNameAttributeX(Name = "Súly", Order = 4)]
        public double Weight { get; set; }

        [DisplayNameAttributeX(Name = "Megbízó", Order = 5)]
        public string Client { get; set; }

        [DisplayNameAttributeX(Name = "Túrapontok", Order = 6)]
        [Required(ErrorMessage = "Kötelező mező:TPoints")]
        public List<FTLPoint> TPoints { get; set; }

        
    }
}
