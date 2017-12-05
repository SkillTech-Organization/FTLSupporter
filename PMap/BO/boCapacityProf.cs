using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;
using PMap.Common.Attrib;

namespace PMap.BO
{
    [Serializable]
    public class boCapacityProf
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true, FieldName="CPP_NAME1")]
        public string CPP_NAME { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public double CPP_LOADVOL { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public double CPP_LOADQTY { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool CPP_DELETED { get; set; }

        [WriteFieldAttribute(Insert = false, Update = true)]
        public DateTime LASTDATE { get; set; }

    }
}
