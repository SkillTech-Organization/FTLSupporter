using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMapTestApp
{
    public partial class dlgTestGeoCoding : Form
    {
        public dlgTestGeoCoding()
        {
            InitializeComponent();
        }

        private void dlgTestGeoCoding_Load(object sender, EventArgs e)
        {
            txtAddr.Focus();
        }
    }
}
