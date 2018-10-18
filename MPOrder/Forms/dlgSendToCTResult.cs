using PMapCore.Forms.Base;
using PMapCore.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPOrder.Forms
{
    public partial class dlgSendToCTResult : BaseDialog
    {
        public List<SendResult> Result { get; set; } = new List<SendResult>();
        public dlgSendToCTResult()
            :base(eEditMode.infomode)
        {
            InitializeComponent();
            InitDialog();

        }

        private void dlgSendToCTResult_Load(object sender, EventArgs e)
        {
            gridResult.DataSource = Result;
            gridViewResult.RefreshData();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            PMapGridPrinting pr = new PMapGridPrinting(gridResult, this.Text, true);
            pr.ShowPreview();

        }

        private void excelToolStripButton_Click(object sender, EventArgs e)
        {

            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                gridResult.ExportToXls(openExcel.FileName);
            }

        }
    }
}
