using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotsGame
{
    public partial class frmDlgPE : Form
    {
        public frmDlgPE()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PEminXY.minX = (int)numericUpDownX.Value;
            PEminXY.minY = (int)numericUpDownY.Value;
            PEminXY.maxX = (int)numericUpDownXmax.Value;
            PEminXY.maxY = (int)numericUpDownYmax.Value;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
