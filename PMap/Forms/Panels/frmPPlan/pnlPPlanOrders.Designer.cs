namespace PMap.Forms.Panels.frmPPlan
{
    partial class pnlPPlanOrders
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
            this.gridPlanOrders = new DevExpress.XtraGrid.GridControl();
            this.gridViewPlanOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDEP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDEP_ADDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOD_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOD_QTY_INC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOD_VOLUME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_LENGTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_WIDTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_HEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnOPENCLOSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnORD_NUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDEP_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_REG_NUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTPL_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPTP_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reChkVisible = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlanOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlanOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reChkVisible)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPlanOrders
            // 
            this.gridPlanOrders.AllowDrop = true;
            this.gridPlanOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPlanOrders.Location = new System.Drawing.Point(0, 0);
            this.gridPlanOrders.MainView = this.gridViewPlanOrders;
            this.gridPlanOrders.Name = "gridPlanOrders";
            this.gridPlanOrders.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reChkVisible});
            this.gridPlanOrders.Size = new System.Drawing.Size(429, 438);
            this.gridPlanOrders.TabIndex = 2;
            this.gridPlanOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPlanOrders});
            this.gridPlanOrders.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridPlanOrders_DragDrop);
            this.gridPlanOrders.DragOver += new System.Windows.Forms.DragEventHandler(this.gridPlanOrders_DragOver);
            this.gridPlanOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridPlanOrders_MouseDown);
            this.gridPlanOrders.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridPlanOrders_MouseMove);
            // 
            // gridViewPlanOrders
            // 
            this.gridViewPlanOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnID,
            this.gridColumnDEP_NAME,
            this.gridColumnDEP_ADDR,
            this.gridColumnTOD_QTY,
            this.gridColumnTOD_QTY_INC,
            this.gridColumnTOD_VOLUME,
            this.gridColumnORD_LENGTH,
            this.gridColumnORD_WIDTH,
            this.gridColumnORD_HEIGHT,
            this.gridColumnOPENCLOSE,
            this.gridColumnORD_NUM,
            this.gridColumnDEP_CODE,
            this.gridColumnTRK_ID,
            this.gridColumnTRK_CODE,
            this.gridColumnTRK_REG_NUM,
            this.gridColumnTPL_ID,
            this.gridColumnPTP_ID});
            this.gridViewPlanOrders.GridControl = this.gridPlanOrders;
            this.gridViewPlanOrders.Name = "gridViewPlanOrders";
            this.gridViewPlanOrders.OptionsBehavior.Editable = false;
            this.gridViewPlanOrders.OptionsCustomization.AllowGroup = false;
            this.gridViewPlanOrders.OptionsDetail.AllowZoomDetail = false;
            this.gridViewPlanOrders.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewPlanOrders.OptionsDetail.ShowDetailTabs = false;
            this.gridViewPlanOrders.OptionsDetail.SmartDetailExpand = false;
            this.gridViewPlanOrders.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewPlanOrders.OptionsView.ShowDetailButtons = false;
            this.gridViewPlanOrders.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewPlanOrders.OptionsView.ShowGroupPanel = false;
            this.gridViewPlanOrders.PaintStyleName = "MixedXP";
            this.gridViewPlanOrders.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewPlanOrders_RowStyle);
            this.gridViewPlanOrders.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewUnplannedOrders_FocusedRowChanged);
            // 
            // gridColumnID
            // 
            this.gridColumnID.Caption = "ID";
            this.gridColumnID.FieldName = "ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnDEP_NAME
            // 
            this.gridColumnDEP_NAME.Caption = "Lerakó";
            this.gridColumnDEP_NAME.FieldName = "DEP_NAME";
            this.gridColumnDEP_NAME.Name = "gridColumnDEP_NAME";
            this.gridColumnDEP_NAME.Visible = true;
            this.gridColumnDEP_NAME.VisibleIndex = 0;
            this.gridColumnDEP_NAME.Width = 100;
            // 
            // gridColumnDEP_ADDR
            // 
            this.gridColumnDEP_ADDR.Caption = "Cím";
            this.gridColumnDEP_ADDR.FieldName = "DEP_ADDR";
            this.gridColumnDEP_ADDR.Name = "gridColumnDEP_ADDR";
            this.gridColumnDEP_ADDR.Visible = true;
            this.gridColumnDEP_ADDR.VisibleIndex = 2;
            this.gridColumnDEP_ADDR.Width = 100;
            // 
            // gridColumnTOD_QTY
            // 
            this.gridColumnTOD_QTY.Caption = "Mennyiség";
            this.gridColumnTOD_QTY.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOD_QTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOD_QTY.FieldName = "TOD_QTY";
            this.gridColumnTOD_QTY.Name = "gridColumnTOD_QTY";
            this.gridColumnTOD_QTY.Visible = true;
            this.gridColumnTOD_QTY.VisibleIndex = 3;
            this.gridColumnTOD_QTY.Width = 68;
            // 
            // gridColumnTOD_QTY_INC
            // 
            this.gridColumnTOD_QTY_INC.Caption = "Be.menny.";
            this.gridColumnTOD_QTY_INC.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOD_QTY_INC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOD_QTY_INC.FieldName = "TOD_QTY_INC";
            this.gridColumnTOD_QTY_INC.Name = "gridColumnTOD_QTY_INC";
            this.gridColumnTOD_QTY_INC.Visible = true;
            this.gridColumnTOD_QTY_INC.VisibleIndex = 4;
            this.gridColumnTOD_QTY_INC.Width = 68;
            // 
            // gridColumnTOD_VOLUME
            // 
            this.gridColumnTOD_VOLUME.Caption = "Térfogat";
            this.gridColumnTOD_VOLUME.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOD_VOLUME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOD_VOLUME.FieldName = "TOD_VOLUME";
            this.gridColumnTOD_VOLUME.Name = "gridColumnTOD_VOLUME";
            this.gridColumnTOD_VOLUME.Visible = true;
            this.gridColumnTOD_VOLUME.VisibleIndex = 6;
            this.gridColumnTOD_VOLUME.Width = 68;
            // 
            // gridColumnORD_LENGTH
            // 
            this.gridColumnORD_LENGTH.Caption = "Hosszúság";
            this.gridColumnORD_LENGTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnORD_LENGTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnORD_LENGTH.FieldName = "ORD_LENGTH";
            this.gridColumnORD_LENGTH.Name = "gridColumnORD_LENGTH";
            this.gridColumnORD_LENGTH.Visible = true;
            this.gridColumnORD_LENGTH.VisibleIndex = 7;
            // 
            // gridColumnORD_WIDTH
            // 
            this.gridColumnORD_WIDTH.Caption = "Szélesség";
            this.gridColumnORD_WIDTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnORD_WIDTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnORD_WIDTH.FieldName = "ORD_WIDTH";
            this.gridColumnORD_WIDTH.Name = "gridColumnORD_WIDTH";
            this.gridColumnORD_WIDTH.Visible = true;
            this.gridColumnORD_WIDTH.VisibleIndex = 8;
            // 
            // gridColumnORD_HEIGHT
            // 
            this.gridColumnORD_HEIGHT.Caption = "Magasság";
            this.gridColumnORD_HEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnORD_HEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnORD_HEIGHT.FieldName = "ORD_HEIGHT";
            this.gridColumnORD_HEIGHT.Name = "gridColumnORD_HEIGHT";
            this.gridColumnORD_HEIGHT.Visible = true;
            this.gridColumnORD_HEIGHT.VisibleIndex = 9;
            // 
            // gridColumnOPENCLOSE
            // 
            this.gridColumnOPENCLOSE.Caption = "Nyitva tartás";
            this.gridColumnOPENCLOSE.FieldName = "OPENCLOSE";
            this.gridColumnOPENCLOSE.Name = "gridColumnOPENCLOSE";
            this.gridColumnOPENCLOSE.Visible = true;
            this.gridColumnOPENCLOSE.VisibleIndex = 10;
            // 
            // gridColumnORD_NUM
            // 
            this.gridColumnORD_NUM.Caption = "Megr.sz.";
            this.gridColumnORD_NUM.FieldName = "ORD_NUM";
            this.gridColumnORD_NUM.Name = "gridColumnORD_NUM";
            this.gridColumnORD_NUM.Visible = true;
            this.gridColumnORD_NUM.VisibleIndex = 5;
            // 
            // gridColumnDEP_CODE
            // 
            this.gridColumnDEP_CODE.Caption = "Lerakókód";
            this.gridColumnDEP_CODE.FieldName = "DEP_CODE";
            this.gridColumnDEP_CODE.Name = "gridColumnDEP_CODE";
            this.gridColumnDEP_CODE.Visible = true;
            this.gridColumnDEP_CODE.VisibleIndex = 1;
            // 
            // gridColumnTRK_ID
            // 
            this.gridColumnTRK_ID.Caption = "Jármű";
            this.gridColumnTRK_ID.FieldName = "TRK_ID";
            this.gridColumnTRK_ID.Name = "gridColumnTRK_ID";
            // 
            // gridColumnTRK_CODE
            // 
            this.gridColumnTRK_CODE.Caption = "Jármű";
            this.gridColumnTRK_CODE.FieldName = "TRK_CODE";
            this.gridColumnTRK_CODE.Name = "gridColumnTRK_CODE";
            this.gridColumnTRK_CODE.Visible = true;
            this.gridColumnTRK_CODE.VisibleIndex = 11;
            // 
            // gridColumnTRK_REG_NUM
            // 
            this.gridColumnTRK_REG_NUM.Caption = "Rendszám";
            this.gridColumnTRK_REG_NUM.FieldName = "TRK_REG_NUM";
            this.gridColumnTRK_REG_NUM.Name = "gridColumnTRK_REG_NUM";
            this.gridColumnTRK_REG_NUM.Visible = true;
            this.gridColumnTRK_REG_NUM.VisibleIndex = 12;
            // 
            // gridColumnTPL_ID
            // 
            this.gridColumnTPL_ID.FieldName = "TPL_ID";
            this.gridColumnTPL_ID.Name = "gridColumnTPL_ID";
            // 
            // gridColumnPTP_ID
            // 
            this.gridColumnPTP_ID.FieldName = "PTP_ID";
            this.gridColumnPTP_ID.Name = "gridColumnPTP_ID";
            // 
            // reChkVisible
            // 
            this.reChkVisible.AutoHeight = false;
            this.reChkVisible.Name = "reChkVisible";
            // 
            // pnlPPlanOrders
            // 
            this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 438);
            this.Controls.Add(this.gridPlanOrders);
            this.Name = "pnlPPlanOrders";
            this.Text = "Megrendelések";
            ((System.ComponentModel.ISupportInitialize)(this.gridPlanOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlanOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reChkVisible)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDEP_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOD_QTY;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOD_QTY_INC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOD_VOLUME;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reChkVisible;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDEP_ADDR;
        public DevExpress.XtraGrid.GridControl gridPlanOrders;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewPlanOrders;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_LENGTH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_WIDTH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_HEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOPENCLOSE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnORD_NUM;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDEP_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_REG_NUM;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTPL_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPTP_ID;

    }
}