using GMap.NET;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using PMap;
using PMap.BLL;
using PMap.BO;
using PMap.Common;
using PMap.MapProvider;
using PMap.Route;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMRoute
{
    public static class RouteFuncs
    {
        public static bool CreateMapfile(string p_iniPath, string p_dbConf, string p_dir)
        {
            DateTime dt = DateTime.Now;
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                PMapCommonVars.Instance.ConnectToDB();
                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);
                foreach( var rr in RouteData.Instance.Edges)
                {
                    rr.Value.EDG_NAME = "";
                    rr.Value.Tolls = null;
                }
                JsonSerializerSettings jsonsettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
                var jsonString = JsonConvert.SerializeObject(RouteData.Instance.Edges, jsonsettings);
                Util.String2File(jsonString, Path.Combine(p_dir , Global.EXTFILE_EDG));
                
    
                jsonString = JsonConvert.SerializeObject(RouteData.Instance.NodePositions, jsonsettings);
                Util.String2File(jsonString, Path.Combine(p_dir , Global.EXTFILE_NOD));

                return true;
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                throw;
                    }

            //Util.Log2File(">>END:InitPMapRouteData() -->" + sRet);

        }


        public static bool GetDistance(string p_iniPath, string p_dbConf, string p_dir,
            double p_fromLat, double p_fromLng, double p_toLat, double p_toLng, 
            string p_RZN_ID_LIST, int p_weight, int p_height, int p_width,
            out int o_distance, out int o_duration)
        {
            /*
                        System.Diagnostics.Trace.TraceInformation("p_iniPath" + p_iniPath);
                        System.Diagnostics.Trace.TraceInformation("p_dbConf" + p_dbConf);
                        System.Diagnostics.Trace.TraceInformation("p_dir" + p_dir);
            */
            /*
            Util.String2File(String.Format("{0}: {1}\n", DateTime.Now.ToString(Global.DATETIMEFORMAT), "p_iniPath:" + p_iniPath), @"D:\home\site\wwwroot\PMRoute\log\GetDistanceStart.log", true);
            Util.String2File(String.Format("{0}: {1}\n", DateTime.Now.ToString(Global.DATETIMEFORMAT), "p_dbConf:" + p_dbConf), @"D:\home\site\wwwroot\PMRoute\log\GetDistanceStart.log", true);
            Util.String2File(String.Format("{0}: {1}\n", DateTime.Now.ToString(Global.DATETIMEFORMAT), "p_dir:" + p_dir), @"D:\home\site\wwwroot\PMRoute\log\GetDistanceStart.log", true);

    */
            PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
            var oriCutMapForRouting = PMapIniParams.Instance.CutMapForRouting;
            PMapIniParams.Instance.CutMapForRouting = false;

            DateTime dt = DateTime.Now;
            try
            {

                Util.Log2File("GetDistance() mmry:" + GC.GetTotalMemory(false).ToString(), false);

                RouteData.Instance.InitFromFiles(p_dir, false);

//var os = Util.GetObjectSize(RouteData.Instance.Edges);

                Util.Log2File("GetNearestNOD_ID:" + new GMap.NET.PointLatLng(p_fromLat, p_fromLng).ToString(), false);
                int fromNOD_ID = RouteData.Instance.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_fromLat, p_fromLng));
                if (fromNOD_ID == 0)
                {

                    var exc =  new Exception(String.Format("Position can't be matched on map:{0}",
                                      new GMap.NET.PointLatLng(p_fromLat, p_fromLng)));

                    Util.ExceptionLog(exc);
                    throw exc;

                }

                Util.Log2File("GetNearestNOD_ID:" + new GMap.NET.PointLatLng(p_toLat, p_toLng).ToString(), false);
                int toNOD_ID = RouteData.Instance.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_toLat, p_toLng));
                if (toNOD_ID == 0)
                {
                    var exc = new Exception(String.Format("Position can't be matched on map:{0}",
                                      new GMap.NET.PointLatLng(p_toLat, p_toLng)));
                    Util.ExceptionLog(exc);
                    throw exc;
                }


                Util.Log2File("fromNOD_ID" + fromNOD_ID.ToString() + ", toNOD_ID" + toNOD_ID.ToString(), false);

                RectLatLng boundary = bllRoute.getBoundary(p_fromLat, p_fromLng, p_toLat, p_toLng);

                Dictionary<string, List<int>[]> NeighborsFull = null;
                Dictionary<string, List<int>[]> NeighborsCut = null;
                CRoutePars routePar = new CRoutePars()
                {
                    RZN_ID_LIST = p_RZN_ID_LIST,
                    Weight = p_weight,
                    Height = p_height,
                    Width = p_width
                };

                RouteData.Instance.getNeigboursByBound(routePar, ref NeighborsFull, ref NeighborsCut, boundary, null);
//var os2 = Util.GetObjectSize(NeighborsFull);
//var os3 = Util.GetObjectSize(NeighborsCut);

                PMapRoutingProvider provider = new PMapRoutingProvider();

                boRoute result = provider.GetRoute(fromNOD_ID, toNOD_ID, routePar,
                    NeighborsFull[routePar.Hash],
                    PMapIniParams.Instance.CutMapForRouting ? NeighborsCut[routePar.Hash] : null,
                     PMapIniParams.Instance.FastestPath ? ECalcMode.ShortestPath : ECalcMode.FastestPath);

                o_distance = (int)result.CalcDistance;
                o_duration = bllPlanEdit.GetDuration(result.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);

                Util.Log2File("o_distance" + o_distance.ToString() + ", o_duration" + o_duration.ToString() + ", mmry:" + GC.GetTotalMemory(false).ToString(), false);


                return true;
            }
           catch (Exception e)
            {
                Util.ExceptionLog(e);
                throw;
            }
            finally
            {
                PMapIniParams.Instance.CutMapForRouting = oriCutMapForRouting;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //Util.Log2File(">>END:InitPMapRouteData() -->" + sRet);

        }
    }
}
