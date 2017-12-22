namespace PMap.Forms
{
    partial class dlgTourDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTourDetails));
            this.gridTourDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewTourDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imlTypes = new System.Windows.Forms.ImageList();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDist = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpeed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOneWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colWZone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoadType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDestTaffic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColEDG_ETLCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColOrigToll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColToll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDG_MAXWEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDG_MAXHEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDG_MAXWIDTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageEditType = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.itemImageType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.excelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTourDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTourDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImageType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.toolMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridTourDetails
            // 
            this.gridTourDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTourDetails.Location = new System.Drawing.Point(0, 25);
            this.gridTourDetails.MainView = this.gridViewTourDetails;
            this.gridTourDetails.Name = "gridTourDetails";
            this.gridTourDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.imageEditType,
            this.itemImageType,
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageComboBox1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridTourDetails.Size = new System.Drawing.Size(960, 326);
            this.gridTourDetails.TabIndex = 5;
            this.gridTourDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTourDetails});
            // 
            // gridViewTourDetails
            // 
            this.gridViewTourDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridType,
            this.colText,
            this.colDist,
            this.colSpeed,
            this.colDuration,
            this.colOneWay,
            this.colWZone,
            this.colRoadType,
            this.colDestTaffic,
            this.gridColEDG_ETLCODE,
            this.gridColOrigToll,
            this.gridColToll,
            this.colEDG_MAXWEIGHT,
            this.colEDG_MAXHEIGHT,
            this.colEDG_MAXWIDTH});
            this.gridViewTourDetails.GridControl = this.gridTourDetails;
            this.gridViewTourDetails.Name = "gridViewTourDetails";
            this.gridViewTourDetails.OptionsBehavior.Editable = false;
            this.gridViewTourDetails.OptionsCustomization.AllowGroup = false;
            this.gridViewTourDetails.OptionsCustomization.AllowSort = false;
            this.gridViewTourDetails.OptionsMenu.EnableColumnMenu = false;
            this.gridViewTourDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewTourDetails.OptionsView.ShowFooter = true;
            this.gridViewTourDetails.OptionsView.ShowGroupPanel = false;
            // 
            // gridType
            // 
            this.gridType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridType.FieldName = "Type";
            this.gridType.Name = "gridType";
            this.gridType.Visible = true;
            this.gridType.VisibleIndex = 0;
            this.gridType.Width = 27;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1)});
            this.repositoryItemImageComboBox1.LargeImages = this.imlTypes;
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imlTypes;
            // 
            // imlTypes
            // 
            this.imlTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTypes.ImageStream")));
            this.imlTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTypes.Images.SetKeyName(0, "bullet_blue.png");
            this.imlTypes.Images.SetKeyName(1, "Flag2.png");
            // 
            // colText
            // 
            this.colText.Caption = "Leírás";
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.OptionsColumn.AllowEdit = false;
            this.colText.Visible = true;
            this.colText.VisibleIndex = 1;
            this.colText.Width = 280;
            // 
            // colDist
            // 
            this.colDist.Caption = "Távolság (m)";
            this.colDist.DisplayFormat.FormatString = "#,#0.00";
            this.colDist.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDist.FieldName = "Dist";
            this.colDist.Name = "colDist";
            this.colDist.OptionsColumn.AllowEdit = false;
            this.colDist.SummaryItem.DisplayFormat = "{0}";
            this.colDist.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDist.Visible = true;
            this.colDist.VisibleIndex = 2;
            this.colDist.Width = 80;
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
            this.colSpeed.VisibleIndex = 3;
            this.colSpeed.Width = 67;
            // 
            // colDuration
            // 
            this.colDuration.Caption = "Menetidő (perc)";
            this.colDuration.DisplayFormat.FormatString = "#,#0.00";
            this.colDuration.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDuration.FieldName = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.OptionsColumn.AllowEdit = false;
            this.colDuration.Visible = true;
            this.colDuration.VisibleIndex = 4;
            this.colDuration.Width = 78;
            // 
            // colOneWay
            // 
            this.colOneWay.Caption = "Egyirányú?";
            this.colOneWay.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOneWay.FieldName = "OneWay";
            this.colOneWay.Name = "colOneWay";
            this.colOneWay.OptionsColumn.AllowEdit = false;
            this.colOneWay.Visible = true;
            this.colOneWay.VisibleIndex = 5;
            this.colOneWay.Width = 57;
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
            this.colWZone.VisibleIndex = 6;
            this.colWZone.Width = 57;
            // 
            // colRoadType
            // 
            this.colRoadType.Caption = "Úttípus";
            this.colRoadType.FieldName = "RoadType";
            this.colRoadType.Name = "colRoadType";
            this.colRoadType.OptionsColumn.AllowEdit = false;
            this.colRoadType.Visible = true;
            this.colRoadType.VisibleIndex = 7;
            this.colRoadType.Width = 57;
            // 
            // colDestTaffic
            // 
            this.colDestTaffic.Caption = "Célforgalomben használható?";
            this.colDestTaffic.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colDestTaffic.FieldName = "DestTraffic";
            this.colDestTaffic.Name = "colDestTaffic";
            this.colDestTaffic.OptionsColumn.AllowEdit = false;
            this.colDestTaffic.Visible = true;
            this.colDestTaffic.VisibleIndex = 8;
            this.colDestTaffic.Width = 57;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridColEDG_ETLCODE
            // 
            this.gridColEDG_ETLCODE.Caption = "Útdíj szelvény";
            this.gridColEDG_ETLCODE.FieldName = "EDG_ETLCODE";
            this.gridColEDG_ETLCODE.Name = "gridColEDG_ETLCODE";
            this.gridColEDG_ETLCODE.OptionsColumn.AllowEdit = false;
            this.gridColEDG_ETLCODE.Visible = true;
            this.gridColEDG_ETLCODE.VisibleIndex = 9;
            this.gridColEDG_ETLCODE.Width = 57;
            // 
            // gridColOrigToll
            // 
            this.gridColOrigToll.Caption = "Szelvény útdíj";
            this.gridColOrigToll.DisplayFormat.FormatString = "#,#0.00";
            this.gridColOrigToll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColOrigToll.FieldName = "OrigToll";
            this.gridColOrigToll.Name = "gridColOrigToll";
            this.gridColOrigToll.OptionsColumn.AllowEdit = false;
            this.gridColOrigToll.Visible = true;
            this.gridColOrigToll.VisibleIndex = 10;
            this.gridColOrigToll.Width = 55;
            // 
            // gridColToll
            // 
            this.gridColToll.Caption = "Számított útdíj";
            this.gridColToll.DisplayFormat.FormatString = "#,#0.00";
            this.gridColToll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColToll.FieldName = "Toll";
            this.gridColToll.Name = "gridColToll";
            this.gridColToll.OptionsColumn.AllowEdit = false;
            this.gridColToll.SummaryItem.DisplayFormat = "{0:C2}";
            this.gridColToll.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColToll.Visible = true;
            this.gridColToll.VisibleIndex = 11;
            this.gridColToll.Width = 67;
            // 
            // colEDG_MAXWEIGHT
            // 
            this.colEDG_MAXWEIGHT.Caption = "Súlykorlát";
            this.colEDG_MAXWEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.colEDG_MAXWEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEDG_MAXWEIGHT.FieldName = "EDG_MAXWEIGHT";
            this.colEDG_MAXWEIGHT.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.colEDG_MAXWEIGHT.Name = "colEDG_MAXWEIGHT";
            this.colEDG_MAXWEIGHT.OptionsColumn.AllowEdit = false;
            this.colEDG_MAXWEIGHT.Visible = true;
            this.colEDG_MAXWEIGHT.VisibleIndex = 12;
            // 
            // colEDG_MAXHEIGHT
            // 
            this.colEDG_MAXHEIGHT.Caption = "Magasságkorlát";
            this.colEDG_MAXHEIGHT.DisplayFormat.FormatString = "#,#0.00";
            this.colEDG_MAXHEIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEDG_MAXHEIGHT.FieldName = "EDG_MAXHEIGHT";
            this.colEDG_MAXHEIGHT.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.colEDG_MAXHEIGHT.Name = "colEDG_MAXHEIGHT";
            this.colEDG_MAXHEIGHT.OptionsColumn.AllowEdit = false;
            this.colEDG_MAXHEIGHT.Visible = true;
            this.colEDG_MAXHEIGHT.VisibleIndex = 13;
            // 
            // colEDG_MAXWIDTH
            // 
            this.colEDG_MAXWIDTH.Caption = "Szélességkorlát";
            this.colEDG_MAXWIDTH.DisplayFormat.FormatString = "#,#0.00";
            this.colEDG_MAXWIDTH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEDG_MAXWIDTH.FieldName = "EDG_MAXWIDTH";
            this.colEDG_MAXWIDTH.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.colEDG_MAXWIDTH.Name = "colEDG_MAXWIDTH";
            this.colEDG_MAXWIDTH.OptionsColumn.AllowEdit = false;
            this.colEDG_MAXWIDTH.Visible = true;
            this.colEDG_MAXWIDTH.VisibleIndex = 14;
            // 
            // imageEditType
            // 
            this.imageEditType.AllowFocused = false;
            this.imageEditType.AutoHeight = false;
            this.imageEditType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageEditType.Name = "imageEditType";
            // 
            // itemImageType
            // 
            this.itemImageType.AutoHeight = false;
            this.itemImageType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.itemImageType.Name = "itemImageType";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // toolMain
            // 
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.excelToolStripButton});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(960, 25);
            this.toolMain.TabIndex = 4;
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
            // openExcel
            // 
            this.openExcel.CheckFileExists = false;
            this.openExcel.DefaultExt = "xls";
            this.openExcel.Filter = "*.xls|*.xls";
            // 
            // dlgTourDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 390);
            this.Controls.Add(this.gridTourDetails);
            this.Controls.Add(this.toolMain);
            this.Name = "dlgTourDetails";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Túrarészletező";
            this.Controls.SetChildIndex(this.toolMain, 0);
            this.Controls.SetChildIndex(this.gridTourDetails, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTourDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTourDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImageType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridTourDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTourDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox itemImageType;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colDist;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit imageEditType;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private DevExpress.XtraGrid.Columns.GridColumn gridType;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.ImageList imlTypes;
        private DevExpress.XtraGrid.Columns.GridColumn colSpeed;
        private DevExpress.XtraGrid.Columns.GridColumn colDuration;
        private DevExpress.XtraGrid.Columns.GridColumn colOneWay;
        private DevExpress.XtraGrid.Columns.GridColumn colWZone;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colRoadType;
        private DevExpress.XtraGrid.Columns.GridColumn colDistTaffic;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colDestTaffic;
        private DevExpress.XtraGrid.Columns.GridColumn gridColEDG_ETLCODE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColOrigToll;
        private DevExpress.XtraGrid.Columns.GridColumn gridColToll;
        private System.Windows.Forms.ToolStripButton excelToolStripButton;
        private System.Windows.Forms.OpenFileDialog openExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_MAXWEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_MAXHEIGHT;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_MAXWIDTH;
    }
}