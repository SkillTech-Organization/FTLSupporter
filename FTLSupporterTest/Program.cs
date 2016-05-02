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
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
            };

            FTLPoint tp3  =  new FTLPoint()
            {
                TPID = "TP003",
                Name = "Budapest",
                Addr = "M5 Tesco",
                Open =  DateTime.Now.Date.AddHours(6),
                Close =  DateTime.Now.Date.AddHours(12),
                SrvDuration = 15,
                Lat = 47.4254452,
                Lng = 19.1494274,
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
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
                Arrival = DateTime.MinValue
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

            #endregion

            #region járművek és futó szállítási feladatok

            /*Szabad jármű, Gyáli tartózkodással */
            FTLTruck trk1 = new FTLTruck()
            {
                TruckID = "TRK1 Gyál",
                GVWR = 3500,
                Capacity = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Száraz,Romlandó",
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
            tpx1.Arrival = DateTime.Now.Date.AddHours(7);
            trk2.CurrTPoints.Add(tpx1);
            var tpx2 = tp8.ShallowCopy();
            tpx2.Arrival = DateTime.Now.Date.AddHours(9);
            trk2.CurrTPoints.Add(tpx2);
            var tpx3 = tp3.ShallowCopy();
            tpx3.Arrival = DateTime.Now.Date.AddHours(11);
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
                FixCost = 10000,
                KMCost = 50,
                RelocateCost = 500,
                MaxKM = 9999,
                MaxDuration = 9999,
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                RunningTaskID = "",
                CurrIsOneWay = false,
                CurrTPoints = new List<FTLPoint>(),
                TPointCompleted = 1                         /* Kecskemét teljesítve */
                
            };

            var tpx5 = tp8.ShallowCopy();
            tpx5.Arrival = DateTime.Now.Date.AddHours(8);
            trk3.CurrTPoints.Add(tpx5);
            var tpx6 = tp4.ShallowCopy();
            tpx6.Arrival = DateTime.Now.Date.AddHours(12);
            trk3.CurrTPoints.Add(tpx6);
            var tpx7 = tp5.ShallowCopy();
            tpx7.Arrival = DateTime.Now.Date.AddHours(14);
            trk3.CurrTPoints.Add(tpx7);

            #endregion

            var lstTsk = new List<FTLTask> { tsk1, tsk2};
            var lstTrk = new List<FTLTruck> { trk1, trk2, trk3 };

            var res = FTLInterface.FTLSupport(lstTsk, lstTrk, "", "DB0", true);


        }

        static void Mainx(string[] args)
        {
            #region saját teszt
            /*
            FTLSupporter.FTLTask tsk = new FTLSupporter.FTLTask()
            {
                TaskID = "TSK001",
                Client = "Megbízó 1",
                PartnerNameFrom = "Felrakó 1",
                OpenFrom = DateTime.Now.Date.AddHours(6),                  //Felrakás kezdete időablak reggel 6-tól
                CloseFrom = DateTime.Now.Date.AddHours(20),                   //Felrakás vége időablak 20-ig
                LatFrom = 47.244,                                           //Velenei tó környéke
                LngFrom = 18.628,
                PartnerNameTo = "Lerakó 1",
                OpenTo = DateTime.Now.Date.AddHours(10),                   //Lelrakás kezdete időablak reggel 10-tól
                CloseTo = DateTime.Now.Date.AddHours(23),                     //Lelrakás vége időablak 20-ig
                LatTo = 46.881,                                             //Kecskemét környéke
                LngTo = 19.707,
                CargoType = "Normál",
                Weight = 1000,
                TruckTypes = "Trailer,Hűtős"
            };

            FTLSupporter.FTLTruck trk1 = new FTLSupporter.FTLTruck()
            {
                RegNo = "3,5T",
                TruckWeight = 3500,
                CapacityWeight = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Normál",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,
                EngineEuro = 1,

                // futó túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                TaskID = "Szállítási feladat 1",
                IsOneWay = true,
                TimeFrom = DateTime.Now.Date.AddHours(7),                 //07:00
                LatFrom = 47.665,                                           //valahol Győr környéke
                LngFrom = 17.668,

                TimeTo = DateTime.Now.Date.AddHours(15),                   //15:00 (tervezett időpont)
                LatTo = 47.932,                                           //valahol Nyíregyháza környéke
                LngTo = 21.680,
                TimeUnload = DateTime.Now.Date.AddHours(16),               //16:00

                TimeCurr = DateTime.Now.Date.AddHours(9),                 //09:00
                LatCurr = 47.500,                                          //valahol Tatabánya környéke
                LngCurr = 18.558

            };

            FTLSupporter.FTLTruck trk2 = new FTLSupporter.FTLTruck()
            {
                RegNo = "12Tonna I.",
                TruckWeight = 12000,
                CapacityWeight = 10000,
                TruckType = "Hűtős",
                CargoTypes = "Normál,Extra",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,
                EngineEuro = 2,

                TruckTaskType = FTLTruck.eTruckTaskType.Available,
                TimeCurr = DateTime.Now.Date.AddHours(8),                 //Elérhatőség : 08:00
                LatCurr = 47.391,                                          //Bp, Ócsai u.
                LngCurr = 19.118

            };

            FTLSupporter.FTLTruck trk3 = new FTLSupporter.FTLTruck()
            {
                RegNo = "12Tonna II.",
                TruckWeight = 12000,
                CapacityWeight = 10000,
                TruckType = "Darus",
                CargoTypes = "Normál,Extra",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,
                EngineEuro = 3,


                // trevezett túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Planned,
                TaskID = "Tervezett zállítási feladat 2",
                IsOneWay = true,
                TimeFrom = DateTime.Now.Date.AddHours(16),                 //16:00
                LatFrom = 46.242,                                         //valahol Szeged
                LngFrom = 20.148,

                TimeTo = DateTime.Now.Date.AddHours(22),                  //22:00
                LatTo = 48.668,                                           //valahol Hatvan környéke
                LngTo = 19.668,
                TimeUnload = DateTime.Now.Date.AddHours(23),              //23:00

                //              Tervezett feladat esrtén nincs értelmezve
                //TimeCurr = DateTime.Now.Date.AddHours(11),                //11:00
                //LatCurr = 0,                                              //   valahol Tatabánya környéke
                //LngCurr = 0
                

            };

            //KM miatt nem szerepel az eredményben
            FTLSupporter.FTLTruck trk4 = new FTLSupporter.FTLTruck()
            {
                RegNo = "3,5T kevésKM",
                TruckWeight = 3500,
                CapacityWeight = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Normál",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 100,
                MaxDuration = 0,
                EngineEuro = 1,

                // futó túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                TaskID = "Szállítási feladat kevésKM",
                IsOneWay = true,
                TimeFrom = DateTime.Now.Date.AddHours(10),                 //10:00
                LatFrom = 47.665,                                           //valahol Győr környéke
                LngFrom = 17.668,

                TimeTo = DateTime.Now.Date.AddHours(18),                   //18:00
                LatTo = 47.932,                                           //valahol Nyíregyháza környéke
                LngTo = 21.680,
                TimeUnload = DateTime.Now.Date.AddHours(19),               //19:00

                TimeCurr = DateTime.Now.Date.AddHours(11),                 //11:00
                LatCurr = 47.500,                                          //valahol Tatabánya környéke
                LngCurr = 18.558

            };

            //Max futásidő túllépés miatt nem szerepel az eredményben
            FTLSupporter.FTLTruck trk5 = new FTLSupporter.FTLTruck()
            {
                RegNo = "3,5T kevésFutásidő",
                TruckWeight = 3500,
                CapacityWeight = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Normál",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 100,
                EngineEuro = 1,

                // futó túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                TaskID = "Szállítási feladat (kevésFutásidő)",
                IsOneWay = true,
                TimeFrom = DateTime.Now.Date.AddHours(10),                 //10:00
                LatFrom = 47.665,                                           //valahol Győr környéke
                LngFrom = 17.668,

                TimeTo = DateTime.Now.Date.AddHours(18),                   //18:00
                LatTo = 47.932,                                           //valahol Nyíregyháza környéke
                LngTo = 21.680,
                TimeUnload = DateTime.Now.Date.AddHours(19),               //19:00

                TimeCurr = DateTime.Now.Date.AddHours(11),                 //11:00
                LatCurr = 47.500,                                          //valahol Tatabánya környéke
                LngCurr = 18.558

            };

            FTLSupporter.FTLTruck trk6 = new FTLSupporter.FTLTruck()
            {
                RegNo = "3,5T nem ér oda",
                TruckWeight = 3500,
                CapacityWeight = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Normál",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,
                EngineEuro = 1,

                // futó túra adatok
                TruckTaskType = FTLTruck.eTruckTaskType.Running,
                TaskID = "Szállítási feladat nem ér oda",
                IsOneWay = true,
                TimeFrom = DateTime.Now.Date.AddHours(10),                 //10:00
                LatFrom = 47.665,                                           //valahol Győr környéke
                LngFrom = 17.668,

                TimeTo = DateTime.Now.Date.AddHours(18),                   //18:00 (tervezett időpont)
                LatTo = 47.932,                                           //valahol Nyíregyháza környéke
                LngTo = 21.680,
                TimeUnload = DateTime.Now.Date.AddHours(19),               //19:00

                TimeCurr = DateTime.Now.Date.AddHours(11),                 //09:00
                LatCurr = 47.500,                                          //valahol Tatabánya környéke
                LngCurr = 18.558

            };

            List<FTLSupporter.FTLTruck> lstTrk = new List<FTLSupporter.FTLTruck>();
            lstTrk.Add(trk1);
            lstTrk.Add(trk2);
            lstTrk.Add(trk3);
            lstTrk.Add(trk4);
            lstTrk.Add(trk5);
            lstTrk.Add(trk6);
             List<FTLSupporter.FTLResult> res = FTLSupporter.FTLInterface.FTLSupport(tsk, lstTrk, "", "DB0", true);
           */

            #endregion
            var tsk = new FTLTaskX

            {

                TaskID = "TSK001",
                Client = "Megbízó 1",

                PartnerNameFrom = "Felrakó 1",
                LatFrom = 47.244,
                LngFrom = 18.628,
                OpenFrom = DateTime.Now.Date.AddHours(6),
                CloseFrom = DateTime.Now.Date.AddHours(22),

                PartnerNameTo = "Lerakó 1",
                LatTo = 46.881,
                LngTo = 19.707,
                OpenTo = DateTime.Now.Date.AddHours(12),
                CloseTo = DateTime.Now.Date.AddHours(22),

                CargoType = "Normál",
                Weight = 1000,
                TruckTypes = "Hűtős",
                LoadDuration = 1,
                UnLoadDuration = 1

            };





            var trk1 = new FTLTruckX

            {

                RegNo = "AAA-111",
                TruckWeight = 3500,
                CapacityWeight = 2000,
                TruckType = "Hűtős",
                CargoTypes = "Normál",
                FixCost = 5000,
                KMCost = 65,
                RelocateCost = 55,

                MaxKM = 0,
                MaxDuration = 0,
                TruckTaskType = FTLTruckX.eTruckTaskTypeX.Running,
                TaskID = "Szállítási feladat 1",
                IsOneWay = true,

                TimeFrom = DateTime.Now.Date.AddHours(6),
                LatFrom = 47.665,
                LngFrom = 17.668,


                TimeTo = DateTime.Now.Date.AddHours(18),
                LatTo = 48.407,
                LngTo = 20.852,

                TimeUnload = DateTime.Now.Date.AddHours(19),

                TimeCurr = DateTime.Now.Date.AddHours(8),

                LatCurr = 47.500,
                LngCurr = 18.558

            };



            var trk2 = new FTLTruckX

            {

                RegNo = "BBB-222",
                TruckWeight = 12000,
                CapacityWeight = 10000,
                TruckType = "Hűtős",
                CargoTypes = "Normál,Extra",
                FixCost = 5000,
                KMCost = 65,

                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,
                TruckTaskType = FTLTruckX.eTruckTaskTypeX.Available,
                TimeCurr = DateTime.Now.Date.AddHours(8),

                LatCurr = 47.391,
                LngCurr = 19.118

            };



            var trk3 = new FTLTruckX

            {

                RegNo = "CCC-333",
                TruckWeight = 12000,
                CapacityWeight = 10000,
                TruckType = "Hűtős",
                CargoTypes = "Normál,Extra",
                FixCost = 5000,
                KMCost = 65,

                RelocateCost = 55,
                MaxKM = 0,
                MaxDuration = 0,
                TruckTaskType = FTLTruckX.eTruckTaskTypeX.Planned,
                TaskID = "Tervezett zállítási feladat 2",

                IsOneWay = true,
                TimeFrom = DateTime.Now.Date.AddHours(16),
                LatFrom = 46.242,
                LngFrom = 20.148,
                TimeTo = DateTime.Now.Date.AddHours(22),

                LatTo = 48.668,
                LngTo = 19.668,
                TimeUnload = DateTime.Now.Date.AddHours(23)

            };



            var lstTrk = new List<FTLTruckX> { trk1, trk2, trk3 };

            var res = FTLInterfaceX.FTLSupport(tsk, lstTrk, "", "DB0", true);


            int i = 1;
            foreach (var rr in res)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("VISSZATERESI ERTEK " + i++.ToString() + "/" + res.Count.ToString());

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Status     :" + rr.Status);
                Console.WriteLine("Objektumnév:" + rr.ObjectName);
                Console.WriteLine("Elemsorszám:" + rr.ItemNo.ToString());
                Console.WriteLine("Üzenet     :" + rr.Message);
                if (rr.Data != null)
                    Console.WriteLine("Adat       :" + rr.Data.ToString());       //OK esetén az eredmények listája
                if (rr.Status == FTLResultX.FTLResultStatus.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    List<FTLCalcTourX> clcTours = (List<FTLCalcTourX>)rr.Data;
                    foreach (FTLCalcTourX clc in clcTours)
                    {
                        FTLTruckX trk = lstTrk.Where(t => t.RegNo == clc.RegNo).FirstOrDefault();
                        if (trk != null)
                        {
                            Console.WriteLine("Sorsz:{0}, Jármű:{1}, Száll.feladat ktg:{2}, Időtartam:{3}", clc.Rank, clc.RegNo, clc.AdditionalCost, clc.FullDuration);
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                            Console.Write(" Átállás {0},{1}->{2},{3}\t", trk.LatTo, trk.LngTo, tsk.LatFrom, tsk.LngFrom);
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("hu");
                            Console.WriteLine("KM:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}", clc.RelKm, clc.RelToll, clc.RelCost);

                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                            Console.Write(" Beoszt {0},{1}->{2},{3}\t", tsk.LatFrom, tsk.LngFrom, tsk.LatTo, tsk.LngTo);
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("hu");
                            Console.WriteLine("KM:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}", clc.T2Km, clc.T2Toll, clc.T2Cost);

                            if (!trk.IsOneWay)
                            {
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                                Console.Write(" Vissza {0},{1}->{2},{3}\t", tsk.LatTo, tsk.LngTo, trk.LatFrom, trk.LngFrom);
                                Thread.CurrentThread.CurrentCulture = new CultureInfo("hu");
                                Console.WriteLine("KM:{0:#,#0.00},útdíj:{1:#,#0.00},ktg:{2:#,#0.00}", clc.RetKm, clc.RetToll, clc.RetCost);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ismeretlen jármű:{1}", clc.RegNo);

                        }

                    }


                }

            }
            Console.ReadKey();
        }
    }

}


/*
 *   var tsk = new FTLTask

            {

                TaskID = "TSK001",Client = "Megbízó 1",PartnerNameFrom = "Felrakó 1",OpenFrom = DateTime.Now.Date.AddHours(6),OpenTo = DateTime.Now.Date.AddHours(12),

                LatFrom = 47.244,LngFrom = 18.628,PartnerNameTo = "Lerakó 1",CloseFrom = DateTime.Now.Date.AddHours(10),CloseTo = DateTime.Now.Date.AddHours(18),

                LatTo = 46.881,LngTo = 19.707,CargoType = "Normál",Weight = 1000,TruckTypes = "Hűtős",LoadDuration = 1,UnLoadDuration = 1

            };

 

            var trk1 = new FTLTruck

            {

                RegNo = "AAA-111",TruckWeight = 3500,CapacityWeight = 2000,TruckType = "Hűtős",CargoTypes = "Normál",FixCost = 5000,KMCost = 65,RelocateCost = 55,

                MaxKM = 0,MaxDuration = 0, TruckTaskType = FTLTruck.eTruckTaskType.Running,TaskID = "Szállítási feladat 1",IsOneWay = true,

                TimeFrom = DateTime.Now.Date.AddHours(10),LatFrom = 47.665,LngFrom = 17.668,TimeTo = DateTime.Now.Date.AddHours(18),

                LatTo = 48.407,LngTo = 20.852,TimeUnload = DateTime.Now.Date.AddHours(19),TimeCurr = DateTime.Now.Date.AddHours(11),

                LatCurr = 47.500,LngCurr = 18.558

            };

 

            var trk2 = new FTLTruck

            {

                RegNo = "BBB-222",TruckWeight = 12000,CapacityWeight = 10000,TruckType = "Hűtős",CargoTypes = "Normál,Extra",FixCost = 5000,KMCost = 65,

                RelocateCost = 55,MaxKM = 0,MaxDuration = 0,TruckTaskType = FTLTruck.eTruckTaskType.Available,TimeCurr = DateTime.Now.Date.AddHours(8),

                LatCurr = 47.391,LngCurr = 19.118

            };

 

            var trk3 = new FTLTruck

            {

                RegNo = "CCC-333",TruckWeight = 12000,CapacityWeight = 10000,TruckType = "Hűtős",CargoTypes = "Normál,Extra",FixCost = 5000,KMCost = 65,

                RelocateCost = 55,MaxKM = 0,MaxDuration = 0,TruckTaskType = FTLTruck.eTruckTaskType.Planned,TaskID = "Tervezett zállítási feladat 2",

                IsOneWay = true,TimeFrom = DateTime.Now.Date.AddHours(16),LatFrom = 46.242,LngFrom = 20.148,TimeTo = DateTime.Now.Date.AddHours(22),

                LatTo = 48.668,LngTo = 19.668,TimeUnload = DateTime.Now.Date.AddHours(23)

            };

 */
