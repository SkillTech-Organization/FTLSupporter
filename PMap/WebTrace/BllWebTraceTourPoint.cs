using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.WebTrace
{
    public class BllWebTraceTourPoint : AzureBllBase< PMTourPoint>
    {

        public BllWebTraceTourPoint(string p_user) : base(p_user)
        {
        }

    }
}
