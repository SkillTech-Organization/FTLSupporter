using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using DevExpress.Utils;
using PMapCore.Base.Printing;

namespace PMapUI.Printing.Base
{
    public class BaseGridPrinting : PrintableComponentLink
    {
        protected const string FONT_ARIAL = "Arial";

        protected int HeaderRowPos { get; set; }
        protected int FooterRowPos { get; set; }

        private GridControl m_grid;
        private string m_header1;
        private string m_header2;
        private string m_printingTimeFormat;
        private string m_printingCopyright;

        public BaseGridPrinting(GridControl p_grid, PaperKind p_paperKind, bool p_landscape, string p_header1, string p_header2, string p_printingCopyright, string p_printingTimeFormat, bool p_Createdoc)
            : base(new PrintingSystem())
        {
            m_grid = p_grid;
            m_header1 = p_header1;
            m_header2 = p_header2;
            m_printingTimeFormat = p_printingTimeFormat;
            m_printingCopyright = p_printingCopyright;

            this.Component = p_grid;

            //this.PaperKind = p_paperKind;

            this.PaperKind = p_paperKind;
            this.Landscape = p_landscape;

            //Fontkészlet beállítása
            BaseView gw = p_grid.DefaultView;
            IEnumerator enu = gw.Appearance.GetEnumerator();
            Dictionary<string, Font> dicOriFonts = new Dictionary<string, Font>();
            while (enu.MoveNext())
            {
                AppearanceObject app = (AppearanceObject)enu.Current;
                dicOriFonts.Add(app.Name, app.Font);
                FontStyle fs = app.Font.Style;
                app.Font = new Font(FONT_ARIAL, 8, fs);
            }
            if (p_Createdoc)
                CreateDocument();

            //Fontkészlet helyreállítása
            IDictionaryEnumerator e = dicOriFonts.GetEnumerator();
            while (e.MoveNext())
            {
                AppearanceObject app = gw.Appearance.GetAppearance(e.Key.ToString());
                FontStyle fs = app.Font.Style;
                app.Font = new Font((Font)e.Value, fs);
            }
        }
        protected override void CreateDetail(BrickGraphics graph)
        {
            base.CreateDetail(graph);
        }


        protected override void CreateMarginalHeader(BrickGraphics graph)
        {
            HeaderRowPos = PrintingCommon.CreateMarginalHeader(graph, m_header1, m_header2);
        }

        protected override void CreateMarginalFooter(BrickGraphics p_graph)
        {
            FooterRowPos = PrintingCommon.CreateMarginalFooter(p_graph, m_printingTimeFormat, m_printingCopyright);
        }


        //egyenlőre nem írjuk felül....
        protected override void CreateDetailHeader(BrickGraphics graph)
        {
            
            base.CreateDetailHeader(graph);
        }

    }
}
