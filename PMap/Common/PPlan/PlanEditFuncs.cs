﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL;
using PMap.BO;
using GMap.NET.WindowsForms;
using PMap.Forms.Base;
using PMap.Localize;
using System.Drawing;
using PMap.DB.Base;
using System.Windows.Forms;

namespace PMap.Common.PPlan
{
    public class PlanEditFuncs
    {
        private bllPlan m_bllPlan;
        private bllPlanEdit m_bllPlanEdit;
        private BasePanel m_panel;
        public PlanEditFuncs(BasePanel p_panel)
        {
            m_panel = p_panel;
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB.DB);
            m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB.DB);
        }

        public boPlanTour RemoveTourPoint(boPlanTourPoint p_TourPoint)
        {
            boPlanTour refreshedTour = null;
            if (p_TourPoint != null && UI.Confirm(PMapMessages.Q_PEDIT_DELDEP, p_TourPoint.CLT_NAME, p_TourPoint.Tour.TRUCK))
            {
                if (m_bllPlanEdit.RemoveOrderFromTour(p_TourPoint, Global.defWeather, true))
                {
                    refreshedTour = RefreshToursAfterModify(p_TourPoint.Tour.ID, 0);
                }
            }
            return refreshedTour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_ReorganizedTourPoint">"Átszervezendő túrapont</param>
        /// <param name="p_Tour">Cél túra</param>
        /// <param name="p_InsertionPoint">beszúrás túrapont</param>
        public boPlanTour ReorganizeTour(boPlanTourPoint p_ReorganizedTourPoint, boPlanTour p_Tour, boPlanTourPoint p_InsertionPoint)
        {

            boPlanTour refreshedTour = null;
            if (p_InsertionPoint.PTP_TYPE == Global.PTP_TYPE_WHS_E)
            {
                UI.Error(PMapMessages.E_PEDIT_WRONGINSPOINT);
                return refreshedTour;
            }

            if (UI.Confirm(PMapMessages.Q_PEDIT_REORDER, p_ReorganizedTourPoint.Tour.TRUCK, p_ReorganizedTourPoint.CLT_NAME))
            {
                bllPlanCheck.checkOrderResult res = bllPlanCheck.checkOrderResult.OK;
                //Egyelőre nem kell kézi tervezésnél a járműellenőrzés
                //res = bllPlanCheck.CheckAll(m_EditedTourPoint.TOD_ID, m_EditedRoute.Tour.ID);
                res = bllPlanCheck.checkOrderResult.OK;

                //MEGJ:a m_EditedTourPoint.Tour t
                if (!checkInsertionPoint(p_ReorganizedTourPoint.Tour.RZN_ID_LIST, p_InsertionPoint.NOD_ID, p_ReorganizedTourPoint.NOD_ID, p_InsertionPoint.NextTourPoint.NOD_ID))
                {
                    //Megj.:Egyelőre nem kezeljük azt a helyzetet, amikor nincs távolaág számítva
                    //(ez esetben pl. egy távolságszámítást indíthatnánk a két pontra)
                    UI.Message(PMapMessages.E_PEDIT_NOTFITTEDDEP);
                    //                    resetEditMode();
                    return refreshedTour;
                }


                //Létező túrapont átszervezése
                //
                if (res == bllPlanCheck.checkOrderResult.OK && m_bllPlanEdit.RemoveOrderFromTour(p_ReorganizedTourPoint, Global.defWeather, true))
                {
                    boPlanOrder upo = m_bllPlan.GetPlanOrder(p_ReorganizedTourPoint.TOD_ID);

                    int PTP_ID = m_bllPlanEdit.AddOrderToTour(p_Tour.ID, upo, p_InsertionPoint, Global.defWeather);

                    //Túrák befrissítése

                    if (p_ReorganizedTourPoint.Tour.ID != p_Tour.ID)
                        refreshedTour = RefreshToursAfterModify(p_ReorganizedTourPoint.Tour.ID, p_Tour.ID);
                    else
                        refreshedTour = RefreshToursAfterModify(p_ReorganizedTourPoint.Tour.ID, 0);

                    //Rápozícionálunk a túrára
                    PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, p_Tour);
                    m_panel.DoNotifyDataChanged(eve);

                    //rápozícionálunk az új túrapontra
                    //
                    eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, PPlanCommonVars.Instance.GetTourPointByID(PTP_ID));
                    m_panel.DoNotifyDataChanged(eve);
                }
            }
            return refreshedTour;
        }

        public boPlanTour TurnTour(int p_TPL_ID)
        {
            boPlanTour refreshedTour = null;
            if (UI.Confirm(PMapMessages.Q_PEDIT_REVERSE))
            {
                m_bllPlanEdit.TurnTour(p_TPL_ID, Global.defWeather);
                refreshedTour = RefreshToursAfterModify(p_TPL_ID, 0);
            }
            return refreshedTour;
        }


        public boPlanTour AddOrderToTour(boPlanTour p_Tour, boPlanTourPoint p_InsertionPoint, boPlanOrder p_planOrder)
        {
            boPlanTour refreshedTour = null;
            if (UI.Confirm(PMapMessages.Q_PEDIT_DEPINTOTOUR, p_Tour.TRUCK, p_planOrder.DEP_NAME))
            {

                if (p_InsertionPoint.PTP_TYPE == Global.PTP_TYPE_WHS_E)
                {
                    UI.Error(PMapMessages.E_PEDIT_WRONGINSPOINT);
                    return refreshedTour;
                }

                bllPlanCheck.checkOrderResult res = bllPlanCheck.checkOrderResult.OK;

                //Egyelőre nem kell kézi tervezésnél a járműellenőrzés
                //res = bllPlanCheck.CheckAll(m_EditedUnplannedOrder.ID, m_EditedRoute.Tour.ID);


                //Új megrendelés felvétele a túrába
                //
                if (res == bllPlanCheck.checkOrderResult.OK)
                {

                    if (!checkInsertionPoint(p_Tour.RZN_ID_LIST, p_InsertionPoint.NOD_ID, p_planOrder.NOD_ID, p_InsertionPoint.NextTourPoint.NOD_ID))
                    {
                        //Megj.:Egyelőre nem kezeljük azt a helyzetet, amikor nincs távolaág számítva
                        //(ez esetben pl. egy távolságszámítást indíthatnánk a két pontra)
                        UI.Message(PMapMessages.E_PEDIT_NOINSPOINT);
                        //resetEditMode();
                        return refreshedTour;

                    }
                    int PTP_ID = m_bllPlanEdit.AddOrderToTour(p_Tour.ID, p_planOrder, p_InsertionPoint, Global.defWeather);
                    if (PTP_ID > 0)
                    {
                        refreshedTour = RefreshToursAfterModify(p_Tour.ID, 0);

                        //Rápozícionálunk a túrára
                        PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, refreshedTour);
                        m_panel.DoNotifyDataChanged(eve);

                        //rápozícionálunk az új túrapontra
                        //
                        eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, PPlanCommonVars.Instance.GetTourPointByID(PTP_ID));
                        m_panel.DoNotifyDataChanged(eve);
                    }
                }
            }
            return refreshedTour;
        }


        public boPlanTour CreateNewTour(int p_PLN_ID, int p_WHS_ID, int p_TPL_ID, Color p_color, DateTime p_WhsS, DateTime p_WhsE, int p_srvTime)
        {
            boPlanTour refreshedTour = null;
            if (UI.Confirm(PMapMessages.Q_PEDIT_NEWTOUR))
            {

                m_bllPlanEdit.CreateNewTour(p_PLN_ID, p_WHS_ID, p_TPL_ID, p_color, p_WhsS, p_WhsE, p_srvTime);
                refreshedTour = RefreshToursAfterModify(p_TPL_ID, 0);

                PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, refreshedTour);
                m_panel.DoNotifyDataChanged(eve);
            }
            return refreshedTour;
        }

        public void DelTour(boPlanTour p_delTour)
        {
            if (UI.Confirm(PMapMessages.Q_PEDIT_DELTOUR, p_delTour.TRUCK))
            {
                m_bllPlanEdit.DeleteTour(p_delTour.ID);
                RefreshToursAfterModify(p_delTour.ID, 0);
            }
        }

        public boPlanTour ChangeTruck(int p_PLN_ID, int p_OldTPL_ID, int p_NewTPL_ID, Color p_color)
        {

            boPlanTour refreshedTour = null;
            using (TransactionBlock transObj = new TransactionBlock(PMapCommonVars.Instance.CT_DB.DB))
            {
                try
                {
                    m_bllPlanEdit.ChangeTruck(p_PLN_ID, p_OldTPL_ID, p_NewTPL_ID, Global.defWeather);
                    m_bllPlanEdit.UpdateTourColor(p_NewTPL_ID, p_color);
                    m_bllPlanEdit.UpdateTourSelect(p_NewTPL_ID, true);
                }
                catch (Exception exc)
                {
                    PMapCommonVars.Instance.CT_DB.DB.Rollback();
                    Util.ExceptionLog(exc);
                    throw new Exception(exc.Message);
                }

            }
            refreshedTour = RefreshToursAfterModify(p_OldTPL_ID, p_NewTPL_ID);

            PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, refreshedTour);
            m_panel.DoNotifyDataChanged(eve);

            return refreshedTour;
        }

        public void RefreshOrdersFromDB()
        {
            PPlanCommonVars.Instance.PlanOrderList = m_bllPlan.GetPlanOrders(PPlanCommonVars.Instance.PLN_ID);
            m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.RefreshOrders));
            PPlanCommonVars.Instance.FocusedUnplannedOrder = null;
        }

        public boPlanTour RefreshToursAfterModify(int pChangedTourID1, int pChangedTourID2)
        {
            Cursor oldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            RefreshOrdersFromDB();

            boPlanTour TourWithFreshData = null;
            PPlanCommonVars.Instance.FocusedPoint = null;

            if (pChangedTourID1 > 0)
            {
                TourWithFreshData = RefreshTourDataFromDB(pChangedTourID1);
            }
            if (pChangedTourID2 > 0)
            {
                TourWithFreshData = RefreshTourDataFromDB(pChangedTourID2);
            }
            Cursor.Current = oldCursor;
            return TourWithFreshData;
        }

        public bool SetPositionByOrd_NUM(string sFindORD_NUM)
        {
            string sFndText = sFindORD_NUM.ToUpper();


            boPlanOrder ord = PPlanCommonVars.Instance.GetOrderByORD_NUM(sFndText);
            if (ord != null)
            {

                PlanEventArgs evt = new PlanEventArgs(ePlanEventMode.ChgFocusedOrder, ord);
                m_panel.DoNotifyDataChanged(evt);

                if (ord.PTP_ID > 0)
                {
                    boPlanTourPoint ptp = PPlanCommonVars.Instance.GetTourPointByID(ord.PTP_ID);
                    evt = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, ptp.Tour);
                    m_panel.DoNotifyDataChanged(evt);

                    evt = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, ptp);
                    m_panel.DoNotifyDataChanged(evt);
                }
                return true;

            }
            return false;
        }

   
        private bool checkInsertionPoint(string p_RZN_ID_LIST, int p_NOD_ID_START, int p_NOD_ID_INS, int p_NOD_ID_END)
        {


            //Ellenőrizni, találunk-e útvonalat a beszúrási pont és a beszúrt lerakó között
            bllPlanCheck.checkDistanceResult dresultStart =
                    bllPlanCheck.CheckDistance(p_RZN_ID_LIST, p_NOD_ID_START, p_NOD_ID_INS);
            //Ellenőrizzük, van-e útvonal a lerakó és a beszúrás érkezési pontja között
            bllPlanCheck.checkDistanceResult dresultEnd =
                    bllPlanCheck.CheckDistance(p_RZN_ID_LIST, p_NOD_ID_INS, p_NOD_ID_END);

            return (dresultStart == bllPlanCheck.checkDistanceResult.OK &&
                    dresultEnd == bllPlanCheck.checkDistanceResult.OK);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_TourWithFreshData"></param>
        private boPlanTour RefreshTourDataFromDB(int p_TPL_ID)
        {

            boPlanTour TourWithOldData = PPlanCommonVars.Instance.GetTourByID(p_TPL_ID);
            boPlanTour TourWithFreshData = m_bllPlan.GetPlanTour(p_TPL_ID);
            if (TourWithOldData != null)
            {

                //kitöröljük a régi adatokkal rendelkező túrát és beszúrjuk a firssítést
                int index = PPlanCommonVars.Instance.TourList.IndexOf(TourWithOldData);
                PPlanCommonVars.Instance.TourList.Remove(TourWithOldData);
                m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.RemoveTour, TourWithOldData));

                if (TourWithFreshData != null)
                {
                    PPlanCommonVars.Instance.TourList.Insert(index, TourWithFreshData);
                    m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.AddTour, TourWithFreshData));
                }
            }
            else
            {
                //Nem volt még a listában, hozzáadás
                if (TourWithFreshData != null)
                {
                    PPlanCommonVars.Instance.TourList.Add(TourWithFreshData);
                    m_panel.DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.AddTour, TourWithFreshData));
                }
            }
            return TourWithFreshData;
        }
    }
}

