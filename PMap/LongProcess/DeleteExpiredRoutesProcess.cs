using PMapCore.BLL;
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
            : base(new BaseSilngleProgressDialog(1, 2, PMapMessages.M_OPT_LOADMAPDATA, false), System.Threading.ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

        }
        protected override void DoWork()
        {
            bllRoute route = new bllRoute(m_DB);
            route.DeleteOldDistances(PMapIniParams.Instance.RoutesExpire);
            this.ProcessForm.NextStep();


        }
    }

}
