namespace PMapUI.Forms
{
    partial class dlgAddOpenClose
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblOrdName = new System.Windows.Forms.Label();
            this.lblOpen = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.dtpOpen = new System.Windows.Forms.DateTimePicker();
            this.dtpClose = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.85827F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.14173F));
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOrdName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOpen, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblClose, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpOpen, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpClose, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(381, 133);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.AliceBlue;
            this.txtName.Location = new System.Drawing.Point(132, 11);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(246, 21);
            this.txtName.TabIndex = 17;
            this.txtName.TabStop = false;
            // 
            // lblOrdName
            // 
            this.lblOrdName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrdName.AutoSize = true;
            this.lblOrdName.Location = new System.Drawing.Point(3, 15);
            this.lblOrdName.Name = "lblOrdName";
            this.lblOrdName.Size = new System.Drawing.Size(123, 13);
            this.lblOrdName.TabIndex = 0;
            this.lblOrdName.Text = "Megnevezés";
            // 
            // lblOpen
            // 
            this.lblOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOpen.AutoSize = true;
            this.lblOpen.Location = new System.Drawing.Point(3, 59);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(123, 13);
            this.lblOpen.TabIndex = 1;
            this.lblOpen.Text = "Nyitva tartás kezdete";
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(3, 104);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(123, 13);
            this.lblClose.TabIndex = 2;
            this.lblClose.Text = "Nyitva tartás vége";
            // 
            // dtpOpen
            // 
            this.dtpOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpOpen.CustomFormat = "HH:mm";
            this.dtpOpen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOpen.Location = new System.Drawing.Point(132, 55);
            this.dtpOpen.Name = "dtpOpen";
            this.dtpOpen.ShowUpDown = true;
            this.dtpOpen.Size = new System.Drawing.Size(246, 21);
            this.dtpOpen.TabIndex = 18;
            // 
            // dtpClose
            // 
            this.dtpClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpClose.CustomFormat = "HH:mm";
            this.dtpClose.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpClose.Location = new System.Drawing.Point(132, 100);
            this.dtpClose.Name = "dtpClose";
            this.dtpClose.ShowUpDown = true;
            this.dtpClose.Size = new System.Drawing.Size(246, 21);
            this.dtpClose.TabIndex = 19;
            // 
            // dlgAddOpenClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 172);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dlgAddOpenClose";
            this.tabSchemeProvider.SetTabScheme(this, SMcMaster.TabOrderManager.TabScheme.AcrossFirst);
            this.Text = "Nyitva tartás módosítása";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblOrdName;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DateTimePicker dtpOpen;
        private System.Windows.Forms.DateTimePicker dtpClose;
    }
}