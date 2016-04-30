using PMap.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLRoute
    {
        public class FTLToll
        {
            public int ETollCat { get; set; }                          //A díjszámításnál használandó járműkategória. 
            public int EngineEuro { get; set; }                        //Jármű motor EURO kategória
            public double Toll { get; set; }                           //Útdíj
        }

        public FTLRoute()
        {
            Toll = new List<FTLToll>();
            route = null;
        }
        public int fromNOD_ID { get; set; }
        public int toNOD_ID { get; set; }
        public string RZN_ID_LIST { get; set; }
        public boRoute route { get; set; }
        public double duration { get; set; }
        public List<FTLToll> Toll { get; set; }

    }
}
