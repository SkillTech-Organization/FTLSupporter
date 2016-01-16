using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.BLL.Base;
using PMap.DB.Base;

namespace PMap.BLL
{
    public class bllApp : bllBase
    {
        public bllApp(SQLServerAccess p_DBA)
            : base(p_DBA, "APP_APPVER")
        {
        }
    }
}
