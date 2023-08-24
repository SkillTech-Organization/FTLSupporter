using System.Collections.Generic;
using System.Linq;

namespace FTLSupporter
{
    public class FTLResponse
    {
        public string RequestID { get; set; }
        public List<FTLResult> Result { get; set; } = new List<FTLResult>();

        public bool HasError
        {
            get
            {
                return Result.Any(a =>
                     a.Status == FTLResult.FTLResultStatus.VALIDATIONERROR ||
                     a.Status == FTLResult.FTLResultStatus.EXCEPTION ||
                     a.Status == FTLResult.FTLResultStatus.ERROR);
            }
        }
    }
}
