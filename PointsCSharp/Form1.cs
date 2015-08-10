#define DEBUG
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;


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

        public int iBoardSize = 10;//- количество клеток квадрата в длинну 
        private float startX = -0.5f, startY = -0.5f;

        private ArrayDots aDots;//Основной массив, где хранятся все поставленные точки. С єтого массива рисуются все точки
        private ArrayDots aTemp ;// временный массив точек
        private ArrayDots aBlocked;
        private ArrayDots path;
        private ArrayList paths;
        Dot firstDot;//точка от которой ищется контур
        int levelRecursion;
        private Links[] lnks; //массив где хранятся связи между точками, учавствует в отрисовке
        private Links[] temp_lnks; //массив где хранятся связи между точками, если связи не окружили точку - обнуляется, если да, то добавляется в lnks

        private Color colorGamer1 = Color.FromArgb(255,255,100,0),
                           colorGamer2 = Color.FromArgb(255,100, 150, 150),
                           colorCursor = Color.DarkOrange;
        private float PointWidth = 0.25f;
        private Pen boardPen = new Pen(Color.DarkSlateBlue, 0.05f);
        private SolidBrush drawBrush = new SolidBrush(Color.MediumPurple);
        private Font drawFont = new Font("Arial", 0.22f);


        private Point MousePos;
        private Point t;
        private Graphics gr;
        private int nRelation = 0;

        //statistic
        private float s1;//площадь занятая игроком1
        private float s2;
        private int count_blocked;//счетчик количества окруженных точек

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


        public Point TranslateCoordinates(Point MousePos)
        {
            Point p = new Point();
            int top_x = (int)(startX + 0.5f), top_y = (int)(startY + 0.5f);
            p.X = top_x + (MousePos.X / (pbxBoard.ClientSize.Width / (iBoardSize + 1)));
            p.Y = top_y + (MousePos.Y / (pbxBoard.ClientSize.Height / (iBoardSize + 1)));

            return p;
        }

        private Dot HodComp(Dot lastDotGamer)
        {
            int x, y;
            Random rnd = new Random();
        r:
            x = lastDotGamer.x + (int)((10 * rnd.NextDouble()) - 6);
            if (x < 0) { x = 0; }
            else if (x > iBoardSize) { x = iBoardSize; }
            y = lastDotGamer.y + (int)((10 * rnd.NextDouble()) - 3);
            if (y < 0) { y = 0; }
            else if (y > iBoardSize) { y = iBoardSize; }

            if (aDots.Contains(x, y) >= 0) { goto r; }

            Dot dot = new Dot(x, y, Dot.Owner.Player2, null);
            aDots.Add(dot);
            return dot;
        }

        private void pbxBoard_Paint(object sender, PaintEventArgs e)
        {
            gr = e.Graphics;
            if (антиалToolStripMenuItem.Checked)
            {
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            //Устанавливаем масштаб
            SetScale(gr, pbxBoard.ClientSize.Width, pbxBoard.ClientSize.Height,
                startX, startX + iBoardSize + 1.0f, startY, iBoardSize + startY + 1.0f);
            //Рисуем доску
            DrawBoard();
            //Рисуем точки
            DrawPoints();
            //Отрисовка курсора
            gr.DrawEllipse(new Pen(colorCursor, 0.05f), MousePos.X - PointWidth, MousePos.Y - PointWidth, PointWidth * 2, PointWidth * 2);
            //Отрисовка замкнутого региона игрока1
            DrawLinks(drawFont);
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
        private void DrawLinks(Font drawFont)
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
                    gr.DrawString(lnks[i].Dot1.IndexRelation.ToString(), drawFont, new SolidBrush(Color.Teal), x, y);
#endif 
                }
            }
        }
        private void DrawRegion(int DotOwn)
        {
            if (path.Count>3)
            {
                Pen PenGamer = DotOwn == 1 ? new Pen(colorGamer1, 0.05f) : new Pen(colorGamer2, 0.05f);
                
                for (int i = 0; i < path.Count; i++ )
                {
                    if (i < path.Count - 1)
                    {
                        gr.DrawLine(PenGamer, path[i].x, path[i].y, path[i + 1].x, path[i + 1].y);
                    }
                    else
                    {
                        gr.DrawLine(PenGamer, path[i].x, path[i].y, path[0].x, path[0].y);
                    }

                }

            }


            //#if DEBUG                         
            //                    x = (lnks[i].Dot2.x - lnks[i].Dot1.x) / 2.0f + lnks[i].Dot1.x;
            //                    y = (lnks[i].Dot2.y - lnks[i].Dot1.y) / 2.0f + lnks[i].Dot1.y;
            //                    gr.DrawString(lnks[i].Dot1.IndexRelation.ToString(), drawFont, new SolidBrush(Color.Teal), x, y);
            //#endif 
        }

        private void DrawPoints()
        {
            //отрисовываем поставленные точки
            if (aDots.Count > 0)
            {
                Color c;
                foreach (Dot p in aDots)
                {
                    switch (p.Own)
                    {
                        case 1:
                            c = p.Blocked == true ? Color.FromArgb(130, colorGamer1) : colorGamer1;
                            gr.FillEllipse(new SolidBrush(c), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                            break;
                        case 2:
                            c = p.Blocked == true ? Color.FromArgb(130, colorGamer2) : colorGamer2;
                            gr.FillEllipse(new SolidBrush(c), p.x - PointWidth, p.y - PointWidth, PointWidth * 2, PointWidth * 2);
                            break;
                    }
                }
            }
        }
        private void DrawBoard()
        {
            //рисуем доску из клеток

            Pen pen = new Pen(new SolidBrush(Color.MediumSeaGreen), 0.15f);// 0
            gr.DrawLine(pen, 0, 0, 0, iBoardSize);
            gr.DrawLine(pen, 0, 0, iBoardSize, 0);

            for (float i = 0; i <= iBoardSize; i++)
            {
                SolidBrush drB = i == 0 ? new SolidBrush(Color.MediumSeaGreen) : drawBrush;
                gr.DrawString("y" + (i + startY + 0.5f).ToString(), drawFont, drB, startX, i + startY + 0.5f - 0.2f);
                gr.DrawString("x" + (i + startX + 0.5f).ToString(), drawFont, drB, i + startX + 0.5f - 0.2f, startY);
                gr.DrawLine(boardPen, i + startX + 0.5f, startY + 0.5f, i + startX + 0.5f, iBoardSize + startY + 0.5f);
                gr.DrawLine(boardPen, startX + 0.5f, i + startY + 0.5f, iBoardSize + startX + 0.5f, i + startY + 0.5f);
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
        private Dot GetDot(int x, int y)//получаем точку по координатам. Если в массиве ее нет, вернется пустая точка
        {
            int i = aDots.Contains(x, y);
            return i >= 0 ? aDots[i] : new Dot(x, y);
        }
        private Dot GetDot(Point Point)//получаем точку из массива. Если в массиве ее нет, вернется пустая точка
        {
            int i = aDots.Contains(Point.X, Point.Y);
            return i >= 0 ? aDots[i] : new Dot(Point.X, Point.Y);
        }
        private void Relate(Dot new_dot, int x, int y)//устанавливает связь между соседними точками
        {
            int i = aDots.Contains(x, y);
            if (i >= 0)
            {
                if (aDots[i].Own == new_dot.Own)
                {
                    Dot neibor_dot = aDots[i];
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
        private int FindAllCycles(Dot lastDot)
        {
            aDots.UnmarkAllDots();
            levelRecursion = 0;
            lastDot.FirstDot = true;
            firstDot = lastDot;
            if (FindRegion(lastDot))
            {
                Array.Resize(ref temp_lnks, max_path.Count);
                temp_lnks = max_path.Count > 3 ? LinkDots(max_path) : null;
            }
            lastDot.FirstDot = false;
            Array.Resize(ref temp_lnks, max_path.Count);
            temp_lnks = max_path.Count > 3 ? LinkDots(max_path) : null;
            return max_path.Count;
        }
        private bool FindRegion(Dot dot, bool r)//Ищет замкнутый цикл точек
        {
            if (dot.CountRelations < 2 | dot.Marked==true | dot.Blocked == true)
            {
                return false;
            }
            firstDot = levelRecursion == 0 ? dot : firstDot;
            //dot.IndexDot = levelRecursion;
            levelRecursion += 1;
            dot.Marked = true;
            if (dot.NeiborDots(firstDot) & levelRecursion>3)
            {
                path.Add(dot);
                return true;
            }
            for (int i = 0; i < dot.CountRelations; i++)
            {
                if (dot[i].Marked == false)
                {
                    dot[i].Parent = dot;
                    if (FindRegion(dot[i]))
                    {
                        dot.InRegion = true;
                        dot[i].InRegion = true;
                        path.Add(dot);
                        levelRecursion -= 1;
                        return true;
                    }
                }
            }
            levelRecursion -= 1;
            return false;
        }
        private ArrayDots max_path = new ArrayDots();
        private bool FindRegion(Dot dot)//Ищет замкнутый цикл точек
        {
            if (path.Count == 0)
            {
                dot.Parent = null;
            }
            path.Add(dot);
            if (dot.CountRelations < 2 | dot.Marked == true | dot.Blocked == true)
            {
                path.Remove(dot);
                return false;
            }
            dot.Marked = true;
            for (int i = 0; i < dot.CountRelations; i++)
            {
                if (dot[i].FirstDot == true & path.Count > 3)
                {
                    dot[i].InRegion = true;
                   return true;
                }
                if (dot[i].Marked == false)
                {
                    dot[i].Parent = dot;
                    if (FindRegion(dot[i]))
                    {
                        dot[i].InRegion = true;
                        if (path.Count > max_path.Count)
                        {
                            max_path = path;
                        }

                        if (dot.Parent == null)
                        {
                            path = new ArrayDots();
                            path.Add(firstDot);
                            aDots.UnmarkAllDots();
                        }
                    }
                    else
                    {
                        path.Remove(dot);
                    }
                }
            }
            if(max_path.Count < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool DotIsFree(Dot dot)//проверяет заблокирована ли точка
        {
            dot.Marked = true;
            if (dot.x==0 | dot.y==0 | dot.x == iBoardSize | dot.y == iBoardSize)
            {
                return true;
            }
            Dot[] d = new Dot[4] { GetDot(dot.x + 1, dot.y), GetDot(dot.x - 1, dot.y), GetDot(dot.x, dot.y + 1), GetDot(dot.x, dot.y - 1) };
            for (int i = 0; i < 4; i++)
            {
                if (d[i].Marked == false) //& d[i].Blocked==false)
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
            for (int i = 0; i < 4; i++)
            {
                if (d[i].Own != 0 & d[i].Own != flg_own & d[i].Blocked==false)
                {
                    d[i].InRegion = true;
                    path.Add(d[i]);
                }
            }
            return false;
        }
        private int ScanRegion(int IndexRelation)//сканирует замкнутый регион по индексу связи, возвращает количество окруженных точек противника, пустые не считает
        {
            ArrayDots arr_dots= new ArrayDots();
            Dot.Owner Owner; 
            foreach (Dot d in aDots)
            {
                if((d.IndexRelation == IndexRelation) & (d.InRegion==true))
                    {
                        arr_dots.Add(d);
                    }
            }
            
            if (arr_dots.Count==0)
            {
                return 0;
            }
            else
            {
                Owner = (Dot.Owner) arr_dots[0].Own;
            }
            Dot[] arr_d = new Dot[arr_dots.Count];
            arr_dots.Sort(ref arr_d);
            int min_x = arr_d[0].x, max_x = arr_d[arr_d.Length - 1].x;
            int count_bl=0;
            for (int i = min_x+1; i < max_x; i++)
            {
                int min_y, max_y;
                ArrayDots y_line = new ArrayDots();
                for (int j = 0; j < arr_d.Length; j++)
                {
                    if (arr_d[j].x==i)
                    {
                        y_line.Add(new Dot(arr_d[j].x, arr_d[j].y));                   
                    }
                }
                min_y = y_line[0].y;
                max_y = y_line[y_line.Count-1].y;
                if (min_y== max_y)
                {
                    break;
                }
                for (int k = min_y+1; k < max_y; k++)
                {
                    Dot bd = GetDot(i, k);
                    if (bd.Own != (int)Owner)
                    {
                        bd.Blocked = true;
                        count_bl += bd.Own != (int)Dot.Owner.None ? 1 : 0;
                        if (aBlocked.Contains(bd)==-1)
                        {
                            aBlocked.Add(bd);
                        }
                    }
                    
                }
            }
            return count_bl;
        }
        private int ScanRegion(int IndexRelation, bool c)//сканирует замкнутый регион по индексу связи, возвращает количество окруженных точек противника, пустые не считает
        {
            ArrayDots arr_dots = new ArrayDots();
            Dot.Owner Owner;

            foreach (Dot d in aDots)
            {
                if ((d.IndexRelation == IndexRelation) & (d.InRegion == true))
                {
                    arr_dots.Add(d);
                }
            }

            if (arr_dots.Count == 0)
            {
                return 0;
            }
            else
            {
                Owner = (Dot.Owner)arr_dots[0].Own;
            }
            Dot[] arr_d = new Dot[arr_dots.Count];
            arr_dots.Sort(ref arr_d);
            int min_x = arr_d[0].x, max_x = arr_d[arr_d.Length - 1].x;
            int count_bl = 0;
            for (int i = min_x + 1; i < max_x; i++)
            {
                int min_y, max_y;
                ArrayDots y_line = new ArrayDots();
                for (int j = 0; j < arr_d.Length; j++)
                {
                    if (arr_d[j].x == i)
                    {
                        y_line.Add(new Dot(arr_d[j].x, arr_d[j].y));
                    }
                }
                min_y = y_line[0].y;
                max_y = y_line[y_line.Count - 1].y;
                if (min_y == max_y)
                {
                    break;
                }
                for (int k = min_y + 1; k < max_y; k++)
                {
                    Dot bd = GetDot(i, k);
                    if (bd.Own != (int)Owner)
                    {
                        bd.Blocked = true;
                        count_bl += bd.Own != (int)Dot.Owner.None ? 1 : 0;
                        if (aBlocked.Contains(bd) == -1)
                        {
                            aBlocked.Add(bd);
                        }
                    }

                }
            }
            return count_bl;
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
        private Links[] LinkDots(ArrayDots dots)//устанавливает связь между двумя точками и возвращает массив связей 
        {
            Links[] arr_l = new Links[0];
            Links l;
            int j = 0;
            foreach (Dot d in dots)
            {
                for (int i = 0; i < dots.Count; i++)
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
                        dot = new Dot(MousePos.X, MousePos.Y, Dot.Owner.Player1, null);
                        res = LinkPath(dot);
                        s1 += res;
                        break;
                    case MouseButtons.Right:
                        //============Ход компьютера=================
                        dot = new Dot(MousePos.X, MousePos.Y, Dot.Owner.Player2, null);
                        res = LinkPath(dot);
                        s2 += res;
                        break;
                }
                txtDbg.Text = "Игрок1 окружил точек: " + GetCountBlockedDots(Dot.Owner.Player1, false) + "; \r\n" +
                              "Захваченая площадь: " + s1.ToString() + "; \r\n" +
                              "Игрок2 окружил точек: " + GetCountBlockedDots(Dot.Owner.Player2, false) + "; \r\n" +
                              "Захваченая площадь: " + s2.ToString() + "; \r\n";
                pbxBoard.Invalidate();

            }
#if DEBUG
            lstDbg1.Items.Clear();
                foreach (Dot p in aBlocked)
                {
                    lstDbg1.Items.Add(p.x.ToString() +  p.y.ToString() + "; IR-" + p.IndexRelation + 
                        "; InR-" + p.InRegion + "; BLOCKED!");
                }
            lstDbg2.Items.Clear();
            foreach (Dot p in path)
            {
                lstDbg2.Items.Add(p.x.ToString() + p.y.ToString() + "; IR-" + p.IndexRelation +
                    "; InR-" + p.InRegion);
            }

#endif
        }

        private int LinkPath(Dot dot)
        {
            int res = Hod(dot);
            
            if (count_blocked - res != 0)
            {
                temp_lnks = LinkDots(path);
                Array.Resize(ref lnks, temp_lnks.Length + lnks.Length);
                temp_lnks.CopyTo(lnks, lnks.Length - temp_lnks.Length);
                temp_lnks = null;
            }
            count_blocked = res;
            return res;
        }

        private float Hod(Dot dot, bool c)//Ход игрока. Точка ставится либо компом либо игроком
        {
            if (aDots.Contains(dot) == -1)
            {
                aDots.Add(dot);
                FindNeiborDots(dot);
                if (FindAllCycles(dot) > 0)
                {
                    int cb = GetCountBlockedDots((Dot.Owner)dot.Own, false);
                    int dr = ScanRegion(dot.IndexRelation);
                    for (int i = 0; i < aBlocked.Count; i++)
                    {
                        aDots.UnmarkAllDots();
                        if(DotIsFree(aBlocked[i])==false)
                        {
                            MessageBox.Show("Dot blocked!"); 
                        }
                    }
                    
                    count_blocked = GetCountBlockedDots((Dot.Owner)dot.Own, false);
                    if (count_blocked - cb != 0)
                    {
                        Array.Resize(ref lnks, temp_lnks.Length + lnks.Length);
                        temp_lnks.CopyTo(lnks, lnks.Length - temp_lnks.Length);
#if DEBUG
                        lstDbg2.Items.Clear();
                        foreach (Links l in lnks)
                        {
                            lstDbg2.Items.Add(l.Dot1.x + ":" + l.Dot1.y + " - " + l.Dot2.x + ":" + l.Dot2.y + "; IR-" + l.Dot1.IndexRelation +
                            "; InR-" + l.Dot1.InRegion);
                        }
#endif
                        return SquarePolygon(aDots.CountDotsInRegion(dot.Own), dr);

                    }
                    else
                    {
                        temp_lnks = null; //обнуляет связи которые не окружили ни одной точки
                    }

                }
            }
            return 0;
        }
        int flg_own;
        private int Hod(Dot dot)//Ход игрока. Точка ставится либо компом либо игроком
        {
            aDots.Add(dot);
            FindNeiborDots(dot);
            for (int i = 0; i < aDots.Count; i++)
            {
               
                flg_own = aDots[i].Own;
                if (flg_own > 0)
                {
                    aDots.UnmarkAllDots();
                    if (DotIsFree(aDots[i]) == false)
                    {
                        aDots[i].Blocked = true;
                        aBlocked.Add(aDots[i]);
                    }
                    else
                    {
                        int res = aBlocked.Contains(aDots[i]);
                        if (res >= 0)
                        {
                            aBlocked.Remove(res);
                        }
                    }
                }
            }

            return aBlocked.Count;
        }

        private int GetCountBlockedDots(Dot.Owner Owner, bool allDots)/*возвращает количество окруженных точек, 
            если задать allDots-то в том числе будут и не занятые точки*/

        {
            int counter = 0;
            switch (allDots)
            {
                case true:
                    for (int i = 0; i < aBlocked.Count; i++)
                    {
                        counter += aBlocked[i].Own != (int)Owner ? 1 : 0;
                    }
                    break;
                case false:
                    for (int i = 0; i < aBlocked.Count; i++)
                    {
                        counter += aBlocked[i].Own != (int)Owner & aBlocked[i].Own != (int)Dot.Owner.None ? 1 : 0;
                    }
                    break;
            }
            return counter;
        }


        private int SumLinks(Links[] l)
        {
            if (l== null) { return 0; }
            int counter=0;
            foreach (Links itemLink in l)
            {
                counter += (int)itemLink.CostLink;
            }
            return counter;
        }
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
            lblStatus.Text = p.X + " : " + p.Y + "; IR " + GetDot(p).IndexRelation;
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
            numericUpDown1.Left = pbxBoard.Width/2;
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
        private void newGame() 
        {
            aDots = new ArrayDots(iBoardSize);  
            aTemp = new ArrayDots();
            aBlocked = new ArrayDots();
            path = new ArrayDots();  
            lnks = new Links[0];
            temp_lnks = null;
            max_path = new ArrayDots(); 
            paths = new ArrayList(); 


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
            pbxBoard.Invalidate();  
        }
    }
}
