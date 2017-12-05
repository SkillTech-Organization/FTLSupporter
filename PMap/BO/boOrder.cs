using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;
using PMap.Common.Attrib;

namespace PMap.BO
{
    [Serializable]
    public class boOrder
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public int OTP_ID { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public int CTP_ID { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public int DEP_ID { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public int WHS_ID { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ORD_NUM { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ORD_ORIGNUM { get; set; }                     //Masterplast mező
        [WriteFieldAttribute(Insert = true, Update = true)]
        public DateTime ORD_DATE { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ORD_CLIENTNUM { get; set; }

        [WriteFieldAttribute(Insert = false, Update = true)]
        public DateTime ORD_LOCKDATE { get; set; }                  //Új felvitelkor nem szabad tölteni
        [WriteFieldAttribute(Insert = true, Update = true)]
        public DateTime ORD_FIRSTDATE { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_QTY { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_ORIGQTY1 { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_ORIGQTY2 { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_ORIGQTY3 { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_ORIGQTY4 { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_ORIGQTY5 { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public int ORD_SERVS { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public int ORD_SERVE { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_VOLUME { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_LENGTH { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_WIDTH { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ORD_HEIGHT { get; set; }

        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool ORD_LOCKED { get; set; }
        [WriteFieldAttribute(Insert = false, Update = true)]
        public bool ORD_ISOPT { get; set; }                      //Új felvitelkor nem szabad tölteni
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ORD_GATE { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ORD_COMMENT { get; set; }
        [WriteFieldAttribute(Insert = false, Update = true)]
        public bool ORD_UPDATED { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool ORD_ACTIVE { get; set; }
        [WriteFieldAttribute(Insert = false, Update = true)]
        public DateTime LASTDATE { get; set; }

    }
}
