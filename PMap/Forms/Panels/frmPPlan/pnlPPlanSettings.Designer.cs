namespace PMapCore.Forms.Panels.frmPPlan
{
    partial class pnlPPlanSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlPPlanSettings));
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpMarkerTooltipMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbNever = new System.Windows.Forms.RadioButton();
            this.rdbOnMouseOver = new System.Windows.Forms.RadioButton();
            this.rdbAlways = new System.Windows.Forms.RadioButton();
            this.grpDepots = new System.Windows.Forms.GroupBox();
            this.chkShowAllOrdersInGrid = new System.Windows.Forms.CheckBox();
            this.chkZoomToSelectedUnPlanned = new System.Windows.Forms.CheckBox();
            this.chkZoomToSelectedTour = new System.Windows.Forms.CheckBox();
            this.chkShowUnplannedDepots = new System.Windows.Forms.CheckBox();
            this.chkShowPlannedDepots = new System.Windows.Forms.CheckBox();
            this.pZoom = new System.Windows.Forms.GroupBox();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.btnHideShowAllTours = new System.Windows.Forms.Button();
            this.tblSearchORD_NUM = new System.Windows.Forms.TableLayoutPanel();
            this.buttonFind = new System.Windows.Forms.Button();
            this.txtORD_NUM = new System.Windows.Forms.TextBox();
            this.lblORD_NUM = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblApproah = new System.Windows.Forms.Label();
            this.numTPArea = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpMarkerTooltipMode.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpDepots.SuspendLayout();
            this.pZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.tblSearchORD_NUM.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTPArea)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSettings.Controls.Add(this.tableLayoutPanel1);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(273, 562);
            this.pnlSettings.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpMarkerTooltipMode, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpDepots, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pZoom, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnHideShowAllTours, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tblSearchORD_NUM, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(271, 560);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grpMarkerTooltipMode
            // 
            this.grpMarkerTooltipMode.Controls.Add(this.tableLayoutPanel2);
            this.grpMarkerTooltipMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMarkerTooltipMode.Location = new System.Drawing.Point(3, 178);
            this.grpMarkerTooltipMode.Name = "grpMarkerTooltipMode";
            this.grpMarkerTooltipMode.Size = new System.Drawing.Size(265, 94);
            this.grpMarkerTooltipMode.TabIndex = 38;
            this.grpMarkerTooltipMode.TabStop = false;
            this.grpMarkerTooltipMode.Text = "Címke megjelenítés";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.rdbNever, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rdbOnMouseOver, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rdbAlways, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(259, 75);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // rdbNever
            // 
            this.rdbNever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbNever.AutoSize = true;
            this.rdbNever.Checked = true;
            this.rdbNever.Location = new System.Drawing.Point(3, 4);
            this.rdbNever.Name = "rdbNever";
            this.rdbNever.Size = new System.Drawing.Size(253, 17);
            this.rdbNever.TabIndex = 0;
            this.rdbNever.TabStop = true;
            this.rdbNever.Text = "Nincs ";
            this.rdbNever.UseVisualStyleBackColor = true;
            this.rdbNever.CheckedChanged += new System.EventHandler(this.rdbNever_CheckedChanged);
            // 
            // rdbOnMouseOver
            // 
            this.rdbOnMouseOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbOnMouseOver.AutoSize = true;
            this.rdbOnMouseOver.Location = new System.Drawing.Point(3, 29);
            this.rdbOnMouseOver.Name = "rdbOnMouseOver";
            this.rdbOnMouseOver.Size = new System.Drawing.Size(253, 17);
            this.rdbOnMouseOver.TabIndex = 1;
            this.rdbOnMouseOver.Text = "Egérpozícionállással";
            this.rdbOnMouseOver.UseVisualStyleBackColor = true;
            this.rdbOnMouseOver.CheckedChanged += new System.EventHandler(this.rdbOnMouseOver_CheckedChanged);
            // 
            // rdbAlways
            // 
            this.rdbAlways.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbAlways.AutoSize = true;
            this.rdbAlways.Location = new System.Drawing.Point(3, 54);
            this.rdbAlways.Name = "rdbAlways";
            this.rdbAlways.Size = new System.Drawing.Size(253, 17);
            this.rdbAlways.TabIndex = 2;
            this.rdbAlways.Text = "Állandó megjelenítés";
            this.rdbAlways.UseVisualStyleBackColor = true;
            this.rdbAlways.CheckedChanged += new System.EventHandler(this.rdbAlways_CheckedChanged);
            // 
            // grpDepots
            // 
            this.grpDepots.Controls.Add(this.chkShowAllOrdersInGrid);
            this.grpDepots.Controls.Add(this.chkZoomToSelectedUnPlanned);
            this.grpDepots.Controls.Add(this.chkZoomToSelectedTour);
            this.grpDepots.Controls.Add(this.chkShowUnplannedDepots);
            this.grpDepots.Controls.Add(this.chkShowPlannedDepots);
            this.grpDepots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDepots.Location = new System.Drawing.Point(3, 58);
            this.grpDepots.Name = "grpDepots";
            this.grpDepots.Size = new System.Drawing.Size(265, 114);
            this.grpDepots.TabIndex = 37;
            this.grpDepots.TabStop = false;
            this.grpDepots.Text = "Megjelenítés beállítások";
            // 
            // chkShowAllOrdersInGrid
            // 
            this.chkShowAllOrdersInGrid.AutoSize = true;
            this.chkShowAllOrdersInGrid.Checked = true;
            this.chkShowAllOrdersInGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowAllOrdersInGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShowAllOrdersInGrid.Location = new System.Drawing.Point(3, 84);
            this.chkShowAllOrdersInGrid.Name = "chkShowAllOrdersInGrid";
            this.chkShowAllOrdersInGrid.Size = new System.Drawing.Size(259, 17);
            this.chkShowAllOrdersInGrid.TabIndex = 4;
            this.chkShowAllOrdersInGrid.Text = "Összes megr.listában";
            this.chkShowAllOrdersInGrid.UseVisualStyleBackColor = true;
            this.chkShowAllOrdersInGrid.CheckedChanged += new System.EventHandler(this.chkAllOrders_CheckedChanged);
            // 
            // chkZoomToSelectedUnPlanned
            // 
            this.chkZoomToSelectedUnPlanned.AutoSize = true;
            this.chkZoomToSelectedUnPlanned.Checked = true;
            this.chkZoomToSelectedUnPlanned.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZoomToSelectedUnPlanned.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkZoomToSelectedUnPlanned.Location = new System.Drawing.Point(3, 67);
            this.chkZoomToSelectedUnPlanned.Name = "chkZoomToSelectedUnPlanned";
            this.chkZoomToSelectedUnPlanned.Size = new System.Drawing.Size(259, 17);
            this.chkZoomToSelectedUnPlanned.TabIndex = 3;
            this.chkZoomToSelectedUnPlanned.Text = "Kiszolgálatlan megr.középen";
            this.chkZoomToSelectedUnPlanned.UseVisualStyleBackColor = true;
            this.chkZoomToSelectedUnPlanned.CheckedChanged += new System.EventHandler(this.chkZoomToSelectedUnPlanned_CheckedChanged);
            // 
            // chkZoomToSelectedTour
            // 
            this.chkZoomToSelectedTour.AutoSize = true;
            this.chkZoomToSelectedTour.Checked = true;
            this.chkZoomToSelectedTour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZoomToSelectedTour.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkZoomToSelectedTour.Location = new System.Drawing.Point(3, 50);
            this.chkZoomToSelectedTour.Name = "chkZoomToSelectedTour";
            this.chkZoomToSelectedTour.Size = new System.Drawing.Size(259, 17);
            this.chkZoomToSelectedTour.TabIndex = 2;
            this.chkZoomToSelectedTour.Text = "Túrára nagyítás";
            this.chkZoomToSelectedTour.UseVisualStyleBackColor = true;
            this.chkZoomToSelectedTour.CheckedChanged += new System.EventHandler(this.chkZoomToSelectedTour_CheckedChanged);
            // 
            // chkShowUnplannedDepots
            // 
            this.chkShowUnplannedDepots.AutoSize = true;
            this.chkShowUnplannedDepots.Checked = true;
            this.chkShowUnplannedDepots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowUnplannedDepots.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShowUnplannedDepots.Location = new System.Drawing.Point(3, 33);
            this.chkShowUnplannedDepots.Name = "chkShowUnplannedDepots";
            this.chkShowUnplannedDepots.Size = new System.Drawing.Size(259, 17);
            this.chkShowUnplannedDepots.TabIndex = 1;
            this.chkShowUnplannedDepots.Text = "Kiszolgálatlan megrendelések";
            this.chkShowUnplannedDepots.UseVisualStyleBackColor = true;
            this.chkShowUnplannedDepots.CheckedChanged += new System.EventHandler(this.chkShowUnplannedDepots_CheckedChanged);
            // 
            // chkShowPlannedDepots
            // 
            this.chkShowPlannedDepots.AutoSize = true;
            this.chkShowPlannedDepots.Checked = true;
            this.chkShowPlannedDepots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPlannedDepots.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShowPlannedDepots.Location = new System.Drawing.Point(3, 16);
            this.chkShowPlannedDepots.Name = "chkShowPlannedDepots";
            this.chkShowPlannedDepots.Size = new System.Drawing.Size(259, 17);
            this.chkShowPlannedDepots.TabIndex = 0;
            this.chkShowPlannedDepots.Text = "Betervezett lerakók és raktár";
            this.chkShowPlannedDepots.UseVisualStyleBackColor = true;
            this.chkShowPlannedDepots.CheckedChanged += new System.EventHandler(this.chkShowPlannedDepots_CheckedChanged);
            // 
            // pZoom
            // 
            this.pZoom.Controls.Add(this.tbZoom);
            this.pZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pZoom.Location = new System.Drawing.Point(3, 3);
            this.pZoom.Name = "pZoom";
            this.pZoom.Size = new System.Drawing.Size(265, 49);
            this.pZoom.TabIndex = 32;
            this.pZoom.TabStop = false;
            this.pZoom.Text = "Nagyítás";
            // 
            // tbZoom
            // 
            this.tbZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbZoom.LargeChange = 1;
            this.tbZoom.Location = new System.Drawing.Point(3, 16);
            this.tbZoom.Maximum = 20;
            this.tbZoom.Minimum = 2;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(259, 30);
            this.tbZoom.TabIndex = 0;
            this.tbZoom.Value = 2;
            // 
            // btnHideShowAllTours
            // 
            this.btnHideShowAllTours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHideShowAllTours.Location = new System.Drawing.Point(3, 278);
            this.btnHideShowAllTours.Name = "btnHideShowAllTours";
            this.btnHideShowAllTours.Size = new System.Drawing.Size(265, 29);
            this.btnHideShowAllTours.TabIndex = 39;
            this.btnHideShowAllTours.Text = "Összes túra elrejtése";
            this.btnHideShowAllTours.UseVisualStyleBackColor = true;
            this.btnHideShowAllTours.Click += new System.EventHandler(this.btnHideShowAllTours_Click);
            // 
            // tblSearchORD_NUM
            // 
            this.tblSearchORD_NUM.ColumnCount = 3;
            this.tblSearchORD_NUM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblSearchORD_NUM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSearchORD_NUM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblSearchORD_NUM.Controls.Add(this.buttonFind, 0, 0);
            this.tblSearchORD_NUM.Controls.Add(this.txtORD_NUM, 0, 0);
            this.tblSearchORD_NUM.Controls.Add(this.lblORD_NUM, 0, 0);
            this.tblSearchORD_NUM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSearchORD_NUM.Location = new System.Drawing.Point(0, 310);
            this.tblSearchORD_NUM.Margin = new System.Windows.Forms.Padding(0);
            this.tblSearchORD_NUM.Name = "tblSearchORD_NUM";
            this.tblSearchORD_NUM.RowCount = 1;
            this.tblSearchORD_NUM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSearchORD_NUM.Size = new System.Drawing.Size(271, 35);
            this.tblSearchORD_NUM.TabIndex = 40;
            // 
            // buttonFind
            // 
            this.buttonFind.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
            this.buttonFind.Location = new System.Drawing.Point(224, 3);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(44, 29);
            this.buttonFind.TabIndex = 3;
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // txtORD_NUM
            // 
            this.txtORD_NUM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtORD_NUM.Location = new System.Drawing.Point(73, 7);
            this.txtORD_NUM.Name = "txtORD_NUM";
            this.txtORD_NUM.Size = new System.Drawing.Size(145, 20);
            this.txtORD_NUM.TabIndex = 2;
            // 
            // lblORD_NUM
            // 
            this.lblORD_NUM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblORD_NUM.AutoSize = true;
            this.lblORD_NUM.Location = new System.Drawing.Point(3, 11);
            this.lblORD_NUM.Name = "lblORD_NUM";
            this.lblORD_NUM.Size = new System.Drawing.Size(64, 13);
            this.lblORD_NUM.TabIndex = 1;
            this.lblORD_NUM.Text = "Megr.szám:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.43396F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.56604F));
            this.tableLayoutPanel3.Controls.Add(this.lblApproah, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.numTPArea, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 348);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(265, 36);
            this.tableLayoutPanel3.TabIndex = 41;
            // 
            // lblApproah
            // 
            this.lblApproah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApproah.AutoSize = true;
            this.lblApproah.Location = new System.Drawing.Point(3, 11);
            this.lblApproah.Name = "lblApproah";
            this.lblApproah.Size = new System.Drawing.Size(125, 13);
            this.lblApproah.TabIndex = 5;
            this.lblApproah.Text = "Túrapontkörzet";
            // 
            // numTPArea
            // 
            this.numTPArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numTPArea.Location = new System.Drawing.Point(134, 8);
            this.numTPArea.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTPArea.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numTPArea.Name = "numTPArea";
            this.numTPArea.Size = new System.Drawing.Size(128, 20);
            this.numTPArea.TabIndex = 4;
            this.numTPArea.ValueChanged += new System.EventHandler(this.numTPArea_ValueChanged);
            // 
            // pnlPPlanSettings
            // 
            this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 562);
            this.Controls.Add(this.pnlSettings);
            this.Name = "pnlPPlanSettings";
            this.Text = "Beállítások";
            this.pnlSettings.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpMarkerTooltipMode.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpDepots.ResumeLayout(false);
            this.grpDepots.PerformLayout();
            this.pZoom.ResumeLayout(false);
            this.pZoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.tblSearchORD_NUM.ResumeLayout(false);
            this.tblSearchORD_NUM.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTPArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox pZoom;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.GroupBox grpDepots;
        private System.Windows.Forms.CheckBox chkShowUnplannedDepots;
        private System.Windows.Forms.CheckBox chkShowPlannedDepots;
        private System.Windows.Forms.GroupBox grpMarkerTooltipMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton rdbNever;
        private System.Windows.Forms.RadioButton rdbOnMouseOver;
        private System.Windows.Forms.RadioButton rdbAlways;
        private System.Windows.Forms.Button btnHideShowAllTours;
        private System.Windows.Forms.CheckBox chkZoomToSelectedTour;
        private System.Windows.Forms.CheckBox chkZoomToSelectedUnPlanned;
        private System.Windows.Forms.TableLayoutPanel tblSearchORD_NUM;
        public System.Windows.Forms.TextBox txtORD_NUM;
        public System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.CheckBox chkShowAllOrdersInGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblORD_NUM;
        private System.Windows.Forms.Label lblApproah;
        public System.Windows.Forms.NumericUpDown numTPArea;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}