﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder
{
    public class SendResult
    {
        public enum RESTYPE
        {
            OK = 0,
            WARNING = 1,
            ERROR = 2
        }


        public RESTYPE ResultType { get; set; }

        public int ResultTypeVal { get { return (int)ResultType; } }

        //Vevő rendelés száma, Customer Order  code
        public string CustomerOrderNumber { get; set; }

        public string Message { get; set; }


    }
}