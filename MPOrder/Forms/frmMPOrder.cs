using PMapCore.BO.Mapei;
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
                    for (int rowIndex = 2; rowIndex <= lastUsedRow; rowIndex++)
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
                        var ConfPlannedQtyX = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var PalletOrderQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var PalletPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var PalletBulkQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var GrossWeightPlanned = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var GrossWeightPlannedX = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var ADR = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                        var ADRMultiplier = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var ADRLimitedQuantity = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""));
                        var Freeze = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                        var Melt = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);
                        var UV = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false);

                        boMPOrder item = new boMPOrder()
                            {
                                CompanyCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                                CustomerCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            CustomerOrderNumber = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            CustomerOrderDate = (val[rowIndex, ++columnIndex] != null ? DateTime.Parse( val[rowIndex, columnIndex].ToString())  : DateTime.Now.Date),
                            ShippingDate = (val[rowIndex, ++columnIndex] != null ? DateTime.Parse(val[rowIndex, columnIndex].ToString()) : DateTime.Now.Date),
                            WarehouseCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            TotalGrossWeightOfOrder = Double.Parse("0"+ (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            NumberOfPalletForDel = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            ShippAddressID = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            ShippAddressCompanyName = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            ShippAddressZipCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            ShippingAddressCity = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            ShippingAddressStreetAndNumber = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            Note = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            RowNumber = Int32.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            ProductCode = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            U_M = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            ProdDescription = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : ""),
                            ConfOrderQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            ConfPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            ConfPlannedQtyX = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            PalletOrderQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            PalletPlannedQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            PalletBulkQty = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            GrossWeightPlanned = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            GrossWeightPlannedX = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            ADR = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false),
                            ADRMultiplier = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            ADRLimitedQuantity = Double.Parse("0" + (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex].ToString() : "")),
                            Freeze = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false),
                            Melt = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false),
                            UV = (val[rowIndex, ++columnIndex] != null ? val[rowIndex, columnIndex] : false),
                            Bordero =  "",
                            Carrier = "",
                            VehicleType = "",
                            KM = 0,
                            Forfait = 0,
                            Currency = "HUF"
                        };
                        items.Add(item);

                    }
                }

                catch (Exception ex)
                {
                    //Utils.String2File($"SetOutputFileNames_iop ERROR: {Utils.GetExceptionText(ex)} ", logFile, true);
                    throw new Exception(string.Format(PMapMessages.E_MPORD_INTEROP_ERR, ex.Message));
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
