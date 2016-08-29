using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotsGame
{

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
    public class GameDots : IEnumerator, IEnumerable
    {
        private List<Dot> list_moves; //список ходов
        public List<Dot> ListMoves
        {
            get { return list_moves; }
        }

        private List<Dot> _Dots; // главная коллекция
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
        public List<Dot> FreeDots
        {
            get
            {
                return (from Dot d in _Dots where d.Own == 0 && d.Blocked == false select d).ToList();
            }
        }
        public List<Dot> NotEmptyNonBlockedDots
        {
            get
            {
                return (from Dot d in _Dots where d.Own != 0 && d.Blocked == false select d).ToList();
            }
        }
        public GameDots CopyDots
        {
            get
            {
                GameDots ad = new GameDots(BoardWidth,BoardHeight);
                for (int i = 0; i < Dots.Count; i++)
                {
                    ad._Dots[i].Blocked = Dots[i].Blocked;
                    ad._Dots[i].Fixed = Dots[i].Fixed;
                    ad._Dots[i].IndexDot = Dots[i].IndexDot;
                    ad._Dots[i].Own = Dots[i].Own;
                    ad._Dots[i].x = Dots[i].x;
                    ad._Dots[i].y = Dots[i].y;
                }
                //ad.Dots = _Dots.ConvertAll(dot => new Dot(dot.x, dot.y, dot.Own));
                return ad;
            }
        }

        private Dot last_move; //последний ход
        public int win_player;//переменная получает номер игрока, котрый окружил точки

        int position = -1;
        private int nBoardWidth;//размер поля
        private int nBoardHeight;//размер поля

        public int BoardWidth
        {
            get { return nBoardWidth; }
            set { nBoardWidth = value; }
        }

        public int BoardHeight
        {
            get { return nBoardHeight; }
            set { nBoardHeight = value; }
        }

        public int BoardSize
        {
            get
            {
                return nBoardWidth * nBoardHeight;
            }
        }
        public GameDots(int boardwidth, int boardheight)
        {
            int counter = 0;
            int ind;
            BoardHeight = boardheight;
            BoardWidth = boardwidth;
            _Dots = new List<Dot>(boardwidth * boardheight); // главная коллекция точек
            for (int i = 0; i < boardwidth; i++)
            {
                for (int j = 0; j < boardheight; j++)
                {
                    ind = IndexDot(i, j);
                    _Dots.Add(new Dot(i, j));
                    _Dots[ind].IndexDot = ind;
                    if (i == 0 | i == (boardwidth - 1) | j == 0 | j == (boardheight - 1)) _Dots[ind].Fixed = true;
                    counter += 1;
                }
            }

            lnks = new List<Links>();
            list_moves = new List<Dot>();

        }

        public class DotEq : EqualityComparer<Dot>
        {
            public override int GetHashCode(Dot dot)
            {
                int hCode = dot.x * dot.y;
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
                if (i >= BoardWidth) i = BoardWidth - 1;
                if (j >= BoardHeight) j = BoardHeight -1;
                if (i < 0) i = 0;
                if (j < 0) j = 0;
                return _Dots[IndexDot(i, j)];
            }
        }
        private void Add(Dot dot, int Owner)//добавляет точку в массив
        {
            int ind = IndexDot(dot.x, dot.y);
            //if (Contains(dot))
            //{
                _Dots[ind].Own = Owner;
                if (Owner != 0) _Dots[ind].IndexRelation = _Dots[ind].IndexDot;
                _Dots[ind].Blocked = false;
                if (dot.x == 0 | dot.x == (BoardWidth - 1) | dot.y == 0 | 
                    dot.y == (BoardHeight - 1)) _Dots[ind].Fixed = true;
                AddNeibor(_Dots[ind]);
            //}
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

        private void Remove(Dot dot)//удаляет точку из массива
        {
            int ind = IndexDot(dot.x, dot.y);
            int i = _Dots[ind].IndexDot;
            RemoveNeibor(dot);
            _Dots[ind] = new Dot(dot.x, dot.y);
            _Dots[ind].IndexDot = i;
            _Dots[ind].IndexRelation = i;
        }
        private void Remove(int x, int y)//удаляет точку из массива
        {
            //if (Contains(x, y))
            //{
                int ind = IndexDot(x,y);

                RemoveNeibor(Dots[ind]);
                int i = Dots[ind].IndexDot;
                _Dots[ind] = new Dot(x, y);
                _Dots[ind].IndexDot = i;
                _Dots[ind].IndexRelation = i;
                _Dots[ind].NeiborDots.Clear();
                _Dots[ind].BlokingDots.Clear();
                _Dots[ind].Own = 0;
            //}
        }
        public float Distance(Dot dot1, Dot dot2)//расстояние между точками
        {
            return (float)Math.Sqrt(Math.Pow((dot1.x -dot2.x), 2) + Math.Pow((dot1.y -dot2.y), 2));
        }
        /// <summary>
        /// возвращает список соседних точек заданной точки
        /// </summary>
        /// <param name="dot"> точка Dot из массива точек типа ArrayDots </param>
        /// <returns> список точек </returns>
        public List<Dot> NeiborDotsSNWE(Dot dot)//SNWE -S -South, N -North, W -West, E -East
        {
            Dot[] dts = new Dot[4] {
                                    this[dot.x + 1, dot.y],
                                    this[dot.x - 1, dot.y],
                                    this[dot.x, dot.y + 1],
                                    this[dot.x, dot.y - 1]
                                    };
            return dts.ToList();
        }

        public List<Dot> NeiborDotsAll(Dot dot)
        {
            Dot[] dts = new Dot[8] {
                                    this[dot.x + 1, dot.y],
                                    this[dot.x - 1, dot.y],
                                    this[dot.x, dot.y + 1],
                                    this[dot.x, dot.y - 1],
                                    this[dot.x + 1, dot.y + 1],
                                    this[dot.x - 1, dot.y - 1],
                                    this[dot.x - 1, dot.y + 1],
                                    this[dot.x + 1, dot.y - 1]
                                    };
            return dts.ToList();
        }




        //public bool Contains(Dot dot)//проверяет, есть ли точка с такими координатами в массиве
        //{
        //    if (dot == null) return false;
        //    if (dot.x >= 0 & dot.x < BoardWidth & dot.y >= 0 & dot.y < BoardHeight)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool Contains(int x, int y)//проверяет, есть ли точка с такими координатами в массиве
        //{
        //    if (x >= 0 & x < BoardWidth & y >= 0 & y < BoardHeight)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public void UnmarkAllDots()
        {
            foreach (Dot d in _Dots) d.UnmarkDot();
        }
        public int MinX()
        {
            var q = from Dot d in _Dots where d.Own != 0 & d.Blocked == false select d;
            int minX = BoardWidth;
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
            int minY = BoardHeight;
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
                    _Dots[IndexDot(d.x + 1, d.y -1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y -1)].Own == Owner & 
                    _Dots[IndexDot(d.x + 1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x, d.y -1)].Blocked == false & _Dots[IndexDot(d.x, d.y -1)].Own == Owner & _Dots[IndexDot(d.x, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x -1, d.y -1)].Blocked == false & _Dots[IndexDot(d.x -1, d.y -1)].Own == Owner & _Dots[IndexDot(d.x -1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x -1, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x -1, d.y -1)].Blocked == false & _Dots[IndexDot(d.x -1, d.y -1)].Own == Owner & _Dots[IndexDot(d.x + 1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y + 1)].Own == Owner
                    | d.Own == 0 & _Dots[IndexDot(d.x -1, d.y + 1)].Blocked == false & _Dots[IndexDot(d.x -1, d.y + 1)].Own == Owner & _Dots[IndexDot(d.x + 1, d.y -1)].Blocked == false & _Dots[IndexDot(d.x + 1, d.y -1)].Own == Owner
                    select d;
            return q.Count();
        }
        /// <summary>
        /// Вычисляет индекс точки в списке по ее координатам
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int IndexDot(int x, int y)
        {
            if (x < 0) x = 0;
            else if (x > BoardWidth) x = BoardWidth - 1;
            if (y < 0) y = 0;
            else if (y > BoardHeight) y = BoardHeight - 1;

            int index = x * BoardHeight + y;

            return index;
        }


        public List<Dot> Rotate90(List<Dot> DotsForRotation)
        {
            int x;
            int y;
            List<Dot> newList = new List<Dot>(DotsForRotation.Count);
            foreach (Dot d in DotsForRotation)
            {
                x = d.x; y = d.y;
                Dot dot = d.DotCopy; 
                dot.x = y;
                dot.y = x;
                newList.Add(dot);
            }
            return newList;
        }
        public List<Dot> Rotate_Mirror_Horizontal(List<Dot> DotsForRotation)
        {
            int x;
            //int y;
            List<Dot> newList = new List<Dot>(DotsForRotation.Count);
            foreach (Dot d in DotsForRotation)
            {
                x = BoardWidth - d.x; 
                //d.x = x;
                Dot dot = d.DotCopy;
                dot.x = x;
                newList.Add(dot);
            }
            return newList;
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
        public void Clear()//Не очищает список, а заменяет точки на Own == 0
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

        /// <summary>
        /// Пересканирует блокированные точки и устанавливает статус Blocked
        /// </summary>
        public void RescanBlockedDots()
        {
            //-------------------Rescan Blocked Dots------------------
            var dots_bl = from Dot d in Dots
                          where d.BlokingDots.Count > 0
                          select d.Blocked = true;

            //-------------------Rescan Blocked EmptyDots---------------------

            while ((from Dot d in Dots
                where d.Own == 0 && d.Blocked == false &&
                      this[d.x + 1, d.y].Blocked |
                      this[d.x - 1, d.y].Blocked |
                      this[d.x, d.y + 1].Blocked |
                      this[d.x, d.y - 1].Blocked
                select d.Blocked = true).Count()!=0);
        }

        /// <summary>
        /// проверяет заблокирована ли точка. Перед использованием функции надо установить flg_own
        /// </summary>
        /// <param name="dot">поверяемая точка</param>
        /// <param name="flg_own">владелец проверяемой точки, этот параметр нужен для рекурсии</param>
        /// <returns></returns>
        public bool DotIsFree(Dot dot, int flg_own) 
        {
            dot.Marked = true;
            if (dot.x == 0 | dot.y == 0 | dot.x == BoardWidth - 1 | dot.y == BoardHeight - 1)
            {
                return true;
            }
            Dot[] d = new Dot[4] { this[dot.x + 1, dot.y], this[dot.x - 1, dot.y], this[dot.x, dot.y + 1], this[dot.x, dot.y - 1] };
            //--------------------------------------------------------------------------------
            if (flg_own == 0)// если точка не принадлежит никому и рядом есть незаблокированные точки -эта точка считается свободной(незаблокированной)
            {
                var q = from Dot fd in d where fd.Blocked == false select fd;
                if (q.Count() > 0) return true;
            }
            //----------------------------------------------------------------------------------
            for (int i = 0; i < 4; i++)
            {
                if (d[i].Marked == false)
                {
                    if (d[i].Own == 0 | d[i].Own == flg_own | d[i].Own != flg_own 
                      & d[i].Blocked & d[i].BlokingDots.Contains(dot) == false)
                    {
                        if (DotIsFree(d[i], flg_own))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //public bool DotIsFree(Dot dot, int flg_own, ArrayDots this)
        //{
        //    dot.Marked = true;

        //    //if (dot.x == 0 | dot.y == 0 | dot.x == iMapSize -1 | dot.y == iMapSize -1)
        //    if (dot.x == 0 | dot.y == 0 | dot.x == nSize - 1 | dot.y == nSize - 1)
        //    {
        //        return true;
        //    }
        //    Dot[] d = new Dot[4] { this[dot.x + 1, dot.y], this[dot.x - 1, dot.y], this[dot.x, dot.y + 1], this[dot.x, dot.y - 1] };
        //    //--------------------------------------------------------------------------------
        //    if (flg_own == 0)// если точка не принадлежит никому и рядом есть незаблокированные точки -эта точка считается свободной(незаблокированной)
        //    {
        //        var q = from Dot fd in d where fd.Blocked == false select fd;
        //        if (q.Count() > 0) return true;
        //    }
        //    //----------------------------------------------------------------------------------
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (d[i].Marked == false)
        //        {
        //            if (d[i].Own == 0 | d[i].Own == flg_own | d[i].Own != flg_own & d[i].Blocked & d[i].BlokingDots.Contains(dot) == false)
        //            {
        //                if (DotIsFree(d[i], flg_own, this))
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public bool DotIsFree(Dot dot, GameDots this)
        //{
        //    if (dot.Fixed) return true;
        //    var qry = from Dot d1 in this
        //              where d1.Blocked == false && d1.Own == dot.Own | d1.Own == 0
        //              orderby this.Distance(dot, d1)
        //              from Dot d2 in this
        //              where d2.Blocked == false && this.Distance(d1, d2) == 1 && d2.Own == dot.Own | d2.Own == 0
        //              select new Links(d1, d2);
        //    //var qry1 = from Dot d1 in this
        //    //           where d1.Blocked == false && d1.Own == dot.Own | d1.Own == 0
        //    //           orderby this.Distance(dot, d1)
        //    //           from Dot d2 in this
        //    //           where d2.Blocked == false && this.Distance(d1, d2) == 1 && d2.Own == dot.Own | d2.Own == 0
        //    //           orderby this.Distance(dot, d2)
        //    //           select d2;

        //    var temp = qry.Distinct(new LinksComparer());

        //    var ddd = from Links lks in temp
        //              where lks.Dot1 == dot | lks.Dot2 == dot
        //              select lks;



        //    List<Links> links = temp.ToList();
        //    //int i = 0;
        //    int dist = 0;
        //    for (int i = 0; i < links.Count - 1; i++)
        //    {

        //        dist = links[i].LinksDistance(links[i + 1]);
        //        if (dist > 1) return true;
        //        if (links[i + 1].Fixed) return true;
        //        i++;

        //    }


        //    //var ld = qry1.ToList().Distinct();



        //    if (temp.Where(dt => dt.Fixed).Count() > 0) return true;



        //    //List<Dot> lstDot = qry.ToList();
        //    return true;

        //}

        public void RebuildDots(List<Dot> listMoves)
        {
            GameDots _aDots = new GameDots(BoardWidth,BoardHeight);

            foreach (Dot dot in listMoves)
            {
                _aDots.MakeMove(dot, dot.Own);
            }

            _aDots.LinkDots();//восстанавливаем связи между точками
            _aDots.RescanBlockedDots();
            Dots = _aDots.Dots;
        }

        public void UndoMove(int x, int y)//поле отмена хода
        {
            Undo(x, y);
        }
        public void UndoMove(Dot dot)//поле отмена хода
        {
            //if(dot!=null) Undo(dot.x, dot.y, arrDots);

            //Dot dt = list_moves.Where(d=>d == dot).First(); //list_moves.Where(d=> d.x== x & d.y == y);

            list_moves.Remove(dot);
            Remove(dot);

            RebuildDots(list_moves);
            LinkDots();

            last_move = list_moves.Count == 0 ? null : list_moves.Last();

        }

        private void Undo(int x, int y)//отмена хода
        {
            List<Dot> bl_dot = new List<Dot>();
            List<Links> ln = new List<Links>();
            if (this[x, y].Blocked)//если точка была блокирована, удалить ее из внутренних списков у блокирующих точек
            {
                lst_blocked_dots.Remove(this[x, y]);
                bl_dot.Add(this[x, y]);
                foreach (Dot d in lst_in_region_dots)
                {
                    d.BlokingDots.Remove(this[x, y]);
                }
                count_blocked_dots = CheckBlocked();
            }
            if (this[x, y].BlokingDots.Count > 0)
            {
                //снимаем блокировку с точки bd, которая была блокирована UndoMove(int x, int y)
                foreach (Dot d in this[x, y].BlokingDots)
                {
                    bl_dot.Add(d);
                }
            }

            foreach (Dot d in bl_dot)
            {
                foreach (Links l in lnks)//подготовка связей которые блокировали точку
                {
                    if (l.Dot1.BlokingDots.Contains(d) | l.Dot2.BlokingDots.Contains(d))
                    {
                        ln.Add(l);
                    }
                }
                //удаляем из списка блокированных точек
                foreach (Dot bd in this)
                {
                    if (bd.BlokingDots.Count > 0)
                    {
                        bd.BlokingDots.Remove(d);
                    }
                }
                //восстанавливаем связи у которых одна из точек стала свободной
                var q_lnks = from lnk in lnks
                             where lnk.Dot1.x == d.x & lnk.Dot1.y == d.y | lnk.Dot2.x == d.x & lnk.Dot2.y == d.y
                             select lnk;
                foreach (Links l in q_lnks)
                {
                    l.Dot1.Blocked = false;
                    l.Dot2.Blocked = false;
                }

            }
            //удаляем связи
            foreach (Links l in ln)
            {
                lnks.Remove(l);
            }
            ln = null;
            bl_dot = null;

            Remove(x, y);
            count_blocked_dots = CheckBlocked();
            //ScanBlockedFreeDots();            
            UnmarkAllDots();
            LinkDots();
            last_move = list_moves.Count == 0 ? null : list_moves.Last();
        }

        public Dot CheckPattern(int Owner)
        {
            Pattern p = CheckPatternInPatterns(Owner);
            if (p != null)
            {
                Dot move_dot = p.ResultDot;
                move_dot.iNumberPattern = p.PatternNumber;
                return move_dot;
            }
            return null;

        }

        /// <summary>
        /// Проверяет паттерн, если есть совпадение, возвращает его номер
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public Pattern CheckPatternInPatterns(int Owner)
        {
            var pat = from Dot d in NotEmptyNonBlockedDots
                      where d.Own == Owner
                      select d;

            foreach (Dot d in pat)
            {
                foreach (Pattern p in GameEngine.Patterns)
                {
                    bool flag = true;
                    foreach (DotInPattern dt in p.DotsPattern)
                    {

                        int enemy_own = Owner == 1 ? 2 : 1;
                        int DotOwner = 0;
                        switch (dt.Owner)
                        {
                            case "enemy":
                                DotOwner = enemy_own;
                                break;
                            case "0":
                                DotOwner = 0;
                                break;
                            case "owner":
                                DotOwner = Owner;
                                break;
                            case "!=enemy":
                                DotOwner = Owner | 0;
                                break;

                            default:
                                DotOwner = Owner;
                                break;
                        }

                        //if (d.x < p.Xmin | d.x > p.Xmax |  //если заданы границы, проверяем
                        //    d.y < p.Ymin | d.y > p.Ymax |  //
                        //    this[d.x + dt.dX, d.y + dt.dY].Own != DotOwner) 
                        if (this[d.x + dt.dX, d.y + dt.dY].Own != DotOwner)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        p.Dot = d;
                        return p;
                    }
                }
            }
            return null;
        }
        private int count_in_region;
        private int count_blocked_dots;
        //=================================================================================================
        /// <summary>
        /// Функция делает ход игрока 
        /// </summary>
        /// <param name="dot">точка куда делается ход</param>
        /// <param name="Owner">владелец точки -целое 1-Игрок или 2 -Компьютер</param>
        /// <returns>количество окруженных точек</returns>
        public int MakeMove(Dot dot, int Owner = 0)//
        {
            //if (arrDots.Contains(dot) == false) return 0;
            if (this[dot.x, dot.y].Own == 0)//если точка не занята
            {
                if (Owner == 0) this.Add(dot, dot.Own);
                else this.Add(dot, Owner);
            }
            //--------------------------------
            int res = CheckBlocked(dot.Own);
            //--------------------------------
            var q = from Dot d in Dots where d.Blocked select d;
            count_blocked_dots = q.Count();
            last_move = dot;//зафиксировать последний ход
            if (res != 0)
            {
                LinkDots();
            }
            ListMoves.Add(dot);
            return res;
        }
        /// <summary>
        /// проверяет блокировку точек, маркирует точки которые блокируют, возвращает количество окруженных точек
        /// </summary>
        /// <param name="arrDots"></param>
        /// <param name="last_moveOwner"></param>
        /// <returns>количество окруженных точек</returns>
        private int CheckBlocked(int last_moveOwner = 0)
        {
            int counter = 0;
            var checkdots = from Dot dots in this
                            where dots.Own != 0 | dots.Own == 0 & dots.Blocked
                            orderby dots.Own == last_moveOwner
                            select dots;
            lst_blocked_dots.Clear(); lst_in_region_dots.Clear();
            foreach (Dot d in checkdots)
            {
                UnmarkAllDots();
                if (DotIsFree(d, d.Own) == false)
                //if (DotIsFree(d, arrDots) == false)
                {
                    if (d.Own != 0) d.Blocked = true;
                    d.IndexRelation = 0;
                    var q1 = from Dot dots in this where dots.BlokingDots.Contains(d) select dots;
                    if (q1.Count() == 0)
                    {
                        UnmarkAllDots();
                        MarkDotsInRegion(d, d.Own);

                        foreach (Dot dr in lst_in_region_dots)
                        {
                            win_player = dr.Own;
                            count_in_region++;
                            foreach (Dot bd in lst_blocked_dots)
                            {
                                if (bd.Own != 0) counter += 1;
                                if (dr.BlokingDots.Contains(bd) == false & bd.Own != 0 & dr.Own != bd.Own)
                                {
                                    dr.BlokingDots.Add(bd);
                                }
                            }
                        }
                    }
                }
                else
                {
                    d.Blocked = false;
                }
            }
            RescanBlockedDots();

            if (lst_blocked_dots.Count == 0) win_player = 0;
            return lst_blocked_dots.Count;
        }
        private List<Dot> lst_blocked_dots = new List<Dot>();//список блокированных точек
        private List<Dot> lst_in_region_dots = new List<Dot>();//список блокирующих точек
        /// <summary>
        /// Определяет блокирующие точки и устанавливает этим точкам поле InRegion=true 
        /// </summary>
        /// <param name="blocked_dot">точка, которая блокируется</param>
        /// <param name="flg_own">владелец точки</param>
        private void MarkDotsInRegion(Dot blocked_dot, int flg_own)
        {
            blocked_dot.Marked = true;
            Dot[] dts = new Dot[4] {
                                    this[blocked_dot.x + 1, blocked_dot.y],
                                    this[blocked_dot.x -1, blocked_dot.y],
                                    this[blocked_dot.x, blocked_dot.y + 1],
                                    this[blocked_dot.x, blocked_dot.y -1],
                                    };
            //добавим точки которые попали в окружение
            if (lst_blocked_dots.Contains(blocked_dot) == false)
            {
                lst_blocked_dots.Add(blocked_dot);
            }
            foreach (Dot _d in dts)
            //foreach (Dot _d in this.SetNeiborDotsSNWE(blocked_dot))
            {
                if (_d.Own != 0 & _d.Blocked == false & _d.Own != flg_own)//_d-точка которая окружает
                {
                    //добавим в коллекцию точки которые окружают
                    if (lst_in_region_dots.Contains(_d) == false) lst_in_region_dots.Add(_d);
                }
                else
                {
                    if (_d.Marked == false & _d.Fixed == false)
                    {
                        _d.Blocked = true;
                        MarkDotsInRegion(_d, flg_own);
                    }
                }
            }
        }
        private void MarkDotsInRegion_old(Dot blocked_dot, int flg_own)
        {
            blocked_dot.Marked = true;
            Dot[] dts = new Dot[4] {
                                    this[blocked_dot.x + 1, blocked_dot.y],
                                    this[blocked_dot.x -1, blocked_dot.y],
                                    this[blocked_dot.x, blocked_dot.y + 1],
                                    this[blocked_dot.x, blocked_dot.y -1],
                                    };
            //добавим точки которые попали в окружение
            if (lst_blocked_dots.Contains(blocked_dot) == false)
            {
                lst_blocked_dots.Add(blocked_dot);
            }
            foreach (Dot _d in dts)
            //foreach (Dot _d in this.SetNeiborDotsSNWE(blocked_dot))
            {
                if (_d.Own != 0 & _d.Blocked == false & _d.Own != flg_own)//_d-точка которая окружает
                {
                    //добавим в коллекцию точки которые окружают
                    if (lst_in_region_dots.Contains(_d) == false) lst_in_region_dots.Add(_d);
                }
                else
                {
                    if (_d.Marked == false & _d.Fixed == false)
                    {
                        _d.Blocked = true;
                        MarkDotsInRegion(_d, flg_own);
                    }
                }
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void MakeRating()//возвращает массив вражеских точек вокруг заданной точки
        {
            int res;
            var qd = from Dot dt in this where dt.Own != 0 & dt.Blocked == false select dt;
            foreach (Dot dot in qd)
            {
                //if (dot.x > 0 & dot.y > 0 & dot.x < iMapSize -1 & dot.y < iMapSize -1)
                if (dot.x > 0 & dot.y > 0 & dot.x < BoardWidth - 1 & dot.y < BoardHeight - 1)
                {
                    Dot[] dts = new Dot[4] {this[dot.x + 1, dot.y], 
                                            this[dot.x -1, dot.y],
                                            this[dot.x, dot.y + 1],
                                            this[dot.x, dot.y -1]};
                    res = 0;
                    foreach (Dot item in dts)
                    {
                        if (item.Own != 0 & item.Own != dot.Own) res++;
                        else if (item.Own == dot.Own & item.Rating == 0)
                        {
                            res = -1;
                            break;
                        }
                    }
                    dot.Rating = res + 1;//точка без связей получает рейт 1
                }
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void ScanBlockedFreeDots()//сканирует не занятые узлы на предмет блокировки
        {
            var q = from Dot d in this where d.Own == 0 && d.Blocked == false select d;
            if (q.Count() == 0) return;
            foreach (Dot dot in q)
            {
                Dot[] dts = new Dot[4] {this[dot.x + 1, dot.y], this[dot.x -1, dot.y],
                                        this[dot.x, dot.y + 1], this[dot.x, dot.y -1]};
                foreach (Dot neibour_dot in dts)
                {
                    if (neibour_dot.Blocked)
                    {
                        dot.Blocked = true;
                        ScanBlockedFreeDots();
                        break;
                    }
                }
            }

        }


        private List<Links> lnks;

        public List<Links> ListLinks 
        { 
            get {return lnks;}
        }
        
        public void LinkDots()//устанавливает связь между двумя точками и возвращает массив связей 
        {
            lnks.Clear();
            var qry = from Dot d1 in this
                      where d1.BlokingDots.Count > 0 
                      from Dot d2 in this
                      where d2.Own == d1.Own && d1.Blocked == d2.Blocked && d2.BlokingDots.Count > 0 
                      & Distance(d1, d2) < 2 & Distance(d1, d2) > 0
                      select new Links(d1, d2);

            var temp = qry.Distinct(new LinksComparer());

            lnks = temp.ToList(); //обновляем основной массив связей - lnks              
        }
        /// <summary>
        /// функция проверяет не делается ли ход в точку, которая на следующем ходу будет окружена
        /// </summary>
        /// <param name="dot"></param>
        /// <param name="arrDots"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public bool CheckDot(Dot dot, int Player)
        {
            //ArrayDots this = arrDots.CopyArray;
            int res = MakeMove(dot, Player);
            int pl = Player == 2 ? 1 : 2;
            //if (win_player==pl || CheckMove(pl) != null) // первое условие -ход в уже оеруженный регион, 
            if (win_player == pl)
            {
                UndoMove(dot);
                return true; // да будет окружена
            }
            //будет ли окружена на следующем ходу
            Dot dotEnemy = CheckMove(pl);
            if (dotEnemy != null)
            {
                res = MakeMove(dotEnemy, pl);
                //bool flag = dot.Blocked;
                UndoMove(dotEnemy);
                UndoMove(dot);
                //return flag; // да будет окружена
                return true; // да будет окружена
            }
            //нет не будет
            UndoMove(dot);
            return false;
        }
        //public Dot CheckPattern2Move(int pl2)
        //{
        //    //List<Dot> empty_dots = EmptyNeibourDots(pl2);
        //    //List<Dot> lst_dots2;

        //    //int c = 0;//временно для ограничения рассчетов, чтоб долго не ждать. !Удалить после отладки!

        //    foreach (Dot dot in empty_dots)
        //    {
        //        c++;
        //        if (c > 20) return null;

        //        if (CheckDot(dot,pl2) == false) MakeMove(dot, pl2);
        //        if (CheckDot(dot, pl2) == false) MakeMove(dot, pl2);
        //        //lst_dots2 = CheckPattern2Move(pl2);
        //        foreach (Dot nd in lst_dots2)
        //        {
        //            if (MakeMove(nd, pl2) != 0)
        //            {
        //                UndoMove(nd);
        //                UndoMove(dot);
        //                return dot;
        //            }
        //            UndoMove(nd);
        //        }
        //        UndoMove(dot);
        //    }
        //    return null;
        //}
        //===============================================================================================================
        public List<Dot> CheckRelation(int index)
        {
            List<Dot> lstDots = new List<Dot>();
            Dot d1, d2;
            var q = from Dot dot in this
                    where dot.IndexDot == index & dot.NeiborDots.Count == 1
                    select dot;

            if (q.Count() == 2)
            {
                d1 = q.First();
                d2 = q.Last();
                var qry = from Dot dot in this
                          where dot.Own == 0 & this.Distance(dot, d1) < 2 & this.Distance(dot, d2) < 2
                          select dot;
                return qry.ToList();
            }
            return null;
            //return lstDots;
        }
        //==============================================================================================
        //проверяет ход в результате которого окружение.Возвращает ход который завершает окружение
        //private Dot CheckMove(int Owner, ArrayDots this, bool AllBoard = false)
        public Dot CheckMove(int Owner)
        {
            List<Dot> happy_dots = new List<Dot>();
            var qry = FreeDots.Where(
            #region Query patterns Check
d =>
                                //       + 
                                //    d  
                                //       +
                          this[d.x + 1, d.y - 1].Blocked == false & this[d.x + 1, d.y + 1].Blocked == false &
                          this[d.x + 1, d.y - 1].Own == Owner & this[d.x + 1, d.y + 1].Own == Owner &
                          this[d.x + 1, d.y].Own != Owner
                                //+        
                                //   d     
                                //+       
                          | this[d.x - 1, d.y - 1].Blocked == false & this[d.x - 1, d.y + 1].Blocked == false &
                          this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y + 1].Own == Owner &
                          this[d.x - 1, d.y].Own != Owner
                                //      
                                //   d          
                                //+     +
                        | this[d.x + 1, d.y + 1].Blocked == false & this[d.x - 1, d.y + 1].Blocked == false &
                        this[d.x + 1, d.y + 1].Own == Owner & this[d.x - 1, d.y + 1].Own == Owner &
                        this[d.x, d.y + 1].Own != Owner
                                //+     + 
                                //   d          
                                //     
                        | this[d.x - 1, d.y - 1].Blocked == false & this[d.x + 1, d.y - 1].Blocked == false &
                         this[d.x - 1, d.y - 1].Own == Owner & this[d.x + 1, d.y - 1].Own == Owner &
                         this[d.x, d.y - 1].Own != Owner

                         //    +    
                                //    d   
                                //    +   
                          | this[d.x, d.y + 1].Blocked == false & this[d.x, d.y - 1].Blocked == false &
                              this[d.x, d.y - 1].Own == Owner & this[d.x, d.y + 1].Own == Owner &
                              this[d.x + 1, d.y].Own != Owner &
                              this[d.x - 1, d.y].Own != Owner
                                //        
                                //+   d   +  
                                //       
                          | this[d.x - 1, d.y].Blocked == false & this[d.x + 1, d.y].Blocked == false &
                            this[d.x - 1, d.y].Own == Owner & this[d.x + 1, d.y].Own == Owner &
                            this[d.x, d.y + 1].Own != Owner &
                            this[d.x, d.y - 1].Own != Owner

                              //+        
                                //   d     
                                //      +   
                          | this[d.x - 1, d.y - 1].Blocked == false & this[d.x + 1, d.y + 1].Blocked == false &
                          this[d.x - 1, d.y - 1].Own == Owner & this[d.x + 1, d.y + 1].Own == Owner &
                          this[d.x, d.y - 1].Own != Owner &
                          this[d.x, d.y + 1].Own != Owner &
                          this[d.x + 1, d.y].Own != Owner &
                          this[d.x - 1, d.y].Own != Owner
                                //      +  
                                //   d     
                                //+        
                          | this[d.x - 1, d.y + 1].Blocked == false & this[d.x + 1, d.y - 1].Blocked == false &
                          this[d.x - 1, d.y + 1].Own == Owner & this[d.x + 1, d.y - 1].Own == Owner &
                          this[d.x, d.y - 1].Own != Owner &
                          this[d.x, d.y + 1].Own != Owner &
                          this[d.x + 1, d.y].Own != Owner &
                          this[d.x - 1, d.y].Own != Owner

                               //      +
                                //+  d
                        | this[d.x - 1, d.y].Blocked == false & this[d.x + 1, d.y - 1].Blocked == false &
                          this[d.x - 1, d.y].Own == Owner & this[d.x + 1, d.y - 1].Own == Owner &
                         this[d.x, d.y - 1].Own != Owner &
                         this[d.x + 1, d.y].Own != Owner &
                         this[d.x - 1, d.y - 1].Own != Owner &
                         this[d.x + 1, d.y + 1].Own != Owner &
                          this[d.x, d.y + 1].Own != Owner

                                //+  d
                                //      +
                        | this[d.x - 1, d.y].Blocked == false & this[d.x + 1, d.y + 1].Blocked == false &
                          this[d.x - 1, d.y].Own == Owner & this[d.x + 1, d.y + 1].Own == Owner &
                          this[d.x, d.y + 1].Own != Owner &
                         this[d.x + 1, d.y].Own != Owner &
                         this[d.x - 1, d.y + 1].Own != Owner &
                          this[d.x + 1, d.y - 1].Own != Owner &
                          this[d.x, d.y - 1].Own != Owner

                              //+
                                //   d  +       
                        | this[d.x + 1, d.y].Blocked == false & this[d.x - 1, d.y - 1].Blocked == false &
                         this[d.x + 1, d.y].Own == Owner & this[d.x - 1, d.y - 1].Own == Owner &
                         this[d.x, d.y - 1].Own != Owner &
                         this[d.x - 1, d.y + 1].Own != Owner &
                         this[d.x - 1, d.y].Own != Owner &
                         this[d.x + 1, d.y - 1].Own != Owner &
                          this[d.x, d.y + 1].Own != Owner

                               //   d  +       
                                //+
                        | this[d.x + 1, d.y].Blocked == false & this[d.x - 1, d.y + 1].Blocked == false &
                         this[d.x + 1, d.y].Own == Owner & this[d.x - 1, d.y + 1].Own == Owner &
                         this[d.x, d.y + 1].Own != Owner &
                         this[d.x + 1, d.y + 1].Own != Owner &
                         this[d.x - 1, d.y].Own != Owner &
                         this[d.x - 1, d.y - 1].Own != Owner &
                          this[d.x, d.y - 1].Own != Owner

                               //+   
                                //   d          
                                //   +
                        | this[d.x, d.y + 1].Blocked == false & this[d.x - 1, d.y - 1].Blocked == false &
                         this[d.x, d.y + 1].Own == Owner & this[d.x - 1, d.y - 1].Own == Owner &
                         this[d.x - 1, d.y].Own != Owner &
                         this[d.x - 1, d.y + 1].Own != Owner &
                         this[d.x, d.y - 1].Own != Owner &
                         this[d.x + 1, d.y - 1].Own != Owner &
                          this[d.x + 1, d.y].Own != Owner

                               //   +
                                //   d          
                                //+   
                        | this[d.x, d.y - 1].Blocked == false & this[d.x - 1, d.y + 1].Blocked == false &
                        this[d.x, d.y - 1].Own == Owner & this[d.x - 1, d.y + 1].Own == Owner &
                        this[d.x - 1, d.y].Own != Owner &
                        this[d.x - 1, d.y - 1].Own != Owner &
                        this[d.x + 1, d.y].Own != Owner &
                        this[d.x + 1, d.y + 1].Own != Owner &
                        this[d.x, d.y + 1].Own != Owner

                               //   +
                                //   d          
                                //      +
                        | this[d.x, d.y - 1].Blocked == false & this[d.x + 1, d.y + 1].Blocked == false &
                        this[d.x, d.y - 1].Own == Owner & this[d.x + 1, d.y + 1].Own == Owner &
                        this[d.x + 1, d.y].Own != Owner &
                        this[d.x, d.y + 1].Own != Owner &
                        this[d.x - 1, d.y + 1].Own != Owner &
                        this[d.x - 1, d.y].Own != Owner &
                        this[d.x + 1, d.y - 1].Own != Owner

                                //      +
                                //   d          
                                //   +   
                        | this[d.x, d.y + 1].Blocked == false & this[d.x + 1, d.y - 1].Blocked == false &
                        this[d.x, d.y + 1].Own == Owner & this[d.x + 1, d.y - 1].Own == Owner &
                        this[d.x - 1, d.y - 1].Own != Owner &
                        this[d.x, d.y - 1].Own != Owner &
                        this[d.x + 1, d.y].Own != Owner &
                        this[d.x + 1, d.y + 1].Own != Owner &
                        this[d.x - 1, d.y].Own != Owner
            #endregion
);
#if DEBUG
            Dot[] ad = qry.ToArray();
#endif
            foreach (Dot d in qry)
            {
                //делаем ход
                int result_last_move = MakeMove(d, Owner);
#if DEBUG
                //if (f.chkMove.Checked) Pause();
#endif
                //-----------------------------------
                if (result_last_move != 0 & this[d.x, d.y].Blocked == false)
                {
                    UndoMove(d);
                    d.CountBlockedDots = result_last_move;
                    happy_dots.Add(d);
                    //return d;
                }
                UndoMove(d);
            }
            var x = happy_dots.Where(dd => dd.CountBlockedDots == happy_dots.Max(dt => dt.CountBlockedDots));

            return x.Count() > 0 ? x.First() : null;

            //return null;
        }
        public Dot CheckPatternVilkaNextMove(int Owner)
        {
            var qry = NotEmptyNonBlockedDots.Where(dt => dt.Own == Owner);
            Dot dot_ptn;
            if (qry.Count() != 0)
            {
                foreach (Dot d in qry)
                {
                    foreach (Dot dot_move in NeiborDotsSNWE(d))
                    {
                        if (dot_move.Blocked == false & dot_move.Own == 0)
                        {
                            //делаем ход
                            int result_last_move = MakeMove(dot_move, Owner);
                            int pl = Owner == 2 ? 1 : 2;
                            Dot dt = CheckMove(pl); // проверка чтобы не попасть в капкан
                            if (dt != null)
                            {
                                UndoMove(dot_move);
                                continue;
                            }
                            dot_ptn = CheckPattern_vilochka(d.Own);
#if DEBUG
                            //if (f.chkMove.Checked) Pause();
#endif
                            //-----------------------------------
                            if (dot_ptn != null & result_last_move == 0)
                            {
                                UndoMove(dot_move);
                                return dot_move;
                                //return dot_ptn;
                            }
                            UndoMove(dot_move);
                        }
                    }
                }
            }
            return null;
        }
        public int iNumberPattern;

        public Dot CheckPattern_vilochka(int Owner)
        {
            int enemy_own = Owner == 1 ? 2 : 1;
            //ArrayDots this = aDots.CopyArrayDots;

            var get_non_blocked = from Dot d in this where d.Blocked == false select d; //получить коллекцию незаблокированных точек

            //паттерн на диагональное расположение точек         *d   
            //                                                *  +  
            //                                             *  +  *               
            iNumberPattern = 1;

            var pat1 = from Dot d in get_non_blocked
                       where d.Own == Owner
                           & this[d.x - 1, d.y + 1].Own == Owner & this[d.x - 1, d.y + 1].Blocked == false
                           & this[d.x + 1, d.y - 1].Own == Owner & this[d.x + 1, d.y - 1].Blocked == false
                           & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                           & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                           & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                           & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                           & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                           & this[d.x + 2, d.y + 1].Own != enemy_own & this[d.x + 2, d.y + 1].Blocked == false
                           & this[d.x + 1, d.y + 2].Own != enemy_own & this[d.x + 1, d.y + 2].Blocked == false
                       select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x + 1, pat1.First().y + 1);
            //паттерн на диагональное расположение точек    *  +  *d   
            //                                              +  *    
            //                                              *       

            var pat1_2 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x + 1, d.y - 1].Own == Owner & this[d.x + 1, d.y - 1].Blocked == false
                             & this[d.x - 1, d.y + 1].Own == Owner & this[d.x - 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                             & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                             & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                             & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                             & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                             & this[d.x - 2, d.y - 1].Own != enemy_own & this[d.x - 2, d.y - 1].Blocked == false
                             & this[d.x - 1, d.y - 2].Own != enemy_own & this[d.x - 1, d.y - 2].Blocked == false
                         select d;
            if (pat1_2.Count() > 0) return new Dot(pat1_2.First().x - 1, pat1_2.First().y - 1);

            //============================================================================================================== 
            //паттерн на диагональное расположение точек    *                   
            //                                              +  d    
            //                                              m  +  * 
            var pat1_3 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                             & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                             & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                             & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                             & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                             //& this[d.x + 2, d.y + 1].Own != enemy_own & this[d.x + 2, d.y + 1].Blocked == false
                             //& this[d.x + 1, d.y].Own != enemy_own & this[d.x + 1, d.y].Blocked == false
                             //& this[d.x, d.y -1].Own != enemy_own & this[d.x, d.y -1].Blocked == false
                             //& this[d.x -1, d.y -2].Own != enemy_own & this[d.x -1, d.y -2].Blocked == false
                             & this[d.x - 1, d.y + 2].Own != enemy_own & this[d.x - 1, d.y + 2].Blocked == false
                             & this[d.x - 2, d.y + 1].Own != enemy_own & this[d.x - 2, d.y + 1].Blocked == false
                         select d;
            if (pat1_3.Count() > 0) return new Dot(pat1_3.First().x - 1, pat1_3.First().y + 1);
            //180 Rotate=========================================================================================================== 
            //паттерн на диагональное расположение точек    *  +  m   
            //                                                 d  +  
            //                                                    * 
            var pat1_4 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                             & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                             & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                             & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                             & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                             //& this[d.x -2, d.y -1].Own != enemy_own & this[d.x -2, d.y -1].Blocked == false
                             //& this[d.x -1, d.y].Own != enemy_own & this[d.x -1, d.y].Blocked == false
                             //& this[d.x, d.y + 1].Own != enemy_own & this[d.x, d.y + 1].Blocked == false
                             //& this[d.x + 1, d.y + 2].Own != enemy_own & this[d.x + 1, d.y + 2].Blocked == false
                             & this[d.x + 1, d.y - 2].Own != enemy_own & this[d.x + 1, d.y - 2].Blocked == false
                             & this[d.x + 2, d.y - 1].Own != enemy_own & this[d.x + 2, d.y - 1].Blocked == false
                         select d;
            if (pat1_4.Count() > 0) return new Dot(pat1_4.First().x + 1, pat1_4.First().y - 1);
            //**********************************************************************************        

            //     *     *
            //  *  +  *  +   
            //  *        m
            //     * 
            iNumberPattern = 938;
            var pat938 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x + 1, d.y - 1].Own == Owner & this[d.x + 1, d.y - 1].Blocked == false
                             & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                             & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                             & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y].Own == Owner & this[d.x - 1, d.y].Blocked == false
                             & this[d.x - 2, d.y + 1].Own == Owner & this[d.x - 2, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y + 2].Own == Owner & this[d.x - 1, d.y + 2].Blocked == false
                             & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x + 1, d.y + 2].Own == 0 & this[d.x + 1, d.y + 2].Blocked == false
                             & this[d.x + 2, d.y + 1].Own == 0 & this[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat938.Count() > 0) return new Dot(pat938.First().x + 1, pat938.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat938_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x - 1, d.y + 1].Own == Owner & this[d.x - 1, d.y + 1].Blocked == false
                               & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                               & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                               & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y].Own == Owner & this[d.x + 1, d.y].Blocked == false
                               & this[d.x + 2, d.y - 1].Own == Owner & this[d.x + 2, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y - 2].Own == Owner & this[d.x + 1, d.y - 2].Blocked == false
                               & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                               & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                               & this[d.x - 1, d.y - 2].Own == 0 & this[d.x - 1, d.y - 2].Blocked == false
                               & this[d.x - 2, d.y - 1].Own == 0 & this[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat938_2.Count() > 0) return new Dot(pat938_2.First().x - 1, pat938_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat938_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x + 1, d.y - 1].Own == Owner & this[d.x + 1, d.y - 1].Blocked == false
                                 & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                 & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                 & this[d.x, d.y + 1].Own == Owner & this[d.x, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y + 2].Own == Owner & this[d.x - 1, d.y + 2].Blocked == false
                                 & this[d.x - 2, d.y + 1].Own == Owner & this[d.x - 2, d.y + 1].Blocked == false
                                 & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                                 & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                                 & this[d.x - 2, d.y - 1].Own == 0 & this[d.x - 2, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y - 2].Own == 0 & this[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat938_2_3.Count() > 0) return new Dot(pat938_2_3.First().x - 1, pat938_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat938_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x - 1, d.y + 1].Own == Owner & this[d.x - 1, d.y + 1].Blocked == false
                                   & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                   & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                                   & this[d.x, d.y - 1].Own == Owner & this[d.x, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y - 2].Own == Owner & this[d.x + 1, d.y - 2].Blocked == false
                                   & this[d.x + 2, d.y - 1].Own == Owner & this[d.x + 2, d.y - 1].Blocked == false
                                   & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                                   & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                                   & this[d.x + 2, d.y + 1].Own == 0 & this[d.x + 2, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y + 2].Own == 0 & this[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat938_2_3_4.Count() > 0) return new Dot(pat938_2_3_4.First().x + 1, pat938_2_3_4.First().y + 1);
            //============================================================================================================== 

            //===========ВИЛОЧКА=================================================================================================== 
            //     *   
            //     +  
            //  +  *
            //  *
            iNumberPattern = 670;
            var pat670 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x, d.y + 2].Own == Owner
                             & this[d.x, d.y + 1].Own == enemy_own
                             & this[d.x + 1, d.y - 1].Own == Owner
                             & this[d.x + 1, d.y].Own == enemy_own
                             & this[d.x + 1, d.y + 1].Own == 0
                             & this[d.x + 2, d.y].Own == 0
                             & this[d.x - 1, d.y + 1].Own == 0
                             & this[d.x + 2, d.y - 1].Own != enemy_own
                             & this[d.x, d.y - 1].Own != enemy_own
                             & this[d.x + 2, d.y + 1].Own != enemy_own
                             & this[d.x + 1, d.y + 2].Own != enemy_own
                             & this[d.x - 1, d.y].Own != enemy_own
                         select d;
            if (pat670.Count() > 0) return new Dot(pat670.First().x + 1, pat670.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat670_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x, d.y - 2].Own == Owner
                               & this[d.x, d.y - 1].Own == enemy_own
                               & this[d.x - 1, d.y + 1].Own == Owner
                               & this[d.x - 1, d.y].Own == enemy_own
                               & this[d.x - 1, d.y - 1].Own == 0
                               & this[d.x - 2, d.y].Own == 0
                               & this[d.x + 1, d.y - 1].Own == 0
                               & this[d.x - 2, d.y + 1].Own != enemy_own
                               & this[d.x, d.y + 1].Own != enemy_own
                               & this[d.x - 2, d.y - 1].Own != enemy_own
                               & this[d.x - 1, d.y - 2].Own != enemy_own
                               & this[d.x + 1, d.y].Own != enemy_own
                           select d;
            if (pat670_2.Count() > 0) return new Dot(pat670_2.First().x - 1, pat670_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat670_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x - 2, d.y].Own == Owner
                                 & this[d.x - 1, d.y].Own == enemy_own
                                 & this[d.x + 1, d.y - 1].Own == Owner
                                 & this[d.x, d.y - 1].Own == enemy_own
                                 & this[d.x - 1, d.y - 1].Own == 0
                                 & this[d.x, d.y - 2].Own == 0
                                 & this[d.x - 1, d.y + 1].Own == 0
                                 & this[d.x + 1, d.y - 2].Own != enemy_own
                                 & this[d.x + 1, d.y].Own != enemy_own
                                 & this[d.x - 1, d.y - 2].Own != enemy_own
                                 & this[d.x - 2, d.y - 1].Own != enemy_own
                                 & this[d.x, d.y + 1].Own != enemy_own
                             select d;
            if (pat670_2_3.Count() > 0) return new Dot(pat670_2_3.First().x - 1, pat670_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat670_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x + 2, d.y].Own == Owner
                                   & this[d.x + 1, d.y].Own == enemy_own
                                   & this[d.x - 1, d.y + 1].Own == Owner
                                   & this[d.x, d.y + 1].Own == enemy_own
                                   & this[d.x + 1, d.y + 1].Own == 0
                                   & this[d.x, d.y + 2].Own == 0
                                   & this[d.x + 1, d.y - 1].Own == 0
                                   & this[d.x - 1, d.y + 2].Own != enemy_own
                                   & this[d.x - 1, d.y].Own != enemy_own
                                   & this[d.x + 1, d.y + 2].Own != enemy_own
                                   & this[d.x + 2, d.y + 1].Own != enemy_own
                                   & this[d.x, d.y - 1].Own != enemy_own
                               select d;
            if (pat670_2_3_4.Count() > 0) return new Dot(pat670_2_3_4.First().x + 1, pat670_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 30;
            var pat30 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & this[d.x - 1, d.y + 1].Own == Owner
                            & this[d.x - 1, d.y].Own == enemy_own
                            & this[d.x, d.y - 1].Own == enemy_own
                            & this[d.x, d.y - 2].Own == Owner
                            & this[d.x + 1, d.y - 1].Own == 0
                            & this[d.x - 1, d.y - 1].Own == 0
                            & this[d.x - 1, d.y - 2].Own == 0
                            & this[d.x - 2, d.y - 1].Own == 0
                            & this[d.x - 2, d.y].Own == 0
                        select d;
            if (pat30.Count() > 0) return new Dot(pat30.First().x - 1, pat30.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat30_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & this[d.x + 1, d.y - 1].Own == Owner
                              & this[d.x + 1, d.y].Own == enemy_own
                              & this[d.x, d.y + 1].Own == enemy_own
                              & this[d.x, d.y + 2].Own == Owner
                              & this[d.x - 1, d.y + 1].Own == 0
                              & this[d.x + 1, d.y + 1].Own == 0
                              & this[d.x + 1, d.y + 2].Own == 0
                              & this[d.x + 2, d.y + 1].Own == 0
                              & this[d.x + 2, d.y].Own == 0
                          select d;
            if (pat30_2.Count() > 0) return new Dot(pat30_2.First().x + 1, pat30_2.First().y + 1);
            //--------------Rotate on 90-----------------------------------
            var pat30_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & this[d.x - 1, d.y + 1].Own == Owner
                                & this[d.x, d.y + 1].Own == enemy_own
                                & this[d.x + 1, d.y].Own == enemy_own
                                & this[d.x + 2, d.y].Own == Owner
                                & this[d.x + 1, d.y - 1].Own == 0
                                & this[d.x + 1, d.y + 1].Own == 0
                                & this[d.x + 2, d.y + 1].Own == 0
                                & this[d.x + 1, d.y + 2].Own == 0
                                & this[d.x, d.y + 2].Own == 0
                            select d;
            if (pat30_2_3.Count() > 0) return new Dot(pat30_2_3.First().x + 1, pat30_2_3.First().y + 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat30_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & this[d.x + 1, d.y - 1].Own == Owner
                                  & this[d.x, d.y - 1].Own == enemy_own
                                  & this[d.x - 1, d.y].Own == enemy_own
                                  & this[d.x - 2, d.y].Own == Owner
                                  & this[d.x - 1, d.y + 1].Own == 0
                                  & this[d.x - 1, d.y - 1].Own == 0
                                  & this[d.x - 2, d.y - 1].Own == 0
                                  & this[d.x - 1, d.y - 2].Own == 0
                                  & this[d.x, d.y - 2].Own == 0
                              select d;
            if (pat30_2_3_4.Count() > 0) return new Dot(pat30_2_3_4.First().x - 1, pat30_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 295;
            var pat295 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x - 1, d.y].Own == enemy_own
                             & this[d.x, d.y + 1].Own == enemy_own
                             & this[d.x, d.y + 2].Own == enemy_own
                             & this[d.x + 1, d.y + 3].Own == enemy_own
                             & this[d.x + 2, d.y + 2].Own == enemy_own
                             & this[d.x, d.y - 1].Own == 0
                             & this[d.x + 1, d.y - 1].Own == 0
                             & this[d.x + 1, d.y].Own == 0
                             & this[d.x + 2, d.y].Own == 0
                             & this[d.x + 2, d.y + 1].Own == 0
                             & this[d.x + 1, d.y + 1].Own != enemy_own
                             & this[d.x + 1, d.y + 2].Own != enemy_own
                         select d;
            if (pat295.Count() > 0) return new Dot(pat295.First().x + 1, pat295.First().y);
            //180 Rotate=========================================================================================================== 
            var pat295_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x + 1, d.y].Own == enemy_own
                               & this[d.x, d.y - 1].Own == enemy_own
                               & this[d.x, d.y - 2].Own == enemy_own
                               & this[d.x - 1, d.y - 3].Own == enemy_own
                               & this[d.x - 2, d.y - 2].Own == enemy_own
                               & this[d.x, d.y + 1].Own == 0
                               & this[d.x - 1, d.y + 1].Own == 0
                               & this[d.x - 1, d.y].Own == 0
                               & this[d.x - 2, d.y].Own == 0
                               & this[d.x - 2, d.y - 1].Own == 0
                               & this[d.x - 1, d.y - 1].Own != enemy_own
                               & this[d.x - 1, d.y - 2].Own != enemy_own
                           select d;
            if (pat295_2.Count() > 0) return new Dot(pat295_2.First().x - 1, pat295_2.First().y);
            //--------------Rotate on 90-----------------------------------
            var pat295_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x, d.y + 1].Own == enemy_own
                                 & this[d.x - 1, d.y].Own == enemy_own
                                 & this[d.x - 2, d.y].Own == enemy_own
                                 & this[d.x - 3, d.y - 1].Own == enemy_own
                                 & this[d.x - 2, d.y - 2].Own == enemy_own
                                 & this[d.x + 1, d.y].Own == 0
                                 & this[d.x + 1, d.y - 1].Own == 0
                                 & this[d.x, d.y - 1].Own == 0
                                 & this[d.x, d.y - 2].Own == 0
                                 & this[d.x - 1, d.y - 2].Own == 0
                                 & this[d.x - 1, d.y - 1].Own != enemy_own
                                 & this[d.x - 2, d.y - 1].Own != enemy_own
                             select d;
            if (pat295_2_3.Count() > 0) return new Dot(pat295_2_3.First().x, pat295_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat295_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x, d.y - 1].Own == enemy_own
                                   & this[d.x + 1, d.y].Own == enemy_own
                                   & this[d.x + 2, d.y].Own == enemy_own
                                   & this[d.x + 3, d.y + 1].Own == enemy_own
                                   & this[d.x + 2, d.y + 2].Own == enemy_own
                                   & this[d.x - 1, d.y].Own == 0
                                   & this[d.x - 1, d.y + 1].Own == 0
                                   & this[d.x, d.y + 1].Own == 0
                                   & this[d.x, d.y + 2].Own == 0
                                   & this[d.x + 1, d.y + 2].Own == 0
                                   & this[d.x + 1, d.y + 1].Own != enemy_own
                                   & this[d.x + 2, d.y + 1].Own != enemy_own
                               select d;
            if (pat295_2_3_4.Count() > 0) return new Dot(pat295_2_3_4.First().x, pat295_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 968;
            var pat968 = from Dot d in get_non_blocked
                         where d.Own == Owner
& this[d.x + 1, d.y - 2].Own == Owner
& this[d.x + 2, d.y - 1].Own == 0
& this[d.x + 2, d.y].Own == 0
& this[d.x + 2, d.y + 1].Own == 0
& this[d.x + 1, d.y + 2].Own == Owner
& this[d.x + 1, d.y + 1].Own == enemy_own
& this[d.x, d.y + 1].Own == Owner
& this[d.x, d.y - 1].Own == Owner
& this[d.x + 1, d.y - 1].Own == enemy_own
& this[d.x + 1, d.y].Own == 0
                         select d;
            if (pat968.Count() > 0) return new Dot(pat968.First().x + 1, pat968.First().y);
            //180 Rotate=========================================================================================================== 
            var pat968_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& this[d.x - 1, d.y + 2].Own == Owner
& this[d.x - 2, d.y + 1].Own == 0
& this[d.x - 2, d.y].Own == 0
& this[d.x - 2, d.y - 1].Own == 0
& this[d.x - 1, d.y - 2].Own == Owner
& this[d.x - 1, d.y - 1].Own == enemy_own
& this[d.x, d.y - 1].Own == Owner
& this[d.x, d.y + 1].Own == Owner
& this[d.x - 1, d.y + 1].Own == enemy_own
& this[d.x - 1, d.y].Own == 0
                           select d;
            if (pat968_2.Count() > 0) return new Dot(pat968_2.First().x - 1, pat968_2.First().y);
            //--------------Rotate on 90-----------------------------------
            var pat968_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& this[d.x + 2, d.y - 1].Own == Owner
& this[d.x + 1, d.y - 2].Own == 0
& this[d.x, d.y - 2].Own == 0
& this[d.x - 1, d.y - 2].Own == 0
& this[d.x - 2, d.y - 1].Own == Owner
& this[d.x - 1, d.y - 1].Own == enemy_own
& this[d.x - 1, d.y].Own == Owner
& this[d.x + 1, d.y].Own == Owner
& this[d.x + 1, d.y - 1].Own == enemy_own
& this[d.x, d.y - 1].Own == 0
                             select d;
            if (pat968_2_3.Count() > 0) return new Dot(pat968_2_3.First().x, pat968_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat968_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& this[d.x - 2, d.y + 1].Own == Owner
& this[d.x - 1, d.y + 2].Own == 0
& this[d.x, d.y + 2].Own == 0
& this[d.x + 1, d.y + 2].Own == 0
& this[d.x + 2, d.y + 1].Own == Owner
& this[d.x + 1, d.y + 1].Own == enemy_own
& this[d.x + 1, d.y].Own == Owner
& this[d.x - 1, d.y].Own == Owner
& this[d.x - 1, d.y + 1].Own == enemy_own
& this[d.x, d.y + 1].Own == 0
                               select d;
            if (pat968_2_3_4.Count() > 0) return new Dot(pat968_2_3_4.First().x, pat968_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 880;
            var pat880 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x + 1, d.y + 2].Own == enemy_own & this[d.x + 1, d.y + 2].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y - 1].Own == enemy_own & this[d.x - 1, d.y - 1].Blocked == false
                             & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                             & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                             & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                             & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                             & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                             & this[d.x + 2, d.y + 1].Own == 0 & this[d.x + 2, d.y + 1].Blocked == false
                             & this[d.x - 2, d.y].Own == enemy_own & this[d.x - 2, d.y].Blocked == false
                             & this[d.x - 2, d.y + 1].Own == enemy_own & this[d.x - 2, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y + 2].Own == enemy_own & this[d.x - 1, d.y + 2].Blocked == false
                             & this[d.x - 1, d.y].Own != enemy_own & this[d.x - 1, d.y].Blocked == false
                             & this[d.x - 1, d.y + 1].Own != enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat880.Count() > 0) return new Dot(pat880.First().x + 1, pat880.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat880_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x - 1, d.y - 2].Own == enemy_own & this[d.x - 1, d.y - 2].Blocked == false
                               & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y + 1].Own == enemy_own & this[d.x + 1, d.y + 1].Blocked == false
                               & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                               & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                               & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                               & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                               & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                               & this[d.x - 2, d.y - 1].Own == 0 & this[d.x - 2, d.y - 1].Blocked == false
                               & this[d.x + 2, d.y].Own == enemy_own & this[d.x + 2, d.y].Blocked == false
                               & this[d.x + 2, d.y - 1].Own == enemy_own & this[d.x + 2, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y - 2].Own == enemy_own & this[d.x + 1, d.y - 2].Blocked == false
                               & this[d.x + 1, d.y].Own != enemy_own & this[d.x + 1, d.y].Blocked == false
                               & this[d.x + 1, d.y - 1].Own != enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat880_2.Count() > 0) return new Dot(pat880_2.First().x - 1, pat880_2.First().y + 1);
            //--------------Rotate on 90-----------------------------------
            var pat880_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x - 2, d.y - 1].Own == enemy_own & this[d.x - 2, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                 & this[d.x + 1, d.y + 1].Own == enemy_own & this[d.x + 1, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                 & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                                 & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                                 & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                 & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y - 2].Own == 0 & this[d.x - 1, d.y - 2].Blocked == false
                                 & this[d.x, d.y + 2].Own == enemy_own & this[d.x, d.y + 2].Blocked == false
                                 & this[d.x - 1, d.y + 2].Own == enemy_own & this[d.x - 1, d.y + 2].Blocked == false
                                 & this[d.x - 2, d.y + 1].Own == enemy_own & this[d.x - 2, d.y + 1].Blocked == false
                                 & this[d.x, d.y + 1].Own != enemy_own & this[d.x, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y + 1].Own != enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat880_2_3.Count() > 0) return new Dot(pat880_2_3.First().x + 1, pat880_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat880_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x + 2, d.y + 1].Own == enemy_own & this[d.x + 2, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                   & this[d.x - 1, d.y - 1].Own == enemy_own & this[d.x - 1, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                                   & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                                   & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                   & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                   & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y + 2].Own == 0 & this[d.x + 1, d.y + 2].Blocked == false
                                   & this[d.x, d.y - 2].Own == enemy_own & this[d.x, d.y - 2].Blocked == false
                                   & this[d.x + 1, d.y - 2].Own == enemy_own & this[d.x + 1, d.y - 2].Blocked == false
                                   & this[d.x + 2, d.y - 1].Own == enemy_own & this[d.x + 2, d.y - 1].Blocked == false
                                   & this[d.x, d.y - 1].Own != enemy_own & this[d.x, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y - 1].Own != enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat880_2_3_4.Count() > 0) return new Dot(pat880_2_3_4.First().x - 1, pat880_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 775;
            var pat775 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x - 2, d.y].Own == Owner & this[d.x - 2, d.y].Blocked == false
                             & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                             & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                             & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                             & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                             & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                             & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                             & this[d.x + 1, d.y].Own != enemy_own & this[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat775.Count() > 0) return new Dot(pat775.First().x - 1, pat775.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat775_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x + 2, d.y].Own == Owner & this[d.x + 2, d.y].Blocked == false
                               & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                               & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                               & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                               & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                               & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                               & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                               & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                               & this[d.x - 1, d.y].Own != enemy_own & this[d.x - 1, d.y].Blocked == false
                           select d;
            if (pat775_2.Count() > 0) return new Dot(pat775_2.First().x + 1, pat775_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat775_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x, d.y + 2].Own == Owner & this[d.x, d.y + 2].Blocked == false
                                 & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                                 & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                 & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                                 & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                 & this[d.x, d.y - 1].Own != enemy_own & this[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat775_2_3.Count() > 0) return new Dot(pat775_2_3.First().x - 1, pat775_2_3.First().y + 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat775_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x, d.y - 2].Own == Owner & this[d.x, d.y - 2].Blocked == false
                                   & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                                   & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                                   & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                                   & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                                   & this[d.x, d.y + 1].Own != enemy_own & this[d.x, d.y + 1].Blocked == false
                               select d;
            if (pat775_2_3_4.Count() > 0) return new Dot(pat775_2_3_4.First().x + 1, pat775_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 685;
            var pat685 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x - 2, d.y + 3].Own == Owner & this[d.x - 2, d.y + 3].Blocked == false
                             & this[d.x - 1, d.y + 2].Own == Owner & this[d.x - 1, d.y + 2].Blocked == false
                             & this[d.x, d.y + 3].Own == Owner & this[d.x, d.y + 3].Blocked == false
                             & this[d.x + 1, d.y + 2].Own == Owner & this[d.x + 1, d.y + 2].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                             & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                             & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                             & this[d.x - 3, d.y + 2].Own == 0 & this[d.x - 3, d.y + 2].Blocked == false
                             & this[d.x - 3, d.y + 1].Own == 0 & this[d.x - 3, d.y + 1].Blocked == false
                             & this[d.x - 2, d.y + 2].Own == enemy_own & this[d.x - 2, d.y + 2].Blocked == false
                             & this[d.x - 1, d.y + 1].Own == enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat685.Count() > 0) return new Dot(pat685.First().x - 2, pat685.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat685_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x + 2, d.y - 3].Own == Owner & this[d.x + 2, d.y - 3].Blocked == false
                               & this[d.x + 1, d.y - 2].Own == Owner & this[d.x + 1, d.y - 2].Blocked == false
                               & this[d.x, d.y - 3].Own == Owner & this[d.x, d.y - 3].Blocked == false
                               & this[d.x - 1, d.y - 2].Own == Owner & this[d.x - 1, d.y - 2].Blocked == false
                               & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                               & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                               & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                               & this[d.x + 3, d.y - 2].Own == 0 & this[d.x + 3, d.y - 2].Blocked == false
                               & this[d.x + 3, d.y - 1].Own == 0 & this[d.x + 3, d.y - 1].Blocked == false
                               & this[d.x + 2, d.y - 2].Own == enemy_own & this[d.x + 2, d.y - 2].Blocked == false
                               & this[d.x + 1, d.y - 1].Own == enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                           select d;
            if (pat685_2.Count() > 0) return new Dot(pat685_2.First().x + 2, pat685_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat685_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x - 3, d.y + 2].Own == Owner & this[d.x - 3, d.y + 2].Blocked == false
                                 & this[d.x - 2, d.y + 1].Own == Owner & this[d.x - 2, d.y + 1].Blocked == false
                                 & this[d.x - 3, d.y].Own == Owner & this[d.x - 3, d.y].Blocked == false
                                 & this[d.x - 2, d.y - 1].Own == Owner & this[d.x - 2, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                 & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                                 & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                 & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                 & this[d.x - 2, d.y + 3].Own == 0 & this[d.x - 2, d.y + 3].Blocked == false
                                 & this[d.x - 1, d.y + 3].Own == 0 & this[d.x - 1, d.y + 3].Blocked == false
                                 & this[d.x - 2, d.y + 2].Own == enemy_own & this[d.x - 2, d.y + 2].Blocked == false
                                 & this[d.x - 1, d.y + 1].Own == enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                                 & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                             select d;
            if (pat685_2_3.Count() > 0) return new Dot(pat685_2_3.First().x - 1, pat685_2_3.First().y + 2);
            //--------------Rotate on 90 -2-----------------------------------
            var pat685_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x + 3, d.y - 2].Own == Owner & this[d.x + 3, d.y - 2].Blocked == false
                                   & this[d.x + 2, d.y - 1].Own == Owner & this[d.x + 2, d.y - 1].Blocked == false
                                   & this[d.x + 3, d.y].Own == Owner & this[d.x + 3, d.y].Blocked == false
                                   & this[d.x + 2, d.y + 1].Own == Owner & this[d.x + 2, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                   & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                                   & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                   & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                                   & this[d.x + 2, d.y - 3].Own == 0 & this[d.x + 2, d.y - 3].Blocked == false
                                   & this[d.x + 1, d.y - 3].Own == 0 & this[d.x + 1, d.y - 3].Blocked == false
                                   & this[d.x + 2, d.y - 2].Own == enemy_own & this[d.x + 2, d.y - 2].Blocked == false
                                   & this[d.x + 1, d.y - 1].Own == enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                                   & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                               select d;
            if (pat685_2_3_4.Count() > 0) return new Dot(pat685_2_3_4.First().x + 1, pat685_2_3_4.First().y - 2);
            //============================================================================================================== 
            iNumberPattern = 632;
            var pat632 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                             & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                             & this[d.x - 2, d.y + 1].Own == enemy_own & this[d.x - 2, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y + 1].Own != enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                             & this[d.x - 2, d.y + 2].Own == enemy_own & this[d.x - 2, d.y + 2].Blocked == false
                             & this[d.x - 1, d.y + 3].Own == enemy_own & this[d.x - 1, d.y + 3].Blocked == false
                             & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                             & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                             & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x - 1, d.y + 2].Own != enemy_own & this[d.x - 1, d.y + 2].Blocked == false
                         select d;
            if (pat632.Count() > 0) return new Dot(pat632.First().x, pat632.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat632_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                               & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                               & this[d.x + 2, d.y - 1].Own == enemy_own & this[d.x + 2, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y - 1].Own != enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                               & this[d.x + 2, d.y - 2].Own == enemy_own & this[d.x + 2, d.y - 2].Blocked == false
                               & this[d.x + 1, d.y - 3].Own == enemy_own & this[d.x + 1, d.y - 3].Blocked == false
                               & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                               & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                               & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                               & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                               & this[d.x + 1, d.y - 2].Own != enemy_own & this[d.x + 1, d.y - 2].Blocked == false
                           select d;
            if (pat632_2.Count() > 0) return new Dot(pat632_2.First().x, pat632_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat632_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                                 & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                 & this[d.x - 1, d.y + 2].Own == enemy_own & this[d.x - 1, d.y + 2].Blocked == false
                                 & this[d.x - 1, d.y + 1].Own != enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                                 & this[d.x - 2, d.y + 2].Own == enemy_own & this[d.x - 2, d.y + 2].Blocked == false
                                 & this[d.x - 3, d.y + 1].Own == enemy_own & this[d.x - 3, d.y + 1].Blocked == false
                                 & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                                 & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                                 & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                                 & this[d.x - 2, d.y + 1].Own != enemy_own & this[d.x - 2, d.y + 1].Blocked == false
                             select d;
            if (pat632_2_3.Count() > 0) return new Dot(pat632_2_3.First().x - 1, pat632_2_3.First().y);
            //--------------Rotate on 90 -2-----------------------------------
            var pat632_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                   & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                   & this[d.x + 1, d.y - 2].Own == enemy_own & this[d.x + 1, d.y - 2].Blocked == false
                                   & this[d.x + 1, d.y - 1].Own != enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                                   & this[d.x + 2, d.y - 2].Own == enemy_own & this[d.x + 2, d.y - 2].Blocked == false
                                   & this[d.x + 3, d.y - 1].Own == enemy_own & this[d.x + 3, d.y - 1].Blocked == false
                                   & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                                   & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                                   & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                                   & this[d.x + 2, d.y - 1].Own != enemy_own & this[d.x + 2, d.y - 1].Blocked == false
                               select d;
            if (pat632_2_3_4.Count() > 0) return new Dot(pat632_2_3_4.First().x + 1, pat632_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 1938;
            var pat1938 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & this[d.x, d.y - 3].Own == Owner & this[d.x, d.y - 3].Blocked == false
                              & this[d.x - 1, d.y - 2].Own == Owner & this[d.x - 1, d.y - 2].Blocked == false
                              & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                              & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                              & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                              & this[d.x, d.y - 2].Own != Owner & this[d.x, d.y - 2].Blocked == false
                              & this[d.x, d.y - 1].Own != Owner & this[d.x, d.y - 1].Blocked == false
                              & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                              & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                              & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                              & this[d.x + 2, d.y + 1].Own == 0 & this[d.x + 2, d.y + 1].Blocked == false
                              & this[d.x + 1, d.y - 3].Own == 0 & this[d.x + 1, d.y - 3].Blocked == false
                              & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                          select d;
            if (pat1938.Count() > 0) return new Dot(pat1938.First().x + 1, pat1938.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat1938_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & this[d.x, d.y + 3].Own == Owner & this[d.x, d.y + 3].Blocked == false
                                & this[d.x + 1, d.y + 2].Own == Owner & this[d.x + 1, d.y + 2].Blocked == false
                                & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                & this[d.x, d.y + 2].Own != Owner & this[d.x, d.y + 2].Blocked == false
                                & this[d.x, d.y + 1].Own != Owner & this[d.x, d.y + 1].Blocked == false
                                & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                                & this[d.x - 2, d.y - 1].Own == 0 & this[d.x - 2, d.y - 1].Blocked == false
                                & this[d.x - 1, d.y + 3].Own == 0 & this[d.x - 1, d.y + 3].Blocked == false
                                & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                            select d;
            if (pat1938_2.Count() > 0) return new Dot(pat1938_2.First().x - 1, pat1938_2.First().y + 1);
            //--------------Rotate on 90-----------------------------------
            var pat1938_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & this[d.x + 3, d.y].Own == Owner & this[d.x + 3, d.y].Blocked == false
                                  & this[d.x + 2, d.y + 1].Own == Owner & this[d.x + 2, d.y + 1].Blocked == false
                                  & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                  & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                  & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                  & this[d.x + 2, d.y].Own != Owner & this[d.x + 2, d.y].Blocked == false
                                  & this[d.x + 1, d.y].Own != Owner & this[d.x + 1, d.y].Blocked == false
                                  & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                                  & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                                  & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                  & this[d.x - 1, d.y - 2].Own == 0 & this[d.x - 1, d.y - 2].Blocked == false
                                  & this[d.x + 3, d.y - 1].Own == 0 & this[d.x + 3, d.y - 1].Blocked == false
                                  & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                              select d;
            if (pat1938_2_3.Count() > 0) return new Dot(pat1938_2_3.First().x + 1, pat1938_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat1938_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    & this[d.x - 3, d.y].Own == Owner & this[d.x - 3, d.y].Blocked == false
                                    & this[d.x - 2, d.y - 1].Own == Owner & this[d.x - 2, d.y - 1].Blocked == false
                                    & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                    & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                    & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                    & this[d.x - 2, d.y].Own != Owner & this[d.x - 2, d.y].Blocked == false
                                    & this[d.x - 1, d.y].Own != Owner & this[d.x - 1, d.y].Blocked == false
                                    & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                                    & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                    & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                    & this[d.x + 1, d.y + 2].Own == 0 & this[d.x + 1, d.y + 2].Blocked == false
                                    & this[d.x - 3, d.y + 1].Own == 0 & this[d.x - 3, d.y + 1].Blocked == false
                                    & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                select d;
            if (pat1938_2_3_4.Count() > 0) return new Dot(pat1938_2_3_4.First().x - 1, pat1938_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 1775;
            var pat1775 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                              & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                              & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                              & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                              & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                              & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                              & this[d.x, d.y - 2].Own == Owner & this[d.x, d.y - 2].Blocked == false
                              & this[d.x - 1, d.y - 2].Own == Owner & this[d.x - 1, d.y - 2].Blocked == false
                              & this[d.x - 2, d.y - 1].Own == Owner & this[d.x - 2, d.y - 1].Blocked == false
                              & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                              & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                          select d;
            if (pat1775.Count() > 0) return new Dot(pat1775.First().x + 1, pat1775.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat1775_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                                & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                                & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                & this[d.x, d.y + 2].Own == Owner & this[d.x, d.y + 2].Blocked == false
                                & this[d.x + 1, d.y + 2].Own == Owner & this[d.x + 1, d.y + 2].Blocked == false
                                & this[d.x + 2, d.y + 1].Own == Owner & this[d.x + 2, d.y + 1].Blocked == false
                                & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                                & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                            select d;
            if (pat1775_2.Count() > 0) return new Dot(pat1775_2.First().x - 1, pat1775_2.First().y + 1);
            //--------------Rotate on 90-----------------------------------
            var pat1775_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & this[d.x - 1, d.y - 1].Own == Owner & this[d.x - 1, d.y - 1].Blocked == false
                                  & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                  & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                  & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                                  & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                                  & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                                  & this[d.x + 2, d.y].Own == Owner & this[d.x + 2, d.y].Blocked == false
                                  & this[d.x + 2, d.y + 1].Own == Owner & this[d.x + 2, d.y + 1].Blocked == false
                                  & this[d.x + 1, d.y + 2].Own == Owner & this[d.x + 1, d.y + 2].Blocked == false
                                  & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                                  & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                              select d;
            if (pat1775_2_3.Count() > 0) return new Dot(pat1775_2_3.First().x + 1, pat1775_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat1775_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    & this[d.x + 1, d.y + 1].Own == Owner & this[d.x + 1, d.y + 1].Blocked == false
                                    & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                    & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                    & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                    & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                    & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                                    & this[d.x - 2, d.y].Own == Owner & this[d.x - 2, d.y].Blocked == false
                                    & this[d.x - 2, d.y - 1].Own == Owner & this[d.x - 2, d.y - 1].Blocked == false
                                    & this[d.x - 1, d.y - 2].Own == Owner & this[d.x - 1, d.y - 2].Blocked == false
                                    & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                                    & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                select d;
            if (pat1775_2_3_4.Count() > 0) return new Dot(pat1775_2_3_4.First().x - 1, pat1775_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 274;
            var pat274 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                             & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                             & this[d.x, d.y - 2].Own == enemy_own & this[d.x, d.y - 2].Blocked == false
                             & this[d.x, d.y - 3].Own == enemy_own & this[d.x, d.y - 3].Blocked == false
                             & this[d.x + 1, d.y - 4].Own == enemy_own & this[d.x + 1, d.y - 4].Blocked == false
                             & this[d.x + 2, d.y - 3].Own == enemy_own & this[d.x + 2, d.y - 3].Blocked == false
                             & this[d.x + 2, d.y - 2].Own == enemy_own & this[d.x + 2, d.y - 2].Blocked == false
                             & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                             & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                             & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                             & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                             & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                             & this[d.x + 1, d.y - 1].Own != enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                             & this[d.x + 1, d.y - 2].Own != enemy_own & this[d.x + 1, d.y - 2].Blocked == false
                             & this[d.x + 1, d.y - 3].Own != enemy_own & this[d.x + 1, d.y - 3].Blocked == false
                         select d;
            if (pat274.Count() > 0) return new Dot(pat274.First().x + 1, pat274.First().y);
            //180 Rotate=========================================================================================================== 
            var pat274_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                               & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                               & this[d.x, d.y + 2].Own == enemy_own & this[d.x, d.y + 2].Blocked == false
                               & this[d.x, d.y + 3].Own == enemy_own & this[d.x, d.y + 3].Blocked == false
                               & this[d.x - 1, d.y + 4].Own == enemy_own & this[d.x - 1, d.y + 4].Blocked == false
                               & this[d.x - 2, d.y + 3].Own == enemy_own & this[d.x - 2, d.y + 3].Blocked == false
                               & this[d.x - 2, d.y + 2].Own == enemy_own & this[d.x - 2, d.y + 2].Blocked == false
                               & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                               & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                               & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                               & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                               & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                               & this[d.x - 1, d.y + 1].Own != enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                               & this[d.x - 1, d.y + 2].Own != enemy_own & this[d.x - 1, d.y + 2].Blocked == false
                               & this[d.x - 1, d.y + 3].Own != enemy_own & this[d.x - 1, d.y + 3].Blocked == false
                           select d;
            if (pat274_2.Count() > 0) return new Dot(pat274_2.First().x - 1, pat274_2.First().y);
            //--------------Rotate on 90-----------------------------------
            var pat274_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                 & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                                 & this[d.x + 2, d.y].Own == enemy_own & this[d.x + 2, d.y].Blocked == false
                                 & this[d.x + 3, d.y].Own == enemy_own & this[d.x + 3, d.y].Blocked == false
                                 & this[d.x + 4, d.y - 1].Own == enemy_own & this[d.x + 4, d.y - 1].Blocked == false
                                 & this[d.x + 3, d.y - 2].Own == enemy_own & this[d.x + 3, d.y - 2].Blocked == false
                                 & this[d.x + 2, d.y - 2].Own == enemy_own & this[d.x + 2, d.y - 2].Blocked == false
                                 & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                                 & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                 & this[d.x, d.y - 1].Own == 0 & this[d.x, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                                 & this[d.x - 1, d.y].Own == 0 & this[d.x - 1, d.y].Blocked == false
                                 & this[d.x + 1, d.y - 1].Own != enemy_own & this[d.x + 1, d.y - 1].Blocked == false
                                 & this[d.x + 2, d.y - 1].Own != enemy_own & this[d.x + 2, d.y - 1].Blocked == false
                                 & this[d.x + 3, d.y - 1].Own != enemy_own & this[d.x + 3, d.y - 1].Blocked == false
                             select d;
            if (pat274_2_3.Count() > 0) return new Dot(pat274_2_3.First().x, pat274_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat274_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                   & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                   & this[d.x - 2, d.y].Own == enemy_own & this[d.x - 2, d.y].Blocked == false
                                   & this[d.x - 3, d.y].Own == enemy_own & this[d.x - 3, d.y].Blocked == false
                                   & this[d.x - 4, d.y + 1].Own == enemy_own & this[d.x - 4, d.y + 1].Blocked == false
                                   & this[d.x - 3, d.y + 2].Own == enemy_own & this[d.x - 3, d.y + 2].Blocked == false
                                   & this[d.x - 2, d.y + 2].Own == enemy_own & this[d.x - 2, d.y + 2].Blocked == false
                                   & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                   & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                   & this[d.x, d.y + 1].Own == 0 & this[d.x, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                                   & this[d.x + 1, d.y].Own == 0 & this[d.x + 1, d.y].Blocked == false
                                   & this[d.x - 1, d.y + 1].Own != enemy_own & this[d.x - 1, d.y + 1].Blocked == false
                                   & this[d.x - 2, d.y + 1].Own != enemy_own & this[d.x - 2, d.y + 1].Blocked == false
                                   & this[d.x - 3, d.y + 1].Own != enemy_own & this[d.x - 3, d.y + 1].Blocked == false
                               select d;
            if (pat274_2_3_4.Count() > 0) return new Dot(pat274_2_3_4.First().x, pat274_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 44;
            var pat44 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & this[d.x - 1, d.y + 2].Own == Owner & this[d.x - 1, d.y + 2].Blocked == false
                            & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                            & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                            & this[d.x + 2, d.y - 1].Own == Owner & this[d.x + 2, d.y - 1].Blocked == false
                            & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                            & this[d.x + 1, d.y + 1].Own == 0 & this[d.x + 1, d.y + 1].Blocked == false
                            & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                            & this[d.x + 2, d.y + 1].Own == 0 & this[d.x + 2, d.y + 1].Blocked == false
                            & this[d.x + 1, d.y + 2].Own == 0 & this[d.x + 1, d.y + 2].Blocked == false
                        select d;
            if (pat44.Count() > 0) return new Dot(pat44.First().x + 1, pat44.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat44_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & this[d.x + 1, d.y - 2].Own == Owner & this[d.x + 1, d.y - 2].Blocked == false
                              & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                              & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                              & this[d.x - 2, d.y + 1].Own == Owner & this[d.x - 2, d.y + 1].Blocked == false
                              & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                              & this[d.x - 1, d.y - 1].Own == 0 & this[d.x - 1, d.y - 1].Blocked == false
                              & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                              & this[d.x - 2, d.y - 1].Own == 0 & this[d.x - 2, d.y - 1].Blocked == false
                              & this[d.x - 1, d.y - 2].Own == 0 & this[d.x - 1, d.y - 2].Blocked == false
                          select d;
            if (pat44_2.Count() > 0) return new Dot(pat44_2.First().x - 1, pat44_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat44_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & this[d.x + 2, d.y + 1].Own == Owner & this[d.x + 2, d.y + 1].Blocked == false
                                & this[d.x + 1, d.y].Own == enemy_own & this[d.x + 1, d.y].Blocked == false
                                & this[d.x, d.y - 1].Own == enemy_own & this[d.x, d.y - 1].Blocked == false
                                & this[d.x - 1, d.y - 2].Own == Owner & this[d.x - 1, d.y - 2].Blocked == false
                                & this[d.x + 2, d.y].Own == 0 & this[d.x + 2, d.y].Blocked == false
                                & this[d.x + 1, d.y - 1].Own == 0 & this[d.x + 1, d.y - 1].Blocked == false
                                & this[d.x, d.y - 2].Own == 0 & this[d.x, d.y - 2].Blocked == false
                                & this[d.x + 1, d.y - 2].Own == 0 & this[d.x + 1, d.y - 2].Blocked == false
                                & this[d.x + 2, d.y - 1].Own == 0 & this[d.x + 2, d.y - 1].Blocked == false
                            select d;
            if (pat44_2_3.Count() > 0) return new Dot(pat44_2_3.First().x + 1, pat44_2_3.First().y - 1);
            //--------------Rotate on 90 -2-----------------------------------
            var pat44_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & this[d.x - 2, d.y - 1].Own == Owner & this[d.x - 2, d.y - 1].Blocked == false
                                  & this[d.x - 1, d.y].Own == enemy_own & this[d.x - 1, d.y].Blocked == false
                                  & this[d.x, d.y + 1].Own == enemy_own & this[d.x, d.y + 1].Blocked == false
                                  & this[d.x + 1, d.y + 2].Own == Owner & this[d.x + 1, d.y + 2].Blocked == false
                                  & this[d.x - 2, d.y].Own == 0 & this[d.x - 2, d.y].Blocked == false
                                  & this[d.x - 1, d.y + 1].Own == 0 & this[d.x - 1, d.y + 1].Blocked == false
                                  & this[d.x, d.y + 2].Own == 0 & this[d.x, d.y + 2].Blocked == false
                                  & this[d.x - 1, d.y + 2].Own == 0 & this[d.x - 1, d.y + 2].Blocked == false
                                  & this[d.x - 2, d.y + 1].Own == 0 & this[d.x - 2, d.y + 1].Blocked == false
                              select d;
            if (pat44_2_3_4.Count() > 0) return new Dot(pat44_2_3_4.First().x - 1, pat44_2_3_4.First().y + 1);
            //============================================================================================================== 



            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль

        }
        public Dot CheckPatternMove(int Owner)//паттерны без вражеской точки
        {
            iNumberPattern = 1;
            var pat1 = from Dot d in this
                       where d.Own == 0
                           && this[d.x - 1, d.y + 1].Own == Owner && this[d.x - 1, d.y + 1].Blocked == false
                           && this[d.x + 1, d.y - 1].Own == Owner && this[d.x + 1, d.y - 1].Blocked == false
                           && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                           && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                           && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                           && this[d.x + 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                           && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                           && this[d.x, d.y - 2].Own == 0 && this[d.x, d.y - 2].Blocked == false
                           && this[d.x + 2, d.y].Own == 0 && this[d.x + 2, d.y].Blocked == false
                           && this[d.x, d.y + 2].Own == 0 && this[d.x, d.y + 2].Blocked == false
                           && this[d.x - 2, d.y].Own == 0 && this[d.x - 2, d.y].Blocked == false
                       select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x, pat1.First().y);
            //--------------Rotate on 90-----------------------------------
            var pat1_2_3_4 = from Dot d in this
                             where d.Own == 0
                                 && this[d.x + 1, d.y + 1].Own == Owner && this[d.x + 1, d.y - 1].Blocked == false
                                 && this[d.x - 1, d.y - 1].Own == Owner && this[d.x - 1, d.y + 1].Blocked == false
                                 && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                                 && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                                 && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                                 && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                                 && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                                 && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                                 && this[d.x - 2, d.y].Own == 0 && this[d.x - 2, d.y].Blocked == false
                                 && this[d.x, d.y - 2].Own == 0 && this[d.x, d.y + 2].Blocked == false
                                 && this[d.x + 2, d.y].Own == 0 && this[d.x + 2, d.y].Blocked == false
                                 && this[d.x, d.y + 2].Own == 0 && this[d.x, d.y - 2].Blocked == false
                             select d;
            if (pat1_2_3_4.Count() > 0) return new Dot(pat1_2_3_4.First().x, pat1_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 883;
            var pat883 = from Dot d in this
                         where d.Own == Owner
                             && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                             && this[d.x + 2, d.y].Own == 0 && this[d.x + 2, d.y].Blocked == false
                             && this[d.x + 3, d.y].Own == 0 && this[d.x + 3, d.y].Blocked == false
                             && this[d.x + 3, d.y - 1].Own == Owner && this[d.x + 3, d.y - 1].Blocked == false
                             && this[d.x + 2, d.y - 1].Own == 0 && this[d.x + 2, d.y - 1].Blocked == false
                             && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                             && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                             && this[d.x + 2, d.y - 2].Own == 0 && this[d.x + 2, d.y - 2].Blocked == false
                             && this[d.x + 1, d.y - 2].Own == 0 && this[d.x + 1, d.y - 2].Blocked == false
                             && this[d.x + 3, d.y - 2].Own == 0 && this[d.x + 3, d.y - 2].Blocked == false
                             && this[d.x, d.y - 2].Own == 0 && this[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat883.Count() > 0) return new Dot(pat883.First().x + 1, pat883.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            var pat883_2_3 = from Dot d in this
                             where d.Own == Owner
                                 && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                                 && this[d.x, d.y - 2].Own == 0 && this[d.x, d.y - 2].Blocked == false
                                 && this[d.x, d.y - 3].Own == 0 && this[d.x, d.y - 3].Blocked == false
                                 && this[d.x + 1, d.y - 3].Own == Owner && this[d.x + 1, d.y - 3].Blocked == false
                                 && this[d.x + 1, d.y - 2].Own == 0 && this[d.x + 1, d.y - 2].Blocked == false
                                 && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                                 && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                                 && this[d.x + 2, d.y - 2].Own == 0 && this[d.x + 2, d.y - 2].Blocked == false
                                 && this[d.x + 2, d.y - 1].Own == 0 && this[d.x + 2, d.y - 1].Blocked == false
                                 && this[d.x + 2, d.y - 3].Own == 0 && this[d.x + 2, d.y - 3].Blocked == false
                                 && this[d.x + 2, d.y].Own == 0 && this[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat883_2_3.Count() > 0) return new Dot(pat883_2_3.First().x + 1, pat883_2_3.First().y - 1);
            //=================================================================================
            // 0d край доски
            // m   *
            iNumberPattern = 2;
            var pat2 = from Dot d in this
                       where d.Own == Owner && d.y == 0 && d.x > 0 && d.x < BoardWidth
                           && this[d.x + 1, d.y + 1].Own == Owner && this[d.x + 1, d.y + 1].Blocked == false
                           && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                       select d;
            if (pat2.Count() > 0) return new Dot(pat2.First().x, pat2.First().y + 1);
            var pat2_2 = from Dot d in this
                         where d.Own == Owner && d.y > 1 && d.y < BoardHeight && d.x == 0
                               && this[d.x + 1, d.y + 1].Own == Owner && this[d.x + 1, d.y + 1].Blocked == false
                               && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat2_2.Count() > 0) return new Dot(pat2_2.First().x + 1, pat2_2.First().y);
            var pat2_2_3 = from Dot d in this
                           where d.Own == Owner && d.x == BoardWidth - 1 && d.y > 0 && d.y < BoardHeight
                                 && this[d.x - 1, d.y - 1].Own == Owner && this[d.x - 1, d.y - 1].Blocked == false
                                 && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                           select d;
            if (pat2_2_3.Count() > 0) return new Dot(pat2_2_3.First().x - 1, pat2_2_3.First().y);
            var pat2_2_3_4 = from Dot d in this
                             where d.Own == Owner && d.y == BoardHeight - 1 && d.x > 0 && d.x < BoardWidth
                                   && this[d.x - 1, d.y - 1].Own == Owner && this[d.x - 1, d.y - 1].Blocked == false
                                   && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat2_2_3_4.Count() > 0) return new Dot(pat2_2_3_4.First().x, pat2_2_3_4.First().y - 1);
            ////============================================================================================================== 
            //iNumberPattern = 3;
            //var pat3 = from Dot d in this
            //           where d.Own == Owner && d.Blocked == false
            //                 && this[d.x + 1, d.y -1].Own == Owner && this[d.x + 1, d.y -1].Blocked == false
            //                 && this[d.x + 1, d.y].Blocked == false && this[d.x + 1, d.y].Own !=Owner
            //                 && this[d.x, d.y -1].Blocked == false && this[d.x, d.y -1].Own !=Owner
            //             select d;
            //Dot[] ad = pat3.ToArray();
            //foreach (Dot d in ad)
            //{
            //    if (this[d.x+1, d.y].Own == 0) return new Dot(d.x+1, d.y);
            //    if (this[d.x, d.y -1].Own == 0) return new Dot(d.x, d.y -1);
            //}
            //       *
            //     m
            //  d*
            iNumberPattern = 4;
            var pat4 = from Dot d in this
                       where d.Own == Owner
                           && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y - 2].Own == 0 && this[d.x + 1, d.y - 2].Blocked == false
                           && this[d.x + 2, d.y - 2].Own == Owner && this[d.x + 2, d.y - 2].Blocked == false
                           && this[d.x + 2, d.y - 1].Own == 0 && this[d.x + 2, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                           && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                       select d;
            if (pat4.Count() > 0) return new Dot(pat4.First().x + 1, pat4.First().y - 1);
            //180 Rotate=========================================================================================================== 
            //  *
            //     m
            //        d* 
            var pat4_2 = from Dot d in this
                         where d.Own == Owner
                             && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                             && this[d.x - 1, d.y - 2].Own == 0 && this[d.x - 1, d.y - 2].Blocked == false
                             && this[d.x - 2, d.y - 2].Own == Owner && this[d.x - 2, d.y - 2].Blocked == false
                             && this[d.x - 2, d.y - 1].Own == 0 && this[d.x - 2, d.y - 1].Blocked == false
                             && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                             && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat4_2.Count() > 0) return new Dot(pat4_2.First().x - 1, pat4_2.First().y - 1);
            //============================================================================================================== 
            //d*  m  *
            iNumberPattern = 7;
            var pat7 = from Dot d in this
                       where d.Own == Owner
                           && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                           && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                           && this[d.x + 2, d.y].Own == Owner && this[d.x + 2, d.y].Blocked == false
                       select d;
            if (pat7.Count() > 0) return new Dot(pat7.First().x + 1, pat7.First().y);
            //--------------Rotate on 90-----------------------------------
            //   *
            //   m
            //   d*
            var pat7_2 = from Dot d in this
                         where d.Own == Owner
                             && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                             && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                             && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                             && this[d.x, d.y - 2].Own == Owner && this[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat7_2.Count() > 0) return new Dot(pat7_2.First().x, pat7_2.First().y - 1);
            //============================================================================================================== 
            //    *
            // m 
            //
            // d*
            iNumberPattern = 8;
            var pat8 = from Dot d in this
                       where d.Own == Owner
                           && this[d.x + 1, d.y - 3].Own == Owner && this[d.x + 1, d.y - 3].Blocked == false
                           && this[d.x, d.y - 2].Own == 0 && this[d.x, d.y - 2].Blocked == false
                           && this[d.x + 1, d.y - 2].Own == 0 && this[d.x + 1, d.y - 2].Blocked == false
                           && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                           && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                           && this[d.x, d.y - 3].Own == 0 && this[d.x, d.y - 3].Blocked == false
                       select d;
            if (pat8.Count() > 0) return new Dot(pat8.First().x, pat8.First().y - 2);
            //180 Rotate=========================================================================================================== 
            var pat8_2 = from Dot d in this
                         where d.Own == Owner
                             && this[d.x - 1, d.y + 3].Own == Owner && this[d.x - 1, d.y + 3].Blocked == false
                             && this[d.x, d.y + 2].Own == 0 && this[d.x, d.y + 2].Blocked == false
                             && this[d.x - 1, d.y + 2].Own == 0 && this[d.x - 1, d.y + 2].Blocked == false
                             && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                             && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                             && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                             && this[d.x, d.y + 3].Own == 0 && this[d.x, d.y + 3].Blocked == false
                         select d;
            if (pat8_2.Count() > 0) return new Dot(pat8_2.First().x, pat8_2.First().y + 2);
            //--------------Rotate on 90-----------------------------------
            var pat8_2_3 = from Dot d in this
                           where d.Own == Owner
                               && this[d.x + 3, d.y - 1].Own == Owner && this[d.x + 3, d.y - 1].Blocked == false
                               && this[d.x + 2, d.y].Own == 0 && this[d.x + 2, d.y].Blocked == false
                               && this[d.x + 2, d.y - 1].Own == 0 && this[d.x + 2, d.y - 1].Blocked == false
                               && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                               && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                               && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                               && this[d.x + 3, d.y].Own == 0 && this[d.x + 3, d.y].Blocked == false
                           select d;
            if (pat8_2_3.Count() > 0) return new Dot(pat8_2_3.First().x + 2, pat8_2_3.First().y);
            //--------------Rotate on 90 -2-----------------------------------
            var pat8_2_3_4 = from Dot d in this
                             where d.Own == Owner
                                 && this[d.x - 3, d.y + 1].Own == Owner && this[d.x - 3, d.y + 1].Blocked == false
                                 && this[d.x - 2, d.y].Own == 0 && this[d.x - 2, d.y].Blocked == false
                                 && this[d.x - 2, d.y + 1].Own == 0 && this[d.x - 2, d.y + 1].Blocked == false
                                 && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                                 && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                                 && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                                 && this[d.x - 3, d.y].Own == 0 && this[d.x - 3, d.y].Blocked == false
                             select d;
            if (pat8_2_3_4.Count() > 0) return new Dot(pat8_2_3_4.First().x - 2, pat8_2_3_4.First().y);
            //============================================================================================================== 
            //     *
            //        d*  
            //     m
            //============================================================================================================== 
            iNumberPattern = 9;
            var pat9 = from Dot d in this
                       where d.Own == Owner && d.x >= 2
                       && this[d.x - 1, d.y - 1].Own == Owner && this[d.x - 1, d.y - 1].Blocked == false
                       && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                       && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                       && this[d.x - 2, d.y].Own == 0 && this[d.x - 2, d.y].Blocked == false
                       select d;
            if (pat9.Count() > 0) return new Dot(pat9.First().x - 1, pat9.First().y + 1);
            //180 Rotate=========================================================================================================== 
            //     m  
            // d*  
            //     *
            var pat9_2 = from Dot d in this
                         where d.Own == Owner && d.x <= BoardWidth - 2
                               && this[d.x + 1, d.y + 1].Own == Owner && this[d.x + 1, d.y + 1].Blocked == false
                               && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                               && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                               && this[d.x + 2, d.y].Own == 0 && this[d.x + 2, d.y].Blocked == false
                         select d;
            if (pat9_2.Count() > 0) return new Dot(pat9_2.First().x + 1, pat9_2.First().y - 1);
            //--------------Rotate on 90-----------------------------------
            //         
            //     d*
            //  m       *
            var pat9_2_3 = from Dot d in this
                           where d.Own == Owner && d.y <= BoardHeight - 2
                                 && this[d.x + 1, d.y + 1].Own == Owner && this[d.x + 1, d.y + 1].Blocked == false
                                 && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                                 && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                                 && this[d.x, d.y + 2].Own == 0 && this[d.x, d.y + 2].Blocked == false
                           select d;
            if (pat9_2_3.Count() > 0) return new Dot(pat9_2_3.First().x - 1, pat9_2_3.First().y + 1);
            //--------------Rotate on 90 -2-----------------------------------
            // *      m
            //    d*   
            //
            var pat9_2_3_4 = from Dot d in this
                             where d.Own == Owner && d.y >= 2
                                   && this[d.x - 1, d.y - 1].Own == Owner && this[d.x - 1, d.y - 1].Blocked == false
                                   && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                                   && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                                   && this[d.x, d.y - 2].Own == 0 && this[d.x, d.y - 2].Blocked == false
                             select d;
            if (pat9_2_3_4.Count() > 0) return new Dot(pat9_2_3_4.First().x + 1, pat9_2_3_4.First().y - 1);
            //============================================================================================================== 

            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль
        }

        public List<Dot> CheckPattern2Move(int Owner) //проверка хода на гарантированное окружение(когда точки находятся через две клетки) 
        {
            List<Dot> ld = new List<Dot>();
            iNumberPattern = 1;
            var pat1 = from Dot d in this
                       where d.Own == Owner
                             && this[d.x, d.y].IndexRelation == this[d.x + 2, d.y - 1].IndexRelation
                             && this[d.x + 2, d.y - 1].Own == Owner && this[d.x + 2, d.y - 1].Blocked == false
                             && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                             && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                       select d;
            AddToList(ld, pat1, 1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 2;
            var pat2 = from Dot d in this
                       where d.Own == Owner
                            && d.IndexRelation == this[d.x + 2, d.y].IndexRelation
                            && this[d.x + 2, d.y - 1].Own == Owner && this[d.x + 2, d.y].Blocked == false
                            && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                            && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                       select d;
            AddToList(ld, pat2, 1, 0);
            //-----------------------------------------------------------------------------
            var pat3 = from Dot d in this
                       where d.Own == Owner
                            && d.IndexRelation == this[d.x + 2, d.y].IndexRelation
                            && this[d.x + 2, d.y - 1].Own == Owner && this[d.x + 2, d.y - 1].Blocked == false
                            && this[d.x + 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                            && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                       select d;
            AddToList(ld, pat3, 1, 0);
            //-----------------------------------------------------------------------------
            var pat4 = from Dot d in this
                       where d.Own == Owner
                            && d.IndexRelation == this[d.x + 2, d.y + 1].IndexRelation
                            && this[d.x + 2, d.y + 1].Own == Owner && this[d.x + 2, d.y + 1].Blocked == false
                            && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                            && this[d.x + 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat4, 1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 5;
            var pat5 = from Dot d in this
                       where d.Own == Owner
                          && d.IndexRelation == this[d.x + 1, d.y + 2].IndexRelation
                          && this[d.x + 1, d.y + 2].Own == Owner && this[d.x + 1, d.y + 2].Blocked == false
                          && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                          && this[d.x + 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat5, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 6;
            var pat6 = from Dot d in this
                       where d.Own == Owner
                          && d.IndexRelation == this[d.x, d.y + 2].IndexRelation
                          && this[d.x, d.y + 2].Own == Owner && this[d.x, d.y + 2].Blocked == false
                          && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                          && this[d.x + 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat6, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 7;
            var pat7 = from Dot d in this
                       where d.Own == Owner
                          && d.IndexRelation == this[d.x, d.y + 2].IndexRelation
                          && this[d.x, d.y + 2].Own == Owner && this[d.x, d.y + 2].Blocked == false
                          && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                          && this[d.x - 1, d.y + 1].Own == 0 && this[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat7, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 8;
            var pat8 = from Dot d in this
                       where d.Own == Owner
                          && d.IndexRelation == this[d.x - 1, d.y + 2].IndexRelation
                          && this[d.x - 1, d.y + 2].Own == Owner && this[d.x - 1, d.y + 2].Blocked == false
                          && this[d.x, d.y + 1].Own == 0 && this[d.x, d.y + 1].Blocked == false
                          && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat8, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 9;
            var pat9 = from Dot d in this
                       where d.Own == Owner
                          && d.IndexRelation == this[d.x - 2, d.y + 1].IndexRelation
                          && this[d.x - 2, d.y + 1].Own == Owner && this[d.x - 2, d.y + 1].Blocked == false
                          && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                          && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat9, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 10;
            var pat10 = from Dot d in this
                        where d.Own == Owner
                           && d.IndexRelation == this[d.x - 2, d.y].IndexRelation
                           && this[d.x - 2, d.y].Own == Owner && this[d.x - 2, d.y].Blocked == false
                           && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                           && this[d.x - 1, d.y + 1].Own == 0 && this[d.x - 1, d.y + 1].Blocked == false
                        select d;
            AddToList(ld, pat10, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 11;
            var pat11 = from Dot d in this
                        where d.Own == Owner
                           && d.IndexRelation == this[d.x - 2, d.y].IndexRelation
                           && this[d.x - 2, d.y].Own == Owner && this[d.x - 2, d.y].Blocked == false
                           && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                           && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat11, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 12;
            var pat12 = from Dot d in this
                        where d.Own == Owner
                        && d.IndexRelation == this[d.x - 2, d.y - 1].IndexRelation
                        && this[d.x - 2, d.y - 1].Own == Owner && this[d.x - 2, d.y - 1].Blocked == false
                        && this[d.x - 1, d.y].Own == 0 && this[d.x - 1, d.y].Blocked == false
                        && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat12, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 13;
            var pat13 = from Dot d in this
                        where d.Own == Owner
                        && d.IndexRelation == this[d.x - 1, d.y - 2].IndexRelation
                        && this[d.x - 1, d.y - 2].Own == Owner && this[d.x - 1, d.y - 2].Blocked == false
                        && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                        && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat13, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 14;
            var pat14 = from Dot d in this
                        where d.Own == Owner
                        && d.IndexRelation == this[d.x, d.y - 2].IndexRelation
                        && this[d.x, d.y - 2].Own == Owner && this[d.x, d.y - 2].Blocked == false
                        && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                        && this[d.x - 1, d.y - 1].Own == 0 && this[d.x - 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat14, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 15;
            var pat15 = from Dot d in this
                        where d.Own == Owner
                        && d.IndexRelation == this[d.x, d.y - 2].IndexRelation
                        && this[d.x, d.y - 2].Own == Owner && this[d.x, d.y - 2].Blocked == false
                        && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                        && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat15, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 16;
            var pat16 = from Dot d in this
                        where d.Own == Owner
                           && d.IndexRelation == this[d.x + 1, d.y - 2].IndexRelation
                           && this[d.x + 1, d.y - 2].Own == Owner && this[d.x + 1, d.y - 2].Blocked == false
                           && this[d.x, d.y - 1].Own == 0 && this[d.x, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat16, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 17;
            var pat17 = from Dot d in this
                        where d.Own == Owner
                           && d.IndexRelation == this[d.x + 2, d.y - 1].IndexRelation
                           && this[d.x + 2, d.y - 1].Own == Owner && this[d.x + 2, d.y - 1].Blocked == false
                           && this[d.x + 1, d.y].Own == 0 && this[d.x + 1, d.y].Blocked == false
                           && this[d.x + 1, d.y - 1].Own == 0 && this[d.x + 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat17, 1, 0);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            return ld;
        }

        private static void AddToList(List<Dot> ld, IEnumerable<Dot> pattern, int dx, int dy)
        {
            foreach (Dot dot in pattern)
            {
                Dot d = new Dot(dot.x + dx, dot.y + dy);
                if (ld.Contains(d) == false) ld.Add(d);
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

                //IndexDot = x * BoardWidth + y;  (IndexDot-y) / BoardWidth
                return _Dots[position];
                //int i = position / BoardWidth;
                //if (position / BoardWidth == BoardWidth)
                //{
                //    i = BoardWidth - 1;
                //}
                //int j = position % BoardHeight;
                //if (position % BoardHeight == BoardHeight)
                //{
                //    j = BoardHeight - 1;
                //}
                //return _Dots[IndexDot(i, position % BoardHeight)];
            }
        }
    }
}
