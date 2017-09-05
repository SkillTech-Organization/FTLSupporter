using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Map.LongProcess;

namespace PMap
{
    public static class Global
    {
        public static object lockObject = new object();
        public static object lockObjectNotify = new object();
        public static object lockObjectInit = new object();
        //        public static CalcRouteLocker lockObjectCalc = new CalcRouteLocker();
        public static object lockObjectCalc = new object();

        public const string PMapName = "PMap";
        public const string IniFileName = "PMap.ini";
        public const string IdFileName = "PMap.id";
        public const string LogFileName = "PMap.log";
        public const string MsgFileName = "PMap.msg";
        public const string DbgFileName = "PMap.dbg";
        public const string DbgSqlFileName = "PMap.sql";
        public const string ExcFileName = "PMap.exc";

        public const string PMapRegKey = "Software\\PMap";
        public const string PMapRegINI = "ini";
        public const string PMapRegLOG = "log";

        public const string iniPMap = "PMap";
        public const string iniIDFile = "IDFile";
        public const string iniCTIniFile = "CTIniFile";
        public const string iniLogDir = "LogDir";
        public const string iniLogVerbose = "LogVerbose";
        public const string iniDepCodeInToolTip = "DepCodeInToolTip";
        public const string iniTestMode = "TestMode";
        public const string iniParseLog = "ParseLog";
        public const string iniALog = "ALog";

        public const string iniPriority = "Priority";
        public const string iniInitRouteDataProcess = "InitRouteDataProcess";
        public const string iniCalcPMapRoutesByPlan = "CalcPMapRoutesByPlan";
        public const string iniCalcPMapRoutesByOrders = "CalcPMapRoutesByOrders";

        public const string iniGeocoding = "Geocoding";
        public const string iniGeocodeByGoogle = "GeocodeByGoogle";

        public const string iniRoute = "Route";
        public const string iniRouteThreadNum = "RouteThreadNum";
        public const string iniFastestPath = "FastestPath";
        public const string iniDestTraffic = "DestTraffic";
        public const string iniCutMapForRouting = "CutMapForRouting";
        public const string iniCutExtDegree = "CutExtDegree";

        public const string iniSpeeds = "Speeds";
        public const string iniSpeed = "Speed";

        public const string iniGMap = "GMap";
        public const string iniMapType = "MapType";
        public const string iniGoogleMapsAPIKey = "GoogleMapsAPIKey";
        public const string iniMapCacheMode = "MapCacheMode";
        public const string iniMapCacheDB = "MapCacheDB";


        public const string iniPlan = "Plan";
        public const string iniPlanFile = "PlanFile";
        public const string iniPlanResultFile = "PlanResultFile";
        public const string iniPlanAppl = "PlanAppl";
        public const string iniPlanArgs = "PlanArgs";
        public const string iniPlanOK = "PlanOK";
        public const string iniPlanErr = "PlanErr";
        public const string iniOptimizeTimeOutSec = "OptimizeTimeOutSec";
        public const string iniTrkMaxWorkTime = "TrkMaxWorkTime";
        public const string iniOrdVolumeMultiplier = "OrdVolumeMultiplier";
        public const string iniOrdCommentInTooltip = "OrdCommentInTooltip";

        public const string iniProxy = "Proxy";
        public const string UseProxy = "UseProxy";
        public const string ProxyServer = "Server";
        public const string ProxyPort = "Port";
        public const string ProxyUser = "User";
        public const string ProxyPassword = "Password";
        public const string ProxyDomain = "Domain";


        public const string iniDB = "DB";
        public const string iniDBConfigName = "DBConfigName";
        public const string iniDBServer = "DBServer";
        public const string iniDBName = "DBName";
        public const string iniDBUser = "DBUser";
        public const string iniDBPwd = "DBPwd";
        public const string iniDBCmdTimeOut = "DBCmdTimeOut";


        //Kezelt térképtípusok
        public const int mtGMap = 1;
        public const int mtOpenStreetMap = 2;

        public const int mtBingMap = 5;
        public const int mtYahooMap = 6;
        public const int mtOviMap = 7;
        public const int mtTest = 99;

        public const string selectorLayerName = "selector";
        public const string routeLayerName = "route";
        public const string depotsLayerName = "depots";
        public const string boundaryLayerName = "boundary";

        public const int DefZoom = 10;
        public const int DefMinZoom = 7;
        public const int DefMaxZoom = 18;

        public const double DefPosLat = 47.49194;
        public const double DefPosLng = 19.14414;

        //Túra útvonal megjelenítés szélességek
        public const int TourLineWidthNormal = 4;
        public const int TourLineWidthSelected = 8;


        //Túrapont típusok
        public const int TUT_VALUE_WHS_S = 0;       //Raktári felrakás (túra kezdete)
        public const int TUT_VALUE_WHS_E = 1;       //Raktári lerakás (túra vége)
        public const int TUT_VALUE_DEP = 2;         //Túrapont


        //Megrendeléstípusok
        public const int OTP_OUTPUT = 1;   //Kiszállítás
        public const int OTP_INPUT = 2;    //Beszállítás
        public const int OTP_LOAD = 4;     //Felrakás
        public const int OTP_UNLOAD = 3;   //Lerakás

        //Túraponttípusok
        public const int PTP_TYPE_WHS_S = 0;     //Raktári felrakás (túra kezdete) Raktárból ki
        public const int PTP_TYPE_WHS_E = 1;     //Raktári lerakás (túra vége) Raktárba vissza
        public const int PTP_TYPE_DEP = 2;       //Túrapont, le/felrakás 

        public const int TPS_VALUE_NOTEXEC = 0;
        public const int TPS_VALUE_OK = 1;
        public const int TPS_VALUE_LATED = 2;
        public const int TPS_VALUE_DEL = 3;

        public const int TRS_VALUE_PUB = 1;
        public const int TRS_VALUE_RUNNING = 2;
        public const int TRS_VALUE_COMPLETED = 3;
        public const int TRS_VALUE_CANCELED = 4;

        public const int TCT_VALUE_ARR = 1;
        public const int TCT_VALUE_SRV = 2;
        public const int TCT_VALUE_DEP = 3;

        public const int RST_NORESTRICT = 1;    //1:korlátozás nélküli
        public const int RST_BIGGER12T = 2;     //2:12 tonnánál több
        public const int RST_MAX12T = 3;        //3:max 12 tonna
        public const int RST_MAX75T = 4;        //4:max 7.5 tonna
        public const int RST_MAX35T = 5;        //5.max 3.5 tonna

        public const int CTP_VALUE_DRY = 1;      //TODO:Ez csak a Pfenninges alkalmazásnál volt igaz. Árutípus-törzs mezővel kell megoldani!

        public static DateTime SQLMINDATE = new DateTime(1753, 1, 1);

        public const double ROUTE_APPROACH = 0.002;    // 

        //Térképre illesztés paraméterek (lehet, hogy ini paraméterbe ki kell ezeket tenni)
        //
        public const int NearestNOD_ID_Approach = 40000;         //Mekkora körzetben keressen lehetséges node-okat
        public const int NearestNOD_ID_ApproachBig = 180000;     //Nagyobb körzet a II. menetes keresésnek
        public const int EdgeApproachCity = 1500;                //Közelítő tűrése városon belül (EDG.RDT_VALUE=6 or EDG.EDG_STRNUM1!=0 or EDG.EDG_STRNUM2!=0 or EDG.EDG_STRNUM3!=0 or EDG.EDG_STRNUM4!=0)
        public const int EdgeApproachHighway = 30000;            //Közelítő tűrése városon kivül !(EDG.RDT_VALUE=6 or EDG.EDG_STRNUM1!=0 or EDG.EDG_STRNUM2!=0 or EDG.EDG_STRNUM3!=0 or EDG.EDG_STRNUM4!=0)


        public const int LatLngDivider = 1000000;               //Adatbázis koordináta -> GPS koordiánta osztó

        public const int CostDivider = 10;

        public const int csQTY_DEC = 100;
        public const int csQTYSRVDivider = 10;                  //10 kg-onként van kiszolgálási idő megadva
        public const int csVolumeMultiplier = 1000;             //m3->dm3 konverzióhoz

        public const string NUMFORMAT = "#,#0.00";
        public const string INTFORMAT = "#,#0";
        public const string DATEFORMAT = "yyyy.MM.dd";
        public const string DATETIMEFORMAT = "yyyy.MM.dd HH:mm:ss";
        public const string DATETIMEFORMAT_PLAN = "yyyy.MM.dd HH:mm";

        public const double VAT = 1.27;

        public const string SMP_PLAN = "PLAN";

        public const string SEP_EDGE = ",";
        public const string SEP_POINT = "|";
        public const string SEP_COORD = "#";

        public const char SEP_EDGEC = ',';
        public const char SEP_POINTC = '|';
        public const char SEP_COORDC = '#';

        public const int defWeather = 1;

        public static Color SELECTEDBTNCOLOR = System.Drawing.Color.Gold;
        public static Color MANDATORYCOLOR = Color.OldLace;
        public static Color DISABLEDCOLOR = Color.AliceBlue;
        public static Color DISABLEDFONTCOLOR = Color.Black;
        public static Color UNPLANNEDITEMCOLOR = Color.Azure;

        public static string ETOLLCAT_Prefix = "J";
        public const int ETOLLCAT_MAX35T = 1;         //1.max 3.5 tonna (nics útdíj)
        public const int ETOLLCAT_MAX75T = 2;         //2:max 7.5 tonna
        public const int ETOLLCAT_MAX12T = 3;         //3:max 12 tonna
        public const int ETOLLCAT_BIGGER12T = 4;      //4:12 tonnánál több

    }
}
