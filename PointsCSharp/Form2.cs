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

        public TextBox txtboxPattern
        {
            get
            {
                return this.txtPattern;
            }
        }

        public ListBox listDbg2()
        {
            return lstDbg2;
        }
        public int Pause
        {
            get
            {
                return (int)numericUpDown1.Value;

            }
        }
        private void tlsEditPattern_Click(object sender, EventArgs e)
        {
            switch (tlsEditPattern.Checked)
            {
                case true:
                    tlsEditPattern.Checked = false;
                    tlsТочкаОтсчета.Enabled=false;
                    tlsПустая.Enabled = false;
                    tlsПустая.Checked = false;
                    tlsТочкаХода.Enabled = false;
                    tlsКромеВражеской.Enabled = false;
                    
                    break;
                case false:
                    tlsEditPattern.Checked = true;
                    tlsТочкаОтсчета.Enabled = true;
                    tlsПустая.Enabled = true;
                    tlsПустая.Checked = true;
                    tlsТочкаХода.Enabled = true;
                    tlsКромеВражеской.Enabled = true;
                    break;
            }
        }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
            Activate();
        }

        private void tlsТочкаОтсчета_CheckedChanged(object sender, EventArgs e)
        {
            if (tlsТочкаОтсчета.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsПустая.Checked = false;
            }
        }

        private void tlsПустая_CheckedChanged(object sender, EventArgs e)
        {
            if (tlsПустая.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
            }
        }

        private void tlsКромеВражеской_CheckedChanged(object sender, EventArgs e)
        {
            if (tlsКромеВражеской.Checked)
            {
                tlsПустая.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
            }
        }

        private void tlsТочкаХода_CheckedChanged(object sender, EventArgs e)
        {
            if(tlsТочкаХода.Checked)
            {
                tlsПустая.Checked = false;
                tlsКромеВражеской.Checked = false;
                tlsТочкаОтсчета.Checked = false;
            }
        }

        private void tlsКромеВражеской_Click(object sender, EventArgs e)
        {
            
        }

        private void tlsMakePattern_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDrawPattern_Click(object sender, EventArgs e)
        {

        }
    }
}
