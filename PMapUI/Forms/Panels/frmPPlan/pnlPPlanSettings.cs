using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using GMap.NET.WindowsForms;
using PMapCore.DB;
using PMapCore.BO;
using PMapCore.BLL;
using PMapCore.Strings;
using PMapUI.Forms.Base;
using PMapUI.Forms.Funcs;
using PMapCore.Common.PPlan;
using PMapUI.Common;
using PMapCore.Common;

namespace PMapUI.Forms.Panels.frmPPlan
{
    public partial class pnlPPlanSettings : BasePanel
    {
        private bool m_hideAll = true;
        private bllPlanEdit m_bllPlanEdit;
        private bllPlan m_bllPlan;
        private PlanEditFuncs m_PlanEditFuncs;

        private PPlanCommonVars m_PPlanCommonVars;

        public pnlPPlanSettings(PPlanCommonVars p_PPlanCommonVars)
        {
            InitializeComponent();
            m_PPlanCommonVars = p_PPlanCommonVars;
            Init();
            tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
        }


        public void Init()
        {
            InitPanel();
            m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_PlanEditFuncs = new PlanEditFuncs(this, m_PPlanCommonVars);

            tbZoom.Minimum = Global.DefMinZoom;
            tbZoom.Maximum = Global.DefMaxZoom;
            tbZoom.Value = m_PPlanCommonVars.Zoom;
            chkShowPlannedDepots.Checked = m_PPlanCommonVars.ShowPlannedDepots;
            chkShowUnplannedDepots.Checked = m_PPlanCommonVars.ShowUnPlannedDepots;
            chkZoomToSelectedTour.Checked = m_PPlanCommonVars.ZoomToSelectedPlan;
            chkZoomToSelectedUnPlanned.Checked = m_PPlanCommonVars.ZoomToSelectedUnPlanned;
            chkShowAllOrdersInGrid.Checked = m_PPlanCommonVars.ShowAllOrdersInGrid;
            numTPArea.Value = PMapCommonVars.Instance.TPArea;

            switch (m_PPlanCommonVars.TooltipMode)
            {
                case MarkerTooltipMode.OnMouseOver:
                    rdbOnMouseOver.Checked = true;
                    break;
                case MarkerTooltipMode.Never:
                    rdbNever.Checked = true;
                    break;
                case MarkerTooltipMode.Always:
                    rdbAlways.Checked = true;
                    break;
                default:
                    break;
            }

        }

        private void tbZoom_ValueChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.Zoom = (tbZoom.Value);
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgZoom));



        }


        public void RefreshPanel(PlanEventArgs p_planEventArgs)
        {
            switch (p_planEventArgs.EventMode)
            {
                case ePlanEventMode.ChgZoom:
                    tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
                    tbZoom.Value = m_PPlanCommonVars.Zoom;
                    tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
                    break;
                case ePlanEventMode.ChgShowPlannedFlag:
                    break;
                case ePlanEventMode.ChgShowUnPlannedFlag:
                    break;
                default:
                    break;
            }

        }

        private void chkShowPlannedDepots_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.ShowPlannedDepots = chkShowPlannedDepots.Checked;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgShowPlannedFlag));


        }

        private void chkShowUnplannedDepots_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.ShowUnPlannedDepots = chkShowUnplannedDepots.Checked;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgShowUnPlannedFlag));
        }

        private void rdbNever_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.TooltipMode = MarkerTooltipMode.Never;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTooltipMode));

        }

        private void rdbOnMouseOver_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.TooltipMode = MarkerTooltipMode.OnMouseOver;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTooltipMode));
        }

        private void rdbAlways_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.TooltipMode = MarkerTooltipMode.Always;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTooltipMode));
        }

        private void btnHideShowAllTours_Click(object sender, EventArgs e)
        {

            foreach (boPlanTour rt in m_PPlanCommonVars.TourList)
            {
                rt.PSelect = !m_hideAll;
                rt.Layer.IsVisibile = !m_hideAll;
            }
            m_bllPlanEdit.ChangeAllTourSelected(m_PPlanCommonVars.PLN_ID, !m_hideAll);

            if (m_hideAll)
            {
                m_hideAll = false;
                btnHideShowAllTours.Text = PMapMessages.M_SETT_SHOWALL;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.HideAllTours));
            }
            else
            {
                m_hideAll = true;
                btnHideShowAllTours.Text = PMapMessages.M_SETT_HIDEALL;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ShowAllTours));
            }


        }

        private void chkZoomToSelectedTour_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.ZoomToSelectedPlan = chkZoomToSelectedTour.Checked;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgZoomToSelectedTour));

        }

        private void chkZoomToSelectedUnPlanned_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.ZoomToSelectedUnPlanned = chkZoomToSelectedUnPlanned.Checked;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgZoomToSelectedUnPlanned));
        }

        private void chkAllOrders_CheckedChanged(object sender, EventArgs e)
        {
            m_PPlanCommonVars.ShowAllOrdersInGrid = chkShowAllOrdersInGrid.Checked;
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgShowAllOrdersInGrid));
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {

            if (!m_PlanEditFuncs.SetPositionByOrd_NUM(txtORD_NUM.Text))
                UI.Message(PMapMessages.E_PLANSETT_ORDER_NOT_FOUND);
        }

        private void numTPArea_ValueChanged(object sender, EventArgs e)
        { 
            PMapCommonVars.Instance.TPArea = (int)numTPArea.Value;
        }
    }
}
