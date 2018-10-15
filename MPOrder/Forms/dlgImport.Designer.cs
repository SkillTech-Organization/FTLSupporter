namespace MPOrder.Forms
{
    partial class dlgImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgImport));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFile = new System.Windows.Forms.Button();
            this.lblvFile = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtmShippingDateX = new System.Windows.Forms.DateTimePicker();
            this.lblCTDate = new System.Windows.Forms.Label();
            this.openCSV = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 70);
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(621, 35);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.BackColor = System.Drawing.Color.Yellow;
            this.btnFile.Image = ((System.Drawing.Image)(resources.GetObject("btnFile.Image")));
            this.btnFile.Location = new System.Drawing.Point(587, 4);
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
            this.lblvFile.Location = new System.Drawing.Point(103, 6);
            this.lblvFile.Name = "lblvFile";
            this.lblvFile.Size = new System.Drawing.Size(478, 22);
            this.lblvFile.TabIndex = 2;
            this.lblvFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFile
            // 
            this.lblFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(3, 11);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(94, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "Fájl";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dtmShippingDateX, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCTDate, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(621, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dtmShippingDateX
            // 
            this.dtmShippingDateX.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtmShippingDateX.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmShippingDateX.Location = new System.Drawing.Point(103, 7);
            this.dtmShippingDateX.Name = "dtmShippingDateX";
            this.dtmShippingDateX.Size = new System.Drawing.Size(171, 21);
            this.dtmShippingDateX.TabIndex = 12;
            // 
            // lblCTDate
            // 
            this.lblCTDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCTDate.AutoSize = true;
            this.lblCTDate.Location = new System.Drawing.Point(3, 11);
            this.lblCTDate.Name = "lblCTDate";
            this.lblCTDate.Size = new System.Drawing.Size(94, 13);
            this.lblCTDate.TabIndex = 0;
            this.lblCTDate.Text = "Szállítási dátum";
            // 
            // openCSV
            // 
            this.openCSV.CheckFileExists = false;
            this.openCSV.DefaultExt = "csv";
            this.openCSV.Filter = "*.csv|*.CSV";
            // 
            // dlgImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 109);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgImport";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "CSV import";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblCTDate;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.DateTimePicker dtmShippingDateX;
        private System.Windows.Forms.Label lblvFile;
        private System.Windows.Forms.OpenFileDialog openCSV;
        private System.Windows.Forms.Button btnFile;
    }
}