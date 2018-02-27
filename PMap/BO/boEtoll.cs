using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;
using PMap.Common.Attrib;

namespace PMap.BO
{
    [Serializable]
    public class boEtoll
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ETL_CODE { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_LEN_KM { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_J2_TOLL_KM { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_J3_TOLL_KM { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_J4_TOLL_KM { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_J2_TOLL_FULL { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_J3_TOLL_FULL { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ETL_J4_TOLL_FULL { get; set; }
        [WriteFieldAttribute(Insert = false, Update = true)]
        public DateTime LASTDATE { get; set; }

        public Dictionary<int, double> TollsToDict()
        {
            Dictionary<int, double> retTolls = new Dictionary<int, double>();
            retTolls.Add(Global.ETOLLCAT_J0, 0);
            retTolls.Add(Global.ETOLLCAT_J2, ETL_J2_TOLL_FULL);
            retTolls.Add(Global.ETOLLCAT_J3, ETL_J3_TOLL_FULL);
            retTolls.Add(Global.ETOLLCAT_J4, ETL_J4_TOLL_FULL);
            return retTolls;
        }

    }
}
