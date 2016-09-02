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
    public static class GameMessages
    {
        public static string Message {get;set;}

    }
    public partial class GameEngine
    {
        public int SkillLevel = 5;
        public int SkillDepth = 5;
        public int SkillNumSq = 3;

        //-------------------------------------------------
        public int iScaleCoef = 1;//-коэффициент масштаба

        public float startX = -0.5f, startY = -0.5f;
        //==========================================================================================================
        private GameDots aDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        //===========================================================================================================
        public GameDots gameDots {get {return aDots;}}
        private string status=string.Empty;
        public string  Status
        {
            get{return status;}
            set{status=value;}
        }
        public bool Autoplay
        {
            get { return f.rbtnHand.Checked; }
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
        private int _pause = 10;
        
#if DEBUG
        
        public Form2 f = new Form2();
        public Form2 DebugWindow 
        {
            get {return f;}
        }

#endif


        public GameEngine(PictureBox CanvasGame)
        {
            pbxBoard = CanvasGame;
            NewGame(Properties.Settings.Default.BoardWidth, Properties.Settings.Default.BoardHeight);
            LoadPattern();
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
        public string Statistic()
        {
            var q5 = from Dot d in aDots where d.Own == 1 select d;
            var q6 = from Dot d in aDots where d.Own == 2 select d;
            var q7 = from Dot d in aDots where d.Own== 1 & d.Blocked select d;
            var q8 = from Dot d in aDots where d.Own == 2 & d.Blocked select d;
            return q8.Count().ToString() + ":" + q7.Count().ToString();
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
        public void NewGame(int boardWidth, int boardHeigth)
        {
            aDots = new GameDots(boardWidth,boardHeigth); 
            lstDotsInPattern = new List<Dot>();
            startX = -0.5f;
            startY = -0.5f;
            SetLevel(Properties.Settings.Default.Level);
            Redraw=true;
#if DEBUG
        f.Show();

#endif
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
            Properties.Settings.Default.BoardWidth = newSizeWidth;
            Properties.Settings.Default.BoardHeight = newSizeHeight;
            NewGame(Properties.Settings.Default.BoardWidth, Properties.Settings.Default.BoardHeight);
            pbxBoard.Invalidate();
        }


        private List<List<Dot>> ListRotatePatterns(List<Dot> listPat)
        {
            List<List<Dot>> lstlstPat = new List<List<Dot>>();
            
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
            Dot d=null;
            try
            {
                // создаем объект BinaryReader
                BinaryReader reader = new BinaryReader(File.Open(path_savegame, FileMode.Open));
                // пока не достигнут конец файла считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    d = new Dot((int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte());
                    aDots.MakeMove(d);
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
