﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public  class FTLQueueResponse
    {
        public string RequestID { get; set; }
        public List<FTLResult> Result { get; set; } = new List<FTLResult>();
    }
}