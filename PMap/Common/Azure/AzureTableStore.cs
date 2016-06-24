using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace PMap.Common.Azure
{
    public class AzureTableStore
    {
        private static volatile object m_lock = new object();

        private string m_accountName = "pmaplog";
        private string m_accountKey = "jazm8HHskSidL3HMqRrjytwqMH5hAkv8QgNo9XiF/pxsqer7YhAE8dKQ9m7zGw1h6z5L2HwwXMhaGp/Mf+xytQ==";

        private StorageCredentials m_creds = null;
        private CloudStorageAccount m_account = null;
        private CloudTableClient m_client = null;

        static private AzureTableStore m_instance = null;        //Mivel statikus tag a program indulásakor 
        static public AzureTableStore Instance                   //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new AzureTableStore();
                    }
                    return m_instance;

                }
            }

        }
        private AzureTableStore() { }

        public string AzureAccount { get { return m_accountName; } set { m_accountName = value; } }
        public string AzureKey { get { return m_accountKey; } set { m_accountKey = value; } }

        private void InitTableStore()
        {
            lock (m_lock)
            {
                try
                {
                    if (m_creds == null)
                        m_creds = new StorageCredentials(m_accountName, m_accountKey);
                    if (m_account == null)
                        m_account = new CloudStorageAccount(m_creds, useHttps: true);
                    if (m_client == null)
                        m_client = m_account.CreateCloudTableClient();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool parseHttpStatus(int p_HttpStatusCode)
        {
            return ("200,201,202,203,204,205").Contains(p_HttpStatusCode.ToString());
        }

        private DynamicTableEntity cloneForWrite(object p_obj)
        {

            DynamicTableEntity TableEntity = null;
            Type tp = p_obj.GetType();
            PropertyInfo PartitionKeyProp = tp.GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(AzureTablePartitionKeyAttr))).FirstOrDefault();
            if (PartitionKeyProp == null)
                throw new Exception("AzureTablePartitionKeyAttr annotation is missing!");

            PropertyInfo RowKeyProp = tp.GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(AzureTableRowKeyAttr))).FirstOrDefault();
            if (RowKeyProp != null)
                TableEntity = new DynamicTableEntity(tp.GetProperty(PartitionKeyProp.Name).GetValue(p_obj, null).ToString(), tp.GetProperty(RowKeyProp.Name).GetValue(p_obj, null).ToString());
            else
                TableEntity = new DynamicTableEntity(tp.GetProperty(PartitionKeyProp.Name).GetValue(p_obj, null).ToString(), "");

            try
            {
                PropertyInfo[] writeProps = tp.GetProperties().Where(pi => (Attribute.IsDefined(pi, typeof(DataMemberAttribute)) &&
                                                                           !Attribute.IsDefined(pi, typeof(AzureTablePartitionKeyAttr)) &&
                                                                           !Attribute.IsDefined(pi, typeof(AzureTableRowKeyAttr)))).ToArray<PropertyInfo>();
                foreach (var propInf in writeProps)
                {
                    try
                    {

                        if (propInf.CanWrite)
                        {

                            var val = tp.GetProperty(propInf.Name).GetValue(p_obj, null);
                            if (propInf.PropertyType == typeof(bool?) || propInf.PropertyType == typeof(bool))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((bool?)val));
                            else if (propInf.PropertyType == typeof(byte[]))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((byte[])val));
                            else if (propInf.PropertyType == typeof(DateTime?) || propInf.PropertyType == typeof(DateTime))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((DateTime?)val));
                            else if (propInf.PropertyType == typeof(DateTimeOffset?) || propInf.PropertyType == typeof(DateTimeOffset))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((DateTimeOffset?)val));
                            else if (propInf.PropertyType == typeof(double?) || propInf.PropertyType == typeof(double))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((double?)val));
                            else if (propInf.PropertyType == typeof(Guid?) || propInf.PropertyType == typeof(Guid))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((Guid?)val));
                            else if (propInf.PropertyType == typeof(int?) || propInf.PropertyType == typeof(int))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((int?)val));
                            else if (propInf.PropertyType == typeof(long?) || propInf.PropertyType == typeof(long))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((long?)val));
                            else if (propInf.PropertyType == typeof(string))
                                TableEntity.Properties.Add(propInf.Name, new EntityProperty((string)val));
                        }

                    }
                    catch (Exception ex) { }     //szebben megoldani!
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }     //szebben megoldani!
            return TableEntity;
        }

        private object getFromDynamic<T>(DynamicTableEntity p_obj)
        {
            object result = Activator.CreateInstance(typeof(T));
            Type t = typeof(T);
            PropertyInfo PartitionKeyProp = typeof(T).GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(AzureTablePartitionKeyAttr))).FirstOrDefault();
            if (PartitionKeyProp == null)
                throw new Exception("AzureTablePartitionKeyAttr annotation is missing!");

            if (PartitionKeyProp.PropertyType == typeof(Guid?) || PartitionKeyProp.PropertyType == typeof(Guid))
                t.GetProperty(PartitionKeyProp.Name).SetValue(result, new Guid(p_obj.PartitionKey), null);
            else if (PartitionKeyProp.PropertyType == typeof(int?) || PartitionKeyProp.PropertyType == typeof(int))
                t.GetProperty(PartitionKeyProp.Name).SetValue(result, int.Parse(p_obj.PartitionKey), null);
            else
                t.GetProperty(PartitionKeyProp.Name).SetValue(result, p_obj.PartitionKey, null);


            PropertyInfo RowKeyProp = t.GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(AzureTableRowKeyAttr))).FirstOrDefault();
            if (RowKeyProp != null)
            {
                if (RowKeyProp.PropertyType == typeof(Guid?) || RowKeyProp.PropertyType == typeof(Guid))
                    t.GetProperty(RowKeyProp.Name).SetValue(result, new Guid(p_obj.RowKey), null);
                else if (RowKeyProp.PropertyType == typeof(int?) || RowKeyProp.PropertyType == typeof(int))
                    t.GetProperty(RowKeyProp.Name).SetValue(result, int.Parse(p_obj.RowKey), null);
                else
                    t.GetProperty(RowKeyProp.Name).SetValue(result, p_obj.RowKey, null);
            }
            PropertyInfo[] writeProps = t.GetProperties().Where(pi => (Attribute.IsDefined(pi, typeof(DataMemberAttribute)) &&
                                                                   !Attribute.IsDefined(pi, typeof(AzureTablePartitionKeyAttr)) &&
                                                                   !Attribute.IsDefined(pi, typeof(AzureTableRowKeyAttr)))).ToArray<PropertyInfo>();
            foreach (var propInf in writeProps)
            {
                try
                {
                    if (propInf.CanWrite && p_obj.Properties.ContainsKey(propInf.Name))
                    {
                        if (p_obj.Properties[propInf.Name].PropertyType == EdmType.String)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].StringValue, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.Int64)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].Int64Value, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.Int32)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].Int32Value, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.DateTime)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].DateTime, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.Guid)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].GuidValue, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.Boolean)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].BooleanValue, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.Double)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].DoubleValue, null);
                        else if (p_obj.Properties[propInf.Name].PropertyType == EdmType.Binary)
                            t.GetProperty(propInf.Name).SetValue(result, p_obj.Properties[propInf.Name].BinaryValue, null);
                    }
                }
                catch (Exception ex) { }     //szebben megoldani!
            }
            return result;
        }

        private void setPropFromDyn(Type t, DynamicTableEntity p_obj, PropertyInfo propInf, object result)
        {
        }



        public bool Insert(object p_obj)
        {
            try
            {
                InitTableStore();
                Type tp = p_obj.GetType();
                AzureTableObjBase.enObjectState oriState = AzureTableObjBase.enObjectState.New;
                if (tp.IsSubclassOf(typeof(AzureTableObjBase)))
                {
                    AzureTableObjBase mb = (AzureTableObjBase)p_obj;
                    oriState = mb.ObjState;
                    mb.SetObjState(AzureTableObjBase.enObjectState.Stored);
                }


                DynamicTableEntity dynObj = cloneForWrite(p_obj);
                CloudTable table = null;
                table = m_client.GetTableReference(p_obj.GetType().Name);
                table.CreateIfNotExists();
                TableOperation insertOperation = TableOperation.InsertOrReplace(dynObj);
                TableResult res = table.Execute(insertOperation);
                bool bOK =  parseHttpStatus(res.HttpStatusCode);
                if (!bOK && tp.IsSubclassOf(typeof(AzureTableObjBase)))
                {
                    AzureTableObjBase mb = (AzureTableObjBase)p_obj;
                    mb.SetObjState(oriState);
                }

                return bOK;
            }
            catch (StorageException sex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Modify(object p_obj)
        {
            try
            {
                InitTableStore();
                Type tp = p_obj.GetType();
                AzureTableObjBase.enObjectState oriState = AzureTableObjBase.enObjectState.Modified;
                if (tp.IsSubclassOf(typeof(AzureTableObjBase)))
                {
                    AzureTableObjBase mb = (AzureTableObjBase)p_obj;
                    oriState = mb.ObjState;
                    mb.SetObjState(AzureTableObjBase.enObjectState.Stored);
                }


                DynamicTableEntity dynObj = cloneForWrite(p_obj);
                dynObj.ETag = "*";
                CloudTable table = null;
                table = m_client.GetTableReference(p_obj.GetType().Name);
                table.CreateIfNotExists();
                TableOperation modifyOperation = TableOperation.Replace(dynObj);
                TableResult res = table.Execute(modifyOperation);
                bool bOK = parseHttpStatus(res.HttpStatusCode);
                if (!bOK && tp.IsSubclassOf(typeof(AzureTableObjBase)))
                {
                    AzureTableObjBase mb = (AzureTableObjBase)p_obj;
                    mb.SetObjState(oriState);
                }

                return bOK;

            }
            catch (StorageException sex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(object p_obj)
        {
            try
            {
                InitTableStore();
                Type tp = p_obj.GetType();
                AzureTableObjBase.enObjectState oriState = AzureTableObjBase.enObjectState.Stored;
                if (tp.IsSubclassOf(typeof(AzureTableObjBase)))
                {
                    AzureTableObjBase mb = (AzureTableObjBase)p_obj;
                    oriState = mb.ObjState;
                    mb.SetObjState(AzureTableObjBase.enObjectState.Inactive);
                }
                DynamicTableEntity dynObj = cloneForWrite(p_obj);
                dynObj.ETag = "*";

                CloudTable table = null;
                table = m_client.GetTableReference(p_obj.GetType().Name);
                table.CreateIfNotExists();
                TableOperation deleteOperation = TableOperation.Delete(dynObj);
                TableResult res = table.Execute(deleteOperation);
                bool bOK = parseHttpStatus(res.HttpStatusCode);
                if (!bOK && tp.IsSubclassOf(typeof(AzureTableObjBase)))
                {
                    AzureTableObjBase mb = (AzureTableObjBase)p_obj;
                    mb.SetObjState(oriState);
                }
                return bOK;
            }
            catch (StorageException sex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public T Retrieve<T>(T p_obj)
        {
            string partitionKey = "";
            string rowKey = "";
            Type tp = p_obj.GetType();
            PropertyInfo PartitionKeyProp = tp.GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(AzureTablePartitionKeyAttr))).FirstOrDefault();
            if (PartitionKeyProp == null)
                throw new Exception("AzureTablePartitionKeyAttr annotation is missing!");
            partitionKey = tp.GetProperty(PartitionKeyProp.Name).GetValue(p_obj, null).ToString();

            PropertyInfo RowKeyProp = tp.GetProperties().Where(pi => Attribute.IsDefined(pi, typeof(AzureTableRowKeyAttr))).FirstOrDefault();
            if (RowKeyProp != null)
                rowKey = tp.GetProperty(RowKeyProp.Name).GetValue(p_obj, null).ToString();
            return Retrieve<T>(partitionKey, rowKey);
        }

        public T Retrieve<T>(string p_partitionKey, string p_rowKey)
        {
            try
            {
                InitTableStore();
                CloudTable table = null;
                table = m_client.GetTableReference(typeof(T).Name);
                table.CreateIfNotExists();

                TableOperation retrieveOperation = TableOperation.Retrieve<DynamicTableEntity>(p_partitionKey, p_rowKey);
                TableResult res = table.Execute(retrieveOperation);
                if (parseHttpStatus(res.HttpStatusCode) && res.Result != null)
                {
                    var o = getFromDynamic<T>((DynamicTableEntity)res.Result);
                    return (T)Convert.ChangeType(o, typeof(T)); ;
                }
                else
                    return default(T);
            }
            catch (StorageException sex)
            {
                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ObservableCollection<T> RetrieveList<T>()
        {
            return RetrieveList<T>("", "");
        }

       public ObservableCollection<T> RetrieveList<T>(string p_where)
        {
            return RetrieveList<T>(p_where, "");
        }
 
        public ObservableCollection<T> RetrieveList<T>(string p_where, string p_orderBy )
        {
            List<T> lstResult = new List<T>();
            try
            {
                InitTableStore();
                CloudTable table = null;
                table = m_client.GetTableReference(typeof(T).Name);
                table.CreateIfNotExists();

                TableQuery<DynamicTableEntity> query;
                if (p_where != "")
                {
                    p_where = FixTableStorageWhere(p_where);
                    query = new TableQuery<DynamicTableEntity>().Where(p_where);
                }
                else
                    query = new TableQuery<DynamicTableEntity>();
                var res = table.ExecuteQuery(query);
                if (res.Any())
                {
                    foreach (DynamicTableEntity item in res)
                    {
                        var o = getFromDynamic<T>((DynamicTableEntity)item);
                        lstResult.Add((T)Convert.ChangeType(o, typeof(T)));
                    }
                }

                if( p_orderBy != "")
                {
                    lstResult = lstResult.OrderBy(x => x.GetType().GetProperty(p_orderBy).GetValue(x, null)).ToList();
                }

            }
            catch (StorageException sex)
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new ObservableCollection<T>(lstResult);
        }

        public static string DateFilter(DateTime p_dt)
        {
            return  "datetime'" + p_dt.ToString("yyyy-MM-ddTHH:mm:ss") + "'";
        }

        private static string FixTableStorageWhere(string p_where)
        {
            if (p_where.Contains("datetime"))
            {
                p_where = p_where.Replace("'''", "'");
                p_where = p_where.Replace("'datetime''", "datetime'");
            }

            if (p_where.Contains("guid"))
            {
                p_where = p_where.Replace("'''", "'");
                p_where = p_where.Replace("'guid''", "guid'");
            }

            return p_where;
        }
    }
}
