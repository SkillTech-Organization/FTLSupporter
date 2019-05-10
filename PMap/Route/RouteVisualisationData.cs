using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.Common;
using PMapCore.BLL;
using PMapCore.Strings;
using PMapCore.LongProcess;
using Map.LongProcess;
using PMapCore.BO;
using PMapCore.BO.DataXChange;

namespace PMapCore.Route
{
    public class RouteVisualisationData
    {
        public static bool FillData(List<boXRouteSection> p_lstRouteSection, int p_TRK_ID, int p_CalcTRK_ETOLLCAT, bool p_GetRouteWithTruckSpeeds, out string sError)
        {
            sError = "";
            if (p_lstRouteSection.Count <= 1)
            {
                sError = PMapMessages.E_ROUTVIS_EMPTYINPUT;
                return false;
            }
            bllRouteVis bllRouteVis;
            bllDepot bllDepot;
            bllTruck bllTruck;

            bllRouteVis = new bllRouteVis(PMapCommonVars.Instance.CT_DB);
            bllDepot = new bllDepot(PMapCommonVars.Instance.CT_DB);
            bllTruck = new bllTruck(PMapCommonVars.Instance.CT_DB);

            //Az utolsó elemre rárakom a finish-t
            p_lstRouteSection.Last().RouteSectionType = boXRouteSection.ERouteSectionType.Finish;

            sError = bllRouteVis.checkIDList(p_lstRouteSection.Select( i=>i.Start_DEP_ID).ToList());
            if (sError != "")
            {
                return false;
            }
            RouteVisCommonVars.Instance.GetRouteWithTruckSpeeds = p_GetRouteWithTruckSpeeds;
            RouteVisCommonVars.Instance.Truck = bllTruck.GetTruck(p_TRK_ID);
            if (RouteVisCommonVars.Instance.Truck == null)
            {
                sError = String.Format(PMapMessages.E_ROUTVIS_MISSINGTRK, p_TRK_ID);
                return false;
            }

            RouteVisCommonVars.Instance.CalcTRK_ETOLLCAT = p_CalcTRK_ETOLLCAT > 0 ? Math.Min(p_CalcTRK_ETOLLCAT, 4) : RouteVisCommonVars.Instance.Truck.TRK_ETOLLCAT;
            List<boDepot> allDepots = bllDepot.GetAllDepots("DEP.ID in (" + string.Join(",", p_lstRouteSection.Select(i => i.Start_DEP_ID).ToArray()) + ") ");

            //átadott ID-k szeritnt összeállítjuk a listát
            RouteVisCommonVars.Instance.lstRouteDepots = new List<RouteVisCommonVars.CRouteDepots>();
            foreach (boXRouteSection rtItem in p_lstRouteSection)
            {
                RouteVisCommonVars.CRouteDepots crDep = new RouteVisCommonVars.CRouteDepots();
                crDep.Depot = allDepots.Find(i => i.ID == rtItem.Start_DEP_ID);
                crDep.RouteSectionType = rtItem.RouteSectionType;
                if (crDep.Depot != null)
                    RouteVisCommonVars.Instance.lstRouteDepots.Add(crDep);
            }



            // RouteData.Instance singleton feltoltese
            InitRouteDataProcess irdp = new InitRouteDataProcess();
            irdp.Run();
            irdp.ProcessForm.ShowDialog();

            //RouteVisCommonVars singleton feltöltese
            RouteVisDataProcess rvdp = new RouteVisDataProcess();
            rvdp.Run();
            rvdp.ProcessForm.ShowDialog();

            return true;
        }

    }
}
