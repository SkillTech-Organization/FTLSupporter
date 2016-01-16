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
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        public InitRouteDataProcess()
            : base(new BaseSilngleProgressDialog(1, 4, PMapMessages.M_OPT_LOADMAPDATA, false), PMapIniParams.Instance.InitRouteDataProcess)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

        }

        protected override void DoWork()
        {
            RouteData.Instance.Init(m_DB, this.ProcessForm);

        }
    }

}
