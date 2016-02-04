using System;
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
using FTLSupporter;

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
            try
            {
                PMapIniParams.Instance.ReadParams("", dbConf);

                dlgSelPlan d = new dlgSelPlan();
                if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SQLServerAccess db = new SQLServerAccess();
                    db.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                    //db.ExecuteNonQuery("truncate table DST_DISTANCE");
                    //db.ExecuteNonQuery("delete DST_DISTANCE where NOD_ID_FROM = 13 or NOD_ID_TO = 13");
                    db.Close();
                    (new PMapInterface()).CalcPMapRoutesByPlan("", dbConf, d.m_PLN_ID, true);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
                return;
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
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PMapCommonVars.Instance.ConnectToDB();


                BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, PMapIniParams.Instance.OptimizeTimeOutSec, "Túraoptimalizálás", true);
                //TourOptimizerProcess top = new TourOptimizerProcess(pd, d.m_PLN_ID, 12405);
                TourOptimizerProcess top = new TourOptimizerProcess(pd, d.m_PLN_ID, 0, true, false);
                top.Run();
                pd.ShowDialog();

                PMapCommonVars.Instance.CT_DB.Close();


            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            PMapIniParams.Instance.ReadParams("", dbConf);

            dlgSelPlan d = new dlgSelPlan();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PMapCommonVars.Instance.ConnectToDB();

                bllOptimize opt = new bllOptimize(PMapCommonVars.Instance.CT_DB, d.m_PLN_ID, 0, true);

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
                    List<boRoute> res = route.GetDistancelessNodesForAllZones(1000000);
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
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
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
            List<boXDepot> depots = new List<boXDepot>();

       //     dlgTestInput d = new dlgTestInput();
            boXDepot dep = new boXDepot();

            dep.DEP_CODE = "TEST12";
            dep.DEP_NAME = "Lerakónév12";
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
            dep.DEP_NAME = "HATÁR TOP KFT.";
            dep.ZIP_NUM = 6422;
            dep.ZIP_CITY = "Tompa";
            dep.DEP_ADRSTREET = "Alsósáskalapos";
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

     //       d.propertyGridCtrl1.SetObject(dep);

     //       if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            trk.TRK_WIDTH = 4;
            trk.TRK_HEIGHT = 3;
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

            List<dtXResult> res = (new PMapInterface()).CreateNewPlan("", dbConf, "Xtest1", 1, new DateTime(2013, 04, 11), new DateTime(2013, 04, 12), false, new DateTime(2013, 04, 11), new DateTime(2013, 04, 12));
            dlgRouteVisCalcRes dd = new dlgRouteVisCalcRes();
            var pp = res.First();
            dd.propertyGridCtrl1.SetObject(pp);
            if( pp.Status == dtXResult.EStatus.OK)
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
                    ir.Text = "VISSZATERESI ERTEK " + i.ToString() + "/" + res.Count.ToString();
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
                if( rr.Data != null)
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

            /* lat/lng kezelő:
               
            declare @lat float 
            declare @lng float
            set @lat = 47.244
            set @lng = 18.628

            select top 1 ID, NOD_YPOS, NOD_XPOS, ZIP_NUM, abs( NOD_YPOS-@lat) as YD, abs( NOD_XPOS-@lng) as XD from NOD_NODE 
            order by abs( NOD_YPOS-@lat) + abs( NOD_XPOS-@lng) asc
             */

            FTLSupporter.FTLTask tsk = new FTLSupporter.FTLTask()
            {
                TaskID = "TSK001",
                IsOneWay = true,
                Client = "Megbízó 1",
                PartnerNameFrom = "Felrakó 1",
                StartFrom = DateTime.Now.Date.AddHours(6),                  //Felrakás kezdete reggel 6-tól
                EndFrom = DateTime.Now.Date.AddHours(12),                   //Felrakás vége reggel 12-ig
                LatFrom = 47.244,                                           //Velenei tó környéke
                LngFrom = 18.628,
                PartnerNameTo = "Lerakó 1",
                StartTo = DateTime.Now.Date.AddHours(10),                   //Lelrakás kezdete reggel 10-tól
                EndTo = DateTime.Now.Date.AddHours(18),                     //Lelrakás vége 18-ig
                LatTo = 46.881,                                             //Kecskemét környéke
                LngTo = 19.707,
                CargoType = "Normál",
                Weight = 1000,
                TruckTypes = "Trailer,Hűtős"
            };

            FTLSupporter.FTLTruck trk1 = new FTLSupporter.FTLTruck()
            {
                RegNo = "AAA-111",
                TruckWeight = 3500,
                CapacityWeight = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Normál",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,

                // futó túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                TaskID = "Szállítási feladat 1",
                IsOneWay = true,
                IsRunningTask = true,
                TimeFrom = DateTime.Now.Date.AddHours(10),                 //10:00
                LatFrom = 47.665,                                           //valahol Győr környéke
                LngFrom = 17.668,

                TimeTo = DateTime.Now.Date.AddHours(18),                   //18:00
                LatTo = 48.407,                                           //valahol Nyíregyháza környéke
                LngTo = 20.852,
                TimeFinish = DateTime.Now.Date.AddHours(19),               //19:00

                TimeCurr = DateTime.Now.Date.AddHours(11),                 //11:00
                LatCurr = 47.500,                                          //valahol Tatabánya környéke
                LngCurr = 18.558

            };

            FTLSupporter.FTLTruck trk2 = new FTLSupporter.FTLTruck()
            {
                RegNo = "BBB-222",
                TruckWeight = 12000,
                CapacityWeight = 10000,
                TruckType = "Hűtős",
                CargoTypes = "Normál,Extra",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,

                TruckTaskType = FTLTruck.eTruckTaskType.Available,
                TimeCurr = DateTime.Now.Date.AddHours(8),                 //Elérhatőség : 08:00
                LatCurr = 47.391,                                          //Bp, Ócsai u.
                LngCurr = 19.118
            
            };

            FTLSupporter.FTLTruck trk3 = new FTLSupporter.FTLTruck()
            {
                RegNo = "CCC-333",
                TruckWeight = 12000,
                CapacityWeight = 10000,
                TruckType = "Darus",
                CargoTypes = "Normál,Extra",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,

            
                // trevezett túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Planned,
                TaskID = "Tervezett zállítási feladat 2",
                IsOneWay = true,
                IsRunningTask = true,
                TimeFrom = DateTime.Now.Date.AddHours(16),                 //16:00
                LatFrom = 46.242,                                         //valahol Szeged
                LngFrom = 20.148,

                TimeTo = DateTime.Now.Date.AddHours(22),                  //22:00
                LatTo = 48.668,                                           //valahol Hatvan környéke
                LngTo = 19.668,
                TimeFinish = DateTime.Now.Date.AddHours(23),              //23:00

                /*
                TimeCurr = DateTime.Now.Date.AddHours(11),                //11:00
                LatCurr = 0,                                              //   valahol Tatabánya környéke
                LngCurr = 0
                */
            
            };

            List<FTLSupporter.FTLTruck> lstTrk = new List<FTLSupporter.FTLTruck>();
            lstTrk.Add( trk1);
            lstTrk.Add( trk2);
            lstTrk.Add( trk3);
            
            List<FTLSupporter.FTLResult> res = FTLSupporter.FTLInterface.FTLSupport(tsk, lstTrk,  "", dbConf);
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
}
