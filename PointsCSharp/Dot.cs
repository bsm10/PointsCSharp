using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace DotsGame
{
    public class Links //: IEqualityComparer<Links>
    {
        public Dot Dot1;
        public Dot Dot2;
        private float cost;
        public override string ToString()
        {
            string s = string.Empty;
            if (Dot1.Own == 1 & Dot2.Own == 1) s = " Player";
            if (Dot1.Own == 2 & Dot2.Own == 2) s = " Computer";
            if (Dot1.Own == 0 | Dot2.Own == 0) s = " None";

            return Dot1.x + ":" + Dot1.y + "-" + Dot2.x + ":" + Dot2.y + s + " Cost - " + CostLink.ToString() + " Fixed " + Fixed.ToString();
        }
        public override int GetHashCode()
        {
            //Check whether the object is null
            if (object.ReferenceEquals(this, null)) return 0;

            //Get hash code for the Dot1
            int hashLinkDot1 = Dot1.GetHashCode();

            //Get hash code for the Dot2
            int hashLinkDot2 = Dot2.GetHashCode();

            //Calculate the hash code for the Links
            return hashLinkDot1 * hashLinkDot2;
        }

        public bool Blocked
        {
            get
            {
                return (Dot1.Blocked & Dot2.Blocked);
            }
        }

        public bool Fixed
        {
            get
            {
                return (Dot1.Fixed | Dot2.Fixed);
            }
        }


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
        public int LinksDistance (Links link)
        {
            decimal d1, d2;
            d1 = Math.Round((decimal)(Dot1.x + link.Dot1.x + Dot2.x + link.Dot2.x) / 4);
            d2 = Math.Round((decimal)(Dot1.y + link.Dot1.y + Dot2.y + link.Dot2.y) / 4);
            return (int)Math.Round((d1+d2)/4);
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

        public bool Equals(Links otherLink)//Проверяет равенство связей по точкам
        {
            return GetHashCode().Equals(otherLink.GetHashCode());
        }

    }
    class LinksComparer : IEqualityComparer<Links>
    {
        public bool Equals(Links link1, Links link2)
        {
            
            return link1.Equals(link2);
        }
        
        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Links links)
        {
            //Check whether the object is null
            if (object.ReferenceEquals(links, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashLinkDot1 = links.Dot1.GetHashCode();

            //Get hash code for the Code field.
            int hashLinkDot2 = links.Dot2.GetHashCode();

            //Calculate the hash code for the product.
            return hashLinkDot1 * hashLinkDot2;
        }

    }
    public class ComparerDots : IComparer<Dot>
    {
        public int Compare(Dot d1, Dot d2)
        {
            if (d1.x.CompareTo(d2.x) != 0)
            {
                return d1.x.CompareTo(d2.x);
            }
            else if (d1.y.CompareTo(d2.y) != 0)
            {
                return d1.y.CompareTo(d2.y);
            }
            else
            {
                return 0;
            }
        }
    }
    public class ComparerDotsByOwn : IComparer<Dot>
    {
        public int Compare(Dot d1, Dot d2)
        {
            if (d1.x.CompareTo(d2.Own) != 0)
            {
                return d1.Own.CompareTo(d2.Own);
            }
            else if (d1.Own.CompareTo(d2.Own) != 0)
            {
                return d1.Own.CompareTo(d2.Own);
            }
            else
            {
                return 0;
            }
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
                //    //if(Math.Sqrt(Math.Pow(Math.Abs(d.x -x),2) + Math.Pow(Math.Abs(d.y -y),2))==1)
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
        /// <summary>
        /// Удаляем метки паттернов
        /// </summary>
        public void PatternsRemove()
        {
            PatternsFirstDot = false;
            PatternsMoveDot  = false;
            PatternsAnyDot   = false;
            PatternsEmptyDot = false;
        }
        public bool PatternsFirstDot {get; set;}
        public bool PatternsMoveDot { get; set; }
        public bool PatternsAnyDot { get; set; }
        public bool PatternsEmptyDot { get; set; }

        //public PE_XYminmax PE_XY = new PE_XYminmax();


        //public class PE_XYminmax
        //{
        //    public int minX { get; set; }
        //    public int maxX { get; set; }
        //    public int minY { get; set; }
        //    public int maxY { get; set; }
        //}

        public override string ToString()
            {
            string s;
            if (Own == 1) s = " Player";
            else if (Own == 2) s = " Computer";
            else s = " None";
            
            s = Blocked ? x + ":" + y + s + " Blocked" : x + ":" + y + s;
                return s;
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
            return Math.Abs(x -dot.x) <= 1 & Math.Abs(y -dot.y) <= 1;

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
        
        public bool ValidMove
        {
            get { return Blocked == false && Own == 0; }
        }
    }
}
