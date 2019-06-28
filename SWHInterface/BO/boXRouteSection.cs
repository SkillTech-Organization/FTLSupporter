using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.Strings;
using System.ComponentModel;
using PMapCore.BO.Base;
using PMapCore.Common.Attrib;
using PMapCore.BLL.DataXChange;
using System.ComponentModel.DataAnnotations;

namespace SWHInterface.BO
{
    public class boXRouteSection : boXBase
    {
        public enum ERouteSectionType
        {
            [Description("Untyped")]
            Untyped = 0,
            [Description("Empty")]
            Empty = 1,
            [Description("Loaded")]
            Loaded = 2,
            [Description("Finish")]
            Finish = 3
        };
        public boXRouteSection()
        {
            RouteSectionType = ERouteSectionType.Untyped;
        }

        [Required(ErrorMessage = DXMessages.RQ_TourSection)]
        [DisplayNameAttributeX(Name = "Útvonal-szakasz típus", Order = 2)]
        public ERouteSectionType RouteSectionType { get; set; }


        /****************/
        [Required(ErrorMessage = DXMessages.RQ_DEP_NAME)]
        [DisplayNameAttributeX(Name = "Lerakónév", Order = 2)]
        public string DEP_NAME { get; set; }

        [ErrorIfConstAttrX(EvalMode.IsSmallerOrEqual, 0, DXMessages.RQ_LAT)]
        [DisplayNameAttributeX(Name = "Hosszúsági fok", Order = 14)]
        public double Lat { get; set; }

        [ErrorIfConstAttrX(EvalMode.IsSmallerOrEqual, 0, DXMessages.RQ_LNG)]
        [DisplayNameAttributeX(Name = "Szélességi fok", Order = 15)]
        public double Lng { get; set; }

        /*A címadatok csak tájékoztató jellegűek, nem kötelező tölteni*/
        [DisplayNameAttributeX(Name = "Irányítószám", Order = 3)]
        public int ZIP_NUM { get; set; }

        [DisplayNameAttributeX(Name = "Város", Order = 4)]
        public string ZIP_CITY { get; set; }

        [DisplayNameAttributeX(Name = "Utca/közterület", Order = 5)]
        public string DEP_ADRSTREET { get; set; }

        [DisplayNameAttributeX(Name = "Házszám", Order = 6)]
        public string DEP_ADRNUM { get; set; }
        

        internal int ZIP_ID { get; set; } = -1;
        internal int NOD_ID { get; set; } = -1;
        internal int EDG_ID { get; set; } = -1;

        internal dtXResult itemRes { get; set; } = null;
    }
}
