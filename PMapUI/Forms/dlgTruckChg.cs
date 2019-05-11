using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapCore.DB;
using PMapCore.DB.Base;
using PMapCore.BO;
using PMapCore.BLL;
using PMapCore.Common;
using PMapUI.Forms.Base;
using PMapCore.Strings;
using PMapUI.Forms.Funcs;
using PMapCore.Common.PPlan;
using PMapUI.Common;

namespace PMapUI.Forms
{
    public partial class dlgTruckChg : BaseDialog
    {
        public int TPL_ID { get; private set; }
        private int m_PLN_ID;
        private boPlanTour m_Tour;
        private boActivePlanInfo m_ActivePlanInfo;
        private DataTable m_dtUnplannedTrucks;

        private bllPlan m_bllPlan;
        private PlanEditFuncs m_PlanEditFuncs;
        private PPlanCommonVars m_PPlanCommonVars;

        public dlgTruckChg(int p_PLN_ID, boPlanTour p_Tour, PlanEditFuncs p_PlanEditFuncs, PPlanCommonVars p_PPlanCommonVars)
            : base(eEditMode.editmode)
        {
            InitializeComponent();

            m_bllPlan = new bllPlan(PMapCommonVars.Instance.CT_DB);
            m_PlanEditFuncs = p_PlanEditFuncs;
            m_PPlanCommonVars = p_PPlanCommonVars;

            TPL_ID = 0;
            m_PLN_ID = p_PLN_ID;
            m_Tour = p_Tour;
            txTRK_NAME.Text = p_Tour.TRUCK;

            m_dtUnplannedTrucks = m_bllPlan.GetUnplannedTrucks(m_PLN_ID);
            m_ActivePlanInfo = m_bllPlan.GetActivePlanInfo(m_PLN_ID);
            cmbTruck.DisplayMember = "TRK_DISP";
            cmbTruck.ValueMember = "TPL_ID";
            cmbTruck.Items.Clear();
            cmbTruck.DataSource = m_dtUnplannedTrucks;
            InitDialog();
        }

        private void cmbTruck_TextChanged(object sender, EventArgs e)
        {
            var linq = (from rec in m_dtUnplannedTrucks.AsEnumerable()
                        where rec.Field<int>("TPL_ID") == Convert.ToInt32(cmbTruck.SelectedValue)
                        select rec);
            if (linq.Count<DataRow>() > 0)
            {
                Random rnd = new Random((int)DateTime.Now.Millisecond);
                int iColor = Util.getFieldValue<int>(linq.First<DataRow>(), "TRK_COLOR");
                Color TrkColor = Color.FromArgb(iColor);
                if (TrkColor == Color.FromArgb(-1) || TrkColor == Color.FromArgb(0) || TrkColor == Color.FromArgb(0, 255, 255, 255))
                    cmbColor.SelectedColor = Color.FromArgb(rnd.Next(0, 127) * 2, rnd.Next(0, 255), rnd.Next(0, 255));
                else
                {
                    cmbColor.SelectedColor = Color.FromArgb(TrkColor.R, TrkColor.G, TrkColor.B);     //Ezt így kell !!!
                    cmbColor.Refresh();
                }
            }

        }

        public override Control ValidateForm()
        {
            bllPlanCheck.checkOrderResult res;

            TPL_ID = Convert.ToInt32(cmbTruck.SelectedValue);

            boPlanTour newTour = m_PPlanCommonVars.TourList.Where(i => i.ID == TPL_ID).FirstOrDefault();


            boPlanTourPoint prevTourPoint = null;
            foreach (boPlanTourPoint rTourPoint in m_Tour.TourPoints)
            {
                if (rTourPoint.TOD_ID > 0)
                {
                    res = bllPlanCheck.CheckAll(rTourPoint.TOD_ID, TPL_ID);
                    if (res != bllPlanCheck.checkOrderResult.OK)
                    {
                        UI.Error(rTourPoint.TIME_AND_NAME + ":\n\n" + bllPlanCheck.GetOrderResultText(res));
                        return cmbTruck;
                    }

                }
                if (prevTourPoint != null &&
                    bllPlanCheck.CheckDistance(newTour.RZN_ID_LIST,  newTour.TRK_WEIGHT, newTour.TRK_XHEIGHT, newTour.TRK_XWIDTH, prevTourPoint.NOD_ID, rTourPoint.NOD_ID) != bllPlanCheck.checkDistanceResult.OK)
                {
                    UI.Error(PMapMessages.E_CHGTRK_NOTFITTEDDEP, prevTourPoint.TIME_AND_NAME.Trim(), rTourPoint.TIME_AND_NAME.Trim());
                    return cmbTruck;
                }

                prevTourPoint = rTourPoint;
            }

            return null;
        }

        public override bool OKPressed()
        {
            m_PlanEditFuncs.ChangeTruck(m_PLN_ID, m_Tour.ID, TPL_ID, cmbColor.SelectedColor);
            return true;
        }
    }
}
