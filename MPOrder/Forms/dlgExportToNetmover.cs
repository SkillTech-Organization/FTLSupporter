using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.Common;
using PMapCore.Forms.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPOrder.Forms
{
    public partial class dlgExportToNetmover : BaseDialog
    {
        string m_exportFile;
        DateTime m_shippingDate;
        List<boPlan> m_lstPlan = new List<boPlan>();
        bllPlan m_bllPlan ;
        public dlgExportToNetmover(string p_exportFile, DateTime p_shippingDate )
            :base(eEditMode.editmode)
        {
            InitializeComponent();
            InitDialog();
            m_shippingDate = p_shippingDate;
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_lstPlan = m_bllPlan.GetPlansByDateTime(m_shippingDate.Date.AddHours(12)); //Déli 12 óra jó lesz...

            m_exportFile = Path.GetFullPath(p_exportFile) + "\\EXP_" + Path.GetFileName(p_exportFile);
            dlgSaveCSV.FileName = m_exportFile;
        }


        private void dlgExportToNetmover_Load(object sender, EventArgs e)
        {
            gridPlans.DataSource = m_lstPlan;
            gridViewPlans.RefreshData();
            buttonOK.Enabled = m_lstPlan.Count > 0;

        }

        private void btnFile_Click(object sender, EventArgs e)
        {

            if (dlgSaveCSV.ShowDialog() == DialogResult.OK)
            {
                lblvFile.Text = dlgSaveCSV.FileName;
                m_exportFile = dlgSaveCSV.FileName;
            }
        }
        public override bool OKPressed()
        {


            return base.OKPressed();
        }
    }
}
