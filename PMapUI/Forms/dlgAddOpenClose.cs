using PMapCore.BLL;
using PMapCore.Common;
using PMapUI.Forms.Funcs;
using PMapUI.Forms.Base;
using PMapCore.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMapUI.Common;

namespace PMapUI.Forms
{
    public partial class dlgAddOpenClose : BaseDialog
    {
        private bllPlan m_bllPlan;
        private int m_TOD_ID;

        public dlgAddOpenClose(int p_TOD_ID)
            : base(eEditMode.editmode)
        {
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_TOD_ID = p_TOD_ID;
            InitializeComponent();
            var boPlanOrder = m_bllPlan.GetPlanOrder(p_TOD_ID);
            if (boPlanOrder == null)
                throw new Exception("Unkown TOD_ID:" + p_TOD_ID.ToString());

            txtName.Text = boPlanOrder.ORD_NUM + " " + boPlanOrder.DEP_NAME;
            dtpOpen.Value = new DateTime(1980, 01, 01, (int)(boPlanOrder.TOD_SERVS / 60), (int)(boPlanOrder.TOD_SERVS % 60), 0, 0);
            dtpClose.Value = new DateTime(1980, 01, 01, (int)(boPlanOrder.TOD_SERVE / 60), (int)(boPlanOrder.TOD_SERVE % 60), 0, 0);
        }

        public override Control ValidateForm()
        {
            if (dtpClose.Value < dtpOpen.Value)
            {
                UI.Error(PMapMessages.E_PEDIT_WRONGOPENCLOSE);
                return dtpClose;
            }
            return null;
        }
        public override bool OKPressed()
        {
            var bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
            int TOD_SERVS = dtpOpen.Value.Hour * 60 + dtpOpen.Value.Minute ;
            int TOD_SERVE = dtpClose.Value.Hour * 60 + dtpClose.Value.Minute;
            bllPlanEdit.UpdateTourOrderOpenClose(m_TOD_ID, TOD_SERVS, TOD_SERVE);
            return true;
        }
    }
}
