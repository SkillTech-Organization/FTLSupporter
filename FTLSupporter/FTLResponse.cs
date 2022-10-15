﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public  class FTLResponse
    {
        public string ProcessID { get; set; }
        public int MaxTruckDistance { get; set; } = 0;
        public List<FTLResult> Result { get; set; } = new List<FTLResult>();

        public bool HasError
        {
            get {
                return Result.Any(a =>
                     a.Status == FTLResult.FTLResultStatus.VALIDATIONERROR ||
                     a.Status == FTLResult.FTLResultStatus.EXCEPTION ||
                     a.Status == FTLResult.FTLResultStatus.ERROR);
            }
        }
    }
}
