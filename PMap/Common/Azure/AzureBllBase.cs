﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMap.Common.Azure
{
    public class AzureBllBase<T> where T : AzureTableObjBase
    {
        [IgnoreDataMember]
        public DateTimeKind DateTimeKind { get; set; } = DateTimeKind.Utc;
        protected string User { get; private set; }

        public AzureBllBase(string p_user)
        {
            User = p_user;
        }

        /*
        public virtual void GetGridSchemaModel(string p_hiddenColumns, out string schemaField, out string columns)
        {
            Utils.GetGridSchemaModel<T>(p_hiddenColumns, out schemaField, out columns);
        }
        */
        public virtual bool Insert(T p_obj)
        {
            validateObj(p_obj);
            return AzureTableStore.Instance.Insert(p_obj, User);
        }

        private bool Modify(T p_obj)                //ezt a metódust valószínűleg megszüntetem
        {
            validateObj(p_obj);
            return AzureTableStore.Instance.Modify(p_obj, User);
        }

        //CRUD functions
        public virtual void MaintainItem(T p_obj)
        {
            switch (p_obj.State)
            {
                case AzureTableObjBase.enObjectState.New:
                    validateObj(p_obj);
                    AzureTableStore.Instance.Insert(p_obj, User);
                    break;
                case AzureTableObjBase.enObjectState.Stored:
                    break;
                case AzureTableObjBase.enObjectState.Modified:
                    validateObj(p_obj);
                    AzureTableStore.Instance.Modify(p_obj, User);
                    break;
                case AzureTableObjBase.enObjectState.Inactive:
                    AzureTableStore.Instance.Delete(p_obj);
                    break;
                default:
                    break;
            }
        }

   
        public virtual bool Delete(T p_obj)
        {
            return AzureTableStore.Instance.Delete(p_obj);
        }

        public virtual bool DeleteRange<T>(List<AzureItemKeys> p_itemKeys)
        {
            return AzureTableStore.Instance.DeleteRange<T>(p_itemKeys);
        }

        public virtual T Retrieve(object p_partitionKey, object p_rowKey)
        {
            return AzureTableStore.Instance.Retrieve<T>(p_partitionKey, p_rowKey);
        }

        public virtual ObservableCollection<T> RetrieveList(out int Total, string p_where = "", string p_orderBy = "", int pageSize = 0, int page = 1)
        {
            var res = AzureTableStore.Instance.RetrieveObservableList<T>(p_where, p_orderBy, out Total, pageSize, page);
            return res;
        }

        public virtual T RetrieveFirst(string p_where = "", string p_orderBy = "")
        {
            var total = 0;
            var res = AzureTableStore.Instance.RetrieveList<T>(p_where, p_orderBy, out total, 1);
            if (res != null)
                return res.FirstOrDefault();
            return null;
        }

        private void validateObj(T p_obj)
        {
            List<ObjectValidator.ValidationError> validationErros = ObjectValidator.ValidateObject(p_obj, new Type[] { typeof(DataMemberAttribute) });
            if (validationErros.Count > 0)
            {
                string errMsg = null;
                foreach (var err in validationErros)
                {
                    if (errMsg != null)
                        errMsg += Environment.NewLine;
                    else
                        errMsg = "";
                    errMsg = err.Field + ":" + err.Message;
                }
                throw new Exception(errMsg);
            }
        }
    }
}
