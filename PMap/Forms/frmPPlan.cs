﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using GMap.NET;
using PMap;
using PMap.DB;
using System.IO;
using System.Xml.Serialization;
using PMap.Localize.Base;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using PMap.Properties;
using WeifenLuo.WinFormsUI.Docking;
using PMap.LongProcess.Base;
using PMap.LongProcess;
using PMap.BO;
using PMap.BLL;
using PMap.Localize;
using PMap.Forms.Panels.frmPPlan;
using PMap.Common;
using PMap.Forms.Base;
using PMap.Common.PPlan;
using PMap.Route;

namespace PMap.Forms
{
    public partial class frmPPlan : BaseForm
    {

        private pnlPPlanEditor m_pnlPPlanEditor = null;
        private pnlPPlanSettings m_pnlPPlanSettings = null;
        private pnlPPlanTours m_pnlPPlanTours = null;
        private pnlPPlanTourPoints m_pnlPPlanTourPoints = null;
        private pnlPPlanOrders m_pnlPlanOrders = null;

        private PlanParams m_planParams;

        private bool m_firsActivate = true;

        private bllPlanEdit m_bllPlanEdit;
        private bllPlan m_bllPlan;
        private bllSemaphore m_bllSemaphore;
        
        private int m_USR_ID = 0;
        private bool m_IsEnablePlanManage = false;
        private PlanEditFuncs m_PlanEditFuncs;

        private string m_defaultMPP_TGRID = "";
        private string m_defaultMPP_PGRID = "";
        private string m_defaultMPP_UGRID = "";

        private PPlanCommonVars m_PPlanCommonVars;
        public class FormSerializeHelper
        {
            public FormWindowState WindowState { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }

            public FormSerializeHelper()
            {
            }

            public FormSerializeHelper(Form p_form)
            {
                WindowState = p_form.WindowState;
                Width = p_form.Width;
                Height = p_form.Height;
                Left = p_form.Left;
                Top = p_form.Top;
            }
        }


        public frmPPlan()
        {
            InitializeComponent();
            InitForm();
        }


        public frmPPlan(int p_PLN_ID, int p_USR_ID, PlanParams p_planParams)
        {
            InitializeComponent();
            InitForm();

            m_PPlanCommonVars = new PPlanCommonVars();
            m_USR_ID = p_USR_ID;
            m_planParams = p_planParams;
            m_IsEnablePlanManage = (p_PLN_ID == 0);
            
            initPPlanVariables(p_PLN_ID);

 
            RestoreLayout(false);

            initPPlanForm(p_PLN_ID);
            SetViewMode();

 
        }


        private void initPPlanVariables( int p_PLN_ID)
        {
            try
            {

                //minden inicializáció végén állítjuk be a listenert.
                m_PPlanCommonVars.NotifyDataChanged -= Instance_NotifyDataChanged;

                if (!DesignMode)
                {


                    InitPMap.startErrCode res = InitPMap.Start(true);
                    switch (res)
                    {
                        case InitPMap.startErrCode.FatalErr:
                            string msg = PMapMessages.E_PPLAN_FATALERRINSTART;
                            Util.Log2File(msg);
                            throw new Exception(msg);
                        case InitPMap.startErrCode.NoInternetConn:
                            Util.Log2File(PMapMessages.E_PPLAN_NOINTERNETCONN);
                            PMapCommonVars.Instance.MapAccessMode = AccessMode.CacheOnly;
                            break;
                        case InitPMap.startErrCode.OK:
                            PMapCommonVars.Instance.MapAccessMode = PMapIniParams.Instance.MapCacheMode;
                            break;
                    }

                    m_PPlanCommonVars.Zoom = Global.DefZoom;
                    m_PPlanCommonVars.TooltipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Never;
                    m_PPlanCommonVars.CurrentPosition = new PointLatLng(Global.DefPosLat, Global.DefPosLng);
                    m_PPlanCommonVars.ZoomToSelectedPlan = true;
                    m_PPlanCommonVars.ShowUnPlannedDepots = true;
                    m_PPlanCommonVars.ShowPlannedDepots = true;

                    PMapCommonVars.Instance.ConnectToDB();

                    m_PPlanCommonVars.PLN_ID = p_PLN_ID;
                    PMapCommonVars.Instance.USR_ID = m_USR_ID;


                    m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB.DB);
                    m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB.DB);
                    m_bllSemaphore = new bllSemaphore(PMapCommonVars.Instance.CT_DB.DB);
                    m_bllSemaphore.ClearSemaphores();


                    m_PPlanCommonVars.TourList = m_bllPlan.GetPlanTours(p_PLN_ID);
                    m_PPlanCommonVars.PlanOrderList = m_bllPlan.GetPlanOrders(p_PLN_ID);

                    //Alapértelmezések
                    m_PPlanCommonVars.FocusedTour = m_PPlanCommonVars.TourList.FirstOrDefault();
                    if( m_PPlanCommonVars.FocusedTour != null)
                        m_PPlanCommonVars.FocusedPoint = m_PPlanCommonVars.FocusedTour.TourPoints.FirstOrDefault();

                    if (m_PPlanCommonVars.PlanOrderList != null && m_PPlanCommonVars.PlanOrderList.Count > 0)
                        m_PPlanCommonVars.FocusedOrder = m_PPlanCommonVars.PlanOrderList.FirstOrDefault(x => x.PTP_ID == 0 || m_PPlanCommonVars.ShowAllOrdersInGrid);
                    else
                        m_PPlanCommonVars.FocusedOrder = null;
                

                    m_bllPlanEdit.SetTourColors(m_PPlanCommonVars.TourList);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        private void initPPlanForm(int p_PLN_ID)
        {

            if (m_pnlPPlanTours != null)
                m_pnlPPlanTours.SetFocusedTourBySelectedItem();

            cmbPlans.ComboBox.DisplayMember = "FullPlanName";
            cmbPlans.ComboBox.ValueMember = "ID";

            btnNewPlan.Visible = m_IsEnablePlanManage;
            cmbPlans.Visible = m_IsEnablePlanManage;
            btnDelPlan.Visible = m_IsEnablePlanManage;
            tbSepPlan.Visible = m_IsEnablePlanManage;

            if (m_IsEnablePlanManage)
            {
                cmbPlans.SelectedIndexChanged -= new EventHandler(cmbPlans_SelectedIndexChanged);
                cmbPlans.ComboBox.DataSource = m_bllPlan.GetAllPlans();
                if (p_PLN_ID > 0)
                    cmbPlans.ComboBox.SelectedValue = p_PLN_ID;
                cmbPlans.SelectedIndexChanged += new EventHandler(cmbPlans_SelectedIndexChanged);
            }

            btnDelPlan.Enabled = (p_PLN_ID > 0);
            btnFindORD_NUM.Enabled = (p_PLN_ID > 0);
            btnToEditMode.Enabled = (p_PLN_ID > 0);
            btnToViewMode.Enabled = (p_PLN_ID > 0);
            btnDelTourPoint.Enabled = (p_PLN_ID > 0);
            btnTourDetails.Enabled = (p_PLN_ID > 0);
            btnOptimizeAll.Enabled = (p_PLN_ID > 0);
            btnOptimizeTrk.Enabled = (p_PLN_ID > 0);
            btnNewTour.Enabled = (p_PLN_ID > 0);
            btnDelTour.Enabled = (p_PLN_ID > 0);
            btnChgTruck.Enabled = (p_PLN_ID > 0);
            btnTurnTour.Enabled = (p_PLN_ID > 0);

            //minden inicializáció végén állítjuk be a listenert.
            m_PPlanCommonVars.NotifyDataChanged += Instance_NotifyDataChanged;
            if( m_IsEnablePlanManage)
                this.Text = "Megnyitott terv:"+ (p_PLN_ID > 0 ? cmbPlans.ComboBox.Text : "-") + " Adatbázis=" + PMapIniParams.Instance.DBConfigName  ;
            else
                this.Text = "Adatbázis=" + PMapIniParams.Instance.DBConfigName;
            RefreshAll(new PlanEventArgs(ePlanEventMode.Init));
 
        }

        void Instance_NotifyDataChanged(object sender, PlanEventArgs e)
        {
            RefreshAll(e);
        }

       

        private void RefreshAll(PlanEventArgs e)
        {
            if (m_pnlPPlanEditor != null)
                m_pnlPPlanEditor.RefreshPanel(e);
            if (m_pnlPPlanTours != null)
                m_pnlPPlanTours.RefreshPanel(e);
            if (m_pnlPPlanTourPoints != null)
                m_pnlPPlanTourPoints.RefreshPanel(e);
            if (m_pnlPlanOrders != null)
                m_pnlPlanOrders.RefreshPanel(e);
            if (m_pnlPPlanSettings != null)
                m_pnlPPlanSettings.RefreshPanel(e);

            if (e.EventMode == ePlanEventMode.ChgFocusedTourPoint)
            {
                btnDelTourPoint.Enabled = e.TourPoint != null && btnToViewMode.Visible;
            }

            if (e.EventMode == ePlanEventMode.ChgFocusedTour ||
                e.EventMode == ePlanEventMode.ChgTourSelected)
            {

                btnTourDetails.Enabled = e.Tour != null;
                btnOptimizeTrk.Enabled = e.Tour != null;

                bool bVisible = e.Tour != null && e.Tour.Layer != null && e.Tour.Layer.IsVisibile;
                btnDelTour.Enabled = e.Tour != null && m_PPlanCommonVars.EditMode && bVisible;
                btnChgTruck.Enabled = e.Tour != null && m_PPlanCommonVars.EditMode && bVisible;
                btnTurnTour.Enabled = e.Tour != null && m_PPlanCommonVars.EditMode && bVisible;
                btnDelTourPoint.Enabled = m_pnlPPlanTourPoints.IsFocusedItemExist() && m_PPlanCommonVars.EditMode && bVisible;
            }



            if (e.EventMode == ePlanEventMode.EditorMode)
                SetEditMode();
            
            if (e.EventMode == ePlanEventMode.ViewerMode)
                SetViewMode();


            btnTourDetails.Enabled = m_PPlanCommonVars.FocusedTour != null;
            btnOptimizeTrk.Enabled = m_PPlanCommonVars.FocusedTour != null;
            btnTurnTour.Enabled = m_PPlanCommonVars.FocusedTour != null && m_PPlanCommonVars.EditMode;
            btnDelTour.Enabled = m_PPlanCommonVars.FocusedTour != null && m_PPlanCommonVars.EditMode;
            btnChgTruck.Enabled = m_PPlanCommonVars.FocusedTour != null && m_PPlanCommonVars.EditMode;


        }

        void m_pnlPPlanTourPoints_NotifyPanelChanged(object sender, EventArgs e)
        {
            RefreshAll( (PlanEventArgs)e);
        }

        void m_pnlUnplannedOrders_NotifyPanelChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlPPlanTours_NotifyPanelChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlPPlanEditor_NotifyPanelChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlPPlanSettings_NotifyPanelChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SaveLayout()
        {
            FormSerializeHelper fs = new FormSerializeHelper(this);
            string MPP_WINDOW = XMLSerializator.SerializeObject(fs);
            MemoryStream msDock = new MemoryStream();
            dockPanel.SaveAsXml(msDock, UTF8Encoding.UTF8, true);
            string MPP_DOCK = Encoding.UTF8.GetString(msDock.GetBuffer(), 0, Convert.ToInt32(msDock.Length));
            msDock.Close();

            string MPP_PARAM = XMLSerializator.SerializeObject(m_PPlanCommonVars);
            string MPP_TGRID = Util.SaveGridLayoutToString(m_pnlPPlanTours.gridViewTours);
            string MPP_PGRID = Util.SaveGridLayoutToString(m_pnlPPlanTourPoints.gridViewTourPoints);
            string MPP_UGRID = Util.SaveGridLayoutToString(m_pnlPlanOrders.gridViewPlanOrders);
            bllMapFormPar.SaveParameters(m_PPlanCommonVars.PLN_ID, PMapCommonVars.Instance.USR_ID, MPP_WINDOW, MPP_DOCK, MPP_PARAM, MPP_TGRID, MPP_PGRID, MPP_UGRID);
        }


        private void RestoreLayout(bool p_reset)
        {
            string MPP_WINDOW = "";
            string MPP_DOCK = "";
            string MPP_PARAM = "";
            string MPP_TGRID = m_defaultMPP_TGRID;
            string MPP_PGRID = m_defaultMPP_PGRID;
            string MPP_UGRID = m_defaultMPP_UGRID;


            dockPanel.DockBottomPortion = 0.45;
            dockPanel.DockLeftPortion = 0.15;
            dockPanel.DockRightPortion = 0.35;


            try
            {

                if (p_reset)
                    bllMapFormPar.RemoveParameters(m_PPlanCommonVars.PLN_ID, PMapCommonVars.Instance.USR_ID);

                bllMapFormPar.RestoreParameters(m_PPlanCommonVars.PLN_ID, PMapCommonVars.Instance.USR_ID, out MPP_WINDOW, out MPP_DOCK, out MPP_PARAM, out MPP_TGRID, out MPP_PGRID, out MPP_UGRID);
                
                dockPanel.SuspendLayout(true);

                CreatePanels(false);

                if( MPP_WINDOW != "")
                {
                    FormSerializeHelper fs = (FormSerializeHelper)XMLSerializator.DeserializeObject(MPP_WINDOW, typeof(FormSerializeHelper));

                    this.WindowState = fs.WindowState;
                    this.Width = fs.Width;
                    this.Height = fs.Height;
                    this.Left = fs.Left;
                    this.Top = fs.Top;

                }


                if (MPP_PARAM != "")
                { 

                    PPlanCommonVars p = (PPlanCommonVars)XMLSerializator.DeserializeObject(MPP_PARAM, typeof(PPlanCommonVars));

                    m_PPlanCommonVars.ShowPlannedDepots = p.ShowPlannedDepots;
                    m_PPlanCommonVars.ShowUnPlannedDepots = p.ShowUnPlannedDepots;
                    m_PPlanCommonVars.TooltipMode = p.TooltipMode;
                    m_PPlanCommonVars.ZoomToSelectedPlan = p.ZoomToSelectedPlan;
                    m_PPlanCommonVars.ZoomToSelectedUnPlanned = p.ZoomToSelectedUnPlanned;
                    m_PPlanCommonVars.ShowAllOrdersInGrid = p.ShowAllOrdersInGrid;
               }


                m_pnlPPlanEditor.DockPanel = null;
                m_pnlPPlanSettings.DockPanel = null;
                m_pnlPPlanTours.DockPanel = null;
                m_pnlPPlanTourPoints.DockPanel = null;
                m_pnlPlanOrders.DockPanel = null;

                if (MPP_DOCK != "")
                {

                    byte[] byteArray = Encoding.UTF8.GetBytes(MPP_DOCK);
                    MemoryStream msDock = new MemoryStream(byteArray);
                    DeserializeDockContent deserializeDockContent;
                    deserializeDockContent = new DeserializeDockContent(getContentFromPersistString);
                    dockPanel.LoadFromXml(msDock, deserializeDockContent);
                    msDock.Close();
                }
                else
                {
                    m_pnlPPlanSettings.Show(dockPanel, DockState.DockLeft);
                    m_pnlPPlanTours.Show(dockPanel, DockState.DockBottom);
                    m_pnlPPlanTourPoints.Show(m_pnlPPlanTours.Pane, DockAlignment.Bottom, 0.5);
                    m_pnlPlanOrders.Show(dockPanel, DockState.DockRight);
                    m_pnlPPlanEditor.Show(dockPanel, DockState.Document);

                }

                dockPanel.ResumeLayout(true, true);

                dockPanel.Refresh();

                if( MPP_TGRID != "")
                    Util.RestoreGridLayoutFromString(m_pnlPPlanTours.gridViewTours, MPP_TGRID);
                if (MPP_PGRID != "") 
                    Util.RestoreGridLayoutFromString(m_pnlPPlanTourPoints.gridViewTourPoints, MPP_PGRID);
                if(MPP_UGRID != "")
                    Util.RestoreGridLayoutFromString(m_pnlPlanOrders.gridViewPlanOrders, MPP_UGRID);

                //befrissítjük a túra és túrapont grideket (a layout betöltéssel megváltozott a rendezettség)
  //?              m_pnlPPlanTours.RefreshPanel(new PlanEventArgs(ePlanEventMode.FirstTour));
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                CreatePanels(true);
            }
        }

        private IDockContent getContentFromPersistString(string p_persistString)
        {
            if (m_pnlPPlanEditor.GetType().ToString() == p_persistString)
                return m_pnlPPlanEditor;
            if (m_pnlPPlanSettings.GetType().ToString() == p_persistString)
                return m_pnlPPlanSettings;
            if (m_pnlPPlanTours.GetType().ToString() == p_persistString)
                return m_pnlPPlanTours;
            if (m_pnlPPlanTourPoints.GetType().ToString() == p_persistString)
                return m_pnlPPlanTourPoints;
            if (m_pnlPlanOrders.GetType().ToString() == p_persistString)
                return m_pnlPlanOrders;
            return null;
        }

        private void SetEditMode()
        {
            btnToEditMode.Visible = false;
            btnToViewMode.Visible = true;
            btnDelTourPoint.Enabled = m_PPlanCommonVars.FocusedPoint != null;
            btnTurnTour.Enabled = m_PPlanCommonVars.FocusedTour != null;
            btnDelTour.Enabled = m_PPlanCommonVars.FocusedTour != null;
            btnChgTruck.Enabled = m_PPlanCommonVars.FocusedTour != null;
        }

        private void SetViewMode()
        {
            btnToViewMode.Visible = false;
            btnToEditMode.Visible = true;
            btnTurnTour.Enabled = false;
            btnDelTour.Enabled = false;
            btnChgTruck.Enabled = false;
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            SaveLayout();
        }

        private void btnRestoreLayout_Click(object sender, EventArgs e)
        {
            RestoreLayout(false);

        }

        private void frmPPlan_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveLayout();
        }

        private void btnEditMode_Click(object sender, EventArgs e)
        {
            m_PPlanCommonVars.EditMode = true;
        }

        private void btnViewMode_Click(object sender, EventArgs e)
        {
            m_PPlanCommonVars.EditMode = false;
        }

        private void btnResetScreen_Click(object sender, EventArgs e)
        {
            RestoreLayout(true);
            m_PPlanCommonVars.EditMode = false;

        }

        private void CreatePanels(bool bForce)
        {
            if (bForce || m_pnlPPlanSettings == null || m_pnlPPlanSettings.IsDisposed)
            {
                if (m_pnlPPlanSettings != null && !m_pnlPPlanSettings.IsDisposed)
                    m_pnlPPlanSettings.Dispose();

                m_pnlPPlanSettings = new pnlPPlanSettings(m_PPlanCommonVars);
                m_pnlPPlanSettings.NotifyPanelChanged += new EventHandler<EventArgs>(m_pnlPPlanSettings_NotifyPanelChanged);
            }

            if (bForce || m_pnlPlanOrders == null || m_pnlPlanOrders.IsDisposed)
            {
                if (m_pnlPlanOrders != null && !m_pnlPlanOrders.IsDisposed)
                    m_pnlPlanOrders.Dispose();

                m_pnlPlanOrders = new pnlPPlanOrders(m_PPlanCommonVars);
                m_pnlPlanOrders.NotifyPanelChanged += new EventHandler<EventArgs>(m_pnlUnplannedOrders_NotifyPanelChanged);

                //letároljuk a default beállítást
                m_defaultMPP_UGRID = Util.SaveGridLayoutToString( m_pnlPlanOrders.gridViewPlanOrders);

            }

            if (bForce || m_pnlPPlanTours == null || m_pnlPPlanTours.IsDisposed)
            {
                if (m_pnlPPlanTours != null && !m_pnlPPlanTours.IsDisposed)
                    m_pnlPPlanTours.Dispose();

                m_pnlPPlanTours = new pnlPPlanTours(m_PPlanCommonVars);
                m_pnlPPlanTours.NotifyPanelChanged += new EventHandler<EventArgs>(m_pnlPPlanTours_NotifyPanelChanged);


                //letároljuk a default beállítást
                m_defaultMPP_TGRID = Util.SaveGridLayoutToString(m_pnlPPlanTours.gridViewTours);
            
            }

            if (bForce || m_pnlPPlanTourPoints == null || m_pnlPPlanTourPoints.IsDisposed)
            {
                if (m_pnlPPlanTourPoints != null && !m_pnlPPlanTourPoints.IsDisposed)
                    m_pnlPPlanTourPoints.Dispose();

                m_pnlPPlanTourPoints = new pnlPPlanTourPoints(m_PPlanCommonVars);
                m_pnlPPlanTourPoints.NotifyPanelChanged += new EventHandler<EventArgs>(m_pnlPPlanTourPoints_NotifyPanelChanged);

                //letároljuk a default beállítást
                m_defaultMPP_PGRID = Util.SaveGridLayoutToString(m_pnlPPlanTourPoints.gridViewTourPoints);

            }

            if (bForce || m_pnlPPlanEditor == null || m_pnlPPlanEditor.IsDisposed)
            {
                if (m_pnlPPlanEditor != null && !m_pnlPPlanEditor.IsDisposed)
                    m_pnlPPlanEditor.Dispose();

                m_pnlPPlanEditor = new pnlPPlanEditor(m_PPlanCommonVars);
                m_pnlPPlanEditor.NotifyPanelChanged += new EventHandler<EventArgs>(m_pnlPPlanEditor_NotifyPanelChanged);
            }

            m_PlanEditFuncs = new PlanEditFuncs(m_pnlPPlanEditor, m_PPlanCommonVars);
            
        }

        private void btnTourDetails_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null)
            {

                BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, m_PPlanCommonVars.FocusedTour.TourPoints.Count() - 1, "Túrarészletező", true);
                GetTourDetailsProcess cdp = new GetTourDetailsProcess(pd, m_PPlanCommonVars.FocusedTour);
                cdp.Run();
                pd.ShowDialog();
                if (cdp.Completed)
                {

                    dlgTourDetails td = new dlgTourDetails(m_PPlanCommonVars.FocusedTour, cdp.TourDetails);
                    td.ShowDialog(this);
                }
            }
        }

        private void btnDelTourPoint_Click(object sender, EventArgs e)
        {
            m_PlanEditFuncs.RemoveTourPoint(m_PPlanCommonVars.FocusedPoint);
        }

        private void btnTurnTour_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null)
                m_PlanEditFuncs.TurnTour(m_PPlanCommonVars.FocusedTour.ID);
        }

        private void btnOptimizeTrk_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null)
            {
                if (m_PPlanCommonVars.FocusedTour.LOCKED)
                {
                    UI.Message( PMapMessages.E_PEDIT_LOCKEDTRUCK);
                    return;
                }
                dlgOptimize dOpt = new dlgOptimize(m_PPlanCommonVars.PLN_ID, m_PPlanCommonVars.FocusedTour.ID);
                if (dOpt.ShowDialog() == System.Windows.Forms.DialogResult.OK && dOpt.Result == TourOptimizerProcess.eOptResult.OK)
                {
                    m_PlanEditFuncs.RefreshToursAfterModify(m_PPlanCommonVars.FocusedTour.ID, 0);
                    m_PPlanCommonVars.PlanOrderList = m_bllPlan.GetPlanOrders(m_PPlanCommonVars.PLN_ID);  //
                }
                else
                {
                    UI.Message(PMapMessages.E_PEDIT_OPTISNFINISHED);
                }
            }
        }

        private void btnNewTour_Click(object sender, EventArgs e)
        {
            dlgNewTour dlgNewTour = new dlgNewTour(m_PPlanCommonVars.PLN_ID, m_PlanEditFuncs, m_PPlanCommonVars);
            dlgNewTour.ShowDialog(this);
        }

        private void btnDelTour_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null )
            {
                m_PlanEditFuncs.DelTour(m_PPlanCommonVars.FocusedTour);
            }
        }

        private void btnChgTruck_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null)
            {
                dlgTruckChg dlgTrkChg = new dlgTruckChg(m_PPlanCommonVars.PLN_ID, m_PPlanCommonVars.FocusedTour, m_PlanEditFuncs, m_PPlanCommonVars);
                dlgTrkChg.ShowDialog(this);
            }
        }


        /*****************/
        #region F3/F4 kezelés

        private KeyMessageFilter m_filter = null;

        private void frmPPlan_Load(object sender, EventArgs e)
        {
            m_filter = new KeyMessageFilter(this);
            Application.AddMessageFilter(m_filter);
        }

        public class KeyMessageFilter : IMessageFilter
        {
            private Dictionary<Keys, bool> m_keyTable = new Dictionary<Keys, bool>();
            private frmPPlan m_frmPPlan = null;

            public KeyMessageFilter(frmPPlan p_frmPPlan)
            {
                m_frmPPlan = p_frmPPlan;
            }


            public Dictionary<Keys, bool> KeyTable
            {
                get { return m_keyTable; }
                private set { m_keyTable = value; }
            }

            public bool IsKeyPressed()
            {
                return m_keyPressed;
            }

            public bool IsKeyPressed(Keys k)
            {
                bool pressed = false;

                if (KeyTable.TryGetValue(k, out pressed))
                {
                    return pressed;
                }

                return false;
            }

            private const int WM_KEYDOWN = 0x0100;
            private const int WM_KEYUP = 0x0101;
            private const int WM_KEYPRESS = 0x102;


            private bool m_keyPressed = false;


            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_KEYUP)
                {
                    KeyTable[(Keys)m.WParam] = false;

                    m_keyPressed = false;
                }

                if (m.Msg == WM_KEYDOWN)
                {
                    Keys k = (Keys)m.WParam;
                    if (k == Keys.F3)
                    {
                        KeyTable[(Keys)m.WParam] = true;
                        if (m_frmPPlan.m_pnlPPlanTours != null)
                        {
                            PlanEventArgs e = new PlanEventArgs(ePlanEventMode.PrevTour);
                            m_frmPPlan.m_pnlPPlanTours.RefreshPanel(e);
                        }
                    }
                    if (k == Keys.F4)
                    {
                        KeyTable[(Keys)m.WParam] = true;
                        Console.WriteLine("F4");
                        if (m_frmPPlan.m_pnlPPlanTours != null)
                        {
                            PlanEventArgs e = new PlanEventArgs(ePlanEventMode.NextTour);
                            m_frmPPlan.m_pnlPPlanTours.RefreshPanel(e);
                        }
                    }
                    m_keyPressed = true;
                }
                return false;
            }
        }

        private void btnFindORD_NUM_Click(object sender, EventArgs e)
        {
            dlgFindORD_NUM dlg = new dlgFindORD_NUM();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                                if (!m_PlanEditFuncs.SetPositionByOrd_NUM(dlg.txtORD_NUM.Text))
                    UI.Message(PMapMessages.M_PEDIT_ORDNOTFOUND);
            }
        }

        private void frmPPlan_Activated(object sender, EventArgs e)
        {
            //Ha van lebegő ablakunk, az első aktiváláskor újra kell frissíteni a layoutott.
            if (m_firsActivate && (m_pnlPPlanEditor.IsFloat || m_pnlPPlanSettings.IsFloat || m_pnlPPlanTours.IsFloat
                || m_pnlPPlanTourPoints.IsFloat || m_pnlPlanOrders.IsFloat))
            {
                RestoreLayout(false);
            }
            m_firsActivate = false;
        }

        private void btnOptimizeAll_Click(object sender, EventArgs e)
        {
            dlgOptimize dOpt = new dlgOptimize(m_PPlanCommonVars.PLN_ID, 0);
            if (dOpt.ShowDialog() == System.Windows.Forms.DialogResult.OK && dOpt.Result == TourOptimizerProcess.eOptResult.OK)
            {
                this.initPPlanForm(m_PPlanCommonVars.PLN_ID);
            }
            else
            {
                MessageBox.Show(this, PMapMessages.E_PEDIT_OPTISNFINISHED);
            }
        }

        private void btnNewPlan_Click(object sender, EventArgs e)
        {
            dlgNewPlan np = new dlgNewPlan(m_planParams);
            if (np.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                initPPlanVariables(np.PLN_ID);
                initPPlanForm(np.PLN_ID);
            }

        }

        private void cmbPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PLN_ID = (int)cmbPlans.ComboBox.SelectedValue;
            initPPlanVariables(PLN_ID);
            initPPlanForm(PLN_ID);
        }

        private void btnDelPlan_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.PLN_ID > 0 && UI.Confirm(PMapMessages.Q_PEDIT_DELPLAN))
            {
                if (m_bllSemaphore.SetPlanSemaphore(m_PPlanCommonVars.PLN_ID) == bllSemaphore.SEMValues.SMV_FREE)
                {

                    m_bllPlanEdit.DeletePlan(m_PPlanCommonVars.PLN_ID);
                    initPPlanVariables(0);
                    initPPlanForm(0);
                }
                else
                {
                    UI.Warning(PMapMessages.E_PEDIT_OPTANOTHERWS);
                }
            }
        }

        private void btnCalcDistances_Click(object sender, EventArgs e)
        {
            if (UI.Confirm(PMapMessages.Q_PEDIT_CALCDST))
            {
                bllRoute bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB.DB);
                List<boRoute> res = bllRoute.GetDistancelessPlanNodes(m_PPlanCommonVars.PLN_ID);
                if (res.Count == 0)
                    return;

                bool bOK = false;

                if (PMapIniParams.Instance.RouteThreadNum > 1)
                    bOK = PMRouteInterface.GetPMapRoutesMulti(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, true);
                else
                    bOK = PMRouteInterface.GetPMapRoutesSingle(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, true);

                if (bOK)
                {
//                    UI.Message(PMapMessages.M_PEDIT_CALCDST_END);
                }

            }

        }

        private void cmbPlans_Click(object sender, EventArgs e)
        {

        }


   
    }

    /*****************/
        #endregion
}
