#define DEBUG
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;


namespace DotsGame
{

    public partial class Form1 : Form
    {
#if DEBUG
        Form f = new Form2();
        public ListBox lstDbg1;
        public ListBox lstDbg2;
        public TextBox txtDbg;
#endif

        public int iScaleCoef = 1;//- коэффициент масштаба
        public int iBoardSize = 10 ;//- количество клеток квадрата в длинну
        public int iMapSize;//- количество клеток квадрата в длинну


        private float startX = -0.5f, startY = -0.5f;

        private ArrayDots aDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        private Links[] lnks; //массив где хранятся связи между точками, учавствует в отрисовке
        private Links[] temp_lnks; //массив где хранятся связи между точками, если связи не окружили точку - обнуляется, если да, то добавляется в lnks
        private Point[] pts;

        private Color colorGamer1 = Properties.Settings.Default.Color_Gamer1,
                           colorGamer2 = Properties.Settings.Default.Color_Gamer2,
                           colorCursor = Properties.Settings.Default.Color_Cursor;
        private float PointWidth = 0.25f;
        private Pen boardPen = new Pen(Color.DarkSlateBlue, 0.05f);
        private SolidBrush drawBrush = new SolidBrush(Color.MediumPurple);
        private Font drawFont = new Font("Arial", 0.22f);


        private Point MousePos;
        private Point t;
        private int nRelation = 0;

        //statistic
        private float s1;//площадь занятая игроком1
        private float s2;
        private int count_blocked;//счетчик количества окруженных точек
        private int count_dot1, count_dot2;//количество поставленных точек

        public Form1()
        {
            InitializeComponent();

            int Xres = Screen.PrimaryScreen.WorkingArea.Width;
            int Yres = Screen.PrimaryScreen.WorkingArea.Height;

            Height = 2 * Yres / 3;
            Width = Height;

#if DEBUG
            f.Show(this);
            MoveDebugWindow();
            lstDbg1 = (ListBox)f.Controls.Find("lstPoints", false)[0];
            lstDbg2 = (ListBox)f.Controls.Find("lstRPoints", false)[0];
            txtDbg = (TextBox)f.Controls.Find("txtDebug", false)[0];
#endif
            newGame();

            toolStripTextBox1.Text = iBoardSize.ToString();
        }

        private void newGame()
        {
            iMapSize=iBoardSize * iScaleCoef;
            aDots = new ArrayDots(iMapSize);
            lnks = new Links[0];
            temp_lnks = null;
            pts = null;
            count_dot1 = 0; count_dot2 = 0;

            startX = -0.5f;
            startY = -0.5f;
            nRelation = 0;
            s1 = 0; s2 = 0;
#if DEBUG
            lstDbg1.Items.Clear();
            lstDbg2.Items.Clear();
#endif
            pbxBoard.Invalidate();
        }

        public Point TranslateCoordinates(Point MousePos)
        {
            Point p = new Point();
            int top_x = (int)(startX + 0.5f), top_y = (int)(startY + 0.5f);
            p.X = top_x + (MousePos.X / (pbxBoard.ClientSize.Width / (iBoardSize + 1)));
            p.Y = top_y + (MousePos.Y / (pbxBoard.ClientSize.Height / (iBoardSize + 1)));

            return p;
        }


        private void pbxBoard_Paint(object sender, PaintEventArgs e)
        {
            DrawGame(e.Graphics);
        }

        private void DrawGame(Graphics gr)
        {
            if (антиалToolStripMenuItem.Checked)
            {
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
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
        private void DrawBoard(Graphics gr)
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
        private void DrawLinks(Graphics gr)
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
        private void DrawRegion(int DotOwn)
        {

            if (pts!=null)
            {
                // gr.DrawPolygon(PenGamer, pnt);
                //gr.FillPolygon(new SolidBrush(Color.FromArgb(130, colorGamer1)), pts, System.Drawing.Drawing2D.FillMode.Winding);

            }
        }
        private void DrawPoints(Graphics gr)
        {
            //отрисовываем поставленные точки
            if (aDots.Count > 0)
            {
                foreach (Dot p in aDots)
                {
                    switch (p.Own)
                    {
                        case 1:
                            SetColorAndDrawDots(gr,colorGamer1, p);
                            break;
                        case 2:
                            SetColorAndDrawDots(gr,colorGamer2, p);
                            break;
                    }
                }
            }
        }

        private void SetColorAndDrawDots(Graphics gr,Color colorGamer, Dot p) //Вспомогательная функция для DrawPoints. Выбор цвета точки в зависимости от ее состояния и рисование элипса
        {
            Color c;
            if (p.Blocked)
            {
                gr.FillEllipse(new SolidBrush(Color.FromArgb(130, colorGamer)), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
            }
            else
            {
                int G = colorGamer.G > 50 ? colorGamer.G - 50 : 120;
                c = p.InRegion == true ? Color.FromArgb(255, colorGamer.R, G , colorGamer.B) : colorGamer;
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
            if (dot.x == 0 | dot.y == 0 | dot.x == iMapSize-1 | dot.y == iMapSize-1)
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
                    if (d.DotsEquals(dots[i])==false & d.NeiborDots(dots[i]))
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
        private void pbxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            MousePos = TranslateCoordinates(e.Location);
            Dot dot = new Dot(MousePos);
            int res;
            if (MousePos.X > startX-0.5f & MousePos.Y > startY -0.5f)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        res = LinkPath(new Dot(MousePos.X, MousePos.Y, Dot.Owner.Player1, null));
                        s1 += res;
                        count_dot1 += 1;
                        break;
                    case MouseButtons.Right:
                        //============Ход компьютера=================
                        res = LinkPath(new Dot(MousePos.X, MousePos.Y, Dot.Owner.Player2, null));
                        s2 += res;
                        count_dot2 += 1;
                        
                        break;
                }
                txtDbg.Text = "Игрок1 окружил точек: " + 0 + "; \r\n" +
                              "Захваченая площадь: " + s1.ToString() + "; \r\n" +
                              "Игрок2 окружил точек: " + 0+ "; \r\n" +
                              "Захваченая площадь: " + s2.ToString() + "; \r\n" +
                              "Игрок1 точек поставил: " + count_dot1.ToString() + "; \r\n" +
                              "Игрок2 точек поставил: " + count_dot2.ToString() + "; \r\n";
                pbxBoard.Invalidate();

            }
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

        private int LinkPath(Dot dot)
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
            count_blocked = res;
            return res;
        }

   
        int flg_own;
        private int Hod(Dot dot)//Ход игрока. Точка ставится либо компом либо игроком
        {
            if (aDots.Contains(dot) == false) return 0;
            int counter=0;
            if (aDots[dot.x,dot.y].Own == 0)//если точка не занята
            {
                aDots.Add(dot);
                FindNeiborDots(dot);//проверяем соседние точки и устанавливаем IndexRelation
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
                    }
                }
            }
            return counter;
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
            }
            for (int i=0; i<4; i++)
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
            
           }//Маркирует точки InRegion true которые блокируют заданную точку

        private void pbxBoard_MouseMove(object sender, MouseEventArgs e)
        {
            
            Point p = TranslateCoordinates(e.Location);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    MousePos = p;
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    startX = (t.X - (e.X / (pbxBoard.ClientSize.Width / (iBoardSize + 1)))) - 0.5f;
                    startY = (t.Y - (e.Y / (pbxBoard.ClientSize.Height / (iBoardSize + 1)))) - 0.5f;
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                
                    break;
            }
           
#if DEBUG
            if (aDots.Contains(p.X, p.Y))
                lblStatus.Text = p.X + " : " + p.Y + "; IndexR -" + aDots[p.X, p.Y].IndexRelation + " InReg -" + aDots[p.X, p.Y].InRegion;
            Text = startX + " : " + startY; 
#else
                lblStatus.Text = p.X + " : " + p.Y;
#endif
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
                    t = TranslateCoordinates(e.Location);
                    break;
                case MouseButtons.Middle:
                    t = TranslateCoordinates(e.Location);
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
                MoveDebugWindow();
            #endif
        }
 
        private void Form1_Move(object sender, EventArgs e)
        {
        #if DEBUG
            MoveDebugWindow();
        #endif
        }
        #if DEBUG
            private void MoveDebugWindow()
        {
            f.Top = Top;
            f.Left = Left + Width;
        }
        #endif

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
            newGame();
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
                iBoardSize = (int)i;
                pbxBoard.Invalidate();
            }
        }
        private void цветКурсораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
                colorCursor = MyDialog.Color;
            Properties.Settings.Default.Color_Cursor = colorCursor;
            Properties.Settings.Default.Save();
            pbxBoard.Invalidate();  
        }
        private void цветИгрокаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
                colorGamer1 = MyDialog.Color;
            Properties.Settings.Default.Color_Gamer1 = colorGamer1;
            Properties.Settings.Default.Save();
            pbxBoard.Invalidate();
        }
        private void цветПротивникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
                colorGamer2 = MyDialog.Color;
            Properties.Settings.Default.Color_Gamer2 = colorGamer2;
            Properties.Settings.Default.Save();
            pbxBoard.Invalidate();
        }


    }
}
