using PMap.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLRoute
    {
        public int fromNOD_ID { get; set; }
        public int toNOD_ID { get; set; }
        public string RZN_ID_LIST { get; set; }
        public boRoute route { get; set; }
        public double duration { get; set; }
        public double toll { get; set; }


    }
}
