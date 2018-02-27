﻿using System;
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
using System.IO;
using Newtonsoft.Json;

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

        //Lazy objects are thread safe, double checked and they have better performance than locks.
        //see it: http://csharpindepth.com/Articles/General/Singleton.aspx
        private static readonly Lazy<RouteData> m_instance = new Lazy<RouteData>(() => new RouteData(), true);

        private static volatile bool m_Initalized = false;

        public Dictionary<string, boEdge> Edges = null; //Az útvonalak korlátozás-zónatípusonként

        public Dictionary<int, PointLatLng> NodePositions  = null;  //Node koordináták

        public int NodeCount {
            get
            {
               return NodePositions.Keys.Max() + 1;
            }
        }

        private RouteData()
        {
        }

        //Singleton technika...
        static public RouteData Instance                                  //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                return m_instance.Value;            //It's thread safe!
            }
        }


        public void InitFromFiles(string p_dir, bool p_Forced = false)
        {
            using (GlobalLocker lockObj = new GlobalLocker(Global.lockObjectInit))
            {
                if (!m_Initalized || p_Forced)
                {
                    JsonSerializerSettings jsonsettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

                    DateTime dtStart = DateTime.Now;
                    string strEdges = Util.FileToString(Path.Combine(p_dir, Global.EXTFILE_EDG));
                    var xEdges = JsonConvert.DeserializeObject<Dictionary<string, boEdge>>(strEdges);
                    Edges = xEdges;

                    string strNodePositions = Util.FileToString(Path.Combine(p_dir, Global.EXTFILE_NOD));
                    var xNodePositions = JsonConvert.DeserializeObject<Dictionary<int, PointLatLng>>(strNodePositions);
                    NodePositions = xNodePositions;

                    Util.Log2File("RouteData.InitFromFiles()  " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());
                    m_Initalized = true;
                }
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
                    Dictionary<string, Dictionary<int, double>> dicAllTolls = new Dictionary<string, Dictionary<int, double>>();

                    bllRoute m_bllRoute;
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
                                EDG_STRNUM1 = Util.getFieldValue<string>(dr, "EDG_STRNUM1"),
                                EDG_STRNUM2 = Util.getFieldValue<string>(dr, "EDG_STRNUM2"),
                                EDG_STRNUM3 = Util.getFieldValue<string>(dr, "EDG_STRNUM3"),
                                EDG_STRNUM4 = Util.getFieldValue<string>(dr, "EDG_STRNUM4"),
                                ZIP_NUM_FROM = Util.getFieldValue<int>(dr, "ZIP_NUM_FROM"),
                                ZIP_NUM_TO = Util.getFieldValue<int>(dr, "ZIP_NUM_TO"),
                                EDG_ONEWAY = OneWay,
                                EDG_DESTTRAFFIC = DestTraffic,
                                WZONE = Util.getFieldValue<string>(dr, "RZN_ZONECODE") + " " + Util.getFieldValue<string>(dr, "RZN_ZoneName"),
                                CalcSpeed = PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")],
                                CalcDuration = (float)(Util.getFieldValue<float>(dr, "EDG_LENGTH") / PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")] / 3.6 * 60),
                                EDG_ETLCODE = Util.getFieldValue<string>(dr, "EDG_ETLCODE"),
                                Tolls = dicAllTolls[Util.getFieldValue<string>(dr, "EDG_ETLCODE")],
                                fromLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD1_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD1_XPOS") / Global.LatLngDivider),
                                toLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD2_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD2_XPOS") / Global.LatLngDivider),
                                EDG_MAXWEIGHT = Util.getFieldValue<int>(dr, "EDG_MAXWEIGHT"),
                                EDG_MAXHEIGHT = Util.getFieldValue<int>(dr, "EDG_MAXHEIGHT"),
                                EDG_MAXWIDTH = Util.getFieldValue<int>(dr, "EDG_MAXWIDTH")

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
                                    EDG_STRNUM1 = Util.getFieldValue<string>(dr, "EDG_STRNUM1"),
                                    EDG_STRNUM2 = Util.getFieldValue<string>(dr, "EDG_STRNUM2"),
                                    EDG_STRNUM3 = Util.getFieldValue<string>(dr, "EDG_STRNUM3"),
                                    EDG_STRNUM4 = Util.getFieldValue<string>(dr, "EDG_STRNUM4"),
                                    EDG_ONEWAY = OneWay,
                                    EDG_DESTTRAFFIC = DestTraffic,
                                    WZONE = Util.getFieldValue<string>(dr, "RZN_ZONECODE") + " " + Util.getFieldValue<string>(dr, "RZN_ZoneName"),
                                    CalcSpeed = PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")],
                                    CalcDuration = (float)(Util.getFieldValue<float>(dr, "EDG_LENGTH") / PMapIniParams.Instance.dicSpeed[Util.getFieldValue<int>(dr, "RDT_VALUE")] / 3.6 * 60),
                                    EDG_ETLCODE = Util.getFieldValue<string>(dr, "EDG_ETLCODE"),
                                    Tolls = dicAllTolls[Util.getFieldValue<string>(dr, "EDG_ETLCODE")],
                                    fromLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD2_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD2_XPOS") / Global.LatLngDivider),
                                    toLatLng = new PointLatLng(Util.getFieldValue<double>(dr, "NOD1_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dr, "NOD1_XPOS") / Global.LatLngDivider),
                                    EDG_MAXWEIGHT = Util.getFieldValue<int>(dr, "EDG_MAXWEIGHT"),
                                    EDG_MAXHEIGHT = Util.getFieldValue<int>(dr, "EDG_MAXHEIGHT"),
                                    EDG_MAXWIDTH = Util.getFieldValue<int>(dr, "EDG_MAXWIDTH")
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
                        throw;
                    }

                    finally
                    {
                        //             PMapCommonVars.Instance.CT_DB.CloseQuery();
                    }
                    m_Initalized = true;
                }
            }
        }

        public void getNeigboursByBound(CRoutePars p_RoutePar, ref Dictionary<string, List<int>[]> o_neighborsFull, ref Dictionary<string, List<int>[]> o_neighborsCut, RectLatLng p_cutBoundary, List<boPlanTourPoint> p_tourPoints)
        {
            getNeigboursByBound(new CRoutePars[] { p_RoutePar }.ToList(), ref o_neighborsFull, ref o_neighborsCut, p_cutBoundary, p_tourPoints);
        }

        /// <summary>
        /// Az útvonalszámításhoz a feltételeknek megfelelő teljes és vágott térkép készítése 
        /// </summary>
        /// <param name="p_boundary"></param>
        /// <param name="aRZN_ID_LIST">Behajtásiövezet-lista</param>
        /// <returns></returns>
        public void getNeigboursByBound(List<CRoutePars> p_RoutePars, ref Dictionary<string, List<int>[]> o_neighborsFull, ref Dictionary<string, List<int>[]> o_neighborsCut, RectLatLng p_cutBoundary, List<boPlanTourPoint> p_tourPoints)
        {
            DateTime dtStart = DateTime.Now;



            o_neighborsFull = new Dictionary<string, List<int>[]>();    //csomópont szomszédok korlátozás-route paraméterenként
            o_neighborsCut = new Dictionary<string, List<int>[]>();     //csomópont szomszédok korlátozás-route paraméterenként (vágott térkép)
            //Térkép készítése minden behajtásiövezet-listára. Csak akkora méretű térképet használunk,
            //amelybe beleférnek (kis ráhagyással persze) a lerakók.
            foreach (var routePar in p_RoutePars)
            {
                List<int>[] MapFull = null;
                List<int>[] MapCut = null;

                PrepareMap(routePar, ref MapFull, ref MapCut, p_cutBoundary, p_tourPoints);
                o_neighborsFull.Add(routePar.Hash, MapFull);
                o_neighborsCut.Add(routePar.Hash, MapCut);

          
            }
            Console.WriteLine("getNeigboursByBound " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString() + ", mmry:" + GC.GetTotalMemory(false));
        }


        /// <summary>
        /// Az útvonalszámításhoz a feltételeknek megfelelő teljes és vágott térkép készítése 
        /// </summary>
        /// <param name="p_boundary"></param>
        /// <param name="aRZN_ID_LIST">Behajtásiövezet-lista</param>
        /// <returns></returns>
        public void PrepareMap(CRoutePars p_RoutePar, ref List<int>[] o_mapFull, ref List<int>[] o_mapCut, RectLatLng p_cutBoundary, List<boPlanTourPoint> p_tourPoints)
        {
            DateTime dtStart = DateTime.Now;

            bool CalcForCompletedTour = false;


            Util.Log2File("PrepareMap available 1: " + GC.GetTotalMemory(false).ToString());

            int nEdgCnt = 0;

            Util.Log2File("PrepareMap available 2: " + GC.GetTotalMemory(false).ToString());

            string[] aRZN = aRZN = p_RoutePar.RZN_ID_LIST.Split(',');
            List<int> tourPointsRzn = new List<int>();

            List<PointLatLng> tourPointPositions = new List<PointLatLng>();

            if (p_tourPoints != null && aRZN.Length > 0 && aRZN.First().StartsWith(Global.COMPLETEDTOUR))           //
            {
                //Útvonalszámításnak jeleztük, hogy a túra letervezett, a túrapontok környzetetében a súlykorlátozások feloldhatóak

                tourPointPositions = p_tourPoints.Select(s => new PointLatLng(s.NOD_YPOS / Global.LatLngDivider, s.NOD_XPOS / Global.LatLngDivider)).ToList();

                CalcForCompletedTour = true;
                var nodes = p_tourPoints.GroupBy(g => g.NOD_ID).Select(s => s.Key).ToList();
                tourPointsRzn = RouteData.Instance.Edges.Where(w => nodes.Any(a => a == w.Value.NOD_ID_FROM) || nodes.Any(a => a == w.Value.NOD_ID_TO)).GroupBy(g => g.Value.RZN_ID).Select(s => s.Key).ToList();


            }
            double TPArea = (double)PMapCommonVars.Instance.TPArea / Global.LatLngDivider;

            //Az adott feletételeknek megfelelő élek beválogatása
            var lstEdges = RouteData.Instance.Edges
                .Where(edg =>
                   (edg.Value.EDG_DESTTRAFFIC && PMapIniParams.Instance.DestTraffic)      /// PMapIniParams.Instance.DestTraffic paraméter beállítása esetén a célforgalomban használható 
                       ||                                                                            /// utaknál nem veszük a korlátozást figyelembe (SzL, 2013.04.16)
                   //Övezet feltételek
                   //
                   ( /*sRZN_ID_LIST == "" */                                                    /// Van-e rajta behajtási korlátozást figyelembe vegyünk-e? ( sRZN_ID_LIST == "" --> NEM)
                        edg.Value.RZN_ID != 0 &&                                               /// Védett övezet-e
                         (aRZN.Contains(edg.Value.RZN_ID.ToString())                            /// Az él szerepel-e a zónalistában? 
                         || tourPointsRzn.Contains(edg.Value.RZN_ID))                           /// Az él szerepel-e a túrapontok zónalistában? 
                        )
                    //Korlátozás feltételek
                    //
                    ||
                    (
                       (
                        //letervezett túrák pontjainak környékén a súly- és méretkorlátozást nem vesszük figyelembe
                        (CalcForCompletedTour &&
                        tourPointPositions.Any(a => Math.Abs(edg.Value.toLatLng.Lng - a.Lng) <= TPArea && Math.Abs(edg.Value.toLatLng.Lat - a.Lat) <= TPArea))
                       )
                       || (
                       //Korlátozás feltételek
                       (edg.Value.EDG_MAXWEIGHT == 0 || p_RoutePar.Weight == 0 || p_RoutePar.Weight <= edg.Value.EDG_MAXWEIGHT)   /// Súlykorlátozás
                           && (edg.Value.EDG_MAXHEIGHT == 0 || p_RoutePar.Height == 0 || p_RoutePar.Height <= edg.Value.EDG_MAXHEIGHT)   /// Magasságkorlátozás
                           && (edg.Value.EDG_MAXWIDTH == 0 || p_RoutePar.Width == 0 || p_RoutePar.Width <= edg.Value.EDG_MAXWIDTH)      /// Szélességlátozás
                          )
                    )
                 ).Select(s => s.Value).ToList();

            var mapFull = new List<int>[RouteData.Instance.Edges.Count + 1].Select(p => new List<int>()).ToArray();
            lstEdges.ForEach(edg => mapFull[edg.NOD_ID_FROM].Add(edg.NOD_ID_TO));
            o_mapFull = mapFull;
            if (PMapIniParams.Instance.CutMapForRouting && p_cutBoundary != null)
            {
                var mapCut = new List<int>[RouteData.Instance.Edges.Count + 1].Select(p => new List<int>()).ToArray();

                lstEdges.Where(edg => p_cutBoundary.Contains(edg.fromLatLng) && p_cutBoundary.Contains(edg.toLatLng)).
                    ToList().ForEach(edg => { ++nEdgCnt; mapCut[edg.NOD_ID_FROM].Add(edg.NOD_ID_TO); });

                o_mapCut = mapCut;

            }
            else
            {
                o_mapCut = null;
            }

            Console.WriteLine("CRoutePars:" + p_RoutePar.ToString() + " edgcnt:" + Edges.Count.ToString() + "->" + nEdgCnt.ToString());
        }




        public int GetNearestNOD_ID(PointLatLng p_pt)
        {
            int diff;
            return GetNearestNOD_ID(p_pt, out diff);
        }

        /// <summary>
        /// Egy térképi ponthoz legközelebb lévő NOD_ID visszaadása
        /// </summary>
        /// <param name="p_pt"></param>
        /// <param name="r_diff"></param>
        /// <returns></returns>
        public int GetNearestNOD_ID(PointLatLng p_pt, out int r_diff)
        {
            r_diff = Int32.MaxValue;

            //Legyünk következetesek, a PMAp-os térkép esetében:
            //X --> lng, Y --> lat

            var nearest = RouteData.Instance.Edges/*.Where(
                w => Util.DistanceBetweenSegmentAndPoint(w.Value.fromLatLng.Lng, w.Value.fromLatLng.Lat,
                w.Value.toLatLng.Lng, w.Value.toLatLng.Lat, ptX, ptY) <=
                 (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                 Global.NearestNOD_ID_Approach : Global.EdgeApproachCity))*/
                  .Where(
                w => Math.Abs(w.Value.fromLatLng.Lng - p_pt.Lng) + Math.Abs(w.Value.fromLatLng.Lat - p_pt.Lat) <
                    (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                    ((double)Global.EdgeApproachCity / Global.LatLngDivider) : ((double)Global.EdgeApproachHighway / Global.LatLngDivider)) 
                    &&
                    Math.Abs(w.Value.toLatLng.Lng - p_pt.Lng) + Math.Abs(w.Value.toLatLng.Lat - p_pt.Lat) <
                    (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                    ((double)Global.EdgeApproachCity / Global.LatLngDivider) : ((double)Global.EdgeApproachHighway / Global.LatLngDivider)))
                 .OrderBy(o => Util.DistanceBetweenSegmentAndPoint(o.Value.fromLatLng.Lng, o.Value.fromLatLng.Lat,
                o.Value.toLatLng.Lng, o.Value.toLatLng.Lat, p_pt.Lng, p_pt.Lat)).Select(s => s.Value).FirstOrDefault();
            if (nearest != null)
            {
                r_diff = (int)(Util.DistanceBetweenSegmentAndPoint(nearest.fromLatLng.Lng, nearest.fromLatLng.Lat,
                nearest.toLatLng.Lng, nearest.toLatLng.Lat, p_pt.Lng, p_pt.Lat) * Global.LatLngDivider);


                return Math.Abs(nearest.fromLatLng.Lng - p_pt.Lng) + Math.Abs(nearest.fromLatLng.Lat - p_pt.Lat) <
                    Math.Abs(nearest.toLatLng.Lng - p_pt.Lng) + Math.Abs(nearest.toLatLng.Lat - p_pt.Lat) ? nearest.NOD_ID_FROM : nearest.NOD_ID_TO;
            }
            return 0;
        }
        public int GetNearestReachableNOD_IDForTruck(PointLatLng p_pt, string p_RZN_ID_LIST, int p_weight, int p_height, int p_width)
        {
            int diff;
            return GetNearestReachableNOD_IDForTruck(p_pt, p_RZN_ID_LIST, p_weight, p_height, p_width, out diff);
        }


        public int GetNearestReachableNOD_IDForTruck(PointLatLng p_pt, string p_RZN_ID_LIST, int p_weight, int p_height, int p_width, out int r_diff)
        {
            r_diff = Int32.MaxValue;

            //Legyünk következetesek, a PMAp-os térkép esetében:
            //X --> lng, Y --> lat
            var lstRZN = p_RZN_ID_LIST.Split(',');

          
            var nearest = RouteData.Instance.Edges.Where(
                w => (Math.Abs(w.Value.fromLatLng.Lng - p_pt.Lng) + Math.Abs(w.Value.fromLatLng.Lat - p_pt.Lat) <
                    (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                    ((double)Global.EdgeApproachCity / Global.LatLngDivider) : ((double)Global.EdgeApproachHighway / Global.LatLngDivider)) 
                    &&
                    Math.Abs(w.Value.toLatLng.Lng - p_pt.Lng) + Math.Abs(w.Value.toLatLng.Lat - p_pt.Lat) <
                    (w.Value.RDT_VALUE == 6 || w.Value.EDG_STRNUM1 != "0" || w.Value.EDG_STRNUM2 != "0" || w.Value.EDG_STRNUM3 != "0" || w.Value.EDG_STRNUM4 != "0" ?
                    ((double)Global.EdgeApproachCity / Global.LatLngDivider) : ((double)Global.EdgeApproachHighway / Global.LatLngDivider))) &&
                    (p_RZN_ID_LIST == "" || w.Value.RZN_ID == 0 || lstRZN.Contains(w.Value.RZN_ID.ToString())) &&
                    (w.Value.EDG_MAXWEIGHT == 0 || p_weight == 0 || w.Value.EDG_MAXWEIGHT <= p_weight) &&
                    (w.Value.EDG_MAXHEIGHT == 0 || p_height == 0 || w.Value.EDG_MAXHEIGHT <= p_height) &&
                    (w.Value.EDG_MAXWIDTH == 0 || p_width == 0 || w.Value.EDG_MAXWIDTH <= p_width))
                 .OrderBy(o => Util.DistanceBetweenSegmentAndPoint(o.Value.fromLatLng.Lng, o.Value.fromLatLng.Lat,
                o.Value.toLatLng.Lng, o.Value.toLatLng.Lat, p_pt.Lng, p_pt.Lat)).Select(s => s.Value).FirstOrDefault();
            if (nearest != null)
            {
                r_diff = (int)(Util.DistanceBetweenSegmentAndPoint(nearest.fromLatLng.Lng, nearest.fromLatLng.Lat,
                nearest.toLatLng.Lng, nearest.toLatLng.Lat, p_pt.Lng, p_pt.Lat) * Global.LatLngDivider);


                return Math.Abs(nearest.fromLatLng.Lng - p_pt.Lng) + Math.Abs(nearest.fromLatLng.Lat - p_pt.Lat) <
                    Math.Abs(nearest.toLatLng.Lng - p_pt.Lng) + Math.Abs(nearest.toLatLng.Lat - p_pt.Lat) ? nearest.NOD_ID_FROM : nearest.NOD_ID_TO;
            }
            return 0;
        }

    }
}
