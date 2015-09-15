namespace PMap.Forms
{
    partial class dlgNewPlan
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
            this.lblPLN_NAME = new System.Windows.Forms.Label();
            this.txtPLN_NAME = new System.Windows.Forms.TextBox();
            this.lblWHS = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.lblPLN_DATE_B = new System.Windows.Forms.Label();
            this.dtpPLN_DATE_B = new System.Windows.Forms.DateTimePicker();
            this.dtpPLN_DATE_E = new System.Windows.Forms.DateTimePicker();
            this.lblPLN_DATE_E = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblPLN_NAME, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPLN_NAME, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWHS, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbWarehouse, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPLN_DATE_B, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpPLN_DATE_B, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpPLN_DATE_E, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblPLN_DATE_E, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(394, 127);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblPLN_NAME
            // 
            this.lblPLN_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPLN_NAME.AutoSize = true;
            this.lblPLN_NAME.Location = new System.Drawing.Point(3, 9);
            this.lblPLN_NAME.Name = "lblPLN_NAME";
            this.lblPLN_NAME.Size = new System.Drawing.Size(114, 13);
            this.lblPLN_NAME.TabIndex = 0;
            this.lblPLN_NAME.Text = "Terv neve:";
            // 
            // txtPLN_NAME
            // 
            this.txtPLN_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPLN_NAME.BackColor = System.Drawing.Color.Wheat;
            this.txtPLN_NAME.Location = new System.Drawing.Point(123, 5);
            this.txtPLN_NAME.Name = "txtPLN_NAME";
            this.txtPLN_NAME.Size = new System.Drawing.Size(268, 21);
            this.txtPLN_NAME.TabIndex = 17;
            this.txtPLN_NAME.TabStop = false;
            // 
            // lblWHS
            // 
            this.lblWHS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWHS.AutoSize = true;
            this.lblWHS.Location = new System.Drawing.Point(3, 40);
            this.lblWHS.Name = "lblWHS";
            this.lblWHS.Size = new System.Drawing.Size(114, 13);
            this.lblWHS.TabIndex = 18;
            this.lblWHS.Text = "Raktár:";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWarehouse.BackColor = System.Drawing.Color.Wheat;
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(123, 36);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(268, 21);
            this.cmbWarehouse.TabIndex = 19;
            // 
            // lblPLN_DATE_B
            // 
            this.lblPLN_DATE_B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPLN_DATE_B.AutoSize = true;
            this.lblPLN_DATE_B.Location = new System.Drawing.Point(3, 71);
            this.lblPLN_DATE_B.Name = "lblPLN_DATE_B";
            this.lblPLN_DATE_B.Size = new System.Drawing.Size(114, 13);
            this.lblPLN_DATE_B.TabIndex = 20;
            this.lblPLN_DATE_B.Text = "Terv kezdete:";
            // 
            // dtpPLN_DATE_B
            // 
            this.dtpPLN_DATE_B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpPLN_DATE_B.CustomFormat = "yyyy.MM.dd HH:mm";
            this.dtpPLN_DATE_B.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPLN_DATE_B.Location = new System.Drawing.Point(123, 67);
            this.dtpPLN_DATE_B.Name = "dtpPLN_DATE_B";
            this.dtpPLN_DATE_B.ShowUpDown = true;
            this.dtpPLN_DATE_B.Size = new System.Drawing.Size(268, 21);
            this.dtpPLN_DATE_B.TabIndex = 21;
            // 
            // dtpPLN_DATE_E
            // 
            this.dtpPLN_DATE_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpPLN_DATE_E.CustomFormat = "yyyy.MM.dd HH:mm";
            this.dtpPLN_DATE_E.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPLN_DATE_E.Location = new System.Drawing.Point(123, 99);
            this.dtpPLN_DATE_E.Name = "dtpPLN_DATE_E";
            this.dtpPLN_DATE_E.ShowUpDown = true;
            this.dtpPLN_DATE_E.Size = new System.Drawing.Size(268, 21);
            this.dtpPLN_DATE_E.TabIndex = 23;
            // 
            // lblPLN_DATE_E
            // 
            this.lblPLN_DATE_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPLN_DATE_E.AutoSize = true;
            this.lblPLN_DATE_E.Location = new System.Drawing.Point(3, 103);
            this.lblPLN_DATE_E.Name = "lblPLN_DATE_E";
            this.lblPLN_DATE_E.Size = new System.Drawing.Size(114, 13);
            this.lblPLN_DATE_E.TabIndex = 22;
            this.lblPLN_DATE_E.Text = "Terv vége:";
            // 
            // dlgNewPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 166);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgNewPlan";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Új terv létrehozása";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPLN_NAME;
        private System.Windows.Forms.TextBox txtPLN_NAME;
        private System.Windows.Forms.Label lblWHS;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label lblPLN_DATE_B;
        private System.Windows.Forms.Label lblPLN_DATE_E;
        private System.Windows.Forms.DateTimePicker dtpPLN_DATE_B;
        private System.Windows.Forms.DateTimePicker dtpPLN_DATE_E;
    }
}