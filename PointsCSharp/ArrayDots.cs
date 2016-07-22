using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotsGame
{
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
    public class ArrayDots_old : IEnumerator, IEnumerable
    {
        private Dot[,] Dots;//основной массив, где хранятся точки
        public Dot[,] DotsCopy
        {
            get
            {
                Dot[,] dots = (Dot[,])Array.CreateInstance(typeof(Dot), nSize, nSize); //(Dot[,])Dots.Clone();
                for (int i = 0; i < nSize; i++)
			    {
                    for (int j = 0; j < nSize; j++)
                    {
                        dots[i, j] = Dots[i, j].DotCopy;
                    }
			    } 
                return dots;
            }
        }

        int position = -1;
        private int nSize;//размер поля

        public ArrayDots_old(int size)
        {
            int counter=0;
            Dots = new Dot[size, size];
            nSize = size; 
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Dots[i,j]=new Dot(i, j);
                    Dots[i,j].IndexDot = counter;
                    if(i==0 | i == (size-1) | j == 0 | j==(size-1)) Dots[i,j].Fixed=true;
                    counter += 1;
                }
            }
        }

    public class DotEq : EqualityComparer<Dot>
        {
            public override int GetHashCode(Dot dot)
            {
                int hCode = dot.x ^ dot.y;
                return hCode.GetHashCode();
            }

            public override bool Equals(Dot d1, Dot d2)
            {
                return Default.Equals(d1, d2);
            }
        }

        public int Count
        {
            get
            {
                return Dots.Length;
            }
        }
        public Dot this[int i, int j]//Индексатор возвращает элемент из массива по его индексу
        {
            get
            {
                if (i < 0) i = 0;
                if (j < 0) j = 0;
                if (i >= nSize) i = nSize - 1;
                if (j >= nSize) j = nSize - 1;
                return Dots[i,j];
            }
            set
            {
                Dots[i,j] = value;
            }
        }
        public void Add(Dot dot, int Owner)//добавляет точку в массив
        {
            if (Contains(dot))
            {
                Dots[dot.x, dot.y].Own = Owner;
                if (Owner!=0) Dots[dot.x, dot.y].IndexRelation = Dots[dot.x, dot.y].IndexDot;
                Dots[dot.x, dot.y].Blocked = false;
                AddNeibor(Dots[dot.x, dot.y]);
            }
        }

        private void AddNeibor(Dot dot)
        {
            //выбрать соседние точки, если такие есть
            var q = from Dot d in Dots where d.Own == dot.Own & Distance(dot,d) < 2 select d;

                foreach (Dot d in q)
                {
                    if(d!=dot)
                    {
                        if (dot.Rating > d.Rating) dot.Rating = d.Rating;
                        if (dot.NeiborDots.Contains(d) == false) dot.NeiborDots.Add(d);
                        if (d.NeiborDots.Contains(dot) == false) d.NeiborDots.Add(dot);
                    }
                }
                MakeIndexRelation(dot);
        }

        private void RemoveNeibor(Dot dot)
        {
            foreach(Dot d in Dots)
            {
                if(d.NeiborDots.Contains(dot)) d.NeiborDots.Remove(dot);
            }
        }
        
        public void Remove(Dot dot)//удаляет точку из массива
        {
            int i = Dots[dot.x, dot.y].IndexDot;
            RemoveNeibor(dot);
            Dots[dot.x, dot.y] = new Dot(dot.x, dot.y);
            Dots[dot.x, dot.y].IndexDot=i;
            Dots[dot.x, dot.y].IndexRelation = i;
        }
        public void Remove(int x, int y)//удаляет точку из массива
        {
            if (Contains(x, y))
            {
                RemoveNeibor(Dots[x,y]);
                int i = Dots[x, y].IndexDot;
                Dots[x, y] = new Dot(x, y);
                Dots[x, y].IndexDot = i;
                Dots[x, y].IndexRelation = i;
                Dots[x, y].NeiborDots.Clear();
                Dots[x, y].BlokingDots.Clear();
                Dots[x, y].Own=0;
            }
        }
        public float Distance(Dot dot1, Dot dot2)//расстояние между точками
        {
            return (float)Math.Sqrt(Math.Pow((dot1.x - dot2.x),2) + Math.Pow((dot1.y - dot2.y), 2));
        }
        public bool Contains(Dot dot)//проверяет, есть ли точка с такими координатами в массиве
        {
            if (dot == null) return false;
                if (dot.x >=0 & dot.x<nSize & dot.y >= 0 & dot.y<nSize )
                {
                    return true;
                } 
            return false;
        }

        public bool Contains(int x, int y)//проверяет, есть ли точка с такими координатами в массиве
        {
            if (x >= 0 & x < nSize & y >= 0 & y < nSize)
            {
                return true;
            }
            return false;
        }
        public void UnmarkAllDots()
        {
            foreach (Dot d in Dots)
            {
                d.Marked = false;
                d.PatternsFirstDot = false;
                d.PatternsMoveDot = false;
                d.PatternsAnyDot=false;
                d.PatternsEmptyDot=false;
            }
        }
        public int MinX()
        {
        var q = from Dot d in Dots where d.Own!=0 & d.Blocked==false select d;
            int minX=nSize;
            foreach (Dot d in q)
            {
                if(minX>d.x)minX=d.x; 
            }
            return minX;
        }
        public int MaxX()
        {
            var q = from Dot d in Dots where d.Own != 0 & d.Blocked == false select d;
            int maxX = 0;
            foreach (Dot d in q)
            {
                if (maxX < d.x) maxX = d.x;
            }
            return maxX;
        }
        public int MaxY()
        {
            var q = from Dot d in Dots where d.Own != 0 & d.Blocked == false select d;
            int maxY = 0;
            foreach (Dot d in q)
            {
                if (maxY < d.y) maxY = d.y;
            }
            return maxY;
        }
        public int MinY()
        {
            var q = from Dot d in Dots where d.Own != 0 & d.Blocked == false select d;
            int minY = nSize;
            foreach (Dot d in q)
            {
                if (minY > d.y) minY = d.y;
            }
            return minY;
        }

        public int CountNeibourDots(int Owner)//количество точек определенного цвета возле пустой точки
        {
            var q = from Dot d in Dots
                    where d.Blocked == false & d.Own == 0 & 
                    Dots[d.x + 1, d.y - 1].Blocked == false & Dots[d.x + 1, d.y - 1].Own == Owner & Dots[d.x + 1, d.y + 1].Blocked == false & Dots[d.x + 1, d.y + 1].Own == Owner
                    | d.Own == 0 & Dots[d.x, d.y - 1].Blocked == false & Dots[d.x, d.y - 1].Own == Owner & Dots[d.x, d.y + 1].Blocked == false & Dots[d.x, d.y + 1].Own == Owner
                    | d.Own == 0 & Dots[d.x-1, d.y - 1].Blocked == false & Dots[d.x-1, d.y - 1].Own == Owner & Dots[d.x-1, d.y + 1].Blocked == false & Dots[d.x-1, d.y + 1].Own == Owner
                    | d.Own == 0 & Dots[d.x-1, d.y - 1].Blocked == false & Dots[d.x-1, d.y - 1].Own == Owner & Dots[d.x+1, d.y + 1].Blocked == false & Dots[d.x+1, d.y + 1].Own == Owner
                    | d.Own == 0 & Dots[d.x-1, d.y + 1].Blocked == false & Dots[d.x-1, d.y + 1].Own == Owner & Dots[d.x+1, d.y - 1].Blocked == false & Dots[d.x+1, d.y - 1].Own == Owner
                    select d;
            return q.Count();
        }
        
        public List<Dot> EmptyNeibourDots(int Owner)//список не занятых точек возле определенной точки
        {
            List<Dot> ld = new List<Dot>();     
            foreach (Dot d in Dots)
            {
                if(d.Own==Owner)
                {
                    var q = from Dot dot in Dots
                    where dot.Blocked == false & dot.Own == 0 & Distance(dot,d)<2
                    select dot;
                    foreach(Dot empty_d in q)
                    {
                        if (ld.Contains(empty_d)==false) ld.Add(empty_d); 
                    }
                }
            }
            return ld;
        }
       
        public int MakeIndexRelation (Dot dot)
        {
            if (dot.NeiborDots.Count>0)
            {
                foreach(Dot d in dot.NeiborDots)
                {
                    if (dot.Blocked == false & dot.Own == d.Own) d.IndexRelation = dot.IndexRelation;
                }
            }
            else
            {
            }
            return dot.IndexRelation;
        }

        public Dot[] NotBlockedDots()
        {
            var q = from Dot d in Dots where d.Blocked==false select d;
            return q.ToArray();
        }
        
        public void Clear()
        {
            foreach (Dot d in Dots)
            {
                d.Own = 0;
                d.Marked = false;
                d.Blocked = false;
                d.BlokingDots.Clear();
                d.Rating=0;
            }
           
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            position = -1;
            //return this;
            yield return Dots;
        }
        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < Dots.Length);
        }
        //IEnumerable
        public void Reset()
        { position = 0; }
        //IEnumerable
        public object Current
        {
            get
            {
                int i = position / nSize;
                if (position / nSize == nSize)
                {
                    i = nSize - 1;
                }
                int j = position % nSize;
                if (position % nSize == nSize)
                {
                    j = nSize - 1;
                }
                return Dots[i, position % nSize];
            }
        }
    }
    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
    public class ArrayDots : IEnumerator, IEnumerable
    {
        private List<Dot> _Dots;
        public List<Dot> Dots
        {
            get
            {
                return _Dots;
            }
            set
            {
                _Dots = value;
            }
        }
        public ArrayDots CopyArrayDots
        {
            get
            {
                ArrayDots ad = new ArrayDots(nSize);
                for (int i = 0; i < _Dots.Count; i++)
                {
                    ad._Dots[i].Blocked = _Dots[i].Blocked;
                    ad._Dots[i].Fixed = _Dots[i].Fixed;
                    ad._Dots[i].IndexDot = _Dots[i].IndexDot;
                    ad._Dots[i].Own = _Dots[i].Own;
                    ad._Dots[i].x = _Dots[i].x;
                    ad._Dots[i].y = _Dots[i].y;
                }
                //ad.Dots = _Dots.ConvertAll(dot => new Dot(dot.x, dot.y, dot.Own));
                return ad;
            }
        }


        int position = -1;
        private int nSize;//размер поля

        public ArrayDots(int size)
        {
            int counter = 0;
            int ind;
            _Dots = new List<Dot>(size*size);
            nSize = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    ind = IndexDot(i, j);
                    _Dots.Add(new Dot(i, j));
                    _Dots[ind].IndexDot = ind;
                    if (i == 0 | i == (size - 1) | j == 0 | j == (size - 1)) _Dots[ind].Fixed = true;
                    counter += 1;
                }
            }
        }

        public class DotEq : EqualityComparer<Dot>
        {
            public override int GetHashCode(Dot dot)
            {
                int hCode = dot.x ^ dot.y;
                return hCode.GetHashCode();
            }

            public override bool Equals(Dot d1, Dot d2)
            {
                return Default.Equals(d1, d2);
            }
        }

        public int Count
        {
            get
            {
                return _Dots.Count;
            }
        }
        public Dot this[int i, int j]//Индексатор возвращает элемент из массива по его индексу
        {
            get
            {
                if (i < 0) i = 0;
                if (j < 0) j = 0;
                if (i >= nSize) i = nSize - 1;
                if (j >= nSize) j = nSize - 1;
                return _Dots[IndexDot(i, j)];
            }
        }
        public void Add(Dot dot, int Owner)//добавляет точку в массив
        {
            int ind = IndexDot(dot.x, dot.y);
            if (Contains(dot))
            {
                _Dots[ind].Own = Owner;
                if (Owner != 0) _Dots[ind].IndexRelation = _Dots[ind].IndexDot;
                _Dots[ind].Blocked = false;
                AddNeibor(_Dots[ind]);
            }
        }

        private void AddNeibor(Dot dot)
        {
            //выбрать соседние точки, если такие есть
            var q = from Dot d in _Dots where d.Own == dot.Own & Distance(dot, d) < 2 select d;

            foreach (Dot d in q)
            {
                if (d != dot)
                {
                    if (dot.Rating > d.Rating) dot.Rating = d.Rating;
                    if (dot.NeiborDots.Contains(d) == false) dot.NeiborDots.Add(d);
                    if (d.NeiborDots.Contains(dot) == false) d.NeiborDots.Add(dot);
                }
            }
            MakeIndexRelation(dot);
        }

        private void RemoveNeibor(Dot dot)
        {
            foreach (Dot d in Dots)
            {
                if (d.NeiborDots.Contains(dot)) d.NeiborDots.Remove(dot);
            }
        }

        public void Remove(Dot dot)//удаляет точку из массива
        {
            int ind = IndexDot(dot.x, dot.y);
            int i = _Dots[ind].IndexDot;
            RemoveNeibor(dot);
            _Dots[ind] = new Dot(dot.x, dot.y);
            _Dots[ind].IndexDot = i;
            _Dots[ind].IndexRelation = i;
        }
        public void Remove(int x, int y)//удаляет точку из массива
        {
            if (Contains(x, y))
            {
                int ind = IndexDot(x,y);

                RemoveNeibor(Dots[ind]);
                int i = Dots[ind].IndexDot;
                _Dots[ind] = new Dot(x, y);
                _Dots[ind].IndexDot = i;
                _Dots[ind].IndexRelation = i;
                _Dots[ind].NeiborDots.Clear();
                _Dots[ind].BlokingDots.Clear();
                _Dots[ind].Own = 0;
            }
        }
        public float Distance(Dot dot1, Dot dot2)//расстояние между точками
        {
            return (float)Math.Sqrt(Math.Pow((dot1.x - dot2.x), 2) + Math.Pow((dot1.y - dot2.y), 2));
        }
        public bool Contains(Dot dot)//проверяет, есть ли точка с такими координатами в массиве
        {
            if (dot == null) return false;
            if (dot.x >= 0 & dot.x < nSize & dot.y >= 0 & dot.y < nSize)
            {
                return true;
            }
            return false;
        }

        public bool Contains(int x, int y)//проверяет, есть ли точка с такими координатами в массиве
        {
            if (x >= 0 & x < nSize & y >= 0 & y < nSize)
            {
                return true;
            }
            return false;
        }
        public void UnmarkAllDots()
        {
            foreach (Dot d in _Dots)
            {
                d.Marked = false;
                d.PatternsFirstDot = false;
                d.PatternsMoveDot = false;
                d.PatternsAnyDot = false;
                d.PatternsEmptyDot = false;
            }
        }
        public int MinX()
        {
            var q = from Dot d in _Dots where d.Own != 0 & d.Blocked == false select d;
            int minX = nSize;
            foreach (Dot d in q)
            {
                if (minX > d.x) minX = d.x;
            }
            return minX;
        }
        public int MaxX()
        {
            var q = from Dot d in _Dots where d.Own != 0 & d.Blocked == false select d;
            int maxX = 0;
            foreach (Dot d in q)
            {
                if (maxX < d.x) maxX = d.x;
            }
            return maxX;
        }
        public int MaxY()
        {
            var q = from Dot d in _Dots where d.Own != 0 & d.Blocked == false select d;
            int maxY = 0;
            foreach (Dot d in q)
            {
                if (maxY < d.y) maxY = d.y;
            }
            return maxY;
        }
        public int MinY()
        {
            var q = from Dot d in _Dots where d.Own != 0 & d.Blocked == false select d;
            int minY = nSize;
            foreach (Dot d in q)
            {
                if (minY > d.y) minY = d.y;
            }
            return minY;
        }

        public int CountNeibourDots(int Owner)//количество точек определенного цвета возле пустой точки
        {
            var q = from Dot d in _Dots
                    where d.Blocked == false & d.Own == 0 &
                    _Dots[IndexDot(d.x + 1, d.y - 1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y - 1)].Own == Owner & 
                    _Dots[IndexDot(d.x + 1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x, d.y - 1)].Blocked == false & _Dots[IndexDot(d.x, d.y - 1)].Own == Owner & _Dots[IndexDot(d.x, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x - 1, d.y - 1)].Blocked == false & _Dots[IndexDot(d.x - 1, d.y - 1)].Own == Owner & _Dots[IndexDot(d.x - 1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x - 1, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x - 1, d.y - 1)].Blocked == false & _Dots[IndexDot(d.x - 1, d.y - 1)].Own == Owner & _Dots[IndexDot(d.x + 1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x - 1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x - 1, d.y + 1)].Own == Owner & _Dots[IndexDot(d.x + 1, d.y - 1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y - 1)].Own == Owner
                    select d;
            return q.Count();
        }
        public int IndexDot(int x, int y)
        {
            return x*nSize+y;
        }
        public List<Dot> EmptyNeibourDots(int Owner)//список не занятых точек возле определенной точки
        {
            List<Dot> ld = new List<Dot>();
            foreach (Dot d in _Dots)
            {
                if (d.Own == Owner)
                {
                    var q = from Dot dot in _Dots
                            where dot.Blocked == false & dot.Own == 0 & Distance(dot, d) < 2
                            select dot;
                    foreach (Dot empty_d in q)
                    {
                        if (ld.Contains(empty_d) == false) ld.Add(empty_d);
                    }
                }
            }
            return ld;
        }

        public int MakeIndexRelation(Dot dot)
        {
            if (dot.NeiborDots.Count > 0)
            {
                foreach (Dot d in dot.NeiborDots)
                {
                    if (dot.Blocked == false & dot.Own == d.Own) d.IndexRelation = dot.IndexRelation;
                }
            }
            else
            {
            }
            return dot.IndexRelation;
        }

        public Dot[] NotBlockedDots()
        {
            var q = from Dot d in _Dots where d.Blocked == false select d;
            return q.ToArray();
        }

        public void Clear()
        {
            foreach (Dot d in _Dots)
            {
                d.Own = 0;
                d.Marked = false;
                d.Blocked = false;
                d.BlokingDots.Clear();
                d.Rating = 0;
            }

        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            position = -1;
            //return this;
            return _Dots.GetEnumerator();
        }
        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < _Dots.Count);
        }
        //IEnumerable
        public void Reset()
        { position = 0; }
        //IEnumerable
        public object Current
        {
            get
            {
                int i = position / nSize;
                if (position / nSize == nSize)
                {
                    i = nSize - 1;
                }
                int j = position % nSize;
                if (position % nSize == nSize)
                {
                    j = nSize - 1;
                }
                return _Dots[IndexDot(i, position % nSize)];
            }
        }
    }

}
