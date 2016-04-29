using GMap.NET;
using PMap;
using PMap.BLL;
using PMap.BO;
using PMap.Common;
using PMap.DB.Base;
using PMap.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public static class FTLInterfaceX
    {

        public static List<FTLResultX> FTLSupport(FTLTaskX p_Task, List<FTLTruckX> p_TruckList, string p_iniPath, string p_dbConf, bool p_logErr = false)
        {
            string sErrLog = "";

            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);

            bool bValid = true;
            List<FTLResultX> result = new List<FTLResultX>();

            //Paraméterek validálása
            List<ObjectValidator.ValidationError> tskErros = ObjectValidator.ValidateObject(p_Task);
            if (tskErros.Count != 0)
            {
                bValid = false;
                foreach (var err in tskErros)
                {
                    FTLResultX itemRes = new FTLResultX()
                    {
                        ObjectName = "TASK",
                        ItemNo = 0,
                        Field = err.Field,
                        Status = FTLResultX.FTLResultStatus.VALIDATIONERROR,
                        Message = err.Message

                    };
                    result.Add(itemRes);
                }
            }
            foreach (FTLTruckX trk in p_TruckList)
            {
                List<ObjectValidator.ValidationError> trkErros = ObjectValidator.ValidateObject(trk);
                if (trkErros.Count != 0)
                {
                    bValid = false;
                    int item = 0;
                    foreach (var err in trkErros)
                    {
                        FTLResultX itemRes = new FTLResultX()
                        {
                            ObjectName = "TRUCK",
                            ItemNo = item++,
                            Field = err.Field,
                            Status = FTLResultX.FTLResultStatus.VALIDATIONERROR,
                            Message = err.Message

                        };
                        result.Add(itemRes);
                    }
                }
            }



            if (bValid)
            {
                SQLServerAccess DB;

                DB = new SQLServerAccess();
                DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
                bllRoute route = new bllRoute(DB);
                
                
                //1. Szóbajöhető járművek meghatározása
                //  1.1: Ha ki van töltve, mely típusú járművek szállíthatják, a megfelelő típusú járművek leválogatása
                //  1.2: Szállíthatja-e jármű az árutípust?
                //  1.3: Járműkapacitás megfelelő ?
                //  1.4: A felrakás megkezdése előtt rendelkezésre áll-e 
                //       1.4.1 : Elérhető a rakodás megkezdésekor
                //       1.4.2 : Nincs a szállítási feladat megkezdésekor futó szállítási feladata
                //
                    List<FTLTruckX> lstTrucks = p_TruckList.Where(x => (p_Task.TruckTypes.Length >= 0 ?  ("," + p_Task.TruckTypes + ",").IndexOf("," + x.TruckType + ",") >= 0 : true) &&
                                                                ("," + x.CargoTypes + ",").IndexOf("," + p_Task.CargoType + ",") >= 0 &&
                                                                x.CapacityWeight >= p_Task.Weight &&
                                                                (x.TruckTaskType == FTLTruckX.eTruckTaskTypeX.Available ? x.TimeCurr <= p_Task.CloseFrom : 
                                                                (x.TruckTaskType == FTLTruckX.eTruckTaskTypeX.Planned ? x.TimeUnload <= p_Task.CloseFrom :
                                                                x.TimeCurr <= p_Task.CloseFrom))).ToList();
                    if (p_logErr)
                    {
                        //Hibalista generálása
                        //
                        List<FTLTruckX> lstTrucksErr = p_TruckList.Where(x => !(p_Task.TruckTypes.Length >= 0 ? ("," + p_Task.TruckTypes + ",").IndexOf("," + x.TruckType + ",") >= 0 : true)).ToList();
                        if (lstTrucksErr.Count > 0)
                            sErrLog += "Járműtípus miatt nem teljesítheti a túrát:" + string.Join(",", lstTrucksErr.Select(x => x.RegNo).ToArray()) + Environment.NewLine;

                        lstTrucksErr = p_TruckList.Where(x => !(("," + x.CargoTypes + ",").IndexOf("," + p_Task.CargoType + ",") >= 0)).ToList();
                        if (lstTrucksErr.Count > 0)
                            sErrLog += "Árutípus miatt nem teljesítheti a túrát:" + string.Join(",", lstTrucksErr.Select(x => x.RegNo).ToArray()) + Environment.NewLine;

                        lstTrucksErr = p_TruckList.Where(x => !(x.CapacityWeight >= p_Task.Weight)).ToList();
                        if (lstTrucksErr.Count > 0)
                            sErrLog += "Kapacitás miatt nem teljesítheti a túrát:" + string.Join(",", lstTrucksErr.Select(x => x.RegNo).ToArray()) + Environment.NewLine;

                        lstTrucksErr = p_TruckList.Where(x => !(x.TruckTaskType == FTLTruckX.eTruckTaskTypeX.Available ? x.TimeCurr <= p_Task.CloseFrom :
                                                                    (x.TruckTaskType == FTLTruckX.eTruckTaskTypeX.Planned ? x.TimeUnload <= p_Task.CloseFrom :
                                                                    x.TimeCurr <= p_Task.CloseFrom))).ToList();
                        if (lstTrucksErr.Count > 0)
                            sErrLog += "A túra teljesítésekor nem érhető el:" + string.Join(",", lstTrucksErr.Select(x => x.RegNo).ToArray()) + Environment.NewLine;
                    }


                foreach (FTLTruckX trk in lstTrucks)
                {
                    trk.RZN_ID_LIST = route.GetRestZonesByRST_ID(trk.RST_ID);
                }
                

                //3.nod ID-k meghatározása
                List<FTLRoute> lstRoutes = new List<FTLRoute>();

                //  3.1:Szállítási feladatok
                p_Task.NOD_ID_FROM = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_Task.LatFrom, p_Task.LngFrom));
                p_Task.NOD_ID_TO = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_Task.LatTo, p_Task.LngTo));

                foreach (string RZN_ID_LIST in lstTrucks.GroupBy(g => g.RZN_ID_LIST).Select(s => s.Key).ToList())
                {
                    lstRoutes.Add(new FTLRoute { fromNOD_ID = p_Task.NOD_ID_FROM, toNOD_ID = p_Task.NOD_ID_TO, RZN_ID_LIST = RZN_ID_LIST });
                }
                //            lstNOD_ID.Add(new int[] { p_Task.NOD_ID_FROM, p_Task.NOD_ID_TO});

                //  3.2:Futó túrainformációk
                foreach (FTLTruckX trk in lstTrucks)
                {
                    trk.NOD_ID_FROM = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.LatFrom, trk.LngFrom));
                    trk.NOD_ID_CURR = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.LatCurr, trk.LngCurr));
                    trk.NOD_ID_TO = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.LatTo, trk.LngTo));


                    //3.2.1. Számítandó útvonalak gyűjése
                    //3.2.1.1. From -> Curr (nem kell)
                    if( lstRoutes.Where( x=>x.fromNOD_ID==trk.NOD_ID_FROM && x.toNOD_ID==trk.NOD_ID_CURR && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault() == null)
                        lstRoutes.Add(new FTLRoute { fromNOD_ID =  trk.NOD_ID_FROM, toNOD_ID =  trk.NOD_ID_CURR, RZN_ID_LIST = trk.RZN_ID_LIST });
                    //3.2.1.2. Curr -> To
                    if (lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_CURR && x.toNOD_ID == trk.NOD_ID_TO && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault() == null)
                        lstRoutes.Add(new FTLRoute { fromNOD_ID = trk.NOD_ID_CURR, toNOD_ID = trk.NOD_ID_TO, RZN_ID_LIST = trk.RZN_ID_LIST });
                    //3.2.1.3. to ->  taskFrom (ez átállás)
                    if (lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_TO && x.toNOD_ID == p_Task.NOD_ID_FROM && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault() == null)
                        lstRoutes.Add(new FTLRoute { fromNOD_ID = trk.NOD_ID_TO, toNOD_ID = p_Task.NOD_ID_FROM, RZN_ID_LIST = trk.RZN_ID_LIST });
                    //3.2.1.4. taskFrom -> taskTo (a beosztandó túra teljesítése)
                    if (lstRoutes.Where(x => x.fromNOD_ID == p_Task.NOD_ID_FROM && x.toNOD_ID == p_Task.NOD_ID_TO && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault() == null)
                        lstRoutes.Add(new FTLRoute { fromNOD_ID = p_Task.NOD_ID_FROM, toNOD_ID = p_Task.NOD_ID_TO, RZN_ID_LIST = trk.RZN_ID_LIST });
                    //3.2.1.5. Nem irányos túra esetén TaskTo --> From
                    if (!trk.IsOneWay && lstRoutes.Where(x => x.toNOD_ID == p_Task.NOD_ID_TO && x.toNOD_ID == trk.NOD_ID_FROM && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault() == null)
                        lstRoutes.Add(new FTLRoute { fromNOD_ID = p_Task.NOD_ID_TO, toNOD_ID = trk.NOD_ID_FROM, RZN_ID_LIST = trk.RZN_ID_LIST });

                }

  
                foreach( FTLRoute r in lstRoutes.OrderBy( o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                    Console.WriteLine( r.fromNOD_ID.ToString() + " -> " +  r.toNOD_ID.ToString() + " " + r.RZN_ID_LIST);

                //4. legeneráljuk az összes futó túra befejezés és a szállítási feladat felrakás távolságot/menetidőt
                ProcessNotifyIcon ni =  new ProcessNotifyIcon();
                FTLCalcRouteProcess rp = new FTLCalcRouteProcess(ni, lstRoutes, lstTrucks);
                rp.RunWait();

                foreach (FTLRoute r in lstRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                {
                    Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " " + r.RZN_ID_LIST + " dist:" + r.route.DST_DISTANCE.ToString() + " duration:" + r.duration.ToString());

                }
  

                //5.Létrehozzuk az eredmény objektumokat. Az FTLTruck adatokhoz hozzávesszük a túrát és feltöltjük a számítás eredményével
                //
                List<FTLCalcTourX> lstCalcTours = new List<FTLCalcTourX>();
                foreach (FTLTruckX trk in lstTrucks)
                {
                    FTLCalcTourX clc = new FTLCalcTourX()
                    {
                        RegNo = trk.RegNo,
                        CurrTaskID = trk.TaskID,
                        TaskID = p_Task.TaskID
                    };

                    //5.1 Útvonalak kiolvasása
                    //5.1.1 From -> Curr 
                    FTLRoute r1 = lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_FROM && x.toNOD_ID == trk.NOD_ID_CURR && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                    //5.1.2 Curr -> To
                    FTLRoute r2 = lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_CURR && x.toNOD_ID == trk.NOD_ID_TO && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                    //5.1.3 to ->  taskFrom (ez átállás)
                    FTLRoute r3 = lstRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_TO && x.toNOD_ID == p_Task.NOD_ID_FROM && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                    //5.1.4 taskFrom -> taskTo (a beosztandó túra teljesítése)
                    FTLRoute r4 = lstRoutes.Where(x => x.fromNOD_ID == p_Task.NOD_ID_FROM && x.toNOD_ID == p_Task.NOD_ID_TO && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                    //5.1.5 Nem irányos túra esetén TaskTo --> From
                    FTLRoute r5 = lstRoutes.Where(x => x.fromNOD_ID == p_Task.NOD_ID_TO && x.toNOD_ID == trk.NOD_ID_FROM && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();


                    string sLastETLCode = "";

                    //From -> Curr
                    if (r1 != null)
                    {
                        clc.T1Km = r1.route.DST_DISTANCE / 1000;
                        clc.T1Cost = trk.KMCost * r1.route.DST_DISTANCE / 1000;
                        clc.T1Toll = bllPlanEdit.GetToll(r1.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                        if (trk.TruckTaskType != FTLTruckX.eTruckTaskTypeX.Available)
                            clc.T1Duration = (trk.TimeCurr - trk.TimeFrom).TotalMinutes;
                    }

                    //Curr -> To
                    if (r2 != null)
                    {
                        clc.T1Km += r2.route.DST_DISTANCE / 1000;
                        clc.T1Cost += trk.KMCost * r2.route.DST_DISTANCE / 1000;
                        clc.T1Toll += bllPlanEdit.GetToll(r2.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                        clc.T1Duration += r2.duration;
                        clc.TimeCurrTourFinish = trk.TimeCurr.AddMinutes(r2.duration + trk.CurrUnloadDuration);
                    }

                    //to ->  taskFrom (ez átállás)
                    if (r3 != null)
                    {
                        clc.RelKm += r3.route.DST_DISTANCE / 1000;
                        clc.RelCost += trk.RelocateCost * r3.route.DST_DISTANCE / 1000;
                        clc.RelToll += bllPlanEdit.GetToll(r3.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                        clc.RelDuration += r3.duration;

                        //felrakás kezdete/vége beállítás
                        clc.TimeStartFrom = clc.TimeCurrTourFinish.AddMinutes(r3.duration);
                        clc.TimeEndFrom = clc.TimeStartFrom.AddMinutes(p_Task.LoadDuration);
                    }

                    //taskFrom --> taskTo  (beosztandó túra teljesítése)
                    if (r4 != null)
                    {
                        clc.T2Km = r4.route.DST_DISTANCE / 1000;
                        clc.T2Cost = trk.KMCost * r4.route.DST_DISTANCE / 1000;
                        clc.T2Toll = bllPlanEdit.GetToll(r4.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                        clc.T2Duration += r4.duration;

                        //II.túra megérkezés és befejezés számítása
                        clc.TimeStartTo = clc.TimeEndFrom.AddMinutes(r4.duration);
                        clc.TimeEndTo = clc.TimeStartTo.AddMinutes(p_Task.UnLoadDuration);
                        clc.TimeComplete = clc.TimeEndTo;
                    }

                    // TimeCurrFinish feltöltése 

                    if (!trk.IsOneWay && r5 != null)
                    {
                        clc.RetKm = r5.route.DST_DISTANCE / 1000;
                        clc.RetCost = trk.KMCost * r5.route.DST_DISTANCE / 1000;
                        clc.RetToll = bllPlanEdit.GetToll(r5.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                        clc.RetDuration += r5.duration;

                        clc.TimeComplete = clc.TimeEndTo.AddMinutes(r5.duration);
                    }
                    lstCalcTours.Add(clc);

                }

                //Nem teljesíjtések kiszűrése
                //
                //1. max tejesítési időbe nem fér a túra
                //
                var linqDuration = from clcx in lstCalcTours
                             join trkx in lstTrucks on new { clcx.RegNo } equals new { trkx.RegNo }
                             where trkx.MaxDuration == 0 || clcx.FullDuration <= trkx.MaxDuration
                             select clcx;

                if (p_logErr)
                {
                    var linqDurationErr = from clcx in lstCalcTours
                                       join trkx in lstTrucks on new { clcx.RegNo } equals new { trkx.RegNo }
                                       where !(trkx.MaxDuration == 0 || clcx.FullDuration <= trkx.MaxDuration)
                                       select clcx;

                    if (linqDurationErr.ToList().Count > 0)
                        sErrLog += "Max tejesítési időbe nem fér a túra:" + string.Join(",", linqDurationErr.Select(x => x.RegNo+"("+x.FullDuration.ToString()+")").ToArray()) + Environment.NewLine;

                }

                lstCalcTours = linqDuration.ToList();

                //2. max teljesítési KM-be nem fér a túra
                var linqKM = from clcx in lstCalcTours
                             join trkx in lstTrucks on new {clcx.RegNo} equals new {trkx.RegNo}
                             where trkx.MaxKM == 0 || clcx.FullKM <= trkx.MaxKM
                             select clcx;

                if (p_logErr)
                {
                    var linqKMErr = from clcx in lstCalcTours
                                 join trkx in lstTrucks on new { clcx.RegNo } equals new { trkx.RegNo }
                                 where !(trkx.MaxKM == 0 || clcx.FullKM <= trkx.MaxKM)
                                 select clcx;
                    if (linqKMErr.ToList().Count > 0)
                        sErrLog += "Max tejesítési KM-be nem fér a túra:" + string.Join(",", linqKMErr.Select(x => x.RegNo + "(" + x.FullKM.ToString() + ")").ToArray()) + Environment.NewLine;

                }

                lstCalcTours = linqKM.ToList();


                //3. nyitva tartási időre nem ér oda
                var linqOpen = from clcx in lstCalcTours
                               where p_Task.OpenFrom <= clcx.TimeStartFrom && p_Task.CloseFrom >= clcx.TimeStartFrom &&
                               p_Task.OpenTo <= clcx.TimeStartTo && p_Task.CloseTo >= clcx.TimeStartTo
                             select clcx;


                if (p_logErr)
                {
                    var linqOpenErr = from clcx in lstCalcTours
                                   where !(p_Task.OpenFrom <= clcx.TimeStartFrom && p_Task.CloseFrom >= clcx.TimeStartFrom &&
                                   p_Task.OpenTo <= clcx.TimeStartTo && p_Task.CloseTo >= clcx.TimeStartTo)
                                   select clcx;
                    if (linqOpenErr.ToList().Count > 0)
                        sErrLog += "Nyitva tartási időre nem ér oda:" + string.Join(",", linqOpenErr.Select(x => x.RegNo + "(" + x.TimeStartFrom.ToString() + "-" + x.TimeStartTo.ToString() + ")").ToArray()) + Environment.NewLine;

                }

                lstCalcTours = linqOpen.ToList();
                
/*
 *                   var linqTours = (from o in routesNearBy
                                     orderby o.ItemID
                                     where o.ItemID.CompareTo(m_EditedRoute.ItemID) > 0
                                     select o);
   var query = from person in people
                join pet in pets on person equals pet.Owner
                select new { OwnerName = person.FirstName, PetName = pet.Name };

 * 
 */




                //Költség fordított sorrendben berendezzük
                int rank = 0;
                List<FTLCalcTourX> lstCalcRes = lstCalcTours.OrderBy(x => x.AdditionalCost).Select(x => x).ToList();
                lstCalcRes.ForEach(r => r.Rank = rank++);

        

                 //Eredmény összeállítása
                FTLResultX resOK = new FTLResultX()
                {
                    ObjectName = "RESULT",
                    ItemNo = 0,
                    Field = "",
                    Status = FTLResultX.FTLResultStatus.OK,
                    Message = "",
                    Data = lstCalcRes

                };



                result.Add(resOK);
            }

            return result;
        }


    }
}
