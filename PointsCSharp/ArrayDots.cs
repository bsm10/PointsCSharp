using System.Collections;
using System.Collections.Generic;

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
        private Dot[,] Dots;
        private Dot[,] _Dots;
        
        int position = -1;
        private int nSize;
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
            }
        }
        public void Add(int x, int y, int own)//меняет владельца точки
        {
            Dots[x, y].Own = own;
            Dots[x, y].Blocked = false;
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

        }
        public void Remove(int x, int y)//удаляет точку из массива
        {
            int i = Dots[x, y].IndexDot;
            Dots[x, y] = new Dot(x, y);
            Dots[x, y].IndexDot = i;

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
        public void Save()//сохраняет текущее состояние массива БЕТА!
        {
            Dot[,] tmp = new Dot[nSize, nSize];
            tmp=(Dot[,])Dots.Clone();
            _Dots=tmp;
            //Array.Copy(tmp, 0, _Dots, 0, nSize * nSize);
        }
        public void Restore()//восстанавливает состояние массива БЕТА!
        {
            Dots = null;
            Dots =(Dot[,])_Dots.Clone();
        }
        public void Clear()
        {
            foreach (Dot d in Dots)
            {
                d.Own = 0;
                d.IndexRelation = 0;
                d.Parent = null;
                d.Marked = false;
                d.InRegion=false;
                d.IndexRelation=0;
                d.Blocked  = false;
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
