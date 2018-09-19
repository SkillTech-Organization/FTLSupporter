using MPOrder.BO;
using PMapCore.BLL.Base;
using PMapCore.Common;
using PMapCore.DB.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder.BLL
{
    public class bllMPOrder : bllBase
    {
        public bllMPOrder(SQLServerAccess p_DBA)
            : base(p_DBA, "MPO_MPORDER")
        {
        }

        public List<boMPOrder> GetAllMPOrders(string p_where = "", params object[] p_pars)
        {
            string sSql = "select * " + Environment.NewLine +
                          "  from MPO_MPORDER MPO " + Environment.NewLine +
                         "  left outer join PCU_PACKUNIT PCU on upper(PCU.PCU_NAME1) = upper( MPO.U_M)  " + Environment.NewLine;
            if (p_where != "")
                sSql += " where " + p_where;
            DataTable dt = DBA.Query2DataTable(sSql, p_pars);
            var linq = (from o in dt.AsEnumerable()
                        orderby o.Field<int>("ID")
                        select new boMPOrder
                        {
                            ID = Util.getFieldValue<int>(o, "ID"),
                            CompanyCode = Util.getFieldValue<string>(o, "CompanyCode"),
                            CustomerCode = Util.getFieldValue<string>(o, "CustomerCode"),
                            CustomerOrderNumber = Util.getFieldValue<string>(o, "CustomerOrderNumber"),
                            CustomerOrderDate = Util.getFieldValue<DateTime>(o, "CustomerOrderDate"),
                            ShippingDate = Util.getFieldValue<DateTime>(o, "ShippingDate"),
                            WarehouseCode = Util.getFieldValue<string>(o, "WarehouseCode"),
                            TotalGrossWeightOfOrder = Util.getFieldValue<double>(o, "TotalGrossWeightOfOrder"),
                            NumberOfPalletForDel = Util.getFieldValue<double>(o, "NumberOfPalletForDel"),
                            ShippAddressID = Util.getFieldValue<string>(o, "ShippAddressID"),
                            ShippAddressCompanyName = Util.getFieldValue<string>(o, "ShippAddressCompanyName"),
                            ShippAddressZipCode = Util.getFieldValue<string>(o, "ShippAddressZipCode"),
                            ShippingAddressCity = Util.getFieldValue<string>(o, "ShippingAddressCity"),
                            ShippingAddressStreetAndNumber = Util.getFieldValue<string>(o, "ShippingAddressStreetAndNumber"),
                            Note = Util.getFieldValue<string>(o, "Note"),
                            RowNumber = Util.getFieldValue<int>(o, "RowNumber"),
                            ProductCode = Util.getFieldValue<string>(o, "ProductCode"),
                            U_M = Util.getFieldValue<string>(o, "U_M"),
                            ProdDescription = Util.getFieldValue<string>(o, "ProdDescription"),
                            ConfOrderQty = Util.getFieldValue<double>(o, "ConfOrderQty"),
                            ConfPlannedQty = Util.getFieldValue<double>(o, "ConfPlannedQty"),
                            PalletOrderQty = Util.getFieldValue<double>(o, "PalletOrderQty"),
                            PalletPlannedQty = Util.getFieldValue<double>(o, "PalletPlannedQty"),
                            PalletBulkQty = Util.getFieldValue<double>(o, "PalletBulkQty"),
                            GrossWeightPlanned = Util.getFieldValue<double>(o, "GrossWeightPlanned"),
                            ADR = Util.getFieldValue<bool>(o, "ADR"),
                            ADRMultiplier = Util.getFieldValue<double>(o, "ADRMultiplier"),
                            ADRLimitedQuantity = Util.getFieldValue<bool>(o, "ADRLimitedQuantity"),
                            Freeze = Util.getFieldValue<bool>(o, "Freeze"),
                            Melt = Util.getFieldValue<bool>(o, "Melt"),
                            UV = Util.getFieldValue<bool>(o, "UV"),
                            Bordero = Util.getFieldValue<string>(o, "Bordero"),
                            Carrier = Util.getFieldValue<string>(o, "Carrier"),
                            VehicleType = Util.getFieldValue<string>(o, "VehicleType"),
                            KM = Util.getFieldValue<double>(o, "KM"),
                            Forfait = Util.getFieldValue<double>(o, "Forfait"),
                            Currency = Util.getFieldValue<string>(o, "Currency"),
                            UnitWeight = Util.getFieldValue<double>(o, "PCU_EXCVALUE"),


                        });
            return linq.ToList();
        }


        public boMPOrder GetMPOrder(int p_ID)
        {
            List<boMPOrder> lstMPOrder = GetAllMPOrders("ID = ? ", p_ID);
            if (lstMPOrder.Count == 0)
                return null;
            else
                return lstMPOrder[0];

        }


        public boMPOrder GetMPOrderByCODE(string p_CustomerOrderNumber, string p_ProductCode)
        {
            List<boMPOrder> lstMPOrder = GetAllMPOrders("upper(CustomerOrderNumber) = ? and upper(ProductCode) = ? ", p_CustomerOrderNumber.ToUpper(), p_ProductCode.ToUpper());
            if (lstMPOrder.Count == 0)
            {
                return null;
            }
            //            else if (lstMPOrder.Count == 1)
            else if (lstMPOrder.Count >= 1)
            {
                return lstMPOrder[0];
            }
            else
            {
                throw new DuplicatedCRR_CODEException();
            }
        }


        public bool IsExists(string p_CustomerOrderNumber, string p_ProductCode)
        {
            string sSql = "select case when exists( select ID from MPO_MPORDER where upper(CustomerOrderNumber) = ? and upper(ProductCode) = ?)  then 'OK' else '' end";
            var retVal = DBA.ExecuteScalar(sSql, p_CustomerOrderNumber, p_ProductCode);
            return retVal.ToString() == "OK";
        }


        public int AddMPOrder(boMPOrder p_MPOrder)
        {
            return AddItem(p_MPOrder, false);
        }

        public List<boMPOrderF> GetAllMPOrdersForGrid(DateTime p_ShippingDate)
        {
            var lst = GetAllMPOrders("ShippingDate = ?", p_ShippingDate.Date);
            if (lst != null)
            {
                var res = lst.GroupBy(g1 => new
                {
                    g1.SentToCT,
                    g1.CompanyCode,
                    g1.CustomerCode,
                    g1.CustomerOrderNumber,
                    g1.CustomerOrderDate,
                    g1.ShippingDate,
                    g1.WarehouseCode,
                    g1.ShippAddressID,
                    g1.ShippAddressCompanyName,
                    g1.ShippAddressZipCode,
                    g1.ShippingAddressCity,
                    g1.ShippingAddressStreetAndNumber,
                    g1.Bordero,
                    g1.Carrier,
                    g1.VehicleType,
                    g1.KM,
                    g1.Forfait,
                    g1.Currency,
                    g1.UnitWeight
                }).Select(s => new boMPOrderF()
                {
                    SentToCT = s.Key.SentToCT,
                    CompanyCode = s.Key.CompanyCode,
                    CustomerCode = s.Key.CustomerCode,
                    CustomerOrderNumber = s.Key.CustomerOrderNumber,
                    CustomerOrderDate = s.Key.CustomerOrderDate,
                    ShippingDate = s.Key.ShippingDate,
                    WarehouseCode = s.Key.WarehouseCode,
                    ShippAddressID = s.Key.ShippAddressID,
                    ShippAddressCompanyName = s.Key.ShippAddressCompanyName,
                    ShippAddressZipCode = s.Key.ShippAddressZipCode,
                    ShippingAddressCity = s.Key.ShippingAddressCity,
                    ShippingAddressStreetAndNumber = s.Key.ShippingAddressStreetAndNumber,
                    Bordero = s.Key.Bordero,
                    Carrier = s.Key.Carrier,
                    VehicleType = s.Key.VehicleType,
                    KM = s.Key.KM,
                    Forfait = s.Key.Forfait,
                    Currency = s.Key.Currency,
                    UnitWeight = s.Key.UnitWeight,
                    Items = s.ToList().Select(s2 => new boMPOrderT()
                    {
                        ID = s2.ID,
                        CustomerOrderNumber = s2.CustomerOrderNumber,
                        RowNumber = s2.RowNumber,
                        ProductCode = s2.ProductCode,
                        U_M = s2.U_M,
                        ProdDescription = s2.ProdDescription,
                        ConfOrderQty = s2.ConfOrderQty,
                        PalletOrderQty = s2.PalletOrderQty,
                        PalletPlannedQty = s2.PalletPlannedQty,
                        PalletBulkQty = s2.PalletBulkQty,
                        ADR = s2.ADR,
                        ADRMultiplier = s2.ADRMultiplier,
                        ADRLimitedQuantity = s2.ADRLimitedQuantity,
                        ConfPlannedQty = s2.ConfPlannedQty,
                        GrossWeightPlanned = s2.GrossWeightPlanned,
                        Freeze = s2.Freeze,
                        Melt = s2.Melt,
                        UV = s2.UV,
                        UnitWeight = s2.UnitWeight
                    }
                   ).ToList()
                }).ToList();
                return res;
            }
            else
            {
                return new List<boMPOrderF>();
            }
        }
    }
}