using MPOrder.LongProcess;
using PMapCore.BLL.Mapei;
using PMapCore.BO.Mapei;
using PMapCore.Common;
using PMapCore.Localize;
using PMapCore.LongProcess.Base;
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
                var excelApp = new Excel.Application();
                var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var ci = new System.Globalization.CultureInfo("hu-HU");
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;

                Excel.Workbook excelWorkbook = null;

                try
                {
                    excelApp.Visible = false;

                    excelWorkbook = excelApp.Workbooks.Open(openExcel.FileName);

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


                    var import = new ImportFromXML(new BaseSilngleProgressDialog(0, lastUsedRow * 2, string.Format(PMapMessages.E_MPORD_EXCELIMP, openExcel.FileName), false), val, lastUsedRow, lastUsedColumn);
                    import.Run();
                    import.ProcessForm.ShowDialog();

                }

                catch (Exception ex)
                {
                    Util.ExceptionLog(ex);
                    UI.Error(string.Format(PMapMessages.E_MPORD_EXCELIMP_ERR, ex.Message));
                    //throw new Exception(string.Format(PMapMessages.E_MPORD_INTEROP_ERR, ex.Message));
                }
                finally
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;

                    if (excelWorkbook != null)
                        excelWorkbook.Close(0);
                    if (excelApp != null)
                    {
                        excelApp.Quit();
                        Marshal.FinalReleaseComObject(excelApp);
                    }
                }
            }
        }

    }
}
