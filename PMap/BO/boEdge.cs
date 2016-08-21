using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.Web.Script.Serialization;

namespace PMap.BO
{
    [Serializable]
    public class boEdge
    {
        public int ID { get; set; }
        public int NOD_ID_FROM { get; set; }
        public int NOD_ID_TO { get; set; }
        public int RDT_VALUE { get; set; }
        public string EDG_NAME { get; set; }
        public float EDG_LENGTH { get; set; }                       //futásidő miatt float
        public bool EDG_ONEWAY { get; set; }
        public bool EDG_DESTTRAFFIC { get; set; }
        public int RZN_ID { get; set; }
        public int RST_ID { get; set; }
        public string WZONE { get; set; }
        public string EDG_ETLCODE { get; set; }
        public float CalcSpeed { get; set; }                      //idealizált sebességprofil sebesség (ez alapján számítjuk a leggyorsabb utat)
        public float CalcDuration { get; set; }                   //menetidő (idealizált sebességprofil alapján) megj.:futásidő miatt float
        [ScriptIgnore]
        public Dictionary<string, double> Tolls { get; set; }      //Útdíjak járműkategóriánként, teljes szelvénydíjakkal
        [ScriptIgnore]
        public PointLatLng fromLatLng { get; set; }                //LatLng kiemelése, hogy gyors lehessen a térképkivágás útvonalszámításnál
        [ScriptIgnore]
        public PointLatLng toLatLng { get; set; }
    }
  

}
