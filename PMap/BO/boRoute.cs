using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;

namespace PMap.BO
{

    [Serializable]
    public class boRoute
    {
        public boRoute()
        {
            Edges = new List<boEdge>();
            Route = new MapRoute("");
        }

        public int ID { get; set; }
        public int NOD_ID_FROM { get; set; }
        public int NOD_ID_TO { get; set; }
        public string RZN_ID_LIST { get; set; }
        public int DST_WEIGHT { get; set; }
        public int DST_WIDTH { get; set; }
        public int DST_HEIGHT { get; set; }
        public double DST_DISTANCE { get; set; }
        public double CalcDistance                  //lehet, hogy nem hasznaljuk
        {
            get
            {
                if (Edges != null)
                    return Edges.Sum(e => e.EDG_LENGTH);
                else
                    return 0;
            }
        }
        public MapRoute Route { get; set; }         //Az útvonal GPS kordinátákkal
        public List<boEdge> Edges { get; set; }      //Az útvonal élekkel 

    }
}
