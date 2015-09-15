namespace PMapTestApp
{
    partial class dlgCheckRouteDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgCheckRouteDetails));
            this.layoutDialog = new System.Windows.Forms.TableLayoutPanel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.gridRouteDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewRouteDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDist = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpeed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_ID_FROM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_ID_TO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoadType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOneWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colWZone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDestTraffic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colEDG_ETLCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageEditType = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.itemImageType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbRST_ID_LIST = new System.Windows.Forms.ComboBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSpeedProfile = new System.Windows.Forms.ComboBox();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.excelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
            this.layoutDialog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRouteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRouteDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImageType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutDialog
            // 
            this.layoutDialog.ColumnCount = 1;
            this.layoutDialog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.9924F));
            this.layoutDialog.Controls.Add(this.buttonClose, 0, 0);
            this.layoutDialog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutDialog.Location = new System.Drawing.Point(0, 462);
            this.layoutDialog.Name = "layoutDialog";
            this.layoutDialog.RowCount = 1;
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDialog.Size = new System.Drawing.Size(905, 40);
            this.layoutDialog.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.CausesValidation = false;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(410, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(84, 34);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Bezárás";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // gridRouteDetails
            // 
            this.gridRouteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRouteDetails.Location = new System.Drawing.Point(0, 65);
            this.gridRouteDetails.MainView = this.gridViewRouteDetails;
            this.gridRouteDetails.Name = "gridRouteDetails";
            this.gridRouteDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.imageEditType,
            this.itemImageType,
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageComboBox1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridRouteDetails.Size = new System.Drawing.Size(905, 397);
            this.gridRouteDetails.TabIndex = 7;
            this.gridRouteDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRouteDetails});
            // 
            // gridViewRouteDetails
            // 
            this.gridViewRouteDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colText,
            this.colDist,
            this.colSpeed,
            this.colDuration,
            this.colNOD_ID_FROM,
            this.colNOD_ID_TO,
            this.colRoadType,
            this.colOneWay,
            this.colWZone,
            this.colDestTraffic,
            this.colEDG_ETLCODE});
            this.gridViewRouteDetails.GridControl = this.gridRouteDetails;
            this.gridViewRouteDetails.Name = "gridViewRouteDetails";
            this.gridViewRouteDetails.OptionsBehavior.Editable = false;
            this.gridViewRouteDetails.OptionsCustomization.AllowGroup = false;
            this.gridViewRouteDetails.OptionsCustomization.AllowSort = false;
            this.gridViewRouteDetails.OptionsMenu.EnableColumnMenu = false;
            this.gridViewRouteDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewRouteDetails.OptionsView.ShowFooter = true;
            this.gridViewRouteDetails.OptionsView.ShowGroupPanel = false;
            // 
            // colText
            // 
            this.colText.Caption = "Leírás";
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.OptionsColumn.AllowEdit = false;
            this.colText.Visible = true;
            this.colText.VisibleIndex = 3;
            this.colText.Width = 364;
            // 
            // colDist
            // 
            this.colDist.Caption = "Távolság (m)";
            this.colDist.DisplayFormat.FormatString = "#,#0.00";
            this.colDist.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDist.FieldName = "Dist";
            this.colDist.Name = "colDist";
            this.colDist.OptionsColumn.AllowEdit = false;
            this.colDist.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDist.Visible = true;
            this.colDist.VisibleIndex = 4;
            this.colDist.Width = 104;
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
            this.colSpeed.VisibleIndex = 5;
            this.colSpeed.Width = 87;
            // 
            // colDuration
            // 
            this.colDuration.Caption = "Menetidő (perc)";
            this.colDuration.DisplayFormat.FormatString = "#,#0.00";
            this.colDuration.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDuration.FieldName = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.OptionsColumn.AllowEdit = false;
            this.colDuration.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDuration.Visible = true;
            this.colDuration.VisibleIndex = 6;
            this.colDuration.Width = 102;
            // 
            // colNOD_ID_FROM
            // 
            this.colNOD_ID_FROM.Caption = "Kezdő node";
            this.colNOD_ID_FROM.FieldName = "NOD_ID_FROM";
            this.colNOD_ID_FROM.Name = "colNOD_ID_FROM";
            this.colNOD_ID_FROM.OptionsColumn.AllowEdit = false;
            this.colNOD_ID_FROM.Visible = true;
            this.colNOD_ID_FROM.VisibleIndex = 0;
            // 
            // colNOD_ID_TO
            // 
            this.colNOD_ID_TO.Caption = "Befejező node";
            this.colNOD_ID_TO.FieldName = "NOD_ID_TO";
            this.colNOD_ID_TO.Name = "colNOD_ID_TO";
            this.colNOD_ID_TO.OptionsColumn.AllowEdit = false;
            this.colNOD_ID_TO.Visible = true;
            this.colNOD_ID_TO.VisibleIndex = 1;
            // 
            // colRoadType
            // 
            this.colRoadType.Caption = "Úttípus";
            this.colRoadType.FieldName = "RoadType";
            this.colRoadType.Name = "colRoadType";
            this.colRoadType.OptionsColumn.AllowEdit = false;
            this.colRoadType.Visible = true;
            this.colRoadType.VisibleIndex = 2;
            // 
            // colOneWay
            // 
            this.colOneWay.Caption = "Egyirányú?";
            this.colOneWay.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOneWay.FieldName = "OneWay";
            this.colOneWay.Name = "colOneWay";
            this.colOneWay.OptionsColumn.AllowEdit = false;
            this.colOneWay.Visible = true;
            this.colOneWay.VisibleIndex = 7;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colWZone
            // 
            this.colWZone.Caption = "Súlykorlátozás zóna";
            this.colWZone.FieldName = "WZone";
            this.colWZone.Name = "colWZone";
            this.colWZone.OptionsColumn.AllowEdit = false;
            this.colWZone.Visible = true;
            this.colWZone.VisibleIndex = 8;
            // 
            // colDestTraffic
            // 
            this.colDestTraffic.Caption = "Célforgalomban használható?";
            this.colDestTraffic.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colDestTraffic.FieldName = "DestTraffic";
            this.colDestTraffic.Name = "colDestTraffic";
            this.colDestTraffic.OptionsColumn.AllowEdit = false;
            this.colDestTraffic.Visible = true;
            this.colDestTraffic.VisibleIndex = 9;
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
            this.colEDG_ETLCODE.VisibleIndex = 10;
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
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbRST_ID_LIST);
            this.panel1.Controls.Add(this.lblDistance);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblDuration);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbSpeedProfile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 40);
            this.panel1.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(266, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Behajt.övezetek";
            // 
            // cmbRST_ID_LIST
            // 
            this.cmbRST_ID_LIST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRST_ID_LIST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRST_ID_LIST.FormattingEnabled = true;
            this.cmbRST_ID_LIST.Location = new System.Drawing.Point(357, 9);
            this.cmbRST_ID_LIST.Name = "cmbRST_ID_LIST";
            this.cmbRST_ID_LIST.Size = new System.Drawing.Size(249, 21);
            this.cmbRST_ID_LIST.TabIndex = 6;
            this.cmbRST_ID_LIST.SelectedIndexChanged += new System.EventHandler(this.cmbRST_ID_LIST_SelectedIndexChanged);
            // 
            // lblDistance
            // 
            this.lblDistance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDistance.Location = new System.Drawing.Point(671, 10);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(78, 18);
            this.lblDistance.TabIndex = 5;
            this.lblDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(613, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Távolság";
            // 
            // lblDuration
            // 
            this.lblDuration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDuration.Location = new System.Drawing.Point(814, 10);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(78, 18);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(756, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Menetidő";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sebességprofil:";
            // 
            // cmbSpeedProfile
            // 
            this.cmbSpeedProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeedProfile.FormattingEnabled = true;
            this.cmbSpeedProfile.Location = new System.Drawing.Point(89, 9);
            this.cmbSpeedProfile.Name = "cmbSpeedProfile";
            this.cmbSpeedProfile.Size = new System.Drawing.Size(170, 21);
            this.cmbSpeedProfile.TabIndex = 0;
            this.cmbSpeedProfile.SelectedIndexChanged += new System.EventHandler(this.cmbSpeedProfile_SelectedIndexChanged);
            this.cmbSpeedProfile.TextChanged += new System.EventHandler(this.cmbSpeedProfile_TextChanged);
            // 
            // toolMain
            // 
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.excelToolStripButton});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.Size = new System.Drawing.Size(905, 25);
            this.toolMain.TabIndex = 9;
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
            // dlgCheckRouteDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 502);
            this.Controls.Add(this.gridRouteDetails);
            this.Controls.Add(this.layoutDialog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolMain);
            this.Name = "dlgCheckRouteDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Útvonalrészletek";
            this.Load += new System.EventHandler(this.dlgCheckRouteDetails_Load);
            this.layoutDialog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRouteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRouteDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImageType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel layoutDialog;
        public System.Windows.Forms.Button buttonClose;
        private DevExpress.XtraGrid.GridControl gridRouteDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRouteDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colDist;
        private DevExpress.XtraGrid.Columns.GridColumn colSpeed;
        private DevExpress.XtraGrid.Columns.GridColumn colDuration;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit imageEditType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox itemImageType;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbSpeedProfile;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_ID_FROM;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_ID_TO;
        private DevExpress.XtraGrid.Columns.GridColumn colRoadType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDistance;
        private DevExpress.XtraGrid.Columns.GridColumn colOneWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colWZone;
        private DevExpress.XtraGrid.Columns.GridColumn colDestTraffic;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colEDG_ETLCODE;
        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton excelToolStripButton;
        private System.Windows.Forms.OpenFileDialog openExcel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbRST_ID_LIST;
    }
}