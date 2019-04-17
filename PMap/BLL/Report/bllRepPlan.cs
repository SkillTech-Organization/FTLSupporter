using PMapCore.BLL.Base;
using PMapCore.BO.Report;
using PMapCore.Common;
using PMapCore.DB.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.BLL.Report
{
    public class bllRepPlan : bllBase
    {
        public bllRepPlan(SQLServerAccess p_DBA)
            : base(p_DBA, "")
        {
        }

        public List<boRepPlan> GetRepPlanData( int p_PLN_ID)
        {

           // var sTruck = PMapIniParams.Instance.TruckCode;
            var sTruck = "TRK_REG_NUM";

            string sSql = "; with  " + Environment.NewLine +
                            "CTE_TPL as ( select * from TPL_TRUCKPLAN TPL where TPL.PLN_ID = ?   ),  " + Environment.NewLine +
                            "CTE_TPLX as (" + Environment.NewLine +
                            "select distinct " + sTruck + " as TRUCK, TRK_ID, " + Environment.NewLine +
                            "coalesce(MPO.CustomerOrderNumber, ORD_NUM, '') AS ORD_NUM, ORD.ORD_QTY, " + Environment.NewLine +

                            /*
                            "case when PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " then DEP_NAME else WHS_NAME end AS CLIENT,  " + Environment.NewLine +
                            "case when PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " then isnull(convert(varchar(max), ZIP.ZIP_NUM), '') +' ' + isnull(ZIP.ZIP_CITY, '') + ' ' + DEP_ADRSTREET else '' end AS DEP_ADRSTREET,  " + Environment.NewLine +
                            "case when PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " then DEP_ADRNUM else '' end AS DEP_ADRNUM, " + Environment.NewLine +
                            "case when PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " then DEP_OPEN else 0 end as DEP_OPEN, " + Environment.NewLine +
                            "case when PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " then DEP_CLOSE else 0 end as DEP_CLOSE, " + Environment.NewLine +
                            */

                            "coalesce(MPO.CustomerCode, DEP_NAME, '')  as CLIENT,  " + Environment.NewLine +
                            "isnull(convert(varchar(max), ZIP.ZIP_NUM), '') +' ' + isnull(ZIP.ZIP_CITY, '') + ' ' + DEP_ADRSTREET as DEP_ADRSTREET,  " + Environment.NewLine +
                            "DEP_ADRNUM as DEP_ADRNUM, " + Environment.NewLine +
                            "DEP_OPEN as DEP_OPEN, " + Environment.NewLine +
                            "DEP_CLOSE as DEP_CLOSE, " + Environment.NewLine +

                            "PTP.PTP_ARRTIME, PTP.PTP_TOLL as PTP_TOLLX, PTP_DISTANCE as PTP_DISTANCEX, " + Environment.NewLine +
                            "PTP_TYPE, TPL.PLN_ID as PLN_ID , " + Environment.NewLine +
                            "MPO.Bordero, case when PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " then DEP.NOD_ID else WHS.NOD_ID end AS NODID, PTP_ORDER," + Environment.NewLine +
                            "MPO.ORD_ID," + Environment.NewLine +
                            "MPO.CustomerOrderNumber, MPO.ADR, NOD.NOD_NUM " + Environment.NewLine +
                            "FROM CTE_TPL TPL " + Environment.NewLine +
                            "inner join TRK_TRUCK TRK on TPL.TRK_ID = TRK.ID " + Environment.NewLine +
                            "inner join PTP_PLANTOURPOINT PTP on PTP.TPL_ID = TPL.ID " + Environment.NewLine +
                            "left join TOD_TOURORDER TOD on PTP.TOD_ID = TOD.ID " + Environment.NewLine +
                            "left join DEP_DEPOT DEP on TOD.DEP_ID = DEP.ID " + Environment.NewLine +
                            "left join NOD_NODE NOD on NOD.ID = Dep.NOD_ID " + Environment.NewLine +
                            "left join ZIP_ZIPCODE ZIP on NOD.ZIP_NUM = ZIP.ZIP_NUM " + Environment.NewLine +
                            "left join WHS_WAREHOUSE WHS ON PTP.WHS_ID = WHS.ID " + Environment.NewLine +
                            "left join ORD_ORDER ORD on TOD.ORD_ID = ORD.ID " + Environment.NewLine +
                            "left join MPO_MPORDER MPO on MPO.ORD_ID = ORD.ID " + Environment.NewLine +
                            "left join CRR_CARRIER CRR on CRR.ID = TRK.CRR_ID " + Environment.NewLine +
                            "left join SPP_SPEEDPROF SPP on SPP.ID = TRK.SPP_ID " + Environment.NewLine +
                            "left join CPP_CAPACITYPROF CPP on CPP.ID = TRK.CPP_ID " + Environment.NewLine +
                            "where PTP_TYPE = " + Global.PTP_TYPE_DEP.ToString() + " " + Environment.NewLine +
                            "), " + Environment.NewLine +
                            "CTE as ( select *,	ROW_NUMBER() OVER(PARTITION BY TRUCK  ORDER BY  PTP_ORDER) AS RowNumber " + Environment.NewLine +
                            "from CTE_TPLX )" + Environment.NewLine +
                            "select CTE.* " + Environment.NewLine +
                            ",case when CTEX.TRUCK is null then CTE.PTP_TOLLX else 0 end as PTP_TOLL" + Environment.NewLine +
                            ",case when CTEX.TRUCK is null then CTE.PTP_DISTANCEX else 0 end as PTP_DISTANCE" + Environment.NewLine +
                            ",isnull(stuff " + Environment.NewLine +
                            "   ((SELECT distinct    ',' + RZN.RZN_ZoneCode " + Environment.NewLine +
                            "       FROM         RZN_RESTRZONE RZN " + Environment.NewLine +
                            "       left join EDG_EDGE EDG1 on EDG1.NOD_NUM = CTE.NOD_NUM and EDG1.RZN_ZONECODE = RZN.RZN_ZoneCode " + Environment.NewLine +
                            "       left join EDG_EDGE EDG2 on EDG2.NOD_NUM = CTE.NOD_NUM and EDG2.RZN_ZONECODE = RZN.RZN_ZoneCode " + Environment.NewLine +
                            "       where EDG1.NOD_NUM is not null or EDG2.NOD_NUM is not null " + Environment.NewLine +
                            "   FOR XML PATH('')), 1, 1, ''), '') AS RZN_Code_List " + Environment.NewLine +
                            "from CTE" + Environment.NewLine +
                            "left outer" + Environment.NewLine +
                            "join CTE CTEX on CTEX.TRUCK = CTE.TRUCK and CTEX.PTP_ORDER = CTE.PTP_ORDER and CTEX.RowNumber = CTE.RowNumber - 1" + Environment.NewLine +
                            "ORDER BY CTE.TRUCK, CTE.PTP_ORDER";
            DataTable dt = DBA.Query2DataTable(sSql, p_PLN_ID);
            var linq = (from o in dt.AsEnumerable()
                        orderby o.Field<string>("TRUCK"), o.Field<double>("PTP_ORDER")
                        select new boRepPlan
                        {
                            TRK_ID  = Util.getFieldValue<int>(o, "TRK_ID"),
                            TRUCK  = Util.getFieldValue<string>(o, "TRUCK"),
                            RZN_Code_List = Util.getFieldValue<string>(o, "RZN_Code_List"),
                            PTP_ARRITME = Util.getFieldValue<DateTime>(o, "PTP_ARRTIME"),
                            PTP_TYPE = Util.getFieldValue<int>(o, "PTP_TYPE"),
                            PTP_ORDER = Util.getFieldValue<double>(o, "PTP_ORDER"),
                            ORD_NUM = Util.getFieldValue<string>(o, "ORD_NUM"),
                            ORD_QTY = Util.getFieldValue<double>(o, "ORD_QTY"),
                            CLIENT = Util.getFieldValue<string>(o, "CLIENT"),
                            FullAddr = Util.getFieldValue<string>(o, "DEP_ADRSTREET") + " " + Util.getFieldValue<string>(o, "DEP_ADRNUM"),
                            OPEN = Util.GetTimeStringFromInt(Util.getFieldValue<int>(o, "DEP_OPEN")),
                            CLOSE = Util.GetTimeStringFromInt(Util.getFieldValue<int>(o, "DEP_CLOSE")),
                            PTP_TOLL = Util.getFieldValue<double>(o, "PTP_TOLL"),
                            PTP_DISTANCE = Util.getFieldValue<double>(o, "PTP_DISTANCE"),
                            Bordero = Util.getFieldValue<string>(o, "Bordero"),
                            ADR = Util.getFieldValue<bool>(o, "ADR")
                        });
            return linq.ToList();
        }

    }
}
