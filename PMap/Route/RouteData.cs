using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using PMap.DB;
using System.Data;
using PMap.DB.Base;
using PMap.LongProcess.Base;
using PMap.BO;
using PMap.BLL;
using PMap.Localize;
using PMap.Common;


namespace PMap.Route
{
    public class CCalcNodeFrom
    {
        public int NOD_ID_FROM { get; set; }
        public string RZN_ID_LIST { get; set; }

    }


    /// <summary>
    /// </summary>
    public class RouteData
    {

        private static volatile object m_Lock = new object();
        private static volatile bool m_Initalized = false;

        public Dictionary<string, boEdge> Edges = null; //Az útvonalak korlátozás-zónatípusonként

        public Dictionary<int, PointLatLng> NodePositions = null;  //Node koordináták

        Dictionary<string, Dictionary<string, double>> dicAllTolls = new Dictionary<string, Dictionary<string, double>>();

        private bllRoute m_bllRoute;

        public int NodeCount { get; set; }
        RouteData()
        {
        }

        //Singleton technika...
        static private RouteData instance = null;        //Mivel statikus tag a program indulásakor 
        static public RouteData Instance                                  //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                lock (m_Lock)
                {
                    if (instance == null)
                    {

                        instance = new RouteData();
                    }
                }
                return instance;

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_DBA"></param>
        public void Init(SQLServerAccess p_DBA, BaseProgressDialog p_form, bool p_Forced = false)
        {


            using (GlobalLocker lockObj = new GlobalLocker(Global.lockObjectInit))
            {
                if (!m_Initalized || p_Forced)
                {
                    m_bllRoute = new bllRoute(p_DBA);


                    Edges = new Dictionary<string, boEdge>();
                    NodePositions = null;

                    if (p_form != null)
                    {
                        p_form.SetInfoText(PMapMessages.M_ROUTEDT_EDGES); //TODO:Message/be
                        p_form.NextStep();
                    }


                    //összes útdíj felolvasása
                    dicAllTolls = m_bllRoute.GetAllTolls();
                    
                    /// <summary>
                    /// Teljes térkép felolvasása
                    /// Megj: Az útvonalkereső 0-tól kezdődő sorszámokkal dolgozik, ezért az összes a 0. elemet dummy értékre vesszük
                    /// </summary>
                    try
                    {
                        //üríteni! a EDG_ETLCODE='67u81k45m67u88k' 
                        DateTime dtStart = DateTime.Now;
                        DataTable dt = m_bllRoute.GetEdgesToDT();
                        NodeCount = m_bllRoute.GetMaxNodeID() + 1;

                        foreach (DataRow dr in dt.Rows)
                        {

                            int Source = Util.getFieldValue<int>(dr, "NOD_NUM");
                            int Destination = Util.getFieldValue<int>(dr, "NOD_NUM2");
                            bool OneWay = Util.getFieldValue<bool>(dr, "EDG_ONEWAY");
                            bool DestTraffic = Util.getFieldValue<bool>(dr, "EDG_DESTTRAFFIC");

                            string keyFrom = Source.ToString() + "," + Destination.ToString();
                            string keyTo = Destination.ToString() + "," + Source.ToString();

                            boEdge edgeFrom = new boEdge
                              {
                                  ID = Util.getFieldValue<int>(dr, "ID"),
                                  NOD_ID_FROM = Source,
                                  NOD_ID_TO = Destination,
                                  EDG_NAME = Util.getFieldValue<string>(dr, "EDG_NAME"),
                                  EDG_LENGTH = Util.getFieldValue<float>(dr, "EDG_LENGTH"),
                                  RDT_VALUE = Util.getFieldValue<int>(dr, "RDT_VALUE"),
                                  RZN_ID = Util.getFieldValue<int>(dr, "RZN_ID"),
                                  RST_ID = Util.getFieldValue<int>(dr, "RST_ID"),
                                  EDG_ONEWAY = OneWay,
                                  EDG_DESTTRAFFIC = DestTraffic,
                                  WZONE = Util.getFieldValue<string>(dr, "RZN_ZONECODE") + " " + Util.getFieldValue<string>(dr, "RZN_ZoneName"),
                                  CalcSpeed = PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")],
                                  CalcDuration = (float)(Util.getFieldValue<float>(dr, "EDG_LENGTH") / PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")] / 3.6 * 60),
                                  EDG_ETLCODE = Util.getFieldValue<string>(dr, "EDG_ETLCODE"),
                                  Tolls = dicAllTolls[Util.getFieldValue<string>(dr, "EDG_ETLCODE")],
                                  fromLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD1_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD1_XPOS") / Global.LatLngDivider),
                                  toLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD2_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD2_XPOS") / Global.LatLngDivider)
                                  
                              };


                            if (!Edges.ContainsKey(keyFrom))
                            {
                                Edges.Add(keyFrom, edgeFrom);
                            }
                            else
                            {
                                if (Edges[keyFrom].RDT_VALUE > edgeFrom.RDT_VALUE)
                                    Edges[keyFrom] = edgeFrom;
                            }

                            if (!OneWay)
                            {
                                boEdge edgeTo = new boEdge
                                {
                                    ID = Util.getFieldValue<int>(dr, "ID"),
                                    NOD_ID_FROM = Destination,
                                    NOD_ID_TO = Source,
                                    EDG_NAME = Util.getFieldValue<string>(dr, "EDG_NAME"),
                                    EDG_LENGTH = Util.getFieldValue<float>(dr, "EDG_LENGTH"),
                                    RDT_VALUE = Util.getFieldValue<int>(dr, "RDT_VALUE"),
                                    RZN_ID = Util.getFieldValue<int>(dr, "RZN_ID"),
                                    RST_ID = Util.getFieldValue<int>(dr, "RST_ID"),
                                    EDG_ONEWAY = OneWay,
                                    EDG_DESTTRAFFIC = DestTraffic,
                                    WZONE = Util.getFieldValue<string>(dr, "RZN_ZONECODE") + " " + Util.getFieldValue<string>(dr, "RZN_ZoneName"),
                                    CalcSpeed = PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")],
                                    CalcDuration = (float)(Util.getFieldValue<float>(dr, "EDG_LENGTH") / PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")] / 3.6 * 60),
                                    EDG_ETLCODE = Util.getFieldValue<string>(dr, "EDG_ETLCODE"),
                                    Tolls = dicAllTolls[Util.getFieldValue<string>(dr, "EDG_ETLCODE")],
                                    fromLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD2_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD2_XPOS") / Global.LatLngDivider),
                                    toLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD1_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD1_XPOS") / Global.LatLngDivider)
                                };

                                if (!Edges.ContainsKey(keyTo))
                                {
                                    Edges.Add(keyTo, edgeTo);
                                }
                                else
                                {
                                    if (Edges[keyTo].RDT_VALUE > edgeTo.RDT_VALUE)
                                        Edges[keyTo] = edgeTo;
                                }

                            }

                        }

                        if (p_form != null)
                        {
                            p_form.SetInfoText(PMapMessages.M_ROUTEDT_POS);
                            p_form.NextStep();
                        }
                        DataTable dtNodes = m_bllRoute.GetAllNodesToDT();
                        NodePositions = (from row in dtNodes.AsEnumerable()
                                         select new
                                         {
                                             Key = Util.getFieldValue<int>(row, "ID"),
                                             Value = new PointLatLng(Util.getFieldValue<double>(row, "NOD_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(row, "NOD_XPOS") / Global.LatLngDivider)
                                         }).ToDictionary(n => n.Key, n => n.Value);



                        Util.Log2File("RouteData.Init()  " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());
                        m_Initalized = true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    finally
                    {
                        //             PMapCommonVars.Instance.CT_DB.CloseQuery();
                    }
                    m_Initalized = true;
                }
            }
        }

        public void getNeigboursByBound(string p_RZN_ID_LIST, out Dictionary<string, List<int>[]> o_neighborsFull, out Dictionary<string, List<int>[]> o_neighborsCut, RectLatLng p_cutBoundary)
        {
            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";
            List<string> arr = new List<string>();
            arr.Add(p_RZN_ID_LIST);
            getNeigboursByBound(arr, out o_neighborsFull, out o_neighborsCut, p_cutBoundary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_boundary"></param>
        /// <param name="aRZN_ID_LIST">Behajtásiövezet-lista</param>
        /// <returns></returns>
        public void getNeigboursByBound(List<string> aRZN_ID_LIST, out Dictionary<string, List<int>[]> o_neighborsFull, out Dictionary<string, List<int>[]> o_neighborsCut, RectLatLng p_cutBoundary)
        {
            DateTime dtStart = DateTime.Now;
            o_neighborsFull = new Dictionary<string, List<int>[]>();  //csomópont szomszédok korlátozás-zónatípusonként
            o_neighborsCut = new Dictionary<string, List<int>[]>();  //csomópont szomszédok korlátozás-zónatípusonként (vágott térkép)

            //Térkép készítése minden behajtásiövezet-listára. Csak akkora méretű térképet használunk,
            //amelybe beleférnek (kis ráhagyással persze) a lerakók.
            foreach (string sRZN_ID_LIST in aRZN_ID_LIST)
            {
                int nEdgCnt = 0;
                List<int>[] neighboursFull = new List<int>[RouteData.Instance.Edges.Count + 1].Select(p => new List<int>()).ToArray();
                List<int>[] neighboursCut = new List<int>[RouteData.Instance.Edges.Count + 1].Select(p => new List<int>()).ToArray();

                string[] aRZN = aRZN = sRZN_ID_LIST.Split(',');

                foreach (KeyValuePair<string, boEdge> item in RouteData.Instance.Edges)
                {
                    boEdge edg = (boEdge)item.Value;

                    if ( /*sRZN_ID_LIST == ""                                                 /// Van-e rajta behajtási korlátozást figyelembe vegyünk-e? ( sRZN_ID_LIST == "" --> NEM)
                        || */ edg.RZN_ID == 0                                                  /// Védett övezet-e
                        || (edg.EDG_DESTTRAFFIC && PMapIniParams.Instance.DestTraffic)               /// PMapIniParams.Instance.DestTraffic paraméter beállítása esetén a célforgalomban használható 
                        /// utaknál nem veszük a korlátozást figyelembe (SzL, 2013.04.16)
                        || aRZN.Contains(edg.RZN_ID.ToString()))                           /// Az él szerepel-e a zónalistában?
                    /// 
                    {
                        neighboursFull[edg.NOD_ID_FROM].Add(edg.NOD_ID_TO);
                        if (PMapIniParams.Instance.CutMapForRouting && p_cutBoundary != null &&             /// a térképkivágás határain nelül vagyunk ?
                           (p_cutBoundary.Contains(edg.fromLatLng) && p_cutBoundary.Contains(edg.toLatLng)))
                        {
                            nEdgCnt++;
                            neighboursCut[edg.NOD_ID_FROM].Add(edg.NOD_ID_TO);
                        }
                    }
                }
                Console.WriteLine("RZN_ID_LIST:" + sRZN_ID_LIST + " edgcnt:" + Edges.Count.ToString() + "->" + nEdgCnt.ToString());
                o_neighborsFull.Add(sRZN_ID_LIST, neighboursFull);
                o_neighborsCut.Add(sRZN_ID_LIST, neighboursCut);
            }
            Console.WriteLine("getNeigboursByBound " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());
        }
    }
}
