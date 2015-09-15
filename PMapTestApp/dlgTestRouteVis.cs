using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMap.Forms.Base;

namespace PMapTestApp
{
    public partial class dlgTestRouteVis : Form
    {
        public dlgTestRouteVis()
        {
            InitializeComponent();
            txtDEPID.Text = "1;2,2;2,3;2,4;2;2;2,5;2,6;2,7;2,8;2,9;2,100;1,3497;0";
            //txtDEPID.Text = "59;62,62;59";
            // txtDEPID.Text = "5161;2,5162;2,5164;1,5163;0";
            txtTRKID.Text = "19";
        }
    }
}
