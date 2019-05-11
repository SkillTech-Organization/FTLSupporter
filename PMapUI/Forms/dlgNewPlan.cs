using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapUI.Forms.Base;
using PMapCore.BLL;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.BO;
using PMapCore.Strings;
using PMapCore.BO.DataXChange;
using PMapCore.Common.PPlan;
using PMapUI.Common;

namespace PMapUI.Forms
{
    public partial class dlgNewPlan : BaseDialog
    {
        public int PLN_ID = 0;
        private bllPlanEdit m_bllPlanEdit;
        private bllWarehouse m_bllWarehouse;
        private PlanParams m_planParams;

        public dlgNewPlan( PlanParams p_planParams)
        {
            InitializeComponent();

            m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
            m_bllWarehouse = new bllWarehouse(PMapCommonVars.Instance.CT_DB);
            m_planParams = p_planParams;

            cmbWarehouse.DisplayMember = "WHS_NAME";
            cmbWarehouse.ValueMember = "ID";
            cmbWarehouse.Items.Clear();
            cmbWarehouse.DataSource = m_bllWarehouse.GetAllWarehouses();
            dtpPLN_DATE_B.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpPLN_DATE_E.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0);
            InitDialog();

        }

        public override Control ValidateForm()
        {
            if (txtPLN_NAME.Text.Trim() == "")
            {
                UI.Error(PMapMessages.E_NEWPLAN_EMPTY_PLNAME);
                return txtPLN_NAME;
            }

            if (dtpPLN_DATE_B.Value > dtpPLN_DATE_E.Value)
            {
                UI.Error(DXMessages.E_PLN_WRONG_DATE);
                return dtpPLN_DATE_B;
            }

            if ((dtpPLN_DATE_E.Value - dtpPLN_DATE_B.Value).TotalDays > 3)
            {
                UI.Error(DXMessages.E_PLN_TOOIBIG_INTERVAL);
                return dtpPLN_DATE_B;
            }

            return null;
        }

        public override bool OKPressed()
        {
            using (TransactionBlock transObj = new TransactionBlock(PMapCommonVars.Instance.CT_DB))
            {
                try
                {
                    //egyelőre nincs intervallum kezelés 
                    int WHS_ID = Convert.ToInt32(cmbWarehouse.SelectedValue);
                    boXNewPlan ret = m_bllPlanEdit.CreatePlan(txtPLN_NAME.Text.Trim(), WHS_ID, dtpPLN_DATE_B.Value, dtpPLN_DATE_E.Value, false, dtpPLN_DATE_B.Value, dtpPLN_DATE_E.Value, m_planParams.EnabledTrucksInNewPlan);
                    if (ret.Status != boXNewPlan.EStatus.OK)
                    {
                        UI.Error(ret.ErrMessage);
                        PMapCommonVars.Instance.CT_DB.Rollback();
                        return false;
                    }
                    if (ret.lstDepWithoutGeoCoding.Count > 0)
                    {
                        dlgDepotList dl = new dlgDepotList(ret.lstDepWithoutGeoCoding, PMapMessages.M_NEWPLAN_GEOCODELESS_DEP);
                        dl.ShowDialog();
                    }
                    PLN_ID = ret.PLN_ID;
                    return true;
                }
                catch (Exception exc)
                {
                    PMapCommonVars.Instance.CT_DB.Rollback();
                    Util.ExceptionLog(exc);
                    throw new Exception(exc.Message);
                }

            }
        }

   
    }
}

