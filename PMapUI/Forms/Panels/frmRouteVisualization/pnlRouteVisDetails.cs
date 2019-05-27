using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapUI.Forms.Base;
using PMapCore.Common;
using GMap.NET.WindowsForms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using System.Runtime.ExceptionServices;

namespace PMapUI.Forms.Panels.frmRouteVisualization
{
    public partial class pnlRouteVisDetails : BasePanel
    {



        public pnlRouteVisDetails()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            try
            {
                if (!DesignMode)
                {
                    InitPanel();


                    tbZoom.Minimum = Global.DefMinZoom;
                    tbZoom.Maximum = Global.DefMaxZoom;
                    tbZoom.Value = RouteVisCommonVars_DEPRECATED.Instance.Zoom;
                    switch (RouteVisCommonVars_DEPRECATED.Instance.TooltipMode)
                    {
                        case MarkerTooltipMode.OnMouseOver:
                            rdbOnMouseOver.Checked = true;
                            break;
                        case MarkerTooltipMode.Never:
                            rdbNever.Checked = true;
                            break;
                        case MarkerTooltipMode.Always:
                            rdbAlways.Checked = true;
                            break;
                        default:
                            break;
                    }
                    gridDetails.DataSource = RouteVisCommonVars_DEPRECATED.Instance.lstDetails;
                    gridViewDetails.FocusedRowHandle = 0;
                    //                    gridDepots.DataSource = RouteVisCommonVars.Instance.lstRouteDepots.Select(i => i.Depot).ToList();
                    gridDepots.DataSource = RouteVisCommonVars_DEPRECATED.Instance.lstRouteDepots;
                    RepositoryItemImageComboBox itemImageComboBox = new RepositoryItemImageComboBox();
                    itemImageComboBox.Items.AddRange(new object[] {
															  new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sports", "SPORTS", 1),
															  new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Saloon", "SALOON", 2),
															  new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Truck", "TRUCK", 3)});
                    itemImageComboBox.SmallImages = imlRouteSectionType;
                    //gridColumn1.ColumnEdit = itemImageComboBox;
                    //                    gridControl1.DataSource = RouteVisCommonVars.Instance.lstRouteDepots;
                    //                    bandedGridColumn1.ColumnEdit = itemImageComboBox;


                }
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }

        }

        private void tbZoom_ValueChanged(object sender, EventArgs e)
        {
            RouteVisCommonVars_DEPRECATED.Instance.Zoom = (tbZoom.Value);
            DoNotifyDataChanged(new RouteVisEventArgs_DEPRECATED(eRouteVisEventMode.ChgZoom));
        }

        public void RefreshPanel(RouteVisEventArgs_DEPRECATED p_evtArgs)
        {
            switch (p_evtArgs.EventMode)
            {
                case eRouteVisEventMode.ReInit:
                    init();
                    break;
                case eRouteVisEventMode.ChgZoom:
                    tbZoom.ValueChanged -= new EventHandler(tbZoom_ValueChanged);
                    tbZoom.Value = RouteVisCommonVars_DEPRECATED.Instance.Zoom;
                    tbZoom.ValueChanged += new EventHandler(tbZoom_ValueChanged);
                    break;
                case eRouteVisEventMode.ChgDepotSelected:
                    gridViewDepots.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDepots_FocusedRowChanged);
                    int rowHandle = gridViewDepots.LocateByValue(0, gridColumnID, RouteVisCommonVars_DEPRECATED.Instance.SelectedDepID);
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        gridViewDepots.FocusedRowHandle = rowHandle;
                    }
                    gridViewDepots.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDepots_FocusedRowChanged);
                    break;
                default:
                    break;
            }

        }

        private void rdbNever_CheckedChanged(object sender, EventArgs e)
        {
            RouteVisCommonVars_DEPRECATED.Instance.TooltipMode = MarkerTooltipMode.Never;
            DoNotifyDataChanged(new RouteVisEventArgs_DEPRECATED(eRouteVisEventMode.ChgTooltipMode));
        }

        private void rdbOnMouseOver_CheckedChanged(object sender, EventArgs e)
        {
            RouteVisCommonVars_DEPRECATED.Instance.TooltipMode = MarkerTooltipMode.OnMouseOver;
            DoNotifyDataChanged(new RouteVisEventArgs_DEPRECATED(eRouteVisEventMode.ChgTooltipMode));

        }

        private void rdbAlways_CheckedChanged(object sender, EventArgs e)
        {
            RouteVisCommonVars_DEPRECATED.Instance.TooltipMode = MarkerTooltipMode.Always;
            DoNotifyDataChanged(new RouteVisEventArgs_DEPRECATED(eRouteVisEventMode.ChgTooltipMode));
        }


        private void gridViewDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                RouteVisCommonVars_DEPRECATED.Instance.SelectedType = (int)gridViewDetails.GetRowCellValue(e.FocusedRowHandle, gridColumnType);
            }

        }


        private void repositoryItemCheckEditVisible_CheckedChanged(object sender, EventArgs e)
        {

            int type = (int)gridViewDetails.GetRowCellValue(gridViewDetails.FocusedRowHandle, gridColumnType);

            CheckEdit chkEdt = (CheckEdit)sender;
            RouteVisCommonVars_DEPRECATED.Instance.lstDetails[type].Visible = chkEdt.Checked;
            DoNotifyDataChanged(new RouteVisEventArgs_DEPRECATED(eRouteVisEventMode.ChgRouteVisible));

        }

        private void gridViewDepots_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewDepots.FocusedRowHandle >= 0)
            {
                int ID = (int)gridViewDepots.GetRowCellValue(gridViewDepots.FocusedRowHandle, gridColumnID);
                RouteVisCommonVars_DEPRECATED.Instance.SelectedDepID = ID;
                DoNotifyDataChanged(new RouteVisEventArgs_DEPRECATED(eRouteVisEventMode.ChgDepotSelected));
            }
        }
    }
}
