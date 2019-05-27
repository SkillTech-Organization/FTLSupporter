using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMapCore.Common
{
    public enum eRouteVisEventMode
    {
        ReInit,                         //
        ChgZoom,                        //Zoom változott
        ChgTooltipMode,                 //Változott markerek tooltip megjelenítési módja
        ChgRouteVisible,                //Változott egy útvonal láthatósága
        ChgDepotSelected                //Változott egy lerakó kiválasztottsága
    }

    public class RouteVisEventArgs_DEPRECATED : EventArgs
    {
        public eRouteVisEventMode EventMode { get; set; }
        public RouteVisEventArgs_DEPRECATED(eRouteVisEventMode p_eventMode)
        {
            EventMode = p_eventMode;
        }

    }
}
