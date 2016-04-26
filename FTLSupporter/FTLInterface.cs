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

                bool bValid = true;

                //Paraméterek validálása
                      var List<FTLResult> ValidateObjList(List<object> p_list)
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

                foreach (FTLTask tsk in p_TaskList)
                {
                    List<ObjectValidator.ValidationError> tskErros = ObjectValidator.ValidateObject(tsk);
                    if (tskErros.Count != 0)
                    {
                        bValid = false;
                        foreach (var err in tskErros)
                        {
                            FTLResMsg msg = new FTLResMsg()
                            {
                                Field = err.Field,
                                Message = err.Message,
                                CallStack = ""
                            };

                            FTLResult itemRes = new FTLResult()
                            {
                                Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                                ObjectName = "FTLTask",
                                ItemID = tsk.TaskID,
                                Data = msg
                            };
                            result.Add(itemRes);
                        }

                        //Túrapontok validálása
                        foreach(FTLPoint pt in tsk.TPoints)
                        {

                        }

                    }
                }

                foreach (FTLTruck trk in p_TruckList)
                {
                    List<ObjectValidator.ValidationError> trkErros = ObjectValidator.ValidateObject(trk);
                    if (trkErros.Count != 0)
                    {
                        bValid = false;
                        foreach (var err in trkErros)
                        {
                            FTLResMsg msg = new FTLResMsg()
                            {
                                Field = err.Field,
                                Message = err.Message,
                                CallStack = ""
                            };



                            FTLResult itemRes = new FTLResult()
                            {
                                Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                                ObjectName = "FTLTruck",
                                ItemID = trk.TruckID,
                                Data = msg
                            };
                            result.Add(itemRes);
                        }
                    }
                }



                SQLServerAccess DB;

                DB = new SQLServerAccess();
                DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                bllRoute route = new bllRoute(DB);
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);








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
        
        private List<FTLResult> ValidateObjList( List<object>p_list)
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
