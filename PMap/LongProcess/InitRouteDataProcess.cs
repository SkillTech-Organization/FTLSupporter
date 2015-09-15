using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.LongProcess.Base;
using PMap.DB.Base;
using PMap;
using PMap.Route;
using PMap.Localize;
using PMap.Common;

namespace PMap.LongProcess
{
    

    public class InitRouteDataProcess : BaseLongProcess
    {
        private SQLServerConnect m_conn = null;                 //A multithread miatt saját connection kell
        public InitRouteDataProcess()
            : base(new BaseSilngleProgressDialog(1, 4, PMapMessages.M_OPT_LOADMAPDATA, false), PMapIniParams.Instance.InitRouteDataProcess)
        {
            m_conn = new PMap.DB.Base.SQLServerConnect(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_conn.ConnectDB();

        }

        protected override void DoWork()
        {
            RouteData.Instance.Init(m_conn, this.ProcessForm);

        }
    }

}
