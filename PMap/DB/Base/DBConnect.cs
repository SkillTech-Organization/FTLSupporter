using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.DB.Base
{
    public class DBConnect : IDisposable
    {


        public DBAccess DB { get; protected set; }

        public string DataSource { get; private set; }
        public string InitialCatalog { get; private set; }
        public string UserID { get; private set; }
        public string Password { get; private set; }
        public int TimeOut { get; private set; }



        public DBConnect(string p_dataSource, string p_initialCatalog, string p_userID, string p_password, int p_TimeOut)
        {
            DataSource = p_dataSource;
            InitialCatalog = p_initialCatalog;
            UserID = p_userID;
            Password = p_password;
            TimeOut = p_TimeOut;
        }

        public void Dispose()
        {
            CloseDB();
        }


        public virtual void ConnectDB()
        {
        }

        public virtual void DBUpdater()
        {
        }

        /// Adatbaziskapcsolat lezarasa
        /// </summary>
        public void CloseDB()
        {
            if (DB != null)
            {
                DB.Close();
                DB = null;
            }
        }

    }
}
