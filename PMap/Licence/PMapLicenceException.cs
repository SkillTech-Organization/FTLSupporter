using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.Licence
{
    internal class PMapLicenceException : Exception
    {
        public PMapLicenceException( string p_msg)
            :base( p_msg)
        {
        }
    }
}
