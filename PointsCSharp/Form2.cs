using System;
using System.Windows.Forms;

namespace DotsGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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
    }
}
