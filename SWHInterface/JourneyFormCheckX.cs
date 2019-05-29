using PMapCore.BLL.DataXChange;
using PMapCore.BO.DataXChange;
using PMapCore.Common;
using PMapCore.Licence;
using PMapCore.LongProcess;
using PMapCore.LongProcess.Base;
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

                        };
                        resultArr.Add(trkErrRes);
                        return resultArr;
                    }
                }

                //1. geokódolás
                ProcessNotifyIcon ni = new ProcessNotifyIcon();
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
                        errRes.ErrMessage = errResult;
                        resultArr.Add(errRes);

                        sRetStatus = retErr;
                    }
                }
                if (sRetStatus == retErr)
                {
                    return resultArr;
                }


                //2. RouteData.Instance singleton feltoltese

                InitRouteDataProcess irdp = new InitRouteDataProcess(ni);
                irdp.RunWait();

                //Az utolsó elemre rárakjuk a finish-t
                p_lstRouteSection.Last().RouteSectionType = boXRouteSection.ERouteSectionType.Finish;


                //
                JourneyFormDataProcess rvdp = new JourneyFormDataProcess(ni, p_lstRouteSection, p_XTruck);
                rvdp.RunWait();

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
                }
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


    }
}
