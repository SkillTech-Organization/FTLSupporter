﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.DB.Base;
using PMap.Common;
using System.Reflection;
using System.Collections;

namespace PMap.BLL.Base
{
    public class bllBase
    {

        public const string FIELD_ID = "ID";
        public const string FIELD_DELETED = "DELETED";
        public const string FIELD_UTIME = "UTIME";
        public const string FIELD_ATIME = "ATIME";
        public const string FIELD_LASTDATE = "LASTDATE";

        public DBAccess DBA { get; private set; }
        public string TableName  { get; private set; }
        public bllBase(DBAccess p_DBA, string p_TableName)
        {
            DBA = p_DBA;
            TableName = p_TableName;
        }

        private Hashtable getValues(object p_boObject, bool p_insert)
        {
            Type objType = p_boObject.GetType();

            List<PropertyInfo> propInsFields =
            objType.GetProperties().Where(
                p =>
                    p.GetCustomAttributes(typeof(WriteFieldAttribute), true)
                    .Where(custAttr => p_insert ? 
                                        ((WriteFieldAttribute)custAttr).Insert : 
                                        ((WriteFieldAttribute)custAttr).Update)
                    .Any()
                ).ToList();
            Hashtable values = new Hashtable();
            foreach (PropertyInfo propInf in propInsFields)
            {
                object val = propInf.GetValue(p_boObject, null);
                string fieldName = propInf.Name;

                WriteFieldAttribute[] wfa = (WriteFieldAttribute[])propInf.GetCustomAttributes(typeof(WriteFieldAttribute), true);
                if (wfa.Length > 0)
                {
                    //ha több WriteFieldAttribute van megadva, akkor is csak a legelsőt vesszük figyelembe
                    if (wfa[0].FieldName != null)
                        fieldName = wfa[0].FieldName;
                    if (wfa[0].FixValue != null)
                        val = wfa[0].FixValue;
                }
                if (fieldName.ToUpper() == bllBase.FIELD_LASTDATE)
                    val = DateTime.Now;
                if( p_insert && fieldName.ToUpper() == bllBase.FIELD_ATIME)
                    val = DateTime.Now;
                if (!p_insert && fieldName.ToUpper() == bllBase.FIELD_UTIME)
                    val = DateTime.Now;

                values.Add(fieldName, val);

            }
            return values;
        }

        private string getIDName(object p_boObject)
        {
            List<PropertyInfo> propIDField = p_boObject.GetType().GetProperties().Where(
                    p =>
                        p.GetCustomAttributes(typeof(WriteFieldAttribute), true)
                        .Where(custAttr => ((WriteFieldAttribute)custAttr).ID)
                        .Any()
                    ).ToList();

            string IDName = bllBase.FIELD_ID;
            if (propIDField.Count > 0)
                IDName = propIDField.First().Name;
            return IDName;

        }



        protected int AddItem(object p_boObject)
        {
            // ha nincs táblanév definiálva, exception
            if (TableName == "")
                throw new EmptyTableNameException();

            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {
                    Hashtable values = getValues(p_boObject, true);
                    string IDName = getIDName(p_boObject);
                    int ID = DBA.InsertHash(TableName, IDName, values, true);
                    bllHistory.WriteHistory(0, this.TableName, ID, bllHistory.EMsgCodes.ADD, p_boObject);

                    return ID;

                }

                catch (Exception e)
                {
                    DBA.Rollback();
                    throw e;
                }
                finally
                {
                }
            }
        }

        protected void UpdateItem(object p_boObject)
        {

            // ha nincs táblanév definiálva, exception
            if (TableName == "")
                throw new EmptyTableNameException();

            string IDName = getIDName(p_boObject);
            int ID = (int)p_boObject.GetType().GetProperties()
                             .Single(pi => pi.Name == IDName)
                             .GetValue(p_boObject, null);

            using (TransactionBlock transObj = new TransactionBlock(DBA))
            {
                try
                {
                    //TODO:naplózást megoldani
                    Hashtable values = getValues(p_boObject,false);

                    DBA.UpdateHash(TableName, IDName, ID, values);
                    bllHistory.WriteHistory(0, this.TableName, ID, bllHistory.EMsgCodes.UPD, p_boObject);
                }

                catch (Exception e)
                {
                    DBA.Rollback();
                    throw e;
                }
                finally
                {
                }
            }
        }

    }
}
