using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.DB;
using PMapCore.BO;
using PMapCore.BLL;
using PMapCore.Strings;
using PMapCore.Common;
using PMapUI.Forms.Base;
using PMapUI.Forms.Funcs;
using PMapCore.Common.PPlan;

namespace PMapUI.Forms
{
    public partial class dlgNewTour : BaseDialog
    {
        private int m_PLN_ID;
        private boActivePlanInfo m_ActivePlanInfo;
        private DataTable m_dtUnplannedTrucks;

        private bllPlan m_bllPlan;
        private PlanEditFuncs m_PlanEditFuncs;
        private PPlanCommonVars m_PPlanCommonVars;

        public dlgNewTour(int p_PLN_ID, PlanEditFuncs p_PlanEditFuncs, PPlanCommonVars p_PPlanCommonVars)
            : base(eEditMode.editmode)
        {
            InitializeComponent();
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_PlanEditFuncs = p_PlanEditFuncs;
            m_PPlanCommonVars = p_PPlanCommonVars;
            
            m_PLN_ID = p_PLN_ID;
            m_dtUnplannedTrucks = m_bllPlan.GetUnplannedTrucks(m_PLN_ID);
            m_ActivePlanInfo = m_bllPlan.GetActivePlanInfo(m_PLN_ID);
            cmbTruck.DisplayMember = "TRK_DISP";
            cmbTruck.ValueMember = "TPL_ID";
            cmbTruck.Items.Clear();
            cmbTruck.DataSource = m_dtUnplannedTrucks;
            txWHS_NAME.Text = m_ActivePlanInfo.WHS_NAME;
            frcSrvTime.Value  = m_ActivePlanInfo.WHS_SRVTIME_UNLOAD;
            txtSrvTime.Text = m_ActivePlanInfo.WHS_SRVTIME.ToString();
            dtpWhsS.MinDate = m_ActivePlanInfo.OPEN;
            dtpWhsE.MinDate = m_ActivePlanInfo.OPEN;
            dtpWhsS.MaxDate = m_ActivePlanInfo.CLOSE;
            dtpWhsE.MaxDate = m_ActivePlanInfo.CLOSE;


            //Win10 compatibilitás miatt újraépítjük a dátumot
            /*
            dtpWhsS.Value = new DateTime(m_ActivePlanInfo.OPEN.Year, m_ActivePlanInfo.OPEN.Month, m_ActivePlanInfo.OPEN.Day, m_ActivePlanInfo.OPEN.Hour, m_ActivePlanInfo.OPEN.Minute, m_ActivePlanInfo.OPEN.Second);
            DateTime dtWrk = m_ActivePlanInfo.OPEN.AddMinutes(Convert.ToInt32(m_ActivePlanInfo.WHS_SRVTIME));
            dtpWhsE.Value = new DateTime(dtWrk.Year, dtWrk.Month, dtWrk.Day, dtWrk.Hour, dtWrk.Minute, dtWrk.Second);
            */
            dtpWhsS.Value = m_ActivePlanInfo.OPEN;
            dtpWhsE.Value =  m_ActivePlanInfo.OPEN.AddMinutes(Convert.ToInt32(m_ActivePlanInfo.WHS_SRVTIME)) > m_ActivePlanInfo.CLOSE ?
                m_ActivePlanInfo.CLOSE :  m_ActivePlanInfo.OPEN.AddMinutes(Convert.ToInt32(m_ActivePlanInfo.WHS_SRVTIME));

            InitDialog();
            AskOnExit = false;


        }

        public override Control ValidateForm()
        {
            if (dtpWhsE.Value < dtpWhsS.Value)
            {
                UI.Error(PMapMessages.E_PEDIT_WRONGARR);
                return dtpWhsE;
            }
            return null;
        }

        public override bool OKPressed()
        {

            int TPL_ID = Convert.ToInt32(cmbTruck.SelectedValue);
            m_PlanEditFuncs.CreateNewTour(m_PLN_ID, m_ActivePlanInfo.WHS_ID, TPL_ID, cmbColor.SelectedColor, dtpWhsS.Value, dtpWhsE.Value, Convert.ToInt32(frcSrvTime.Value));
            return true;
        }

        
        private void dtpWhsS_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpWhsE.Value = dtpWhsS.Value.AddMinutes(Convert.ToInt32(m_ActivePlanInfo.WHS_SRVTIME));
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                if (ex.GetType() == typeof(ArgumentOutOfRangeException))
                {
                    UI.Error(PMapMessages.E_PEDIT_WRONTIME, dtpWhsE.MinDate, dtpWhsE.MaxDate);
                }
 
            }
  
        }

        private void cmbTruck_TextChanged(object sender, EventArgs e)
        {

            int TPL_ID = Convert.ToInt32(cmbTruck.SelectedValue);
            boPlanTour tour = m_PPlanCommonVars.TourList.Where( i=> i.ID == TPL_ID).ToList().First();
            if (tour != null)
            {
                cmbColor.SelectedColor = tour.PCOLOR;

            }
            else
            {

                // Valamiért nincs meg a jármű
                Random rnd = new Random((int)DateTime.Now.Millisecond);
                cmbColor.SelectedColor = Color.FromArgb(rnd.Next(0, 127) * 2, rnd.Next(0, 255), rnd.Next(0, 255));
            }
        }


    }
}
