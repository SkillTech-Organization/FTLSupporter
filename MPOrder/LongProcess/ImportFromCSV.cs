using MPOrder.BLL;
using MPOrder.BO;
using PMapCore.BLL;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.Localize;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace MPOrder.LongProcess
{
    public class ImportFromCSV : BaseLongProcess
    {
        private SQLServerAccess m_DB;
        private string m_fileName;
        public int AddedCount { get; private set; } = 0;
        public int ItemsCount { get; private set; } = 0;

        public ImportFromCSV(BaseProgressDialog p_Form, string p_fileName)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_fileName = p_fileName;
        }
        protected override void DoWork()
        {
            try
            {
                Encoding ecFile = Encoding.GetEncoding("ISO-8859-2");
                var items = new List<boMPOrder>();
                var lines = File.ReadAllLines(m_fileName, ecFile);

                foreach (string line in lines)
                {
                    ProcessForm.NextStep();


          
                        var val = line.Split(';');

                    int columnIndex = 0;
                    var CompanyCode = val[columnIndex++];
                    var CustomerCode = val[columnIndex++];
                    var CustomerOrderNumber = val[columnIndex++];
                    var CustomerOrderDate = csvDate(val[columnIndex++]);
                    var ShippingDate = csvDate(val[columnIndex++]);
                    var WarehouseCode = val[columnIndex++];
                    var TotalGrossWeightOfOrder = csvDouble(val[columnIndex++]);
                    var NumberOfPalletForDel = csvDouble(val[columnIndex++]);
                    var ShippAddressID = val[columnIndex++];
                    var ShippAddressCompanyName = val[columnIndex++];
                    var ShippAddressZipCode = val[columnIndex++];
                    var ShippingAddressCity = val[columnIndex++];
                    var ShippingAddressStreetAndNumber = val[columnIndex++];
                    var Note = val[columnIndex++];
                    var RowNumber = csvInt(val[columnIndex++]);
                    var ProductCode = val[columnIndex++];
                    var U_M = val[columnIndex++];
                    var ProdDescription = val[columnIndex++];
                    var ConfOrderQty = csvDouble(val[columnIndex++]);
                    var ConfPlannedQty = csvDouble(val[columnIndex++]);
                    var NetWeight = csvDouble(val[columnIndex++]);
                    var PalletPlannedQty = csvDouble(val[columnIndex++]);
                    var PalletBulkQty = csvDouble(val[columnIndex++]);
                    var GrossWeightPlanned = csvDouble(val[columnIndex++]);
                    var ADR = val[columnIndex++];
                    var ADRMultiplier = csvDouble(val[columnIndex++]);
                    var ADRLimitedQuantity = val[columnIndex++];
                    var Freeze = val[columnIndex++];
                    var Melt = val[columnIndex++];
                    var UV = val[columnIndex++];


                    double UnitWeight = GrossWeightPlanned / ConfOrderQty;

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
                        NetWeight = NetWeight,
                        PalletPlannedQty = PalletPlannedQty,
                        PalletBulkQty = PalletBulkQty,
                        GrossWeightPlanned = GrossWeightPlanned,
                        GrossWeightPlannedX = GrossWeightPlanned,
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
                        Currency = "HUF",
                        ADRMultiplierX = ADRMultiplier,
                        UnitWeight = UnitWeight
                    };
                    items.Add(item);

                }


                var bllMPOrderx = new bllMPOrder(PMapCommonVars.Instance.CT_DB);

                var added = 0;
                foreach (var item in items)
                {
                    ProcessForm.NextStep();
                    if (!bllMPOrderx.IsExist(item.CustomerOrderNumber, item.ProductCode))
                    {

                        bllMPOrderx.AddMPOrder(item);
                        added++;
                    }
                }
                AddedCount = added;
                ItemsCount = items.Count;
            }

            catch
            {
                throw;
            }

        }

        private DateTime csvDate( string p_strDate)
        {
            var ret = DateTime.MinValue;
            if( !string.IsNullOrWhiteSpace( p_strDate) && p_strDate.Length == 8)
            {
                ret = DateTime.Parse(Util.LeftString(p_strDate, 4) + "." + p_strDate.Substring(4, 2) + "." + Util.RightString(p_strDate, 2));
            }

            return ret;
        }
        private double csvDouble( string p_strDouble)
        {
            double ret = 0;
            if (Double.TryParse(p_strDouble, out ret))
                return ret;
            return ret;
        }

        private int csvInt(string p_strInt)
        {
            int ret = 0;
            if (Int32.TryParse(p_strInt, out ret))
                return ret;
            return ret;
        }
    }
}