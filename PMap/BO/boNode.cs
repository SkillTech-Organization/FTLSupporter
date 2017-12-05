﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.BO
{
    [Serializable]
    public class boNode
    {
        public int ID { get; set; }
        public int NOD_NUM { get; set; }
        public string NOD_NAME { get; set; }
        public int ZIP_ID { get; set; }
        public int ZIP_NUM { get; set; }
        public double NOD_XPOS { get; set; }
        public double NOD_YPOS { get; set; }
        public bool NOD_DELETED { get; set; }
        public DateTime LASTDATE { get; set; }
    }
}
