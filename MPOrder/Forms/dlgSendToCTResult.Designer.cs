namespace MPOrder.Forms
{
    partial class dlgSendToCTResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSendToCTResult));
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.excelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gridResult = new DevExpress.XtraGrid.GridControl();
            this.gridViewResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grcResultTypeVal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repResultTypeVal = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imlResultType = new System.Windows.Forms.ImageList();
            this.grcCustomerOrderNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.openExcel = new System.Windows.Forms.OpenFileDialog();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.toolMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repResultTypeVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
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
            this.toolMain.Size = new System.Drawing.Size(656, 25);
            this.toolMain.TabIndex = 12;
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
            // gridResult
            // 
            this.gridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResult.EmbeddedNavigator.TextStringFormat = "Record {0} of {1}";
            this.gridResult.Location = new System.Drawing.Point(0, 25);
            this.gridResult.MainView = this.gridViewResult;
            this.gridResult.Name = "gridResult";
            this.gridResult.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repResultTypeVal,
            this.repositoryItemImageComboBox1});
            this.gridResult.Size = new System.Drawing.Size(656, 283);
            this.gridResult.TabIndex = 14;
            this.gridResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewResult});
            // 
            // gridViewResult
            // 
            this.gridViewResult.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grcResultTypeVal,
            this.grcCustomerOrderNumber,
            this.grcMessage});
            this.gridViewResult.GridControl = this.gridResult;
            this.gridViewResult.Images = this.imlResultType;
            this.gridViewResult.Name = "gridViewResult";
            this.gridViewResult.OptionsBehavior.Editable = false;
            this.gridViewResult.OptionsCustomization.AllowGroup = false;
            this.gridViewResult.OptionsCustomization.AllowSort = false;
            this.gridViewResult.OptionsMenu.EnableColumnMenu = false;
            this.gridViewResult.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewResult.OptionsView.ShowFooter = true;
            this.gridViewResult.OptionsView.ShowGroupPanel = false;
            this.gridViewResult.PaintStyleName = "(Default)";
            // 
            // grcResultTypeVal
            // 
            this.grcResultTypeVal.Caption = "_";
            this.grcResultTypeVal.ColumnEdit = this.repResultTypeVal;
            this.grcResultTypeVal.FieldName = "ResultTypeVal";
            this.grcResultTypeVal.Name = "grcResultTypeVal";
            this.grcResultTypeVal.Visible = true;
            this.grcResultTypeVal.VisibleIndex = 0;
            this.grcResultTypeVal.Width = 42;
            // 
            // repResultTypeVal
            // 
            this.repResultTypeVal.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repResultTypeVal.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("OK", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Figyelem", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hiba", 2, 2)});
            this.repResultTypeVal.LargeImages = this.imlResultType;
            this.repResultTypeVal.Name = "repResultTypeVal";
            this.repResultTypeVal.SmallImages = this.imlResultType;
            // 
            // imlResultType
            // 
            this.imlResultType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlResultType.ImageStream")));
            this.imlResultType.TransparentColor = System.Drawing.Color.Transparent;
            this.imlResultType.Images.SetKeyName(0, "tick.png");
            this.imlResultType.Images.SetKeyName(1, "bullet_error.png");
            this.imlResultType.Images.SetKeyName(2, "messagebox_warning.png");
            // 
            // grcCustomerOrderNumber
            // 
            this.grcCustomerOrderNumber.Caption = "Rendelésszám";
            this.grcCustomerOrderNumber.FieldName = "CustomerOrderNumber";
            this.grcCustomerOrderNumber.Name = "grcCustomerOrderNumber";
            this.grcCustomerOrderNumber.OptionsColumn.AllowEdit = false;
            this.grcCustomerOrderNumber.Visible = true;
            this.grcCustomerOrderNumber.VisibleIndex = 1;
            this.grcCustomerOrderNumber.Width = 88;
            // 
            // grcMessage
            // 
            this.grcMessage.Caption = "Szöveg";
            this.grcMessage.FieldName = "Message";
            this.grcMessage.Name = "grcMessage";
            this.grcMessage.OptionsColumn.AllowEdit = false;
            this.grcMessage.Visible = true;
            this.grcMessage.VisibleIndex = 2;
            this.grcMessage.Width = 464;
            // 
            // openExcel
            // 
            this.openExcel.CheckFileExists = false;
            this.openExcel.DefaultExt = "xls";
            this.openExcel.Filter = "*.xls|*.xls";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("1", "1", -1)});
            this.repositoryItemImageComboBox1.LargeImages = this.imlResultType;
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imlResultType;
            // 
            // dlgSendToCTResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 347);
            this.Controls.Add(this.gridResult);
            this.Controls.Add(this.toolMain);
            this.Name = "dlgSendToCTResult";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "CT áttöltés eredménye";
            this.Load += new System.EventHandler(this.dlgSendToCTResult_Load);
            this.Controls.SetChildIndex(this.toolMain, 0);
            this.Controls.SetChildIndex(this.gridResult, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repResultTypeVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton excelToolStripButton;
        private DevExpress.XtraGrid.GridControl gridResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewResult;
        private DevExpress.XtraGrid.Columns.GridColumn grcResultTypeVal;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repResultTypeVal;
        private DevExpress.XtraGrid.Columns.GridColumn grcMessage;
        private DevExpress.XtraGrid.Columns.GridColumn grcCustomerOrderNumber;
        private System.Windows.Forms.ImageList imlResultType;
        private System.Windows.Forms.OpenFileDialog openExcel;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
    }
}