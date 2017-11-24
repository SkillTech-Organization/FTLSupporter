using PMap.Common;
using PMap.Route;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMRoute
{
    public class RouteFuncs
    {
        public bool CreateMapfile(string p_iniPath, string p_dbConf, string p_file)
        {
            DateTime dt = DateTime.Now;
            try
            {
                PMapIniParams.Instance.ReadParams(p_iniPath, p_dbConf);
                PMapCommonVars.Instance.ConnectToDB();
                RouteData.Instance.Init(PMapCommonVars.Instance.CT_DB, null);
                FileInfo fi = new FileInfo(p_file + ".edg");
                BinarySerializer.Serialize(fi,RouteData.Instance.Edges);
                FileInfo fi2 = new FileInfo(p_file + ".nod");
                BinarySerializer.Serialize(fi2, RouteData.Instance.NodePositions);


                return true;
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
                UI.Error(e.Message);
            }
            return true;

            //Util.Log2File(">>END:InitPMapRouteData() -->" + sRet);

        }

    }
}
