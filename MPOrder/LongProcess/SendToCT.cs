using MPOrder.BLL;
using MPOrder.BO;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPOrder.LongProcess
{
    public class SendToCT : BaseLongProcess
    {
        public List<SendResult> Result { get; set; } = new List<SendResult>();

        private SQLServerAccess m_DB;
        List<boMPOrderF> m_data = new List<boMPOrderF>();

        public SendToCT(BaseProgressDialog p_Form, List<boMPOrderF> p_data)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_data = p_data;
        }
        protected override void DoWork()
        {
            try
            {
                var bllMPOrderX = new bllMPOrder(m_DB);
                Result = bllMPOrderX.SendToCT(m_data, ProcessForm);
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
        }
    }
}
