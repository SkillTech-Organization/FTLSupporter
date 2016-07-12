using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.Forms.Base;
using PMap.Localize;
using PMap.Forms.Panels.frmRouteVisualization;
using System.IO;
using PMap.Common;
using WeifenLuo.WinFormsUI.Docking;
using GMap.NET;
using PMap.BLL;
using PMap.BO;
using PMap.Route;
using PMap.MapProvider;
using PMap.LongProcess.Base;
using PMap.LongProcess;
using GMap.NET.WindowsForms;
using PMap.Markers;
using Map.LongProcess;
using PMap.BO.DataXChange;
using PMap.Common.PPlan;

namespace PMap.Forms
{
    public partial class frmRouteVisualization : BaseForm
    {
        private pnlRouteVisMap m_pnlRouteVisMap = null;
        private pnlRouteVisDetails m_pnlRouteVisDetails = null;
        private List<boXRouteSection> m_lstRouteSection;

        private int m_TRK_ID;

        private PPlanCommonVars m_PPlanCommonVars = new PPlanCommonVars();



        public frmRouteVisualization(List<boXRouteSection> p_lstRouteSection, int p_TRK_ID)
        {
            InitializeComponent();
            InitForm();

            initRouteVisMapForm();

            m_lstRouteSection = p_lstRouteSection;
            m_TRK_ID = p_TRK_ID;
            this.Text = PMapMessages.T_ROUTEVIS;
        }

        private void initRouteVisMapForm()
        {
            try
            {

                if (!DesignMode)
                {

                    PMapCommonVars.Instance.ConnectToDB();
                    
                    RouteVisCommonVars.Instance.Zoom = Global.DefZoom;
                    RouteVisCommonVars.Instance.TooltipMode = GMap.NET.WindowsForms.MarkerTooltipMode.OnMouseOver;
                    RouteVisCommonVars.Instance.CurrentPosition = new PointLatLng(Global.DefPosLat, Global.DefPosLng);

                    this.Text += "<< DB=" + PMapIniParams.Instance.DBConfigName + ">>";
                    RestoreLayout(true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void SaveLayout()
        {
            /*
            PMap.Forms.frmPPlan.FormSerializeHelper fs = new PMap.Forms.frmPPlan.FormSerializeHelper(this);
            string MPP_WINDOW = XMLSerializator.SerializeObject(fs);
            MemoryStream msDock = new MemoryStream();
            dockPanel.SaveAsXml(msDock, UTF8Encoding.UTF8, true);
            string MPP_DOCK = Encoding.UTF8.GetString(msDock.GetBuffer(), 0, Convert.ToInt32(msDock.Length));
            msDock.Close();

            PPlanCommonVarsSerializeHelper ps = new PPlanCommonVarsSerializeHelper(PPlanCommonVars.Instance);
            string MPP_PARAM = XMLSerializator.SerializeObject(ps);
            string MPP_TGRID = Util.SaveGridLayoutToString(m_pnlPPlanTours.gridViewTours);
            string MPP_PGRID = Util.SaveGridLayoutToString(m_pnlPPlanTourPoints.gridViewTourPoints);
            string MPP_UGRID = Util.SaveGridLayoutToString(m_pnlUnplannedOrders.gridViewUnplannedOrders);
            bllMapFormPar.SaveParameters(PPlanCommonVars.Instance.PLN_ID, PPlanCommonVars.Instance.USR_ID, MPP_WINDOW, MPP_DOCK, MPP_PARAM, MPP_TGRID, MPP_PGRID, MPP_UGRID);
             */
        }

        private void RestoreLayout(bool p_reset)
        {
            /*
            string MPP_WINDOW = "";
            string MPP_DOCK = "";
            string MPP_PARAM = "";
            string MPP_TGRID = "";
            string MPP_PGRID = "";
            string MPP_UGRID = "";
            */
            dockPanel.DockBottomPortion = 0.45;
            dockPanel.DockLeftPortion = 0.15;
            dockPanel.DockRightPortion = 0.35;

            CreatePanels(true);
            dockPanel.Refresh();

        }

        private void CreatePanels(bool bForce)
        {
            if (bForce || m_pnlRouteVisMap == null || m_pnlRouteVisMap.IsDisposed)
            {
                if (m_pnlRouteVisMap != null && !m_pnlRouteVisMap.IsDisposed)
                    m_pnlRouteVisMap.Dispose();

                m_pnlRouteVisMap = new pnlRouteVisMap(m_PPlanCommonVars);
                m_pnlRouteVisMap.Show(dockPanel, DockState.Document);
                m_pnlRouteVisMap.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlRouteVisMap_NotifyDataChanged);
            }
            if (bForce || m_pnlRouteVisDetails == null || m_pnlRouteVisDetails.IsDisposed)
            {
                if (m_pnlRouteVisDetails != null && !m_pnlRouteVisDetails.IsDisposed)
                    m_pnlRouteVisDetails.Dispose();

                m_pnlRouteVisDetails = new pnlRouteVisDetails();
                m_pnlRouteVisDetails.Show(dockPanel, DockState.DockRight);
                m_pnlRouteVisDetails.NotifyDataChanged += new EventHandler<EventArgs>(m_pnlRouteVisDetails_NotifyDataChanged);
            }

        }

        void m_pnlRouteVisDetails_NotifyDataChanged(object sender, EventArgs e)
        {
            m_pnlRouteVisMap.RefreshPanel( (RouteVisEventArgs)e);
        }

        void m_pnlRouteVisMap_NotifyDataChanged(object sender, EventArgs e)
        {
            m_pnlRouteVisDetails.RefreshPanel( (RouteVisEventArgs)e);
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmRouteVisualization_Load(object sender, EventArgs e)
        {
            m_pnlRouteVisDetails.RefreshPanel(new RouteVisEventArgs(eRouteVisEventMode.ReInit));
            m_pnlRouteVisMap.RefreshPanel(new RouteVisEventArgs(eRouteVisEventMode.ReInit));
        }

        private void btnTourDetails_Click(object sender, EventArgs e)
        {

            dlgRouteVisDetails dtl = new dlgRouteVisDetails(RouteVisCommonVars.Instance.lstDetails[RouteVisCommonVars.Instance.SelectedType].Details,
                                    RouteVisCommonVars.Instance.SelectedType == RouteVisCommonVars.TY_FASTEST ? PMapMessages.M_ROUTVIS_FASTEST : PMapMessages.M_ROUTVIS_SHORTEST);
            dtl.ShowDialog();
        }

    }
}
