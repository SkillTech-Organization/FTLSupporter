using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder
{
    public class SendToCTResult
    {
        public enum RESTYPE
        {
            OK = 0,
            WARNING = 1,
            ERROR = 2
        }


        public RESTYPE ResultType { get; set; }
        public string Message { get; set; }


    }
}
