using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;

namespace PMapCore.Markers
{
    public enum MarkerType
    {
        ordinary,
        PlannedDepot,
        UnPlannedDepot,
    }

    public class PMapMarker
    {
        public PointLatLng Position { get; set; }
        public String Hint { get; set; }
        public MarkerType Type { get; set; }

        public PMapMarker(PointLatLng p_Position, String p_Hint)
        {
            Position = p_Position;
            Hint = p_Hint;
            Type = MarkerType.ordinary;
        }

        public PMapMarker(PointLatLng p_Position, String p_Hint, MarkerType p_Type)
        {
            Position = p_Position;
            Hint = p_Hint;
            Type = p_Type;
        }
    }

}
