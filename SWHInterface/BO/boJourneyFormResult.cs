using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWHInterface.BO
{
    public class boJourneyFormResult
    {
        public boXRouteSummary TotalSummary = new boXRouteSummary();
        public List<boXRouteSummary> SectionSummaries = new List<boXRouteSummary>();

    }
}
