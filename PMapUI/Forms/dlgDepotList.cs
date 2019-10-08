﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMapUI.Forms.Base;
using PMapCore.BO;
using PMapUI.Printing;

namespace PMapUI.Forms
{
    public partial class dlgDepotList : BaseDialog
    {
        List<boDepot> m_lstDepot = null;
        public dlgDepotList(List<boDepot> p_lstDepot, string p_caption)
            : base(eEditMode.infomode)
        {
            InitializeComponent();
            m_lstDepot = p_lstDepot;
            gridDepots.DataSource = p_lstDepot;
            this.Text += "<<<" + p_caption + ">>>";
            InitDialog();
            AskOnExit = false;

        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            PMapGridPrinting pr = new PMapGridPrinting(gridDepots, this.Text, true);
            pr.ShowPreview();
        }

        private void excelToolStripButton_Click(object sender, EventArgs e)
        {
            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                gridDepots.ExportToXls(openExcel.FileName);
            }
        }
    }
}