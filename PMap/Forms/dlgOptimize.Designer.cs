namespace PMapCore.Forms
{
    partial class dlgOptimize
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
            this.txtPLN_NAME = new System.Windows.Forms.TextBox();
            this.lblPLN_NAME = new System.Windows.Forms.Label();
            this.txtPLN_DATE_B = new System.Windows.Forms.TextBox();
            this.lblPLN_DATE_B = new System.Windows.Forms.Label();
            this.lblPLN_DATE_E = new System.Windows.Forms.Label();
            this.txtPLN_DATE_E = new System.Windows.Forms.TextBox();
            this.chkSecondary = new System.Windows.Forms.CheckBox();
            this.chkReplan = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtPLN_NAME, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPLN_NAME, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPLN_DATE_B, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPLN_DATE_B, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPLN_DATE_E, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtPLN_DATE_E, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkSecondary, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkReplan, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(405, 102);
            this.tableLayoutPanel1.TabIndex = 1004;
            // 
            // txtPLN_NAME
            // 
            this.txtPLN_NAME.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPLN_NAME.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPLN_NAME.Location = new System.Drawing.Point(205, 3);
            this.txtPLN_NAME.Name = "txtPLN_NAME";
            this.txtPLN_NAME.ReadOnly = true;
            this.txtPLN_NAME.Size = new System.Drawing.Size(197, 21);
            this.txtPLN_NAME.TabIndex = 16;
            this.txtPLN_NAME.TabStop = false;
            // 
            // lblPLN_NAME
            // 
            this.lblPLN_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPLN_NAME.AutoSize = true;
            this.lblPLN_NAME.Location = new System.Drawing.Point(3, 6);
            this.lblPLN_NAME.Name = "lblPLN_NAME";
            this.lblPLN_NAME.Size = new System.Drawing.Size(196, 13);
            this.lblPLN_NAME.TabIndex = 0;
            this.lblPLN_NAME.Text = "Terv neve";
            // 
            // txtPLN_DATE_B
            // 
            this.txtPLN_DATE_B.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPLN_DATE_B.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPLN_DATE_B.Location = new System.Drawing.Point(205, 28);
            this.txtPLN_DATE_B.Name = "txtPLN_DATE_B";
            this.txtPLN_DATE_B.ReadOnly = true;
            this.txtPLN_DATE_B.Size = new System.Drawing.Size(197, 21);
            this.txtPLN_DATE_B.TabIndex = 25;
            this.txtPLN_DATE_B.TabStop = false;
            // 
            // lblPLN_DATE_B
            // 
            this.lblPLN_DATE_B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPLN_DATE_B.AutoSize = true;
            this.lblPLN_DATE_B.Location = new System.Drawing.Point(3, 31);
            this.lblPLN_DATE_B.Name = "lblPLN_DATE_B";
            this.lblPLN_DATE_B.Size = new System.Drawing.Size(196, 13);
            this.lblPLN_DATE_B.TabIndex = 24;
            this.lblPLN_DATE_B.Text = "Terv kezdete";
            // 
            // lblPLN_DATE_E
            // 
            this.lblPLN_DATE_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPLN_DATE_E.AutoSize = true;
            this.lblPLN_DATE_E.Location = new System.Drawing.Point(3, 56);
            this.lblPLN_DATE_E.Name = "lblPLN_DATE_E";
            this.lblPLN_DATE_E.Size = new System.Drawing.Size(196, 13);
            this.lblPLN_DATE_E.TabIndex = 26;
            this.lblPLN_DATE_E.Text = "Terv vége";
            // 
            // txtPLN_DATE_E
            // 
            this.txtPLN_DATE_E.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPLN_DATE_E.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPLN_DATE_E.Location = new System.Drawing.Point(205, 53);
            this.txtPLN_DATE_E.Name = "txtPLN_DATE_E";
            this.txtPLN_DATE_E.ReadOnly = true;
            this.txtPLN_DATE_E.Size = new System.Drawing.Size(197, 21);
            this.txtPLN_DATE_E.TabIndex = 27;
            this.txtPLN_DATE_E.TabStop = false;
            // 
            // chkSecondary
            // 
            this.chkSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSecondary.AutoSize = true;
            this.chkSecondary.Location = new System.Drawing.Point(3, 80);
            this.chkSecondary.Name = "chkSecondary";
            this.chkSecondary.Size = new System.Drawing.Size(196, 17);
            this.chkSecondary.TabIndex = 28;
            this.chkSecondary.Text = "Másodlagos algoritmus";
            this.chkSecondary.UseVisualStyleBackColor = true;
            // 
            // chkReplan
            // 
            this.chkReplan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReplan.AutoSize = true;
            this.chkReplan.Location = new System.Drawing.Point(205, 80);
            this.chkReplan.Name = "chkReplan";
            this.chkReplan.Size = new System.Drawing.Size(197, 17);
            this.chkReplan.TabIndex = 29;
            this.chkReplan.Text = "Újratervezés";
            this.chkReplan.UseVisualStyleBackColor = true;
            // 
            // dlgOptimize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 141);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgOptimize";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Tervoptimalizálás";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dlgOptimize_KeyPress);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtPLN_NAME;
        private System.Windows.Forms.Label lblPLN_NAME;
        private System.Windows.Forms.TextBox txtPLN_DATE_B;
        private System.Windows.Forms.Label lblPLN_DATE_B;
        private System.Windows.Forms.Label lblPLN_DATE_E;
        private System.Windows.Forms.TextBox txtPLN_DATE_E;
        private System.Windows.Forms.CheckBox chkSecondary;
        private System.Windows.Forms.CheckBox chkReplan;
    }
}