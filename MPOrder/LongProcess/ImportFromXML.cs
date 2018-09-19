using MPOrder.BLL;
using MPOrder.BO;
using PMapCore.BLL;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.Localize;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace MPOrder.LongProcess
{
    public class ImportFromXML : BaseLongProcess
    {
        private SQLServerAccess m_DB;
        private string m_fileName;
        private dynamic val;
        private int lastUsedRow;
        private int lastUsedColumn;
        public ImportFromXML(BaseProgressDialog p_Form, dynamic p_excelValues, int p_lastUsedRow, int p_lastUsedColumn)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            val = p_excelValues;
            lastUsedRow = p_lastUsedRow;
            lastUsedColumn = p_lastUsedColumn;
        }
        protected override void DoWork()
        {
            try
            {
                bllPackUnit bllPu = new bllPackUnit(PMapCommonVars.Instance.CT_DB);
                var lstPackUnits = bllPu.GetAllPackUnits();

                var items = new List<boMPOrder>();

                for (int rowIndex = 3; rowIndex <= lastUsedRow; rowIndex++)
                {
                    ProcessForm.NextStep();
                    int columnIndex = 0;
                    var CompanyCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var CustomerCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var CustomerOrderNumber = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var CustomerOrderDate = (val[rowIndex, ++columnIndex] != null ? DateTime.Parse(val[rowIndex, columnIndex].ToString()) : DateTime.Now.Date);
                    var ShippingDate = (val[rowIndex, ++columnIndex] != null ? DateTime.Parse(val[rowIndex, columnIndex].ToString()) : DateTime.Now.Date);
                    var WarehouseCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var TotalGrossWeightOfOrder = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var NumberOfPalletForDel = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var ShippAddressID = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var ShippAddressCompanyName = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var ShippAddressZipCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var ShippingAddressCity = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var ShippingAddressStreetAndNumber = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var Note = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var RowNumber = Int32.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var ProductCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var U_M = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var ProdDescription = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "");
                    var ConfOrderQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
//                    var ConfPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var PalletOrderQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var PalletPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var PalletBulkQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var GrossWeightPlanned = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var ADR = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                    var ADRMultiplier = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                    var ADRLimitedQuantity = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                    var Freeze = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                    var Melt = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                    var UV = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);

                    var pu = lstPackUnits.FirstOrDefault(f => f.PCU_NAME1.ToUpper() == U_M.ToUpper() && !f.PCU_DELETED);
                    double UnitWeight = 0;
                    if (pu != null)
                        UnitWeight = pu.PCU_EXCVALUE;

                    boMPOrder item = new boMPOrder()
                    {
                        CompanyCode = CompanyCode,
                        CustomerCode = CustomerCode,
                        CustomerOrderNumber = CustomerOrderNumber,
                        CustomerOrderDate = CustomerOrderDate,
                        ShippingDate = ShippingDate,
                        WarehouseCode = WarehouseCode,
                        TotalGrossWeightOfOrder = TotalGrossWeightOfOrder,
                        NumberOfPalletForDel = NumberOfPalletForDel,
                        ShippAddressID = ShippAddressID,
                        ShippAddressCompanyName = ShippAddressCompanyName,
                        ShippAddressZipCode = ShippAddressZipCode,
                        ShippingAddressCity = ShippingAddressCity,
                        ShippingAddressStreetAndNumber = ShippingAddressStreetAndNumber,
                        Note = Note,
                        RowNumber = RowNumber,
                        ProductCode = ProductCode,
                        U_M = U_M,
                        ProdDescription = ProdDescription,
                        ConfOrderQty = ConfOrderQty,
                        ConfPlannedQty = ConfOrderQty,
                        PalletOrderQty = PalletOrderQty,
                        PalletPlannedQty = PalletPlannedQty,
                        PalletBulkQty = PalletBulkQty,
                        GrossWeightPlanned = ConfOrderQty * UnitWeight,
                        ADR = (ADR.ToUpper() == "I"),
                        ADRMultiplier = ADRMultiplier,
                        ADRLimitedQuantity = (ADRLimitedQuantity.ToUpper() == "I"),
                        Freeze = (Freeze.ToUpper() == "I"),
                        Melt = (Melt.ToUpper() == "I"),
                        UV = (UV.ToUpper() == "I"),
                        Bordero = "",
                        Carrier = "",
                        VehicleType = "",
                        KM = 0,
                        Forfait = 0,
                        Currency = "HUF"
                    };
                    items.Add(item);

                }
                var bllMPOrderx = new bllMPOrder(PMapCommonVars.Instance.CT_DB);

                var added = 0;
                foreach (var item in items)
                {
                    ProcessForm.NextStep();
                    if( !bllMPOrderx.IsExists(item.CustomerOrderNumber, item.ProductCode))
                    {

                        bllMPOrderx.AddMPOrder(item);
                        added++;
                    }
                }
                UI.Message(string.Format(PMapMessages.E_MPORD_EXCELIMP_LOADED, added, items.Count));

            }

            catch
            {
                throw;
            }

        }
    }
}