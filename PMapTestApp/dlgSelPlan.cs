using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.DB.Base;
using PMapCore.Common;

namespace PMapTestApp
{
    public partial class dlgSelPlan : Form
    {
        public int m_PLN_ID = 0;
        public dlgSelPlan()
        {
            InitializeComponent();
            SQLServerAccess db = new SQLServerAccess();
            db.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            DataTable dt = db.Query2DataTable("select * from PLN_PUBLICATEDPLAN order by ID desc");
            db.Close();
            gridPLN.DataSource = dt;

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            m_PLN_ID = (int)gridViewPLN.GetRowCellValue(gridViewPLN.FocusedRowHandle, colID);
        }


        private void gridPLN_DoubleClick(object sender, EventArgs e)
        {
            m_PLN_ID = (int)gridViewPLN.GetRowCellValue(gridViewPLN.FocusedRowHandle, colID);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
