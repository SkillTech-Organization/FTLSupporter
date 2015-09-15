namespace PMapTestApp
{
    partial class dlgSelPlan
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
            this.tblDlgButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.gridPLN = new DevExpress.XtraGrid.GridControl();
            this.gridViewPLN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPLN_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPLN_DATE_B = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPLN_DATE_E = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tblDlgButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPLN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPLN)).BeginInit();
            this.SuspendLayout();
            // 
            // tblDlgButtons
            // 
            this.tblDlgButtons.ColumnCount = 2;
            this.tblDlgButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDlgButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDlgButtons.Controls.Add(this.buttonOK, 0, 0);
            this.tblDlgButtons.Controls.Add(this.buttonCancel, 1, 0);
            this.tblDlgButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblDlgButtons.Location = new System.Drawing.Point(0, 230);
            this.tblDlgButtons.Name = "tblDlgButtons";
            this.tblDlgButtons.RowCount = 1;
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDlgButtons.Size = new System.Drawing.Size(587, 40);
            this.tblDlgButtons.TabIndex = 1003;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOK.Location = new System.Drawing.Point(200, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(90, 34);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(296, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Mégsem";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // gridPLN
            // 
            this.gridPLN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPLN.Location = new System.Drawing.Point(0, 0);
            this.gridPLN.MainView = this.gridViewPLN;
            this.gridPLN.Name = "gridPLN";
            this.gridPLN.Size = new System.Drawing.Size(587, 230);
            this.gridPLN.TabIndex = 1004;
            this.gridPLN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPLN});
            this.gridPLN.DoubleClick += new System.EventHandler(this.gridPLN_DoubleClick);
            // 
            // gridViewPLN
            // 
            this.gridViewPLN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colPLN_NAME,
            this.colPLN_DATE_B,
            this.colPLN_DATE_E});
            this.gridViewPLN.GridControl = this.gridPLN;
            this.gridViewPLN.Name = "gridViewPLN";
            this.gridViewPLN.OptionsBehavior.Editable = false;
            this.gridViewPLN.OptionsCustomization.AllowGroup = false;
            this.gridViewPLN.OptionsCustomization.AllowSort = false;
            this.gridViewPLN.OptionsMenu.EnableColumnMenu = false;
            this.gridViewPLN.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewPLN.OptionsView.ShowFooter = true;
            this.gridViewPLN.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 98;
            // 
            // colPLN_NAME
            // 
            this.colPLN_NAME.Caption = "Megnevezés";
            this.colPLN_NAME.FieldName = "PLN_NAME";
            this.colPLN_NAME.Name = "colPLN_NAME";
            this.colPLN_NAME.OptionsColumn.AllowEdit = false;
            this.colPLN_NAME.Visible = true;
            this.colPLN_NAME.VisibleIndex = 1;
            this.colPLN_NAME.Width = 248;
            // 
            // colPLN_DATE_B
            // 
            this.colPLN_DATE_B.Caption = "Kezdődátum";
            this.colPLN_DATE_B.DisplayFormat.FormatString = "d";
            this.colPLN_DATE_B.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPLN_DATE_B.FieldName = "PLN_DATE_B";
            this.colPLN_DATE_B.Name = "colPLN_DATE_B";
            this.colPLN_DATE_B.OptionsColumn.AllowEdit = false;
            this.colPLN_DATE_B.Visible = true;
            this.colPLN_DATE_B.VisibleIndex = 2;
            this.colPLN_DATE_B.Width = 109;
            // 
            // colPLN_DATE_E
            // 
            this.colPLN_DATE_E.Caption = "Végdátum";
            this.colPLN_DATE_E.DisplayFormat.FormatString = "d";
            this.colPLN_DATE_E.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPLN_DATE_E.FieldName = "PLN_DATE_E";
            this.colPLN_DATE_E.Name = "colPLN_DATE_E";
            this.colPLN_DATE_E.Visible = true;
            this.colPLN_DATE_E.VisibleIndex = 3;
            this.colPLN_DATE_E.Width = 111;
            // 
            // dlgSelPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 270);
            this.Controls.Add(this.gridPLN);
            this.Controls.Add(this.tblDlgButtons);
            this.Name = "dlgSelPlan";
            this.Text = "Terv kiválasztása";
            this.tblDlgButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPLN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPLN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tblDlgButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonCancel;
        private DevExpress.XtraGrid.GridControl gridPLN;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPLN;
        private DevExpress.XtraGrid.Columns.GridColumn colPLN_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn colPLN_DATE_B;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colPLN_DATE_E;
    }
}