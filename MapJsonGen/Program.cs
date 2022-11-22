using GMap.NET;
using Newtonsoft.Json;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.Route;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapJsonGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Használat: MapJsonGen <<connect string>> <<sebességek>> <<output könyvtár>> ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("   connect string   : SQL szerver connect string");
                    Console.WriteLine("   sebességek (7 db): Autópálya,Autóút,Főútvonal,Mellékút,Egyéb alárendelt,");
                    Console.WriteLine("                      Város(utca),Fel/lehajtók, rámpák");
                    Console.WriteLine("                      pl: 70,60,50,40,35,15,15");
                    Console.WriteLine("   output könyvtár  : generált JSon-ok helye");
                    return;

                }

                var connectString = args.First();

                PMapCommonVars.Instance.CT_DB = new SQLServerAccess();
                PMapCommonVars.Instance.CT_DB.Connect(connectString, 999);
                PMapCommonVars.Instance.CT_DB.Open();

                var dicSpeed = new Dictionary<int, int>();
                var speeds = args[1].Split(',');

                for (int i = 0; i < 7; i++)
                {
                    dicSpeed.Add(i, Convert.ToInt32(speeds[i]));
                }


                CreateMapfile(args[2], dicSpeed);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nem kezelt kivétel:" + Util.GetExceptionText(e));
                throw;
            }
        }


        public static bool CreateMapfile(string p_dir, Dictionary<int, int> p_speeds)
        {
            DateTime dt = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Térkép feltöltése adatbázisból....");
            RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, p_speeds);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("JSon generálás....");
            foreach (var rr in RouteData.Instance.Edges)
            {
                //rr.Value.EDG_NAME = "";
                //    rr.Value.Tolls = null;
            }

            JsonSerializerSettings jsonsettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var jsonString = JsonConvert.SerializeObject(RouteData.Instance.Edges, jsonsettings);
            Util.String2File(jsonString, Path.Combine(p_dir, Global.EXTFILE_EDG), Encoding.UTF8);


            jsonString = JsonConvert.SerializeObject(RouteData.Instance.NodePositions, jsonsettings);
            Util.String2File(jsonString, Path.Combine(p_dir, Global.EXTFILE_NOD), Encoding.UTF8);

            var bllRoute = new PMapCore.BLL.bllRoute(PMapCommonVars.Instance.CT_DB);

            var RZNbyTypes = new Dictionary<int, string>();  //Behajtási zóna ID gyűjtemények
            RZNbyTypes.Add(Global.RST_NORESTRICT, bllRoute.GetRestZonesByRST_ID(Global.RST_NORESTRICT));
            RZNbyTypes.Add(Global.RST_BIGGER12T, bllRoute.GetRestZonesByRST_ID(Global.RST_BIGGER12T));
            RZNbyTypes.Add(Global.RST_MAX12T, bllRoute.GetRestZonesByRST_ID(Global.RST_MAX12T));
            RZNbyTypes.Add(Global.RST_MAX75T, bllRoute.GetRestZonesByRST_ID(Global.RST_MAX75T));
            RZNbyTypes.Add(Global.RST_MAX35T, bllRoute.GetRestZonesByRST_ID(Global.RST_MAX35T));

            jsonString = JsonConvert.SerializeObject(RZNbyTypes, jsonsettings);
            Util.String2File(jsonString, Path.Combine(p_dir, Global.EXTFILE_RZNTyp), Encoding.UTF8);

            var rzn = bllRoute.GetAllRZones();
            jsonString = JsonConvert.SerializeObject(rzn, jsonsettings);
            Util.String2File(jsonString, Path.Combine(p_dir, Global.EXTFILE_RZN), Encoding.UTF8);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kész! Fájlok helye:" + p_dir);
            Console.WriteLine("Tovább bármely billentyűvel");
            Console.ReadKey();
            return true;
        }

    }
}
