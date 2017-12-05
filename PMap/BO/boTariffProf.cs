﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;
using PMap.Common.Attrib;

namespace PMap.BO
{
    [Serializable]
    public class boTariffProf
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true, FieldName="TFP_NAME1")]
        public string TFP_NAME { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public double TFP_FIXCOST { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double TFP_KMCOST { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double TFP_HOURCOST { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool TFP_DELETED { get; set; }
        [WriteFieldAttribute(Insert = false, Update = true)]
        public DateTime LASTDATE { get; set; }



    }
}
