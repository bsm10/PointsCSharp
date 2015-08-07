using System;
using System.Drawing;

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

    }
    public class Dot
    {
        public int x, y, Own;
        public bool Blocked = false;
        private int IndexRel;
        private int _IndexDot;
        public int IndexRelation
        {
            get {return IndexRel;}
            set {
                IndexRel = value;
                    if (RelatedDots != null)
                    {
                        for (int i = 0; i < RelatedDots.Length; i++)
                        {
                            if (RelatedDots[i].IndexRelation != IndexRel)
                                {
                                    RelatedDots[i].IndexRelation = IndexRel;
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
        private bool _NuberOfPath;
        public bool NuberOfPath
        {
            get
            {
                return _NuberOfPath;
            }
            set
            {
                _NuberOfPath = value;
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
                _IndexDot=value; 
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
            Own = (int)OwnerDot;
            IndexRelation = 0;
            _ParentDot=ParentDot;
            _IndexDot+=1;
        }
        public Dot(int x, int y, int IndexOwner, Dot ParentDot)
        {
            this.x = x;
            this.y = y;
            Own = IndexOwner;
            IndexRelation = 0;
            _ParentDot = ParentDot;
            _IndexDot += 1;

        }
        public Dot(int x, int y)
        {
            this.x = x;
            this.y = y;
            Own = (int)Owner.None;
            IndexRelation = 0;
            _ParentDot = null;
            _IndexDot += 1;
        }
        public Dot(Point Point)
        {
            x = Point.X;
            y = Point.Y;
            Own = (int)Owner.None;
            IndexRelation = 0;
            _ParentDot = null;
            _IndexDot += 1;
        }

        public void AddRelationDot(Dot RelationDot)
        {
            Array.Resize(ref RelatedDots, r+1);
            RelatedDots[r] = RelationDot;
            r+=1;
        }
    }
}
