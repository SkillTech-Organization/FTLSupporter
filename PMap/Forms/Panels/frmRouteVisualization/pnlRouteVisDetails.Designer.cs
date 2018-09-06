namespace PMapCore.Forms.Panels.frmRouteVisualization
{
    partial class pnlRouteVisDetails
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlRouteVisDetails));
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.gridDepots = new DevExpress.XtraGrid.GridControl();
            this.gridViewDepots = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnRouteSectionType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imlRouteSectionType = new System.Windows.Forms.ImageList(this.components);
            this.gridColumnDEP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDEP_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNOD_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNOD_XPOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNOD_YPOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFullAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.tblTooltipMode = new System.Windows.Forms.TableLayoutPanel();
            this.grpMarkerTooltipMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbNever = new System.Windows.Forms.RadioButton();
            this.rdbOnMouseOver = new System.Windows.Forms.RadioButton();
            this.rdbAlways = new System.Windows.Forms.RadioButton();
            this.tblData = new System.Windows.Forms.TableLayoutPanel();
            this.gridDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVisible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditVisible = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnToll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDistance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDistanceEmpty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDistanceLoaded = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tblDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDepots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.tblTooltipMode.SuspendLayout();
            this.grpMarkerTooltipMode.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tblData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditVisible)).BeginInit();
            this.SuspendLayout();
            // 
            // tblDetails
            // 
            this.tblDetails.ColumnCount = 1;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Controls.Add(this.tbZoom, 0, 0);
            this.tblDetails.Controls.Add(this.gridDepots, 0, 3);
            this.tblDetails.Controls.Add(this.tblTooltipMode, 0, 1);
            this.tblDetails.Controls.Add(this.tblData, 0, 2);
            this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetails.Location = new System.Drawing.Point(0, 0);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowCount = 4;
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Size = new System.Drawing.Size(571, 435);
            this.tblDetails.TabIndex = 0;
            // 
            // tbZoom
            // 
            this.tbZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbZoom.LargeChange = 1;
            this.tbZoom.Location = new System.Drawing.Point(3, 3);
            this.tbZoom.Maximum = 20;
            this.tbZoom.Minimum = 2;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(565, 34);
            this.tbZoom.TabIndex = 1;
            this.tbZoom.Value = 2;
            this.tbZoom.ValueChanged += new System.EventHandler(this.tbZoom_ValueChanged);
            // 
            // gridDepots
            // 
            this.gridDepots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDepots.Location = new System.Drawing.Point(3, 223);
            this.gridDepots.MainView = this.gridViewDepots;
            this.gridDepots.Name = "gridDepots";
            this.gridDepots.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemImageEdit1,
            this.repositoryItemComboBox1});
            this.gridDepots.Size = new System.Drawing.Size(565, 209);
            this.gridDepots.TabIndex = 40;
            this.gridDepots.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDepots});
            // 
            // gridViewDepots
            // 
            this.gridViewDepots.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnID,
            this.gridColumnRouteSectionType,
            this.gridColumnDEP_NAME,
            this.gridColumnDEP_CODE,
            this.gridColumnNOD_ID,
            this.gridColumnNOD_XPOS,
            this.gridColumnNOD_YPOS,
            this.gridColumnFullAddress});
            this.gridViewDepots.GridControl = this.gridDepots;
            this.gridViewDepots.Name = "gridViewDepots";
            this.gridViewDepots.OptionsBehavior.Editable = false;
            this.gridViewDepots.OptionsCustomization.AllowGroup = false;
            this.gridViewDepots.OptionsDetail.AllowZoomDetail = false;
            this.gridViewDepots.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewDepots.OptionsDetail.ShowDetailTabs = false;
            this.gridViewDepots.OptionsDetail.SmartDetailExpand = false;
            this.gridViewDepots.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDepots.OptionsView.ShowDetailButtons = false;
            this.gridViewDepots.OptionsView.ShowGroupPanel = false;
            this.gridViewDepots.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDepots_FocusedRowChanged);
            // 
            // gridColumnID
            // 
            this.gridColumnID.Caption = "ID";
            this.gridColumnID.FieldName = "Depot.ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnRouteSectionType
            // 
            this.gridColumnRouteSectionType.Caption = " ";
            this.gridColumnRouteSectionType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumnRouteSectionType.FieldName = "RouteSectionTypeInt";
            this.gridColumnRouteSectionType.Name = "gridColumnRouteSectionType";
            this.gridColumnRouteSectionType.Visible = true;
            this.gridColumnRouteSectionType.VisibleIndex = 0;
            this.gridColumnRouteSectionType.Width = 20;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("1", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("2", 2, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("3", 3, 2)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imlRouteSectionType;
            // 
            // imlRouteSectionType
            // 
            this.imlRouteSectionType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlRouteSectionType.ImageStream")));
            this.imlRouteSectionType.TransparentColor = System.Drawing.Color.Transparent;
            this.imlRouteSectionType.Images.SetKeyName(0, "lorry_flatbed.png");
            this.imlRouteSectionType.Images.SetKeyName(1, "lorry.png");
            this.imlRouteSectionType.Images.SetKeyName(2, "flag_finish2.png");
            // 
            // gridColumnDEP_NAME
            // 
            this.gridColumnDEP_NAME.Caption = "Lerakó";
            this.gridColumnDEP_NAME.FieldName = "Depot.DEP_NAME";
            this.gridColumnDEP_NAME.Name = "gridColumnDEP_NAME";
            this.gridColumnDEP_NAME.Visible = true;
            this.gridColumnDEP_NAME.VisibleIndex = 2;
            this.gridColumnDEP_NAME.Width = 234;
            // 
            // gridColumnDEP_CODE
            // 
            this.gridColumnDEP_CODE.Caption = "Lerakókód";
            this.gridColumnDEP_CODE.FieldName = "Depot.DEP_CODE";
            this.gridColumnDEP_CODE.Name = "gridColumnDEP_CODE";
            this.gridColumnDEP_CODE.Visible = true;
            this.gridColumnDEP_CODE.VisibleIndex = 1;
            this.gridColumnDEP_CODE.Width = 94;
            // 
            // gridColumnNOD_ID
            // 
            this.gridColumnNOD_ID.Caption = "Node ID";
            this.gridColumnNOD_ID.FieldName = "Depot.NOD_ID";
            this.gridColumnNOD_ID.Name = "gridColumnNOD_ID";
            this.gridColumnNOD_ID.Width = 55;
            // 
            // gridColumnNOD_XPOS
            // 
            this.gridColumnNOD_XPOS.Caption = "Hosszúsági fok";
            this.gridColumnNOD_XPOS.FieldName = "Depot.NOD_XPOS";
            this.gridColumnNOD_XPOS.Name = "gridColumnNOD_XPOS";
            this.gridColumnNOD_XPOS.Width = 52;
            // 
            // gridColumnNOD_YPOS
            // 
            this.gridColumnNOD_YPOS.Caption = "Szélességi fok";
            this.gridColumnNOD_YPOS.FieldName = "Depot.NOD_YPOS";
            this.gridColumnNOD_YPOS.Name = "gridColumnNOD_YPOS";
            this.gridColumnNOD_YPOS.Width = 52;
            // 
            // gridColumnFullAddress
            // 
            this.gridColumnFullAddress.Caption = "Cím";
            this.gridColumnFullAddress.FieldName = "Depot.FullAddress";
            this.gridColumnFullAddress.Name = "gridColumnFullAddress";
            this.gridColumnFullAddress.Visible = true;
            this.gridColumnFullAddress.VisibleIndex = 3;
            this.gridColumnFullAddress.Width = 202;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Images = this.imlRouteSectionType;
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // tblTooltipMode
            // 
            this.tblTooltipMode.ColumnCount = 1;
            this.tblTooltipMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTooltipMode.Controls.Add(this.grpMarkerTooltipMode, 0, 0);
            this.tblTooltipMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTooltipMode.Location = new System.Drawing.Point(0, 40);
            this.tblTooltipMode.Margin = new System.Windows.Forms.Padding(0);
            this.tblTooltipMode.Name = "tblTooltipMode";
            this.tblTooltipMode.RowCount = 1;
            this.tblTooltipMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTooltipMode.Size = new System.Drawing.Size(571, 100);
            this.tblTooltipMode.TabIndex = 42;
            // 
            // grpMarkerTooltipMode
            // 
            this.grpMarkerTooltipMode.Controls.Add(this.tableLayoutPanel3);
            this.grpMarkerTooltipMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMarkerTooltipMode.Location = new System.Drawing.Point(3, 3);
            this.grpMarkerTooltipMode.Name = "grpMarkerTooltipMode";
            this.grpMarkerTooltipMode.Size = new System.Drawing.Size(565, 94);
            this.grpMarkerTooltipMode.TabIndex = 44;
            this.grpMarkerTooltipMode.TabStop = false;
            this.grpMarkerTooltipMode.Text = "Címke megjelenítés";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.rdbNever, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.rdbOnMouseOver, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.rdbAlways, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(559, 75);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // rdbNever
            // 
            this.rdbNever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbNever.AutoSize = true;
            this.rdbNever.Location = new System.Drawing.Point(3, 4);
            this.rdbNever.Name = "rdbNever";
            this.rdbNever.Size = new System.Drawing.Size(553, 17);
            this.rdbNever.TabIndex = 0;
            this.rdbNever.Text = "Nincs ";
            this.rdbNever.UseVisualStyleBackColor = true;
            this.rdbNever.CheckedChanged += new System.EventHandler(this.rdbNever_CheckedChanged);
            // 
            // rdbOnMouseOver
            // 
            this.rdbOnMouseOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbOnMouseOver.AutoSize = true;
            this.rdbOnMouseOver.Checked = true;
            this.rdbOnMouseOver.Location = new System.Drawing.Point(3, 29);
            this.rdbOnMouseOver.Name = "rdbOnMouseOver";
            this.rdbOnMouseOver.Size = new System.Drawing.Size(553, 17);
            this.rdbOnMouseOver.TabIndex = 1;
            this.rdbOnMouseOver.TabStop = true;
            this.rdbOnMouseOver.Text = "Egérpozícionállással";
            this.rdbOnMouseOver.UseVisualStyleBackColor = true;
            this.rdbOnMouseOver.CheckedChanged += new System.EventHandler(this.rdbOnMouseOver_CheckedChanged);
            // 
            // rdbAlways
            // 
            this.rdbAlways.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbAlways.AutoSize = true;
            this.rdbAlways.Location = new System.Drawing.Point(3, 54);
            this.rdbAlways.Name = "rdbAlways";
            this.rdbAlways.Size = new System.Drawing.Size(553, 17);
            this.rdbAlways.TabIndex = 2;
            this.rdbAlways.Text = "Állandó megjelenítés";
            this.rdbAlways.UseVisualStyleBackColor = true;
            this.rdbAlways.CheckedChanged += new System.EventHandler(this.rdbAlways_CheckedChanged);
            // 
            // tblData
            // 
            this.tblData.ColumnCount = 1;
            this.tblData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblData.Controls.Add(this.gridDetails, 0, 0);
            this.tblData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblData.Location = new System.Drawing.Point(3, 143);
            this.tblData.Name = "tblData";
            this.tblData.RowCount = 1;
            this.tblData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblData.Size = new System.Drawing.Size(565, 74);
            this.tblData.TabIndex = 43;
            // 
            // gridDetails
            // 
            this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetails.Location = new System.Drawing.Point(3, 3);
            this.gridDetails.MainView = this.gridViewDetails;
            this.gridDetails.Name = "gridDetails";
            this.gridDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditVisible});
            this.gridDetails.Size = new System.Drawing.Size(559, 68);
            this.gridDetails.TabIndex = 41;
            this.gridDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetails});
            // 
            // gridViewDetails
            // 
            this.gridViewDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnType,
            this.gridColumnVisible,
            this.gridColumnName,
            this.gridColumnToll,
            this.gridColumnDistance,
            this.gridColumnDuration,
            this.gridColumnDistanceEmpty,
            this.gridColumnDistanceLoaded});
            this.gridViewDetails.GridControl = this.gridDetails;
            this.gridViewDetails.Name = "gridViewDetails";
            this.gridViewDetails.OptionsCustomization.AllowGroup = false;
            this.gridViewDetails.OptionsDetail.AllowZoomDetail = false;
            this.gridViewDetails.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewDetails.OptionsDetail.ShowDetailTabs = false;
            this.gridViewDetails.OptionsDetail.SmartDetailExpand = false;
            this.gridViewDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDetails.OptionsView.ShowDetailButtons = false;
            this.gridViewDetails.OptionsView.ShowGroupPanel = false;
            this.gridViewDetails.PaintStyleName = "MixedXP";
            this.gridViewDetails.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDetails_FocusedRowChanged);
            // 
            // gridColumnType
            // 
            this.gridColumnType.Caption = "Típus";
            this.gridColumnType.FieldName = "Type";
            this.gridColumnType.Name = "gridColumnType";
            this.gridColumnType.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnVisible
            // 
            this.gridColumnVisible.ColumnEdit = this.repositoryItemCheckEditVisible;
            this.gridColumnVisible.FieldName = "Visible";
            this.gridColumnVisible.Name = "gridColumnVisible";
            this.gridColumnVisible.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnVisible.OptionsColumn.FixedWidth = true;
            this.gridColumnVisible.OptionsColumn.ShowCaption = false;
            this.gridColumnVisible.Visible = true;
            this.gridColumnVisible.VisibleIndex = 0;
            this.gridColumnVisible.Width = 23;
            // 
            // repositoryItemCheckEditVisible
            // 
            this.repositoryItemCheckEditVisible.AutoHeight = false;
            this.repositoryItemCheckEditVisible.Name = "repositoryItemCheckEditVisible";
            this.repositoryItemCheckEditVisible.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEditVisible_CheckedChanged);
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Megnevezés";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 86;
            // 
            // gridColumnToll
            // 
            this.gridColumnToll.Caption = "Útdíj (Ft)";
            this.gridColumnToll.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnToll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnToll.FieldName = "SumToll";
            this.gridColumnToll.Name = "gridColumnToll";
            this.gridColumnToll.OptionsColumn.AllowEdit = false;
            this.gridColumnToll.Visible = true;
            this.gridColumnToll.VisibleIndex = 2;
            this.gridColumnToll.Width = 86;
            // 
            // gridColumnDistance
            // 
            this.gridColumnDistance.Caption = "Távolság (m)";
            this.gridColumnDistance.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnDistance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnDistance.FieldName = "SumDistance";
            this.gridColumnDistance.Name = "gridColumnDistance";
            this.gridColumnDistance.OptionsColumn.AllowEdit = false;
            this.gridColumnDistance.Visible = true;
            this.gridColumnDistance.VisibleIndex = 3;
            this.gridColumnDistance.Width = 86;
            // 
            // gridColumnDuration
            // 
            this.gridColumnDuration.Caption = "Menetidő (p)";
            this.gridColumnDuration.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnDuration.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnDuration.FieldName = "SumDuration";
            this.gridColumnDuration.Name = "gridColumnDuration";
            this.gridColumnDuration.OptionsColumn.AllowEdit = false;
            this.gridColumnDuration.Visible = true;
            this.gridColumnDuration.VisibleIndex = 4;
            this.gridColumnDuration.Width = 86;
            // 
            // gridColumnDistanceEmpty
            // 
            this.gridColumnDistanceEmpty.Caption = "Üres távolság (m)";
            this.gridColumnDistanceEmpty.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnDistanceEmpty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnDistanceEmpty.FieldName = "SumDistanceEmpty";
            this.gridColumnDistanceEmpty.Name = "gridColumnDistanceEmpty";
            this.gridColumnDistanceEmpty.OptionsColumn.AllowEdit = false;
            this.gridColumnDistanceEmpty.Visible = true;
            this.gridColumnDistanceEmpty.VisibleIndex = 5;
            this.gridColumnDistanceEmpty.Width = 86;
            // 
            // gridColumnDistanceLoaded
            // 
            this.gridColumnDistanceLoaded.Caption = "Terhelt távolság (m)";
            this.gridColumnDistanceLoaded.DisplayFormat.FormatString = "#,#0.00";
            this.gridColumnDistanceLoaded.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnDistanceLoaded.FieldName = "SumDistanceLoaded";
            this.gridColumnDistanceLoaded.Name = "gridColumnDistanceLoaded";
            this.gridColumnDistanceLoaded.OptionsColumn.AllowEdit = false;
            this.gridColumnDistanceLoaded.Visible = true;
            this.gridColumnDistanceLoaded.VisibleIndex = 6;
            this.gridColumnDistanceLoaded.Width = 91;
            // 
            // pnlRouteVisDetails
            // 
            this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 435);
            this.Controls.Add(this.tblDetails);
            this.Name = "pnlRouteVisDetails";
            this.Text = "Részletek";
            this.tblDetails.ResumeLayout(false);
            this.tblDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDepots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.tblTooltipMode.ResumeLayout(false);
            this.grpMarkerTooltipMode.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tblData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditVisible)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblDetails;
        private System.Windows.Forms.TrackBar tbZoom;
        public DevExpress.XtraGrid.GridControl gridDepots;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewDepots;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDEP_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDEP_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNOD_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNOD_XPOS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNOD_YPOS;
        private System.Windows.Forms.TableLayoutPanel tblTooltipMode;
        private System.Windows.Forms.GroupBox grpMarkerTooltipMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton rdbNever;
        private System.Windows.Forms.RadioButton rdbOnMouseOver;
        private System.Windows.Forms.RadioButton rdbAlways;
        private System.Windows.Forms.TableLayoutPanel tblData;
        public DevExpress.XtraGrid.GridControl gridDetails;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVisible;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnToll;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDistance;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDuration;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditVisible;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFullAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRouteSectionType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDistanceEmpty;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDistanceLoaded;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.ImageList imlRouteSectionType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
    }
}