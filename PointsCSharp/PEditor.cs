using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DotsGame
{
    public class Pattern
    {
        public int PatternNumber { get; set; }
        List<DotInPattern> _DotsPattern = new List<DotInPattern>();
        //public int minX { get; set; }
        //public int maxX { get; set; }
        //public int minY { get; set; }
        //public int maxY { get; set; }

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

    //public static class PEminXY
    //{
    //    public static int minX = 0;
    //    public static int maxX = 0;
    //    public static int minY = 0;
    //    public static int maxY = 0;
    //}
    public partial class GameEngine
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
                _EditMode = value;
                if (_EditMode) lstDotsInPattern = new List<Dot>();
            }
        }
        //Редактор паттернов
        public static List<Pattern> Patterns = new List<Pattern>();
        public List<Dot> lstDotsInPattern;

        public string Path_PatternData
        {
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
            _gameDots.UnmarkAllDots();
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
            lines.Add("Dots");
            for (int i = 0; i < ListPatternDots.Count; i++)
            {
                string own = "";
                //if (firstDot.Own == ListPatternDots[i].Own) own = "owner";
                //if (firstDot.Own != ListPatternDots[i].Own) own = "enemy";
                if (ListPatternDots[i].Own == 1) own = "enemy";
                if (ListPatternDots[i].Own == 2) own = "owner";
                if (ListPatternDots[i].Own == 0 & ListPatternDots[i].PatternsAnyDot == false) own = "0";
                if (ListPatternDots[i].PatternsAnyDot) own = "!= enemy";
                dx = ListPatternDots[i].x - firstDot.x;
                dy = ListPatternDots[i].y - firstDot.y;
                s = dx.ToString() + ", " + dy.ToString() + ", " + own;
                lines.Add(s);
            }
            lines.Add("Result");
            lines.Add((moveDot.x - firstDot.x).ToString() + ", " +
                      (moveDot.y - firstDot.y).ToString());
            lines.Add("End");

            WritePatternToFile(lines);
            s = string.Empty;
            foreach (string st in lines) s = s + st + " \r\n";
            //DebugWindow.txtboxPattern.Text = s;

        }//private void AddPatternDots(List<Dot> ListPatternDots)

    }//---public partial class GameEngine--

}//--namespace DotsGame---
