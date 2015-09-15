namespace PMap.Forms.Panels.frmPPlan
{
    partial class pnlPPlanEditor
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrLat = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrLng = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblToolTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblCurrLat,
            this.toolStripStatusLabel2,
            this.lblCurrLng,
            this.lblToolTip});
            this.statusStrip.Location = new System.Drawing.Point(0, 290);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(960, 24);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(26, 19);
            this.toolStripStatusLabel1.Text = "Lat:";
            // 
            // lblCurrLat
            // 
            this.lblCurrLat.AutoSize = false;
            this.lblCurrLat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblCurrLat.Name = "lblCurrLat";
            this.lblCurrLat.Size = new System.Drawing.Size(80, 19);
            this.lblCurrLat.Text = "toolStripStatusLabel2";
            this.lblCurrLat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(28, 19);
            this.toolStripStatusLabel2.Text = "Lng:";
            // 
            // lblCurrLng
            // 
            this.lblCurrLng.AutoSize = false;
            this.lblCurrLng.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblCurrLng.Name = "lblCurrLng";
            this.lblCurrLng.Size = new System.Drawing.Size(80, 19);
            this.lblCurrLng.Text = "toolStripStatusLabel3";
            // 
            // lblToolTip
            // 
            this.lblToolTip.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblToolTip.Name = "lblToolTip";
            this.lblToolTip.Size = new System.Drawing.Size(0, 19);
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
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
            this.gMapControl.Size = new System.Drawing.Size(960, 290);
            this.gMapControl.TabIndex = 6;
            this.gMapControl.Zoom = 0D;
            this.gMapControl.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gMapControl_OnMarkerEnter);
            this.gMapControl.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.gMapControl_OnMarkerLeave);
            this.gMapControl.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gMapControl_OnMapZoomChanged);
            this.gMapControl.Paint += new System.Windows.Forms.PaintEventHandler(this.gMapControl_Paint);
            this.gMapControl.DoubleClick += new System.EventHandler(this.gMapControl_DoubleClick);
            this.gMapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseDown);
            this.gMapControl.MouseEnter += new System.EventHandler(this.gMapControl_MouseEnter);
            this.gMapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseMove);
            this.gMapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseUp);
            // 
            // pnlPPlanEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 314);
            this.Controls.Add(this.gMapControl);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "pnlPPlanEditor";
            this.Text = "~~";
            this.Load += new System.EventHandler(this.pnlPPlanEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pnlPPlanEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pnlPPlanEditor_KeyUp);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrLat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrLng;
        private System.Windows.Forms.ToolStripStatusLabel lblToolTip;
        private GMap.NET.WindowsForms.GMapControl gMapControl;
    }
}