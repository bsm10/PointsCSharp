//#define DEBUG
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DotsGame
{
    public partial class Form1 : Form
    {
        public GameEngine game;
        private Point t;

        public Form1()
        {
            InitializeComponent();

            int Xres = Screen.PrimaryScreen.WorkingArea.Width;
            int Yres = Screen.PrimaryScreen.WorkingArea.Height;
            float scl_coef=(float)Xres/ Yres;

            Height = 800; //4 * Yres / 5;
            Width = 400;//Height-50;

            game = new GameEngine(pbxBoard);

            //game.SetLevel(2);
            toolStripStatusLabel2.ForeColor = game.colorGamer1;
            toolStripStatusLabel2.Text = "Ход игрока";

            scl_coef = (float)pbxBoard.ClientSize.Height/ pbxBoard.ClientSize.Width;
            Properties.Settings.Default.BoardHeight = (int)Math.Round(Properties.Settings.Default.BoardWidth * (double)scl_coef);

            toolStripTextBox1.Text = Properties.Settings.Default.BoardWidth.ToString();
            toolStripTextBox2.Text = Properties.Settings.Default.BoardHeight.ToString();

            if (Properties.Settings.Default.Level==0) легкоToolStripMenuItem.Checked=true;
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
                            if(ListPatterns.Contains(game.gameDots[dot.x, dot.y])==false)
                            {
                                 ListPatterns.Add(game.gameDots[dot.x, dot.y]);
                            }
                            if (PE_EmptyDot)
                            {
                                if (game.gameDots[dot.x, dot.y].PatternsAnyDot) game.gameDots[dot.x, dot.y].PatternsAnyDot = false;
                                game.gameDots[dot.x, dot.y].PatternsEmptyDot = true;
                            }
                            if (PE_FirstDot)
                            {
                                if (game.lstDotsInPattern.Where(d => d.PatternsFirstDot).Count() == 0)
                                game.gameDots[dot.x, dot.y].PatternsFirstDot = true;
                                break;
                            }
                            if (PE_MoveDot)
                            {
                                if (game.lstDotsInPattern.Where(d=>d.PatternsMoveDot).Count()==0)
                                    game.gameDots[dot.x, dot.y].PatternsMoveDot = true;
                                
                                //PE_MoveDot = false;
                            }
                            if (PE_AnyDot)
                            {
                                if (game.gameDots[dot.x, dot.y].PatternsEmptyDot) game.gameDots[dot.x, dot.y].PatternsEmptyDot = false;
                                game.gameDots[dot.x, dot.y].PatternsAnyDot = true;
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
                        if (game.gameDots[game.MousePos.X, game.MousePos.Y].Own > 0) break;//предовращение хода если клик был по занятой точке
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
                 #if DEBUG 
                    case MouseButtons.Right:
                        if (game.EditMode == true)
                        {
                            break;
                        }
                        //============Ход компьютера  в ручном режиме=================
                        MoveGamer(2, new Dot(dot.x, dot.y, 2));
                        break;
                    case MouseButtons.Middle:
                        if (game.EditMode == true)
                        {
                            ListPatterns.Remove(game.gameDots[dot.x, dot.y]);
                            game.gameDots[dot.x, dot.y].PatternsRemove();

                            //break;
                        }
                        //game.gameDots.ListMoves.Remove(game.gameDots[dot.x, dot.y]);
                        game.gameDots.UndoMove(dot);
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
            toolStripStatusLabel1.Text = p.X + " : " + p.Y + "; " + "IndexDot " + game.gameDots.IndexDot(p.X, p.Y);
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
            ChangeBoardSize();
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            ChangeBoardSize();
        }

        private void ChangeBoardSize()
        {
            decimal x, y;

            if (decimal.TryParse(toolStripTextBox1.Text, out x) &
                decimal.TryParse(toolStripTextBox2.Text, out y))
                game.ResizeBoard((int)x, (int)y);

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
            game.NewGame(Properties.Settings.Default.BoardWidth, Properties.Settings.Default.BoardHeight);
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
            if (pl_move== null) pl_move = game.gameDots.PickComputerMove(game.gameDots.LastMove);
            if (pl_move == null)
            {
                MessageBox.Show("Сдаюсь! \r\n" + game.Statistic());
                game.NewGame(Properties.Settings.Default.BoardWidth, Properties.Settings.Default.BoardHeight);
                return 1;
            } 
            pl_move.Own=Player;
            game.gameDots.MakeMove(pl_move);
            pbxBoard.Invalidate();
            statusStrip1.Refresh();
            int pl = Player == 1 ? 2 : 1;
            toolStripStatusLabel2.ForeColor = pl == 1 ? game.colorGamer1 : game.colorGamer2;
            toolStripStatusLabel2.Text = "Ход игрока" + pl + "...";
            if (game.gameDots.FreeDots.Count == 0)
            {
                MessageBox.Show("Game over! \r\n" + game.Statistic());
                return 1;
            }

            lstMoves.DataSource = null;
            lstMoves.DataSource = game.gameDots.ListMoves;
            if (lstMoves.Items.Count > 0) lstMoves.SetSelected(lstMoves.Items.Count - 1, true);
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
                game.gameDots.SetLevel(1);
            }
        }
        private void легкоToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (легкоToolStripMenuItem.Checked)
            {
                тяжелоToolStripMenuItem.Checked = false;
                среднеToolStripMenuItem.Checked = false;
                game.gameDots.SetLevel(0);
            }
        }
        private void тяжелоToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (тяжелоToolStripMenuItem.Checked)
            {
                легкоToolStripMenuItem.Checked = false;
                среднеToolStripMenuItem.Checked = false;
                game.gameDots.SetLevel(2);
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
            if (game.lstDotsInPattern.Where(dot=>dot.PatternsFirstDot).Count()==0 |
            game.lstDotsInPattern.Where(dot => dot.PatternsMoveDot).Count() == 0)
            {
                MessageBox.Show("Проверь первую точку (FirstDot) и точку куда ходить (MoveDot)");
                return;
                //Ставьте точку хода (ЛКМ), на точку отмеченную как пустую
            }
            game.MakePattern();
            
        }
        private void tlsТочкаОтсчета_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsТочкаОтсчета.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsПустая.Checked = false;
                tlsDotClean.Checked = false;
            }

        }
        private void tlsКромеВражеской_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsКромеВражеской.Checked)
            {
                tlsПустая.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
                tlsDotClean.Checked = false;
            }

        }
        private void tlsТочкаХода_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsТочкаХода.Checked)
            {
                tlsПустая.Checked = false;
                tlsКромеВражеской.Checked = false;
                tlsТочкаОтсчета.Checked = false;
                tlsDotClean.Checked = false;
            }

        }
        private void tlsПустая_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsПустая.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
                tlsDotClean.Checked = false;
            }

        }
        private void tlsDotClean_CheckStateChanged(object sender, EventArgs e)
        {
            if (tlsDotClean.Checked)
            {
                tlsКромеВражеской.Checked = false;
                tlsТочкаХода.Checked = false;
                tlsТочкаОтсчета.Checked = false;
                tlsПустая.Checked = false;
            }
        }
        private void tlsMirror_Click(object sender, EventArgs e)
        {
            game.gameDots.Dots = game.gameDots.Rotate_Mirror_Horizontal(game.gameDots.Dots);
            pbxBoard.Refresh();

        }
        private void tlsRotate90_Click(object sender, EventArgs e)
        {
            game.gameDots.Dots = game.gameDots.Rotate90(game.gameDots.Dots);
            pbxBoard.Refresh();
        }

            //#region Pattern Editor
            public List<Dot> ListPatterns
            {
                get { return game.lstDotsInPattern; }
            }
            public bool PE_FirstDot
            {
                get { return tlsТочкаОтсчета.Checked; }
                set { tlsТочкаОтсчета.Checked = value; }
            }
            public bool PE_EmptyDot
            {
                get { return tlsПустая.Checked; }
                set { tlsПустая.Checked = value; }

            }
            public bool PE_AnyDot
            {
                get { return tlsКромеВражеской.Checked; }
                set { tlsКромеВражеской.Checked = value; }

            }
            public bool PE_MoveDot
            {
                get { return tlsТочкаХода.Checked; }
                set { tlsТочкаХода.Checked = value; }

            }

            public int PE_Player
            {
                get
                {
                    return tlsRedDot.Checked ? 1 : 2;
                }

            }

            private void tlsDist_Click(object sender, EventArgs e)
            {
               frmDlgPE dlg = new frmDlgPE();
               
               dlg.ShowDialog();
            }

            //#endregion

    }
}
