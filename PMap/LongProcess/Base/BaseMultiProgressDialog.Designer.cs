namespace PMap.LongProcess.Base
{
    partial class BaseMultiProgressDialog
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
            this.btnAbort = new System.Windows.Forms.Button();
            this.lstInfoText = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnAbort, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lstInfoText, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.48175F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.51825F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(299, 172);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAbort.Location = new System.Drawing.Point(103, 145);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(92, 23);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "Megszakít";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // lstInfoText
            // 
            this.lstInfoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInfoText.FormattingEnabled = true;
            this.lstInfoText.Location = new System.Drawing.Point(3, 3);
            this.lstInfoText.Name = "lstInfoText";
            this.lstInfoText.Size = new System.Drawing.Size(293, 135);
            this.lstInfoText.TabIndex = 2;
            // 
            // BaseMultiProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 172);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BaseMultiProgressDialog";
            this.Text = "BaseMultiProgressDialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.ListBox lstInfoText;
    }
}