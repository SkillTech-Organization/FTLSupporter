﻿using System;
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
using System.Text.RegularExpressions;
using FastMember;
using PMap.Cache;

namespace PMap.BLL
{
    public class bllRoute : bllBase
    {


        private bllPlan m_bllPlan;
        public bllRoute(SQLServerAccess p_DBA)
            : base(p_DBA, "")
        {
            m_bllPlan = new bllPlan(p_DBA);

        }

        /* multithread-os környezetből hívható rutinok */
        public int GetMaxNodeID()
        {
            DataTable dtx = DBA.Query2DataTable("select max(ID) as MAXID from NOD_NODE");
            return Util.getFieldValue<int>(dtx.Rows[0], "MAXID");
        }

        public PointLatLng GetPointLatLng(int p_NOD_ID)
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

        public DataTable GetEdgesToDT()
        {
            String sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                         "select EDG.ID, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, EDG.EDG_LENGTH, EDG.RDT_VALUE ,EDG.EDG_ETLCODE, EDG.EDG_ONEWAY, " + Environment.NewLine +
                         "EDG.EDG_DESTTRAFFIC, EDG.NOD_NUM, EDG.NOD_NUM2, EDG.RZN_ZONECODE,EDG_STRNUM1, EDG_STRNUM2, EDG_STRNUM3, EDG_STRNUM4,  " + Environment.NewLine +
                         "NOD1.NOD_YPOS as NOD1_YPOS, NOD1.NOD_XPOS as NOD1_XPOS, " + Environment.NewLine +
                         "NOD2.NOD_YPOS as NOD2_YPOS, NOD2.NOD_XPOS as NOD2_XPOS, RZN.ID as RZN_ID, RZN.RST_ID, RZN.RZN_ZoneName, NOD1.ZIP_NUM as ZIP_NUM_FROM, NOD2.ZIP_NUM as ZIP_NUM_TO, " + Environment.NewLine +
                         "EDG_MAXWEIGHT, EDG_MAXHEIGHT, EDG_MAXWIDTH " + Environment.NewLine +
                         "from EDG_EDGE (NOLOCK) EDG " + Environment.NewLine +
                         "inner join NOD_NODE (NOLOCK) NOD1 on NOD1.ID = EDG.NOD_NUM " + Environment.NewLine +
                         "inner join NOD_NODE (NOLOCK) NOD2 on NOD2.ID = EDG.NOD_NUM2 " + Environment.NewLine +
                         "left outer join RZN_RESTRZONE (NOLOCK) RZN on EDG.RZN_ZONECODE = RZN.RZN_ZoneCode " + Environment.NewLine +
                         "where EDG.NOD_NUM <> EDG.NOD_NUM2 and RDT_VALUE <> 0 " + Environment.NewLine +
                         "order by ID " ; //Meg kell rendezni, hogy a duplikátumok közül csak az elsőt vegyük minden esetbe figyelembe

            return DBA.Query2DataTable(sSql);
        }


        public DataTable GetNodestoDT(String p_NodeList)
        {
            return DBA.Query2DataTable("select * from NOD_NODE where ID in (" + p_NodeList + ")");
        }



        public void WriteRoutes(List<boRoute> p_Routes, bool p_savePoints)
        {
            //           using (DBLockHolder lockObj = new DBLockHolder(DBA))
            {

                using (TransactionBlock transObj = new TransactionBlock(DBA))
                {
                    try
                    {
                        DateTime dtStart = DateTime.Now;

                        SqlCommand command = new SqlCommand(null, DBA.Conn);
                        command.CommandText = "insert into DST_DISTANCE ( NOD_ID_FROM, NOD_ID_TO, RZN_ID_LIST, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH, DST_DISTANCE, DST_EDGES, DST_POINTS) VALUES(@NOD_ID_FROM, @NOD_ID_TO, @RZN_ID_LIST, @DST_MAXWEIGHT, @DST_MAXHEIGHT, @DST_MAXWIDTH, @DST_DISTANCE, @DST_EDGES, @DST_POINTS)";

                        command.Parameters.Add(new SqlParameter("@NOD_ID_FROM", SqlDbType.Int, 0));
                        command.Parameters.Add(new SqlParameter("@NOD_ID_TO", SqlDbType.Int, 0));
                        command.Parameters.Add(new SqlParameter("@RZN_ID_LIST", SqlDbType.VarChar, Int32.MaxValue));
                        command.Parameters.Add(new SqlParameter("@DST_MAXWEIGHT", SqlDbType.Float, 0));
                        command.Parameters.Add(new SqlParameter("@DST_MAXHEIGHT", SqlDbType.Float, 0));
                        command.Parameters.Add(new SqlParameter("@DST_MAXWIDTH", SqlDbType.Float, 0));
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
                            command.Parameters["@DST_MAXWEIGHT"].Value = route.DST_MAXWEIGHT;
                            command.Parameters["@DST_MAXHEIGHT"].Value = route.DST_MAXHEIGHT;
                            command.Parameters["@DST_MAXWIDTH"].Value = route.DST_MAXWIDTH;

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
                        throw;
                    }

                    finally
                    {
                    }

                }
            }
        }

        private class boRouteX : boRoute
        {
            bool m_savePoints = true;
            public boRouteX(bool p_savePoints)
            {
                m_savePoints = p_savePoints;
            }

            public byte[] DST_EDGES
            {
                get
                {
                    if (Edges != null && Route != null)
                    {
                        return Util.ZipStr(string.Join(Global.SEP_EDGE, Edges.Select(x => (x.ID).ToString()).ToArray()));
                    }
                    return new byte[0];
                }
            }
            public byte[] DST_POINTS
            {
                get
                {
                    if (Edges != null && Route != null)
                    {
                        if (m_savePoints)
                            return Util.ZipStr(string.Join(Global.SEP_POINT, Route.Points.Select(x => x.Lat.ToString() + Global.SEP_COORD + x.Lng.ToString()).ToArray()));
                        else
                            return new byte[0];
                    }
                    return new byte[0];
                }
            }
        }

        public void WriteRoutesBulk(List<boRoute> p_Routes, bool p_savePoints)
        {
            if( p_Routes.Count() == 0)
		return;

            DataTable dt;
            DataTable table = new DataTable();

            List<boRouteX> rtX = p_Routes.Select(i => new boRouteX(p_savePoints)
            {
                NOD_ID_FROM = i.NOD_ID_FROM,
                NOD_ID_TO = i.NOD_ID_TO,
                RZN_ID_LIST = i.RZN_ID_LIST,
                DST_MAXWEIGHT = i.DST_MAXWEIGHT,
                DST_MAXHEIGHT = i.DST_MAXHEIGHT,
                DST_MAXWIDTH = i.DST_MAXWIDTH,
                DST_DISTANCE = i.DST_DISTANCE,
                Route = i.Route,
                Edges = i.Edges

            }
            ).ToList();



            using (var reader = ObjectReader.Create(rtX,
                "NOD_ID_FROM", "NOD_ID_TO", "RZN_ID_LIST", "DST_MAXWEIGHT", "DST_MAXHEIGHT", "DST_MAXWIDTH", "DST_DISTANCE", "DST_EDGES", "DST_POINTS"))
            {
                table.Load(reader);
            }
            // more on triggers in next post
            SqlBulkCopy bulkCopy =
                new SqlBulkCopy
                (
                DBA.Conn,
                SqlBulkCopyOptions.TableLock,
                null
                );
            bulkCopy.BulkCopyTimeout = PMapIniParams.Instance.DBCmdTimeOut;
            // set the destination table name
            bulkCopy.DestinationTableName = "DST_DISTANCE";
            bulkCopy.ColumnMappings.Add("NOD_ID_FROM", "NOD_ID_FROM");
            bulkCopy.ColumnMappings.Add("NOD_ID_TO", "NOD_ID_TO");
            bulkCopy.ColumnMappings.Add("RZN_ID_LIST", "RZN_ID_LIST");
            bulkCopy.ColumnMappings.Add("DST_MAXWEIGHT", "DST_MAXWEIGHT");
            bulkCopy.ColumnMappings.Add("DST_MAXHEIGHT", "DST_MAXHEIGHT");
            bulkCopy.ColumnMappings.Add("DST_MAXWIDTH", "DST_MAXWIDTH");
            bulkCopy.ColumnMappings.Add("DST_DISTANCE", "DST_DISTANCE");
            bulkCopy.ColumnMappings.Add("DST_EDGES", "DST_EDGES");
            bulkCopy.ColumnMappings.Add("DST_POINTS", "DST_POINTS");

            // write the data in the "dataTable"
            bulkCopy.WriteToServer(table, DataRowState.Unchanged);

        }
    




        public boRoute GetRouteFromDB( int p_NOD_ID_FROM, int p_NOD_ID_TO, string p_RZN_ID_LIST, int p_Weight, int p_Height, int p_Width)
        {
            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            boRoute result = null;
            using (LogForRouteCache lockObj = new LogForRouteCache(RouteCache.Locker))
            {
                result = RouteCache.Instance.Items.Where(w => w.NOD_ID_FROM == p_NOD_ID_FROM &&
                                    w.NOD_ID_TO == p_NOD_ID_TO &&
                                     w.DST_MAXWEIGHT == p_Weight &&
                                      w.DST_MAXHEIGHT == p_Height &&
                                      w.DST_MAXWIDTH == p_Width).FirstOrDefault();
            }
            if (result == null)
            {
                string sSql = "select * from DST_DISTANCE DST " + Environment.NewLine +
                               "where  NOD_ID_FROM = ? and NOD_ID_TO = ? and RZN_ID_LIST = ? and DST_MAXWEIGHT = ? and DST_MAXHEIGHT = ? and DST_MAXWIDTH = ?  ";
                DataTable dt = DBA.Query2DataTable(sSql, p_NOD_ID_FROM, p_NOD_ID_TO, p_RZN_ID_LIST, p_Weight, p_Height, p_Width);

                if (dt.Rows.Count == 1 && Util.getFieldValue<double>(dt.Rows[0], "DST_DISTANCE") >= 0.0)
                {


                    result = new boRoute();
                    result.DST_DISTANCE = Util.getFieldValue<int>(dt.Rows[0], "DST_DISTANCE");
                    result.RZN_ID_LIST = Util.getFieldValue<string>(dt.Rows[0], "RZN_ID_LIST");
                    result.DST_MAXWEIGHT = Util.getFieldValue<int>(dt.Rows[0], "DST_MAXWEIGHT");
                    result.DST_MAXHEIGHT = Util.getFieldValue<int>(dt.Rows[0], "DST_MAXHEIGHT");
                    result.DST_MAXWIDTH = Util.getFieldValue<int>(dt.Rows[0], "DST_MAXWIDTH");


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
                               "EDG.EDG_ONEWAY, EDG.EDG_DESTTRAFFIC, EDG.RDT_VALUE, EDG.EDG_ETLCODE, RZN.RZN_ZONENAME,EDG_MAXWEIGHT,EDG_MAXHEIGHT, EDG_MAXWIDTH " + Environment.NewLine +
                               " from EDG_EDGE  EDG " + Environment.NewLine +
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
                                                   .DefaultIfEmpty(new boEtoll()).First().TollsToDict(),
                                            EDG_MAXWEIGHT = Util.getFieldValue<int>(r, "EDG_MAXWEIGHT"),
                                            EDG_MAXHEIGHT = Util.getFieldValue<int>(r, "EDG_MAXHEIGHT"),
                                            EDG_MAXWIDTH = Util.getFieldValue<int>(r, "EDG_MAXWIDTH")

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

                if (result != null)
                {
                    using (LogForRouteCache lockObj = new LogForRouteCache(RouteCache.Locker))
                    {
                        RouteCache.Instance.Items.Add(result);
                    }
                }
            }
            return result;
        }


        public MapRoute GetMapRouteFromDB(int p_NOD_ID_FROM, int p_NOD_ID_TO, string p_RZN_ID_LIST, int p_Weight, int p_Height, int p_Width)
        {




            if (p_RZN_ID_LIST == null)
                p_RZN_ID_LIST = "";

            MapRoute result = null;

           using (LogForRouteCache lockObj = new LogForRouteCache(RouteCache.Locker))
            {
                var boResult = RouteCache.Instance.Items.Where(w => w.NOD_ID_FROM == p_NOD_ID_FROM &&
                                    w.NOD_ID_TO == p_NOD_ID_TO &&
                                     w.DST_MAXWEIGHT == p_Weight &&
                                      w.DST_MAXHEIGHT == p_Height &&
                                      w.DST_MAXWIDTH == p_Width).FirstOrDefault();
                if( boResult != null)
                {
                    return boResult.Route;
                }
            }


            string sSql = "select * from DST_DISTANCE DST " + Environment.NewLine +
                           "where NOD_ID_FROM = ? and NOD_ID_TO = ? and RZN_ID_LIST=? and DST_MAXWEIGHT = ? and DST_MAXHEIGHT = ? and DST_MAXWIDTH = ?";
            DataTable dt = DBA.Query2DataTable(sSql, p_NOD_ID_FROM, p_NOD_ID_TO, p_RZN_ID_LIST, p_Weight, p_Height, p_Width);

            if (dt.Rows.Count == 1)
            {

                result = new MapRoute("");

                if (Util.getFieldValue<double>(dt.Rows[0], "DST_DISTANCE") >= 0.0)
                {
                    byte[] buff = Util.getFieldValue<byte[]>(dt.Rows[0], "DST_POINTS");
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


        public DataTable GetRestZonesToDT()
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
                          "UNION  select '' as RESTZONE_IDS, '***nincs engedély***' as RESTZONE_NAMES " + Environment.NewLine +
                          "EXCEPT select '' as RESTZONE_IDS, '' as RESTZONE_NAMES ";

            return DBA.Query2DataTable(sSql);
        }


        public string GetAllRestZones()
        {
            string sRZN_ID_LIST = "";
            string sSql = "(select distinct     " + Environment.NewLine +
                          "isnull(stuff(        " + Environment.NewLine +
                          "( " + Environment.NewLine +
                          "select ',' + convert( varchar(MAX), RZN.ID )  " + Environment.NewLine +
                          "from RZN_RESTRZONE RZN   " + Environment.NewLine +
                          "order by RZN.ID          " + Environment.NewLine +
                          "FOR XML PATH('')         " + Environment.NewLine +
                          "), 1, 1, ''), '')  as RZN_ID_LIST  " + Environment.NewLine +
                          ")   ";
            DataTable dt = DBA.Query2DataTable(sSql);
            if (dt.Rows.Count > 0)
            {
                sRZN_ID_LIST = Util.getFieldValue<string>(dt.Rows[0], "RZN_ID_LIST");
            }
            return sRZN_ID_LIST;
        }

        public string GetRestZonesByRST_ID(int p_RST_ID)
        {
            string sRZN_ID_LIST = "";
            string sSql = "select  isnull( stuff ((SELECT ',' + CONVERT(varchar(MAX), ID) " + Environment.NewLine +
                          "  FROM RZN_RESTRZONE RZN " + Environment.NewLine;
            if (p_RST_ID != Global.RST_NORESTRICT)
                sSql += "  WHERE RST_ID <=? " + Environment.NewLine;
            sSql += " ORDER BY ID FOR XML PATH('')), 1, 1, ''), '') AS RZN_ID_LIST ";

            DataTable dt = DBA.Query2DataTable(sSql, p_RST_ID);
            if (dt.Rows.Count > 0)
            {
                sRZN_ID_LIST = Util.getFieldValue<string>(dt.Rows[0], "RZN_ID_LIST");
            }
            return sRZN_ID_LIST;
        }

        /// <summary>
        /// Zónakód-ID átfordító dictionary visszaadása
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetAllRZones()
        {
            Dictionary<string, int> dicRZones = new Dictionary<string, int>();

            string sSql = "select * from RZN_RESTRZONE order by RZN_ZoneCode";
            DataTable dte = DBA.Query2DataTable(sSql);
            foreach (DataRow dre in dte.Rows)
            {
                dicRZones.Add(Util.getFieldValue<string>(dre, "RZN_ZoneCode"),
                                Util.getFieldValue<int>(dre, "ID"));
            }

            return dicRZones;
        }



        public DataTable GetSpeedProfsToDT()
        {
            return DBA.Query2DataTable("select * from SPP_SPEEDPROF where SPP_DELETED = 0");
        }


        public Dictionary<int, string> GetRoadTypesToDict()
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

        public List<int> GetAllNodes()
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

           string sSQL = "; WITH CTE_TPL as (" + Environment.NewLine +
                    "select distinct " + Environment.NewLine +
                    "	  isnull(stuff(  " + Environment.NewLine +
                    "	  (  " + Environment.NewLine +
                    "		  select ',' + convert( varchar(MAX), TRZX.RZN_ID )  " + Environment.NewLine +
                    "		  from TRZ_TRUCKRESTRZONE TRZX  " + Environment.NewLine +
                    "		  where TRZX.TRK_ID = TPL.TRK_ID " + Environment.NewLine +
                    "		  order by TRZX.RZN_ID " + Environment.NewLine +
                    "		  FOR XML PATH('') " + Environment.NewLine +
                    "	  ), 1, 1, ''), '') as RESTZONES, TRK.TRK_WEIGHT, TRK.TRK_XHEIGHT, TRK.TRK_XWIDTH " + Environment.NewLine +
                    "	  from TPL_TRUCKPLAN TPL " + Environment.NewLine +
                    "	  inner join TRK_TRUCK TRK on TRK.ID = TPL.TRK_ID " + Environment.NewLine +
                    "	  where TPL.PLN_ID = ? " + Environment.NewLine +
                    ") " + Environment.NewLine +
                    "select NOD_FROM.ID as NOD_ID_FROM, NOD_TO.ID as NOD_ID_TO, CTE_TPL.RESTZONES, CTE_TPL.TRK_WEIGHT as DST_MAXWEIGHT, CTE_TPL.TRK_XHEIGHT as DST_MAXHEIGHT, CTE_TPL.TRK_XWIDTH as DST_MAXWIDTH " + Environment.NewLine +
                    "	from (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS  " + Environment.NewLine +
                    "		union  " + Environment.NewLine +
                    "		select distinct NOD_ID as ID from DEP_DEPOT DEP  " + Environment.NewLine +
                    "		inner join TOD_TOURORDER TOD on TOD.DEP_ID = DEP.ID and TOD.PLN_ID = ? " + Environment.NewLine +
                    "		) NOD_FROM  " + Environment.NewLine +
                    "inner join (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                    "	    union  " + Environment.NewLine +
                    "	    select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                    "	    inner join TOD_TOURORDER TOD on TOD.DEP_ID = DEP.ID and TOD.PLN_ID = ? " + Environment.NewLine +
                    "	    ) NOD_TO on NOD_TO.ID != NOD_FROM.ID and NOD_TO.ID > 0 and NOD_FROM.ID > 0 " + Environment.NewLine +
                    "inner join CTE_TPL on 1=1 " + Environment.NewLine +
                    "EXCEPT  " + Environment.NewLine +
                    "select DST.NOD_ID_FROM as NOD_ID_FROM, DST.NOD_ID_TO as NOD_ID_TO, isnull(DST.RZN_ID_LIST, '') as RESTZONES, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH from DST_DISTANCE DST " + Environment.NewLine +
                    "order by 1,2,3,4,5,6";




            DataTable dt = DBA.Query2DataTable(sSQL, pPLN_ID, pPLN_ID, pPLN_ID);
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES"),
                        DST_MAXWEIGHT = Util.getFieldValue<int>(row, "DST_MAXWEIGHT"),
                        DST_MAXHEIGHT = Util.getFieldValue<int>(row, "DST_MAXHEIGHT"),
                        DST_MAXWIDTH = Util.getFieldValue<int>(row, "DST_MAXWIDTH")
                    }).ToList();
        }

        /// <summary>
        /// NODE_ID-k által meghatározott téglalap
        /// </summary>
        /// <param name="p_nodes"></param>
        /// <returns></returns>
        public RectLatLng getBoundary(List<int> p_nodes)
        {
            string sNODE_IDs = string.Join(",", p_nodes.Select(i => i.ToString()).ToArray());
            string sSql = "select * from NOD_NODE where id in (" + sNODE_IDs + ")";
            DataTable dt = DBA.Query2DataTable(sSql);
            //a koordinátákat egy 'kifordított' négyzetre inicializálkuk, hogy az első 
            //tételnél biztosan kapjanak értéket
            double dLat1 = Util.getFieldValue<double>(dt.Rows[0], "NOD_YPOS") / Global.LatLngDivider;
            double dLng1 = Util.getFieldValue<double>(dt.Rows[0], "NOD_XPOS") / Global.LatLngDivider;
            double dLat2 = Util.getFieldValue<double>(dt.Rows[1], "NOD_YPOS") / Global.LatLngDivider;
            double dLng2 = Util.getFieldValue<double>(dt.Rows[1], "NOD_XPOS") / Global.LatLngDivider;
            return getBoundary(dLat1, dLng1, dLat2, dLng2);

        }
        public RectLatLng getBoundary(double dLat1, double dLng1, double dLat2, double dLng2)
        {
            //a koordinátákat egy 'kifordított' négyzetre inicializálkuk, hogy az első 
            //tételnél biztosan kapjanak értéket
            double dTop = -180;
            double dLeft = 180;
            double dBottom = 180;
            double dRight = -180;

            if (dLng1 < dLeft)
                dLeft = dLng1;
            if (dLat1 > dTop)
                dTop = dLat1;
            if (dLng1 > dRight)
                dRight = dLng1;
            if (dLat1 < dBottom)
                dBottom = dLat1;

            if (dLng2 < dLeft)
                dLeft = dLng2;
            if (dLat2 > dTop)
                dTop = dLat2;
            if (dLng2 > dRight)
                dRight = dLng2;
            if (dLat2 < dBottom)
                dBottom = dLat2;

            dLeft -= PMapIniParams.Instance.CutExtDegree;
            dTop += PMapIniParams.Instance.CutExtDegree;
            dRight += PMapIniParams.Instance.CutExtDegree;
            dBottom -= PMapIniParams.Instance.CutExtDegree;
            RectLatLng boundary = RectLatLng.FromLTRB(dLeft, dTop, dRight, dBottom);
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

            string sSQL = "; WITH CTE_TRK as (" + Environment.NewLine +
                            "select distinct " + Environment.NewLine +
                            "	  isnull(stuff(  " + Environment.NewLine +
                            "	  (  " + Environment.NewLine +
                            "		  select ',' + convert( varchar(MAX), TRZX.RZN_ID )  " + Environment.NewLine +
                            "		  from TRZ_TRUCKRESTRZONE TRZX  " + Environment.NewLine +
                            "		  where TRZX.TRK_ID = TRK.ID " + Environment.NewLine +
                            "		  order by TRZX.RZN_ID   " + Environment.NewLine +
                            "		  FOR XML PATH('')  " + Environment.NewLine +
                            "	  ), 1, 1, ''), '') as RESTZONES, TRK.TRK_WEIGHT, TRK.TRK_XHEIGHT, TRK.TRK_XWIDTH " + Environment.NewLine +
                            "	  from TRK_TRUCK TRK " + Environment.NewLine +
                            "	  where TRK_ACTIVE = 1 " + Environment.NewLine +
                            ") " + Environment.NewLine +
                            "--Összegy√jtjük a megrednelésekben szereplo NODE-ID-ket  " + Environment.NewLine +
                            "select NOD_FROM.ID as NOD_ID_FROM, NOD_TO.ID as NOD_ID_TO, CTE_TRK.RESTZONES, CTE_TRK.TRK_WEIGHT as DST_MAXWEIGHT, CTE_TRK.TRK_XHEIGHT as DST_MAXHEIGHT, CTE_TRK.TRK_XWIDTH  as DST_MAXWIDTH " + Environment.NewLine +
                            "from (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                            "union " + Environment.NewLine +
                            "select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                            "inner join ORD_ORDER ORD on ORD.DEP_ID = DEP.ID and ORD_DATE >= ? and ORD_DATE <= ? " + Environment.NewLine +
                            ") NOD_FROM  " + Environment.NewLine +
                            "inner join (select distinct NOD_ID as ID from WHS_WAREHOUSE WHS " + Environment.NewLine +
                            "union " + Environment.NewLine +
                            "select distinct NOD_ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                            "inner join ORD_ORDER ORD on ORD.DEP_ID = DEP.ID and ORD_DATE >= ? and ORD_DATE <= ? " + Environment.NewLine +
                            ") NOD_TO on NOD_TO.ID <> NOD_FROM.ID " + Environment.NewLine +
                            "inner join CTE_TRK on 1=1 " + Environment.NewLine +
                            "where NOD_FROM.ID <> 0 and  NOD_TO.ID <> 0 " + Environment.NewLine +
                            "EXCEPT  " + Environment.NewLine +
                            "select DST.NOD_ID_FROM as NOD_ID_FROM, DST.NOD_ID_TO as NOD_ID_TO, isnull(DST.RZN_ID_LIST, '') as RESTZONES, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH from DST_DISTANCE DST  " + Environment.NewLine +
                            "order by 1,2,3,4,5,6";


            DataTable dt = DBA.Query2DataTable(sSQL, p_ORD_DATE_S, p_ORD_DATE_E, p_ORD_DATE_S, p_ORD_DATE_E);
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES"),
                        DST_MAXWEIGHT = Util.getFieldValue<int>(row, "DST_MAXWEIGHT"),
                        DST_MAXHEIGHT = Util.getFieldValue<int>(row, "DST_MAXHEIGHT"),
                        DST_MAXWIDTH = Util.getFieldValue<int>(row, "DST_MAXWIDTH")
                    }).ToList();

        }

        /// <summary>
        /// Egy összes behajtási zónát használó hiányzó távolságok lekérése
        /// </summary>
        /// <param name="p_maxRecCount"></param>
        /// <returns></returns>
        public List<boRoute> GetDistancelessNodesForAllZones__ONLYFORTEST(int p_maxRecCount)
        {
            string sSQL = "select top " + p_maxRecCount.ToString() + " * from ( select * from " + Environment.NewLine +
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
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES"),
                        DST_MAXWEIGHT = Util.getFieldValue<int>(row, "DST_MAXWEIGHT"),
                        DST_MAXHEIGHT = Util.getFieldValue<int>(row, "DST_MAXHEIGHT"),
                        DST_MAXWIDTH = Util.getFieldValue<int>(row, "DST_MAXWIDTH")
                    }).ToList();

        }

        /// <summary>
        /// Egy lerakó ID lista hiányzó, összes behajtási zónát használó távolságainak lekérése
        /// 
        /// </summary>
        /// <param name="p_lstDEP_ID"></param>
        /// <returns></returns>
        public List<boRoute> GetDistancelessNodesForAllZonesByDepList__NEMKELL(List<int> p_lstDEP_ID)
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

            DataTable dt = DBA.Query2DataTable(String.Format(sSQL, sDepIDList, sDepIDList));
            return (from row in dt.AsEnumerable()
                    select new boRoute
                    {
                        NOD_ID_FROM = Util.getFieldValue<int>(row, "NOD_ID_FROM"),
                        NOD_ID_TO = Util.getFieldValue<int>(row, "NOD_ID_TO"),
                        RZN_ID_LIST = Util.getFieldValue<string>(row, "RESTZONES"),
                        DST_MAXWEIGHT = Util.getFieldValue<int>(row, "DST_MAXWEIGHT"),
                        DST_MAXHEIGHT = Util.getFieldValue<int>(row, "DST_MAXHEIGHT"),
                        DST_MAXWIDTH = Util.getFieldValue<int>(row, "DST_MAXWIDTH")
                    }).ToList();



        }

        public void WriteOneRoute(boRoute p_Route)
        {
            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {
                    DBA.ExecuteNonQuery("delete from DST_DISTANCE where RZN_ID_LIST = ? and NOD_ID_FROM=? and NOD_ID_TO=? and DST_MAXWEIGHT=? and DST_MAXHEIGHT=? and DST_MAXWIDTH=? ", p_Route.RZN_ID_LIST, p_Route.NOD_ID_FROM, p_Route.NOD_ID_TO, p_Route.DST_MAXWEIGHT, p_Route.DST_MAXHEIGHT, p_Route.DST_MAXWIDTH);
                    String sSql = "insert into DST_DISTANCE ( RZN_ID_LIST, NOD_ID_FROM, NOD_ID_TO, DST_MAXWEIGHT, DST_MAXHEIGHT, DST_MAXWIDTH, DST_DISTANCE, DST_EDGES, DST_POINTS) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
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
                        p_Route.DST_MAXWEIGHT,
                        p_Route.DST_MAXHEIGHT,
                        p_Route.DST_MAXWIDTH,
                        p_Route.DST_DISTANCE,
                        bEdges,
                        bPoints
                    );
                }


                catch (Exception e)
                {
                    DBA.Rollback();
                    throw;
                }

                finally
                {
                }
            }
        }

        private string getEgesFromEdgeList(List<boEdge> p_Edges)
        {
            return string.Join(Global.SEP_EDGE, p_Edges.Select(x => (x.ID).ToString()).ToArray());
        }

        private string getPointsFromPointList(List<PointLatLng> p_Points)
        {
            return string.Join(Global.SEP_POINT, p_Points.Select(x => x.Lat.ToString() + Global.SEP_COORD + x.Lng.ToString()).ToArray());
        }

        public boEdge GetEdgeByID(int p_ID)
        {
            string sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
              "select EDG.ID as EDGID, EDG.NOD_NUM, EDG.NOD_NUM2, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, EDG.EDG_LENGTH, " + Environment.NewLine +
              "EDG.EDG_ONEWAY, EDG.EDG_DESTTRAFFIC, EDG.RDT_VALUE, EDG.EDG_ETLCODE, RZN.RZN_ZONENAME, EDG.EDG_MAXWEIGHT, EDG.EDG_MAXHEIGHT, EDG.EDG_MAXWIDTH from EDG_EDGE  EDG " + Environment.NewLine +
              "left outer join RZN_RESTRZONE RZN on RZN.RZN_ZoneCode = EDG.RZN_ZONECODE " + Environment.NewLine +
              " where EDG.ID = ?  ";

            return fillEdgeFromDt( DBA.Query2DataTable(sSql, p_ID));
        }

        public boEdge GetEdgeByNOD_ID(int p_NOD_ID, string p_street = "")
        {

            string sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                   "select EDG.ID as EDGID, EDG.NOD_NUM, EDG.NOD_NUM2, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME, EDG.EDG_LENGTH, " + Environment.NewLine +
                   "EDG.EDG_ONEWAY, EDG.EDG_DESTTRAFFIC, EDG.RDT_VALUE, EDG.EDG_ETLCODE, RZN.RZN_ZONENAME, EDG.EDG_MAXWEIGHT, EDG.EDG_MAXHEIGHT, EDG.EDG_MAXWIDTH from EDG_EDGE  EDG " + Environment.NewLine +
                   "left outer join RZN_RESTRZONE RZN on RZN.RZN_ZoneCode = EDG.RZN_ZONECODE " + Environment.NewLine +
                   " where EDG.NOD_NUM = ? or EDG.NOD_NUM2 = ? ";
            if (p_street != "")
                sSql += " and UPPER(convert(varchar(max),decryptbykey(EDG_NAME_ENC))) like '%" + p_street.ToUpper() + "%' ";
            return fillEdgeFromDt(DBA.Query2DataTable(sSql, p_NOD_ID, p_NOD_ID));
        }

        private boEdge fillEdgeFromDt(DataTable dt)
        {
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
                    EDG_MAXWEIGHT = Util.getFieldValue<int>(dt.Rows[0], "EDG_MAXWEIGHT"),
                    EDG_MAXHEIGHT = Util.getFieldValue<int>(dt.Rows[0], "EDG_MAXHEIGHT"),
                    EDG_MAXWIDTH = Util.getFieldValue<int>(dt.Rows[0], "EDG_MAXWIDTH"),
                    Tolls = PMapCommonVars.Instance.LstEToll.Where(i => i.ETL_CODE == Util.getFieldValue<string>(dt.Rows[0], "EDG_ETLCODE"))
                           .DefaultIfEmpty(new boEtoll()).First().TollsToDict()

                };
                return res;
                
            }
            else
                return null;
        }
        /// <summary>
        /// NODE üzleti objektum visszaadása ID alapján
        /// </summary>
        /// <param name="p_NOD_ID"></param>
        /// <returns></returns>
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
            int diff = 0;
            return GetNearestNOD_ID(p_pt, out diff);

        }


        /*****************************************************************/
        /* kivezetni a metódust, helyette a                             */
        /* RouteData.Instance.GetNearestNOD_ID -t kell használni !!!    */
        /*****************************************************************/

        /// <summary>
        /// Egy térképi ponthoz legközelebb lévő NOD_ID visszaadása
        /// </summary>
        /// <param name="p_pt"></param>
        /// <returns></returns>
        public int GetNearestNOD_ID(PointLatLng p_pt, out int r_diff)
         {
             r_diff = Int32.MaxValue;

             string ptX = (Math.Round(p_pt.Lng * Global.LatLngDivider)).ToString();
             string ptY = (Math.Round(p_pt.Lat * Global.LatLngDivider)).ToString();


             string sSql = "; with CTE as ( select NOD.ID as NOD_ID, NOD2.ID as NOD2_ID, NOD.ZIP_NUM as NOD_ZIP_NUM, NOD2.ZIP_NUM as NOD2_ZIP_NUM, " + Environment.NewLine +
             "NOD.NOD_XPOS as NOD_NOD_XPOS, NOD.NOD_YPOS as NOD_NOD_YPOS, NOD2.NOD_XPOS as NOD2_NOD_XPOS, NOD2.NOD_YPOS as NOD2_NOD_YPOS, " + Environment.NewLine +
             "EDG.RDT_VALUE as EDG_RDT_VALUE, EDG.EDG_STRNUM2 as EDG_EDG_STRNUM1, EDG.EDG_STRNUM2 as EDG_EDG_STRNUM2, EDG.EDG_STRNUM3 as EDG_EDG_STRNUM3, EDG.EDG_STRNUM4 as EDG_EDG_STRNUM4, " + Environment.NewLine +
             "EDG.EDG_MAXWEIGHT, EDG.EDG_MAXHEIGHT, EDG.EDG_MAXWIDTH, " + Environment.NewLine +
             "dbo.fnDistanceBetweenSegmentAndPoint(NOD.NOD_XPOS, NOD.NOD_YPOS, NOD2.NOD_XPOS, NOD2.NOD_YPOS, " + ptX + ",  " + ptY + ") as XDIFF " + Environment.NewLine +
             "from EDG_EDGE EDG " + Environment.NewLine +
             "inner join NOD_NODE NOD on NOD.ID = EDG.NOD_NUM " + Environment.NewLine +
             "inner join NOD_NODE NOD2 on NOD2.ID = EDG.NOD_NUM2 " + Environment.NewLine +
             "where NOD.NOD_XPOS != NOD2.NOD_XPOS and NOD.NOD_YPOS != NOD2.NOD_YPOS and " + Environment.NewLine +
             "(abs(NOD.NOD_XPOS - " + ptX + ") + abs(NOD.NOD_YPOS - " + ptY + ") < {0}   AND " + Environment.NewLine +
             "abs(NOD2.NOD_XPOS - " + ptX + ") + abs(NOD2.NOD_YPOS - " + ptY + ") < {0})) " + Environment.NewLine +
             "select top 1 " + Environment.NewLine +
             "case when abs(NOD_NOD_XPOS - " + ptX + ") + abs(NOD_NOD_YPOS - " + ptY + ") < abs(NOD2_NOD_XPOS - " + ptX + ") + abs(NOD2_NOD_YPOS - " + ptY + ") then NOD_ID else NOD2_ID end as ID, " + Environment.NewLine +
             "case when abs(NOD_NOD_XPOS - " + ptX + ") + abs(NOD_NOD_YPOS - " + ptY + ") < abs(NOD2_NOD_XPOS - " + ptX + ") + abs(NOD2_NOD_YPOS - " + ptY + ") then NOD_ZIP_NUM else NOD2_ZIP_NUM end as ZIP_NUM, " + Environment.NewLine +
             "XDIFF " + Environment.NewLine +
             "from CTE " + Environment.NewLine +
             "where  CTE.XDIFF <= (case when(EDG_RDT_VALUE = 6 or EDG_EDG_STRNUM1 != 0 or EDG_EDG_STRNUM2 != 0 or EDG_EDG_STRNUM3 != 0 or EDG_EDG_STRNUM4 != 0) then {1} else {2} end)  " + Environment.NewLine +
             "order by CTE.XDIFF asc";



             DataTable dt = DBA.Query2DataTable(String.Format(sSql, Global.NearestNOD_ID_Approach, Global.EdgeApproachCity, Global.EdgeApproachHighway));


             //Extrém esetben előfordulhat, hogy az eredeti közelítéssel (Global.NearestNOD_ID_Approach) nem találunk élt, mert az adott pozíciótol
             //nagyon messze vannak a végpontok. Ebben az esetben egy újabb lekérdezést indítunk 3 szoros közelítési távolsággal. 
             //Futásidőre optimalizálás miatt van így megoldva.
             if (dt.Rows.Count == 0)
             {
                 dt = DBA.Query2DataTable(String.Format(sSql, Global.NearestNOD_ID_ApproachBig, Global.EdgeApproachCity, Global.EdgeApproachHighway));
             }



             if (dt.Rows.Count > 0)
             {
                 r_diff = Util.getFieldValue<int>(dt.Rows[0], "XDIFF");
                 return Util.getFieldValue<int>(dt.Rows[0], "ID");
             }
             return 0;
         }




        /********************************************************************************/
        /* kivezetni a metódust, helyette a                                             */
        /* RouteData.Instance.GetNearestReachableNOD_IDForTruck -t kell használni !!!   */
        /********************************************************************************/
        /// <summary>
        /// Egy térképi ponthoz a leközelebb eső, egy jármű által megközelíthető pont (p_RZN_ID_LIST tartalmazza a behajtási zónákat)
        /// Globális paraméterek:
        ///     Global.NearestNOD_ID_Approach = 60000;         //Mekkora körzetben keressen lehetséges node-okat
        ///     Global.NearestNOD_ID_ApproachBig = 180000;     //Nagyobb körzet a II. menetes keresésnek
        ///     Global.EdgeApproachCity = 2000;                //Közelítő tűrés városon belül (RDT_VALUE>=3 és EDG_ETLCODE == '')
        ///     Global.EdgeApproachHighway = 30000;            //Közelítő tűrés városon kivül (RDT_VALUE in (1,2) VAGY EDG_ETLCODE != '')
        /// </summary>
        /// <param name="p_pt"></param>
        /// <param name="p_RZN_ID_LIST"></param>
        /// <returns></returns>
        public int GetNearestReachableNOD_IDForTruck(PointLatLng p_pt, string p_RZN_ID_LIST, int p_weight, int p_height, int p_width, out int r_diff)
         {
             string ptX = (Math.Round(p_pt.Lng * Global.LatLngDivider)).ToString();
             string ptY = (Math.Round(p_pt.Lat * Global.LatLngDivider)).ToString();
             r_diff = -1;

             /*
             string sSql = " select top 1  " + Environment.NewLine +
             "case when abs(NOD.NOD_XPOS - " + ptX + ") + abs(NOD.NOD_YPOS - " + ptY + ") < abs(NOD2.NOD_XPOS - " + ptX + ") + abs(NOD2.NOD_YPOS - " + ptY + ") then NOD.ID else NOD2.ID end as ID,  " + Environment.NewLine +
             "case when abs(NOD.NOD_XPOS - " + ptX + ") + abs(NOD.NOD_YPOS - " + ptY + ") < abs(NOD2.NOD_XPOS - " + ptX + ") + abs(NOD2.NOD_YPOS - " + ptY + ") then NOD.ZIP_NUM else NOD2.ZIP_NUM end as ZIP_NUM,  " + Environment.NewLine +
             "dbo.fnDistanceBetweenSegmentAndPoint(NOD.NOD_XPOS, NOD.NOD_YPOS, NOD2.NOD_XPOS, NOD2.NOD_YPOS, " + ptX + ", " + ptY + ") as XDIFF  " + Environment.NewLine +
             "from EDG_EDGE EDG  " + Environment.NewLine +
             "inner join NOD_NODE NOD on NOD.ID = EDG.NOD_NUM  " + Environment.NewLine +
             "inner join NOD_NODE NOD2 on NOD2.ID = EDG.NOD_NUM2  " + Environment.NewLine +
             "left outer join RZN_RESTRZONE RZN on RZN.RZN_ZoneCode = EDG.RZN_ZONECODE " + Environment.NewLine +
             "where NOD.NOD_XPOS != NOD2.NOD_XPOS and NOD.NOD_YPOS != NOD2.NOD_YPOS and  " + Environment.NewLine +
             "(abs(NOD.NOD_XPOS - " + ptX + ") + abs(NOD.NOD_YPOS - " + ptY + ") < {0}   OR  " + Environment.NewLine +
             "abs(NOD2.NOD_XPOS - " + ptX + ") + abs(NOD2.NOD_YPOS - " + ptY + ") < {0} ) and " + Environment.NewLine +
             "( RZN.ID is null  " + Environment.NewLine +
             "    " + (p_RZN_ID_LIST.Length > 0 ? " or ( RZN.ID is NOT null and charindex( ','+convert( varchar(50), isnull(RZN.ID,0)), '," + p_RZN_ID_LIST + "') > 0) " : "") + Environment.NewLine +
             "    " + (PMapIniParams.Instance.DestTraffic ? "  or EDG.EDG_DESTTRAFFIC = 1 " : " ") + "  " + Environment.NewLine +
             " ) and  " + Environment.NewLine +
             " dbo.fnDistanceBetweenSegmentAndPoint(NOD.NOD_XPOS, NOD.NOD_YPOS, NOD2.NOD_XPOS, NOD2.NOD_YPOS, " + ptX + ", " + ptY + ") <= " + Environment.NewLine +
             "  (case when (EDG.RDT_VALUE=6 or EDG.EDG_STRNUM1!=0 or EDG.EDG_STRNUM2!=0 or EDG.EDG_STRNUM3!=0 or EDG.EDG_STRNUM4!=0) then {1}  else {2} end) " + Environment.NewLine +
             "order by dbo.fnDistanceBetweenSegmentAndPoint(NOD.NOD_XPOS, NOD.NOD_YPOS, NOD2.NOD_XPOS, NOD2.NOD_YPOS, " + ptX + ", " + ptY + ") asc ";
             */

        string sSql = "; with CTE as ( select NOD.ID as NOD_ID, NOD2.ID as NOD2_ID, NOD.ZIP_NUM as NOD_ZIP_NUM, NOD2.ZIP_NUM as NOD2_ZIP_NUM, " + Environment.NewLine +
            "NOD.NOD_XPOS as NOD_NOD_XPOS, NOD.NOD_YPOS as NOD_NOD_YPOS, NOD2.NOD_XPOS as NOD2_NOD_XPOS, NOD2.NOD_YPOS as NOD2_NOD_YPOS, " + Environment.NewLine +
            "EDG.RDT_VALUE as EDG_RDT_VALUE, EDG.EDG_STRNUM2 as EDG_EDG_STRNUM1, EDG.EDG_STRNUM2 as EDG_EDG_STRNUM2, EDG.EDG_STRNUM3 as EDG_EDG_STRNUM3, EDG.EDG_STRNUM4 as EDG_EDG_STRNUM4, " + Environment.NewLine +
            "RZN.ID as RZN_ID,EDG.EDG_DESTTRAFFIC as EDG_EDG_DESTTRAFFIC, " + Environment.NewLine +
            "dbo.fnDistanceBetweenSegmentAndPoint(NOD.NOD_XPOS, NOD.NOD_YPOS, NOD2.NOD_XPOS, NOD2.NOD_YPOS, " + ptX + ",  " + ptY + ") as XDIFF" + Environment.NewLine +
            "from EDG_EDGE EDG " + Environment.NewLine +
            "inner join NOD_NODE NOD on NOD.ID = EDG.NOD_NUM " + Environment.NewLine +
            "inner join NOD_NODE NOD2 on NOD2.ID = EDG.NOD_NUM2 " + Environment.NewLine +
            "left outer join RZN_RESTRZONE RZN on RZN.RZN_ZoneCode = EDG.RZN_ZONECODE " + Environment.NewLine +
            "where NOD.NOD_XPOS != NOD2.NOD_XPOS and NOD.NOD_YPOS != NOD2.NOD_YPOS and " + Environment.NewLine +
            "(abs(NOD.NOD_XPOS - " + ptX + ") + abs(NOD.NOD_YPOS - " + ptY + ") < {0}   OR " + Environment.NewLine +    //itt nem AND
            "abs(NOD2.NOD_XPOS - " + ptX + ") + abs(NOD2.NOD_YPOS - " + ptY + ") < {0}) " + Environment.NewLine +
            "and (EDG.EDG_MAXWEIGHT=0 or EDG.EDG_MAXWEIGHT>={3}) and (EDG.EDG_MAXHEIGHT=0 or EDG.EDG_MAXHEIGHT={4}) and (EDG.EDG_MAXWIDTH=0 or EDG.EDG_MAXWIDTH={5}))" + Environment.NewLine +
            "select top 1 " + Environment.NewLine +
            "case when abs(NOD_NOD_XPOS - " + ptX + ") + abs(NOD_NOD_YPOS - " + ptY + ") < abs(NOD2_NOD_XPOS - " + ptX + ") + abs(NOD2_NOD_YPOS - " + ptY + ") then NOD_ID else NOD2_ID end as ID, " + Environment.NewLine +
            "case when abs(NOD_NOD_XPOS - " + ptX + ") + abs(NOD_NOD_YPOS - " + ptY + ") < abs(NOD2_NOD_XPOS - " + ptX + ") + abs(NOD2_NOD_YPOS - " + ptY + ") then NOD_ZIP_NUM else NOD2_ZIP_NUM end as ZIP_NUM, " + Environment.NewLine +
            "CTE.XDIFF" + Environment.NewLine +
            "from CTE " + Environment.NewLine +
            "where ( RZN_ID is null  " + Environment.NewLine +
            "    " + (p_RZN_ID_LIST.Length > 0 ? " or ( RZN_ID is NOT null and charindex( ','+convert( varchar(50), isnull(RZN_ID,0))+',', '," + p_RZN_ID_LIST + ",') > 0) " : "") + Environment.NewLine +
            "    " + (PMapIniParams.Instance.DestTraffic ? "  or EDG_EDG_DESTTRAFFIC = 1 " : " ") + "  " + Environment.NewLine +
            " ) and  " + Environment.NewLine +
            "CTE.XDIFF <= (case when(EDG_RDT_VALUE = 6 or EDG_EDG_STRNUM1 != 0 or EDG_EDG_STRNUM2 != 0 or EDG_EDG_STRNUM3 != 0 or EDG_EDG_STRNUM4 != 0) then {1} else {2} end)  " + Environment.NewLine +
            "order by CTE.XDIFF  asc";

            DataTable dt = DBA.Query2DataTable(String.Format(sSql, Global.NearestNOD_ID_ApproachBig, Global.EdgeApproachCity, Global.EdgeApproachHighway, p_weight, p_height, p_width));
            if (dt.Rows.Count > 0)
            {
                r_diff = Util.getFieldValue<int>(dt.Rows[0], "XDIFF");
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


            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            p_addr = regex.Replace(p_addr, " ");

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
            sqlCmd.CommandText = sSql + (sWhereAddr != "" || sWhereZipNum != "" || sWhereAddrNum != "" ? " where " + sWhereAddr + sWhereZipNum + sWhereAddrNum : "");

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
                    DataTable dt3 = d3.Tables[0];
                    string sDB_ZIP_NUM = "";

                    if (dt3.Rows.Count > 0)
                        sDB_ZIP_NUM = ("0000" + Util.getFieldValue<int>(dt3.Rows[0], "ZIP_NUM").ToString()).RightString(4);


                    if (dt3.Rows.Count > 0 && (sZIP_NUM == "" ||
                        (sDB_ZIP_NUM.Substring(0, 1) == "1" && sDB_ZIP_NUM.Substring(0, 1) == sZIP_NUM.Substring(0, 1)) ||
                        (sDB_ZIP_NUM.Substring(0, 1) != "1" && sDB_ZIP_NUM.Substring(0, 3) == sZIP_NUM.Substring(0, 3))))
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
            string street_name = "";
            var xElement = xdoc.Element("GeocodeResponse");
            if (xElement != null)
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

                        var xx = result.Elements("address_component").ToList();
                        foreach( var nd in xx)
                        {
                           
                            var t = nd.Element("type");
                            if (t != null)
                            {
                                if( t.Value == "route")
                                {
                                    var ln = nd.Element("long_name");
                                    if( ln != null)
                                    {
                                        var names = ln.Value.Split(' ');
                                        if (names.Length > 0)
                                            street_name = names[0];
                                        break;
                                    }
                                }
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

                boEdge edg = GetEdgeByNOD_ID(NOD_ID, street_name);
                if (edg == null && street_name != "")
                    edg = GetEdgeByNOD_ID(NOD_ID);


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

        public DataTable GetNotReverseGeocodedNodesToDT(int p_RDT_VALUE)
        {
            return DBA.Query2DataTable("select EDG.ID as EDG_ID, NOD.ID as NOD_ID, NOD_YPOS, NOD_XPOS, 1 as FromTo " + Environment.NewLine +
            "from EDG_EDGE EDG " + Environment.NewLine +
            "inner join NOD_NODE NOD on NOD.ID = EDG.NOD_NUM " + Environment.NewLine +
            "where isnull(NOD.NOD_NAME, '') = '' and RDT_VALUE=? " + Environment.NewLine +
            "UNION " + Environment.NewLine +
            "select EDG.ID as EDG_ID, NOD2.ID as NOD_ID, NOD_YPOS, NOD_XPOS, 2 as FromTo " + Environment.NewLine +
            "from EDG_EDGE EDG " + Environment.NewLine +
            "inner join NOD_NODE NOD2 on NOD2.ID = EDG.NOD_NUM2 " + Environment.NewLine +
            " where isnull(NOD2.NOD_NAME, '') = '' and RDT_VALUE=?", p_RDT_VALUE, p_RDT_VALUE);
        }

        public void UpdateNodeAddress(int ID, string p_NOD_NAME, string p_ZIP_NUM)
        {
            string sSQL = "update NOD_NODE set NODE_NAME=?, ZIP_NUM=? where ID=?";
            PMapCommonVars.Instance.CT_DB.ExecuteNonQuery(sSQL, p_NOD_NAME, p_ZIP_NUM, ID);
        }

        //"ENCRYPTBYKEY(KEY_GUID('EDGKey')," & getStr(EDG_NAME) & ")
        /*
        public void UpdateEdgeNodeAddress(int ID, string p_EDG_NAME, string EDG_STRNUM1 { get; set; }                     //páratlan oldal számozás kezdet
        public string EDG_STRNUM2 { get; set; }                     //páratlan oldal számozás vége
        public string EDG_STRNUM3 { get; set; }                     //páros oldal számozás kezdet
        public string EDG_STRNUM4 )
        open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************'
        */  
    }
}
