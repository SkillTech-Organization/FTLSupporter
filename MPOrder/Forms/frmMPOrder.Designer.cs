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
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.xcelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tsbExportItems = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlus = new System.Windows.Forms.ToolStripButton();
            this.btnMinus = new System.Windows.Forms.ToolStripButton();
            this.tsMegr = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpMegrF = new System.Windows.Forms.GroupBox();
            this.gridMegrF = new DevExpress.XtraGrid.GridControl();
            this.gridViewMegrF = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grcMEGRID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcMEGRTIPX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColRKTKOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcMEGRDAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcVEVONEV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcSZAMLASZ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcMEGJ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridMegrT = new DevExpress.XtraGrid.GridControl();
            this.gridViewMegrT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gerMEGRID_T = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcROWNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcTermKod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcTERMNEV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcDB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcDATUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcRELROW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcFBiz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcSZVEVOID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcSZVEVONEV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPartnerNev = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cboMegrType = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnFindPartner = new System.Windows.Forms.Button();
            this.tsMegr.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpMegrF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(24, 22);
            this.printToolStripButton.Text = "&Nyomtatás";
            // 
            // xcelToolStripButton
            // 
            this.xcelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xcelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("xcelToolStripButton.Image")));
            this.xcelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xcelToolStripButton.Name = "xcelToolStripButton";
            this.xcelToolStripButton.Size = new System.Drawing.Size(24, 22);
            this.xcelToolStripButton.Text = "&Export Excelbe";
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
            this.printToolStripButton,
            this.xcelToolStripButton,
            this.tsbExportItems,
            this.toolStripSeparator2,
            this.btnPlus,
            this.btnMinus});
            this.tsMegr.Location = new System.Drawing.Point(0, 0);
            this.tsMegr.Name = "tsMegr";
            this.tsMegr.Size = new System.Drawing.Size(1024, 25);
            this.tsMegr.TabIndex = 10;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 474);
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
            this.splitContainer1.Size = new System.Drawing.Size(1018, 408);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.TabIndex = 1;
            // 
            // grpMegrF
            // 
            this.grpMegrF.Controls.Add(this.gridMegrF);
            this.grpMegrF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMegrF.Location = new System.Drawing.Point(0, 0);
            this.grpMegrF.Name = "grpMegrF";
            this.grpMegrF.Size = new System.Drawing.Size(1018, 214);
            this.grpMegrF.TabIndex = 11;
            this.grpMegrF.TabStop = false;
            // 
            // gridMegrF
            // 
            this.gridMegrF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMegrF.Location = new System.Drawing.Point(3, 16);
            this.gridMegrF.MainView = this.gridViewMegrF;
            this.gridMegrF.Name = "gridMegrF";
            this.gridMegrF.Size = new System.Drawing.Size(1012, 195);
            this.gridMegrF.TabIndex = 9;
            this.gridMegrF.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMegrF,
            this.gridView1,
            this.gridView2});
            // 
            // gridViewMegrF
            // 
            this.gridViewMegrF.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grcMEGRID,
            this.grcMEGRTIPX,
            this.gridColRKTKOD,
            this.grcMEGRDAT,
            this.grcVEVONEV,
            this.grcSZAMLASZ,
            this.grcMEGJ});
            this.gridViewMegrF.GridControl = this.gridMegrF;
            this.gridViewMegrF.Name = "gridViewMegrF";
            this.gridViewMegrF.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMegrF.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMegrF.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMegrF.OptionsView.ShowFooter = true;
            // 
            // grcMEGRID
            // 
            this.grcMEGRID.Caption = "Megr.száma";
            this.grcMEGRID.FieldName = "MEGRID";
            this.grcMEGRID.Name = "grcMEGRID";
            this.grcMEGRID.OptionsColumn.AllowEdit = false;
            this.grcMEGRID.Visible = true;
            this.grcMEGRID.VisibleIndex = 0;
            this.grcMEGRID.Width = 80;
            // 
            // grcMEGRTIPX
            // 
            this.grcMEGRTIPX.Caption = "Típus";
            this.grcMEGRTIPX.FieldName = "MEGRTIPX";
            this.grcMEGRTIPX.Name = "grcMEGRTIPX";
            this.grcMEGRTIPX.Visible = true;
            this.grcMEGRTIPX.VisibleIndex = 1;
            // 
            // gridColRKTKOD
            // 
            this.gridColRKTKOD.Caption = "Raktár";
            this.gridColRKTKOD.FieldName = "RKTKOD";
            this.gridColRKTKOD.Name = "gridColRKTKOD";
            this.gridColRKTKOD.OptionsColumn.AllowEdit = false;
            this.gridColRKTKOD.OptionsColumn.AllowFocus = false;
            this.gridColRKTKOD.Visible = true;
            this.gridColRKTKOD.VisibleIndex = 2;
            // 
            // grcMEGRDAT
            // 
            this.grcMEGRDAT.Caption = "Dátum";
            this.grcMEGRDAT.FieldName = "MEGRDAT";
            this.grcMEGRDAT.Name = "grcMEGRDAT";
            this.grcMEGRDAT.OptionsColumn.AllowEdit = false;
            this.grcMEGRDAT.Visible = true;
            this.grcMEGRDAT.VisibleIndex = 3;
            this.grcMEGRDAT.Width = 60;
            // 
            // grcVEVONEV
            // 
            this.grcVEVONEV.Caption = "Partner";
            this.grcVEVONEV.FieldName = "VEVONEV";
            this.grcVEVONEV.Name = "grcVEVONEV";
            this.grcVEVONEV.OptionsColumn.AllowEdit = false;
            this.grcVEVONEV.Visible = true;
            this.grcVEVONEV.VisibleIndex = 4;
            this.grcVEVONEV.Width = 239;
            // 
            // grcSZAMLASZ
            // 
            this.grcSZAMLASZ.Caption = "Telj.szla.";
            this.grcSZAMLASZ.FieldName = "SZAMLASZ";
            this.grcSZAMLASZ.Name = "grcSZAMLASZ";
            this.grcSZAMLASZ.OptionsColumn.AllowEdit = false;
            this.grcSZAMLASZ.Visible = true;
            this.grcSZAMLASZ.VisibleIndex = 5;
            this.grcSZAMLASZ.Width = 82;
            // 
            // grcMEGJ
            // 
            this.grcMEGJ.Caption = "Megjegyzés";
            this.grcMEGJ.FieldName = "MEGJ";
            this.grcMEGJ.Name = "grcMEGJ";
            this.grcMEGJ.OptionsColumn.AllowEdit = false;
            this.grcMEGJ.Visible = true;
            this.grcMEGJ.VisibleIndex = 6;
            this.grcMEGJ.Width = 182;
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
            this.gridMegrT.Size = new System.Drawing.Size(1018, 190);
            this.gridMegrT.TabIndex = 10;
            this.gridMegrT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMegrT,
            this.gridView3});
            // 
            // gridViewMegrT
            // 
            this.gridViewMegrT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gerMEGRID_T,
            this.grcROWNO,
            this.grcTermKod,
            this.grcTERMNEV,
            this.grcDB,
            this.grcAR,
            this.grcDATUM,
            this.grcRELROW,
            this.grcFBiz,
            this.grcSZVEVOID,
            this.grcSZVEVONEV});
            this.gridViewMegrT.GridControl = this.gridMegrT;
            this.gridViewMegrT.Name = "gridViewMegrT";
            this.gridViewMegrT.OptionsCustomization.AllowGroup = false;
            this.gridViewMegrT.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMegrT.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMegrT.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMegrT.OptionsView.ShowFooter = true;
            this.gridViewMegrT.OptionsView.ShowGroupPanel = false;
            // 
            // gerMEGRID_T
            // 
            this.gerMEGRID_T.Caption = "Megr.ID";
            this.gerMEGRID_T.FieldName = "MEGRID";
            this.gerMEGRID_T.Name = "gerMEGRID_T";
            this.gerMEGRID_T.Visible = true;
            this.gerMEGRID_T.VisibleIndex = 0;
            // 
            // grcROWNO
            // 
            this.grcROWNO.Caption = "Sor";
            this.grcROWNO.FieldName = "ROWNO";
            this.grcROWNO.Name = "grcROWNO";
            this.grcROWNO.Visible = true;
            this.grcROWNO.VisibleIndex = 1;
            this.grcROWNO.Width = 59;
            // 
            // grcTermKod
            // 
            this.grcTermKod.Caption = "Termékkód";
            this.grcTermKod.FieldName = "TERMKOD";
            this.grcTermKod.Name = "grcTermKod";
            this.grcTermKod.OptionsColumn.AllowEdit = false;
            this.grcTermKod.OptionsColumn.AllowFocus = false;
            this.grcTermKod.Visible = true;
            this.grcTermKod.VisibleIndex = 2;
            this.grcTermKod.Width = 68;
            // 
            // grcTERMNEV
            // 
            this.grcTERMNEV.Caption = "Terméknév";
            this.grcTERMNEV.FieldName = "TERMNEV";
            this.grcTERMNEV.Name = "grcTERMNEV";
            this.grcTERMNEV.OptionsColumn.AllowEdit = false;
            this.grcTERMNEV.OptionsColumn.AllowFocus = false;
            this.grcTERMNEV.Visible = true;
            this.grcTERMNEV.VisibleIndex = 3;
            this.grcTERMNEV.Width = 119;
            // 
            // grcDB
            // 
            this.grcDB.Caption = "Mennyiség";
            this.grcDB.DisplayFormat.FormatString = "#,#0.00";
            this.grcDB.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcDB.FieldName = "DB";
            this.grcDB.Name = "grcDB";
            this.grcDB.OptionsColumn.AllowEdit = false;
            this.grcDB.OptionsColumn.AllowFocus = false;
            this.grcDB.Visible = true;
            this.grcDB.VisibleIndex = 4;
            this.grcDB.Width = 46;
            // 
            // grcAR
            // 
            this.grcAR.Caption = "Ár";
            this.grcAR.DisplayFormat.FormatString = "#,#0.00";
            this.grcAR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.grcAR.FieldName = "AR";
            this.grcAR.Name = "grcAR";
            this.grcAR.OptionsColumn.AllowEdit = false;
            this.grcAR.OptionsColumn.AllowFocus = false;
            this.grcAR.Visible = true;
            this.grcAR.VisibleIndex = 5;
            this.grcAR.Width = 59;
            // 
            // grcDATUM
            // 
            this.grcDATUM.Caption = "Visszaig.dát.";
            this.grcDATUM.FieldName = "DATUM";
            this.grcDATUM.Name = "grcDATUM";
            this.grcDATUM.OptionsColumn.AllowEdit = false;
            this.grcDATUM.OptionsColumn.AllowFocus = false;
            this.grcDATUM.Visible = true;
            this.grcDATUM.VisibleIndex = 6;
            this.grcDATUM.Width = 78;
            // 
            // grcRELROW
            // 
            this.grcRELROW.Caption = "Kapcs.megr.";
            this.grcRELROW.FieldName = "RELROW";
            this.grcRELROW.Name = "grcRELROW";
            this.grcRELROW.OptionsColumn.AllowEdit = false;
            this.grcRELROW.OptionsColumn.AllowFocus = false;
            this.grcRELROW.Visible = true;
            this.grcRELROW.VisibleIndex = 7;
            this.grcRELROW.Width = 31;
            // 
            // grcFBiz
            // 
            this.grcFBiz.Caption = "Küls.bizsz.";
            this.grcFBiz.FieldName = "FBIZ";
            this.grcFBiz.Name = "grcFBiz";
            this.grcFBiz.OptionsColumn.AllowEdit = false;
            this.grcFBiz.OptionsColumn.AllowFocus = false;
            this.grcFBiz.Visible = true;
            this.grcFBiz.VisibleIndex = 8;
            this.grcFBiz.Width = 59;
            // 
            // grcSZVEVOID
            // 
            this.grcSZVEVOID.Caption = "Vevőkód";
            this.grcSZVEVOID.FieldName = "SZVEVOID";
            this.grcSZVEVOID.Name = "grcSZVEVOID";
            this.grcSZVEVOID.OptionsColumn.AllowEdit = false;
            this.grcSZVEVOID.OptionsColumn.AllowFocus = false;
            this.grcSZVEVOID.Visible = true;
            this.grcSZVEVOID.VisibleIndex = 9;
            // 
            // grcSZVEVONEV
            // 
            this.grcSZVEVONEV.Caption = "Vevőnév";
            this.grcSZVEVONEV.FieldName = "VEVONEV";
            this.grcSZVEVONEV.Name = "grcSZVEVONEV";
            this.grcSZVEVONEV.OptionsColumn.AllowEdit = false;
            this.grcSZVEVONEV.OptionsColumn.AllowFocus = false;
            this.grcSZVEVONEV.Visible = true;
            this.grcSZVEVONEV.VisibleIndex = 10;
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
            this.grpFilter.Size = new System.Drawing.Size(1018, 54);
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
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtPartnerNev, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboMegrType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnClear, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFindPartner, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1012, 35);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bizonylattípus:";
            // 
            // txtPartnerNev
            // 
            this.txtPartnerNev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPartnerNev.BackColor = System.Drawing.Color.OldLace;
            this.txtPartnerNev.Enabled = false;
            this.txtPartnerNev.Location = new System.Drawing.Point(323, 7);
            this.txtPartnerNev.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtPartnerNev.Name = "txtPartnerNev";
            this.txtPartnerNev.Size = new System.Drawing.Size(581, 20);
            this.txtPartnerNev.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.BackColor = System.Drawing.Color.Yellow;
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.Location = new System.Drawing.Point(980, 4);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(29, 26);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // cboMegrType
            // 
            this.cboMegrType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMegrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMegrType.FormattingEnabled = true;
            this.cboMegrType.Location = new System.Drawing.Point(123, 7);
            this.cboMegrType.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboMegrType.Name = "cboMegrType";
            this.cboMegrType.Size = new System.Drawing.Size(194, 21);
            this.cboMegrType.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.OldLace;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(910, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(29, 26);
            this.btnClear.TabIndex = 9;
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnFindPartner
            // 
            this.btnFindPartner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindPartner.BackColor = System.Drawing.Color.OldLace;
            this.btnFindPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnFindPartner.Image")));
            this.btnFindPartner.Location = new System.Drawing.Point(945, 4);
            this.btnFindPartner.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFindPartner.Name = "btnFindPartner";
            this.btnFindPartner.Size = new System.Drawing.Size(29, 26);
            this.btnFindPartner.TabIndex = 10;
            this.btnFindPartner.UseVisualStyleBackColor = false;
            // 
            // frmMPOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 499);
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
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMegrT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMegrT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton xcelToolStripButton;
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
        private DevExpress.XtraGrid.Columns.GridColumn grcMEGRID;
        private DevExpress.XtraGrid.Columns.GridColumn grcMEGRTIPX;
        private DevExpress.XtraGrid.Columns.GridColumn gridColRKTKOD;
        private DevExpress.XtraGrid.Columns.GridColumn grcMEGRDAT;
        private DevExpress.XtraGrid.Columns.GridColumn grcVEVONEV;
        private DevExpress.XtraGrid.Columns.GridColumn grcSZAMLASZ;
        private DevExpress.XtraGrid.Columns.GridColumn grcMEGJ;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridMegrT;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMegrT;
        private DevExpress.XtraGrid.Columns.GridColumn gerMEGRID_T;
        private DevExpress.XtraGrid.Columns.GridColumn grcROWNO;
        private DevExpress.XtraGrid.Columns.GridColumn grcTermKod;
        private DevExpress.XtraGrid.Columns.GridColumn grcTERMNEV;
        private DevExpress.XtraGrid.Columns.GridColumn grcDB;
        private DevExpress.XtraGrid.Columns.GridColumn grcAR;
        private DevExpress.XtraGrid.Columns.GridColumn grcDATUM;
        private DevExpress.XtraGrid.Columns.GridColumn grcRELROW;
        private DevExpress.XtraGrid.Columns.GridColumn grcFBiz;
        private DevExpress.XtraGrid.Columns.GridColumn grcSZVEVOID;
        private DevExpress.XtraGrid.Columns.GridColumn grcSZVEVONEV;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPartnerNev;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cboMegrType;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFindPartner;
    }
}