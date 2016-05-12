using PMap.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    internal class FTLPMapRoute
    {
        public class FTLToll
        {
            public int ETollCat { get; set; }                          //A díjszámításnál használandó járműkategória. 
            public int EngineEuro { get; set; }                        //Jármű motor EURO kategória
            public double Toll { get; set; }                           //Útdíj
        }

        public FTLPMapRoute()
        {
            route = null;
        }
        public int fromNOD_ID { get; set; }
        public int toNOD_ID { get; set; }
        public string RZN_ID_LIST { get; set; }
        public boRoute route { get; set; }

        public string NODEList
        {
            get {
                return fromNOD_ID.ToString() + "," + string.Join(",", route.Edges.Select(x => x.NOD_ID_TO.ToString()).ToArray()); 

            }
        }

    }
}
