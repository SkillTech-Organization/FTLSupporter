using PMap;
using PMap.BLL;
using PMap.BO;
using PMap.Common;
using PMap.Common.Attrib;
using PMap.DB.Base;
using PMap.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FTLSupporter
{
    public class FTLInterface
    {
        public static List<FTLResult> FTLSupport(List<FTLTask> p_TaskList, List<FTLTruck> p_TruckList, string p_iniPath, string p_dbConf, bool p_cacheRoutes)
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
                    if ((trk.TruckTaskType == FTLTruck.eTruckTaskType.Planned || trk.TruckTaskType == FTLTruck.eTruckTaskType.Running) &&
                        trk.TPointCompleted > trk.CurrTPoints.Count - 1)
                    {
                        FTLResErrMsg msg = new FTLResErrMsg()
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
                        result.Add(itemRes);
                    }
                }


                if (result.Count == 0)
                {
                    PMapCommonVars.Instance.ConnectToDB();
                    bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
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

                    Dictionary<string, FTLRoute> dicRoutes = new Dictionary<string, FTLRoute>();

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


                        //3.nod ID-k meghatározása
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
                                if (trk.CurrTPoints[i].NOD_ID == 0)
                                    trk.CurrTPoints[i].NOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.CurrTPoints[i].Lat, trk.CurrTPoints[i + 1].Lng));
                            }
                        }

                        //4. Kiszámolandó útvonalak összegyűjtése
                        string sKey;
                        List<string> lstRZN_ID_LIST = clctsk.CalcTrucks.GroupBy(g => g.RZN_ID_LIST).Select(s => s.Key).ToList();
                        //4.1 Beosztandó szállítási feladatok összes pontjára minden szóbejöhető jármű zónalistájával
                        for (int i = 0; i < clctsk.Task.TPoints.Count - 1; i++)
                        {
                            foreach (string RZN_ID_LIST in lstRZN_ID_LIST)
                            {
                                sKey = clctsk.Task.TPoints[i].NOD_ID.ToString() + "," + clctsk.Task.TPoints[i + 1].NOD_ID.ToString() + "," + RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = clctsk.Task.TPoints[i].NOD_ID, toNOD_ID = clctsk.Task.TPoints[i + 1].NOD_ID, RZN_ID_LIST = RZN_ID_LIST });
                            }
                        }

                        foreach (FTLTruck trk in clctsk.CalcTrucks)
                        {
                            if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                            {
                                //4.2 futó túrapontok közötti távolságok
                                for (int i = 0; i < trk.CurrTPoints.Count - 1; i++)
                                {
                                    sKey = trk.CurrTPoints[i].NOD_ID.ToString() + "," + trk.CurrTPoints[i + 1].NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.CurrTPoints[i].NOD_ID, toNOD_ID = trk.CurrTPoints[i + 1].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }

                                //4.3 Utolsó teljesített túrapont --> aktuális járműpozíció
                                if (trk.TPointCompleted > 0)
                                {
                                    sKey = trk.CurrTPoints[trk.TPointCompleted - 1].NOD_ID.ToString() + "," + trk.NOD_ID_CURR.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.CurrTPoints[trk.TPointCompleted - 1].NOD_ID, toNOD_ID = trk.NOD_ID_CURR, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }

                                //4.4 Aktuális járműpozíció --> első nem teljesített túrapont
                                sKey = trk.NOD_ID_CURR.ToString() + "," + trk.CurrTPoints[trk.TPointCompleted].NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.NOD_ID_CURR, toNOD_ID = trk.CurrTPoints[trk.TPointCompleted].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                                //4.5 Teljesített utolsó túrapont -> beosztandó első túrapont (átállás)
                                sKey = trk.CurrTPoints.Last().NOD_ID.ToString() + "," + clctsk.Task.TPoints.First().NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.CurrTPoints.Last().NOD_ID, toNOD_ID = clctsk.Task.TPoints.First().NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                                //4.6 Beosztandó túrapont utolsó --> teljesítés legelső túrapont (csak NEM irányos túra esetén !!)
                                if (!trk.CurrIsOneWay)
                                {
                                    sKey = clctsk.Task.TPoints.Last().NOD_ID.ToString() + "," + trk.CurrTPoints.First().NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = clctsk.Task.TPoints.Last().NOD_ID, toNOD_ID = trk.CurrTPoints.First().NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }
                            }
                            else
                            {
                                /********************************************/
                                /* FTLTruck.eTruckTaskType.Available esetén */
                                /********************************************/

                                //4.5 Aktuális pozíció -> beosztandó első túrapont (átállás)
                                sKey = trk.NOD_ID_CURR.ToString() + "," + clctsk.Task.TPoints.First().NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = trk.NOD_ID_CURR, toNOD_ID = clctsk.Task.TPoints.First().NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                                //4.6 Beosztandó túrapont utolsó --> aktuális pozíció (csak NEM irányos túra esetén !!)
                                if (!trk.CurrIsOneWay)
                                {
                                    sKey = clctsk.Task.TPoints.Last().NOD_ID.ToString() + "," + trk.NOD_ID_CURR.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLRoute { fromNOD_ID = clctsk.Task.TPoints.Last().NOD_ID, toNOD_ID = trk.NOD_ID_CURR, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }

                            }
                        }
                    }

                    //5. legeneráljuk az összes futó túra befejezés és a szállítási feladat felrakás távolságot/menetidőt

                    List<FTLRoute> lstRoutes = dicRoutes.Values.ToList();       //ide fogjuk visszaírni az eredményt(is)
                    List<FTLRoute> lstCalcRoutes = new List<FTLRoute>();        //Számolandó útvonalak

                    //debug info
                    foreach (FTLRoute r in lstRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                        Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " zónák:" + r.RZN_ID_LIST);

                    // 5.1 ha cache-eljük az útvonalakat, megnézzük, kiolvassuk a meglévőket
                    //
                    if (p_cacheRoutes)
                    {
                        foreach (FTLRoute r in lstRoutes)
                        {
                            boRoute rt = route.GetRouteFromDB(r.RZN_ID_LIST, r.fromNOD_ID, r.toNOD_ID);
                            if (rt != null)
                            {
                                r.route = rt;
                                r.duration = bllPlanEdit.GetDuration(rt.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                            }
                            else
                            {
                                lstCalcRoutes.Add(r);
                            }
                        }
                        lstRoutes.RemoveAll(x => x.route == null);
                    }
                    else
                    {
                        lstCalcRoutes = lstRoutes;
                        lstRoutes.Clear();
                    }

                    if (lstCalcRoutes.Count > 0)
                    {
                        ProcessNotifyIcon ni = new ProcessNotifyIcon();
                        FTLCalcRouteProcess rp = new FTLCalcRouteProcess(ni, lstCalcRoutes, p_cacheRoutes);
                        rp.RunWait();
                    }

                    lstRoutes.AddRange(lstCalcRoutes);

                    //
                    //debug info
                    foreach (FTLRoute r in lstRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                        Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " zónák:" + r.RZN_ID_LIST + " dist:" + r.route.DST_DISTANCE.ToString() + " duration:" + r.duration.ToString());


                    //6.eredmény összeállítása
                    /*
                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        //6.1 Útvonalak kiolvasása
                        //6.1.1 From -> Curr 
                        FTLRoute r1 = lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_FROM && x.toNOD_ID == trk.NOD_ID_CURR && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                        //6.1.2 Curr -> To
                        FTLRoute r2 = lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_CURR && x.toNOD_ID == trk.NOD_ID_TO && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                        //6.1.3 to ->  taskFrom (ez átállás)
                        FTLRoute r3 = lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_TO && x.toNOD_ID == p_Task.NOD_ID_FROM && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                        //6.1.4 taskFrom -> taskTo (a beosztandó túra teljesítése)
                        FTLRoute r4 = lstRoutes.Where(x => x.fromNOD_ID == p_Task.NOD_ID_FROM && x.toNOD_ID == p_Task.NOD_ID_TO && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                        //6.1.5 Nem irányos túra esetén TaskTo --> From
                        FTLRoute r5 = lstRoutes.Where(x => x.fromNOD_ID == p_Task.NOD_ID_TO && x.toNOD_ID == trk.NOD_ID_FROM && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                    }
                    */

                        //Útvonalszámításhoz a pontok összeszedése
                        foreach (FTLTask tsk in p_TaskList)
                    {



                    }
                    //Eredményt a resultba

                }
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
                FTLResErrMsg rm = new FTLResErrMsg();
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
                        FTLResErrMsg msg = new FTLResErrMsg()
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
