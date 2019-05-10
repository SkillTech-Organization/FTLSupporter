namespace PMapUI.Forms.Panels.frmPPlan
{
    partial class pnlPPlanTours
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlPPlanTours));
            this.gridColumnVOLErr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnQTYErr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridTours = new DevExpress.XtraGrid.GridControl();
            this.gridViewTours = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCOLOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.gridColumnTRK_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRUCK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSTART = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEND = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTDURATION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOLL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnQTYDETAILS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVOLDETAILS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOLLDETAILS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCNTDETAILS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCPP_LOADQTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCPP_LOADVOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_WEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_XHEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_XWIDTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_HEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_WIDTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTRK_LENGTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reChkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnLOCKED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLockedEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColTRK_ENGINEEURO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColTRK_ETOLLCAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColTollMultiplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTOURPOINTCNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reChkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLockedEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumnVOLErr
            // 
            this.gridColumnVOLErr.FieldName = "VOLErr";
            this.gridColumnVOLErr.Name = "gridColumnVOLErr";
            this.gridColumnVOLErr.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnQTYErr
            // 
            this.gridColumnQTYErr.FieldName = "QTYErr";
            this.gridColumnQTYErr.Name = "gridColumnQTYErr";
            this.gridColumnQTYErr.OptionsColumn.AllowEdit = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gridTours);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1511, 445);
            this.panel1.TabIndex = 0;
            // 
            // gridTours
            // 
            this.gridTours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTours.Location = new System.Drawing.Point(0, 0);
            this.gridTours.MainView = this.gridViewTours;
            this.gridTours.Name = "gridTours";
            this.gridTours.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reChkSelect,
            this.repositoryItemColorEdit1,
            this.repositoryItemLockedEdit});
            this.gridTours.Size = new System.Drawing.Size(1509, 443);
            this.gridTours.TabIndex = 0;
            this.gridTours.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTours});
            this.gridTours.Click += new System.EventHandler(this.gridTours_Click);
            // 
            // gridViewTours
            // 
            this.gridViewTours.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSelect,
            this.gridColumnID,
            this.gridColumnCOLOR,
            this.gridColumnTRK_ID,
            this.gridColumnTRUCK,
            this.gridColumnSTART,
            this.gridColumnEND,
            this.gridColumnTDURATION,
            this.gridColumnQTY,
            this.gridColumnVOL,
            this.gridColumnDST,
            this.gridColumnTOLL,
            this.gridColumnQTYDETAILS,
            this.gridColumnVOLDETAILS,
            this.gridColumnTOLLDETAILS,
            this.gridColumnCNTDETAILS,
            this.gridColumnCPP_LOADQTY,
            this.gridColumnCPP_LOADVOL,
            this.gridColumnVOLErr,
            this.gridColumnQTYErr,
            this.gridColumnTRK_WEIGHT,
            this.gridColumnTRK_XHEIGHT,
            this.gridColumnTRK_XWIDTH,
            this.gridColumnTRK_HEIGHT,
            this.gridColumnTRK_WIDTH,
            this.gridColumnTRK_LENGTH,
            this.gridColumnLOCKED,
            this.gridColTRK_ENGINEEURO,
            this.gridColTRK_ETOLLCAT,
            this.gridColTollMultiplier,
            this.gridColumnTOURPOINTCNT});
            this.gridViewTours.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.gridColumnVOLErr;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Expression = "true";
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Column = this.gridColumnQTYErr;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Expression = "true";
            this.gridViewTours.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.gridViewTours.GridControl = this.gridTours;
            this.gridViewTours.Name = "gridViewTours";
            this.gridViewTours.OptionsCustomization.AllowGroup = false;
            this.gridViewTours.OptionsDetail.AllowZoomDetail = false;
            this.gridViewTours.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewTours.OptionsDetail.ShowDetailTabs = false;
            this.gridViewTours.OptionsDetail.SmartDetailExpand = false;
            this.gridViewTours.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewTours.OptionsView.ShowDetailButtons = false;
            this.gridViewTours.OptionsView.ShowGroupPanel = false;
            this.gridViewTours.PaintStyleName = "MixedXP";
            this.gridViewTours.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewTours_CustomDrawCell);
            this.gridViewTours.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewTours_RowStyle);
            this.gridViewTours.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewTours_FocusedRowChanged);
            // 
            // gridColumnID
            // 
            this.gridColumnID.Caption = "ID";
            this.gridColumnID.FieldName = "ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnCOLOR
            // 
            this.gridColumnCOLOR.Caption = "Szin";
            this.gridColumnCOLOR.ColumnEdit = this.repositoryItemColorEdit1;
            this.gridColumnCOLOR.FieldName = "PCOLOR";
            this.gridColumnCOLOR.MaxWidth = 45;
            this.gridColumnCOLOR.MinWidth = 45;
            this.gridColumnCOLOR.Name = "gridColumnCOLOR";
            this.gridColumnCOLOR.OptionsColumn.AllowEdit = false;
            this.gridColumnCOLOR.Visible = true;
            this.gridColumnCOLOR.VisibleIndex = 1;
            this.gridColumnCOLOR.Width = 45;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemColorEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemColorEdit1_EditValueChanged);
            // 
            // gridColumnTRK_ID
            // 
            this.gridColumnTRK_ID.Caption = "TRK_ID";
            this.gridColumnTRK_ID.FieldName = "TRK_ID";
            this.gridColumnTRK_ID.Name = "gridColumnTRK_ID";
            this.gridColumnTRK_ID.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnTRUCK
            // 
            this.gridColumnTRUCK.Caption = "Jármű";
            this.gridColumnTRUCK.FieldName = "TRUCK";
            this.gridColumnTRUCK.Name = "gridColumnTRUCK";
            this.gridColumnTRUCK.OptionsColumn.AllowEdit = false;
            this.gridColumnTRUCK.Visible = true;
            this.gridColumnTRUCK.VisibleIndex = 2;
            this.gridColumnTRUCK.Width = 58;
            // 
            // gridColumnSTART
            // 
            this.gridColumnSTART.Caption = "Indulás";
            this.gridColumnSTART.DisplayFormat.FormatString = "yyyy.MM.dd HH:mm";
            this.gridColumnSTART.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnSTART.FieldName = "StartStr";
            this.gridColumnSTART.Name = "gridColumnSTART";
            this.gridColumnSTART.OptionsColumn.AllowEdit = false;
            this.gridColumnSTART.Visible = true;
            this.gridColumnSTART.VisibleIndex = 3;
            this.gridColumnSTART.Width = 58;
            // 
            // gridColumnEND
            // 
            this.gridColumnEND.Caption = "Befejezés";
            this.gridColumnEND.DisplayFormat.FormatString = "yyyy.MM.dd HH:mm";
            this.gridColumnEND.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnEND.FieldName = "EndStr";
            this.gridColumnEND.Name = "gridColumnEND";
            this.gridColumnEND.OptionsColumn.AllowEdit = false;
            this.gridColumnEND.Visible = true;
            this.gridColumnEND.VisibleIndex = 4;
            this.gridColumnEND.Width = 58;
            // 
            // gridColumnTDURATION
            // 
            this.gridColumnTDURATION.Caption = "Túra időtartama";
            this.gridColumnTDURATION.DisplayFormat.FormatString = "HH:mm";
            this.gridColumnTDURATION.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnTDURATION.FieldName = "TDURATION";
            this.gridColumnTDURATION.Name = "gridColumnTDURATION";
            this.gridColumnTDURATION.OptionsColumn.AllowEdit = false;
            this.gridColumnTDURATION.Visible = true;
            this.gridColumnTDURATION.VisibleIndex = 20;
            this.gridColumnTDURATION.Width = 49;
            // 
            // gridColumnQTY
            // 
            this.gridColumnQTY.Caption = "Mennyiség";
            this.gridColumnQTY.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnQTY.FieldName = "QTY";
            this.gridColumnQTY.Name = "gridColumnQTY";
            this.gridColumnQTY.OptionsColumn.AllowEdit = false;
            this.gridColumnQTY.Visible = true;
            this.gridColumnQTY.VisibleIndex = 5;
            this.gridColumnQTY.Width = 58;
            // 
            // gridColumnVOL
            // 
            this.gridColumnVOL.Caption = "Térfogat";
            this.gridColumnVOL.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnVOL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnVOL.FieldName = "VOL";
            this.gridColumnVOL.Name = "gridColumnVOL";
            this.gridColumnVOL.OptionsColumn.AllowEdit = false;
            this.gridColumnVOL.Visible = true;
            this.gridColumnVOL.VisibleIndex = 6;
            this.gridColumnVOL.Width = 58;
            // 
            // gridColumnDST
            // 
            this.gridColumnDST.Caption = "Távolság";
            this.gridColumnDST.DisplayFormat.FormatString = "#,#0";
            this.gridColumnDST.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnDST.FieldName = "DST";
            this.gridColumnDST.Name = "gridColumnDST";
            this.gridColumnDST.OptionsColumn.AllowEdit = false;
            this.gridColumnDST.Visible = true;
            this.gridColumnDST.VisibleIndex = 7;
            this.gridColumnDST.Width = 58;
            // 
            // gridColumnTOLL
            // 
            this.gridColumnTOLL.Caption = "Útdíj";
            this.gridColumnTOLL.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTOLL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTOLL.FieldName = "TOLL";
            this.gridColumnTOLL.Name = "gridColumnTOLL";
            this.gridColumnTOLL.OptionsColumn.AllowEdit = false;
            this.gridColumnTOLL.Visible = true;
            this.gridColumnTOLL.VisibleIndex = 8;
            this.gridColumnTOLL.Width = 58;
            // 
            // gridColumnQTYDETAILS
            // 
            this.gridColumnQTYDETAILS.Caption = "Mennyiségrészletek";
            this.gridColumnQTYDETAILS.FieldName = "QTYDETAILS";
            this.gridColumnQTYDETAILS.Name = "gridColumnQTYDETAILS";
            this.gridColumnQTYDETAILS.OptionsColumn.AllowEdit = false;
            this.gridColumnQTYDETAILS.Visible = true;
            this.gridColumnQTYDETAILS.VisibleIndex = 9;
            this.gridColumnQTYDETAILS.Width = 58;
            // 
            // gridColumnVOLDETAILS
            // 
            this.gridColumnVOLDETAILS.Caption = "Térfogatrészletek";
            this.gridColumnVOLDETAILS.FieldName = "VOLDETAILS";
            this.gridColumnVOLDETAILS.Name = "gridColumnVOLDETAILS";
            this.gridColumnVOLDETAILS.OptionsColumn.AllowEdit = false;
            this.gridColumnVOLDETAILS.Visible = true;
            this.gridColumnVOLDETAILS.VisibleIndex = 12;
            this.gridColumnVOLDETAILS.Width = 56;
            // 
            // gridColumnTOLLDETAILS
            // 
            this.gridColumnTOLLDETAILS.Caption = "Útdíj részletek";
            this.gridColumnTOLLDETAILS.FieldName = "TOLLDETAILS";
            this.gridColumnTOLLDETAILS.Name = "gridColumnTOLLDETAILS";
            this.gridColumnTOLLDETAILS.OptionsColumn.AllowEdit = false;
            this.gridColumnTOLLDETAILS.Visible = true;
            this.gridColumnTOLLDETAILS.VisibleIndex = 25;
            this.gridColumnTOLLDETAILS.Width = 127;
            // 
            // gridColumnCNTDETAILS
            // 
            this.gridColumnCNTDETAILS.Caption = "Pontok száma";
            this.gridColumnCNTDETAILS.FieldName = "CNTDETAILS";
            this.gridColumnCNTDETAILS.Name = "gridColumnCNTDETAILS";
            this.gridColumnCNTDETAILS.OptionsColumn.AllowEdit = false;
            this.gridColumnCNTDETAILS.Visible = true;
            this.gridColumnCNTDETAILS.VisibleIndex = 13;
            this.gridColumnCNTDETAILS.Width = 56;
            // 
            // gridColumnCPP_LOADQTY
            // 
            this.gridColumnCPP_LOADQTY.Caption = "Kapacitás";
            this.gridColumnCPP_LOADQTY.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnCPP_LOADQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnCPP_LOADQTY.FieldName = "CPP_LOADQTY";
            this.gridColumnCPP_LOADQTY.Name = "gridColumnCPP_LOADQTY";
            this.gridColumnCPP_LOADQTY.OptionsColumn.AllowEdit = false;
            this.gridColumnCPP_LOADQTY.Visible = true;
            this.gridColumnCPP_LOADQTY.VisibleIndex = 10;
            this.gridColumnCPP_LOADQTY.Width = 58;
            // 
            // gridColumnCPP_LOADVOL
            // 
            this.gridColumnCPP_LOADVOL.Caption = "Térfogatkapacitás (dm3)";
            this.gridColumnCPP_LOADVOL.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnCPP_LOADVOL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnCPP_LOADVOL.FieldName = "CPP_LOADVOL";
            this.gridColumnCPP_LOADVOL.Name = "gridColumnCPP_LOADVOL";
            this.gridColumnCPP_LOADVOL.OptionsColumn.AllowEdit = false;
            this.gridColumnCPP_LOADVOL.Visible = true;
            this.gridColumnCPP_LOADVOL.VisibleIndex = 11;
            this.gridColumnCPP_LOADVOL.Width = 57;
            // 
            // gridColumnTRK_WEIGHT
            // 
            this.gridColumnTRK_WEIGHT.Caption = "Összsúly";
            this.gridColumnTRK_WEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTRK_WEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTRK_WEIGHT.FieldName = "TRK_WEIGHT";
            this.gridColumnTRK_WEIGHT.Name = "gridColumnTRK_WEIGHT";
            this.gridColumnTRK_WEIGHT.OptionsColumn.AllowEdit = false;
            this.gridColumnTRK_WEIGHT.Visible = true;
            this.gridColumnTRK_WEIGHT.VisibleIndex = 14;
            this.gridColumnTRK_WEIGHT.Width = 27;
            // 
            // gridColumnTRK_XHEIGHT
            // 
            this.gridColumnTRK_XHEIGHT.Caption = "Telj.magasság";
            this.gridColumnTRK_XHEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTRK_XHEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTRK_XHEIGHT.FieldName = "TRK_XHEIGHT";
            this.gridColumnTRK_XHEIGHT.Name = "gridColumnTRK_XHEIGHT";
            this.gridColumnTRK_XHEIGHT.OptionsColumn.AllowEdit = false;
            this.gridColumnTRK_XHEIGHT.Visible = true;
            this.gridColumnTRK_XHEIGHT.VisibleIndex = 15;
            this.gridColumnTRK_XHEIGHT.Width = 26;
            // 
            // gridColumnTRK_XWIDTH
            // 
            this.gridColumnTRK_XWIDTH.Caption = "Telj.széles";
            this.gridColumnTRK_XWIDTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTRK_XWIDTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTRK_XWIDTH.FieldName = "TRK_XWIDTH";
            this.gridColumnTRK_XWIDTH.Name = "gridColumnTRK_XWIDTH";
            this.gridColumnTRK_XWIDTH.OptionsColumn.AllowEdit = false;
            this.gridColumnTRK_XWIDTH.Visible = true;
            this.gridColumnTRK_XWIDTH.VisibleIndex = 16;
            this.gridColumnTRK_XWIDTH.Width = 117;
            // 
            // gridColumnTRK_HEIGHT
            // 
            this.gridColumnTRK_HEIGHT.Caption = "Rakt.magasság";
            this.gridColumnTRK_HEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTRK_HEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTRK_HEIGHT.FieldName = "TRK_HEIGHT";
            this.gridColumnTRK_HEIGHT.Name = "gridColumnTRK_HEIGHT";
            this.gridColumnTRK_HEIGHT.OptionsColumn.AllowEdit = false;
            this.gridColumnTRK_HEIGHT.Visible = true;
            this.gridColumnTRK_HEIGHT.VisibleIndex = 19;
            this.gridColumnTRK_HEIGHT.Width = 43;
            // 
            // gridColumnTRK_WIDTH
            // 
            this.gridColumnTRK_WIDTH.Caption = "Rakt.széles";
            this.gridColumnTRK_WIDTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTRK_WIDTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTRK_WIDTH.FieldName = "TRK_WIDTH";
            this.gridColumnTRK_WIDTH.Name = "gridColumnTRK_WIDTH";
            this.gridColumnTRK_WIDTH.OptionsColumn.AllowEdit = false;
            this.gridColumnTRK_WIDTH.Visible = true;
            this.gridColumnTRK_WIDTH.VisibleIndex = 18;
            this.gridColumnTRK_WIDTH.Width = 43;
            // 
            // gridColumnTRK_LENGTH
            // 
            this.gridColumnTRK_LENGTH.Caption = "Rakt.hossz";
            this.gridColumnTRK_LENGTH.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnTRK_LENGTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnTRK_LENGTH.FieldName = "TRK_LENGTH";
            this.gridColumnTRK_LENGTH.Name = "gridColumnTRK_LENGTH";
            this.gridColumnTRK_LENGTH.OptionsColumn.AllowEdit = false;
            this.gridColumnTRK_LENGTH.Visible = true;
            this.gridColumnTRK_LENGTH.VisibleIndex = 17;
            this.gridColumnTRK_LENGTH.Width = 60;
            // 
            // gridColumnSelect
            // 
            this.gridColumnSelect.ColumnEdit = this.reChkSelect;
            this.gridColumnSelect.FieldName = "PSelect";
            this.gridColumnSelect.Name = "gridColumnSelect";
            this.gridColumnSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnSelect.OptionsColumn.FixedWidth = true;
            this.gridColumnSelect.OptionsColumn.ShowCaption = false;
            this.gridColumnSelect.Visible = true;
            this.gridColumnSelect.VisibleIndex = 0;
            this.gridColumnSelect.Width = 22;
            // 
            // reChkSelect
            // 
            this.reChkSelect.AutoHeight = false;
            this.reChkSelect.Name = "reChkSelect";
            this.reChkSelect.CheckedChanged += new System.EventHandler(this.reChkVisible_CheckedChanged);
            this.reChkSelect.EditValueChanged += new System.EventHandler(this.reChkSelect_EditValueChanged);
            // 
            // gridColumnLOCKED
            // 
            this.gridColumnLOCKED.Caption = "Zárolt";
            this.gridColumnLOCKED.ColumnEdit = this.repositoryItemLockedEdit;
            this.gridColumnLOCKED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnLOCKED.FieldName = "LOCKED";
            this.gridColumnLOCKED.Name = "gridColumnLOCKED";
            this.gridColumnLOCKED.OptionsColumn.AllowEdit = false;
            this.gridColumnLOCKED.Visible = true;
            this.gridColumnLOCKED.VisibleIndex = 21;
            this.gridColumnLOCKED.Width = 49;
            // 
            // repositoryItemLockedEdit
            // 
            this.repositoryItemLockedEdit.AutoHeight = false;
            this.repositoryItemLockedEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemLockedEdit.Name = "repositoryItemLockedEdit";
            this.repositoryItemLockedEdit.PictureChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemLockedEdit.PictureChecked")));
            this.repositoryItemLockedEdit.PictureUnchecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemLockedEdit.PictureUnchecked")));
            // 
            // gridColTRK_ENGINEEURO
            // 
            this.gridColTRK_ENGINEEURO.Caption = "EURO kat.";
            this.gridColTRK_ENGINEEURO.FieldName = "TRK_ENGINEEURO";
            this.gridColTRK_ENGINEEURO.Name = "gridColTRK_ENGINEEURO";
            this.gridColTRK_ENGINEEURO.OptionsColumn.AllowEdit = false;
            this.gridColTRK_ENGINEEURO.Visible = true;
            this.gridColTRK_ENGINEEURO.VisibleIndex = 22;
            this.gridColTRK_ENGINEEURO.Width = 49;
            // 
            // gridColTRK_ETOLLCAT
            // 
            this.gridColTRK_ETOLLCAT.Caption = "Járműkat.";
            this.gridColTRK_ETOLLCAT.FieldName = "TRK_ETOLLCAT";
            this.gridColTRK_ETOLLCAT.Name = "gridColTRK_ETOLLCAT";
            this.gridColTRK_ETOLLCAT.OptionsColumn.AllowEdit = false;
            this.gridColTRK_ETOLLCAT.Visible = true;
            this.gridColTRK_ETOLLCAT.VisibleIndex = 23;
            this.gridColTRK_ETOLLCAT.Width = 49;
            // 
            // gridColTollMultiplier
            // 
            this.gridColTollMultiplier.Caption = "Útdíjszorzó";
            this.gridColTollMultiplier.DisplayFormat.FormatString = "#,#0.00";
            this.gridColTollMultiplier.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColTollMultiplier.FieldName = "TollMultiplier";
            this.gridColTollMultiplier.Name = "gridColTollMultiplier";
            this.gridColTollMultiplier.OptionsColumn.AllowEdit = false;
            this.gridColTollMultiplier.Visible = true;
            this.gridColTollMultiplier.VisibleIndex = 24;
            this.gridColTollMultiplier.Width = 97;
            // 
            // gridColumnTOURPOINTCNT
            // 
            this.gridColumnTOURPOINTCNT.Caption = "túrapontok száma";
            this.gridColumnTOURPOINTCNT.CustomizationCaption = "TOURPOINTCNT";
            this.gridColumnTOURPOINTCNT.FieldName = "TOURPOINTCNT";
            this.gridColumnTOURPOINTCNT.Name = "gridColumnTOURPOINTCNT";
            this.gridColumnTOURPOINTCNT.OptionsColumn.AllowEdit = false;
            // 
            // pnlPPlanTours
            // 
            this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1511, 445);
            this.Controls.Add(this.panel1);
            this.Name = "pnlPPlanTours";
            this.Text = "Túrák";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reChkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLockedEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRUCK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSTART;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEND;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDST;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCOLOR;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reChkSelect;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVOL;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        public DevExpress.XtraGrid.GridControl gridTours;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewTours;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnQTYDETAILS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCPP_LOADQTY;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVOLDETAILS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCPP_LOADVOL;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVOLErr;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnQTYErr;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCNTDETAILS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_LENGTH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_WIDTH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_HEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTDURATION;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLOCKED;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemLockedEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOLL;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOLLDETAILS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColTRK_ENGINEEURO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColTRK_ETOLLCAT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColTollMultiplier;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTOURPOINTCNT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_WEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_XHEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTRK_XWIDTH;
    }
}