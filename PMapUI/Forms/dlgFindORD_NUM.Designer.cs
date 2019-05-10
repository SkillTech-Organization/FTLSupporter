namespace PMapUI.Forms
{
    partial class dlgFindORD_NUM
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
            this.lblORD_NUM = new System.Windows.Forms.Label();
            this.txtORD_NUM = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblORD_NUM, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtORD_NUM, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(369, 40);
            this.tableLayoutPanel1.TabIndex = 1001;
            // 
            // lblORD_NUM
            // 
            this.lblORD_NUM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblORD_NUM.AutoSize = true;
            this.lblORD_NUM.Location = new System.Drawing.Point(3, 13);
            this.lblORD_NUM.Name = "lblORD_NUM";
            this.lblORD_NUM.Size = new System.Drawing.Size(178, 13);
            this.lblORD_NUM.TabIndex = 0;
            this.lblORD_NUM.Text = "Keresett megrendelésszám:";
            // 
            // txtORD_NUM
            // 
            this.txtORD_NUM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORD_NUM.Location = new System.Drawing.Point(187, 9);
            this.txtORD_NUM.Name = "txtORD_NUM";
            this.txtORD_NUM.Size = new System.Drawing.Size(179, 21);
            this.txtORD_NUM.TabIndex = 1;
            // 
            // dlgFindORD_NUM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 79);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgFindORD_NUM";
            this.Text = "Megrendelésszám keresése";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblORD_NUM;
        public System.Windows.Forms.TextBox txtORD_NUM;
    }
}