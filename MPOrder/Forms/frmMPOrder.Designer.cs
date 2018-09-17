namespace MPOrder.Forms
{
    partial class frmMPOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMPOrder));
            this.btnExcelImport = new System.Windows.Forms.ToolStripButton();
            this.tsbExportItems = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlus = new System.Windows.Forms.ToolStripButton();
            this.btnMinus = new System.Windows.Forms.ToolStripButton();
            this.tsMegr = new System.Windows.Forms.ToolStrip();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpMegrF = new System.Windows.Forms.GroupBox();
            this.gridMegrF = new DevExpress.XtraGrid.GridControl();
            this.gridViewMegrF = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grcSentToCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.edSentToCT = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grcCustomerOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcCustomerOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricShippingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricWarehouseCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcTotalGrossWeightOfOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcNumberOfPalletForDel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcShippAddressID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcShippAddressCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricShippingAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRMultiplierSum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricConfPlannedQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcGrossWeightPlanned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridMegrT = new DevExpress.XtraGrid.GridControl();
            this.gridViewMegrT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gricID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcRowNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcProdDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcU_M = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcConfOrderQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPalletOrderQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPalletPlannedQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPalletBulkQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricADR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRMultiplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRLimitedQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcConfPlannedQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.edConfPlannedQtyX = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gricGrossWeightPlanned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtmOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
            this.grcConfPlannedQtyX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricGrossWeightPlannedX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tsMegr.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpMegrF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edSentToCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edConfPlannedQtyX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExcelImport
            // 
            this.btnExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcelImport.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelImport.Image")));
            this.btnExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcelImport.Name = "btnExcelImport";
            this.btnExcelImport.Size = new System.Drawing.Size(24, 22);
            this.btnExcelImport.Text = "&Export Excelbe";
            this.btnExcelImport.Click += new System.EventHandler(this.btnExcelImport_Click);
            // 
            // tsbExportItems
            // 
            this.tsbExportItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExportItems.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportItems.Image")));
            this.tsbExportItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportItems.Name = "tsbExportItems";
            this.tsbExportItems.Size = new System.Drawing.Size(24, 22);
            this.tsbExportItems.Text = "&Tételek exportja Excelbe";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPlus
            // 
            this.btnPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnPlus.Image")));
            this.btnPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(24, 22);
            this.btnPlus.Text = "Összes csoport kinyitása";
            // 
            // btnMinus
            // 
            this.btnMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMinus.Image = ((System.Drawing.Image)(resources.GetObject("btnMinus.Image")));
            this.btnMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(24, 22);
            this.btnMinus.Text = "Összes csoport bezárása";
            // 
            // tsMegr
            // 
            this.tsMegr.AutoSize = false;
            this.tsMegr.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsMegr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnQuit,
            this.btnExcelImport,
            this.tsbExportItems,
            this.toolStripSeparator2,
            this.btnPlus,
            this.btnMinus});
            this.tsMegr.Location = new System.Drawing.Point(0, 0);
            this.tsMegr.Name = "tsMegr";
            this.tsMegr.Size = new System.Drawing.Size(1167, 25);
            this.tsMegr.TabIndex = 10;
            // 
            // btnQuit
            // 
            this.btnQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(24, 22);
            this.btnQuit.Text = "Kilépés";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpFilter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1167, 594);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpMegrF);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridMegrT);
            this.splitContainer1.Size = new System.Drawing.Size(1161, 528);
            this.splitContainer1.SplitterDistance = 276;
            this.splitContainer1.TabIndex = 1;
            // 
            // grpMegrF
            // 
            this.grpMegrF.Controls.Add(this.gridMegrF);
            this.grpMegrF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMegrF.Location = new System.Drawing.Point(0, 0);
            this.grpMegrF.Name = "grpMegrF";
            this.grpMegrF.Size = new System.Drawing.Size(1161, 276);
            this.grpMegrF.TabIndex = 11;
            this.grpMegrF.TabStop = false;
            // 
            // gridMegrF
            // 
            this.gridMegrF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMegrF.Location = new System.Drawing.Point(3, 16);
            this.gridMegrF.MainView = this.gridViewMegrF;
            this.gridMegrF.Name = "gridMegrF";
            this.gridMegrF.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.edSentToCT});
            this.gridMegrF.Size = new System.Drawing.Size(1155, 257);
            this.gridMegrF.TabIndex = 9;
            this.gridMegrF.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMegrF,
            this.gridView1,
            this.gridView2});
            // 
            // gridViewMegrF
            // 
            this.gridViewMegrF.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grcSentToCT,
            this.grcCustomerOrderNumber,
            this.grcCustomerOrderDate,
            this.gricShippingDate,
            this.gricWarehouseCode,
            this.grcTotalGrossWeightOfOrder,
            this.grcNumberOfPalletForDel,
            this.grcShippAddressID,
            this.grcShippAddressCompanyName,
            this.gricShippingAddress,
            this.gricNote,
            this.gricConfPlannedQty,
            this.grcGrossWeightPlanned,
            this.grcADRMultiplierSum});
            this.gridViewMegrF.GridControl = this.gridMegrF;
            this.gridViewMegrF.Name = "gridViewMegrF";
            this.gridViewMegrF.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMegrF.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMegrF.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMegrF.OptionsView.ShowFooter = true;
            this.gridViewMegrF.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewMegrF_FocusedRowChanged);
            // 
            // grcSentToCT
            // 
            this.grcSentToCT.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grcSentToCT.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grcSentToCT.AppearanceCell.Options.UseBackColor = true;
            this.grcSentToCT.Caption = "->CT";
            this.grcSentToCT.ColumnEdit = this.edSentToCT;
            this.grcSentToCT.FieldName = "SentToCT";
            this.grcSentToCT.Name = "grcSentToCT";
            this.grcSentToCT.Visible = true;
            this.grcSentToCT.VisibleIndex = 0;
            this.grcSentToCT.Width = 41;
            // 
            // edSentToCT
            // 
            this.edSentToCT.AutoHeight = false;
            this.edSentToCT.Name = "edSentToCT";
            // 
            // grcCustomerOrderNumber
            // 
            this.grcCustomerOrderNumber.Caption = "Vevő rendelés száma";
            this.grcCustomerOrderNumber.FieldName = "CustomerOrderNumber";
            this.grcCustomerOrderNumber.Name = "grcCustomerOrderNumber";
            this.grcCustomerOrderNumber.OptionsColumn.AllowEdit = false;
            this.grcCustomerOrderNumber.Visible = true;
            this.grcCustomerOrderNumber.VisibleIndex = 1;
            this.grcCustomerOrderNumber.Width = 84;
            // 
            // grcCustomerOrderDate
            // 
            this.grcCustomerOrderDate.Caption = "Vevő rendelés dátuma";
            this.grcCustomerOrderDate.FieldName = "CustomerOrderDate";
            this.grcCustomerOrderDate.Name = "grcCustomerOrderDate";
            this.grcCustomerOrderDate.OptionsColumn.AllowEdit = false;
            this.grcCustomerOrderDate.Visible = true;
            this.grcCustomerOrderDate.VisibleIndex = 2;
            this.grcCustomerOrderDate.Width = 84;
            // 
            // gricShippingDate
            // 
            this.gricShippingDate.Caption = "Szállítási dátum";
            this.gricShippingDate.FieldName = "ShippingDate";
            this.gricShippingDate.Name = "gricShippingDate";
            this.gricShippingDate.OptionsColumn.AllowEdit = false;
            this.gricShippingDate.Visible = true;
            this.gricShippingDate.VisibleIndex = 3;
            this.gricShippingDate.Width = 84;
            // 
            // gricWarehouseCode
            // 
            this.gricWarehouseCode.Caption = "Raktár kód";
            this.gricWarehouseCode.FieldName = "WarehouseCode";
            this.gricWarehouseCode.Name = "gricWarehouseCode";
            this.gricWarehouseCode.OptionsColumn.AllowEdit = false;
            this.gricWarehouseCode.Visible = true;
            this.gricWarehouseCode.VisibleIndex = 4;
            this.gricWarehouseCode.Width = 84;
            // 
            // grcTotalGrossWeightOfOrder
            // 
            this.grcTotalGrossWeightOfOrder.Caption = "Rendelés Össz bruttó súly";
            this.grcTotalGrossWeightOfOrder.DisplayFormat.FormatString = "#,#0.00";
            this.grcTotalGrossWeightOfOrder.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcTotalGrossWeightOfOrder.FieldName = "TotalGrossWeightOfOrder";
            this.grcTotalGrossWeightOfOrder.Name = "grcTotalGrossWeightOfOrder";
            this.grcTotalGrossWeightOfOrder.OptionsColumn.AllowEdit = false;
            this.grcTotalGrossWeightOfOrder.Visible = true;
            this.grcTotalGrossWeightOfOrder.VisibleIndex = 5;
            this.grcTotalGrossWeightOfOrder.Width = 84;
            // 
            // grcNumberOfPalletForDel
            // 
            this.grcNumberOfPalletForDel.Caption = "Szállítandó raklapok száma";
            this.grcNumberOfPalletForDel.DisplayFormat.FormatString = "#,#0.00";
            this.grcNumberOfPalletForDel.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcNumberOfPalletForDel.FieldName = "NumberOfPalletForDel";
            this.grcNumberOfPalletForDel.Name = "grcNumberOfPalletForDel";
            this.grcNumberOfPalletForDel.OptionsColumn.AllowEdit = false;
            this.grcNumberOfPalletForDel.Visible = true;
            this.grcNumberOfPalletForDel.VisibleIndex = 6;
            this.grcNumberOfPalletForDel.Width = 84;
            // 
            // grcShippAddressID
            // 
            this.grcShippAddressID.Caption = "Szállítási Cím ID";
            this.grcShippAddressID.FieldName = "ShippAddressID";
            this.grcShippAddressID.Name = "grcShippAddressID";
            this.grcShippAddressID.OptionsColumn.AllowEdit = false;
            this.grcShippAddressID.Visible = true;
            this.grcShippAddressID.VisibleIndex = 7;
            this.grcShippAddressID.Width = 84;
            // 
            // grcShippAddressCompanyName
            // 
            this.grcShippAddressCompanyName.Caption = "Cégnév";
            this.grcShippAddressCompanyName.FieldName = "ShippAddressCompanyName";
            this.grcShippAddressCompanyName.Name = "grcShippAddressCompanyName";
            this.grcShippAddressCompanyName.OptionsColumn.AllowEdit = false;
            this.grcShippAddressCompanyName.Visible = true;
            this.grcShippAddressCompanyName.VisibleIndex = 8;
            this.grcShippAddressCompanyName.Width = 84;
            // 
            // gricShippingAddress
            // 
            this.gricShippingAddress.Caption = "Cím";
            this.gricShippingAddress.FieldName = "ShippingAddress";
            this.gricShippingAddress.Name = "gricShippingAddress";
            this.gricShippingAddress.OptionsColumn.AllowEdit = false;
            this.gricShippingAddress.Visible = true;
            this.gricShippingAddress.VisibleIndex = 9;
            this.gricShippingAddress.Width = 84;
            // 
            // gricNote
            // 
            this.gricNote.Caption = "Megjegyzés";
            this.gricNote.FieldNameSortGroup = "Note";
            this.gricNote.Name = "gricNote";
            this.gricNote.OptionsColumn.AllowEdit = false;
            this.gricNote.Visible = true;
            this.gricNote.VisibleIndex = 10;
            this.gricNote.Width = 84;
            // 
            // grcADRMultiplierSum
            // 
            this.grcADRMultiplierSum.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcADRMultiplierSum.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grcADRMultiplierSum.AppearanceCell.Options.UseBackColor = true;
            this.grcADRMultiplierSum.Caption = "ADR össz";
            this.grcADRMultiplierSum.DisplayFormat.FormatString = "#,#0.00";
            this.grcADRMultiplierSum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcADRMultiplierSum.FieldName = "ADRMultiplierSum";
            this.grcADRMultiplierSum.Name = "grcADRMultiplierSum";
            this.grcADRMultiplierSum.OptionsColumn.AllowEdit = false;
            this.grcADRMultiplierSum.Visible = true;
            this.grcADRMultiplierSum.VisibleIndex = 13;
            this.grcADRMultiplierSum.Width = 85;
            // 
            // gricConfPlannedQty
            // 
            this.gricConfPlannedQty.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gricConfPlannedQty.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.gricConfPlannedQty.AppearanceCell.Options.UseBackColor = true;
            this.gricConfPlannedQty.Caption = "Szállítandó mennyiség";
            this.gricConfPlannedQty.DisplayFormat.FormatString = "#,#0.00";
            this.gricConfPlannedQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gricConfPlannedQty.FieldName = "ConfPlannedQty";
            this.gricConfPlannedQty.Name = "gricConfPlannedQty";
            this.gricConfPlannedQty.OptionsColumn.AllowEdit = false;
            this.gricConfPlannedQty.Visible = true;
            this.gricConfPlannedQty.VisibleIndex = 11;
            this.gricConfPlannedQty.Width = 84;
            // 
            // grcGrossWeightPlanned
            // 
            this.grcGrossWeightPlanned.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcGrossWeightPlanned.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grcGrossWeightPlanned.AppearanceCell.Options.UseBackColor = true;
            this.grcGrossWeightPlanned.Caption = "Szállítandó bruttó súly";
            this.grcGrossWeightPlanned.DisplayFormat.FormatString = "#,#0.00";
            this.grcGrossWeightPlanned.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcGrossWeightPlanned.FieldName = "GrossWeightPlanned";
            this.grcGrossWeightPlanned.Name = "grcGrossWeightPlanned";
            this.grcGrossWeightPlanned.OptionsColumn.AllowEdit = false;
            this.grcGrossWeightPlanned.Visible = true;
            this.grcGrossWeightPlanned.VisibleIndex = 12;
            this.grcGrossWeightPlanned.Width = 84;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridMegrF;
            this.gridView1.Name = "gridView1";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridMegrF;
            this.gridView2.Name = "gridView2";
            // 
            // gridMegrT
            // 
            this.gridMegrT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMegrT.Location = new System.Drawing.Point(0, 0);
            this.gridMegrT.MainView = this.gridViewMegrT;
            this.gridMegrT.Name = "gridMegrT";
            this.gridMegrT.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.edConfPlannedQtyX});
            this.gridMegrT.Size = new System.Drawing.Size(1161, 248);
            this.gridMegrT.TabIndex = 10;
            this.gridMegrT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMegrT,
            this.gridView3});
            // 
            // gridViewMegrT
            // 
            this.gridViewMegrT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gricID,
            this.grcRowNumber,
            this.grcProductCode,
            this.grcProdDescription,
            this.grcU_M,
            this.grcConfOrderQty,
            this.grcPalletOrderQty,
            this.grcPalletPlannedQty,
            this.grcPalletBulkQty,
            this.gricADR,
            this.grcADRMultiplier,
            this.grcADRLimitedQuantity,
            this.grcConfPlannedQty,
            this.gricGrossWeightPlanned,
            this.grcConfPlannedQtyX,
            this.gricGrossWeightPlannedX});
            this.gridViewMegrT.GridControl = this.gridMegrT;
            this.gridViewMegrT.Name = "gridViewMegrT";
            this.gridViewMegrT.OptionsCustomization.AllowGroup = false;
            this.gridViewMegrT.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMegrT.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMegrT.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMegrT.OptionsView.ShowFooter = true;
            this.gridViewMegrT.OptionsView.ShowGroupPanel = false;
            // 
            // gricID
            // 
            this.gricID.Caption = "ID";
            this.gricID.FieldName = "ID";
            this.gricID.Name = "gricID";
            this.gricID.OptionsColumn.AllowEdit = false;
            // 
            // grcRowNumber
            // 
            this.grcRowNumber.Caption = "Sor száma";
            this.grcRowNumber.FieldName = "RowNumber";
            this.grcRowNumber.Name = "grcRowNumber";
            this.grcRowNumber.OptionsColumn.AllowEdit = false;
            this.grcRowNumber.Visible = true;
            this.grcRowNumber.VisibleIndex = 0;
            // 
            // grcProductCode
            // 
            this.grcProductCode.Caption = "Termékkód";
            this.grcProductCode.FieldName = "ProductCode";
            this.grcProductCode.Name = "grcProductCode";
            this.grcProductCode.OptionsColumn.AllowEdit = false;
            this.grcProductCode.Visible = true;
            this.grcProductCode.VisibleIndex = 1;
            // 
            // grcProdDescription
            // 
            this.grcProdDescription.Caption = "Megnevezés";
            this.grcProdDescription.FieldName = "ProdDescription";
            this.grcProdDescription.Name = "grcProdDescription";
            this.grcProdDescription.OptionsColumn.AllowEdit = false;
            this.grcProdDescription.Visible = true;
            this.grcProdDescription.VisibleIndex = 2;
            // 
            // grcU_M
            // 
            this.grcU_M.Caption = "M.e";
            this.grcU_M.FieldName = "U_M";
            this.grcU_M.Name = "grcU_M";
            this.grcU_M.OptionsColumn.AllowEdit = false;
            this.grcU_M.Visible = true;
            this.grcU_M.VisibleIndex = 3;
            // 
            // grcConfOrderQty
            // 
            this.grcConfOrderQty.Caption = "Rendelt br.súly";
            this.grcConfOrderQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcConfOrderQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcConfOrderQty.FieldName = "ConfOrderQty";
            this.grcConfOrderQty.Name = "grcConfOrderQty";
            this.grcConfOrderQty.OptionsColumn.AllowEdit = false;
            this.grcConfOrderQty.Visible = true;
            this.grcConfOrderQty.VisibleIndex = 4;
            // 
            // grcPalletOrderQty
            // 
            this.grcPalletOrderQty.Caption = "Rendelt raklap";
            this.grcPalletOrderQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcPalletOrderQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcPalletOrderQty.FieldName = "PalletOrderQty";
            this.grcPalletOrderQty.Name = "grcPalletOrderQty";
            this.grcPalletOrderQty.OptionsColumn.AllowEdit = false;
            this.grcPalletOrderQty.Visible = true;
            this.grcPalletOrderQty.VisibleIndex = 5;
            // 
            // grcPalletPlannedQty
            // 
            this.grcPalletPlannedQty.Caption = "Szállítandó raklap";
            this.grcPalletPlannedQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcPalletPlannedQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcPalletPlannedQty.FieldName = "PalletPlannedQty";
            this.grcPalletPlannedQty.Name = "grcPalletPlannedQty";
            this.grcPalletPlannedQty.OptionsColumn.AllowEdit = false;
            this.grcPalletPlannedQty.Visible = true;
            this.grcPalletPlannedQty.VisibleIndex = 6;
            // 
            // grcPalletBulkQty
            // 
            this.grcPalletBulkQty.Caption = "PalletBulkQty";
            this.grcPalletBulkQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcPalletBulkQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcPalletBulkQty.FieldName = "PalletBulkQty";
            this.grcPalletBulkQty.Name = "grcPalletBulkQty";
            this.grcPalletBulkQty.OptionsColumn.AllowEdit = false;
            this.grcPalletBulkQty.Visible = true;
            this.grcPalletBulkQty.VisibleIndex = 7;
            // 
            // gricADR
            // 
            this.gricADR.Caption = "ADR";
            this.gricADR.FieldName = "ADR";
            this.gricADR.Name = "gricADR";
            this.gricADR.OptionsColumn.AllowEdit = false;
            // 
            // grcADRMultiplier
            // 
            this.grcADRMultiplier.Caption = "ADR szorzó";
            this.grcADRMultiplier.DisplayFormat.FormatString = "#,#0.00";
            this.grcADRMultiplier.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcADRMultiplier.FieldName = "ADRMultiplier";
            this.grcADRMultiplier.Name = "grcADRMultiplier";
            this.grcADRMultiplier.OptionsColumn.AllowEdit = false;
            this.grcADRMultiplier.Visible = true;
            this.grcADRMultiplier.VisibleIndex = 8;
            // 
            // grcADRLimitedQuantity
            // 
            this.grcADRLimitedQuantity.Caption = "ADR köteles mennyiség";
            this.grcADRLimitedQuantity.FieldName = "ADRLimitedQuantity";
            this.grcADRLimitedQuantity.Name = "grcADRLimitedQuantity";
            this.grcADRLimitedQuantity.OptionsColumn.AllowEdit = false;
            this.grcADRLimitedQuantity.Visible = true;
            this.grcADRLimitedQuantity.VisibleIndex = 9;
            // 
            // grcConfPlannedQty
            // 
            this.grcConfPlannedQty.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grcConfPlannedQty.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grcConfPlannedQty.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.grcConfPlannedQty.AppearanceCell.Options.UseBackColor = true;
            this.grcConfPlannedQty.AppearanceCell.Options.UseBorderColor = true;
            this.grcConfPlannedQty.Caption = "Eredeti szállítandó mennyiség";
            this.grcConfPlannedQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcConfPlannedQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcConfPlannedQty.FieldName = "ConfPlannedQty";
            this.grcConfPlannedQty.Name = "grcConfPlannedQty";
            this.grcConfPlannedQty.Visible = true;
            this.grcConfPlannedQty.VisibleIndex = 10;
            // 
            // edConfPlannedQtyX
            // 
            this.edConfPlannedQtyX.AutoHeight = false;
            this.edConfPlannedQtyX.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edConfPlannedQtyX.Name = "edConfPlannedQtyX";
            this.edConfPlannedQtyX.EditValueChanged += new System.EventHandler(this.edConfPlannedQty_EditValueChanged);
            // 
            // gricGrossWeightPlanned
            // 
            this.gricGrossWeightPlanned.Caption = "!Szállítandó bruttó súly";
            this.gricGrossWeightPlanned.DisplayFormat.FormatString = "#,#0.00";
            this.gricGrossWeightPlanned.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gricGrossWeightPlanned.FieldName = "GrossWeightPlanned";
            this.gricGrossWeightPlanned.Name = "gricGrossWeightPlanned";
            this.gricGrossWeightPlanned.OptionsColumn.AllowEdit = false;
            this.gricGrossWeightPlanned.Visible = true;
            this.gricGrossWeightPlanned.VisibleIndex = 11;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridMegrT;
            this.gridView3.Name = "gridView3";
            // 
            // grpFilter
            // 
            this.grpFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpFilter.Controls.Add(this.tableLayoutPanel2);
            this.grpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFilter.Location = new System.Drawing.Point(3, 3);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(1161, 54);
            this.grpFilter.TabIndex = 10;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Szűrő";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Controls.Add(this.dtmOrderDate, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1155, 35);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // dtmOrderDate
            // 
            this.dtmOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtmOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmOrderDate.Location = new System.Drawing.Point(123, 7);
            this.dtmOrderDate.Name = "dtmOrderDate";
            this.dtmOrderDate.Size = new System.Drawing.Size(194, 20);
            this.dtmOrderDate.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Dátum";
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.BackColor = System.Drawing.Color.Yellow;
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.Location = new System.Drawing.Point(1123, 4);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(29, 26);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // openExcel
            // 
            this.openExcel.CheckFileExists = false;
            this.openExcel.DefaultExt = "xls";
            this.openExcel.Filter = "*.xlsx|*.xlsx";
            // 
            // grcConfPlannedQtyX
            // 
            this.grcConfPlannedQtyX.Caption = "Szállítandó mennyiség";
            this.grcConfPlannedQtyX.ColumnEdit = this.edConfPlannedQtyX;
            this.grcConfPlannedQtyX.DisplayFormat.FormatString = "#,#0.00";
            this.grcConfPlannedQtyX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcConfPlannedQtyX.FieldName = "ConfPlannedQtyX";
            this.grcConfPlannedQtyX.Name = "grcConfPlannedQtyX";
            this.grcConfPlannedQtyX.Visible = true;
            this.grcConfPlannedQtyX.VisibleIndex = 12;
            // 
            // gricGrossWeightPlannedX
            // 
            this.gricGrossWeightPlannedX.Caption = "!Szállítandó bruttó súly";
            this.gricGrossWeightPlannedX.DisplayFormat.FormatString = "#,#0.00";
            this.gricGrossWeightPlannedX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gricGrossWeightPlannedX.FieldName = "GrossWeightPlannedX";
            this.gricGrossWeightPlannedX.Name = "gricGrossWeightPlannedX";
            this.gricGrossWeightPlannedX.Visible = true;
            this.gricGrossWeightPlannedX.VisibleIndex = 13;
            // 
            // frmMPOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 619);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tsMegr);
            this.Name = "frmMPOrder";
            this.Text = "frmMPOrder";
            this.tsMegr.ResumeLayout(false);
            this.tsMegr.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpMegrF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edSentToCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edConfPlannedQtyX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripButton btnExcelImport;
        private System.Windows.Forms.ToolStripButton tsbExportItems;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnPlus;
        private System.Windows.Forms.ToolStripButton btnMinus;
        private System.Windows.Forms.ToolStrip tsMegr;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpMegrF;
        private DevExpress.XtraGrid.GridControl gridMegrF;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMegrF;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridMegrT;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMegrT;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilter;
        private DevExpress.XtraGrid.Columns.GridColumn grcCustomerOrderNumber;
        private DevExpress.XtraGrid.Columns.GridColumn grcCustomerOrderDate;
        private DevExpress.XtraGrid.Columns.GridColumn gricShippingDate;
        private DevExpress.XtraGrid.Columns.GridColumn gricWarehouseCode;
        private DevExpress.XtraGrid.Columns.GridColumn grcTotalGrossWeightOfOrder;
        private DevExpress.XtraGrid.Columns.GridColumn grcNumberOfPalletForDel;
        private DevExpress.XtraGrid.Columns.GridColumn grcShippAddressID;
        private DevExpress.XtraGrid.Columns.GridColumn grcShippAddressCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn gricShippingAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gricNote;
        private DevExpress.XtraGrid.Columns.GridColumn gricConfPlannedQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcGrossWeightPlanned;
        private DevExpress.XtraGrid.Columns.GridColumn gricID;
        private DevExpress.XtraGrid.Columns.GridColumn grcRowNumber;
        private DevExpress.XtraGrid.Columns.GridColumn grcProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn grcProdDescription;
        private DevExpress.XtraGrid.Columns.GridColumn grcU_M;
        private DevExpress.XtraGrid.Columns.GridColumn grcConfOrderQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcConfPlannedQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcPalletOrderQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcPalletPlannedQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcPalletBulkQty;
        private DevExpress.XtraGrid.Columns.GridColumn gricGrossWeightPlanned;
        private DevExpress.XtraGrid.Columns.GridColumn gricADR;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRMultiplier;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRLimitedQuantity;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private System.Windows.Forms.OpenFileDialog openExcel;
        private System.Windows.Forms.DateTimePicker dtmOrderDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edConfPlannedQtyX;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRMultiplierSum;
        private DevExpress.XtraGrid.Columns.GridColumn grcSentToCT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit edSentToCT;
        private DevExpress.XtraGrid.Columns.GridColumn grcConfPlannedQtyX;
        private DevExpress.XtraGrid.Columns.GridColumn gricGrossWeightPlannedX;
    }
}