using GMap.NET;
using PMap;
using PMap.BLL;
using PMap.BO;
using PMap.Common;
using PMap.Common.Attrib;
using PMap.DB.Base;
using PMap.Licence;
using PMap.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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
                ChkLic.Check(PMapIniParams.Instance.IDFile);


                PMapCommonVars.Instance.ConnectToDB();
                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                /*
                List<FTLPMapRoute> lstCalcPMapRoutesx = new List<FTLPMapRoute>();        //Számolandó útvonalak
                var rx = new FTLPMapRoute { fromNOD_ID = 345264, toNOD_ID = 340835, RZN_ID_LIST = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15" };
                lstCalcPMapRoutesx.Add(rx);
                ProcessNotifyIcon nix = new ProcessNotifyIcon();
                FTLCalcRouteProcess rpx = new FTLCalcRouteProcess(nix, lstCalcPMapRoutesx, false);
                rpx.RunWait();
                */


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


                //  int nn = route.GetNearestReachableNOD_IDForTruck(new GMap.NET.PointLatLng(47.4254452, 19.1494274), route.GetRestZonesByRST_ID(Global.RST_BIGGER12T), Global.NearestNOD_ID_Approach / 2);

                foreach (FTLTask tsk in p_TaskList)
                {
                    if (tsk.TPoints.Count >= 2)
                    {

                        //Koordináta feloldás és ellenőrzés
                        foreach (FTLPoint pt in tsk.TPoints)
                        {
                            //A beosztandó szállíási feladat esetén olyan NOD_ID-t keresünk, 
                            //amelyet egy távolságon belül lehetőleg minden járműtípus számára elérhető
                            //
                            // 
                            string sXRZN_ID_LIST;
                            pt.NOD_ID = 0;
                            for (int iRST = Global.RST_BIGGER12T; iRST <= Global.RST_MAX75T && pt.NOD_ID == 0; iRST++)
                            {
                                sXRZN_ID_LIST = route.GetRestZonesByRST_ID(iRST);
                                //Kicsit s
                                pt.NOD_ID = FTLGetNearestReachableNOD_IDForTruck(route, new GMap.NET.PointLatLng(pt.Lat, pt.Lng), sXRZN_ID_LIST, Global.NearestNOD_ID_Approach);
                            }

                            //nem találtun korlátozásokhoz NODE-t, nézünk egy korlátozás nélküli közeli pontot. 
                            //
                            if (pt.NOD_ID == 0)
                            {
                                sXRZN_ID_LIST = route.GetRestZonesByRST_ID(Global.RST_NORESTRICT);
                                pt.NOD_ID = FTLGetNearestReachableNOD_IDForTruck(route, new GMap.NET.PointLatLng(pt.Lat, pt.Lng), sXRZN_ID_LIST, Global.NearestNOD_ID_Approach);
                                if (pt.NOD_ID == 0)
                                {
                                    result.Add(getValidationError(pt, "Lat,Lng", FTLMessages.E_WRONGCOORD));
                                }
                            }
                        }
                    }
                    else
                    {
                        result.Add(getValidationError(tsk, "TPoints", FTLMessages.E_FEWPOINTS));
                    }
                }


                //Validálás, koordináta feloldás:jármű aktuális pozíció, szállítási feladat
                //
                //1.1 A járművek zónalistájának összeállítása
                foreach (FTLTruck trk in p_TruckList)
                {
                    trk.RZN_ID_LIST = route.GetRestZonesByRST_ID(trk.RST_ID);

                    //Teljesített túrapont ellenőrzés
                    if ((trk.TruckTaskType == FTLTruck.eTruckTaskType.Planned || trk.TruckTaskType == FTLTruck.eTruckTaskType.Running) &&
                        (trk.TPointCompleted < 0 || trk.TPointCompleted > trk.CurrTPoints.Count - 1))
                    {
                        result.Add(getValidationError(trk, "TPointCompleted", FTLMessages.E_TRKWRONGCOMPLETED));
                    }

                    trk.NOD_ID_CURR = FTLGetNearestReachableNOD_IDForTruck(route, new GMap.NET.PointLatLng(trk.CurrLat, trk.CurrLng), trk.RZN_ID_LIST, Global.NearestNOD_ID_Approach);
                    if (trk.NOD_ID_CURR == 0)
                        result.Add(getValidationError(trk, "CurrLat,CurrLng", FTLMessages.E_WRONGCOORD));

                    //Koordináta feloldás és ellenőrzés
                    foreach (FTLPoint pt in trk.CurrTPoints)
                    {
                        pt.NOD_ID = FTLGetNearestReachableNOD_IDForTruck(route, new GMap.NET.PointLatLng(pt.Lat, pt.Lng), trk.RZN_ID_LIST, Global.NearestNOD_ID_Approach);
                        if (pt.NOD_ID == 0)
                        {
                            result.Add(getValidationError(pt, "Lat,Lng", FTLMessages.E_WRONGCOORD));
                        }
                    }
                }


                if (result.Count == 0)
                {
                    /********************************/
                    /* Eredmény objektum felépítése */
                    /********************************/
                    List<FTLCalcTask> tskResult = new List<FTLCalcTask>();
                    foreach (FTLTask tsk in p_TaskList)
                    {
                        var clctsk = new FTLCalcTask() { Task = tsk };
                        tskResult.Add(clctsk);

                        foreach (FTLTruck trk in p_TruckList)
                        {
                            FTLCalcTour clctour = new FTLCalcTour();
                            clctour.Truck = trk;
                            clctsk.CalcTours.Add(clctour);
                        }
                    }


                    //1. Előkészítés:


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
                            clctsk.CalcTours.Where(x => lstTrucksErr.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_TRKTYPE); });

                        lstTrucksErr = p_TruckList.Where(x => !(("," + x.CargoTypes + ",").IndexOf("," + clctsk.Task.CargoType + ",") >= 0)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErr.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_TRKCARGOTYPE); });

                        lstTrucksErr = p_TruckList.Where(x => !(x.Capacity >= clctsk.Task.Weight)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErr.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_TRKCAPACITY); });

                        lstTrucksErr = p_TruckList.Where(x => !(clctsk.Task.TPoints.Where(p => p.Close > x.CurrTime).FirstOrDefault() != null)).ToList();
                        if (lstTrucksErr.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErr.Contains(x.Truck)).ToList()
                                            .ForEach(x =>
                                            {
                                                x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_TRKCLOSETP +
                                    string.Join(",", clctsk.Task.TPoints.Where(p => p.Close > x.Truck.CurrTime).Select(s => s.Name).ToArray()));
                                            });


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

                    }

                    //5. legeneráljuk az összes futó túra befejezés és a szállítási feladat felrakás távolságot/menetidőt

                    List<FTLPMapRoute> lstPMapRoutes = dicRoutes.Values.ToList();       //ide fogjuk visszaírni az eredményt(is)
                    List<FTLPMapRoute> lstCalcPMapRoutes = new List<FTLPMapRoute>();        //Számolandó útvonalak

                    //debug info
                    /*
                    foreach (FTLPMapRoute r in lstPMapRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                        Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " zónák:" + r.RZN_ID_LIST);
                    */

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
                    /*
                    foreach (FTLPMapRoute r in lstPMapRoutes.OrderBy(o => o.fromNOD_ID.ToString() + o.toNOD_ID.ToString() + o.RZN_ID_LIST))
                        Console.WriteLine(r.fromNOD_ID.ToString() + " -> " + r.toNOD_ID.ToString() + " zónák:" + r.RZN_ID_LIST + " dist:" + r.route.DST_DISTANCE.ToString());
                    */

                    //6.eredmény összeállítása

                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        foreach (FTLCalcTour clctour in clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK))
                        {

                            FTLTruck trk = clctour.Truck;
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
                                    Arrival = trk.CurrTPoints[0].RealArrival,
                                    Departure = trk.CurrTPoints[0].RealDeparture,
                                    Completed = trk.TPointCompleted > 0,
                                    PMapRoute = null,
                                    Current = false
                                });

                                //6.1.2 : második pont->utolsó teljesített pont 

                                for (int i = 1; i < trk.TPointCompleted - 1; i++)
                                {
                                    FTLPMapRoute rt = lstPMapRoutes.Where(x => x.fromNOD_ID == trk.CurrTPoints[i - 1].NOD_ID && x.toNOD_ID == trk.CurrTPoints[i].NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                    clctour.T1CalcRoute.Add(new FTLCalcRoute()
                                    {
                                        TPoint = trk.CurrTPoints[i],
                                        Arrival = trk.CurrTPoints[i].RealArrival,
                                        Departure = trk.CurrTPoints[i].RealDeparture,
                                        Completed = true,
                                        PMapRoute = rt,
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
                            clctour.RelCalcRoute = new FTLCalcRoute()
                            {
                                TPoint = clctsk.Task.TPoints.First(),
                                Arrival = DateTime.MinValue,
                                Departure = DateTime.MinValue,
                                Completed = false,
                                PMapRoute = rtx,
                                Current = false
                            };

                            //6.3 : második pont->utolsó teljesített pont 

                            for (int i = 1; i < clctsk.Task.TPoints.Count; i++)
                            {
                                FTLPMapRoute rt = lstPMapRoutes.Where(x => x.fromNOD_ID == clctsk.Task.TPoints[i - 1].NOD_ID && x.toNOD_ID == clctsk.Task.TPoints[i].NOD_ID && x.RZN_ID_LIST == trk.RZN_ID_LIST).FirstOrDefault();
                                clctour.T2CalcRoute.Add(new FTLCalcRoute()
                                {
                                    TPoint = clctsk.Task.TPoints[i],
                                    Arrival = DateTime.MinValue,
                                    Departure = DateTime.MinValue,
                                    Completed = false,
                                    PMapRoute = rt,
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
                                    pt2 = trk.CurrTPoints.First();
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
                                    Current = false
                                };
                            }
                        }
                    }

                    /********************************************************************************************************************************************************/


                    /**************************************************************************************************************/
                    /* Hiba beállítása járművekre amelyek nem tudják teljesíteni a túrákat, mert nem találtunk útvonalat hozzájuk */
                    /**************************************************************************************************************/
                    foreach (FTLCalcTask clctsk in tskResult)
                    {

                        List<FTLTruck> lstTrucksErrT1 = clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                               x.Truck.TruckTaskType != FTLTruck.eTruckTaskType.Available &&
                                                                               x.T1CalcRoute.Where(r => r.PMapRoute != null &&
                                                                                                    r.PMapRoute.fromNOD_ID != r.PMapRoute.toNOD_ID && r.PMapRoute.route.Edges.Count == 0).FirstOrDefault() != null).Select(s => s.Truck).ToList();
                        if (lstTrucksErrT1.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErrT1.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_T1MISSROUTE); });

                        List<FTLTruck> lstTrucksErrRel = clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                                x.RelCalcRoute.PMapRoute.fromNOD_ID != x.RelCalcRoute.PMapRoute.toNOD_ID && x.RelCalcRoute.PMapRoute.route.Edges.Count == 0).Select(s => s.Truck).ToList();
                        if (lstTrucksErrRel.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErrRel.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_RELMISSROUTE); });


                        List<FTLTruck> lstTrucksErrT2 = clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                               x.T2CalcRoute.Where(r => r.PMapRoute != null &&
                                                                                    r.PMapRoute.fromNOD_ID != r.PMapRoute.toNOD_ID && r.PMapRoute.route.Edges.Count == 0).FirstOrDefault() != null).Select(s => s.Truck).ToList();
                        if (lstTrucksErrT2.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErrT2.Contains(x.Truck)).ToList()
                                           .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_T2MISSROUTE); });

                        List<FTLTruck> lstTrucksErrRet = clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                                x.RetCalcRoute.PMapRoute != null && x.RetCalcRoute.PMapRoute.fromNOD_ID != x.RetCalcRoute.PMapRoute.toNOD_ID && x.RetCalcRoute.PMapRoute.route.Edges.Count == 0).Select(s => s.Truck).ToList();
                        if (lstTrucksErrRet.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErrRet.Contains(x.Truck)).ToList()
                                           .ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; x.Msg.Add(FTLMessages.E_RETMISSROUTE); });
                    }


                    /***************/
                    /* Számítások  */
                    /***************/

                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        foreach (FTLCalcTour clctour in clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK))
                        {

                            FTLTruck trk = clctour.Truck;


                            string sLastETLCode = "";
                            DateTime dtPrevTime = DateTime.MinValue;

                            if (trk.TruckTaskType == FTLTruck.eTruckTaskType.Available)
                            {
                                dtPrevTime = trk.CurrTime;
                            }


                            /**********************************************/
                            /* Aktuálisan teljestített útvonal számítása */
                            /**********************************************/
                            var firstCurrPoint = trk.CurrTPoints.FirstOrDefault();
                            if (firstCurrPoint != null)
                                clctour.T1Start = firstCurrPoint.RealArrival;

                            foreach (FTLCalcRoute clr in clctour.T1CalcRoute)
                            {
                                if (clr == clctour.T1CalcRoute.First())     // legelső túrapont (raktári felrakás)
                                {
                                    if (clr.Completed)
                                    {
                                        // ha teljesítve van a felrakás, a tényadatokat vesszük
                                        clr.RouteDuration = 0;
                                        clr.WaitingDuration = 0;
                                        clr.SrvDuration = Convert.ToInt32((clr.TPoint.RealDeparture - clr.TPoint.RealArrival).TotalMinutes);
                                        clr.Arrival = clr.TPoint.RealArrival;
                                        clr.Departure = clr.TPoint.RealDeparture;
                                    }
                                    else
                                    {
                                        //Ha nincs teljesítve a felrakás, a clr.Arrival-t vesszük első időpontnak
                                        clr.RouteDuration = 0;
                                        clr.WaitingDuration = 0;
                                        clr.SrvDuration = clr.TPoint.SrvDuration;
                                        clr.Arrival = clr.TPoint.RealArrival;
                                        clr.Departure = clr.TPoint.RealArrival.AddMinutes(clr.TPoint.SrvDuration);
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
                                            clr.RouteDuration = Convert.ToInt32((trk.CurrTime - dtPrevTime).TotalMinutes);
                                            clr.WaitingDuration = 0;
                                            clr.SrvDuration = 0;    // ez egy köztes pont, itt nincs kiszolgálási idő
                                            clr.Arrival = trk.CurrTime;
                                            clr.Departure = trk.CurrTime;
                                        }
                                        else
                                        {
                                            //teljesített túrapont esetén a tényadatokat olvassuk ki.
                                            clr.RouteDuration = Convert.ToInt32((clr.TPoint.RealArrival - dtPrevTime).TotalMinutes);
                                            clr.WaitingDuration = 0;            //nem tudjuk meghatározni, a menetidő vagy a rakodás a várakozást is tartalmazza-e
                                            clr.SrvDuration = Convert.ToInt32((clr.TPoint.RealDeparture - clr.TPoint.RealArrival).TotalMinutes);
                                            clr.Arrival = clr.TPoint.RealArrival;
                                            clr.Departure = clr.TPoint.RealDeparture;
                                        }
                                    else
                                    {
                                        clr.RouteDuration = bllPlanEdit.GetDuration(clr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                        clr.Arrival = dtPrevTime.AddMinutes(clr.RouteDuration);
                                        if (clr.Arrival < clr.TPoint.Open)
                                            clr.WaitingDuration = Convert.ToInt32((clr.TPoint.Open - clr.Arrival).TotalMinutes);        ////Ha hamarabb érkezünk, mint a nyitva tartás kezdete, várunk
                                        else
                                            clr.WaitingDuration = 0;

                                        clr.SrvDuration = clr.TPoint.SrvDuration;

                                        clr.Departure = dtPrevTime.AddMinutes(clr.RouteDuration + clr.WaitingDuration + clr.SrvDuration);
                                    }
                                }
                                dtPrevTime = clr.Departure;
                                clctour.T1Duration += clr.RouteDuration + clr.WaitingDuration + clr.SrvDuration;
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

                            relclr.RouteDuration = bllPlanEdit.GetDuration(relclr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                            relclr.Arrival = dtPrevTime.AddMinutes(relclr.RouteDuration);
                            if (relclr.Arrival < relclr.TPoint.Open)
                                relclr.WaitingDuration = Convert.ToInt32((relclr.TPoint.Open - relclr.Arrival).TotalMinutes);        ////Ha hamarabb érkezünk, mint a nyitva tartás kezdete, várunk
                            else
                                relclr.WaitingDuration = 0;

                            relclr.SrvDuration = relclr.TPoint.SrvDuration;
                            relclr.Departure = dtPrevTime.AddMinutes(relclr.RouteDuration + relclr.WaitingDuration + relclr.SrvDuration);

                            dtPrevTime = relclr.Departure;
                            clctour.RelDuration = relclr.RouteDuration + relclr.WaitingDuration + relclr.SrvDuration;
                            clctour.RelM = relclr.Distance;
                            clctour.RelToll = relclr.Toll;
                            clctour.RelCost = trk.RelocateCost * relclr.Distance / 1000;

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
                                clr.RouteDuration = bllPlanEdit.GetDuration(clr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                clr.Arrival = dtPrevTime.AddMinutes(clr.RouteDuration);
                                if (clr.Arrival < clr.TPoint.Open)
                                    clr.WaitingDuration = Convert.ToInt32((clr.TPoint.Open - clr.Arrival).TotalMinutes);        ////Ha hamarabb érkezünk, mint a nyitva tartás kezdete, várunk
                                else
                                    clr.WaitingDuration = 0;
                                clr.SrvDuration = clr.TPoint.SrvDuration;
                                clr.Departure = dtPrevTime.AddMinutes(clr.RouteDuration + clr.WaitingDuration + clr.SrvDuration);

                                dtPrevTime = clr.Departure;
                                clctour.T2Duration += clr.RouteDuration + clr.WaitingDuration + clr.SrvDuration;
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
                                var retclr = clctour.RetCalcRoute;      //csak hogy ne kelljen a clctour.RetCalcRoute válozónevet használni
                                retclr.Distance = retclr.PMapRoute.route.DST_DISTANCE;
                                retclr.Toll = bllPlanEdit.GetToll(retclr.PMapRoute.route.Edges, trk.ETollCat, bllPlanEdit.GetTollMultiplier(trk.ETollCat, trk.EngineEuro), ref sLastETLCode);
                                retclr.RouteDuration = bllPlanEdit.GetDuration(retclr.PMapRoute.route.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);
                                retclr.Arrival = dtPrevTime.AddMinutes(retclr.RouteDuration);
                                if (retclr.TPoint != null && retclr.Arrival < retclr.TPoint.Open)       //Ha a visszatérés túrapontra történik és hamarabb érkezünk vissza, mint a nyitva tartás kezdete, várunk 
                                    retclr.WaitingDuration = Convert.ToInt32((retclr.TPoint.Open - retclr.Arrival).TotalMinutes);
                                else
                                    retclr.WaitingDuration = 0;

                                retclr.SrvDuration = 0;                             //visszatérés esetén nincs kiszolgálás 
                                retclr.Departure = dtPrevTime.AddMinutes(retclr.RouteDuration + retclr.WaitingDuration + retclr.SrvDuration);

                                dtPrevTime = retclr.Departure;
                                clctour.RetDuration = retclr.RouteDuration + retclr.WaitingDuration + retclr.SrvDuration;
                                clctour.RetM = retclr.Distance;
                                clctour.RetToll = retclr.Toll;
                                clctour.RetCost = trk.KMCost * retclr.Distance / 1000;

                                clctour.RetStart = clctour.T2End;
                                clctour.RetEnd = retclr.Departure;
                            }

                        }
                    }

                    /*******************************************************/
                    /* Max. munkaidő és távolság, nyitva tartás ellenőrzés */
                    /*******************************************************/
                    foreach (FTLCalcTask clctsk in tskResult)
                    {

                        List<FTLTruck> lstTrucksErrDuration = clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                               x.Truck.MaxDuration > 0 && x.Truck.MaxDuration < x.FullDuration).Select(s => s.Truck).ToList();
                        if (lstTrucksErrDuration.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErrDuration.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Msg.Add(FTLMessages.E_MAXDURATION); });

                        List<FTLTruck> lstTrucksErrKM = clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                               x.Truck.MaxKM > 0 && x.Truck.MaxKM < x.FullM / 1000).Select(s => s.Truck).ToList();
                        if (lstTrucksErrKM.Count > 0)
                            clctsk.CalcTours.Where(x => lstTrucksErrKM.Contains(x.Truck)).ToList()
                                            .ForEach(x => { x.Msg.Add(FTLMessages.E_MAXKM); });

                        List<FTLTruck> lstTrucksErrOpen = new List<FTLTruck>();
                        foreach (FTLCalcTour clctour in clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK))
                        {

                            //Teljesítés nyitva tartások ellenőrzése
                            List<FTLPoint> lstOpenErrT1 = clctour.T1CalcRoute.Where(x => x.TPoint != null && x.Arrival > x.TPoint.Close).Select(s => s.TPoint).ToList();
                            if (lstOpenErrT1.Count > 0)
                            {
                                lstTrucksErrOpen.Add(clctour.Truck);
                                foreach (FTLPoint tp in lstOpenErrT1)
                                {
                                    clctour.Msg.Add("(T1)" + FTLMessages.E_CLOSETP + tp.Name);
                                }
                            }

                            //Átállás nyitva tartás ellenőrzése
                            if (clctour.RelCalcRoute.Arrival > clctour.RelCalcRoute.TPoint.Close)
                            {
                                lstTrucksErrOpen.Add(clctour.Truck);
                                clctour.Msg.Add("(Rel)" + FTLMessages.E_CLOSETP + clctour.RelCalcRoute.TPoint.Name);
                            }

                            //Beosztott túra tartás ellenőrzése
                            List<FTLPoint> lstOpenErrT2 = clctour.T2CalcRoute.Where(x => x.Arrival > x.TPoint.Close).Select(s => s.TPoint).ToList();
                            if (lstOpenErrT2.Count > 0)
                            {
                                lstTrucksErrOpen.Add(clctour.Truck);
                                foreach (FTLPoint tp in lstOpenErrT2)
                                {
                                    clctour.Msg.Add("(T2)" + FTLMessages.E_CLOSETP + tp.Name);
                                }
                            }

                            //Visszatérés nyitva tartás ellenőrzése (ha van visszatérési pont)
                            if (clctour.RetCalcRoute.TPoint != null)
                            {
                                if (clctour.RetCalcRoute.Arrival > clctour.RetCalcRoute.TPoint.Close)
                                {
                                    lstTrucksErrOpen.Add(clctour.Truck);
                                    clctour.Msg.Add("(Ret)" + FTLMessages.E_CLOSETP + clctour.RetCalcRoute.TPoint.Name);
                                }
                            }

                        }

                        //Miután minden hibát megállapítottunk, beállítjuk a hibastátuszt
                        //
                        clctsk.CalcTours.Where(x => lstTrucksErrDuration.Contains(x.Truck) ||
                                                    lstTrucksErrKM.Contains(x.Truck) ||
                                                    lstTrucksErrOpen.Contains(x.Truck)
                                               ).ToList().ForEach(x => { x.Status = FTLCalcTour.FTLCalcTourStatus.ERR; });

                    }

                    /****************************/
                    /* Eredmények véglegesítése */
                    /****************************/
                    foreach (FTLCalcTask clctsk in tskResult)
                    {
                        //Útvonalpontok 
                        clctsk.CalcTours.ForEach(x =>
                                {
                                    x.T1CalcRoute.Where(w => w.PMapRoute != null).ToList()
                                        .ForEach(i => i.RoutePoints = string.Join(",", i.PMapRoute.route.Route.Points));
                                    x.RelCalcRoute.RoutePoints = x.RelCalcRoute.PMapRoute == null ? "" : string.Join(",", x.RelCalcRoute.PMapRoute.route.Route.Points);
                                    x.T2CalcRoute.Where(w => w.PMapRoute != null).ToList()
                                        .ForEach(i => i.RoutePoints = string.Join(",", i.PMapRoute.route.Route.Points));
                                    x.RetCalcRoute.RoutePoints = x.RetCalcRoute.PMapRoute == null ? "" : string.Join(",", x.RetCalcRoute.PMapRoute.route.Route.Points);
                                });

                        //Költség fordított sorrendben berendezzük
                        int rank = 1;
                        clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK).
                                         OrderBy(x => x.AdditionalCost).Select(x => x).ToList().
                                         ForEach(r => r.Rank = rank++);

                        // A hibás tételek rank-ja: 999999
                        clctsk.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.ERR).ToList().ForEach(x => { x.Rank = 999999; });
                    }


                    FTLResult res = new FTLResult()
                    {
                        Status = FTLResult.FTLResultStatus.RESULT,
                        ObjectName = "",
                        ItemID = "",
                        Data = tskResult

                    };
                    result.Add(res);
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

        private static List<FTLResult> ValidateObjList<T>(List<T> p_list)
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

        public static List<FTLResult> FTLSupportX(List<FTLTask> p_TaskList, List<FTLTruck> p_TruckList, string p_iniPath, string p_dbConf, bool p_cacheRoutes)
        {
           /*
                List<FTLResult> res = FTLInterface.FTLSupport(p_TaskList, p_TruckList, p_iniPath, p_dbConf, p_cacheRoutes);
                
                                 FileInfo fi = new FileInfo( "res.res");
                                 BinarySerializer.Serialize(fi, res);
            */
                                 FileInfo fi = new FileInfo("res.res");
                                 List<FTLResult> res = (List<FTLResult>)BinarySerializer.Deserialize(fi);

            var calcResult = res.Where(i => i.Status == FTLResult.FTLResultStatus.RESULT).FirstOrDefault();
            if (calcResult != null)
            {
                FTLInterface.FTLSetBestTruck(res);
                List<FTLCalcTask> calcTaskList = ((List<FTLCalcTask>)calcResult.Data);

                while (calcTaskList.Where(x => x.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).ToList().Count == 0).ToList().Count != 0)         //addig megy a ciklus, amíg van olyan calcTask amelynnek nincs OK-s CalcTours-a (azaz nincs eredménye)
                {
                    List<FTLTask> lstTsk2 = new List<FTLTask>();
                    var lstTrk2 = FTLInterface.FTLGenerateTrucksFromCalcTours(res);
                    lstTsk2.AddRange(calcTaskList.Where(x => x.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).ToList().Count == 0).Select(s => s.Task));
                    List<FTLResult> res2 = FTLInterface.FTLSupport(lstTsk2, lstTrk2, p_iniPath, p_dbConf, p_cacheRoutes);

                    var calcResult2 = res2.Where(x => x.Status == FTLResult.FTLResultStatus.RESULT).FirstOrDefault();
                    if (calcResult2 != null)
                    {
                        //Elvileg itt már kell, hogy legyen result típusú tétel, mert a validálás az előző menetmen megrtörtént.

                        
                        FTLInterface.FTLSetBestTruck(res2);

                        List<FTLCalcTask> calcTaskList2 = ((List<FTLCalcTask>)calcResult2.Data);

                        //Megvizsgáljuk, hogy a számítási menet hozott-e eredményt.
                        if (calcTaskList2.Where(x => x.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).ToList().Count != 0).ToList().Count == 0)
                            return res;             //ha nincs eredmény, ennyi volt...


                        foreach(FTLCalcTask calcTask2 in  calcTaskList2)
                        {
                            //van-e eredmény?
                            var calcTour2 = calcTask2.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).FirstOrDefault();
                            if (calcTour2 != null)
                            {
                                //megkeressük a tételt a RES-ben és beírjuk az eredménylistát.
                                var calcTaskOri = calcTaskList.Where( i=>i.Task.TaskID ==calcTask2.Task.TaskID).FirstOrDefault();
                                if(calcTaskOri != null)
                                {

                                    //Ha az teljesítő jármű előző túráiban visszatérés van, akkor megszűntetni a visszatérést
                                    //
                                    var prevCalcRetTasks = calcTaskList.Where(i => i.Task.TaskID != calcTask2.Task.TaskID &&
                                                    i.CalcTours.Where(x => x.Status == FTLCalcTour.FTLCalcTourStatus.OK &&
                                                                      x.Truck.TruckID == calcTour2.Truck.TruckID && 
                                                                      !x.Truck.CurrIsOneWay).Count() > 0);
                                    foreach (var pct in prevCalcRetTasks)
                                    {
                                        var OriCalcTour = pct.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).FirstOrDefault();
                                        if (OriCalcTour != null && !OriCalcTour.Truck.CurrIsOneWay)
                                        {
                                            OriCalcTour.RetM = 0;
                                            OriCalcTour.RetToll = 0;
                                            OriCalcTour.RetCost = 0;
                                            OriCalcTour.RetDuration = 0;
                                            OriCalcTour.RetStart = OriCalcTour.T2End;
                                            OriCalcTour.RetEnd = OriCalcTour.T2End;
                                            OriCalcTour.RetCalcRoute = new FTLCalcRoute();
                                        }
                                    }

                                    //Beírjuk a túrateljesíjtést
                                    calcTaskOri.CalcTours = calcTask2.CalcTours;

                                }
                            }
                        }




                    }
                    else
                    {
                        //Ha nincs eredmény (Status == RESULT), akkor felveszünk egy hibatételt és kilépünk
                        FTLResErrMsg rm = new FTLResErrMsg();
                        rm.Field = "";
                        rm.Message = FTLMessages.E_ERRINSECONDPHASE;
                        FTLResult resErr = new FTLResult()
                        {
                            Status = FTLResult.FTLResultStatus.ERROR,
                            ObjectName = "",
                            ItemID = "",
                            Data = rm

                        };
                        res2.Add(resErr);
                        return res2;
                    }
                        


                }

            }

            return res;
        }




        public static void FTLSetBestTruck(List<FTLResult> p_calcResult)
        {
            //1. kiszámoljuk az teljesitéseket

            //Eredmény megállapítása
            //2. minden járműhöz hozzárendeljuk azt a túrát, amely teljesítésében a legkisebb az átállás+visszaérkezés költsége

            //2.1 Van-e eredmény ?
            var calcResult = p_calcResult.Where(i => i.Status == FTLResult.FTLResultStatus.RESULT).FirstOrDefault();
            if (calcResult != null)
            {
                List<FTLCalcTask> calcTaskList = ((List<FTLCalcTask>)calcResult.Data);
                /*
                //init:kitöröljük az összes ERR státuszú járművet
                foreach (var ct in calcTaskList)
                {
                    ct.CalcTours.RemoveAll(i => i.Status != FTLCalcTour.FTLCalcTourStatus.OK);
                }
                */
                //2.2 végigmenni a taskok listáján
                foreach (var calcTask in calcTaskList)
                {
                    //2.3 A hozzárendelendő jármű megállapítása
                    FTLTruck trk = null;
                    bool done = false;
                    while (!done && trk == null)
                    {
                        //2.3.1 Alapesetben a legjobb rank-ú
                        FTLCalcTour calcTour = calcTask.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).OrderBy(o => o.Rank).FirstOrDefault();
                        if (calcTour != null)
                        {
                            trk = calcTour.Truck;

                            //Amennyiben van hozzárendelhető jármű, megnézzük, hogy más taskban szerepel-e jobb eredménnyel?
                            foreach (var calcTask2 in calcTaskList.Where(i => i != calcTask).ToList())
                            {
                                //Ha a kérdéses jármű másutt is első, de jobba a költségmutatói, akkor 
                                //nem választjuk ki, és a következő ciklusban a sorban következő lesz a hozzárendelt járművet vesszük

                                //Az első jármű lekérdezése
                                FTLCalcTour calcTour2 = calcTask2.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK)
                                                                           .OrderBy(o => o.Rank).FirstOrDefault();

                                if (calcTour2 != null && trk == calcTour2.Truck && calcTour.RelCost + calcTour.RetCost > calcTour2.RelCost + calcTour2.RetCost)
                                {
                                    calcTask.CalcTours.Where(i => i.Truck == trk && i.Status == FTLCalcTour.FTLCalcTourStatus.OK).Select(c => { c.Status = FTLCalcTour.FTLCalcTourStatus.ERR; c.Msg.Add(FTLMessages.E_OTHERTASK); return c; }).ToList();
                                    //calcTask.CalcTours.RemoveAll(i => i.Truck == trk);
                                    trk = null;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            //nincs több választható jármű, ciklus vége
                            done = true;
                        }
                    }

                    if (trk != null)
                    {
                        //a taskhoz lehetett járművet rendelni
                        //3.1 az aktuális taskból kitörlünk minden más járművet

                        // calcTask.CalcTours.RemoveAll(i => i.Truck != trk);
                        calcTask.CalcTours.Where(i => i.Truck != trk && i.Status == FTLCalcTour.FTLCalcTourStatus.OK).Select(c => { c.Status = FTLCalcTour.FTLCalcTourStatus.ERR; c.Msg.Add(FTLMessages.E_NOTASK); return c; }).ToList();

                        //A többi taskból pedig a kiválasztott járművet töröljük
                        foreach (var ct in calcTaskList.Where(i => i != calcTask).ToList())
                        {
                            // ct.CalcTours.RemoveAll(i => i.Truck == trk);
                            ct.CalcTours.Where(i => i.Truck == trk && i.Status == FTLCalcTour.FTLCalcTourStatus.OK).Select(c => { c.Status = FTLCalcTour.FTLCalcTourStatus.ERR; c.Msg.Add(FTLMessages.E_OTHERTASK); return c; }).ToList();
                        }
                    }
                    else
                    {
                        // calcTask.CalcTours.Clear();
                        calcTask.CalcTours.Where(i => i.Status == FTLCalcTour.FTLCalcTourStatus.OK).Select(c => { c.Status = FTLCalcTour.FTLCalcTourStatus.ERR; c.Msg.Add(FTLMessages.E_NOTASK); return c; }).ToList();


                    }
                }
            }
        }

        public static List<FTLTruck> FTLGenerateTrucksFromCalcTours(List<FTLResult> p_calcResult)
        {
            List<FTLTruck> res = new List<FTLTruck>();
            var calcResult = p_calcResult.Where(i => i.Status == FTLResult.FTLResultStatus.RESULT).FirstOrDefault();
            if (calcResult != null)
            {
                List<FTLCalcTask> calcTaskList = ((List<FTLCalcTask>)calcResult.Data);
                foreach (var calcTask in calcTaskList)
                {
                    foreach (var ct in calcTask.CalcTours.Where( i=>i.Status == FTLCalcTour.FTLCalcTourStatus.OK))
                    {
                        FTLTruck trk = ct.Truck.ShallowCopy();
                        //A túrapontokhoz hozzáadjuk a tervezett túrapontokat (a visszatérést nem!)
                        trk.CurrTPoints.Add(ct.RelCalcRoute.TPoint);                    //átállás
                        trk.CurrTPoints.AddRange(ct.T2CalcRoute.Select(i => i.TPoint));

                        trk.TruckTaskType = FTLTruck.eTruckTaskType.Planned;
                        if (trk.CurrTPoints.Count > 0)
                        {
                            trk.CurrLat = trk.CurrTPoints.Last().Lat;
                            trk.CurrLng = trk.CurrTPoints.Last().Lng;
                            trk.CurrTime = ct.T2End;
                        }
                        res.Add(trk);
                    }
                }
            }
            return res;
        }

        private static int FTLGetNearestReachableNOD_IDForTruck(bllRoute p_route, PointLatLng p_pt, string p_RZN_ID_LIST, int p_approach)
        {
            int NOD_ID = 0;
            if (PMapCommonVars.Instance.TruckNod_IDCahce.ContainsKey(Tuple.Create(p_pt, p_RZN_ID_LIST)))
            {
                NOD_ID = PMapCommonVars.Instance.TruckNod_IDCahce[Tuple.Create(p_pt, p_RZN_ID_LIST)];
            }
            else
            {
                NOD_ID = p_route.GetNearestReachableNOD_IDForTruck(p_pt, p_RZN_ID_LIST, p_approach);
                if( NOD_ID != 0)
                    PMapCommonVars.Instance.TruckNod_IDCahce.Add(Tuple.Create(p_pt, p_RZN_ID_LIST), NOD_ID);
            }
            return NOD_ID;
        }
    }
}
