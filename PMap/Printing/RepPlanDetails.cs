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
        private const string MAPEI_ORDPREFIX = "HUN ";
        private const int height = 25;


        private const string DEF_BORDERO_LABEL = "FuvarFeladat-";
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
            var sTRUCK = "";
            double Qty = 0;
            double dToll = 0;
            double dDistance = 0;
            List<string> lstRZN = new List<string>();

            foreach (var item in m_tourList)
            {

                if (sTRUCK != item.TRUCK)
                {
                    if (!string.IsNullOrWhiteSpace(sTRUCK))
                    {

                        printSummary(graph, ref row, Qty, dToll, dDistance, lstRZN);

                        this.PrintingSystem.InsertPageBreak(row);
                        row += 2;
                    }

                    if (!string.IsNullOrWhiteSpace(item.Bordero))
                    {

                        var fwMeasure = graph.MeasureString(DEF_BORDERO_LABEL);
                        var brick1 = graph.DrawString(DEF_BORDERO_LABEL, foreColor, new Rectangle(0, row, (int)fwMeasure.Width + 1, height), BorderSide.None);
                        var brick2 = graph.DrawString(item.Bordero, foreColor, new Rectangle((int)fwMeasure.Width + 1, row, wORD_NUM / 2, height), BorderSide.None);
                        brick2.Font = new Font(reportFont, 8, FontStyle.Bold);
                        row += height;
                    }

                    graph.DrawString("Jármű:", foreColor, new Rectangle(0, row, wORD_NUM / 2, height), BorderSide.None);
                    var brTruck = graph.DrawString(item.TRUCK, foreColor, new Rectangle(wORD_NUM / 2, row, wORD_NUM, height), BorderSide.None);
                    brTruck.Font = new Font(reportFont, 8, FontStyle.Bold);
                    graph.DrawString("Rakodási időkapu:", foreColor, new Rectangle(wFullAddr + 10, row, wClient / 2, height), BorderSide.None);
                    var brPTP_ARRITME = graph.DrawString(item.PTP_ARRITME.ToString(Global.DATETIMEFORMAT_PLAN), foreColor, new Rectangle(wFullAddr + wClient / 2, row, wClient / 2, height), BorderSide.None);
                    brPTP_ARRITME.Font = new Font(reportFont, 8, FontStyle.Bold);


                    if (item.ADR != null)
                    {
                        graph.DrawString("ADR:", foreColor, new Rectangle(wFullAddr + wClient, row, 50, height), BorderSide.None);
                        var brADR = graph.DrawString((item.ADR ? "igen" : "nem"), foreColor, new Rectangle(wFullAddr + wClient + 50, row, 50, height), BorderSide.None);
                        brADR.Font = new Font(reportFont, 8, FontStyle.Bold);
                    }
                    row += height;

                    sTRUCK = item.TRUCK;
                    Qty = 0;
                    dToll = 0;
                    dDistance = 0;
                    lstRZN = new List<string>();

                }
                var col = 0;
                var sides = BorderSide.None;

                var sORD_NUM = item.ORD_NUM;

                if (sORD_NUM.Contains(MAPEI_ORDPREFIX))
                {
                    sORD_NUM = item.ORD_NUM.Replace(MAPEI_ORDPREFIX, "");
                    var xItems = sORD_NUM.Split('_');
                    sORD_NUM = xItems.First();
                }

                graph.DrawString(item.ORD_NUM, foreColor, new Rectangle(col, row, wORD_NUM, height), sides);
                col += wORD_NUM;

                graph.DrawString(item.CLIENT, foreColor, new Rectangle(col, row, wClient, height), sides);
                col += wClient;

                graph.DrawString(item.FullAddr, foreColor, new Rectangle(col, row, wFullAddr, height), sides);
                col += wFullAddr;

                var txtBrick = graph.DrawString(item.ORD_QTY.ToString(Global.NUMFORMAT), foreColor, new Rectangle(col, row, wORD_QTY, height), sides);
                txtBrick.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;

                col += wORD_QTY;


                Qty += item.ORD_QTY;
                dToll += item.PTP_TOLL;
                dDistance += item.PTP_DISTANCE;

                var lstRZNWrk = item.RZN_Code_List.Split(',');
                foreach (var rzn in lstRZNWrk)
                {
                    if (!string.IsNullOrWhiteSpace(rzn) && !lstRZN.Contains(rzn))
                        lstRZN.Add(rzn);
                }

                row += height;
            }

            if (m_tourList.Count() > 0)
            {
                printSummary(graph, ref row, Qty, dToll, dDistance, lstRZN);
            }
        }

        private void printSummary(BrickGraphics p_graph, ref int p_row, double p_Qty, double p_Toll, double p_Distance, List<string> p_lstRZN)
        {
            var xWidthLabel = 60;
            var xWidthValue = 70;
            var foreColor = Color.Black;
            p_graph.BackColor = Color.White;

            var col = 0;
            p_graph.DrawString("Súly:", foreColor, new Rectangle(0, p_row, xWidthLabel, height), BorderSide.None);
            col += xWidthLabel;
            var txtBrick1 = p_graph.DrawString(p_Qty.ToString(Global.NUMFORMAT), foreColor, new Rectangle(col, p_row, xWidthValue, height), BorderSide.None);
            txtBrick1.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBrick1.Font = new Font(reportFont, 8, FontStyle.Bold);
            col += xWidthValue+30;

            p_graph.DrawString("Útdíj:", foreColor, new Rectangle(col, p_row, xWidthLabel, height), BorderSide.None);
            col += xWidthLabel;
            var txtBrick2 = p_graph.DrawString( p_Toll.ToString(Global.NUMFORMAT), foreColor, new Rectangle(col, p_row, xWidthValue, height), BorderSide.None);
            txtBrick2.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBrick2.Font = new Font(reportFont, 8, FontStyle.Bold);
            col += xWidthValue + 30;

            var dst = Math.Round( p_Distance / 1000);
            p_graph.DrawString("Távolság:", foreColor, new Rectangle(col, p_row, xWidthLabel, height), BorderSide.None);
            col += xWidthLabel;
            var txtBrick3 = p_graph.DrawString(dst.ToString(Global.INTFORMAT) + " km", foreColor, new Rectangle(col, p_row, xWidthValue, height), BorderSide.None);
            txtBrick3.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtBrick3.Font = new Font(reportFont, 8, FontStyle.Bold);
            col += xWidthValue + 30;

            p_row += height;
            if (p_lstRZN.Count() > 0)
            {
                p_graph.DrawString("Behajtási övezetek:", foreColor, new Rectangle(0, p_row, xWidthLabel*2, height), BorderSide.None);
                col += xWidthLabel * 2;
                var rzn = string.Join(",", p_lstRZN.OrderBy(o => o));

                var txtBrick4 = p_graph.DrawString(rzn, foreColor, new Rectangle( xWidthLabel *2+ 1, p_row, 400, height), BorderSide.None);
                txtBrick4.Font = new Font(reportFont, 8, FontStyle.Bold);

                p_row += height;

            }

        }

    }
}
