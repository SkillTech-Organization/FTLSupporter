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
            DateTime dt = DateTime.Now;
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);

                RouteData.Instance.InitFromFiles(p_dir, false);


                int fromNOD_ID = RouteData.Instance.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_fromLat, p_fromLng));
                if (fromNOD_ID == 0)
                {
                    throw new Exception(String.Format("Position can't be matched on map:{0}",
                                      new GMap.NET.PointLatLng(p_fromLat, p_fromLng)));
                }
                int toNOD_ID = RouteData.Instance.GetNearestNOD_ID(new GMap.NET.PointLatLng(p_toLat, p_toLng));
                if (toNOD_ID == 0)
                {
                    throw new Exception(String.Format("Position can't be matched on map:{0}",
                                      new GMap.NET.PointLatLng(p_toLat, p_toLng)));
                }


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

                RouteData.Instance.getNeigboursByBound(routePar, ref NeighborsFull, ref NeighborsCut, boundary);

                PMapRoutingProvider provider = new PMapRoutingProvider();

                boRoute result = provider.GetRoute(fromNOD_ID, toNOD_ID, routePar,
                    NeighborsFull[routePar.Hash], NeighborsCut[routePar.Hash],
                     PMapIniParams.Instance.FastestPath ? ECalcMode.ShortestPath : ECalcMode.FastestPath);

                o_distance = (int)result.CalcDistance;
                o_duration = bllPlanEdit.GetDuration(result.Edges, PMapIniParams.Instance.dicSpeed, Global.defWeather);



                return true;
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                throw;
            }

            //Util.Log2File(">>END:InitPMapRouteData() -->" + sRet);

        }
    }
}
