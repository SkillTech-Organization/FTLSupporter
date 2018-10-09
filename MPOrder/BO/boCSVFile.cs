using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder.BO
{
    public class boCSVFile
    {
        public string CSVFileName { get; set; } = "";
        public string ShortCSVFileName
        {
            get
            {
                return Path.GetFileName(CSVFileName);
            }
        }
        public DateTime ShippingDateX { get; set; }

    }
}
