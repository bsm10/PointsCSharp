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
