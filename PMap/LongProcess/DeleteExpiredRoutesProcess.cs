﻿using PMapCore.BLL;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.LongProcess.Base;
using PMapCore.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.LongProcess
{
 

    public class DeleteExpiredRoutesProcess : BaseLongProcess
    {
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        public DeleteExpiredRoutesProcess()
            : base(new BaseSilngleProgressDialog(1, 3, PMapMessages.M_ROUTE_DELEXPIRED, false), System.Threading.ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

        }
        protected override void DoWork()
        {
            if (PMapIniParams.Instance.RoutesExpire > 0)
            {
                bllRoute route = new bllRoute(m_DB);
                this.ProcessForm.NextStep();
                route.DeleteOldDistances(PMapIniParams.Instance.RoutesExpire);
                this.ProcessForm.NextStep();
        //Egyelőre nem használjuk        m_DB.ExecuteNonQuery("DBCC SHRINKDATABASE("+m_DB.Conn.Database +", 20)");
                this.ProcessForm.NextStep();
            }
        }
    }

}