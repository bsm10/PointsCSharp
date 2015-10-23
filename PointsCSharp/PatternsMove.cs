using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotsGame
{
    public partial class Game
    {
        private Dot CheckPatternMove(int Owner)//паттерны без вражеской точки
        {
            iNumberPattern = 1;
            var pat1 = from Dot d in aDots
                         where d.Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                             & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                         select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x, pat1.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat1_2_3_4 = from Dot d in aDots
                               where d.Own == 0
                                   & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                   & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               select d;
            if (pat1_2_3_4.Count() > 0) return new Dot(pat1_2_3_4.First().x, pat1_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 883;
            var pat883 = from Dot d in aDots
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             & aDots[d.x + 3, d.y].Own == 0 & aDots[d.x + 3, d.y].Blocked == false
                             & aDots[d.x + 3, d.y - 1].Own == Owner & aDots[d.x + 3, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                             & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                             & aDots[d.x + 3, d.y - 2].Own == 0 & aDots[d.x + 3, d.y - 2].Blocked == false
                             & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat883.Count() > 0) return new Dot(pat883.First().x + 1, pat883.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat883_2_3 = from Dot d in aDots
                             where d.Own == Owner
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y - 3].Own == 0 & aDots[d.x, d.y - 3].Blocked == false
                                 & aDots[d.x + 1, d.y - 3].Own == Owner & aDots[d.x + 1, d.y - 3].Blocked == false
                                 & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                                 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y - 3].Own == 0 & aDots[d.x + 2, d.y - 3].Blocked == false
                                 & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat883_2_3.Count() > 0) return new Dot(pat883_2_3.First().x + 1, pat883_2_3.First().y - 1);
            //=================================================================================
            // 0d край доски
            // m   *
            iNumberPattern = 2;
            var pat2 = from Dot d in aDots
                         where d.Own == Owner & d.y==0 & d.x>0 & d.x<iBoardSize
                             & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat2.Count() > 0) return new Dot(pat2.First().x, pat2.First().y + 1);
            var pat2_2 = from Dot d in aDots
                         where d.Own == Owner & d.y > 1 & d.y < iBoardSize & d.x==0
                               & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y ].Own == 0 & aDots[d.x+1, d.y].Blocked == false
                           select d;
            if (pat2_2.Count() > 0) return new Dot(pat2_2.First().x+1, pat2_2.First().y );
            var pat2_2_3 = from Dot d in aDots
                           where d.Own == Owner & d.x == iBoardSize - 1 & d.y > 0 & d.y < iBoardSize
                                 & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x-1, d.y].Own == 0 & aDots[d.x-1, d.y].Blocked == false
                           select d;
            if (pat2_2_3.Count() > 0) return new Dot(pat2_2_3.First().x - 1, pat2_2_3.First().y);
            var pat2_2_3_4 = from Dot d in aDots
                             where d.Own == Owner & d.y == iBoardSize - 1 & d.x > 0 & d.x < iBoardSize
                                   & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat2_2_3_4.Count() > 0) return new Dot(pat2_2_3_4.First().x, pat2_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 3;
            var pat3 = from Dot d in aDots
                       where d.Own == Owner & d.Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Blocked == false & aDots[d.x + 1, d.y].Own !=Owner
                             & aDots[d.x, d.y - 1].Blocked == false & aDots[d.x, d.y - 1].Own !=Owner
                         select d;
            Dot[] ad = pat3.ToArray();
            foreach (Dot d in ad)
            {
                if (aDots[d.x+1, d.y].Own == 0) return new Dot(d.x+1, d.y);
                if (aDots[d.x, d.y - 1].Own == 0) return new Dot(d.x, d.y - 1);
            }
            //       *
            //     m
            //  d*
            iNumberPattern = 4;
            var pat4 = from Dot d in aDots
                       where d.Own == Owner
                           & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                           & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                           & aDots[d.x + 2, d.y - 2].Own == Owner & aDots[d.x + 2, d.y - 2].Blocked == false
                           & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                           & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                           & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                       select d;
            if (pat4.Count() > 0) return new Dot(pat4.First().x + 1, pat4.First().y - 1);
            //180 Rotate=========================================================================================================== 
            //  *
            //     m
            //        d* 
            var pat4_2 = from Dot d in aDots
                         where d.Own == Owner
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                             & aDots[d.x - 2, d.y - 2].Own == Owner & aDots[d.x - 2, d.y - 2].Blocked == false
                             & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat4_2.Count() > 0) return new Dot(pat4_2.First().x - 1, pat4_2.First().y - 1);
            //============================================================================================================== 
            // если точка рядом с бортом - заземляем
            iNumberPattern = 5;
            var pat5 = from Dot d in aDots
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y].Own == 0 & d.x + 1 == iBoardSize - 1
                         select d;
            if (pat5.Count() > 0) return new Dot(pat5.First().x + 1, pat5.First().y);
            var pat5_2_3_4 = from Dot d in aDots
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y].Own == 0 & d.x - 1 == 0
                             select d;
            if (pat5_2_3_4.Count() > 0) return new Dot(pat5_2_3_4.First().x - 1, pat5_2_3_4.First().y );
            var pat5_2 = from Dot d in aDots
                           where d.Own == Owner
                               & aDots[d.x, d.y + 1].Own == 0 & d.y + 1 == iBoardSize - 1
                           select d;
            if (pat5_2.Count() > 0) return new Dot(pat5_2.First().x , pat5_2.First().y+1);
            var pat5_2_3 = from Dot d in aDots
                             where d.Own == Owner
                                 & aDots[d.x, d.y - 1].Own == 0 & (d.y - 1) == 0
                             select d;
            if (pat5_2_3.Count() > 0) return new Dot(pat5_2_3.First().x, pat5_2_3.First().y - 1);
            //============================================================================================================== 



            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль
        }

        private Dot CheckPattern2Move(int Owner) //проверка хода на гарантированное окружение(когда точки находятся через две клетки) 
        {
            iNumberPattern = 1;
            var pat1 = from Dot d in aDots
                       where d.Own == Owner
                           & d.IndexDot == aDots[d.x + 2, d.y - 1].IndexDot
                           & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                           & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                           & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                       select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x + 1, pat1.First().y);
            iNumberPattern = 2;
            var pat2 = from Dot d in aDots
                       where d.Own == Owner
                            & d.IndexDot == aDots[d.x + 2, d.y].IndexDot
                            & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y].Blocked == false
                            & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                            & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                       select d;
            if (pat2.Count() > 0) return new Dot(pat2.First().x + 1, pat2.First().y);
            var pat3 = from Dot d in aDots
                       where d.Own == Owner
                            & d.IndexDot == aDots[d.x + 2, d.y].IndexDot
                            & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                            & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                       select d;
            if (pat3.Count() > 0) return new Dot(pat3.First().x + 1, pat3.First().y);
            var pat4 = from Dot d in aDots
                       where d.Own == Owner
                            & d.IndexDot == aDots[d.x + 2, d.y + 1].IndexDot
                            & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y ].Own == 0 & aDots[d.x + 1, d.y ].Blocked == false
                            & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                       select d;
            if (pat4.Count() > 0) return new Dot(pat4.First().x + 1, pat4.First().y + 1);
            iNumberPattern = 5;
            var pat5 = from Dot d in aDots
                         where d.Own == Owner
                            & d.IndexDot == aDots[d.x + 1, d.y + 2].IndexDot
                            & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                            & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            if (pat5.Count() > 0) return new Dot(pat5.First().x, pat5.First().y + 1);
            var pat6 = from Dot d in aDots
                       where d.Own == Owner
                          & d.IndexDot == aDots[d.x, d.y + 2].IndexDot
                          & aDots[d.x, d.y + 2].Own == Owner & aDots[d.x, d.y + 2].Blocked == false
                          & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                          & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                       select d;
            if (pat6.Count() > 0) return new Dot(pat6.First().x, pat6.First().y + 1);
            var pat7 = from Dot d in aDots
                       where d.Own == Owner
                          & d.IndexDot == aDots[d.x, d.y + 2].IndexDot
                          & aDots[d.x, d.y + 2].Own == Owner & aDots[d.x, d.y + 2].Blocked == false
                          & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                          & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                       select d;
            if (pat7.Count() > 0) return new Dot(pat7.First().x, pat7.First().y + 1);
            iNumberPattern = 970;
            var pat8 = from Dot d in aDots
                         where d.Own == Owner
                            & d.IndexDot == aDots[d.x - 1, d.y + 2].IndexDot
                            & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                            & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                            & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat8.Count() > 0) return new Dot(pat8.First().x - 1, pat8.First().y + 1);
            iNumberPattern = 9;
            var pat9 = from Dot d in aDots
                         where d.Own == Owner
                            & d.IndexDot == aDots[d.x - 2, d.y + 1].IndexDot
                            & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                            & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                            & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat9.Count() > 0) return new Dot(pat9.First().x - 1, pat9.First().y);
            var pat10 = from Dot d in aDots
                       where d.Own == Owner
                          & d.IndexDot == aDots[d.x - 2, d.y].IndexDot
                          & aDots[d.x - 2, d.y].Own == Owner & aDots[d.x - 2, d.y].Blocked == false
                          & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                          & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                       select d;
            if (pat10.Count() > 0) return new Dot(pat10.First().x - 1, pat10.First().y);
            var pat11 = from Dot d in aDots
                        where d.Own == Owner
                           & d.IndexDot == aDots[d.x - 2, d.y].IndexDot
                           & aDots[d.x - 2, d.y].Own == Owner & aDots[d.x - 2, d.y].Blocked == false
                           & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                           & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                        select d;
            if (pat11.Count() > 0) return new Dot(pat11.First().x - 1, pat11.First().y);
            iNumberPattern = 12;
            var pat12 = from Dot d in aDots
                         where d.Own == Owner
                         & d.IndexDot == aDots[d.x - 2, d.y - 1].IndexDot
                         & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                         & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                         & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat12.Count() > 0) return new Dot(pat12.First().x - 1, pat12.First().y - 1);
            iNumberPattern = 13;
            var pat13 = from Dot d in aDots
                             where d.Own == Owner
                             & d.IndexDot == aDots[d.x - 1, d.y - 2].IndexDot
                             & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                             
                             select d;
            if (pat13.Count() > 0) return new Dot(pat13.First().x - 1, pat13.First().y - 1);
            var pat14 = from Dot d in aDots
                               where d.Own == Owner
                               & d.IndexDot == aDots[d.x, d.y - 2].IndexDot
                               & aDots[d.x, d.y - 2].Own == Owner & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               select d;
            if (pat14.Count() > 0) return new Dot(pat14.First().x - 1, pat14.First().y - 1);
            var pat15 = from Dot d in aDots
                        where d.Own == Owner
                        & d.IndexDot == aDots[d.x, d.y - 2].IndexDot
                        & aDots[d.x, d.y - 2].Own == Owner & aDots[d.x, d.y - 2].Blocked == false
                        & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                        & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                        select d;
            if (pat15.Count() > 0) return new Dot(pat15.First().x - 1, pat15.First().y - 1);
            iNumberPattern = 16;
            var pat16 = from Dot d in aDots
                         where d.Own == Owner
                            & d.IndexDot == aDots[d.x + 1, d.y - 2].IndexDot
                            & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                            & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                            & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                         select d;
            if (pat16.Count() > 0) return new Dot(pat16.First().x + 1, pat16.First().y - 1);
            iNumberPattern = 17;
            var pat17 = from Dot d in aDots
                             where d.Own == Owner
                                & d.IndexDot == aDots[d.x + 2, d.y - 1].IndexDot
                                & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                                & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                             select d;
            if (pat17.Count() > 0) return new Dot(pat17.First().x + 1, pat17.First().y - 1);


          
            
            
             
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            return null;
        } 
    }
}
