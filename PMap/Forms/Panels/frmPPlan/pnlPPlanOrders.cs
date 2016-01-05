using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using DevExpress.XtraGrid;
using PMap.DB;
using PMap.Forms.Base;
using PMap.Common;
using PMap.BO;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PMap.Common.PPlan;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace PMap.Forms.Panels.frmPPlan
{
    public partial class pnlPPlanOrders : BasePanel
    {
        private GridHitInfo m_HitInfo = null;
        private PlanEditFuncs m_PlanEditFuncs;
        private PPlanCommonVars m_PPlanCommonVars;

        public pnlPPlanOrders(PPlanCommonVars p_PPlanCommonVars)
        {
            InitializeComponent();
            m_PPlanCommonVars = p_PPlanCommonVars;
            if (!DesignMode)
            {
                InitPanelBase();
            }
        }

        private void init()
        {
            try
            {

                gridViewPlanOrders.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
                m_PlanEditFuncs = new PlanEditFuncs(this, m_PPlanCommonVars);
                try
                {
                    gridViewPlanOrders.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
                    gridPlanOrders.DataSource = m_PPlanCommonVars.PlanOrderList;
                    gridViewPlanOrders.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
                }
                catch { }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void RefreshPanel(PlanEventArgs p_planEventArgs)
        {
            int rowHandle;
            switch (p_planEventArgs.EventMode)
            {
                case ePlanEventMode.Init:
                    this.init();
//                    refreshAll(false);
                    break;
                case ePlanEventMode.Refresh:
                    refreshAll();
                    break;
                case ePlanEventMode.RefreshOrders:
                    refreshAll();
                    break;
                case ePlanEventMode.ChgZoom:
                    break;
                case ePlanEventMode.ChgShowPlannedFlag:
                    break;
                case ePlanEventMode.ChgShowUnPlannedFlag:
                    break;
                case ePlanEventMode.ChgShowAllOrdersInGrid:
                    refreshAll(false);
                    break;
                case ePlanEventMode.ChgTooltipMode:
                    break;
                case ePlanEventMode.ChgTourSelected:
                    break;
                case ePlanEventMode.ChgFocusedTour:
                    break;
                case ePlanEventMode.ChgFocusedTourPoint:
                    if (p_planEventArgs.TourPoint != null &&
                        p_planEventArgs.TourPoint.ID !=  (int)gridViewPlanOrders.GetRowCellValue(gridViewPlanOrders.FocusedRowHandle, gridColumnPTP_ID))
                    {
                        gridViewPlanOrders.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
                        rowHandle = gridViewPlanOrders.LocateByValue(0, gridColumnPTP_ID, p_planEventArgs.TourPoint.ID);
                        if (rowHandle != GridControl.InvalidRowHandle)
                            gridViewPlanOrders.FocusedRowHandle = rowHandle;
                        gridViewPlanOrders.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
                    }
                    break;
                case ePlanEventMode.ChgFocusedOrder:
                    if (p_planEventArgs.PlanOrder != null &&
                        p_planEventArgs.PlanOrder.PTP_ID !=  (int)gridViewPlanOrders.GetRowCellValue(gridViewPlanOrders.FocusedRowHandle, gridColumnPTP_ID))
                    {
                        gridViewPlanOrders.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
                        rowHandle = gridViewPlanOrders.LocateByValue(0, gridColumnID, p_planEventArgs.PlanOrder.ID);
                        if (rowHandle != GridControl.InvalidRowHandle)
                            gridViewPlanOrders.FocusedRowHandle = rowHandle;
                        gridViewPlanOrders.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
                    }
                    break;

                default:
                    break;
            }
        }

        private void gridViewUnplannedOrders_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                int ID = (int)gridViewPlanOrders.GetRowCellValue(e.FocusedRowHandle, gridColumnID);
                if (ID != GridControl.InvalidRowHandle)
                {
                    int PTP_ID = (int)gridViewPlanOrders.GetRowCellValue(e.FocusedRowHandle, gridColumnPTP_ID);
                    if (PTP_ID == 0)
                    {
                        m_PPlanCommonVars.FocusedOrder = m_PPlanCommonVars.GetPlannedOrderByID(ID);
                    }
                    else
                    {
                        boPlanTourPoint pt = m_PPlanCommonVars.GetTourPointByID(PTP_ID);
                        m_PPlanCommonVars.FocusedPoint = pt;
                        m_PPlanCommonVars.FocusedTour = m_PPlanCommonVars.GetTourByID(pt.TPL_ID);
                    }
                }
            }
        }

        private void refreshAll(bool p_setFocus = true)
        {
            gridViewPlanOrders.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);
            int ID =0;

            if (p_setFocus && gridViewPlanOrders.FocusedRowHandle >= 0)
                ID = (int)gridViewPlanOrders.GetRowCellValue(gridViewPlanOrders.FocusedRowHandle, gridColumnID);

            gridPlanOrders.DataSource = m_PPlanCommonVars.PlanOrderList;
            gridViewPlanOrders.RefreshData();

            if (m_PPlanCommonVars.ShowAllOrdersInGrid)
            {
                gridViewPlanOrders.ActiveFilter.Clear();
            }
            else
            {
                gridViewPlanOrders.ActiveFilter.Add( new ViewColumnFilterInfo( gridColumnPTP_ID, new ColumnFilterInfo( "PTP_ID=0")));
            }


            if (p_setFocus)
            {

                if (ID > 0)
                {
                    int rowHandle = gridViewPlanOrders.LocateByValue(0, gridColumnID, ID);
                    if (rowHandle != GridControl.InvalidRowHandle)
                        gridViewPlanOrders.FocusedRowHandle = rowHandle;
                }
            }

            gridViewPlanOrders.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewUnplannedOrders_FocusedRowChanged);

        }


        private void gridViewPlanOrders_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (gridViewPlanOrders.GetRowCellValue(e.RowHandle, gridColumnTPL_ID) != null && (int)gridViewPlanOrders.GetRowCellValue(e.RowHandle, gridColumnTPL_ID) == 0)
            {
                e.Appearance.BackColor = Global.UNPLANNEDITEMCOLOR;
            }

        }

        private void gridPlanOrders_DragDrop(object sender, DragEventArgs e)
        {
            GridHitInfo hi = gridViewPlanOrders.CalcHitInfo(gridPlanOrders.PointToClient(new Point(e.X, e.Y)));
            PPlanCommonVars.PPlanDragObject drobj = (PPlanCommonVars.PPlanDragObject)e.Data.GetData(typeof(PPlanCommonVars.PPlanDragObject));
            if (drobj != null)
            {
                if (drobj.SourceDataObjectType == PPlanCommonVars.PPlanDragObject.ESourceDataObjectType.TourPoint)
                {
                }

            }
        }

        private void gridPlanOrders_MouseDown(object sender, MouseEventArgs e)
        {
            m_HitInfo = gridViewPlanOrders.CalcHitInfo(new Point(e.X, e.Y));
            if (m_HitInfo.RowHandle < 0) m_HitInfo = null; 

        }

        private void gridPlanOrders_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_HitInfo == null) return;
            if (e.Button != MouseButtons.Left) return;
            Rectangle dragRect = new Rectangle(new Point(
                m_HitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                m_HitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            if (!dragRect.Contains(new Point(e.X, e.Y)) && m_PPlanCommonVars.DraggedObj == null)
            {
                if (m_HitInfo.InRow)
                {
                    int itemID = (int)gridViewPlanOrders.GetRowCellValue(m_HitInfo.RowHandle, gridColumnID);
                    boPlanOrder draggedOrder = m_PPlanCommonVars.GetPlannedOrderByID(itemID);
                    m_PPlanCommonVars.DraggedObj = new PPlanCommonVars.PPlanDragObject(PPlanCommonVars.PPlanDragObject.ESourceDataObjectType.TourPoint) { ID = itemID, DataObject = draggedOrder, SrcGridControl = gridPlanOrders };
                    gridPlanOrders.DoDragDrop(m_PPlanCommonVars.DraggedObj, DragDropEffects.All);
                }
            }


        }

        private void gridPlanOrders_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
