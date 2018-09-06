namespace PMapCore.Forms
{
    partial class dlgRouteVisDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgRouteVisDetails));
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.excelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gridRouteDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewRouteDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnRouteSectionType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imlRouteSectionType = new System.Windows.Forms.ImageList();
            this.colRoadType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDist = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpeed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_ID_FROM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_ID_TO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOneWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colWZone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDestTraffic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colEDG_ETLCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
            this.colEDG_MAXWEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDG_MAXHEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDG_MAXWIDTH = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.toolMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRouteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRouteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolMain
            // 
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.excelToolStripButton});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(1076, 25);
            this.toolMain.TabIndex = 10;
            this.toolMain.Text = "toolStrip1";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
            // 
            // excelToolStripButton
            // 
            this.excelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.excelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("excelToolStripButton.Image")));
            this.excelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.excelToolStripButton.Name = "excelToolStripButton";
            this.excelToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.excelToolStripButton.Text = "Excel export";
            this.excelToolStripButton.Click += new System.EventHandler(this.excelToolStripButton_Click);
            // 
            // gridRouteDetails
            // 
            this.gridRouteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRouteDetails.EmbeddedNavigator.TextStringFormat = "Record {0} of {1}";
            this.gridRouteDetails.Location = new System.Drawing.Point(0, 25);
            this.gridRouteDetails.MainView = this.gridViewRouteDetails;
            this.gridRouteDetails.Name = "gridRouteDetails";
            this.gridRouteDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemImageComboBox2});
            this.gridRouteDetails.Size = new System.Drawing.Size(1076, 381);
            this.gridRouteDetails.TabIndex = 12;
            this.gridRouteDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRouteDetails});
            // 
            // gridViewRouteDetails
            // 
            this.gridViewRouteDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnRouteSectionType,
            this.colRoadType,
            this.colText,
            this.colDist,
            this.colSpeed,
            this.colDuration,
            this.colNOD_ID_FROM,
            this.colNOD_ID_TO,
            this.colOneWay,
            this.colWZone,
            this.colDestTraffic,
            this.colEDG_ETLCODE,
            this.colToll,
            this.colEDG_MAXWEIGHT,
            this.colEDG_MAXHEIGHT,
            this.colEDG_MAXWIDTH});
            this.gridViewRouteDetails.GridControl = this.gridRouteDetails;
            this.gridViewRouteDetails.Images = this.imlRouteSectionType;
            this.gridViewRouteDetails.Name = "gridViewRouteDetails";
            this.gridViewRouteDetails.OptionsBehavior.Editable = false;
            this.gridViewRouteDetails.OptionsCustomization.AllowGroup = false;
            this.gridViewRouteDetails.OptionsCustomization.AllowSort = false;
            this.gridViewRouteDetails.OptionsMenu.EnableColumnMenu = false;
            this.gridViewRouteDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewRouteDetails.OptionsView.ShowFooter = true;
            this.gridViewRouteDetails.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnRouteSectionType
            // 
            this.gridColumnRouteSectionType.Caption = " ";
            this.gridColumnRouteSectionType.ColumnEdit = this.repositoryItemImageComboBox2;
            this.gridColumnRouteSectionType.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnRouteSectionType.FieldName = "RouteSectionTypeInt";
            this.gridColumnRouteSectionType.Name = "gridColumnRouteSectionType";
            this.gridColumnRouteSectionType.OptionsColumn.AllowEdit = false;
            this.gridColumnRouteSectionType.Visible = true;
            this.gridColumnRouteSectionType.VisibleIndex = 0;
            this.gridColumnRouteSectionType.Width = 29;
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("1", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("2", 2, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("3", 3, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("0", 0, -1)});
            this.repositoryItemImageComboBox2.LargeImages = this.imlRouteSectionType;
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.SmallImages = this.imlRouteSectionType;
            // 
            // imlRouteSectionType
            // 
            this.imlRouteSectionType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlRouteSectionType.ImageStream")));
            this.imlRouteSectionType.TransparentColor = System.Drawing.Color.Transparent;
            this.imlRouteSectionType.Images.SetKeyName(0, "lorry_flatbed.png");
            this.imlRouteSectionType.Images.SetKeyName(1, "lorry.png");
            this.imlRouteSectionType.Images.SetKeyName(2, "flag_finish2.png");
            // 
            // colRoadType
            // 
            this.colRoadType.Caption = "Úttípus";
            this.colRoadType.FieldName = "RoadType";
            this.colRoadType.Name = "colRoadType";
            this.colRoadType.OptionsColumn.AllowEdit = false;
            this.colRoadType.Visible = true;
            this.colRoadType.VisibleIndex = 1;
            this.colRoadType.Width = 67;
            // 
            // colText
            // 
            this.colText.Caption = "Leírás";
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.OptionsColumn.AllowEdit = false;
            this.colText.Visible = true;
            this.colText.VisibleIndex = 2;
            this.colText.Width = 334;
            // 
            // colDist
            // 
            this.colDist.Caption = "Távolság (m)";
            this.colDist.DisplayFormat.FormatString = "#,#0.00";
            this.colDist.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDist.FieldName = "Dist";
            this.colDist.Name = "colDist";
            this.colDist.OptionsColumn.AllowEdit = false;
            this.colDist.SummaryItem.DisplayFormat = "{0:#,#0.00}";
            this.colDist.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDist.Visible = true;
            this.colDist.VisibleIndex = 3;
            this.colDist.Width = 93;
            // 
            // colSpeed
            // 
            this.colSpeed.Caption = "Sebesség (Km/h)";
            this.colSpeed.DisplayFormat.FormatString = "#,#0.00";
            this.colSpeed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSpeed.FieldName = "Speed";
            this.colSpeed.Name = "colSpeed";
            this.colSpeed.OptionsColumn.AllowEdit = false;
            this.colSpeed.Visible = true;
            this.colSpeed.VisibleIndex = 4;
            this.colSpeed.Width = 78;
            // 
            // colDuration
            // 
            this.colDuration.Caption = "Menetidő (perc)";
            this.colDuration.DisplayFormat.FormatString = "#,#0.00";
            this.colDuration.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDuration.FieldName = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.OptionsColumn.AllowEdit = false;
            this.colDuration.SummaryItem.DisplayFormat = "{0:#,#0.00}";
            this.colDuration.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDuration.Visible = true;
            this.colDuration.VisibleIndex = 5;
            this.colDuration.Width = 92;
            // 
            // colNOD_ID_FROM
            // 
            this.colNOD_ID_FROM.Caption = "Kezdő node";
            this.colNOD_ID_FROM.FieldName = "NOD_ID_FROM";
            this.colNOD_ID_FROM.Name = "colNOD_ID_FROM";
            this.colNOD_ID_FROM.OptionsColumn.AllowEdit = false;
            // 
            // colNOD_ID_TO
            // 
            this.colNOD_ID_TO.Caption = "Befejező node";
            this.colNOD_ID_TO.FieldName = "NOD_ID_TO";
            this.colNOD_ID_TO.Name = "colNOD_ID_TO";
            this.colNOD_ID_TO.OptionsColumn.AllowEdit = false;
            // 
            // colOneWay
            // 
            this.colOneWay.Caption = "Egyirányú?";
            this.colOneWay.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOneWay.FieldName = "OneWay";
            this.colOneWay.Name = "colOneWay";
            this.colOneWay.OptionsColumn.AllowEdit = false;
            this.colOneWay.Visible = true;
            this.colOneWay.VisibleIndex = 6;
            this.colOneWay.Width = 67;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colWZone
            // 
            this.colWZone.Caption = "Behajtási övezet";
            this.colWZone.FieldName = "WZone";
            this.colWZone.Name = "colWZone";
            this.colWZone.OptionsColumn.AllowEdit = false;
            this.colWZone.Visible = true;
            this.colWZone.VisibleIndex = 7;
            this.colWZone.Width = 67;
            // 
            // colDestTraffic
            // 
            this.colDestTraffic.Caption = "Célforgalomban használható?";
            this.colDestTraffic.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colDestTraffic.FieldName = "DestTraffic";
            this.colDestTraffic.Name = "colDestTraffic";
            this.colDestTraffic.OptionsColumn.AllowEdit = false;
            this.colDestTraffic.Visible = true;
            this.colDestTraffic.VisibleIndex = 8;
            this.colDestTraffic.Width = 67;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // colEDG_ETLCODE
            // 
            this.colEDG_ETLCODE.Caption = "Útdíj szelvény";
            this.colEDG_ETLCODE.FieldName = "EDG_ETLCODE";
            this.colEDG_ETLCODE.Name = "colEDG_ETLCODE";
            this.colEDG_ETLCODE.OptionsColumn.AllowEdit = false;
            this.colEDG_ETLCODE.Visible = true;
            this.colEDG_ETLCODE.VisibleIndex = 9;
            this.colEDG_ETLCODE.Width = 67;
            // 
            // colToll
            // 
            this.colToll.Caption = "Útdíj";
            this.colToll.DisplayFormat.FormatString = "#,#0.00";
            this.colToll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colToll.FieldName = "Toll";
            this.colToll.Name = "colToll";
            this.colToll.SummaryItem.DisplayFormat = "{0:C2}";
            this.colToll.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colToll.Visible = true;
            this.colToll.VisibleIndex = 10;
            this.colToll.Width = 94;
            // 
            // openExcel
            // 
            this.openExcel.CheckFileExists = false;
            this.openExcel.DefaultExt = "xls";
            this.openExcel.Filter = "*.xls|*.xls";
            // 
            // colEDG_MAXWEIGHT
            // 
            this.colEDG_MAXWEIGHT.Caption = "Súlykorlát";
            this.colEDG_MAXWEIGHT.DisplayFormat.FormatString = "Numeric \"#,#0.00\"";
            this.colEDG_MAXWEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEDG_MAXWEIGHT.FieldName = "EDG_MAXWEIGHT";
            this.colEDG_MAXWEIGHT.Name = "colEDG_MAXWEIGHT";
            this.colEDG_MAXWEIGHT.OptionsColumn.AllowEdit = false;
            this.colEDG_MAXWEIGHT.Visible = true;
            this.colEDG_MAXWEIGHT.VisibleIndex = 11;
            // 
            // colEDG_MAXHEIGHT
            // 
            this.colEDG_MAXHEIGHT.Caption = "Magasságkorlát";
            this.colEDG_MAXHEIGHT.DisplayFormat.FormatString = "Numeric \"#,#0.00\"";
            this.colEDG_MAXHEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEDG_MAXHEIGHT.FieldName = "EDG_MAXHEIGHT";
            this.colEDG_MAXHEIGHT.Name = "colEDG_MAXHEIGHT";
            this.colEDG_MAXHEIGHT.OptionsColumn.AllowEdit = false;
            this.colEDG_MAXHEIGHT.Visible = true;
            this.colEDG_MAXHEIGHT.VisibleIndex = 12;
            // 
            // colEDG_MAXWIDTH
            // 
            this.colEDG_MAXWIDTH.Caption = "Szélességkorlát";
            this.colEDG_MAXWIDTH.DisplayFormat.FormatString = "Numeric \"#,#0.00\"";
            this.colEDG_MAXWIDTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEDG_MAXWIDTH.FieldName = "EDG_MAXWIDTH";
            this.colEDG_MAXWIDTH.Name = "colEDG_MAXWIDTH";
            this.colEDG_MAXWIDTH.OptionsColumn.AllowEdit = false;
            this.colEDG_MAXWIDTH.Visible = true;
            this.colEDG_MAXWIDTH.VisibleIndex = 13;
            // 
            // dlgRouteVisDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 445);
            this.Controls.Add(this.gridRouteDetails);
            this.Controls.Add(this.toolMain);
            this.Name = "dlgRouteVisDetails";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Részletek";
            this.Controls.SetChildIndex(this.toolMain, 0);
            this.Controls.SetChildIndex(this.gridRouteDetails, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRouteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRouteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton excelToolStripButton;
        private DevExpress.XtraGrid.GridControl gridRouteDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRouteDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colDist;
        private DevExpress.XtraGrid.Columns.GridColumn colSpeed;
        private DevExpress.XtraGrid.Columns.GridColumn colDuration;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_ID_FROM;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_ID_TO;
        private DevExpress.XtraGrid.Columns.GridColumn colRoadType;
        private DevExpress.XtraGrid.Columns.GridColumn colOneWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colWZone;
        private DevExpress.XtraGrid.Columns.GridColumn colDestTraffic;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_ETLCODE;
        private DevExpress.XtraGrid.Columns.GridColumn colToll;
        private System.Windows.Forms.OpenFileDialog openExcel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnRouteSectionType;
        private System.Windows.Forms.ImageList imlRouteSectionType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_MAXWEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_MAXHEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_MAXWIDTH;
    }
}