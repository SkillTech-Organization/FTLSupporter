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
    public partial class dlgTestReverseGeocoding : Form
    {
        public dlgTestReverseGeocoding()
        {
            InitializeComponent();
            numLat.Value = Decimal.Parse("46,2480591");
            numLng.Value= Decimal.Parse("20,1736693");
        }

        private void dlgTestReverseGeocoding_Load(object sender, EventArgs e)
        {
            numLat.Focus();
        }
    }
}
