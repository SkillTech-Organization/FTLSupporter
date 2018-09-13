using PMapCore.BLL.Base;
using PMapCore.BO.Mapei;
using PMapCore.Common;
using PMapCore.DB.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.BLL.Mapei
{
    public class bllMPOrder : bllBase
    {
        public bllMPOrder(SQLServerAccess p_DBA)
            : base(p_DBA, "")
        {
        }

        public List<boMPOrder> GetAllMPOrders(string p_where = "", params object[] p_pars)
        {
            string sSql = "select MPO.* " + Environment.NewLine +
                          "  from MPO_MPORDER MPO " + Environment.NewLine;
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
                            ConfPlannedQtyX = Util.getFieldValue<double>(o, "ConfPlannedQtyX"),
                            PalletOrderQty = Util.getFieldValue<double>(o, "PalletOrderQty"),
                            PalletPlannedQty = Util.getFieldValue<double>(o, "PalletPlannedQty"),
                            PalletBulkQty = Util.getFieldValue<double>(o, "PalletBulkQty"),
                            GrossWeightPlanned = Util.getFieldValue<double>(o, "GrossWeightPlanned"),
                            GrossWeightPlannedX = Util.getFieldValue<double>(o, "GrossWeightPlannedX"),
                            ADR = Util.getFieldValue<bool>(o, "ADR"),
                            ADRMultiplier = Util.getFieldValue<double>(o, "ADRMultiplier"),
                            ADRLimitedQuantity = Util.getFieldValue<double>(o, "ADRLimitedQuantity"),
                            Freeze = Util.getFieldValue<bool>(o, "Freeze"),
                            Melt = Util.getFieldValue<bool>(o, "Melt"),
                            UV = Util.getFieldValue<bool>(o, "UV"),
                            Bordero = Util.getFieldValue<string>(o, "Bordero"),
                            Carrier = Util.getFieldValue<string>(o, "Carrier"),
                            VehicleType = Util.getFieldValue<string>(o, "VehicleType"),
                            KM = Util.getFieldValue<double>(o, "KM"),
                            Forfait = Util.getFieldValue<double>(o, "Forfait"),
                            Currency = Util.getFieldValue<string>(o, "Currency")
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

        public int AddMPOrder(boMPOrder p_MPOrder)
        {
            return AddItem(p_MPOrder);
        }

    }
}
