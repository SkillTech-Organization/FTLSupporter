using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;
using PMap.DB.Base;
using System.Data;
using PMap.Common;
using PMap.Localize;

namespace PMap.BLL
{
    public class bllRouteVis : bllBase
    {
        public bllRouteVis(SQLServerAccess p_DBA)
            : base(p_DBA, "")
        {
        }

        public string checkIDList(List<int> p_lstDepotID)
        {
            string result = "";
            string depIDs = string.Join(",", p_lstDepotID.ToArray());
            string sSql = "select ID from DEP_DEPOT where ID in ( " + depIDs + ")";
            DataTable dt = DBA.Query2DataTable(sSql);
            var linq = (from o in dt.AsEnumerable()
                        orderby o.Field<int>("ID")
                        select Util.getFieldValue<int>(o, "ID"));
            var diff = p_lstDepotID.Except(linq.ToArray());
            string sMissing = string.Join(",", diff.ToArray());
            if (sMissing != "")
                result = String.Format(PMapMessages.E_ROUTVIS_MISSINGDEPOTS, sMissing);

            sSql = "select DEP.ID as ID from DEP_DEPOT DEP " + Environment.NewLine +
                    "left outer join NOD_NODE NOD on NOD.ID = DEP.NOD_ID  " + Environment.NewLine +
                    "where NOD.ID is null and DEP.ID in ( " + depIDs + ")";
            dt = DBA.Query2DataTable(sSql);
            var linq2 = (from o in dt.AsEnumerable()
                         orderby o.Field<int>("ID")
                         select Util.getFieldValue<int>(o, "ID"));
            string sMissingNode = string.Join(",", linq2.ToArray());
            if (sMissingNode != "")
            {
                if (result != "")
                    result += Environment.NewLine;
                result += String.Format(PMapMessages.E_ROUTVIS_MISSINGNODES, sMissing);
            }
            return result;
        }
    }
}
