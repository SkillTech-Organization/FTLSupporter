using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMapCore.Licence
{
    public class PMapLicenceException : Exception
    {
        internal PMapLicenceException( string p_msg)
            :base( p_msg)
        {
        }
    }
}
