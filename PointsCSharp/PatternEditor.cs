using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DotsGame
{
    public class Pattern
    {
        public int PatternNumber { get; set; }
        List<DotInPattern> _DotsPattern = new List<DotInPattern>();

        public int Xmin { get; set; }
        public int Xmax { get; set; }
        public int Ymin { get; set; }
        public int Ymax { get; set; }

        public List<DotInPattern> DotsPattern
        {
            get
            {
                return _DotsPattern;
            }
            set
            {
                _DotsPattern = value;
            }

        }
        public DotInPattern dXdY_ResultDot = new DotInPattern();
        public Dot Dot { get; set; }
        public Dot ResultDot
        {
            get
            {
                return new Dot(Dot.x + dXdY_ResultDot.dX, Dot.y + dXdY_ResultDot.dY, Dot.Own);
            }
        }

        public override string ToString()
        {
            return "Pattern " + PatternNumber.ToString();
        }

    }

    public class DotInPattern
    {
        public int dX { get; set; }
        public int dY { get; set; }
        public string Owner { get; set; }
        public override string ToString()
        {
            return "dX = " + dX.ToString() + "; dY = " + dY.ToString();
        }

    }

    public partial class Form1
    {
        #region Pattern Editor
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
            get { 
                    return tlsRedDot.Checked ? 1 : 2; 
                }

            //set { tlsТочкаХода.Checked = value; }

        }

        //public bool PE_On
        //{
        //    get
        //    {
        //        if (f.tlsEditPattern.Checked & lstPat == null) lstPat = new List<Dot>();
        //        return f.tlsEditPattern.Checked;

        //    }
        //    set { f.tlsEditPattern.Checked = value; }

        //}
        public void WritePatternToFile(List<string> lines)
        {
            // Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(game.Path_PatternData, true))
            {

                foreach (string s in lines) file.WriteLine(s);
            }
        }

        private int GetNumberPattern()
        {
            int number = 0;
            string line;
            // Read the file and display it line by line.
            StreamReader file = new StreamReader(game.Path_PatternData);
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
            if(game.lstDotsInPattern.Count==0) return;
            List<Dot> lstPat = game.lstDotsInPattern;
            //rotate dots in pattern
            //List<List<Dot>> rotatePattern = new List<List<Dot>>();

            foreach (List<Dot> listDots in ListRotatePatterns(lstPat)) AddPatternDots(listDots);

            lstPat.Clear();
            //tlsEditPattern.Checked = false;
            game.aDots.UnmarkAllDots();
            game.LoadPattern();
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
            listPat = game.aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = game.aDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = game.aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = game.aDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = game.aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);
            listPat = game.aDots.Rotate_Mirror_Horizontal(listPat);
            lstlstPat.Add(listPat);
            listPat = game.aDots.Rotate90(listPat);
            lstlstPat.Add(listPat);

            return lstlstPat;
        }

        #endregion

    }


    public partial class Game
    {
        private bool _EditMode;
        public bool EditMode 
        {
            get
            {
                return _EditMode;
            }

            set
            {
                _EditMode=value;
                if (_EditMode) lstDotsInPattern = new List<Dot>();
            }
        }
        //Редактор паттернов
        public List<Pattern> Patterns = new List<Pattern>();
        public List<Dot> lstDotsInPattern;
        //--------------------------------------------------------
    }
}
