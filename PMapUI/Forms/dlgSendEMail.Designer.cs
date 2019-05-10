namespace PMapUI.Forms
{
    partial class dlgSendEMail
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
            this.lblWHS = new System.Windows.Forms.Label();
            this.txtORD_EMAIL = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.77273F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.22727F));
            this.tableLayoutPanel1.Controls.Add(this.txtORD_EMAIL, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWHS, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 51);
            this.tableLayoutPanel1.TabIndex = 1004;
            // 
            // lblWHS
            // 
            this.lblWHS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWHS.AutoSize = true;
            this.lblWHS.Location = new System.Drawing.Point(3, 19);
            this.lblWHS.Name = "lblWHS";
            this.lblWHS.Size = new System.Drawing.Size(84, 13);
            this.lblWHS.TabIndex = 0;
            this.lblWHS.Text = "E-mail címek";
            // 
            // txtORD_EMAIL
            // 
            this.txtORD_EMAIL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORD_EMAIL.BackColor = System.Drawing.Color.Wheat;
            this.txtORD_EMAIL.Location = new System.Drawing.Point(93, 15);
            this.txtORD_EMAIL.Name = "txtORD_EMAIL";
            this.txtORD_EMAIL.Size = new System.Drawing.Size(520, 21);
            this.txtORD_EMAIL.TabIndex = 18;
            this.txtORD_EMAIL.TabStop = false;
            // 
            // dlgSendEMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 90);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgSendEMail";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "E-mail küldés";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblWHS;
        private System.Windows.Forms.TextBox txtORD_EMAIL;
    }
}