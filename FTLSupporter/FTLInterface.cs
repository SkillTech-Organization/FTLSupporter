using PMap.BLL;
using PMap.Common;
using PMap.Common.Attrib;
using PMap.DB.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FTLSupporter
{
    public class FTLInterface
    {
        public static List<FTLResult> FTLSupport(List<FTLTask> p_TaskList, List<FTLTruck> p_TruckList, string p_iniPath, string p_dbConf, bool p_logErr = false)
        {
            List<FTLResult> result = new List<FTLResult>();

            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);


                //Paraméterek validálása
                result.AddRange(ValidateObjList<FTLTask>(p_TaskList));
                foreach (FTLTask tsk in p_TaskList)
                    result.AddRange(ValidateObjList<FTLPoint>(tsk.TPoints));

                result.AddRange(ValidateObjList<FTLTruck>(p_TruckList));
                foreach (FTLTruck trk in p_TruckList)
                    result.AddRange(ValidateObjList<FTLPoint>(trk.CurrTPoints));




                if (result.Count == 0)
                {
                    SQLServerAccess DB;

                    DB = new SQLServerAccess();
                    DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                    bllRoute route = new bllRoute(DB);
                    PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);

                    //Útvonalszámításhoz a pontok összeszedése
                    foreach (FTLTask tsk in p_TaskList)
                    {



                    }

                }


            }
            catch ( Exception ex)
            {
                Util.ExceptionLog(ex);
                FTLResMsg rm = new FTLResMsg();
                rm.Field = "";
                rm.Message = ex.Message;
                if (ex.InnerException != null)
                    rm.Message += "\ninner exception:" + ex.InnerException.Message;
                rm.CallStack = ex.StackTrace;

                FTLResult res = new FTLResult()
                {
                    Status = FTLResult.FTLResultStatus.EXCEPTION,
                    ObjectName = "",
                    ItemID = "",
                    Data = rm

                };
                result.Add(res);
            }
            return result;

        }
        
        private static List<FTLResult> ValidateObjList<T>( List<T>p_list)
        {
            List<FTLResult> result = new List<FTLResult>();
            foreach (object item in p_list)
            {
                List<ObjectValidator.ValidationError> tskErros = ObjectValidator.ValidateObject(item);
                if (tskErros.Count != 0)
                {
                    foreach (var err in tskErros)
                    {
                        FTLResMsg msg = new FTLResMsg()
                        {
                            Field = err.Field,
                            Message = err.Message,
                            CallStack = ""
                        };

                        PropertyInfo ItemIDProp = item.GetType().GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(ItemIDAttr))).FirstOrDefault();

                        FTLResult itemRes = new FTLResult()
                        {
                            Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                            ObjectName = item.GetType().Name,
                            ItemID = ItemIDProp != null ? item.GetType().GetProperty(ItemIDProp.Name).GetValue(item, null).ToString() : "???",
                            Data = msg
                        };
                        result.Add(itemRes);
                    }
                }
            }

            return result;

        }


    }
}
