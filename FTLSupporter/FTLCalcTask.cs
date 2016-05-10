using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    public class FTLCalcTask
    {
        public FTLCalcTask()
        {
            CalcTours = new List<FTLCalcTour>();
            Messages = new List<string>();
        }

        public FTLTask Task { get; set; }
        public List<FTLCalcTour>  CalcTours { get; set; }


    }
}
