using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;
using PMapCore.DB;
using PMapCore.BO;

namespace PMapCore.Markers
{
    public class PPlanMarkerUnPlanned : GMarkerGoogle
    {
        public boPlanOrder UnplannedOrder { get; private set; }

        public PPlanMarkerUnPlanned(PointLatLng p, boPlanOrder p_UnplannedOrder)
            : base(p, GMarkerGoogleType.red_dot)
        {
            UnplannedOrder = p_UnplannedOrder;
        }

    }
}
