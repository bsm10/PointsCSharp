//#define DEBUG
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Drawing2D;

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
        private int _pause = 100;
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
            NewGame();
        }
        //  ************************************************
        public Dot PickComputerMove(Dot enemy_move)
        {
            int depth=0;
            Dot lm = new Dot(last_move.x,last_move.y);//точка последнего хода
            //проверяем ход который ведет сразу к окружению
            best_move=CheckMove(PLAYER_COMPUTER);
            if (best_move==null) best_move = CheckMove(PLAYER_HUMAN);
            
            //проверяем паттерны
            if (best_move==null) best_move = CheckPattern(PLAYER_COMPUTER);
            if (best_move==null) best_move = CheckPattern(PLAYER_HUMAN);  // проверяем ходы
            int c1=0 , c_root=1000 , dpth=0;
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
#endif
                        BoardValue(ref best_move, PLAYER_COMPUTER, PLAYER_HUMAN, ref depth, ref c1, lm, ref c_root);//раскоментировать для поиска без цикла - вокруг последнего хода
                       // BoardValue(ref best_move, PLAYER_COMPUTER, PLAYER_HUMAN, ref depth, ref c1, d, ref c2);
                       // FindMove(ref best_move, lm);
                    //}

               if(lst_best_move.Count>0) 
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
                MessageBox.Show("best_move == null");
                

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

                    best_move = q.First();                 }

            }
            txtDbg.Text = "Skilllevel: " + SkillLevel + "\r\n Общее число ходов: " + depth.ToString() +
            "\r\n Глубина просчета: " + c_root.ToString() +
            "\r\n Ход на " + best_move.x + ":" + best_move.y;
            best_move.Own = 2;
            //list_moves.Add(best_move);//добавим в реестр ходов
            return new Dot(best_move.x,best_move.y); //так надо чтобы best_move не ссылался на точку в aDots
        }
 //==================================================================================================================
        List<Dot> lst_best_move=new List<Dot>();//сюда заносим лучшие ходы
//===================================================================================================================
        private int BoardValue(ref Dot best_move, int pl1, int pl2, ref int depth, 
                               ref int counter, Dot lm, ref int counter_root)//возвращает Owner кто побеждает в результате хода
        {
            if (counter >= SkillDepth)
            {
                return 0;
            }

            Dot enemy_move = null;
            var random = new Random(DateTime.Now.Millisecond);
            //array = array.OrderBy(x => random.Next()).ToArray();
            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE & d.Blocked==false & Math.Abs(d.x - lm.x) <SkillNumSq
                                                                    & Math.Abs(d.y - lm.y) <SkillNumSq
                      orderby  Math.Sqrt(Math.Abs(d.x - lm.x)* Math.Abs(d.x - lm.x) + Math.Abs(d.y - lm.y)* Math.Abs(d.y - lm.y)) //(random.Next())
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
                    int res_last_move = MakeMove(d);
                    //-----показывает проверяемые ходы--------
                    if (ShowMoves) Pause();
                    #if DEBUG

                    lstDbg1.Items.Add(d.Own + " - " + d.x  + ":" + d.y) ;
                           txtDbg.Text="Общее число ходов: " + depth.ToString() + 
                                       "\r\n Глубина просчета: " + counter.ToString() +
                                       "\r\n проверка вокруг точки " + lm; 
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
                        if (counter < counter_root)
                        {
                            counter_root = counter;
                            best_move = new Dot(d.x,d.y);
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add("Ход на " + best_move.x + ":" + best_move.y + "; ход " + counter);
#endif
                        }
                        UndoMove(d);
                        return PLAYER_HUMAN;//побеждает игрок
                    }
                    else if (d.Own == 2 & res_last_move != 0 | d.Own==1 & aDots[d.x,d.y].Blocked)
                    {
                        if (counter < counter_root)
                        {
                            counter_root = counter;
                            best_move =  new Dot(d.x, d.y);
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add("Ход на " + best_move.x + ":" + best_move.y + "; ход " + counter);
#endif
                        }
                        UndoMove(d);
                        return PLAYER_COMPUTER;//побеждает компьютер
                    }
                    depth++;counter++;
                    //теперь ходит другой игрок
                    int result = BoardValue(ref enemy_move, pl2, pl1, ref depth, ref counter, lm, ref counter_root);
                    //отменить ход
                    UndoMove(d);
                    counter--;
#if DEBUG
                    if (lstDbg1.Items.Count>0) lstDbg1.Items.RemoveAt(lstDbg1.Items.Count-1);
#endif
                    if (depth > SkillLevel * 100)
                    {
                        return PLAYER_NONE;
                    }
                   // if (result > 0) break;

                }
            }
            return PLAYER_NONE;
        }
        private int BoardValue1(ref Dot best_move, int pl1, int pl2, ref int depth,
                       ref int counter, Dot lm, ref int counter_root)//возвращает Owner кто побеждает в результате хода
        {
            if (counter >= SkillDepth)
            {
                return 0;
            }

            Dot enemy_move = null;
            var random = new Random(DateTime.Now.Millisecond);
            //array = array.OrderBy(x => random.Next()).ToArray();
            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE & d.Blocked == false & Math.Abs(d.x - lm.x) < SkillNumSq
                                                                    & Math.Abs(d.y - lm.y) < SkillNumSq
                      orderby Math.Sqrt(Math.Abs(d.x - lm.x) * Math.Abs(d.x - lm.x) + Math.Abs(d.y - lm.y) * Math.Abs(d.y - lm.y)) //(random.Next())
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
                    int res_last_move = MakeMove(d);
                    //-----показывает проверяемые ходы--------
                    if (ShowMoves) Pause();
#if DEBUG

                    lstDbg1.Items.Add(d.Own + " - " + d.x + ":" + d.y);
                    txtDbg.Text = "Общее число ходов: " + depth.ToString() +
                                "\r\n Глубина просчета: " + counter.ToString() +
                                "\r\n проверка вокруг точки " + lm;
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
                        if (counter < counter_root)
                        {
                            counter_root = counter;
                            best_move = new Dot(d.x, d.y);
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add("Ход на " + best_move.x + ":" + best_move.y + "; ход " + counter);
#endif
                        }
                        UndoMove(d);
                        return PLAYER_HUMAN;//побеждает игрок
                    }
                    else if (d.Own == 2 & res_last_move != 0 | d.Own == 1 & aDots[d.x, d.y].Blocked)
                    {
                        if (counter < counter_root)
                        {
                            counter_root = counter;
                            best_move = new Dot(d.x, d.y);
                            lst_best_move.Clear();
                            lst_best_move.Add(best_move);
#if DEBUG
                            lstDbg2.Items.Add("Ход на " + best_move.x + ":" + best_move.y + "; ход " + counter);
#endif
                        }
                        UndoMove(d);
                        return PLAYER_COMPUTER;//побеждает компьютер
                    }
                    depth++; counter++;
                    //теперь ходит другой игрок
                    int result = BoardValue(ref enemy_move, pl2, pl1, ref depth, ref counter, lm, ref counter_root);
                    //отменить ход
                    UndoMove(d);
                    counter--;
#if DEBUG
                    lstDbg1.Items.RemoveAt(lstDbg1.Items.Count - 1);
#endif
                    if (depth > SkillLevel * 100)
                    {
                        return PLAYER_NONE;
                    }
                    // if (result > 0) break;

                }
            }
            return PLAYER_NONE;
        }
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
                        int res_last_move = MakeMove(d);
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

        private int Move(ref Dot best_move, int pl1, int pl2, ref int depth,
                       ref int counter, Dot lm, ref int counter_root)
        {
            Dot enemy_move = null;
            if ((counter >= SkillLevel))
            {
                return 0;
            }
            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE & d.Blocked == false & Math.Abs(d.x - lm.x) < 2 & Math.Abs(d.y - lm.y) < 2
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
                    int res_last_move = MakeMove(d);
                    //-----показывает проверяемые ходы--------
#if DEBUG
                    Pause();
                    lstDbg1.Items.Add(d.Own + " - " + d.x + ":" + d.y);
                    txtDbg.Text = "Общее число ходов: " + depth.ToString() +
                                "\r\n Глубина просчета: " + counter.ToString();
#endif
                    //-----------------------------------
                    if (d.Own == 1 & res_last_move != 0)
                    {
                        best_move = d;
                        UndoMove(d);
                        return PLAYER_HUMAN;
                    }
                    else if (d.Own == 2 & res_last_move != 0)
                    {
                        best_move = d;
                        UndoMove(d);
                        return PLAYER_COMPUTER;
                    }
                    depth++;
                    counter++;
                    //теперь ходит другой игрок
                    int result = Move(ref enemy_move, pl2, pl1, ref depth, ref counter, lm, ref counter_root);
                    //отменить ход
                    UndoMove(d);
                    if (result != 0 & counter < counter_root)
                    {
                        counter_root = counter;
                        best_move = enemy_move;
#if DEBUG
                        lstDbg2.Items.Add("Ход на " + best_move.x + ":" + best_move.y + "; ход " + counter);
#endif
                        return 3;
                    }
                    if (enemy_move != null)
                    {
                        best_move = enemy_move;
                    }
                    counter--;
#if DEBUG
                    lstDbg1.Items.RemoveAt(lstDbg1.Items.Count - 1);
#endif
                }
            }
            return PLAYER_NONE;
        }

        //==============================================================================================
        //проверяет ход в результате которого окружение.Возвращает ход
        private Dot CheckMove(int Owner)
        {
            var qry = from Dot d in aDots where d.Own == PLAYER_NONE & d.Blocked==false select d;
            Dot[] ad = qry.ToArray();
            if (ad.Length != 0)
            {
                foreach (Dot d in ad)
                {
                    //делаем ход
                    d.Own = Owner;
                    int result_last_move = MakeMove(d);
                    //-----------------------------------
                    if (result_last_move != 0 & aDots[d.x,d.y].Blocked==false)
                    {
                         UndoMove(d);
                         return d;
                    }
                    UndoMove(d);
                }
            }
            return null;
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
                              "BlokingDots.Count: " + aDots[x, y].BlokingDots.Count + "\r\n" +
                              "Fixed: " + aDots[x, y].Fixed + "\r\n" +
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
                      where d.Own == PLAYER_NONE
                      select d;
            return (qry.Count()==0);
        }
        public Point TranslateCoordinates(Point MousePos)
        {
            //Point p = new Point();
            //int top_x = (int)(startX + 0.5f), top_y = (int)(startY + 0.5f);
            //p.X = top_x + (MousePos.X / (pbxBoard.ClientSize.Width / (iBoardSize + 1)));
            //p.Y = top_y + (MousePos.Y / (pbxBoard.ClientSize.Height / (iBoardSize + 1)));
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
            //gr.DrawEllipse(new Pen(colorCursor, 0.05f), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
            gr.FillEllipse(new SolidBrush(Color.FromArgb(50, colorCursor)), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
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
                gr.FillEllipse(new SolidBrush(Color.FromArgb(50, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(colorGamer, 0.08f), p.x - PointWidth/2, p.y - PointWidth/2, PointWidth, PointWidth );
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
            for (int i = 0; i < 4; i++)
            {
                if (d[i].Marked == false)
                {
                    if (d[i].Own == 0 | d[i].Own == flg_own | d[i].Own != flg_own & d[i].Blocked)
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
        private bool CheckLink(Dot dot)//поверяет свободны ли точки на концах связи
        {
            var qry = from l in lnks
                      where l.Dot1 == dot | l.Dot2 == dot
                      select l;
            foreach (Links link in qry)
            {
                if (link.Dot1.Blocked == false | link.Dot2.Blocked == false) return true;
            }
        return false;
        }
        private void CheckLinkDotsForBlocked()//поверяет свободны ли точки на концах связeй
        {
            var qry = from Links l in lnks
                      select new { l.Dot1, l.Dot2 };

            foreach (var d in qry)
            {
                d.Dot1.Blocked =  DotIsFree(d.Dot1, d.Dot1.Own) ? false : true;
                d.Dot2.Blocked = DotIsFree(d.Dot2, d.Dot2.Own) ? false : true;
            }
           
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
                    if (d.DotsEquals(dts[i]) == false & d.NeiborDots(dts[i]))
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
            // Set transformations for the Graphics object so its coordinate system matches the one specified.
            // Return the horizontal scale.Start from scratch.
            gr.ResetTransform();
            // Scale so the viewport's width and height map to the Graphics object's width and height.
            gr.ScaleTransform(gr_width / (right_x - left_x), gr_height / (bottom_y - top_y));
            // Translate (left_x, top_y) to the Graphics object's origin.
            gr.TranslateTransform(-left_x, -top_y);
            var transform = new Matrix();
            //transform.Scale(scale, scale);
            _transform = gr.Transform;
        }
        private float SquarePolygon(int dotsRegion, int dotsInRegion)
        {
            return dotsInRegion + dotsRegion / 2.0f - 1;//Формула Пика
        }
        public int MakeMove(Dot dot)//Основная функция - ход игрока - возвращает количество блокированных точек - функция оболочка для CheckBlocked, 
        {
            if (aDots.Contains(dot) == false) return 0;
            if (aDots[dot.x, dot.y].Own == 0)//если точка не занята
            {
                aDots.Add(dot);
                //list_moves.Add(aDots[dot.x,dot.y]);//добавим в реестр ходов
            }
            int res = CheckBlocked();
            int dif=count_blocked - res;
            if (dif!= 0) LinkDots();
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
            count_blocked -= dif;
            last_move = dot;//зафиксировать последний ход
            return dif;
        }
        private int CheckBlocked()//проверяет блокировку точек, маркирует точки которые блокируют, возвращает количество окруженных точек
        {
            int counter=0;
            var q = from Dot dots in aDots
                    where dots.Own != 0
                    select dots;
                foreach (Dot d in q)
                {
                aDots.UnmarkAllDots();
                   if (DotIsFree(d, d.Own) == false)
                   {
                       ld.Clear(); lr.Clear();
                       d.Blocked = true;
                       aDots.UnmarkAllDots();
                       MarkDotsInRegion(d, d.Own);
                       counter += 1;
                       foreach (Dot dr in lr)
                       {
                           foreach (Dot bd in ld)
                           {
                                if (dr.BlokingDots.Contains(bd) == false & bd.Own!=0 ) dr.BlokingDots.Add(bd);
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
        List<Dot> ld = new List<Dot>();//список блокированных точек
        List<Dot> lr = new List<Dot>();//список блокирующих точек
        private void MarkDotsInRegion(Dot blocked_dot,int flg_own)//Ставит InRegion=true точкам которые блокируют заданную в параметре точку
        {
            blocked_dot.Marked = true;
            Dot[] dts = new Dot[4] {aDots[blocked_dot.x + 1, blocked_dot.y], aDots[blocked_dot.x - 1, blocked_dot.y],
                                  aDots[blocked_dot.x, blocked_dot.y + 1], aDots[blocked_dot.x, blocked_dot.y - 1]};
            //добавим точки которые попали в окружение
            if (ld.Contains(blocked_dot) == false)
            {
                ld.Add(blocked_dot);
            }
            foreach (Dot _d in dts)
            {
                if (_d.Own != 0 & _d.Own != flg_own)//_d-точка которая окружает
                {
                    //добавим в коллекцию точки которые окружают
                    if (lr.Contains(_d) == false) lr.Add(_d);
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
                ld.Remove(aDots[x, y]);
                bl_dot.Add(aDots[x, y]);
                foreach (Dot d in lr)
                {
                    d.BlokingDots.Remove(aDots[x, y]);
                }
                count_blocked = CheckBlocked();
                aDots.Remove(x, y);
            }
            aDots[x, y].Own = 0;
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
            }
            bl_dot = null;

            //удаляем связи
            foreach (Links l in ln)
            {
                lnks.Remove(l);
            }
            ln = null;
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
            var dts = from Dot d in aDots where d.Own!=0 select d;
            Dot[] save_dots = dts.ToArray();
            Dots_sg[] dots = new Dots_sg[save_dots.Length];
            for (int i =0; i < save_dots.Length; i++)
            {
                dots[i] = new Dots_sg(save_dots[i].x, save_dots[i].y, save_dots[i].Own);
            }
            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path_savegame, FileMode.Create)))
                {
                    // записываем в файл значение каждого поля структуры
                    foreach (Dots_sg s in dots)
                    {
                        writer.Write(s.x);
                        writer.Write(s.y);
                        writer.Write(s.Own);
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
            // создаем объект BinaryReader
            BinaryReader reader = new BinaryReader(File.Open(path_savegame, FileMode.Open));
           // пока не достигнут конец файла считываем каждое значение из файла
                 while (reader.PeekChar() > -1)
                {
                aDots.Add((int)reader.ReadByte(),(int)reader.ReadByte(),(int)reader.ReadByte());
                }
            
            CheckBlocked();//проверяем блокировку
            LinkDots();//восстанавливаем связи между точками
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
