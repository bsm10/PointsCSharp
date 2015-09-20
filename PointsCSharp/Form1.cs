//#define DEBUG
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
namespace DotsGame
{
    public partial class Form1 : Form
    {
        public Game game;
        private Point t;

        public Form1()
        {
            InitializeComponent();


            int Xres = Screen.PrimaryScreen.WorkingArea.Width;
            int Yres = Screen.PrimaryScreen.WorkingArea.Height;
            float scl_coef=(float)Xres/ Yres;

            Height = 4 * Yres / 5;
            Width = Height-50;// (int)(Height * scl_coef);//Xres / 3;

            game = new Game(pbxBoard);
            game.SetLevel(2);
            toolStripStatusLabel2.ForeColor = game.colorGamer1;
            toolStripStatusLabel2.Text = "Ход игрока";

            toolStripTextBox1.Text = game.iBoardSize.ToString();
        }
        private void pbxBoard_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGame(e.Graphics);
        }
        private int player_move;//переменная хранит значение игрока который делает ход
        private void pbxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            game.MousePos = game.TranslateCoordinates(e.Location);
            Dot dot = new Dot(game.MousePos);
            if (game.MousePos.X > game.startX - 0.5f & game.MousePos.Y > game.startY - 0.5f)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        #region PatternEditor
                        if (game.PE_On==true)
                        {
                            game.ListPatterns.Add(game.aDots[dot.x, dot.y]);
                            game.aDots[dot.x, dot.y].Marked = true;
                            
                            if (game.PE_FirstDot)
                            {
                                game.aDots[dot.x, dot.y].PatternsFirstDot = true;
                                game.PE_FirstDot = false;
                                pbxBoard.Invalidate();
                                game.PE_MoveDot = true;
                                MessageBox.Show("Ставьте точку хода (ЛКМ), на точку отмеченную как пустую");
                                break;
                            }
                            if (game.PE_MoveDot)
                            {
                                game.aDots[dot.x, dot.y].PatternsMoveDot = true;
                                game.PE_MoveDot = false;
                            }

                            break;
                        }
                        #endregion
                        #region Ходы игроков
                        if (game.aDots[game.MousePos.X, game.MousePos.Y].Own > 0) break;//предовращение хода если клик был по занятой точке
                        if (player_move == 2 | player_move == 0)
                        {
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift | game.Autoplay)
                            {
                                MoveGamer(1, new Dot(game.MousePos.X, game.MousePos.Y, 1, null));
                                break;
                            }
                            
                            if (MoveGamer(1, new Dot(game.MousePos.X, game.MousePos.Y, 1, null)) > 0) break;
                            player_move = 1;
                            Application.DoEvents();
                        }
                        //============Ход компьютера=================
                        if (player_move == 1)
                        {
                            if (MoveGamer(2) > 0) break;
                            player_move = 2;
                        }
                        #endregion
                        break;
                    case MouseButtons.Right:
                        if (game.PE_On == true)
                        {
                            game.MakePattern();
                            break;
                        }
                        else { 
                        //============Ход компьютера  в ручном режиме=================
                        MoveGamer(2, new Dot(dot.x, dot.y, 2, null));
                        }
                        break;
                    case MouseButtons.Middle:
                            game.ListMoves.Remove(game.aDots[dot.x, dot.y]);
                            game.UndoMove(dot.x, dot.y);
                        break;
                }
            }
            //lstMoves.DataSource = null;
            //lstMoves.DataSource = game.ListMoves;
            //if (lstMoves.Items.Count > 0) lstMoves.SetSelected(lstMoves.Items.Count - 1, true);
            //rtbStat.Text = game.Statistic();
        }
        public void pbxBoard_MouseWheel(object sender, MouseEventArgs e)
        {
            int d = e.Delta / 120;
            game.iScaleCoef -= d;
            game.iBoardSize -= d;
            if (game.iBoardSize < Game.iBoardSizeMin)
                game.iBoardSize = Game.iBoardSizeMin;
            if (game.iBoardSize > Game.iBoardSizeMax)
                game.iBoardSize = Game.iBoardSizeMax;
            //game.iMapSize = game.iBoardSize * game.iScaleCoef;
            Invalidate(pbxBoard.Region);
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
                    //тягает поле, раскоментить если нужна прокрутка поля
                    //game.startX = (t.X - (e.X / (pbxBoard.ClientSize.Width / (game.iBoardSize + 1)))) - 0.5f;
                    //game.startY = (t.Y - (e.Y / (pbxBoard.ClientSize.Height / (game.iBoardSize + 1)))) - 0.5f;
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:

                    break;
            }

#if DEBUG
            game.Statistic(p.X, p.Y);
#else
               
#endif
            toolStripStatusLabel1.Text = p.X + " : " + p.Y;
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
            menuStrip.Invalidate();
            pbxBoard.Invalidate();
#if DEBUG
            if (game != null) game.MoveDebugWindow(Top, Left, Width);
#endif
        }
        private void Form1_Move(object sender, EventArgs e)
        {
#if DEBUG
            game.MoveDebugWindow(Top, Left, Width);
#endif
        }
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
            //Cursor.Show();
        }
        private void pbxBoard_MouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();

        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.NewGame();
            //lstMoves.DataSource = null;
        }
        private void антиалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (антиалToolStripMenuItem.Checked)
            {
                case true:
                    антиалToolStripMenuItem.Checked = false;
                    break;
                case false:
                    антиалToolStripMenuItem.Checked = true;
                    break;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
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
            MessageBox.Show(message + Application.ProductVersion, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }
        private void ChangeBoardSize(string strSize)
        {
            decimal i;
            if (decimal.TryParse(strSize, out i))
            {
                game.ResizeBoard((int)i);
            }
        }
        private void цветКурсораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.Color = game.colorCursor;
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
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SaveGame();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.LoadGame();
            //lstMoves.DataSource = null;
            //lstMoves.DataSource = game.ListMoves;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void содержаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chkMove_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //game.SkillDepth = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            //game.SkillLevel = (int)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            //game.SkillNumSq = (int)numericUpDown4.Value;
        }

        private void lstMoves_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            //game.SkillLevel = (int)toolStripComboBox1.Selected.ToString;
        }

        private void autoplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            do
            {
                Application.DoEvents();
                if (MoveGamer(1) > 0) break;
                Application.DoEvents();
                if (MoveGamer(2) > 0) break;
            }
            while (true);
            return;
        }

        private int MoveGamer(int Player, Dot pl_move=null)
        {
            if (pl_move== null) pl_move = game.PickComputerMove(game.LastMove);
            pl_move.Own = Player;
            game.MakeMove(pl_move);
            game.ListMoves.Add(pl_move);
            pbxBoard.Invalidate();
            statusStrip1.Refresh();
            toolStripStatusLabel2.ForeColor = Color.BurlyWood;
            toolStripStatusLabel2.Text = "Ход игрока" + Player;
            if (game.GameOver())
            {
                MessageBox.Show("Game over!");
                return 1;
            }
            //lstMoves.DataSource = null;
            //lstMoves.DataSource = game.ListMoves;
            //if (lstMoves.Items.Count > 0) lstMoves.SetSelected(lstMoves.Items.Count - 1, true);
            //rtbStat.Text = game.Statistic();
            return 0;
        }

        private void выделитьШаблонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (выделитьШаблонToolStripMenuItem.Checked)
            //    выделитьШаблонToolStripMenuItem.Checked = false;
            //else
            //{
            //    выделитьШаблонToolStripMenuItem.Checked = true;
            //    game.ListPatterns.Clear();
            //}
        }

        private void точкаОтсчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (точкаОтсчетаToolStripMenuItem.Checked)
                точкаОтсчетаToolStripMenuItem.Checked = false;
            else
            {
                точкаОтсчетаToolStripMenuItem.Checked = true;
                точкаХодаToolStripMenuItem.Checked = false;
            }
        }

        private void точкаХодаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (точкаХодаToolStripMenuItem.Checked)
                точкаХодаToolStripMenuItem.Checked = false;
            else
            {
                точкаХодаToolStripMenuItem.Checked = true;
                точкаОтсчетаToolStripMenuItem.Checked = false;
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
           Activate();
        }



        private void среднеToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (среднеToolStripMenuItem.Checked)
            {
                тяжелоToolStripMenuItem.Checked = false;
                легкоToolStripMenuItem.Checked = false;
                game.SetLevel(1);
            }
        }

        private void легкоToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (легкоToolStripMenuItem.Checked)
            {
                тяжелоToolStripMenuItem.Checked = false;
                среднеToolStripMenuItem.Checked = false;
                game.SetLevel(0);
            }
        }

        private void тяжелоToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (тяжелоToolStripMenuItem.Checked)
            {
                легкоToolStripMenuItem.Checked = false;
                среднеToolStripMenuItem.Checked = false;
                game.SetLevel(2);
            }
        }
    }  
}
