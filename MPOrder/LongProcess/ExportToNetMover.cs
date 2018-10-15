using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPOrder.LongProcess
{
    public class ExportToNetMover : BaseLongProcess
    {
        private SQLServerAccess m_DB;
        public ExportToNetMover(BaseProgressDialog p_Form, int p_PLN_ID)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
     
        }
    }
}
