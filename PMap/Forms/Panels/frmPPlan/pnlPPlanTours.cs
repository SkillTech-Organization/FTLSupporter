using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using PMap.DB;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using PMap.BO;
using PMap.BLL;
using PMap.Forms.Base;
using PMap.Common;
using PMap.Common.PPlan;

namespace PMap.Forms.Panels.frmPPlan
{
    public partial class pnlPPlanTours : BasePanel
    {

        private bllPlanEdit m_bllPlanEdit;
        private PPlanCommonVars m_PPlanCommonVars;

        public pnlPPlanTours(PPlanCommonVars p_PPlanCommonVars)
        {
            InitializeComponent();
            m_PPlanCommonVars = p_PPlanCommonVars;
            if (!DesignMode)
                init();
        }

        private void init()
        {
            try
            {
                InitPanel();
                m_bllPlanEdit = new bllPlanEdit(PMapCommonVars.Instance.CT_DB);

                gridTours.DataSource = m_PPlanCommonVars.TourList;
                gridViewTours.Appearance.FocusedRow.BackColor = Color.FromArgb(255, 128, 128);
                gridViewTours.Appearance.HideSelectionRow.BackColor = Color.FromArgb(128, 255, 128, 128);
                gridViewTours.Appearance.FocusedRow.Options.UseBackColor = false;
                gridViewTours.Appearance.HideSelectionRow.Options.UseBackColor = false;
                gridViewTours.MoveFirst();


            }
            catch (Exception e)
            {
                throw;
            }

        }



        private void gridViewTours_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            if (gridViewTours.GetRowCellValue(e.RowHandle, gridColumnTOURPOINTCNT) != null && (int)gridViewTours.GetRowCellValue(e.RowHandle, gridColumnTOURPOINTCNT) == 0)
            {
                e.Appearance.BackColor = Global.UNPLANNEDITEMCOLOR;
            }

        }

        private void reChkVisible_CheckedChanged(object sender, EventArgs e)
        {
        }


        private void gridViewTours_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                RefreshFocusedRow();
                OnNotifyDataChanged();
            }
        }

        public void OnNotifyDataChanged()
        {
            if (gridViewTours.FocusedRowHandle >= 0)
            {
                int? ID = (int? )gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);
                if (ID != null && ID != GridControl.InvalidRowHandle)
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTour, m_PPlanCommonVars.GetTourByID(ID.Value)));

                else
                {
                    boPlanTour nullTour = null;
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTour, nullTour));
                }
            }
            else
            {
                boPlanTour nullTour = null;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedTour, nullTour));
            }
        }

        public boPlanTour GetSelectedTour()
        {
            if (gridViewTours.FocusedRowHandle != GridControl.InvalidRowHandle)
            {
                int ID = (int)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);
                if (ID != GridControl.InvalidRowHandle)
                    return m_PPlanCommonVars.GetTourByID(ID);
                else
                    return null;
            }
            else
                return null;

        }

        public void RefreshPanel(PlanEventArgs p_planEventArgs)
        {
            switch (p_planEventArgs.EventMode)
            {
                case ePlanEventMode.ReInit:
                    this.init();
                    refreshAll();
                    break;
                case ePlanEventMode.Refresh:
                    refreshAll();
                    OnNotifyDataChanged();
                    break;

                case ePlanEventMode.RemoveTour:
                    if (p_planEventArgs.NeedRefresh)
                    {
                        refreshAll();
                        OnNotifyDataChanged();
                    }
                    break;

                case ePlanEventMode.AddTour:
                    if (p_planEventArgs.NeedRefresh)
                    {
                        refreshAll();
                        OnNotifyDataChanged();
                    }
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
                case ePlanEventMode.RefreshTour:
                    gridViewTours.RefreshData();
                    break;
                case ePlanEventMode.HideAllTours:
                    gridViewTours.RefreshData();
                    break;

                case ePlanEventMode.ShowAllTours:
                    gridViewTours.RefreshData();
                    break;

                case ePlanEventMode.ChgFocusedTour:
                    if (p_planEventArgs.Tour != null && gridViewTours.FocusedRowHandle >= 0)
                    {
                        /*
                        gridViewTours.OptionsSelection.EnableAppearanceFocusedCell = true;
                        gridViewTours.OptionsSelection.EnableAppearanceFocusedRow = true;
                        gridViewTours.OptionsSelection.EnableAppearanceHideSelection = true;
                        */
                        int oriID = (int)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);

                        gridViewTours.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTours_FocusedRowChanged);
                        gridViewTours.RefreshData();
                        RefreshFocusedRow();
                        int ID = gridViewTours.LocateByValue(0, gridColumnID, p_planEventArgs.Tour.ID);
                        if (oriID != ID && ID != GridControl.InvalidRowHandle)
                        {
                            gridViewTours.FocusedRowHandle = ID;
                            //                        if (NotifyDataChanged != null)
                            //                            NotifyDataChanged(this, new PlanEventArgs(eEventMode.ChgFocusedTour, p_planEventArgs.Tour));
                        }
                        RefreshFocusedRow();
                        gridViewTours.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTours_FocusedRowChanged);
                    }
                    else
                    {
                        /* Ha nincs kiválasztott túra nem szüntetjük meg a fókuszt!
                        gridViewTours.OptionsSelection.EnableAppearanceFocusedCell = false;
                        gridViewTours.OptionsSelection.EnableAppearanceFocusedRow = false;
                        gridViewTours.OptionsSelection.EnableAppearanceHideSelection = false;
                        gridViewTours.FocusedRowHandle = GridControl.InvalidRowHandle;
                         */
                    }


                    break;

                case ePlanEventMode.ChgFocusedTourPoint:
                    break;

                case ePlanEventMode.ChgFocusedOrder:
                    break;

                case ePlanEventMode.PrevTour:
                    moveFucus(-1);
                    break;
                case ePlanEventMode.NextTour:
                    moveFucus(1);
                    break;

                case ePlanEventMode.FirstTour:
                    if (gridViewTours.RowCount > 0)
                    {
                        gridViewTours.MoveFirst();
                        OnNotifyDataChanged();
                    }
                    break;
                default:
                    break;
            }
        }

        private void moveFucus(int p_step)
        {
            if (gridViewTours.FocusedRowHandle + p_step >= 0 && gridViewTours.FocusedRowHandle + p_step < gridViewTours.RowCount)
            {
                gridViewTours.FocusedRowHandle += p_step;
            }
        }

        private void refreshAll()
        {
            if (gridViewTours.FocusedRowHandle > 0)
            {
                gridViewTours.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTours_FocusedRowChanged);


                int? ID = (int?)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);

                gridTours.DataSource = m_PPlanCommonVars.TourList;
                gridViewTours.RefreshData();

                if (ID != null)
                {
                    int rowHandle = gridViewTours.LocateByValue(0, gridColumnID, ID.Value);
                    if (rowHandle != GridControl.InvalidRowHandle)
                        gridViewTours.FocusedRowHandle = rowHandle;
                }
                gridViewTours.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewTours_FocusedRowChanged);
            }
        }

        private void RefreshFocusedRow()
        {
            bool QTYErr = (bool)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnQTYErr);
            bool VOLErr = (bool)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnVOLErr);

            gridViewTours.Appearance.FocusedRow.Options.UseBackColor = (QTYErr || VOLErr);
            gridViewTours.Appearance.HideSelectionRow.Options.UseBackColor = (QTYErr || VOLErr);
        }
        private void repositoryItemColorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (gridViewTours.FocusedRowHandle >= 0)
            {
                int ID = (int)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);
                ColorEdit ced = (ColorEdit)sender;

                m_bllPlanEdit.ChangeTourColors(ID, ced.Color.ToArgb());
                boPlanTour tour = m_PPlanCommonVars.GetTourByID(ID);
                tour.PCOLOR = ced.Color;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTourColor, tour, ced.Color));
            }
        }

        private void reChkSelect_EditValueChanged(object sender, EventArgs e)
        {
            if (gridViewTours.FocusedRowHandle >= 0)
            {
                int ID = (int)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);
                CheckEdit chkEdt = (CheckEdit)sender;

                m_bllPlanEdit.ChangeTourSelected(ID, chkEdt.Checked);
                boPlanTour tour = m_PPlanCommonVars.GetTourByID(ID);
                tour.PSelect = chkEdt.Checked;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTourSelected, tour, chkEdt.Checked));
            }
        }

        private void gridTours_Click(object sender, EventArgs e)
        {
            /*
            if (gridViewTours.FocusedRowHandle >= 0)
            {
                int ID = (int)gridViewTours.GetRowCellValue(gridViewTours.FocusedRowHandle, gridColumnID);
                if (ID != GridControl.InvalidRowHandle && NotifyDataChanged != null)
                {
                    NotifyDataChanged(this, new PlanEventArgs(ePlanEventMode.ChgFocusedTour, m_PPlanCommonVars.GetTourByID(ID)));
                }
            }
            */
        }

        private void gridViewTours_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {


            if (e.RowHandle == gridViewTours.FocusedRowHandle && e.Column != gridViewTours.FocusedColumn) return;
            if (e.Column == gridColumnVOLDETAILS && e.RowHandle >= 0)
            {
                bool VOLErr = (bool)gridViewTours.GetRowCellValue(e.RowHandle, gridColumnVOLErr);
                if (VOLErr)
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
            if (e.Column == gridColumnQTYDETAILS && e.RowHandle >= 0)
            {
                bool QTYErr = (bool)gridViewTours.GetRowCellValue(e.RowHandle, gridColumnQTYErr);
                if (QTYErr)
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }

    }
}
