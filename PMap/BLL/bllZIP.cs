using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.DB.Base;
using PMap.BLL.Base;
using PMap.BO;
using System.Data;
using PMap.Common;

namespace PMap.BLL
{
    public class bllZIP : bllBase
    {
        public bllZIP(SQLServerAccess p_DBA)
            : base(p_DBA, "ZIP_ZIPCODE")
        {
        }

        public List<boZIP> GetAllZips(string p_where = "", params object[] p_pars)
        {
            string sSql = "select * from ZIP_ZIPCODE ZIP ";
            if (p_where != "")
                sSql += " where " + p_where;
            DataTable dt = DBA.Query2DataTable(sSql, p_pars);
            var linq = (from o in dt.AsEnumerable()
                        orderby o.Field<int>("ID")
                        select new boZIP()
                {
                    ID = Util.getFieldValue<int>(o, "ID"),
                    ZIP_NUM = Util.getFieldValue<int>(o, "ZIP_NUM"),
                    ZIP_CITY = Util.getFieldValue<string>(o, "ZIP_CITY")
                });


            return linq.ToList();

        }


        public boZIP GetZIPbyNum(int p_ZIP_NUM)
        {
            List<boZIP> res = GetAllZips("ZIP.ZIP_NUM=?", p_ZIP_NUM);
            if (res.Count == 0)
                return null;
            else if (res.Count == 1)
                return res[0];
            else
                throw new DuplicatedZIP_NUMException();
        }
    }
}
