using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using PMapCore.Strings;
using System.ComponentModel;
using PMapCore.BO.Base;
using PMapCore.Common.Attrib;
using PMapCore.Strings;

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
        public int Start_DEP_ID { get; set; }

        [Required(ErrorMessage = DXMessages.RQ_TourSection)]
        [DisplayNameAttributeX(Name = "Útvonal-szakasz típus", Order = 2)]
        public ERouteSectionType RouteSectionType { get; set; }

    }
}
