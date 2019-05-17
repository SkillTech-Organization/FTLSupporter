using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using System.Drawing;
using System.IO;
using System.Reflection;
using PMapCore.Route;
using PMapCore.MapProvider;
using PMapCore.DB;
using PMapCore.Markers;
using System.Globalization;
using System.Threading;
using PMapCore.LongProcess;
using PMapCore.BO;
using PMapCore.BLL;
using PMapCore.BLL.DataXChange;
using PMapCore.Common.PPlan;
using PMapCore.BO.DataXChange;
using PMapCore.Common.Parse;
using PMapCore.Licence;
using PMapCore.Strings;
using System.Diagnostics;
using PMapUI.Forms;
using PMapUI.Common;
using PMapCore.Common;

namespace VBInterface
{


    //CT integráció miatt nem lehet static!

    /// <summary>
    /// CT <--> PMap hívás interface. Minden PMap funckió ezen a rétegen keresztül van kivülről elérve.
    /// </summary>
    public class SWHInterface
    {

        private const string retOK = "OK";
        private const string retCancel = "CANCEL";
        private const string retErr = "ERROR";
        private const string retFailed = "FAILED";

        private const string VBNewLine = "<NL>";

        #region VB-ből és C#-ől egyaránt hívható szolgáltatások

        public void ShowTestMessage()
        {
            Util.Log2File(">>START:ShowTestMessage()");
            MessageBox.Show("Hello TestMessage!");
            Util.Log2File(">>END:ShowTestMessage()");

        }

        public string SelectPosition(string p_lat, string p_lng, string p_sHint, string p_iniPath, string p_dbConf)
        {
            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;


            DateTime dt = DateTime.Now;

            //     logVersion();
            Util.Log2File(">>START:SelectPosition(p_lat=" + p_lat + ", p_lng=" + p_lng + ", p_sHint=" + p_sHint + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")", false);

            try
            {
                double dLat = Convert.ToDouble(p_lat.Replace(',', '.'), CultureInfo.InvariantCulture);
                double dLng = Convert.ToDouble(p_lng.Replace(',', '.'), CultureInfo.InvariantCulture);

                frmPMap pmap = new frmPMap(dLat, dLng, p_sHint.Replace(VBNewLine, "\n"));
                if (pmap.ShowMap() == DialogResult.OK)
                {
                    return String.Format("{0}|{1}", pmap.CurrentPos.Position.Lat, pmap.CurrentPos.Position.Lng);
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //   Util.Log2File(">>END:SelectPosition()-->" + sRet, false);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt);
            return sRetStatus;
        }

        /// <summary>
        /// Geokódolás
        /// </summary>
        /// <param name="p_addr"></param>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <returns></returns>
        public string Geocoding(string p_addr, string p_iniPath, string p_dbConf)
        {
            return GeocodingCore(p_addr, p_iniPath, p_dbConf, false);
        }

        /// <summary>
        /// Geokódolás teljes címre
        /// </summary>
        /// <param name="p_addr"></param>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <returns></returns>
        public string GeocodingFulAddr(string p_addr, string p_iniPath, string p_dbConf)
        {
            return GeocodingCore(p_addr, p_iniPath, p_dbConf, true);
        }

        /// <summary>
        /// Geokódolás core
        /// </summary>
        /// <param name="p_addr"></param>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConfm"></param>
        /// <param name="p_onlyFullAddr"></param>
        /// <returns></returns>
        private string GeocodingCore(string p_addr, string p_iniPath, string p_dbConf, bool p_onlyFullAddr = false)
        {
            Util.Log2File(">>START:GeocodingCore(p_addr=" + p_addr + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ", "+p_onlyFullAddr.ToString() +")", false);
            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;


            DateTime dt = DateTime.Now;
            try
            {
                PMapCommonVars.Instance.ConnectToDB();

                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                boDepot.EIMPADDRSTAT DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.MISSADDR;
                int ZIP_ID = 0;
                int NOD_ID = 0;
                int EDG_ID = 0;
                bool bFound = route.GeocodingByAddr(p_addr, out ZIP_ID, out NOD_ID, out EDG_ID, out DEP_IMPADDRSTAT, p_onlyFullAddr);
                if (bFound && DEP_IMPADDRSTAT != boDepot.EIMPADDRSTAT.MISSADDR)
                {
                    boEdge edg = route.GetEdgeByID(EDG_ID);
                    return String.Format("{0}|{1}|{2}|{3}", ZIP_ID, NOD_ID, EDG_ID, (edg != null ? edg.EDG_NAME : "???"));
                }
                else
                {
                    return boDepot.EIMPADDRSTAT.MISSADDR.ToString();
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //   Util.Log2File(">>END:GeocodingCore()-->" + sRet, false);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt);
            return sRetStatus;
        }
        public string GeocodingByGoogle(string p_addr, string p_iniPath, string p_dbConf)
        {
            p_addr = p_addr.Replace('%', ' ').Replace('&', ' ');
            Util.Log2File(">>START:GeocodingByGoogle(p_addr=" + p_addr + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")", false);
            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;


            DateTime dt = DateTime.Now;

            //     logVersion();
            Util.Log2File(">>START:GeocodingByGoogle(p_addr=" + p_addr + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")", false);

            try
            {
                PMapCommonVars.Instance.ConnectToDB();

                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                int ZIP_ID = 0;
                int NOD_ID = 0;
                int EDG_ID = 0;
                bool bFound = route.GeocodingByGoogle(p_addr, out ZIP_ID, out NOD_ID, out EDG_ID);
                if (bFound)
                {
                    boEdge edg = route.GetEdgeByID(EDG_ID);
                    return String.Format("{0}|{1}|{2}|{3}", ZIP_ID, NOD_ID, EDG_ID, (edg != null ? edg.EDG_NAME : "???"));
                }
                else
                {
                    return boDepot.EIMPADDRSTAT.MISSADDR.ToString();
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt);



            return sRetStatus;
        }

        public string ShowGoogleMap(string p_iniPath, string p_dbConf)
        {
            DateTime dt = DateTime.Now;

            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;

            //logVersion();
            Util.Log2File(">>START:ShowGoogleMap(p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")");

            try
            {
                frmPMap pmap = new frmPMap();
                if (pmap.ShowMap() == DialogResult.OK)
                {
                    return String.Format("{0}|{1}", pmap.CurrentPos.Position.Lat, pmap.CurrentPos.Position.Lng);
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:ShowGoogleMap()-->" + sRet);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt);
            return sRetStatus;
        }

        public string ShowRoute(string p_routeList, string p_iniPath, string p_dbConf)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;

            //            logVersion();
            Util.Log2File(">>START:ShowRoute( p_routeList=" + p_routeList + "p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")");

            try
            {
                p_routeList = p_routeList.Replace(VBNewLine, "\n");
                List<PMapMarker> pts = getRoutePoints(p_routeList);
                frmPMap pmap = new frmPMap();
                pmap.btnCancel.Visible = false;
                sRetStatus = pmap.ShowRoute(pts, Color.Blue) ? retOK : retCancel;
                pmap.ShowDialog();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //            Util.Log2File(">>END:ShowRoute()-->" + sRet);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_routeList);
            return sRetStatus;
        }

        public string ShowDepots(string p_DepotList, string p_iniPath, string p_dbConf)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;

            //            logVersion();
            Util.Log2File(">>START:ShowDepots( p_DepotList=" + p_DepotList + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")");

            try
            {
                p_DepotList = p_DepotList.Replace(VBNewLine, "\n");
                List<PMapMarker> pts = getDepotPoints(p_DepotList);
                frmPMap pmap = new frmPMap();
                pmap.btnCancel.Visible = false;
                sRetStatus = pmap.ShowDepots(pts) ? retOK : retCancel;
                pmap.ShowDialog();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //            Util.Log2File(">>END:ShowDepots()-->" + sRet);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_DepotList);
            return sRetStatus;
        }

        public string ShowDepotsAndRoute(string p_PLN_ID, string p_TPL_ID, string p_iniPath, string p_dbConf)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;

            //logVersion();
            Util.Log2File(">>START:ShowDepotsAndRoute( p_PLN_ID=" + p_PLN_ID + ", p_TPL_ID=" + p_TPL_ID + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")");

            try
            {
                frmPMap pmap = new frmPMap();
                pmap.btnCancel.Visible = false;
                pmap.ShowDepotsAndRoute(Convert.ToInt32(p_PLN_ID), Convert.ToInt32(p_TPL_ID), Color.Blue);
                pmap.ShowDialog();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }

            //Util.Log2File(">>END:ShowDepotsAndRoute()");

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_PLN_ID, p_TPL_ID);
            return sRetStatus;
        }

        private List<PMapMarker> getDepotPoints(string p_DepotList)
        {
            List<PMapMarker> depotPoints = new List<PMapMarker>();
            string[] aPoints = p_DepotList.Split('|');
            for (int i = 0; i < aPoints.Count(); i += 4)
            {
                double dLat = Convert.ToDouble(aPoints[i].Replace(',', '.'), CultureInfo.InvariantCulture);
                double dLng = Convert.ToDouble(aPoints[i + 1].Replace(',', '.'), CultureInfo.InvariantCulture);
                string sHint = aPoints[i + 2];

                MarkerType mType;
                if (aPoints[i + 3] == "0")
                    mType = MarkerType.UnPlannedDepot;
                else
                    mType = MarkerType.PlannedDepot;

                depotPoints.Add(new PMapMarker(new PointLatLng(dLat, dLng), sHint, mType));
            }

            return depotPoints;

        }

        private List<PMapMarker> getRoutePoints(string p_routeList)
        {
            List<PMapMarker> routePoints = new List<PMapMarker>();
            string[] aPoints = p_routeList.Split('|');
            for (int i = 0; i < aPoints.Count(); i += 3)
            {
                double dLat = Convert.ToDouble(aPoints[i].Replace(',', '.'), CultureInfo.InvariantCulture);
                double dLng = Convert.ToDouble(aPoints[i + 1].Replace(',', '.'), CultureInfo.InvariantCulture);
                string sHint = aPoints[i + 2];

                routePoints.Add(new PMapMarker(new PointLatLng(dLat, dLng), sHint));
            }

            return routePoints;
        }


        public string PlanToursVB(string p_PLN_ID, string p_USR_ID, string p_iniPath, string p_dbConf)
        {
            return PlanToursVB(p_PLN_ID, p_USR_ID, new PlanParams(), p_iniPath, p_dbConf);
        }

        public string PlanToursVB(string p_PLN_ID, string p_USR_ID, PlanParams p_planParams, string p_iniPath, string p_dbConf)
        {
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return "0";
            List<dtXResult> res = PlanTours(p_iniPath, p_dbConf, p_PLN_ID, p_USR_ID, p_planParams);
            return (bool)(res.First().Status == dtXResult.EStatus.OK) ? "1" : "0";
        }


        public string InitPMapRouteData(string p_iniPath, string p_dbConf)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                //logVersion();
                Util.Log2File(">>START:InitPMapRouteData( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")");
                PMRouteInterface.StartPMRouteInitProcess();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }

            //Util.Log2File(">>END:InitPMapRouteData() -->" + sRet);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt);
            return sRetStatus;
        }

        /// <summary>
        /// Útvonalszámítás egy tervben szereplő lerakókra
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_PLN_ID"></param>
        /// <returns></returns>
        public string CalcPMapRoutesByPlan(string p_iniPath, string p_dbConf, int p_PLN_ID, bool p_savePoints)
        {
            string sRetStatus = retOK;
            DateTime dt = DateTime.Now;

            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                return retErr;

            Thread thWaitMessage = new Thread(new ThreadStart(waitMessageThread));
            thWaitMessage.SetApartmentState(ApartmentState.STA);
            try
            {

                while (true)
                {
                    using (GlobalLocker lockObj = new GlobalLocker(Global.lockObjectCalc, 500))
                    {
                        if (lockObj.LockSuccessful)
                        {
                            if (thWaitMessage.IsAlive)
                                thWaitMessage.Abort();


                            //logVersion();
                            Util.Log2File(">>START:CalcPMapRoutesByPlan( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_PLN_ID=" + p_PLN_ID.ToString() + ")");

                            PMapCommonVars.Instance.ConnectToDB();

                            sRetStatus = calculatePMapRoutesByPlan(p_PLN_ID, p_savePoints) ? retOK : retFailed;


                            break;
                        }
                        else
                        {
                            if (!thWaitMessage.IsAlive)
                                thWaitMessage.Start();
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }

            finally
            {

                if (thWaitMessage.IsAlive)
                    thWaitMessage.Abort();
            }

            //Util.Log2File(">>END:CalcPMapRoutesByPlan()-->" + sRet);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_PLN_ID, p_savePoints.ToString());
            return sRetStatus;
        }

        private bool calculatePMapRoutesByPlan(int p_PLN_ID, bool p_savePoints)
        {
            bllRoute bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
            List<boRoute> res = bllRoute.GetDistancelessPlanNodes(p_PLN_ID);
            if (res.Count == 0)
                return true;

            bool bOK = false;
            if (PMapIniParams.Instance.RouteThreadNum > 1)
                bOK = PMRouteInterface.GetPMapRoutesMulti(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, p_savePoints);
            else
                bOK = PMRouteInterface.GetPMapRoutesSingle(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, p_savePoints);

            System.GC.Collect();
            return bOK;
        }

        private void waitMessageThread()
        {
            dlgWait d = new dlgWait();
            d.ShowDialog();

        }


        /// <summary>
        /// Útvonalszámítás megrendelések lerakóira. A metódus nyit egy külön thread-ot a számításhoz és visszatér a hívóhoz. 
        /// Így a teljes feldolgozás a háttérben fut.
        /// </summary>
        /// <param name="p_iniPath">CT iniállomány</param>
        /// <param name="p_dbConf">Adatbázis konfig sorszáma</param>
        /// <param name="p_ORD_DATE_S">Megrendelések kezdődádum</param>
        /// <param name="p_ORD_DATE_E">Megrendelések végdátum</param>
        public string CalcPMapRoutesByOrders(string p_iniPath, string p_dbConf, string p_ORD_DATE_S, string p_ORD_DATE_E)
        {
            string sRetStatus = retOK;
            DateTime dt = DateTime.Now;
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                    return retErr;

                //logVersion();
                Util.Log2File(">>START:CalcPMapRoutesByOrders( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_ORD_DATE_S=" + p_ORD_DATE_S + ",p_ORD_DATE_E=" + p_ORD_DATE_E + ")");

                //Egy külön szálban, háttérben futkorászik
                StartPMapRoutesByOrders spm = new StartPMapRoutesByOrders(p_ORD_DATE_S, p_ORD_DATE_E, true);
                spm.Run();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }

            //Util.Log2File(">>END:CalcPMapRoutesByOrders()-->" + sRet);
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_ORD_DATE_S, p_ORD_DATE_E);
            return sRetStatus;
        }

        /// <summary>
        /// Útdjszámítás tevezés alatt álló túrák listájára
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_TPL_ID_List"></param>
        /// <returns></returns>
        public string CalcTOLL(string p_iniPath, string p_dbConf, string p_TPL_ID_List)
        {
            string sRetStatus = retOK;
            DateTime dt = DateTime.Now;
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                    return retErr;

                //logVersion();
                Util.Log2File(">>START:CalcTOLL( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_TPL_ID_List='" + p_TPL_ID_List + "')");
                PMapCommonVars.Instance.ConnectToDB();

                bllPlanEdit bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);

                string[] TPL_IDArr = p_TPL_ID_List.Split(',');
                foreach (string sTPL_ID in TPL_IDArr)
                {
                    bllPlanEdit.CalcTOLL(Convert.ToInt32(sTPL_ID));
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:CalcTOLL()-->" + sRet);
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_TPL_ID_List);
            return sRetStatus;
        }

        /// <summary>
        /// tevezés alatt álló túrák átszámolása
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_TPL_ID_List"></param>
        /// <returns></returns>
        public string RecalcPlTours(string p_iniPath, string p_dbConf, string p_TPL_ID_List)
        {
            string sRetStatus = retOK;
            DateTime dt = DateTime.Now;
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                    return retErr;

                //logVersion();
                Util.Log2File(">>START:RecalcPlTours( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_TPL_ID_List='" + p_TPL_ID_List + "')");
                PMapCommonVars.Instance.ConnectToDB();

                bllPlanEdit bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);

                string[] TPL_IDArr = p_TPL_ID_List.Split(',');
                foreach (string sTPL_ID in TPL_IDArr)
                {
                    bllPlanEdit.RecalcTour(0, Convert.ToInt32(sTPL_ID), Global.defWeather);
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:RecalcPlTours()-->" + sRet);
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_TPL_ID_List);
            return sRetStatus;
        }


        public string CheckLicence(string p_iniPath, string p_dbConf, bool p_showMessage = true)
        {
            string sRetStatus = retOK;
            try
            {
                if (!PMapIniParams.Instance.Loaded)
                    PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);
            }
            catch (PMapLicenceException licex)
            {
                if (p_showMessage)
                    UI.Error(licex.Message);
                sRetStatus = retFailed;
            }
            catch (Exception ex)
            {
                if (p_showMessage)
                    UI.Error(ex.Message);
                sRetStatus = retErr;
            }

            if (p_showMessage && sRetStatus == retOK && PMapCommonVars.Instance.Expired.AddMonths(-1) < DateTime.Now.Date)
                UI.Warning(PMapMessages.W_LIC_EXPIRED_WARN, PMapCommonVars.Instance.Expired.ToString(Global.DATEFORMAT));
            return sRetStatus;
        }

        #endregion

        /* -nem -kel
        public string MPOrderDialog(string p_iniPath, string p_dbConf)
        {

            string sRetStatus = retOK;
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                Util.Log2File(">>START:MPOrderDialog( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + "')");
                PMapCommonVars.Instance.ConnectToDB();

                if (CheckLicence(p_iniPath, p_dbConf, true) != retOK)
                    return retErr;

                frmMPOrder dlg = new frmMPOrder();
                dlg.ShowDialog();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:MPOrderDialog()-->" + sRetStatus);
            return sRetStatus;
        }
        */

        #region Csak C#-ból hívható szolgáltatások
        public List<dtXResult> ImportDepots(string p_iniPath, string p_dbConf, List<boXDepot> p_depots, bool p_addAllTrucksToDepot = true)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            List<dtXResult> result = new List<dtXResult>();
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);
                //logVersion();
                Util.Log2File(">>START:ImportDepots( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_depots.<count>='" + p_depots.Count.ToString() + "')");
                PMapCommonVars.Instance.ConnectToDB();


                dtXDepot xdep = new dtXDepot(PMapCommonVars.Instance.CT_DB);
                result = xdep.ImportDepots(p_depots, p_addAllTrucksToDepot);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;

            }

            //Util.Log2File(">>END:ImportDepots()");
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_depots.Count());
            return result;
        }

        public List<dtXResult> ImportTrucks(string p_iniPath, string p_dbConf, List<boXTruck> p_truck)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            List<dtXResult> result = new List<dtXResult>();
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);

                //logVersion();
                Util.Log2File(">>START:ImportTrucks( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_truck.<count>='" + p_truck.Count.ToString() + "')");
                PMapCommonVars.Instance.ConnectToDB();


                dtXTruck xtrk = new dtXTruck(PMapCommonVars.Instance.CT_DB);
                result = xtrk.ImportTrucks(p_truck);

            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }

            //Util.Log2File(">>END:ImportTrucks()");
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_truck.Count());
            return result;
        }

        public List<dtXResult> ImportOrders(string p_iniPath, string p_dbConf, List<boXOrder> p_orders)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            List<dtXResult> result = new List<dtXResult>();
            try
            {

                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);

                //logVersion();
                Util.Log2File(">>START:ImportOrders( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_orders.<count>='" + p_orders.Count.ToString() + "')");
                PMapCommonVars.Instance.ConnectToDB();


                dtXOrder xord = new dtXOrder(PMapCommonVars.Instance.CT_DB);
                result = xord.ImportOrders(p_orders);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:ImportOrders()");
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_orders.Count());
            return result;
        }

        /// <summary>
        /// Menetlevél ellenőrzés
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_lstRouteSection"></param>
        /// <param name="p_TRK_ID"></param>
        /// <param name="p_GetRouteWithTruckSpeeds"></param>
        /// <param name="p_CalcTRK_ETOLLCAT"></param>
        /// <returns></returns>
        public List<dtXResult> RouteVisualization(string p_iniPath, string p_dbConf, List<boXRouteSection> p_lstRouteSection, int p_TRK_ID, bool p_GetRouteWithTruckSpeeds, int p_CalcTRK_ETOLLCAT = 0)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;

            dtXResult res = new dtXResult();
            boXRouteSummary rSummary = new boXRouteSummary();
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);

                //logVersion();
                Util.Log2File(">>START:RouteVisualization( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_lstDepotID.<count>='" + p_lstRouteSection.Count.ToString() + ",p_TRK_ID=" + p_TRK_ID.ToString() + "')");
                PMapCommonVars.Instance.ConnectToDB();


                string sErr;
                if (RouteVisualisationData.FillData(p_lstRouteSection, p_TRK_ID, p_CalcTRK_ETOLLCAT, p_GetRouteWithTruckSpeeds, out sErr))
                {

                    frmRouteVisualization v = new frmRouteVisualization(p_lstRouteSection, p_TRK_ID);
                    v.ShowDialog();

                    rSummary = fillSummary();
                    if (RouteVisCommonVars.Instance.SelectedType == RouteVisCommonVars.TY_FASTEST)
                        rSummary.FastestRoute.Selected = true;
                    else
                        rSummary.ShortestRoute.Selected = true;

                    res.Status = dtXResult.EStatus.OK;
                    res.Data = rSummary;
                }
                else
                {
                    UI.Error(sErr);
                    res.Status = dtXResult.EStatus.ERROR;
                    res.ErrMessage = sErr;
                }
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                res.Status = dtXResult.EStatus.EXCEPTION;
                res.ErrMessage = e.Message;
                sRetStatus = retErr;
            }

            //Util.Log2File(">>END:RouteVisualization()");
            List<dtXResult> resArr = new List<dtXResult>();
            resArr.Add(res);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_lstRouteSection.Count(), p_TRK_ID, p_GetRouteWithTruckSpeeds, p_CalcTRK_ETOLLCAT);
            return resArr;
        }

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
        public List<dtXResult> RouteVisualizationCalc(string p_iniPath, string p_dbConf, List<boXRouteSection> p_lstRouteSection, int p_TRK_ID, bool p_GetRouteWithTruckSpeeds, int p_CalcTRK_ETOLLCAT = 0)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;

            dtXResult res = new dtXResult();
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);

                //logVersion();
                Util.Log2File(">>START:RouteVisualizationCalc( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_lstDepotID.<count>='" + p_lstRouteSection.Count.ToString() + ",p_TRK_ID=" + p_TRK_ID.ToString() + "')");
                PMapCommonVars.Instance.ConnectToDB();

                string sErr;
                if (RouteVisualisationData.FillData(p_lstRouteSection, p_TRK_ID, p_CalcTRK_ETOLLCAT, p_GetRouteWithTruckSpeeds,  out sErr))
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

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_lstRouteSection.Count(), p_TRK_ID, p_GetRouteWithTruckSpeeds, p_CalcTRK_ETOLLCAT);
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

        /// <summary>
        /// Új terv létrehozása
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_PLN_NAME"></param>
        /// <param name="p_WHS_ID"></param>
        /// <param name="p_PLN_DATE_B"></param>
        /// <param name="p_PLN_DATE_E"></param>
        /// <param name="p_PLN_USEINTERVAL"></param>
        /// <param name="p_PLN_INTERVAL_B"></param>
        /// <param name="p_PLN_INTERVAL_E"></param>
        /// <param name="p_enabledTruckList"></param>
        /// <returns></returns>
        public List<dtXResult> CreateNewPlan(string p_iniPath, string p_dbConf, String p_PLN_NAME, int p_WHS_ID, DateTime p_PLN_DATE_B, DateTime p_PLN_DATE_E, bool p_PLN_USEINTERVAL, DateTime p_PLN_INTERVAL_B, DateTime p_PLN_INTERVAL_E, List<PlanParams.CEnabledTruck> p_enabledTruckList = null)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            dtXResult res = new dtXResult();
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);
                //logVersion();

                Util.Log2File(">>START:CreateNewPlan( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_PLN_NAME='" + p_PLN_NAME + "',p_WHS_ID=" + p_WHS_ID.ToString() +
                    ",p_PLN_DATE_B=" + p_PLN_DATE_B.ToString(Global.DATETIMEFORMAT) +
                    ",p_PLN_DATE_E=" + p_PLN_DATE_E.ToString(Global.DATETIMEFORMAT) +
                    "p_PLN_USEINTERVAL=" + p_PLN_USEINTERVAL.ToString() +
                    ",p_PLN_INTERVAL_B=" + p_PLN_INTERVAL_B.ToString(Global.DATETIMEFORMAT) +
                    ",p_PLN_INTERVAL_E=" + p_PLN_INTERVAL_E.ToString(Global.DATETIMEFORMAT) +
                    ",p_enabledTruckList (cnt)=" + (p_enabledTruckList == null ? "null" : p_enabledTruckList.Count.ToString()) +
                    ")");
                PMapCommonVars.Instance.ConnectToDB();


                bllPlanEdit pe = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);

                boXNewPlan np = pe.CreatePlan(p_PLN_NAME, p_WHS_ID, p_PLN_DATE_B, p_PLN_DATE_E, p_PLN_USEINTERVAL, p_PLN_INTERVAL_B, p_PLN_INTERVAL_E, p_enabledTruckList);
                if (np.Status == boXNewPlan.EStatus.OK)
                {
                    res.Data = np;
                    res.Status = dtXResult.EStatus.OK;
                }
                else
                {
                    res.Data = null;
                    res.Status = (dtXResult.EStatus)Enum.Parse(typeof(dtXResult.EStatus), np.Status.ToString());
                    res.ErrMessage = np.ErrMessage;
                    sRetStatus = retErr;
                }


            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                res.Status = dtXResult.EStatus.EXCEPTION;
                res.ErrMessage = e.Message;
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:CreateNewPlan()");

            List<dtXResult> resArr = new List<dtXResult>();
            resArr.Add(res);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_PLN_NAME, p_WHS_ID, p_PLN_DATE_B, p_PLN_DATE_E, p_PLN_USEINTERVAL, p_PLN_INTERVAL_B, p_PLN_INTERVAL_E, p_enabledTruckList);
            return resArr;
        }


        public List<dtXResult> PlanTours(string p_iniPath, string p_dbConf, string p_PLN_ID, string p_USR_ID)
        {
            return PlanTours(p_iniPath, p_dbConf, p_PLN_ID, p_USR_ID, new PlanParams());
        }

        public List<dtXResult> PlanTours(string p_iniPath, string p_dbConf, string p_PLN_ID, string p_USR_ID, PlanParams p_planParams)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;
            dtXResult res = new dtXResult();
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            ChkLic.Check(PMapIniParams.Instance.IDFile);
            //logVersion();
            Util.Log2File(">>START:PlanTours( p_PLN_ID=" + p_PLN_ID + ", p_USR_ID=" + p_USR_ID + ", p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ")");
            string sRet = "1";
            try
            {
                frmPPlan pp = new frmPPlan(Convert.ToInt32("0" + p_PLN_ID), Convert.ToInt32("0" + p_USR_ID), p_planParams);
                pp.ShowDialog();
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                res.Status = dtXResult.EStatus.EXCEPTION;
                res.ErrMessage = e.Message;
                sRetStatus = retErr;
            }
            finally
            {
                if (PMapCommonVars.Instance.CT_DB != null)
                {
                    bllSemaphore sem = new bllSemaphore(PMapCommonVars.Instance.CT_DB);
                    sem.ClearSemaphores();
                }

            }

            //Util.Log2File(">>END:PlanTours()-->" + sRet);

            List<dtXResult> resArr = new List<dtXResult>();
            resArr.Add(res);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_PLN_ID, p_USR_ID);
            return resArr;
        }


        public List<dtXResult> GetPlan(string p_iniPath, string p_dbConf, int p_PLN_ID)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;

            dtXResult res = new dtXResult();

            boXFullPlan XPlan = new boXFullPlan();

            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);
                //logVersion();
                Util.Log2File(">>START:GetPlan( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_PLN_ID=" + p_PLN_ID.ToString() + ")");
                PMapCommonVars.Instance.ConnectToDB();


                dtXGetPlan xGetPlan = new dtXGetPlan(PMapCommonVars.Instance.CT_DB);
                res.Data = xGetPlan.GetPlan(p_PLN_ID);
                res.Status = dtXResult.EStatus.OK;


            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                res.Status = dtXResult.EStatus.EXCEPTION;
                res.ErrMessage = e.Message;
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:GetPlan()");

            List<dtXResult> resArr = new List<dtXResult>();
            resArr.Add(res);
            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_PLN_ID);
            return resArr;
        }

        public List<dtXResult> GetPlans(string p_iniPath, string p_dbConf, int p_WHS_ID, DateTime p_PLN_DATE_B, DateTime p_PLN_DATE_E)
        {
            DateTime dt = DateTime.Now;
            string sRetStatus = retOK;

            dtXResult res = new dtXResult();

            boXFullPlan XPlan = new boXFullPlan();

            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                ChkLic.Check(PMapIniParams.Instance.IDFile);

                //logVersion();
                Util.Log2File(">>START:GetPlans( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf +
                        ",p_WHS_ID=" + p_WHS_ID.ToString() +
                        ",p_PLN_DATE_B=" + p_PLN_DATE_B.ToString(Global.DATETIMEFORMAT_PLAN) +
                        ",p_PLN_DATE_E=" + p_PLN_DATE_E.ToString(Global.DATETIMEFORMAT_PLAN) + ")");

                PMapCommonVars.Instance.ConnectToDB();


                dtXGetPlan xGetPlan = new dtXGetPlan(PMapCommonVars.Instance.CT_DB);
                res.Data = xGetPlan.GetPlans(p_WHS_ID, p_PLN_DATE_B, p_PLN_DATE_E);
                res.Status = dtXResult.EStatus.OK;

            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                res.Status = dtXResult.EStatus.EXCEPTION;
                res.ErrMessage = e.Message;
                sRetStatus = retErr;
            }
            //Util.Log2File(">>END:GetPlans()");

            List<dtXResult> resArr = new List<dtXResult>();
            resArr.Add(res);

            if (!PMapIniParams.Instance.TestMode)
                ParseLogX.CallsToParse(System.Reflection.MethodBase.GetCurrentMethod().Name, sRetStatus, DateTime.Now - dt, p_WHS_ID, p_PLN_DATE_B, p_PLN_DATE_E);
            return resArr;
        }

        /// <summary>
        /// Útvonal ellenőrzés 
        /// MEGJ.:Az útvonalellenőrzés nem használ súly- és méretkorlátozásokat !
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_lstRoutes"></param>
        /// <returns></returns>
        public List<boXChkRes> CheckRoutes(string p_iniPath, string p_dbConf, List<boXChkRoute> p_lstRoutes)
        {
            DateTime dt = DateTime.Now;
            List<boXChkRes> res = new List<boXChkRes>();

            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            ChkLic.Check(PMapIniParams.Instance.IDFile);

            PMapCommonVars.Instance.ConnectToDB();
            bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
            PMapRoutingProvider provider = new PMapRoutingProvider();
            Dictionary<string, int> allRZones = route.GetAllRZones();

            foreach (var chkRoute in p_lstRoutes)
            {
                boXChkRes itemRes = new boXChkRes();
                itemRes.Status = boXChkRes.ResultStatus.OK;
                try
                {

                    List<ObjectValidator.ValidationError> tskErros = ObjectValidator.ValidateObject(chkRoute);
                    if (tskErros.Count != 0)
                    {
                        itemRes.Status = boXChkRes.ResultStatus.VALIDATIONERROR;
                        foreach (var err in tskErros)
                        {
                            itemRes.ErrMsg += err.Message + " ";
                        }
                        continue;
                    }



                    int diff = 0;
                    int fromNOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(chkRoute.FromLat, chkRoute.FromLng), out diff);
                    if (fromNOD_ID == 0)
                    {
                        itemRes.Status = boXChkRes.ResultStatus.VALIDATIONERROR;
                        itemRes.ErrMsg = "Helytelen kezdőkoordináta";
                        continue;
                    }
                    boNode fromNode = route.GetNode(fromNOD_ID);
                    itemRes.fromLat = fromNode.NOD_YPOS / Global.LatLngDivider;
                    itemRes.fromLng = fromNode.NOD_XPOS / Global.LatLngDivider;

                    int toNOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(chkRoute.ToLat, chkRoute.ToLng), out diff);
                    if (fromNOD_ID == 0)
                    {
                        itemRes.Status = boXChkRes.ResultStatus.VALIDATIONERROR;
                        itemRes.ErrMsg = "Helytelen végkoordináta";
                        continue;
                    }

                    boNode toNode = route.GetNode(toNOD_ID);
                    itemRes.toLat = toNode.NOD_YPOS / Global.LatLngDivider;
                    itemRes.toLng = toNode.NOD_XPOS / Global.LatLngDivider;

                    //Behajtási-övezet kódok lemapolása
                    string sRZN_ID_LIST = "";
                    String[] aRZones = chkRoute.RZones.Replace(" ", "").Split(',');
                    foreach (var zone in aRZones)
                    {
                        if (allRZones.ContainsKey(zone))
                        {
                            sRZN_ID_LIST += ("," + allRZones[zone].ToString());
                        }
                        else
                        {
                            itemRes.Status = boXChkRes.ResultStatus.VALIDATIONERROR;
                            itemRes.ErrMsg += "Helytelen behajtási övezet:" + zone + " ";
                        }
                    }
                    if (itemRes.Status != boXChkRes.ResultStatus.OK)
                    {
                        continue;
                    }


                    Dictionary<string, List<int>[]> NeighborsFull = null;
                    Dictionary<string, List<int>[]> NeighborsCut = null;
                    RectLatLng boundary = bllRoute.getBoundary(itemRes.fromLat, itemRes.fromLng, itemRes.toLat, itemRes.toLng);

                    if (sRZN_ID_LIST != "")
                        sRZN_ID_LIST = sRZN_ID_LIST.Substring(1);

                    var routePar = new CRoutePars() { RZN_ID_LIST = sRZN_ID_LIST, Weight = 0, Height = 0, Width = 0};
                    
                    //A menetlevél ellenőrzés nem használ súly- és méretkorlátozásokat !
                    RouteData.Instance.getNeigboursByBound(routePar, ref NeighborsFull, ref NeighborsCut, boundary, null);


                    boRoute result = provider.GetRoute( fromNOD_ID, toNOD_ID, routePar,
                        NeighborsFull[routePar.Hash],
                        PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
                        PMapIniParams.Instance.FastestPath ? ECalcMode.ShortestPath : ECalcMode.FastestPath);


                    //logVersion();
                    Util.Log2File(">>START:CheckRoutes( p_iniPath=" + p_iniPath + ", p_dbConf=" + p_dbConf + ",p_lstRoutes.<count>='" + p_lstRoutes.Count.ToString());
                    PMapCommonVars.Instance.ConnectToDB();
                }
                catch (Exception e)
                {
                    Util.ExceptionLog(e);
                    itemRes.Status = boXChkRes.ResultStatus.EXCEPTION;
                    itemRes.ErrMsg = e.Message;
                }
                res.Add(itemRes);
            }
            return res;
        }



            /*
                    private void logVersion()
                    {

                        Util.Log2File(String.Format("Product:{0} Ver.:{1}", ApplicationInfo.Title, ApplicationInfo.Version));
                    }
             */
            #endregion
        }

    }