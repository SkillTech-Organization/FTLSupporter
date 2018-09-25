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
            this.btnSelAll = new System.Windows.Forms.ToolStripButton();
            this.btnDeselAll = new System.Windows.Forms.ToolStripButton();
            this.tsMegr = new System.Windows.Forms.ToolStrip();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpMegrF = new System.Windows.Forms.GroupBox();
            this.gridMegrF = new DevExpress.XtraGrid.GridControl();
            this.gridViewMegrF = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grcSentToCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.edSentToCT = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grcCustomerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcCustomerOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcCustomerOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricShippingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricWarehouseCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcNumberOfPalletForDel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcShippAddressID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcShippAddressCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricShippingAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcConfPlannedQtySum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcGrossWeightPlannedSum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRMultiplierXSum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcTotalGrossWeightOfOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridMegrT = new DevExpress.XtraGrid.GridControl();
            this.gridViewMegrT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grcID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcRowNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcProdDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcU_M = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPalletOrderQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPalletPlannedQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPalletBulkQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRLimitedQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcConfOrderQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcConfPlannedQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.edConfPlannedQty = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.grcUnitWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gricGrossWeightPlanned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRMultiplierX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcADRMultiplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtmOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
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
            ((System.ComponentModel.ISupportInitialize)(this.edConfPlannedQty)).BeginInit();
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
            this.btnExcelImport.Text = "&Excel állomány beolvasása";
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
            this.tsbExportItems.Click += new System.EventHandler(this.tsbExportItems_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSelAll
            // 
            this.btnSelAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelAll.Image")));
            this.btnSelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(24, 22);
            this.btnSelAll.Text = "Összes  kijelölése";
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnDeselAll
            // 
            this.btnDeselAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeselAll.Image = ((System.Drawing.Image)(resources.GetObject("btnDeselAll.Image")));
            this.btnDeselAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeselAll.Name = "btnDeselAll";
            this.btnDeselAll.Size = new System.Drawing.Size(24, 22);
            this.btnDeselAll.Text = "Összes kijelölés visszavonása";
            this.btnDeselAll.Click += new System.EventHandler(this.btnDeselAll_Click);
            // 
            // tsMegr
            // 
            this.tsMegr.AutoSize = false;
            this.tsMegr.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsMegr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnQuit,
            this.toolStripSeparator1,
            this.btnExcelImport,
            this.toolStripSeparator2,
            this.btnSelAll,
            this.btnDeselAll,
            this.toolStripSeparator3,
            this.btnSend,
            this.toolStripSeparator4,
            this.tsbExportItems});
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSend
            // 
            this.btnSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(24, 22);
            this.btnSend.Text = "Küldés CorrectTour-ba";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            this.gridMegrF.EmbeddedNavigator.TextStringFormat = "Record {0} of {1}";
            this.gridMegrF.Location = new System.Drawing.Point(3, 17);
            this.gridMegrF.MainView = this.gridViewMegrF;
            this.gridMegrF.Name = "gridMegrF";
            this.gridMegrF.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.edSentToCT});
            this.gridMegrF.Size = new System.Drawing.Size(1155, 256);
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
            this.grcCustomerCode,
            this.grcCustomerOrderNumber,
            this.grcCustomerOrderDate,
            this.gricShippingDate,
            this.gricWarehouseCode,
            this.grcNumberOfPalletForDel,
            this.grcShippAddressID,
            this.grcShippAddressCompanyName,
            this.gricShippingAddress,
            this.gricNote,
            this.grcConfPlannedQtySum,
            this.grcGrossWeightPlannedSum,
            this.grcADRMultiplierXSum,
            this.grcTotalGrossWeightOfOrder});
            this.gridViewMegrF.GridControl = this.gridMegrF;
            this.gridViewMegrF.Name = "gridViewMegrF";
            this.gridViewMegrF.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMegrF.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMegrF.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMegrF.OptionsView.ShowFooter = true;
            this.gridViewMegrF.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewMegrF_CustomDrawCell);
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
            this.edSentToCT.EditValueChanged += new System.EventHandler(this.edSentToCT_EditValueChanged);
            this.edSentToCT.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.edSentToCT_EditValueChanging);
            // 
            // grcCustomerCode
            // 
            this.grcCustomerCode.Caption = "Vevőkód";
            this.grcCustomerCode.FieldName = "CustomerCode";
            this.grcCustomerCode.Name = "grcCustomerCode";
            this.grcCustomerCode.OptionsColumn.AllowEdit = false;
            this.grcCustomerCode.Visible = true;
            this.grcCustomerCode.VisibleIndex = 1;
            // 
            // grcCustomerOrderNumber
            // 
            this.grcCustomerOrderNumber.Caption = "Vevő rendelés száma";
            this.grcCustomerOrderNumber.FieldName = "CustomerOrderNumber";
            this.grcCustomerOrderNumber.Name = "grcCustomerOrderNumber";
            this.grcCustomerOrderNumber.OptionsColumn.AllowEdit = false;
            this.grcCustomerOrderNumber.Visible = true;
            this.grcCustomerOrderNumber.VisibleIndex = 2;
            this.grcCustomerOrderNumber.Width = 84;
            // 
            // grcCustomerOrderDate
            // 
            this.grcCustomerOrderDate.Caption = "Vevő rendelés dátuma";
            this.grcCustomerOrderDate.FieldName = "CustomerOrderDate";
            this.grcCustomerOrderDate.Name = "grcCustomerOrderDate";
            this.grcCustomerOrderDate.OptionsColumn.AllowEdit = false;
            this.grcCustomerOrderDate.Visible = true;
            this.grcCustomerOrderDate.VisibleIndex = 3;
            this.grcCustomerOrderDate.Width = 84;
            // 
            // gricShippingDate
            // 
            this.gricShippingDate.Caption = "Szállítási dátum";
            this.gricShippingDate.FieldName = "ShippingDate";
            this.gricShippingDate.Name = "gricShippingDate";
            this.gricShippingDate.OptionsColumn.AllowEdit = false;
            this.gricShippingDate.Visible = true;
            this.gricShippingDate.VisibleIndex = 4;
            this.gricShippingDate.Width = 84;
            // 
            // gricWarehouseCode
            // 
            this.gricWarehouseCode.Caption = "Raktár kód";
            this.gricWarehouseCode.FieldName = "WarehouseCode";
            this.gricWarehouseCode.Name = "gricWarehouseCode";
            this.gricWarehouseCode.OptionsColumn.AllowEdit = false;
            this.gricWarehouseCode.Visible = true;
            this.gricWarehouseCode.VisibleIndex = 5;
            this.gricWarehouseCode.Width = 84;
            // 
            // grcNumberOfPalletForDel
            // 
            this.grcNumberOfPalletForDel.Caption = "Szállítandó raklapok száma";
            this.grcNumberOfPalletForDel.DisplayFormat.FormatString = "#,#0.00";
            this.grcNumberOfPalletForDel.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcNumberOfPalletForDel.FieldName = "NumberOfPalletForDel";
            this.grcNumberOfPalletForDel.Name = "grcNumberOfPalletForDel";
            this.grcNumberOfPalletForDel.OptionsColumn.AllowEdit = false;
            this.grcNumberOfPalletForDel.Width = 84;
            // 
            // grcShippAddressID
            // 
            this.grcShippAddressID.Caption = "Szállítási Cím ID";
            this.grcShippAddressID.FieldName = "ShippAddressID";
            this.grcShippAddressID.Name = "grcShippAddressID";
            this.grcShippAddressID.OptionsColumn.AllowEdit = false;
            this.grcShippAddressID.Visible = true;
            this.grcShippAddressID.VisibleIndex = 6;
            this.grcShippAddressID.Width = 84;
            // 
            // grcShippAddressCompanyName
            // 
            this.grcShippAddressCompanyName.Caption = "Cégnév";
            this.grcShippAddressCompanyName.FieldName = "ShippAddressCompanyName";
            this.grcShippAddressCompanyName.Name = "grcShippAddressCompanyName";
            this.grcShippAddressCompanyName.OptionsColumn.AllowEdit = false;
            this.grcShippAddressCompanyName.Visible = true;
            this.grcShippAddressCompanyName.VisibleIndex = 7;
            this.grcShippAddressCompanyName.Width = 84;
            // 
            // gricShippingAddress
            // 
            this.gricShippingAddress.Caption = "Cím";
            this.gricShippingAddress.FieldName = "ShippingAddress";
            this.gricShippingAddress.Name = "gricShippingAddress";
            this.gricShippingAddress.OptionsColumn.AllowEdit = false;
            this.gricShippingAddress.Visible = true;
            this.gricShippingAddress.VisibleIndex = 8;
            this.gricShippingAddress.Width = 84;
            // 
            // gricNote
            // 
            this.gricNote.Caption = "Megjegyzés";
            this.gricNote.FieldNameSortGroup = "Note";
            this.gricNote.Name = "gricNote";
            this.gricNote.OptionsColumn.AllowEdit = false;
            this.gricNote.Visible = true;
            this.gricNote.VisibleIndex = 9;
            this.gricNote.Width = 84;
            // 
            // grcConfPlannedQtySum
            // 
            this.grcConfPlannedQtySum.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcConfPlannedQtySum.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcConfPlannedQtySum.AppearanceCell.Options.UseBackColor = true;
            this.grcConfPlannedQtySum.Caption = "Szállítandó mennyiség";
            this.grcConfPlannedQtySum.DisplayFormat.FormatString = "#,#0.00";
            this.grcConfPlannedQtySum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcConfPlannedQtySum.FieldName = "ConfPlannedQtySum";
            this.grcConfPlannedQtySum.Name = "grcConfPlannedQtySum";
            this.grcConfPlannedQtySum.OptionsColumn.AllowEdit = false;
            this.grcConfPlannedQtySum.Visible = true;
            this.grcConfPlannedQtySum.VisibleIndex = 10;
            this.grcConfPlannedQtySum.Width = 84;
            // 
            // grcGrossWeightPlannedSum
            // 
            this.grcGrossWeightPlannedSum.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcGrossWeightPlannedSum.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcGrossWeightPlannedSum.AppearanceCell.Options.UseBackColor = true;
            this.grcGrossWeightPlannedSum.Caption = "Szállítandó bruttó súly";
            this.grcGrossWeightPlannedSum.DisplayFormat.FormatString = "#,#0.00";
            this.grcGrossWeightPlannedSum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcGrossWeightPlannedSum.FieldName = "GrossWeightPlannedSum";
            this.grcGrossWeightPlannedSum.Name = "grcGrossWeightPlannedSum";
            this.grcGrossWeightPlannedSum.OptionsColumn.AllowEdit = false;
            this.grcGrossWeightPlannedSum.Visible = true;
            this.grcGrossWeightPlannedSum.VisibleIndex = 11;
            this.grcGrossWeightPlannedSum.Width = 84;
            // 
            // grcADRMultiplierXSum
            // 
            this.grcADRMultiplierXSum.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcADRMultiplierXSum.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcADRMultiplierXSum.AppearanceCell.Options.UseBackColor = true;
            this.grcADRMultiplierXSum.Caption = "ADR össz";
            this.grcADRMultiplierXSum.DisplayFormat.FormatString = "#,#0.00";
            this.grcADRMultiplierXSum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcADRMultiplierXSum.FieldName = "ADRMultiplierXSum";
            this.grcADRMultiplierXSum.Name = "grcADRMultiplierXSum";
            this.grcADRMultiplierXSum.OptionsColumn.AllowEdit = false;
            this.grcADRMultiplierXSum.Visible = true;
            this.grcADRMultiplierXSum.VisibleIndex = 12;
            this.grcADRMultiplierXSum.Width = 85;
            // 
            // grcTotalGrossWeightOfOrder
            // 
            this.grcTotalGrossWeightOfOrder.Caption = "Rendelés Össz bruttó súly";
            this.grcTotalGrossWeightOfOrder.DisplayFormat.FormatString = "#,#0.00";
            this.grcTotalGrossWeightOfOrder.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcTotalGrossWeightOfOrder.FieldName = "TotalGrossWeightOfOrder";
            this.grcTotalGrossWeightOfOrder.Name = "grcTotalGrossWeightOfOrder";
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
            this.gridMegrT.EmbeddedNavigator.TextStringFormat = "Record {0} of {1}";
            this.gridMegrT.Location = new System.Drawing.Point(0, 0);
            this.gridMegrT.MainView = this.gridViewMegrT;
            this.gridMegrT.Name = "gridMegrT";
            this.gridMegrT.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.edConfPlannedQty});
            this.gridMegrT.Size = new System.Drawing.Size(1161, 248);
            this.gridMegrT.TabIndex = 10;
            this.gridMegrT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMegrT,
            this.gridView3});
            // 
            // gridViewMegrT
            // 
            this.gridViewMegrT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grcID,
            this.grcRowNumber,
            this.grcProductCode,
            this.grcProdDescription,
            this.grcU_M,
            this.grcPalletOrderQty,
            this.grcPalletPlannedQty,
            this.grcPalletBulkQty,
            this.grcADR,
            this.grcADRLimitedQuantity,
            this.grcConfOrderQty,
            this.grcConfPlannedQty,
            this.grcUnitWeight,
            this.gricGrossWeightPlanned,
            this.grcADRMultiplierX,
            this.grcADRMultiplier});
            this.gridViewMegrT.GridControl = this.gridMegrT;
            this.gridViewMegrT.Name = "gridViewMegrT";
            this.gridViewMegrT.OptionsCustomization.AllowGroup = false;
            this.gridViewMegrT.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMegrT.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMegrT.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMegrT.OptionsView.ShowFooter = true;
            this.gridViewMegrT.OptionsView.ShowGroupPanel = false;
            this.gridViewMegrT.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewMegrT_CustomDrawCell);
            // 
            // grcID
            // 
            this.grcID.Caption = "ID";
            this.grcID.FieldName = "ID";
            this.grcID.Name = "grcID";
            this.grcID.OptionsColumn.AllowEdit = false;
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
            // grcPalletOrderQty
            // 
            this.grcPalletOrderQty.Caption = "Rendelt raklap";
            this.grcPalletOrderQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcPalletOrderQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcPalletOrderQty.FieldName = "PalletOrderQty";
            this.grcPalletOrderQty.Name = "grcPalletOrderQty";
            this.grcPalletOrderQty.OptionsColumn.AllowEdit = false;
            // 
            // grcPalletPlannedQty
            // 
            this.grcPalletPlannedQty.Caption = "Szállítandó raklap";
            this.grcPalletPlannedQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcPalletPlannedQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcPalletPlannedQty.FieldName = "PalletPlannedQty";
            this.grcPalletPlannedQty.Name = "grcPalletPlannedQty";
            this.grcPalletPlannedQty.OptionsColumn.AllowEdit = false;
            // 
            // grcPalletBulkQty
            // 
            this.grcPalletBulkQty.Caption = "PalletBulkQty";
            this.grcPalletBulkQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcPalletBulkQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcPalletBulkQty.FieldName = "PalletBulkQty";
            this.grcPalletBulkQty.Name = "grcPalletBulkQty";
            this.grcPalletBulkQty.OptionsColumn.AllowEdit = false;
            // 
            // grcADR
            // 
            this.grcADR.Caption = "ADR";
            this.grcADR.FieldName = "ADR";
            this.grcADR.Name = "grcADR";
            this.grcADR.OptionsColumn.AllowEdit = false;
            this.grcADR.Visible = true;
            this.grcADR.VisibleIndex = 9;
            // 
            // grcADRLimitedQuantity
            // 
            this.grcADRLimitedQuantity.Caption = "ADR köteles mennyiség";
            this.grcADRLimitedQuantity.FieldName = "ADRLimitedQuantity";
            this.grcADRLimitedQuantity.Name = "grcADRLimitedQuantity";
            this.grcADRLimitedQuantity.OptionsColumn.AllowEdit = false;
            // 
            // grcConfOrderQty
            // 
            this.grcConfOrderQty.Caption = "Rendelt menny.";
            this.grcConfOrderQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcConfOrderQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcConfOrderQty.FieldName = "ConfOrderQty";
            this.grcConfOrderQty.Name = "grcConfOrderQty";
            this.grcConfOrderQty.OptionsColumn.AllowEdit = false;
            this.grcConfOrderQty.Visible = true;
            this.grcConfOrderQty.VisibleIndex = 4;
            // 
            // grcConfPlannedQty
            // 
            this.grcConfPlannedQty.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grcConfPlannedQty.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grcConfPlannedQty.AppearanceCell.Options.UseBackColor = true;
            this.grcConfPlannedQty.Caption = "Mennyiség";
            this.grcConfPlannedQty.ColumnEdit = this.edConfPlannedQty;
            this.grcConfPlannedQty.DisplayFormat.FormatString = "#,#0.00";
            this.grcConfPlannedQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcConfPlannedQty.FieldName = "ConfPlannedQty";
            this.grcConfPlannedQty.Name = "grcConfPlannedQty";
            this.grcConfPlannedQty.Visible = true;
            this.grcConfPlannedQty.VisibleIndex = 6;
            // 
            // edConfPlannedQty
            // 
            this.edConfPlannedQty.AutoHeight = false;
            this.edConfPlannedQty.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edConfPlannedQty.Name = "edConfPlannedQty";
            this.edConfPlannedQty.ValueChanged += new System.EventHandler(this.edConfPlannedQtyX_ValueChanged);
            this.edConfPlannedQty.EditValueChanged += new System.EventHandler(this.edConfPlannedQty_EditValueChanged);
            this.edConfPlannedQty.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.edConfPlannedQtyX_EditValueChanging);
            // 
            // grcUnitWeight
            // 
            this.grcUnitWeight.Caption = "Egységsúly";
            this.grcUnitWeight.DisplayFormat.FormatString = "#,#0.00";
            this.grcUnitWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcUnitWeight.FieldName = "UnitWeight";
            this.grcUnitWeight.Name = "grcUnitWeight";
            this.grcUnitWeight.OptionsColumn.AllowEdit = false;
            this.grcUnitWeight.Visible = true;
            this.grcUnitWeight.VisibleIndex = 5;
            // 
            // gricGrossWeightPlanned
            // 
            this.gricGrossWeightPlanned.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gricGrossWeightPlanned.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gricGrossWeightPlanned.AppearanceCell.Options.UseBackColor = true;
            this.gricGrossWeightPlanned.Caption = "Súly";
            this.gricGrossWeightPlanned.DisplayFormat.FormatString = "#,#0.00";
            this.gricGrossWeightPlanned.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gricGrossWeightPlanned.FieldName = "GrossWeightPlanned";
            this.gricGrossWeightPlanned.Name = "gricGrossWeightPlanned";
            this.gricGrossWeightPlanned.OptionsColumn.AllowEdit = false;
            this.gricGrossWeightPlanned.Visible = true;
            this.gricGrossWeightPlanned.VisibleIndex = 7;
            // 
            // grcADRMultiplierX
            // 
            this.grcADRMultiplierX.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcADRMultiplierX.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grcADRMultiplierX.AppearanceCell.Options.UseBackColor = true;
            this.grcADRMultiplierX.Caption = "ADR szorzó";
            this.grcADRMultiplierX.DisplayFormat.FormatString = "#,#0.00";
            this.grcADRMultiplierX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcADRMultiplierX.FieldName = "ADRMultiplierX";
            this.grcADRMultiplierX.Name = "grcADRMultiplierX";
            this.grcADRMultiplierX.OptionsColumn.AllowEdit = false;
            this.grcADRMultiplierX.Visible = true;
            this.grcADRMultiplierX.VisibleIndex = 8;
            // 
            // grcADRMultiplier
            // 
            this.grcADRMultiplier.Caption = "Eredeti ADR szorzó";
            this.grcADRMultiplier.FieldName = "ADRMultiplier";
            this.grcADRMultiplier.Name = "grcADRMultiplier";
            this.grcADRMultiplier.OptionsColumn.AllowEdit = false;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1155, 34);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // dtmOrderDate
            // 
            this.dtmOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtmOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmOrderDate.Location = new System.Drawing.Point(123, 6);
            this.dtmOrderDate.Name = "dtmOrderDate";
            this.dtmOrderDate.Size = new System.Drawing.Size(194, 21);
            this.dtmOrderDate.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
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
            // frmMPOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 619);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tsMegr);
            this.Name = "frmMPOrder";
            this.Text = "Megrendelések előkészítése";
            this.Activated += new System.EventHandler(this.frmMPOrder_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMPOrder_FormClosed);
            this.Load += new System.EventHandler(this.frmMPOrder_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.edConfPlannedQty)).EndInit();
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
        private System.Windows.Forms.ToolStripButton btnSelAll;
        private System.Windows.Forms.ToolStripButton btnDeselAll;
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
        private DevExpress.XtraGrid.Columns.GridColumn grcNumberOfPalletForDel;
        private DevExpress.XtraGrid.Columns.GridColumn grcShippAddressID;
        private DevExpress.XtraGrid.Columns.GridColumn grcShippAddressCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn gricShippingAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gricNote;
        private DevExpress.XtraGrid.Columns.GridColumn grcConfPlannedQtySum;
        private DevExpress.XtraGrid.Columns.GridColumn grcGrossWeightPlannedSum;
        private DevExpress.XtraGrid.Columns.GridColumn grcID;
        private DevExpress.XtraGrid.Columns.GridColumn grcRowNumber;
        private DevExpress.XtraGrid.Columns.GridColumn grcProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn grcProdDescription;
        private DevExpress.XtraGrid.Columns.GridColumn grcU_M;
        private DevExpress.XtraGrid.Columns.GridColumn grcConfOrderQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcPalletOrderQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcPalletPlannedQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcPalletBulkQty;
        private DevExpress.XtraGrid.Columns.GridColumn gricGrossWeightPlanned;
        private DevExpress.XtraGrid.Columns.GridColumn grcADR;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRMultiplierX;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRLimitedQuantity;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private System.Windows.Forms.OpenFileDialog openExcel;
        private System.Windows.Forms.DateTimePicker dtmOrderDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit edConfPlannedQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRMultiplierXSum;
        private DevExpress.XtraGrid.Columns.GridColumn grcSentToCT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit edSentToCT;
        private DevExpress.XtraGrid.Columns.GridColumn grcConfPlannedQty;
        private DevExpress.XtraGrid.Columns.GridColumn grcUnitWeight;
        private DevExpress.XtraGrid.Columns.GridColumn grcTotalGrossWeightOfOrder;
        private DevExpress.XtraGrid.Columns.GridColumn grcADRMultiplier;
        private DevExpress.XtraGrid.Columns.GridColumn grcCustomerCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}