using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public class FTLResultX
    {
        public enum FTLResultStatus
        {
            [Description("OK")]
            OK,
            [Description("VALIDATIONERROR")]
            VALIDATIONERROR,
            [Description("PARAMETERERROR")]
            PARAMETERERROR,
            [Description("ERROR")]
            ERROR,
            [Description("EXCEPTION")]
            EXCEPTION,
            [Description("WARNING")]
            WARNING
        };
        public FTLResultStatus Status { get; set; }
        public string ObjectName { get; set; }
        public int ItemNo { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }



    }
}
