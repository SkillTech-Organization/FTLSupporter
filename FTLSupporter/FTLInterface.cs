﻿using PMap;
using PMap.BLL;
using PMap.BO;
using PMap.Common;
using PMap.DB.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public static class FTLInterface
    {

        public static List<FTLResult> FTLSupport(FTLTask p_Task, List<FTLTruck> p_TruckList, List<FTLRunningTask> p_RunningTaskList, string p_iniPath, string p_dbConf)
        {

            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);

            bool bValid = true;
            List<FTLResult> result = new List<FTLResult>();

            //Paraméterek validálása
            List<ObjectValidator.ValidationError> tskErros = ObjectValidator.ValidateObject(p_Task);
            if (tskErros.Count != 0)
            {
                bValid = false;
                foreach (var err in tskErros)
                {
                    FTLResult itemRes = new FTLResult()
                    {
                        Object = "TASK",
                        Item = 0,
                        Field = err.Field,
                        Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                        Message = err.Message

                    };
                    result.Add(itemRes);
                }
            }
            foreach (FTLTruck trk in p_TruckList)
            {
                List<ObjectValidator.ValidationError> trkErros = ObjectValidator.ValidateObject(trk);
                if (trkErros.Count != 0)
                {
                    bValid = false;
                    int item = 0;
                    foreach (var err in trkErros)
                    {
                        FTLResult itemRes = new FTLResult()
                        {
                            Object = "TRUCK",
                            Item = item++,
                            Field = err.Field,
                            Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
                            Message = err.Message

                        };
                        result.Add(itemRes);
                    }
                }
            }

            foreach (FTLRunningTask rtsk in p_RunningTaskList)
            {
                List<ObjectValidator.ValidationError> trkErros = ObjectValidator.ValidateObject(rtsk);
                if (trkErros.Count != 0)
                {
                    bValid = false;
                    int item = 0;
                    foreach (var err in trkErros)
                    {
                        FTLResult itemRes = new FTLResult()
                        {
                            Object = "RUNNINGTASK",
                            Item = item++,
                            Field = err.Field,
                            Status = FTLResult.FTLResultStatus.VALIDATIONERROR,
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
                //  1.1: Ha ki van töltve, mely típusú járművek szállíthatják, a megfelelő típusú járművek levállogatása
                //  1.2: Szállíthatja-e jármű az árutípust?
                //  1.3: Járműkapacitás megfelelő ?
                //  1.4: A felrakás megkezdése előtt rendelkezésre áll-e 
                //       1.4.1 : Elérhető a rakodás megkezdésekor
                //       1.4.2 : Nincs a szállítási feladat megkezdésekor futó szállítási feladata
                //
                List<FTLTruck> lstTrucks = p_TruckList.Where(x => (p_Task.TruckTypes.Length >= 0 ?  ("," + p_Task.TruckTypes + ",").IndexOf("," + x.TruckType + ",") >= 0 : true) &&
                                                                ("," + x.CargoTypes + ",").IndexOf("," + p_Task.CargoType + ",") >= 0 &&
                                                                x.CapacityWeight >= p_Task.Weight &&
                                                                x.Available <= p_Task.StartFrom &&
                                                                p_RunningTaskList.FirstOrDefault(r => r.RegNo == x.RegNo && r.TimeFinish < p_Task.StartFrom) == null).ToList();

                //2. A felhasználható futó túraadatok levállogatása
                //  2.1:Csak azok a futó túraadatok szükésgesek, amelyhez tartozó járművek át lettek adva
                List<FTLRunningTask> lstRTask = p_RunningTaskList.Where(x => lstTrucks.FirstOrDefault(t => t.RegNo == x.RegNo) != null).ToList();


                //3.nod ID-k meghatározása
                List<int[]> lstNOD_ID = new List<int[]>();

                //  3.1:Szállítási feladatok
                p_Task.NOD_ID_FROM = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_Task.LatFrom, p_Task.LngFrom));
                p_Task.NOD_ID_TO = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_Task.LatTo, p_Task.LngTo));
                lstNOD_ID.Add(new int[] { p_Task.NOD_ID_FROM, p_Task.NOD_ID_TO});

                //  3.2:Futó túrainformációk
                foreach (FTLRunningTask rtsk in lstRTask)
                {
                    rtsk.NOD_ID_FROM = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(rtsk.LatFrom, rtsk.LngFrom));
                    rtsk.NOD_ID_CURR = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(rtsk.LatCurr, rtsk.LngCurr));
                    rtsk.NOD_ID_TO = route.GetNearestNOD_ID(new GMap.NET.PointLatLng(rtsk.LatTo, rtsk.LngTo));

                    lstNOD_ID.Add(new int[] { rtsk.NOD_ID_FROM, rtsk.NOD_ID_CURR });
                    lstNOD_ID.Add(new int[] { rtsk.NOD_ID_CURR, rtsk.NOD_ID_TO });
                }

                //Járművek behajtási övezeteinek meghatározása
                //
            /*
                       public const int RST_NORESTRICT = 1;    //1:korlátozás nélküli
        public const int RST_MORE12T = 2;       //2:12 tonnánál több
        public const int RST_MAX12T = 3;        //3:max 12 tonna
        public const int RST_MAX75T = 4;        //4:max 7.5 tonna
        public const int RST_MAX35T = 5;        //5.max 3.5 tonna
             */
                foreach (FTLTruck trk in lstTrucks)
                {
                    if (trk.CapacityWeight <= 3500)
                        trk.RST_ID = Global.RST_MAX35T;
                    else if (trk.CapacityWeight <= 7500)
                        trk.RST_ID = Global.RST_MAX75T;
                    else if (trk.CapacityWeight <= 12000)
                        trk.RST_ID = Global.RST_MAX12T;
                    else if (trk.CapacityWeight > 12000)
                        trk.RST_ID = Global.RST_MORE12T;
                    trk.RZN_ID_LIST = route.GetRestZonesByRST_ID(trk.RST_ID);
                }


                //4. legeneráljuk az összes futó túra befejezés és a szállítási feladat felrakás távolságot/menetidőt



                var x1 = lstTrucks.FirstOrDefault(t => t.RegNo == "AAA-111");
                var x2 = lstTrucks.FirstOrDefault(t => t.RegNo == "AAA-111xcx");


                Console.WriteLine(lstTrucks.Count);                                            



                //Eredmény összeállítása
                FTLResult resOK = new FTLResult()
                {
                    Object = "RUNNINGTASK",
                    Item = 0,
                    Field = "",
                    Status = FTLResult.FTLResultStatus.OK,
                    Message = ""

                };

                result.Add(resOK);
            }

            return result;
        }


    }
}
