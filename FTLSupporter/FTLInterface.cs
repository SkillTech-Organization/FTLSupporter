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
                PMapCommonVars.Instance.ConnectToDB();
                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);


                //Paraméterek validálása
                result.AddRange(ValidateObjList<FTLTask>(p_TaskList));
                foreach (FTLTask tsk in p_TaskList)
                    result.AddRange(ValidateObjList<FTLPoint>(tsk.TPoints));


                result.AddRange(ValidateObjList<FTLTruck>(p_TruckList));
                foreach (FTLTruck trk in p_TruckList)
                {
                    result.AddRange(ValidateObjList<FTLPoint>(trk.CurrTPoints));

                }

                //Validálás, koordináta feloldás: beosztandó szállítási feladat
                //
                foreach (FTLTask tsk in p_TaskList)
                {
                    //Koordináta feloldás és ellenőrzés
                    foreach (FTLPoint pt in tsk.TPoints)
                    {
                        pt.NOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(pt.Lat, pt.Lng));
                        if (pt.NOD_ID == 0)
                        {
                            result.Add(getValidationError(pt, "Lat,Lng", FTLMessages.E_WRONGCOORD));
                        }
                    }
                }


                //Validálás, koordináta feloldás:jármű aktuális pozíció, szállítási feladat
                //
                foreach (FTLTruck trk in p_TruckList)
                {

                    //Teljesített túrapont ellenőrzés
                    if ((trk.TruckTaskType == FTLTruck.eTruckTaskType.Planned || trk.TruckTaskType == FTLTruck.eTruckTaskType.Running) &&
                        trk.TPointCompleted > trk.CurrTPoints.Count - 1)
                    {
                        result.Add(getValidationError(trk, "TPointCompleted", FTLMessages.E_TRKWRONGCOMPLETED));
                    }

                    trk.NOD_ID_CURR = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(trk.CurrLat, trk.CurrLng));
                    if (trk.NOD_ID_CURR == 0)
                        result.Add(getValidationError(trk, "CurrLat,CurrLng", FTLMessages.E_WRONGCOORD));

                    //Koordináta feloldás és ellenőrzés
                    foreach (FTLPoint pt in trk.CurrTPoints)
                    {
                        pt.NOD_ID = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(pt.Lat, pt.Lng));
                        if (pt.NOD_ID == 0)
                        {
                            result.Add(getValidationError(pt, "Lat,Lng", FTLMessages.E_WRONGCOORD));
                        }
                    }
                }


                if (result.Count == 0)
                {
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

                    Dictionary<string, FTLPMapRoute> dicRoutes = new Dictionary<string, FTLPMapRoute>();

                    /************************************************************************************/
                    /*Járművek előszűrése, NOD_ID meghatározás és visszatérési érték objektum felépítése*/
                    /************************************************************************************/

                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        //2. Szóbajöhető járművek meghatározása
                        //
                        //  2.1: Ha ki van töltve, mely típusú járművek szállíthatják, a megfelelő típusú járművek leválogatása
                        //  2.2: Szállíthatja-e jármű az árutípust?
                        //  2.3: Járműkapacitás megfelelő ?
                        //  2.4: Az jármű pillanatnyi időpontja az összes túrapont zárása előtti-e
                        //
                        List<FTLTruck> CalcTrucks = p_TruckList.Where(x => (clctsk.Task.TruckTypes.Length >= 0 ? ("," + clctsk.Task.TruckTypes + ",").IndexOf("," + x.TruckType + ",") >= 0 : true) &&
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


                        //4. Kiszámolandó útvonalak összegyűjtése
                        string sKey;
                        List<string> lstRZN_ID_LIST = CalcTrucks.GroupBy(g => g.RZN_ID_LIST).Select(s => s.Key).ToList();
                        //4.1 Beosztandó szállítási feladatok összes pontjára minden szóbejöhető jármű zónalistájával
                        for (int i = 0; i < clctsk.Task.TPoints.Count - 1; i++)
                        {
                            foreach (string RZN_ID_LIST in lstRZN_ID_LIST)
                            {
                                sKey = clctsk.Task.TPoints[i].NOD_ID.ToString() + "," + clctsk.Task.TPoints[i + 1].NOD_ID.ToString() + "," + RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = clctsk.Task.TPoints[i].NOD_ID, toNOD_ID = clctsk.Task.TPoints[i + 1].NOD_ID, RZN_ID_LIST = RZN_ID_LIST });
                            }
                        }

                        foreach (FTLTruck trk in CalcTrucks)
                        {
                            if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                            {
                                //4.2 futó túrapontok közötti távolságok
                                for (int i = 0; i < trk.CurrTPoints.Count - 1; i++)
                                {
                                    sKey = trk.CurrTPoints[i].NOD_ID.ToString() + "," + trk.CurrTPoints[i + 1].NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = trk.CurrTPoints[i].NOD_ID, toNOD_ID = trk.CurrTPoints[i + 1].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }

                                //4.3 Utolsó teljesített túrapont --> aktuális járműpozíció
                                if (trk.TPointCompleted > 0)
                                {
                                    sKey = trk.CurrTPoints[trk.TPointCompleted - 1].NOD_ID.ToString() + "," + trk.NOD_ID_CURR.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = trk.CurrTPoints[trk.TPointCompleted - 1].NOD_ID, toNOD_ID = trk.NOD_ID_CURR, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }

                                //4.4 Aktuális járműpozíció --> első nem teljesített túrapont
                                sKey = trk.NOD_ID_CURR.ToString() + "," + trk.CurrTPoints[trk.TPointCompleted].NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = trk.NOD_ID_CURR, toNOD_ID = trk.CurrTPoints[trk.TPointCompleted].NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                                //4.5 Teljesített utolsó túrapont -> beosztandó első túrapont (átállás)
                                sKey = trk.CurrTPoints.Last().NOD_ID.ToString() + "," + clctsk.Task.TPoints.First().NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                if (!dicRoutes.ContainsKey(sKey))
                                    dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = trk.CurrTPoints.Last().NOD_ID, toNOD_ID = clctsk.Task.TPoints.First().NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                                //4.6 Beosztandó túrapont utolsó --> teljesítés legelső túrapont (csak NEM irányos túra esetén !!)
                                if (!trk.CurrIsOneWay)
                                {
                                    sKey = clctsk.Task.TPoints.Last().NOD_ID.ToString() + "," + trk.CurrTPoints.First().NOD_ID.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = clctsk.Task.TPoints.Last().NOD_ID, toNOD_ID = trk.CurrTPoints.First().NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });
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
                                    dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = trk.NOD_ID_CURR, toNOD_ID = clctsk.Task.TPoints.First().NOD_ID, RZN_ID_LIST = trk.RZN_ID_LIST });

                                //4.6 Beosztandó túrapont utolsó --> aktuális pozíció (csak NEM irányos túra esetén !!)
                                if (!trk.CurrIsOneWay)
                                {
                                    sKey = clctsk.Task.TPoints.Last().NOD_ID.ToString() + "," + trk.NOD_ID_CURR.ToString() + "," + trk.RZN_ID_LIST;
                                    if (!dicRoutes.ContainsKey(sKey))
                                        dicRoutes.Add(sKey, new FTLPMapRoute { fromNOD_ID = clctsk.Task.TPoints.Last().NOD_ID, toNOD_ID = trk.NOD_ID_CURR, RZN_ID_LIST = trk.RZN_ID_LIST });
                                }

                            }
                        }

                        /********************************/
                        /* Eredmény objektum felépítése */ 
                        /********************************/
                        foreach (FTLTruck trk in CalcTrucks)
                        {
                            FTLCalcTour clctour = new FTLCalcTour();
                            clctour.Truck = trk;
                            clctsk.CalcTours.Add(clctour);
                        }
                    }

                    //5. legeneráljuk az összes futó túra befejezés és a szállítási feladat felrakás távolságot/menetidőt

                    List<FTLPMapRoute> lstPMapRoutes = dicRoutes.Values.ToList();       //ide fogjuk visszaírni az eredményt(is)
                    List<FTLPMapRoute> lstCalcPMapRoutes = new List<FTLPMapRoute>();        //Számolandó útvonalak

                    //debug info
                    foreach (FTLPMapRoute r in lstPMapRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                        Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " zónák:" + r.RZN_ID_LIST);

                    // 5.1 ha cache-eljük az útvonalakat, megnézzük, kiolvassuk a meglévőket
                    //
                    if (p_cacheRoutes)
                    {
                        foreach (FTLPMapRoute r in lstPMapRoutes)
                        {
                            boRoute rt = route.GetRouteFromDB(r.RZN_ID_LIST, r.fromNOD_ID, r.toNOD_ID);
                            if (rt != null)
                            {
                                r.route = rt;
                            }
                            else
                            {
                                lstCalcPMapRoutes.Add(r);
                            }
                        }
                        lstPMapRoutes.RemoveAll(x => x.route == null);
                    }
                    else
                    {
                        lstCalcPMapRoutes = lstPMapRoutes;
                        lstPMapRoutes.Clear();
                    }

                    if (lstCalcPMapRoutes.Count > 0)
                    {
                        ProcessNotifyIcon ni = new ProcessNotifyIcon();
                        FTLCalcRouteProcess rp = new FTLCalcRouteProcess(ni, lstCalcPMapRoutes, p_cacheRoutes);
                        rp.RunWait();
                    }

                    lstPMapRoutes.AddRange(lstCalcPMapRoutes);

                    //
                    //debug info
                    foreach (FTLPMapRoute r in lstPMapRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                        Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " zónák:" + r.RZN_ID_LIST + " dist:" + r.route.DST_DISTANCE.ToString());


                    //6.eredmény összeállítása

                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        foreach ( FTLCalcTour clctour in clctsk.CalcTours)
                        {

                            FTLTruck trk  = clctour.Truck; 
                            // Útvonal összeállítása

                            /***********/
                            /* T1 túra */
                            /***********/
                            if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                            {

                                //6.1.1 : legelső pont:
                                clctour.T1CalcRoute.Add(new FTLCalcRoute()
                                   {
                                       TPoint = trk.CurrTPoints[0],
                                       Arrival = trk.CurrTPoints[0].Arrival,
                                       Departure = trk.CurrTPoints[0].Departure,
                                       Completed = trk.TPointCompleted > 0,
                                       PMapRoute = null,
                                       Duration = 0,
                                       Distance = 0,
                                       Toll = 0,
                                       Current = false
                                   });

                                //6.1.2 : második pont->utolsó teljesített pont 

                                for (int i = 1; i < trk.TPointCompleted - 1; i++)
                                {
                                    FTLPMapRoute rt = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.CurrTPoints[i - 1].NOD_ID && x.toNOD_ID == trk.CurrTPoints[i].NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                    clctour.T1CalcRoute.Add(new FTLCalcRoute()
                                      {
                                          TPoint = trk.CurrTPoints[i],
                                          Arrival = trk.CurrTPoints[i].Arrival,
                                          Departure = trk.CurrTPoints[i].Departure,
                                          Completed = true,
                                          PMapRoute = rt,
                                          Duration = 0,
                                          Distance = 0,
                                          Toll = 0,
                                          Current = false
                                      });
                                }

                                //6.1.3  Utolsó teljesített pont -> Curr
                                if (trk.TPointCompleted > 0)
                                {
                                    FTLPMapRoute rt = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.CurrTPoints[trk.TPointCompleted - 1].NOD_ID && x.toNOD_ID == trk.NOD_ID_CURR && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                    clctour.T1CalcRoute.Add(new FTLCalcRoute()
                                    {
                                        TPoint = null,
                                        Arrival = DateTime.MinValue,
                                        Departure = DateTime.MinValue,
                                        Completed = true,
                                        PMapRoute = rt,
                                        Duration = 0,
                                        Distance = 0,
                                        Toll = 0,
                                        Current = true
                                    });

                                    //6.1.4  Curr --> első teljesítetlen túrapont 
                                    rt = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_CURR && x.toNOD_ID == trk.CurrTPoints[trk.TPointCompleted].NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                    clctour.T1CalcRoute.Add(new FTLCalcRoute()
                                    {
                                        TPoint = trk.CurrTPoints[trk.TPointCompleted],
                                        Arrival = DateTime.MinValue,
                                        Departure = DateTime.MinValue,
                                        Completed = false,
                                        PMapRoute = rt,
                                        Duration = 0,
                                        Distance = 0,
                                        Toll = 0,
                                        Current = false
                                    });
                                }

                                //6.1.5  első teljesítetlen túrapont --> befejezés

                                for (int i = trk.TPointCompleted + 1; i < trk.CurrTPoints.Count; i++)
                                {
                                    FTLPMapRoute rt = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.CurrTPoints[i - 1].NOD_ID && x.toNOD_ID == trk.CurrTPoints[i].NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                    clctour.T1CalcRoute.Add(new FTLCalcRoute()
                                     {
                                         TPoint = trk.CurrTPoints[i],
                                         Arrival = DateTime.MinValue,
                                         Departure = DateTime.MinValue,
                                         Completed = false,
                                         PMapRoute = rt,
                                         Duration = 0,
                                         Distance = 0,
                                         Toll = 0,
                                         Current = false
                                     });
                                }
                            }
                            else
                            {
                                // elérhetőség esetén a legelső túrapont az átállás lesz
                            }



                            /***********/
                            /* Átállás */
                            /***********/
                            FTLPMapRoute rtx;
                            if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                            {
                                //6.2  utolsó beosztott túrapont --> első beosztandó túrapont
                                rtx = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.CurrTPoints.Last().NOD_ID && x.toNOD_ID == clctsk.Task.TPoints.First().NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                            }
                            else
                            {
                                //6.2  elérhetőség esetén CURR --> első beosztandó túrapont
                                rtx = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.NOD_ID_CURR && x.toNOD_ID == clctsk.Task.TPoints.First().NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                            }
                            clctour.RelCalcRoute= new FTLCalcRoute()
                            {
                                TPoint = clctsk.Task.TPoints.First(),
                                Arrival = DateTime.MinValue,
                                Departure = DateTime.MinValue,
                                Completed = false,
                                PMapRoute = rtx,
                                Duration = 0,
                                Distance = 0,
                                Toll = 0,
                                Current = false
                            };
                            
                             //6.3 : második pont->utolsó teljesített pont 

                            for (int i = 1; i < clctsk.Task.TPoints.Count ; i++)
                            {
                                FTLPMapRoute rt = lstPMapRoutes.Where(x => x.fromNOD_ID == clctsk.Task.TPoints[i - 1].NOD_ID && x.toNOD_ID == clctsk.Task.TPoints[i].NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                clctour.T2CalcRoute.Add(new FTLCalcRoute()
                                {
                                    TPoint = clctsk.Task.TPoints[i],
                                    Arrival = DateTime.MinValue,
                                    Departure = DateTime.MinValue,
                                    Completed = false,
                                    PMapRoute = rt,
                                    Duration = 0,
                                    Distance = 0,
                                    Toll = 0,
                                    Current = false
                                });
                            }

                            //6.4 : Nem irányos túra esetén tervezett utolsó pont -> futó első pont 
                            if (!trk.CurrIsOneWay)
                            {
                                FTLPoint pt2 = null; 

                                FTLPMapRoute rtx2;
                                if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                                {
                                    //6.2  utolsó beosztott túrapont --> első beosztandó túrapont
                                    rtx2 = lstPMapRoutes.Where(x => x.fromNOD_ID == clctsk.Task.TPoints.Last().NOD_ID && x.toNOD_ID == trk.CurrTPoints.First().NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                    pt2 = clctsk.Task.TPoints.Last();
                                }
                                else
                                {
                                    //6.2  elérhetőség esetén utolsó beosztandó túrapont --> CURR
                                    rtx2 = lstPMapRoutes.Where(x => x.fromNOD_ID == clctsk.Task.TPoints.Last().NOD_ID && x.toNOD_ID == trk.NOD_ID_CURR && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                }
                                clctour.RetCalcRoute = new FTLCalcRoute()
                                {
                                    TPoint = pt2,
                                    Arrival = DateTime.MinValue,
                                    Departure = DateTime.MinValue,
                                    Completed = false,
                                    PMapRoute = rtx2,
                                    Duration = 0,
                                    Distance = 0,
                                    Toll = 0,
                                    Current = false
                                };
                            }
                        }
                    }

/********************************************************************************************************************************************************/

                    
                    /*****************************************************************************************************/
                    /* Járművek törlése, amelyek nem tudják teljesíteni a túrákat, mert nem találtunk útvonalat hozzájuk */
                    /*****************************************************************************************************/
                    foreach (FTLCalcTask clctsk in tskResult)
                    {

                        List<FTLTruck> lstTrucksErrT1 = clctsk.CalcTours.Where(x => x.Truck.TruckTaskType != FTLTruck.eTruckTaskType.Available &&
                                                                               x.T1CalcRoute.Where(r => r.PMapRoute != null &&
                                                                                                    r.PMapRoute.fromNOD_ID != r.PMapRoute.toNOD_ID && r.PMapRoute.route.Edges.Count == 0).FirstOrDefault() != null).Select(s => s.Truck).ToList();
                        if (lstTrucksErrT1.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_T1MISSROUTE + string.Join(",", lstTrucksErrT1.Select(x => x.TruckID).ToArray()));

                        List<FTLTruck> lstTrucksErrRel = clctsk.CalcTours.Where(x => x.RelCalcRoute.PMapRoute.fromNOD_ID != x.RelCalcRoute.PMapRoute.toNOD_ID && x.RelCalcRoute.PMapRoute.route.Edges.Count == 0).Select(s => s.Truck).ToList();
                        if (lstTrucksErrRel.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_RELMISSROUTE + string.Join(",", lstTrucksErrRel.Select(x => x.TruckID).ToArray()));


                        List<FTLTruck> lstTrucksErrT2 = clctsk.CalcTours.Where(x => x.T2CalcRoute.Where(r => r.PMapRoute != null &&
                                                                                    r.PMapRoute.fromNOD_ID != r.PMapRoute.toNOD_ID && r.PMapRoute.route.Edges.Count == 0).FirstOrDefault() != null).Select(s => s.Truck).ToList();
                        if (lstTrucksErrT2.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_T2MISSROUTE + string.Join(",", lstTrucksErrT2.Select(x => x.TruckID).ToArray()));

                        List<FTLTruck> lstTrucksErrRet = clctsk.CalcTours.Where(x => x.RetCalcRoute.PMapRoute != null && x.RetCalcRoute.PMapRoute.fromNOD_ID != x.RetCalcRoute.PMapRoute.toNOD_ID && x.RetCalcRoute.PMapRoute.route.Edges.Count == 0).Select(s => s.Truck).ToList();
                        if (lstTrucksErrRet.Count > 0)
                            clctsk.Messages.Add(FTLMessages.E_RETMISSROUTE + string.Join(",", lstTrucksErrRet.Select(x => x.TruckID).ToArray()));

                        clctsk.CalcTours.RemoveAll( x => lstTrucksErrT1.Contains(x.Truck) ||
                                                        lstTrucksErrRel.Contains(x.Truck) ||
                                                        lstTrucksErrT2.Contains(x.Truck) ||
                                                        lstTrucksErrRet.Contains(x.Truck));
                    }

                    
                    /***************/
                    /* Számítások  */
                    /***************/

                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        foreach ( FTLCalcTour clctour in clctsk.CalcTours)
                        {

                            FTLTruck trk  = clctour.Truck; 
                            

                            string sLastETLCode = "";
                            DateTime dtPrevTime = DateTime.MinValue;




                            if (trk.TruckTaskType == FTLTruck.eTruckTaskType.Available)
                            {
                                dtPrevTime = trk.CurrTime;
                            }


                            /**********************************************/
                            /* Aktuálisan teljestített útvonal számítása */
                            /**********************************************/
                            foreach (FTLCalcRoute clr in clctour.T1CalcRoute)
                            {
                                if (clr == clctour.T1CalcRoute.First())     // legelső túrapont (raktári felrakás)
                                {
                                    if (clr.Completed)
                                    {
                                        // ha teljesítve van a felrakás, a tényadatokat vesszük
                                        clr.Duration = Convert.ToInt32((clr.TPoint.Departure - clr.TPoint.Arrival).TotalMinutes);
                                        clr.Arrival = clr.TPoint.Arrival;
                                        clr.Departure = clr.TPoint.Departure;
                                    }
                                    else
                                    {
                                        //Ha nincs teljesítve a felrakás, a clr.Arrival-t vesszük első időpontnak
                                        clr.Duration = clr.TPoint.SrvDuration;
                                        clr.Arrival = clr.TPoint.Arrival;
                                        clr.Departure = clr.TPoint.Arrival.AddMinutes(clr.TPoint.SrvDuration);
                                    }
                                    clr.Distance = 0;
                                    clr.Toll = 0;
                                }
                                else
                                {

                                    clr.Distance = clr.PMapRoute.route.DST_DISTANCE;
                                    clr.Toll = bllPlanEdit.GetToll(clr.PMapRoute.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);

                                    if (clr.Completed)
                                        if (clr.Current)
                                        {
                                            //akutális pozíció mindig teljesített
                                            clr.Duration = bllPlanEdit.GetDuration(clr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                            clr.Arrival = dtPrevTime.AddMinutes(clr.Duration);             // ez egy köztes pont, itt nincs kiszolgálási idő
                                            clr.Departure = dtPrevTime.AddMinutes(clr.Duration);
                                        }
                                        else
                                        {
                                            //teljesített túrapont esetén a tényadatokat olvassuk ki.
                                            clr.Duration = Convert.ToInt32((clr.TPoint.Departure - dtPrevTime).TotalMinutes);
                                            clr.Arrival = clr.TPoint.Arrival;
                                            clr.Departure = clr.TPoint.Departure;
                                        }
                                    else
                                    {
                                        int routeduration = bllPlanEdit.GetDuration(clr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                        clr.Arrival = dtPrevTime.AddMinutes(routeduration);
                                        clr.Duration = routeduration + clr.TPoint.SrvDuration;
                                        clr.Departure = dtPrevTime.AddMinutes(clr.Duration);
                                    }
                                }
                                dtPrevTime = clr.Departure;
                                clctour.T1M += clr.Distance;
                                clctour.T1Toll += clr.Toll;
                                clctour.T1Cost += trk.KMCost * clr.Distance / 1000;
                            }


                            if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                            {
                                clctour.T1End = clctour.T1CalcRoute.Last().Departure;
                            }


                            /*********************/
                            /* Átállás számítása */
                            /*********************/
                            var relclr = clctour.RelCalcRoute;      //csak hogy ne kelljen a clctour.RelCalcRoute válozónevet használni
                            relclr.Distance = relclr.PMapRoute.route.DST_DISTANCE;
                            relclr.Toll = bllPlanEdit.GetToll(relclr.PMapRoute.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                            
                            int rtduration =  bllPlanEdit.GetDuration(relclr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                            relclr.Arrival = dtPrevTime.AddMinutes(rtduration);             // ez egy köztes pont, itt nincs kiszolgálási idő
                            relclr.Duration = rtduration + relclr.TPoint.SrvDuration;
                            relclr.Departure = dtPrevTime.AddMinutes( relclr.Duration);

                            dtPrevTime = relclr.Departure;
                            clctour.RelM = relclr.Distance;
                            clctour.RelToll = relclr.Toll;
                            clctour.RelCost = trk.RelocateCost * relclr.Distance / 1000;
                            clctour.RelDuration = relclr.Duration;

                            if (trk.TruckTaskType != FTLTruck.eTruckTaskType.Available)
                                clctour.RelStart = clctour.T1End;
                            else
                                clctour.RelStart = trk.CurrTime;
                            clctour.RelEnd = relclr.Departure;



                            /*********************************/
                            /* II. túra teljesítés számítása */
                            /*********************************/
                            foreach (FTLCalcRoute clr in clctour.T2CalcRoute)
                            {
                                clr.Distance = clr.PMapRoute.route.DST_DISTANCE;
                                clr.Toll = bllPlanEdit.GetToll(clr.PMapRoute.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                                int routeduration = bllPlanEdit.GetDuration(clr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                clr.Arrival = dtPrevTime.AddMinutes(routeduration);
                                clr.Duration = routeduration + clr.TPoint.SrvDuration;
                                clr.Departure = dtPrevTime.AddMinutes(clr.Duration);

                                dtPrevTime = clr.Departure;
                                clctour.T2M += clr.Distance;
                                clctour.T2Toll = clr.Toll;
                                clctour.T2Cost = trk.KMCost * clr.Distance / 1000;
                            }

                            clctour.T2Start = clctour.RelEnd;
                            clctour.T2End = clctour.T2CalcRoute.Last().Departure;


                            /*************************/
                            /* Visszatérés számítása */
                            /*************************/
                            if (!trk.CurrIsOneWay)
                            {
                                var retclr = clctour.RetCalcRoute;      //csak hogy ne kelljen a clctour.RerCalcRoute válozónevet használni
                                retclr.Distance = retclr.PMapRoute.route.DST_DISTANCE;
                                retclr.Toll = bllPlanEdit.GetToll(retclr.PMapRoute.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                                retclr.Duration = bllPlanEdit.GetDuration(retclr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                retclr.Arrival = dtPrevTime.AddMinutes(retclr.Duration);             
                                retclr.Departure = dtPrevTime.AddMinutes(retclr.Duration);
                                dtPrevTime = retclr.Departure;
                                clctour.RelM = retclr.Distance;
                                clctour.RelToll = retclr.Toll;
                                clctour.RelCost = trk.KMCost * retclr.Distance / 1000;

                                clctour.RetStart = clctour.T2End;
                                clctour.RetEnd = retclr.Departure;
                            }

                        }
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
                        result.Add(getValidationError(item, err.Field, err.Message));
                    }
                }
            }

            return result;

        }

        private static FTLResult getValidationError(Object p_obj, string p_field, string p_msg)
        {
            FTLResErrMsg msg = new FTLResErrMsg() { Field = p_field, Message = p_msg, CallStack = "" };
            PropertyInfo ItemIDProp = p_obj.GetType().GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(ItemIDAttr))).FirstOrDefault();

            FTLResult itemRes = new FTLResult()
            {
                Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                ObjectName = p_obj.GetType().Name,
                ItemID = ItemIDProp != null ? p_obj.GetType().GetProperty(ItemIDProp.Name).GetValue(p_obj, null).ToString() : "???",
                Data = msg
            };
            return itemRes;
        }




    }
}
