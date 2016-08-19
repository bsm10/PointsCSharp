//#define DEBUG
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
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
            Width = Height-50;

            game = new Game(pbxBoard);
            //game.SetLevel(2);
            toolStripStatusLabel2.ForeColor = game.colorGamer1;
            toolStripStatusLabel2.Text = "Ход игрока";

            toolStripTextBox1.Text = game.iBoardSize.ToString();

            if(Properties.Settings.Default.Level==0) легкоToolStripMenuItem.Checked=true;
            if (Properties.Settings.Default.Level == 1) среднеToolStripMenuItem.Checked = true;
            if (Properties.Settings.Default.Level == 2) тяжелоToolStripMenuItem.Checked = true;
        }
        private void pbxBoard_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGame(e.Graphics);
        }
        private int player_move;//переменная хранит значение игрока который делает ход
        private bool autoplay;
        private void pbxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            game.MousePos = game.TranslateCoordinates(e.Location);
            Dot dot = new Dot(game.MousePos.X, game.MousePos.Y);
            if (game.MousePos.X > game.startX -0.5f & game.MousePos.Y > game.startY -0.5f)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                       
                        #region PatternEditor
                        #if DEBUG
                        if (game.EditMode==true)
                        {
                            if(ListPatterns.Contains(game.aDots[dot.x, dot.y])==false)
                            {
                                 ListPatterns.Add(game.aDots[dot.x, dot.y]);
                            }
                            if (PE_EmptyDot)
                            {
                                if (game.aDots[dot.x, dot.y].PatternsAnyDot) game.aDots[dot.x, dot.y].PatternsAnyDot = false;
                                game.aDots[dot.x, dot.y].PatternsEmptyDot = true;
                            }
                            if (PE_FirstDot)
                            {
                                game.aDots[dot.x, dot.y].PatternsFirstDot = true;
                                PE_FirstDot = false;
                                pbxBoard.Invalidate();
                                PE_MoveDot = true;
                                MessageBox.Show("Ставьте точку хода (ЛКМ), на точку отмеченную как пустую");
                                break;
                            }
                            if (PE_MoveDot)
                            {
                                game.aDots[dot.x, dot.y].PatternsMoveDot = true;
                                PE_MoveDot = false;
                            }
                            if (PE_AnyDot)
                            {
                                if (game.aDots[dot.x, dot.y].PatternsEmptyDot) game.aDots[dot.x, dot.y].PatternsEmptyDot = false;
                                game.aDots[dot.x, dot.y].PatternsAnyDot = true;
                            }
                            else if (!PE_EmptyDot & !PE_FirstDot & !PE_MoveDot & !PE_AnyDot)
                            //расстановка точек в режиме редактирования паттернов
                            {
                                MoveGamer(PE_Player, new Dot(game.MousePos.X, game.MousePos.Y, 1));
                                break;
                            }
                            break;
                        }
                        #endif
                        #endregion

                        #region Ходы игроков
                        if (game.aDots[game.MousePos.X, game.MousePos.Y].Own > 0) break;//предовращение хода если клик был по занятой точке
                        if (player_move == 2 | player_move == 0)
                        {
                        #if DEBUG
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift | game.Autoplay )
                            {
                                MoveGamer(1, new Dot(game.MousePos.X, game.MousePos.Y, 1));
                                break;
                            }
                        #endif
                            if (MoveGamer(1, new Dot(game.MousePos.X, game.MousePos.Y, 1)) > 0) break;
                            player_move = 1;
                            //Application.DoEvents();
                        }
                        //============Ход компьютера=================
                        if (player_move == 1)
                        {
                            game.Redraw = false;
                            if (MoveGamer(2) > 0) break;
                            //if (MoveGamer(2) > 0) break;
                            game.Redraw=true;
                            player_move = 2;
                        }
                        #endregion
                        break;
                 #if DEBUG //паттерны и пр.
                    case MouseButtons.Right:
                        //if (game.EditMode == true)
                        //{
                        //    MakePattern();
                        //    break;
                        //}
                        //else { 
                        //============Ход компьютера  в ручном режиме=================
                        MoveGamer(2, new Dot(dot.x, dot.y, 2));
                        //}
                        break;
                    case MouseButtons.Middle:
                        if (game.EditMode == true)
                        {
                            ListPatterns.Remove(game.aDots[dot.x, dot.y]);
                            break;
                        }

                            game.ListMoves.Remove(game.aDots[dot.x, dot.y]);
                            game.UndoMove(dot.x, dot.y, game.aDots);
                        break;
                 #endif
                }
            }
            //lstMoves.DataSource = null;
            //lstMoves.DataSource = game.ListMoves;
            //if (lstMoves.Items.Count > 0) lstMoves.SetSelected(lstMoves.Items.Count -1, true);
            //rtbStat.Text = game.Statistic();
        }
        public void pbxBoard_MouseWheel(object sender, MouseEventArgs e)
        {
            //int d = e.Delta / 120;
            //game.iScaleCoef -= d;
            //game.iBoardSize -= d;
            //if (game.iBoardSize < Game.iBoardSizeMin)
            //    game.iBoardSize = Game.iBoardSizeMin;
            //if (game.iBoardSize > Game.iBoardSizeMax)
            //    game.iBoardSize = Game.iBoardSizeMax;
            ////game.iMapSize = game.iBoardSize * game.iScaleCoef;
            //Invalidate(pbxBoard.Region);
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
                    //game.startX = (t.X -(e.X / (pbxBoard.ClientSize.Width / (game.iBoardSize + 1)))) -0.5f;
                    //game.startY = (t.Y -(e.Y / (pbxBoard.ClientSize.Height / (game.iBoardSize + 1)))) -0.5f;
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
            if (game.Redraw) pbxBoard.Invalidate();
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
            game.path_savegame = Application.CommonAppDataPath + @"\dots.dts";
            game.SaveGame();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void содержаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void autoplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(autoplayToolStripMenuItem.Checked) 
            {
                autoplayToolStripMenuItem.Checked=false;
                autoplay=false;
            }
            else
            {
                autoplayToolStripMenuItem.Checked=true;
                autoplay = true;
            }

            do
            {
                Application.DoEvents();
                if (MoveGamer(1) > 0) break;
                Application.DoEvents();
                if (MoveGamer(2) > 0) break;
            }
            while (autoplay);
            return;
        }
        private int MoveGamer(int Player, Dot pl_move=null)
        {
            toolStripStatusLabel2.ForeColor = Player == 1 ? game.colorGamer1 : game.colorGamer2;
            toolStripStatusLabel2.Text = "Ход игрока" + Player + "...";
            Application.DoEvents();
            if (pl_move== null) pl_move = game.PickComputerMove(game.LastMove);
            if (pl_move == null)
            {
                MessageBox.Show("Сдаюсь! \r\n" + game.Statistic());
                game.NewGame();
                return 1;
            } 
            pl_move.Own=Player;
            game.MakeMove(pl_move, game.aDots,Player);
            game.ListMoves.Add(pl_move);
            pbxBoard.Invalidate();
            statusStrip1.Refresh();
            int pl = Player == 1 ? 2 : 1;
            toolStripStatusLabel2.ForeColor = pl == 1 ? game.colorGamer1 : game.colorGamer2;
            toolStripStatusLabel2.Text = "Ход игрока" + pl + "...";
            if (game.GameOver())
            {
                MessageBox.Show("Game over! \r\n" + game.Statistic());
                return 1;
            }

            //lstMoves.DataSource = null;
            //lstMoves.DataSource = game.ListMoves;
            //if (lstMoves.Items.Count > 0) lstMoves.SetSelected(lstMoves.Items.Count -1, true);
            //rtbStat.Text = game.Statistic();
            return 0;
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

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.path_savegame = saveFileDialog1.FileName;
            game.SaveGame();
        }

        private void открытьПоследнююToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.path_savegame = Application.CommonAppDataPath + @"\dots.dts";
            game.LoadGame();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.path_savegame = openFileDialog1.FileName;
            game.LoadGame();
        }

        private void pbxBoard_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.aDots.Dots=game.aDots.Rotate90(game.aDots.Dots);
            pbxBoard.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.aDots.Dots=game.aDots.Rotate_Mirror_Horizontal(game.aDots.Dots);
            pbxBoard.Refresh();
        }

        private void редакторПаттерновToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolEditorPattern.Visible = toolEditorPattern.Visible ? false : true;
            game.EditMode=true;
        }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
            Activate();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            toolEditorPattern.Visible = false;
            game.EditMode = false;
        }


        private void tlsRedDot_CheckStateChanged(object sender, EventArgs e)
        {
            tlsBlueDot.Checked = tlsRedDot.Checked ? false : true;
        }

        private void tlsBlueDot_CheckStateChanged(object sender, EventArgs e)
        {
            tlsRedDot.Checked = tlsBlueDot.Checked ? false : true;
        }

        private void tlsMakePattern_Click(object sender, EventArgs e)
        {
            MakePattern();
        }

        private void tlsТочкаОтсчета_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsТочкаОтсчета.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsПустая.Checked = false;
            }

        }

        private void tlsКромеВражеской_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsКромеВражеской.Checked)
            {
                tlsПустая.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
            }

        }

        private void tlsТочкаХода_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsТочкаХода.Checked)
            {
                tlsПустая.Checked = false;
                tlsКромеВражеской.Checked = false;
                tlsТочкаОтсчета.Checked = false;
            }

        }

        private void tlsПустая_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsПустая.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
            }

        }


    }  
}
