using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using PMapCore.LongProcess.Base;
using PMapCore.LongProcess;
using PMapCore.BO;
using PMapCore.BLL;
using PMapCore.Strings;
using PMapUI.Forms.Panels.frmPPlan;
using PMapCore.Common;
using PMapUI.Forms.Base;
using PMapUI.Forms.Funcs;
using PMapCore.Route;
using PMapCore.WebTrace;
using PMapCore.Common.Azure;
using PMapCore.DB.Base;
using System.Runtime.ExceptionServices;
using PMapUI.Printing;
using PMapCore.BLL.Report;
using PMapCore.BO.Report;
using PMapCore.Common.PPlan;
using PMapUI.Common;

namespace PMapUI.Forms
{
    public partial class frmPPlan : BaseForm
    {

        private pnlPPlanEditor m_pnlPPlanEditor = null;
        private pnlPPlanSettings m_pnlPPlanSettings = null;
        private pnlPPlanTours m_pnlPPlanTours = null;
        private pnlPPlanTourPoints m_pnlPPlanTourPoints = null;
        private pnlPPlanOrders m_pnlPlanOrders = null;

        private PlanParams m_planParams;

        //     private boPlanTour m_selTour = null;
        private bool m_firsActivate = true;

        private bllPlanEdit m_bllPlanEdit;
        private bllPlan m_bllPlan;
        private bllSemaphore m_bllSemaphore;
        private bllUser m_bllUser;
        private bllRoute m_bllRoute;

        private int m_USR_ID = 0;
        private bool m_IsEnablePlanManage = false;
        private PlanEditFuncs m_PlanEditFuncs;

        private string m_defaultMPP_TGRID = "";
        private string m_defaultMPP_PGRID = "";
        private string m_defaultMPP_UGRID = "";
        private PPlanCommonVars m_PPlanCommonVars;

  
        public frmPPlan()
        {
            InitializeComponent();
            InitForm();
        }


        public frmPPlan(int p_PLN_ID, int p_USR_ID, PlanParams p_planParams)
        {
            InitializeComponent();
            InitForm();

            startForm();

            m_USR_ID = p_USR_ID;
            m_planParams = p_planParams;
            m_IsEnablePlanManage = (p_PLN_ID == 0);

            m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
            m_bllSemaphore = new bllSemaphore(PMapCommonVars.Instance.CT_DB);
            m_bllSemaphore.ClearSemaphores();
            m_bllUser = new bllUser(PMapCommonVars.Instance.CT_DB);

            initPPlanForm(p_PLN_ID, false);


            RestoreLayout(false);
            SetViewMode(false);

        }


        private void startForm()
        {
            try
            {
                m_PPlanCommonVars = new PPlanCommonVars();

                if (!DesignMode)
                {



                    m_PPlanCommonVars.Zoom = Global.DefZoom;
                    m_PPlanCommonVars.TooltipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Never;
                    m_PPlanCommonVars.CurrentPosition = new PointLatLng(Global.DefPosLat, Global.DefPosLng);
                    m_PPlanCommonVars.ZoomToSelectedPlan = true;
                    m_PPlanCommonVars.ShowUnPlannedDepots = true;
                    m_PPlanCommonVars.ShowPlannedDepots = true;

                    PMapCommonVars.Instance.ConnectToDB();


                }
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }

        }



        private void initPPlanForm(int p_PLN_ID, bool p_Reinit)
        {
            if (m_pnlPPlanTours != null)
                m_pnlPPlanTours.OnNotifyDataChanged();

            if (!p_Reinit)
            {
                cmbPlans.ComboBox.DisplayMember = "FullPlanName";
                cmbPlans.ComboBox.ValueMember = "ID";
            }

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

            btnToCloud.Visible = AzureTableStore.Instance.AzureAccount == PMapIniParams.Instance.AzureAccount;

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
            btnToCloud.Enabled = (p_PLN_ID > 0);

            btnCompleteTourRoutes.Visible = PMapIniParams.Instance.TourRoute;


            m_PPlanCommonVars.PLN_ID = p_PLN_ID;
            PMapCommonVars.Instance.USR_ID = m_USR_ID;

            m_PPlanCommonVars.TourList = m_bllPlan.GetPlanTours(m_PPlanCommonVars.PLN_ID);
            m_PPlanCommonVars.PlanOrderList = m_bllPlan.GetPlanOrders(m_PPlanCommonVars.PLN_ID);

            m_bllPlanEdit.SetTourColors(m_PPlanCommonVars.TourList);

            if (p_Reinit)
            {
                PlanEventArgs eve = new PlanEventArgs(ePlanEventMode.ReInit);
                RefreshAll(eve);
            }
            this.Text += "<< DB=" + PMapIniParams.Instance.DBConfigName + ">>";
        }

        private void RefreshAll(PlanEventArgs e)
        {
            m_pnlPPlanEditor.RefreshPanel(e);
            m_pnlPPlanTours.RefreshPanel(e);
            m_pnlPPlanTourPoints.RefreshPanel(e);
            m_pnlPlanOrders.RefreshPanel(e);
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

                bool bVisible = e.Tour != null && e.Tour.Layer.IsVisibile;
                btnDelTour.Enabled = e.Tour != null && m_pnlPPlanEditor.EditMode && bVisible;
                btnChgTruck.Enabled = e.Tour != null && m_pnlPPlanEditor.EditMode && bVisible;
                btnTurnTour.Enabled = e.Tour != null && m_pnlPPlanEditor.EditMode && bVisible;
                btnDelTourPoint.Enabled = m_pnlPPlanTourPoints.IsFocusedItemExist() && m_pnlPPlanEditor.EditMode && bVisible;

            }



            if (e.EventMode == ePlanEventMode.EditorMode)
                SetEditMode(false);

            if (e.EventMode == ePlanEventMode.ViewerMode)
                SetViewMode(false);

        }

        void m_pnlPPlanTourPoints_NotifyDataChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlUnplannedOrders_NotifyDataChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlPPlanTours_NotifyDataChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlPPlanEditor_NotifyDataChanged(object sender, EventArgs e)
        {
            RefreshAll((PlanEventArgs)e);
        }

        void m_pnlPPlanSettings_NotifyDataChanged(object sender, EventArgs e)
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
            string MPP_TGRID = UI.SaveGridLayoutToString(m_pnlPPlanTours.gridViewTours);
            string MPP_PGRID = UI.SaveGridLayoutToString(m_pnlPPlanTourPoints.gridViewTourPoints);
            string MPP_UGRID = UI.SaveGridLayoutToString(m_pnlPlanOrders.gridViewPlanOrders);
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

                if (MPP_WINDOW != "")
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
                    m_PPlanCommonVars.ShowAllOrdersInGrid = p.ShowAllOrdersInGrid;
                    m_PPlanCommonVars.ZoomToSelectedUnPlanned = p.ZoomToSelectedUnPlanned;

                    m_pnlPPlanSettings.Init();
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

                if (MPP_TGRID != "")
                    UI.RestoreGridLayoutFromString(m_pnlPPlanTours.gridViewTours, MPP_TGRID);
                if (MPP_PGRID != "")
                    UI.RestoreGridLayoutFromString(m_pnlPPlanTourPoints.gridViewTourPoints, MPP_PGRID);
                if (MPP_UGRID != "")
                    UI.RestoreGridLayoutFromString(m_pnlPlanOrders.gridViewPlanOrders, MPP_UGRID);

                //befrissítjük a túra és túrapont grideket (a layout betöltéssel megváltozott a rendezettség)
                m_pnlPPlanTours.RefreshPanel(new PlanEventArgs(ePlanEventMode.FirstTour));
                btnToCloud.Visible = (AzureTableStore.Instance.AzureAccount == PMapIniParams.Instance.AzureAccount);
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

        private void SetEditMode(bool p_refresh)
        {
            if (p_refresh)
            {
                PlanEventArgs ev = new PlanEventArgs(ePlanEventMode.EditorMode);
                RefreshAll(ev);
            }
            btnToEditMode.Visible = false;
            btnToViewMode.Visible = true;
            btnDelTourPoint.Enabled = m_pnlPPlanTourPoints.IsFocusedItemExist();
            btnTurnTour.Enabled = m_PPlanCommonVars.FocusedTour != null;
            btnDelTour.Enabled = m_PPlanCommonVars.FocusedTour != null;
            btnChgTruck.Enabled = m_PPlanCommonVars.FocusedTour != null;

            btnOpenClose.Enabled = m_pnlPlanOrders.GetID() > 0;



        }

        private void SetViewMode(bool p_refresh)
        {
            if (p_refresh)
            {
                PlanEventArgs ev = new PlanEventArgs(ePlanEventMode.ViewerMode);
                RefreshAll(ev);
            }
            btnToViewMode.Visible = false;
            btnToEditMode.Visible = true;
            btnTurnTour.Enabled = false;
            btnDelTour.Enabled = false;
            btnChgTruck.Enabled = false;
            btnOpenClose.Enabled = false;
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
            SetEditMode(true);
        }

        private void btnViewMode_Click(object sender, EventArgs e)
        {
            SetViewMode(true);
        }

        private void btnResetScreen_Click(object sender, EventArgs e)
        {
            RestoreLayout(true);
            SetViewMode(false);

        }

        private void CreatePanels(bool bForce)
        {
            if (bForce || m_pnlPPlanSettings == null || m_pnlPPlanSettings.IsDisposed)
            {
                if (m_pnlPPlanSettings != null && !m_pnlPPlanSettings.IsDisposed)
                    m_pnlPPlanSettings.Dispose();

                m_pnlPPlanSettings = new pnlPPlanSettings(m_PPlanCommonVars);
                m_pnlPPlanSettings.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlPPlanSettings_NotifyDataChanged);
            }

            if (bForce || m_pnlPlanOrders == null || m_pnlPlanOrders.IsDisposed)
            {
                if (m_pnlPlanOrders != null && !m_pnlPlanOrders.IsDisposed)
                    m_pnlPlanOrders.Dispose();

                m_pnlPlanOrders = new pnlPPlanOrders(m_PPlanCommonVars);
                m_pnlPlanOrders.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlUnplannedOrders_NotifyDataChanged);

                //letároljuk a default beállítást
                m_defaultMPP_UGRID = UI.SaveGridLayoutToString(m_pnlPlanOrders.gridViewPlanOrders);

            }

            if (bForce || m_pnlPPlanTours == null || m_pnlPPlanTours.IsDisposed)
            {
                if (m_pnlPPlanTours != null && !m_pnlPPlanTours.IsDisposed)
                    m_pnlPPlanTours.Dispose();

                m_pnlPPlanTours = new pnlPPlanTours(m_PPlanCommonVars);
                m_pnlPPlanTours.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlPPlanTours_NotifyDataChanged);

                m_PPlanCommonVars.FocusedTour = m_pnlPPlanTours.GetSelectedTour();
                btnTourDetails.Enabled = m_PPlanCommonVars.FocusedTour != null;
                btnOptimizeTrk.Enabled = m_PPlanCommonVars.FocusedTour != null;
                btnTurnTour.Enabled = m_PPlanCommonVars.FocusedTour != null && m_pnlPPlanEditor != null && m_pnlPPlanEditor.EditMode;
                btnDelTour.Enabled = m_PPlanCommonVars.FocusedTour != null && m_pnlPPlanEditor != null && m_pnlPPlanEditor.EditMode;
                btnChgTruck.Enabled = m_PPlanCommonVars.FocusedTour != null && m_pnlPPlanEditor != null && m_pnlPPlanEditor.EditMode;

                //letároljuk a default beállítást
                m_defaultMPP_TGRID = UI.SaveGridLayoutToString(m_pnlPPlanTours.gridViewTours);

            }

            if (bForce || m_pnlPPlanTourPoints == null || m_pnlPPlanTourPoints.IsDisposed)
            {
                if (m_pnlPPlanTourPoints != null && !m_pnlPPlanTourPoints.IsDisposed)
                    m_pnlPPlanTourPoints.Dispose();

                m_pnlPPlanTourPoints = new pnlPPlanTourPoints(m_PPlanCommonVars);
                m_pnlPPlanTourPoints.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlPPlanTourPoints_NotifyDataChanged);

                //letároljuk a default beállítást
                m_defaultMPP_PGRID = UI.SaveGridLayoutToString(m_pnlPPlanTourPoints.gridViewTourPoints);

            }

            if (bForce || m_pnlPPlanEditor == null || m_pnlPPlanEditor.IsDisposed)
            {
                if (m_pnlPPlanEditor != null && !m_pnlPPlanEditor.IsDisposed)
                    m_pnlPPlanEditor.Dispose();

                m_pnlPPlanEditor = new pnlPPlanEditor(m_PPlanCommonVars);
                m_pnlPPlanEditor.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlPPlanEditor_NotifyDataChanged);
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
                    UI.Message(PMapMessages.E_PEDIT_LOCKEDTRUCK);
                    return;
                }

                if (PMapIniParams.Instance.TourRoute)
                    recalcCompletedTour(m_PPlanCommonVars.FocusedTour);
                else
                    calcMissingDistances();

                dlgOptimize dOpt = new dlgOptimize(m_PPlanCommonVars.PLN_ID, m_PPlanCommonVars.FocusedTour.ID);
                if (dOpt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (dOpt.Result == TourOptimizerProcess.eOptResult.OK)
                    {
                        m_PlanEditFuncs.RefreshToursAfterModify(m_PPlanCommonVars.FocusedTour.ID, 0);
                        m_PPlanCommonVars.PlanOrderList = m_bllPlan.GetPlanOrders(m_PPlanCommonVars.PLN_ID);  //
                    }
                    else if(dOpt.Result == TourOptimizerProcess.eOptResult.IgnoredHappened)
                    {
                        m_bllPlan.SetTourUnCompleted(m_PPlanCommonVars.FocusedTour.ID);
                        UI.Message(PMapMessages.E_PEDIT_IGNOREDORDERHAPPENED2, dOpt.IgnoredOrders);
                    }
                    RefreshAll(new PlanEventArgs(ePlanEventMode.RefreshOrders));
                }
                else
                {
                    
                    UI.Message(PMapMessages.E_PEDIT_OPTERR);
                }
            }
            else
            {
                UI.Message(PMapMessages.E_NOSELTOUR);
            }

        }

        private void btnNewTour_Click(object sender, EventArgs e)
        {
            dlgNewTour dlgNewTour = new dlgNewTour(m_PPlanCommonVars.PLN_ID, m_PlanEditFuncs, m_PPlanCommonVars);
            dlgNewTour.ShowDialog(this);
        }

        private void btnDelTour_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null)
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
            if (dOpt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                this.initPPlanForm(m_PPlanCommonVars.PLN_ID, true);

                if (dOpt.Result == TourOptimizerProcess.eOptResult.OK)
                {
                    MessageBox.Show(this, PMapMessages.M_PEDIT_OPTOK);
                }
                else if (dOpt.Result == TourOptimizerProcess.eOptResult.IgnoredHappened)
                {
                    MessageBox.Show(this, PMapMessages.E_PEDIT_IGNOREDORDERHAPPENED1);
                }
                else
                {
                    MessageBox.Show(this, PMapMessages.E_PEDIT_OPTERR);

                }

            }
        }

        private void btnNewPlan_Click(object sender, EventArgs e)
        {
            dlgNewPlan np = new dlgNewPlan(m_planParams);
            if (np.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                initPPlanForm(np.PLN_ID, true);
            }

        }

        private void cmbPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            initPPlanForm((int)cmbPlans.ComboBox.SelectedValue, true);
        }

        private void btnDelPlan_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.PLN_ID > 0 && UI.Confirm(PMapMessages.Q_PEDIT_DELPLAN))
            {
                if (m_bllSemaphore.SetPlanSemaphore(m_PPlanCommonVars.PLN_ID) == bllSemaphore.SEMValues.SMV_FREE)
                {

                    m_bllPlanEdit.DeletePlan(m_PPlanCommonVars.PLN_ID);
                    initPPlanForm(0, true);
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
                calcMissingDistances();

            }

        }

        private void calcMissingDistances()
        {
            bllRoute bllRoute = new bllRoute(PMapCommonVars.Instance.CT_DB);
            List<boRoute> res = bllRoute.GetDistancelessPlanNodes(m_PPlanCommonVars.PLN_ID);
            bllSemaphore bllSemaphore = new bllSemaphore(PMapCommonVars.Instance.CT_DB);

            if (res.Count == 0)
                return;

            bool bOK = false;

            bllSemaphore.SetCalcRoutePlanSemaphore(m_PPlanCommonVars.PLN_ID, Global.CLCROUTE_OWNER);

            if (PMapIniParams.Instance.RouteThreadNum > 1)
                bOK = PMRouteInterface.GetPMapRoutesMulti(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, true);
            else
                bOK = PMRouteInterface.GetPMapRoutesSingle(res, "", PMapIniParams.Instance.CalcPMapRoutesByPlan, true, true);

            if (bOK)
            {
                bllSemaphore.FreeCalcRoutePlanSemaphore(m_PPlanCommonVars.PLN_ID, Global.CLCROUTE_OWNER);
                //                    UI.Message(PMapMessages.M_PEDIT_CALCDST_END);
            }
        }
        private void cmbPlans_Click(object sender, EventArgs e)
        {

        }

        private void btnToCloud_Click(object sender, EventArgs e)
        {
            if (UI.Confirm(PMapMessages.Q_PEDIT_UPLOAD))
            {
                var crypto = new AuthCryptoHelper("$2a$10$GH1ygiHqiZ9Q18Bk.1hrJ.");
                
                try
                {


                    using (new WaitCursor())
                    {


                        Util.Log2File("SendToAzure START, PLN_ID:"+ m_PPlanCommonVars.PLN_ID.ToString());


                        //Felhasználók
                        var us = AzureTableStore.Instance.RetrieveList<PMUser>();
                        AzureTableStore.Instance.DeleteRange<PMUser>(us.Select(s => new AzureItemKeys(s.PartitionKey, s.ID)).ToList());


                        var lstUsers = m_bllUser.GetAllUsers();
                        FileInfo fiUsers = new FileInfo(Path.Combine(PMapIniParams.Instance.LogDir, "users.dmp"));
                        BinarySerializer.Serialize(fiUsers, lstUsers);

                        foreach (var usr in lstUsers.Where(w => !w.USR_DELETED).ToList())
                        {
                            PMUser pmUsr = new PMUser()
                            {
                                ID = usr.USR_LOGIN,
                                UserName = usr.USR_NAME,
                                Password = !String.IsNullOrWhiteSpace(usr.USR_PASSWD) ? crypto.HashPassword(usr.USR_PASSWD) : "",
                                UserType = !String.IsNullOrWhiteSpace(usr.UST_NAME) ? usr.UST_NAME : "Admin"

                            };
                            AzureTableStore.Instance.Insert(pmUsr, Environment.MachineName);

                        }


                        var tours = m_bllPlan.GetPlanTours(m_PPlanCommonVars.PLN_ID);

                        FileInfo fiTours = new FileInfo(Path.Combine(PMapIniParams.Instance.LogDir, "tours.dmp"));
                        BinarySerializer.Serialize(fiTours, tours);


                        var tourList = m_bllPlan.GetToursForAzure(m_PPlanCommonVars.PLN_ID, tours);
                        FileInfo fiToursList = new FileInfo(Path.Combine(PMapIniParams.Instance.LogDir, "tourlist.dmp"));
                        BinarySerializer.Serialize(fiToursList, tourList);


                        BllWebTraceTour bllWebTrace = new BllWebTraceTour(Environment.MachineName);
                        BllWebTraceTourPoint bllWebTraceTourPoint = new BllWebTraceTourPoint(Environment.MachineName);


                        var tp = AzureTableStore.Instance.RetrieveList<PMTourPoint>();

                        //5 napnyi adatot megtartunk
                        var tr = AzureTableStore.Instance.RetrieveList<PMTour>();
                        var delTours = tr.Where(w => w.Start.Date.AddDays(5) <= DateTime.Now.Date || w.PLN_ID == m_PPlanCommonVars.PLN_ID).Select(s => new AzureItemKeys(s.PartitionKey, s.ID)).ToList();
                        AzureTableStore.Instance.DeleteRange<PMTour>(delTours);


                        var delPoints = tp.Where(w => delTours.Any( a=>a.RowKey==w.TourID.ToString())).Select(s => new AzureItemKeys(s.TourID.ToString(), AzureTableStore.GetValidAzureKeyValue(typeof(string), s.Order))).ToList();
                        AzureTableStore.Instance.DeleteRange<PMTourPoint>(delPoints);




                        foreach (var xTr in tourList)
                        {
                            bllWebTrace.MaintainItem(xTr);
                            AzureTableStore.Instance.BatchInsertOrReplace<PMTourPoint>(xTr.TourPoints, Environment.MachineName);
                        }

                        Util.Log2File("SendToAzure END tours:"+ tourList.Count().ToString());

                        UI.Message(PMapMessages.M_PEDIT_UPLOADOK);

                        if (UI.Confirm(PMapMessages.Q_PEDIT_SENDEMAIL1) && UI.Confirm(PMapMessages.Q_PEDIT_SENDEMAIL2))
                        {
                            using (TransactionBlock transObj = new TransactionBlock(PMapCommonVars.Instance.CT_DB))
                            {
                                try
                                {
                                    Dictionary<string, List<PMTracedTour>> lstEmail = new Dictionary<string, List<PMTracedTour>>();
                                    foreach (var tour in tours)
                                    {
                                        foreach (var tourpont in tour.TourPoints)
                                        {
                                            if (!tourpont.TOD_SENTEMAIL && !String.IsNullOrEmpty(tourpont.ORD_EMAIL))
                                            {
                                                m_bllPlan.SetTourPointSent(tourpont.TOD_ID);

                                                //érvénytelen karakterek kiszedése
                                                var emailAddr = tourpont.ORD_EMAIL.Replace(" ", "");
                                                emailAddr = emailAddr.Replace("\"", "");
                                                emailAddr = emailAddr.Replace("'", "");
                                                emailAddr = emailAddr.Replace(",", ";");

                                                var emailAddress = emailAddr.Split(';').ToList();
                                                foreach (string em in emailAddress)
                                                {

                                                    PMTracedTour tt = new PMTracedTour() { TourID = tourpont.Tour.ID, Order = tourpont.PTP_ORDER };

                                                    if (!lstEmail.ContainsKey(em))
                                                    {
                                                        List<PMTracedTour> tracedTour = new List<PMTracedTour>();
                                                        tracedTour.Add(tt);

                                                        lstEmail.Add(em, tracedTour);
                                                    }
                                                    else
                                                    {
                                                        lstEmail[em].Add(tt);
                                                    }


                                                }

                                            }
                                        }
                                    }
                                    int sentEmails = 0;
                                    List<string> invalidEmails = new List<string>();
                                    foreach (var emailItem in lstEmail)
                                    {
                                        if (Util.IsValidEmail(emailItem.Key))
                                        {
                                            var token = NotificationMail.GetToken(emailItem.Value);
                                            NotificationMail.SendNotificationMail(emailItem.Key, token);
                                            Util.Log2File(String.Format(PMapMessages.M_MAIL_SENT, emailItem.Key));
                                            sentEmails++;
                                        }
                                        else
                                        {
                                            invalidEmails.Add(emailItem.Key);
                                        }
                                    }

                                    PMapCommonVars.Instance.CT_DB.Commit();

                                    if (invalidEmails.Count == 0)
                                    {
                                        UI.Message(PMapMessages.E_SNDEMAIL_OK, sentEmails);
                                        Util.Log2File( String.Format(PMapMessages.E_SNDEMAIL_OK, sentEmails));
                                    }
                                    else
                                    {
                                        UI.Message(PMapMessages.E_SNDEMAIL_OK2, sentEmails, invalidEmails.Count, String.Join("\n", invalidEmails));
                                        Util.Log2File(String.Format(PMapMessages.E_SNDEMAIL_OK2, sentEmails, invalidEmails.Count, String.Join("\n", invalidEmails)));
                                    }
                                }
                                catch (Exception exx)
                                {
                                    PMapCommonVars.Instance.CT_DB.Rollback();
                                    throw;
                                }
                            }


                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.ExceptionLog(ex);

                    UI.Error(ex.Message);
                    //                throw;
                }
                finally
                {
                }
            }
        }

        private void btnCheckMapOn_Click(object sender, EventArgs e)
        {

            SetViewMode(true);
            PlanEventArgs ev = new PlanEventArgs(ePlanEventMode.CheckMode);
            RefreshAll(ev);

            btnCheckMapOn.Visible = false;
            btnCheckMapOff.Visible = true;
            PMapCommonVars.Instance.IsCheckMode = true;
        }

        private void btnCheckMapOff_Click(object sender, EventArgs e)
        {
            SetViewMode(true);
            btnCheckMapOff.Visible = false;
            btnCheckMapOn.Visible = true;
            PMapCommonVars.Instance.IsCheckMode = true;

        }

        private void btnCompleteTourRoutes_Click(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.FocusedTour != null)

            {
                if (m_PPlanCommonVars.FocusedTour.LOCKED)
                {
                    UI.Message(PMapMessages.E_PEDIT_LOCKEDTRUCK);
                    return;
                }
                recalcCompletedTour(m_PPlanCommonVars.FocusedTour);
                RefreshAll(new PlanEventArgs(ePlanEventMode.Refresh));
            }
            else
            {
                UI.Message(PMapMessages.E_NOSELTOUR);
            }

        }

        private void recalcCompletedTour(boPlanTour p_tour)
        {

            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);
            m_bllPlan.SetTourCompleted(p_tour);
            BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, p_tour.TOURPOINTCNT, PMapMessages.T_COMPLETE_TOURROUTES, false);
            CalcRoutesForTours rpp = new CalcRoutesForTours(pd, p_tour);
            rpp.Run();
            pd.ShowDialog();

            if (rpp.CompleteCode == CalcRoutesForTours.eCompleteCode.UserBreak)
            {
                UI.Message(PMapMessages.E_TOURCOMPL_ABORTED);
                m_bllPlan.SetTourUnCompleted(p_tour);
                // PMapCommonVars.Instance.CT_DB.Rollback();
            }

            if (rpp.CompleteCode == CalcRoutesForTours.eCompleteCode.NoRouteOccured)
            {
                var pts = string.Join(Environment.NewLine, rpp.NoRoutes);
                UI.Message(PMapMessages.E_TOURCOMPL_NOGETROUTES, pts);
                m_bllPlan.SetTourUnCompleted(p_tour);
                // PMapCommonVars.Instance.CT_DB.Rollback();
            }


            m_bllPlanEdit.RecalcTour(0, p_tour.ID, Global.defWeather);

            m_PlanEditFuncs.RefreshToursAfterModify(p_tour.ID, 0);
            
        }

        private void btnOpenClose_Click(object sender, EventArgs e)
        {
            int ID = m_pnlPlanOrders.GetID();
            if (ID > 0)
            {
                dlgAddOpenClose aoc = new dlgAddOpenClose(ID);
                if (aoc.ShowDialog(this) == DialogResult.OK)
                {
                    var bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
                    m_PPlanCommonVars.PlanOrderList = bllPlan.GetPlanOrders(m_PPlanCommonVars.PLN_ID);
                    m_pnlPlanOrders.RefreshAll(true);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            try

            {

                var bllRepPlan = new bllRepPlan(PMapCommonVars.Instance.CT_DB);
                List<boRepPlan> tourList = new List<boRepPlan>();
                using (new WaitCursor())
                {
                    tourList = bllRepPlan.GetRepPlanData(m_PPlanCommonVars.PLN_ID);
                }

                 var rep = new RepPlanDetails(tourList);
                rep.ShowPreview();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    #endregion
}
