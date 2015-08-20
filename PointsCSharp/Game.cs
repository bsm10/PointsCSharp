//#define DEBUG
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DotsGame
{
    public partial class Game
    {
        //private const int NUM_SQUARES = 9;
        //private int GameInProgress;
        //  Player values.
        private const int PLAYER_DRAW = -1;
        private const int PLAYER_NONE = 0;
        private const int PLAYER_HUMAN = 1;
        private const int PLAYER_COMPUTER = 2;
        private const int NUM_PLAYERS = 2;
        //  Values of board positions
        private const int VALUE_HIGH = 5;
        private const int VALUE_WIN = 4;
        private const int VALUE_UNKNOWN = 3;
        private const int VALUE_DRAW = 2;
        private const int VALUE_LOSE = 1;
        private const int VALUE_LOW = 0;
        //  Variables for precomputed moves and responses.
        private const int NUM_PATTERNS = 10;
        private Dot[] pattern;
        private Dot[] pat_move;
        private Dot last_move;
        public int SkillLevel = 10000;
        //-------------------------------------------------
        public int iScaleCoef = 1;//- коэффициент масштаба
        public int iBoardSize = 5;//- количество клеток квадрата в длинну
        public int iMapSize;//- количество клеток квадрата в длинну

        public float startX = -0.5f, startY = -0.5f;
        public ArrayDots aDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        private Links[] lnks; //массив где хранятся связи между точками, учавствует в отрисовке
        private Links[] temp_lnks; //массив где хранятся связи между точками, если связи не окружили точку - обнуляется, если да, то добавляется в lnks
        private Point[] pts;
        
        public Color colorGamer1 = Properties.Settings.Default.Color_Gamer1,
                           colorGamer2 = Properties.Settings.Default.Color_Gamer2,
                           colorCursor = Properties.Settings.Default.Color_Cursor;
        private float PointWidth = 0.25f;
        public Pen boardPen = new Pen(Color.DarkSlateBlue, 0.05f);
        private SolidBrush drawBrush = new SolidBrush(Color.MediumPurple);
        public Font drawFont = new Font("Arial", 0.22f);


        public Point MousePos;
        private int nRelation = 0;

        //statistic
        public float square1;//площадь занятая игроком1
        public float square2;
        public int count_blocked;//счетчик количества окруженных точек
        public int count_blocked1, count_blocked2;
        public int count_dot1, count_dot2;//количество поставленных точек
       
        private PictureBox pbxBoard;

#if DEBUG
        public Form f = new Form2();
        public ListBox lstDbg1;
        public ListBox lstDbg2;
        public TextBox txtDbg;
        public TextBox txtDot;
#endif

        public Game(PictureBox CanvasGame)
        {
            pbxBoard = CanvasGame;
            newGame();
        }
        /*1) Забить паттерны в массив, поик паттернов в аДотс, расстановка веса позиции

        */
        //  ************************************************
        //  Initialize patterns. The moves and replies were
        //  obtained by letting the program run through the
        //  entire game tree for each move. We store the
        //  results here to save time in later games.
        //  ************************************************
        //private void InitializePatterns()
        //{
        //    Dot pat;
        //    int i;
        //    //  Blank all the patterns.
        //    for (pat = 1; (pat <= NUM_PATTERNS); pat++)
        //    {
        //        for (i = 1; (i <= NUM_SQUARES); i++)
        //        {
        //            pattern(pat, i) = PLAYER_NONE;
        //        }
        //    }
        //    //  If the opponent moves first, respond accordingly.
        //    for (pat = 1; (pat <= NUM_SQUARES); pat++)
        //    {
        //        pattern(pat, pat) = PLAYER_HUMAN;
        //    }
        //    pat_move(1) = 5;
        //    pat_move(2) = 1;
        //    pat_move(3) = 5;
        //    pat_move(4) = 1;
        //    pat_move(5) = 1;
        //    pat_move(6) = 3;
        //    pat_move(7) = 5;
        //    pat_move(8) = 2;
        //    pat_move(9) = 5;
        //    //  If we move first, take the center square.
        //    pat_move(NUM_PATTERNS) = 5;
        //}
        //private void CheckPatterns(int best_move)
        //{
        //    int pat;
        //    int i;
        //    for (pat = 1; (pat <= NUM_PATTERNS); pat++)
        //    {
        //        for (i = 1; (i <= NUM_SQUARES); i++)
        //        {
        //            if ((pattern(pat, i) != Board(i)))
        //            {
        //                break;
        //            }
        //        }
        //        //  See if we matched a pattern
        //        if ((i > NUM_SQUARES))
        //        {
        //            best_move = pat_move(pat);
        //            return;
        //        }
        //    }
        //    best_move = -1;
        //}
        //  ************************************************
        //  Use a minimax strategy to pick a move.
        // 
        //  For each possible move the computer could make,
        //  compute the best possible response the human
        //  could make. Select the move for which this
        //  response is worst (for the human).
        // 
        //  Moves get values:
        //        VALUE_WIN       This player will win.
        //        VALUE_UNKNOWN   Can't tell who will win.
        //        VALUE_DRAW      There will be a draw.
        //        VALUE_LOSE      The other player will win.
        //  ************************************************
        public Dot PickComputerMove()
        {
            Dot best_move;
            //  The best move we can make.
            int best_value;
            //  The best move's value.
            best_move = null;
            best_value = VALUE_LOW;
            int depth=0;
            //  If we did not find a pattern, look for the best
            //  possible move.
            #if DEBUG
                lstDbg1.Items.Clear();
            #endif
            if (best_move ==null)
            {
                BoardValue(ref best_move, ref best_value, PLAYER_COMPUTER, PLAYER_HUMAN, ref depth);
            }
            if (best_move == null)
            {
                var qry = from Dot d in aDots
                          where d.Own == PLAYER_NONE
                          select d;

                    best_move=qry.First();
                
            }
            CheckBlocked();
            return best_move;
        }
        //  ************************************************
        //  Return the player who has won. If it is a draw,
        //  return PLAYER_DRAW. If the game is not yet over,
        //  return PLAYER_NONE.
        //  ************************************************
        private int Winner()
        {
            var count_bl1 = from Dot d in aDots // сколько точек окружил игрок1
                      where d.Blocked == true & d.Own==2
                      select d;
            
            var count_bl2 = from Dot d in aDots // сколько точек окружил комп
                                 where d.Blocked == true & d.Own == 1
                                 select d;
            int bl1 = count_bl1.Count();
            int bl2 = count_bl2.Count();
            //проверяем сколько точек окружено после сделанного хода
            if (CheckBlocked() == 0) return PLAYER_NONE;
            //выбираем блокированные точки игроком1
            var qry = from Dot d in aDots
                      where d.Blocked == true & d.Own!=1
                      select d;
            if (bl1 - qry.Count() != 0)
            {
                //MessageBox.Show("Human");
                return PLAYER_HUMAN; 
            }
            var qry1 = from Dot d in aDots
                      where d.Blocked == true & d.Own != 2
                      select d;
            if (bl2 - qry1.Count() != 0)
            {
               // MessageBox.Show("Computer"); 
                return PLAYER_COMPUTER;
            }
            return PLAYER_NONE;
        }

        private void BoardValue(ref Dot best_move, ref int best_value, int pl1, int pl2, ref int depth)
        {
            int pl;
            Dot good_i;
            int good_value;
            Dot enemy_i = null;//=new Dot(0,0);
            int enemy_value = 0;
            Application.DoEvents();
            if ((depth >= SkillLevel))
            {
                best_value = VALUE_UNKNOWN;
                return;
            }
            pl = Winner();//Это должно быть условием выхода из рекурсии
            if (pl != PLAYER_NONE)
            {
                if (pl == pl1)
                {
                    best_value = VALUE_WIN;
                }
                else if (pl == pl2)
                {
                    best_value = VALUE_LOSE;
                }
                else
                {
                    best_value = VALUE_DRAW;
                }
                return;
            }
            good_i = null;//-1;
            good_value = VALUE_HIGH;

            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE// & d.Blocked == false
                      select d;

            Dot[] ad;
            ad = qry.ToArray();
            int i = ad.Length;
            if (i != 0)
            {
                foreach (Dot d in ad)
                {
                    //делаем ход
                    d.Own = pl1;
                    
                    //-----показывает проверяемые ходы--------
                        
                        #if DEBUG
                            //Pause(1000);
                            
                            if (d.Own == 2) lstDbg1.Items.Add(d.Own + " - " + d.x  + ":" + d.y) ;
                        #endif
                    //-----------------------------------
                    depth++;
                    //теперь ходит другой игрок
                    BoardValue(ref enemy_i, ref enemy_value, pl2, pl1, ref depth);
                    //отменить ход
                    d.Own = PLAYER_NONE;
                    //Unblocked();
                    //ScanForBlocked();
                    if (enemy_value == VALUE_UNKNOWN) break;
                    //  See if this is lower than the previous best.
                    if (enemy_value < good_value)
                    {
                        good_i = d;
                        good_value = enemy_value;
                        //  If we will win, things can get no better
                        //  so take the move.
                        if (good_value <= VALUE_LOSE)
                        {
                            //break;
                        }
                    }
                }
                //  Translate the opponent's value into ours.
                if (good_value == VALUE_WIN)
                {
                    //  Opponent wins, we lose.
                    best_value = VALUE_LOSE;
                }
                else if (enemy_value == VALUE_LOSE)
                {
                    //  Opponent loses, we win.
                    best_value = VALUE_WIN;
                }
                else
                {
                    //  DRAW and UNKNOWN are the same for both players.
                    best_value = good_value;
                }
                best_move = good_i;
            }
        }
        public void Statistic()
        {
            txtDbg.Text = "Игрок1 окружил точек: " + 0 + "; \r\n" +
              "Захваченая площадь: " + square1.ToString() + "; \r\n" +
              "Игрок2 окружил точек: " + 0 + "; \r\n" +
              "Захваченая площадь: " + square2.ToString() + "; \r\n" +
              "Игрок1 точек поставил: " + count_dot1.ToString() + "; \r\n" +
              "Игрок2 точек поставил: " + count_dot2.ToString() + "; \r\n";
        }
        public void Statistic(int x, int y)
        {
            if (aDots.Contains(x, y))
            {
                txtDot.Text = "Blocked: " + aDots[x, y].Blocked + "\r\n" +
                              "CountRelations: " + aDots[x, y].CountRelations + "\r\n" +
                              "FirstDot: " + aDots[x, y].FirstDot + "\r\n" +
                              "Fixed: " + aDots[x, y].Fixed + "\r\n" +
                              "IndexDot: " + aDots[x, y].IndexDot + "\r\n" +
                              "IndexRelation: " + aDots[x, y].IndexRelation + "\r\n" +
                              "InRegion: " + aDots[x, y].InRegion + "\r\n" +
                              "Own: " + aDots[x, y].Own + "\r\n" +
                              "X: " + aDots[x, y].x + "; Y: " + aDots[x, y].y;
            }

        }

        private void Pause(int ms)
        {
            pbxBoard.Invalidate();
            System.Threading.Thread.Sleep(ms);
        }
        public void newGame()
        {
            iMapSize = iBoardSize * iScaleCoef;
            aDots = new ArrayDots(iMapSize);
            lnks = new Links[0];
            temp_lnks = null;
            pts = null;
            count_dot1 = 0; count_dot2 = 0;
            startX = -0.5f;
            startY = -0.5f;
            nRelation = 0;
            square1 = 0; square2 = 0;
            count_blocked1=0;count_blocked2=0;
            count_blocked=0;

#if DEBUG
            aDots.Add(0, 0, 2); aDots.Add(1, 0, 1); aDots.Add(2, 0, 2); aDots.Add(3, 0, 2); aDots.Add(4, 0, 2);
            aDots.Add(0, 1, 2); aDots.Add(1, 1, 0); aDots.Add(2, 1, 2); aDots.Add(3, 1, 1);
            aDots.Add(0, 2, 1); aDots.Add(1, 3, 1); aDots.Add(3, 2, 2); aDots.Add(4, 2, 2);
            aDots.Add(0, 3, 2); aDots.Add(1, 4, 2); aDots.Add(3, 3, 1);
            aDots.Add(0, 4, 2); aDots.Add(3, 4, 2); aDots.Add(4, 4, 2);

            f.Show();
            //MoveDebugWindow();

            lstDbg1 = (ListBox)f.Controls.Find("lstDbg1", false)[0];
            lstDbg2 = (ListBox)f.Controls.Find("lstDbg2", false)[0];
            txtDbg = (TextBox)f.Controls.Find("txtDebug", false)[0];
            txtDot = (TextBox)f.Controls.Find("txtDotStatus", false)[0];

#endif
            pbxBoard.Invalidate();
        }
        public bool GameOver()
        {
            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE
                      select d;
            return (qry.Count()==0);
        }
        public Point TranslateCoordinates(Point MousePos)
        {
            Point p = new Point();
            int top_x = (int)(startX + 0.5f), top_y = (int)(startY + 0.5f);
            p.X = top_x + (MousePos.X / (pbxBoard.ClientSize.Width / (iBoardSize + 1)));
            p.Y = top_y + (MousePos.Y / (pbxBoard.ClientSize.Height / (iBoardSize + 1)));

            return p;
        }
        public void DrawGame(Graphics gr)
        {
            //if (антиалToolStripMenuItem.Checked)
            //{
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //}
            //Устанавливаем масштаб
            //SetScale(gr, pbxBoard.ClientSize.Width, pbxBoard.ClientSize.Height,
            //    startX, startX + iBoardSize + 1.0f, startY, iBoardSize + startY + 1.0f);
            SetScale(gr, pbxBoard.ClientSize.Width, pbxBoard.ClientSize.Height,
                startX, startX + iBoardSize, startY, iBoardSize + startY);
            //Рисуем доску
            DrawBoard(gr);
            //Рисуем точки
            DrawPoints(gr);
            //Отрисовка курсора
            gr.DrawEllipse(new Pen(colorCursor, 0.05f), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
            //Отрисовка замкнутого региона игрока1
            DrawLinks(gr);

            //DrawRegion(1);
#if DEBUG
            if (aDots != null)
            {
                SolidBrush drBrush = new SolidBrush(Color.DarkMagenta);
                Font drFont = new Font("Arial", 0.2f);
                foreach (Dot d in aDots)
                {
                    gr.DrawString(d.IndexDot.ToString(), drFont, drBrush, d.x, d.y);
                }
            }
#endif

        }
        public void DrawBoard(Graphics gr)
        {
            //рисуем доску из клеток

            Pen pen = new Pen(new SolidBrush(Color.MediumSeaGreen), 0.15f);// 0
            gr.DrawLine(pen, 0, 0, 0, iMapSize - 1);
            gr.DrawLine(pen, 0, 0, iMapSize - 1, 0);
            gr.DrawLine(pen, 0, iMapSize - 1, iMapSize - 1, iMapSize - 1);
            gr.DrawLine(pen, iMapSize - 1, iMapSize - 1, iMapSize - 1, 0);
            for (float i = 0; i <= iBoardSize; i++)
            {
                SolidBrush drB = i == 0 ? new SolidBrush(Color.MediumSeaGreen) : drawBrush;
                gr.DrawString("y" + (i + startY + 0.5f).ToString(), drawFont, drB, startX, i + startY + 0.5f - 0.2f);
                gr.DrawString("x" + (i + startX + 0.5f).ToString(), drawFont, drB, i + startX + 0.5f - 0.2f, startY);
                gr.DrawLine(boardPen, i + startX + 0.5f, startY + 0.5f, i + startX + 0.5f, iBoardSize + startY + 0.5f);
                gr.DrawLine(boardPen, startX + 0.5f, i + startY + 0.5f, iBoardSize + startX + 0.5f, i + startY + 0.5f);
            }
        }
        public void DrawLinks(Graphics gr)
        {
            if (lnks != null)
            {
                Pen PenGamer;

                for (int i = 0; i < lnks.Length; i++)
                {
                    float x, y;
                    PenGamer = lnks[i].Dot1.Own == 1 ? new Pen(colorGamer1, 0.05f) : new Pen(colorGamer2, 0.05f);
                    gr.DrawLine(PenGamer, lnks[i].Dot1.x, lnks[i].Dot1.y, lnks[i].Dot2.x, lnks[i].Dot2.y);
#if DEBUG
                    x = (lnks[i].Dot2.x - lnks[i].Dot1.x) / 2.0f + lnks[i].Dot1.x;
                    y = (lnks[i].Dot2.y - lnks[i].Dot1.y) / 2.0f + lnks[i].Dot1.y;
                    gr.DrawString(lnks[i].Dot1.IndexRelation.ToString(), drawFont, new SolidBrush(Color.Navy), x, y);
#endif
                }
            }
        }
        public void DrawRegion(int DotOwn)
        {

            if (pts != null)
            {
                // gr.DrawPolygon(PenGamer, pnt);
                //gr.FillPolygon(new SolidBrush(Color.FromArgb(130, colorGamer1)), pts, System.Drawing.Drawing2D.FillMode.Winding);

            }
        }
        public void DrawPoints(Graphics gr)
        {
            //отрисовываем поставленные точки
            if (aDots.Count > 0)
            {
                foreach (Dot p in aDots)
                {
                    switch (p.Own)
                    {
                        case 1:
                            SetColorAndDrawDots(gr, colorGamer1, p);
                            break;
                        case 2:
                            SetColorAndDrawDots(gr, colorGamer2, p);
                            break;
                    }
                }
            }
        }
        private void SetColorAndDrawDots(Graphics gr, Color colorGamer, Dot p) //Вспомогательная функция для DrawPoints. Выбор цвета точки в зависимости от ее состояния и рисование элипса
        {
            Color c;
            if (p.Blocked)
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(130, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
            else
            {
                int G = colorGamer.G > 50 ? colorGamer.G - 50 : 120;
                c = p.InRegion == true ? Color.FromArgb(255, colorGamer.R, G, colorGamer.B) : colorGamer;
                gr.FillEllipse(new SolidBrush(colorGamer), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(c, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
        }
        private void FindNeiborDots(Dot Parent_dot)
        {
            Relate(Parent_dot, Parent_dot.x + 1, Parent_dot.y + 1);
            Relate(Parent_dot, Parent_dot.x, Parent_dot.y + 1);
            Relate(Parent_dot, Parent_dot.x - 1, Parent_dot.y + 1);
            Relate(Parent_dot, Parent_dot.x + 1, Parent_dot.y);
            Relate(Parent_dot, Parent_dot.x - 1, Parent_dot.y);
            Relate(Parent_dot, Parent_dot.x - 1, Parent_dot.y - 1);
            Relate(Parent_dot, Parent_dot.x, Parent_dot.y - 1);
            Relate(Parent_dot, Parent_dot.x + 1, Parent_dot.y - 1);
        }
        private void Relate(Dot new_dot, int x, int y)//устанавливает связь между соседними точками
        {
            if (aDots.Contains(x, y))
            {
                if (aDots[x, y].Own == new_dot.Own)
                {
                    Dot neibor_dot = aDots[x, y];
                    if (neibor_dot.IndexRelation == 0)
                    {
                        nRelation += 1;
                        new_dot.IndexRelation = nRelation;
                        neibor_dot.IndexRelation = nRelation;
                    }
                    else if (neibor_dot.IndexRelation > 0)
                    {
                        if (new_dot.IndexRelation > neibor_dot.IndexRelation | new_dot.IndexRelation == 0)
                        {
                            new_dot.IndexRelation = neibor_dot.IndexRelation;
                        }
                        else if (new_dot.IndexRelation < neibor_dot.IndexRelation)
                        {
                            neibor_dot.IndexRelation = new_dot.IndexRelation;
                        }

                    }
                    new_dot.AddRelationDot(neibor_dot);
                    neibor_dot.AddRelationDot(new_dot);
                }
            }
        }
        private bool DotIsFree(Dot dot)//проверяет заблокирована ли точка
        {
            dot.Marked = true;
            if (dot.x == 0 | dot.y == 0 | dot.x == iMapSize - 1 | dot.y == iMapSize - 1)
            {
                return true;
            }
            Dot[] d = new Dot[4] { aDots[dot.x + 1, dot.y], aDots[dot.x - 1, dot.y], aDots[dot.x, dot.y + 1], aDots[dot.x, dot.y - 1] };
            for (int i = 0; i < 4; i++)
            {
                if (d[i].Marked == false)
                {
                    if (d[i].Own == 0 | d[i].Own == flg_own | d[i].Own != flg_own & d[i].Blocked)
                    {
                        if (DotIsFree(d[i]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private Links LinkDots(Dot dot1, Dot dot2)//устанавливает связь между двумя точками и добавляет в коллекцию 
        {

            Links l = new Links(dot1, dot2);
            if (l.LinkExist(temp_lnks) >= 0)
            {
                return l;
            }
            if (temp_lnks == null)
            {
                Array.Resize(ref temp_lnks, 1);
            }
            else
            {
                Array.Resize(ref temp_lnks, temp_lnks.Length + 1);
            }
            temp_lnks[temp_lnks.Length - 1] = l;
            return l;
        }
        private Links[] LinkDots(Dot[] dots)//устанавливает связь между двумя точками и возвращает массив связей 
        {
            Links[] arr_l = new Links[0];
            Links l;
            int j = 0;
            foreach (Dot d in dots)
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    if (d.DotsEquals(dots[i]) == false & d.NeiborDots(dots[i]))
                    {
                        l = new Links(dots[i], d);
                        if (l.LinkExist(arr_l) == -1)
                        {
                            Array.Resize(ref arr_l, j + 1);
                            arr_l[j] = l;
                            j += 1;
                        }
                    }

                }
            }
            return arr_l;
        }
        private void SetScale(Graphics gr, int gr_width, int gr_height, float left_x, float right_x, float top_y, float bottom_y)
        {
            // Set transformations for the Graphics object so its coordinate system matches the one specified.
            // Return the horizontal scale.Start from scratch.
            gr.ResetTransform();
            // Scale so the viewport's width and height map to the Graphics object's width and height.
            gr.ScaleTransform(gr_width / (right_x - left_x), gr_height / (bottom_y - top_y));
            // Translate (left_x, top_y) to the Graphics object's origin.
            gr.TranslateTransform(-left_x, -top_y);
        }
        private float SquarePolygon(Links[] l)
        {
            if (l == null) { return 0; }
            int s1 = 0, s2 = 0;
            for (int i = 0; i < l.Length; i++)
            {
                if (i < l.Length - 1)
                {
                    s1 += l[i].Dot1.x * l[i + 1].Dot1.y;
                    s2 += l[i].Dot1.y * l[i + 1].Dot1.x;
                }
                else
                {
                    s1 += l[i].Dot1.x * l[0].Dot1.y;
                    s2 += l[i].Dot1.y * l[0].Dot1.x;
                }
            }
            return Math.Abs(s1 - s2) / 2.0f;
        }
        private float SquarePolygon(int dotsRegion, int dotsInRegion)
        {
            return dotsInRegion + dotsRegion / 2.0f - 1;//Формула Пика
        }
        public int MakeMove(Dot dot)//Возвращает количество блокированных точек
        {
            int res = Hod(dot);
            if (count_blocked - res != 0)
            {
                var qry = from Dot d in aDots
                          where d.InRegion == true
                          select d;
                Dot[] dts = qry.ToArray();
                lnks = LinkDots(dts);
                if (dts.Length > 3)
                {
                    pts = new Point[dts.Length];
                    int i = 0;
                    foreach (Dot d in dts)
                    {
                        pts[i].X = d.x;
                        pts[i].Y = d.y;
                        i += 1;
                    }
                }
            }
            switch (dot.Own)//подсчитать кол-во поставленых точек
            {
                case 1:
                    last_move=dot;
                    square1 += res;
                    count_dot1 += 1;
                    break;
                case 2:
                    square2 += res;
                    count_dot2 += 1;
                    break;
            }
            count_blocked = res;
            return count_blocked;
        }
        int flg_own;
        private int Hod(Dot dot)//Ход игрока. Точка ставится либо компом либо игроком/ Возвращает количество блокированных точек
        {
            if (aDots.Contains(dot) == false) return 0;
            
            if (aDots[dot.x, dot.y].Own == 0)//если точка не занята
            {
                aDots.Add(dot);
                FindNeiborDots(dot);//проверяем соседние точки и устанавливаем IndexRelation
            }
            return CheckBlocked();//CheckBlocked(dot);
        }
        private int CheckBlocked()
        {
            int counter=0;
                foreach (Dot d in aDots)
                {
                    flg_own = d.Own;
                    if (flg_own > 0)
                    {
                        aDots.UnmarkAllDots();
                        if (DotIsFree(d) == false)
                        {
                            d.Blocked = true;
                            aDots.UnmarkAllDots();
                            MarkDotsInRegion(d);
                            counter += 1;
                        }
                        else
                        {
                            d.Blocked = false;
                        }   
                    }
                }
             return counter;
        }
        private int ScanForBlocked()
        {
            int counter = 0;
            foreach (Dot d in aDots)
            {
               aDots.UnmarkAllDots();
               if (DotIsFree(d) == false)
               {
                    d.Blocked = true;
                    counter += 1;
               }
               else d.Blocked = false;
            }
            return counter;
        }

        private void Unblocked()
        {
            foreach (Dot d in aDots)
            {
                if (d.Own > 0 & d.Blocked)
                {
                    aDots.UnmarkAllDots();
                    if (DotIsFree(d)) d.Blocked = false;
                }
            }
        }
        private void MarkDotsInRegion(Dot dot)//Ставит InRegion=true точкам которые окружили заданную в параметре точку
        {
            dot.Marked = true;
            Dot[] dts = new Dot[8] {aDots[dot.x + 1, dot.y], aDots[dot.x - 1, dot.y],
                                  aDots[dot.x, dot.y + 1], aDots[dot.x, dot.y - 1],
                                  aDots[dot.x + 1, dot.y + 1], aDots[dot.x - 1, dot.y - 1],
                                  aDots[dot.x + 1, dot.y - 1], aDots[dot.x - 1, dot.y + 1]};
            foreach (Dot _d in dts)
            {
                if (_d.Own != 0 & _d.Own != flg_own)
                {
                    _d.InRegion = true;
                }
                else
                {
                    _d.Blocked=true;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (aDots.Contains(dts[i]))
                    if (dts[i].Marked == false)
                    {
                        if (dts[i].Own == 0 | dts[i].Own == flg_own)
                        {
                            MarkDotsInRegion(dts[i]);
                        }
                        else
                        {
                            dts[i].InRegion = true;
                        }
                    }
            }

        }
#if DEBUG
        public void MoveDebugWindow(int top, int left, int width)
        {
            f.Top = top;
            f.Left = left + width;
        }
#endif
        //Маркирует точки InRegion true которые блокируют заданную точку
    }

}
