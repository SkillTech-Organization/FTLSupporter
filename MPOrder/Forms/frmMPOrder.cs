using PMapCore.BLL.Mapei;
using PMapCore.BO.Mapei;
using PMapCore.Common;
using PMapCore.Localize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace MPOrder.Forms
{
    public partial class frmMPOrder : Form
    {
        public frmMPOrder()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcelImport_Click(object sender, EventArgs e)
        {
            if (openExcel.ShowDialog() == DialogResult.OK)
            {

                var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var ci = new System.Globalization.CultureInfo("hu-HU");
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;

                var excelApp = new Excel.Application();
                Excel.Workbook excelWorkbook = null;
                try
                {
                    excelApp.Visible = false;

                    excelWorkbook = excelApp.Workbooks.Open(openExcel.FileName);

                    //Utils.String2File($"SetOutputFileNames_iop OPENED, path: {excelfile} ", logFile, true);

                    Excel.Sheets excelSheets = excelWorkbook.Worksheets;
                    Excel.Worksheet ws = (Excel.Worksheet)excelSheets.get_Item("Sheet1");
                    if (ws == null)
                    {
                        Marshal.ReleaseComObject(excelSheets);
                        throw new Exception(PMapMessages.E_MPORD_SEETNOTFOUND);
                    }


                    Excel.Range last = ws.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                    Excel.Range range = ws.get_Range("A1", last);
                    var val = range.Value;

                    int lastUsedRow = last.Row;
                    int lastUsedColumn = last.Column;
                    var items = new List<boMPOrder>();
                    for (int rowIndex = 3; rowIndex <= lastUsedRow; rowIndex++)
                    {
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
                        var ConfPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var ConfPlannedQtyX = ConfPlannedQty;
                        var PalletOrderQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var PalletPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var PalletBulkQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var GrossWeightPlanned = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var GrossWeightPlannedX = GrossWeightPlanned;
                        var ADR = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                        var ADRMultiplier = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var ADRLimitedQuantity = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var Freeze = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                        var Melt = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                        var UV = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);

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
                            ConfPlannedQty = ConfPlannedQty,
                            ConfPlannedQtyX = ConfPlannedQtyX,
                            PalletOrderQty = PalletOrderQty,
                            PalletPlannedQty = PalletPlannedQty,
                            PalletBulkQty = PalletBulkQty,
                            GrossWeightPlanned = GrossWeightPlanned,
                            GrossWeightPlannedX = GrossWeightPlannedX,
                            ADR = ADR,
                            ADRMultiplier = ADRMultiplier,
                            ADRLimitedQuantity = ADRLimitedQuantity,
                            Freeze = Freeze,
                            Melt = Melt,
                            UV = UV,
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
                        var existed = bllMPOrderx.GetMPOrderByCODE(item.CustomerOrderNumber, item.ProductCode);
                        if (existed == null)
                        {

                            bllMPOrderx.AddMPOrder(item);
                            added++;
                        }
                    }
                    //itt tartok
                }

                catch (Exception ex)
                {
                    Util.ExceptionLog(ex);
                    UI.Message(string.Format(PMapMessages.E_MPORD_EXCELIMP_ERR, ex.Message));
                    //throw new Exception(string.Format(PMapMessages.E_MPORD_INTEROP_ERR, ex.Message));
                }
                finally
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;

                    excelWorkbook.Close(0);
                    excelApp.Quit();
                    Marshal.FinalReleaseComObject(excelApp);
                }
            }


        }

    }
}
