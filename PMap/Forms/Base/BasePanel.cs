using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.Properties;
using System.IO;
using PMapCore.Localize.Base;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using SMcMaster;
using WeifenLuo.WinFormsUI.Docking;



namespace PMapCore.Forms.Base
{
    public partial class BasePanel : DockContent
    {
        
        public event EventHandler<EventArgs> NotifyDataChanged;


        public BasePanel()
        {
            InitializeComponent();
        }

        public void DoNotifyDataChanged(EventArgs e)
        {
            if( this.NotifyDataChanged != null)
                NotifyDataChanged(this, e);
        }

        protected void InitPanel()
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

            PreviewLocalizer.Active = new DXPrintPreviewLocalizer(new StringReader(PMapCore.Properties.Resources.ppreviewloc_hu));
            GridLocalizer.Active = new DXGridLocalizer(new StringReader(PMapCore.Properties.Resources.gridloc_hu));
            Localizer.Active = new DXEditorLocalizer(new StringReader(PMapCore.Properties.Resources.editorloc_hu));

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
