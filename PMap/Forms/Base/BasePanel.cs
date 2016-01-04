﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.Properties;
using System.IO;
using PMap.Localize.Base;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using SMcMaster;
using WeifenLuo.WinFormsUI.Docking;



namespace PMap.Forms.Base
{
    public partial class BasePanel : DockContent
    {
 
        public event EventHandler<EventArgs> NotifyPanelChanged;


        public BasePanel()
        {
            InitializeComponent();
        }

        public void DoNotifyPanelChanged(EventArgs e)
        {
            if( this.NotifyPanelChanged != null)
                NotifyPanelChanged(this, e);
        }



        protected void InitPanelBase()
        {
            InitPanel(TabOrderManager.TabScheme.AcrossFirst);
        }

        protected void InitPanel(TabOrderManager.TabScheme p_tabscheme)
        {

            //továbbfejlesztés
            //SuspendLayout();
            //ScaleControls(this, NyilWConfig.ScreenFont, true);
            //ResumeLayout();

            //            if( p_tabscheme != TabOrderManager.TabScheme.None )

            PreviewLocalizer.Active = new DXPrintPreviewLocalizer(new StringReader(PMap.Properties.Resources.ppreviewloc_hu));
            GridLocalizer.Active = new DXGridLocalizer(new StringReader(PMap.Properties.Resources.gridloc_hu));
            Localizer.Active = new DXEditorLocalizer(new StringReader(PMap.Properties.Resources.editorloc_hu));

            tabSchemeProvider.SetTabScheme(this, p_tabscheme);
            this.SelectNextControl(this, true, true, true, true);
        }

        public virtual void RefreshPanel(EventArgs p_evt)
        {
        }

        public virtual void UpdateControls()
        {
        }

    }
}
