using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.Common;
using PMapCore.Route;
using SWHInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRoutesUtil
{
    class Program
    {
        private static Dictionary<int, string> m_rdt = null;   //Úttípusok

        private const string dbConf = "DB0";
        private const string RZN_FULL = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15";
        private class DRRoute
        {
            public string addrFrom ;
            public string addrTo ;
            public string RZNList;
            public boRoute route ;
            public string result;

            public double Dist;
            public double Duration;
            public double TollJ2;
            public double TollJ3;
            public double TollJ4;

            public double DistMotorway;
            public double DistMainroad;

        }

        static void Main(string[] args)
        {

            if( string.IsNullOrWhiteSpace( args.FirstOrDefault()))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Használat: DRRoutesUtil [csv file]");
                //Console.ReadKey();
                return;
            }
            try
            {



                PMapIniParams.Instance.ReadParams("", dbConf);
                //   PMapIniParams.Instance.LogVerbose = PMapIniParams.eLogVerbose.nolog;
                var filename = args.FirstOrDefault().ToLower();
                var input = File.ReadAllLines(filename, Encoding.GetEncoding(Global.PM_ENCODING))
                                   .Skip(1)
                                   .Select(a => { Console.WriteLine(a); return a; })
                                   .Select(x => x.Split(';'))
                                   .Select(x =>
                                       new DRRoute()
                                       {
                                           addrFrom = string.Format("{0} {1}", x[1], x[0]),
                                           addrTo = string.Format("{0} {1} {2}", x[3], x[2], x[4]),
                                           RZNList = (!string.IsNullOrWhiteSpace(x[3]) &&  x[3].First() == '1' ? RZN_FULL : ""),
                                           route = new boRoute() { RZN_ID_LIST = "" },
                                           result = ""
                                       }
                                   ).ToList();

                //       var rt = new DRRoute() { addrFrom = "Szeged 46.2425213,20.1716678", addrTo = "Győr" };
                var vbintf = new VBInterface.PMapInterface();

                PMapCommonVars.Instance.ConnectToDB();

                bllRoute route = new bllRoute(PMapCommonVars.Instance.CT_DB);
                foreach (var drItem in input)
                {

                    try
                    {
                        int ZIP_IDFrom = 0;
                        int NOD_IDFrom = 0;
                        int EDG_IDFrom = 0;

                        bool bFoundFrom = true;
                        var geoFrom =
                            input.Where(w => w.route.NOD_ID_FROM != 0 && w.addrFrom == drItem.addrFrom).FirstOrDefault();
                        if (geoFrom == null)
                        {
                            bFoundFrom = route.GeocodingByGoogle(drItem.addrFrom, out ZIP_IDFrom, out NOD_IDFrom, out EDG_IDFrom, false);
                        }
                        else
                        {
                            NOD_IDFrom = geoFrom.route.NOD_ID_FROM;
                        }
                        if (bFoundFrom)
                        {
                            drItem.route.NOD_ID_FROM = NOD_IDFrom;

                            int ZIP_IDTo = 0;
                            int NOD_IDTo = 0;
                            int EDG_IDTo = 0;

                            bool bFoundTo = route.GeocodingByGoogle(drItem.addrTo, out ZIP_IDTo, out NOD_IDTo, out EDG_IDTo, false);
                            if (bFoundTo)
                            {
                                drItem.route.NOD_ID_TO = NOD_IDTo;
                                drItem.route.RZN_ID_LIST = drItem.RZNList;
                                drItem.route.DST_MAXWEIGHT = 40000;
                                //
                            }
                            else
                            {
                                drItem.result = "Érkezés nem geokódolható:" + drItem.addrTo;

                            }
                        }
                        else
                        {
                            drItem.result = "Indulás nem geokódolható" + drItem.addrFrom;
                        }
                    }
                    catch (DuplicatedZIP_NUMException dex)
                    {
                        drItem.result = dex.Message;
                    }
                    catch( Exception ex)
                    {
                        throw;
                    }
                }

                var calcRoutes = input.Where(w => string.IsNullOrWhiteSpace(w.result)).Select(s => s.route).ToList();
                var bCalcRes = PMRouteInterface.GetPMapRoutesSingle(calcRoutes,
                                "", System.Threading.ThreadPriority.Normal, false, true);



                //                double dTollMultiplier = bllPlanEdit.GetTollMultiplier(p_Truck.TRK_ETOLLCAT, p_Truck.TRK_ENGINEEURO);
                m_rdt = route.GetRoadTypesToDict();


                double dTollMultiplier2 = bllPlanEdit.GetTollMultiplier(Global.ETOLLCAT_J2, 4);
                double dTollMultiplier3 = bllPlanEdit.GetTollMultiplier(Global.ETOLLCAT_J3, 4);
                double dTollMultiplier4 = bllPlanEdit.GetTollMultiplier(Global.ETOLLCAT_J4, 4);


                foreach (var drItem in input)
                {
                    if (string.IsNullOrWhiteSpace(drItem.result))
                    {
                        drItem.route = route.GetRouteFromDB(drItem.route.NOD_ID_FROM, drItem.route.NOD_ID_TO, drItem.route.RZN_ID_LIST, drItem.route.DST_MAXWEIGHT, 0, 0);

                        string LastETLCODE = "";
                        foreach (boEdge edge in drItem.route.Edges)
                        {

                            double currSpeed = 0;
                            currSpeed = PMapIniParams.Instance.dicSpeed[edge.RDT_VALUE];

                            drItem.Dist += edge.EDG_LENGTH;
                            drItem.Duration += edge.EDG_LENGTH / (currSpeed / 3.6 * 60 * Global.defWeather);
                            if(edge.RDT_VALUE == 1 || edge.RDT_VALUE == 2)
                            {
                                drItem.DistMotorway += edge.EDG_LENGTH;
                            }

                            if (edge.RDT_VALUE == 3)
                            {
                                drItem.DistMainroad += edge.EDG_LENGTH;
                            }
                            if (LastETLCODE != edge.EDG_ETLCODE)
                            {
                                if (!string.IsNullOrWhiteSpace(edge.EDG_ETLCODE))
                                {
                                    drItem.TollJ2 += edge.Tolls[Global.ETOLLCAT_J2] * dTollMultiplier2;
                                    drItem.TollJ3 += edge.Tolls[Global.ETOLLCAT_J3] * dTollMultiplier3;
                                    drItem.TollJ4 += edge.Tolls[Global.ETOLLCAT_J4] * dTollMultiplier4;
                                }
                                LastETLCODE = edge.EDG_ETLCODE;
                            }
                        }
                        //Indul;Cél;Távolság;Távolság gyorsforgalmi;Távolság főút;Útdíj J2;Útdíj J3;Útdíj J4
                        drItem.result = string.Format("{0};{1};{2};{3};{4};{5};{6};{7}", drItem.addrFrom, drItem.addrTo,
                            drItem.Dist, drItem.DistMotorway, drItem.DistMainroad,
                            Math.Round( drItem.TollJ2), Math.Round(drItem.TollJ3), Math.Round(drItem.TollJ4));
                    }

                }
                var hdr = "Indul;Cél;Távolság;Távolság gyorsforgalmi;Távolság főút;Útdíj J2;Útdíj J3;Útdíj J4";
                var outFileName = filename.Replace(".csv", ".res");
                Util.String2File( hdr + "\n", outFileName, false, Encoding.GetEncoding(Global.PM_ENCODING));
                input.ForEach(item =>
                       Util.String2File( item.result + "\n", outFileName, true, Encoding.GetEncoding(Global.PM_ENCODING))
                );
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nem kezelt kivétel:" + Util.GetExceptionText(ex));
                Console.ReadKey();
                return;


            }

        }
    }
}
