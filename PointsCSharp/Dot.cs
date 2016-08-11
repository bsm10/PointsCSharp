using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace DotsGame
{
    public class Links
    {
        public Dot Dot1;
        public Dot Dot2;
        private float cost;
        public override string ToString()
        {
            string s;
            if (Dot1.Own == 1) s = " Player";
            else s = " Computer";
            return Dot1.x + ":" + Dot1.y + "-" + Dot2.x + ":" + Dot2.y + s;
        }
        public bool Blocked{get;set;}

        //{
        //    get
        //    {
        //        return Dot1.Blocked & Dot2.Blocked;    
        //    }
        //}
        public float CostLink
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        public Links(Dot Dot1, Dot Dot2)
        {
            this.Dot1 = Dot1;
            this.Dot2 = Dot2;
            if (Math.Abs(Dot1.x - Dot2.x) + Math.Abs(Dot1.y - Dot2.y) < 2)
            {
                cost = 1;
            }
            else
            {
                cost = 2;
            }
        }
        public Links(int x1, int y1, int x2, int y2)
        {
            Dot1 = new Dot(x1, y1);
            Dot2 = new Dot(x2, y2);
            if (Math.Abs(Dot1.x - Dot2.x) + Math.Abs(Dot1.y - Dot2.y) < 2)
            {
                cost = 1;
            }
            else
            {
                cost = 0.5f;
            }
        }
        public int LinkExist(Links[] arr_lnks)
        {
            if (arr_lnks != null)
            {
                for (int i = 0; i < arr_lnks.Length; i++)
                {
                    if(arr_lnks[i]!=null)
                    {
                        if ((Dot1.x == arr_lnks[i].Dot1.x) & (Dot1.y == arr_lnks[i].Dot1.y) &
                           ((Dot2.x == arr_lnks[i].Dot2.x) & (Dot2.y == arr_lnks[i].Dot2.y)) |
                            ((Dot2.x == arr_lnks[i].Dot1.x) & (Dot2.y == arr_lnks[i].Dot1.y) &
                           ((Dot1.x == arr_lnks[i].Dot2.x) & (Dot1.y == arr_lnks[i].Dot2.y))))
                        {
                            return i;
                        }

                    }
                }

            }
            return -1;
        }
    }
    public class Dot: IEquatable<Dot>
    {
        public int x, y;
        private int _Own;
        private bool _Blocked; 

        public bool Blocked 
        { 
            get {return _Blocked;}
            set
            {
                _Blocked=value;
                if (_Blocked)
                {
                    IndexRelation = 0;
                    if (NeiborDots.Count > 0)
                    {
                        foreach (Dot d in NeiborDots)
                        {
                            if (d.Blocked == false) d.IndexRelation = d.IndexDot;
                        }
                    }
                }
            }
        }
        private List<Dot> _BlokingDots;
        public List<Dot> BlokingDots 
            {
                get
                { 
                    return _BlokingDots;//Список точек, которые блокируются этой точкой
                }
            }
        private List<Dot> _NeiborDots = new List<Dot>();
        public List<Dot> NeiborDots
        {
            get
            {
                return _NeiborDots;//Список соседних точек
            }
        }
        public bool Fixed { get; set; }
        public int CountBlockedDots { get; set; }
        public bool Selected { get; set; }
        public int Own
        {
            get { return _Own; }
            set
            {
                _Own = value;
                //if (_Own==0)
                //{
                //    Blocked = false;
                //}
            }

        }
        private int rating;
        public int Rating 
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                //foreach(Dot d in NeiborDots)
                //{
                //    //if(Math.Sqrt(Math.Pow(Math.Abs(d.x - x),2) + Math.Pow(Math.Abs(d.y - y),2))==1)
                //    //{
                //        if (rating < d.rating) d.Rating = rating;
                //        else rating = d.Rating;
                //    //}
                    
                //}
            }
        }
        public bool Marked { get; set; }

        public int _IndexDot;
        public int IndexDot
        {
            get
            {
                return _IndexDot;
            }
            set
            {
                _IndexDot = value;
                _IndexRel = _IndexDot;
            }
        }
        public bool BonusDot { get; set; }
        public Dot DotCopy
        {
            get
            {
                return (Dot)this.MemberwiseClone();
                //Dot d = new Dot(x,y,Own);
                //d.Blocked=Blocked;
                //return d;
            }
        }
        public int iNumberPattern { get; set; }

        public Dot(int x, int y, int Owner = 0)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            Own = Owner;
            //IndexRelation = IndexDot;
        }
        public void UnmarkDot()
        {
            this.Marked = false;
            this.PatternsFirstDot = false;
            this.PatternsMoveDot = false;
            this.PatternsAnyDot = false;
            this.PatternsEmptyDot = false;
        }


        public bool PatternsFirstDot {get; set;}
        public bool PatternsMoveDot { get; set; }
        public bool PatternsAnyDot { get; set; }
        public bool PatternsEmptyDot { get; set; }
        public override string ToString()
            {
            string s;
            if (Own == 1) s = " Player";
            else if (Own == 2) s = " Computer";
            else s = " None";
                return x + ":" + y + s;
            }
        public bool Equals(Dot dot)//Проверяет равенство точек по координатам
        {
            return (x == dot.x) & (y == dot.y);
        }
        public bool IsNeiborDots(Dot dot)//возвращает истину если соседние точки рядом. 
        {
            if (dot.Blocked | dot.Blocked | dot.Own != Own)
            {
                return false;
            }
            return Math.Abs(x - dot.x) <= 1 & Math.Abs(y - dot.y) <= 1;

        }
        private int _IndexRel;

        public int IndexRelation
        {
            get { return _IndexRel; }
            set
            {
                _IndexRel = value;
                if (NeiborDots.Count > 0)
                {
                    foreach (Dot d in NeiborDots)
                    {
                        if (d.Blocked == false)
                        {
                            if (d.IndexRelation != _IndexRel & _IndexRel != 0)
                            {
                                d.IndexRelation = _IndexRel;
                            }
                        }
                    }

                }
            }

        }

    }
}
