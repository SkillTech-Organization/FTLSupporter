using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using System.Drawing;

namespace PMapCore.Base.Printing
{
    public static class PrintingCommon  
    {

        public const string FONT_ARIAL = "Arial";

        public static int CreateMarginalFooter(BrickGraphics p_graph, string p_printingTimeFormat, string p_printingCopyright)
        {
            Font oriFont = p_graph.Font;

            int footerRowPos = 0;

            p_graph.Modifier = BrickModifier.MarginalFooter;

            //Lapszám
            string format = "{0} / {1}";
            int rowHeight = CalcRowHeight(p_graph, format, p_graph.ClientPageSize.Width);
            PageInfoBrick brick = p_graph.DrawPageInfo(PageInfo.NumberOfTotal, format, Color.Black,
                new RectangleF(0, footerRowPos, p_graph.ClientPageSize.Width, rowHeight), BorderSide.None);
            brick.StringFormat = new BrickStringFormat(StringFormatFlags.NoClip, StringAlignment.Center, StringAlignment.Center);

            //nyomtatás dátuma
            p_graph.Font = new Font(FONT_ARIAL, 8);
            p_graph.StringFormat = new BrickStringFormat(StringFormatFlags.LineLimit, StringAlignment.Far, StringAlignment.Near);
            string printedText = String.Format("{0:"+p_printingTimeFormat+"}", DateTime.Now);
            rowHeight = CalcRowHeight(p_graph, printedText, p_graph.ClientPageSize.Width / 3);
            p_graph.DrawString(printedText, Color.Black, new RectangleF(p_graph.ClientPageSize.Width * 2 / 3, rowHeight, p_graph.ClientPageSize.Width / 3, rowHeight), BorderSide.None);


            //Copyright szöveg
            footerRowPos += rowHeight;
            p_graph.Font = new Font(FONT_ARIAL, 8);
            p_graph.BackColor = Color.Transparent;
            int textHeight8 = CalcRowHeight(p_graph, p_printingCopyright, p_graph.ClientPageSize.Width);
            p_graph.StringFormat = new BrickStringFormat(StringFormatFlags.NoClip, StringAlignment.Near, StringAlignment.Near);
            p_graph.DrawString(p_printingCopyright, Color.Black, new RectangleF(0, footerRowPos, p_graph.ClientPageSize.Width, textHeight8), BorderSide.None);
            footerRowPos += textHeight8;


            p_graph.Font = oriFont;

            return footerRowPos;
        }

        public static int CreateMarginalHeader(BrickGraphics graph, string p_header1, string p_header2)
        {
            graph.Modifier = BrickModifier.MarginalHeader;

            int headerRowPos = 0;
            Font oriFont = graph.Font;
            graph.Font = new Font(FONT_ARIAL, 16);
            int rowHeight1 = CalcRowHeight(graph, p_header1, graph.ClientPageSize.Width);
            PageInfoBrick brick = graph.DrawPageInfo(PageInfo.None, p_header1, Color.Black, new RectangleF(0, 0, 0, rowHeight1), BorderSide.None);
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;
            headerRowPos = rowHeight1;

            graph.Font = new Font(FONT_ARIAL, 12);
            int rowHeight2 = CalcRowHeight(graph, p_header2, graph.ClientPageSize.Width);
            PageInfoBrick brick2 = graph.DrawPageInfo(PageInfo.None, p_header2, Color.Black, new RectangleF(0, rowHeight1, 0, rowHeight2), BorderSide.None);
            brick2.Alignment = BrickAlignment.Center;
            brick2.AutoWidth = true;

            headerRowPos += rowHeight2;

            graph.Font = oriFont;

            return headerRowPos;
        }


        public static int CalcRowHeight(BrickGraphics gr, string s, float w)
        {
            return (int)gr.MeasureString(s, Convert.ToInt32(w)).Height;
        }

    }
}
