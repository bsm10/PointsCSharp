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
    public class Dot
    {
        public int x, y;
        private int _Own;
        private bool _Blocked = false;
        private int _IndexRel;
        private int _IndexDot;
        private bool _Fixed;
        public bool Blocked
        {
            get { return _Blocked; }
            set
            {
                if (Fixed==false) 
                {
                    _Blocked = value;
                    if(_Blocked)
                    {
                        IndexRelation=0;
                        InRegion=false;
                    }
                    else
                    {
                        _WhoBlocking.Clear();
                        //if(_WhoBlocking.Count>0) 
                        //{
                        //    foreach (Dot d in _WhoBlocking)
                        //    {
                        //        d.BlokingDots.Remove(d);
                        //    }
                        //}
                    }
                }
            }

        }
        private List<Dot> _BlokingDots;
        public List<Dot> BlokingDots//Список точек, которые блокируются этой точкой
        {
            get { return _BlokingDots; }
        }
        public bool Fixed
        {
            get { return _Fixed; }
            set { 
                _Fixed = value;
                if (RelatedDots != null)
                {
                    for (int i = 0; i < RelatedDots.Length; i++)
                    {
                        if (_Fixed == true)
                        {
                            RelatedDots[i].Fixed = _Fixed;
                        }
                    }
                }
            }
        }
        public int Own
        {
            get { return _Own; }
            set
            {
                _Own = value;
                if (_Own==0)
                {
                    IndexRelation = 0;
                    InRegion = false;
                    Blocked = false;
                }
            }

        }
        private List<Dot> _WhoBlocking;
        public List<Dot> WhoBlocking//Список точек, которые блокируют точку
        {
            get { return _WhoBlocking; }
        }

        public int IndexRelation
        {
            get {return _IndexRel;}
            set {
                _IndexRel = value;
                if (RelatedDots != null)
                {
                    for (int i = 0; i < RelatedDots.Length; i++)
                    {
                        if (RelatedDots[i].IndexRelation != _IndexRel & RelatedDots[i].Blocked == false & _IndexRel!=0)
                        {
                            RelatedDots[i].IndexRelation = _IndexRel;
                        }
                    }
                }
            }
        }
        private Dot _ParentDot;
        public Dot Parent
        {
            get { return _ParentDot; }
            set
            {
                _ParentDot = value;
            }

        }//родительский узел
        private bool _Marked;
        public bool Marked
        {
            get
            {
                return _Marked;
            }
            set
            {
                _Marked=value;
            }
        }
        private bool _FirstDot;
        public bool FirstDot
        {
            get
            {
                return _FirstDot;
            }
            set
            {
                _FirstDot = value;
            }
        }
        private bool _InRegion;
        public bool InRegion
        {
            get
            {
                return _InRegion;
            }
            set
            {
                _InRegion = value;
            }
        }
        public Dot[] RelatedDots;
        public int CountRelations
        {
            get
            {
                if (RelatedDots==null)
                {
                    return 0;
                }
                else
                {
                    return RelatedDots.Length;
                }
                
            }
        }
        public int IndexDot
        {
            get
            {
                return _IndexDot;
            }
            set
            {
                _IndexDot = value;
            }
        }
        private int r=0;
        public enum Owner : int {None = 0,Player1 = 1,Player2 = 2}
        public Dot this[int i]//Индексатор возвращает элемент из массива по его индексу(в данном случае - связанные точки)
        {
            get
            {
                return RelatedDots[i];
            }
            set
            {
                RelatedDots[i] = value;
            }
        }
        public Dot (int x,int y, Owner OwnerDot,Dot ParentDot)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            _WhoBlocking = new List<Dot>();
            Own = (int)OwnerDot;
            IndexRelation = 0;
            _ParentDot=ParentDot;
        }
        public Dot(int x, int y, int IndexOwner, Dot ParentDot)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            _WhoBlocking = new List<Dot>();
            Own = IndexOwner;
            IndexRelation = 0;
            _ParentDot = ParentDot;
        }
        public Dot(int x, int y)
        {
            this.x = x;
            this.y = y;
            _BlokingDots = new List<Dot>();
            _WhoBlocking = new List<Dot>();
            Own = (int)Owner.None;
            IndexRelation = 0;
            _ParentDot = null;
        }
        public Dot(Point Point)
        {
            x = Point.X;
            y = Point.Y;
            _BlokingDots = new List<Dot>();
            _WhoBlocking = new List<Dot>();
            Own = (int)Owner.None;
            IndexRelation = 0;
            _ParentDot = null;
        }
        public void AddRelationDot(Dot RelationDot)
        {
        if(RelationDot.Blocked==false)
            {  
            Array.Resize(ref RelatedDots, r+1);
            RelatedDots[r] = RelationDot;
            r+=1;
            }
        }

        public bool DotsEquals(Dot dot)//Проверяет равенство точек по координатам
        {
            return (x == dot.x) & (y == dot.y);
        }
        public bool NeiborDots(Dot dot)//возвращает истину если соседние точки рядом. 
        {
            if (dot.Blocked | dot.Blocked | dot.Own != Own)
            {
                return false;
            }
            return Math.Abs(x - dot.x) <= 1 & Math.Abs(y - dot.y) <= 1;
        }
    }
}
