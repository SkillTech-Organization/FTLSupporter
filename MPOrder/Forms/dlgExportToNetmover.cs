using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.Common;
using PMapUI.Forms.Base;
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
        public string ExportFile { get; private set; }
        public int PLN_ID { get; private set; } = 0;

        private string m_CSVFile;
        private DateTime m_shippingDate;

        List<boPlan> m_lstPlan = new List<boPlan>();
        bllPlan m_bllPlan ;
        public dlgExportToNetmover(string p_CSVFile, DateTime p_shippingDate )
            :base(eEditMode.editmode)
        {
            InitializeComponent();
            InitDialog();

            m_CSVFile = p_CSVFile;
            m_shippingDate = p_shippingDate;
            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_lstPlan = m_bllPlan.GetPlansByDateTime(m_shippingDate.Date.AddHours(12)); //Déli 12 óra jó lesz...

            ExportFile = Path.GetDirectoryName(p_CSVFile) + "\\EXP_" + Path.GetFileName(p_CSVFile);
            dlgSaveCSV.FileName = ExportFile;
            lblvFile.Text = ExportFile;
        }


        private void dlgExportToNetmover_Load(object sender, EventArgs e)
        {
            gridPlans.DataSource = m_lstPlan;
            gridViewPlans.RefreshData();
      //      buttonOK.Enabled = m_lstPlan.Count > 0;

        }

        private void btnFile_Click(object sender, EventArgs e)
        {

            if (dlgSaveCSV.ShowDialog() == DialogResult.OK)
            {
                lblvFile.Text = dlgSaveCSV.FileName;
                ExportFile = dlgSaveCSV.FileName;
            }
        }
        public override bool OKPressed()
        {


            return base.OKPressed();
        }

        private void gridViewPlans_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            PLN_ID = (int) gridViewPlans.GetRowCellValue(e.FocusedRowHandle, grcID);
            buttonOK.Enabled = PLN_ID > 0;
        }
    }
}
