using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLResult
    {
        public enum FTLResultStatus
        {
            [Description("RESULT")]
            RESULT,
            [Description("VALIDATIONERROR")]
            VALIDATIONERROR,
            [Description("EXCEPTION")]
            EXCEPTION
        };
        public FTLResultStatus Status { get; set; }
        public string ObjectName { get; set; }
        public string ItemID { get; set; }
        public object Data { get; set; }

    }
}
