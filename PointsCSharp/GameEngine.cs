using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;


namespace DotsGame
{
    public partial class GameEngine
    {
        private const int PLAYER_DRAW = -1;
        private const int PLAYER_NONE = 0;
        private const int PLAYER_HUMAN = 1;
        private const int PLAYER_COMPUTER = 2;
        public int SkillLevel = 5;
        public int SkillDepth = 5;
        public int SkillNumSq = 3;

        //-------------------------------------------------
        public int iScaleCoef = 1;//-коэффициент масштаба
        public int iBoardSize = 10;//-количество клеток квадрата в длинну
        //public int iMapSize;//-количество клеток квадрата в длинну -размер всей карты
        public const int iBoardSizeMin = 5;
        public const int iBoardSizeMax = 20;

        public float startX = -0.5f, startY = -0.5f;
        private GameDots aDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        public GameDots gameDots {get {return aDots;}}

        //public List<Dot> lstDots;

        //private List<Links> lnks;
        private Dot best_move; //ход который должен сделать комп
        private Dot last_move; //последний ход

        //private int win_player;//переменная получает номер игрока, котрый окружил точки

        private string status=string.Empty;
        public string  Status
        {
            get{return status;}
            set{status=value;}
        }
        public bool Autoplay
        {
            get { return f.rbtnHand.Checked; }
            //set { f.rbtnHand.Checked = value; }
        }

        public Dot LastMove
        {
            get
            {
                if(last_move==null)//когда выбирается первая точка для хода
                {
                    var random = new Random(DateTime.Now.Millisecond);
                    var q = from Dot d in aDots where d.x <= iBoardSize / 2 & d.x > iBoardSize / 3 
                                                    & d.y <= iBoardSize / 2 & d.y > iBoardSize / 3
                                                    orderby (random.Next())
                                                    select d;
                    
                    last_move = q.First();//это для того чтобы поставить первую точку                
                }
                return last_move;
            }
        }

        public List<Dot> dots_in_region;//записывает сюда точки, которые окружают точку противника
        //=========== цвета, шрифты ===================================================
        public Color colorGamer1 = Properties.Settings.Default.Color_Gamer1,
                           colorGamer2 = Properties.Settings.Default.Color_Gamer2,
                           colorCursor = Properties.Settings.Default.Color_Cursor;
        private float PointWidth = 0.18f;
        public Pen boardPen = new Pen(Color.FromArgb(150,200,200), 0.08f);//(Color.DarkSlateBlue, 0.05f);
        private SolidBrush drawBrush = new SolidBrush(Color.MediumPurple);
        public Font drawFont = new Font("Arial", 0.22f);
        public bool Redraw {get; set;}
        //===============================================================================
        public Graphics _gr;
        public Graphics GraphicsGame
        {
            get { return _gr; }
        }
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
        
        public Form2 f = new Form2();
        public Form2 DebugWindow 
        {
            get {return f;}
        }

#endif

#if DEBUG
        Stopwatch stopWatch = new Stopwatch();//для диагностики времени выполнения
        Stopwatch sW_BM = new Stopwatch();
        Stopwatch sW2 = new Stopwatch();
#endif

        public GameEngine(PictureBox CanvasGame)
        {
            pbxBoard = CanvasGame;
            LoadPattern();
            NewGame();
        }
        public void SetLevel(int iLevel=1)
        {
            switch (iLevel)
            {
                case 0://easy
                    SkillLevel = 10;
                    SkillDepth = 5;
                    SkillNumSq = 3;
                    break;
                case 1://mid
                    SkillLevel = 30;
                    SkillDepth = 10;//20;
                    SkillNumSq = 4;
                    break;
                case 2://hard
                    SkillLevel = 50;
                    SkillDepth = 50;//50;
                    SkillNumSq = 2;//5;
                    break;
            }
            Properties.Settings.Default.Level=iLevel;
            Properties.Settings.Default.Save();
#if DEBUG
            f.numericUpDown2.Value = SkillDepth;
            f.numericUpDown4.Value = SkillNumSq;
            f.numericUpDown3.Value = SkillLevel;
#endif
        }
        //  ************************************************
        public Dot PickComputerMove(Dot enemy_move)
        {
            #region если первый ход выбираем произвольную соседнюю точку
            if (aDots.ListMoves.Count < 2)
            {
                var random = new Random(DateTime.Now.Millisecond);
                var fm = from Dot d in aDots
                         where d.Own==0 & Math.Sqrt(Math.Pow(Math.Abs(d.x -enemy_move.x), 2) + Math.Pow(Math.Abs(d.y -enemy_move.y), 2)) < 2
                         orderby random.Next()
                         select d;
                return new Dot(fm.First().x, fm.First().y); //так надо чтобы best_move не ссылался на точку в aDots;
            }
            #endregion
#region  Если ситуация проигрышная -сдаемся          
            var q1 = from Dot d in aDots
                     where d.Own == PLAYER_COMPUTER && (d.Blocked == false)
                    select d;
            var q2 = from Dot d in aDots
                     where d.Own == PLAYER_HUMAN && (d.Blocked == false)
                     select d;
            float res1=q2.Count();
            float res2=q1.Count();
            if (res1/res2>2.0)
            {
                return null;
            }
            
#endregion


            float s1 = square1; float s2 = square2;
            int pl1=0; int pl2=0;
            pl1 = enemy_move.Own == PLAYER_HUMAN ? PLAYER_HUMAN : PLAYER_COMPUTER;
            pl2 = pl1 == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
            //if (enemy_move.Own == PLAYER_HUMAN) { pl1 = PLAYER_HUMAN; pl2 = PLAYER_COMPUTER; }
            //else if (enemy_move.Own == PLAYER_COMPUTER) { pl1 = PLAYER_COMPUTER; pl2 = PLAYER_HUMAN; }
            best_move=null;
            int depth=0;
            var t1 = DateTime.Now.Millisecond;
#if DEBUG
            stopWatch.Start();
#endif
            Dot lm = new Dot(last_move.x, last_move.y);//точка последнего хода
            //проверяем ход который ведет сразу к окружению и паттерны
            //BestMove(pl1, pl2);
            int c1 = 0, c_root = 1000;// , dpth=0;
            lst_best_move.Clear();

#if DEBUG
                f.lstDbg2.Items.Clear();
                f.lstDbg1.Items.Clear();
#endif
                Dot dot1 = null, dot2 =null;
                //PLAYER_HUMAN -ставим в параметр -первым ходит игрок1(человек)
                Play(ref best_move, dot1, dot2, PLAYER_HUMAN, PLAYER_COMPUTER, ref depth, ref c1, lm, ref c_root);
                //Play1(ref best_move, dot1, dot2, PLAYER_HUMAN, ref depth, ref c1, lm, ref c_root);
            if (best_move == null)
            {
                //MessageBox.Show("best_move == null");
                var random = new Random(DateTime.Now.Millisecond);
                var q = from Dot d in aDots//любая точка
                        where d.Blocked == false & d.Own == PLAYER_NONE 
                        orderby random.Next()
                        select d;

                if (q.Count() > 0) best_move = q.First();
                else return null;
            }

#if DEBUG
            stopWatch.Stop();

            f.txtDebug.Text = "Skilllevel: " + SkillLevel + "\r\n Общее число ходов: " + depth.ToString() +
            "\r\n Глубина просчета: " + c_root.ToString() +
            "\r\n Ход на " + best_move.x + ":" + best_move.y +
            "\r\n время просчета " + stopWatch.ElapsedMilliseconds.ToString() + " мс";
            stopWatch.Reset();
#endif

            square1 =s1; square2=s2;

            return new Dot(best_move.x,best_move.y); //так надо чтобы best_move не ссылался на точку в aDots
        }
        //===============================================================================================
        //-----------------------------------Поиск лучшего хода------------------------------------------
        //===============================================================================================
        private Dot BestMove(int pl1, int pl2)
        {
            String strDebug = String.Empty;
            Dot bm;
#if DEBUG
            sW2.Start();
            f.lblBestMove.Text = "CheckMove(pl2,pl1)...";
            Application.DoEvents();
#endif
            bm = aDots.CheckMove(pl2);
            if (bm != null)
            {
                bm.iNumberPattern = 777; //777-ход в результате которого получается окружение -компьютер побеждает
                return bm;
            }
            bm = aDots.CheckMove(pl1);
            if (bm != null)
            {
                bm.iNumberPattern = 666; //666-ход в результате которого получается окружение -компьютер проигрывает
                return bm;
            }
            #region DEBUG
#if DEBUG
            sW2.Stop();
            strDebug = "CheckMove pl1,pl2 -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            //проверяем паттерны
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern_vilochka проверяем ходы на два вперед...";
            Application.DoEvents();
#endif
            #endregion
            #region CheckPattern_vilochka
            bm = aDots.CheckPattern_vilochka(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPattern_vilochka " + aDots.iNumberPattern);
                }
#endif
                #endregion
                return bm;
            }

            bm = aDots.CheckPattern_vilochka(pl1);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG

                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl1 + " -CheckPattern_vilochka " + aDots.iNumberPattern);
                }
#endif
                #endregion
                return bm;
            }
            #region DEBUG

#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern_vilochka -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern2Move...";

            Application.DoEvents();
#endif
            #endregion
            #endregion
            #region CheckPattern2Move проверяем ходы на два вперед

//            bm = aDots.CheckPattern2Move(pl2);
//            if (bm != null )
//            {
//                #region DEBUG
//#if DEBUG
//                {
//                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPattern2Move!");
//                }
//#endif
//                #endregion
//                return bm;
//            }
//            #region DEBUG
//#if DEBUG
//            sW2.Stop();
//            strDebug = strDebug + "\r\nCheckPattern2Move(pl2) -" + sW2.Elapsed.Milliseconds.ToString();
//            f.txtBestMove.Text = strDebug;
//            sW2.Reset();
//            sW2.Start();
//            f.lblBestMove.Text = "CheckPatternVilkaNextMove...";
//            Application.DoEvents();
//#endif

            //#endregion
            #endregion
            #region CheckPatternVilkaNextMove
            bm = aDots.CheckPatternVilkaNextMove(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + "CheckPatternVilkaNextMove " + aDots.iNumberPattern);
                }
#endif
                #endregion
                return bm;
            }
            #region DEBUG

#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPatternVilkaNextMove -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern(pl2)...";
            Application.DoEvents();
#endif
            #endregion
            #endregion
            #region CheckPattern

            bm = aDots.CheckPattern(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPattern " + bm.iNumberPattern);
                }
#endif
                #endregion

                if (aDots.CheckDot(bm, pl2) == false) return bm;
            }
            #region Debug
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern(pl2) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern(pl1)...";
            Application.DoEvents();
#endif
            #endregion
            bm = aDots.CheckPattern(pl1);
            if (bm != null)
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl1 + " -CheckPattern " + bm.iNumberPattern);
                }
#endif
                #endregion

                if (aDots.CheckDot(bm, pl2) == false) return bm;
            }
            #region DEBUG
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern(pl1) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPatternMove...";
            Application.DoEvents();
#endif
            #endregion
            #endregion

            #region CheckPatternMove
            bm = aDots.CheckPatternMove(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPatternMove " + aDots.iNumberPattern);
                }
#endif
                #endregion
                if (aDots.CheckDot(bm, pl2) == false) return bm;
            }
            bm = aDots.CheckPatternMove(pl1);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPatternMove " + aDots.iNumberPattern);
                }
#endif
                #endregion
                if (aDots.CheckDot(bm, pl1) == false) return bm;
            }
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPatternMove(pl2) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            Application.DoEvents();
            sW2.Reset();
#endif

            #endregion

            return null;
        }

        private Dot BestMove_MultiTread(int pl1, int pl2)
        {
            String strDebug = String.Empty;
            Dot bm;
#if DEBUG
            sW2.Start();
            f.lblBestMove.Text = "CheckMove(pl2,pl1)...";
            Application.DoEvents();
#endif
            bm = aDots.CheckMove(pl2);
            if (bm != null)
            {
                bm.iNumberPattern = 777; //777-ход в результате которого получается окружение -компьютер побеждает
                return bm;
            }
            bm = aDots.CheckMove(pl1);
            if (bm != null)
            {
                bm.iNumberPattern = 666; //666-ход в результате которого получается окружение -компьютер проигрывает
                return bm;
            }
            #region DEBUG
#if DEBUG
            sW2.Stop();
            strDebug = "CheckMove pl1,pl2 -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            //проверяем паттерны
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern2Move проверяем ходы на два вперед...";
            Application.DoEvents();
#endif
            #endregion


            Task<Dot>[] taskArray = new Task<Dot>[4];
            GameDots[] ad_Array = new GameDots[4] { aDots.CopyDots,
                                                     aDots.CopyDots,
                                                     aDots.CopyDots,
                                                     aDots.CopyDots };


            taskArray[0] = Task<Dot>.Factory.StartNew(() =>
            {
                return aDots.CheckPattern(pl2);
            }, TaskCreationOptions.AttachedToParent);
            taskArray[1] = Task<Dot>.Factory.StartNew(() =>
            {
                return aDots.CheckPattern(pl1);
            }, TaskCreationOptions.AttachedToParent);

            taskArray[2] = Task<Dot>.Factory.StartNew(() =>
            {
                return aDots.CheckPattern_vilochka(pl2);
            }, TaskCreationOptions.AttachedToParent);

            taskArray[3] = Task<Dot>.Factory.StartNew(() =>
            {
                return aDots.CheckPattern_vilochka(pl1);
            }, TaskCreationOptions.AttachedToParent);

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token; 
            
            #region CheckPattern_vilochka

            //taskArray[2].Wait();
            bm = taskArray[2].Result;
            taskArray[2].Dispose();

            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPattern_vilochka " + aDots.iNumberPattern);
                }
#endif
                #endregion
                return bm;
            }

            //taskArray[3].Wait();
            bm = taskArray[3].Result;
            taskArray[3].Dispose();
            if (bm != null )
            {
                #region DEBUG
#if DEBUG

                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl1 + " -CheckPattern_vilochka " + aDots.iNumberPattern);
                }
#endif
                #endregion
                return bm;
            }
            #region DEBUG

#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern_vilochka -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPatternVilkaNextMove...";
            
            Application.DoEvents();
#endif
            #endregion
            #endregion
            #region CheckPattern2Move проверяем ходы на два вперед

//            bm = aDots.CheckPattern2Move(pl2);
//            if (bm != null )
//            {
//                #region DEBUG
//#if DEBUG
//                {
//                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPattern2Move!");
//                }
//#endif
//                #endregion
//                return bm;
//            }
            #region DEBUG
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern2Move(pl2) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern_vilochka...";
            Application.DoEvents();
#endif

            #endregion
            #endregion
            #region CheckPatternVilkaNextMove
            bm = aDots.CheckPatternVilkaNextMove(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + "CheckPatternVilkaNextMove " + aDots.iNumberPattern);
                }
#endif
                #endregion
                return bm;
            }
            #region DEBUG

#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPatternVilkaNextMove -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern(pl2)...";
            Application.DoEvents();
#endif
            #endregion
            #endregion
            #region CheckPattern

            bm = taskArray[0].Result; //CheckPattern(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPattern " + bm.iNumberPattern);
                }
#endif
                #endregion

                if (aDots.CheckDot(bm, pl2) == false) return bm;
            }
            #region Debug
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern(pl2) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPattern(pl1)...";
            Application.DoEvents();
#endif
            #endregion
            bm = taskArray[1].Result; //CheckPattern(pl1);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl1 + " -CheckPattern " + bm.iNumberPattern);
                }
#endif
                #endregion

                if (aDots.CheckDot(bm, pl2) == false) return bm;
            }
            #region DEBUG
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPattern(pl1) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            sW2.Reset();
            sW2.Start();
            f.lblBestMove.Text = "CheckPatternMove...";
            Application.DoEvents();
#endif
            #endregion
            #endregion
            #region CheckPatternMove
            bm = aDots.CheckPatternMove(pl2);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPatternMove " + aDots.iNumberPattern);
                }
#endif
                #endregion
                if (aDots.CheckDot(bm, pl2) == false) return bm;
            }
            bm = aDots.CheckPatternMove(pl1);
            if (bm != null )
            {
                #region DEBUG
#if DEBUG
                {
                    f.lstDbg2.Items.Add(bm.x + ":" + bm.y + " player" + pl2 + " -CheckPatternMove " + aDots.iNumberPattern);
                }
#endif
                #endregion
                if (aDots.CheckDot(bm, pl1) == false) return bm;
            }
#if DEBUG
            sW2.Stop();
            strDebug = strDebug + "\r\nCheckPatternMove(pl2) -" + sW2.Elapsed.Milliseconds.ToString();
            f.txtBestMove.Text = strDebug;
            Application.DoEvents();
            sW2.Reset();
#endif

            #endregion

            return null;
        }

        private void CloseTasks(Task<Dot>[] taskArray)
        {
            for(int i = 0; i < taskArray.Length; i++)
            {
                if (taskArray[i].IsCompleted | taskArray[i].IsCanceled | taskArray[i].IsFaulted)
                {
                    taskArray[i].Dispose();
                }
            }
        }


        //==================================================================================================================
        List<Dot> lst_best_move = new List<Dot>();//сюда заносим лучшие ходы
        int res_last_move; //хранит результат хода
        //===================================================================================================================

        private int Play(ref Dot best_move, Dot move1, Dot move2, int player1, int player2, ref int count_moves, 
                               ref int recursion_depth, Dot lastmove, ref int counter_root)//возвращает Owner кто побеждает в результате хода
        {
#region Debug Skill
#if DEBUG
            SkillDepth=(int)f.numericUpDown2.Value;
            SkillNumSq = (int)f.numericUpDown4.Value;
            SkillLevel = (int)f.numericUpDown3.Value;
#endif
#endregion
            recursion_depth++; 
            if (recursion_depth >= 8)//SkillDepth)
            {
                return 0;
            }
            Dot enemy_move = null;
            #if DEBUG
                sW_BM.Start();
            #endif
            
            //проверяем ход который ведет сразу к окружению и паттерны
            best_move = BestMove(player1, player2);
            
            #if DEBUG
                sW_BM.Stop();
                f.lblBestMove.Text = "BestMove player" + player1 + " -" + sW_BM.Elapsed.Milliseconds.ToString();
                Application.DoEvents();
                sW_BM.Reset();
            #endif

                if (best_move!=null && best_move.iNumberPattern == 777)
                {
                    return PLAYER_COMPUTER;
                }
                else if (best_move != null && best_move.iNumberPattern == 666)
                {
                    return PLAYER_HUMAN;
                }
                else if (best_move != null)
                {
                    if (aDots.CheckDot(best_move, player2)) best_move = null;
                    if (best_move != null) return PLAYER_COMPUTER;
                }

            var qry = from Dot d in aDots
                      where d.Own == PLAYER_NONE & d.Blocked == false & Math.Abs(d.x -lastmove.x) < SkillNumSq
                                                                    & Math.Abs(d.y -lastmove.y) < SkillNumSq
                      orderby Math.Sqrt(Math.Pow(Math.Abs(d.x -lastmove.x), 2) + Math.Pow(Math.Abs(d.y -lastmove.y), 2))
                      select d;
            Dot[] ad = qry.ToArray();
            int i = ad.Length;
            if (i != 0)
            {
                string sfoo = "";
                #region Cycle
                foreach (Dot d in ad)
                {
                    Application.DoEvents();
                    //player2=1;
                    player2 = player1 == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
                    //if (count_moves>SkillLevel) break;
                    //**************делаем ход***********************************
                    res_last_move = aDots.MakeMove(d,player2);
                    count_moves++;
                    #region проверка на окружение

                    if (aDots.win_player == PLAYER_COMPUTER)
                    {
                        best_move = d;
                        aDots.UndoMove(d);
                        return PLAYER_COMPUTER;
                    }

                    //если ход в заведомо окруженный регион -пропускаем такой ход
                    if (aDots.win_player == PLAYER_HUMAN)
                    {
                        aDots.UndoMove(d);
                        continue;
                    }

                    #endregion
                    #region проверяем ход чтобы точку не окружили на следующем ходу
                    sfoo = "CheckMove player" + player1;
                    best_move = aDots.CheckMove(player1);
                    if (best_move == null)
                    {
                        sfoo = "next move win player" + player2;
                        best_move = aDots.CheckMove(player2);
                        if (best_move != null)
                        {
                            best_move = d;
                            aDots.UndoMove(d);
                            return player2;
                        }
                    }
                    else
                    {
                        aDots.UndoMove(d);
                        continue;
                    }
                    #endregion
#region Debug statistic
#if DEBUG
                    if (f.chkMove.Checked) Pause(); //делает паузу если значение поля pause>0
                    f.lstDbg1.Items.Add(d.Own + " -" + d.x + ":" + d.y);
                    f.txtDebug.Text = "Общее число ходов: " + count_moves.ToString() +
                                       "\r\n Глубина просчета: " + recursion_depth.ToString() +
                                       "\r\n проверка вокруг точки " + lastmove +
                                       "\r\n move1 " + move1 +
                                       "\r\n move2 " + move2 +
                                       "\r\n время поиска " + stopWatch.ElapsedMilliseconds;
#endif
#endregion
                    //теперь ходит другой игрок ===========================================================================
                    int result = Play(ref enemy_move, move1,move2, player2, player1, ref count_moves, ref recursion_depth, lastmove, ref counter_root);
                    //отменить ход
                    aDots.UndoMove(d);
                    recursion_depth--;
#if DEBUG
                    if (f.lstDbg1.Items.Count > 0) f.lstDbg1.Items.RemoveAt(f.lstDbg1.Items.Count -1);
#endif
                    if (count_moves > 8)//SkillLevel)
                        return PLAYER_NONE;
                    if (result!=0)
                    {
                        //best_move = enemy_move;
                        best_move = d;    
                        return result;
                    }
                    //это конец тела цикла
                }
                #endregion
            }
            return PLAYER_NONE;
        }

        //******************************************************************************************************************
//        private int OldPlay1(ref Dot best_move, Dot move1, Dot move2, int player, ref int count_moves,
//                               ref int recursion_depth, Dot lastmove, ref int counter_root)//возвращает Owner кто побеждает в результате хода
//        {
//#if DEBUG
//            SkillDepth = (int)f.numericUpDown2.Value;
//            SkillNumSq = (int)f.numericUpDown4.Value;
//            SkillLevel = (int)f.numericUpDown3.Value;
//#endif
//            recursion_depth++;
//            //if (recursion_depth >= SkillDepth)
//            if (recursion_depth >= 4)
//            {
//                return 0;
//            }
//            Dot enemy_move = null;

//            var qry = from Dot d in aDots
//                      where d.Own == PLAYER_NONE & d.Blocked == false & Math.Abs(d.x -lastmove.x) < SkillNumSq
//                                                                    & Math.Abs(d.y -lastmove.y) < SkillNumSq
//                      orderby Math.Sqrt(Math.Pow(Math.Abs(d.x -lastmove.x), 2) + Math.Pow(Math.Abs(d.y -lastmove.y), 2))
//                      select d;
//            Dot[] ad = qry.ToArray();
//            int i = ad.Length;
//            if (i != 0)
//            {
//                foreach (Dot d in ad)
//                {
//                    //**************делаем ход***********************************
//                    d.Own = player == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
//                    int res_last_move = (int)MakeMove(d);
//                    count_moves++;
//                    #region проверить не замыкается ли регион
//                    //проверяем ход который ведет сразу к окружению
//                    Dot dt = CheckMove(player, false);
//                    if (dt != null)
//                    {
//                        if (recursion_depth < 2)
//                        {
//                            counter_root = recursion_depth;
//                            best_move = dt.Own == PLAYER_COMPUTER ? d : dt;
//                            lst_best_move.Clear();
//                            lst_best_move.Add(best_move);
//#if DEBUG
//                            f.lstDbg2.Items.Add(recursion_depth + " CheckMove(Play) " + best_move.x + ":" + best_move.y + "; win player " + player);
//#endif
//                            UndoMove(d);
//                            return player;
//                        }
//                    }
//                    #endregion
//                    player = d.Own;
//                    if (player == 1 & recursion_depth < 3) move1 = d;
//                    if (player == 2 & recursion_depth < 2) move2 = d;
//                    //-----показывает проверяемые ходы--------
                    
//                    #region Debug
//#if DEBUG
//                    if (f.chkMove.Checked) Pause(); //делает паузу если значение поля pause>0
//                    f.lstDbg1.Items.Add(d.Own + " -" + d.x + ":" + d.y);
//                    f.txtDebug.Text = "Общее число ходов: " + count_moves.ToString() +
//                                       "\r\n Глубина просчета: " + recursion_depth.ToString() +
//                                       "\r\n проверка вокруг точки " + lastmove +
//                                       "\r\n move1 " + move1 +
//                                       "\r\n move2 " + move2 +
//                                       "\r\n время поиска " + stopWatch.ElapsedMilliseconds;
//#endif
//                    #endregion
//                    #region Проверка res_last_move если больше нуля значит кто-то окружает
//                    //-----------------------------------
//                    if (res_last_move != 0 & aDots[d.x, d.y].Blocked)//если ход в окруженный регион
//                    {
//                        best_move = null;
//                        UndoMove(d);
//                        return d.Own == PLAYER_HUMAN ? PLAYER_COMPUTER : PLAYER_HUMAN;
//                    }
//                    if (d.Own == 1 & res_last_move != 0)
//                    {
//                        if (recursion_depth < counter_root)
//                        {
//                            counter_root = recursion_depth;
//                            best_move = new Dot(move1.x, move1.y);
//                            lst_best_move.Clear();
//                            lst_best_move.Add(best_move);
//#if DEBUG
//                            f.lstDbg2.Items.Add(recursion_depth + "ход на " + best_move.x + ":" + best_move.y + "; win HUMAN");
//#endif
//                        }
//                        UndoMove(d);
//                        return PLAYER_HUMAN;//побеждает игрок
//                    }
//                    else if (d.Own == 2 & res_last_move != 0 | d.Own == 1 & aDots[d.x, d.y].Blocked)
//                    {
//                        if (recursion_depth < counter_root)
//                        {
//                            counter_root = recursion_depth;
//                            best_move = new Dot(move2.x, move2.y);
//                            lst_best_move.Clear();
//                            lst_best_move.Add(best_move);
//#if DEBUG
//                            f.lstDbg2.Items.Add(recursion_depth + " ход на " + best_move.x + ":" + best_move.y + "; win COM");
//#endif
//                        }
//                        UndoMove(d);
//                        return PLAYER_COMPUTER;//побеждает компьютер
//                    }
//                    #endregion
//                    //теперь ходит другой игрок =========================================================================================
//                    int result = Play1(ref enemy_move, move1, move2, player, ref count_moves, ref recursion_depth, lastmove, ref counter_root);
//                    //отменить ход
//                    UndoMove(d);
//                    recursion_depth--;
//#if DEBUG
//                    if (f.lstDbg1.Items.Count > 0) f.lstDbg1.Items.RemoveAt(f.lstDbg1.Items.Count -1);
//#endif
//                    if (enemy_move == null & recursion_depth > 2)
//                        break;
//                    //if (count_moves > SkillLevel * 10)
//                    if (count_moves > 4)
//                        return PLAYER_NONE;
//                    //if (result != 0)
//                    //{
//                    //    best_move = enemy_move;
//                    //    return result;
//                    //}
//                }
//            }
//            return PLAYER_NONE;
//        }

        //**************************************************************************************************************
        private int FindMove(ref Dot move, Dot last_mv)//возвращает Owner кто побеждает в результате хода
        {
            int depth = 0, counter = 0, counter_root = 1000, own;
            own = PLAYER_HUMAN;//последним ходил игрок
            List<Dot> mvs = new List<Dot>();
            Dot[] ad=null;
            int minX = aDots.MinX();
            int minY = aDots.MinY();
            int maxX = aDots.MaxX();
            int maxY = aDots.MaxY();

            int i = 0;
            do
            {
                if (i == 0)
                {
                    var qry = from Dot d in aDots
                              where d.Own == PLAYER_NONE & d.Blocked == false
                                                        & d.x <= maxX + 1 & d.x >= minX -1
                                                        & d.y <= maxY + 1 & d.y >= minY -1
                              orderby d.x
                              select d;
                    ad = qry.ToArray();
                    if (qry.Count() == 0)
                    {
                        foreach (Dot d in mvs)
                        {
                            aDots.UndoMove(d);
                        }
                        mvs.Clear();
                        qry = null;
                        i++;
                    }
                }
                else if (i == 1)
                {
                    var qry1 = from Dot d in aDots
                              where d.Own == PLAYER_NONE & d.Blocked == false
                                                        & d.x <= maxX + 1 & d.x >= minX -1
                                                        & d.y <= maxY + 1 & d.y >= minY -1
                              orderby d.y descending
                              select d;
                    ad = qry1.ToArray();
                    if (qry1.Count() == 0)
                    {
                        foreach (Dot d in mvs)
                        {
                            aDots.UndoMove(d);
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
                        int res_last_move = aDots.MakeMove(d,own);
                        mvs.Add(d); 
                        //-----показывает проверяемые ходы-----------------------------------------------
#if DEBUG
                        if (f.chkMove.Checked) Pause();

                        f.lstDbg1.Items.Add(d.Own + " -" + d.x + ":" + d.y);
                        f.txtDebug.Text = "Общее число ходов: " + depth.ToString() +
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
                                f.lstDbg2.Items.Add("Ход на " + move.x + ":" + move.y + "; ход " + counter);
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
                                f.lstDbg2.Items.Add("Ход на " + move.x + ":" + move.y + "; ход " + counter);
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

        public string Statistic()
        {
            var q5 = from Dot d in aDots where d.Own == 1 select d;
            var q6 = from Dot d in aDots where d.Own == 2 select d;
            var q7 = from Dot d in aDots where d.Own== 1 & d.Blocked select d;
            var q8 = from Dot d in aDots where d.Own == 2 & d.Blocked select d;
            return q8.Count().ToString() + ":" + q7.Count().ToString();

            //return "Игрок1 окружил точек: " + q8.Count() + "; \r\n" +
            //  "Игрок1 Захваченая площадь: " + square1 + "; \r\n" +
            //  "Игрок1 точек поставил: " + q5.Count() + "; \r\n" +
            //  "Игрок2 окружил точек: " + q7.Count() + "; \r\n" +
            //  "Игрок2 Захваченая площадь: " + square2 + "; \r\n" +
            //  "Игрок2 точек поставил: " + q6.Count() + "; \r\n";
        }
        public void Statistic(int x, int y)
        {
                #if DEBUG
                f.txtDotStatus.Text = "Blocked: " + aDots[x, y].Blocked + "\r\n" +
                              "BlokingDots.Count: " + aDots[x, y].BlokingDots.Count + "\r\n" +
                              "NeiborDots.Count: " + aDots[x, y].NeiborDots.Count + "\r\n" +
                              "Rating: " + aDots[x, y].Rating + "\r\n" +
                              "IndexDot: " + aDots[x, y].IndexDot + "\r\n" +
                              "IndexRelation: " + aDots[x, y].IndexRelation + "\r\n" +
                              "Own: " + aDots[x, y].Own + "\r\n" +
                              "X: " + aDots[x, y].x + "; Y: " + aDots[x, y].y;
               #endif
        }
        public int pause 
           {
            get
                {
                 return _pause;
                 //
                }
            set
            {
                _pause = value;
            }
            }
        private void Pause()
        {
        #if DEBUG
            if (f.Pause>0)
            {
                Application.DoEvents();
                pbxBoard.Invalidate();
                System.Threading.Thread.Sleep(f.Pause);
            }
        #endif
        }
        public void Pause(int ms)
        {
            Application.DoEvents();
            pbxBoard.Invalidate();
            System.Threading.Thread.Sleep(ms);
        }
        public void NewGame()
        {
            //iMapSize = iBoardSize * iScaleCoef;
            //aDots = new ArrayDots(iMapSize);
            iBoardSize = Properties.Settings.Default.BoardSize;
            aDots = new GameDots(iBoardSize, iBoardSize);

            //lstDots = new List<Dot>(iBoardSize);
            lstDotsInPattern = new List<Dot>();

            //lnks = new List<Links>();
            dots_in_region = new List<Dot>();
            //list_moves = aDots.ListMoves;//new List<Dot>();
            count_dot1 = 0; count_dot2 = 0;
            startX = -0.5f;
            startY = -0.5f;
            square1 = 0; square2 = 0;
            count_blocked1=0;count_blocked2=0;
            count_blocked=0;
            SetLevel(Properties.Settings.Default.Level);
            Redraw=true;
#if DEBUG
        f.Show();

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
        //------------------------------------------------------------------------------------

        private float SquarePolygon(int nBlockedDots, int nRegionDots)
        {
            return nBlockedDots + nRegionDots / 2.0f -1;//Формула Пика
        }

        public void ResizeBoard(int newSize)//изменение размера доски
        {
            iBoardSize=newSize;
            Properties.Settings.Default.BoardSize=newSize;
            NewGame();
            pbxBoard.Invalidate();
        }

        public string Path_PatternData {
            get
            {
                return Application.StartupPath + @"\Resources\patterns.dts"; //@"d:\Proj\PointsCSharp\PointsCSharp\patterns.dts"; 
            }
        }
        public void WritePatternToFile(List<string> lines)
        {
            // Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Path_PatternData, true))
            {

                foreach (string s in lines) file.WriteLine(s);
            }
        }

        private int GetNumberPattern()
        {
            int number = 0;
            string line;
            // Read the file and display it line by line.
            StreamReader file = new StreamReader(Path_PatternData);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Trim() == "Begin")
                {
                    line = file.ReadLine();
                    number = Convert.ToInt32(line);
                }
            }
            file.Close();
            number++;
            return number;
        }
        public void MakePattern()//сохраняет паттерн в текстовое поле
        {
            if (lstDotsInPattern.Count == 0) return;
            List<Dot> lstPat = lstDotsInPattern;
            //rotate dots in pattern
            foreach (List<Dot> listDots in ListRotatePatterns(lstPat)) AddPatternDots(listDots);

            lstPat.Clear();
            //tlsEditPattern.Checked = false;
            aDots.UnmarkAllDots();
            LoadPattern();
        }
        //----------------------------------------------------------
        private void AddPatternDots(List<Dot> ListPatternDots)
        {
            List<string> lines = new List<string>();
            string s = string.Empty;

            int dx, dy;
            Dot firstDot = ListPatternDots.Find(d => d.PatternsFirstDot);
            Dot moveDot = ListPatternDots.Find(dt => dt.PatternsMoveDot);
            //------------------------------------------------
            lines.Add("Begin");
            lines.Add(GetNumberPattern().ToString());
            lines.Add("3,3");
            lines.Add("Dots");

            for (int i = 0; i < ListPatternDots.Count; i++)
            {
                string own = "";
                if (firstDot.Own == ListPatternDots[i].Own) own = "owner";
                if (firstDot.Own != ListPatternDots[i].Own) own = "enemy";
                if (ListPatternDots[i].Own == 0 & ListPatternDots[i].PatternsAnyDot == false) own = "0";
                if (ListPatternDots[i].PatternsAnyDot) own = "!= enemy";
                dx = ListPatternDots[i].x - firstDot.x;
                dy = ListPatternDots[i].y - firstDot.y;
                if ((dx == 0 & dy == 0) == false)
                {
                    s = dx.ToString() + ", " + dy.ToString() + ", " + own;
                    lines.Add(s);
                }
            }
            lines.Add("Result");
            lines.Add((moveDot.x - firstDot.x).ToString() + ", " +
                      (moveDot.y - firstDot.y).ToString());
            lines.Add("End");

            WritePatternToFile(lines);
            s = string.Empty;
            foreach (string st in lines) s = s + st + " \r\n";
            //DebugWindow.txtboxPattern.Text = s;

        }

        //public void MakePattern_old()//сохраняет паттерн в текстовое поле
        //{
        //    string s, strdX, strdY, sWhere = "", sMove = "";
        //    int dx, dy, ind;
        //    ind = lstPat.FindIndex(
        //        delegate (Dot dt)
        //        {
        //            return dt.PatternsFirstDot == true;
        //        });
        //    var random = new Random(DateTime.Now.Millisecond);
        //    string n = random.Next(1, 1000).ToString();
        //    for (int i = 0; i < lstPat.Count; i++)
        //    {
        //        string own = "";
        //        if (lstPat[ind].Own == lstPat[i].Own) own = "== Owner";
        //        if (lstPat[ind].Own != lstPat[i].Own) own = "== enemy_own";
        //        if (lstPat[i].Own == 0 & lstPat[i].PatternsAnyDot==false) own = " == 0";
        //        if (lstPat[i].PatternsAnyDot) own = " != enemy_own";

        //        dx = lstPat[i].x -lstPat[ind].x;
        //        if (dx == 0) strdX = "";
        //        else if (dx > 0) strdX = "+" + dx.ToString();
        //        else strdX = dx.ToString();

        //        dy = lstPat[i].y -lstPat[ind].y;
        //        if (dy == 0) strdY = "";
        //        else if (dy > 0) strdY = "+" + dy.ToString();
        //        else strdY = dy.ToString();

        //        if ((dx == 0 & dy == 0) == false) sWhere += " && aDots[d.x" + strdX + ", d.y" + strdY + "].Own " + own + " && aDots[d.x" + strdX + ", d.y" + strdY + "].Blocked == false \r\n";

        //        if (lstPat[i].PatternsMoveDot)
        //        {
        //            //sMove = " if (pat" + n + ".Count() > 0) return new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + ");";
        //            sMove = " if (pat" + n + ".Count() > 0) \r\n " + "{" +
        //            "result_dot = new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + "); \r\n" +
        //            "result_dot.iNumberPattern = iNumberPattern; \r\n" +
        //            "return result_dot; \r\n " + "}";

        //        }
        //    }
        //    s = "iNumberPattern = " + n + "; \r\n";
        //    s += "var pat" + n + " = from Dot d in aDots where d.Own == Owner \r\n" + sWhere + "select d; \r\n" + sMove + "\r\n";
        //    n += "_2";
        //    sWhere = ""; sMove = "";
        //    for (int i = 0; i < lstPat.Count ; i++)
        //    {
        //        string own = "";
        //        if (lstPat[ind].Own == lstPat[i].Own) own = "== Owner";
        //        if (lstPat[ind].Own != lstPat[i].Own) own = "== enemy_own";
        //        if (lstPat[i].Own == 0 & lstPat[i].PatternsAnyDot == false) own = " == 0";
        //        if (lstPat[i].PatternsAnyDot) own = " != enemy_own";

        //        dx = lstPat[ind].x -lstPat[i].x;
        //        if (dx == 0) strdX = "";
        //        else if (dx > 0) strdX = "+" + dx.ToString();
        //        else strdX = dx.ToString();

        //        dy = lstPat[ind].y -lstPat[i].y;
        //        if (dy == 0) strdY = "";
        //        else if (dy > 0) strdY = "+" + dy.ToString();
        //        else strdY = dy.ToString();
        //        if ((dx == 0 & dy == 0) == false) sWhere += " && aDots[d.x" + strdX + ", d.y" + strdY + "].Own " + own + " && aDots[d.x" + strdX + ", d.y" + strdY + "].Blocked == false \r\n";
        //        if (lstPat[i].PatternsMoveDot)
        //        {
        //            //sMove = " if (pat" + n + ".Count() > 0) return new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + ");";
        //            sMove = " if (pat" + n + ".Count() > 0) \r\n " + "{" +
        //            "result_dot = new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + "); \r\n" +
        //            "result_dot.iNumberPattern = iNumberPattern; \r\n" +
        //            "return result_dot; \r\n " + "}";
        //        }

        //    }
        //    s += "//180 Rotate=========================================================================================================== \r\n";
        //    s += "var pat" + n + " = from Dot d in aDots where d.Own == Owner \r\n" + sWhere + "select d; \r\n" + sMove + "\r\n";

        //    n += "_3";
        //    sWhere = ""; sMove = "";
        //    List<Dot> l =RotateMatrix(90);
        //    for (int i = 0; i < l.Count ; i++)
        //    {
        //        string own = "";
        //        if (l[ind].Own == l[i].Own) own = "== Owner";
        //        if (l[ind].Own != l[i].Own) own = "== enemy_own";
        //        if (l[i].Own == 0 & l[i].PatternsAnyDot == false) own = " == 0";
        //        if (l[i].PatternsAnyDot) own = " != enemy_own";

        //        dx = l[ind].x -l[i].x;
        //        if (dx == 0) strdX = "";
        //        else if (dx > 0) strdX = "+" + dx.ToString();
        //        else strdX = dx.ToString();

        //        dy = l[ind].y -l[i].y;
        //        if (dy == 0) strdY = "";
        //        else if (dy > 0) strdY = "+" + dy.ToString();
        //        else strdY = dy.ToString();
        //        if ((dx == 0 & dy == 0) == false) sWhere += " && aDots[d.x" + strdX + ", d.y" + strdY + "].Own " + own + " && aDots[d.x" + strdX + ", d.y" + strdY + "].Blocked == false \r\n";
        //        if (l[i].PatternsMoveDot)
        //        {
        //            //sMove = " if (pat" + n + ".Count() > 0) return new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + ");";
        //            sMove = " if (pat" + n + ".Count() > 0) \r\n " + "{" +
        //            "result_dot = new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + "); \r\n" +
        //            "result_dot.iNumberPattern = iNumberPattern; \r\n" +
        //            "return result_dot; \r\n" + "}";

        //        }
        //    }
        //    s += "//--------------Rotate on 90-----------------------------------\r\n";
        //    s += "var pat" + n + " = from Dot d in aDots where d.Own == Owner \r\n" + sWhere + "select d; \r\n" + sMove + "\r\n";
        //    n += "_4";
        //    sWhere = ""; sMove = "";
        //    for (int i = 0; i < l.Count ; i++)
        //    {
        //        string own = "";
        //        if (l[ind].Own == l[i].Own) own = "== Owner";
        //        if (l[ind].Own != l[i].Own) own = "== enemy_own";
        //        if (l[i].Own == 0 & l[i].PatternsAnyDot == false) own = " == 0";
        //        if (l[i].PatternsAnyDot) own = " != enemy_own";

        //        dx = l[i].x -l[ind].x;
        //        if (dx == 0) strdX = "";
        //        else if (dx > 0) strdX = "+" + dx.ToString();
        //        else strdX = dx.ToString();

        //        dy = l[i].y -l[ind].y;
        //        if (dy == 0) strdY = "";
        //        else if (dy > 0) strdY = "+" + dy.ToString();
        //        else strdY = dy.ToString();
        //        if ((dx == 0 & dy == 0) == false) sWhere += " && aDots[d.x" + strdX + ", d.y" + strdY + "].Own " + own + " && aDots[d.x" + strdX + ", d.y" + strdY + "].Blocked == false \r\n";
        //        if (l[i].PatternsMoveDot)
        //        {
        //            //sMove = " if (pat" + n + ".Count() > 0) return new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + ");";
        //            sMove = " if (pat" + n + ".Count() > 0) \r\n " + "{" +
        //            "result_dot = new Dot(pat" + n + ".First().x" + strdX + "," + "pat" + n + ".First().y" + strdY + "); \r\n" +
        //            "result_dot.iNumberPattern = iNumberPattern; \r\n" +
        //            "return result_dot; \r\n" + "}";

        //        }
        //    }
        //    s += "//--------------Rotate on 90 -2-----------------------------------\r\n";
        //    s += "var pat" + n + " = from Dot d in aDots where d.Own == Owner \r\n" + sWhere + "select d; \r\n" + sMove + "\r\n";
        //    s += "//============================================================================================================== \r\n";
        //    f.txtDebug.Text = s;
        //    MessageBox.Show("Into clipboard!");
        //    Clipboard.Clear();
        //    Clipboard.SetText(s);

        //    lstPat.Clear();
        //    f.tlsEditPattern.Checked=false;
        //    aDots.UnmarkAllDots();
        //}

        private List<List<Dot>> ListRotatePatterns(List<Dot> listPat)
        {
            List<List<Dot>> lstlstPat = new List<List<Dot>>();

            //List<Dot> l = new List<Dot>(listPat.Count);
            Dot firstDot = listPat.Find(d => d.PatternsFirstDot);
            Dot moveDot = listPat.Find(dt => dt.PatternsMoveDot);

            lstlstPat.Add(listPat);
            listPat = aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = aDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = aDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = aDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);

            return lstlstPat;
        }

        
        public void LoadPattern()
        {
            int counter_line = 0;
            //try
            //{
                
                string line;
                // Read the file and display it line by line.
                StreamReader file = new StreamReader(Path_PatternData);
                Pattern ptrn=new Pattern();
                Patterns.Clear();
                while ((line = file.ReadLine()) != null)
                {
                    counter_line++;
                    switch (line.Trim())
                    {
                        case "Begin":
                            ptrn = new Pattern();
                            //number pattern
                            line = file.ReadLine();
                            counter_line++;
                            int x;
                            if (int.TryParse(line, out x)) ptrn.PatternNumber = x; //Convert.ToInt32(line);
                            //отступы от края поля
                            line = file.ReadLine();
                            counter_line++;
                            string[] sMinMax = line.Replace(" ", string.Empty).Split(new char[] { ',' });
                            if (sMinMax.Length == 1)
                            {
                                ptrn.Xmin = 0;
                                ptrn.Xmax = iBoardSize - 1;
                                ptrn.Ymin = 0;
                                ptrn.Ymax = iBoardSize - 1;

                            }
                            else
                            {
                                ptrn.Xmin = Convert.ToInt32(sMinMax[0]);
                                ptrn.Xmax = iBoardSize - Convert.ToInt32(sMinMax[0]);
                                ptrn.Ymin = Convert.ToInt32(sMinMax[1]);
                                ptrn.Ymax = iBoardSize - Convert.ToInt32(sMinMax[1]);
                            }
                            break;
                        case "Dots": //точки паттерна
                            while ((line = file.ReadLine().Replace(" ", string.Empty)) != "Result")
                            {
                                counter_line++;
                                string[] ss = line.Split(new char[] { ',' });
                                DotInPattern dtp = new DotInPattern();
                                dtp.dX = Convert.ToInt32(ss[0]);
                                dtp.dY = Convert.ToInt32(ss[1]);
                                dtp.Owner = ss[2];
                                ptrn.DotsPattern.Add(dtp);
                            }
                            counter_line++;
                            line = file.ReadLine().Replace(" ", string.Empty);
                            counter_line++;
                            string[] sss = line.Split(new char[] { ',' });
                            ptrn.dXdY_ResultDot.dX = Convert.ToInt32(sss[0]);
                            ptrn.dXdY_ResultDot.dY = Convert.ToInt32(sss[1]);
                            break;
                        case "End":
                           Patterns.Add(ptrn);
                           break;
                        default:
                           break;

                    }

            }



            //    if (line.Trim() == "Begin")
            //    {
            //        ptrn = new Pattern();
            //        while (line != "End")
            //        {
            //            line = file.ReadLine();
            //            counter_line++;
            //            int x;
            //            if (int.TryParse(line, out x)) ptrn.PatternNumber = x; //Convert.ToInt32(line);
            //            line = file.ReadLine();
            //            counter_line++;
            //            if (line.Trim()!="Dots")
            //            {
            //                string[] sMinMax = line.Replace(" ", string.Empty).Split(new char[] { ',' });
            //                if (sMinMax.Length == 1)
            //                {
            //                    ptrn.Xmin = 0;
            //                    ptrn.Xmax = iBoardSize - 1;
            //                    ptrn.Ymin = 0;
            //                    ptrn.Ymax = iBoardSize - 1;

            //                }
            //                else
            //                {
            //                    ptrn.Xmin = Convert.ToInt32(sMinMax[0]);
            //                    ptrn.Xmax = iBoardSize - Convert.ToInt32(sMinMax[0]);
            //                    ptrn.Ymin = Convert.ToInt32(sMinMax[1]);
            //                    ptrn.Ymax = iBoardSize - Convert.ToInt32(sMinMax[1]);
            //                }

            //            }
            //            #region Dots
            //            else if (line.Trim() == "Dots")
            //            {
            //                while ((line = file.ReadLine().Replace(" ", string.Empty)) != "Result")
            //                {
            //                    counter_line++;
            //                    string[] ss = line.Split(new char[] { ',' });
            //                    DotInPattern dtp = new DotInPattern();
            //                    dtp.dX = Convert.ToInt32(ss[0]);
            //                    dtp.dY = Convert.ToInt32(ss[1]);
            //                    dtp.Owner = ss[2];
            //                    ptrn.DotsPattern.Add(dtp);
            //                }
            //                counter_line++;
            //                line = file.ReadLine().Replace(" ", string.Empty);
            //                counter_line++;
            //                string[] sss = line.Split(new char[] { ',' });
            //                ptrn.dXdY_ResultDot.dX = Convert.ToInt32(sss[0]);
            //                ptrn.dXdY_ResultDot.dY = Convert.ToInt32(sss[1]);
            //                line = file.ReadLine().Trim();
            //                counter_line++;
            //            }
            //            #endregion

            //        }
            //        Patterns.Add(ptrn);
            //        counter++;

            //    }
            //    #endregion
            //    next:;
            //}

            file.Close();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message + " \r\n LoadPattern() -" + counter.ToString() +
            //       " \r\n line -" + counter_line.ToString());
            //}

        }

        #region SAVE_LOAD Game
        public string path_savegame = Application.CommonAppDataPath + @"\dots.dts";
        public void SaveGame()
        {
            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path_savegame, FileMode.Create)))
                {

        		for (int i = 0; i < aDots.ListMoves.Count; i++)
           			{
                        writer.Write((byte)aDots.ListMoves[i].x);
                        writer.Write((byte)aDots.ListMoves[i].y);
                        writer.Write((byte)aDots.ListMoves[i].Own);
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
            aDots.ListLinks.Clear();
            aDots.ListMoves.Clear();
            Dot d=null;
            try
            {
                // создаем объект BinaryReader
                BinaryReader reader = new BinaryReader(File.Open(path_savegame, FileMode.Open));
                // пока не достигнут конец файла считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    d = new Dot((int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte());
                    aDots.MakeMove(d, d.Own);
                    aDots.ListMoves.Add(aDots[d.x, d.y]);
                }
                last_move = d;
                //CheckBlocked();//проверяем блокировку
                aDots.LinkDots();//восстанавливаем связи между точками
                aDots.RescanBlockedDots();
                //RescanBlocked();
                //ScanBlockedFreeDots();
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        #endregion
        #region RENDER
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
            _gr = gr;
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
            gr.FillEllipse(new SolidBrush(Color.FromArgb(130, Color.WhiteSmoke)), MousePos.X - PointWidth / 2, MousePos.Y - PointWidth / 2, PointWidth, PointWidth);
            gr.DrawEllipse(new Pen(Color.FromArgb(50, colorCursor), 0.05f), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
            //Отрисовка замкнутого региона игрока1
            DrawLinks(gr);

        }
        public void DrawBoard(Graphics gr)//рисуем доску из клеток
        {
            Pen pen = new Pen(new SolidBrush(Color.MediumSeaGreen), 0.15f);// 0
            for (float i = 0; i <= iBoardSize; i++)
            {
                SolidBrush drB = i == 0 ? new SolidBrush(Color.MediumSeaGreen) : drawBrush;
#if DEBUG
                gr.DrawString("y" + (i + startY + 0.5f).ToString(), drawFont, drB, startX, i + startY + 0.5f - 0.2f);
                gr.DrawString("x" + (i + startX + 0.5f).ToString(), drawFont, drB, i + startX + 0.5f - 0.2f, startY);
#endif
                gr.DrawLine(boardPen, i + startX + 0.5f, startY + 0.5f, i + startX + 0.5f, iBoardSize + startY - 0.5f);
                gr.DrawLine(boardPen, startX + 0.5f, i + startY + 0.5f, iBoardSize + startX - 0.5f, i + startY + 0.5f);
            }
        }
        public void DrawLinks(Graphics gr)//отрисовка связей
        {
            List<Links> lnks = aDots.ListLinks;
            if (lnks != null)
            {
                Pen PenGamer;
                for (int i = 0; i < lnks.Count; i++)
                {
                    if (lnks[i].Blocked)
                    {
                        PenGamer = lnks[i].Dot1.Own == 1 ? new Pen(Color.FromArgb(130, colorGamer1), 0.1f) :
                                                           new Pen(Color.FromArgb(130, colorGamer2), 0.1f);

                        gr.DrawLine(PenGamer, lnks[i].Dot1.x, lnks[i].Dot1.y, lnks[i].Dot2.x, lnks[i].Dot2.y);
                    }
                    else
                    {
                        PenGamer = lnks[i].Dot1.Own == 1 ? new Pen(colorGamer1, 0.1f) : new Pen(colorGamer2, 0.1f);
                        gr.DrawLine(PenGamer, lnks[i].Dot1.x, lnks[i].Dot1.y, lnks[i].Dot2.x, lnks[i].Dot2.y);
                    }
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
                            SetColorAndDrawDots(gr, p, colorGamer1);
                            break;
                        case 2:
                            SetColorAndDrawDots(gr, p, colorGamer2);
                            break;
                        case 0:
                            if (EditMode) SetColorAndDrawDots(gr, p, Color.FromArgb(150, Color.WhiteSmoke));
                            //if (p.PatternsEmptyDot) SetColorAndDrawDots(gr, Color.Bisque, p);
                            //if (p.PatternsAnyDot) SetColorAndDrawDots(gr, Color.DarkOrange, p);
                            //if (p.PatternsFirstDot) SetColorAndDrawDots(gr, Color.DarkOrange, p);
                            break;
                    }
                }
            }
        }
        private void SetColorAndDrawDots(Graphics gr, Dot p, Color colorGamer) //Вспомогательная функция для DrawPoints. Выбор цвета точки в зависимости от ее состояния и рисование элипса
        {

            Color c;
            if (p.Blocked)
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(130, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
            else if (last_move != null && p.x == last_move.x & p.y == last_move.y)//точка последнего хода должна для удовства выделяться
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(140, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.FromArgb(140, Color.WhiteSmoke), 0.08f), p.x - PointWidth / 2, p.y - PointWidth / 2, PointWidth, PointWidth);
                gr.DrawEllipse(new Pen(colorGamer, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth + PointWidth, PointWidth + PointWidth);
            }
            else
            {
                int G = colorGamer.G > 50 ? colorGamer.G - 50 : 120;
                c = p.BlokingDots.Count > 0 ? Color.FromArgb(255, colorGamer.R, G, colorGamer.B) : colorGamer;
                gr.FillEllipse(new SolidBrush(colorGamer), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(c, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
            if (p.PatternsEmptyDot)
            {
                //gr.FillEllipse(new SolidBrush(Color.FromArgb(50, Color.Bisque)), p.x -PointWidth, p.y -PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.DarkOliveGreen, 0.05f), p.x - PointWidth * 1.3f, p.y - PointWidth * 1.3f, PointWidth * 1.3f * 2, PointWidth * 1.3f * 2);
            }
            if (p.PatternsMoveDot)
            {
                //gr.FillEllipse(new SolidBrush(Color.FromArgb(50, Color.Plum)), p.x -PointWidth, p.y -PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.LimeGreen, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.DarkOliveGreen, 0.05f), p.x - PointWidth * 1.3f, p.y - PointWidth * 1.3f, PointWidth * 1.3f * 2, PointWidth * 1.3f * 2);
            }
            if (p.PatternsFirstDot)
            {
                //gr.FillEllipse(new SolidBrush(Color.FromArgb(50, Color.ForestGreen)), p.x -PointWidth, p.y -PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.DarkSeaGreen, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.DarkOliveGreen, 0.05f), p.x - PointWidth * 1.3f, p.y - PointWidth * 1.3f, PointWidth * 1.3f * 2, PointWidth * 1.3f * 2);
            }
            if (p.PatternsAnyDot)
            {
                gr.FillEllipse(new SolidBrush(Color.Yellow), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.Orange, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.DarkOliveGreen, 0.05f), p.x - PointWidth * 1.3f, p.y - PointWidth * 1.3f, PointWidth * 1.3f * 2, PointWidth * 1.3f * 2);
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

        #endregion

        //=========================================================================
#if DEBUG
        public void MoveDebugWindow(int top, int left, int width)
        {
            f.Top = top;
            f.Left = left + width;
        }

#endif
        //==========================================================================

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
