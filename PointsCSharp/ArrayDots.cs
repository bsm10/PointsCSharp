using System;
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
        private Dot[] Dots;
        int position = -1;
        public ArrayDots()

        {
            Dots = new Dot[0]; 
            //Count = 0;
        }
        public int Count
        {
            get
            {
                return Dots.Length;
            }
        }
        public int CountDotsInRegion (int Owner)
        {
            int j = 0;
            for (int i = 0; i < Count; i++)
            {
                if(Dots[i].Own == Owner)
                {
                    j += Dots[i].InRegion == true ? 1 : 0;
                }
                    
            }
            return  j;
        }

        public Dot this[int i]//Индексатор возвращает элемент из массива по его индексу
        {
            get
            {
                return Dots[i];
            }
            set
            {
                Dots[i] = value;
            }
        }

        public void Add(Dot Dot)//добавляет точку в массив
        {
            if (Contains(Dot) == -1)
            {
                Array.Resize(ref Dots, Count + 1);
                Dots[Count - 1] = Dot;
            }
        }
        public void AddArrayDots(ArrayDots array_dots)//добавляет точку в массив
        {
            foreach (Dot p in array_dots)
            {
                Add(p);
            }

        }

        public void Sort(ref Dot[] arrDots)//принимает пустой массив, в который возвращает отсортированный массив по Х и У
        {
            arrDots=(Dot[])Dots.Clone();
            ComparerDots cmp = new ComparerDots();
            Array.Sort(arrDots, cmp);
        }
        public void Remove(Dot Dot)//удаляет точку из массива
        {
            int res = Contains(Dot);
            Dot[] Temp;
            Temp = new Dot[Count-1];
            int j=0;
            if (res>=0)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (Dots[i].x == Dot.x & Dots[i].y == Dot.y)
                    {
                    //сдесь делаем пропуск, чтобы элемент не попал в новый массив
                    }
                    else
                    {
                        Temp[j] = Dots[i];
                        j += 1;
                    }
                }
                Dots = Temp;
            }
        }
        public int Contains(Dot Dot)//проверяет, есть ли точка с такими координатами в массиве
        {
            for (int i = 0; i < Count; i++)
            {
                if (Dots[i].x == Dot.x & Dots[i].y == Dot.y)
                {
                    return i;
                } 
            }
            return -1;
        }
        public int Contains(int x, int y)//проверяет, есть ли точка с такими координатами в массиве
        {
            for (int i = 0; i < Count; i++)
            {
                if (Dots[i].x == x & Dots[i].y == y)
                {
                    return i;
                }
            }
            return -1;
        }
        public void UnmarkAllDots()
        {
            for (int i = 0; i < Count; i++)
            {
                Dots[i].Marked = false;
                Dots[i].FirstDot = false;
            }
        }
        public void Clear()
        {
            Dots = new Dot[0];
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
            get {return Dots[position];}
        }
    }
}
