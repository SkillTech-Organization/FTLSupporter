using GMap.NET;
using PMapCore.BLL;
using PMapCore.BLL.DataXChange;
using PMapCore.BO;
using PMapCore.BO.DataXChange;
using PMapCore.Common;
using PMapCore.Common.Parse;
using PMapCore.Common.PPlan;
using PMapCore.Licence;
using PMapCore.LongProcess;
using PMapCore.LongProcess.Base;
using PMapCore.MapProvider;
using PMapCore.Route;
using PMapCore.Strings;
using SWHInterface.BO;
using SWHInterface.LongProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHInterface
{
    public class PMapInterface
    {
        /// <summary>
        /// Menetlevél ellenőrzés BATCH
        /// </summary>
        /// <param name="p_iniPath"></param>
        /// <param name="p_dbConf"></param>
        /// <param name="p_lstRouteSection"></param>
        /// <param name="p_XTruck"></param>
        /// <returns></returns>
        public List<dtXResult> JourneyFormCheck(string p_iniPath, string p_dbConf, List<boXRouteSection> p_lstRouteSection, boXTruck p_XTruck)
        {
            return JourneyFormCheckX.Process(p_iniPath, p_dbConf, p_lstRouteSection, p_XTruck);
         }
    }
}
