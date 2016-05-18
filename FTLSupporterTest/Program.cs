using FTLSupporter;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTLSupporterTest
{
    class Program
    {
        static void Main(string[] args)
        {

            #region túraponok 
            FTLPoint tp1  =  new FTLPoint()
            {
                TPID = "TP001",
                Name = "Győr",
                Addr = "Szalmatelep",
                Open =  DateTime.Now.Date.AddHours(6),
                Close =  DateTime.Now.Date.AddHours(20),
                SrvDuration = 20,
                Lat = 47.6764844,
                Lng = 17.6660156,
                RealArrival = DateTime.MinValue
            };

            FTLPoint tp2  =  new FTLPoint()
            {
                TPID = "TP002",
                Name = "Székesfehérvár",
                Addr = "Új váralja sor",
                Open =  DateTime.Now.Date.AddHours(6),
                Close =  DateTime.Now.Date.AddHours(20),
                SrvDuration = 30,
                Lat = 47.1768204,
                Lng = 18.4189224,
                RealArrival = DateTime.MinValue
            };

            FTLPoint tp3  =  new FTLPoint()
            {
                TPID = "TP003",
                Name = "Budapest M5",
                Addr = "M5 Tesco",
                Open =  DateTime.Now.Date.AddHours(6),
                Close =  DateTime.Now.Date.AddHours(22),
                SrvDuration = 15,
                Lat = 47.4264324,       //47.4254452,
                Lng = 19.1512299,       // 19.1494274,
                RealArrival = DateTime.MinValue
            };

            FTLPoint tp4  =  new FTLPoint()
            {
                TPID = "TP004",
                Name = "Budapest belváros",
                Addr = "Városház u.",
                Open =  DateTime.Now.Date.AddHours(6),
                Close =  DateTime.Now.Date.AddHours(20),
                SrvDuration = 15,
                Lat = 47.4937477,
                Lng = 19.0563869,
                RealArrival = DateTime.MinValue
            };


            FTLPoint tp5 = new FTLPoint()
            {
                TPID = "TP005",
                Name = "Hatvan",
                Addr = "Bercsényi u.",
                Open = DateTime.Now.Date.AddHours(6),
                Close = DateTime.Now.Date.AddHours(20),
                SrvDuration = 15,
                Lat = 47.6711096,
                Lng = 19.6692610,
                RealArrival = DateTime.MinValue
            };

            FTLPoint tp6 = new FTLPoint()
            {
                TPID = "TP006",
                Name = "Debrecen",
                Addr = "Szoboszlói u.",
                Open = DateTime.Now.Date.AddHours(6),
                Close = DateTime.Now.Date.AddHours(20),
                SrvDuration = 15,
                Lat = 47.5144182,
                Lng = 21.6061592,
                RealArrival = DateTime.MinValue
            };

            FTLPoint tp7 = new FTLPoint()
            {
                TPID = "TP007",
                Name = "Nyíregyháza",
                Addr = "Huszár sor",
                Open = DateTime.Now.Date.AddHours(6),
                Close = DateTime.Now.Date.AddHours(20),
                SrvDuration = 15,
                Lat = 47.9415894,
                Lng = 21.7126322,
                RealArrival = DateTime.MinValue
            };
            
            FTLPoint tp8 = new FTLPoint()
            {
                TPID = "TP008",
                Name = "Kecskemét",
                Addr = "Mercedes gyár",
                Open = DateTime.Now.Date.AddHours(6),
                Close = DateTime.Now.Date.AddHours(20),
                SrvDuration = 15,
                Lat = 46.8733652,
                Lng = 19.7151375,
                RealArrival = DateTime.MinValue
            };

            FTLPoint tp9 = new FTLPoint()
            {
                TPID = "TP009",
                Name = "Szeged",
                Addr = "Rókusi krt.",
                Open = DateTime.Now.Date.AddHours(6),
                Close = DateTime.Now.Date.AddHours(20),
                SrvDuration = 15,
                Lat = 46.2652228,
                Lng = 20.1315880,
                RealArrival = DateTime.MinValue
            };

            #endregion 

            #region beosztandó szállítási feladatok

            FTLTask tsk1 = new FTLTask()
            {
                TaskID = "TSK1",
                CargoType="Száraz",
                TruckTypes="Hűtős,Egyéb",
                Weight = 100,
                Client = "Budapest belváros-Hatvan-Debrecen",
                TPoints = new List<FTLPoint>()
            };
            tsk1.TPoints.Add(tp4.ShallowCopy());
            tsk1.TPoints.Add(tp5.ShallowCopy());
            tsk1.TPoints.Add(tp6.ShallowCopy());

            FTLTask tsk2 = new FTLTask()
            {
                TaskID = "TSK2",
                CargoType = "Romlandó",
                TruckTypes = "Hűtős",
                Weight = 100,
                Client = "Debrecen-Nyíregyháza",
                TPoints = new List<FTLPoint>()
            };
            tsk2.TPoints.Add(tp6.ShallowCopy());
            tsk2.TPoints.Add(tp7.ShallowCopy());

            FTLTask tsk3 = new FTLTask()
            {
                TaskID = "TSK3",
                CargoType = "Száraz",
                TruckTypes = "Hűtős",
                Weight = 100,
                Client = "Nyíregyháza-Hatvan-Budapest M5",
                TPoints = new List<FTLPoint>()
            };
            tsk3.TPoints.Add(tp7.ShallowCopy());
            tsk3.TPoints.Add(tp5.ShallowCopy());
            tsk3.TPoints.Add(tp3.ShallowCopy());
            #endregion

            #region járművek és futó szállítási feladatok

            /*Szabad jármű, Gyáli tartózkodással */
            FTLTruck trk1 = new FTLTruck()
            {
                TruckID = "TRK1 Gyál",
                GVWR = 20000,
                Capacity = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Száraz,Romlandó",
                EngineEuro = 2,
                FixCost = 10000,
                KMCost = 50,
                RelocateCost = 500,
                MaxKM = 9999,
                MaxDuration = 9999,
                TruckTaskType = FTLTruck.eTruckTaskType.Available,
                RunningTaskID = "",
                CurrIsOneWay = false,
                CurrTime = DateTime.Now.Date.AddHours(7),
                CurrLat = 47.3844618,
                CurrLng = 19.2114830,
                CurrTPoints = new List<FTLPoint>()
            };


            /*Szeged-Kecskemét-Budapest tervezett */
            /*Indulás 7:00, KKMét:9:00, Bp:11:00 */
            FTLTruck trk2 = new FTLTruck()
            {
                TruckID = "TRK2 Szeged-Kecskemét-Budapest",
                GVWR = 3500,
                Capacity = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Száraz",
                EngineEuro = 2,
                FixCost = 10000,
                KMCost = 50,
                RelocateCost = 500,
                MaxKM = 9999,
                MaxDuration = 9999,
                TruckTaskType = FTLTruck.eTruckTaskType.Planned,
                RunningTaskID = "",
                CurrIsOneWay = false,
                CurrTPoints = new List<FTLPoint>()
            };

            var tpx1 = tp9.ShallowCopy();
            tpx1.RealArrival = DateTime.Now.Date.AddHours(7);
            trk2.CurrTPoints.Add(tpx1);
            var tpx2 = tp8.ShallowCopy();
            tpx2.RealArrival = DateTime.Now.Date.AddHours(9);
            trk2.CurrTPoints.Add(tpx2);
            var tpx3 = tp3.ShallowCopy();
            tpx3.RealArrival = DateTime.Now.Date.AddHours(11);
            trk2.CurrTPoints.Add(tpx3);


            /*Kecskemét-Budapest-Hatvan futó */
            /*KKMét:8:00, Bp:12:00, Hatvan:14:00 */
            FTLTruck trk3 = new FTLTruck()
            {
                TruckID = "TRK3 Kecskemét-Budapest-Hatvan",
                GVWR = 3500,
                Capacity = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Nemlétező típus",
                EngineEuro = 2,
                FixCost = 10000,
                KMCost = 50,
                RelocateCost = 500,
                MaxKM = 9999,
                MaxDuration = 9999,
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                RunningTaskID = "",
                CurrIsOneWay = false,
                CurrTPoints = new List<FTLPoint>(),
                TPointCompleted = 1,                         /* Kecskemét teljesítve */
                    CurrTime = DateTime.Now.Date.AddHours(9),       //9:00-kor tart itt
                    CurrLat = 47.047533,                     
                    CurrLng = 19.557893,
            };

            var tpx5 = tp8.ShallowCopy();
            tpx5.RealArrival = DateTime.Now.Date.AddHours(8);
            trk3.CurrTPoints.Add(tpx5);
            var tpx6 = tp4.ShallowCopy();
            tpx6.RealArrival = DateTime.Now.Date.AddHours(12);
            trk3.CurrTPoints.Add(tpx6);
            var tpx7 = tp5.ShallowCopy();
            tpx7.RealArrival = DateTime.Now.Date.AddHours(14);
            trk3.CurrTPoints.Add(tpx7);


            /*Kecskemét-Szeged (várunk egy kicsit)-Debrecen futó */
            /*KKMét:4:00, Szeged:06:00, Debrecen:?? */
            FTLTruck trk4 = new FTLTruck()
            {
                TruckID = "TRK4 Kecskemét-Szeged-Debrecen 12T",
                GVWR = 12000,
                Capacity = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Száraz",
                EngineEuro = 2,
                FixCost = 10000,
                KMCost = 50,
                RelocateCost = 500,
                MaxKM = 9999,
                MaxDuration = 9999,
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                RunningTaskID = "",
                CurrIsOneWay = false,
                CurrTPoints = new List<FTLPoint>(),
                TPointCompleted = 1,                             /* Kecskemét már teljesítve van */
                CurrTime = DateTime.Now.Date.AddHours(5),       //05:00-kor tart Szeged határában
                CurrLat = 46.2737422,
                CurrLng = 20.0910293,
            };

            var tpx8 = tp8.ShallowCopy();                       //Kecskemét
            tpx8.RealArrival = DateTime.Now.Date.AddHours(4);
            trk4.CurrTPoints.Add(tpx8);

            var tpx9 = tp9.ShallowCopy();                       //Szeged
            tpx9.RealArrival = DateTime.Now.Date.AddHours(7);
            trk4.CurrTPoints.Add(tpx9);

            var tpx10 = tp6.ShallowCopy();                      //Debrecen
            tpx10.RealArrival = DateTime.Now.Date.AddHours(12);
            trk4.CurrTPoints.Add(tpx10);

            #endregion

            var lstTsk = new List<FTLTask> { tsk1, tsk2, tsk3 };
            var lstTrk = new List<FTLTruck> { trk1, trk2, trk3, trk4 };

            //var lstTsk = new List<FTLTask> { tsk3 };
            //var lstTrk = new List<FTLTruck> { trk4 };

            var res = FTLInterface.FTLSupport(lstTsk, lstTrk, "", "DB0", true);


            int i = 1;
            foreach (var rr in res)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("VISSZATÉRÉSI ÉRTÉK " + i++.ToString() + "/" + res.Count.ToString());
                Console.WriteLine("Status     :" + rr.Status);
                Console.WriteLine("Objektumnév:" + rr.ObjectName);
                Console.WriteLine("Elem ID    :" + rr.ItemID);
                if (rr.Data != null)
                    Console.WriteLine("Adat       :" + rr.Data.ToString());       //OK esetén az eredmények listája


                if (rr.Status == FTLResult.FTLResultStatus.RESULT)
                {
                    List<FTLCalcTask> tskResult = (List<FTLCalcTask>)rr.Data;
                    foreach (FTLCalcTask clctsk in  tskResult)
                    {

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("");
                        Console.WriteLine("Feladat:{0}, Megbízó:{1}", clctsk.Task.TaskID, clctsk.Task.Client);
                        foreach (FTLCalcTour clctour in clctsk.CalcTours)
                        {
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Státusz:{0}, Helyezés:{1}, Jármű:{2}, Jármű állapot:{3}", clctour.Status, clctour.Rank, clctour.Truck.TruckID, clctour.Truck.TruckTaskType);

                            if (clctour.Status == FTLCalcTour.FTLCalcTourStatus.OK)
                            {
                                //Részletező

                                //Aktuális túra
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                if (clctour.Truck.TruckTaskType !=  FTLTruck.eTruckTaskType.Available )
                                {
                                    Console.WriteLine("Aktuális túra kezd:{0},bef:{1}", clctour.T1Start, clctour.T1End);
                                    Console.WriteLine(" táv.:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}, időtartam:{3:#,#0.00}", clctour.T1M, clctour.T1Toll, clctour.T1Cost, clctour.T1Duration);

                                    foreach (FTLCalcRoute clcroute in clctour.T1CalcRoute)
                                    {
                                        Console.WriteLine("{0} érk:{1},ind:{2},táv:{3:#,#0.00}", clcroute.TPoint != null ? clcroute.TPoint.Name : "**Aktuálsi poz.**", clcroute.Arrival, clcroute.Departure, clcroute.Distance);
                                    }
                                }
                                //Átállás
                                Console.WriteLine("Átállás kezd:{0},bef:{1}", clctour.RelStart, clctour.RelEnd);
                                Console.WriteLine(" táv.:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}, időtartam:{3:#,#0.00}", clctour.RelM, clctour.RelToll, clctour.RelCost, clctour.RelDuration);
                                Console.WriteLine("{0} érk:{1},ind:{2},táv:{3:#,#0.00}", clctour.RelCalcRoute.TPoint.Name, clctour.RelCalcRoute.Arrival, clctour.RelCalcRoute.Departure, clctour.RelCalcRoute.Distance);

                                //Beosztandó túra
                                Console.WriteLine("Beosztandó túra kezd:{0},bef:{1}", clctour.T2Start, clctour.T2End);
                                Console.WriteLine(" táv.:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}, időtartam:{3:#,#0.00}", clctour.T2M, clctour.T2Toll, clctour.T2Cost, clctour.T2Duration);

                                foreach (FTLCalcRoute clcroute in clctour.T2CalcRoute)
                                {
                                    Console.WriteLine("{0} érk:{1},ind:{2},táv:{3:#,#0.00}", clcroute.TPoint != null ? clcroute.TPoint.Name : "**Nincs neve**", clcroute.Arrival, clcroute.Departure, clcroute.Distance);
                                }

                                //Visszatérés
                                if (!clctour.Truck.CurrIsOneWay)
                                {
                                    Console.WriteLine("Visszatérés kezd:{0},bef:{1}", clctour.RetStart, clctour.RetEnd);
                                    Console.WriteLine(" táv.:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}, időtartam:{3:#,#0.00}", clctour.RetM, clctour.RetToll, clctour.RetCost, clctour.RetDuration);
                                    Console.WriteLine("{0} érk:{1},ind:{2},táv:{3:#,#0.00}", clctour.RetCalcRoute.TPoint != null ? clctour.RetCalcRoute.TPoint.Name : "**Nincs neve**", clctour.RetCalcRoute.Arrival, clctour.RetCalcRoute.Departure, clctour.RetCalcRoute.Distance);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Hibák:");
                                foreach (var sMsg in clctour.Msg)
                                    Console.WriteLine(sMsg);
                            }
                        }
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");

                }
                else
                {
                    FTLResErrMsg em = (FTLResErrMsg)rr.Data;
                    Console.WriteLine(em.Message);
                    Console.WriteLine(em.CallStack);
                }
            }
            Console.ReadKey();
        }
   }

}

