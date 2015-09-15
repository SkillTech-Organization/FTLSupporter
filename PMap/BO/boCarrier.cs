using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;

namespace PMap.BO
{
    public class boCarrier
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }
        
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string CRR_CODE { get; set; }
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string CRR_NAME { get; set; }


        //a többi mezőt majd később kezeljük

        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool CRR_DELETED { get; set; }
        [WriteFieldAttribute(Insert = false, Update = true)]
        public DateTime LASTDATE { get; set; }

    }
}
