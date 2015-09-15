using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.DB.Base
{
    public class SQLServerConnect : DBConnect
    {
        public SQLServerConnect(string p_dataSource, string p_initialCatalog, string p_userID, string p_password, int p_TimeOut)
            : base(p_dataSource, p_initialCatalog, p_userID, p_password, p_TimeOut)
        {
        }

        /// <summary>
        /// Kapcsolodas az adatbazishoz
        /// </summary>
        public override void ConnectDB()
        {
            if (DB != null)
                DB.Close();

            try
            {
                if( UserID == "" && Password == "")
                    DB = new SQLServerAccess(String.Format("Data Source={0};Initial Catalog={1};Trusted_Connection=Yes", DataSource, InitialCatalog), this.TimeOut);
                else
                    DB = new SQLServerAccess(String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", DataSource, InitialCatalog, UserID, Password), this.TimeOut);
               
                    
                    
                DB.Connect();
                
            }

            catch (Exception e)
            {
                throw e;
            }

        }


    }
}
