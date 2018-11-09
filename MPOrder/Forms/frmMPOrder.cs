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
using System.IO;
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
        List<boCSVFile> m_CSVFiles = new List<boCSVFile>();
        public frmMPOrder()
        {
            InitializeComponent();
            m_bllMPOrder = new bllMPOrder(PMapCommonVars.Instance.CT_DB);
            m_bllPackUnit = new bllPackUnit(PMapCommonVars.Instance.CT_DB);
            InitForm();
            RestoreLayout(false);

            m_CSVFiles = m_bllMPOrder.GetFiles();
            cmbCSVFileName.DisplayMember = "ShortCSVFileName";
            cmbCSVFileName.ValueMember = "ShortCSVFileName";
            cmbCSVFileName.Items.Clear();
            cmbCSVFileName.DataSource = m_CSVFiles;
            if (m_CSVFiles.Count > 0)
            {
                var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
                dtmShippingDateX.Value = selItem.ShippingDateX;
            }
            fillGrids();
            setButtons();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcelImport_Click(object sender, EventArgs e)
        {
            var dlg = new dlgImport();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var ci = new System.Globalization.CultureInfo("hu-HU");
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;

                try
                {
                    var lineCount = File.ReadAllLines(dlg.FileName).Length;

                    var import = new ImportFromCSV(new BaseSilngleProgressDialog(0, lineCount * 2, string.Format(PMapMessages.M_MPORD_CSVLIMP, dlg.FileName), false), dlg.FileName, dlg.ShippingDateX);
                    import.Run();
                    import.ProcessForm.ShowDialog();

                    m_CSVFiles = m_bllMPOrder.GetFiles();
                    cmbCSVFileName.DataSource = m_CSVFiles;
                    cmbCSVFileName.SelectedIndex = cmbCSVFileName.FindStringExact(Path.GetFileName(dlg.FileName));
                    var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
                    dtmShippingDateX.Value = selItem.ShippingDateX;
                    fillGrids();
                    UI.Message(string.Format(PMapMessages.M_MPORD_CSVLIMP_LOADED, import.AddedCount, import.ItemsCount));

                    if (import.ErrResult.Count > 0)
                    {
                        var dlgRes = new dlgSendToCTResult();
                        dlgRes.Text = "CSV beolvasás eredménye";
                        dlgRes.Result = import.ErrResult;
                        dlgRes.ShowDialog();
                    }
                }

                catch (Exception ex)
                {
                    Util.ExceptionLog(ex);
                    UI.Error(string.Format(PMapMessages.E_MPORD_CSVIMP_ERR, ex.Message));
                    //throw new Exception(string.Format(PMapMessages.E_MPORD_INTEROP_ERR, ex.Message));
                }
                finally
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;

                }
            }

        }


        private void fillGrids()
        {

            if (cmbCSVFileName.SelectedItem == null)
                return;
            Cursor oldCursor = Cursor;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                var CSVFile = (boCSVFile)cmbCSVFileName.SelectedItem;

                gridMegrT.DataSource = null;
                int focusedRowF = gridViewMegrF.FocusedRowHandle;
                int focusedRowT = gridViewMegrT.FocusedRowHandle;
                m_data = m_bllMPOrder.GetAllMPOrdersForGrid(CSVFile.CSVFileName);
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
                initMegrTGrid();
                setEditors();
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
                initMegrTGrid();
                setEditors();
            }
        }

        private void initMegrTGrid()
        {
            if (gridViewMegrF.FocusedRowHandle >= 0)
            {
                boMPOrderF row = (boMPOrderF)gridViewMegrF.GetRow(gridViewMegrF.FocusedRowHandle);
                /*
                List<boMPOrderF> data = (List<boMPOrderF>)gridViewMegrF.DataSource;
                var master = data[gridViewMegrF.FocusedRowHandle];
                */
                if (row != null)
                {
                    gridMegrT.DataSource = null;
                    gridMegrT.DataSource = row.Items;
                    if (m_firstT && row.Items.Count > 0)
                    {
                        gridViewMegrF.BestFitColumns();
                        m_firstT = false;
                    }
                }
            }
            else
            {
                gridMegrT.DataSource = null;
            }
            setButtons();
        }


        private void edConfPlannedQty_EditValueChanged(object sender, EventArgs e)
        {
            refreshCurrentF();
        }

        private void refreshCurrentF()
        {
            //   gridMegrF.DataSource = m_data;
            //   gridViewMegrF.RefreshData();
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcConfPlannedQtySum);
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcGrossWeightPlannedXSum);
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcADRMultiplierXSum);
            gridViewMegrF.RefreshRowCell(gridViewMegrF.FocusedRowHandle, grcNumberOfPalletForDelX);

            gridViewMegrF.RefreshRow(gridViewMegrF.FocusedRowHandle);
        }


        private void gridViewMegrF_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {


            double GrossWeightPlannedSum = (double)gridViewMegrF.GetRowCellValue(e.RowHandle, grcGrossWeightPlannedSum);
            double GrossWeightPlannedXSum = (double)gridViewMegrF.GetRowCellValue(e.RowHandle, grcGrossWeightPlannedXSum);

            if (GrossWeightPlannedSum != GrossWeightPlannedXSum)
            {
                if( e.RowHandle == gridViewMegrF.FocusedRowHandle)
                    e.Appearance.ForeColor = Color.LightCyan;
                else
                    e.Appearance.ForeColor = Color.Blue;
            }

            if (e.Column == grcADRMultiplierXSum || e.Column == grcGrossWeightPlannedXSum || e.Column == grcConfPlannedQtySum)
            {
                string CustomerOrderNumber = (string)gridViewMegrF.GetRowCellValue(e.RowHandle, grcCustomerOrderNumber);
                if (!string.IsNullOrWhiteSpace(CustomerOrderNumber))
                {
                    var item = m_data.Where(w => w.CustomerOrderNumber == CustomerOrderNumber).FirstOrDefault();
                    if (item == null || item.Items.Any(a => a.UnitWeight == 0))
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }

            if (e.Column == grcNumberOfPalletForDelX)
            {
                var SentToCT = (bool)gridViewMegrF.GetRowCellValue(e.RowHandle, grcSentToCT);
                if (SentToCT)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 192);
                    e.Appearance.BackColor2 = Color.FromArgb(255, 255, 192);
                    e.Appearance.ForeColor = Color.Black;
                }
            }

        }

        private void gridViewMegrT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            double GrossWeightPlanned = (double)gridViewMegrT.GetRowCellValue(e.RowHandle, grcGrossWeightPlanned);
            double GrossWeightPlannedX = (double)gridViewMegrT.GetRowCellValue(e.RowHandle, grcGrossWeightPlannedX);

            if (GrossWeightPlanned != GrossWeightPlannedX)
            {
                if (e.RowHandle == gridViewMegrT.FocusedRowHandle)
                    e.Appearance.ForeColor = Color.LightCyan;
                else
                    e.Appearance.ForeColor = Color.Blue;
            }


            if (e.Column == grcADRMultiplierX || e.Column == grcUnitWeight || e.Column == grcConfPlannedQty)
            {
                double UnitWeight = (double)gridViewMegrT.GetRowCellValue(e.RowHandle, grcUnitWeight);
                if (UnitWeight == 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }


            if (e.Column == grcConfPlannedQty || e.Column == grcPalletBulkQtyX || e.Column == grcPalletPlannedQtyX)
            {
                var SentToCT = (bool)gridViewMegrF.GetRowCellValue(gridViewMegrF.FocusedRowHandle, grcSentToCT);
                if (SentToCT)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 192);
                    e.Appearance.BackColor2 = Color.FromArgb(255, 255, 192);
                    e.Appearance.ForeColor = Color.Black;
                }
            }

        }

        private void edSentToCT_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;

            string CustomerOrderNumber = (string)gridViewMegrF.GetRowCellValue(gridViewMegrF.FocusedRowHandle, grcCustomerOrderNumber);
            bool SentToCT = (bool)e.NewValue;
            m_bllMPOrder.SetSentToCT(selItem.CSVFileName, CustomerOrderNumber, SentToCT);

            //visszaírjuk a grid adataiba, hogy a EditValueChanged-beli képenyőfrissítés lássa a változást
            gridViewMegrF.SetRowCellValue(gridViewMegrF.FocusedRowHandle, grcSentToCT, SentToCT);


            if ( !SentToCT)
            {
                resetF();
            }
            gridMegrF.Refresh();
            gridMegrT.Refresh();

            setEditors();
        }
        private void edSentToCT_EditValueChanged(object sender, EventArgs e)
        {

          
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
            m_bllMPOrder.SetAllSentToCT(selItem.CSVFileName, true);
            fillGrids();
        }

        private void btnDeselAll_Click(object sender, EventArgs e)
        {
            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
            m_bllMPOrder.SetAllSentToCT(selItem.CSVFileName, false);
            fillGrids();
        }

        private void tsbExportItems_Click(object sender, EventArgs e)
        {

            var CSVFile = (boCSVFile)cmbCSVFileName.SelectedItem;

            var expDlg = new dlgExportToNetmover(CSVFile.CSVFileName, CSVFile.ShippingDateX);
            if( expDlg.ShowDialog() == DialogResult.OK)
            {
                var sndproc = new SendToNetMover(new BaseSilngleProgressDialog(0, m_data.Count*2, PMapMessages.M_MPORD_SENDTNETMOVER, false), CSVFile.CSVFileName,  expDlg.PLN_ID, expDlg.ExportFile);
                sndproc.Run();
                sndproc.ProcessForm.ShowDialog();
                /*
                var dlgRes = new dlgSendToCTResult();
                dlgRes.Result = sndproc.Result;
                dlgRes.ShowDialog();
                */
                fillGrids();
                UI.Message( PMapMessages.M_MPORD_SENDTONETMOVER_OK);

            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (UI.Confirm(PMapMessages.Q_MPORD_SENDTOCT))
            {
                var sndproc = new SendToCT(new BaseSilngleProgressDialog(0, m_data.Count, PMapMessages.M_MPORD_SENDTOCT, false), m_data);
                sndproc.Run();
                sndproc.ProcessForm.ShowDialog();
                var dlgRes = new dlgSendToCTResult();
                dlgRes.Result = sndproc.Result;
                dlgRes.ShowDialog();

            }
        }

        private void SaveLayout()
        {
            FormSerializeHelper fs = new FormSerializeHelper(this);
            string MPP_WINDOW = XMLSerializator.SerializeObject(fs);

            string MPP_TGRID = Util.SaveGridLayoutToString(gridViewMegrF);
            string MPP_PGRID = Util.SaveGridLayoutToString(gridViewMegrT);
            bllMapFormPar.SaveParameters(-1, PMapCommonVars.Instance.USR_ID, MPP_WINDOW, "", "", MPP_TGRID, MPP_PGRID, "");
        }

        private void frmMPOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveLayout();
        }

        private void RestoreLayout(bool p_reset)
        {
            string MPP_WINDOW = "";
            string MPP_DOCK = "";
            string MPP_PARAM = "";
            string MPP_TGRID = "";
            string MPP_PGRID = "";
            string MPP_UGRID = "";



            try
            {

                if (p_reset)
                    bllMapFormPar.RemoveParameters(-1, PMapCommonVars.Instance.USR_ID);

                bllMapFormPar.RestoreParameters(-1, PMapCommonVars.Instance.USR_ID, out MPP_WINDOW, out MPP_DOCK, out MPP_PARAM, out MPP_TGRID, out MPP_PGRID, out MPP_UGRID);


                if (MPP_WINDOW != "")
                {
                    FormSerializeHelper fs = (FormSerializeHelper)XMLSerializator.DeserializeObject(MPP_WINDOW, typeof(FormSerializeHelper));

                    this.WindowState = fs.WindowState;
                    this.Width = fs.Width;
                    this.Height = fs.Height;
                    this.Left = fs.Left;
                    this.Top = fs.Top;

                }
                /* Egyelőre a grideket nem töljük vissza */
                if (MPP_TGRID != "")
                    Util.RestoreGridLayoutFromString(gridViewMegrF, MPP_TGRID);
                if (MPP_PGRID != "")
                    Util.RestoreGridLayoutFromString(gridViewMegrT, MPP_PGRID);


            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }

        private void frmMPOrder_Activated(object sender, EventArgs e)
        {
        }

        private void frmMPOrder_Load(object sender, EventArgs e)
        {
            RestoreLayout(false);
        }



        private void tbsDelete_Click(object sender, EventArgs e)
        {
            if (UI.Confirm(PMapMessages.Q_MPORD_DELITEM))
            {
                string CustomerOrderNumber = (string)gridViewMegrF.GetRowCellValue(gridViewMegrF.FocusedRowHandle, grcCustomerOrderNumber);
                var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
                var CSVFileName = selItem.CSVFileName;

                m_data.RemoveAll(r => r.CustomerOrderNumber == CustomerOrderNumber);
                m_bllMPOrder.DeleteItemByCustomerOrderNumber(CSVFileName, CustomerOrderNumber);
                fillGrids();
            }
        }

     

        private void edResetT_Click(object sender, EventArgs e)
        {
            resetT();
        }

        private void resetT()
        {

            int ID = (int)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcID);

            var ConfOrderQty = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfOrderQty);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfPlannedQty, ConfOrderQty);

            var GrossWeightPlanned = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcGrossWeightPlanned);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcGrossWeightPlannedX, GrossWeightPlanned);

            var ADRMultiplier = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplier);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplierX, ADRMultiplier);

            var PalletPlannedQty = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletPlannedQty);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletPlannedQtyX, PalletPlannedQty);

            var PalletBulkQty = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletBulkQty);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletBulkQtyX, PalletBulkQty);


            m_bllMPOrder.SetManualValuesT(ID, ConfOrderQty, GrossWeightPlanned, ADRMultiplier, PalletPlannedQty, PalletBulkQty);
            refreshCurrentF();
        }

        private void repResetF_Click(object sender, EventArgs e)
        {
            resetF();
          }

        private void resetF()
        {

            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
            var CSVFileName = selItem.CSVFileName;
            var CustomerOrderNumber = (string)gridViewMegrF.GetRowCellValue(gridViewMegrF.FocusedRowHandle, grcCustomerOrderNumber);


            var item = m_data.Where(w => w.CustomerOrderNumber == CustomerOrderNumber).FirstOrDefault();
            if (item != null)
            {
                item.NumberOfPalletForDelX = item.NumberOfPalletForDel;
                m_bllMPOrder.SetManualValuesF(CSVFileName, CustomerOrderNumber, item.NumberOfPalletForDelX);
                foreach (var fe in item.Items)
                {

                    fe.ADRMultiplierX = fe.ADRMultiplier;
                    fe.GrossWeightPlannedX = fe.GrossWeightPlanned;
                    fe.ConfPlannedQty = fe.ConfOrderQty;
                    fe.PalletPlannedQtyX = fe.PalletPlannedQty;
                    fe.PalletBulkQtyX = fe.PalletBulkQty;

                    m_bllMPOrder.SetManualValuesT(fe.ID, fe.ConfPlannedQty, fe.GrossWeightPlannedX, fe.ADRMultiplierX, fe.PalletPlannedQtyX, fe.PalletBulkQtyX);
                }
                refreshCurrentF();
                initMegrTGrid();
            }
        }

        private void gridMegrF_Click(object sender, EventArgs e)
        {

        }

        private void cmbCSVFileName_TextChanged(object sender, EventArgs e)
        {

            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
            dtmShippingDateX.Value = selItem.ShippingDateX;

            fillGrids();
            setButtons();
        }

        private void edConfPlannedQtyX_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            double ADRMultiplierX;
            double newQty = Double.Parse("0" + e.NewValue.ToString().Replace(".", ","));
            int ID = (int)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcID);

            double UnitWeight = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcUnitWeight);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfPlannedQty, newQty);
            gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcGrossWeightPlannedX, newQty * UnitWeight);


            double ADRMultiplier = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplier);
            bool ADR = (bool)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADR);
            if (true || ADR)
            {
                double ConfOrderQty = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfOrderQty);
                if (newQty != 0)
                    ADRMultiplierX = Math.Round(ADRMultiplier * (newQty / ConfOrderQty));
                else
                    ADRMultiplierX = 0;

                gridViewMegrT.SetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplierX, ADRMultiplierX);
            }
            else
            {
                ADRMultiplierX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplierX);
            }

            var PalletPlannedQtyX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletPlannedQtyX);
            var PalletBulkQtyX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletBulkQtyX);


            m_bllMPOrder.SetManualValuesT(ID, newQty, newQty * UnitWeight, ADRMultiplierX, PalletPlannedQtyX, PalletBulkQtyX);
            refreshCurrentF();
        }

        private void edConfPlannedQtyX_ValueChanged(object sender, EventArgs e)
        {
        }


        private void edNumberOfPalletForDelX_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
            var CSVFileName = selItem.CSVFileName;
            var CustomerOrderNumber = (string)gridViewMegrF.GetRowCellValue(gridViewMegrF.FocusedRowHandle, grcCustomerOrderNumber);

            var NumberOfPalletForDelX = Double.Parse("0" + e.NewValue.ToString().Replace(".", ","));


            m_bllMPOrder.SetManualValuesF(CSVFileName, CustomerOrderNumber, NumberOfPalletForDelX);

        }

        private void edPalletBulkQtyX_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int ID = (int)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcID);

            var ConfPlannedQty = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfPlannedQty);
            var GrossWeightPlannedX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcGrossWeightPlannedX);
            var ADRMultiplierX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplierX);
            var PalletPlannedQtyX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletPlannedQtyX);
            var PalletBulkQtyX = Double.Parse("0" + e.NewValue.ToString().Replace(".", ",")); ;


            m_bllMPOrder.SetManualValuesT(ID, ConfPlannedQty, GrossWeightPlannedX, ADRMultiplierX, PalletPlannedQtyX, PalletBulkQtyX);
            refreshCurrentF();

        }

        private void edPalletPlannedQtyX_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int ID = (int)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcID);

            var ConfPlannedQty = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcConfPlannedQty);
            var GrossWeightPlannedX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcGrossWeightPlannedX);
            var ADRMultiplierX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcADRMultiplierX);
            var PalletPlannedQtyX = Double.Parse("0" + e.NewValue.ToString().Replace(".", ","));
            var PalletBulkQtyX = (double)gridViewMegrT.GetRowCellValue(gridViewMegrT.FocusedRowHandle, grcPalletBulkQtyX);


            m_bllMPOrder.SetManualValuesT(ID, ConfPlannedQty, GrossWeightPlannedX, ADRMultiplierX, PalletPlannedQtyX, PalletBulkQtyX);
            refreshCurrentF();

        }

        private void btnSetShippingDateX_Click(object sender, EventArgs e)
        {
            var selItem = (boCSVFile)cmbCSVFileName.SelectedItem;
            var CSVFileName = selItem.CSVFileName;
            m_bllMPOrder.SetShippingDateX(CSVFileName, dtmShippingDateX.Value.Date);
            fillGrids();
        }

        private void setButtons()
        {
            btnExcelImport.Enabled = true;
            btnSelAll.Enabled = gridViewMegrF.RowCount > 0;
            btnDeselAll.Enabled = gridViewMegrF.RowCount > 0;
            btnSend.Enabled = gridViewMegrF.RowCount > 0;
            tbsDelete.Enabled = gridViewMegrT.RowCount > 0;
            tsbExportItems.Enabled = gridViewMegrF.RowCount > 0;
            btnSetShippingDateX.Enabled = gridViewMegrF.RowCount > 0;
            dtmShippingDateX.Enabled = gridViewMegrF.RowCount > 0;
        }

        private void setEditors()
        {
            if (gridViewMegrF.RowCount > 0)
            {
                var SentToCT = (bool)gridViewMegrF.GetRowCellValue(gridViewMegrF.FocusedRowHandle, grcSentToCT);
                grcNumberOfPalletForDelX.OptionsColumn.AllowEdit = SentToCT;
                grcConfPlannedQty.OptionsColumn.AllowEdit = SentToCT;
                grcADRMultiplierX.OptionsColumn.AllowEdit = SentToCT;
                grcPalletBulkQtyX.OptionsColumn.AllowEdit = SentToCT;
                grcPalletPlannedQtyX.OptionsColumn.AllowEdit = SentToCT;

            }
        }

    
    }
}
