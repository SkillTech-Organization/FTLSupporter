using PMapCore.BLL.Base;
using PMapCore.DB.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.BLL.Mapei
{
    public class bllMPOrder : bllBase
    {
        public bllMPOrder(SQLServerAccess p_DBA)
            : base(p_DBA, "")
        {
        }
    }
}
