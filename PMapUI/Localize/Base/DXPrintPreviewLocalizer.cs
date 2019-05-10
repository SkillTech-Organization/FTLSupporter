using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting.Localization;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Export.Pdf;
using PMapCore.Properties;
using PMapCore.Common;

namespace PMapUI.Localize.Base
{
    class DXPrintPreviewLocalizer : PreviewLocalizer
    {
        protected Hashtable Strings {get;set;}

        public DXPrintPreviewLocalizer(StringReader p_stringReader)
            : base()
        {
            Strings = new Hashtable(250);

            string temp;
            do
            {
                temp = p_stringReader.ReadLine();
                if (temp == null || temp == "") break;
                ArrayList al = CSVParser.Parse(temp.Trim());
                Strings.Add(al[0], al[1]);
            }
            while (true);
        }

        public override string GetLocalizedString(PreviewStringId p_id)
        {
            string s = p_id.ToString();
            object o = Strings[s];
            if (o == null)
            {
                Console.WriteLine(s);
                return s;
            }
            if (o.ToString() == "")
            {
                string sOriString = base.GetLocalizedString(p_id);
                Console.WriteLine(s + "-->" + sOriString);
                return sOriString;
            }

            return o.ToString();
        }
    }
}
