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
    public sealed class PPlanCommonVars
    {

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

        // private static readonly object padlock = new object();
        [System.Xml.Serialization.XmlIgnoreAttribute]
        private static volatile object padlock = new object();

        //Singleton technika...
        [System.Xml.Serialization.XmlIgnoreAttribute]
        static private PPlanCommonVars instance = new PPlanCommonVars();        //Mivel statikus tag a program indulásakor 

        [System.Xml.Serialization.XmlIgnoreAttribute]
        static public PPlanCommonVars Instance                                  //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PPlanCommonVars();
                    }
                }
                return instance;
            }
        }
        public void Reset()
        {
            this.Zoom = 0;
            this.CurrentPosition = new PointLatLng();
            this.PLN_ID = 0;
            this.TourList = null;
            this.PlanOrderList = null;
            this.SelectedTour = null;
            this.Changed = false;
            this.DraggedObj = null;
            this.FocusedTour = null;
            this.FocusedPoint = null;
            this.FocusedUnplannedOrder = null;

            this.ShowPlannedDepots = false;
            this.ShowUnPlannedDepots = false;
            this.ZoomToSelectedPlan = false;
            this.ZoomToSelectedUnPlanned = false;
            this.ShowAllOrdersInGrid = false;
        }


        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int Zoom { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public PointLatLng CurrentPosition { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int PLN_ID { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public List<boPlanTour> TourList { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public List<boPlanOrder> PlanOrderList { get; set; }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanTour SelectedTour { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool Changed { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public PPlanDragObject DraggedObj { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanTour FocusedTour { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanTourPoint FocusedPoint { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public boPlanOrder FocusedUnplannedOrder { get; set; }



        /// <summary>
        /// serilizálandó paraméterek
        /// </summary>
        public bool ShowPlannedDepots { get; set; }
        public bool ShowUnPlannedDepots { get; set; }
        public MarkerTooltipMode TooltipMode { get; set; }
        public bool ZoomToSelectedPlan { get; set; }
        public bool ZoomToSelectedUnPlanned { get; set; }
        public bool ShowAllOrdersInGrid { get; set; }

        public boPlanTour GetTourByID(int p_ID)
        {
            var linq = (from o in PPlanCommonVars.Instance.TourList
                        where o.ID == p_ID
                        select o);

            if (linq.Count<boPlanTour>() > 0)
                return linq.First<boPlanTour>();
            else
                return null;
        }

        public boPlanOrder GetPlannedOrderByID(int p_ID)
        {
            var linq = (from o in PPlanCommonVars.Instance.PlanOrderList
                        where o.ID == p_ID
                        select o);

            if (linq.Count<boPlanOrder>() > 0)
                return linq.First<boPlanOrder>();
            else
                return null;
        }

        public boPlanTourPoint GetTourPointByID(int p_ID)
        {
            foreach (boPlanTour tour in PPlanCommonVars.Instance.TourList)
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
            foreach (boPlanTour tour in PPlanCommonVars.Instance.TourList)
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
            foreach (boPlanOrder upt in PPlanCommonVars.Instance.PlanOrderList)
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
