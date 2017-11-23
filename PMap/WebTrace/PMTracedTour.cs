using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMap.WebTrace
{
    [Serializable]
    public class PMTracedTour
    {
        [DataMember]
        [DisplayNameAttributeX(Name = "Túra azonosítója")]
        public int TourID { get; set; }


        [DataMember]
        [DisplayNameAttributeX(Name = "Túrapont sorszáma")]
        public int Order { get; set; }


    }
}
