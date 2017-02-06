namespace PMapTestApp
{
    partial class dlgSelWHSDEP
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
            this.gridWHSDEP = new DevExpress.XtraGrid.GridControl();
            this.gridViewWHSDEP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXCODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXADDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_XPOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOD_YPOS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tblDlgButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridWHSDEP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWHSDEP)).BeginInit();
            this.tblDlgButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridWHSDEP
            // 
            this.gridWHSDEP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWHSDEP.Location = new System.Drawing.Point(0, 0);
            this.gridWHSDEP.MainView = this.gridViewWHSDEP;
            this.gridWHSDEP.Name = "gridWHSDEP";
            this.gridWHSDEP.Size = new System.Drawing.Size(666, 263);
            this.gridWHSDEP.TabIndex = 1006;
            this.gridWHSDEP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewWHSDEP});
            this.gridWHSDEP.DoubleClick += new System.EventHandler(this.gridWHSDEP_DoubleClick);
            // 
            // gridViewWHSDEP
            // 
            this.gridViewWHSDEP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colXCODE,
            this.colXNAME,
            this.colXADDR,
            this.colNOD_ID,
            this.colNOD_XPOS,
            this.colNOD_YPOS});
            this.gridViewWHSDEP.GridControl = this.gridWHSDEP;
            this.gridViewWHSDEP.Name = "gridViewWHSDEP";
            this.gridViewWHSDEP.OptionsBehavior.Editable = false;
            this.gridViewWHSDEP.OptionsCustomization.AllowGroup = false;
            this.gridViewWHSDEP.OptionsCustomization.AllowSort = false;
            this.gridViewWHSDEP.OptionsMenu.EnableColumnMenu = false;
            this.gridViewWHSDEP.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewWHSDEP.OptionsView.ShowFooter = true;
            this.gridViewWHSDEP.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 45;
            // 
            // colXCODE
            // 
            this.colXCODE.Caption = "Kód";
            this.colXCODE.FieldName = "XCODE";
            this.colXCODE.Name = "colXCODE";
            this.colXCODE.Visible = true;
            this.colXCODE.VisibleIndex = 1;
            // 
            // colXNAME
            // 
            this.colXNAME.Caption = "Megnevezés";
            this.colXNAME.FieldName = "XNAME";
            this.colXNAME.Name = "colXNAME";
            this.colXNAME.OptionsColumn.AllowEdit = false;
            this.colXNAME.Visible = true;
            this.colXNAME.VisibleIndex = 2;
            this.colXNAME.Width = 153;
            // 
            // colXADDR
            // 
            this.colXADDR.Caption = "Cím";
            this.colXADDR.FieldName = "XADDR";
            this.colXADDR.Name = "colXADDR";
            this.colXADDR.OptionsColumn.AllowEdit = false;
            this.colXADDR.Visible = true;
            this.colXADDR.VisibleIndex = 3;
            this.colXADDR.Width = 166;
            // 
            // colNOD_ID
            // 
            this.colNOD_ID.Caption = "NOD_ID";
            this.colNOD_ID.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNOD_ID.FieldName = "NOD_ID";
            this.colNOD_ID.Name = "colNOD_ID";
            this.colNOD_ID.OptionsColumn.AllowEdit = false;
            this.colNOD_ID.Visible = true;
            this.colNOD_ID.VisibleIndex = 4;
            this.colNOD_ID.Width = 81;
            // 
            // colNOD_XPOS
            // 
            this.colNOD_XPOS.Caption = "Szélesség";
            this.colNOD_XPOS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNOD_XPOS.FieldName = "NOD_XPOS";
            this.colNOD_XPOS.Name = "colNOD_XPOS";
            this.colNOD_XPOS.OptionsColumn.AllowEdit = false;
            this.colNOD_XPOS.Visible = true;
            this.colNOD_XPOS.VisibleIndex = 5;
            this.colNOD_XPOS.Width = 79;
            // 
            // colNOD_YPOS
            // 
            this.colNOD_YPOS.Caption = "Hosszúság";
            this.colNOD_YPOS.FieldName = "NOD_YPOS";
            this.colNOD_YPOS.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNOD_YPOS.Name = "colNOD_YPOS";
            this.colNOD_YPOS.OptionsColumn.AllowEdit = false;
            this.colNOD_YPOS.Visible = true;
            this.colNOD_YPOS.VisibleIndex = 6;
            this.colNOD_YPOS.Width = 107;
            // 
            // tblDlgButtons
            // 
            this.tblDlgButtons.ColumnCount = 2;
            this.tblDlgButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDlgButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDlgButtons.Controls.Add(this.buttonOK, 0, 0);
            this.tblDlgButtons.Controls.Add(this.buttonCancel, 1, 0);
            this.tblDlgButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblDlgButtons.Location = new System.Drawing.Point(0, 263);
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
            this.tblDlgButtons.Size = new System.Drawing.Size(666, 40);
            this.tblDlgButtons.TabIndex = 1005;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonOK.Location = new System.Drawing.Point(240, 3);
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
            this.buttonCancel.Location = new System.Drawing.Point(336, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Mégsem";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dlgSelWHSDEP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 303);
            this.Controls.Add(this.gridWHSDEP);
            this.Controls.Add(this.tblDlgButtons);
            this.Name = "dlgSelWHSDEP";
            this.Text = "dlgSelWHSDEP";
            ((System.ComponentModel.ISupportInitialize)(this.gridWHSDEP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewWHSDEP)).EndInit();
            this.tblDlgButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridWHSDEP;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewWHSDEP;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colXNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_ID;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_XPOS;
        public System.Windows.Forms.TableLayoutPanel tblDlgButtons;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonCancel;
        private DevExpress.XtraGrid.Columns.GridColumn colXADDR;
        private DevExpress.XtraGrid.Columns.GridColumn colNOD_YPOS;
        private DevExpress.XtraGrid.Columns.GridColumn colXCODE;
    }
}