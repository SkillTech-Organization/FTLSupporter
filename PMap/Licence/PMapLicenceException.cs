using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMap.Licence
{
    public class PMapLicenceException : Exception
    {
        //TODO:Internal!
        public PMapLicenceException( string p_msg)
            :base( p_msg)
        {
        }
    }
}
