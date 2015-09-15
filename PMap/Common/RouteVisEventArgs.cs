using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.Common
{
    public enum eRouteVisEventMode
    {
        ReInit,                         //
        ChgZoom,                        //Zoom változott
        ChgTooltipMode,                 //Változott markerek tooltip megjelenítési módja
        ChgRouteVisible,                //Változott egy útvonal láthatósága
        ChgDepotSelected                //Változott egy lerakó kiválasztottsága
    }

    public class RouteVisEventArgs : EventArgs
    {
        public eRouteVisEventMode EventMode { get; set; }
        public RouteVisEventArgs(eRouteVisEventMode p_eventMode)
        {
            EventMode = p_eventMode;
        }

    }
}
