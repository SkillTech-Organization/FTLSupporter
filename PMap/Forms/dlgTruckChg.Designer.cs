namespace PMapCore.Forms
{
    partial class dlgTruckChg
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
            this.txTRK_NAME = new System.Windows.Forms.TextBox();
            this.lblWHS = new System.Windows.Forms.Label();
            this.lblTRK = new System.Windows.Forms.Label();
            this.cmbTruck = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColor = new PMapCore.Controls.ColorComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txTRK_NAME, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWHS, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTRK, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbTruck, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblColor, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbColor, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(472, 119);
            this.tableLayoutPanel1.TabIndex = 1003;
            // 
            // txTRK_NAME
            // 
            this.txTRK_NAME.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txTRK_NAME.BackColor = System.Drawing.Color.AliceBlue;
            this.txTRK_NAME.Location = new System.Drawing.Point(239, 9);
            this.txTRK_NAME.Name = "txTRK_NAME";
            this.txTRK_NAME.ReadOnly = true;
            this.txTRK_NAME.Size = new System.Drawing.Size(230, 21);
            this.txTRK_NAME.TabIndex = 16;
            this.txTRK_NAME.TabStop = false;
            // 
            // lblWHS
            // 
            this.lblWHS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWHS.AutoSize = true;
            this.lblWHS.Location = new System.Drawing.Point(3, 13);
            this.lblWHS.Name = "lblWHS";
            this.lblWHS.Size = new System.Drawing.Size(230, 13);
            this.lblWHS.TabIndex = 0;
            this.lblWHS.Text = "Kiválasztott jármű";
            // 
            // lblTRK
            // 
            this.lblTRK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTRK.AutoSize = true;
            this.lblTRK.Location = new System.Drawing.Point(3, 52);
            this.lblTRK.Name = "lblTRK";
            this.lblTRK.Size = new System.Drawing.Size(230, 13);
            this.lblTRK.TabIndex = 1;
            this.lblTRK.Text = "Jármű:";
            // 
            // cmbTruck
            // 
            this.cmbTruck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTruck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTruck.FormattingEnabled = true;
            this.cmbTruck.Location = new System.Drawing.Point(239, 48);
            this.cmbTruck.Name = "cmbTruck";
            this.cmbTruck.Size = new System.Drawing.Size(230, 21);
            this.cmbTruck.TabIndex = 18;
            this.cmbTruck.TextChanged += new System.EventHandler(this.cmbTruck_TextChanged);
            // 
            // lblColor
            // 
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(3, 92);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(230, 13);
            this.lblColor.TabIndex = 22;
            this.lblColor.Text = "Szín:";
            // 
            // cmbColor
            // 
            this.cmbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColor.Extended = false;
            this.cmbColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbColor.Location = new System.Drawing.Point(239, 87);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.SelectedColor = System.Drawing.Color.Black;
            this.cmbColor.Size = new System.Drawing.Size(230, 23);
            this.cmbColor.TabIndex = 23;
            // 
            // dlgTruckChg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 158);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgTruckChg";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Rendszámcsere";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txTRK_NAME;
        private System.Windows.Forms.Label lblWHS;
        private System.Windows.Forms.Label lblTRK;
        private System.Windows.Forms.ComboBox cmbTruck;
        private System.Windows.Forms.Label lblColor;
        private Controls.ColorComboBox cmbColor;
    }
}