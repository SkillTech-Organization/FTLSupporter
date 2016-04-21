using PMap.BLL;
using PMap.Common;
using PMap.DB.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLInterface
    {
        public static List<FTLResult> FTLSupport(List<FTLTask> p_TaskList, List<FTLTruck> p_TruckList, string p_iniPath, string p_dbConf, bool p_logErr = false)
        {
            List<FTLResult> ret = new List<FTLResult>();

            try
            {

                SQLServerAccess DB;

                DB = new SQLServerAccess();
                DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                bllRoute route = new bllRoute(DB);


            }
            catch( Exception ex)
            {
                Util.ExceptionLog(ex);
                FTLResMsg rm = new FTLResMsg();
                rm.Field = "";
                rm.Message = ex.Message;
                if (ex.InnerException != null)
                    rm.Message += "\ninner exception:" + ex.InnerException.Message;
                rm.CallStack = ex.StackTrace;
            }
            return ret;

        }

    }
}
