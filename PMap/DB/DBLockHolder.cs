using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMap.DB.Base;
using PMap.Common;


namespace PMap.DB
{
    public class DBLockHolder : LockHolder<SQLServerAccess>
    {
        public DBLockHolder(SQLServerAccess handle, int milliSecondTimeout)
            : base(handle, milliSecondTimeout)
        {

        }

        public DBLockHolder(SQLServerAccess handle)
            : base(handle)
        {
        }
    }
}
