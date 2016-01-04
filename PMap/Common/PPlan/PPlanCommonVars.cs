using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using PMap.DB.Base;
using System.ComponentModel;
using PMap.DB;
using GMap.NET.WindowsForms;
using System.Xml.Serialization;
using GMap.NET.MapProviders;
using PMap.Route;
using PMap.BO;
using DevExpress.XtraGrid;


namespace PMap.Common.PPlan
{
    public class PPlanCommonVars
    {
        public event EventHandler<PlanEventArgs> NotifyDataChanged;

        public void DoNotifyDataChanged(PlanEventArgs e)
        {
            if (this.NotifyDataChanged != null)
                NotifyDataChanged(this, e);
        }
        
        public class PPlanDragObject
        {
            public enum ESourceDataObjectType
            {
                [Description("TourPoint")]
                TourPoint = 0,
                [Description("Order")]
                Order = 1
            };

            public PPlanDragObject(ESourceDataObjectType p_SourceDataObjectType)
            {
                SourceDataObjectType = p_SourceDataObjectType;
            }

            public int ID { get; set; }
            public ESourceDataObjectType SourceDataObjectType { get; private set; }
            public object DataObject { get; set; }
            public DevExpress.XtraGrid.GridControl SrcGridControl { get; set; }
        }

        public PPlanCommonVars()
        {
            TourList = new List<boPlanTour>();
            PlanOrderList = new List<boPlanOrder>();
        }

         private bool m_editMode = false;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool EditMode
        {
            get { return m_editMode; }
            set
            {
                m_editMode = value;
                if (m_editMode)
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.EditorMode));
                else
                    DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ViewerMode));

            }
        }

        private int m_zoom;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int Zoom        
        {
            get { return m_zoom; }
            set
            {
                m_zoom = value;
                DoNotifyDataChanged( new PlanEventArgs(ePlanEventMode.ChgZoom));
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public PointLatLng CurrentPosition { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int PLN_ID { get; set; }

        private List<boPlanTour> m_tourList;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public List<boPlanTour> TourList 
        {
            get { return m_tourList; }

            set
            {
                m_tourList = value;
                if (m_tourList != null && m_tourList.Count > 0)
                    FocusedTour = m_tourList.FirstOrDefault();
                else
                    FocusedTour = null;
            }
        }

        public void AddTourToList(int pIndex, boPlanTour p_PlanTour)
        {
            TourList.Insert(pIndex, p_PlanTour);
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.AddTour, p_PlanTour));
        }

        public void AddTourToList(boPlanTour p_PlanTour)
        {
            TourList.Add( p_PlanTour);
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.AddTour, p_PlanTour));
        }

        public void RemoveTourFromList(boPlanTour p_PlanTour)
        {
            TourList.Remove(p_PlanTour);
            DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.RemoveTour, p_PlanTour));
        }


        private List<boPlanOrder> m_planOrderList;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public List<boPlanOrder> PlanOrderList
        {
            get { return m_planOrderList; }
            set
            {
                m_planOrderList = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.RefreshOrders));
                if( m_planOrderList != null && m_planOrderList.Count > 0)
                {
                    boPlanOrder po = m_planOrderList.FirstOrDefault(x => x.PTP_ID == 0 || ShowAllOrdersInGrid);
                    if (po != null)
                    {
                        if (po.PTP_ID == 0)
                        {
                            FocusedUnplannedOrder = null;
                            FocusedOrder = po;
                        }
                        else
                        {
                            FocusedUnplannedOrder = po;
                            FocusedOrder = null;
                        }
                    }
                    else
                    {
                        FocusedUnplannedOrder = null;
                        FocusedOrder = null;
                    }
                }
                else
                {
                    FocusedUnplannedOrder = null;
                    FocusedOrder = null;
                }
            }
        }



        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool Changed { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public PPlanDragObject DraggedObj { get; set; }

        private boPlanTour m_focusedTour;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanTour FocusedTour
        {
            get { return m_focusedTour; }
            set
            {
                m_focusedTour = value;
                PlanEventArgs pea = new PlanEventArgs(ePlanEventMode.ChgFocusedTour, m_focusedTour);
                DoNotifyDataChanged(pea);
                if (m_focusedTour != null)
                    FocusedPoint = m_focusedTour.TourPoints.FirstOrDefault();
                else
                    FocusedPoint = null;
            }
        }

        private boPlanTourPoint m_focusedPoint;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanTourPoint FocusedPoint
        {
            get { return m_focusedPoint; }
            set { 
                m_focusedPoint = value;
                PlanEventArgs pea = new PlanEventArgs(ePlanEventMode.ChgFocusedTourPoint, m_focusedPoint);
                DoNotifyDataChanged(pea);
            }
        }

        private boPlanOrder m_focusedOrder { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanOrder FocusedOrder
        {
            get { return m_focusedOrder; }
            set
            {
                m_focusedOrder = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgFocusedOrder, m_focusedOrder));
            }
        }

        private boPlanOrder m_focusedUnplannedOrder { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanOrder FocusedUnplannedOrder
        {
            get { return m_focusedUnplannedOrder; }
            set { m_focusedUnplannedOrder = value; }
        }


        /// <summary>
        /// serilizálandó paraméterek
        /// </summary>
        private bool m_showPlannedDepots;
        public bool ShowPlannedDepots
        {
            get { return m_showPlannedDepots; }
            set
            {
                m_showPlannedDepots = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgShowPlannedFlag));
            }
        }

        private bool m_showUnPlannedDepots;
        public bool ShowUnPlannedDepots 
        {
            get { return m_showUnPlannedDepots; }
            set
            {
                m_showUnPlannedDepots = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgShowUnPlannedFlag));
            }
        }


        private MarkerTooltipMode m_tooltipMode;
        public MarkerTooltipMode TooltipMode
       {
           get { return m_tooltipMode; }
            set
            {
                m_tooltipMode = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgTooltipMode));
            }
        }


        private bool m_zoomToSelectedPlan;
        public bool ZoomToSelectedPlan
        {
            get { return m_zoomToSelectedPlan; }
            set
            {
                m_zoomToSelectedPlan = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgZoomToSelectedTour));
            }
        }

        private bool m_zoomToSelectedUnPlanned;
        public bool ZoomToSelectedUnPlanned
        {
            get { return m_zoomToSelectedUnPlanned; }
            set
            {
                m_zoomToSelectedUnPlanned = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgZoomToSelectedUnPlanned));
            }
        }

        private bool m_showAllOrdersInGrid;

        public bool ShowAllOrdersInGrid
       {
           get { return m_showAllOrdersInGrid; }
            set
            {
                m_showAllOrdersInGrid = value;
                DoNotifyDataChanged(new PlanEventArgs(ePlanEventMode.ChgShowAllOrdersInGrid));
            }
        }

        public boPlanTour GetTourByID(int p_ID)
        {
            var linq = (from o in TourList
                        where o.ID == p_ID
                        select o);

            if (linq.Count<boPlanTour>() > 0)
                return linq.First<boPlanTour>();
            else
                return null;
        }

        public boPlanOrder GetPlannedOrderByID(int p_ID)
        {
            var linq = (from o in PlanOrderList
                        where o.ID == p_ID
                        select o);

            if (linq.Count<boPlanOrder>() > 0)
                return linq.First<boPlanOrder>();
            else
                return null;
        }

        public boPlanTourPoint GetTourPointByID(int p_ID)
        {
            foreach (boPlanTour tour in TourList)
            {

                var linq = (from o in tour.TourPoints
                            where o.ID == p_ID
                            select o);
                if (linq.Count<boPlanTourPoint>() > 0)
                {
                    return linq.First<boPlanTourPoint>();
                }
            }
            return null;
        }

        public boPlanTourPoint GetTourPointByORD_NUM(string p_ORD_NUM)
        {
            boPlanTourPoint oTp = null;
            p_ORD_NUM = p_ORD_NUM.ToUpper();
            foreach (boPlanTour tour in TourList)
            {
                if (tour.TourPoints != null)
                {
                    foreach (boPlanTourPoint tp in tour.TourPoints)
                    {
                        if (tp.ORD_NUM.ToUpper() == p_ORD_NUM)
                        {
                            oTp = tp;
                            break;
                        }
                    }
                    if (oTp != null)
                        break;
                }
            }
            return oTp;
        }

        public boPlanOrder GetOrderByORD_NUM(string p_ORD_NUM)
        {
            boPlanOrder oUpOrder = null;
            p_ORD_NUM = p_ORD_NUM.ToUpper();
            foreach (boPlanOrder upt in PlanOrderList)
            {
                if (upt.ORD_NUM.ToUpper() == p_ORD_NUM)
                {
                    oUpOrder = upt;
                    break;
                }
            }
            return oUpOrder;
        }

    }
}
