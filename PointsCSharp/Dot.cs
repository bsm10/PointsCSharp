using System;
using System.Drawing;
using System.Collections.Generic;
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
        public bool Blocked { get; set; }
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
                return _NeiborDots;//Список точек, которые блокируются этой точкой
            }
        }
        public bool Fixed { get; set; }
        public bool Selected { get; set; }
        public int Own
        {
            get { return _Own; }
            set
            {
                _Own = value;
                if (_Own==0)
                {
                    Blocked = false;
                }
            }

        }
        //public enum _Rating { None = 0, Crit = 1, Bad = 2, Mid = 3, Good = 4, Nice = 5 }
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
        public int IndexDot { get; set; }
        public enum Owner : int {None = 0,Player1 = 1,Player2 = 2}
        public Dot (int x,int y, Owner OwnerDot,Dot ParentDot)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            Own = (int)OwnerDot;
        }
        public Dot(int x, int y, int Owner, Dot ParentDot)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            Own = Owner;
        }
        public Dot(int x, int y)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            Own = (int)Owner.None;
        }
        public Dot(Point Point)
        {
            x = Point.X;
            y = Point.Y;
            _BlokingDots = new List<Dot>();
            Own = (int)Owner.None;
        }
        public bool PatternsFirstDot {get; set;}
        public bool PatternsMoveDot { get; set; }
        public bool PatternsAnyDot { get; set; }
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
    }
}
