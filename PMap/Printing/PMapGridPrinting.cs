﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using System.Drawing.Printing;
using PMap.Printing.Base;

namespace PMap.Printing
{
    public class PMapGridPrinting : BaseGridPrinting
    {
        public PMapGridPrinting(GridControl p_grid, string p_header, bool p_landscape)
            : base(p_grid, PaperKind.A4, p_landscape, p_header, "", "PMap - Pratix Kft.,2011-2014", Global.DATETIMEFORMAT, true)
        {
        }

    }
}
