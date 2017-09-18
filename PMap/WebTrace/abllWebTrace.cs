using PMap.Common.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.WebTrace
{
    public class abllWebTrace : AzureBllBase< wtTour>
    {

        public abllWebTrace(string p_user) : base(p_user)
        {
        }

    }
}
