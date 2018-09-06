using PMapCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMapCore.Cache
{
  
    public class LogForRouteCache : LockHolder<object>
    {
        public LogForRouteCache(object handle, int milliSecondTimeout)
            : base(handle, milliSecondTimeout)
        {

        }

        public LogForRouteCache(object handle)
            : base(handle)
        {
        }
    }

}
