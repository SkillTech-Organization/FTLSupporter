using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using PMapCore.DB;
using DevExpress.XtraGrid;
using PMapCore.BO;
using PMapCore.Forms.Base;
using PMapCore.Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PMapCore.BLL;
using PMapCore.Localize;
using PMapCore.Common.PPlan;
using DevExpress.XtraEditors;
using System.Runtime.ExceptionServices;

namespace PMapCore.Forms.Panels.frmPPlan
{
    public partial class pnlPPlanTourPoints : BasePanel
    {

        private GridHitInfo m_StartDragHitInfo = null;
        private bllPlanEdit m_bllPlanEdit;
        private bllPlan m_bllPlan;
        private PlanEditFuncs m_PlanEditFuncs;

        private PPlanCommonVars m_PPlanCommonVars;
        private Color m_Focused_BackColor;
        private Color m_HideSel_BackColor;

        public pnlPPlanTourPoints(PPlanCommonVars p_PPlanCommonVars)
        {
            InitializeComponent();
            m_PPlanCommonVars = p_PPlanCommonVars;

            m_Focused_BackColor = gridViewTourPoints.Appearance.FocusedRow.BackColor;
            m_HideSel_BackColor = gridViewTourPoints.Appearance.HideSelectionRow.BackColor;

            Init();
        }

        public bool IsFocusedItemExist()
        {
            return gridViewTourPoints.FocusedRowHandle != GridControl.InvalidRowHandle;
        }
        private void Init()
        {
            try
            {
                InitPanel();
                m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);
                m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
                m_PlanEditFuncs = new PlanEditFuncs(this, m_PPlanCommonVars);

                if (!DesignMode)
                {
                    // A form teljes inicialiálásához egy ChgFocusedTourPoint esemény kell, amit a 
                    // túra panelről jövő ChgFocusedTour esemény vált ki
                    // 
                    gridTourPoints.DataSource = new List<boPlanTourPoint>();
                    gridViewTourPoints.Appearance.GetAppearance("HeaderPanel").BackColor = Color.Empty;
                    gridViewTourPoints.Appearance.GetAppearance("HeaderPanel").BorderColor = Color.Empty;
                    gridViewTourPoints.Appearance.GetAppearance("HeaderPanel").Options.UseBackColor = false;

                }
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }

        }


        public void RefreshPanel(PlanEventArgs p_planEventArgs)
        {
            switch (p_planEventArgs.EventMode)
            {
                case ePlanEventMode.ReInit:
                    this.Init();
                    break;
                case ePlanEventMode.Refresh:
                    gridViewTourPoints.RefreshData();
                    break;
                case ePlanEventMode.ChgZoom:
                    break;
                case ePlanEventMode.ChgShowPlannedFlag:
                    break;
                case ePlanEventMode.ChgShowUnPlannedFlag:
                    break;
                case ePlanEventMode.ChgTooltipMode:
                    break;
                case ePlanEventMode.ChgTourSelected:
                    break;
                case ePlanEventMode.ChgTourColor:
                    SetTourColor(p_planEventArgs.Color);
                    break;
                case ePlanEventMode.ChgFocusedTour:
                    gridViewTourPoints.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTourPoints_FocusedRowChanged);

                    SetDataSet(p_planEventArgs.Tour);

                    gridViewTourPoints.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTourPoints_FocusedRowChanged);
                    break;

                case ePlanEventMode.ChgFocusedTourPoint:
                    refreshTourPoints(p_planEventArgs.TourPoint.TPL_ID, p_planEventArgs.TourPoint.ID);
                    break;

                default:
                    break;
            }
        }

        private void gridViewTourPoints_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
        }

        private void refreshTourPoints(int TPL_ID, int PTP_ID)
        {
            gridViewTourPoints.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTourPoints_FocusedRowChanged);
            SetDataSet(m_PPlanCommonVars.GetTourByID(TPL_ID));
            if (gridTourPoints.DataSource != null)
            {

                int ID = gridViewTourPoints.LocateByValue(0, gridColumnID, PTP_ID);
                if (ID != GridControl.InvalidRowHandle)
                    gridViewTourPoints.FocusedRowHandle = ID;
            }
            gridViewTourPoints.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTourPoints_FocusedRowChanged);
        }

        private void SetTourColor(Color p_Color)
        {
            gridViewTourPoints.Appearance.GetAppearance("HeaderPanel").BackColor = p_Color;
            gridViewTourPoints.Appearance.GetAppearance("HeaderPanel").BorderColor = p_Color;
            gridViewTourPoints.Appearance.GetAppearance("HeaderPanel").Options.UseBackColor = true;
        }

        private void gridViewTourPoints_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle != GridControl.InvalidRowHandle)
            {
                int? ID = (int?)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnID);
                if (ID != null && ID.Value != GridControl.InvalidRowHandle)
                {

                    DateTime OPEN = (DateTime)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnOPEN);
                    DateTime CLOSE = (DateTime)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnCLOSE);
                    DateTime PTP_ARRTIME = (DateTime)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnPTP_ARRTIME);
                    DateTime PTP_SERVTIME = (DateTime)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnPTP_SERVTIME);
                    DateTime PTP_DEPTIME = (DateTime)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnPTP_DEPTIME);
                    int PTP_TYPE = (int)gridViewTourPoints.GetRowCellValue(e.FocusedRowHandle, gridColumnPTP_TYPE);

                    if (PTP_TYPE == Global.PTP_TYPE_DEP &&
                        (PTP_ARRTIME > CLOSE ||
                        (PTP_SERVTIME < OPEN || PTP_SERVTIME > CLOSE) ||
                         (PTP_DEPTIME < OPEN || PTP_DEPTIME > CLOSE)))
                    {
                        gridViewTourPoints.Appearance.FocusedRow.BackColor = Color.LightCoral;
                        gridViewTourPoints.Appearance.HideSelectionRow.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        gridViewTourPoints.Appearance.FocusedRow.BackColor = m_Focused_BackColor;
                        gridViewTourPoints.Appearance.HideSelectionRow.BackColor = m_HideSel_BackColor;
                    }

                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, m_PPlanCommonVars.GetTourPointByID(ID.Value)));
                }
            }

        }

        private void SetDataSet(boPlanTour p_Tour)
        {
            if (p_Tour != null)
            {
                gridTourPoints.DataSource = p_Tour.TourPoints;
                SetTourColor(p_Tour.PCOLOR);
            }
            else
            {
                gridTourPoints.DataSource = null;
                SetTourColor(Color.Gray);

            }
        }

        private void gridTourPoints_Click(object sender, EventArgs e)
        {
            if (gridViewTourPoints.FocusedRowHandle != GridControl.InvalidRowHandle)
            {
                int ID = (int)gridViewTourPoints.GetRowCellValue(gridViewTourPoints.FocusedRowHandle, gridColumnID);
                if (ID != GridControl.InvalidRowHandle)
                {
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, m_PPlanCommonVars.GetTourPointByID(ID)));
                }
            }
        }

        private void gridViewTourPoints_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.RowHandle == gridViewTourPoints.FocusedRowHandle && e.Column != gridViewTourPoints.FocusedColumn) return;
            int PTP_TYPE = (int)gridViewTourPoints.GetRowCellValue(e.RowHandle, gridColumnPTP_TYPE);

            if (PTP_TYPE == Global.PTP_TYPE_DEP && (e.Column == gridColumnPTP_ARRTIME || e.Column == gridColumnPTP_SERVTIME || e.Column == gridColumnPTP_DEPTIME))
            {
                DateTime OPEN = (DateTime)gridViewTourPoints.GetRowCellValue(e.RowHandle, gridColumnOPEN);
                DateTime CLOSE = (DateTime)gridViewTourPoints.GetRowCellValue(e.RowHandle, gridColumnCLOSE);
                DateTime PTP_ARRTIME = (DateTime)gridViewTourPoints.GetRowCellValue(e.RowHandle, gridColumnPTP_ARRTIME);
                DateTime PTP_SERVTIME = (DateTime)gridViewTourPoints.GetRowCellValue(e.RowHandle, gridColumnPTP_SERVTIME);
                DateTime PTP_DEPTIME = (DateTime)gridViewTourPoints.GetRowCellValue(e.RowHandle, gridColumnPTP_DEPTIME);
                if (e.Column == gridColumnPTP_ARRTIME && (PTP_ARRTIME > CLOSE))        //Megj.:Zárás utáni érkezés a probléma, nyitás előtt érkezés esetén a jármű vár.
                {
                    //                    e.Appearance.BackColor = Color.Red;
                    //                  e.Appearance.BackColor2 = Color.Red;
                    e.Appearance.ForeColor = Color.Red;

                }

                if (e.Column == gridColumnPTP_SERVTIME && (PTP_SERVTIME < OPEN || PTP_SERVTIME > CLOSE))
                {
                    //              e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.Red;

                }
                if (e.Column == gridColumnPTP_DEPTIME && (PTP_DEPTIME < OPEN || PTP_DEPTIME > CLOSE))
                {
                    //                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.Red;
                }

            }
            if ((PTP_TYPE == Global.PTP_TYPE_WHS_S || PTP_TYPE == Global.PTP_TYPE_WHS_E) && e.Column == gridColumnSendEMail)
            {
                e.Handled = true;
              
            }

        }

        #region Drag&drop
        private void gridTourPoints_MouseDown(object sender, MouseEventArgs e)
        {
            m_StartDragHitInfo = gridViewTourPoints.CalcHitInfo(new Point(e.X, e.Y));
            if (m_StartDragHitInfo.RowHandle < 0) m_StartDragHitInfo = null; 
            
        }

        private void gridTourPoints_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left) return;


            if (m_StartDragHitInfo != null)
            {
                Rectangle dragRect = new Rectangle(new Point(
                    m_StartDragHitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                    m_StartDragHitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);


                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    if (m_StartDragHitInfo.InRow && m_PPlanCommonVars.DraggedObj == null)
                    {
                        int itemID = (int)gridViewTourPoints.GetRowCellValue(m_StartDragHitInfo.RowHandle, gridColumnID);
                        boPlanTourPoint draggedTourPoint = m_PPlanCommonVars.GetTourPointByID(itemID);

                        if (draggedTourPoint.PTP_TYPE == Global.PTP_TYPE_DEP)        //csak túrapontot drag&drop-olunk
                        {
                            m_PPlanCommonVars.DraggedObj = new PPlanCommonVars.PPlanDragObject(PPlanCommonVars.PPlanDragObject.ESourceDataObjectType.TourPoint) { ID = itemID, DataObject = draggedTourPoint, SrcGridControl = gridTourPoints };
                        }

                    }

                    if (m_PPlanCommonVars.DraggedObj != null)
                        gridTourPoints.DoDragDrop(m_PPlanCommonVars.DraggedObj, DragDropEffects.All);

                }
            }
        }

        private void gridTourPoints_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void gridTourPoints_DragDrop(object sender, DragEventArgs e)
        {
            if (m_PPlanCommonVars.DraggedObj != null )
            {

                GridHitInfo hi = gridViewTourPoints.CalcHitInfo(gridTourPoints.PointToClient(new Point(e.X, e.Y)));
                int insPosHandle = hi.RowHandle;
                if (insPosHandle >= 0)
                {

                    int itemID = (int)gridViewTourPoints.GetRowCellValue(insPosHandle, gridColumnID);
                    boPlanTourPoint insPoint = m_PPlanCommonVars.GetTourPointByID(itemID);

                    if (m_PPlanCommonVars.DraggedObj.SrcGridControl == gridTourPoints)
                    {

                        // A griden belül mozogtunk el
                        boPlanTourPoint draggedPoint = (boPlanTourPoint)m_PPlanCommonVars.DraggedObj.DataObject;

                        if (insPoint.NextTourPoint != draggedPoint)
                            m_PlanEditFuncs.ReorganizeTour(draggedPoint, insPoint.Tour, insPoint);

                    }
                    else if (m_PPlanCommonVars.DraggedObj.SrcGridControl.Name.ToUpper() == "GRIDPLANORDERS")
                    {
                        //a megrendelés gridről jöttünk
                        boPlanOrder draggedOrder = (boPlanOrder)m_PPlanCommonVars.DraggedObj.DataObject;
                        if (draggedOrder.PTP_ID > 0)        //Van-e a megrendelésnek túrapontja
                        {
                            //Már túrába szervezett megrendelés, újraszervezés
                            boPlanTourPoint draggedPoint = m_PPlanCommonVars.GetTourPointByID(draggedOrder.PTP_ID);
                            m_PlanEditFuncs.ReorganizeTour(draggedPoint, insPoint.Tour, insPoint);
                        }
                        else
                        {
                            //Új túra beszervezése
                            m_PlanEditFuncs.AddOrderToTour(insPoint.Tour, insPoint, draggedOrder);
                        }
                    }
                }

                m_PPlanCommonVars.DraggedObj = null;

            }
                m_StartDragHitInfo = null;
        }

        private void gridTourPoints_DragLeave(object sender, EventArgs e)
        {
            if (m_PPlanCommonVars.DraggedObj != null )
            {
                if (m_PPlanCommonVars.DraggedObj.SrcGridControl == gridTourPoints)
                {

                    boPlanTourPoint draggedPoint = (boPlanTourPoint)m_PPlanCommonVars.DraggedObj.DataObject;
                    m_PlanEditFuncs.RemoveTourPoint(draggedPoint);

                    m_PPlanCommonVars.DraggedObj= null;
                }
                else if (m_PPlanCommonVars.DraggedObj.SrcGridControl.Name.ToUpper() == "GRIDPLANORDERS")
                {

                    m_PPlanCommonVars.DraggedObj = null;
                }

            }
            m_StartDragHitInfo = null;

        }


        private void gridTourPoints_MouseUp(object sender, MouseEventArgs e)
        {
            m_StartDragHitInfo = null;
            m_PPlanCommonVars.DraggedObj = null;
        }





        #endregion

        private void reChkSendEMail_EditValueChanged(object sender, EventArgs e)
        {
            if (gridViewTourPoints.FocusedRowHandle >= 0)
            {
                CheckEdit chkEdt = (CheckEdit)sender;
                int ID = (int)gridViewTourPoints.GetRowCellValue(gridViewTourPoints.FocusedRowHandle, gridColumnID);

                var tp = m_PPlanCommonVars.GetTourPointByID(ID);


                dlgSendEMail dlgSend = new dlgSendEMail(tp);
                dlgSend.ShowDialog(this);

                /*
                    int ID = (int)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);

                    m_bllPlanEdit.ChangeTourSelected(ID, chkEdt.Checked);
                    boPlanTour tour = m_PPlanCommonVars.GetTourByID(ID);
                    tour.PSelect = chkEdt.Checked;
                     DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTourSelected, tour, chkEdt.Checked));
                   */
            }
        }

        private void reChkSendEMail_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
