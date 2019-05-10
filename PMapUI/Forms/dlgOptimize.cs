﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.LongProcess.Base;
using PMapCore.LongProcess;
using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.Strings;
using PMapUI.Forms.Base;
using PMapCore.Common;
using System.Runtime.ExceptionServices;

namespace PMapUI.Forms
{
    public partial class dlgOptimize : BaseDialog
    {

        public TourOptimizerProcess.eOptResult Result { get; set; }
        public string IgnoredOrders { get; private set; } = "";

        private boPlan m_boPlan = null;
        private boPlanTour m_boTour = null;

        private bllPlanEdit m_bllPlanEdit;
        private bllPlan m_bllPlan;
        private bllSemaphore m_bllSemaphore;

        public dlgOptimize(int p_PLN_ID, int p_TPL_ID)
            : base(eEditMode.editmode)
        {
            InitializeComponent();
            m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_bllSemaphore = new bllSemaphore(PMapCommonVars.Instance.CT_DB);

            m_boPlan = m_bllPlan.GetPlan(p_PLN_ID);

            if (p_TPL_ID > 0)
                m_boTour = m_bllPlan.GetPlanTour(p_TPL_ID);

            txtPLN_NAME.Text = m_boPlan.PLN_NAME;
            txtPLN_DATE_B.Text = m_boPlan.PLN_DATE_B.ToString(Global.DATETIMEFORMAT);
            txtPLN_DATE_E.Text = m_boPlan.PLN_DATE_E.ToString(Global.DATETIMEFORMAT);

            InitDialog();
            this.AcceptButton = buttonOK;

            AskOnExit = false;

        }

        public override bool OKPressed()
        {

            try
            {
                if (m_bllSemaphore.SetPlanSemaphore(m_boPlan.ID) == bllSemaphore.SEMValues.SMV_FREE)
                {

                    m_bllPlanEdit.SetOptimizePars(m_boPlan.ID, 0, 1, 999, chkReplan.Checked ? 1 : 0, m_boTour != null ? m_boTour.ID : 0);


                    BaseSilngleProgressDialog pd = new BaseSilngleProgressDialog(0, PMapIniParams.Instance.OptimizeTimeOutSec, m_boTour != null ? PMapMessages.M_OPT_HDR_TOUR : PMapMessages.M_OPT_HDR_PLAN, true);
                    TourOptimizerProcess top = new TourOptimizerProcess(pd, m_boPlan.ID, m_boTour != null ? m_boTour.ID : 0, chkReplan.Checked, !PMapIniParams.Instance.TestMode);

                    top.Run();
                    pd.ShowDialog();
                    IgnoredOrders = top.IgnoredOrders;
                    Result = top.Result;
                    return true;
                }
                else
                {
                    UI.Warning(PMapMessages.E_PEDIT_OPTANOTHERWS);
                    return false;
                }
            }

            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
            finally
            {
                m_bllSemaphore.FreePlanSemaphore(m_boPlan.ID);
            }
        }

        private void dlgOptimize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                OKPressed();
            }
        }
    }
}