using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using PMap.Common;
using PMap.Localize;
using PMap.Localize.Base;

namespace PMap.DB.Base
{
    public abstract class DBAccess
    {

        /// <summary>
        /// Konn.string
        /// </summary>
        public string ConnectionString { get; private set; }

        ///<summary>
        ///Kapcsolat
        ///</summary>
        public DbConnection Con { get; protected set; }

        /// <summary>
        /// SQL utasitas
        /// </summary>
        //        private DbCommand Cmd { get; set; }

        /// <summary>
        /// Visszakapott rekordszet
        /// </summary>
        private DbDataReader QueryReader { get; set; }

        /// <summary>
        /// Tranzakcio
        /// </summary>
        public DbTransaction Tran { get; private set; }

        /// <summary>
        /// Adapter 
        /// </summary>
        public DbDataAdapter DA { get; set; }

        public int CommandTimeOut { get; private set; }

        ///<summary>
        ///Inicializalas
        ///</summary>
        public DBAccess(string p_connectionstring, int p_CommandTimeOut)
        {
            ConnectionString = p_connectionstring;
            CommandTimeOut = p_CommandTimeOut;
        }

        ///<summary>
        ///Kapcsolodas
        ///</summary>
        public virtual void Connect()
        {
            Con.Open();
            //Cmd = Con.CreateCommand();
            //Cmd.Connection = Con;
        }

        ///<summary>
        ///A kapcsolat lezarasa
        ///</summary>
        public virtual void Close()
        {
            Con.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsInTran()
        {
            return Tran != null;
        }


        ///<summary>
        ///UPDATE-jellegu utasitas vegrehajtasa
        ///Az SQL szoveg a ?-es megoldassal, a parameterek meg parameterek
        ///</summary>
        ///<param name="sql">SQL szöveg</param>
        ///<param name="param">Paraméterek, vesszővel elválasztva</param>
        public void ExecuteNonQuery(string p_sql, params object[] p_param)
        {
            DateTime dtStart = DateTime.Now;
            DbCommand cmd = PrepareCommand(p_sql, p_param);
            cmd.ExecuteNonQuery();
            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).Ticks));
        }

        ///<summary>
        ///Altalanos SELECT vegrehajtasa
        ///?-es SQL-t kell atadni
        ///</summary>
        ///<param name="sql">SQL szöveg</param>
        ///<param name="param">Paraméterek, vesszővel elválasztva</param>
        public void ExecuteQuery(string p_sql, params object[] p_param)
        {
            DateTime dtStart = DateTime.Now;

            DbCommand sqlcmd = PrepareCommand(p_sql, p_param);
            QueryReader = sqlcmd.ExecuteReader();
            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).Ticks));
        }


        ///<summary>
        ///Egy erteket visszaado SELECT
        ///Az SQL es a parameterek
        ///</summary>
        ///<param name="sql">SQL szöveg</param>
        ///<param name="param">Paraméterek, vesszővel elválasztva</param>
        public object ExecuteScalar(string p_sql, params object[] p_param)
        {
            DateTime dtStart = DateTime.Now;
            DbCommand sqlcmd = PrepareCommand(p_sql, p_param);
            object ret = sqlcmd.ExecuteScalar();
            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).Ticks));
            return ret;
        }

        ///<summary>
        ///Altalanos SELECT vegrehajtasa DataTable-be
        ///</summary>
        ///<param name="sql">SQL szöveg</param>
        ///<param name="param">Paraméterek, vesszővel elválasztva</param>
        public DataTable Query2DataTable(string p_sql, params object[] p_param)
        {
            DateTime dtStart = DateTime.Now;
            DbCommand sqlcmd = PrepareCommand(p_sql, p_param);
            DA.SelectCommand = sqlcmd;
            DataSet d = new DataSet();
            DA.Fill(d);
            DataTable ret = d.Tables[0];
            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).Ticks));

            return ret;
        }



        ///<summary>
        ///Egy sor beolvasa a rekordszetbol
        ///</summary>
        public bool Read()
        {
            bool b = QueryReader.Read();
            if (!b) CloseQuery();
            return b;
        }

        ///<summary>
        ///Lekerdezes lezarasa
        ///</summary>
        public void CloseQuery()
        {
            QueryReader.Close();
        }

        ///<summary>
        ///Adott szamu mezo lekerdezese
        ///</summary>
        ///<param name="field_number">Mezo sorszama</param>
        public object GetField(int p_field_number)
        {
            return QueryReader[p_field_number] == DBNull.Value ? null : QueryReader[p_field_number];
        }

        ///<summary>
        ///Adott nevu mezo lekerdezese
        ///</summary>
        ///<param name="field_number">Mezo neve</param>
        public object GetField(string p_field_name)
        {
            return QueryReader[p_field_name] == DBNull.Value ? null : QueryReader[p_field_name];
        }


        ///<summary>
        ///Tranzakcio megnyitasa
        ///</summary>
        public void BeginTran()
        {
            if (QueryReader != null)
                if (!QueryReader.IsClosed)
                    CloseQuery();

            Tran = Con.BeginTransaction();
        }

        ///<summary>
        ///Tranzakcio veglegesitese
        ///</summary>
        public void Commit()
        {
            if (QueryReader != null)
                if (!QueryReader.IsClosed)
                    CloseQuery();

            if (Tran != null)
                Tran.Commit();
            Tran = null;
        }

        ///<summary>
        ///Tranzakcio visszavonasa
        ///</summary>
        public void Rollback()
        {
            if (QueryReader != null)
                if (!QueryReader.IsClosed)
                    CloseQuery();

            if (Tran != null)
                Tran.Rollback();
            Tran = null;
        }

        ///<summary>
        ///Insert ID lekerdezese alap
        ///</summary>
        public virtual int LastID()
        {
            throw new Exception("LastID should be overridden in derived classes.");
        }


        ///<summary>
        ///Rekord létezés lekérdezés alap
        ///</summary>
        public virtual bool IsExists(string p_tablename, string p_where)
        {
            throw new Exception("IsExists should be overridden in derived classes.");
        }

        ///<summary>
        ///Az aktualis lekerdezest egy listbe teszi, indexe 0-...sorszam, a belso hashtable a mezonevek
        ///pl. ((Hashtable)h[12])["MEZONEV"]
        ///</summary>
        public List<Hashtable> FillQuery()
        {
            List<Hashtable> temp = new List<Hashtable>();

            int i = 0;
            while (Read())
            {
                Hashtable row = new Hashtable();

                for (int j = 0; j < QueryReader.FieldCount; j++)
                    if (!row.ContainsKey(QueryReader.GetName(j)))
                    {
                        Object value = GetField(j);

                        if (value == null)
                            row.Add(QueryReader.GetName(j), DBNull.Value);
                        else
                            if (value.GetType() != typeof(SByte))
                                row.Add(QueryReader.GetName(j), value);
                            else
                                row.Add(QueryReader.GetName(j), (value.ToString() == "1"));
                    }

                temp.Add(row);
                i++;
            }

            return temp;
        }

        /// <summary>
        /// Reszleges insert egy tablaba
        /// </summary>
        /// <param name="tablename">Tabla neve</param>
        /// <param name="param">mezonev, ertek, mezonev, ertek</param>
        /// <returns>Rekordazonosito</returns>
        public int InsertEx(string p_tablename, params object[] p_param)
        {

            String sql1 = "insert into  " + p_tablename + "(";
            String sql2 = "values(";
            object[] paramvalues = new object[p_param.Length / 2];

            for (int i = 0; i < p_param.Length; i += 2)
            {
                sql1 += p_param[i];
                sql2 += "?";
                paramvalues[i / 2] = p_param[i + 1];

                if (i < p_param.Length - 2)
                {
                    sql1 += ",";
                    sql2 += ",";
                }
            }

            sql1 += ") ";
            sql2 += ")";

            ExecuteNonQuery(sql1 + sql2, paramvalues);
            return LastID();
        }

        /// <summary>
        /// Reszleges update-eles adott tablaba
        /// </summary>
        /// <param name="tablename">Tabla neve</param>
        /// <param name="ID">Azonosito</param>
        /// <param name="param">mezonev, ertek, mezonev, ertek...</param>
        public void UpdateEx(string p_tablename, string p_IDName,  int p_id, params object[] p_param)
        {
            String sql = "update " + p_tablename + " set ";
            object[] paramvalues = new object[p_param.Length / 2 + 1];

            for (int i = 0; i < p_param.Length; i += 2)
            {
                sql += p_param[i] + "=?";
                if (i < p_param.Length - 2)
                    sql += ",";
                paramvalues[i / 2] = p_param[i + 1];
            }

            sql += " where "+p_IDName+"=?";
            paramvalues[paramvalues.Length - 1] = p_id;

            ExecuteNonQuery(sql, paramvalues);
        }

        /// <summary>
        /// Egy hashtabla adatait szurja be (FIELDNAME=VALUE parok)
        /// </summary>
        /// <param name="tablename">Tabla neve</param>
        /// <param name="values">Ertekek</param>
        /// <param name="skipid">Kihagyja-e az ID mezot</param>
        /// <returns>Rekordazonosito</returns>
        public int InsertHash(string p_tablename, string IDName, Hashtable p_values, bool p_skipid)
        {
            if (p_skipid)
                if (p_values.ContainsKey(IDName))
                    p_values.Remove(IDName);

            object[] list = new object[p_values.Count * 2];
            IDictionaryEnumerator e = p_values.GetEnumerator();

            int i = 0;

            while (e.MoveNext())
            {
                list[i] = e.Key;
                i++;
                list[i] = e.Value;
                i++;
            }

            return InsertEx(p_tablename, list);
        }

        public void UpdateHash(string p_tablename, string p_IDName, int p_id, Hashtable p_values)
        {
            object[] list = new object[p_values.Count * 2];
            IDictionaryEnumerator e = p_values.GetEnumerator();

            int i = 0;

            while (e.MoveNext())
            {
                list[i] = e.Key;
                i++;
                list[i] = e.Value;
                i++;
            }


            UpdateEx(p_tablename, p_IDName, p_id, list);
        }

        private string SetParameterNames(string p_sql)
        {
            string retVal = "";
            string[] aSql = p_sql.Split('?');
            if (aSql.Length == 1)
                retVal = p_sql;
            else
            {
                for (int i = 0; i < aSql.Length - 1; i++)
                {
                    retVal += aSql[i] + "@P" + i.ToString();
                }
                retVal += aSql[aSql.Length - 1];
            }
            return retVal;
        }

        private DbCommand PrepareCommand(string p_sql, params object[] p_param)
        {
            if (QueryReader != null)
                if (!QueryReader.IsClosed)
                    CloseQuery();
            string sPar = "";
            DbCommand sqlCmd;
            sqlCmd = Con.CreateCommand();
            sqlCmd.Parameters.Clear();
            sqlCmd.CommandText = SetParameterNames(p_sql.Replace("'***************'", "'FormClosedEventArgs01'"));
            for (int i = 0; i < p_param.Length; i++)
            {
                //SqlParameter par = new SqlParameter("@P" + i.ToString(), p_param[i]);
                DbParameter par = sqlCmd.CreateParameter();
                par.ParameterName = "@P" + i.ToString();
                par.Value = p_param[i];
                sqlCmd.Parameters.Add(par);

                if (sPar != "")
                    sPar += Environment.NewLine;
                sPar += par.ParameterName + "=" + par.Value.ToString();
            }
            sqlCmd.Transaction = Tran;

            logSqlInfo(Environment.NewLine + p_sql + Environment.NewLine + "PARAMETERS:" + Environment.NewLine + sPar);

            sqlCmd.CommandTimeout = this.CommandTimeOut;

            return sqlCmd;
        }

        private void logSqlInfo(string p_InfoText)
        {
            if (PMapIniParams.Instance.LogVerbose >= PMapIniParams.eLogVerbose.sql)
            {
                string sLogFileName = Path.Combine(PMapIniParams.Instance.LogDir, Global.DbgSqlFileName);
                Util.Log2File(p_InfoText, sLogFileName, false);
            }
        }
    }
}
