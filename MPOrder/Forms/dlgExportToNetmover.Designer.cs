namespace MPOrder.Forms
{
    partial class dlgExportToNetmover
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgExportToNetmover));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFile = new System.Windows.Forms.Button();
            this.lblvFile = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.gridPlans = new DevExpress.XtraGrid.GridControl();
            this.gridViewPlans = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grcID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPLN_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPLN_DATE_B = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grcPLN_DATE_E = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dlgSaveCSV = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlans)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridPlans, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.76636F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.23364F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(698, 176);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel3.Controls.Add(this.btnFile, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblvFile, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblFile, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 131);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(698, 45);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.BackColor = System.Drawing.Color.Yellow;
            this.btnFile.Image = ((System.Drawing.Image)(resources.GetObject("btnFile.Image")));
            this.btnFile.Location = new System.Drawing.Point(664, 9);
            this.btnFile.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(31, 26);
            this.btnFile.TabIndex = 5;
            this.btnFile.UseVisualStyleBackColor = false;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblvFile
            // 
            this.lblvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblvFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblvFile.Location = new System.Drawing.Point(103, 11);
            this.lblvFile.Name = "lblvFile";
            this.lblvFile.Size = new System.Drawing.Size(555, 22);
            this.lblvFile.TabIndex = 2;
            this.lblvFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFile
            // 
            this.lblFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(3, 16);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(94, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "Fájl";
            // 
            // gridPlans
            // 
            this.gridPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPlans.EmbeddedNavigator.TextStringFormat = "Record {0} of {1}";
            this.gridPlans.Location = new System.Drawing.Point(3, 3);
            this.gridPlans.MainView = this.gridViewPlans;
            this.gridPlans.Name = "gridPlans";
            this.gridPlans.Size = new System.Drawing.Size(692, 125);
            this.gridPlans.TabIndex = 16;
            this.gridPlans.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPlans});
            // 
            // gridViewPlans
            // 
            this.gridViewPlans.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grcID,
            this.grcPLN_NAME,
            this.grcPLN_DATE_B,
            this.grcPLN_DATE_E});
            this.gridViewPlans.GridControl = this.gridPlans;
            this.gridViewPlans.Name = "gridViewPlans";
            this.gridViewPlans.OptionsBehavior.Editable = false;
            this.gridViewPlans.OptionsCustomization.AllowGroup = false;
            this.gridViewPlans.OptionsCustomization.AllowSort = false;
            this.gridViewPlans.OptionsMenu.EnableColumnMenu = false;
            this.gridViewPlans.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewPlans.OptionsView.ShowFooter = true;
            this.gridViewPlans.OptionsView.ShowGroupPanel = false;
            this.gridViewPlans.PaintStyleName = "(Default)";
            this.gridViewPlans.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewPlans_FocusedRowChanged);
            // 
            // grcID
            // 
            this.grcID.Caption = "ID";
            this.grcID.FieldName = "ID";
            this.grcID.Name = "grcID";
            this.grcID.OptionsColumn.AllowEdit = false;
            // 
            // grcPLN_NAME
            // 
            this.grcPLN_NAME.Caption = "Terv neve";
            this.grcPLN_NAME.FieldName = "PLN_NAME";
            this.grcPLN_NAME.Name = "grcPLN_NAME";
            this.grcPLN_NAME.OptionsColumn.AllowEdit = false;
            this.grcPLN_NAME.Visible = true;
            this.grcPLN_NAME.VisibleIndex = 0;
            // 
            // grcPLN_DATE_B
            // 
            this.grcPLN_DATE_B.Caption = "Kezdve";
            this.grcPLN_DATE_B.FieldName = "PLN_DATE_B";
            this.grcPLN_DATE_B.Name = "grcPLN_DATE_B";
            this.grcPLN_DATE_B.OptionsColumn.AllowEdit = false;
            this.grcPLN_DATE_B.Visible = true;
            this.grcPLN_DATE_B.VisibleIndex = 1;
            // 
            // grcPLN_DATE_E
            // 
            this.grcPLN_DATE_E.Caption = "Befejezés";
            this.grcPLN_DATE_E.FieldName = "PLN_DATE_E";
            this.grcPLN_DATE_E.Name = "grcPLN_DATE_E";
            this.grcPLN_DATE_E.OptionsColumn.AllowEdit = false;
            this.grcPLN_DATE_E.Visible = true;
            this.grcPLN_DATE_E.VisibleIndex = 2;
            // 
            // dlgSaveCSV
            // 
            this.dlgSaveCSV.DefaultExt = "csv";
            this.dlgSaveCSV.Filter = "*.csv|*.CSV";
            // 
            // dlgExportToNetmover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 215);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgExportToNetmover";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Küldés Netmover-be";
            this.Load += new System.EventHandler(this.dlgExportToNetmover_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridPlans;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPlans;
        private DevExpress.XtraGrid.Columns.GridColumn grcID;
        private DevExpress.XtraGrid.Columns.GridColumn grcPLN_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn grcPLN_DATE_B;
        private DevExpress.XtraGrid.Columns.GridColumn grcPLN_DATE_E;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lblvFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.SaveFileDialog dlgSaveCSV;
    }
}