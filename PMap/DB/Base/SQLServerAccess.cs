using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace PMap.DB.Base
{
    class SQLServerAccess : DBAccess
    {
        ///<summary>
        ///Kapcsolat letrehozasa (kapcsolodni kulon kell a Connect-tel)
        ///</summary>
        ///<param name="connection_string">Connection string</param>
        public SQLServerAccess(string connection_string, int p_TimeOut)
            : base(connection_string, p_TimeOut)
        {
            Con = new SqlConnection(connection_string);
            DA = new SqlDataAdapter();
        }

        ///<summary>
        ///Utolso INSERT ID lekerdezese
        ///</summary>
        public override int LastID()
        {
            string s = ExecuteScalar("SELECT   @@IDENTITY").ToString();
            return Convert.ToInt32(s);
        }

        ///<summary>
        ///Rekord létezés lekérdezés
        ///</summary>
        public override bool IsExists(string p_tablename, string p_where)
        {
            string s = ExecuteScalar( String.Format("select case when exists (select top 1 * from {0} where {1}) then 1 else 0 end", p_tablename, p_where)).ToString();
            return Convert.ToInt32(s) == 1;
        }


    }
}
