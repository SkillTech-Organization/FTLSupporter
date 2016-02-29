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

                /*              Tervezett feladat esrtén nincs értelmezve
                TimeCurr = DateTime.Now.Date.AddHours(11),                //11:00
                LatCurr = 0,                                              //   valahol Tatabánya környéke
                LngCurr = 0
                */

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

            List<FTLSupporter.FTLResult> res = FTLSupporter.FTLInterface.FTLSupport(tsk, lstTrk, "", "DB0");
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
                if (rr.Status == FTLResult.FTLResultStatus.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    List<FTLCalcTour> clcTours = (List<FTLCalcTour>)rr.Data;
                    foreach (FTLCalcTour clc in clcTours)
                    {
                        FTLTruck trk = lstTrk.Where(t => t.RegNo == clc.RegNo).FirstOrDefault();
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

