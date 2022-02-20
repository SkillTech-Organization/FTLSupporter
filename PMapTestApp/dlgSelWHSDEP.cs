﻿using System;
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
    public partial class dlgSelWHSDEP : Form
    {
        public string m_XNAME { get; set; }
        public int m_NOD_ID { get; set; }
        public decimal m_NOD_XPOS { get; set; }
        public decimal m_NOD_YPOS { get; set; }

        public dlgSelWHSDEP()
        {
            InitializeComponent();
            string sSql = "select * from (select WHS.ID+10000000 as ID  , '**' + WHS_CODE + '**' as XCODE,  WHS_NAME  as XNAME, " +
                    "convert( varchar(max), ZIP.ZIP_NUM) + ' ' + ZIP_CITY + ' ' + WHS_ADRSTREET as XADDR, NOD_ID, NOD_XPOS, NOD_YPOS from WHS_WAREHOUSE (nolock) WHS " +
                    "inner join NOD_NODE (nolock) NOD on NOD.ID = NOD_ID " +
                    "inner join ZIP_ZIPCODE (nolock) ZIP on ZIP.ID = WHS.ZIP_ID " +
                    "union " +
                    "select DEP.ID, DEP_CODE as XCODE, DEP_NAME as XNAME,  "+
                    "convert( varchar(max), ZIP.ZIP_NUM) + ' ' + ZIP_CITY + ' ' + DEP_ADRSTREET as XADDR, NOD_ID, NOD_XPOS, NOD_YPOS from DEP_DEPOT (nolock) DEP " +
                    "inner join NOD_NODE (nolock) NOD on NOD.ID = NOD_ID " +
                    "inner join ZIP_ZIPCODE (nolock) ZIP on ZIP.ID = DEP.ZIP_ID " +
                    ") x " +
                    "order by x.XNAME";
            SQLServerAccess db = new SQLServerAccess();
            db.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            DataTable dt = db.Query2DataTable( sSql);
            db.Close();
            gridWHSDEP.DataSource = dt;


        }

        private void selItem()
        {
            m_XNAME = (string)gridViewWHSDEP.GetRowCellValue(gridViewWHSDEP.FocusedRowHandle, colXNAME);
            m_NOD_ID = (int)gridViewWHSDEP.GetRowCellValue(gridViewWHSDEP.FocusedRowHandle, colNOD_ID);
            m_NOD_XPOS = Convert.ToDecimal(gridViewWHSDEP.GetRowCellValue(gridViewWHSDEP.FocusedRowHandle, colNOD_XPOS));
            m_NOD_YPOS = Convert.ToDecimal(gridViewWHSDEP.GetRowCellValue(gridViewWHSDEP.FocusedRowHandle, colNOD_YPOS));

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            selItem();
        }

        private void gridWHSDEP_DoubleClick(object sender, EventArgs e)
        {
            selItem();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
}
