using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMapCore.Common
{
    public class FormSerializeHelper
    {
        public FormWindowState WindowState { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public FormSerializeHelper()
        {
        }

        public FormSerializeHelper(Form p_form)
        {
            WindowState = p_form.WindowState;
            Width = p_form.Width;
            Height = p_form.Height;
            Left = p_form.Left;
            Top = p_form.Top;
        }
    }

}
