namespace PMapUI.Forms
{
    partial class dlgDepotList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgDepotList));
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.excelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gridDepots = new DevExpress.XtraGrid.GridControl();
            this.gridViewDepots = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDEP_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDEP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colZIP_NUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colZIP_CITY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDEP_ADRSTREET = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridDEP_ADRNUM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageEditType = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.itemImageType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.toolMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDepots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImageType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
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
            this.toolMain.Size = new System.Drawing.Size(571, 25);
            this.toolMain.TabIndex = 11;
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
            // gridDepots
            // 
            this.gridDepots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDepots.EmbeddedNavigator.TextStringFormat = "Record {0} of {1}";
            this.gridDepots.Location = new System.Drawing.Point(0, 25);
            this.gridDepots.MainView = this.gridViewDepots;
            this.gridDepots.Name = "gridDepots";
            this.gridDepots.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.imageEditType,
            this.itemImageType,
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageComboBox1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridDepots.Size = new System.Drawing.Size(571, 278);
            this.gridDepots.TabIndex = 13;
            this.gridDepots.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDepots});
            // 
            // gridViewDepots
            // 
            this.gridViewDepots.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colDEP_CODE,
            this.colDEP_NAME,
            this.colZIP_NUM,
            this.colZIP_CITY,
            this.colDEP_ADRSTREET,
            this.gridDEP_ADRNUM});
            this.gridViewDepots.GridControl = this.gridDepots;
            this.gridViewDepots.Name = "gridViewDepots";
            this.gridViewDepots.OptionsBehavior.Editable = false;
            this.gridViewDepots.OptionsCustomization.AllowGroup = false;
            this.gridViewDepots.OptionsCustomization.AllowSort = false;
            this.gridViewDepots.OptionsMenu.EnableColumnMenu = false;
            this.gridViewDepots.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDepots.OptionsView.ShowFooter = true;
            this.gridViewDepots.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colDEP_CODE
            // 
            this.colDEP_CODE.Caption = "Fel/lerakókód";
            this.colDEP_CODE.FieldName = "DEP_CODE";
            this.colDEP_CODE.Name = "colDEP_CODE";
            this.colDEP_CODE.Visible = true;
            this.colDEP_CODE.VisibleIndex = 0;
            this.colDEP_CODE.Width = 67;
            // 
            // colDEP_NAME
            // 
            this.colDEP_NAME.Caption = "Megnevezés";
            this.colDEP_NAME.FieldName = "DEP_NAME";
            this.colDEP_NAME.Name = "colDEP_NAME";
            this.colDEP_NAME.Visible = true;
            this.colDEP_NAME.VisibleIndex = 1;
            this.colDEP_NAME.Width = 111;
            // 
            // colZIP_NUM
            // 
            this.colZIP_NUM.Caption = "Irsz.";
            this.colZIP_NUM.FieldName = "ZIP_NUM";
            this.colZIP_NUM.Name = "colZIP_NUM";
            this.colZIP_NUM.Visible = true;
            this.colZIP_NUM.VisibleIndex = 2;
            this.colZIP_NUM.Width = 40;
            // 
            // colZIP_CITY
            // 
            this.colZIP_CITY.Caption = "Város";
            this.colZIP_CITY.FieldName = "ZIP_CITY";
            this.colZIP_CITY.Name = "colZIP_CITY";
            this.colZIP_CITY.Visible = true;
            this.colZIP_CITY.VisibleIndex = 3;
            this.colZIP_CITY.Width = 104;
            // 
            // colDEP_ADRSTREET
            // 
            this.colDEP_ADRSTREET.Caption = "Közterület";
            this.colDEP_ADRSTREET.FieldName = "DEP_ADRSTREET";
            this.colDEP_ADRSTREET.Name = "colDEP_ADRSTREET";
            this.colDEP_ADRSTREET.Visible = true;
            this.colDEP_ADRSTREET.VisibleIndex = 4;
            this.colDEP_ADRSTREET.Width = 120;
            // 
            // gridDEP_ADRNUM
            // 
            this.gridDEP_ADRNUM.Caption = "Házszám";
            this.gridDEP_ADRNUM.FieldName = "DEP_ADRNUM";
            this.gridDEP_ADRNUM.Name = "gridDEP_ADRNUM";
            this.gridDEP_ADRNUM.Visible = true;
            this.gridDEP_ADRNUM.VisibleIndex = 5;
            this.gridDEP_ADRNUM.Width = 51;
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
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // openExcel
            // 
            this.openExcel.CheckFileExists = false;
            this.openExcel.DefaultExt = "xls";
            this.openExcel.Filter = "*.xls|*.xls";
            // 
            // dlgDepotList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 342);
            this.Controls.Add(this.gridDepots);
            this.Controls.Add(this.toolMain);
            this.Name = "dlgDepotList";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "";
            this.Controls.SetChildIndex(this.toolMain, 0);
            this.Controls.SetChildIndex(this.gridDepots, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDepots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImageType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton excelToolStripButton;
        private DevExpress.XtraGrid.GridControl gridDepots;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDepots;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colDEP_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn colDEP_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn colZIP_NUM;
        private DevExpress.XtraGrid.Columns.GridColumn colZIP_CITY;
        private DevExpress.XtraGrid.Columns.GridColumn colDEP_ADRSTREET;
        private DevExpress.XtraGrid.Columns.GridColumn gridDEP_ADRNUM;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit imageEditType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox itemImageType;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private System.Windows.Forms.OpenFileDialog openExcel;
    }
}