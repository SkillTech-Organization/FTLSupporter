using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using GMap.NET.MapProviders;
using System.Net;

namespace PMapCore.Common
{
    public sealed class PMapIniParams
    {

        public enum eLogVerbose
        {
            nolog = 0,      //no logging
            normal = 1,     //normal (default)
            debug = 2,      //additional debugging information + .DBG debug console log
            sql = 3,        //debugging information + sql log into .SQL 
        }


        //Útvonalszámítás sebességprofil default értékek
        private static int[] aSpeedDefaults = 
        {
            70,  //Autópálya
		    50,  //Autóút
			40,  //Fõútvonal
			35,  //Mellékút
	        25,  //Egyéb alárendelt út
			15,  //Város (utca)
	        15   //Fel/lehajtók, rámpák
        };

        public bool  Loaded { get; private set; }

        public string IDFile { get; private set; }
        public string CTIniFile { get; private set; }
        public string LogDir { get; private set; }
        public eLogVerbose LogVerbose { get; private set; }
        public bool TestMode { get; private set; }
        public bool ParseLog { get; private set; }
        public bool ALog { get; private set; }
        public bool TourRoute { get; private set; }             //Egyedi túraútvonalak
        public string TourpointToolTip { get; private set; }    //Túrapont tooltip
        public string TruckCode { get; private set; }           //Járműkód
        public int RoutesExpire { get; private set; }           //DST_DISTANCE érvényességei
        public double WeightAreaDegree { get; private set; }       //Lerakó környéke súlykrolátozás gyűjtése fokban. 1 fok Ez É-D irányban 111 km, K-Ny irányban kb 40-54 km.


        public string AzureAccount { get; private set; }        //Felhőbe küldés tablestore account név (a Key a licence-ből jön !)

        public string AuthTokenCryptAESKey { get; private set; }
        public string AuthTokenCryptAESIV { get; private set; }
        public string WebLoginTemplate { get; private set; }
        public string WebLoginSenderEmail { get; private set; }
        public string WebDriverTemplate { get; private set; }
        public string WebDriverSenderEmail { get; private set; }

        public ThreadPriority InitRouteDataProcess { get; private set; }
        public ThreadPriority CalcPMapRoutesByPlan { get; private set; }
        public ThreadPriority CalcPMapRoutesByOrders { get; private set; }


        public bool GeocodingByGoogle { get; private set; }

        public int RouteThreadNum { get; private set; }
        public bool FastestPath { get; private set; }
        public bool DestTraffic { get; private set; }
        public bool CutMapForRouting { get;  set; }           //Útvonalszámítás vágja-e a térképet?
        public double CutExtDegree { get; private set; }               //A kivágásnál mekkora ráhagyással kel dolgozni? (fokban megadva)
        public int CalcPMapRoutesMemTreshold { get; private set; }      //Útvonalszámítás memória KB-ben 

        public Dictionary<int, int> dicSpeed { get; private set; }

        public int MapType { get; private set; }

        public string GoogleMapsAPIKey { get; private set; }
        public AccessMode MapCacheMode { get; private set; }
        public string MapCacheDB { get; private set; }

        public string PlanFile { get; private set; }
        public string PlanResultFile { get; private set; }
        public string PlanAppl { get; private set; }
        public string PlanArgs { get; private set; }
        public string PlanOK { get; private set; }
        public string PlanErr { get; private set; }
        public int OptimizeTimeOutSec { get; private set; }
        public int TrkMaxWorkTime { get; private set; }
        public double OrdVolumeMultiplier { get; private set; }

        public bool UseProxy { get; private set; }
        public string ProxyServer { get; private set; }
        public int ProxyPort { get; private set; }
        public string ProxyUser { get; private set; }
        public string ProxyPassword { get; private set; }
        public string ProxyDomain { get; private set; }


        public string IniPath { get; private set; }

        //CT inifájlból beolvasott paraméterek
        public string DBConf { get; set; }                 //az adatbázis konfigurációt tartalmazó ini csoport
        public string DBConfigName { get; set; }           //(ezek a paraméterek kivülről is feltölthetőek !)
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string DBUser { get; set; }
        public string DBPwd { get; set; }
        public int DBCmdTimeOut { get; set; }

        //Mapei paraméterek
        //
        public int MapeiOpen { get; set; }
        public int MapeiClose { get; set; }
        public int MapeiSrvTime { get; set; }
        public double MapeiQtySrvTime { get; set; }
        public string MapeiDefCargoType { get; set; }
        public int MapeiSumOrderKg { get; set; }

        //Lazy objects are thread safe, double checked and they have better performance than locks.
        //see it: http://csharpindepth.com/Articles/General/Singleton.aspx
        private static readonly Lazy<PMapIniParams> m_instance = new Lazy<PMapIniParams>(() => new PMapIniParams(), true);

 
        static public PMapIniParams Instance                //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                return m_instance.Value;            //It's thread safe!
            }
        }

        private PMapIniParams()
        {
            Loaded = false;
        }

        public void ReadParams(string p_iniPath, string p_dbConf, string p_iniFileName = "PMap.ini")
        {
  
            if (p_iniPath == "")
                p_iniPath = Path.GetDirectoryName(Application.ExecutablePath);
            IniPath = p_iniPath;
            DBConf = p_dbConf;

            //            MessageBox.Show("p_iniPath=" + p_iniPath);

            /*
            RegistryKey key = Registry.LocalMachine.OpenSubKey(Global.PMapRegKey, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            // If the return value is null, the key doesn't exist
            if (key != null)
                iniPath = key.GetValue(Global.PMapRegINI, iniPath).ToString();
            else
                iniPath = Path.GetDirectoryName(Application.ExecutablePath);
             */


            INIFile ini = new INIFile(Path.Combine(p_iniPath, p_iniFileName));

            IDFile = ini.ReadString(Global.iniPMap, Global.iniIDFile);
            CTIniFile = ini.ReadString(Global.iniPMap, Global.iniCTIniFile);




            LogDir = ini.ReadString(Global.iniPMap, Global.iniLogDir);
            if (LogDir != "")
            {
                if (LogDir.Substring(LogDir.Length - 1, 1) != "\\")
                    LogDir += "\\";
            }
            else
                LogDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";


            string sLogVerbose = ini.ReadString(Global.iniPMap, Global.iniLogVerbose);
            if (sLogVerbose != "")
                LogVerbose = (eLogVerbose)Enum.Parse(typeof(eLogVerbose), sLogVerbose);
            else
                LogVerbose = eLogVerbose.normal;


            //Debug átirányítása
            //
            if (LogVerbose >= PMapIniParams.eLogVerbose.debug)
            {
                string DbgFileName = Path.Combine(LogDir, Global.DbgFileName);

                TextWriterTraceListener[] listeners = new TextWriterTraceListener[] 
                {
                new TextWriterTraceListener(DbgFileName),
                new TextWriterTraceListener(Console.Out)
                };
                Debug.Listeners.AddRange(listeners);
            }

            string sTestMode = ini.ReadString(Global.iniPMap, Global.iniTestMode);
            TestMode = (sTestMode == "1" || sTestMode.ToLower() == "true");

            string sParseLog = ini.ReadString(Global.iniPMap, Global.iniParseLog);
            ParseLog = (sParseLog == "1" || sParseLog.ToLower() == "true");

            string sALog = ini.ReadString(Global.iniPMap, Global.iniALog);
            ALog = (sALog == "1" || sALog.ToLower() == "true");

            string sTourRoute = ini.ReadString(Global.iniPMap, Global.iniTourRoute);
            TourRoute = (sTourRoute == "1" || sTourRoute.ToLower() == "true");



            string sTourpointToolTip = ini.ReadString(Global.iniPMap, Global.iniTourpointToolTip);
            if( string.IsNullOrWhiteSpace(sTourpointToolTip))
            {

          //      sTourpointToolTip = "DEP_CODE + '  ' + DEP_NAME + '\\n' + CAST(ZIP.ZIP_NUM  AS VARCHAR) + ' ' + ZIP.ZIP_CITY + ' ' +DEP_ADRSTREET";
                sTourpointToolTip = "DEP_CODE + '  ' + DEP_NAME + '\\n' + CAST(ZIP.ZIP_NUM  AS VARCHAR) + ' ' + ZIP.ZIP_CITY + ' ' +DEP_ADRSTREET + " +
                        "'\\nTérfogat:'+CAST(ORD_VOLUME AS VARCHAR)+', Mennyiség:' + CAST(TOD_QTY AS VARCHAR)+'\\n'+ ORD_COMMENT";
            }
            TourpointToolTip = sTourpointToolTip;
            //ToolTipText = (PMapIniParams.Instance.DepCodeInToolTip ? Util.getFieldValue<string>(p_dr, "DEP_CODE") + "  " : "") + Util.getFieldValue<string>(p_dr, "DEP_NAME") + "\n" + Util.getFieldValue<int>(p_dr, "ZIP_NUM") + " " + Util.getFieldValue<string>(p_dr, "ZIP_CITY") + " " + Util.getFieldValue<string>(p_dr, "DEP_ADRSTREET"),
            string sTruckCode = ini.ReadString(Global.iniPMap, Global.iniTruckCode);
            if (string.IsNullOrWhiteSpace(sTruckCode))
            {

                sTruckCode = "TRK_REG_NUM + case when isnull(TRK_TRAILER, '') <> '' then '/' + TRK_TRAILER else '' end";
            }
            TruckCode = sTruckCode;


            string sRoutesExpire = ini.ReadString(Global.iniPMap, Global.iniRoutesExpire);
            if (sRoutesExpire != "")
                RoutesExpire = Convert.ToInt32(sRoutesExpire);
            else
                RoutesExpire = -1;


            string sWeightAreaDegree = ini.ReadString(Global.iniPMap, Global.iniWeightAreaDegree);
            WeightAreaDegree = Convert.ToDouble("0" + sWeightAreaDegree.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (WeightAreaDegree <= 0)
                WeightAreaDegree = Global.WEIGHTAREA_DEGREE;

            AzureAccount = ini.ReadString(Global.iniWeb, Global.iniAzureAccount);                   //A Web -es feltöltés AzureAccount-ja ini-ben, a Key a licence-ben van!
            AuthTokenCryptAESKey = ini.ReadString(Global.iniWeb, Global.iniAuthTokenCryptAESKey);
            AuthTokenCryptAESIV = ini.ReadString(Global.iniWeb, Global.iniAuthTokenCryptAESIV);
            WebLoginTemplate = ini.ReadString(Global.iniWeb, Global.iniWebLoginTemplate);
            WebDriverTemplate = ini.ReadString(Global.iniWeb, Global.iniWebDriverTemplate);
            WebLoginSenderEmail = ini.ReadString(Global.iniWeb, Global.iniWebDriverSenderEmail);
 
            string sInitRouteDataProcess = ini.ReadString(Global.iniPriority, Global.iniInitRouteDataProcess);
            if (sInitRouteDataProcess != "")
                InitRouteDataProcess = (ThreadPriority)Enum.Parse(typeof(ThreadPriority), sInitRouteDataProcess);
            else
                InitRouteDataProcess = ThreadPriority.Normal;


            string sCalcPMapRoutesByPlan = ini.ReadString(Global.iniPriority, Global.iniCalcPMapRoutesByPlan);
            if (sCalcPMapRoutesByPlan != "")
                CalcPMapRoutesByPlan = (ThreadPriority)Enum.Parse(typeof(ThreadPriority), sCalcPMapRoutesByPlan);
            else
                CalcPMapRoutesByPlan = ThreadPriority.Normal;

            string sCalcPMapRoutesByOrders = ini.ReadString(Global.iniPriority, Global.iniCalcPMapRoutesByOrders);
            if (sInitRouteDataProcess != "")
                CalcPMapRoutesByOrders = (ThreadPriority)Enum.Parse(typeof(ThreadPriority), sCalcPMapRoutesByOrders);
            else
                CalcPMapRoutesByOrders = ThreadPriority.Normal;

            string sGeocodingByGoogle = ini.ReadString(Global.iniGeocoding, Global.iniGeocodeByGoogle);
            GeocodingByGoogle = (sGeocodingByGoogle == "1" || sGeocodingByGoogle.ToLower() == "true");


            string sRouteThreadNum = ini.ReadString(Global.iniRoute, Global.iniRouteThreadNum);
            if (sRouteThreadNum != "")
                RouteThreadNum = Convert.ToInt32(sRouteThreadNum);
            else
                RouteThreadNum = 1;

            string sFastestPath = ini.ReadString(Global.iniRoute, Global.iniFastestPath);
            FastestPath = (sFastestPath == "1" || sFastestPath.ToLower() == "true");

            string sDestTraffic = ini.ReadString(Global.iniRoute, Global.iniDestTraffic);
            DestTraffic = (sDestTraffic == "1" || sDestTraffic.ToLower() == "true");

            string sCutMapForRouting = ini.ReadString(Global.iniRoute, Global.iniCutMapForRouting);
            CutMapForRouting = (sCutMapForRouting == "1" || sCutMapForRouting.ToLower() == "true");

            string sCutExtDegree = ini.ReadString(Global.iniRoute, Global.iniCutExtDegree);
            CutExtDegree = Convert.ToDouble("0" + sCutExtDegree.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (CutExtDegree <= 0)
                CutExtDegree = 0.05;


            string sCalcPMapRoutesMemTreshold = ini.ReadString(Global.iniRoute, Global.CalcPMapRoutesMemTreshold);
            if (sCalcPMapRoutesMemTreshold != "")
                CalcPMapRoutesMemTreshold = Convert.ToInt32(sCalcPMapRoutesMemTreshold);
            else
                CalcPMapRoutesMemTreshold = 100;


            dicSpeed = new Dictionary<int, int>();
            for (int i = 1; i <= 7; i++)
            {
                string sSpeed = ini.ReadString(Global.iniSpeeds, Global.iniSpeed + i.ToString());
                if (sSpeed != "")
                    dicSpeed.Add(i, Convert.ToInt32(sSpeed));
                else
                    dicSpeed.Add(i, aSpeedDefaults[i - 1]);
            }



            string sMapType = ini.ReadString(Global.iniGMap, Global.iniMapType);
            if (sMapType != "")
                MapType = Convert.ToInt32(sMapType);
            else
                MapType = Global.mtGMap;
            
            switch (MapType)
            {
                case Global.mtGMap:
                    PMapCommonVars.Instance.MapProvider = GMapProviders.GoogleTerrainMap;
                    break;
                case Global.mtOpenStreetMap:
                    PMapCommonVars.Instance.MapProvider = GMapProviders.OpenStreetMap;
                    break;
                /*
                 Ezekre nics útvonalszámítás implementálva
                case Global.mtBingMap:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.BingMap;
                    break;
                case Global.mtYahooMap:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.YahooMap;
                    break;
                case Global.mtOviMap:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.OviMap;
                    break;
                case Global.mtTest:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.YandexMap;
                    break;
                 
                 
                 */

                default:
                    PMapCommonVars.Instance.MapProvider = GMapProviders.GoogleTerrainMap;
                    break;

            }


            GoogleMapsAPIKey = ini.ReadString(Global.iniGMap, Global.iniGoogleMapsAPIKey);
            GoogleMapProvider.Instance.APIKey = GoogleMapsAPIKey;


            MapCacheMode = AccessMode.ServerOnly;
            string sAccesMode = ini.ReadString(Global.iniGMap, Global.iniMapCacheMode);
            if (sAccesMode != "")
                MapCacheMode = (AccessMode)Enum.Parse(typeof(AccessMode), sAccesMode);

            PMapCommonVars.Instance.MapAccessMode = MapCacheMode;


            MapCacheDB = ini.ReadString(Global.iniGMap, Global.iniMapCacheDB);
            if (MapCacheDB != "")
            {
                if (MapCacheDB.Substring(MapCacheDB.Length - 1, 1) != "\\")
                    MapCacheDB += "\\";
            }

            PlanFile = ini.ReadString(Global.iniPlan, Global.iniPlanFile);
            PlanResultFile = ini.ReadString(Global.iniPlan, Global.iniPlanResultFile);
            PlanAppl = ini.ReadString(Global.iniPlan, Global.iniPlanAppl);
            PlanArgs = ini.ReadString(Global.iniPlan, Global.iniPlanArgs);
            PlanOK = ini.ReadString(Global.iniPlan, Global.iniPlanOK);
            PlanErr = ini.ReadString(Global.iniPlan, Global.iniPlanErr);
            OptimizeTimeOutSec = Convert.ToInt32("0" + ini.ReadString(Global.iniPlan, Global.iniOptimizeTimeOutSec));
            if (OptimizeTimeOutSec < 60)
                OptimizeTimeOutSec = 60;

            TrkMaxWorkTime = Convert.ToInt32("0" + ini.ReadString(Global.iniPlan, Global.iniTrkMaxWorkTime));
            if (TrkMaxWorkTime == 0)
                TrkMaxWorkTime = 1440;
           

            OrdVolumeMultiplier = Convert.ToDouble("0" + ini.ReadString(Global.iniPlan, Global.iniOrdVolumeMultiplier).Replace(',', '.'), CultureInfo.InvariantCulture);
            if (OrdVolumeMultiplier == 0)
                OrdVolumeMultiplier = 0.001;         //alapértelmezés 0.001 a dm3 --> m3 konverzióhoz

            string sUseProxy = ini.ReadString(Global.iniProxy, Global.UseProxy);
            UseProxy = (sUseProxy == "1" || sUseProxy.ToLower() == "true");
            if (UseProxy)
            {
                ProxyServer = ini.ReadString(Global.iniProxy, Global.ProxyServer);
                string sProxyPort = ini.ReadString(Global.iniProxy, Global.ProxyPort);
                if (sProxyPort != "")
                    ProxyPort = Convert.ToInt32(sProxyPort);
                ProxyUser = ini.ReadString(Global.iniProxy, Global.ProxyUser);
                ProxyPassword = ini.ReadString(Global.iniProxy, Global.ProxyPassword);
                ProxyDomain = ini.ReadString(Global.iniProxy, Global.ProxyDomain);

                GMapProvider.WebProxy = new WebProxy(ProxyServer, ProxyPort);
                GMapProvider.WebProxy.Credentials = new NetworkCredential(ProxyUser, ProxyPassword, ProxyDomain);

            }
            else
            {
                GMapProvider.WebProxy = null;
            }
                

            // DB paraméterek felolvasása CT inifájlból

            INIFile iniCT = new INIFile(CTIniFile);
            DBConfigName = iniCT.ReadString(DBConf, Global.iniDBConfigName);
            DBServer = iniCT.ReadString(DBConf, Global.iniDBServer);
            DBName = iniCT.ReadString(DBConf, Global.iniDBName);
            DBUser = iniCT.ReadString(DBConf, Global.iniDBUser);
            DBPwd = iniCT.ReadString(DBConf, Global.iniDBPwd);
            DBCmdTimeOut = Convert.ToInt32("0" + iniCT.ReadString(DBConf, Global.iniDBCmdTimeOut));
            if (DBCmdTimeOut == 0)
                DBCmdTimeOut = 60;


            // MAPEI paraméterek

            MapeiOpen = Convert.ToInt32("0" + ini.ReadString(Global.iniMapei, Global.iniMapeiOpen));
            MapeiClose = Convert.ToInt32("0" + ini.ReadString(Global.iniMapei, Global.iniMapeiClose));
            MapeiSrvTime = Convert.ToInt32("0" + ini.ReadString(Global.iniMapei, Global.iniMapeiSrvTime));
            MapeiQtySrvTime = Convert.ToDouble("0" + ini.ReadString(Global.iniMapei, Global.iniMapeiQtySrvTime).Replace(',', '.'), CultureInfo.InvariantCulture);
            MapeiDefCargoType = ini.ReadString(Global.iniMapei, Global.iniMapeiDefCargoType);
            MapeiSumOrderKg = Convert.ToInt32("0" + ini.ReadString(Global.iniMapei, Global.iniMapeiSumOrderKg));
            if (MapeiSumOrderKg <= 0)
                MapeiSumOrderKg = 23500;
            Loaded = true;


        }

    }
}
