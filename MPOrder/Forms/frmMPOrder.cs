using MPOrder.BLL;
using MPOrder.BO;
using MPOrder.LongProcess;
using PMapCore.BLL;
using PMapCore.BO;
using PMapCore.Common;
using PMapCore.Forms.Base;
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
    public partial class frmMPOrder : BaseForm
    {
        bllMPOrder m_bllMPOrder;
        bllPackUnit m_bllPackUnit;
        List<boMPOrderF> m_data = new List<boMPOrderF>();
        List<boPackUnit> m_packUnits = new List<boPackUnit>();
        bool m_firstF = false;
        bool m_firstT = false;

        public frmMPOrder()
        {
            InitializeComponent();
            m_bllMPOrder = new bllMPOrder(PMapCommonVars.Instance.CT_DB);
            m_bllPackUnit = new bllPackUnit(PMapCommonVars.Instance.CT_DB);
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
                    fillGrids();
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            fillGrids();
        }

        private void fillGrids()
        {

            Cursor oldCursor = Cursor;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                int focusedRowF = gridViewMegrF.FocusedRowHandle;
                int focusedRowT = gridViewMegrT.FocusedRowHandle;
                m_data = m_bllMPOrder.GetAllMPOrdersForGrid(dtmOrderDate.Value);
                gridMegrF.DataSource = m_data;
                if (m_firstF && m_data.Count > 0)
                {
                    gridViewMegrF.BestFitColumns();
                    m_firstF = false;
                }

                if (gridViewMegrF.RowCount <= focusedRowF)
                    gridViewMegrF.FocusedRowHandle = gridViewMegrF.RowCount - 1;
                else
                    gridViewMegrF.FocusedRowHandle = focusedRowF;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Cursor.Current = oldCursor;
            }
        }

        private void gridViewMegrF_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {

                List<boMPOrderF> data = (List<boMPOrderF>)gridViewMegrF.DataSource;
                var master = data[e.FocusedRowHandle];
                if (master != null)
                {
                    gridMegrT.DataSource = master.Items;
                    if (m_firstT && master.Items.Count > 0)
                    {
                        gridViewMegrF.BestFitColumns();
                        m_firstT = false;
                    }
                }

            }
        }

        private void edConfPlannedQty_EditValueChanged(object sender, EventArgs e)
           {

            //   gridMegrF.DataSource = m_data;
            //   gridViewMegrF.RefreshData();
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcConfPlannedQtySum);
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcGrossWeightPlannedSum);
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcADRMultiplierSum);
            
            gridViewMegrF.RefreshRow(gridViewMegrF.FocusedRowHandle);

        }

        private void edConfPlannedQtyX_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

            double newQty = Double.Parse("0" + e.NewValue.ToString().Replace(".", ","));
            double UnitWeight = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcUnitWeight);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfPlannedQty, newQty);
          //  gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, gricGrossWeightPlannedX, newQty * UnitWeight);

        }

        private void edConfPlannedQtyX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gridViewMegrF_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.Column == grcADRMultiplierSum || e.Column == grcGrossWeightPlannedSum || e.Column == grcConfPlannedQtySum)
            {
                double UnitWeight = (double)gridViewMegrF.GetRowCellValue(e.RowHandle, grcUnitWeightF);
                if (UnitWeight == 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }

        }

        private void gridViewMegrT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == grcADRMultiplier || e.Column == grcUnitWeight || e.Column == grcConfPlannedQty)
            {
                double UnitWeight = (double)gridViewMegrT.GetRowCellValue(e.RowHandle, grcUnitWeight);
                if (UnitWeight == 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }

        }
    }
}
