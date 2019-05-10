using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
namespace PMapUI.Forms
{
    partial class frmPMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPMap));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpCurrPos = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurrLng = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrLat = new System.Windows.Forms.Label();
            this.pZoom = new System.Windows.Forms.GroupBox();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.grpDepots = new System.Windows.Forms.GroupBox();
            this.chkShowDepots = new System.Windows.Forms.CheckBox();
            this.chkShowRouteDepots = new System.Windows.Forms.CheckBox();
            this.grpMarkerTooltipMode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbNever = new System.Windows.Forms.RadioButton();
            this.rdbOnMouseOver = new System.Windows.Forms.RadioButton();
            this.rdbAlways = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.grpCurrPos.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.grpDepots.SuspendLayout();
            this.grpMarkerTooltipMode.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gMapControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(862, 582);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 2;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(435, 580);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 0D;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.grpCurrPos, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pZoom, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpDepots, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpMarkerTooltipMode, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 580);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.96875F));
            this.tableLayoutPanel9.Controls.Add(this.btnOK, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.btnCancel, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 508);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(411, 69);
            this.tableLayoutPanel9.TabIndex = 35;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(3, 39);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(405, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(405, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Vissza";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpCurrPos
            // 
            this.grpCurrPos.Controls.Add(this.tableLayoutPanel3);
            this.grpCurrPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCurrPos.Location = new System.Drawing.Point(3, 3);
            this.grpCurrPos.Name = "grpCurrPos";
            this.grpCurrPos.Size = new System.Drawing.Size(411, 69);
            this.grpCurrPos.TabIndex = 2;
            this.grpCurrPos.TabStop = false;
            this.grpCurrPos.Text = "Aktuális pozíció";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.68217F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.31783F));
            this.tableLayoutPanel3.Controls.Add(this.lblCurrLng, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblCurrLat, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(405, 49);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblCurrLng
            // 
            this.lblCurrLng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrLng.AutoSize = true;
            this.lblCurrLng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrLng.Location = new System.Drawing.Point(119, 29);
            this.lblCurrLng.Name = "lblCurrLng";
            this.lblCurrLng.Size = new System.Drawing.Size(283, 15);
            this.lblCurrLng.TabIndex = 3;
            this.lblCurrLng.Text = "lblCurrLng";
            this.lblCurrLng.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lat:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lng:";
            // 
            // lblCurrLat
            // 
            this.lblCurrLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrLat.AutoSize = true;
            this.lblCurrLat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrLat.Location = new System.Drawing.Point(119, 4);
            this.lblCurrLat.Name = "lblCurrLat";
            this.lblCurrLat.Size = new System.Drawing.Size(283, 15);
            this.lblCurrLat.TabIndex = 2;
            this.lblCurrLat.Text = "lblCurrLat";
            this.lblCurrLat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pZoom
            // 
            this.pZoom.Controls.Add(this.tbZoom);
            this.pZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pZoom.Location = new System.Drawing.Point(3, 78);
            this.pZoom.Name = "pZoom";
            this.pZoom.Size = new System.Drawing.Size(411, 69);
            this.pZoom.TabIndex = 31;
            this.pZoom.TabStop = false;
            this.pZoom.Text = "Nagyítás";
            // 
            // tbZoom
            // 
            this.tbZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbZoom.LargeChange = 1;
            this.tbZoom.Location = new System.Drawing.Point(3, 17);
            this.tbZoom.Maximum = 22;
            this.tbZoom.Minimum = 1;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(405, 49);
            this.tbZoom.TabIndex = 0;
            this.tbZoom.Value = 2;
            this.tbZoom.ValueChanged += new System.EventHandler(this.tbZoom_ValueChanged);
            // 
            // grpDepots
            // 
            this.grpDepots.Controls.Add(this.chkShowDepots);
            this.grpDepots.Controls.Add(this.chkShowRouteDepots);
            this.grpDepots.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDepots.Location = new System.Drawing.Point(3, 153);
            this.grpDepots.Name = "grpDepots";
            this.grpDepots.Size = new System.Drawing.Size(411, 52);
            this.grpDepots.TabIndex = 36;
            this.grpDepots.TabStop = false;
            this.grpDepots.Text = "Helyek megjelenítése";
            // 
            // chkShowDepots
            // 
            this.chkShowDepots.AutoSize = true;
            this.chkShowDepots.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShowDepots.Location = new System.Drawing.Point(3, 34);
            this.chkShowDepots.Name = "chkShowDepots";
            this.chkShowDepots.Size = new System.Drawing.Size(405, 17);
            this.chkShowDepots.TabIndex = 1;
            this.chkShowDepots.Text = "Lerakók";
            this.chkShowDepots.UseVisualStyleBackColor = true;
            this.chkShowDepots.CheckedChanged += new System.EventHandler(this.chkShowDepots_CheckedChanged);
            // 
            // chkShowRouteDepots
            // 
            this.chkShowRouteDepots.AutoSize = true;
            this.chkShowRouteDepots.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShowRouteDepots.Location = new System.Drawing.Point(3, 17);
            this.chkShowRouteDepots.Name = "chkShowRouteDepots";
            this.chkShowRouteDepots.Size = new System.Drawing.Size(405, 17);
            this.chkShowRouteDepots.TabIndex = 0;
            this.chkShowRouteDepots.Text = "Útvonal";
            this.chkShowRouteDepots.UseVisualStyleBackColor = true;
            this.chkShowRouteDepots.CheckedChanged += new System.EventHandler(this.chkShowRouteDepots_CheckedChanged);
            // 
            // grpMarkerTooltipMode
            // 
            this.grpMarkerTooltipMode.Controls.Add(this.tableLayoutPanel2);
            this.grpMarkerTooltipMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMarkerTooltipMode.Location = new System.Drawing.Point(3, 211);
            this.grpMarkerTooltipMode.Name = "grpMarkerTooltipMode";
            this.grpMarkerTooltipMode.Size = new System.Drawing.Size(411, 94);
            this.grpMarkerTooltipMode.TabIndex = 34;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(405, 74);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // rdbNever
            // 
            this.rdbNever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbNever.AutoSize = true;
            this.rdbNever.Location = new System.Drawing.Point(3, 3);
            this.rdbNever.Name = "rdbNever";
            this.rdbNever.Size = new System.Drawing.Size(399, 17);
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
            this.rdbOnMouseOver.Location = new System.Drawing.Point(3, 27);
            this.rdbOnMouseOver.Name = "rdbOnMouseOver";
            this.rdbOnMouseOver.Size = new System.Drawing.Size(399, 17);
            this.rdbOnMouseOver.TabIndex = 1;
            this.rdbOnMouseOver.TabStop = true;
            this.rdbOnMouseOver.Text = "Egérpozícionállással";
            this.rdbOnMouseOver.UseVisualStyleBackColor = true;
            this.rdbOnMouseOver.CheckedChanged += new System.EventHandler(this.rdbOnMouseOver_CheckedChanged);
            // 
            // rdbAlways
            // 
            this.rdbAlways.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbAlways.AutoSize = true;
            this.rdbAlways.Location = new System.Drawing.Point(3, 52);
            this.rdbAlways.Name = "rdbAlways";
            this.rdbAlways.Size = new System.Drawing.Size(399, 17);
            this.rdbAlways.TabIndex = 2;
            this.rdbAlways.TabStop = true;
            this.rdbAlways.Text = "Állandó megjelenítés";
            this.rdbAlways.UseVisualStyleBackColor = true;
            this.rdbAlways.CheckedChanged += new System.EventHandler(this.rdbAlways_CheckedChanged);
            // 
            // frmPMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 582);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Térképi kiávlasztás, megjelenítés";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.grpCurrPos.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.pZoom.ResumeLayout(false);
            this.pZoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.grpDepots.ResumeLayout(false);
            this.grpDepots.PerformLayout();
            this.grpMarkerTooltipMode.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpCurrPos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblCurrLng;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurrLat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox pZoom;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.GroupBox grpDepots;
        private System.Windows.Forms.CheckBox chkShowDepots;
        private System.Windows.Forms.CheckBox chkShowRouteDepots;
        private System.Windows.Forms.GroupBox grpMarkerTooltipMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton rdbNever;
        private System.Windows.Forms.RadioButton rdbOnMouseOver;
        private System.Windows.Forms.RadioButton rdbAlways;
        private GMapControl gMapControl;
    }
}

