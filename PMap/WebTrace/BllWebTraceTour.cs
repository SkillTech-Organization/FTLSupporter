
using PMapCore.Common.Azure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PMapCore.WebTrace
{
    public class BllWebTraceTour : AzureBllBase< PMTour>
    {

        BllWebTraceTourPoint m_bllWebTraceTourPoint;
        public BllWebTraceTour(string p_user) : base(p_user)
        {
            m_bllWebTraceTourPoint = new BllWebTraceTourPoint(p_user);
        }
        public override PMTour Retrieve(object p_partitionKey, object p_rowKey)
        {
            
            var tour = base.Retrieve(p_partitionKey, p_rowKey);
            if (tour != null )
            {
                int total;
                var tp = m_bllWebTraceTourPoint.RetrieveList(out total, String.Format("PartitionKey eq '{0}' ", tour.ID)).ToList();
                if( tp != null)
                {
                    tour.TourPoints = tp;
                }
            }
            return tour;
        }

        
        public override ObservableCollection<PMTour> RetrieveList(out int Total, string p_where = "", string p_orderBy = "", int pageSize = 0, int page = 1)
        {
            var tourList = base.RetrieveList(out Total, p_where, p_orderBy);
            foreach( var tour in tourList)
            {
                int total;
                var tp = m_bllWebTraceTourPoint.RetrieveList(out total, String.Format("PartitionKey eq '{0}' ", tour.ID)).ToList();
                if (tp != null)
                {
                    tour.TourPoints = tp;
                }

            }
            return tourList;
        }
        

    }
}
