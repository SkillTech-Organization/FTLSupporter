using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PMap.Common.Attrib
{
    public class DisplayNameAttributeX : DisplayNameAttribute
    {
        public string Name { get; set; }
        public int Order { get; set; }



        public DisplayNameAttributeX()
            : base()
        {
        }

        public DisplayNameAttributeX(string p_name)
            : base(p_name)
        {
            Name = p_name;
        }
        public DisplayNameAttributeX(string p_name, int p_order)
            : base(p_name)
        {
            Name = p_name;
            Order = p_order;
        }
        public override string DisplayName
        {
            get
            {
                return Order.ToString().PadLeft(2, ' ') + "\t" + Name;
            }
        }

    }
}
