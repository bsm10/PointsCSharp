using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    
    public class ArrayDots : IEnumerator, IEnumerable
    {
        private Dot[,] Dots;//основной массив, где хранятся точки
        int position = -1;
        private int nSize;//размер поля
        public ArrayDots(int size)

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
        public void Add(Dot Dot)//добавляет точку в массив
        {
            if (Contains(Dot))
            {
                Dots[Dot.x, Dot.y].Own = Dot.Own;
                Dots[Dot.x, Dot.y].Blocked = false;
                MakeRating();
                AddNeibor(Dots[Dot.x, Dot.y]);
                
            }
        }
        public void Add(int x, int y, int own)//меняет владельца точки
        {
            Dots[x, y].Own = own;
            Dots[x, y].Blocked = false;
            MakeRating();
            AddNeibor(Dots[x, y]);
            
        }
        private void AddNeibor(Dot dot)
        {
            if (dot.x > 0 & dot.y > 0 & dot.x < nSize-1 & dot.y < nSize-1)
            {    
                Dot[] dts = new Dot[8] {Dots[dot.x + 1, dot.y], Dots[dot.x - 1, dot.y],
                                        Dots[dot.x, dot.y + 1], Dots[dot.x, dot.y - 1],
                                        Dots[dot.x+1, dot.y + 1], Dots[dot.x-1, dot.y - 1],
                                        Dots[dot.x+1, dot.y - 1 ], Dots[dot.x-1, dot.y + 1]};                                       

                var q = from Dot d in dts where d.Blocked==false & d.Own == dot.Own select d;

                foreach (Dot d in q)
                {
                    if (dot.Rating>d.Rating) dot.Rating=d.Rating;
                    if(dot.NeiborDots.Contains(d)==false) dot.NeiborDots.Add(d);
                    if (d.NeiborDots.Contains(dot) == false) d.NeiborDots.Add(dot);     
                }
            }
            else if(dot.x==0)
            {
                if (Dots[dot.x+1,dot.y].Own == dot.Own) 
                {
                    dot.NeiborDots.Add(Dots[dot.x + 1, dot.y]);
                    Dots[dot.x + 1, dot.y].NeiborDots.Add(dot);
                    Dots[dot.x+1,dot.y].Rating=0;
                }
            }
            else if (dot.x == nSize-1)
            {
                if (Dots[dot.x - 1, dot.y].Own == dot.Own)
                {
                    dot.NeiborDots.Add(Dots[dot.x + 1, dot.y]);
                    Dots[dot.x - 1, dot.y].NeiborDots.Add(dot);
                    Dots[dot.x - 1, dot.y].Rating = 0;
                }
            }
            else if (dot.y == 0)
            {
                if (Dots[dot.x , dot.y+1].Own == dot.Own)
                {
                    dot.NeiborDots.Add(Dots[dot.x, dot.y+1]);
                    Dots[dot.x, dot.y+1].NeiborDots.Add(dot);
                    Dots[dot.x, dot.y+1].Rating = 0;
                }
            }
            else if (dot.y == nSize - 1)
            {
                if (Dots[dot.x, dot.y - 1].Own == dot.Own)
                {
                    dot.NeiborDots.Add(Dots[dot.x, dot.y - 1]);
                    Dots[dot.x, dot.y - 1].NeiborDots.Add(dot);
                    Dots[dot.x, dot.y - 1].Rating = 0;
                }
            }
        }
        private void RemoveNeibor(Dot dot)
        {
            foreach(Dot d in Dots)
            {
                if(d.NeiborDots.Contains(dot)) d.NeiborDots.Remove(dot);
            }
        }
        
        //public void Sort(ref Dot[] arrDots)//принимает пустой массив, в который возвращает отсортированный массив по Х и У
        //{
        //    arrDots=(Dot[])Dots.Clone();
        //    ComparerDots cmp = new ComparerDots();
        //    Array.Sort(arrDots, cmp);
        //}
        public void Remove(Dot dot)//удаляет точку из массива
        {
            int i = Dots[dot.x, dot.y].IndexDot;
            Dots[dot.x, dot.y] = new Dot(dot.x, dot.y);
            Dots[dot.x, dot.y].IndexDot=i;
            MakeRating();
        }
        public void Remove(int x, int y)//удаляет точку из массива
        {
            if (Contains(x, y))
            {
                int i = Dots[x, y].IndexDot;
                Dots[x, y] = new Dot(x, y);
                Dots[x, y].IndexDot = i;
                MakeRating();
            }
        }
        public float Distance(Dot dot1, Dot dot2)//расстояние между точками
        {
            return (float)Math.Sqrt(Math.Pow((dot1.x - dot2.x),2) + Math.Pow((dot1.y - dot2.y), 2));
        }
        public bool Contains(Dot Dot)//проверяет, есть ли точка с такими координатами в массиве
        {
                if (Dot.x >=0 & Dot.x<nSize & Dot.y >= 0 & Dot.y<nSize )
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
            }
        }
        public Dot[] NotBlockedDots()
        {
            var q = from Dot d in Dots where d.Blocked==false select d;
            return q.ToArray();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void MakeRating()//возвращает массив вражеских точек вокруг заданной точки
        {
            int res = 0;
            var qd = from Dot dt in Dots where dt.Own!=0 select dt;
            foreach (Dot dot in qd)
            {
                if (dot.x > 0 & dot.y > 0 & dot.x < nSize-1 & dot.y < nSize-1)
                {    
                    Dot[] dts = new  Dot[8] {Dots[dot.x + 1, dot.y], Dots[dot.x - 1, dot.y],
                                        Dots[dot.x, dot.y + 1], Dots[dot.x, dot.y - 1],
                                        Dots[dot.x+1, dot.y + 1], Dots[dot.x-1, dot.y - 1],
                                        Dots[dot.x+1, dot.y - 1 ], Dots[dot.x-1, dot.y + 1]};                                       
                    var q = from Dot d in dts where d.Own!=0 & d.Own != dot.Own select d;
                    res = q.Count();
                    dot.Rating = res;
                }
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
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
            position =-1;
            return this;
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
            get {return Dots[position/nSize,position%nSize];}
        }
    }
}
