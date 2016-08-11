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
            ArrayDots _aDots = aDots.CopyArrayDots;
            iNumberPattern = 1;
            var pat1 = from Dot d in _aDots.AsParallel()
                         where d.Own == 0
                             && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                         select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x, pat1.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat1_2_3_4 = from Dot d in _aDots
                               where d.Own == 0
                                   && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               select d;
            if (pat1_2_3_4.Count() > 0) return new Dot(pat1_2_3_4.First().x, pat1_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 883;
            var pat883 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y - 1].Own == Owner && _aDots[d.x + 3, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 3, d.y - 2].Own == 0 && _aDots[d.x + 3, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat883.Count() > 0) return new Dot(pat883.First().x + 1, pat883.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat883_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y - 3].Own == Owner && _aDots[d.x + 1, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 3].Own == 0 && _aDots[d.x + 2, d.y - 3].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat883_2_3.Count() > 0) return new Dot(pat883_2_3.First().x + 1, pat883_2_3.First().y - 1);
            //=================================================================================
            // 0d край доски
            // m   *
            iNumberPattern = 2;
            var pat2 = from Dot d in _aDots
                         where d.Own == Owner && d.y==0 && d.x>0 && d.x<iBoardSize
                             && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat2.Count() > 0) return new Dot(pat2.First().x, pat2.First().y + 1);
            var pat2_2 = from Dot d in _aDots
                         where d.Own == Owner && d.y > 1 && d.y < iBoardSize && d.x==0
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y ].Own == 0 && _aDots[d.x+1, d.y].Blocked == false
                           select d;
            if (pat2_2.Count() > 0) return new Dot(pat2_2.First().x+1, pat2_2.First().y );
            var pat2_2_3 = from Dot d in _aDots
                           where d.Own == Owner && d.x == iBoardSize - 1 && d.y > 0 && d.y < iBoardSize
                                 && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x-1, d.y].Own == 0 && _aDots[d.x-1, d.y].Blocked == false
                           select d;
            if (pat2_2_3.Count() > 0) return new Dot(pat2_2_3.First().x - 1, pat2_2_3.First().y);
            var pat2_2_3_4 = from Dot d in _aDots
                             where d.Own == Owner && d.y == iBoardSize - 1 && d.x > 0 && d.x < iBoardSize
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat2_2_3_4.Count() > 0) return new Dot(pat2_2_3_4.First().x, pat2_2_3_4.First().y - 1);
            ////============================================================================================================== 
            //iNumberPattern = 3;
            //var pat3 = from Dot d in _aDots
            //           where d.Own == Owner && d.Blocked == false
            //                 && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
            //                 && _aDots[d.x + 1, d.y].Blocked == false && _aDots[d.x + 1, d.y].Own !=Owner
            //                 && _aDots[d.x, d.y - 1].Blocked == false && _aDots[d.x, d.y - 1].Own !=Owner
            //             select d;
            //Dot[] ad = pat3.ToArray();
            //foreach (Dot d in ad)
            //{
            //    if (_aDots[d.x+1, d.y].Own == 0) return new Dot(d.x+1, d.y);
            //    if (_aDots[d.x, d.y - 1].Own == 0) return new Dot(d.x, d.y - 1);
            //}
            //       *
            //     m
            //  d*
            iNumberPattern = 4;
            var pat4 = from Dot d in _aDots
                       where d.Own == Owner
                           && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                           && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                           && _aDots[d.x + 2, d.y - 2].Own == Owner && _aDots[d.x + 2, d.y - 2].Blocked == false
                           && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                           && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                           && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                       select d;
            if (pat4.Count() > 0) return new Dot(pat4.First().x + 1, pat4.First().y - 1);
            //180 Rotate=========================================================================================================== 
            //  *
            //     m
            //        d* 
            var pat4_2 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat4_2.Count() > 0) return new Dot(pat4_2.First().x - 1, pat4_2.First().y - 1);
            //============================================================================================================== 
            //// если точка рядом с бортом - заземляем
            //iNumberPattern = 5;
            //var pat5 = from Dot d in _aDots
            //             where d.Own == Owner
            //                 && _aDots[d.x + 1, d.y].Own == 0 && d.x + 1 == iBoardSize - 1
            //             select d;
            //if (pat5.Count() > 0) return new Dot(pat5.First().x + 1, pat5.First().y);
            //var pat5_2_3_4 = from Dot d in _aDots
            //                 where d.Own == Owner
            //                     && _aDots[d.x - 1, d.y].Own == 0 && d.x - 1 == 0
            //                 select d;
            //if (pat5_2_3_4.Count() > 0) return new Dot(pat5_2_3_4.First().x - 1, pat5_2_3_4.First().y );
            //var pat5_2 = from Dot d in _aDots
            //               where d.Own == Owner
            //                   && _aDots[d.x, d.y + 1].Own == 0 && d.y + 1 == iBoardSize - 1
            //               select d;
            //if (pat5_2.Count() > 0) return new Dot(pat5_2.First().x , pat5_2.First().y+1);
            //var pat5_2_3 = from Dot d in _aDots
            //                 where d.Own == Owner
            //                     && _aDots[d.x, d.y - 1].Own == 0 && (d.y - 1) == 0
            //                 select d;
            //if (pat5_2_3.Count() > 0) return new Dot(pat5_2_3.First().x, pat5_2_3.First().y - 1);
            ////============================================================================================================== 
            ////     m  *
            //// d*      
            //iNumberPattern = 6;
            //var pat6 = from Dot d in _aDots
            //             where d.Own == Owner
            //                 &&&& d.IndexRelation != _aDots[d.x + 2, d.y - 1].IndexRelation
            //                 && _aDots[d.x + 2, d.y - 1].Blocked == false
            //                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
            //                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
            //             select d;
            //if (pat6.Count() > 0) return new Dot(pat6.First().x + 1, pat6.First().y - 1);
            ////180 Rotate=========================================================================================================== 
            ////         d*
            //// *   m
            //var pat6_2 = from Dot d in _aDots
            //               where d.Own == Owner
            //                   &&&& d.IndexRelation != _aDots[d.x - 2, d.y + 1].IndexRelation
            //                   && _aDots[d.x - 2, d.y + 1].Blocked == false
            //                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
            //                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
            //               select d;
            //if (pat6_2.Count() > 0) return new Dot(pat6_2.First().x - 1, pat6_2.First().y + 1);
            ////--------------Rotate on 90----------------------------------- 
            ////     *     
            ////     m 
            ////  d*
            //var pat6_2_3 = from Dot d in _aDots
            //                 where d.Own == Owner
            //                    &&&& d.IndexRelation != _aDots[d.x + 1, d.y - 2].IndexRelation
            //                     && _aDots[d.x + 1, d.y - 2].Blocked == false
            //                     && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
            //                     && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
            //                 select d;
            //if (pat6_2_3.Count() > 0) return new Dot(pat6_2_3.First().x + 1, pat6_2_3.First().y - 1);
            ////--------------Rotate on 90 - 2----------------------------------- 
            ////        d*     
            ////     m 
            ////     *
            //var pat6_2_3_4 = from Dot d in _aDots
            //                   where d.Own == Owner
            //                        &&&& d.IndexRelation != _aDots[d.x - 1, d.y + 2].IndexRelation
            //                       && _aDots[d.x - 1, d.y + 2].Blocked == false
            //                       && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
            //                       && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
            //                   select d;
            //if (pat6_2_3_4.Count() > 0) return new Dot(pat6_2_3_4.First().x - 1, pat6_2_3_4.First().y + 1);
            //============================================================================================================== 
            //d*  m  *
            iNumberPattern = 7;
            var pat7 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y-1].Own == 0 && _aDots[d.x + 1, d.y-1].Blocked == false
                             && _aDots[d.x + 1, d.y+1].Own == 0 && _aDots[d.x + 1, d.y+1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false
                         select d;
            if (pat7.Count() > 0) return new Dot(pat7.First().x + 1, pat7.First().y);
            //--------------Rotate on 90----------------------------------- 
            //   *
            //   m
            //   d*
            var pat7_2 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x-1, d.y - 1].Own == 0 && _aDots[d.x-1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == Owner && _aDots[d.x, d.y - 2].Blocked == false
                             select d;
            if (pat7_2.Count() > 0) return new Dot(pat7_2.First().x, pat7_2.First().y - 1);
            //============================================================================================================== 
            //    *
            // m 
            //
            // d*
            iNumberPattern = 8;
            var pat8 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y - 3].Own == Owner && _aDots[d.x + 1, d.y - 3].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                         select d;
            if (pat8.Count() > 0) return new Dot(pat8.First().x, pat8.First().y - 2);
            //180 Rotate=========================================================================================================== 
            var pat8_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y + 3].Own == Owner && _aDots[d.x - 1, d.y + 3].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                           select d;
            if (pat8_2.Count() > 0) return new Dot(pat8_2.First().x, pat8_2.First().y + 2);
            //--------------Rotate on 90----------------------------------- 
            var pat8_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 3, d.y - 1].Own == Owner && _aDots[d.x + 3, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                             select d;
            if (pat8_2_3.Count() > 0) return new Dot(pat8_2_3.First().x + 2, pat8_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat8_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 3, d.y + 1].Own == Owner && _aDots[d.x - 3, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                               select d;
            if (pat8_2_3_4.Count() > 0) return new Dot(pat8_2_3_4.First().x - 2, pat8_2_3_4.First().y);
                //============================================================================================================== 
                //     *
                //        d*  
                //     m
                //============================================================================================================== 
            iNumberPattern = 9;
            var pat9 = from Dot d in _aDots
                             where d.Own == Owner && d.x >= 2 
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                         select d;
            if (pat9.Count() > 0) return new Dot(pat9.First().x - 1, pat9.First().y + 1);
            //180 Rotate=========================================================================================================== 
            //     m  
            // d*  
            //     *
            var pat9_2 = from Dot d in _aDots
                         where d.Own == Owner && d.x <= iBoardSize - 2 
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                           select d;
            if (pat9_2.Count() > 0) return new Dot(pat9_2.First().x + 1, pat9_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            //         
            //     d*
            //  m       *
            var pat9_2_3 = from Dot d in _aDots
                           where d.Own == Owner && d.y <= iBoardSize - 2
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             select d;
            if (pat9_2_3.Count() > 0) return new Dot(pat9_2_3.First().x - 1, pat9_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            // *      m
            //    d*   
            //
            var pat9_2_3_4 = from Dot d in _aDots
                             where d.Own == Owner && d.y >= 2 
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               select d;
            if (pat9_2_3_4.Count() > 0) return new Dot(pat9_2_3_4.First().x + 1, pat9_2_3_4.First().y - 1);
                //============================================================================================================== 

            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль
        }

        private List<Dot> CheckPattern2Move(int Owner, ArrayDots arrDots) //проверка хода на гарантированное окружение(когда точки находятся через две клетки) 
        {
            List<Dot> ld = new List<Dot>();
            iNumberPattern = 1;
            var pat1 = from Dot d in arrDots
                       where d.Own == Owner
                             && arrDots[d.x, d.y].IndexRelation == arrDots[d.x + 2, d.y - 1].IndexRelation
                             && arrDots[d.x + 2, d.y - 1].Own == Owner && arrDots[d.x + 2, d.y - 1].Blocked == false
                             && arrDots[d.x + 1, d.y - 1].Own == 0 && arrDots[d.x + 1, d.y - 1].Blocked == false
                             && arrDots[d.x + 1, d.y].Own == 0 && arrDots[d.x + 1, d.y].Blocked == false
                       select d;
            AddToList(ld, pat1, 1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 2;
            var pat2 = from Dot d in arrDots
                       where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x + 2, d.y].IndexRelation
                            && arrDots[d.x + 2, d.y - 1].Own == Owner && arrDots[d.x + 2, d.y].Blocked == false
                            && arrDots[d.x + 1, d.y - 1].Own == 0 && arrDots[d.x + 1, d.y - 1].Blocked == false
                            && arrDots[d.x + 1, d.y].Own == 0 && arrDots[d.x + 1, d.y].Blocked == false
                       select d;
            AddToList(ld, pat2, 1, 0);
            //-----------------------------------------------------------------------------
            var pat3 = from Dot d in arrDots
                       where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x + 2, d.y].IndexRelation
                            && arrDots[d.x + 2, d.y - 1].Own == Owner && arrDots[d.x + 2, d.y - 1].Blocked == false
                            && arrDots[d.x + 1, d.y + 1].Own == 0 && arrDots[d.x + 1, d.y + 1].Blocked == false
                            && arrDots[d.x + 1, d.y].Own == 0 && arrDots[d.x + 1, d.y].Blocked == false
                       select d;
            AddToList(ld, pat3, 1, 0);
            //-----------------------------------------------------------------------------
            var pat4 = from Dot d in arrDots
                       where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x + 2, d.y + 1].IndexRelation
                            && arrDots[d.x + 2, d.y + 1].Own == Owner && arrDots[d.x + 2, d.y + 1].Blocked == false
                            && arrDots[d.x + 1, d.y ].Own == 0 && arrDots[d.x + 1, d.y ].Blocked == false
                            && arrDots[d.x + 1, d.y + 1].Own == 0 && arrDots[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat4, 1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 5;
            var pat5 = from Dot d in arrDots
                         where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x + 1, d.y + 2].IndexRelation
                            && arrDots[d.x + 1, d.y + 2].Own == Owner && arrDots[d.x + 1, d.y + 2].Blocked == false
                            && arrDots[d.x, d.y + 1].Own == 0 && arrDots[d.x, d.y + 1].Blocked == false
                            && arrDots[d.x + 1, d.y + 1].Own == 0 && arrDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            AddToList(ld, pat5, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 6;
            var pat6 = from Dot d in arrDots
                       where d.Own == Owner
                          && d.IndexRelation == arrDots[d.x, d.y + 2].IndexRelation
                          && arrDots[d.x, d.y + 2].Own == Owner && arrDots[d.x, d.y + 2].Blocked == false
                          && arrDots[d.x, d.y + 1].Own == 0 && arrDots[d.x, d.y + 1].Blocked == false
                          && arrDots[d.x + 1, d.y + 1].Own == 0 && arrDots[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat6, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 7;
            var pat7 = from Dot d in arrDots
                       where d.Own == Owner
                          && d.IndexRelation == arrDots[d.x, d.y + 2].IndexRelation
                          && arrDots[d.x, d.y + 2].Own == Owner && arrDots[d.x, d.y + 2].Blocked == false
                          && arrDots[d.x, d.y + 1].Own == 0 && arrDots[d.x, d.y + 1].Blocked == false
                          && arrDots[d.x - 1, d.y + 1].Own == 0 && arrDots[d.x + 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat7, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 8;
            var pat8 = from Dot d in arrDots
                         where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x - 1, d.y + 2].IndexRelation
                            && arrDots[d.x - 1, d.y + 2].Own == Owner && arrDots[d.x - 1, d.y + 2].Blocked == false
                            && arrDots[d.x, d.y + 1].Own == 0 && arrDots[d.x, d.y + 1].Blocked == false
                            && arrDots[d.x - 1, d.y + 1].Own == 0 && arrDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            AddToList(ld, pat8, 0, 1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 9;
            var pat9 = from Dot d in arrDots
                         where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x - 2, d.y + 1].IndexRelation
                            && arrDots[d.x - 2, d.y + 1].Own == Owner && arrDots[d.x - 2, d.y + 1].Blocked == false
                            && arrDots[d.x - 1, d.y].Own == 0 && arrDots[d.x - 1, d.y].Blocked == false
                            && arrDots[d.x - 1, d.y + 1].Own == 0 && arrDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            AddToList(ld, pat9, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 10;
            var pat10 = from Dot d in arrDots
                       where d.Own == Owner
                          && d.IndexRelation == arrDots[d.x - 2, d.y].IndexRelation
                          && arrDots[d.x - 2, d.y].Own == Owner && arrDots[d.x - 2, d.y].Blocked == false
                          && arrDots[d.x - 1, d.y].Own == 0 && arrDots[d.x - 1, d.y].Blocked == false
                          && arrDots[d.x - 1, d.y + 1].Own == 0 && arrDots[d.x - 1, d.y + 1].Blocked == false
                       select d;
            AddToList(ld, pat10, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 11;
            var pat11 = from Dot d in arrDots
                        where d.Own == Owner
                           && d.IndexRelation == arrDots[d.x - 2, d.y].IndexRelation
                           && arrDots[d.x - 2, d.y].Own == Owner && arrDots[d.x - 2, d.y].Blocked == false
                           && arrDots[d.x - 1, d.y].Own == 0 && arrDots[d.x - 1, d.y].Blocked == false
                           && arrDots[d.x - 1, d.y - 1].Own == 0 && arrDots[d.x - 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat11, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 12;
            var pat12 = from Dot d in arrDots
                         where d.Own == Owner
                         && d.IndexRelation == arrDots[d.x - 2, d.y - 1].IndexRelation
                         && arrDots[d.x - 2, d.y - 1].Own == Owner && arrDots[d.x - 2, d.y - 1].Blocked == false
                         && arrDots[d.x - 1, d.y].Own == 0 && arrDots[d.x - 1, d.y].Blocked == false
                         && arrDots[d.x - 1, d.y - 1].Own == 0 && arrDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            AddToList(ld, pat12, -1, 0);
            //-----------------------------------------------------------------------------
            iNumberPattern = 13;
            var pat13 = from Dot d in arrDots
                             where d.Own == Owner
                             && d.IndexRelation == arrDots[d.x - 1, d.y - 2].IndexRelation
                             && arrDots[d.x - 1, d.y - 2].Own == Owner && arrDots[d.x - 1, d.y - 2].Blocked == false
                             && arrDots[d.x, d.y - 1].Own == 0 && arrDots[d.x, d.y - 1].Blocked == false
                             && arrDots[d.x - 1, d.y - 1].Own == 0 && arrDots[d.x - 1, d.y - 1].Blocked == false
                             select d;
            AddToList(ld, pat13, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 14;
            var pat14 = from Dot d in arrDots
                               where d.Own == Owner
                               && d.IndexRelation == arrDots[d.x, d.y - 2].IndexRelation
                               && arrDots[d.x, d.y - 2].Own == Owner && arrDots[d.x, d.y - 2].Blocked == false
                               && arrDots[d.x, d.y - 1].Own == 0 && arrDots[d.x, d.y - 1].Blocked == false
                               && arrDots[d.x - 1, d.y - 1].Own == 0 && arrDots[d.x - 1, d.y - 1].Blocked == false
                               select d;
            AddToList(ld, pat14, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 15;
            var pat15 = from Dot d in arrDots
                        where d.Own == Owner
                        && d.IndexRelation == arrDots[d.x, d.y - 2].IndexRelation
                        && arrDots[d.x, d.y - 2].Own == Owner && arrDots[d.x, d.y - 2].Blocked == false
                        && arrDots[d.x, d.y - 1].Own == 0 && arrDots[d.x, d.y - 1].Blocked == false
                        && arrDots[d.x + 1, d.y - 1].Own == 0 && arrDots[d.x + 1, d.y - 1].Blocked == false
                        select d;
            AddToList(ld, pat15, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 16;
            var pat16 = from Dot d in arrDots
                         where d.Own == Owner
                            && d.IndexRelation == arrDots[d.x + 1, d.y - 2].IndexRelation
                            && arrDots[d.x + 1, d.y - 2].Own == Owner && arrDots[d.x + 1, d.y - 2].Blocked == false
                            && arrDots[d.x, d.y - 1].Own == 0 && arrDots[d.x, d.y - 1].Blocked == false
                            && arrDots[d.x + 1, d.y - 1].Own == 0 && arrDots[d.x + 1, d.y - 1].Blocked == false
                         select d;
            AddToList(ld, pat16, 0, -1);
            //-----------------------------------------------------------------------------
            iNumberPattern = 17;
            var pat17 = from Dot d in arrDots
                             where d.Own == Owner
                                && d.IndexRelation == arrDots[d.x + 2, d.y - 1].IndexRelation
                                && arrDots[d.x + 2, d.y - 1].Own == Owner && arrDots[d.x + 2, d.y - 1].Blocked == false
                                && arrDots[d.x + 1, d.y].Own == 0 && arrDots[d.x + 1, d.y].Blocked == false
                                && arrDots[d.x + 1, d.y - 1].Own == 0 && arrDots[d.x + 1, d.y - 1].Blocked == false
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
    }
}
