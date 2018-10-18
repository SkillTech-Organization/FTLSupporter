using MPOrder.BLL;
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
    public class SendToNetMover : BaseLongProcess
    {
        public List<SendResult> Result { get; set; } = new List<SendResult>();
        private SQLServerAccess m_DB;
        private string m_CSVFile;
        private int m_PLN_ID;
        private string m_ExportFile;

        public SendToNetMover(BaseProgressDialog p_Form, string p_CSVFile, int p_PLN_ID, string p_ExportFile)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_CSVFile = p_CSVFile;
            m_PLN_ID = p_PLN_ID;
            m_ExportFile = p_ExportFile;
        }


        protected override void DoWork()
        {
            try
            {
                var bllMPOrderX = new bllMPOrder(m_DB);
                Result = bllMPOrderX.SendToNetMover(m_CSVFile, m_PLN_ID, m_ExportFile, ProcessForm);
            }
            catch
            {
                throw;
            }
        }
    }
}
