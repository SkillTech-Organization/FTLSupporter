namespace PMap.Forms
{
    partial class frmPPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPPlan));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.dmPPlan = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewPlan = new System.Windows.Forms.ToolStripButton();
            this.cmbPlans = new System.Windows.Forms.ToolStripComboBox();
            this.btnDelPlan = new System.Windows.Forms.ToolStripButton();
            this.tbSepPlan = new System.Windows.Forms.ToolStripSeparator();
            this.btnFindORD_NUM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnToEditMode = new System.Windows.Forms.ToolStripButton();
            this.btnToViewMode = new System.Windows.Forms.ToolStripButton();
            this.btnDelTourPoint = new System.Windows.Forms.ToolStripButton();
            this.btnTourDetails = new System.Windows.Forms.ToolStripButton();
            this.btnOptimizeAll = new System.Windows.Forms.ToolStripButton();
            this.btnOptimizeTrk = new System.Windows.Forms.ToolStripButton();
            this.btnNewTour = new System.Windows.Forms.ToolStripButton();
            this.btnDelTour = new System.Windows.Forms.ToolStripButton();
            this.btnChgTruck = new System.Windows.Forms.ToolStripButton();
            this.btnTurnTour = new System.Windows.Forms.ToolStripButton();
            this.btnCalcDistances = new System.Windows.Forms.ToolStripButton();
            this.btnToCloud = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckMapOn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckMapOff = new System.Windows.Forms.ToolStripButton();
            this.btnSaveLayout = new System.Windows.Forms.ToolStripButton();
            this.btnRestoreLayout = new System.Windows.Forms.ToolStripButton();
            this.btnResetScreen = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dmPPlan)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dmPPlan
            // 
            this.dmPPlan.Form = this;
            this.dmPPlan.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // btnExit
            // 
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(23, 22);
            this.btnExit.Text = "toolStripButton1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnQuit,
            this.toolStripSeparator4,
            this.btnNewPlan,
            this.cmbPlans,
            this.btnDelPlan,
            this.tbSepPlan,
            this.btnFindORD_NUM,
            this.toolStripSeparator3,
            this.btnToEditMode,
            this.btnToViewMode,
            this.btnDelTourPoint,
            this.btnTourDetails,
            this.btnOptimizeAll,
            this.btnOptimizeTrk,
            this.btnNewTour,
            this.btnDelTour,
            this.btnChgTruck,
            this.btnTurnTour,
            this.btnCalcDistances,
            this.btnToCloud,
            this.toolStripSeparator1,
            this.btnCheckMapOn,
            this.btnCheckMapOff,
            this.toolStripSeparator2,
            this.btnSaveLayout,
            this.btnRestoreLayout,
            this.btnResetScreen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1016, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnQuit
            // 
            this.btnQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuit.Image = ((System.Drawing.Image)(resources.GetObject("btnQuit.Image")));
            this.btnQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(23, 22);
            this.btnQuit.Text = "Kilépés";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnNewPlan
            // 
            this.btnNewPlan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnNewPlan.Image")));
            this.btnNewPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPlan.Name = "btnNewPlan";
            this.btnNewPlan.Size = new System.Drawing.Size(23, 22);
            this.btnNewPlan.Text = "Új terv létrehozása";
            this.btnNewPlan.Click += new System.EventHandler(this.btnNewPlan_Click);
            // 
            // cmbPlans
            // 
            this.cmbPlans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlans.Name = "cmbPlans";
            this.cmbPlans.Size = new System.Drawing.Size(300, 25);
            this.cmbPlans.ToolTipText = "Terv megnyitása";
            this.cmbPlans.SelectedIndexChanged += new System.EventHandler(this.cmbPlans_SelectedIndexChanged);
            this.cmbPlans.Click += new System.EventHandler(this.cmbPlans_Click);
            // 
            // btnDelPlan
            // 
            this.btnDelPlan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnDelPlan.Image")));
            this.btnDelPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelPlan.Name = "btnDelPlan";
            this.btnDelPlan.Size = new System.Drawing.Size(23, 22);
            this.btnDelPlan.Text = "Terv törlése";
            this.btnDelPlan.Click += new System.EventHandler(this.btnDelPlan_Click);
            // 
            // tbSepPlan
            // 
            this.tbSepPlan.Name = "tbSepPlan";
            this.tbSepPlan.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFindORD_NUM
            // 
            this.btnFindORD_NUM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFindORD_NUM.Image = ((System.Drawing.Image)(resources.GetObject("btnFindORD_NUM.Image")));
            this.btnFindORD_NUM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindORD_NUM.Name = "btnFindORD_NUM";
            this.btnFindORD_NUM.Size = new System.Drawing.Size(23, 22);
            this.btnFindORD_NUM.Text = "Megrendelésszám keresése";
            this.btnFindORD_NUM.Click += new System.EventHandler(this.btnFindORD_NUM_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnToEditMode
            // 
            this.btnToEditMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToEditMode.Image = ((System.Drawing.Image)(resources.GetObject("btnToEditMode.Image")));
            this.btnToEditMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToEditMode.Name = "btnToEditMode";
            this.btnToEditMode.Size = new System.Drawing.Size(23, 22);
            this.btnToEditMode.Text = "Szerkesztés";
            this.btnToEditMode.Click += new System.EventHandler(this.btnEditMode_Click);
            // 
            // btnToViewMode
            // 
            this.btnToViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToViewMode.Image = ((System.Drawing.Image)(resources.GetObject("btnToViewMode.Image")));
            this.btnToViewMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToViewMode.Name = "btnToViewMode";
            this.btnToViewMode.Size = new System.Drawing.Size(23, 22);
            this.btnToViewMode.Text = "Áttekintés";
            this.btnToViewMode.Click += new System.EventHandler(this.btnViewMode_Click);
            // 
            // btnDelTourPoint
            // 
            this.btnDelTourPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelTourPoint.Image = ((System.Drawing.Image)(resources.GetObject("btnDelTourPoint.Image")));
            this.btnDelTourPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelTourPoint.Name = "btnDelTourPoint";
            this.btnDelTourPoint.Size = new System.Drawing.Size(23, 22);
            this.btnDelTourPoint.Text = "toolStripButton1";
            this.btnDelTourPoint.ToolTipText = "Kiválasztott túrapont törlése";
            this.btnDelTourPoint.Click += new System.EventHandler(this.btnDelTourPoint_Click);
            // 
            // btnTourDetails
            // 
            this.btnTourDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTourDetails.Image = ((System.Drawing.Image)(resources.GetObject("btnTourDetails.Image")));
            this.btnTourDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTourDetails.Name = "btnTourDetails";
            this.btnTourDetails.Size = new System.Drawing.Size(23, 22);
            this.btnTourDetails.Text = "Kiválasztott túra részletezése";
            this.btnTourDetails.Click += new System.EventHandler(this.btnTourDetails_Click);
            // 
            // btnOptimizeAll
            // 
            this.btnOptimizeAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOptimizeAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOptimizeAll.Image = ((System.Drawing.Image)(resources.GetObject("btnOptimizeAll.Image")));
            this.btnOptimizeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptimizeAll.Name = "btnOptimizeAll";
            this.btnOptimizeAll.Size = new System.Drawing.Size(23, 22);
            this.btnOptimizeAll.Text = "Teljes tervezés";
            this.btnOptimizeAll.Click += new System.EventHandler(this.btnOptimizeAll_Click);
            // 
            // btnOptimizeTrk
            // 
            this.btnOptimizeTrk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOptimizeTrk.Image = ((System.Drawing.Image)(resources.GetObject("btnOptimizeTrk.Image")));
            this.btnOptimizeTrk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptimizeTrk.Name = "btnOptimizeTrk";
            this.btnOptimizeTrk.Size = new System.Drawing.Size(23, 22);
            this.btnOptimizeTrk.Text = "Túraoptimailzálás egy járműre";
            this.btnOptimizeTrk.Click += new System.EventHandler(this.btnOptimizeTrk_Click);
            // 
            // btnNewTour
            // 
            this.btnNewTour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewTour.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTour.Image")));
            this.btnNewTour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewTour.Name = "btnNewTour";
            this.btnNewTour.Size = new System.Drawing.Size(23, 22);
            this.btnNewTour.Text = "Új túra létrehozása";
            this.btnNewTour.Click += new System.EventHandler(this.btnNewTour_Click);
            // 
            // btnDelTour
            // 
            this.btnDelTour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelTour.Image = ((System.Drawing.Image)(resources.GetObject("btnDelTour.Image")));
            this.btnDelTour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelTour.Name = "btnDelTour";
            this.btnDelTour.Size = new System.Drawing.Size(23, 22);
            this.btnDelTour.Text = "Túra törlése";
            this.btnDelTour.Click += new System.EventHandler(this.btnDelTour_Click);
            // 
            // btnChgTruck
            // 
            this.btnChgTruck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChgTruck.Image = ((System.Drawing.Image)(resources.GetObject("btnChgTruck.Image")));
            this.btnChgTruck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChgTruck.Name = "btnChgTruck";
            this.btnChgTruck.Size = new System.Drawing.Size(23, 22);
            this.btnChgTruck.Text = "Járműcsere";
            this.btnChgTruck.Click += new System.EventHandler(this.btnChgTruck_Click);
            // 
            // btnTurnTour
            // 
            this.btnTurnTour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTurnTour.Image = ((System.Drawing.Image)(resources.GetObject("btnTurnTour.Image")));
            this.btnTurnTour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTurnTour.Name = "btnTurnTour";
            this.btnTurnTour.Size = new System.Drawing.Size(23, 22);
            this.btnTurnTour.Text = "Túra megfordítása";
            this.btnTurnTour.Visible = false;
            this.btnTurnTour.Click += new System.EventHandler(this.btnTurnTour_Click);
            // 
            // btnCalcDistances
            // 
            this.btnCalcDistances.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCalcDistances.Image = ((System.Drawing.Image)(resources.GetObject("btnCalcDistances.Image")));
            this.btnCalcDistances.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCalcDistances.Name = "btnCalcDistances";
            this.btnCalcDistances.Size = new System.Drawing.Size(23, 22);
            this.btnCalcDistances.Text = "Hiányzó távolságok kiszámolása";
            this.btnCalcDistances.Click += new System.EventHandler(this.btnCalcDistances_Click);
            // 
            // btnToCloud
            // 
            this.btnToCloud.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToCloud.Image = ((System.Drawing.Image)(resources.GetObject("btnToCloud.Image")));
            this.btnToCloud.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToCloud.Name = "btnToCloud";
            this.btnToCloud.Size = new System.Drawing.Size(23, 22);
            this.btnToCloud.Text = "Túra feltöltése a felhőbe";
            this.btnToCloud.Click += new System.EventHandler(this.btnToCloud_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCheckMapOn
            // 
            this.btnCheckMapOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckMapOn.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckMapOn.Image")));
            this.btnCheckMapOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckMapOn.Name = "btnCheckMapOn";
            this.btnCheckMapOn.Size = new System.Drawing.Size(23, 22);
            this.btnCheckMapOn.Text = "toolStripButton1";
            this.btnCheckMapOn.ToolTipText = "Ellenőrző mód bekapcsolása";
            this.btnCheckMapOn.Click += new System.EventHandler(this.btnCheckMapOn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCheckMapOff
            // 
            this.btnCheckMapOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckMapOff.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckMapOff.Image")));
            this.btnCheckMapOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckMapOff.Name = "btnCheckMapOff";
            this.btnCheckMapOff.Size = new System.Drawing.Size(23, 22);
            this.btnCheckMapOff.Text = "toolStripButton1";
            this.btnCheckMapOff.ToolTipText = "Ellenőrző mód kikapcsolása";
            this.btnCheckMapOff.Visible = false;
            this.btnCheckMapOff.Click += new System.EventHandler(this.btnCheckMapOff_Click);
            // 
            // btnSaveLayout
            // 
            this.btnSaveLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveLayout.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveLayout.Image")));
            this.btnSaveLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveLayout.Name = "btnSaveLayout";
            this.btnSaveLayout.Size = new System.Drawing.Size(23, 22);
            this.btnSaveLayout.Text = "Képernyőbeállítások mentése";
            this.btnSaveLayout.Click += new System.EventHandler(this.btnSaveLayout_Click);
            // 
            // btnRestoreLayout
            // 
            this.btnRestoreLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRestoreLayout.Image = ((System.Drawing.Image)(resources.GetObject("btnRestoreLayout.Image")));
            this.btnRestoreLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRestoreLayout.Name = "btnRestoreLayout";
            this.btnRestoreLayout.Size = new System.Drawing.Size(23, 22);
            this.btnRestoreLayout.Text = "Képernyőbeállítások visszaállítása";
            this.btnRestoreLayout.Click += new System.EventHandler(this.btnRestoreLayout_Click);
            // 
            // btnResetScreen
            // 
            this.btnResetScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResetScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnResetScreen.Image")));
            this.btnResetScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResetScreen.Name = "btnResetScreen";
            this.btnResetScreen.Size = new System.Drawing.Size(23, 22);
            this.btnResetScreen.Text = "Képernyő alaphelyzetbe állítása";
            this.btnResetScreen.Click += new System.EventHandler(this.btnResetScreen_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dockPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 660);
            this.panel1.TabIndex = 4;
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.dockPanel.DockBottomPortion = 150D;
            this.dockPanel.DockLeftPortion = 200D;
            this.dockPanel.DockRightPortion = 200D;
            this.dockPanel.DockTopPortion = 150D;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.dockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.RightToLeftLayout = true;
            this.dockPanel.Size = new System.Drawing.Size(1016, 660);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel.Skin = dockPanelSkin1;
            this.dockPanel.TabIndex = 3;
            // 
            // frmPPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 685);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmPPlan";
            this.Text = "Tervezés";
            this.Activated += new System.EventHandler(this.frmPPlan_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPPlan_FormClosed);
            this.Load += new System.EventHandler(this.frmPPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dmPPlan)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dmPPlan;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.Panel panel1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveLayout;
        private System.Windows.Forms.ToolStripButton btnRestoreLayout;
        private System.Windows.Forms.ToolStripSeparator tbSepPlan;
        private System.Windows.Forms.ToolStripButton btnToEditMode;
        private System.Windows.Forms.ToolStripButton btnToViewMode;
        private System.Windows.Forms.ToolStripButton btnResetScreen;
        private System.Windows.Forms.ToolStripButton btnTourDetails;
        private System.Windows.Forms.ToolStripButton btnDelTourPoint;
        private System.Windows.Forms.ToolStripButton btnTurnTour;
        private System.Windows.Forms.ToolStripButton btnOptimizeTrk;
        private System.Windows.Forms.ToolStripButton btnNewTour;
        private System.Windows.Forms.ToolStripButton btnDelTour;
        private System.Windows.Forms.ToolStripButton btnChgTruck;
        private System.Windows.Forms.ToolStripButton btnFindORD_NUM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnOptimizeAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnNewPlan;
        private System.Windows.Forms.ToolStripComboBox cmbPlans;
        private System.Windows.Forms.ToolStripButton btnDelPlan;
        private System.Windows.Forms.ToolStripButton btnCalcDistances;
        private System.Windows.Forms.ToolStripButton btnToCloud;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCheckMapOn;
        private System.Windows.Forms.ToolStripButton btnCheckMapOff;
    }
}