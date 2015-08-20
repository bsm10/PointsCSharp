//#define DEBUG
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DotsGame
{

    public partial class Form1 : Form
    {
//#if DEBUG
//        public Form f = new Form2();
//        public ListBox lstDbg1;
//        public ListBox lstDbg2;
//        public TextBox txtDbg;
//        public TextBox txtDot;
//#endif
    Game game;
    private Point t;
   // private int own;

        public Form1()
        {
            InitializeComponent();

            int Xres = Screen.PrimaryScreen.WorkingArea.Width;
            int Yres = Screen.PrimaryScreen.WorkingArea.Height;

            Height = 2 * Yres / 3;
            Width = Height;

//#if DEBUG
//            f.Show(this);

//            MoveDebugWindow();
//            //lstDbg1 = (ListBox)f.Controls.Find("lstDbg1", false)[0];
//            //lstDbg2 = (ListBox)f.Controls.Find("lstDbg2", false)[0];
//            txtDbg = (TextBox)f.Controls.Find("txtDebug", false)[0];
//            txtDot = (TextBox)f.Controls.Find("txtDotStatus", false)[0];
//#endif
            game = new Game(pbxBoard);

            toolStripTextBox1.Text = game.iBoardSize.ToString();
        }


        private void pbxBoard_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGame(e.Graphics);
        }
        private void pbxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            game.MousePos = game.TranslateCoordinates(e.Location);
            Dot dot = new Dot(game.MousePos);
            int res;
            if (game.MousePos.X > game.startX - 0.5f & game.MousePos.Y > game.startY - 0.5f)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                       //own=1;//игрок1
                       res = game.MakeMove(new Dot(game.MousePos.X, game.MousePos.Y, 1, null));
                       pbxBoard.Invalidate();
                       if (game.GameOver())
                        {
                            MessageBox.Show("Game over!");
                            game.newGame();
                            return;
                        }
                        //============Ход компьютера=================
                        Dot move = game.PickComputerMove();
                        move.Own = 2;
                        res = game.MakeMove(move);
                        pbxBoard.Invalidate();
                        if (game.GameOver())
                        {
                            MessageBox.Show("Game over!");
                            game.newGame();
                            return;
                        }
                        break;

                    case MouseButtons.Right:
                        //============Ход компьютера  в ручном режиме=================
                        //res = game.MakeMove(new Dot(game.MousePos.X, game.MousePos.Y, 2, null));
                        break;
                }
                //res = game.MakeMove(new Dot(game.MousePos.X, game.MousePos.Y, own, null));
                //txtDbg.Text = "Игрок1 окружил точек: " + 0 + "; \r\n" +
                //              "Захваченая площадь: " + game.square1.ToString() + "; \r\n" +
                //              "Игрок2 окружил точек: " + 0+ "; \r\n" +
                //              "Захваченая площадь: " + game.square2.ToString() + "; \r\n" +
                //              "Игрок1 точек поставил: " + game.count_dot1.ToString() + "; \r\n" +
                //              "Игрок2 точек поставил: " + game.count_dot2.ToString() + "; \r\n";


            }
        }
        private void pbxBoard_MouseMove(object sender, MouseEventArgs e)
        {
            
            Point p = game.TranslateCoordinates(e.Location);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    game.MousePos = p;
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    game.startX = (t.X - (e.X / (pbxBoard.ClientSize.Width / (game.iBoardSize + 1)))) - 0.5f;
                    game.startY = (t.Y - (e.Y / (pbxBoard.ClientSize.Height / (game.iBoardSize + 1)))) - 0.5f;
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                
                    break;
            }

#if DEBUG
            game.Statistic(p.X,p.Y);
#else
               
#endif
           lblStatus.Text = p.X + " : " + p.Y;
           pbxBoard.Invalidate(); 
        }
        private void pbxBoard_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    t = game.TranslateCoordinates(e.Location);
                    break;
                case MouseButtons.Middle:
                    t = game.TranslateCoordinates(e.Location);
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
         {
            pbxBoard.Top = menuStrip.Height + 10;
            pbxBoard.Height = Height - menuStrip.Height - 80;
            pbxBoard.Left = 10;
            pbxBoard.Width = Width - 30;
            lblStatus.Left = 10;
            lblStatus.Top = pbxBoard.Top + pbxBoard.Height + 2;
            numericUpDown1.Top = lblStatus.Top;
            numericUpDown1.Left = pbxBoard.Width*2/3;
            menuStrip.Invalidate();
            pbxBoard.Invalidate();
#if DEBUG
            if (game!=null) game.MoveDebugWindow(Top, Left, Width);
#endif
        }
        private void Form1_Move(object sender, EventArgs e)
        {
        #if DEBUG
            game.MoveDebugWindow(Top,Left,Width);
        #endif
        }
//#if DEBUG
//        private void MoveDebugWindow()
//        {
//            f.Top = Top;
//            f.Left = Left + Width;
//        }
//#endif
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            ChangeBoardSize(toolStripTextBox1.Text);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }
        private void pbxBoard_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }
        private void pbxBoard_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();

        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.newGame();
        }
        private void антиалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (антиалToolStripMenuItem.Checked)
            {
                case true:
                   антиалToolStripMenuItem.Checked=false;
                   break;
                case false:
                    антиалToolStripMenuItem.Checked=true;
                    break; 
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ChangeBoardSize(numericUpDown1.Value.ToString() );
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }
        private void опрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Dots v.";
            const string caption = "Borys Malyi, bsm10@i.ua";
            MessageBox.Show(message+Application.ProductVersion, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }
        private void ChangeBoardSize(string strSize)
        {
            decimal i;
            if (decimal.TryParse(strSize, out i))
            {
                game.iBoardSize = (int)i;
                pbxBoard.Invalidate();
            }
        }
        private void цветКурсораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color=game.colorCursor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                game.colorCursor = MyDialog.Color;
            Properties.Settings.Default.Color_Cursor = game.colorCursor;
            Properties.Settings.Default.Save();
            pbxBoard.Invalidate();  
        }
        private void цветИгрокаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color = game.colorGamer1;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                game.colorGamer1 = MyDialog.Color;
            Properties.Settings.Default.Color_Gamer1 = game.colorGamer1;
            Properties.Settings.Default.Save();
            pbxBoard.Invalidate();
        }
        private void цветПротивникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color = game.colorGamer2;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                game.colorGamer2 = MyDialog.Color;
            Properties.Settings.Default.Color_Gamer2 = game.colorGamer2;
            Properties.Settings.Default.Save();
            pbxBoard.Invalidate();
        }


    }
}
