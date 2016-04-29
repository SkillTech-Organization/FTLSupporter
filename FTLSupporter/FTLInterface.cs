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
        public static List<FTLResult> FTLSupport(List<FTLTask> p_TaskList, List<FTLTruck> p_TruckList, string p_iniPath, string p_dbConf)
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
                {
                    result.AddRange(ValidateObjList<FTLPoint>(trk.CurrTPoints));

                    //Teljesített túrapont ellenőrzés
                    if( trk.TPointCompleted > trk.CurrTPoints.Count-1)
                    {
                         FTLResMsg msg = new FTLResMsg()
                        {
                            Field = "TPointCompleted",
                            Message = FTLMessages.E_TRKWRONGCOMPLETED,
                            CallStack = ""
                        };

                        FTLResult itemRes = new FTLResult()
                        {
                            Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                            ObjectName = trk.GetType().Name,
                            ItemID = trk.TruckID.ToString(),
                            Data = msg
                        };
                        result.Add( itemRes);
                    }
                }
                

                if (result.Count == 0)
                {
                    SQLServerAccess DB;

                    DB = new SQLServerAccess();
                    DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                    bllRoute route = new bllRoute(DB);
                    PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);

                    //0.Visszatérési érték felépítése
                    //
                    List<FTLCalcTask> tskResult = new List<FTLCalcTask>();
                    foreach (FTLTask tsk in p_TaskList)
                    {
                        var tskr = new FTLCalcTask() { Task = tsk };
                        tskResult.Add(tskr);
                    }


                    //1. Előkészítés:
                    
                    //1.1 A járművek zónalistájának összeállítása
                    foreach (FTLTruck trk in p_TruckList)
                    {
                        trk.RZN_ID_LIST = route.GetRestZonesByRST_ID(trk.RST_ID);
                    }
                    

                    //2. Szóbajöhető járművek meghatározása
                    //
                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        //  2.1: Ha ki van töltve, mely típusú járművek szállíthatják, a megfelelő típusú járművek leválogatása
                        //  2.2: Szállíthatja-e jármű az árutípust?
                        //  2.3: Járműkapacitás megfelelő ?
                        //  2.4: Az jármű pillanatnyi időpontja az összes túrapont zárása előtti-e
                        //
                        clctsk.CalcTrucks = p_TruckList.Where(x => (clctsk.Task.TruckTypes.Length >= 0 ? ("," + clctsk.Task.TruckTypes + ",").IndexOf("," + x.TruckType + ",") >= 0 : true) &&
                                                                    ("," + x.CargoTypes + ",").IndexOf("," + clctsk.Task.CargoType + ",") >= 0 &&
                                                                    x.Capacity >= clctsk.Task.Weight &&
                                                                    clctsk.Task.TPoints.Where(p => p.Close > x.CurrTime).FirstOrDefault() != null).ToList();
                        //Hibalista generálása
                        //
                        List<FTLTruck> lstTrucksErr = p_TruckList.Where(x => !(clctsk.Task.TruckTypes.Length >= 0 ? ("," + clctsk.Task.TruckTypes + ",").IndexOf("," + x.TruckType + ",") >= 0 : true)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_TRKTYPE + string.Join(",", lstTrucksErr.Select(x => x.TruckID).ToArray()));

                        lstTrucksErr = p_TruckList.Where(x => !(("," + x.CargoTypes + ",").IndexOf("," + clctsk.Task.CargoType + ",") >= 0)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_TRKCARGOTYPE + string.Join(",", lstTrucksErr.Select(x => x.TruckID).ToArray()));

                        lstTrucksErr = p_TruckList.Where(x => !(x.Capacity >= clctsk.Task.Weight)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_TRKCAPACITY + string.Join(",", lstTrucksErr.Select(x => x.TruckID).ToArray()));

                        lstTrucksErr = p_TruckList.Where(x => !(clctsk.Task.TPoints.Where(p => p.Close > x.CurrTime).FirstOrDefault() != null)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_TRKCLOSETP + string.Join(",", lstTrucksErr.Select(x => x.TruckID).ToArray()));
                    }

                    //3.nod ID-k meghatározása
                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        foreach (FTLPoint tpoint in clctsk.Task.TPoints)
                        {
                            if (tpoint.NOD_ID == 0)
                                tpoint.NOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(tpoint.Lat, tpoint.Lng));
                        }
                        foreach (FTLTruck trk in clctsk.CalcTrucks)
                        {
                            if (trk.NOD_ID_CURR == 0)
                                trk.NOD_ID_CURR = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.CurrLat, trk.CurrLng));
                        
                         //4.3 nem teljesített túrapontok közötti távolságok
                        for (int i = trk.TPointCompleted; i < trk.CurrTPoints.Count - 1; i++)
                        {
                                if( trk.CurrTPoints[i].NOD_ID == 0)
                                    trk.CurrTPoints[i].NOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.CurrTPoints[i].Lat, trk.CurrTPoints[i+1].Lng));
                            }
                        }
                    }

                    //4. Kiszámolandó útvonalak összegyűjtése
                    Dictionary<string, FTLRoute> dicRoutes = new Dictionary<string, FTLRoute>();
                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        string sKey;
                        List<string> lstRZN_ID_LIST = clctsk.CalcTrucks.GroupBy(g => g.RZN_ID_LIST).Select(s => s.Key).ToList();
                        //4.1 Beosztandó szállítási feladatok összes pontjára minden szóbejöhető jármű zónalistájával
                        for (int i = 0; i < clctsk.Task.TPoints.Count - 1; i++)
                        {
                            foreach (string RZN_ID_LIST in lstRZN_ID_LIST )
                            {
                                sKey = clctsk.Task.TPoints[i].NOD_ID.ToString()+ "," + clctsk.Task.TPoints[i+1].NOD_ID.ToString() +","+RZN_ID_LIST;
                                if(! dicRoutes.ContainsKey( sKey))
                                    dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = clctsk.Task.TPoints[i].NOD_ID, toNOD_ID = clctsk.Task.TPoints[i+1].NOD_ID, RZN_ID_LIST = RZN_ID_LIST });
                            }
                        }

                        foreach (FTLTruck trk in clctsk.CalcTrucks)
                        {

                            //4.2 Aktuális járműpozíció --> első nem teljesített túrapont
                                sKey = trk.NOD_ID_CURR.ToString()+ "," +  trk.CurrTPoints[trk.TPointCompleted].NOD_ID.ToString() +","+trk.RZN_ID_LIST;
                                if(! dicRoutes.ContainsKey( sKey))
                                dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.NOD_ID_CURR, toNOD_ID = trk.CurrTPoints[trk.TPointCompleted].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                            //4.3 nem teljesített túrapontok közötti távolságok
                            for (int i = trk.TPointCompleted; i < trk.CurrTPoints.Count - 1; i++)
                            {
                                sKey = trk.CurrTPoints[i].NOD_ID.ToString() + "," + trk.CurrTPoints[i + 1].NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.CurrTPoints[i].NOD_ID, toNOD_ID = trk.CurrTPoints[i+1].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });
                            }

                            //4.4 Teljesített utolsó túrapont -> beosztandó első túrapont (átállás)
                            sKey = trk.CurrTPoints.Last().NOD_ID.ToString() + "," + clctsk.Task.TPoints.First().NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                            if (!dicRoutes.ContainsKey(sKey))
                                dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.CurrTPoints.Last().NOD_ID, toNOD_ID = trk.CurrTPoints[i + 1].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });




                        }




                        foreach (FTLTruck trk in clctsk.CalcTrucks)
                        {
                            if (trk.NOD_ID_CURR == 0)
                                trk.NOD_ID_CURR = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.CurrLat, trk.CurrLng));
                            foreach (FTLPoint tp in trk.CurrTPoints)
                            {
                                if (tp.NOD_ID == 0)
                                    tp.NOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(tp.Lat, tp.Lng));
                            }
                        }
                    }




                    
                    //  3.1:Szállítási feladatok
                    p_Task.NOD_ID_FROM = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_Task.LatFrom, p_Task.LngFrom));
                    p_Task.NOD_ID_TO = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_Task.LatTo, p_Task.LngTo));

                    foreach (string RZN_ID_LIST in lstTrucks.GroupBy(g => g.RZN_ID_LIST).Select(s => s.Key).ToList())
                    {
                        dicRoutes.Add(new FTLRouteX { fromNOD_ID = p_Task.NOD_ID_FROM, toNOD_ID = p_Task.NOD_ID_TO, RZN_ID_LIST = RZN_ID_LIST });
                    }


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
