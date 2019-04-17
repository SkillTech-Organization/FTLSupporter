using DevExpress.XtraPrinting;
using PMapCore.BLL.Report;
using PMapCore.BO;
using PMapCore.BO.Report;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.Localize;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.Printing
{
    public class RepPlanDetails : Link
    {


        private const int wORD_NUM = 150;
        private const int wClient = 270;
        private const int wFullAddr = 410;
        private const int wORD_QTY = 100;
        private const string reportFont = "Microsoft Sans Serif";
        private List<boRepPlan> m_tourList;
        public RepPlanDetails(List<boRepPlan> p_tourList)
                    : base(new PrintingSystem())
        {
            m_tourList = p_tourList;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
           this.Landscape = true;
            this.CreateReportHeaderArea += RepPlanDetails_CreateReportHeaderArea;
            this.CreateDetailHeaderArea += RepPlanDetails_CreateDetailHeaderArea;
            this.CreateMarginalHeaderArea += RepPlanDetails_CreateMarginalHeaderArea;
            this.CreateMarginalFooterArea += RepPlanDetails_CreateMarginalFooterArea;

            //       this.ShowPreview();


        }

        private void RepPlanDetails_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {

            BrickGraphics graph = e.Graph;
            var foreColor = Color.Black;

            graph.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);
            graph.Font = new Font(reportFont, 18, FontStyle.Bold);

            // Set the background color to Light Green.
            graph.BackColor = Color.White;

            var col = 0;
            var sides = BorderSide.None;
            graph.DrawString("Fuvarösszesítő lap", foreColor, new Rectangle(col, 0, wORD_NUM + wClient + wFullAddr + wORD_QTY, 25), sides);

            // Set the line alignment.
            graph.StringFormat = graph.StringFormat.ChangeAlignment(StringAlignment.Near);
        }

        private void RepPlanDetails_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            BrickGraphics graph = e.Graph;
            graph.Font = new Font(reportFont, 8, FontStyle.Italic);
            RectangleF dateTimeBounds = new RectangleF(0, 0, 0, e.Graph.Font.Height);

            PageInfoBrick pageNumberBrick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, PMapMessages.REP_PAGE, Color.Black, dateTimeBounds, BorderSide.None);
            pageNumberBrick.Alignment = BrickAlignment.Far;
            pageNumberBrick.AutoWidth = true;

            PageInfoBrick dateTimeBrick = e.Graph.DrawPageInfo(PageInfo.DateTime, string.Empty, e.Graph.ForeColor, dateTimeBounds, BorderSide.None);
            dateTimeBrick.Alignment = BrickAlignment.Near;
            dateTimeBrick.AutoWidth = true;
        }
    

         private void RepPlanDetails_CreateDetailHeaderArea(object sender, CreateAreaEventArgs e)
        {
            BrickGraphics graph = e.Graph;
            var foreColor = Color.Black;

            graph.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);
            graph.Font = new Font(reportFont, 8);

            // Set the background color to Light Green.
            graph.BackColor = Color.LightGray;

            var col = 0;
            var sides = BorderSide.All;
            graph.DrawString("Megrendelés száma", foreColor, new Rectangle(col, 0, wORD_NUM, 25), sides);
            col += wORD_NUM;

            graph.DrawString("Számlázási név", foreColor, new Rectangle(col, 0, wClient, 25), sides);
            col += wClient;

            graph.DrawString("Szállítási cím", foreColor, new Rectangle(col, 0, wFullAddr, 25), sides);
            col += wFullAddr;

            graph.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Far);
            graph.DrawString("Súly", foreColor, new Rectangle(col, 0, wORD_QTY, 25), sides);
            col += wORD_QTY;


            // Set the line alignment.
            graph.StringFormat = graph.StringFormat.ChangeAlignment(StringAlignment.Near);
        }

   
        private void RepPlanDetails_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
       
        }


        protected override void CreateDetail(BrickGraphics graph)
        {
            base.CreateDetail(graph);
            graph.StringFormat = graph.StringFormat.ChangeAlignment(StringAlignment.Near);
            var foreColor = Color.Black;
            graph.BackColor = Color.White;

            var row = 1;
            var height = 25;
            var sTRUCK = "";
            foreach (var item in m_tourList)
            {

                if (sTRUCK != item.TRUCK)
                {
                    if (!string.IsNullOrWhiteSpace(sTRUCK))
                    {
                        row++;
                        this.PrintingSystem.InsertPageBreak(row);
                    }

                    if (!string.IsNullOrWhiteSpace(item.Bordero))
                    {
                        var brick1 = graph.DrawString("FuvarFeladat--", foreColor, new Rectangle(0, row, wORD_NUM / 2, height), BorderSide.None);
                        var brick2 = graph.DrawString(item.Bordero, foreColor, new Rectangle(wORD_NUM / 2 -5, row, wORD_NUM / 2, height), BorderSide.None);
                        brick2.Font = new Font(reportFont, 8, FontStyle.Bold);
                        row += height;
                    }

                    graph.DrawString("Jármű:", foreColor, new Rectangle(0, row, wORD_NUM / 2, height), BorderSide.None);
                    var brTruck = graph.DrawString(item.TRUCK, foreColor, new Rectangle(wORD_NUM / 2, row, wORD_NUM/2, height), BorderSide.None);
                    brTruck.Font = new Font(reportFont, 8, FontStyle.Bold);
                    graph.DrawString("Rakodási időkapu:", foreColor, new Rectangle(wFullAddr+20, row, wClient / 2, height), BorderSide.None);
                    var brPTP_ARRITME = graph.DrawString( item.PTP_ARRITME.ToString( Global.DATETIMEFORMAT_PLAN), foreColor, new Rectangle(wFullAddr+ wClient/2, row, wClient / 2, height), BorderSide.None);
                    brPTP_ARRITME.Font = new Font(reportFont, 8, FontStyle.Bold);


                    if (item.ADR != null)
                    {
                        graph.DrawString("ADR:", foreColor, new Rectangle(wFullAddr + wClient, row, wClient, height), BorderSide.None);
                        var brADR = graph.DrawString((item.ADR ? "igen" : "nem"), foreColor, new Rectangle(wFullAddr + wClient + 50, row, 50, height), BorderSide.None);
                        brADR.Font = new Font(reportFont, 8, FontStyle.Bold);
                    }
                    row += height;

                    sTRUCK = item.TRUCK;
                    row += height;

                }
                var col = 0;
                var sides = BorderSide.None;
                graph.DrawString( item.ORD_NUM, foreColor, new Rectangle(col, row, wORD_NUM, height), sides);
                col += wORD_NUM;

                graph.DrawString(item.CLIENT, foreColor, new Rectangle(col, row, wClient, height), sides);
                col += wClient;

                graph.DrawString(item.FullAddr, foreColor, new Rectangle(col, row, wFullAddr, height), sides);
                col += wFullAddr;

                var txtBrick = graph.DrawString(item.ORD_QTY.ToString(Global.NUMFORMAT), foreColor, new Rectangle(col, row, wORD_QTY, height), sides);
                txtBrick.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;

                col += wORD_QTY;

                row += height;
            }

    //        CreateRow(graph);
        }
       
    }
}
