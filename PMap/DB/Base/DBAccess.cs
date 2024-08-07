using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using PMapCore.Common;
using PMapCore.Strings;
using PMapCore.Strings.Base;
using System.Reflection;

namespace PMapCore.DB.Base
{
    public abstract class DBAccess<TDbConnection, TDbDataAdapter, TDbTransaction, TDbCommand, TDbParameter> : IDisposable
        where TDbConnection : DbConnection
        where TDbDataAdapter : DbDataAdapter
        where TDbTransaction : DbTransaction
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
    {
        public virtual string SQL_ID { get { return "ID"; } }

        /// <summary>
        /// Konn.string
        /// </summary>
        public string ConnectionString { get; private set; }

        ///<summary>
        ///Kapcsolat
        ///</summary>
        public TDbConnection Conn { get; private set; }

        public TDbDataAdapter DA { get; private set; }

        /// <summary>
        /// Tranzakcio
        /// </summary>
        public TDbTransaction Tran { get; private set; }

        public int CommandTimeOut { get; private set; }

        ///<summary>
        ///Inicializalas
        ///</summary>
        ///
        public DBAccess()
        {
        }


        public DBAccess(TDbConnection p_Con, TDbDataAdapter p_DA, int p_commandTimeOut)
        {
            Conn = p_Con;
            DA = p_DA;
            CommandTimeOut = p_commandTimeOut;
        }

        public DBAccess(string p_connection_string)
        {
            Connect(p_connection_string, 30);
        }

        public DBAccess(string p_connection_string, int p_commandTimeOut)
        {
            Connect(p_connection_string, p_commandTimeOut);     
        }

        ///<summary>
        ///Kapcsolodas
        ///</summary>
        public void Connect(string p_connection_string, int p_commandTimeOut)
        {
            Conn = (TDbConnection)Activator.CreateInstance(typeof(TDbConnection), p_connection_string);
            DA = (TDbDataAdapter)Activator.CreateInstance(typeof(TDbDataAdapter));
            CommandTimeOut = p_commandTimeOut;
        }
        
        public virtual void Open()
        {
            Conn.Open();

            //Cmd = Con.CreateCommand();
            //Cmd.Connection = Con;
        }

        ///<summary>
        ///A kapcsolat lezarasa
        ///</summary>
        public virtual void Close()
        {
            Conn.Close();
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
            TDbCommand cmd = PrepareCommand(p_sql, p_param);
            cmd.ExecuteNonQuery();
            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).ToString()));
 
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
            TDbCommand sqlcmd = PrepareCommand(p_sql, p_param);
            object ret = sqlcmd.ExecuteScalar();
            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).ToString()));
            return ret;
        }

        ///<summary>
        ///Altalanos SELECT vegrehajtasa DataTable-be
        ///</summary>
        ///<param name="sql">SQL szöveg</param>
        ///<param name="param">Paraméterek, vesszővel elválasztva</param>
        public DataTable Query2DataTable(string p_sql, params object[] p_param)
        {
            DataSet dst = new DataSet();

            DateTime dtStart = DateTime.Now;
            TDbCommand sqlcmd = PrepareCommand(p_sql, p_param);
            DA.SelectCommand = sqlcmd;
            DataSet ds = new DataSet();
            DA.Fill(ds);
            DataTable ret = ds.Tables[0];

            logSqlInfo(String.Format(Messages.LOG_DURATION, (DateTime.Now - dtStart).ToString()));

            return ret;
        }




        ///<summary>
        ///Tranzakcio megnyitasa
        ///</summary>
        public void BeginTran()
        {

            Tran = (TDbTransaction)Conn.BeginTransaction();
        }

        ///<summary>
        ///Tranzakcio veglegesitese
        ///</summary>
        public void Commit()
        {
            if (Tran != null)
                Tran.Commit();
            Tran = null;
        }

        ///<summary>
        ///Tranzakcio visszavonasa
        ///</summary>
        public void Rollback()
        {
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
        public virtual bool IsExists(string p_tablename, string p_where, params object[] p_param)
        {
            string s = ExecuteScalar
               (String.Format("select case when exists (select  * from {0} where {1}) then 1 else 0 end", p_tablename, p_where), p_param).ToString();
            return Convert.ToInt32(s) == 1;
        }


        #region adatkezelés I (preferált módszer)

        public void InsertObjEx(object p_obj)
        {

            Type tp = p_obj.GetType();
            String sql1 = "insert into  " + tp.Name + "(";
            String sql2 = "values(";

            List<object> insertPars = new List<object>();
            List<object> values = new List<object>();


            try
            {
                PropertyInfo[] writeProps = tp.GetProperties();
                foreach (var propInf in writeProps)
                {
                    try
                    {

                        if (propInf.CanWrite && propInf.Name.ToUpper() != SQL_ID.ToUpper())
                        {
                            if (propInf.PropertyType == typeof(Guid))
                                values.Add(tp.GetProperty(propInf.Name).GetValue(p_obj, null).ToString());
                            else
                                values.Add(tp.GetProperty(propInf.Name).GetValue(p_obj, null));
                            if (values.Count > 1)
                            {
                                sql1 += ",";
                                sql2 += ",";
                            }

                            sql1 += propInf.Name;
                            sql2 += "?";
                        }

                    }
                    catch (Exception ex) { throw ex; }     //TODO: szebben megoldani!
                }

                sql1 += ")";
                sql2 += ")";
                ExecuteNonQuery(sql1 + sql2, values.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public void updaeteObjEx(object p_obj, string p_where = null)
        {

            Type tp = p_obj.GetType();
            string sql = "update  " + tp.Name + " set ";

            List<object> updPars = new List<object>();
            List<object> values = new List<object>();

            try
            {

                object ID = new object();
                PropertyInfo[] writeProps = tp.GetProperties();
                foreach (var propInf in writeProps)
                {
                    try
                    {
                        if (propInf.Name.ToUpper() == SQL_ID.ToUpper())
                        {
                            ID = tp.GetProperty(propInf.Name).GetValue(p_obj, null);
                        }
                        else
                        {
                            if (propInf.CanWrite)
                            {
                                if (propInf.PropertyType == typeof(Guid))
                                    values.Add(tp.GetProperty(propInf.Name).GetValue(p_obj, null).ToString());
                                else
                                    values.Add(tp.GetProperty(propInf.Name).GetValue(p_obj, null));

                                if (values.Count > 1)
                                {
                                    sql += ",";
                                }

                                sql += propInf.Name + "=?";
                            }
                        }
                    }
                    catch (Exception ex) { }     //TODO: szebben megoldani!
                }

                if (p_where != null)
                {
                    sql += " where  " + p_where;
                }
                else
                {
                    sql += " where  " + SQL_ID + "=?";
                    values.Add(ID);

                }


                ExecuteNonQuery(sql, values.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public void DeleteObjEx(object p_obj, string p_where = null)
        {

            Type tp = p_obj.GetType();
            string sql = "delete from " + tp.Name;

            try
            {
                if (p_where != null)
                    sql += " where  " + p_where;

                ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<T> GetListObjEx<T>(string p_where, params object[] p_param)
        {
            PropertyInfo[] outProps = typeof(T).GetProperties();

            List<T> res = new List<T>();



            string fields = "";
            foreach (var propInf in outProps)
            {
                if (propInf.CanWrite)
                {

                    if (!string.IsNullOrWhiteSpace(fields))
                    {
                        fields += ",";
                    }
                    fields += propInf.Name;
                }
            }

            string sql = "select " + fields + " from " + typeof(T).Name;
            if (p_where != "")
                sql += " where " + p_where;


            DataTable dt = Query2DataTable(sql, p_param);
            foreach (DataRow dr in dt.Rows)
            {
                var element = (T)Activator.CreateInstance(typeof(T));
                foreach (var f in outProps)
                {
                    try
                    {
                        if (f.CanWrite)
                        {
                            if (f.Name.ToUpper() == SQL_ID.ToUpper())
                            {
                                //a kulcsot külön kezeljük, hogy a ID mező neve kisbetű/nagybetű ne legyen probléma
                                //PL SQLite-ban a sor ID mezőneve a rendszer által adott 'rowid', de a wrapper objektumban
                                //'RowID'-t használunk
                                f.SetValue(element, Int32.Parse(dr.Field<object>(SQL_ID).ToString()), null);
                            }
                            else
                            {
                                var o = dr.Field<object>(f.Name);
                                if (f.PropertyType == typeof(Int32) && o.GetType() == typeof(Int64))
                                {
                                    f.SetValue(element, Int32.Parse(o.ToString()), null);
                                }
                                else if (f.PropertyType == typeof(Boolean) && o.GetType() == typeof(Decimal))
                                {
                                    f.SetValue(element, (Decimal)o != 0, null);
                                }
                                else if (f.PropertyType == typeof(Guid))
                                {
                                    f.SetValue(element, new Guid(o.ToString()), null);
                                }
                                else
                                {
                                    if (o != null && o.GetType() != typeof(DBNull))
                                        f.SetValue(element, o, null);
                                }
                            }
                        }
                    }
                    catch (Exception e) { throw e; }     //szebben megoldani!
                }
                res.Add(element);
            }

            return res;

        }


        #endregion

        #region adatkezelés II ( mezőnév/érték párok a paraméterlistában)
        /// <summary>
        /// Reszleges insert egy tablaba
        /// </summary>
        /// <param name="tablename">Tabla neve</param>
        /// <param name="param">mezonev, ertek, mezonev, ertek</param>
        /// <returns>Rekordazonosito</returns>
        public int InsertPar(string p_tablename, params object[] p_param)
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
        public void UpdatePar(string p_tablename, string p_IDName, int p_id, params object[] p_param)
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

            sql += " where " + p_IDName + "=?";
            paramvalues[paramvalues.Length - 1] = p_id;

            ExecuteNonQuery(sql, paramvalues);
        }


        #endregion

        #region adatkezelés III ( mezőnév/érték párok hashtable-ban átadva)
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

            return InsertPar(p_tablename, list);
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


            UpdatePar(p_tablename, p_IDName, p_id, list);
        }

        #endregion

        #region belső metódusok

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

        private TDbCommand PrepareCommand(string p_sql, params object[] p_param)
        {
            TDbCommand sqlCmd;
            string sPar = "";

            sqlCmd = (TDbCommand)Conn.CreateCommand();
            sqlCmd.CommandText = SetParameterNames(p_sql.Replace("'***************'", "'FormClosedEventArgs01'"));

            sqlCmd.Parameters.Clear();
            for (int i = 0; i < p_param.Length; i++)
            {
                //SqlParameter par = new SqlParameter("@P" + i.ToString(), p_param[i]);
                TDbParameter par = (TDbParameter)sqlCmd.CreateParameter();
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
                Util.Log2File(p_InfoText, sLogFileName);
            }
        }

        public void Dispose()
        {
            Conn.Close();
        }

        #endregion

    }
}
