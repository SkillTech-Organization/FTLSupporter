using FTLSupporter;
using PMapCore.BLL;
using PMapCore.BLL.DataXChange;
using PMapCore.BO.DataXChange;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.Licence;
using PMapCore.LongProcess;
using PMapCore.LongProcess.Base;
using PMapCore.Route;
using PMapCore.Strings;
using SWHInterface.BO;
using SWHInterface.LongProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHInterface
{

    public static class JourneyFormCheckX
    {
        private const string retOK = "OK";
        private const string retCancel = "CANCEL";
        private const string retErr = "ERROR";
        private const string retFailed = "FAILED";

        /// <summary>
        /// Menetlevél ellenőrzés BATCH
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_lstRouteSection"></param>
        /// <param name="p_XTruck"></param>
        /// <returns></returns>
        internal static List<dtXResult> Process(string p_iniPath, string p_dbConf, List<boXRouteSection> p_lstRouteSection, boXTruck p_XTruck)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            List<dtXResult> resultArr = new List<dtXResult>();

            try
            {
                Util.Log2File(">>START:JourneyFormCheck( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_lstDepotID.<count>='" + p_lstRouteSection.Count.ToString() + ",p_XTruck=" + p_XTruck.ToString() + "')");

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);

                var DB = new SQLServerAccess();
                DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                var bllRoute = new bllRoute(DB);
                var etbllEtoll = new bllEtoll(DB);

                PMapCommonVars.Instance.LstEToll = etbllEtoll.GetAllEtolls();


                //kicsit átverjük a validátort. Feltöltjük azokat a mezőket, amleyek egyébként kötelezőek, de nem szükésges a 
                //menetlevél ellenőrzéshez
                p_XTruck.TRK_CODE = string.IsNullOrWhiteSpace(p_XTruck.TRK_CODE) ? "TRK_CODE" : p_XTruck.TRK_CODE;
                p_XTruck.TRK_REG_NUM = string.IsNullOrWhiteSpace(p_XTruck.TRK_REG_NUM) ? "TRK_REG_NUM" : p_XTruck.TRK_REG_NUM;
                p_XTruck.CRR_CODE = string.IsNullOrWhiteSpace(p_XTruck.CRR_CODE) ? "CRR_CODE" : p_XTruck.CRR_CODE;
                p_XTruck.WHS_CODE = string.IsNullOrWhiteSpace(p_XTruck.WHS_CODE) ? "WHS_CODE" : p_XTruck.WHS_CODE;
                p_XTruck.CPP_LOADQTY = p_XTruck.CPP_LOADQTY == 0 ? 1 : p_XTruck.CPP_LOADQTY;
                p_XTruck.CPP_LOADVOL = p_XTruck.CPP_LOADVOL == 0 ? 1 : p_XTruck.CPP_LOADVOL;
                p_XTruck.TFP_KMCOST = p_XTruck.TFP_KMCOST == 0 ? 1 : p_XTruck.TFP_KMCOST;
                p_XTruck.TFP_HOURCOST = p_XTruck.TFP_HOURCOST == 0 ? 1 : p_XTruck.TFP_HOURCOST;

                /* Ezeket a mezőket kötelező kitölteni :
                    kell TRK_WEIGHT 
                    kell TRK_XHEIGHT 
                    kell TRK_XWIDTH 
                    kell TRK_ETOLLCAT 
                    kell TRK_ENGINEEURO 
                    kell SPV_VALUE1
                    kell SPV_VALUE2
                    kell SPV_VALUE3
                    kell SPV_VALUE4
                    kell SPV_VALUE5
                    kell SPV_VALUE6
                    kell SPV_VALUE7
                */

                List<ObjectValidator.ValidationError> validationErros = ObjectValidator.ValidateObject(p_XTruck);
                if (validationErros.Count > 0)
                {
                    foreach (var err in validationErros)
                    {
                        dtXResult trkErrRes = new dtXResult()
                        {
                            ItemNo = 0,
                            Field = err.Field,
                            Status = dtXResult.EStatus.VALIDATIONERROR,
                            ErrMessage = err.Message
                            //Data = p_XTruck

                        };
                        resultArr.Add(trkErrRes);
                        return resultArr;
                    }
                }

                var ItemNo = 0;
                foreach (var item in p_lstRouteSection)
                {
                    var validationErrosRoute = ObjectValidator.ValidateObject(item);
                    if (validationErrosRoute.Count > 0)
                    {
                        foreach (var err in validationErrosRoute)
                        {
                            dtXResult routeErrRes = new dtXResult()
                            {
                                ItemNo = ItemNo,
                                Field = err.Field,
                                Status = dtXResult.EStatus.VALIDATIONERROR,
                                ErrMessage = err.Message
                                //Data = item
                            };
                            resultArr.Add(routeErrRes);
                            sRetStatus = retErr;
                        }
                    }
                    ItemNo++;
                }

                if (sRetStatus == retErr)
                {
                    return resultArr;
                }


                ProcessNotifyIcon ni = new ProcessNotifyIcon();

                /* NEM KELL GEOKÓDOLÁS !! HElyette térképre illesztés
                //1. geokódolás - 
                DepotGeocodingProcess dp = new DepotGeocodingProcess(ni, p_lstRouteSection);
                dp.RunWait();

                //1.1 geokódolás eredménye
                string errResult = "";
                foreach (var item in p_lstRouteSection)
                {


                    if (item.EDG_ID <= 0)
                    {
                        dtXResult errRes = new dtXResult();
                        errRes.Status = dtXResult.EStatus.ERROR;
                        errRes.Field = item.itemRes.Field;
                        errRes.ErrMessage = item.itemRes.ErrMessage;
                        resultArr.Add(errRes);

                        sRetStatus = retErr;
                    }
                }
                if (sRetStatus == retErr)
                {
                    return resultArr;
                }
                */

                //2. RouteData.Instance singleton feltoltese
                var dtXDate = DateTime.Now;
                InitRouteDataProcess irdp = new InitRouteDataProcess(ni);
                irdp.RunWait();
                Util.Log2File(String.Format("JourneyFormCheck térkép feltöltés időtartam:{0}", (DateTime.Now - dtXDate).ToString()));
                //gyors térképre illesztéshez...
                dtXDate = DateTime.Now;
                var EdgesArr = RouteData.Instance.Edges.Select(s => s.Value).ToArray();
                Util.Log2File(String.Format("JourneyFormCheck térkép tömb előkészítés időtartam:{0}", (DateTime.Now - dtXDate).ToString()));


                string RZN_ID_LIST = "";
                if (p_XTruck.TRK_WEIGHT <= Global.RST_WEIGHT35)
                    RZN_ID_LIST = GetRestZonesByRST_ID(bllRoute, Global.RST_MAX35T);
                else if (p_XTruck.TRK_WEIGHT <= Global.RST_WEIGHT75)
                    RZN_ID_LIST = GetRestZonesByRST_ID(bllRoute, Global.RST_MAX75T);
                else if (p_XTruck.TRK_WEIGHT <= Global.RST_WEIGHT120)
                    RZN_ID_LIST = GetRestZonesByRST_ID(bllRoute, Global.RST_MAX12T);
                else if (p_XTruck.TRK_WEIGHT > Global.RST_WEIGHT120)
                    RZN_ID_LIST = GetRestZonesByRST_ID(bllRoute, Global.RST_BIGGER12T);
                else
                    RZN_ID_LIST = GetRestZonesByRST_ID(bllRoute, Global.RST_NORESTRICT);

                ItemNo = 0;


                dtXDate = DateTime.Now;

                foreach (var item in p_lstRouteSection)
                {
                    int NOD_ID = 0;
                    var pt = new GMap.NET.PointLatLng(item.Lat, item.Lng);
                    var ptKey = pt.ToString();
                    if (FTLNodePtCache.Instance.Items.ContainsKey(ptKey))
                    {

                        NOD_ID = FTLNodePtCache.Instance.Items[ptKey];
                    }
                    else
                    {
                        NOD_ID = FTLSupporter.FTLInterface.GetNearestReachableNOD_IDForTruck_FAST(EdgesArr, pt, RZN_ID_LIST, p_XTruck.TRK_WEIGHT, p_XTruck.TRK_HEIGHT, p_XTruck.TRK_WIDTH);
                        if(NOD_ID > 0)
                            FTLNodePtCache.Instance.Items.TryAdd(ptKey, NOD_ID);
                    }
                    if (NOD_ID > 0)
                    {
                        var edg = bllRoute.GetEdgeByNOD_ID(NOD_ID);
                        item.NOD_ID = NOD_ID;
                        item.EDG_ID = edg.ID;
                            }
                    else
                    {
                        dtXResult errRes = new dtXResult();
                        errRes.Status = dtXResult.EStatus.ERROR;
                        //errRes.Field = item.itemRes.Field;
                        errRes.ErrMessage = PMapMessages.E_JRNFORM_WRONGLATLNG;
                        //errRes.Data = item;
                        errRes.ItemNo = ItemNo;
                        resultArr.Add(errRes);

                        sRetStatus = retErr;
                    }
                    ItemNo++;
                }

                Util.Log2File(String.Format("JourneyFormCheck térképre illesztés időtartam:{0}", (DateTime.Now - dtXDate).ToString()));

                if (sRetStatus == retErr)
                {
                    return resultArr;
                }
                //Az utolsó elemre rárakjuk a finish-t
                p_lstRouteSection.Last().RouteSectionType = boXRouteSection.ERouteSectionType.Finish;


                //
                dtXDate = DateTime.Now;
                JourneyFormDataProcess rvdp = new JourneyFormDataProcess(ni, p_lstRouteSection, p_XTruck, RZN_ID_LIST);
                rvdp.RunWait();
                Util.Log2File(String.Format("JourneyFormCheck feldolgozás időtartam:{0}", (DateTime.Now - dtXDate).ToString()));

                if (rvdp.Result != null)
                {
                    dtXResult OKRes = new dtXResult()
                    {
                        ItemNo = 0,
                        Field = "",
                        Status = dtXResult.EStatus.OK,
                        ErrMessage = "",
                        Data = rvdp.Result
                    };
                    resultArr.Add(OKRes);
                }
                else
                {
                    dtXResult errRes = new dtXResult()
                    {
                        ItemNo = 0,
                        Field = "",
                        Status = dtXResult.EStatus.ERROR,
                        ErrMessage = PMapMessages.E_JRNFORM_NORESULT
                    };
                    resultArr.Add(errRes);
                    Util.Log2File(">>ERROR:JourneyFormCheck()");
                }
                Util.Log2File(">>END  :JourneyFormCheck() teljes időtartam:{0}", (DateTime.Now - dt).ToString());

                return resultArr;
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                dtXResult expRes = new dtXResult();
                expRes.Status = dtXResult.EStatus.EXCEPTION;
                expRes.ErrMessage = e.Message;
                sRetStatus = retErr;
                resultArr.Add(expRes);
            }
            //Util.Log2File(">>END:JourneyFormCheck()");
            return resultArr;
        }

        private static string GetRestZonesByRST_ID(bllRoute p_bllRoute, int p_RST)
        {
            string RZN_ID_LIST = "";
            if (PMapCommonVars.Instance.RZN_ID_LISTCahce.ContainsKey(p_RST))
            {
                RZN_ID_LIST = PMapCommonVars.Instance.RZN_ID_LISTCahce[p_RST];
            }
            else
            {
                RZN_ID_LIST = p_bllRoute.GetRestZonesByRST_ID(p_RST);
                PMapCommonVars.Instance.RZN_ID_LISTCahce.Add(p_RST, RZN_ID_LIST);
            }
            return RZN_ID_LIST;
        }



    }
}
