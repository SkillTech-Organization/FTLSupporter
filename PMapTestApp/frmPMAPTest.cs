﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VBInterface;
using System.IO;
using PMap;
using PMap.DB.Base;
using PMap.BLL;
using PMap.BO;
using PMap.LongProcess.Base;
using PMap.LongProcess;
using PMap.Route;
using PMap.Common;
using PMap.BLL.DataXChange;
using PMap.Common.PPlan;
using PMap.BO.DataXChange;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenCage.Geocode;
using System.Net.Http;
using PMap.WebTrace;
using System.Web.Script.Serialization;
using PMap.Common.Azure;
using System.Net;
using Newtonsoft.Json;

namespace PMapTestApp
{
    public partial class frmPMapTest : Form
    {
        private const string dbConf = "DB0";

        public frmPMapTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String sParam = "46.3|20.1|egy|46.2064|19.785|kettő|46.1855|18.95555|Három|46.076|18.234|Négy";
            //String sParam = "47.49419|19.079535|Raktár<NL>1081 Budapest 8.ker Jászberényi út 7|47.51679|19.107978|bbbb<NL>9061 Vámosszabadi Arany János|47.501038|19.082688|asdasddas<NL>1047 Budapest 4.ker asdasd|47.49419|19.079535|Raktár<NL>1081 Budapest 8.ker Jászberényi út 7";
            // String sParam ="47.49419|19.079535|Raktár<NL>1081 Budapest 8.ker Jászberényi út 7|47.503959|19.088922|asdasddas<NL>1047 Budapest 4.ker asdasd|47.519213|19.111754|bbbb<NL>9061 Vámosszabadi Arany János|47.49419|19.079535|Raktár<NL>1081 Budapest 8.ker Jászberényi út 7";
            //String sParam = "47.49194|19.14414|1106 Raktár (Budapest 10.ker)|47.496263|19.068621|1085 SPAR MAGYARORSZÁG KFT.8259 % 8259 KAISER' SZUPERMARKET (Budapest 8.ker)|47.501556|19.081227|1077 SZEZÁM HUNGARY KFT. % RONI ABC (Budapest 7.ker)|47.50671|19.092574|1146 CBA VÖRÖSVÁR KFT. % RÉCSEI BEVÁSÁRLÓKÖZPONT (Budapest 14.ker)|47.49194|19.14414|1106 Raktár (Budapest 10.ker)";
            //String sParam = "47.49194|19.14414|1106 Raktár (Budapest 10.ker)|47.449019|19.113298|1205 CBA CURIOSUM KFT. % ÁSZ PLUSZ (Budapest 20.ker)|47.440924|19.095694|1203 SPAR MAGYARORSZÁG KFT.103. % 103. SPAR SZUPERMARKET (Budapest 20.ker)|47.420968|19.067596|1215 CSEP-KER 21 KER. ÉS ÉRT. ZRT. 7. % ABC (Budapest 21.ker)|47.415001|19.063438|1214 RAVEL-TRADE KFT. % REÁL ÉLELMISZER (Budapest 21.ker)|47.40763|19.079396|1213 CBA BÁNSA KFT. % SZIGET ÉLELMISZERÜZLET (Budapest 21.ker)|47.416645|19.077999|1212 RAVEL-TRADE KFT. % REÁL ÉLELMISZER (Budapest 21.ker)|47.435764|19.099449|1203 CBA REMIZ-KER.99 KFT. % ERZSÉBET ÁRUHÁZ (Budapest 20.ker)|47.437385|19.127032|1202 LAKI MIHÁLYNÉ % BOROZÓ (Budapest 20.ker)|47.443059|19.121877|1205 CBA CURIOSUM KFT. % ÁSZ-NET ÉLELMISZER (Budapest 20.ker)|47.457329|19.17812|1183 ARZENÁL KFT. % REÁL SÁRKÁNY DISZKONT (Budapest 18.ker)|47.475924|19.164744|1108 CSEMEGE MATCH ZRT. 604. % 604.SMATCH (Budapest 10.ker)|47.49194|19.14414|1106 Raktár (Budapest 10.ker)";
            String sParam = "47.197178|18.4144592|SZFEHERVAR|48.1028460|20.7875061|MISK";
            MessageBox.Show("Route eredmény:" + (new PMapInterface()).ShowRoute(sParam, "", dbConf));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //            string selpos = pmi.SelectPosition("46.3", "20.1", "Kiválasztott DEPO<NL>sortörés", "");
            string selpos = (new PMapInterface()).SelectPosition("47,4983569", "19.0404224", "Kiválasztott DEPO<NL>sortörés", "", dbConf);
            if (selpos != "")
                MessageBox.Show("Kiválasztva:" + selpos);
            else
                MessageBox.Show("Nincs kiválasztás!" + selpos);

        }


        private void button3_Click(object sender, EventArgs e)
        {
            String sParam = "46.3|20.1|egy|0|46.2064|19.785|kettő|1|46.1855|18.95555|Három|1|46.076|18.234|Négy|0";
            sParam = "46,90357|19,700399|BUDAPEST BAROSS TÉR 9. 2767028<NL>Rábakecöl BAROSS TÉR 9. |0|48,546077|21,35137|ACSA KOSSUTH U. 51 2766460<NL>Kéked KOSSUTH U. 51 |0";
            sParam = "47,923971|16,868939|corner1|0|47,93082000|16,868939|corner2|0|47,93082000|16,877319|corner3|0|47,923971|16,877319|corner4|0|47,925685|16,870908|pont1|0";
            sParam = "48,033194|21,747552|Vadász Csárda<NL>Nyíregyháza Kemecsei út 1. |0";
            MessageBox.Show("ShowDepot eredmény:" + (new PMapInterface()).ShowDepots(sParam, "", dbConf));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sDepot = "46,90357|19,700399|BUDAPEST BAROSS TÉR 9. 2767028<NL>Rábakecöl BAROSS TÉR 9. |1|48,546077|21,35137|ACSA KOSSUTH U. 51 2766460<NL>Kéked KOSSUTH U. 51 |0";
            String sRoute = "46.3|20.1|egy|46.2064|19.785|kettő|46.1855|18.95555|Három|46.076|18.234|Négy";

            (new PMapInterface()).ShowDepotsAndRoute("679", "12133", "", dbConf);
            MessageBox.Show("OK");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgSelPlan d = new dlgSelPlan();


            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PlanParams testPlanParams = new PlanParams();
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = 1 });
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = 2 });
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = 3 });
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = 4 });
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = 5 });
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = 6 });
                (new PMapInterface()).PlanToursVB("" + d.m_PLN_ID, "1", testPlanParams, "", dbConf);
            }
        }



        private static Random randomSeed = new Random();


        private void button8_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgSelPlan d = new dlgSelPlan();


            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                (new PMapInterface()).CalcPMapRoutesByPlan("", dbConf, d.m_PLN_ID, true);
            }

        }


        private void button10_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            frmRouteCheck fc = new frmRouteCheck();
            fc.ShowDialog();
        }

        private void frmPMapTest_Load(object sender, EventArgs e)
        {
            //        (new PMapInterface()).InitPMapRouteData("", dbConf);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                PMapIniParams.Instance.ReadParams("", dbConf);

                SQLServerAccess db = new SQLServerAccess();
                db.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                db.ExecuteNonQuery("truncate table DST_DISTANCE");
                db.Close();


                (new PMapInterface()).CalcPMapRoutesByOrders("", dbConf, "2013.03.22", "2013.03.22");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
                return;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            (new PMapInterface()).CalcTOLL("", dbConf,
                "12277,12278,12279,12280,12281,12282,12283,12284,12285,12286,12287,12288,12289,12290,12291,12292,12293,12294");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgSelPlan d = new dlgSelPlan();
            //            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PMapCommonVars.Instance.ConnectToDB();

                BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, PMapIniParams.Instance.OptimizeTimeOutSec, "Túraoptimalizálás", true);
                //TourOptimizerProcess top = new TourOptimizerProcess(pd, d.m_PLN_ID, 12405);
                //TourOptimizerProcess top = new TourOptimizerProcess(pd, d.m_PLN_ID, 0, true, false);
                TourOptimizerProcess top = new TourOptimizerProcess(pd, 12, 0, true, false);
                top.Run();
                pd.ShowDialog();

                PMapCommonVars.Instance.CT_DB.Close();


            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);




            dlgSelPlan d = new dlgSelPlan();
            //          if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PMapCommonVars.Instance.ConnectToDB();
                bllPlanEdit pe = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
                pe.RecalcTour(0, 115, Global.defWeather);



                //                bllOptimize opt = new bllOptimize(PMapCommonVars.Instance.CT_DB, d.m_PLN_ID, 0, true);
                bllOptimize opt = new bllOptimize(PMapCommonVars.Instance.CT_DB, 12, 0, true);

                opt.FillOptimize(null);
                opt.ProcessResult(PMapIniParams.Instance.PlanResultFile, null);


                PMapCommonVars.Instance.CT_DB.Close();


            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            (new PMapInterface()).InitPMapRouteData("", dbConf);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (saveCSV.ShowDialog() == DialogResult.OK)
            {
                PMapIniParams.Instance.ReadParams("", dbConf);

                PMapCommonVars.Instance.ConnectToDB();

                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                bllDepot depot = new bllDepot(PMapCommonVars.Instance.CT_DB);

                if (UI.Confirm("Összes távolság törlése?"))
                    PMapCommonVars.Instance.CT_DB.ExecuteNonQuery("truncate table DST_DISTANCE");


                bool bOK = true;
                while (bOK)
                {
                    //hiányzó távolságok kiszámítása
                    List<boRoute> res = route.GetDistancelessNodesForAllZones__ONLYFORTEST(1000000);
                    if (res.Count == 0)
                        break;
                    bOK = PMRouteInterface.GetPMapRoutesSingle(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, false);
                }

                if (bOK)
                {
                    List<boDepot> d = depot.GetAllDepots();
                    BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(1, d.Count, "Útdíj export", true);
                    TollExportProcess tep = new TollExportProcess(pd, saveCSV.FileName, d);
                    tep.Run();
                    pd.ShowDialog();
                }
                PMapCommonVars.Instance.CT_DB.Close();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);


            dlgTestGeoCoding d = new dlgTestGeoCoding();
            d.txtAddr.Text = "1027 budapest petőfi utca 333";
            d.txtAddr.Text = "Ivanka pri Dunaji Cintorínska, Ivanka pri Dunaji";
            d.txtAddr.Text = "Jászberény Kossuth u 80";
            d.txtAddr.Text = "Jászberény KOSSUTH U. 23.";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var vbintf = new VBInterface.PMapInterface();

                UI.Message("Geocoding->" + vbintf.GeocodingByGoogle(d.txtAddr.Text, "", dbConf));


                PMapCommonVars.Instance.ConnectToDB();


                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                bllDepot depot = new bllDepot(PMapCommonVars.Instance.CT_DB);
                int ZIP_ID = 0;
                int NOD_ID = 0;
                int EDG_ID = 0;
                boDepot.EIMPADDRSTAT DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.MISSADDR;
                bool bFound = route.GeocodingByAddr(d.txtAddr.Text, out ZIP_ID, out NOD_ID, out EDG_ID, out DEP_IMPADDRSTAT);
                if (bFound)
                {
                    string sSql = "open symmetric key EDGKey decryption by certificate CertPMap  with password = '***************' " + Environment.NewLine +
                                   "select NOD.ID as NOD_ID, EDG.ID as EDG_ID, NOD.ZIP_NUM, ZIP_CITY, convert(varchar(max),decryptbykey(EDG_NAME_ENC)) as EDG_NAME,  " + Environment.NewLine +
                                   "EDG_STRNUM1, EDG_STRNUM2, EDG_STRNUM3, EDG_STRNUM4 " + Environment.NewLine +
                                   "from NOD_NODE NOD " + Environment.NewLine +
                                   "inner join EDG_EDGE EDG on EDG.NOD_NUM = NOD.ID or EDG.NOD_NUM2 = NOD.ID " + Environment.NewLine +
                                   "inner join ZIP_ZIPCODE ZIP on ZIP.ZIP_NUM = NOD.ZIP_NUM " + Environment.NewLine +
                                   "where NOD.ID = " + NOD_ID.ToString() + " and EDG.ID=" + EDG_ID.ToString();
                    DataTable dt = PMapCommonVars.Instance.CT_DB.Query2DataTable(sSql);
                    if (dt.Rows.Count == 1)
                    {
                        UI.Message("NOD.ID = " + NOD_ID.ToString() + ",EDG_ID=" + EDG_ID.ToString() + ",Addr=" +
                            (Util.getFieldValue<int>(dt.Rows[0], "ZIP_NUM")).ToString() + " " +
                            Util.getFieldValue<string>(dt.Rows[0], "ZIP_CITY") + " " +
                            Util.getFieldValue<string>(dt.Rows[0], "EDG_NAME"));

                    }
                    else
                        UI.Message("Hiba a cím lekérdezésében! rekordszám:" + dt.Rows.Count.ToString());
                }
                else
                {
                    UI.Message("Nem található cím!");
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            /*
            dlgImportResult irx = new dlgImportResult();
            irx.propertyGridCtrl1.SetObject(new dtXResult());
            irx.ShowDialog();
            */

            List<boXDepot> depots = new List<boXDepot>();

            dlgTestInput d = new dlgTestInput();
            boXDepot dep = new boXDepot();

            dep.DEP_CODE = "TEST12x";
            dep.DEP_NAME = "Lerakónév12x";
            dep.ZIP_NUM = 6726;
            dep.DEP_ADRSTREET = "vedres 1";
            dep.DEP_ADRNUM = "1";
            dep.DEP_OPEN = 60;
            dep.DEP_CLOSE = 1200;
            dep.DEP_COMMENT = "Megjegyzés";
            dep.DEP_SRVTIME = 10;
            dep.DEP_QTYSRVTIME = 5;
            dep.DEP_LIFETIME = 0;
            dep.WHS_CODE = "WHS";
            dep.Lat = 0;
            dep.Lng = 0;

            dep.DEP_CODE = "25939";
            dep.DEP_NAME = "HATÁR TOP KFTx.";
            dep.ZIP_NUM = 6422;
            dep.ZIP_CITY = "Tompa";
            dep.DEP_ADRSTREET = "Alsósáskalaposxx";
            dep.DEP_ADRNUM = "18/a";
            dep.DEP_OPEN = 0;
            dep.DEP_CLOSE = 1439;
            dep.DEP_COMMENT = "";
            dep.DEP_SRVTIME = 0;
            dep.DEP_QTYSRVTIME = 5;
            dep.DEP_LIFETIME = 0;
            dep.WHS_CODE = "1";
            dep.Lat = 0.0;
            dep.Lng = 0.0;

            d.propertyGridCtrl1.SetObject(dep);

            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                depots.Add(dep);
                List<dtXResult> res = (new PMapInterface()).ImportDepots("", dbConf, depots);
                dlgImportResult ir = new dlgImportResult();

                int i = 1;
                foreach (var rr in res)
                {
                    ir.Text = "VISSZATERESI ERTEK " + i.ToString() + "/" + res.Count.ToString();
                    i++;
                    ir.propertyGridCtrl1.SetObject(rr);
                    ir.ShowDialog();
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dlgTestInput d = new dlgTestInput();
            List<boXTruck> trks = new List<boXTruck>();

            boXTruck trk = new boXTruck();

            trk.TRK_CODE = "TRKCODE1";
            trk.TRK_REG_NUM = "TRK_REG_NUM1";
            trk.TRK_TRAILER = "TRK_TRAILER1";
            trk.TRK_LENGTH = 10;
            trk.TRK_WIDTH = 300;
            trk.TRK_HEIGHT = 320;
            trk.TRK_WIDTH = 300;
            trk.TRK_HEIGHT = 400;
            trk.TRK_COLOR = Color.Azure;
            trk.TRK_GPS = true;
            trk.TRK_BACKPANEL = false;
            trk.TRK_LOGO = false;
            trk.TRK_AXLENUM = 4;
            trk.TRK_ETOLLCAT = 2;
            trk.TRK_ENGINEEURO = 5;
            trk.TRK_IDLETIME = 600;
            trk.TRK_ACTIVE = true;
            trk.TRK_COMMENT = "TRK_COMMENT1";
            trk.CRR_CODE = "WAB";
            trk.WHS_CODE = "WHS";
            trk.SPV_VALUE1 = 90;
            trk.SPV_VALUE2 = 80;
            trk.SPV_VALUE3 = 70;
            trk.SPV_VALUE4 = 50;
            trk.SPV_VALUE5 = 40;
            trk.SPV_VALUE6 = 30;
            trk.SPV_VALUE7 = 20;

            trk.CPP_LOADQTY = 1000;

            trk.CPP_LOADVOL = 20;

            trk.TFP_FIXCOST = 2000;
            trk.TFP_KMCOST = 100;
            trk.TFP_HOURCOST = 3000;
            d.propertyGridCtrl1.SetObject(trk);
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                trks.Add(trk);
                List<dtXResult> res = (new PMapInterface()).ImportTrucks("", dbConf, trks);

                dlgImportResult ir = new dlgImportResult();

                int i = 1;
                foreach (var rr in res)
                {
                    ir.Text = "VISSZATERESI ERTEK " + i.ToString() + "/" + res.Count.ToString();
                    i++;
                    ir.propertyGridCtrl1.SetObject(rr);
                    ir.ShowDialog();
                }

            }

        }

        private void button17_Click(object sender, EventArgs e)
        {

            dlgTestRouteVis d = new dlgTestRouteVis();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> lstDepotID = new List<string>(d.txtDEPID.Text.Split(','));
                List<dtXResult> res = (new PMapInterface()).RouteVisualization("", dbConf,
                    lstDepotID.Select(i => new boXRouteSection()
                    {
                        Start_DEP_ID = Convert.ToInt32(i.Split(';')[0]),
                        RouteSectionType = (boXRouteSection.ERouteSectionType)Enum.Parse(typeof(boXRouteSection.ERouteSectionType), i.Split(';')[1])
                    }).ToList(), Convert.ToInt32(d.txtTRKID.Text), false, 1);
                dlgRouteVisCalcRes dd = new dlgRouteVisCalcRes();

                dd.propertyGridCtrl1.SetObject(res.First());
                var rr = (boXRouteSummary)res.First().Data;
                dd.propertyGridCtrl2.SetObject(rr.FastestRoute);
                dd.propertyGridCtrl3.SetObject(rr.ShortestRoute);
                dd.ShowDialog();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);
            SQLServerAccess db = new SQLServerAccess();
            db.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            List<dtXResult> res = (new PMapInterface()).CreateNewPlan("", dbConf, "2X20170828", 1, new DateTime(2017, 08, 29), new DateTime(2017, 08, 30), false, new DateTime(2017, 08, 29), new DateTime(2017, 08, 30));
            dlgRouteVisCalcRes dd = new dlgRouteVisCalcRes();
            var pp = res.First();
            dd.propertyGridCtrl1.SetObject(pp);
            if (pp.Status == dtXResult.EStatus.OK)
                dd.propertyGridCtrl2.SetObject(((boXNewPlan)(pp.Data)).lstDepWithoutGeoCoding);

            dd.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            dlgTestRouteVis d = new dlgTestRouteVis();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> lstDepotID = new List<string>(d.txtDEPID.Text.Split(','));
                List<dtXResult> res = (new PMapInterface()).RouteVisualizationCalc("", dbConf,
                    lstDepotID.Select(i => new boXRouteSection()
                    {
                        Start_DEP_ID = Convert.ToInt32(i.Split(';')[0]),
                        RouteSectionType = (boXRouteSection.ERouteSectionType)Enum.Parse(typeof(boXRouteSection.ERouteSectionType), i.Split(';')[1])
                    }).ToList(), Convert.ToInt32(d.txtTRKID.Text), false, 1);
                dlgRouteVisCalcRes dd = new dlgRouteVisCalcRes();

                dd.propertyGridCtrl1.SetObject(res.First());
                var rr = (boXRouteSummary)res.First().Data;
                dd.propertyGridCtrl2.SetObject(rr.FastestRoute);
                dd.propertyGridCtrl3.SetObject(rr.ShortestRoute);
                dd.ShowDialog();

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            List<boXOrder> orders = new List<boXOrder>();
            Random rnd = new Random();
            dlgTestInput d = new dlgTestInput();
            boXOrder ord = new boXOrder()
            {
                ORD_NUM = "ORD_" + rnd.Next(1, 100).ToString(),
                ORD_FIRSTDATE = DateTime.Now.Date,
                ORD_DATE = DateTime.Now.Date,
                WHS_CODE = "WHS",
                DEP_CODE = "DEP_" + rnd.Next(1, 100).ToString(),
                ORD_CLIENTNUM = "CLIENT_" + rnd.Next(1, 100).ToString(),
                CTP_CODE = "1",
                OTP_CODE = "1",
                ORD_QTY = rnd.Next(1, 10),
                ORD_VOLUME = rnd.Next(1, 10),
                ORD_LENGTH = rnd.Next(1, 10),
                ORD_WIDTH = rnd.Next(1, 10),
                ORD_HEIGHT = rnd.Next(1, 10),
                ORD_COMMENT = "",
                ORD_SERVS = 600,
                ORD_SERVE = 1200,
                DEP_NAME = "DEP_NAME_" + rnd.Next(1, 100).ToString(),
                ZIP_NUM = 3527,
                ZIP_CITY = "Miskolc",
                DEP_ADRSTREET = "Besenyői út",
                DEP_ADRNUM = "24",
                DEP_OPEN = 600,
                DEP_CLOSE = 900,
                DEP_COMMENT = "COMMENT_ " + rnd.Next(1, 100).ToString(),
                DEP_SRVTIME = 20,
                DEP_QTYSRVTIME = 1,
                DEP_LIFETIME = 0
            };

            d.propertyGridCtrl1.SetObject(ord);

            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                orders.Add(ord);
                List<dtXResult> res = (new PMapInterface()).ImportOrders("", dbConf, orders);
                dlgImportResult ir = new dlgImportResult();

                int i = 1;
                foreach (var rr in res)
                {
                    ir.Text = "VISSZATÉRÉSI ÉRTÉK " + i.ToString() + "/" + res.Count.ToString();
                    i++;
                    ir.propertyGridCtrl1.SetObject(rr);
                    ir.ShowDialog();
                }
            }

        }

        private void button21_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            PlanParams testPlanParams = new PlanParams();
            for (int i = 0; i < 50; i++)
            {
                testPlanParams.EnabledTrucksInNewPlan.Add(new PlanParams.CEnabledTruck() { TRK_ID = i });
            }

            (new PMapInterface()).PlanToursVB("", "1", testPlanParams, "", dbConf);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Util.Log2File("árvíztűrőtükörfúróÁRVÍZTŰRŐTÜKÖRFÚRÓ", true);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgSelPlan d = new dlgSelPlan();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<dtXResult> res = (new PMapInterface()).GetPlan("", dbConf, d.m_PLN_ID);
                dlgRouteVisCalcRes ir = new dlgRouteVisCalcRes();
                dtXResult rr = res.First();
                ir.propertyGridCtrl1.SetObject(rr);
                if (rr.Data != null)
                    ir.propertyGridCtrl2.SetObject(rr.Data);
                ir.ShowDialog();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            List<dtXResult> res = (new PMapInterface()).GetPlans("", dbConf, 0, DateTime.MinValue, DateTime.MinValue);
            dlgRouteVisCalcRes ir = new dlgRouteVisCalcRes();
            dtXResult rr = res.First();
            ir.propertyGridCtrl1.SetObject(rr);
            if (rr.Data != null)
                ir.propertyGridCtrl2.SetObject(rr.Data);
            ir.ShowDialog();

        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            string sRes = (new PMapInterface()).CheckLicence("", dbConf);
            MessageBox.Show("Licence eredmény:" + sRes);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            List<boXChkRoute> lstRoutes = new List<boXChkRoute>();
            lstRoutes.Add(new boXChkRoute() { FromLat = 46.232789, FromLng = 20.1404686, ToLat = 47.492975, ToLng = 19.1200849, RZones = "B35,CS12,CS7,DB1,DP1,DP3,DP7,ÉB1,ÉB7,ÉP1,HB1,KP1,KV3,P35,P75" });
            List<boXChkRes> res = (new PMapInterface()).CheckRoutes("", dbConf, lstRoutes);
            dlgImportResult ir = new dlgImportResult();
            int i = 1;
            foreach (var rr in res)
            {
                ir.Text = "VISSZATERESI ERTEK " + i.ToString() + "/" + res.Count.ToString();
                i++;
                ir.propertyGridCtrl1.SetObject(rr);
                ir.ShowDialog();
            }
        }



        private async void btnReverseGeocoding_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgTestReverseGeocoding d = new dlgTestReverseGeocoding();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                var gc = new Geocoder("e0f24ed22a53d63a9e2d7c3ba72ff7fd");
                var result = gc.Geocode("82 Clerkenwell Road, London");

                var reserveresult = gc.ReverseGeocode(Convert.ToDouble(d.numLat.Value), Convert.ToDouble(d.numLng.Value));
                var address = await GetAddressForCoordinates(d.numLat.Value, d.numLng.Value);
                MessageBox.Show("Cím:" + address);

            }
        }
        /*
        https://api.opencagedata.com/geocode/v1/json?q=-23.5373732,-46.8374628&pretty=1&key=YOUR-API-KEY'

        https://api.opencagedata.com/geocode/v1/json?q=41.40139%2C2.12870&pretty=1&key=
    */
        private async Task<string> GetAddressForCoordinates(decimal latitude, decimal longitude)
        {
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(@"https://api.opencagedata.com/geocode/v1/") };
            HttpResponseMessage httpResult = await httpClient.GetAsync(
                String.Format("json?q={0},{1}&pretty=1&key={2}",
                latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", "."),
                "e0f24ed22a53d63a9e2d7c3ba72ff7fd"));
            //         String.Format("json?format=json&lat={0:00.00000000}&lon={1:00.00000000}", latitude, longitude).Replace(",", "."));
            var lf = await httpResult.Content.ReadAsStringAsync();
            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(lf);
            // var Album album = jalbum.ToObject<Album>();
            var bb = jObject["results"][0]["components"]["city"];

            var formattedAddress = jObject["results"][0]["formatted"];
            return formattedAddress.ToString();
            /*
                    string house = jObject.GetNamedObject("addressparts").GetNamedString("house");
                        string road = jsonObject.GetNamedObject("addressparts").GetNamedString("road");
                        string city = jsonObject.GetNamedObject("addressparts").GetNamedString("city");
                        string state = jsonObject.GetNamedObject("addressparts").GetNamedString("state");
                        string postcode = jsonObject.GetNamedObject("addressparts").GetNamedString("postcode");
                        string country = jsonObject.GetNamedObject("addressparts").GetNamedString("country");
                        return string.Format("{0} {1}, {2}, {3} {4} ({5})", house, road, city, state, postcode, country);
                        */
        }

        private void btnBatchReverseGeoc_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);
            //Egy külön szálban, háttérben futkorászik
            BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, 2500, "Útvonal részletező", true);
            OCAddrProcess OCAproc = new OCAddrProcess(pd);
            OCAproc.Run();
            pd.ShowDialog();

        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgSelPlan d = new dlgSelPlan();


            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
 
                SQLServerAccess db = new SQLServerAccess();
                db.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                bllPlan bllPlan = new bllPlan(db);
                var TourList = bllPlan.GetToursForAzure(d.m_PLN_ID);
                db.Close();

                //connectionString = "DefaultEndpointsProtocol=https;AccountName=petawebdbtest;AccountKey=ucXUpxndw4j+73Ygjk7Cg3I93voioqGC5PCCelVr4g8aSpub+AEfk99YG6c/8768Exzv9wXDcQQd/o7xenoxzQ==;EndpointSuffix=core.windows.net" />

                AzureTableStore.Instance.AzureAccount = "petawebdbtest";
                AzureTableStore.Instance.AzureKey = "ucXUpxndw4j+73Ygjk7Cg3I93voioqGC5PCCelVr4g8aSpub+AEfk99YG6c/8768Exzv9wXDcQQd/o7xenoxzQ==";
                      BllWebTraceTour bllWebTrace = new BllWebTraceTour(Environment.MachineName);
                BllWebTraceTourPoint bllWebTraceTourPoint = new BllWebTraceTourPoint(Environment.MachineName);

                AzureTableStore.Instance.DeleteTable("PMTour");
                AzureTableStore.Instance.DeleteTable("PMMapPoint");
                foreach (var xTr in TourList)
                {
                    bllWebTrace.MaintainItem(xTr);
                    foreach( var xTp in xTr.TourPoints)
                    {
                        bllWebTraceTourPoint.MaintainItem(xTp);
                    }
                }
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            BllWebTraceTour bllWebTrace = new BllWebTraceTour(Environment.MachineName);
            //egy elem
            var l1 = bllWebTrace.Retrieve(PMTour.PartitonConst, "12780");
            //összes elem (van where paraméter is!)
            int total;
            var l2 = bllWebTrace.RetrieveList(out total);

        }

        /*
         backendbe bekerult egy CryptoHandler.cs, es a Web.configba az also ket kulcs-ertek par, 
         ami a token generalashoz kell majd neked. A hivas ugy megy, hogy az Auth/GenerateTempUserToken-re 
         kell kuldened egy HTTPGet-et egy parameterrel: tokenContent={base64-be kodolva azt, amit 
         keresztultoltam az encrypten}
         encrypten egy JSON-be serializalt List<string> kell, ahol minden elem egy rendszam (jelenleg)
         feature-vehicle-tracking branch legutolso commitja, ha keresed
        */
        private void button29_Click(object sender, EventArgs e)
        {
            List<string> lstRegNo = new List<string>();
            lstRegNo.Add("KVS-483");
            lstRegNo.Add("LSN-700");
            lstRegNo.Add("PEK-064");
            lstRegNo.Add("PEB-687");
            
            /*
            < add key = "AuthTokenCryptAESKey" value = "VhHe1F6DExaWl1T0bcOxdok58CyIXnjwCDQmojbwpH4=" />
            < add key = "AuthTokenCryptAESIV" value = "GFXXSSi7IQFN0bgbwuuVng==" />
            */
            var AuthTokenCryptAESKey = "VhHe1F6DExaWl1T0bcOxdok58CyIXnjwCDQmojbwpH4=";
            var AuthTokenCryptAESIV = "GFXXSSi7IQFN0bgbwuuVng==";

            JsonSerializerSettings jsonsettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var jsonRegno = JsonConvert.SerializeObject(lstRegNo, jsonsettings);

            var crypted = AESCryptoHelper.EncryptString(jsonRegno, AuthTokenCryptAESKey, AuthTokenCryptAESIV);
            byte[] bytes = Encoding.Default.GetBytes(crypted);
            string base64 = Convert.ToBase64String(bytes);

            Console.WriteLine(base64);


            string url = @"http://mplastwebtest.azurewebsites.net/Auth/GenerateTempUserToken?tokencontent="+ base64;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string html = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);
            
            //http://mplastwebtest.azurewebsites.net/Auth/TokenLoginRedirect?token=P6w/g1SU1wb/F6cJBwYDF9Ct/9Zw0hGbBosLMnTAq0ZYImQBKW7QsRJ5brMqiYBr

        }
    }
}
