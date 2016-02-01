using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PMap.MapProvider;
using System.Data.SqlClient;
using PMap.DB.Base;
using GMap.NET;
using PMap.Route;
using PMap.BO;
using PMap.BLL.Base;
using PMap.Common;
using System.Data.Common;
using System.Net;
using System.Xml.Linq;
using System.Globalization;

namespace PMap.BLL
{
    public class bllRoute : bllBase
    {


        private bllPlan m_bllPlan;
        public bllRoute(SQLServerAccess p_DBA)
            :base(p_DBA, "")
        {
            m_bllPlan = new bllPlan(p_DBA);

        }

        /* multithread-os környezetből hívható rutinok */
        public  int GetMaxNodeID()
        {
            DataTable dtx = DBA.Query2DataTable("select max(ID) as MAXID from NOD_NODE");
            return Util.getFieldValue<int>(dtx.Rows[0], "MAXID");
        }

        public  PointLatLng GetPointLatLng(int p_NOD_ID)
        {
            string sSql = "select * from NOD_NODE NOD  where ID=?";
            DataTable dt = DBA.Query2DataTable(sSql, p_NOD_ID);

            if (dt.Rows.Count == 1)
            {
                return new PointLatLng(Util.getFieldValue<double>(dt.Rows[0], "NOD_YPOS") / Global.LatLngDivider, Util.getFieldValue<double>(dt.Rows[0], "NOD_XPOS") / Global.LatLngDivider);
            }
            else
                return new PointLatLng();
        }

        public  DataTable GetEdgesToDT()
        {
            /*
            String sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                          "select EDG.*, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, NOD1.NOD_YPOS as NOD1_YPOS, NOD1.NOD_XPOS as NOD1_XPOS, " + Environment.NewLine +
                          "NOD2.NOD_YPOS as NOD2_YPOS, NOD2.NOD_XPOS as NOD2_XPOS, RZN.ID as RZN_ID, RZN.RST_ID, RZN.RZN_ZoneName " + Environment.NewLine +
                          "from EDG_EDGE EDG " + Environment.NewLine +
                          "inner join NOD_NODE NOD1 on NOD1.ID = EDG.NOD_NUM " + Environment.NewLine +
                          "inner join NOD_NODE NOD2 on NOD2.ID = EDG.NOD_NUM2 " + Environment.NewLine +
                          "left outer join RZN_RESTRZONE RZN on EDG.RZN_ZONECODE = RZN.RZN_ZoneCode " + Environment.NewLine + 
                          "where EDG.NOD_NUM <> EDG.NOD_NUM2 and RDT_VALUE <> 0";
            */
            String sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                          "select EDG.ID, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, EDG.EDG_LENGTH, EDG.RDT_VALUE ,EDG.EDG_ETLCODE, EDG.EDG_ONEWAY, EDG.EDG_DESTTRAFFIC, EDG.NOD_NUM, EDG.NOD_NUM2, EDG.RZN_ZONECODE, " + Environment.NewLine +
                          "NOD1.NOD_YPOS as NOD1_YPOS, NOD1.NOD_XPOS as NOD1_XPOS, " + Environment.NewLine +
                          "NOD2.NOD_YPOS as NOD2_YPOS, NOD2.NOD_XPOS as NOD2_XPOS, RZN.ID as RZN_ID, RZN.RST_ID, RZN.RZN_ZoneName " + Environment.NewLine +
                          "from EDG_EDGE (NOLOCK) EDG " + Environment.NewLine +
                          "inner join NOD_NODE (NOLOCK) NOD1 on NOD1.ID = EDG.NOD_NUM " + Environment.NewLine +
                          "inner join NOD_NODE (NOLOCK) NOD2 on NOD2.ID = EDG.NOD_NUM2 " + Environment.NewLine +
                          "left outer join RZN_RESTRZONE (NOLOCK) RZN on EDG.RZN_ZONECODE = RZN.RZN_ZoneCode " + Environment.NewLine +
                          "where EDG.NOD_NUM <> EDG.NOD_NUM2 and RDT_VALUE <> 0";

            return DBA.Query2DataTable(sSql);
        }


        public  DataTable GetNodestoDT(String p_NodeList)
        {
            return DBA.Query2DataTable("select * from NOD_NODE where ID in (" + p_NodeList + ")");
        }



        public  void WriteRoutes(List<boRoute> p_Routes, bool p_savePoints)
        {
            //           using (DBLockHolder lockObj = new DBLockHolder(DBA))
            {

                using (TransactionBlock transObj = new TransactionBlock(DBA))
                {
                    try
                    {
                        DateTime dtStart = DateTime.Now;
  
                        SqlCommand command = new SqlCommand(null, DBA.Conn);
                        command.CommandText = "insert into DST_DISTANCE ( NOD_ID_FROM, NOD_ID_TO, RZN_ID_LIST, DST_DISTANCE, DST_EDGES, DST_POINTS) VALUES(@NOD_ID_FROM, @NOD_ID_TO, @RZN_ID_LIST, @DST_DISTANCE, @DST_EDGES, @DST_POINTS)";

                        command.Parameters.Add(new SqlParameter("@NOD_ID_FROM", SqlDbType.Int, 0));
                        command.Parameters.Add(new SqlParameter("@NOD_ID_TO", SqlDbType.Int, 0));
                        command.Parameters.Add(new SqlParameter("@RZN_ID_LIST", SqlDbType.VarChar, Int32.MaxValue));
                        command.Parameters.Add(new SqlParameter("@DST_DISTANCE", SqlDbType.Float, 0));
                        command.Parameters.Add(new SqlParameter("@DST_EDGES", SqlDbType.VarBinary, Int32.MaxValue));
                        command.Parameters.Add(new SqlParameter("@DST_POINTS", SqlDbType.VarBinary, Int32.MaxValue));

                        command.Transaction = DBA.Tran;
                        command.Prepare();



                        foreach (var route in p_Routes)
                        {
                            command.Parameters["@NOD_ID_FROM"].Value = route.NOD_ID_FROM;
                            command.Parameters["@NOD_ID_TO"].Value = route.NOD_ID_TO;
                            command.Parameters["@RZN_ID_LIST"].Value = route.RZN_ID_LIST;
                            command.Parameters["@DST_DISTANCE"].Value = route.DST_DISTANCE;
                            if (route.Edges != null && route.Route != null)
                            {
                                command.Parameters["@DST_EDGES"].Value = Util.ZipStr(getEgesFromEdgeList(route.Edges));
                                if (p_savePoints)
                                    command.Parameters["@DST_POINTS"].Value = Util.ZipStr(getPointsFromPointList(route.Route.Points));
                                else
                                    command.Parameters["@DST_POINTS"].Value = new byte[0];
                            }
                            else
                            {
                                command.Parameters["@DST_EDGES"].Value = new byte[0];
                                command.Parameters["@DST_POINTS"].Value = new byte[0];
                            }
                            command.ExecuteNonQuery();

                        }
                        Console.WriteLine("WriteDistance " + Util.GetSysInfo() + " Időtartam:" + (DateTime.Now - dtStart).ToString());
                    }
                    catch (Exception e)
                    {
                        DBA.Rollback();
                        throw e;
                    }

                    finally
                    {
                    }

                }
            }
        }
        





        public  boRoute GetRouteFromDB(string p_RZN_ID_LIST, int p_NOD_ID_FROM, int p_NOD_ID_TO)
        {
            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            boRoute result = null;
            string sSql = "select * from DST_DISTANCE DST " + Environment.NewLine +
                           "where RZN_ID_LIST=? and NOD_ID_FROM = ? and NOD_ID_TO = ? ";
            DataTable dt = DBA.Query2DataTable(sSql, p_RZN_ID_LIST, p_NOD_ID_FROM, p_NOD_ID_TO);

            if (dt.Rows.Count == 1 && Util.getFieldValue<double>(dt.Rows[0], "DST_DISTANCE") >= 0.0)
            {


                result = new boRoute();
                result.DST_DISTANCE = Util.getFieldValue<double>(dt.Rows[0], "DST_DISTANCE");
                byte[] buff = Util.getFieldValue<byte[]>(dt.Rows[0], "DST_POINTS");
                String points = Util.UnZipStr(buff);
                String[] aPoints = points.Split(Global.SEP_POINTC);
                foreach (string point in aPoints)
                {
                    string[] aPosLatLng = point.Split(Global.SEP_COORDC);
                    result.Route.Points.Add(new PointLatLng(Convert.ToDouble(aPosLatLng[0].Replace(',', '.'), CultureInfo.InvariantCulture), Convert.ToDouble(aPosLatLng[1].Replace(',', '.'), CultureInfo.InvariantCulture)));

                }


                buff = Util.getFieldValue<byte[]>(dt.Rows[0], "DST_EDGES");
                String edges = Util.UnZipStr(buff);

                Dictionary<string, boEdge> dicEdges = new Dictionary<string, boEdge>();
                if (edges != "")
                {
                    sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                           "select EDG.ID as EDGID, EDG.NOD_NUM, EDG.NOD_NUM2, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, EDG.EDG_LENGTH, " + Environment.NewLine +
                           "EDG.EDG_ONEWAY, EDG.EDG_DESTTRAFFIC, EDG.RDT_VALUE, EDG.EDG_ETLCODE, RZN.RZN_ZONENAME from EDG_EDGE  EDG " + Environment.NewLine +
                           "left outer join RZN_RESTRZONE RZN on RZN.RZN_ZoneCode = EDG.RZN_ZONECODE " + Environment.NewLine +
                           " where EDG.ID in (" + edges + ")";

                    DataTable dtEdges = DBA.Query2DataTable(sSql);
                    dicEdges = (from r in dtEdges.AsEnumerable()
                                select new
                                 {
                                     Key = Util.getFieldValue<int>(r, "EDGID").ToString(),
                                     Value = new boEdge
                                            {
                                                ID = Util.getFieldValue<int>(r, "EDGID"),
                                                NOD_ID_FROM = Util.getFieldValue<int>(r, "NOD_NUM"),
                                                NOD_ID_TO = Util.getFieldValue<int>(r, "NOD_NUM2"),
                                                EDG_NAME = Util.getFieldValue<string>(r, "EDG_NAME") != "" ? Util.getFieldValue<string>(r, "EDG_NAME") : "*** nincs név ***",
                                                EDG_LENGTH = Util.getFieldValue<int>(r, "EDG_LENGTH"),
                                                RDT_VALUE = Util.getFieldValue<int>(r, "RDT_VALUE"),
                                                WZONE = Util.getFieldValue<string>(r, "RZN_ZONENAME"),
                                                EDG_ONEWAY = Util.getFieldValue<bool>(r, "EDG_ONEWAY"),
                                                EDG_DESTTRAFFIC = Util.getFieldValue<bool>(r, "EDG_DESTTRAFFIC"),
                                                EDG_ETLCODE = Util.getFieldValue<string>(r, "EDG_ETLCODE"),
                                                Tolls = PMapCommonVars.Instance.LstEToll.Where(i => i.ETL_CODE == Util.getFieldValue<string>(r, "EDG_ETLCODE"))
                                                       .DefaultIfEmpty(new boEtoll()).First().TollsToDict()

                                            }
                                 }).ToDictionary(n => n.Key, n => n.Value);
                    //így boztosítjuk a visszaadott élek rendezettségét
                    String[] aEdges = edges.Split(Global.SEP_EDGEC);
                    foreach (string e in aEdges)
                    {
                        result.Edges.Add(dicEdges[e]);
                    }
                }
            }
            return result;
        }


        public  MapRoute GetMapRouteFromDB(string p_RZN_ID_LIST, int p_NOD_ID_FROM, int p_NOD_ID_TO)
        {
            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            MapRoute result = null;
            string sSql = "select * from DST_DISTANCE DST " + Environment.NewLine +
                           "where RZN_ID_LIST=? and NOD_ID_FROM = ? and NOD_ID_TO = ? ";
            DataTable dt = DBA.Query2DataTable(sSql, p_RZN_ID_LIST, p_NOD_ID_FROM, p_NOD_ID_TO);

            if (dt.Rows.Count == 1)
            {

                result = new MapRoute("");

                if (Util.getFieldValue<double>(dt.Rows[0], "DST_DISTANCE") >= 0.0)
                {
                    byte[] buff = Util.getFieldValue<byte[]>( dt.Rows[0], "DST_POINTS");
                    String points = Util.UnZipStr(buff);
                    String[] aPoints = points.Split(Global.SEP_POINTC);
                    foreach (string point in aPoints)
                    {
                        string[] aPosLatLng = point.Split(Global.SEP_COORDC);
                        result.Points.Add(new PointLatLng(Convert.ToDouble(aPosLatLng[0].Replace(',', '.'), CultureInfo.InvariantCulture), Convert.ToDouble(aPosLatLng[1].Replace(',', '.'), CultureInfo.InvariantCulture)));

                    }
                }

            }
            return result;
        }


        public  DataTable GetRestZonesToDT()
        {
            string sSql = "select distinct " + Environment.NewLine +
                          "isnull( stuff( " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "    select ',' + convert( varchar(MAX), TRZX.RZN_ID )  " + Environment.NewLine +
                          "    from TRZ_TRUCKRESTRZONE TRZX " + Environment.NewLine +
                          "    where TRZX.TRK_ID = TRK.ID " + Environment.NewLine +
                          "    order by TRZX.RZN_ID  " + Environment.NewLine +
                          "    FOR XML PATH('') " + Environment.NewLine +
                          "), 1, 1, ''), '') as RESTZONE_IDS, " + Environment.NewLine +
                          "isnull( stuff( " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "    select ',' + convert( varchar(MAX), RZNX.RZN_ZoneCode ) " + Environment.NewLine +
                          "    from TRZ_TRUCKRESTRZONE TRZX " + Environment.NewLine +
                          "    inner join RZN_RESTRZONE RZNX on RZNX.ID = TRZX.RZN_ID " + Environment.NewLine +
                          "    where TRZX.TRK_ID = TRK.ID " + Environment.NewLine +
                          "    order by TRZX.RZN_ID  " + Environment.NewLine +
                          "    FOR XML PATH('') " + Environment.NewLine +
                          "), 1, 1, ''), '') as RESTZONE_NAMES " + Environment.NewLine +
                          "from TRK_TRUCK TRK " + Environment.NewLine +
                          "UNION  " + Environment.NewLine +
                          " select '' as RESTZONE_IDS, '***nincs engedély***' as RESTZONE_NAMES";

            return DBA.Query2DataTable(sSql);
        }


        public string GetAllRestZones()
        {
            string sRESTZONES = Global.RST_ALLRESTZONES;
            string sSql = "(select distinct     " + Environment.NewLine +
                          "isnull(stuff(        " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "select ',' + convert( varchar(MAX), RZN.ID )  " + Environment.NewLine +
                          "from RZN_RESTRZONE RZN   " + Environment.NewLine +
                          "order by RZN.ID          " + Environment.NewLine +
                          "FOR XML PATH('')         " + Environment.NewLine +
                          "), 1, 1, ''), '')  as RESTZONES  " + Environment.NewLine +
                          ")   ";
            DataTable dt = DBA.Query2DataTable(sSql);
            if (dt.Rows.Count > 0)
            {
                sRESTZONES = Util.getFieldValue<string>(dt.Rows[0], "RESTZONES");
            }
            return sRESTZONES;
        }

        public string GetRestZonesByRST_ID(int p_RST_ID)
        {
            string sRESTZONES = Global.RST_ALLRESTZONES;
            string sSql = "select  isnull( stuff ((SELECT ',' + CONVERT(varchar(MAX), ID) " + Environment.NewLine +
                          "  FROM RZN_RESTRZONE RZN " + Environment.NewLine;
            if (p_RST_ID != Global.RST_NORESTRICT)
                sSql += "  WHERE RST_ID <=? " + Environment.NewLine;
            sSql += " ORDER BY ID FOR XML PATH('')), 1, 1, ''), '') AS RZN_ID_LIST ";

            DataTable dt = DBA.Query2DataTable(sSql, p_RST_ID);
            if (dt.Rows.Count > 0)
            {
                sRESTZONES = Util.getFieldValue<string>(dt.Rows[0], "RESTZONES");
            }
            return sRESTZONES;
       }

        public  DataTable GetSpeedProfsToDT()
        {
            return DBA.Query2DataTable("select * from SPP_SPEEDPROF where SPP_DELETED = 0");
        }


        public  Dictionary<int, string> GetRoadTypesToDict()
        {
            DataTable dt = DBA.Query2DataTable("select * from RDT_ROADTYPE");
            return (from r in dt.AsEnumerable()
                    select new
                    {
                        Key = Util.getFieldValue<int>(r, "ID"),
                        Value = Util.getFieldValue<string>(r, "RDT_NAME1")
                    }).ToDictionary(n => n.Key, n => n.Value);


        }

        public DataTable GetAllNodesToDT()
        {
            return DBA.Query2DataTable("select * from NOD_NODE ");
        }

        public  List<int> GetAllNodes()
        {
            string sSQL = "select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine + 
                            "   inner join NOD_NODE NOD on WHS.NOD_ID = NOD.ID " + Environment.NewLine + 
                            "union " + Environment.NewLine + 
                            "select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine + 
                            "  inner join NOD_NODE NOD on dep.NOD_ID = nod.ID";
            DataTable dt = DBA.Query2DataTable(sSQL);

            return dt.AsEnumerable().Select(row => Util.getFieldValue<int>(row, "ID")).ToList();
        }

        /// <summary>
        /// Egy terv hiányzó távolságai. Csak a terveben lévő járművek övezet listáira kérünk távolságokat
        /// </summary>
        /// <param name="pPLN_ID"></param>
        /// <returns></returns>
        /// </summary>
        /// <param name="pPLN_ID"></param>
        /// <returns></returns>
        public List<boRoute> GetDistancelessPlanNodes(int pPLN_ID)
        {
            string sSQL = "--Meghatározzuk, milyen távolságrekordokra van szükésgünk  a tervben: " + Environment.NewLine +
            "--    A tervben szereplő lerakók között képezünk NOD_ID_FROM NOD_ID_TO távolságokat.  " + Environment.NewLine +
            "--    Ezekhez hozzárakjuk a tervben lévő járművek övezetlistáit (RESTZONE -it.)  " + Environment.NewLine +
            " select  * from  " + Environment.NewLine +
            "	(select NOD_FROM.ID as NOD_ID_FROM, NOD_TO.ID as NOD_ID_TO   " + Environment.NewLine +
            "	 from (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS  " + Environment.NewLine +
            "		   union  " + Environment.NewLine +
            "		   select distinct NOD_ID as ID from DEP_DEPOT DEP  " + Environment.NewLine +
            "		   inner join TOD_TOURORDER TOD on TOD.DEP_ID = DEP.ID and TOD.PLN_ID = ?  " + Environment.NewLine +
            "		   ) NOD_FROM  " + Environment.NewLine +
            "	inner join (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS   " + Environment.NewLine +
            "	       union  " + Environment.NewLine +
            "	      select distinct NOD_ID as ID from DEP_DEPOT DEP  " + Environment.NewLine +
            "	      inner join TOD_TOURORDER TOD on TOD.DEP_ID = DEP.ID and TOD.PLN_ID = ?  " + Environment.NewLine +
            "	      ) NOD_TO on NOD_TO.ID != NOD_FROM.ID and NOD_TO.ID > 0 and NOD_FROM.ID > 0 " + Environment.NewLine +
            "	) ALLNODES, " + Environment.NewLine +
            "	(select distinct " + Environment.NewLine +
            "	  isnull(stuff(  " + Environment.NewLine +
            "	  (  " + Environment.NewLine +
            "		  select ',' + convert( varchar(MAX), TRZX.RZN_ID )  " + Environment.NewLine +
            "		  from TRZ_TRUCKRESTRZONE TRZX  " + Environment.NewLine +
            "		  where TRZX.TRK_ID = TPL.TRK_ID " + Environment.NewLine +
            "		  order by TRZX.RZN_ID   " + Environment.NewLine +
            "		  FOR XML PATH('')  " + Environment.NewLine +
            "	  ), 1, 1, ''), '') as RESTZONES  " + Environment.NewLine +
            "	  from TPL_TRUCKPLAN TPL " + Environment.NewLine +
            "	  where TPL.PLN_ID = ?  " + Environment.NewLine +
            "	) ALLRSTZ " + Environment.NewLine +
            "--kivonjuk a létező távolságokat  " + Environment.NewLine +
            "EXCEPT  " + Environment.NewLine +
            "select DST.NOD_ID_FROM as NOD_ID_FROM, DST.NOD_ID_TO as NOD_ID_TO, isnull(DST.RZN_ID_LIST, '') as RESTZONES from DST_DISTANCE DST  " + Environment.NewLine +
            "order by 1,2,3";

            DataTable dt = DBA.Query2DataTable(sSQL, pPLN_ID, pPLN_ID, pPLN_ID);
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES")
                    }).ToList();
        }

        public  RectLatLng getBoundary(List<int> p_nodes)
        {
            string sNODE_IDs = string.Join(",", p_nodes.Select(i => i.ToString()).ToArray());
            string sSql = "select * from NOD_NODE where id in (" + sNODE_IDs + ")";
            DataTable dt = DBA.Query2DataTable(sSql);
            RectLatLng boundary = new RectLatLng();
            //a koordinátákat egy 'kifordított' négyzetre inicializálkuk, hogy az első 
            //tételnél biztosan kapjanak értéket
            double dTop = -180;
            double dLeft = 180;
            double dBottom = 180;
            double dRight = -180;
            foreach (DataRow dr in dt.Rows)
            {
                double dLat = Util.getFieldValue<double>(dr, "NOD_YPOS") / Global.LatLngDivider;
                double dLng = Util.getFieldValue<double>(dr, "NOD_XPOS") / Global.LatLngDivider;
                if (dLng < dLeft)
                    dLeft = dLng;
                if (dLat > dTop)
                    dTop = dLat;
                if (dLng > dRight)
                    dRight = dLng;
                if (dLat < dBottom)
                    dBottom = dLat;

            }

            dLeft -= PMapIniParams.Instance.CutExtDegree;
            dTop += PMapIniParams.Instance.CutExtDegree;
            dRight += PMapIniParams.Instance.CutExtDegree;
            dBottom -= PMapIniParams.Instance.CutExtDegree;
            boundary = RectLatLng.FromLTRB(dLeft, dTop, dRight, dBottom);
            return boundary;

        }


        /// <summary>
        /// Hiányzó távolságok gyűjtése megrendelések alapján
        /// </summary>
        /// <param name="p_ORD_DATE_S"></param>
        /// <param name="p_ORD_DATE_E"></param>
        /// <returns></returns>
        public List<boRoute> GetDistancelessOrderNodes(DateTime p_ORD_DATE_S, DateTime p_ORD_DATE_E)
        {


            string sSQL = "select * from " + Environment.NewLine +
                          "  ( " + Environment.NewLine +
                          "      --Összegyűjtjük a megrednelésekben szereplő NODE-ID-ket " + Environment.NewLine +
                          "      select NOD_FROM.ID as NOD_ID_FROM, NOD_TO.ID as NOD_ID_TO " + Environment.NewLine +
                          "      from (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                          "          union " + Environment.NewLine +
                          "          select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                          "          inner join ORD_ORDER ORD on ORD.DEP_ID = DEP.ID and ORD_DATE >= ? and ORD_DATE <= ? " + Environment.NewLine +
                          "          ) NOD_FROM  " + Environment.NewLine +
                          "      inner join (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                          "          union " + Environment.NewLine +
                          "          select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                          "          inner join ORD_ORDER ORD on ORD.DEP_ID = DEP.ID and ORD_DATE >= ? and ORD_DATE <= ? " + Environment.NewLine +
                          "          ) NOD_TO on NOD_TO.ID <> NOD_FROM.ID " + Environment.NewLine +
                          "      where NOD_FROM.ID <> 0 and  NOD_TO.ID <> 0 " + Environment.NewLine +
                          "  )ALLNODES, " + Environment.NewLine +
                          "  --Hozzárakjuk a rendszerben előforduló, járművekhez rendelt összes övezet-lista kiosztást  " + Environment.NewLine +
                          "  (select distinct  " + Environment.NewLine +
                          "    isnull( stuff(  " + Environment.NewLine +
                          "    (  " + Environment.NewLine +
                          "        select ',' + convert( varchar(MAX), TRZX.RZN_ID )  " + Environment.NewLine +
                          "        from TRZ_TRUCKRESTRZONE TRZX  " + Environment.NewLine +
                          "        where TRZX.TRK_ID = TRK.ID " + Environment.NewLine +
                          "        order by TRZX.RZN_ID   " + Environment.NewLine +
                          "        FOR XML PATH('')  " + Environment.NewLine +
                          "    ), 1, 1, ''), '') as RESTZONES  " + Environment.NewLine +
                          "    from TRK_TRUCK TRK " + Environment.NewLine +
                          "    where TRK.TRK_DELETED = 0  " + Environment.NewLine +
                          "  ) ALLRSTZ " + Environment.NewLine +
                          "  --kivonjuk a létező távolságokat  " + Environment.NewLine +
                          "  EXCEPT  " + Environment.NewLine +
                          "  select DST.NOD_ID_FROM as NOD_ID_FROM, DST.NOD_ID_TO as NOD_ID_TO, isnull(DST.RZN_ID_LIST, '') as RESTZONES from DST_DISTANCE DST  " + Environment.NewLine +
                          "  order by 1,2,3";


            DataTable dt = DBA.Query2DataTable(sSQL, p_ORD_DATE_S, p_ORD_DATE_E, p_ORD_DATE_S, p_ORD_DATE_E);
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES")
                    }).ToList();

        }

        /// <summary>
        /// Egy összes behajtási zónát használó hiányzó távolságok lekérése
        /// </summary>
        /// <param name="p_maxRecCount"></param>
        /// <returns></returns>
        public List<boRoute> GetDistancelessNodesForAllZones( int p_maxRecCount)
        {
            string sSQL = "select top "+ p_maxRecCount.ToString() +" * from ( select * from " + Environment.NewLine +
                          "  ( " + Environment.NewLine +
                          "      --összes használt NODE-ID  " + Environment.NewLine +
                          "      select NOD_FROM.ID as NOD_ID_FROM, NOD_TO.ID as NOD_ID_TO " + Environment.NewLine +
                          "      from (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                          "          union " + Environment.NewLine +
                          "          select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                          "          ) NOD_FROM  " + Environment.NewLine +
                          "      inner join (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                          "          union " + Environment.NewLine +
                          "          select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                          "          ) NOD_TO on NOD_TO.ID <> NOD_FROM.ID " + Environment.NewLine +
                          "      where NOD_FROM.ID <> 0 and  NOD_TO.ID <> 0 " + Environment.NewLine +
                          "  )ALLNODES, " + Environment.NewLine +
                          "  --Hozzárakjuk a összes behajtási zónát " + Environment.NewLine +
                          "  (select distinct  " + Environment.NewLine +
                          "     isnull( stuff( (  select ',' + convert( varchar(MAX), RZN.ID )  " + Environment.NewLine +
                          "         from RZN_RESTRZONE RZN  " + Environment.NewLine +
                          "         order by RZN.ID   " + Environment.NewLine +
                          "         FOR XML PATH('')  " + Environment.NewLine +
                          "  ), 1, 1, ''), '') as RESTZONES  " + Environment.NewLine +
                          "  ) ALLRSTZ " + Environment.NewLine +
                          "  --kivonjuk a létező távolságokat  " + Environment.NewLine +
                          "  EXCEPT  " + Environment.NewLine +
                          "  select DST.NOD_ID_FROM as NOD_ID_FROM, DST.NOD_ID_TO as NOD_ID_TO, isnull(DST.RZN_ID_LIST, '') as RESTZONES from DST_DISTANCE DST  " + Environment.NewLine +
                          ") topRecs " + Environment.NewLine +
                          "  order by 1,2,3";

            DataTable dt = DBA.Query2DataTable(sSQL);
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES")
                    }).ToList();

        }
            
        /// <summary>
        /// Egy lerakó ID lista hiányzó, összes behajtási zónát használó távolságainak lekérése
        /// 
        /// </summary>
        /// <param name="p_lstDEP_ID"></param>
        /// <returns></returns>
        public List<boRoute> GetDistancelessNodesForAllZonesByDepList( List<int> p_lstDEP_ID)
        {
            string sDepIDList = string.Join(",", p_lstDEP_ID.Select(x => x.ToString()).ToArray());

            string sSQL = " select  * from  " + Environment.NewLine +
                          " (select NOD_FROM.ID as NOD_ID_FROM, NOD_TO.ID as NOD_ID_TO    " + Environment.NewLine +
                          "  from ( select distinct NOD_ID as ID from DEP_DEPOT DEP  where DEP.ID in ({0}) " + Environment.NewLine +
                          " ) NOD_FROM  " + Environment.NewLine +
                          " inner join (select distinct NOD_ID as ID from DEP_DEPOT DEP  where DEP.ID in ({1}) " + Environment.NewLine +
                          " ) NOD_TO on NOD_TO.ID != NOD_FROM.ID and NOD_TO.ID > 0 and NOD_FROM.ID > 0 " + Environment.NewLine +
                          ") ALLNODES,          " + Environment.NewLine +
                          "(select distinct     " + Environment.NewLine +
                          "isnull(stuff(        " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "select ',' + convert( varchar(MAX), RZN.ID )  " + Environment.NewLine +
                          "from RZN_RESTRZONE RZN   " + Environment.NewLine +
                          "order by RZN.ID          " + Environment.NewLine +
                          "FOR XML PATH('')         " + Environment.NewLine +
                          "), 1, 1, ''), '')  as RESTZONES  " + Environment.NewLine +
                          ") ALLRSTZ   " + Environment.NewLine +
                          "--kivonjuk a létező távolságokat " + Environment.NewLine +
                          "EXCEPT " + Environment.NewLine +
                          "select DST.NOD_ID_FROM as NOD_ID_FROM, DST.NOD_ID_TO as NOD_ID_TO, isnull(DST.RZN_ID_LIST, '') as RESTZONES from DST_DISTANCE DST";
            
            DataTable dt = DBA.Query2DataTable(String.Format( sSQL, sDepIDList, sDepIDList));
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES")
                    }).ToList();



        }

        public  void WriteOneRoute(boRoute p_Route)
        {
            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {
                    DBA.ExecuteNonQuery("delete from DST_DISTANCE where RZN_ID_LIST = ? and NOD_ID_FROM=? and NOD_ID_TO=?", p_Route.RZN_ID_LIST, p_Route.NOD_ID_FROM, p_Route.NOD_ID_TO);
                    String sSql = "insert into DST_DISTANCE ( RZN_ID_LIST, NOD_ID_FROM, NOD_ID_TO, DST_DISTANCE, DST_EDGES, DST_POINTS) VALUES(?, ?, ?, ?, ?, ?)";
                        byte[] bEdges = null;
                        byte[] bPoints = null;

                        if (p_Route.Edges != null && p_Route.Route.Points != null)
                        {
                            bEdges = Util.ZipStr(getEgesFromEdgeList(p_Route.Edges));
                            bPoints = Util.ZipStr(getPointsFromPointList(p_Route.Route.Points));
                        }
                        else
                        {
                            bEdges = new byte[0];
                            bPoints = new byte[0];

                        }

                        DBA.ExecuteNonQuery(sSql,
                            p_Route.RZN_ID_LIST,
                            p_Route.NOD_ID_FROM,
                            p_Route.NOD_ID_TO,
                            p_Route.DST_DISTANCE,
                            bEdges,
                            bPoints
                        );
                    }
                

                catch (Exception e)
                {
                    DBA.Rollback();
                    throw e;
                }

                finally
                {
                }
            }
        }

        private  string getEgesFromEdgeList(List<boEdge> p_Edges)
        {
            return string.Join(Global.SEP_EDGE, p_Edges.Select(x => (x.ID).ToString()).ToArray());
        }

        private  string getPointsFromPointList(List<PointLatLng> p_Points)
        {
            return string.Join(Global.SEP_POINT, p_Points.Select(x => x.Lat.ToString() + Global.SEP_COORD + x.Lng.ToString()).ToArray());
        }


        public boEdge GetEdgeByNOD_ID(int p_NOD_ID)
        {

            string sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                   "select EDG.ID as EDGID, EDG.NOD_NUM, EDG.NOD_NUM2, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, EDG.EDG_LENGTH, " + Environment.NewLine +
                   "EDG.EDG_ONEWAY, EDG.EDG_DESTTRAFFIC, EDG.RDT_VALUE, EDG.EDG_ETLCODE, RZN.RZN_ZONENAME from EDG_EDGE  EDG " + Environment.NewLine +
                   "left outer join RZN_RESTRZONE RZN on RZN.RZN_ZoneCode = EDG.RZN_ZONECODE " + Environment.NewLine +
                   " where EDG.NOD_NUM = ? or EDG.NOD_NUM2 = ? ";

            DataTable dt = DBA.Query2DataTable(sSql, p_NOD_ID, p_NOD_ID);
            if (dt.Rows.Count > 0)
            {
                boEdge res = new boEdge
                             {
                                 ID = Util.getFieldValue<int>(dt.Rows[0], "EDGID"),
                                 NOD_ID_FROM = Util.getFieldValue<int>(dt.Rows[0], "NOD_NUM"),
                                 NOD_ID_TO = Util.getFieldValue<int>(dt.Rows[0], "NOD_NUM2"),
                                 EDG_NAME = Util.getFieldValue<string>(dt.Rows[0], "EDG_NAME") != "" ? Util.getFieldValue<string>(dt.Rows[0], "EDG_NAME") : "*** nincs név ***",
                                 EDG_LENGTH = Util.getFieldValue<int>(dt.Rows[0], "EDG_LENGTH"),
                                 RDT_VALUE = Util.getFieldValue<int>(dt.Rows[0], "RDT_VALUE"),
                                 WZONE = Util.getFieldValue<string>(dt.Rows[0], "RZN_ZONENAME"),
                                 EDG_ONEWAY = Util.getFieldValue<bool>(dt.Rows[0], "EDG_ONEWAY"),
                                 EDG_DESTTRAFFIC = Util.getFieldValue<bool>(dt.Rows[0], "EDG_DESTTRAFFIC"),
                                 EDG_ETLCODE = Util.getFieldValue<string>(dt.Rows[0], "EDG_ETLCODE"),
                                 Tolls = PMapCommonVars.Instance.LstEToll.Where(i => i.ETL_CODE == Util.getFieldValue<string>(dt.Rows[0], "EDG_ETLCODE"))
                                        .DefaultIfEmpty(new boEtoll()).First().TollsToDict()

                             };
                return res;
            }
            else
                return null;
        }

        public boNode GetNode(int p_NOD_ID)
        {

            string sSql = "select NOD.ID as NODID, ZIP.ID as ZIP_ID, * from NOD_NODE NOD left outer join ZIP_ZIPCODE ZIP on ZIP.ZIP_NUM = nod.ZIP_NUM" + Environment.NewLine +
                   " where NOD.ID = ? ";

            DataTable dt = DBA.Query2DataTable(sSql, p_NOD_ID);
            if (dt.Rows.Count > 0)
            {
                boNode res = new boNode
                {
                    ID = Util.getFieldValue<int>(dt.Rows[0], "NODID"),
                    NOD_NUM = Util.getFieldValue<int>(dt.Rows[0], "NOD_NUM"),
                    NOD_NAME = Util.getFieldValue<string>(dt.Rows[0], "NOD_NAME"),
                    ZIP_ID = Util.getFieldValue<int>(dt.Rows[0], "ZIP_ID"),
                    ZIP_NUM = Util.getFieldValue<int>(dt.Rows[0], "ZIP_NUM"),
                    NOD_XPOS = Util.getFieldValue<double>(dt.Rows[0], "NOD_XPOS"),
                    NOD_YPOS = Util.getFieldValue<double>(dt.Rows[0], "NOD_YPOS"),
                    NOD_DELETED = Util.getFieldValue<bool>(dt.Rows[0], "NOD_DELETED"),
                    LASTDATE = Util.getFieldValue<DateTime>(dt.Rows[0], "LASTDATE")

                };
                return res;
            }
            else
                return null;
        }


        public int GetNearestNOD_ID(PointLatLng p_pt)
        {
            string sSql = "select top 1 ID, ZIP_NUM, abs( NOD_YPOS-?) as YD, abs( NOD_XPOS-?) as XD from NOD_NODE " +
                "order by abs( NOD_YPOS-?) + abs( NOD_XPOS-?) asc";
            DataTable dt = DBA.Query2DataTable(sSql, p_pt.Lat * Global.LatLngDivider, p_pt.Lng * Global.LatLngDivider,
                  p_pt.Lat * Global.LatLngDivider, p_pt.Lng * Global.LatLngDivider);
            if (dt.Rows.Count > 0)
            {
                return Util.getFieldValue<int>(dt.Rows[0], "ID");
            }
            return 0;
        }

        public bool GeocodingByAddr(string p_addr, out int ZIP_ID, out int NOD_ID, out int EDG_ID, out boDepot.EIMPADDRSTAT DEP_IMPADDRSTAT)
        {
            string sZIP_NUM = "";
            string sCity = "";
            string sStreet = "";
            string sStreetType = "";
            int nAddrNum = 0;
            string[] parts = p_addr.Split(' ');
            int nCurrPart = 0;

            //irányítószám-e?
            if (parts.Length > nCurrPart)
            {
                int nZIP_NUM = 0;
                if (int.TryParse(parts[nCurrPart], out nZIP_NUM))
                {
                    sZIP_NUM = parts[nCurrPart];
                    nCurrPart++;
                }
            }

            //településnév keresése
            if (parts.Length > nCurrPart)
            {
                sCity = parts[nCurrPart];
                nCurrPart++;
            }

            //utca keresése
            if (parts.Length > nCurrPart)
            {
                sStreet = parts[nCurrPart];
                nCurrPart++;
            }

            //közterület típus (nem vesz részt a keresésben)
            if (parts.Length > nCurrPart)
            {
                sStreetType = parts[nCurrPart];
                nCurrPart++;
            }

            //Házszám keresése
            if (parts.Length > nCurrPart)
            {
                int.TryParse(parts[nCurrPart], out nAddrNum);
                nCurrPart++;
            }


            SqlCommand sqlCmd;
            sqlCmd = DBA.Conn.CreateCommand();
            sqlCmd.Parameters.Clear();

            string sWhereAddr = "";
            string sWhereZipNum = "";
            string sWhereAddrNum = "";

            if (sCity != "")
            {
                if (sWhereAddr != "")
                    sWhereAddr += " and ";
                sWhereAddr += " upper(ZIP_CITY) + '.' like @ZIP_CITY ";
                DbParameter par = sqlCmd.CreateParameter();
                par.ParameterName = "@ZIP_CITY";
                par.Value = "%" + sCity.ToUpper() + "[^A-Z]%";
                sqlCmd.Parameters.Add(par);
            }

            if (sStreet != "")
            {
                if (sWhereAddr != "")
                    sWhereAddr += " and ";
                sWhereAddr += " upper(EDG_NAMEX) + '.' like @EDG_NAME ";
                DbParameter par = sqlCmd.CreateParameter();
                par.ParameterName = "@EDG_NAME";
                par.Value = sStreet.ToUpper() + "[^A-Z]%";
                sqlCmd.Parameters.Add(par);
            }

            if (sZIP_NUM != "")
            {
                if (sWhereAddr != "")
                    sWhereZipNum += " and ";

                sWhereZipNum += " ( ZIP_NUM= @ZIP_NUM) ";

                DbParameter par = sqlCmd.CreateParameter();
                par.ParameterName = "@ZIP_NUM";
                par.Value = sZIP_NUM.ToUpper();
                sqlCmd.Parameters.Add(par);
            }

            if (nAddrNum > 0)
            {
                if (sWhereAddr != "")
                    sWhereAddrNum += " and ";
                sWhereAddrNum += " ((EDG_STRNUM1 <= @STR_NUM and EDG_STRNUM2 >= @STR_NUM) or (EDG_STRNUM3 <= @STR_NUM and EDG_STRNUM4 >= @STR_NUM))";
                DbParameter par = sqlCmd.CreateParameter();
                par.ParameterName = "@STR_NUM";
                par.Value = nAddrNum;
                sqlCmd.Parameters.Add(par);
            }

//                          "  convert(varchar(max),decryptbykey(EDG_NAME_ENC)) collate SQL_Latin1_General_CP1253_CI_AI as EDG_NAMEX, " + Environment.NewLine +
//                          "  convert(varchar(max),decryptbykey(EDG_NAME_ENC)) collate SQL_Latin1_General_CP1253_CI_AI as EDG_NAMEX, " + Environment.NewLine +
            string sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                          "select  * from ( " + Environment.NewLine +
                          "  select NOD.ID as NOD_ID, EDG.ID as EDG_ID, NOD.ZIP_NUM, ZIP.ID as ZIP_ID, ZIP_CITY, " + Environment.NewLine +
                          "  convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAMEX, " + Environment.NewLine +
                          "  EDG_STRNUM1, EDG_STRNUM2, EDG_STRNUM3, EDG_STRNUM4 " + Environment.NewLine +
                          "  from NOD_NODE NOD " + Environment.NewLine +
                          "  inner join EDG_EDGE EDG on EDG.NOD_NUM = NOD.ID " + Environment.NewLine +
                          "  inner join ZIP_ZIPCODE ZIP on ZIP.ZIP_NUM = NOD.ZIP_NUM " + Environment.NewLine +
                          " UNION " + Environment.NewLine +
                          "  select NOD.ID as NOD_ID, EDG.ID as EDG_ID, NOD.ZIP_NUM, ZIP.ID as ZIP_ID, ZIP_CITY, " + Environment.NewLine +
                          "  convert(varchar(max),decryptbykey(EDG_NAME_ENC))  as EDG_NAMEX, " + Environment.NewLine +
                          "  EDG_STRNUM1, EDG_STRNUM2, EDG_STRNUM3, EDG_STRNUM4 " + Environment.NewLine +
                          "  from NOD_NODE NOD " + Environment.NewLine +
                          "  inner join EDG_EDGE EDG on EDG.NOD_NUM2 = NOD.ID " + Environment.NewLine +
                          "  inner join ZIP_ZIPCODE ZIP on ZIP.ZIP_NUM = NOD.ZIP_NUM " + Environment.NewLine +
                          ") cc  ";
            sSql = sSql.Replace("'***************'", "'FormClosedEventArgs01'");

            //Teljes címre keresés
            sqlCmd.CommandText =  sSql + (sWhereAddr != "" || sWhereZipNum != "" || sWhereAddrNum != "" ? " where " + sWhereAddr + sWhereZipNum +  sWhereAddrNum : "");

            DBA.DA.SelectCommand = sqlCmd;
            DataSet d = new DataSet();
            DBA.DA.Fill(d);
            DataTable dt = d.Tables[0];
            if (dt.Rows.Count > 0)
            {

                ZIP_ID = Util.getFieldValue<int>(dt.Rows[0], "ZIP_ID");
                NOD_ID = Util.getFieldValue<int>(dt.Rows[0], "NOD_ID");
                EDG_ID = Util.getFieldValue<int>(dt.Rows[0], "EDG_ID");
                DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.AUTOADDR_FULL;
                return true;
            }
            else
            {

                //keresés házszám nélkül
                sqlCmd.CommandText = sSql + (sWhereAddr != "" || sWhereZipNum != "" ? " where " + sWhereAddr + sWhereZipNum : "");
                DBA.DA.SelectCommand = sqlCmd;
                DataSet d2 = new DataSet();
                DBA.DA.Fill(d2);
                DataTable dt2 = d2.Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    ZIP_ID = Util.getFieldValue<int>(dt2.Rows[0], "ZIP_ID");
                    NOD_ID = Util.getFieldValue<int>(dt2.Rows[0], "NOD_ID");
                    EDG_ID = Util.getFieldValue<int>(dt2.Rows[0], "EDG_ID");
                    DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.AUTOADDR_WITHOUT_HNUM;
                    return true;
                }
                else
                {
                    //keresés irányítószám és házszám nélkül
                    //Ha van irányítószám, akkor az ahhoz legközelebb lévőt vesszük
                    sqlCmd.CommandText = sSql + (sWhereAddr != "" ? " where " + sWhereAddr : "") + (sZIP_NUM != "" ? " order by ABS( cc.ZIP_NUM - " + sZIP_NUM + ") " : "");
                    DBA.DA.SelectCommand = sqlCmd;
                    DataSet d3 = new DataSet();
                    DBA.DA.Fill(d3);
                    DataTable dt3 =  d3.Tables[0];
                    string sDB_ZIP_NUM = "";

                    if (dt3.Rows.Count > 0)
                         sDB_ZIP_NUM =  ("0000" + Util.getFieldValue<int>(dt3.Rows[0], "ZIP_NUM").ToString()).RightString( 4);


                    if (dt3.Rows.Count > 0 && (sZIP_NUM == "" ||
                        (sDB_ZIP_NUM.Substring(0, 1) == "1" && sDB_ZIP_NUM.Substring(0, 1) == sZIP_NUM.Substring(0, 1)) ||
                        (sDB_ZIP_NUM.Substring(0, 1) != "1" &&  sDB_ZIP_NUM.Substring(0, 3) == sZIP_NUM.Substring(0, 3))))
                    {
                        //Megadott irányítószám esetén csak akkor vesszük megtaláltnak a pontot, ha az átadott és a megtalált pont irányítószáma 
                        //ugyan abba a körzetbe esik
                        ZIP_ID = Util.getFieldValue<int>(dt3.Rows[0], "ZIP_ID");
                        NOD_ID = Util.getFieldValue<int>(dt3.Rows[0], "NOD_ID");
                        EDG_ID = Util.getFieldValue<int>(dt3.Rows[0], "EDG_ID");
                        DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.AUTOADDR_WITHOUT_ZIP_HNUM;
                        return true;
                    }
                    else
                    {
                        //utolsó lehetőség a google-hoz fordulni.
                        if (PMapIniParams.Instance.GeocodingByGoogle && GeocodingByGoogle(p_addr, out ZIP_ID, out NOD_ID, out EDG_ID))
                        {
                            DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.AUTOADDR_GOOGLE;
                            return true;
                        }

                        if (PMapIniParams.Instance.GeocodingByGoogle && sCity != "" && GeocodingByGoogle(sCity, out ZIP_ID, out NOD_ID, out EDG_ID))
                        {
                            DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.AUTOADDR_GOOGLE_ONLYCITY;
                            return true;
                        }
                        //nincs eredmény...
                        ZIP_ID = 0;
                        NOD_ID = 0;
                        EDG_ID = 0;
                        DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.MISSADDR;
                        return false;
                    }
                }
            }
        }


        /*****/
        public bool GeocodingByGoogle(string p_addr, out int ZIP_ID, out int NOD_ID, out int EDG_ID)
        {
            ZIP_ID = 0;
            NOD_ID = 0;
            EDG_ID = 0;
            PointLatLng ResultPt = new PointLatLng();
            string requestUri;
            if (PMapIniParams.Instance.GoogleMapsAPIKey != "")
            {
                requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}&sensor=false",
                    Uri.EscapeDataString(p_addr), PMapIniParams.Instance.GoogleMapsAPIKey);
            }
            else
            {
                requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false",
                Uri.EscapeDataString(p_addr));
            }

            var request = WebRequest.Create(requestUri);

            if (PMapIniParams.Instance.UseProxy)
            {
                request.Proxy = new WebProxy(PMapIniParams.Instance.ProxyServer, PMapIniParams.Instance.ProxyPort);
                request.Proxy.Credentials = new NetworkCredential(PMapIniParams.Instance.ProxyUser, PMapIniParams.Instance.ProxyPassword, PMapIniParams.Instance.ProxyDomain);
            }


            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var xElement = xdoc.Element("GeocodeResponse");
            if (xElement != null )
            {
                if (xElement.Element("status").Value == "OK")
                {
                    var result = xElement.Element("result");
                    if (result != null)
                    {
                        var element = result.Element("geometry");
                        if (element != null)
                        {
                            var locationElement = element.Element("location");
                            if (locationElement != null)
                            {
                                var xElementLat = locationElement.Element("lat");
                                if (xElementLat != null)
                                    ResultPt.Lat = Convert.ToDouble(xElementLat.Value.Replace(',', '.'), CultureInfo.InvariantCulture);

                                var xElementLng = locationElement.Element("lng");
                                if (xElementLng != null)
                                    ResultPt.Lng = Convert.ToDouble(xElementLng.Value.Replace(',', '.'), CultureInfo.InvariantCulture);
                            }
                        }
                    }
                }
                else
                {
                    Util.Log2File("Google geocoding error:" + xElement.ToString());
                }

            }
            if (ResultPt.Lat != 0 && ResultPt.Lng != 0)
            {
                NOD_ID = GetNearestNOD_ID(ResultPt);
                boNode nod = GetNode(NOD_ID);
                if (nod == null)
                    return false;
                ZIP_ID = nod.ZIP_ID;

                boEdge edg = GetEdgeByNOD_ID(NOD_ID);
                if (edg == null)
                    return false;
         
                EDG_ID = edg.ID;
                return true;
            }
            else
            {
                return false;
            }
        }


        public Dictionary<string, Dictionary<string, double>> GetAllTolls()
        {
            Dictionary<string, Dictionary<string, double>> dicAllTolls = new Dictionary<string, Dictionary<string, double>>();

            string sSql = "select * from ETL_ETOLL ETL order by ETL.ETL_CODE";
            DataTable dte = DBA.Query2DataTable(sSql);
            foreach (DataRow dre in dte.Rows)
            {
                Dictionary<string, double> tolls = new Dictionary<string, double>();
                tolls.Add("J2", Util.getFieldValue<double>(dre, "ETL_J2_TOLL_FULL"));
                tolls.Add("J3", Util.getFieldValue<double>(dre, "ETL_J2_TOLL_FULL"));
                tolls.Add("J4", Util.getFieldValue<double>(dre, "ETL_J2_TOLL_FULL"));
                dicAllTolls.Add(Util.getFieldValue<string>(dre, "ETL_CODE"), tolls);

            }
            //díjnélküli tétel
            Dictionary<string, double> tollsX = new Dictionary<string, double>();
            tollsX.Add("J2", 0);
            tollsX.Add("J3", 0);
            tollsX.Add("J4", 0);
            dicAllTolls.Add("", tollsX);

            return dicAllTolls;
        }
    }
}
