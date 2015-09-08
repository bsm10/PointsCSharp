//#define DEBUG
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace DotsGame
{

    public partial class Game
    {
        private const int PLAYER_DRAW = -1;
        private const int PLAYER_NONE = 0;
        private const int PLAYER_HUMAN = 1;
        private const int PLAYER_COMPUTER = 2;
        public int SkillLevel = 5;
        public int SkillDepth = 20;
        public int SkillNumSq = 3;
        //-------------------------------------------------
        public int iScaleCoef = 1;//- коэффициент масштаба
        public int iBoardSize = 10;//- количество клеток квадрата в длинну
        public int iMapSize;//- количество клеток квадрата в длинну
        public const int iBoardSizeMin = 5;
        public const int iBoardSizeMax = 20;

        public float startX = -0.5f, startY = -0.5f;
        public ArrayDots aDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        private List<Links> lnks;
        private Dot best_move; //ход который должен сделать комп
        private Dot last_move; //последний ход
        private List<Dot> list_moves; //список ходов
        
        public List<Dot> ListMoves 
        {
            get { return list_moves; }   
        }
        public Dot LastMove
        {
            get
            {
                return last_move;
            }
        }
        public bool ShowMoves { get; set; }

        public List<Dot> dots_in_region;//записывает сюда точки, которые окружают точку противника
        //=========== цвета, шрифты ===================================================
        public Color colorGamer1 = Properties.Settings.Default.Color_Gamer1,
                           colorGamer2 = Properties.Settings.Default.Color_Gamer2,
                           colorCursor = Properties.Settings.Default.Color_Cursor;
        private float PointWidth = 0.25f;
        public Pen boardPen = new Pen(Color.DarkSlateBlue, 0.05f);
        private SolidBrush drawBrush = new SolidBrush(Color.MediumPurple);
        public Font drawFont = new Font("Arial", 0.22f);
        //===============================================================================

        public Point MousePos;

        //statistic
        public float square1;//площадь занятая игроком1
        public float square2;
        public int count_blocked;//счетчик количества окруженных точек
        public int count_blocked1, count_blocked2;
        public int count_dot1, count_dot2;//количество поставленных точек
       
        private PictureBox pbxBoard;
        private int _pause = 10;

#if DEBUG
        public Form f = new Form2();
        public ListBox lstDbg1;
        public ListBox lstDbg2;
        public TextBox txtDbg;
        public TextBox txtDot;
#endif

        Stopwatch stopWatch = new Stopwatch();//для диагностики времени выполнения

        public Game(PictureBox CanvasGame)
        {
            pbxBoard = CanvasGame;
            NewGame();
        }
        //  ************************************************
        public Dot PickComputerMove(Dot enemy_move)
        {
            float s1 = square1; float s2 = square2;
            best_move=null;
            int depth=0;
            var t1 = DateTime.Now.Millisecond;
            stopWatch.Start();
            Dot lm = new Dot(last_move.x, last_move.y);//точка последнего хода
            //проверяем ход который ведет сразу к окружению
            best_move = CheckMove(PLAYER_COMPUTER);
            if (best_move == null) best_move = CheckMove(PLAYER_HUMAN);

            //проверяем паттерны
            if (best_move==null) best_move = CheckPattern(PLAYER_COMPUTER);
            if (best_move==null) best_move = CheckPattern(PLAYER_HUMAN);  // проверяем ходы
            int c1=0 , c_root=1000;// , dpth=0;
            if (best_move ==null)
            {
               var dots_on_board = from Dot d in aDots where d.Own != 0 & d.Blocked==false select d;
               Dot[] ad;
               ad = dots_on_board.ToArray();
#if DEBUG
                lstDbg2.Items.Clear();
#endif
                lst_best_move.Clear();
               //foreach (Dot d in ad)
               //     {
#if DEBUG
                        lstDbg1.Items.Clear();
                        lstDbg1.BeginUpdate();
#endif
                Dot dot1 = null, dot2 =null;
                //PLAYER_HUMAN - ставим в параметр - первым ходит игрок1(человек)
                Play(ref best_move,  dot1,dot2, PLAYER_HUMAN, ref depth, ref c1, lm, ref c_root);//раскоментировать для поиска без цикла - вокруг последнего хода
                                                                                                       // BoardValue(ref best_move, PLAYER_COMPUTER, PLAYER_HUMAN, ref depth, ref c1, d, ref c2);
                                                                                                       // FindMove(ref best_move, lm);
                                                                                                       //}
                if (lst_best_move.Count>0) 
                {
                    best_move=lst_best_move[0];
                }
               else
                {
                    FindMove(ref best_move, lm);
                }
                
            }
            if (best_move == null)
            {
                //MessageBox.Show("best_move == null");
                

                // //паттерн на точку находящуюся в двух клетках 
                var qry = from Dot d in aDots
                          where d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x + 2, d.y].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x - 2, d.y].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x, d.y + 2].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x, d.y - 2].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x + 2, d.y + 1].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x + 2, d.y - 1].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x - 2, d.y + 1].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x - 2, d.y - 1].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x + 1, d.y - 2].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x - 1, d.y - 2].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x + 1, d.y + 2].Own == PLAYER_COMPUTER |
                                d.Blocked == false & d.Own == PLAYER_NONE & aDots[d.x - 1, d.y + 2].Own == PLAYER_COMPUTER
                          select d;
                if (qry.Count() > 0)
                {
                    best_move = qry.First();
                }
                else
                {
                    var q = from Dot d in aDots//любая точка
                              where d.Blocked == false & d.Own == PLAYER_NONE /* & Math.Abs(d.x - enemy_move.x) < 2 &
                                                                                Math.Abs(d.y - enemy_move.y) < 2*/
                              select d;

                    best_move = q.First();
                }

            }
            
#if DEBUG
    lstDbg1.EndUpdate();
#endif
            //stopWatch.Stop();
           
            txtDbg.Text = "Skilllevel: " + SkillLevel + "\r\n Общее число ходов: " + depth.ToString() +
            "\r\n Глубина просчета: " + c_root.ToString() +
            "\r\n Ход на " + best_move.x + ":" + best_move.y +
            "\r\n время просчета " + stopWatch.ElapsedMilliseconds.ToString() + " мс";
            best_move.Own = 2;
            stopWatch.Reset();
            square1=s1; square2=s2;

            //list_moves.Add(best_move);//добавим в реестр ходов
            return new Dot(best_move.x,best_move.y); //так надо чтобы best_move не ссылался на точку в aDots
        }
 //==================================================================================================================
        List<Dot> lst_best_move=new List<Dot>();//сюда заносим лучшие ходы
//===================================================================================================================
        private int Play(ref Dot best_move, Dot move1, Dot move2, int player, ref int count_moves, 
                               ref int recursion_depth, Dot lm, ref int counter_root)//возвращает Owner кто побеждает в результате хода
        {
            recursion_depth++;
            if (recursion_depth >= SkillDepth)
            {
                return 0;
            }
            Dot enemy_move = null;
            //var random = new Random(DateTime.Now.Millisecond);
            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE & d.Blocked==false & Math.Abs(d.x - lm.x) <SkillNumSq
                                                                    & Math.Abs(d.y - lm.y) <SkillNumSq
                      orderby Math.Sqrt(Math.Pow(Math.Abs(d.x - lm.x), 2) + Math.Pow(Math.Abs(d.y - lm.y), 2)) //Math.Sqrt(Math.Abs(d.x - lm.x)* Math.Abs(d.x - lm.x) + Math.Abs(d.y - lm.y)* Math.Abs(d.y - lm.y)) //(random.Next())
                      select d;
            Dot[] ad = qry.ToArray();
            int i = ad.Length;
            if (i != 0)
            {
                foreach (Dot d in ad)
                {
                    //**************делаем ход***********************************
                    d.Own = player == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
                    int res_last_move = (int)MakeMove(d);
                    Dot dt = CheckMove(player, false);//проверить не замыкается ли регион
                    count_moves++;
                    if (dt != null)
                    {
                        if (recursion_depth < counter_root)
                        {
                            counter_root = recursion_depth;
                            best_move = dt;
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add(recursion_depth + "ход на " + best_move.x + ":" + best_move.y + "; win HUMAN");
#endif
                            UndoMove(d);
                            return player;
                        }
                    }

                    player = d.Own;
                    if (move1 == null & player == 1 & recursion_depth <= 2) move1 = d;
                    if (move2 == null & player == 2 & recursion_depth <= 2) move2 = d;
                    //-----показывает проверяемые ходы--------
                    if (ShowMoves) Pause();
                    #if DEBUG
                        //Application.DoEvents();
                        lstDbg1.Items.Add(d.Own + " - " + d.x  + ":" + d.y) ;
                           txtDbg.Text="Общее число ходов: " + count_moves.ToString() + 
                                       "\r\n Глубина просчета: " + recursion_depth.ToString() +
                                       "\r\n проверка вокруг точки " + lm +
                                       "\r\n move1 " + move1 +
                                       "\r\n move2 " + move2 +
                                       "\r\n время поиска " + stopWatch.ElapsedMilliseconds; 
                    #endif
                    //-----------------------------------
                    if (res_last_move != 0 & aDots[d.x, d.y].Blocked)//если ход в окруженный регион
                    {
                        best_move = null;
                        UndoMove(d);
                        return d.Own == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
                    }
                    if (d.Own == 1 & res_last_move != 0)
                    {
                        if (recursion_depth < counter_root)
                        {
                            counter_root = recursion_depth;
                            //best_move = new Dot(d.x,d.y);
                            best_move = new Dot(move1.x, move1.y);
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add(recursion_depth + "ход на " + best_move.x + ":" + best_move.y + "; win HUMAN");
#endif
                        }
                        UndoMove(d);
                        return PLAYER_HUMAN;//побеждает игрок
                    }
                    else if (d.Own == 2 & res_last_move != 0 | d.Own==1 & aDots[d.x,d.y].Blocked)
                    {
                        if (recursion_depth < counter_root)
                        {
                            counter_root = recursion_depth;
                            //best_move =  new Dot(d.x, d.y);
                            best_move = new Dot(move2.x, move2.y);
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add(recursion_depth +" ход на " + best_move.x + ":" + best_move.y + "; win COM");
#endif
                        }
                        UndoMove(d);
                        return PLAYER_COMPUTER;//побеждает компьютер
                    }
                    count_moves++;
                    
                    //теперь ходит другой игрок =========================================================================================
                    int result = Play(ref enemy_move, move1,move2, player, ref count_moves, ref recursion_depth, lm, ref counter_root);
                    //отменить ход
                    UndoMove(d);
                    recursion_depth--;
#if DEBUG
                    if (lstDbg1.Items.Count>0) lstDbg1.Items.RemoveAt(lstDbg1.Items.Count-1);
#endif
                    if (enemy_move == null & recursion_depth > 1)
                        break;
                    if (count_moves > SkillLevel * 100)
                        return PLAYER_NONE;
                }
            }
            return PLAYER_NONE;
        }
        //**************************************************************************************************************
        //+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        private int FindMove(ref Dot move, Dot last_mv)//возвращает Owner кто побеждает в результате хода
        {
            int depth = 0, counter = 0, counter_root = 1000, own;
            own = PLAYER_HUMAN;//последним ходил игрок
            List<Dot> mvs = new List<Dot>();
            Dot[] ad=null; 

            int i = 0;
            do
            {
                if (i == 0)
                {
                    var qry = from Dot d in aDots
                              where d.Own == PLAYER_NONE & d.Blocked == false //& Math.Abs(d.x - last_mv.x) < SkillNumSq
                                                                              // & Math.Abs(d.y - last_mv.y) < SkillNumSq
                              orderby d.x
                              select d;
                    ad = qry.ToArray();
                    if (qry.Count() == 0)
                    {
                        foreach (Dot d in mvs)
                        {
                            UndoMove(d);
                        }
                        mvs.Clear();
                        qry = null;
                        i++;
                    }
                }
                else if (i == 1)
                {
                    var qry1 = from Dot d in aDots
                          where d.Own == PLAYER_NONE & d.Blocked == false//& Math.Abs(d.x - last_mv.x) < SkillNumSq
                                                                         // & Math.Abs(d.y - last_mv.y) < SkillNumSq
                          orderby d.y descending
                          select d;
                    ad = qry1.ToArray();
                    if (qry1.Count() == 0)
                    {
                        foreach (Dot d in mvs)
                        {
                            UndoMove(d);
                        }
                        mvs.Clear();
                        return 0;
                    }

                }
                depth++; 

                if (ad.Length != 0)
                {
                    foreach (Dot d in ad)
                    {
                        counter++;
                        switch (own)
                        {
                            case PLAYER_HUMAN:
                                own = PLAYER_COMPUTER;
                                break;
                            case PLAYER_COMPUTER:
                                own = PLAYER_HUMAN;
                                break;
                        }
                        //ход делает комп, если последним ходил игрок
                        d.Own = own;
                        int res_last_move = (int)MakeMove(d);
                        mvs.Add(d); 
                        //-----показывает проверяемые ходы-----------------------------------------------
                        if (ShowMoves) Pause();
#if DEBUG

                        lstDbg1.Items.Add(d.Own + " - " + d.x + ":" + d.y);
                        txtDbg.Text = "Общее число ходов: " + depth.ToString() +
                                "\r\n Глубина просчета: " + counter.ToString() +
                                "\r\n проверка вокруг точки " + last_move;
#endif
                        //------------------------------------------------------------------------------
                        if (res_last_move != 0 & aDots[d.x, d.y].Blocked)//если ход в окруженный регион
                        {
                            move = null;
                            //UndoMove(d);
                            //return d.Own == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
                            break;
                        }
                        if (d.Own == 1 & res_last_move != 0)
                        {
                            if (counter < counter_root)
                            {
                                counter_root = counter;
                                move = new Dot(d.x, d.y);
#if DEBUG
                                lstDbg2.Items.Add("Ход на " + move.x + ":" + move.y + "; ход " + counter);
#endif
                            }
                            //UndoMove(d);
                            break;//return PLAYER_HUMAN;//побеждает игрок
                        }
                        else if (d.Own == 2 & res_last_move != 0 | d.Own == 1 & aDots[d.x, d.y].Blocked)
                        {
                            if (counter < counter_root)
                            {
                                counter_root = counter;
                                move = new Dot(d.x, d.y);
#if DEBUG
                                lstDbg2.Items.Add("Ход на " + move.x + ":" + move.y + "; ход " + counter);
#endif
                            }
                            //UndoMove(d);
                            //return PLAYER_COMPUTER;//побеждает компьютер
                            break;
                        }
                        if (depth > SkillLevel * 100)//количество просчитываемых комбинаций
                        {
                            //return PLAYER_NONE;
                            break;
                        }

                    }
            }
      } while (true);

      //return PLAYER_NONE;
}

        //==============================================================================================
        //проверяет ход в результате которого окружение.Возвращает ход который завершает окружение
        private Dot CheckMove(int Owner, bool AllBoard=true)
        {
            var qry = AllBoard ? from Dot d in aDots where d.Own == PLAYER_NONE & d.Blocked==false select d :
                                            from Dot d in aDots where d.Own == PLAYER_NONE & d.Blocked == false & 
                                            Math.Abs(d.x - LastMove.x) < 2 & Math.Abs(d.y - LastMove.y) < 2 select d;
            Dot[] ad = qry.ToArray();
            if (ad.Length != 0)
            {
                foreach (Dot d in ad)
                {
                    //делаем ход
                    d.Own = Owner;
                    int result_last_move = (int)MakeMove(d);
                    if (ShowMoves) Pause();
                    //-----------------------------------
                    if (result_last_move != 0 & aDots[d.x, d.y].Blocked == false)
                    {
                        UndoMove(d);
                        return d;
                    }
                    UndoMove(d);
                }
            }
            return null;
        }
        
        public string Statistic()
        {
            var q5 = from Dot d in aDots where d.Own == 1 select d;
            count_dot1 = q5.Count();
            var q6 = from Dot d in aDots where d.Own == 2 select d;
            count_dot2 = q5.Count();

            return "Игрок1 окружил точек: " + count_blocked1 + "; \r\n" +
              "Захваченая площадь: " + square1 + "; \r\n" +
              "Игрок2 окружил точек: " + count_blocked2 + "; \r\n" +
              "Захваченая площадь: " + square2 + "; \r\n" +
              "Игрок1 точек поставил: " + count_dot1.ToString() + "; \r\n" +
              "Игрок2 точек поставил: " + count_dot2.ToString() + "; \r\n";
        }
        public void Statistic(int x, int y)
        {
            if (aDots.Contains(x, y))
            {
                txtDot.Text = "Blocked: " + aDots[x, y].Blocked + "\r\n" +
                              "BlokingDots.Count: " + aDots[x, y].BlokingDots.Count + "\r\n" +
                              "NeiborDots.Count: " + aDots[x, y].NeiborDots.Count + "\r\n" +
                              "Rating: " + aDots[x, y].Rating + "\r\n" +
                              "IndexDot: " + aDots[x, y].IndexDot + "\r\n" +
                              "Own: " + aDots[x, y].Own + "\r\n" +
                              "X: " + aDots[x, y].x + "; Y: " + aDots[x, y].y;
            }
        }
        public int pause 
           {
            get
                {
                 return _pause;
                }
            set
            {
                _pause = value;
            }
            }
        private void Pause()
        {
            if (pause>0)
            {
                Application.DoEvents();
                pbxBoard.Invalidate();
                System.Threading.Thread.Sleep(pause);
            }
        }
        private void Pause(int ms)
        {
            Application.DoEvents();
            pbxBoard.Invalidate();
            System.Threading.Thread.Sleep(ms);
        }
        public void NewGame()
        {
            iMapSize = iBoardSize * iScaleCoef;
            aDots = new ArrayDots(iMapSize);
            lnks = new List<Links>();
            dots_in_region = new List<Dot>();
            list_moves = new List<Dot>();
            count_dot1 = 0; count_dot2 = 0;
            startX = -0.5f;
            startY = -0.5f;
            square1 = 0; square2 = 0;
            count_blocked1=0;count_blocked2=0;
            count_blocked=0;
#if DEBUG
            //aDots.Add(0, 0, 2); aDots.Add(1, 0, 1); aDots.Add(2, 0, 2); aDots.Add(3, 0, 2); aDots.Add(4, 0, 2);
            //aDots.Add(0, 1, 2); aDots.Add(1, 1, 0); aDots.Add(2, 1, 2); aDots.Add(3, 1, 1);
            //aDots.Add(0, 2, 1); aDots.Add(1, 3, 1); aDots.Add(3, 2, 2); aDots.Add(4, 2, 2);
            //aDots.Add(0, 3, 2); aDots.Add(1, 4, 2); aDots.Add(3, 3, 1);
            //aDots.Add(0, 4, 2); aDots.Add(3, 4, 2); aDots.Add(4, 4, 2);
            //aDots.Add(1, 1, 1); aDots.Add(0, 2, 1); aDots.Add(1, 2, 2);
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
                      where d.Own == PLAYER_NONE & d.Blocked==false
                      select d;
            return (qry.Count()==0);
        }
        public Point TranslateCoordinates(Point MousePos)
        {
            var transform = _transform.Clone();
            transform.Invert();
            var points = new[] { MousePos };
            transform.TransformPoints(points);
            return points[0];
        }
        public void DrawGame(Graphics gr)//отрисовка хода игры
        {
            //if (антиалToolStripMenuItem.Checked)
            //{
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //}
            //Устанавливаем масштаб
           
            SetScale(gr, pbxBoard.ClientSize.Width, pbxBoard.ClientSize.Height,
                startX, startX + iBoardSize, startY, iBoardSize + startY);

            //Рисуем доску
            DrawBoard(gr);
            //Рисуем точки
            DrawPoints(gr);
            //Отрисовка курсора
            gr.FillEllipse(new SolidBrush(Color.FromArgb(30, colorCursor)), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
            gr.FillEllipse(new SolidBrush(Color.FromArgb(130, Color.WhiteSmoke)), MousePos.X - PointWidth/2, MousePos.Y - PointWidth/2, PointWidth , PointWidth);
            gr.DrawEllipse(new Pen(Color.FromArgb(50, colorCursor), 0.05f), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
            //Отрисовка замкнутого региона игрока1
            DrawLinks(gr);

            //DrawRegion(1);
#if DEBUG
            //if (aDots != null)
            //{
            //    SolidBrush drBrush = new SolidBrush(Color.DarkMagenta);
            //    Font drFont = new Font("Arial", 0.2f);
            //    foreach (Dot d in aDots)
            //    {
            //        gr.DrawString(d.IndexDot.ToString(), drFont, drBrush, d.x, d.y);
            //    }
            //}
#endif

        }
        public void DrawBoard(Graphics gr)//рисуем доску из клеток
        {

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
        public void DrawLinks(Graphics gr)//отрисовка связей
        {
            if (lnks != null)
            {
                Pen PenGamer;

                for (int i = 0; i < lnks.Count; i++)
                {
                    float x, y;
                    PenGamer = lnks[i].Dot1.Own == 1 ? new Pen(colorGamer1, 0.05f) : new Pen(colorGamer2, 0.05f);
                    gr.DrawLine(PenGamer, lnks[i].Dot1.x, lnks[i].Dot1.y, lnks[i].Dot2.x, lnks[i].Dot2.y);
#if DEBUG
                    x = (lnks[i].Dot2.x - lnks[i].Dot1.x) / 2.0f + lnks[i].Dot1.x;
                    y = (lnks[i].Dot2.y - lnks[i].Dot1.y) / 2.0f + lnks[i].Dot1.y;
                    //gr.DrawString(lnks[i].Dot1.IndexRelation.ToString(), drawFont, new SolidBrush(Color.Navy), x, y);
#endif
                }
            }
        }
        public void DrawPoints(Graphics gr)//рисуем поставленные точки
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
            else if (p.x== last_move.x & p.y == last_move.y)//точка последнего хода должна для удовства выделяться
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(140, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                //gr.DrawEllipse(new Pen(colorGamer, 0.08f), p.x - PointWidth/2, p.y - PointWidth/2, PointWidth, PointWidth );
                gr.DrawEllipse(new Pen(Color.FromArgb(140, Color.WhiteSmoke), 0.08f), p.x - PointWidth / 2, p.y - PointWidth / 2, PointWidth, PointWidth);
                gr.DrawEllipse(new Pen(colorGamer, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth+ PointWidth, PointWidth+ PointWidth);
            }
            else
            {
                int G = colorGamer.G > 50 ? colorGamer.G - 50 : 120;
                c = p.BlokingDots.Count > 0 ? Color.FromArgb(255, colorGamer.R, G, colorGamer.B) : colorGamer;
                gr.FillEllipse(new SolidBrush(colorGamer), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(c, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
        }
        private bool DotIsFree(Dot dot,int flg_own)//проверяет заблокирована ли точка. Перед использованием функции надо установить flg_own-владелец проверяемой точки
        {
            dot.Marked = true;
            
            if (dot.x == 0 | dot.y == 0 | dot.x == iMapSize - 1 | dot.y == iMapSize - 1)
            {
                return true;
            }
            Dot[] d = new Dot[4] { aDots[dot.x + 1, dot.y], aDots[dot.x - 1, dot.y], aDots[dot.x, dot.y + 1], aDots[dot.x, dot.y - 1] };
            //--------------------------------------------------------------------------------
            if(flg_own==0)// если точка не принадлежит никому и рядом есть незаблокированные точки - эта точка считается свободной(незаблокированной)
            {
                var q = from Dot fd in d where fd.Blocked == false select fd;
                if(q.Count()>0) return true;
            }
            //----------------------------------------------------------------------------------
            for (int i = 0; i < 4; i++)
            {
                if (d[i].Marked == false)
                {
                    if (d[i].Own == 0 | d[i].Own == flg_own | d[i].Own != flg_own & d[i].Blocked & d[i].BlokingDots.Contains(dot)==false)
                    {
                        if (DotIsFree(d[i], flg_own))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //------------------------------------------------------------------------------------
        public void LinkDots()//устанавливает связь между двумя точками и возвращает массив связей 
        {
            var qry = from Dot d in aDots
                      where d.BlokingDots.Count > 0
                      select d;
            Dot[] dts = qry.ToArray();
            Links l;
            foreach (Dot d in dts)
            {
                for (int i = 0; i < dts.Length; i++)
                {
                    if (d.Equals(dts[i]) == false & d.IsNeiborDots(dts[i]) & d.Blocked == false & dts[i].Blocked == false)
                    {
                        l = new Links(dts[i], d);
                        if (l.LinkExist(lnks.ToArray()) == -1)
                        {
                            lnks.Add(l);
                        }
                    }
                }
            }
        }
        Matrix _transform = new Matrix();//матрица для преобразования координат точек в заданном масштабе
        private void SetScale(Graphics gr, int gr_width, int gr_height, float left_x, float right_x, float top_y, float bottom_y)
        {
        //функция масштабирования, устанавливает массштаб
            gr.ResetTransform();
            gr.ScaleTransform(gr_width / (right_x - left_x), gr_height / (bottom_y - top_y));
            gr.TranslateTransform(-left_x, -top_y);
            var transform = new Matrix();
            _transform = gr.Transform;
        }
        private float SquarePolygon(int nBlockedDots, int nRegionDots)
        {
            return nBlockedDots + nRegionDots / 2.0f - 1;//Формула Пика
        }
        //=================================================================================================
        public float MakeMove(Dot dot)//Основная функция - ход игрока - возвращает захваченую площадь, 
        {
            if (aDots.Contains(dot) == false) return 0;
            if (aDots[dot.x, dot.y].Own == 0)//если точка не занята
            {
                aDots.Add(dot);
            }
            int lst_in_r = lst_in_region_dots.Count;
            int lst_bl_d = lst_blocked_dots.Count;
            float s=0;
            int res = CheckBlocked(dot.Own);
            int dif=count_blocked - res;
            int lst_i_r = lst_in_region_dots.Count - lst_in_r;//количество точек окружающих новый регион
            int lst_b_d = lst_blocked_dots.Count - lst_bl_d;//количество блокир точек
            if (dif!= 0) 
                {
                    switch (dot.Own)//подсчитать кол-во поставленых точек
                    {
                        case 1:
                            square1 += SquarePolygon(lst_b_d, lst_i_r);
                            s=square1;
                            break;
                        case 2:
                            square2 += SquarePolygon(lst_b_d, lst_i_r);
                            s=square2;
                            break;
                    }
                    LinkDots();
                }
            
            count_blocked -= dif;
            last_move = dot;//зафиксировать последний ход
            MakeRating();//пересчитать рейтинг
            return s;//dif;
        }
        private int CheckBlocked()//проверяет блокировку точек, маркирует точки которые блокируют, возвращает количество окруженных точек
        {
            int counter=0;
            var q = from Dot dots in aDots where dots.Own != 0 | dots.Own == 0 & dots.Blocked
                    select dots;
            foreach (Dot d in q)
            {
                aDots.UnmarkAllDots();
                if (DotIsFree(d, d.Own) == false)
                {
                    lst_blocked_dots.Clear(); lst_in_region_dots.Clear();
                    if (d.Own != 0) d.Blocked = true;
                    aDots.UnmarkAllDots();
                    MarkDotsInRegion(d, d.Own);
                    counter += 1;
                    foreach (Dot dr in lst_in_region_dots)
                    {
                        foreach (Dot bd in lst_blocked_dots)
                        {
                           if (dr.BlokingDots.Contains(bd) == false & bd.Own != 0) dr.BlokingDots.Add(bd);
                           // if (dr.BlokingDots.Contains(bd) == false) dr.BlokingDots.Add(bd);
                        }
                    }
                }
                else
                {
                    d.Blocked = false;
                }
            }

            return counter;
        }
        private int CheckBlocked(int last_moveOwner=0)//проверяет блокировку точек, маркирует точки которые блокируют, возвращает количество окруженных точек
        {
            int counter = 0;
            var q = from Dot dots in aDots where dots.Own != 0 | dots.Own == 0 & dots.Blocked select dots;
            Dot[] arrDot = q.ToArray();
            switch (last_moveOwner)
            {
                case 1:
                    IEnumerable<Dot> query1 = arrDot.OrderBy(dot => dot.Own==1);
                    arrDot=query1.ToArray();
                    break;
                case 2:
                    IEnumerable<Dot> query2 = arrDot.OrderBy(dot => dot.Own == 2);
                    arrDot = query2.ToArray();
                    break;
            }
            foreach (Dot d in arrDot)
            {
                aDots.UnmarkAllDots();
                if (DotIsFree(d, d.Own) == false)
                {
                    lst_blocked_dots.Clear(); lst_in_region_dots.Clear();
                    if (d.Own != 0) d.Blocked = true;
                    aDots.UnmarkAllDots();
                    MarkDotsInRegion(d, d.Own);
                    counter += 1;
                    foreach (Dot dr in lst_in_region_dots)
                    {
                        foreach (Dot bd in lst_blocked_dots)
                        {
                            if (dr.BlokingDots.Contains(bd) == false & bd.Own != 0) dr.BlokingDots.Add(bd);
                        }
                    }
                }
                else
                {
                    d.Blocked = false;
                }
            }
            return counter;
        }

        private List<Dot> lst_blocked_dots = new List<Dot>();//список блокированных точек
        private List<Dot> lst_in_region_dots = new List<Dot>();//список блокирующих точек
        private void MarkDotsInRegion(Dot blocked_dot, int flg_own)//Ставит InRegion=true точкам которые блокируют заданную в параметре точку
        {
            blocked_dot.Marked = true;
            Dot[] dts = new Dot[4] {aDots[blocked_dot.x + 1, blocked_dot.y], aDots[blocked_dot.x - 1, blocked_dot.y],
                                  aDots[blocked_dot.x, blocked_dot.y + 1], aDots[blocked_dot.x, blocked_dot.y - 1]};
            //добавим точки которые попали в окружение
            if (lst_blocked_dots.Contains(blocked_dot) == false)
            {
                lst_blocked_dots.Add(blocked_dot);
            }
            foreach (Dot _d in dts)
            {
                if (_d.Own != 0 & _d.Own != flg_own)//_d-точка которая окружает
                {
                    //добавим в коллекцию точки которые окружают
                    if (lst_in_region_dots.Contains(_d) == false) lst_in_region_dots.Add(_d);
                }
                else
                {
                    _d.Blocked = true;
                    if (_d.Marked == false & _d.Fixed == false)
                    {
                        MarkDotsInRegion(_d, flg_own);
                    }
                }
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void MakeRating()//возвращает массив вражеских точек вокруг заданной точки
        {
            int res = 0;
            var qd = from Dot dt in aDots where dt.Own != 0 & dt.Blocked==false select dt;
            foreach (Dot dot in qd)
            {
                if (dot.x > 0 & dot.y > 0 & dot.x < iMapSize - 1 & dot.y < iMapSize - 1)
                {
                    Dot[] dts = new Dot[4] {aDots[dot.x + 1, dot.y], aDots[dot.x - 1, dot.y],aDots[dot.x, dot.y + 1], aDots[dot.x, dot.y - 1]};
                    var q = from Dot d in dts where d.Own != 0 & d.Own != dot.Own select d;
                    res = q.Count();
                    dot.Rating = res;
                }
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void UndoMove(int x, int y)//поле отмена хода
        {
            Undo(x,y);
            //list_moves.Remove(aDots[x, y]);
        }
        public void UndoMove(Dot dot)//поле отмена хода
        {
            Undo(dot.x, dot.y);
            //list_moves.Remove(aDots[dot.x, dot.y]);
        }
        private void Undo(int x, int y)//отмена хода
        {
            List<Dot> bl_dot = new List<Dot>();
            List<Links> ln = new List<Links>();
            if (aDots[x, y].Blocked)//если точка была блокирована, удалить ее из внутренних списков у блокирующих точек
            {
                lst_blocked_dots.Remove(aDots[x, y]);
                bl_dot.Add(aDots[x, y]);
                foreach (Dot d in lst_in_region_dots)
                {
                    d.BlokingDots.Remove(aDots[x, y]);
                }
                count_blocked = CheckBlocked();
                //aDots.Remove(x, y);
            }
            //aDots[x, y].Own = 0;
            if (aDots[x, y].BlokingDots.Count > 0)
            {
                //снимаем блокировку с точки bd, которая была блокирована UndoMove(int x, int y)
                foreach (Dot d in aDots[x, y].BlokingDots)
                {
                    bl_dot.Add(d);

                }
            }

            foreach (Dot d in bl_dot)
            {
                foreach (Links l in lnks)//подготовка связей которые блокировали точку
                {
                    if (l.Dot1.BlokingDots.Contains(d) | l.Dot2.BlokingDots.Contains(d))
                    {
                        ln.Add(l);
                    }
                }
                //удаляем из списка блокированных точек
                foreach (Dot bd in aDots)
                {
                    if (bd.BlokingDots.Count > 0)
                    {
                        bd.BlokingDots.Remove(d);
                    }
                }
                //восстанавливаем связи у которых одна из точек стала свободной
                var q_lnks = from lnk in lnks
                             where lnk.Dot1.x == d.x & lnk.Dot1.y == d.y | lnk.Dot2.x == d.x & lnk.Dot2.y == d.y
                             select lnk;
                foreach (Links l in q_lnks)
                {
                    l.Dot1.Blocked = false;
                    l.Dot2.Blocked = false;
                }

            }
            //удаляем связи
            foreach (Links l in ln)
            {
                lnks.Remove(l);
            }
            ln = null;
            bl_dot = null;

            //list_moves.Remove(aDots[x, y]);
            aDots.Remove(x, y);
            count_blocked = CheckBlocked();
            LinkDots(); 
        }

//=========================================================================
#if DEBUG
        public void MoveDebugWindow(int top, int left, int width)
        {
            f.Top = top;
            f.Left = left + width;
        }
#endif
//==========================================================================
        public string path_savegame = Application.CommonAppDataPath + @"\dots.dat";
        public void SaveGame()
        {
            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path_savegame, FileMode.Create)))
                {

        		for (int i = 0; i < list_moves.Count; i++)
           			{
                        writer.Write((byte)list_moves[i].x);
                        writer.Write((byte)list_moves[i].y);
                        writer.Write((byte)list_moves[i].Own);
                	}
            	}
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void LoadGame()
        {
            aDots.Clear();
            lnks.Clear();
            list_moves.Clear();
            Dot d=null;
            // создаем объект BinaryReader
            BinaryReader reader = new BinaryReader(File.Open(path_savegame, FileMode.Open));
           // пока не достигнут конец файла считываем каждое значение из файла
                 while (reader.PeekChar() > -1)
                {
                 	d=new Dot((int)reader.ReadByte(),(int)reader.ReadByte(),(int)reader.ReadByte(),null);
                	//aDots.Add(d);
                    MakeMove(d);
                	list_moves.Add(d);
                }
            last_move=d;
            //CheckBlocked();//проверяем блокировку
            LinkDots();//восстанавливаем связи между точками
            reader.Close();
        }
    }
    struct Dots_sg//структура для сохранения игры в файл
    {
        public byte x;
        public byte y;
        public byte Own;
        public Dots_sg(int X, int Y, int Owner)
        {
            x = (byte)X;
            y = (byte)Y;
            Own = (byte)Owner;
        }
    }
}
