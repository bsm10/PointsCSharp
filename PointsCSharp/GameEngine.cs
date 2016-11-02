using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace DotsGame
{
    public static class GameMessages
    {
        public static string Message {get;set;}

    }
    public partial class GameEngine
    {

        //-------------------------------------------------
        public int iScaleCoef = 1;//-коэффициент масштаба

        public float startX = -0.5f, startY = -0.5f;
        //==========================================================================================================
        private GameDots _gameDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        //===========================================================================================================
        public GameDots gameDots 
        {
            get 
              {
                return _gameDots;
              }
        }

        public Dot DOT(int x, int y)
        {
            return _gameDots[x,y];
        }
        public Dot DOT(Dot d)
        {
            return _gameDots[d.x, d.y];
        }

        private string status=string.Empty;
        public string  Status
        {
            get{return status;}
            set{status=value;}
        }
        //private Form2 f;
        public DebugForm DebugWindow = new DebugForm();

        public bool Autoplay
        {
            get { return DebugWindow.rbtnHand.Checked; }
        }


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

       
        private PictureBox pbxBoard;

        public Dot LastMove
        {
            get
            {
                return gameDots.LastMove;
            }
        }


        public GameEngine(PictureBox CanvasGame)
        {
            pbxBoard = CanvasGame;
            NewGame(Properties.Settings.Default.BoardWidth, Properties.Settings.Default.BoardHeight);
            LoadPattern();
        }
        //  ************************************************

        

        public Dot PickComputerMove(Dot LastMove, CancellationToken? cancellationToken)
        {
            return gameDots.PickComputerMove(LastMove, cancellationToken);
        }
        public int MakeMove(Dot MoveDot)
        {
            return gameDots.MakeMove(MoveDot);
        }
        public bool GameOver()
        {
            return gameDots.Board_ValidMoves.Count == 0;
        }

        public string Statistic()
        {
            var q5 = from Dot d in _gameDots where d.Own == 1 select d;
            var q6 = from Dot d in _gameDots where d.Own == 2 select d;
            var q7 = from Dot d in _gameDots where d.Own== 1 & d.Blocked select d;
            var q8 = from Dot d in _gameDots where d.Own == 2 & d.Blocked select d;
            return q8.Count().ToString() + ":" + q7.Count().ToString();
        }
        public void Statistic(int x, int y)
        {
                #if DEBUG
            DebugWindow.txtDotStatus.Text = "Blocked: " + _gameDots[x, y].Blocked + "\r\n" +
                              "BlokingDots.Count: " + _gameDots[x, y].BlokingDots.Count + "\r\n" +
                              "NeiborDots.Count: " + _gameDots[x, y].NeiborDots.Count + "\r\n" +
                              "Rating: " + _gameDots[x, y].Rating + "\r\n" +
                              "IndexDot: " + _gameDots[x, y].IndexDot + "\r\n" +
                              "IndexRelation: " + _gameDots[x, y].IndexRelation + "\r\n" +
                              "Own: " + _gameDots[x, y].Own + "\r\n" +
                              "X: " + _gameDots[x, y].x + "; Y: " + _gameDots[x, y].y;
               #endif
        }
        public void NewGame(int boardWidth, int boardHeigth)
        {
            //if (gameDots!=null && DebugWindow!=0)

            _gameDots = new GameDots(boardWidth,boardHeigth); 
            lstDotsInPattern = new List<Dot>();
            startX = -0.5f;
            startY = -0.5f;
            gameDots.SetLevel(Properties.Settings.Default.Level);
            Redraw=true;
            pbxBoard.Invalidate();
        }
        //------------------------------------------------------------------------------------

        public void ResizeBoard(int newSizeWidth, int newSizeHeight)//изменение размера доски
        {
            if (newSizeWidth < 5) newSizeWidth = 5;
            else if (newSizeWidth > 40) newSizeWidth = 40;
            if (newSizeHeight < 5) newSizeHeight = 5;
            else if (newSizeHeight > 40) newSizeHeight = 40;

            gameDots.BoardHeight = newSizeHeight;
            gameDots.BoardWidth = newSizeWidth;
            NewGame(newSizeWidth,newSizeHeight);
            pbxBoard.Invalidate();
        }


        private List<List<Dot>> ListRotatePatterns(List<Dot> listPat)
        {
            List<List<Dot>> lstlstPat = new List<List<Dot>>();
            
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = _gameDots.Rotate90(listPat);
            lstlstPat.Add(listPat);

            return lstlstPat;
        }
      
        public void LoadPattern()
        {
            int counter_line = 0;
            try
            {
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
            file.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("LoadPattern \r\n" + e.Message);
            }

        }
        public Dot this[int i, int j]//Индексатор возвращает элемент из массива по его индексу
        {
            get
            {
                return gameDots[i, j];
            }
        }
        public Dot this[Dot dot]//Индексатор возвращает элемент из массива по его индексу
        {
            get
            {
                return gameDots[dot.x, dot.y];
            }
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

        		for (int i = 0; i < _gameDots.ListMoves.Count; i++)
           			{
                        writer.Write((byte)_gameDots.ListMoves[i].x);
                        writer.Write((byte)_gameDots.ListMoves[i].y);
                        writer.Write((byte)_gameDots.ListMoves[i].Own);
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
            _gameDots.Clear();
            Dot d=null;
            try
            {
                // создаем объект BinaryReader
                BinaryReader reader = new BinaryReader(File.Open(path_savegame, FileMode.Open));
                // пока не достигнут конец файла считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    d = new Dot((int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte());
                    _gameDots.MakeMove(d);
                }
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
            _gr = gr;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Устанавливаем масштаб
            SetScale(gr, pbxBoard.ClientSize.Width, pbxBoard.ClientSize.Height,
                startX, startX + gameDots.BoardWidth, startY, gameDots.BoardHeight + startY);
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
            for (float i = 0; i <= gameDots.BoardWidth; i++)
            {
                SolidBrush drB = i == 0 ? new SolidBrush(Color.MediumSeaGreen) : drawBrush;
#if DEBUG
                //gr.DrawString("y" + (i + startY + 0.5f).ToString(), drawFont, drB, startX, i + startY + 0.5f - 0.2f);
                gr.DrawString("x" + (i + startX + 0.5f).ToString(), drawFont, drB, i + startX + 0.5f - 0.2f, startY);
#endif
                gr.DrawLine(boardPen, i + startX + 0.5f, startY + 0.5f, i + startX + 0.5f, gameDots.BoardHeight + startY - 0.5f);
                //gr.DrawLine(boardPen, startX + 0.5f, i + startY + 0.5f, gameDots.BoardWidth + startX - 0.5f, i + startY + 0.5f);
            }
            for (float i = 0; i <= gameDots.BoardHeight; i++)
            {
                SolidBrush drB = i == 0 ? new SolidBrush(Color.MediumSeaGreen) : drawBrush;
#if DEBUG
                gr.DrawString("y" + (i + startY + 0.5f).ToString(), drawFont, drB, startX, i + startY + 0.5f - 0.2f);
                //gr.DrawString("x" + (i + startX + 0.5f).ToString(), drawFont, drB, i + startX + 0.5f - 0.2f, startY);
#endif
                //gr.DrawLine(boardPen, i + startX + 0.5f, startY + 0.5f, i + startX + 0.5f, gameDots.BoardHeight + startY - 0.5f);
                gr.DrawLine(boardPen, startX + 0.5f, i + startY + 0.5f, gameDots.BoardWidth + startX - 0.5f, i + startY + 0.5f);
            }

        }
        public void DrawLinks(Graphics gr)//отрисовка связей
        {
            List<Links> lnks = _gameDots.ListLinks;
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
            List<Dot> lstDotsForDraw = EditMode ? gameDots.Dots : gameDots.ListMoves;
            //отрисовываем поставленные точки
            foreach (Dot p in lstDotsForDraw)
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
                        break;
                }
            }
        }
        private void SetColorAndDrawDots(Graphics gr, Dot p, Color colorGamer) //Вспомогательная функция для DrawPoints. Выбор цвета точки в зависимости от ее состояния и рисование элипса
        {
            Dot last_move =  gameDots.LastMove;
            Color c;
            if (last_move != null && p.x == last_move.x & p.y == last_move.y)//точка последнего хода должна для удовства выделяться
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(140, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.FromArgb(140, Color.WhiteSmoke), 0.08f), p.x - PointWidth / 2, p.y - PointWidth / 2, PointWidth, PointWidth);
                gr.DrawEllipse(new Pen(colorGamer, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth + PointWidth, PointWidth + PointWidth);
            }
            else
            {
                c = p.Blocked ? Color.FromArgb(130, colorGamer) : Color.FromArgb(255, colorGamer);
                gr.FillEllipse(new SolidBrush(c), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
            if (p.PatternsEmptyDot)
            {
                gr.DrawEllipse(new Pen(Color.DarkOliveGreen, 0.05f), p.x - PointWidth * 1.3f, p.y - PointWidth * 1.3f, PointWidth * 1.3f * 2, PointWidth * 1.3f * 2);
            }
            if (p.PatternsMoveDot)
            {
                gr.DrawEllipse(new Pen(Color.LimeGreen, 0.08f), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                gr.DrawEllipse(new Pen(Color.DarkOliveGreen, 0.05f), p.x - PointWidth * 1.3f, p.y - PointWidth * 1.3f, PointWidth * 1.3f * 2, PointWidth * 1.3f * 2);
            }
            if (p.PatternsFirstDot)
            {
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
        public void UndoDot (Dot dot_move)
        {
            gameDots.UndoMove(dot_move);
        }
        //=========================================================================
#if DEBUG
        public void MoveDebugWindow(int top, int left, int width)
        {
            DebugWindow.Top = top;
            DebugWindow.Left = left + width;
        }

#endif
        //==========================================================================

    }
}
