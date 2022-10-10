using PMapCore.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLSupporter
{
    public class FTLRouteCache
    {

        public System.Collections.Concurrent.ConcurrentBag<boRoute> Items = null;

        private static readonly Lazy<FTLRouteCache> m_instance = new Lazy<FTLRouteCache>(() => new FTLRouteCache(), true);


        static public FTLRouteCache Instance
        {
            get
            {
                return m_instance.Value;            //It's thread safe!
            }
        }

        private FTLRouteCache()
        {
            Items = new System.Collections.Concurrent.ConcurrentBag<boRoute>();
        }
    }
}
