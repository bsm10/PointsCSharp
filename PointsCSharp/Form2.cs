using System;
using System.Windows.Forms;

namespace DotsGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            rbtnAuto.Checked = true;

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public ListBox listDbg1()
        {
            return lstDbg1;
        }
        public ListBox listDbg2()
        {
            return lstDbg2;
        }

        private void lstDbg2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //game.pause = (int)numericUpDown1.Value;
        }
        public int Pause
        {
            get
            {
                return (int)numericUpDown1.Value;

            }
        }

        private void chkMove_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void tlsEditPattern_Click(object sender, EventArgs e)
        {
            switch (tlsEditPattern.Checked)
            {
                case true:
                    tlsТочкаОтсчета.Enabled=false;
                    tlsEditPattern.Checked=false;
                    break;
                case false:
                    tlsEditPattern.Checked = true;
                    tlsТочкаОтсчета.Enabled = true;
                    break;
            }
        }

     }
}
