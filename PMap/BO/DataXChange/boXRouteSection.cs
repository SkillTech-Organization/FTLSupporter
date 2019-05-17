using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PMapCore.Strings;
using System.ComponentModel;
using PMapCore.BO.Base;
using PMapCore.Common.Attrib;
using PMapCore.BLL.DataXChange;

namespace PMapCore.BO.DataXChange
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

        [Required(ErrorMessage = DXMessages.RQ_DEP_ID)]
        [DisplayNameAttributeX(Name = "Szakasz induló lerakó ID", Order = 1)]
        public int Start_DEP_ID { get; set; } = -1;


        [Required(ErrorMessage = DXMessages.RQ_TourSection)]
        [DisplayNameAttributeX(Name = "Útvonal-szakasz típus", Order = 2)]
        public ERouteSectionType RouteSectionType { get; set; }


        /****************/
        [Required(ErrorMessage = DXMessages.RQ_DEP_NAME)]
        [DisplayNameAttributeX(Name = "Lerakónév", Order = 2)]
        public string DEP_NAME { get; set; }

        //[Required(ErrorMessage = DXMessages.RQ_ZIP_NUM)]
        [DisplayNameAttributeX(Name = "Irányítószám", Order = 3)]
        public int ZIP_NUM { get; set; }

        [DisplayNameAttributeX(Name = "Város", Order = 4)]
        public string ZIP_CITY { get; set; }

        [Required(ErrorMessage = DXMessages.RQ_DEP_ADRSTREET)]
        [DisplayNameAttributeX(Name = "Utca/közterület", Order = 5)]
        public string DEP_ADRSTREET { get; set; }

        [DisplayNameAttributeX(Name = "Házszám", Order = 6)]
        public string DEP_ADRNUM { get; set; }



        [DisplayNameAttributeX(Name = "Hosszúsági fok", Order = 14)]
        internal double Lat { get; set; }

        [DisplayNameAttributeX(Name = "Szélességi fok", Order = 15)]
        internal double Lng { get; set; }

        internal int ZIP_ID { get; set; } = -1;
        internal int NOD_ID { get; set; } = -1;
        internal int EDG_ID { get; set; } = -1;

        internal dtXResult itemRes { get; set; } = null;
    }
}
