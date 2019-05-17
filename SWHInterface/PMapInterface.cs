using GMap.NET;
using PMapCore.BLL;
using PMapCore.BLL.DataXChange;
using PMapCore.BO;
using PMapCore.BO.DataXChange;
using PMapCore.Common;
using PMapCore.Common.Parse;
using PMapCore.Common.PPlan;
using PMapCore.Licence;
using PMapCore.LongProcess.Base;
using PMapCore.MapProvider;
using PMapCore.Route;
using SWHInterface.LongProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHInterface
{
    public class PMapInterface
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
        /// <param name="p_TRK_ID"></param>
        /// <param name="p_GetRouteWithTruckSpeeds"></param>
        /// <param name="p_CalcTRK_ETOLLCAT"></param>
        /// <returns></returns>
        public List<dtXResult> JourneyFormCheck(string p_iniPath, string p_dbConf, List<boXRouteSection> p_lstRouteSection, boXTruck p_XTruck, bool p_GetRouteWithTruckSpeeds, int p_CalcTRK_ETOLLCAT = 0)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;

            dtXResult res = new dtXResult();
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);
                
//1. geokódolás
                ProcessNotifyIcon ni = new ProcessNotifyIcon();
                DepotGeocodingProcess dp = new DepotGeocodingProcess(ni, p_lstRouteSection);
                dp.RunWait();



                //logVersion();
                Util.Log2File(">>START:JourneyFormCheck( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_lstDepotID.<count>='" + p_lstRouteSection.Count.ToString() + ",p_XTruck=" + p_XTruck.ToString() + "')");
                PMapCommonVars.Instance.ConnectToDB();
                /*
                string sErr;
                if (RouteVisualisationData.FillData(p_lstRouteSection, p_TRK_ID, p_CalcTRK_ETOLLCAT, p_GetRouteWithTruckSpeeds, out sErr))
                {
                    res.Status = dtXResult.EStatus.OK;
                    res.Data = fillSummary();

                }
                else
                {
                    res.Status = dtXResult.EStatus.ERROR;
                    res.ErrMessage = sErr;
                    sRetStatus = retErr;
                }
                */
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                res.Status = dtXResult.EStatus.EXCEPTION;
                res.ErrMessage = e.Message;
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:RouteVisualizationCalc()");

            List<dtXResult> resArr = new List<dtXResult>();
            resArr.Add(res);

            return resArr;
        }

        /// <summary>
        /// Menetlevél ellenőrzés eredmény felépítése
        /// </summary>
        /// <returns></returns>
        private boXRouteSummary fillSummary()
        {
            boXRouteSummary ret = new boXRouteSummary();
            ret.ShortestRoute.SumDistance = RouteVisCommonVars.Instance.lstDetails[0].SumDistance;
            ret.ShortestRoute.SumDuration = RouteVisCommonVars.Instance.lstDetails[0].SumDuration;
            ret.ShortestRoute.SumToll = RouteVisCommonVars.Instance.lstDetails[0].SumToll;
            ret.ShortestRoute.SumDistanceEmpty = RouteVisCommonVars.Instance.lstDetails[0].SumDistanceEmpty;
            ret.ShortestRoute.SumDurationEmpty = RouteVisCommonVars.Instance.lstDetails[0].SumDurationEmpty;
            ret.ShortestRoute.SumTollEmpty = RouteVisCommonVars.Instance.lstDetails[0].SumTollEmpty;
            ret.ShortestRoute.SumDistanceLoaded = RouteVisCommonVars.Instance.lstDetails[0].SumDistanceLoaded;
            ret.ShortestRoute.SumDurationLoaded = RouteVisCommonVars.Instance.lstDetails[0].SumDurationLoaded;
            ret.ShortestRoute.SumTollLoaded = RouteVisCommonVars.Instance.lstDetails[0].SumTollLoaded;

            ret.FastestRoute.SumDistance = RouteVisCommonVars.Instance.lstDetails[1].SumDistance;
            ret.FastestRoute.SumDuration = RouteVisCommonVars.Instance.lstDetails[1].SumDuration;
            ret.FastestRoute.SumToll = RouteVisCommonVars.Instance.lstDetails[1].SumToll;
            ret.FastestRoute.SumDistanceEmpty = RouteVisCommonVars.Instance.lstDetails[1].SumDistanceEmpty;
            ret.FastestRoute.SumDurationEmpty = RouteVisCommonVars.Instance.lstDetails[1].SumDurationEmpty;
            ret.FastestRoute.SumTollEmpty = RouteVisCommonVars.Instance.lstDetails[1].SumTollEmpty;
            ret.FastestRoute.SumDistanceLoaded = RouteVisCommonVars.Instance.lstDetails[1].SumDistanceLoaded;
            ret.FastestRoute.SumDurationLoaded = RouteVisCommonVars.Instance.lstDetails[1].SumDurationLoaded;
            ret.FastestRoute.SumTollLoaded = RouteVisCommonVars.Instance.lstDetails[1].SumTollLoaded;
            return ret;

        }



    }
}
