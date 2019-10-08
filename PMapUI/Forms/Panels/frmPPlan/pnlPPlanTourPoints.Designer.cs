﻿namespace PMapUI.Forms.Panels.frmPPlan
{
    partial class pnlPPlanTourPoints
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlPPlanTourPoints));
            this.gridTourPoints = new DevExpress.XtraGrid.GridControl();
            this.gridViewTourPoints = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_ORDER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_NUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDEP_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCLT_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_TIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_DISTANCE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_ARRTIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_SERVTIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_DEPTIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOPENCLOSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOD_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOD_QTY_INC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOD_VOLUME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_BUNDLE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColPTP_TOLL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnADDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOPEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCLOSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_LENGTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_WIDTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_HEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSendEMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_EMAIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reChkSendEMail = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTourPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTourPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reChkSendEMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTourPoints
            // 
            this.gridTourPoints.AllowDrop = true;
            this.gridTourPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridTourPoints.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridTourPoints.Location = new System.Drawing.Point(0, 0);
            this.gridTourPoints.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridTourPoints.MainView = this.gridViewTourPoints;
            this.gridTourPoints.Name = "gridTourPoints";
            this.gridTourPoints.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reChkSendEMail,
            this.repositoryItemCheckEdit1});
            this.gridTourPoints.Size = new System.Drawing.Size(746, 443);
            this.gridTourPoints.TabIndex = 1;
            this.gridTourPoints.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTourPoints});
            this.gridTourPoints.Click += new System.EventHandler(this.gridTourPoints_Click);
            this.gridTourPoints.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridTourPoints_DragDrop);
            this.gridTourPoints.DragOver += new System.Windows.Forms.DragEventHandler(this.gridTourPoints_DragOver);
            this.gridTourPoints.DragLeave += new System.EventHandler(this.gridTourPoints_DragLeave);
            this.gridTourPoints.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridTourPoints_MouseDown);
            this.gridTourPoints.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridTourPoints_MouseMove);
            this.gridTourPoints.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridTourPoints_MouseUp);
            // 
            // gridViewTourPoints
            // 
            this.gridViewTourPoints.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridViewTourPoints.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnID,
            this.gridColumnPTP_ORDER,
            this.gridColumnORD_NUM,
            this.gridColumnDEP_CODE,
            this.gridColumnCLT_NAME,
            this.gridColumnPTP_TIME,
            this.gridColumnPTP_DISTANCE,
            this.gridColumnPTP_ARRTIME,
            this.gridColumnPTP_SERVTIME,
            this.gridColumnPTP_DEPTIME,
            this.gridColumnOPENCLOSE,
            this.gridColumnTOD_QTY,
            this.gridColumnTOD_QTY_INC,
            this.gridColumnTOD_VOLUME,
            this.gridColumnPTP_BUNDLE,
            this.gridColPTP_TOLL,
            this.gridColumnADDR,
            this.gridColumnOPEN,
            this.gridColumnCLOSE,
            this.gridColumnPTP_TYPE,
            this.gridColumnORD_LENGTH,
            this.gridColumnORD_WIDTH,
            this.gridColumnORD_HEIGHT,
            this.gridColumnSendEMail,
            this.gridColumnORD_EMAIL});
            this.gridViewTourPoints.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewTourPoints.GridControl = this.gridTourPoints;
            this.gridViewTourPoints.Name = "gridViewTourPoints";
            this.gridViewTourPoints.OptionsCustomization.AllowGroup = false;
            this.gridViewTourPoints.OptionsDetail.AllowZoomDetail = false;
            this.gridViewTourPoints.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewTourPoints.OptionsDetail.ShowDetailTabs = false;
            this.gridViewTourPoints.OptionsDetail.SmartDetailExpand = false;
            this.gridViewTourPoints.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewTourPoints.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewTourPoints.OptionsView.ShowDetailButtons = false;
            this.gridViewTourPoints.OptionsView.ShowGroupPanel = false;
            this.gridViewTourPoints.PaintStyleName = "MixedXP";
            this.gridViewTourPoints.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridViewTourPoints.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gridViewTourPoints_CustomDrawColumnHeader);
            this.gridViewTourPoints.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewTourPoints_CustomDrawCell);
            this.gridViewTourPoints.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewTourPoints_FocusedRowChanged);
            // 
            // gridColumnID
            // 
            this.gridColumnID.Caption = "ID";
            this.gridColumnID.FieldName = "ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnPTP_ORDER
            // 
            this.gridColumnPTP_ORDER.Caption = "Sorrend";
            this.gridColumnPTP_ORDER.FieldName = "PTP_ORDER";
            this.gridColumnPTP_ORDER.Name = "gridColumnPTP_ORDER";
            this.gridColumnPTP_ORDER.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnORD_NUM
            // 
            this.gridColumnORD_NUM.Caption = "Megr.sz.";
            this.gridColumnORD_NUM.FieldName = "ORD_NUM";
            this.gridColumnORD_NUM.Name = "gridColumnORD_NUM";
            this.gridColumnORD_NUM.OptionsColumn.AllowEdit = false;
            this.gridColumnORD_NUM.Visible = true;
            this.gridColumnORD_NUM.VisibleIndex = 3;
            this.gridColumnORD_NUM.Width = 39;
            // 
            // gridColumnDEP_CODE
            // 
            this.gridColumnDEP_CODE.Caption = "Lerakókód";
            this.gridColumnDEP_CODE.FieldName = "DEP_CODE";
            this.gridColumnDEP_CODE.Name = "gridColumnDEP_CODE";
            this.gridColumnDEP_CODE.OptionsColumn.AllowEdit = false;
            this.gridColumnDEP_CODE.Visible = true;
            this.gridColumnDEP_CODE.VisibleIndex = 0;
            this.gridColumnDEP_CODE.Width = 39;
            // 
            // gridColumnCLT_NAME
            // 
            this.gridColumnCLT_NAME.Caption = "Túrapont";
            this.gridColumnCLT_NAME.FieldName = "CLT_NAME";
            this.gridColumnCLT_NAME.Name = "gridColumnCLT_NAME";
            this.gridColumnCLT_NAME.OptionsColumn.AllowEdit = false;
            this.gridColumnCLT_NAME.Visible = true;
            this.gridColumnCLT_NAME.VisibleIndex = 1;
            this.gridColumnCLT_NAME.Width = 52;
            // 
            // gridColumnPTP_TIME
            // 
            this.gridColumnPTP_TIME.Caption = "Menetidő";
            this.gridColumnPTP_TIME.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnPTP_TIME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnPTP_TIME.FieldName = "PTP_TIME";
            this.gridColumnPTP_TIME.Name = "gridColumnPTP_TIME";
            this.gridColumnPTP_TIME.OptionsColumn.AllowEdit = false;
            this.gridColumnPTP_TIME.Visible = true;
            this.gridColumnPTP_TIME.VisibleIndex = 5;
            this.gridColumnPTP_TIME.Width = 35;
            // 
            // gridColumnPTP_DISTANCE
            // 
            this.gridColumnPTP_DISTANCE.Caption = "Távolság";
            this.gridColumnPTP_DISTANCE.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnPTP_DISTANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnPTP_DISTANCE.FieldName = "PTP_DISTANCE";
            this.gridColumnPTP_DISTANCE.Name = "gridColumnPTP_DISTANCE";
            this.gridColumnPTP_DISTANCE.OptionsColumn.AllowEdit = false;
            this.gridColumnPTP_DISTANCE.Visible = true;
            this.gridColumnPTP_DISTANCE.VisibleIndex = 6;
            this.gridColumnPTP_DISTANCE.Width = 35;
            // 
            // gridColumnPTP_ARRTIME
            // 
            this.gridColumnPTP_ARRTIME.Caption = "Érkezés";
            this.gridColumnPTP_ARRTIME.DisplayFormat.FormatString = "HH:mm";
            this.gridColumnPTP_ARRTIME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnPTP_ARRTIME.FieldName = "PTP_ARRTIME";
            this.gridColumnPTP_ARRTIME.Name = "gridColumnPTP_ARRTIME";
            this.gridColumnPTP_ARRTIME.OptionsColumn.AllowEdit = false;
            this.gridColumnPTP_ARRTIME.Visible = true;
            this.gridColumnPTP_ARRTIME.VisibleIndex = 7;
            this.gridColumnPTP_ARRTIME.Width = 35;
            // 
            // gridColumnPTP_SERVTIME
            // 
            this.gridColumnPTP_SERVTIME.Caption = "Kiszolgálás";
            this.gridColumnPTP_SERVTIME.DisplayFormat.FormatString = "HH:mm";
            this.gridColumnPTP_SERVTIME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnPTP_SERVTIME.FieldName = "PTP_SERVTIME";
            this.gridColumnPTP_SERVTIME.Name = "gridColumnPTP_SERVTIME";
            this.gridColumnPTP_SERVTIME.OptionsColumn.AllowEdit = false;
            this.gridColumnPTP_SERVTIME.Visible = true;
            this.gridColumnPTP_SERVTIME.VisibleIndex = 8;
            this.gridColumnPTP_SERVTIME.Width = 35;
            // 
            // gridColumnPTP_DEPTIME
            // 
            this.gridColumnPTP_DEPTIME.Caption = "Távozás";
            this.gridColumnPTP_DEPTIME.DisplayFormat.FormatString = "HH:mm";
            this.gridColumnPTP_DEPTIME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnPTP_DEPTIME.FieldName = "PTP_DEPTIME";
            this.gridColumnPTP_DEPTIME.Name = "gridColumnPTP_DEPTIME";
            this.gridColumnPTP_DEPTIME.OptionsColumn.AllowEdit = false;
            this.gridColumnPTP_DEPTIME.Visible = true;
            this.gridColumnPTP_DEPTIME.VisibleIndex = 9;
            this.gridColumnPTP_DEPTIME.Width = 35;
            // 
            // gridColumnOPENCLOSE
            // 
            this.gridColumnOPENCLOSE.Caption = "Nyitva tartás";
            this.gridColumnOPENCLOSE.FieldName = "OPENCLOSE";
            this.gridColumnOPENCLOSE.Name = "gridColumnOPENCLOSE";
            this.gridColumnOPENCLOSE.OptionsColumn.AllowEdit = false;
            this.gridColumnOPENCLOSE.Visible = true;
            this.gridColumnOPENCLOSE.VisibleIndex = 4;
            this.gridColumnOPENCLOSE.Width = 39;
            // 
            // gridColumnTOD_QTY
            // 
            this.gridColumnTOD_QTY.Caption = "Mennyiség";
            this.gridColumnTOD_QTY.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOD_QTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOD_QTY.FieldName = "TOD_QTY";
            this.gridColumnTOD_QTY.Name = "gridColumnTOD_QTY";
            this.gridColumnTOD_QTY.OptionsColumn.AllowEdit = false;
            this.gridColumnTOD_QTY.Visible = true;
            this.gridColumnTOD_QTY.VisibleIndex = 10;
            this.gridColumnTOD_QTY.Width = 35;
            // 
            // gridColumnTOD_QTY_INC
            // 
            this.gridColumnTOD_QTY_INC.Caption = "Be.menny.";
            this.gridColumnTOD_QTY_INC.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOD_QTY_INC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOD_QTY_INC.FieldName = "TOD_QTY_INC";
            this.gridColumnTOD_QTY_INC.Name = "gridColumnTOD_QTY_INC";
            this.gridColumnTOD_QTY_INC.OptionsColumn.AllowEdit = false;
            this.gridColumnTOD_QTY_INC.Visible = true;
            this.gridColumnTOD_QTY_INC.VisibleIndex = 11;
            this.gridColumnTOD_QTY_INC.Width = 35;
            // 
            // gridColumnTOD_VOLUME
            // 
            this.gridColumnTOD_VOLUME.Caption = "Térfogat";
            this.gridColumnTOD_VOLUME.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOD_VOLUME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOD_VOLUME.FieldName = "TOD_VOLUME";
            this.gridColumnTOD_VOLUME.Name = "gridColumnTOD_VOLUME";
            this.gridColumnTOD_VOLUME.OptionsColumn.AllowEdit = false;
            this.gridColumnTOD_VOLUME.Visible = true;
            this.gridColumnTOD_VOLUME.VisibleIndex = 12;
            this.gridColumnTOD_VOLUME.Width = 35;
            // 
            // gridColumnPTP_BUNDLE
            // 
            this.gridColumnPTP_BUNDLE.Caption = "Göngyöleg";
            this.gridColumnPTP_BUNDLE.FieldName = "PTP_BUNDLE";
            this.gridColumnPTP_BUNDLE.Name = "gridColumnPTP_BUNDLE";
            this.gridColumnPTP_BUNDLE.OptionsColumn.AllowEdit = false;
            this.gridColumnPTP_BUNDLE.Visible = true;
            this.gridColumnPTP_BUNDLE.VisibleIndex = 13;
            this.gridColumnPTP_BUNDLE.Width = 42;
            // 
            // gridColPTP_TOLL
            // 
            this.gridColPTP_TOLL.Caption = "Útdíj";
            this.gridColPTP_TOLL.DisplayFormat.FormatString = "#,#0.00";
            this.gridColPTP_TOLL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColPTP_TOLL.FieldName = "PTP_TOLL";
            this.gridColPTP_TOLL.Name = "gridColPTP_TOLL";
            this.gridColPTP_TOLL.OptionsColumn.AllowEdit = false;
            this.gridColPTP_TOLL.Visible = true;
            this.gridColPTP_TOLL.VisibleIndex = 14;
            this.gridColPTP_TOLL.Width = 39;
            // 
            // gridColumnADDR
            // 
            this.gridColumnADDR.Caption = "Cím";
            this.gridColumnADDR.FieldName = "ADDR";
            this.gridColumnADDR.Name = "gridColumnADDR";
            this.gridColumnADDR.OptionsColumn.AllowEdit = false;
            this.gridColumnADDR.Visible = true;
            this.gridColumnADDR.VisibleIndex = 2;
            this.gridColumnADDR.Width = 39;
            // 
            // gridColumnOPEN
            // 
            this.gridColumnOPEN.Caption = "Nyitás";
            this.gridColumnOPEN.DisplayFormat.FormatString = "yyyy.MM.dd HH:mm";
            this.gridColumnOPEN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnOPEN.FieldName = "OPEN";
            this.gridColumnOPEN.Name = "gridColumnOPEN";
            this.gridColumnOPEN.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnCLOSE
            // 
            this.gridColumnCLOSE.Caption = "Zárva";
            this.gridColumnCLOSE.DisplayFormat.FormatString = "yyyy.MM.dd HH:mm";
            this.gridColumnCLOSE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnCLOSE.FieldName = "CLOSE";
            this.gridColumnCLOSE.Name = "gridColumnCLOSE";
            this.gridColumnCLOSE.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnPTP_TYPE
            // 
            this.gridColumnPTP_TYPE.Caption = "Típus";
            this.gridColumnPTP_TYPE.FieldName = "PTP_TYPE";
            this.gridColumnPTP_TYPE.Name = "gridColumnPTP_TYPE";
            this.gridColumnPTP_TYPE.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnORD_LENGTH
            // 
            this.gridColumnORD_LENGTH.Caption = "Hosszúság";
            this.gridColumnORD_LENGTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnORD_LENGTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnORD_LENGTH.FieldName = "ORD_LENGTH";
            this.gridColumnORD_LENGTH.Name = "gridColumnORD_LENGTH";
            this.gridColumnORD_LENGTH.OptionsColumn.AllowEdit = false;
            this.gridColumnORD_LENGTH.Visible = true;
            this.gridColumnORD_LENGTH.VisibleIndex = 15;
            this.gridColumnORD_LENGTH.Width = 39;
            // 
            // gridColumnORD_WIDTH
            // 
            this.gridColumnORD_WIDTH.Caption = "Szélesség";
            this.gridColumnORD_WIDTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnORD_WIDTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnORD_WIDTH.FieldName = "ORD_WIDTH";
            this.gridColumnORD_WIDTH.Name = "gridColumnORD_WIDTH";
            this.gridColumnORD_WIDTH.OptionsColumn.AllowEdit = false;
            this.gridColumnORD_WIDTH.Visible = true;
            this.gridColumnORD_WIDTH.VisibleIndex = 16;
            this.gridColumnORD_WIDTH.Width = 39;
            // 
            // gridColumnORD_HEIGHT
            // 
            this.gridColumnORD_HEIGHT.Caption = "Magasság";
            this.gridColumnORD_HEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnORD_HEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnORD_HEIGHT.FieldName = "ORD_HEIGHT";
            this.gridColumnORD_HEIGHT.Name = "gridColumnORD_HEIGHT";
            this.gridColumnORD_HEIGHT.OptionsColumn.AllowEdit = false;
            this.gridColumnORD_HEIGHT.Visible = true;
            this.gridColumnORD_HEIGHT.VisibleIndex = 17;
            this.gridColumnORD_HEIGHT.Width = 39;
            // 
            // gridColumnSendEMail
            // 
            this.gridColumnSendEMail.Caption = "E-mail";
            this.gridColumnSendEMail.ColumnEdit = this.reChkSendEMail;
            this.gridColumnSendEMail.FieldName = "TOD_SENTEMAIL";
            this.gridColumnSendEMail.Name = "gridColumnSendEMail";
            this.gridColumnSendEMail.OptionsColumn.FixedWidth = true;
            this.gridColumnSendEMail.Visible = true;
            this.gridColumnSendEMail.VisibleIndex = 18;
            this.gridColumnSendEMail.Width = 35;
            // 
            // gridColumnORD_EMAIL
            // 
            this.gridColumnORD_EMAIL.Caption = "E-mailcímek";
            this.gridColumnORD_EMAIL.FieldName = "ORD_EMAIL";
            this.gridColumnORD_EMAIL.Name = "gridColumnORD_EMAIL";
            this.gridColumnORD_EMAIL.OptionsColumn.AllowEdit = false;
            this.gridColumnORD_EMAIL.Visible = true;
            this.gridColumnORD_EMAIL.VisibleIndex = 19;
            // 
            // reChkSendEMail
            // 
            this.reChkSendEMail.AutoHeight = false;
            this.reChkSendEMail.Caption = "";
            this.reChkSendEMail.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.reChkSendEMail.Name = "reChkSendEMail";
            this.reChkSendEMail.PictureChecked = ((System.Drawing.Image)(resources.GetObject("reChkSendEMail.PictureChecked")));
            this.reChkSendEMail.PictureUnchecked = ((System.Drawing.Image)(resources.GetObject("reChkSendEMail.PictureUnchecked")));
            this.reChkSendEMail.CheckedChanged += new System.EventHandler(this.reChkSendEMail_CheckedChanged);
            this.reChkSendEMail.EditValueChanged += new System.EventHandler(this.reChkSendEMail_EditValueChanged);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // pnlPPlanTourPoints
            // 
            this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 443);
            this.Controls.Add(this.gridTourPoints);
            this.Name = "pnlPPlanTourPoints";
            this.Text = "Túrapontok";
            ((System.ComponentModel.ISupportInitialize)(this.gridTourPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTourPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reChkSendEMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_ORDER;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_BUNDLE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_TIME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_DISTANCE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_ARRTIME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_SERVTIME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_DEPTIME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCLT_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOD_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOD_QTY_INC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOD_VOLUME;
        public DevExpress.XtraGrid.GridControl gridTourPoints;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewTourPoints;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnADDR;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOPENCLOSE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOPEN;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCLOSE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_TYPE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_LENGTH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_WIDTH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_HEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_NUM;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDEP_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColPTP_TOLL;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSendEMail;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reChkSendEMail;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_EMAIL;
    }
}