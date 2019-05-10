using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SMcMaster;
using DevExpress.XtraNavBar;
using System.Collections;
using DevExpress.XtraTab;
using DevExpress.Utils;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors.Controls;
using PMapUI.Localize.Base;
using System.IO;
using PMapCore.Properties;
using PMapUI.Properties;

namespace PMapUI.Forms.Base
{
    public partial class BaseForm : XtraForm, IDisposable
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        protected void InitForm()
        {
            InitForm(TabOrderManager.TabScheme.AcrossFirst);
        }

        protected void InitForm(TabOrderManager.TabScheme p_tabscheme)
        {

            //továbbfejlesztés
            //SuspendLayout();
            //ScaleControls(this, NyilWConfig.ScreenFont, true);
            //ResumeLayout();

            //            if( p_tabscheme != TabOrderManager.TabScheme.None )

            PreviewLocalizer.Active = new DXPrintPreviewLocalizer(new StringReader(Resources.ppreviewloc_hu));
            GridLocalizer.Active = new DXGridLocalizer(new StringReader(Resources.gridloc_hu));
            Localizer.Active = new DXEditorLocalizer(new StringReader(Resources.editorloc_hu));

            tabSchemeProvider.SetTabScheme(this, p_tabscheme);
            this.SelectNextControl(null, true, true, true, true);


        }


        /// <summary>
        /// Van-e az ősök kozott ilyen
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static bool hasAncestor(Type p_t, Type p_ancestor)
        {
            if (p_t.BaseType == p_ancestor) return true;
            if (p_t.BaseType == null) return false;
            return hasAncestor(p_t.BaseType, p_ancestor);
        }

        /// <summary>
        /// Megj.:
        /// Label esetén az AutoSize=false
        /// Toolstrip esetén az AutoSize=false
        /// </summary>
        /// <param name="c"></param>
        /// 
        private const float DEF_FONTSIZE = 8.25f;
        public static void ScaleControls(Control c, Font p_font, bool p_scale)
        {
            if (c != null)
            {
                AppearanceObject.DefaultFont = p_font;

                if (hasAncestor(c.GetType(), typeof(XtraForm)) || hasAncestor(c.GetType(), typeof(Form)))
                {
                    if (p_scale)
                        c.Scale(new SizeF(p_font.Size / DEF_FONTSIZE, p_font.Size / DEF_FONTSIZE));
                }
                else if (c.GetType() == typeof(SplitContainer))
                {
                    SplitContainer sp = (SplitContainer)c;
                    if (sp.FixedPanel == FixedPanel.Panel1)
                        sp.SplitterDistance = (int)((float)(sp.SplitterDistance) * p_font.Size / DEF_FONTSIZE);
                    else
                        if (sp.FixedPanel == FixedPanel.Panel2)
                        {
                            int size = (sp.Orientation == Orientation.Vertical ? sp.Size.Width : sp.Size.Height);
                            sp.SplitterDistance = size - (int)((float)(size - sp.SplitterDistance) * p_font.Size / DEF_FONTSIZE);
                        }
                }
                else if (c.GetType() == typeof(XtraTabControl))
                {
                    XtraTabControl tb = (XtraTabControl)c;
                    foreach (XtraTabPage xtb in tb.TabPages)
                    {
                        FontStyle fs = xtb.Appearance.Header.Font.Style;
                        xtb.Appearance.Header.Font = new Font(p_font, fs);
                    }

                }
                else if (c.GetType() == typeof(NavBarControl))
                {
                    NavBarControl g = (NavBarControl)c;
                    IEnumerator enu = g.Appearance.GetEnumerator();
                    while (enu.MoveNext())
                    {
                        AppearanceObject app = (AppearanceObject)enu.Current;
                        FontStyle fs = app.Font.Style;
                        app.Font = new Font(p_font, fs);

                    }
                }

                else if (c.GetType() == typeof(ToolStrip))
                {
                    ToolStrip ts = (ToolStrip)c;
                    ts.Height = (int)((float)(ts.Size.Height) * p_font.Size / DEF_FONTSIZE);
                    Size s = ts.ImageScalingSize;
                    s.Width = (int)((float)(ts.ImageScalingSize.Width) * p_font.Size / DEF_FONTSIZE);
                    s.Height = (int)((float)(ts.ImageScalingSize.Height) * p_font.Size / DEF_FONTSIZE);
                    ts.ImageScalingSize = s;

                    foreach (ToolStripItem ti in ts.Items)
                    {

                        s = ti.Size;
                        s.Width = (int)((float)(ti.Size.Width) * p_font.Size / DEF_FONTSIZE);
                        s.Height = (int)((float)(ti.Size.Height) * p_font.Size / DEF_FONTSIZE);
                        ti.Size = s;

                        FontStyle fs = ti.Font.Style;
                        ti.Font = new Font(p_font, fs);
                    }

                }
                else
                {
                    FontStyle fs = c.Font.Style;
                    c.Font = new Font(p_font, fs);
                }


                for (int i = 0; i < c.Controls.Count; i++)
                {
                    ScaleControls(c.Controls[i], p_font, false);
                }

            }
        }
    }
}
