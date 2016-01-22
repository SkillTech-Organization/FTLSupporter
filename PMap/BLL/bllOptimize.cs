﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BO;
using System.Data;
using PMap.DB.Base;
using PMap.LongProcess.Base;
using PMap.Localize;
using PMap.Route;
using PMap.BLL.Base;
using PMap.Common;
using System.IO;
using System.Globalization;

namespace PMap.BLL
{
    public class bllOptimize : bllBase
    {
        public boOptimize boOpt { get; private set; }

        private boWarehouse m_boWarehouse;
        private bllPlanEdit m_bllPlanEdit;
        private bllRoute m_bllRoute;
        private bllSpeedProf m_bllSpeedProf;

        public bllOptimize(SQLServerAccess p_DBA, int p_PLN_ID, int p_TPL_ID, bool p_Replan)
            : base(p_DBA, "")
        {
            m_bllPlanEdit = new bllPlanEdit(p_DBA);
            m_bllRoute = new bllRoute(p_DBA);
            m_bllSpeedProf = new bllSpeedProf(p_DBA);

            boOpt = new boOptimize(p_PLN_ID, p_TPL_ID, p_Replan);
        }

        public void CreatePlanfile()
        {
        }
        public void FillOptimize(BaseProgressDialog p_notify)
        {

            bllHistory.WriteHistory(0, "FillOptimize", boOpt.PLN_ID, bllHistory.EMsgCodes.FUNC, "");
            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_PROJINF);
            fillProjectInfo();

            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_OPTPARS);
            fillOptimizePars();


            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_COSTPROF);
            fillCostProfile();


            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_TRUCKTYPE);
            fillTruckType();

            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_WHS);
            fillWarehouse();


            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_CAPPROF);
            fillCapacityProfile();

            if (boOpt.Replan)
            {
                //Ha újratervezés van zárolom a jármûveket
                lockNoReplanned();
            }


            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_TRUCK);
            fillTruck();


            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_DEP);
            fillClient();

            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_ORDER);
            fillOrder();

            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_ORDERTRUCK);
            fillOrderTruck();

            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_DST);
            fillRelationAccess(p_notify);

            if (boOpt.Replan)
            {
                if (p_notify != null)
                    p_notify.SetInfoText(PMapMessages.M_OPT_TOURS);
                fillPlanTours(p_notify);
            }



        }

        public void MakeOptContent()
        {

            bllHistory.WriteHistory(0, "MakeOptContent", boOpt.PLN_ID, bllHistory.EMsgCodes.FUNC, "");

            //Projektfüggetlen adatok
            boOpt.P_setCustomerId();

            boOpt.P_CreateCostProfile();

            boOpt.P_CreateTruckType();

            boOpt.P_CreateWarehouse();

            boOpt.P_CreateCapacityProfile();

            boOpt.P_CreateTruck();

            boOpt.P_CreateClient();

            boOpt.P_CreateOrder();

            boOpt.P_AddOrderTruck();

            boOpt.P_SetRelationAccess();

            boOpt.P_CreatePlanTours();

            boOpt.P_StartEngine();

        }

        private void fillProjectInfo()
        {
            string sSql = "select WHS_ID, PLN_DATE_B,  PLN_DATE_E, PLN_NAME from PLN_PUBLICATEDPLAN where ID = ?";
            DataTable dt = DBA.Query2DataTable(sSql, boOpt.PLN_ID);
            if (dt.Rows.Count > 0)
            {
                boOpt.WHS_ID = Util.getFieldValue<int>(dt.Rows[0], "WHS_ID");
                boOpt.PLN_NAME = Util.getFieldValue<string>(dt.Rows[0], "PLN_NAME");
                boOpt.PLN_DATE_B = Util.getFieldValue<DateTime>(dt.Rows[0], "PLN_DATE_B");
                boOpt.PLN_DATE_E = Util.getFieldValue<DateTime>(dt.Rows[0], "PLN_DATE_E");
                boOpt.MinTime = (int)boOpt.PLN_DATE_B.TimeOfDay.TotalMinutes;
                boOpt.MaxTime = (int)(boOpt.PLN_DATE_E.TimeOfDay.TotalMinutes + boOpt.PLN_DATE_E.Date.Subtract(boOpt.PLN_DATE_B.Date).TotalMinutes);

                bllWarehouse whs = new bllWarehouse(DBA);
                m_boWarehouse = whs.GetWarehouse(boOpt.WHS_ID);


            }
        }

        private void fillOptimizePars()
        {
            string sSql = "select * from OPP_OPTPAR where PLN_ID = ?";
            DataTable dt = DBA.Query2DataTable(sSql, boOpt.PLN_ID);
            if (dt.Rows.Count > 0)
            {
                boOpt.OPP_DISTLIMIT = Util.getFieldValue<int>(dt.Rows[0], "OPP_DISTLIMIT");
                boOpt.OPP_ISDEEP = Util.getFieldValue<int>(dt.Rows[0], "OPP_ISDEEP");
                boOpt.OPP_CUTORDER = Util.getFieldValue<int>(dt.Rows[0], "OPP_CUTORDER");
                boOpt.OPP_REPLAN = Util.getFieldValue<bool>(dt.Rows[0], "OPP_REPLAN");
                boOpt.optTPL_ID = Util.getFieldValue<int>(dt.Rows[0], "TPL_ID");//csak akkor kitöltött, ha egy túrát szeretnénk optimalizálni

            }
        }

        private void fillCostProfile()
        {
            string sSql = "select TFP.ID as ID, TFP_FIXCOST, TFP_KMCOST, TFP_HOURCOST " + Environment.NewLine +
                          "from TFP_TARIFFPROF TFP " + Environment.NewLine +
                          "order by TFP.ID ";
            DataTable dt = DBA.Query2DataTable(sSql);
            int innerID = 1;
            boOpt.dicCostProfile =
                    (from r in dt.AsEnumerable()
                     select new
                      {
                          Key = Util.getFieldValue<int>(r, "ID"),
                          Value = new boOptimize.CCostProfile
                                 {
                                     innerID = innerID++,
                                     ID = Util.getFieldValue<int>(r, "ID"),
                                     fixCostByTruck = Util.getFieldValue<int>(r, "TFP_FIXCOST"),
                                     kmCost = Convert.ToInt32(Math.Round(Util.getFieldValue<double>(r, "TFP_KMCOST") / Global.CostDivider)),
                                     isZone = 0,
                                     zone1 = 0,
                                     zCost1 = 0,
                                     zone2 = 0,
                                     zCost2 = 0,
                                     zone3 = 0,
                                     zCost3 = 0,
                                     zone4 = 0,
                                     zCost4 = 0,
                                     zone5 = 0,
                                     zCost5 = 0,
                                     costByHour = Util.getFieldValue<int>(r, "TFP_HOURCOST"),
                                     fp1 = 0,
                                     fp2 = 0,
                                     fp3 = 0,
                                     fp4 = 0
                                 }
                      }).ToDictionary(n => n.Key, n => n.Value);
        }

        private void fillTruckType()
        {
            string sSql = "select distinct RZN_ID_LIST, SPP.ID as SPP_ID from v_trk_RZN_ID_LIST, SPP_SPEEDPROF SPP order by RZN_ID_LIST ";
            int innerID = 1;
            DataTable dt = DBA.Query2DataTable(sSql);
            boOpt.dicTruckType =
                    (from r in dt.AsEnumerable()
                     select new
                     {
                         Key = Util.getFieldValue<string>(r, "RZN_ID_LIST") + Global.SEP_POINT + Util.getFieldValue<string>(r, "SPP_ID"),
                         Value = new boOptimize.CTruckType
                         {
                             innerID = innerID++,
                             RZN_ID_LIST = Util.getFieldValue<string>(r, "RZN_ID_LIST"),
                             SPP_ID = Util.getFieldValue<int>(r, "SPP_ID"),
                             SpeedValues = getSpeedValues(Util.getFieldValue<int>(r, "SPP_ID"))
                         }
                     }).ToDictionary(n => n.Key, n => n.Value);
        }

        public Dictionary<int, int> getSpeedValues(int p_SPP_ID)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            string sSql = "select * from SPV_SPEEDPROFVALUE where SPP_ID = ? ";
            DataTable dt = DBA.Query2DataTable(sSql, p_SPP_ID);
            result = (from r in dt.AsEnumerable()
                      select new
                      {
                          Key = Util.getFieldValue<int>(r, "RDT_ID"),
                          Value = Util.getFieldValue<int>(r, "SPV_VALUE")
                      }).ToDictionary(n => n.Key, n => n.Value);
            return result;
        }

        private void fillWarehouse()
        {
            string sSql = "select ID, WHS_NAME, WHS_OPEN, WHS_CLOSE, WHS_SRVTIME from WHS_WAREHOUSE order by ID ";
            DataTable dt = DBA.Query2DataTable(sSql);
            int innerID = 1;
            boOpt.dicDepot =
                    (from r in dt.AsEnumerable()
                     select new
                     {
                         Key = Util.getFieldValue<int>(r, "ID"),
                         Value = new boOptimize.CDepot
                         {
                             innerID = innerID++,
                             ID = Util.getFieldValue<int>(r, "ID"),
                             clName = Util.getFieldValue<string>(r, "WHS_NAME"),
                             dpMinTime = (int)Math.Max(Util.getFieldValue<int>(r, "WHS_OPEN"), boOpt.PLN_DATE_B.TimeOfDay.TotalMinutes),
                             dpMaxTime = (int)Math.Min(Util.getFieldValue<int>(r, "WHS_CLOSE"), boOpt.PLN_DATE_E.TimeOfDay.TotalMinutes + boOpt.PLN_DATE_E.Date.Subtract(boOpt.PLN_DATE_B.Date).TotalMinutes),
                             isCentral = Util.getFieldValue<int>(r, "ID") == boOpt.WHS_ID ? 1 : 0,
                             serviceFix = Util.getFieldValue<int>(r, "WHS_SRVTIME"),
                             serviceVar = 0,
                             planstart = (int)boOpt.PLN_DATE_B.TimeOfDay.TotalMinutes,
                             pc2 = 0,
                             pc3 = 0,
                             pc4 = 0,
                             pc5 = 0
                         }
                     }).ToDictionary(n => n.Key, n => n.Value);

            innerID = 1;

            //A dicClient-ben a raktárak nehatív értékű kulccsal vannak felvéve
            boOpt.dicClient =
                    (from r in dt.AsEnumerable()
                     select new
                     {
                         Key = -(Util.getFieldValue<int>(r, "ID")),
                         Value = new boOptimize.CClient
                         {
                             innerID = innerID++,
                             ID = Util.getFieldValue<int>(r, "ID"),
                             isWHS = true,
                             clName = Util.getFieldValue<string>(r, "WHS_NAME"),
                             x = 0,
                             y = 0
                         }
                     }).ToDictionary(n => n.Key, n => n.Value);
        }

        private void fillCapacityProfile()
        {
            string sSql = "select ID, CPP_LOADQTY, CPP_LOADVOL FROM CPP_CAPACITYPROF where CPP_DELETED <> 1";
            DataTable dt = DBA.Query2DataTable(sSql);
            int innerID = 1;
            boOpt.dicCapacityProfile =
                    (from r in dt.AsEnumerable()
                     select new
                     {
                         Key = Util.getFieldValue<int>(r, "ID"),
                         Value = new boOptimize.CCapacityProfile
                         {
                             innerID = innerID++,
                             ID = Util.getFieldValue<int>(r, "ID"),
                             cap1 = Convert.ToInt32(Math.Ceiling(Util.getFieldValue<double>(r, "CPP_LOADQTY") * Global.csQTY_DEC)),
                             cap2 = Util.getFieldValue<int>(r, "CPP_LOADVOL"),              //dm3-ban értendő, nem szorozzuk fel
                             cap3 = 0,
                             cap4 = 0,
                             cap5 = 0
                         }
                     }).ToDictionary(n => n.Key, n => n.Value);
        }

        /// <summary>
        /// Zárolja azokat a jármûveket, melyeknek egy túrapontja van, és ki vannak
        /// teljesen használva. A TPL_NOREPLAN is 1-re állítódik, ezzel jelölöm, hogy
        /// ezt a optimize zárolta.
        /// </summary>
        /// <param name="Opt"></param>
        /// 
        //TODO:átnézni, miért kellhet!

        public void lockNoReplanned()
        {

            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {
                    string sSql = "update tpl_truckplan set TPL_NOREPLAN = 1, TPL_LOCKED = 1 where id in( " + Environment.NewLine +
                                   " select tpl.id " + Environment.NewLine +
                                   " from tpl_truckplan tpl " + Environment.NewLine +
                                   " inner join ptp_plantourpoint ptp on ptp.tpl_id = tpl.id " + Environment.NewLine +
                                   " inner join tod_tourorder tod on ptp.tod_id = tod.id " + Environment.NewLine +
                                   " inner join trk_truck trk on tpl.trk_id = trk.id " + Environment.NewLine +
                                   " inner join cpp_capacityprof cpp on trk.cpp_id = cpp.id " + Environment.NewLine +
                                   " where tpl.PLN_ID = ?  AND TPL_LOCKED != 1 " + Environment.NewLine +
                                   " group by tpl.id, CPP_LOADQTY " + Environment.NewLine +
                                   " having count(*) = 1 and sum(tod_qty) = CPP_LOADQTY " + Environment.NewLine +
                                   ")";

                    DBA.ExecuteNonQuery(sSql, boOpt.PLN_ID);
                }
                catch (Exception e)
                {
                    DBA.Rollback();
                    throw e;
                }
            }
        }

        //TODO:átnézni, miért kellhet!
        public void unLockNoReplanned()
        {

            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {
                    string sSql = "update TPL_TRUCKPLAN set TPL_NOREPLAN = 0, TPL_LOCKED = 0 " + Environment.NewLine +
                                   "where PLN_ID = ? and TPL_NOREPLAN = 1 ";

                    DBA.ExecuteNonQuery(sSql, boOpt.PLN_ID);
                }
                catch (Exception e)
                {
                    DBA.Rollback();
                    throw e;
                }
            }
        }

        private void fillTruck()
        {
            string sSql = "select TPL.ID as TPL_ID, TRK.ID as TRK_ID, TRK_REG_NUM, CRR_OWN, TFP_ID, CPP_ID, PLN_DATE_B, PLN_DATE_E, TPL_LOCKED, " + Environment.NewLine +
                        "isnull(TMX.MAXTIME, isnull(DATEADD(n, TPL.TPL_IDLETIME, AV.AVAIL), PLN.PLN_DATE_B)) AS AVAIL, TPL.TPL_MAXSCORE AS TPL_ORIGSCORE,  " + Environment.NewLine +
                        "TPL.TPL_AVAIL_S, RPS.START, TRK.TRK_BUNDPOINT, TRK.TRK_BUNDTIME, TRK.WHS_ID, TPL.ARR_WHS_ID, RESTZ.RZN_ID_LIST, TRK.SPP_ID  " + Environment.NewLine +
                        "from TPL_TRUCKPLAN TPL  " + Environment.NewLine +
                        "inner join TRK_TRUCK TRK ON TPL.TRK_ID = TRK.ID  " + Environment.NewLine +
                        "left join v_trk_RZN_ID_LIST RESTZ on RESTZ.TRK_ID = TRK.ID  " + Environment.NewLine +
                        "inner join CRR_CARRIER CRR ON TRK.CRR_ID = CRR.ID  " + Environment.NewLine +
                        "inner join PLN_PUBLICATEDPLAN PLN on PLN.ID = TPL.PLN_ID  " + Environment.NewLine +
                        "left join v_TPLANMAXTIME TMX ON TMX.TPL_ID = TPL.ID  " + Environment.NewLine +
                        "left join v_REPLANSTR RPS ON RPS.TPL_ID = TPL.ID  " + Environment.NewLine +
                        "left join (select max(isnull(TOR_ENTIME_R, TOR_ENTIME)) as AVAIL, TOR.TRK_ID from TOR_TOUR TOR  " + Environment.NewLine +
                        "           where isnull(TOR_STTIME_R, TOR_STTIME) <= ? and TRS_VALUE <> ? group by TOR.TRK_ID) as AV on AV.TRK_ID = TRK.ID " + Environment.NewLine +
                        "left join (select sum(TOR_SCORE) as PREVSCR, TOR.TRK_ID from TOR_TOUR TOR " + Environment.NewLine +
                        "           WHERE TOR.TOR_ENTIME between ? and ? group by TOR.TRK_ID) as RNSCR on RNSCR.TRK_ID = TRK.ID " + Environment.NewLine;

            DataTable dt;

            if (boOpt.TPL_ID <= 0)
            {
                //teljes tervezés
                sSql += "where (isnull(TPL_LOCKED,0) = 0 and TPL.TPL_AVAIL_S <= PLN_DATE_E) And TPL_DELETED <> 1 And ( TPL.TPL_NOREPLAN is null or TPL.TPL_NOREPLAN <> 1) and TPL.PLN_ID = ? ";
                dt = DBA.Query2DataTable(sSql, boOpt.PLN_DATE_E, Global.TRS_VALUE_CANCELED, boOpt.PLN_DATE_B, boOpt.PLN_DATE_E, boOpt.PLN_ID);
            }
            else
            {
                //egy túra újratervezése
                sSql += "where TPL.ID=? ";
                dt = DBA.Query2DataTable(sSql, boOpt.PLN_DATE_E, Global.TRS_VALUE_CANCELED, boOpt.PLN_DATE_B, boOpt.PLN_DATE_E, boOpt.TPL_ID);
            }

            //TODO:ttId értékadást ledebugolni !!! 
            //Opt.dicTruckType.Where( i=>i.Value.RZN_ID_LIST==Util.getFieldValue<string>(r, "RZN_ID_LIST")).ToList().First(),

            int innerID = 1;
            boOpt.dicTruck =
                    (from r in dt.AsEnumerable()
                     select new
                     {
                         Key = Util.getFieldValue<int>(r, "TRK_ID"),
                         Value = new boOptimize.CTruck
                         {
                             innerID = innerID++,
                             ID = Util.getFieldValue<int>(r, "TRK_ID"),
                             TPL_ID = Util.getFieldValue<int>(r, "TPL_ID"),

                             ttId = boOpt.dicTruckType[Util.getFieldValue<string>(r, "RZN_ID_LIST") + Global.SEP_POINT + Util.getFieldValue<string>(r, "SPP_ID")].innerID,
                             tkName = Util.getFieldValue<string>(r, "TRK_REG_NUM"),
                             depotStart = boOpt.dicDepot[Util.getFieldValue<int>(r, "WHS_ID")].innerID,
                             depotArr = boOpt.dicDepot[Util.getFieldValue<int>(r, "ARR_WHS_ID")].innerID,

                             //***setTruckInformation
                             cId = boOpt.dicCostProfile[Util.getFieldValue<int>(r, "TFP_ID")].innerID,
                             tOwned = (Util.getFieldValue<int>(r, "CRR_OWN") != 0 ? 1 : 0),
                             maxDistance = 10000000,
                             capId = boOpt.dicCapacityProfile[Util.getFieldValue<int>(r, "CPP_ID")].innerID,
                             maxWorktime = PMapIniParams.Instance.TrkMaxWorkTime, 
                             earliestStart = (int)Math.Max(Util.getFieldValue<DateTime>(r, "AVAIL").TimeOfDay.TotalMinutes - boOpt.PLN_DATE_B.Date.Subtract(Util.getFieldValue<DateTime>(r, "AVAIL").Date).TotalMinutes, boOpt.PLN_DATE_B.TimeOfDay.TotalMinutes),
                             latestStart = (int)Math.Max(Math.Max(Util.getFieldValue<DateTime>(r, "AVAIL").TimeOfDay.TotalMinutes - boOpt.PLN_DATE_B.Date.Subtract(Util.getFieldValue<DateTime>(r, "AVAIL").Date).TotalMinutes, boOpt.PLN_DATE_B.TimeOfDay.TotalMinutes),
                                            boOpt.PLN_DATE_E.TimeOfDay.TotalMinutes + boOpt.PLN_DATE_E.Date.Subtract(boOpt.PLN_DATE_B.Date).TotalMinutes),
                             dailyMax = 0,
                             counterPF1 = 0,
                             counterPF2 = 0

                         }
                     }).ToDictionary(n => n.Key, n => n.Value);
        }

        private void fillClient()
        {
            string sSql = "";
            DataTable dt;


            if (boOpt.TPL_ID <= 0)
            {
                //teljes tervezés
                sSql = "select distinct DEP_ID, DEP_NAME, DEP_SRVTIME " + Environment.NewLine +
                          "from TOD_TOURORDER TOD " + Environment.NewLine +
                          "left join DEP_DEPOT DEP on TOD.DEP_ID = DEP.ID " + Environment.NewLine +
                          "where PLN_ID = ?";
                dt = DBA.Query2DataTable(sSql, boOpt.PLN_ID);
            }
            else
            {
                //egy túra újratervezése
                sSql = "select DISTINCT DEP_ID, DEP_NAME, DEP_SRVTIME " + Environment.NewLine +
                          "from PTP_PLANTOURPOINT PTP " + Environment.NewLine +
                          "inner join TOD_TOURORDER TOD on TOD.ID = PTP.TOD_ID " + Environment.NewLine +
                          "inner join DEP_DEPOT DEP ON TOD.DEP_ID = DEP.ID " + Environment.NewLine +
                          "where PTP.TPL_ID = ? and PTP.PTP_TYPE = ? ";
                dt = DBA.Query2DataTable(sSql, boOpt.TPL_ID, Global.TUT_VALUE_DEP);
            }
            int innerID = boOpt.dicClient.Count + 1;
            boOpt.dicClient = boOpt.dicClient.Concat((from r in dt.AsEnumerable()
                                                      select new
                                                      {
                                                          Key = Util.getFieldValue<int>(r, "DEP_ID"),
                                                          Value = new boOptimize.CClient
                                                          {
                                                              innerID = innerID++,
                                                              ID = Util.getFieldValue<int>(r, "DEP_ID"),
                                                              isWHS = false,
                                                              clName = Util.getFieldValue<string>(r, "DEP_NAME"),
                                                              x = 0,
                                                              y = 0,
                                                              fixService = Util.getFieldValue<int>(r, "DEP_SRVTIME") > 0 ? Util.getFieldValue<int>(r, "DEP_SRVTIME") : 20
                                                          }
                                                      }).ToDictionary(n => n.Key, n => n.Value))
                               .ToDictionary(n => n.Key, n => n.Value);

        }

        private void fillOrder()
        {
            string sSql = "select distinct TOD.ID as TOD_ID, ORD.ID as XORD_ID, ORD.DEP_ID, ORD.ORD_NUM, ORD_QTY, DEP_OPEN, DEP_CLOSE, DEP_SRVTIME, TOD_SERVTIME, ISNULL(PUB.PUBQTY, 0) + ISNULL(LCK.LCKQTY, 0) AS PUBQTY, " + Environment.NewLine +
                    "SVT_SERVTIME_S , SVT_SERVTIME_E, ORD.ID, WHS_OPEN, TOD_DATE, OTP_VALUE, CTP_VALUE, DEP_QTYSRVTIME, DEP.NOD_ID, ORD_DATE, ORD_SERVS, ORD_SERVE, " + Environment.NewLine +
                    "ISNULL(PUB.PUBQTY1, 0) + ISNULL(LCK.LCKQTY1, 0) AS PUBQTY1, ISNULL(PUB.PUBQTY2, 0) + ISNULL(LCK.LCKQTY2, 0) AS PUBQTY2, " + Environment.NewLine +
                    "ISNULL(PUB.PUBQTY3, 0) + ISNULL(LCK.LCKQTY3, 0) AS PUBQTY3, ISNULL(PUB.PUBQTY4, 0) + ISNULL(LCK.LCKQTY4, 0) AS PUBQTY4, ISNULL(PUB.PUBQTY5, 0) + ISNULL(LCK.LCKQTY5, 0) AS PUBQTY5, " + Environment.NewLine +
                    "ISNULL(PUB.PUBVOL, 0) + ISNULL(LCK.LCKVOL, 0) AS PUBVOL, " + Environment.NewLine +
                    "ORD_ORIGQTY1, ORD_ORIGQTY2, ORD_ORIGQTY3, ORD_ORIGQTY4, ORD_ORIGQTY5, ORD_VOLUME " + Environment.NewLine +
                    "from ORD_ORDER ORD " + Environment.NewLine +
                    "inner join DEP_DEPOT DEP on ORD.DEP_ID = DEP.ID " + Environment.NewLine +
                    "inner join CTP_CARGOTYPE CTP on CTP.ID = ORD.CTP_ID " + Environment.NewLine +
                    "inner join OTP_ORDERTYPE OTP on ORD.OTP_ID = OTP.ID " + Environment.NewLine +
                    "inner join TOD_TOURORDER TOD on TOD.ORD_ID = ORD.ID " + Environment.NewLine +
                    "left join v_PLPLANQTY PLQ on PLQ.PLN_ID = TOD.PLN_ID AND PLQ.ORD_ID = TOD.ORD_ID " + Environment.NewLine +
                    "left join v_PUBQTY PUB on PUB.ORD_ID = ORD.ID " + Environment.NewLine +
                    "left join TOP_TOURPOINT TP on TP.ORD_ID = ORD.ID AND TP.TPS_VALUE <> ? " + Environment.NewLine +
                    "left join SVT_SERVICETIME SVT on SVT.CTP_ID = ORD.CTP_ID and DATEPART(dw, TOD_DATE) - 1 = SVT_DAY and SVT.DEP_ID = ORD.DEP_ID " + Environment.NewLine +
                    "left join WHS_WAREHOUSE WHS on ORD.WHS_ID = WHS.ID " + Environment.NewLine +
                    "left join v_PLLOCKQTY LCK on LCK.ORD_ID = TOD.ORD_ID AND LCK.PLN_ID = TOD.PLN_ID " + Environment.NewLine;


            DataTable dt;

            if (boOpt.TPL_ID <= 0)
            {
                //teljes tervezés
                sSql += "left join PTP_PLANTOURPOINT PTP on PTP.TOD_ID = TOD.ID and PTP.PTP_TYPE= ? " + Environment.NewLine +
                        "left join TPL_TRUCKPLAN TPL on TPL.ID = PTP.TPL_ID " + Environment.NewLine +
                        "where TOD.PLN_ID = ? and isnull(TPL.TPL_LOCKED,0) = 0 and (isnull(PUB.PUBQTY, 0) + isnull(LCK.LCKQTY, 0) < ORD_QTY) and OTP_VALUE <> ? " + Environment.NewLine +
                        "order by ORD.ORD_NUM";
                dt = DBA.Query2DataTable(sSql, Global.TPS_VALUE_DEL, Global.PTP_TPOINT, boOpt.PLN_ID, Global.OTP_UNLOAD);
            }
            else
            {
                //egy túra újratervezése
                sSql += "inner join PTP_PLANTOURPOINT PTP on PTP.TOD_ID = TOD.ID and PTP.TPL_ID= ?  and PTP.PTP_TYPE= ? " + Environment.NewLine +
                        " order by ORD.ORD_NUM";
                dt = DBA.Query2DataTable(sSql, Global.TPS_VALUE_DEL, boOpt.TPL_ID, Global.TUT_VALUE_DEP, Global.OTP_UNLOAD);
            }
            int innerID = 1;
            foreach (DataRow dr in dt.Rows)
            {
                boOptimize.COrder ord = new boOptimize.COrder();
                ord.innerID = innerID++;
                ord.ID = Util.getFieldValue<int>(dr, "XORD_ID");
                ord.clId = boOpt.dicClient[Util.getFieldValue<int>(dr, "DEP_ID")].innerID;

                if (boOpt.dicClient[Util.getFieldValue<int>(dr, "DEP_ID")].clName == "CBA ART-KER.FRISS KFT.")
                    Console.WriteLine("x");


                ord.TOD_ID = Util.getFieldValue<int>(dr, "TOD_ID");
                ord.NOD_ID = Util.getFieldValue<int>(dr, "NOD_ID");

                //Megjelöljük, ha a megrendelés dátuma nem egyezik meg a részmegrendelés dátumával
                ord.TOD_DATE = Util.getFieldValue<DateTime>(dr, "TOD_DATE");
                ord.isDiffDates = (Util.getFieldValue<DateTime>(dr, "ORD_DATE") != ord.TOD_DATE);

                ord.dQty1 = Util.getFieldValue<double>(dr, "ORD_ORIGQTY1") - Util.getFieldValue<double>(dr, "PUBQTY1");
                ord.dQty2 = Util.getFieldValue<double>(dr, "ORD_ORIGQTY2") - Util.getFieldValue<double>(dr, "PUBQTY2");
                ord.dQty3 = Util.getFieldValue<double>(dr, "ORD_ORIGQTY3") - Util.getFieldValue<double>(dr, "PUBQTY3");
                ord.dQty4 = Util.getFieldValue<double>(dr, "ORD_ORIGQTY4") - Util.getFieldValue<double>(dr, "PUBQTY4");//a dohányárut nem osztjuk
                ord.dQty5 = Util.getFieldValue<double>(dr, "ORD_ORIGQTY5") - Util.getFieldValue<double>(dr, "PUBQTY5");
                ord.dVolume = Util.getFieldValue<double>(dr, "ORD_VOLUME") - Util.getFieldValue<double>(dr, "PUBVOL");
                ord.dQty = Convert.ToInt32(m_bllPlanEdit.GetOrdQty(ord.dQty1, ord.dQty2, ord.dQty3, ord.dQty5));
                if (Util.getFieldValue<int>(dr, "OTP_VALUE") == Global.OTP_INPUT || Util.getFieldValue<int>(dr, "OTP_VALUE") == Global.OTP_UNLOAD)
                {
                    ord.dQty *= -1;
                }
                ord.orLoad1 = Convert.ToInt32(ord.dQty * Global.csQTY_DEC);
                ord.orLoad2 = Convert.ToInt32(ord.dVolume * Global.csVolumeMultiplier);      //dm3-->m3 konverzió

                ord.mb = 0;
                ord.prType = Util.getFieldValue<int>(dr, "CTP_VALUE");

                ord.readyTime = (int)Math.Max(Util.getFieldValue<int>(dr, "WHS_OPEN"), ord.TOD_DATE.TimeOfDay.TotalMinutes + ord.TOD_DATE.Date.Subtract(boOpt.PLN_DATE_B.Date).TotalMinutes);
                ord.depot = 0;
                ord.stayAfter = 1440;

                ord.canCut = Util.getFieldValue<int>(dr, "CTP_VALUE") == Global.CTP_VALUE_DRY ? 1 : 0;

                //Kiszolgálási idõ (10 kg-ra van megadva)
                ord.orServiceTime = (int)Math.Max(Util.getFieldValue<int>(dr, "DEP_SRVTIME") + Math.Ceiling(Math.Abs(ord.dQty) * Util.getFieldValue<double>(dr, "DEP_QTYSRVTIME") / (Global.csQTYSRVDivider)), 1);

                //Idõablak
                int SERVS = Math.Max(Util.getFieldValue<int>(dr, "ORD_SERVS"), Util.getFieldValue<int>(dr, "SVT_SERVTIME_S") > 0 ? Util.getFieldValue<int>(dr, "SVT_SERVTIME_S") : boOpt.MinTime);
                if (SERVS < Util.getFieldValue<int>(dr, "DEP_OPEN"))
                    SERVS = Util.getFieldValue<int>(dr, "DEP_OPEN");
                ord.orMinTime = SERVS;

                int SERVE = Math.Min(Util.getFieldValue<int>(dr, "ORD_SERVE"), Util.getFieldValue<int>(dr, "SVT_SERVTIME_E") > 0 ? Util.getFieldValue<int>(dr, "SVT_SERVTIME_E") : boOpt.MaxTime);
                if (SERVE > Util.getFieldValue<int>(dr, "DEP_CLOSE"))
                    SERVE = Util.getFieldValue<int>(dr, "DEP_CLOSE");
                ord.orMaxTime = SERVE;

                ord.client = boOpt.dicClient[Util.getFieldValue<int>(dr, "DEP_ID")];
                boOpt.dicOrder.Add(ord.ID, ord);

            }
        }

        private void fillOrderTruck()
        {
            DataTable dt;
            string sSql = "select distinct TPL.ID AS TPL_ID, TRK.ID as TRK_ID, ORD_ID " + Environment.NewLine +
                          "from TOD_TOURORDER TOD " + Environment.NewLine +
                          "inner join DPT_DEPTRUCK dpt ON TOD.DEP_ID = DPT.DEP_ID " + Environment.NewLine;
            if (boOpt.TPL_ID <= 0)
            {
                //teljes tervezés
                sSql += " inner join TPL_TRUCKPLAN TPL on TPL.TRK_ID = DPT.TRK_ID and TPL.PLN_ID = ? and TPL_DELETED <> 1 " + Environment.NewLine;
            }
            else
            {
                //Csak egy jármûre tervezés
                sSql += " inner join TPL_TRUCKPLAN TPL on TPL.TRK_ID = DPT.TRK_ID and TPL.ID= ? " + Environment.NewLine +
                        " inner join PTP_PLANTOURPOINT PTP on PTP.TOD_ID = TOD.ID and PTP.TPL_ID= ?   and PTP.PTP_TYPE= ? " + Environment.NewLine;

            }

            //"inner join DST_DISTANCE DST on DST.RZN_ID_LIST = RZN.RZN_ID_LIST and (DST.NOD_ID_FROM=DEP.NOD_ID or DST.NOD_ID_TO=DEP.NOD_ID) and (DST.NOD_ID_FROM=WHS.NOD_ID or DST.NOD_ID_TO=WHS.NOD_ID) and DST.DST_DISTANCE >= 0 " + Environment.NewLine +

            sSql += "inner join TRK_TRUCK TRK on TRK.ID = TPL.TRK_ID " + Environment.NewLine +
                 "inner join v_trk_RZN_ID_LIST RZN on RZN.TRK_ID=TRK.ID " + Environment.NewLine +
                 "inner join ORD_ORDER ORD on ORD.ID = TOD.ORD_ID " + Environment.NewLine +
                 "inner join WHS_WAREHOUSE WHS on WHS.ID = ? " + Environment.NewLine +
                 "inner join DEP_DEPOT DEP on DEP.ID = TOD.DEP_ID " + Environment.NewLine +
                 "inner join PLN_PUBLICATEDPLAN PLN on PLN.ID = TPL.PLN_ID " + Environment.NewLine +
                 "where TOD.PLN_ID = ? and TPL.TPL_AVAIL_S <= PLN_DATE_E  " + Environment.NewLine +
                 " and  isnull( TPL.TPL_NOREPLAN, 0) = 0 and isnull( TPL.TPL_LOCKED,0) = 0 and ORD.CTP_ID in ( select CTP_ID from TCP_TRUCKCARGOTYPE TCP where TCP.TRK_ID = TRK.ID) " + Environment.NewLine +
                 " and (TRK_LENGTH is null OR TRK_LENGTH=0 or TRK_LENGTH>=ORD_LENGTH) " + Environment.NewLine +
                 " and (TRK_WIDTH is null  OR TRK_WIDTH =0 or TRK_WIDTH >=ORD_WIDTH)  " + Environment.NewLine +
                 " and (TRK_HEIGHT is null OR TRK_HEIGHT=0 or TRK_HEIGHT>=ORD_HEIGHT) ";

            if (boOpt.TPL_ID <= 0)
            {
                //teljes tervezés
                dt = DBA.Query2DataTable(sSql, boOpt.PLN_ID, boOpt.WHS_ID, boOpt.PLN_ID);
            }
            else
            {
                //Csak egy jármûre tervezés
                dt = DBA.Query2DataTable(sSql, boOpt.TPL_ID, boOpt.TPL_ID, Global.TUT_VALUE_DEP, boOpt.WHS_ID, boOpt.PLN_ID);
            }

            foreach (DataRow dr in dt.Rows)
            {
                boOpt.dicOrder[Util.getFieldValue<int>(dr, "ORD_ID")].lstOrderTruck.Add(boOpt.dicTruck[Util.getFieldValue<int>(dr, "TRK_ID")]);
            }
        }

        private void fillRelationAccess(BaseProgressDialog p_notify)
        {

            DateTime dtStart = DateTime.Now;
            if (p_notify != null)
                p_notify.SetInfoText(PMapMessages.M_OPT_DST_QUERY);
            else
                Console.WriteLine(PMapMessages.M_OPT_DST_QUERY);

            Dictionary<int, boEdge> lstAllEdges = new Dictionary<int, boEdge>();

            DataTable dt;
            string sSql = "";
            if (boOpt.TPL_ID <= 0)
            {
                if (p_notify != null)
                    p_notify.SetInfoText(PMapMessages.M_OPT_QEDGES);
                else
                    Console.WriteLine(PMapMessages.M_OPT_QEDGES);

                DataTable dtEdg = m_bllRoute.GetEdgesToDT();
                lstAllEdges = (from r in dtEdg.AsEnumerable()
                               select new
                               {
                                   Key = Util.getFieldValue<int>(r, "ID"),
                                   Value = new boEdge()
                                   {
                                       ID = Util.getFieldValue<int>(r, "ID"),
                                       NOD_ID_FROM = Util.getFieldValue<int>(r, "NOD_NUM"),
                                       NOD_ID_TO = Util.getFieldValue<int>(r, "NOD_NUM2"),
                                       EDG_NAME = Util.getFieldValue<string>(r, "EDG_NAME"),
                                       EDG_LENGTH = Util.getFieldValue<float>(r, "EDG_LENGTH"),
                                       RDT_VALUE = Util.getFieldValue<int>(r, "RDT_VALUE")
                                   }
                               }).ToDictionary(n => n.Key, n => n.Value);

                if (PMapIniParams.Instance.LogVerbose >= PMapIniParams.eLogVerbose.debug)
                    Util.Log2File(" lstAllEdges: " + (DateTime.Now - dtStart).TotalSeconds.ToString() + " s");
                dtStart = DateTime.Now;


                //teljes tervezés
                sSql = "select * from " + Environment.NewLine +
                        "		( " + Environment.NewLine +
                        "		select  * from  " + Environment.NewLine +
                        "			(select NOD_FROM.ID as ID_FROM, NOD_TO.ID as ID_TO, NOD_FROM.NOD_ID as NOD_ID_FROM, NOD_TO.NOD_ID as NOD_ID_TO   " + Environment.NewLine +
                        "			 from (select distinct ID * -1 as ID, NOD_ID from WHS_WAREHOUSE (NOLOCK) WHS  " + Environment.NewLine +
                        "				   union  " + Environment.NewLine +
                        "				   select distinct DEP.ID, NOD_ID from DEP_DEPOT (NOLOCK) DEP  " + Environment.NewLine +
                        "				   inner join TOD_TOURORDER (NOLOCK) TOD on TOD.PLN_ID = ? and TOD.DEP_ID = DEP.ID " + Environment.NewLine +
                        "				   ) NOD_FROM  " + Environment.NewLine +
                        "			inner join (select distinct ID * -1 as ID, NOD_ID from WHS_WAREHOUSE (NOLOCK) WHS   " + Environment.NewLine +
                        "				   union  " + Environment.NewLine +
                        "				  select distinct DEP.ID, NOD_ID  from DEP_DEPOT (NOLOCK) DEP  " + Environment.NewLine +
                        "				  inner join TOD_TOURORDER (NOLOCK) TOD on TOD.PLN_ID = ? and TOD.DEP_ID = DEP.ID " + Environment.NewLine +
                        "				  ) NOD_TO on NOD_TO.NOD_ID <> NOD_FROM.NOD_ID  " + Environment.NewLine +
                        "			) Q1, " + Environment.NewLine +
                        "			(select distinct RZN.RZN_ID_LIST, TRK.SPP_ID " + Environment.NewLine +
                        "			  from TPL_TRUCKPLAN (NOLOCK) TPL " + Environment.NewLine +
                        "			  inner join v_trk_RZN_ID_LIST (NOLOCK) RZN on RZN.TRK_ID  = TPL.TRK_ID " + Environment.NewLine +
                        "			  inner join TRK_TRUCK (NOLOCK) TRK on TRK.ID = TPL.TRK_ID " + Environment.NewLine +
                        "			  where TPL.PLN_ID = ? and isnull(TPL.TPL_LOCKED,0) = 0 " + Environment.NewLine +
                        "			  ) Q2) NODES " + Environment.NewLine +
                        "	inner join DST_DISTANCE (NOLOCK) DST on DST.RZN_ID_LIST=NODES.RZN_ID_LIST and DST.NOD_ID_FROM = NODES.NOD_ID_FROM and DST.NOD_ID_TO=NODES.NOD_ID_TO " + Environment.NewLine +
                        "order by NODES.NOD_ID_FROM, NODES.NOD_ID_TO, NODES.SPP_ID, NODES.RZN_ID_LIST,  DST.DST_EDGES ";

                dt = DBA.Query2DataTable(sSql, boOpt.PLN_ID, boOpt.PLN_ID, boOpt.PLN_ID);
            }
            else
            {
                //Csak egy jármûre tervezés

                sSql = "select * from " + Environment.NewLine +
                        "	( " + Environment.NewLine +
                        "	select * from " + Environment.NewLine +
                        "     (select distinct NOD_FROM.ID as ID_FROM, NOD_TO.ID as ID_TO, NOD_FROM.NOD_ID as NOD_ID_FROM, NOD_TO.NOD_ID as NOD_ID_TO  from " + Environment.NewLine +
                        "		(select ISNULL( DEP.ID, WHS.ID*-1) as ID,  PTP.NOD_ID " + Environment.NewLine +
                        "			from PTP_PLANTOURPOINT (NOLOCK)  PTP " + Environment.NewLine +
                        "			left outer join WHS_WAREHOUSE (NOLOCK) WHS on WHS.ID = PTP.WHS_ID " + Environment.NewLine +
                        "			left outer join TOD_TOURORDER (NOLOCK) TOD on TOD.ID = PTP.TOD_ID " + Environment.NewLine +
                        "			left outer join DEP_DEPOT (NOLOCK) DEP on DEP.ID = TOD.DEP_ID " + Environment.NewLine +
                        "			where TPL_ID = ? " + Environment.NewLine +
                        "			) NOD_FROM " + Environment.NewLine +
                        "		inner join (select ISNULL( DEP.ID, WHS.ID*-1) as ID, PTP.NOD_ID  " + Environment.NewLine +
                        "					from PTP_PLANTOURPOINT (NOLOCK) PTP " + Environment.NewLine +
                        "					left outer join WHS_WAREHOUSE (NOLOCK) WHS on WHS.ID = PTP.WHS_ID " + Environment.NewLine +
                        "					left outer join TOD_TOURORDER (NOLOCK) TOD on TOD.ID = PTP.TOD_ID " + Environment.NewLine +
                        "					left outer join DEP_DEPOT (NOLOCK) DEP on DEP.ID = TOD.DEP_ID " + Environment.NewLine +
                        "					where TPL_ID = ?) NOD_TO on NOD_TO.NOD_ID <> NOD_FROM.NOD_ID " + Environment.NewLine +
                        "	) Q1, " + Environment.NewLine +
                        "	(select distinct RZN.RZN_ID_LIST, TRK.SPP_ID " + Environment.NewLine +
                        "	 from TPL_TRUCKPLAN (NOLOCK) TPL " + Environment.NewLine +
                        "	 inner join v_trk_RZN_ID_LIST (NOLOCK) RZN on RZN.TRK_ID  = TPL.TRK_ID " + Environment.NewLine +
                        "	 inner join TRK_TRUCK (NOLOCK) TRK on TRK.ID = TPL.TRK_ID " + Environment.NewLine +
                        "	 where TPL.ID = ? " + Environment.NewLine +
                        "	 ) Q2 ) NODES " + Environment.NewLine +
                        "inner join DST_DISTANCE (NOLOCK) DST on DST.RZN_ID_LIST =NODES.RZN_ID_LIST and DST.NOD_ID_FROM = NODES.NOD_ID_FROM and  DST.NOD_ID_TO = NODES.NOD_ID_TO " + Environment.NewLine +
                        "order by ID_FROM,ID_TO, NODES.SPP_ID, NODES.RZN_ID_LIST,  DST.DST_EDGES ";

                dt = DBA.Query2DataTable(sSql, boOpt.TPL_ID, boOpt.TPL_ID, boOpt.TPL_ID);
            }

            if (PMapIniParams.Instance.LogVerbose >= PMapIniParams.eLogVerbose.debug)
                Util.Log2File(" FillRelationAccess query: " + (DateTime.Now - dtStart).TotalSeconds.ToString() + " s");
            dtStart = DateTime.Now;

            int lastNOD_ID_FROM = 0;
            int lastNOD_ID_TO = 0;

            int lastSPP_ID = 0;
            int lastDuration = 0;
            int lastDistance = -1;
            int itemNo = 0;
            List<boEdge> lstSelEdges = new List<boEdge>();
            Dictionary<string, boSpeedProfValues> dicSPV = m_bllSpeedProf.GetSpeedValuesToDict();



            foreach (DataRow dr in dt.Rows)
            {

                if (++itemNo % 100 == 0 && p_notify != null)
                    p_notify.SetInfoText(String.Format(PMapMessages.M_OPT_DST_PROC, itemNo, dt.Rows.Count));

                int duration = lastDuration;
                if (lastNOD_ID_FROM != Util.getFieldValue<int>(dr, "NOD_ID_FROM") ||
                     lastNOD_ID_TO != Util.getFieldValue<int>(dr, "NOD_ID_TO") ||
                     lastDistance != Util.getFieldValue<int>(dr, "DST_DISTANCE"))
                {
                    lastNOD_ID_FROM = Util.getFieldValue<int>(dr, "NOD_ID_FROM");
                    lastNOD_ID_TO = Util.getFieldValue<int>(dr, "NOD_ID_TO");
                    lastSPP_ID = -1;
                    lstSelEdges.Clear();

                    if (Util.getFieldValue<int>(dr, "DST_DISTANCE") > 0)
                    {

                        byte[] buff = Util.getFieldValue<byte[]>(dr, "DST_EDGES");
                        if (buff.Length > 0)
                        {
                            String edges = Util.UnZipStr(buff);
                            if (boOpt.TPL_ID <= 0)
                            {

                                List<int> IDs = edges.Split(',').Select(s => int.Parse(s)).ToList();
                                foreach (int id in IDs)
                                    lstSelEdges.Add(lstAllEdges[id]);
                            }
                            else
                            {
                                sSql = String.Format("select * from EDG_EDGE (NOLOCK) EDG " + Environment.NewLine +
                                                    "where EDG.ID in ({0}) ", edges);

                                DataTable dtEdg2 = DBA.Query2DataTable(sSql);
                                lstSelEdges = (from r in dtEdg2.AsEnumerable()
                                               select new boEdge()
                                                   {
                                                       ID = Util.getFieldValue<int>(r, "ID"),
                                                       NOD_ID_FROM = Util.getFieldValue<int>(r, "NOD_NUM"),
                                                       NOD_ID_TO = Util.getFieldValue<int>(r, "NOD_NUM2"),
                                                       EDG_NAME = "",
                                                       EDG_LENGTH = Util.getFieldValue<float>(r, "EDG_LENGTH"),
                                                       RDT_VALUE = Util.getFieldValue<int>(r, "RDT_VALUE"),
                                                   }
                                               ).ToList();
                            }

                        }
                    }
                    else
                    {
                        lstSelEdges.Clear();
                    }
                }

                if (lastSPP_ID != Util.getFieldValue<int>(dr, "SPP_ID"))
                {
                    lastSPP_ID = Util.getFieldValue<int>(dr, "SPP_ID");
                    duration = (int)Math.Round(lstSelEdges.Sum(i => i.EDG_LENGTH / (dicSPV[i.RDT_VALUE + Global.SEP_COORD + lastSPP_ID.ToString()].SPV_VALUE / 3.6 * 60)));
                }

                lastDuration = duration;

                //lastEdges = edges;

                lastDistance = Util.getFieldValue<int>(dr, "DST_DISTANCE");

                //ha nincs távolság a két pont között, akkor max értéket adumk meg. 
                //evvel azt érjük el, hogy a nyitva tartási időn belül ne lehessen
                //kiszolgálni egy túrapontot.
                if (lastDistance == 0)
                {
                    lastDistance = 9999999;
                    lastDuration = 1440 * 2;
                }

                boOptimize.CTruckType ttype = boOpt.dicTruckType[Util.getFieldValue<string>(dr, "RZN_ID_LIST") + Global.SEP_POINT + Util.getFieldValue<string>(dr, "SPP_ID")];
                string sKey = ttype.innerID.ToString() + Global.SEP_POINT + Util.getFieldValue<int>(dr, "ID_FROM").ToString() + Global.SEP_POINT + Util.getFieldValue<int>(dr, "ID_TO").ToString();

                boOpt.lstRelationAccess.Add(new boOptimize.CRelationAccess()
                {
                    ttId = boOpt.dicTruckType[Util.getFieldValue<string>(dr, "RZN_ID_LIST") + Global.SEP_POINT + Util.getFieldValue<string>(dr, "SPP_ID")].innerID,
                    clIdStart = boOpt.dicClient[Util.getFieldValue<int>(dr, "ID_FROM")].innerID,
                    clIdEnd = boOpt.dicClient[Util.getFieldValue<int>(dr, "ID_TO")].innerID,
                    clDistance = lastDistance,
                    clTime = lastDuration
                });

            }
            if (PMapIniParams.Instance.LogVerbose >= PMapIniParams.eLogVerbose.debug)
                Util.Log2File(" calc durations: " + (DateTime.Now - dtStart).TotalSeconds.ToString() + " s");
            dtStart = DateTime.Now;
        }


        private void fillPlanTours(BaseProgressDialog p_notify)
        {
            string sSql = "select PTP.TPL_ID, PTP.PTP_ORDER, PTP.PTP_TYPE, TOD.ORD_ID, DATEDIFF(n, PLN_DATE_B, PTP_ARRTIME) AS ARR, " + Environment.NewLine +
                "DATEDIFF(n, PLN_DATE_B, PTP_SERVTIME) AS SRV, DATEDIFF(n, PLN_DATE_B, PTP_DEPTIME) AS DEP, " + Environment.NewLine +
                "TOD.TOD_QTY, PTP.PTP_DISTANCE, PTP.PTP_TIME, TOD.ORD_ID," + Environment.NewLine +
                "isnull( KMCOST,0) + isnull( HOURCOST,0)  AS COST, PTPE_ORDER - PTP.PTP_ORDER + 1 AS NODECNT, TOURCOUNT, OTP_VALUE, " + Environment.NewLine +
                "TPL.TRK_ID " + Environment.NewLine +
                "from PTP_PLANTOURPOINT (NOLOCK) PTP " + Environment.NewLine +
                "left join TOD_TOURORDER (NOLOCK) TOD on PTP.TOD_ID = TOD.ID " + Environment.NewLine +
                "left join ORD_ORDER (NOLOCK) ORD on ORD.ID = TOD.ORD_ID " + Environment.NewLine +
                "left join OTP_ORDERTYPE (NOLOCK) OTP on OTP.ID = ORD.OTP_ID " + Environment.NewLine +
                "left join TPL_TRUCKPLAN (NOLOCK) TPL on PTP.TPL_ID = TPL.ID " + Environment.NewLine +
                "left join PLN_PUBLICATEDPLAN (NOLOCK) PLN on TPL.PLN_ID = PLN.ID " + Environment.NewLine +
                "left join v_PLTOURKMCOST (NOLOCK) KMC on TPL.ID = KMC.TPL_ID and PTP.PTP_ORDER = KMC.PTP_ORDER " + Environment.NewLine +
                "left join v_PLTOURHOURCOST (NOLOCK) HRC on TPL.ID = HRC.TPL_ID and HRC.START_PTP_ID = PTP.ID " + Environment.NewLine +
                "left join v_PLTOURQTY (NOLOCK) PTQ on TPL.ID = PTQ.TPL_ID AND PTP.PTP_ORDER = PTQ.PTP_ORDER " + Environment.NewLine +
                "left join v_PLTOUR_COUNT (NOLOCK) PTC on PTC.TPL_ID = TPL.ID " + Environment.NewLine;

            DataTable dt;
            if (boOpt.TPL_ID <= 0)
            {
                //teljes tervezés
                sSql += "where TPL.PLN_ID = ? AND TPL.TPL_LOCKED = 0 ORDER BY PTP.TPL_ID, PTP.PTP_ORDER";
                dt = DBA.Query2DataTable(sSql, boOpt.PLN_ID);
            }
            else
            {
                //egy túra újratervezése
                sSql += "where TPL.ID = ? ORDER BY PTP.TPL_ID, PTP.PTP_ORDER";
                dt = DBA.Query2DataTable(sSql, boOpt.TPL_ID);
            }

            int TPL_ID = 0;
            int RouteNodeIndex = 0;
            boOptimize.CPlanTours pt = null;
            foreach (DataRow dr in dt.Rows)
            {
                if (Util.getFieldValue<int>(dr, "PTP_ORDER") == 0)
                {

                    if (TPL_ID != Util.getFieldValue<int>(dr, "TPL_ID"))
                    {
                        pt = new boOptimize.CPlanTours()
                            {
                                TRK_ID = Util.getFieldValue<int>(dr, "TRK_ID"),
                                TOURCOUNT = Util.getFieldValue<int>(dr, "TOURCOUNT"),
                                Cost = Util.getFieldValue<double>(dr, "COST")
                            };
                        pt.RouteExe = new List<boOptimize.CPlanTours.CRouteExe>();
                        boOpt.lstPlanTours.Add(pt);
                    }
                    RouteNodeIndex = 0;
                }

                boOptimize.CPlanTours.CRouteExe rex = new boOptimize.CPlanTours.CRouteExe()
                {
                    tkRouteIndex = pt.TOURCOUNT,                                //a körút sorszáma
                    tkRouteNodeIndex = RouteNodeIndex,                          //a kért csomópont sorszáma (1-től getRouteNodesCount(tkId,tkRouteIndex) -ig)
                    NodeType = Util.getFieldValue<int>(dr, "PTP_TYPE") == 2 ? 0 : 1, //csomópont típusa. 0 = megrendelés, 1 = telephely. Ha OrId nagyobb mint 1000 akkor a NodeType a céldepó azonosítóját tartalmazza.
                    OrId = Util.getFieldValue<int>(dr, "ORD_ID"),               //telephely vagy megrendelés azonosító. Ha áttárolásos megrendelésről van szó, amit a motor automatikusan létrehozott, akkor az eredeti megrendelés azonosítójához hozzáad 1000-t, így az eredeti megrendelés is beazonosítható (feltételezzük, hogy a normál megrendelésszám kisebb, mint 1000).
                    ArrTime = Util.getFieldValue<int>(dr, "PTP_TYPE") == 0 ? -1 : Util.getFieldValue<int>(dr, "ARR"),  //érkezési időpont a csomóponthoz (időegységben), vagy -1 ha a kamion telephelyéről van szó (nap kezdete)
                    DepTime = Util.getFieldValue<int>(dr, "PTP_TYPE") == 1 ? -1 : Util.getFieldValue<int>(dr, "DEP"),   //indulási időpont a csomóponttól (időegységben), vagy -1 ha a kamion telephelyéről van szó (nap vége)
                    quantity = Util.getFieldValue<int>(dr, "PTP_TYPE") == 2 ? Util.getFieldValue<double>(dr, "TOD_QTY") : 0, //a túrában szállított mennyiség az OrId azonosítójú megrendelésből. Ha a „megrendelésosztás” funkció nincs bekapcsolva, akkor az eredeti mennyiség jelenik meg itt. Telephely esetén az érték 0
                };
                pt.RouteExe.Add(rex);
                pt.Qty += Util.getFieldValue<double>(dr, "TOD_QTY");
                pt.Distance += Util.getFieldValue<int>(dr, "PTP_DISTANCE"); //távolság (m)
                pt.Duration += Util.getFieldValue<int>(dr, "PTP_TIME");      //idő (perc)
            }
        }


        private void removePointsBeforeReading()
        {

            bllHistory.WriteHistory(0, "removePointsBeforeReading", 0, bllHistory.EMsgCodes.FUNC, "");
            string sSql = "";
            if (boOpt.TPL_ID <= 0)
            {
                //Kitörlöm a túrapontokat
                sSql = "delete from PTP_PLANTOURPOINT where TPL_ID IN (" + Environment.NewLine +
                          "select distinct ID from TPL_TRUCKPLAN where PLN_ID = ? and TPL_LOCKED = 0)";

                DBA.ExecuteNonQuery(sSql, boOpt.PLN_ID);

                //Kitörlöm a megrendeléseket
                sSql = "delete from TOD_TOURORDER where PLN_ID = ?  and ID not in " + Environment.NewLine +
                          "(select TOD_ID from v_LOCKTOD where PLN_ID =? )";

                DBA.ExecuteNonQuery(sSql, boOpt.PLN_ID, boOpt.PLN_ID);
            }
            else
            {
                //'Kitörlöm a megrendeléseket
                sSql = "delete from TOD_TOURORDER where ID in ( select TOD_ID from PTP_PLANTOURPOINT where TPL_ID= ?)";
                DBA.ExecuteNonQuery(sSql, boOpt.TPL_ID);

                //Kitörlöm a túrapontokat
                sSql = "delete from PTP_PLANTOURPOINT where TPL_ID=? ";
                DBA.ExecuteNonQuery(sSql, boOpt.TPL_ID);
            }

            //tervhibák törlése
            sSql = "update PLE_PLANERROR set PLE_DELETED = 1 where PLN_ID = ? ";
            DBA.ExecuteNonQuery(sSql, boOpt.PLN_ID);
        }





        //Eredmény bejegyzései

        //private const string getRoutesCount = "getRoutesCount";
        private const string getRouteNodesCount = "getRouteNodesCount";
        private const string getRouteNodeExe = "getRouteNodeExe";
        //private const string getRouteDuration = "getRouteDuration";
        //private const string getRouteLength = "getRouteLength";
        //private const string getRouteLoad = "getRouteLoad";
        //private const string getRouteCost = "getRouteCost";
        private const string getIgnoredOrder = "getIgnoredOrder";
        //private const string getIgnoredOrdersCount = "getIgnoredOrdersCount";
        //private const string popError = "popError";

        //getRouteNodeExe paraméterek
        public const int par_tkId = 0;
        public const int par_tkRouteIndex = 1;
        public const int par_tkRouteNodeIndex = 2;
        public const int par_ErrCode = 3;
        public const int par_NodeType = 4;
        public const int par_OrId = 5;
        public const int par_ArrTime = 6;
        public const int par_DepTime = 7;
        public const int par_quantity = 8;

        public void ProcessResult(string p_resultFile, BaseProgressDialog p_notify)
        {

            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {

                    bllPlanEdit ple = new bllPlanEdit(DBA);

                    bllHistory.WriteHistory(0, "ProcessResult", boOpt.PLN_ID, bllHistory.EMsgCodes.FUNC, "");

                    removePointsBeforeReading();

                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(p_resultFile);
                    boOptimize.CTruck currTrk = null;
                    int PTP_ORDER = 0;


                    while ((line = file.ReadLine()) != null)
                    {
                        string[] aFn = line.Split('(');
                        if (aFn[0] == getRouteNodesCount)
                        {
                            if (currTrk != null)
                            {
                                m_bllPlanEdit.RecalcTour(0, currTrk.TPL_ID, Global.defWeather);
                            }
                            string[] aArgs = aFn[1].Replace(")", "").Split(',');

                            if (currTrk == null || currTrk.innerID != Convert.ToInt32(aArgs[0]))
                            {
                                currTrk = boOpt.dicTruck.Where(i => i.Value.innerID == Convert.ToInt32(aArgs[0])).First().Value;
                                PTP_ORDER = 0;

                                if (p_notify != null)
                                    p_notify.SetInfoText(String.Format(PMapMessages.M_OPT_RES_TRK, currTrk.tkName));


                            }
                        }
                        else if (aFn[0] == getRouteNodeExe)
                        {

                            string[] aArgs = aFn[1].Replace(")", "").Split(',');


                            //       ElseIf left(sLine, iStartLen) = GETROUTENODE Then
                            //m_oOptimize.ParseLine Mid(sLine, iStartLen + 2, Len(sLine) - iStartLen - 2), dtStartDate

                            if (aArgs[par_ErrCode] == "0")
                            {
                                int orderIdx = Convert.ToInt32(aArgs[par_tkRouteNodeIndex]);

                                if (aArgs[par_NodeType] == "0")
                                {
                                    //túrapont
                                    boOptimize.COrder ord = boOpt.dicOrder.Where(i => i.Value.innerID == Convert.ToInt32(aArgs[par_OrId])).First().Value;

                                    //Mennyiség-térfogat
                                    //a felszorzott értet visszaosztjuk. (mivel a mennyiségeket 2 tizedesjegyűek is lehetnek,
                                    //de csak egész számok adtahóak át a PVRP-nek.)
                                    double dLoad1 = Math.Max(Convert.ToDouble(aArgs[par_quantity].Replace(',', '.'), CultureInfo.InvariantCulture), 0); //Mivel csak az szétdarabolás arányok 
                                    //meghatározására használjuk a dLoad1-et, nem osztunk vissza

                                    //megrendelés feleosztás esetén arányosítunk

                                    double dVolume = Math.Round(ord.dVolume * ord.orLoad1 / dLoad1, 2); // A térfogat 2 dec. jegyig értelmezett
                                    double dQty = Math.Ceiling(ord.dQty * ord.orLoad1 / dLoad1);
                                    double dQty1 = Math.Ceiling(ord.dQty1 * ord.orLoad1 / dLoad1);
                                    double dQty2 = Math.Ceiling(ord.dQty2 * ord.orLoad1 / dLoad1);
                                    double dQty3 = Math.Ceiling(ord.dQty3 * ord.orLoad1 / dLoad1);
                                    double dQty4 = Math.Ceiling(ord.dQty4 * ord.orLoad1 / dLoad1);
                                    double dQty5 = Math.Ceiling(ord.dQty5 * ord.orLoad1 / dLoad1);


                                    int TOD_ID = ple.CreatePlanOrder(boOpt.PLN_ID, ord.ID,
                                                    dQty, dQty1, dQty2, dQty3, dQty4, dQty5, dVolume,
                                                    ple.getNEWTODNUM(boOpt.PLN_ID, ord.ID), (ord.isDiffDates ? (DateTime?)ord.TOD_DATE : null));


                                    m_bllPlanEdit.CreateTourPoint(currTrk.TPL_ID, TOD_ID, ord.NOD_ID, PTP_ORDER, 0, 0,
                                        DateTime.Now, DateTime.Now, DateTime.Now, -1, Global.PTP_TPOINT, 0, 0);

                                    PTP_ORDER++;
                                }
                                if (aArgs[par_NodeType] == "1")
                                {
                                    //raktár

                                    if (PTP_ORDER == 0)
                                    {
                                        //legelső túrapont
                                        DateTime dtArr = boOpt.PLN_DATE_B.Date.AddMinutes(Convert.ToInt32(aArgs[par_ArrTime]));
                                        DateTime dtSrv = dtArr;
                                        DateTime dtDep = boOpt.PLN_DATE_B.Date.AddMinutes(Convert.ToInt32(aArgs[par_DepTime]));
                                        m_bllPlanEdit.CreateTourPoint(currTrk.TPL_ID, 0, m_boWarehouse.NOD_ID, PTP_ORDER, 0, 0,
                                            dtArr, dtSrv, dtDep, boOpt.WHS_ID, Global.PTP_TYPE_WHS_S, 0, 0);
                                    }
                                    else
                                    {
                                        m_bllPlanEdit.CreateTourPoint(currTrk.TPL_ID, 0, m_boWarehouse.NOD_ID, PTP_ORDER, 0, 0,
                                            DateTime.Now, DateTime.Now, DateTime.Now, boOpt.WHS_ID, Global.PTP_TYPE_WHS_E, 0, 0);
                                    }

                                    PTP_ORDER++;
                                }
                                else if (Convert.ToInt32(aArgs[par_OrId]) >= 1000)
                                {
                                    //Áttárolás
                                    //Pvrp API: Ha OrId nagyobb mint 1000 akkor a NodeType a céldepó azonosítóját tartalmazza.
                                    //OrId	telephely vagy megrendelés azonosító. Ha áttárolásos megrendelésről van szó, amit a motor automatikusan 
                                    //létrehozott, akkor az eredeti megrendelés azonosítójához hozzáad 1000-t, így az eredeti megrendelés is 
                                    //beazonosítható (feltételezzük, hogy a normál megrendelésszám kisebb, mint 1000).
                                    throw new NotImplementedException("Áttárolás feldolgozása nincs megoldva");
                                }

                            }
                            
                        }
                        else if (aFn[0] == getIgnoredOrder)
                        {
                            //ElseIf left(sLine, Len(GETIGNOREDORDER)) = GETIGNOREDORDER And left(sLine, Len(GETIGNOREDORDERSCOUNT)) <> GETIGNOREDORDERSCOUNT Then
                            //m_oOptimize.AddIgnoredOrder sLine
                            //End If
                            string[] aArgs = aFn[1].Replace(")", "").Split(',');
                            int ORD_ID = boOpt.dicOrder.Where(i => i.Value.innerID == Convert.ToInt32(aArgs[1])).First().Value.ID;
                            double dQty = Math.Abs(Convert.ToDouble(aArgs[2].Replace(',', '.'), CultureInfo.InvariantCulture) / Global.csQTY_DEC);        //a problémafájl létrehozásakor felszoroztuk, most
                            //visszaosztjuk, hogy a valós értékeket kapjuk meg.


                            //Opt.dicTruckType.Where( i=>i.Value.RZN_ID_LIST==Util.getFieldValue<string>(r, "RZN_ID_LIST")).ToList().First(),
                            if (p_notify != null)
                                p_notify.SetInfoText(PMapMessages.M_OPT_RES_UNPLANNED);

                            ple.CreateUnplannedPlanOrder(boOpt.PLN_ID, ORD_ID, dQty, ple.getNEWTODNUM(boOpt.PLN_ID, ORD_ID));
                        }

                        Console.WriteLine(line);
                    }
                    file.Close();
                    if (currTrk != null)
                    {
                        m_bllPlanEdit.RecalcTour(0, currTrk.TPL_ID, Global.defWeather);
                    }

                    unLockNoReplanned();
                }

                catch (FileNotFoundException fe)
                {
                    DBA.Rollback();
                    Util.ExceptionLog(fe);
                    UI.Error(fe.Message);

                }

                catch (Exception e)
                {
                    DBA.Rollback();
                    throw e;
                }
            }
        }
    }
}