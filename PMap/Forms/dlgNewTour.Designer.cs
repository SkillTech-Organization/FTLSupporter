namespace PMap.Forms
{
    partial class dlgNewTour
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txWHS_NAME = new System.Windows.Forms.TextBox();
            this.lblWHS = new System.Windows.Forms.Label();
            this.lblTRK = new System.Windows.Forms.Label();
            this.cmbTruck = new System.Windows.Forms.ComboBox();
            this.lblPTP_SRVTIME_UNLOAD = new System.Windows.Forms.Label();
            this.frcSrvTime = new FrcControls.FrcNumericTextBox();
            this.dtpWhsE = new System.Windows.Forms.DateTimePicker();
            this.lblEndDateTime = new System.Windows.Forms.Label();
            this.lblServTime = new System.Windows.Forms.Label();
            this.txtSrvTime = new System.Windows.Forms.TextBox();
            this.lblStartDateTime = new System.Windows.Forms.Label();
            this.dtpWhsS = new System.Windows.Forms.DateTimePicker();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColor = new PMap.Controls.ColorComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txWHS_NAME, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWHS, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTRK, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbTruck, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPTP_SRVTIME_UNLOAD, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.frcSrvTime, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.dtpWhsE, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblEndDateTime, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblServTime, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtSrvTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblStartDateTime, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dtpWhsS, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblColor, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbColor, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(469, 185);
            this.tableLayoutPanel1.TabIndex = 1002;
            // 
            // txWHS_NAME
            // 
            this.txWHS_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txWHS_NAME.BackColor = System.Drawing.Color.AliceBlue;
            this.txWHS_NAME.Location = new System.Drawing.Point(153, 3);
            this.txWHS_NAME.Name = "txWHS_NAME";
            this.txWHS_NAME.ReadOnly = true;
            this.txWHS_NAME.Size = new System.Drawing.Size(313, 21);
            this.txWHS_NAME.TabIndex = 16;
            this.txWHS_NAME.TabStop = false;
            // 
            // lblWHS
            // 
            this.lblWHS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWHS.AutoSize = true;
            this.lblWHS.Location = new System.Drawing.Point(3, 6);
            this.lblWHS.Name = "lblWHS";
            this.lblWHS.Size = new System.Drawing.Size(144, 13);
            this.lblWHS.TabIndex = 0;
            this.lblWHS.Text = "Raktár:";
            // 
            // lblTRK
            // 
            this.lblTRK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTRK.AutoSize = true;
            this.lblTRK.Location = new System.Drawing.Point(3, 32);
            this.lblTRK.Name = "lblTRK";
            this.lblTRK.Size = new System.Drawing.Size(144, 13);
            this.lblTRK.TabIndex = 1;
            this.lblTRK.Text = "Jármű:";
            // 
            // cmbTruck
            // 
            this.cmbTruck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTruck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTruck.FormattingEnabled = true;
            this.cmbTruck.Location = new System.Drawing.Point(153, 29);
            this.cmbTruck.Name = "cmbTruck";
            this.cmbTruck.Size = new System.Drawing.Size(313, 21);
            this.cmbTruck.TabIndex = 18;
            this.cmbTruck.TextChanged += new System.EventHandler(this.cmbTruck_TextChanged);
            // 
            // lblPTP_SRVTIME_UNLOAD
            // 
            this.lblPTP_SRVTIME_UNLOAD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPTP_SRVTIME_UNLOAD.AutoSize = true;
            this.lblPTP_SRVTIME_UNLOAD.Location = new System.Drawing.Point(3, 164);
            this.lblPTP_SRVTIME_UNLOAD.Name = "lblPTP_SRVTIME_UNLOAD";
            this.lblPTP_SRVTIME_UNLOAD.Size = new System.Drawing.Size(144, 13);
            this.lblPTP_SRVTIME_UNLOAD.TabIndex = 5;
            this.lblPTP_SRVTIME_UNLOAD.Text = "Lerakás kiszolgálási idő:";
            // 
            // frcSrvTime
            // 
            this.frcSrvTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.frcSrvTime.Digits = 10;
            this.frcSrvTime.Location = new System.Drawing.Point(153, 160);
            this.frcSrvTime.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.frcSrvTime.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.frcSrvTime.Name = "frcSrvTime";
            this.frcSrvTime.Precision = 2;
            this.frcSrvTime.Size = new System.Drawing.Size(313, 21);
            this.frcSrvTime.TabIndex = 21;
            this.frcSrvTime.Text = "0,00";
            this.frcSrvTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.frcSrvTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // dtpWhsE
            // 
            this.dtpWhsE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpWhsE.CustomFormat = "yyyy.MM.dd HH:mm";
            this.dtpWhsE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWhsE.Location = new System.Drawing.Point(153, 133);
            this.dtpWhsE.Name = "dtpWhsE";
            this.dtpWhsE.Size = new System.Drawing.Size(313, 21);
            this.dtpWhsE.TabIndex = 20;
            // 
            // lblEndDateTime
            // 
            this.lblEndDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEndDateTime.AutoSize = true;
            this.lblEndDateTime.Location = new System.Drawing.Point(3, 136);
            this.lblEndDateTime.Name = "lblEndDateTime";
            this.lblEndDateTime.Size = new System.Drawing.Size(144, 13);
            this.lblEndDateTime.TabIndex = 4;
            this.lblEndDateTime.Text = "Kilépés a raktárból:";
            // 
            // lblServTime
            // 
            this.lblServTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServTime.AutoSize = true;
            this.lblServTime.Location = new System.Drawing.Point(3, 110);
            this.lblServTime.Name = "lblServTime";
            this.lblServTime.Size = new System.Drawing.Size(144, 13);
            this.lblServTime.TabIndex = 3;
            this.lblServTime.Text = "Kiszolgálási idő:";
            // 
            // txtSrvTime
            // 
            this.txtSrvTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSrvTime.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSrvTime.Location = new System.Drawing.Point(153, 107);
            this.txtSrvTime.Name = "txtSrvTime";
            this.txtSrvTime.ReadOnly = true;
            this.txtSrvTime.Size = new System.Drawing.Size(313, 21);
            this.txtSrvTime.TabIndex = 17;
            this.txtSrvTime.TabStop = false;
            this.txtSrvTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblStartDateTime
            // 
            this.lblStartDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStartDateTime.AutoSize = true;
            this.lblStartDateTime.Location = new System.Drawing.Point(3, 84);
            this.lblStartDateTime.Name = "lblStartDateTime";
            this.lblStartDateTime.Size = new System.Drawing.Size(144, 13);
            this.lblStartDateTime.TabIndex = 2;
            this.lblStartDateTime.Text = "Belépés a raktárba:";
            // 
            // dtpWhsS
            // 
            this.dtpWhsS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpWhsS.CustomFormat = "yyyy.MM.dd HH:mm";
            this.dtpWhsS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWhsS.Location = new System.Drawing.Point(153, 81);
            this.dtpWhsS.Name = "dtpWhsS";
            this.dtpWhsS.ShowUpDown = true;
            this.dtpWhsS.Size = new System.Drawing.Size(313, 21);
            this.dtpWhsS.TabIndex = 19;
            this.dtpWhsS.ValueChanged += new System.EventHandler(this.dtpWhsS_ValueChanged);
            // 
            // lblColor
            // 
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(3, 58);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(144, 13);
            this.lblColor.TabIndex = 22;
            this.lblColor.Text = "Szín";
            // 
            // cmbColor
            // 
            this.cmbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColor.Extended = false;
            this.cmbColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbColor.Location = new System.Drawing.Point(153, 55);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.SelectedColor = System.Drawing.Color.Black;
            this.cmbColor.Size = new System.Drawing.Size(313, 20);
            this.cmbColor.TabIndex = 23;
            // 
            // dlgNewTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 224);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgNewTour";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Új túra";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblWHS;
        private System.Windows.Forms.Label lblTRK;
        private System.Windows.Forms.Label lblStartDateTime;
        private System.Windows.Forms.Label lblServTime;
        private System.Windows.Forms.Label lblEndDateTime;
        private System.Windows.Forms.Label lblPTP_SRVTIME_UNLOAD;
        private System.Windows.Forms.TextBox txWHS_NAME;
        private System.Windows.Forms.TextBox txtSrvTime;
        private System.Windows.Forms.DateTimePicker dtpWhsE;
        private System.Windows.Forms.ComboBox cmbTruck;
        private System.Windows.Forms.DateTimePicker dtpWhsS;
        private FrcControls.FrcNumericTextBox frcSrvTime;
        private System.Windows.Forms.Label lblColor;
        private Controls.ColorComboBox cmbColor;
    }
}