﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PMap.DB.Base;
using PMap.Common;

namespace PMap.BLL
{
    public static class bllMapFormPar
    {

        public static void SaveParameters(int p_PLN_ID, int p_USR_ID, string p_MPP_WINDOW, string p_MPP_DOCK, string p_MPP_PARAM, string p_MPP_TGRID, string p_MPP_PGRID, string p_MPP_UGRID)
        {

            //Egyenlőre nincs tervenkénti paraméterbeállítás
            p_PLN_ID = 0;
            using (TransactionBlock transObj = new TransactionBlock(PMapCommonVars.Instance.CT_DB.DB))
            {
                try
                {

                    string sSQL = "select * from MPP_MAPPLANPAR where PLN_ID = ? and USR_ID = ? ";

                    DataTable dt = PMapCommonVars.Instance.CT_DB.DB.Query2DataTable(sSQL, p_PLN_ID, p_USR_ID);
                    if (dt.Rows.Count == 0)
                    {
                        sSQL = "insert into MPP_MAPPLANPAR (PLN_ID, USR_ID, MPP_WINDOW, MPP_DOCK, MPP_PARAM, MPP_TGRID, MPP_PGRID, MPP_UGRID) " +
                               "values( ?, ?, ?, ?, ?, ?, ?, ?)";
                        PMapCommonVars.Instance.CT_DB.DB.ExecuteNonQuery(sSQL,
                            p_PLN_ID, p_USR_ID, p_MPP_WINDOW, p_MPP_DOCK, p_MPP_PARAM, p_MPP_TGRID, p_MPP_PGRID, p_MPP_UGRID);
                    }
                    else
                    {

                        sSQL = "update MPP_MAPPLANPAR set MPP_WINDOW=?, MPP_DOCK=?, MPP_PARAM=?, MPP_TGRID=?, MPP_PGRID=?, MPP_UGRID=? where PLN_ID = ? and USR_ID=? ";
                        PMapCommonVars.Instance.CT_DB.DB.ExecuteNonQuery(sSQL,
                                p_MPP_WINDOW, p_MPP_DOCK, p_MPP_PARAM, p_MPP_TGRID, p_MPP_PGRID, p_MPP_UGRID, p_PLN_ID, p_USR_ID);
                    }
                }
                catch (Exception e)
                {
                    PMapCommonVars.Instance.CT_DB.DB.Rollback();
                    throw e;
                }
            }
        }

        public static bool RestoreParameters(int p_PLN_ID, int p_USR_ID, out string o_MPP_WINDOW, out string o_MPP_DOCK, out string o_MPP_PARAM, out string o_MPP_TGRID, out string o_MPP_PGRID, out string o_MPP_UGRID)
        {

            //Egyenlőre nincs tervenkénti paraméterbeállítás
            p_PLN_ID = 0;
            try
            {
                string sSQL = "select * from MPP_MAPPLANPAR where PLN_ID = ? and USR_ID = ? ";

                DataTable dt = PMapCommonVars.Instance.CT_DB.DB.Query2DataTable(sSQL, p_PLN_ID, p_USR_ID);
                if (dt.Rows.Count > 0)
                {
                    o_MPP_WINDOW = dt.Rows[0].Field<string>("MPP_WINDOW");
                    o_MPP_DOCK = dt.Rows[0].Field<string>("MPP_DOCK");
                    o_MPP_PARAM = dt.Rows[0].Field<string>("MPP_PARAM");
                    o_MPP_TGRID = dt.Rows[0].Field<string>("MPP_TGRID");
                    o_MPP_PGRID = dt.Rows[0].Field<string>("MPP_PGRID");
                    o_MPP_UGRID = dt.Rows[0].Field<string>("MPP_UGRID");

                    return true;
                }
                else
                {
                    o_MPP_WINDOW = "";
                    o_MPP_DOCK = "";
                    o_MPP_PARAM = "";
                    o_MPP_TGRID = "";
                    o_MPP_PGRID = "";
                    o_MPP_UGRID = "";
                    return false;

                }
            }
            catch (Exception e)
            {
                PMapCommonVars.Instance.CT_DB.DB.Rollback();
                throw e;
            }
        }

        public static void RemoveParameters(int p_PLN_ID, int p_USR_ID)
        {
            //Egyenlőre nincs tervenkénti paraméterbeállítás
            p_PLN_ID = 0;
            string sSQL = "delete MPP_MAPPLANPAR where PLN_ID = ? and USR_ID = ? ";
            PMapCommonVars.Instance.CT_DB.DB.ExecuteNonQuery(sSQL, p_PLN_ID, p_USR_ID);
        }
    }
}
