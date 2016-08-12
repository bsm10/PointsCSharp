using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DotsGame
{
    public partial class Game
    {
        private Dot CheckPattern(int Owner, ArrayDots _aDots, int x = 0)
        {

            int enemy_own = Owner == 1 ? 2 : 1;
            //ArrayDots _aDots = aDots.CopyArrayDots;
            Dot result_dot;

            IEnumerable<Dot> get_non_blocked = from Dot d in _aDots where d.Blocked == false select d; //получить коллекцию незаблокированных точек
            
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //                           *     +  ставить сюда 
            iNumberPattern = 3;
            var pat3 = from Dot d in get_non_blocked
                       where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                d.y > 2 && d.y < iBoardSize - 2 && 

                       _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                       _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                       _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                       _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                       _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false &&
                       _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                       select d;
            if (pat3.Count() > 0)
            {
                result_dot = new Dot(pat3.First().x + 1, pat3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //               cтавить сюда+     *   
            var pat3_1_1 = from Dot d in get_non_blocked
                           where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                    d.y > 2 && d.y < iBoardSize - 2 && 

                           _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                           _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                           _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                           _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false &&
                           _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false &&
                           _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                           _aDots[d.x, d.y].Own == Owner && _aDots[d.x, d.y].Blocked == false
                           select d;
            if (pat3_1_1.Count() > 0)
            {
                result_dot = new Dot(pat3_1_1.First().x - 1, pat3_1_1.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            // паттерн на конструкцию    +     *      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_2 = from Dot d in get_non_blocked
                         where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                  d.y > 2 && d.y < iBoardSize - 2 && 

                         _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                         _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                         _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                         _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                         _aDots[d.x - 1, d.y].Own != Owner && _aDots[d.x - 1, d.y].Blocked == false &&
                         _aDots[d.x + 1, d.y].Own != Owner && _aDots[d.x + 1, d.y].Blocked == false &&
                         _aDots[d.x, d.y + 1].Own != Owner && _aDots[d.x, d.y + 1].Blocked == false &&
                         _aDots[d.x, d.y - 1].Own != Owner && _aDots[d.x, d.y - 1].Blocked == false

                         select d;
            if (pat3_2.Count() > 0)
            {
                result_dot = new Dot(pat3_2.First().x - 1, pat3_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            // паттерн на конструкцию    *     +      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_3 = from Dot d in get_non_blocked
                         where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                  d.y > 2 && d.y < iBoardSize - 2 && 

                         _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                         _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                         _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                         _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                         _aDots[d.x - 1, d.y].Own != Owner && _aDots[d.x - 1, d.y].Blocked == false &&
                         _aDots[d.x + 1, d.y].Own != Owner && _aDots[d.x + 1, d.y].Blocked == false &&
                        _aDots[d.x, d.y + 1].Own != Owner && _aDots[d.x, d.y + 1].Blocked == false &&
                        _aDots[d.x, d.y - 1].Own != Owner && _aDots[d.x, d.y - 1].Blocked == false

                         select d;
            if (pat3_3.Count() > 0)
            {
                result_dot = new Dot(pat3_3.First().x + 1, pat3_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //паттерн на конструкцию    *     +      точка окружена через две точки
            //                            d+    
            //                          *     *   
            var pat3_4 = from Dot d in get_non_blocked
                         where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                         d.y > 2 && d.y < iBoardSize - 2 && 
                         _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                         _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                         _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                         _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                         _aDots[d.x - 1, d.y].Own != Owner && _aDots[d.x - 1, d.y].Blocked == false &&
                         _aDots[d.x + 1, d.y].Own != Owner && _aDots[d.x + 1, d.y].Blocked == false &&
                         _aDots[d.x, d.y + 1].Own != Owner && _aDots[d.x, d.y + 1].Blocked == false &&
                         _aDots[d.x, d.y - 1].Own != Owner && _aDots[d.x, d.y - 1].Blocked == false

                         select d;
            if (pat3_4.Count() > 0)
            {
                result_dot = new Dot(pat3_4.First().x + 1, pat3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 279;
            var pat279 = from Dot d in get_non_blocked
                         where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                  d.y > 2 && d.y < iBoardSize - 2 
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                         select d;
            if (pat279.Count() > 0)
            {
                result_dot = new Dot(pat279.First().x - 1, pat279.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat279_2 = from Dot d in get_non_blocked
                           where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                    d.y > 2 && d.y < iBoardSize - 2
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                           select d;
            if (pat279_2.Count() > 0)
            {
                result_dot = new Dot(pat279_2.First().x + 1, pat279_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat279_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                      d.y > 2 && d.y < iBoardSize - 2
                                  && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             select d;
            if (pat279_2_3.Count() > 0)
            {
                result_dot = new Dot(pat279_2_3.First().x - 1, pat279_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat279_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner && d.x > 2 && d.x < iBoardSize - 2 &&
                                                        d.y > 2 && d.y < iBoardSize - 2
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               select d;
            if (pat279_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat279_2_3_4.First().x + 1, pat279_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 883;
            var pat883 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat883.Count() > 0)
            {
                result_dot = new Dot(pat883.First().x - 1, pat883.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat883_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat883_2.Count() > 0)
            {
                result_dot = new Dot(pat883_2.First().x + 1, pat883_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat883_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat883_2_3.Count() > 0)
            {
                result_dot = new Dot(pat883_2_3.First().x, pat883_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat883_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat883_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat883_2_3_4.First().x, pat883_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 973;
            var pat973 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat973.Count() > 0)
            {
                result_dot = new Dot(pat973.First().x - 1, pat973.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat973_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat973_2.Count() > 0)
            {
                result_dot = new Dot(pat973_2.First().x + 1, pat973_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat973_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat973_2_3.Count() > 0)
            {
                result_dot = new Dot(pat973_2_3.First().x, pat973_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat973_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat973_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat973_2_3_4.First().x, pat973_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            iNumberPattern = 563;
            var pat563 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                         select d;
            if (pat563.Count() > 0)
            {
                result_dot = new Dot(pat563.First().x, pat563.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat563_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                           select d;
            if (pat563_2.Count() > 0)
            {
                result_dot = new Dot(pat563_2.First().x, pat563_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat563_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                             select d;
            if (pat563_2_3.Count() > 0)
            {
                result_dot = new Dot(pat563_2_3.First().x - 1, pat563_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat563_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                               select d;
            if (pat563_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat563_2_3_4.First().x + 1, pat563_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            //============================================================================================================== 

            //паттерн на конструкцию   *     *  +    
            //                           d+  *  +
            //                             
            iNumberPattern = 5;
            var pat5_1 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false &&
                                                _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false &&
                                                _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                         select d;
            if (pat5_1.Count() > 0)
            {

                result_dot = new Dot(pat5_1.First().x, pat5_1.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 225;
            var pat225 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat225.Count() > 0)
            {
                result_dot = new Dot(pat225.First().x + 1, pat225.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat225_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat225_2.Count() > 0)
            {
                result_dot = new Dot(pat225_2.First().x - 1, pat225_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat225_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat225_2_3.Count() > 0)
            {
                result_dot = new Dot(pat225_2_3.First().x, pat225_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat225_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat225_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat225_2_3_4.First().x, pat225_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 79;
            var pat79 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                            && _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                            && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                            && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                            && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                            && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                        select d;
            if (pat79.Count() > 0)
            {
                result_dot = new Dot(pat79.First().x + 1, pat79.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat79_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                              && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                              && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                              && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                          select d;
            if (pat79_2.Count() > 0)
            {
                result_dot = new Dot(pat79_2.First().x - 1, pat79_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat79_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                            select d;
            if (pat79_2_3.Count() > 0)
            {
                result_dot = new Dot(pat79_2_3.First().x - 1, pat79_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat79_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                                  && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                  && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                  && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                              select d;
            if (pat79_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat79_2_3_4.First().x + 1, pat79_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 19;
            var pat19 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                            && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y - 3].Own == enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                            && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                        select d;
            if (pat19.Count() > 0)
            {
                result_dot = new Dot(pat19.First().x - 1, pat19.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat19_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y + 3].Own == enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                              && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                          select d;
            if (pat19_2.Count() > 0)
            {
                result_dot = new Dot(pat19_2.First().x + 1, pat19_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat19_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x + 3, d.y].Own == enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                            select d;
            if (pat19_2_3.Count() > 0)
            {
                result_dot = new Dot(pat19_2_3.First().x + 1, pat19_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat19_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                  && _aDots[d.x - 3, d.y].Own == enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                              select d;
            if (pat19_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat19_2_3_4.First().x - 1, pat19_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 20;
            var pat20 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                            && _aDots[d.x - 3, d.y - 1].Own == enemy_own && _aDots[d.x - 3, d.y - 1].Blocked == false
                            && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                        select d;
            if (pat20.Count() > 0)
            {
                result_dot = new Dot(pat20.First().x - 1, pat20.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat20_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x + 3, d.y + 1].Own == enemy_own && _aDots[d.x + 3, d.y + 1].Blocked == false
                              && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                          select d;
            if (pat20_2.Count() > 0)
            {
                result_dot = new Dot(pat20_2.First().x + 1, pat20_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat20_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y + 3].Own == enemy_own && _aDots[d.x + 1, d.y + 3].Blocked == false
                                && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                            select d;
            if (pat20_2_3.Count() > 0)
            {
                result_dot = new Dot(pat20_2_3.First().x + 1, pat20_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat20_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 3].Own == enemy_own && _aDots[d.x - 1, d.y - 3].Blocked == false
                                  && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              select d;
            if (pat20_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat20_2_3_4.First().x - 1, pat20_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 667;
            var pat667 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat667.Count() > 0)
            {
                result_dot = new Dot(pat667.First().x + 1, pat667.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat667_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                           select d;
            if (pat667_2.Count() > 0)
            {
                result_dot = new Dot(pat667_2.First().x - 1, pat667_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat667_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             select d;
            if (pat667_2_3.Count() > 0)
            {
                result_dot = new Dot(pat667_2_3.First().x - 1, pat667_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat667_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               select d;
            if (pat667_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat667_2_3_4.First().x + 1, pat667_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 636;
            var pat636 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                         select d;
            if (pat636.Count() > 0)
            {
                result_dot = new Dot(pat636.First().x + 1, pat636.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat636_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                           select d;
            if (pat636_2.Count() > 0)
            {
                result_dot = new Dot(pat636_2.First().x - 1, pat636_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat636_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             select d;
            if (pat636_2_3.Count() > 0)
            {
                result_dot = new Dot(pat636_2_3.First().x + 1, pat636_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat636_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               select d;
            if (pat636_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat636_2_3_4.First().x - 1, pat636_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 918;
            var pat918 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat918.Count() > 0)
            {
                result_dot = new Dot(pat918.First().x, pat918.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat918_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                           select d;
            if (pat918_2.Count() > 0)
            {
                result_dot = new Dot(pat918_2.First().x, pat918_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat918_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat918_2_3.Count() > 0)
            {
                result_dot = new Dot(pat918_2_3.First().x + 1, pat918_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat918_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               select d;
            if (pat918_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat918_2_3_4.First().x - 1, pat918_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 242;
            var pat242 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                         select d;

            //180 Rotate=========================================================================================================== 
            var pat242_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                           select d;

            //--------------Rotate on 90----------------------------------- 
            var pat242_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             select d;

            //--------------Rotate on 90 - 2----------------------------------- 
            var pat242_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               select d;
            //============================================================================================================== 
            // d+  * 
            //  * m+  + 
            iNumberPattern = 901;
            var pat901 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            if (pat901.Count() > 0)
            {
                result_dot = new Dot(pat901.First().x + 1, pat901.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 180----------------------------------- 
            var pat901_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
&& _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                           select d;
            if (pat901_2.Count() > 0)
            {
                result_dot = new Dot(pat901_2.First().x - 1, pat901_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            // d+ m+  
            //  *  *  + 
            iNumberPattern = 604;
            var pat604 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            if (pat604.Count() > 0)
            {
                result_dot = new Dot(pat604.First().x + 1, pat604.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 180----------------------------------- 
            var pat604_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                           select d;
            if (pat604_2.Count() > 0)
            {
                result_dot = new Dot(pat604_2.First().x - 1, pat604_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            // d*  +    
            //    m*  *   
            iNumberPattern = 1117;
            var pat1117 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                          select d;
            if (pat1117.Count() > 0)
            {
                result_dot = new Dot(pat1117.First().x + 1, pat1117.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1117_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            select d;
            if (pat1117_2.Count() > 0)
            {
                result_dot = new Dot(pat1117_2.First().x - 1, pat1117_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1117_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              select d;
            if (pat1117_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1117_2_3.First().x - 1, pat1117_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1117_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                                    && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                    && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                select d;
            if (pat1117_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1117_2_3_4.First().x + 1, pat1117_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 549;
            //============================================================================================================== 
            var pat549 = from Dot d in get_non_blocked
                         where d.Own == Owner
                              && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                              && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                         select d;
            if (pat549.Count() > 0)
            {
                result_dot = new Dot(pat549.First().x - 1, pat549.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 180----------------------------------- 
            var pat549_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                              && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                              && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                              && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                           select d;
            if (pat549_2.Count() > 0)
            {

                result_dot = new Dot(pat549_2.First().x + 1, pat549_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 2883;
            var pat2883 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                              && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                          select d;
            if (pat2883.Count() > 0)
            {
                result_dot = new Dot(pat2883.First().x + 1, pat2883.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat2883_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == Owner && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                            select d;
            if (pat2883_2.Count() > 0)
            {
                result_dot = new Dot(pat2883_2.First().x - 1, pat2883_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat2883_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == Owner && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              select d;
            if (pat2883_2_3.Count() > 0)
            {
                result_dot = new Dot(pat2883_2_3.First().x + 1, pat2883_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat2883_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                    && _aDots[d.x, d.y + 2].Own == Owner && _aDots[d.x, d.y + 2].Blocked == false
                                    && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                    && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                    && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                select d;
            if (pat2883_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat2883_2_3_4.First().x - 1, pat2883_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 663;
            var pat663 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 3].Own == enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                         select d;
            if (pat663.Count() > 0)
            {
                result_dot = new Dot(pat663.First().x + 1, pat663.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat663_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 3].Own == enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                           select d;
            if (pat663_2.Count() > 0)
            {
                result_dot = new Dot(pat663_2.First().x - 1, pat663_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat663_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 3, d.y].Own == enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             select d;
            if (pat663_2_3.Count() > 0)
            {
                result_dot = new Dot(pat663_2_3.First().x + 1, pat663_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat663_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 3, d.y].Own == enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               select d;
            if (pat663_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat663_2_3_4.First().x - 1, pat663_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 364;
            var pat364 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat364.Count() > 0)
            {
                result_dot = new Dot(pat364.First().x + 1, pat364.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat364_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                           select d;
            if (pat364_2.Count() > 0)
            {
                result_dot = new Dot(pat364_2.First().x - 1, pat364_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat364_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat364_2_3.Count() > 0)
            {
                result_dot = new Dot(pat364_2_3.First().x + 1, pat364_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat364_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               select d;
            if (pat364_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat364_2_3_4.First().x - 1, pat364_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 222;
            var pat222 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 4].Own == Owner && _aDots[d.x, d.y - 4].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                             && _aDots[d.x - 1, d.y - 4].Own == 0 && _aDots[d.x - 1, d.y - 4].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                         select d;
            if (pat222.Count() > 0)
            {
                result_dot = new Dot(pat222.First().x - 1, pat222.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat222_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 4].Own == Owner && _aDots[d.x, d.y + 4].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                               && _aDots[d.x + 1, d.y + 4].Own == 0 && _aDots[d.x + 1, d.y + 4].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                           select d;
            if (pat222_2.Count() > 0)
            {
                result_dot = new Dot(pat222_2.First().x + 1, pat222_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat222_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 4, d.y].Own == Owner && _aDots[d.x + 4, d.y].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                                 && _aDots[d.x + 4, d.y + 1].Own == 0 && _aDots[d.x + 4, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                             select d;
            if (pat222_2_3.Count() > 0)
            {
                result_dot = new Dot(pat222_2_3.First().x + 1, pat222_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat222_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 4, d.y].Own == Owner && _aDots[d.x - 4, d.y].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                                   && _aDots[d.x - 4, d.y - 1].Own == 0 && _aDots[d.x - 4, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                               select d;
            if (pat222_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat222_2_3_4.First().x - 1, pat222_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 564;
            var pat564 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                             && _aDots[d.x + 4, d.y].Own == Owner && _aDots[d.x + 4, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                         select d;
            if (pat564.Count() > 0)
            {
                result_dot = new Dot(pat564.First().x + 2, pat564.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat564_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                               && _aDots[d.x - 4, d.y].Own == Owner && _aDots[d.x - 4, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                           select d;
            if (pat564_2.Count() > 0)
            {
                result_dot = new Dot(pat564_2.First().x - 2, pat564_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat564_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                                 && _aDots[d.x, d.y - 4].Own == Owner && _aDots[d.x, d.y - 4].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             select d;
            if (pat564_2_3.Count() > 0)
            {
                result_dot = new Dot(pat564_2_3.First().x + 1, pat564_2_3.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat564_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                                   && _aDots[d.x, d.y + 4].Own == Owner && _aDots[d.x, d.y + 4].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               select d;
            if (pat564_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat564_2_3_4.First().x - 1, pat564_2_3_4.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 935;
            var pat935 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 3, d.y - 3].Own == Owner && _aDots[d.x - 3, d.y - 3].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                             && _aDots[d.x - 3, d.y - 2].Own == 0 && _aDots[d.x - 3, d.y - 2].Blocked == false
                             && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                             && _aDots[d.x - 2, d.y - 3].Own == 0 && _aDots[d.x - 2, d.y - 3].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat935.Count() > 0)
            {
                result_dot = new Dot(pat935.First().x - 2, pat935.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat935_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 3, d.y + 3].Own == Owner && _aDots[d.x + 3, d.y + 3].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                               && _aDots[d.x + 3, d.y + 2].Own == 0 && _aDots[d.x + 3, d.y + 2].Blocked == false
                               && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                               && _aDots[d.x + 2, d.y + 3].Own == 0 && _aDots[d.x + 2, d.y + 3].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat935_2.Count() > 0)
            {
                result_dot = new Dot(pat935_2.First().x + 2, pat935_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat935_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 3, d.y + 3].Own == Owner && _aDots[d.x + 3, d.y + 3].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                                 && _aDots[d.x + 2, d.y + 3].Own == 0 && _aDots[d.x + 2, d.y + 3].Blocked == false
                                 && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                 && _aDots[d.x + 3, d.y + 2].Own == 0 && _aDots[d.x + 3, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat935_2_3.Count() > 0)
            {
                result_dot = new Dot(pat935_2_3.First().x + 1, pat935_2_3.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat935_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 3, d.y - 3].Own == Owner && _aDots[d.x - 3, d.y - 3].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                                   && _aDots[d.x - 2, d.y - 3].Own == 0 && _aDots[d.x - 2, d.y - 3].Blocked == false
                                   && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                   && _aDots[d.x - 3, d.y - 2].Own == 0 && _aDots[d.x - 3, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat935_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat935_2_3_4.First().x - 1, pat935_2_3_4.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 936;
            var pat936 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                             && _aDots[d.x + 3, d.y - 2].Own == 0 && _aDots[d.x + 3, d.y - 2].Blocked == false
                             && _aDots[d.x + 3, d.y - 3].Own == Owner && _aDots[d.x + 3, d.y - 3].Blocked == false
                             && _aDots[d.x + 2, d.y - 3].Own == 0 && _aDots[d.x + 2, d.y - 3].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                         select d;
            if (pat936.Count() > 0)
            {
                result_dot = new Dot(pat936.First().x + 2, pat936.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat936_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                               && _aDots[d.x - 3, d.y + 2].Own == 0 && _aDots[d.x - 3, d.y + 2].Blocked == false
                               && _aDots[d.x - 3, d.y + 3].Own == Owner && _aDots[d.x - 3, d.y + 3].Blocked == false
                               && _aDots[d.x - 2, d.y + 3].Own == 0 && _aDots[d.x - 2, d.y + 3].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                           select d;
            if (pat936_2.Count() > 0)
            {
                result_dot = new Dot(pat936_2.First().x - 2, pat936_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat936_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                 && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 3].Own == 0 && _aDots[d.x + 2, d.y - 3].Blocked == false
                                 && _aDots[d.x + 3, d.y - 3].Own == Owner && _aDots[d.x + 3, d.y - 3].Blocked == false
                                 && _aDots[d.x + 3, d.y - 2].Own == 0 && _aDots[d.x + 3, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                             select d;
            if (pat936_2_3.Count() > 0)
            {
                result_dot = new Dot(pat936_2_3.First().x + 1, pat936_2_3.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat936_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                   && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y + 3].Own == 0 && _aDots[d.x - 2, d.y + 3].Blocked == false
                                   && _aDots[d.x - 3, d.y + 3].Own == Owner && _aDots[d.x - 3, d.y + 3].Blocked == false
                                   && _aDots[d.x - 3, d.y + 2].Own == 0 && _aDots[d.x - 3, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                               select d;
            if (pat936_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat936_2_3_4.First().x - 1, pat936_2_3_4.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 598;
            var pat598 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 5, d.y - 3].Own == Owner && _aDots[d.x + 5, d.y - 3].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             && _aDots[d.x + 3, d.y - 2].Own == 0 && _aDots[d.x + 3, d.y - 2].Blocked == false
                             && _aDots[d.x + 4, d.y - 1].Own == 0 && _aDots[d.x + 4, d.y - 1].Blocked == false
                             && _aDots[d.x + 4, d.y - 2].Own == 0 && _aDots[d.x + 4, d.y - 2].Blocked == false
                             && _aDots[d.x + 4, d.y - 3].Own == 0 && _aDots[d.x + 4, d.y - 3].Blocked == false
                             && _aDots[d.x + 5, d.y - 2].Own == 0 && _aDots[d.x + 5, d.y - 2].Blocked == false
                             && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                             && _aDots[d.x + 4, d.y].Own == 0 && _aDots[d.x + 4, d.y].Blocked == false
                             && _aDots[d.x + 5, d.y - 1].Own == 0 && _aDots[d.x + 5, d.y - 1].Blocked == false
                         select d;
            if (pat598.Count() > 0)
            {
                result_dot = new Dot(pat598.First().x + 3, pat598.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat598_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 5, d.y + 3].Own == Owner && _aDots[d.x - 5, d.y + 3].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               && _aDots[d.x - 3, d.y + 2].Own == 0 && _aDots[d.x - 3, d.y + 2].Blocked == false
                               && _aDots[d.x - 4, d.y + 1].Own == 0 && _aDots[d.x - 4, d.y + 1].Blocked == false
                               && _aDots[d.x - 4, d.y + 2].Own == 0 && _aDots[d.x - 4, d.y + 2].Blocked == false
                               && _aDots[d.x - 4, d.y + 3].Own == 0 && _aDots[d.x - 4, d.y + 3].Blocked == false
                               && _aDots[d.x - 5, d.y + 2].Own == 0 && _aDots[d.x - 5, d.y + 2].Blocked == false
                               && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                               && _aDots[d.x - 4, d.y].Own == 0 && _aDots[d.x - 4, d.y].Blocked == false
                               && _aDots[d.x - 5, d.y + 1].Own == 0 && _aDots[d.x - 5, d.y + 1].Blocked == false
                           select d;
            if (pat598_2.Count() > 0)
            {
                result_dot = new Dot(pat598_2.First().x - 3, pat598_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat598_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 3, d.y - 5].Own == Owner && _aDots[d.x + 3, d.y - 5].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                 && _aDots[d.x + 2, d.y - 3].Own == 0 && _aDots[d.x + 2, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y - 4].Own == 0 && _aDots[d.x + 1, d.y - 4].Blocked == false
                                 && _aDots[d.x + 2, d.y - 4].Own == 0 && _aDots[d.x + 2, d.y - 4].Blocked == false
                                 && _aDots[d.x + 3, d.y - 4].Own == 0 && _aDots[d.x + 3, d.y - 4].Blocked == false
                                 && _aDots[d.x + 2, d.y - 5].Own == 0 && _aDots[d.x + 2, d.y - 5].Blocked == false
                                 && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                                 && _aDots[d.x, d.y - 4].Own == 0 && _aDots[d.x, d.y - 4].Blocked == false
                                 && _aDots[d.x + 1, d.y - 5].Own == 0 && _aDots[d.x + 1, d.y - 5].Blocked == false
                             select d;
            if (pat598_2_3.Count() > 0)
            {
                result_dot = new Dot(pat598_2_3.First().x + 1, pat598_2_3.First().y - 3);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat598_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 3, d.y + 5].Own == Owner && _aDots[d.x - 3, d.y + 5].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                   && _aDots[d.x - 2, d.y + 3].Own == 0 && _aDots[d.x - 2, d.y + 3].Blocked == false
                                   && _aDots[d.x - 1, d.y + 4].Own == 0 && _aDots[d.x - 1, d.y + 4].Blocked == false
                                   && _aDots[d.x - 2, d.y + 4].Own == 0 && _aDots[d.x - 2, d.y + 4].Blocked == false
                                   && _aDots[d.x - 3, d.y + 4].Own == 0 && _aDots[d.x - 3, d.y + 4].Blocked == false
                                   && _aDots[d.x - 2, d.y + 5].Own == 0 && _aDots[d.x - 2, d.y + 5].Blocked == false
                                   && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                                   && _aDots[d.x, d.y + 4].Own == 0 && _aDots[d.x, d.y + 4].Blocked == false
                                   && _aDots[d.x - 1, d.y + 5].Own == 0 && _aDots[d.x - 1, d.y + 5].Blocked == false
                               select d;
            if (pat598_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat598_2_3_4.First().x - 1, pat598_2_3_4.First().y + 3);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 420;
            var pat420 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat420.Count() > 0)
            {
                result_dot = new Dot(pat420.First().x + 1, pat420.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat420_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                           select d;
            if (pat420_2.Count() > 0)
            {
                result_dot = new Dot(pat420_2.First().x - 1, pat420_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat420_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             select d;
            if (pat420_2_3.Count() > 0)
            {
                result_dot = new Dot(pat420_2_3.First().x + 1, pat420_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat420_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               select d;
            if (pat420_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat420_2_3_4.First().x - 1, pat420_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            //============================================================================================================== 
            iNumberPattern = 670;
            var pat670 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 4].Own == 0 && _aDots[d.x, d.y - 4].Blocked == false
                             && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own != enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own != enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 3].Own != enemy_own && _aDots[d.x + 1, d.y - 3].Blocked == false
                         select d;
            if (pat670.Count() > 0)
            {
                result_dot = new Dot(pat670.First().x - 1, pat670.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat670_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 4].Own == 0 && _aDots[d.x, d.y + 4].Blocked == false
                               && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own != enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own != enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 3].Own != enemy_own && _aDots[d.x - 1, d.y + 3].Blocked == false
                           select d;
            if (pat670_2.Count() > 0)
            {
                result_dot = new Dot(pat670_2.First().x + 1, pat670_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat670_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 4, d.y].Own == 0 && _aDots[d.x + 4, d.y].Blocked == false
                                 && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own != enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own != enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 3, d.y - 1].Own != enemy_own && _aDots[d.x + 3, d.y - 1].Blocked == false
                             select d;
            if (pat670_2_3.Count() > 0)
            {
                result_dot = new Dot(pat670_2_3.First().x + 1, pat670_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat670_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 4, d.y].Own == 0 && _aDots[d.x - 4, d.y].Blocked == false
                                   && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own != enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own != enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 3, d.y + 1].Own != enemy_own && _aDots[d.x - 3, d.y + 1].Blocked == false
                               select d;
            if (pat670_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat670_2_3_4.First().x - 1, pat670_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //============================================================================================================== 
            iNumberPattern = 1883;
            var pat1883 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                              && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                          select d;
            if (pat1883.Count() > 0)
            {
                result_dot = new Dot(pat1883.First().x + 1, pat1883.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1883_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                                && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                            select d;
            if (pat1883_2.Count() > 0)
            {
                result_dot = new Dot(pat1883_2.First().x - 1, pat1883_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1883_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                                  && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                  && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                              select d;
            if (pat1883_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1883_2_3.First().x + 1, pat1883_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1883_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                    && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                    && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                                    && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                    && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                    && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                select d;
            if (pat1883_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1883_2_3_4.First().x - 1, pat1883_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            //============================================================================================================== 

            iNumberPattern = 528;
            var pat528 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y].Own == enemy_own
                             && _aDots[d.x, d.y + 1].Own == enemy_own
                             && _aDots[d.x + 1, d.y + 1].Own == 0
                             && _aDots[d.x + 1, d.y].Own == 0
                             && _aDots[d.x + 2, d.y].Own == enemy_own
                             && _aDots[d.x + 1, d.y - 1].Own == 0
                             && _aDots[d.x, d.y - 1].Own == 0
                             && _aDots[d.x - 1, d.y + 1].Own == Owner
                             && _aDots[d.x, d.y + 2].Own == 0
                         select d;
            if (pat528.Count() > 0)
            {
                result_dot = new Dot(pat528.First().x + 1, pat528.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat528_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y].Own == enemy_own
                               && _aDots[d.x, d.y - 1].Own == enemy_own
                               && _aDots[d.x - 1, d.y - 1].Own == 0
                               && _aDots[d.x - 1, d.y].Own == 0
                               && _aDots[d.x - 2, d.y].Own == enemy_own
                               && _aDots[d.x - 1, d.y + 1].Own == 0
                               && _aDots[d.x, d.y + 1].Own == 0
                               && _aDots[d.x + 1, d.y - 1].Own == Owner
                               && _aDots[d.x, d.y - 2].Own == 0
                           select d;
            if (pat528_2.Count() > 0)
            {
                result_dot = new Dot(pat528_2.First().x - 1, pat528_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat528_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y + 1].Own == enemy_own
                                 && _aDots[d.x - 1, d.y].Own == enemy_own
                                 && _aDots[d.x - 1, d.y - 1].Own == 0
                                 && _aDots[d.x, d.y - 1].Own == 0
                                 && _aDots[d.x, d.y - 2].Own == enemy_own
                                 && _aDots[d.x + 1, d.y - 1].Own == 0
                                 && _aDots[d.x + 1, d.y].Own == 0
                                 && _aDots[d.x - 1, d.y + 1].Own == Owner
                                 && _aDots[d.x - 2, d.y].Own == 0
                             select d;
            if (pat528_2_3.Count() > 0)
            {
                result_dot = new Dot(pat528_2_3.First().x - 1, pat528_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat528_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y - 1].Own == enemy_own
                                   && _aDots[d.x + 1, d.y].Own == enemy_own
                                   && _aDots[d.x + 1, d.y + 1].Own == 0
                                   && _aDots[d.x, d.y + 1].Own == 0
                                   && _aDots[d.x, d.y + 2].Own == enemy_own
                                   && _aDots[d.x - 1, d.y + 1].Own == 0
                                   && _aDots[d.x - 1, d.y].Own == 0
                                   && _aDots[d.x + 1, d.y - 1].Own == Owner
                                   && _aDots[d.x + 2, d.y].Own == 0
                               select d;
            if (pat528_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat528_2_3_4.First().x + 1, pat528_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 490;
            var pat490 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y - 1].Own == Owner
                             && _aDots[d.x + 1, d.y].Own == Owner
                             && _aDots[d.x, d.y - 1].Own == enemy_own
                             && _aDots[d.x + 1, d.y - 1].Own == 0
                             && _aDots[d.x + 2, d.y - 1].Own == enemy_own
                             && _aDots[d.x, d.y - 2].Own == 0
                             && _aDots[d.x + 1, d.y - 2].Own == 0
                             && _aDots[d.x + 2, d.y - 2].Own == 0
                         select d;
            if (pat490.Count() > 0)
            {
                result_dot = new Dot(pat490.First().x + 1, pat490.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat490_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y + 1].Own == Owner
                               && _aDots[d.x - 1, d.y].Own == Owner
                               && _aDots[d.x, d.y + 1].Own == enemy_own
                               && _aDots[d.x - 1, d.y + 1].Own == 0
                               && _aDots[d.x - 2, d.y + 1].Own == enemy_own
                               && _aDots[d.x, d.y + 2].Own == 0
                               && _aDots[d.x - 1, d.y + 2].Own == 0
                               && _aDots[d.x - 2, d.y + 2].Own == 0
                           select d;
            if (pat490_2.Count() > 0)
            {
                result_dot = new Dot(pat490_2.First().x - 1, pat490_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat490_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == Owner
                                 && _aDots[d.x + 1, d.y].Own == enemy_own
                                 && _aDots[d.x + 1, d.y - 1].Own == 0
                                 && _aDots[d.x + 1, d.y - 2].Own == enemy_own
                                 && _aDots[d.x + 2, d.y].Own == 0
                                 && _aDots[d.x + 2, d.y - 1].Own == 0
                                 && _aDots[d.x + 2, d.y - 2].Own == 0
                             select d;
            if (pat490_2_3.Count() > 0)
            {
                result_dot = new Dot(pat490_2_3.First().x + 1, pat490_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat490_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == Owner
                                   && _aDots[d.x - 1, d.y].Own == enemy_own
                                   && _aDots[d.x - 1, d.y + 1].Own == 0
                                   && _aDots[d.x - 1, d.y + 2].Own == enemy_own
                                   && _aDots[d.x - 2, d.y].Own == 0
                                   && _aDots[d.x - 2, d.y + 1].Own == 0
                                   && _aDots[d.x - 2, d.y + 2].Own == 0
                               select d;
            if (pat490_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat490_2_3_4.First().x - 1, pat490_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 378;
            var pat378 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 2, d.y - 1].Own == enemy_own
&& _aDots[d.x - 2, d.y].Own == enemy_own
&& _aDots[d.x - 1, d.y].Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == Owner
&& _aDots[d.x + 1, d.y].Own == enemy_own
&& _aDots[d.x - 1, d.y - 1].Own == 0
&& _aDots[d.x, d.y - 1].Own == 0
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x + 2, d.y].Own == 0
                         select d;
            if (pat378.Count() > 0)
            {
                result_dot = new Dot(pat378.First().x + 1, pat378.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat378_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 2, d.y + 1].Own == enemy_own
&& _aDots[d.x + 2, d.y].Own == enemy_own
&& _aDots[d.x + 1, d.y].Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == Owner
&& _aDots[d.x - 1, d.y].Own == enemy_own
&& _aDots[d.x + 1, d.y + 1].Own == 0
&& _aDots[d.x, d.y + 1].Own == 0
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x - 2, d.y].Own == 0
                           select d;
            if (pat378_2.Count() > 0)
            {
                result_dot = new Dot(pat378_2.First().x - 1, pat378_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat378_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x + 1, d.y + 2].Own == enemy_own
&& _aDots[d.x, d.y + 2].Own == enemy_own
&& _aDots[d.x, d.y + 1].Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == Owner
&& _aDots[d.x, d.y - 1].Own == enemy_own
&& _aDots[d.x + 1, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y].Own == 0
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x, d.y - 2].Own == 0
                             select d;
            if (pat378_2_3.Count() > 0)
            {
                result_dot = new Dot(pat378_2_3.First().x + 1, pat378_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat378_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x - 1, d.y - 2].Own == enemy_own
&& _aDots[d.x, d.y - 2].Own == enemy_own
&& _aDots[d.x, d.y - 1].Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == Owner
&& _aDots[d.x, d.y + 1].Own == enemy_own
&& _aDots[d.x - 1, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y].Own == 0
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x, d.y + 2].Own == 0
                               select d;
            if (pat378_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat378_2_3_4.First().x - 1, pat378_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 472;
            var pat472 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 1].Own == enemy_own
                             && _aDots[d.x + 1, d.y].Own == enemy_own
                             && _aDots[d.x, d.y + 1].Own == Owner
                             && _aDots[d.x + 1, d.y + 1].Own == 0
                             && _aDots[d.x + 1, d.y + 2].Own == 0
                             && _aDots[d.x, d.y + 2].Own == 0
                             && _aDots[d.x, d.y + 3].Own == enemy_own
                             && _aDots[d.x + 1, d.y + 3].Own == 0
                             && _aDots[d.x + 2, d.y + 1].Own == 0
                             && _aDots[d.x + 2, d.y + 2].Own == 0
                         select d;
            if (pat472.Count() > 0)
            {
                result_dot = new Dot(pat472.First().x + 1, pat472.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat472_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 1].Own == enemy_own
                               && _aDots[d.x - 1, d.y].Own == enemy_own
                               && _aDots[d.x, d.y - 1].Own == Owner
                               && _aDots[d.x - 1, d.y - 1].Own == 0
                               && _aDots[d.x - 1, d.y - 2].Own == 0
                               && _aDots[d.x, d.y - 2].Own == 0
                               && _aDots[d.x, d.y - 3].Own == enemy_own
                               && _aDots[d.x - 1, d.y - 3].Own == 0
                               && _aDots[d.x - 2, d.y - 1].Own == 0
                               && _aDots[d.x - 2, d.y - 2].Own == 0
                           select d;
            if (pat472_2.Count() > 0)
            {
                result_dot = new Dot(pat472_2.First().x - 1, pat472_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat472_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y].Own == enemy_own
                                 && _aDots[d.x, d.y - 1].Own == enemy_own
                                 && _aDots[d.x - 1, d.y].Own == Owner
                                 && _aDots[d.x - 1, d.y - 1].Own == 0
                                 && _aDots[d.x - 2, d.y - 1].Own == 0
                                 && _aDots[d.x - 2, d.y].Own == 0
                                 && _aDots[d.x - 3, d.y].Own == enemy_own
                                 && _aDots[d.x - 3, d.y - 1].Own == 0
                                 && _aDots[d.x - 1, d.y - 2].Own == 0
                                 && _aDots[d.x - 2, d.y - 2].Own == 0
                             select d;
            if (pat472_2_3.Count() > 0)
            {
                result_dot = new Dot(pat472_2_3.First().x - 1, pat472_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat472_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y].Own == enemy_own
                                   && _aDots[d.x, d.y + 1].Own == enemy_own
                                   && _aDots[d.x + 1, d.y].Own == Owner
                                   && _aDots[d.x + 1, d.y + 1].Own == 0
                                   && _aDots[d.x + 2, d.y + 1].Own == 0
                                   && _aDots[d.x + 2, d.y].Own == 0
                                   && _aDots[d.x + 3, d.y].Own == enemy_own
                                   && _aDots[d.x + 3, d.y + 1].Own == 0
                                   && _aDots[d.x + 1, d.y + 2].Own == 0
                                   && _aDots[d.x + 2, d.y + 2].Own == 0
                               select d;
            if (pat472_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat472_2_3_4.First().x + 1, pat472_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 758;
            var pat758 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
&& _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                         select d;
            if (pat758.Count() > 0)
            {
                result_dot = new Dot(pat758.First().x - 1, pat758.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat758_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
&& _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                           select d;
            if (pat758_2.Count() > 0)
            {
                result_dot = new Dot(pat758_2.First().x + 1, pat758_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat758_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             select d;
            if (pat758_2_3.Count() > 0)
            {
                result_dot = new Dot(pat758_2_3.First().x + 1, pat758_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat758_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
&& _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               select d;
            if (pat758_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat758_2_3_4.First().x - 1, pat758_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 917;
            var pat917 = from Dot d in _aDots
                         where d.Own == 0
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat917.Count() > 0)
            {
                result_dot = new Dot(pat917.First().x, pat917.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat917_2 = from Dot d in _aDots
                           where d.Own == 0
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat917_2.Count() > 0)
            {
                result_dot = new Dot(pat917_2.First().x, pat917_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 276;
            var pat276 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                         select d;
            if (pat276.Count() > 0)
            {
                result_dot = new Dot(pat276.First().x + 1, pat276.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat276_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                           select d;

            if (pat276_2.Count() > 0)
            {
                result_dot = new Dot(pat276_2.First().x - 1, pat276_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat276_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                             select d;
            if (pat276_2_3.Count() > 0)
            {
                result_dot = new Dot(pat276_2_3.First().x, pat276_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat276_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                               select d;
            if (pat276_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat276_2_3_4.First().x, pat276_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 475;
            var pat475 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == Owner
                             && _aDots[d.x + 2, d.y - 1].Own == Owner
                             && _aDots[d.x + 2, d.y - 2].Own == Owner
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own
                             && _aDots[d.x, d.y - 1].Own == enemy_own
                             && _aDots[d.x, d.y - 2].Own == Owner
                             && _aDots[d.x - 1, d.y - 1].Own == 0
                             && _aDots[d.x + 1, d.y - 2].Own == 0
                         select d;
            if (pat475.Count() > 0)
            {
                result_dot = new Dot(pat475.First().x - 1, pat475.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat475_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == Owner
                               && _aDots[d.x - 2, d.y + 1].Own == Owner
                               && _aDots[d.x - 2, d.y + 2].Own == Owner
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own
                               && _aDots[d.x, d.y + 1].Own == enemy_own
                               && _aDots[d.x, d.y + 2].Own == Owner
                               && _aDots[d.x + 1, d.y + 1].Own == 0
                               && _aDots[d.x - 1, d.y + 2].Own == 0
                           select d;
            if (pat475_2.Count() > 0)
            {
                result_dot = new Dot(pat475_2.First().x + 1, pat475_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat475_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == Owner
                                 && _aDots[d.x + 1, d.y - 2].Own == Owner
                                 && _aDots[d.x + 2, d.y - 2].Own == Owner
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own
                                 && _aDots[d.x + 1, d.y].Own == enemy_own
                                 && _aDots[d.x + 2, d.y].Own == Owner
                                 && _aDots[d.x + 1, d.y + 1].Own == 0
                                 && _aDots[d.x + 2, d.y - 1].Own == 0
                             select d;
            if (pat475_2_3.Count() > 0)
            {
                result_dot = new Dot(pat475_2_3.First().x + 1, pat475_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat475_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == Owner
                                   && _aDots[d.x - 1, d.y + 2].Own == Owner
                                   && _aDots[d.x - 2, d.y + 2].Own == Owner
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own
                                   && _aDots[d.x - 1, d.y].Own == enemy_own
                                   && _aDots[d.x - 2, d.y].Own == Owner
                                   && _aDots[d.x - 1, d.y - 1].Own == 0
                                   && _aDots[d.x - 2, d.y + 1].Own == 0
                               select d;
            if (pat475_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat475_2_3_4.First().x - 1, pat475_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 385;
            var pat385 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat385.Count() > 0)
            {
                result_dot = new Dot(pat385.First().x - 1, pat385.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat385_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat385_2.Count() > 0)
            {
                result_dot = new Dot(pat385_2.First().x + 1, pat385_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat385_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat385_2_3.Count() > 0)
            {
                result_dot = new Dot(pat385_2_3.First().x - 1, pat385_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat385_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat385_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat385_2_3_4.First().x + 1, pat385_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 386;
            var pat386 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                         select d;
            if (pat386.Count() > 0)
            {
                result_dot = new Dot(pat386.First().x + 1, pat386.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat386_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                           select d;
            if (pat386_2.Count() > 0)
            {
                result_dot = new Dot(pat386_2.First().x - 1, pat386_2.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat386_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             select d;
            if (pat386_2_3.Count() > 0)
            {
                result_dot = new Dot(pat386_2_3.First().x + 2, pat386_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat386_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               select d;
            if (pat386_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat386_2_3_4.First().x - 2, pat386_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 879;
            var pat879 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 3, d.y - 3].Own == Owner && _aDots[d.x - 3, d.y - 3].Blocked == false
&& _aDots[d.x - 3, d.y - 2].Own == Owner && _aDots[d.x - 3, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 2, d.y - 2].Own == enemy_own && _aDots[d.x - 2, d.y - 2].Blocked == false
&& _aDots[d.x - 2, d.y - 3].Own == 0 && _aDots[d.x - 2, d.y - 3].Blocked == false
&& _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
&& _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
&& _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
&& _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
&& _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat879.Count() > 0)
            {
                result_dot = new Dot(pat879.First().x, pat879.First().y - 3);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat879_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 3, d.y + 3].Own == Owner && _aDots[d.x + 3, d.y + 3].Blocked == false
&& _aDots[d.x + 3, d.y + 2].Own == Owner && _aDots[d.x + 3, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 2, d.y + 2].Own == enemy_own && _aDots[d.x + 2, d.y + 2].Blocked == false
&& _aDots[d.x + 2, d.y + 3].Own == 0 && _aDots[d.x + 2, d.y + 3].Blocked == false
&& _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
&& _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
&& _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
&& _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
&& _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat879_2.Count() > 0)
            {
                result_dot = new Dot(pat879_2.First().x, pat879_2.First().y + 3);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat879_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner && d.x + 4 < iBoardSize && d.x - 4 > 0 && d.y + 4 < iBoardSize && d.y - 4 > 0
&& _aDots[d.x + 3, d.y + 3].Own == Owner && _aDots[d.x + 3, d.y + 3].Blocked == false
&& _aDots[d.x + 2, d.y + 3].Own == Owner && _aDots[d.x + 2, d.y + 3].Blocked == false
&& _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y + 2].Own == enemy_own && _aDots[d.x + 2, d.y + 2].Blocked == false
&& _aDots[d.x + 3, d.y + 2].Own == 0 && _aDots[d.x + 3, d.y + 2].Blocked == false
&& _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
&& _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat879_2_3.Count() > 0)
            {
                result_dot = new Dot(pat879_2_3.First().x + 3, pat879_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat879_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                && _aDots[d.x - 3, d.y - 3].Own == Owner && _aDots[d.x - 3, d.y - 3].Blocked == false
                                && _aDots[d.x - 2, d.y - 3].Own == Owner && _aDots[d.x - 2, d.y - 3].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 2].Own == enemy_own && _aDots[d.x - 2, d.y - 2].Blocked == false
                                && _aDots[d.x - 3, d.y - 2].Own == 0 && _aDots[d.x - 3, d.y - 2].Blocked == false
                                && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat879_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat879_2_3_4.First().x - 3, pat879_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 823;
            var pat823 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
&& _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat823.Count() > 0)
            {
                result_dot = new Dot(pat823.First().x - 1, pat823.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat823_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat823_2.Count() > 0)
            {
                result_dot = new Dot(pat823_2.First().x + 1, pat823_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat823_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
&& _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat823_2_3.Count() > 0)
            {
                result_dot = new Dot(pat823_2_3.First().x, pat823_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat823_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
&& _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat823_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat823_2_3_4.First().x, pat823_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 555;
            var pat555 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                         select d;
            if (pat555.Count() > 0)
            {
                result_dot = new Dot(pat555.First().x + 1, pat555.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat555_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                           select d;
            if (pat555_2.Count() > 0)
            {
                result_dot = new Dot(pat555_2.First().x - 1, pat555_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat555_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             select d;
            if (pat555_2_3.Count() > 0)
            {
                result_dot = new Dot(pat555_2_3.First().x + 1, pat555_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat555_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               select d;
            if (pat555_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat555_2_3_4.First().x - 1, pat555_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 753;
            var pat753 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 1, d.y].Own == Owner
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own
&& _aDots[d.x - 1, d.y + 2].Own == enemy_own
&& _aDots[d.x - 1, d.y + 3].Own == enemy_own
&& _aDots[d.x - 1, d.y + 4].Own == Owner
&& _aDots[d.x, d.y + 1].Own == 0
&& _aDots[d.x, d.y + 2].Own == 0
&& _aDots[d.x, d.y + 3].Own == 0
&& _aDots[d.x + 1, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y + 2].Own == 0
&& _aDots[d.x + 1, d.y + 3].Own == 0
                         select d;
            if (pat753.Count() > 0)
            {
                result_dot = new Dot(pat753.First().x, pat753.First().y + 3);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat753_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 1, d.y].Own == Owner
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own
&& _aDots[d.x + 1, d.y - 2].Own == enemy_own
&& _aDots[d.x + 1, d.y - 3].Own == enemy_own
&& _aDots[d.x + 1, d.y - 4].Own == Owner
&& _aDots[d.x, d.y - 1].Own == 0
&& _aDots[d.x, d.y - 2].Own == 0
&& _aDots[d.x, d.y - 3].Own == 0
&& _aDots[d.x - 1, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y - 2].Own == 0
&& _aDots[d.x - 1, d.y - 3].Own == 0
                           select d;
            if (pat753_2.Count() > 0)
            {
                result_dot = new Dot(pat753_2.First().x, pat753_2.First().y - 3);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat753_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x, d.y + 1].Own == Owner
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own
&& _aDots[d.x - 2, d.y + 1].Own == enemy_own
&& _aDots[d.x - 3, d.y + 1].Own == enemy_own
&& _aDots[d.x - 4, d.y + 1].Own == Owner
&& _aDots[d.x - 1, d.y].Own == 0
&& _aDots[d.x - 2, d.y].Own == 0
&& _aDots[d.x - 3, d.y].Own == 0
&& _aDots[d.x - 1, d.y - 1].Own == 0
&& _aDots[d.x - 2, d.y - 1].Own == 0
&& _aDots[d.x - 3, d.y - 1].Own == 0
                             select d;
            if (pat753_2_3.Count() > 0)
            {
                result_dot = new Dot(pat753_2_3.First().x - 3, pat753_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat753_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x, d.y - 1].Own == Owner
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own
&& _aDots[d.x + 2, d.y - 1].Own == enemy_own
&& _aDots[d.x + 3, d.y - 1].Own == enemy_own
&& _aDots[d.x + 4, d.y - 1].Own == Owner
&& _aDots[d.x + 1, d.y].Own == 0
&& _aDots[d.x + 2, d.y].Own == 0
&& _aDots[d.x + 3, d.y].Own == 0
&& _aDots[d.x + 1, d.y + 1].Own == 0
&& _aDots[d.x + 2, d.y + 1].Own == 0
&& _aDots[d.x + 3, d.y + 1].Own == 0
                               select d;
            if (pat753_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat753_2_3_4.First().x + 3, pat753_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 673;
            var pat673 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat673.Count() > 0)
            {
                result_dot = new Dot(pat673.First().x - 1, pat673.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat673_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat673_2.Count() > 0)
            {
                result_dot = new Dot(pat673_2.First().x + 1, pat673_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat673_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat673_2_3.Count() > 0)
            {
                result_dot = new Dot(pat673_2_3.First().x + 1, pat673_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat673_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat673_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat673_2_3_4.First().x - 1, pat673_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 389;
            var pat389 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own
&& _aDots[d.x - 1, d.y].Own == 0
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own
&& _aDots[d.x, d.y + 2].Own == 0
&& _aDots[d.x - 2, d.y].Own == 0
&& _aDots[d.x - 1, d.y + 2].Own == 0
&& _aDots[d.x - 2, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y].Own == Owner
                         select d;
            if (pat389.Count() > 0)
            {
                result_dot = new Dot(pat389.First().x - 1, pat389.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat389_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own
&& _aDots[d.x + 1, d.y].Own == 0
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own
&& _aDots[d.x, d.y - 2].Own == 0
&& _aDots[d.x + 2, d.y].Own == 0
&& _aDots[d.x + 1, d.y - 2].Own == 0
&& _aDots[d.x + 2, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y].Own == Owner
                           select d;
            if (pat389_2.Count() > 0)
            {
                result_dot = new Dot(pat389_2.First().x + 1, pat389_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat389_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own
&& _aDots[d.x, d.y + 1].Own == 0
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x - 1, d.y].Own == 0
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own
&& _aDots[d.x - 2, d.y].Own == 0
&& _aDots[d.x, d.y + 2].Own == 0
&& _aDots[d.x - 2, d.y + 1].Own == 0
&& _aDots[d.x - 1, d.y + 2].Own == 0
&& _aDots[d.x, d.y - 1].Own == Owner
                             select d;
            if (pat389_2_3.Count() > 0)
            {
                result_dot = new Dot(pat389_2_3.First().x - 1, pat389_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat389_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own
&& _aDots[d.x, d.y - 1].Own == 0
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x + 1, d.y].Own == 0
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own
&& _aDots[d.x + 2, d.y].Own == 0
&& _aDots[d.x, d.y - 2].Own == 0
&& _aDots[d.x + 2, d.y - 1].Own == 0
&& _aDots[d.x + 1, d.y - 2].Own == 0
&& _aDots[d.x, d.y + 1].Own == Owner
                               select d;
            if (pat389_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat389_2_3_4.First().x + 1, pat389_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            //iNumberPattern = 822;
            //var pat822 = from Dot d in _aDots
            //             where d.Own == Owner
            //                 && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
            //                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
            //                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
            //             select d;
            //if (pat822.Count() > 0)
            //{
            //    result_dot = new Dot(pat822.First().x + 1, pat822.First().y);
            //    result_dot.iNumberPattern = iNumberPattern;
            //    return result_dot;
            //}
            ////180 Rotate=========================================================================================================== 
            //var pat822_2 = from Dot d in _aDots
            //               where d.Own == Owner
            //                   && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
            //                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
            //                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
            //               select d;
            //if (pat822_2.Count() > 0)
            //{
            //    result_dot = new Dot(pat822_2.First().x - 1, pat822_2.First().y);
            //    result_dot.iNumberPattern = iNumberPattern;
            //    return result_dot;
            //}
            ////--------------Rotate on 90----------------------------------- 
            //var pat822_2_3 = from Dot d in _aDots
            //                 where d.Own == Owner
            //                     && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
            //                     && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
            //                     && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
            //                 select d;
            //if (pat822_2_3.Count() > 0)
            //{
            //    result_dot = new Dot(pat822_2_3.First().x, pat822_2_3.First().y - 1);
            //    result_dot.iNumberPattern = iNumberPattern;
            //    return result_dot;
            //}
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat822_2_3_4 = from Dot d in _aDots
            //                   where d.Own == Owner
            //                       && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
            //                       && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
            //                       && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
            //                   select d;
            //if (pat822_2_3_4.Count() > 0)
            //{
            //    result_dot = new Dot(pat822_2_3_4.First().x, pat822_2_3_4.First().y + 1);
            //    result_dot.iNumberPattern = iNumberPattern;
            //    return result_dot;
            //}
            //============================================================================================================== 
            iNumberPattern = 822;
            var pat822 = from Dot d in aDots
                        where d.Own == Owner
                            && aDots[d.x + 2, d.y].Own == enemy_own && aDots[d.x + 2, d.y].Blocked == false
                            && aDots[d.x + 1, d.y + 1].Own == Owner && aDots[d.x + 1, d.y + 1].Blocked == false
                            && aDots[d.x + 1, d.y].Own == 0 && aDots[d.x + 1, d.y].Blocked == false
                            && aDots[d.x + 1, d.y - 1].Own == Owner && aDots[d.x + 1, d.y - 1].Blocked == false
                            && aDots[d.x, d.y - 1].Own == enemy_own && aDots[d.x, d.y - 1].Blocked == false
                            | aDots[d.x, d.y + 1].Own == enemy_own && aDots[d.x, d.y + 1].Blocked == false
                        select d;
            if (pat822.Count() > 0)
            {
                result_dot = new Dot(pat822.First().x + 1, pat822.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat822_2 = from Dot d in aDots
                          where d.Own == Owner
                              && aDots[d.x - 2, d.y].Own == enemy_own && aDots[d.x - 2, d.y].Blocked == false
                              && aDots[d.x - 1, d.y - 1].Own == Owner && aDots[d.x - 1, d.y - 1].Blocked == false
                              && aDots[d.x - 1, d.y].Own == 0 && aDots[d.x - 1, d.y].Blocked == false
                              && aDots[d.x - 1, d.y + 1].Own == Owner && aDots[d.x - 1, d.y + 1].Blocked == false
                              && aDots[d.x, d.y + 1].Own == enemy_own && aDots[d.x, d.y + 1].Blocked == false
                              | aDots[d.x, d.y - 1].Own == enemy_own && aDots[d.x, d.y - 1].Blocked == false
                          select d;
            if (pat822_2.Count() > 0)
            {
                result_dot = new Dot(pat822_2.First().x - 1, pat822_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat822_2_3 = from Dot d in aDots
                            where d.Own == Owner
                                && aDots[d.x, d.y - 2].Own == enemy_own && aDots[d.x, d.y - 2].Blocked == false
                                && aDots[d.x - 1, d.y - 1].Own == Owner && aDots[d.x - 1, d.y - 1].Blocked == false
                                && aDots[d.x, d.y - 1].Own == 0 && aDots[d.x, d.y - 1].Blocked == false
                                && aDots[d.x + 1, d.y - 1].Own == Owner && aDots[d.x + 1, d.y - 1].Blocked == false
                                && aDots[d.x + 1, d.y].Own == enemy_own && aDots[d.x + 1, d.y].Blocked == false
                                | aDots[d.x - 1, d.y].Own == enemy_own && aDots[d.x - 1, d.y].Blocked == false
                            select d;
            if (pat822_2_3.Count() > 0)
            {
                result_dot = new Dot(pat822_2_3.First().x, pat822_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat822_2_3_4 = from Dot d in aDots
                              where d.Own == Owner
                                  && aDots[d.x, d.y + 2].Own == enemy_own && aDots[d.x, d.y + 2].Blocked == false
                                  && aDots[d.x + 1, d.y + 1].Own == Owner && aDots[d.x + 1, d.y + 1].Blocked == false
                                  && aDots[d.x, d.y + 1].Own == 0 && aDots[d.x, d.y + 1].Blocked == false
                                  && aDots[d.x - 1, d.y + 1].Own == Owner && aDots[d.x - 1, d.y + 1].Blocked == false
                                  && aDots[d.x - 1, d.y].Own == enemy_own && aDots[d.x - 1, d.y].Blocked == false
                                  | aDots[d.x + 1, d.y].Own == enemy_own && aDots[d.x + 1, d.y].Blocked == false
                              select d;
            if (pat822_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat822_2_3_4.First().x, pat822_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 933;
            var pat933 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 1, d.y].Own == enemy_own
&& _aDots[d.x + 1, d.y - 1].Own == Owner
&& _aDots[d.x + 2, d.y].Own == Owner
&& _aDots[d.x + 1, d.y].Own == enemy_own
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own
&& _aDots[d.x, d.y + 1].Own == 0
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x, d.y - 1].Own != enemy_own
                         select d;
            if (pat933.Count() > 0)
            {
                result_dot = new Dot(pat933.First().x, pat933.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat933_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 1, d.y].Own == enemy_own
&& _aDots[d.x - 1, d.y + 1].Own == Owner
&& _aDots[d.x - 2, d.y].Own == Owner
&& _aDots[d.x - 1, d.y].Own == enemy_own
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own
&& _aDots[d.x, d.y - 1].Own == 0
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x, d.y + 1].Own != enemy_own
                           select d;
            if (pat933_2.Count() > 0)
            {
                result_dot = new Dot(pat933_2.First().x, pat933_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat933_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x, d.y + 1].Own == enemy_own
&& _aDots[d.x + 1, d.y - 1].Own == Owner
&& _aDots[d.x, d.y - 2].Own == Owner
&& _aDots[d.x, d.y - 1].Own == enemy_own
&& _aDots[d.x - 1, d.y - 1].Own == enemy_own
&& _aDots[d.x - 1, d.y].Own == 0
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y].Own != enemy_own
                             select d;
            if (pat933_2_3.Count() > 0)
            {
                result_dot = new Dot(pat933_2_3.First().x - 1, pat933_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat933_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x, d.y - 1].Own == enemy_own
&& _aDots[d.x - 1, d.y + 1].Own == Owner
&& _aDots[d.x, d.y + 2].Own == Owner
&& _aDots[d.x, d.y + 1].Own == enemy_own
&& _aDots[d.x + 1, d.y + 1].Own == enemy_own
&& _aDots[d.x + 1, d.y].Own == 0
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y].Own != enemy_own
                               select d;
            if (pat933_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat933_2_3_4.First().x + 1, pat933_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 517;
            var pat517 = from Dot d in get_non_blocked
                         where d.Own == Owner && (d.x - 1) > 0 && (d.y + 2) < iBoardSize && (d.x + 2) < iBoardSize && (d.y - 1) > 0
&& _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
&& _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat517.Count() > 0)
            {
                result_dot = new Dot(pat517.First().x + 1, pat517.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat517_2 = from Dot d in get_non_blocked
                           where d.Own == Owner && (d.x - 1) > 0 && (d.y + 2) < iBoardSize && (d.x + 2) < iBoardSize && (d.y - 1) > 0
&& _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
&& _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat517_2.Count() > 0)
            {
                result_dot = new Dot(pat517_2.First().x - 1, pat517_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat517_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner && (d.x - 1) > 0 && (d.y + 2) < iBoardSize && (d.x + 2) < iBoardSize && (d.y - 1) > 0
&& _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat517_2_3.Count() > 0)
            {
                result_dot = new Dot(pat517_2_3.First().x - 1, pat517_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat517_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner && (d.x - 1) > 0 && (d.y + 2) < iBoardSize && (d.x + 2) < iBoardSize && (d.y - 1) > 0
&& _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat517_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat517_2_3_4.First().x + 1, pat517_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 735;
            var pat735 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == Owner
&& _aDots[d.x, d.y - 1].Own == enemy_own
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x, d.y - 2].Own == 0
&& _aDots[d.x + 2, d.y].Own == 0
&& _aDots[d.x + 1, d.y].Own == enemy_own
&& _aDots[d.x, d.y + 1].Own == enemy_own
&& _aDots[d.x - 1, d.y + 1].Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == 0
&& _aDots[d.x + 2, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y + 2].Own == 0
&& _aDots[d.x, d.y + 2].Own == 0
&& _aDots[d.x + 1, d.y - 2].Own == 0
&& _aDots[d.x + 2, d.y - 1].Own == 0
                         select d;
            if (pat735.Count() > 0)
            {
                result_dot = new Dot(pat735.First().x + 1, pat735.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat735_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == Owner
&& _aDots[d.x, d.y + 1].Own == enemy_own
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x, d.y + 2].Own == 0
&& _aDots[d.x - 2, d.y].Own == 0
&& _aDots[d.x - 1, d.y].Own == enemy_own
&& _aDots[d.x, d.y - 1].Own == enemy_own
&& _aDots[d.x + 1, d.y - 1].Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == 0
&& _aDots[d.x - 2, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y - 2].Own == 0
&& _aDots[d.x, d.y - 2].Own == 0
&& _aDots[d.x - 1, d.y + 2].Own == 0
&& _aDots[d.x - 2, d.y + 1].Own == 0
                           select d;
            if (pat735_2.Count() > 0)
            {
                result_dot = new Dot(pat735_2.First().x - 1, pat735_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat735_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == Owner
&& _aDots[d.x + 1, d.y].Own == enemy_own
&& _aDots[d.x + 1, d.y - 1].Own == 0
&& _aDots[d.x + 2, d.y].Own == 0
&& _aDots[d.x, d.y - 2].Own == 0
&& _aDots[d.x, d.y - 1].Own == enemy_own
&& _aDots[d.x - 1, d.y].Own == enemy_own
&& _aDots[d.x - 1, d.y + 1].Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == 0
&& _aDots[d.x - 1, d.y - 2].Own == 0
&& _aDots[d.x - 2, d.y - 1].Own == 0
&& _aDots[d.x - 2, d.y].Own == 0
&& _aDots[d.x + 2, d.y - 1].Own == 0
&& _aDots[d.x + 1, d.y - 2].Own == 0
                             select d;
            if (pat735_2_3.Count() > 0)
            {
                result_dot = new Dot(pat735_2_3.First().x + 1, pat735_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat735_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x - 1, d.y - 1].Own == Owner
&& _aDots[d.x - 1, d.y].Own == enemy_own
&& _aDots[d.x - 1, d.y + 1].Own == 0
&& _aDots[d.x - 2, d.y].Own == 0
&& _aDots[d.x, d.y + 2].Own == 0
&& _aDots[d.x, d.y + 1].Own == enemy_own
&& _aDots[d.x + 1, d.y].Own == enemy_own
&& _aDots[d.x + 1, d.y - 1].Own == Owner
&& _aDots[d.x + 1, d.y + 1].Own == 0
&& _aDots[d.x + 1, d.y + 2].Own == 0
&& _aDots[d.x + 2, d.y + 1].Own == 0
&& _aDots[d.x + 2, d.y].Own == 0
&& _aDots[d.x - 2, d.y + 1].Own == 0
&& _aDots[d.x - 1, d.y + 2].Own == 0
                               select d;
            if (pat735_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat735_2_3_4.First().x - 1, pat735_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 699;
            var pat699 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat699.Count() > 0)
            {
                result_dot = new Dot(pat699.First().x, pat699.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat699_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat699_2.Count() > 0)
            {
                result_dot = new Dot(pat699_2.First().x, pat699_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat699_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat699_2_3.Count() > 0)
            {
                result_dot = new Dot(pat699_2_3.First().x - 1, pat699_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat699_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat699_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat699_2_3_4.First().x + 1, pat699_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 296;
            var pat296 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat296.Count() > 0)
            {
                result_dot = new Dot(pat296.First().x - 1, pat296.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat296_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat296_2.Count() > 0)
            {
                result_dot = new Dot(pat296_2.First().x + 1, pat296_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat296_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                                 && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat296_2_3.Count() > 0)
            {
                result_dot = new Dot(pat296_2_3.First().x, pat296_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat296_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                                   && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat296_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat296_2_3_4.First().x, pat296_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 22;
            var pat22 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                            && _aDots[d.x + 1, d.y + 3].Own == Owner && _aDots[d.x + 1, d.y + 3].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                            && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                        select d;
            if (pat22.Count() > 0)
            {
                result_dot = new Dot(pat22.First().x + 1, pat22.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat22_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                              && _aDots[d.x - 1, d.y - 3].Own == Owner && _aDots[d.x - 1, d.y - 3].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                          select d;
            if (pat22_2.Count() > 0)
            {
                result_dot = new Dot(pat22_2.First().x - 1, pat22_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat22_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 3, d.y - 1].Own == Owner && _aDots[d.x - 3, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                            select d;
            if (pat22_2_3.Count() > 0)
            {
                result_dot = new Dot(pat22_2_3.First().x - 1, pat22_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat22_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                  && _aDots[d.x + 3, d.y + 1].Own == Owner && _aDots[d.x + 3, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                              select d;
            if (pat22_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat22_2_3_4.First().x + 1, pat22_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 25;
            var pat25 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                            && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                            && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                            && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                        select d;
            if (pat25.Count() > 0)
            {
                result_dot = new Dot(pat25.First().x, pat25.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat25_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                              && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                              && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                          select d;
            if (pat25_2.Count() > 0)
            {
                result_dot = new Dot(pat25_2.First().x, pat25_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat25_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                            select d;
            if (pat25_2_3.Count() > 0)
            {
                result_dot = new Dot(pat25_2_3.First().x - 1, pat25_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat25_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                  && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                  && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              select d;
            if (pat25_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat25_2_3_4.First().x + 1, pat25_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 23;
            var pat23 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                            && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                            && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                            && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false
                        select d;
            if (pat23.Count() > 0)
            {
                result_dot = new Dot(pat23.First().x + 1, pat23.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat23_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 2, d.y].Own == Owner && _aDots[d.x - 2, d.y].Blocked == false
                          select d;
            if (pat23_2.Count() > 0)
            {
                result_dot = new Dot(pat23_2.First().x - 1, pat23_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat23_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 2].Own == Owner && _aDots[d.x, d.y - 2].Blocked == false
                            select d;
            if (pat23_2_3.Count() > 0)
            {
                result_dot = new Dot(pat23_2_3.First().x - 1, pat23_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat23_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 2].Own == Owner && _aDots[d.x, d.y + 2].Blocked == false
                              select d;
            if (pat23_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat23_2_3_4.First().x + 1, pat23_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            iNumberPattern = 597;
            var pat597 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                             && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                             && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own != enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat597.Count() > 0)
            {
                result_dot = new Dot(pat597.First().x + 1, pat597.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat597_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                               && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                               && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own != enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat597_2.Count() > 0)
            {
                result_dot = new Dot(pat597_2.First().x - 1, pat597_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat597_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                                 && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own != enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat597_2_3.Count() > 0)
            {
                result_dot = new Dot(pat597_2_3.First().x - 1, pat597_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat597_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                                   && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own != enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat597_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat597_2_3_4.First().x + 1, pat597_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 938;
            var pat938 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y].Own == Owner
                             && _aDots[d.x + 1, d.y].Own == 0
                             && _aDots[d.x + 2, d.y].Own == enemy_own
                             && _aDots[d.x + 2, d.y + 1].Own == 0
                             && _aDots[d.x + 1, d.y + 1].Own == 0
                             && _aDots[d.x, d.y + 1].Own == 0
                             && _aDots[d.x - 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat938.Count() > 0)
            {
                result_dot = new Dot(pat938.First().x + 1, pat938.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat938_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y].Own == Owner
                               && _aDots[d.x - 1, d.y].Own == 0
                               && _aDots[d.x - 2, d.y].Own == enemy_own
                               && _aDots[d.x - 2, d.y - 1].Own == 0
                               && _aDots[d.x - 1, d.y - 1].Own == 0
                               && _aDots[d.x, d.y - 1].Own == 0
                               && _aDots[d.x + 1, d.y - 1].Own == enemy_own
                           select d;
            if (pat938_2.Count() > 0)
            {
                result_dot = new Dot(pat938_2.First().x - 1, pat938_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat938_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y + 1].Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == 0
                                 && _aDots[d.x, d.y - 2].Own == enemy_own
                                 && _aDots[d.x - 1, d.y - 2].Own == 0
                                 && _aDots[d.x - 1, d.y - 1].Own == 0
                                 && _aDots[d.x - 1, d.y].Own == 0
                                 && _aDots[d.x - 1, d.y + 1].Own == enemy_own
                             select d;

            if (pat938_2_3.Count() > 0)
            {
                result_dot = new Dot(pat938_2_3.First().x - 1, pat938_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat938_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y - 1].Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == 0
                                   && _aDots[d.x, d.y + 2].Own == enemy_own
                                   && _aDots[d.x + 1, d.y + 2].Own == 0
                                   && _aDots[d.x + 1, d.y + 1].Own == 0
                                   && _aDots[d.x + 1, d.y].Own == 0
                                   && _aDots[d.x + 1, d.y - 1].Own == enemy_own
                               select d;
            if (pat938_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat938_2_3_4.First().x + 1, pat938_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 44;
            var pat44 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                            && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                            && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                            && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                        select d;
            if (pat44.Count() > 0)
            {
                result_dot = new Dot(pat44.First().x + 1, pat44.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat44_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                              && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                              && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                          select d;
            if (pat44_2.Count() > 0)
            {
                result_dot = new Dot(pat44_2.First().x - 1, pat44_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat44_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                            select d;
            if (pat44_2_3.Count() > 0)
            {
                result_dot = new Dot(pat44_2_3.First().x, pat44_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat44_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                  && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                  && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                              select d;
            if (pat44_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat44_2_3_4.First().x, pat44_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 134;
            var pat134 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                         select d;
            if (pat134.Count() > 0)
            {
                result_dot = new Dot(pat134.First().x, pat134.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat134_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                           select d;
            if (pat134_2.Count() > 0)
            {
                result_dot = new Dot(pat134_2.First().x, pat134_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat134_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                             select d;
            if (pat134_2_3.Count() > 0)
            {
                result_dot = new Dot(pat134_2_3.First().x + 1, pat134_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat134_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                               select d;
            if (pat134_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat134_2_3_4.First().x - 1, pat134_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 671;
            var pat671 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat671.Count() > 0)
            {
                result_dot = new Dot(pat671.First().x - 1, pat671.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat671_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat671_2.Count() > 0)
            {
                result_dot = new Dot(pat671_2.First().x + 1, pat671_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat671_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat671_2_3.Count() > 0)
            {
                result_dot = new Dot(pat671_2_3.First().x - 1, pat671_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat671_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat671_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat671_2_3_4.First().x + 1, pat671_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 726;
            var pat726 = from Dot d in get_non_blocked
                         where d.Own == Owner && d.x > 1 && d.y > 1 && d.x < iBoardSize - 1 && d.y < iBoardSize - 1
                             && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                         select d;
            if (pat726.Count() > 0)
            {
                result_dot = new Dot(pat726.First().x + 1, pat726.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat726_2 = from Dot d in get_non_blocked
                           where d.Own == Owner && d.x > 1 && d.y > 1 && d.x < iBoardSize - 1 && d.y < iBoardSize - 1
                               && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                           select d;
            if (pat726_2.Count() > 0)
            {
                result_dot = new Dot(pat726_2.First().x - 1, pat726_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat726_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner && d.x > 1 && d.y > 1 && d.x < iBoardSize - 1 && d.y < iBoardSize - 1
                                 && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             select d;
            if (pat726_2_3.Count() > 0)
            {
                result_dot = new Dot(pat726_2_3.First().x + 1, pat726_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat726_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner && d.x > 1 && d.y > 1 && d.x < iBoardSize - 1 && d.y < iBoardSize - 1
                                   && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               select d;
            if (pat726_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat726_2_3_4.First().x - 1, pat726_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            ////============================================================================================================== 
            iNumberPattern = 689;
            var pat689 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat689.Count() > 0)
            {
                result_dot = new Dot(pat689.First().x + 1, pat689.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat689_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat689_2.Count() > 0)
            {
                result_dot = new Dot(pat689_2.First().x - 1, pat689_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat689_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat689_2_3.Count() > 0)
            {
                result_dot = new Dot(pat689_2_3.First().x, pat689_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat689_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat689_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat689_2_3_4.First().x, pat689_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 827;
            var pat827 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat827.Count() > 0)
            {
                result_dot = new Dot(pat827.First().x, pat827.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat827_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                           select d;
            if (pat827_2.Count() > 0)
            {
                result_dot = new Dot(pat827_2.First().x, pat827_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat827_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                             select d;
            if (pat827_2_3.Count() > 0)
            {
                result_dot = new Dot(pat827_2_3.First().x + 1, pat827_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat827_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                               select d;
            if (pat827_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat827_2_3_4.First().x - 1, pat827_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 974;
            var pat974 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat974.Count() > 0)
            {
                result_dot = new Dot(pat974.First().x + 1, pat974.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat974_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                           select d;
            if (pat974_2.Count() > 0)
            {
                result_dot = new Dot(pat974_2.First().x - 1, pat974_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat974_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat974_2_3.Count() > 0)
            {
                result_dot = new Dot(pat974_2_3.First().x + 1, pat974_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat974_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               select d;
            if (pat974_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat974_2_3_4.First().x - 1, pat974_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 476;
            var pat476 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                         select d;
            if (pat476.Count() > 0)
            {
                result_dot = new Dot(pat476.First().x + 1, pat476.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat476_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                           select d;
            if (pat476_2.Count() > 0)
            {
                result_dot = new Dot(pat476_2.First().x - 1, pat476_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat476_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             select d;
            if (pat476_2_3.Count() > 0)
            {
                result_dot = new Dot(pat476_2_3.First().x, pat476_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat476_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               select d;
            if (pat476_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat476_2_3_4.First().x, pat476_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 479;
            var pat479 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x + 2, d.y + 2].Own == Owner && _aDots[d.x + 2, d.y + 2].Blocked == false
                             && _aDots[d.x + 3, d.y + 1].Own == Owner && _aDots[d.x + 3, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own != enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            if (pat479.Count() > 0)
            {
                result_dot = new Dot(pat479.First().x + 1, pat479.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat479_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                               && _aDots[d.x - 3, d.y - 1].Own == Owner && _aDots[d.x - 3, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own != enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                           select d;
            if (pat479_2.Count() > 0)
            {
                result_dot = new Dot(pat479_2.First().x - 1, pat479_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat479_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 3].Own == Owner && _aDots[d.x - 1, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own != enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             select d;
            if (pat479_2_3.Count() > 0)
            {
                result_dot = new Dot(pat479_2_3.First().x + 1, pat479_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat479_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y + 2].Own == Owner && _aDots[d.x + 2, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 3].Own == Owner && _aDots[d.x + 1, d.y + 3].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own != enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               select d;
            if (pat479_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat479_2_3_4.First().x - 1, pat479_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 384;
            var pat384 = from Dot d in aDots
                         where d.Own == Owner
                             && aDots[d.x - 1, d.y + 1].Own == Owner && aDots[d.x - 1, d.y + 1].Blocked == false
                             && aDots[d.x - 1, d.y].Own == enemy_own && aDots[d.x - 1, d.y].Blocked == false
                             && aDots[d.x - 1, d.y - 1].Own == 0 && aDots[d.x - 1, d.y - 1].Blocked == false
                             && aDots[d.x - 2, d.y - 1].Own == 0 && aDots[d.x - 2, d.y - 1].Blocked == false
                             && aDots[d.x - 2, d.y - 2].Own == enemy_own && aDots[d.x - 2, d.y - 2].Blocked == false
                             && aDots[d.x - 2, d.y].Own == 0 && aDots[d.x - 2, d.y].Blocked == false
                             && aDots[d.x - 2, d.y + 1].Own == 0 && aDots[d.x - 2, d.y + 1].Blocked == false

                         select d;
            if (pat384.Count() > 0)
            {
                result_dot = new Dot(pat384.First().x - 1, pat384.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat384_2 = from Dot d in aDots
                           where d.Own == Owner
                               && aDots[d.x + 1, d.y - 1].Own == Owner && aDots[d.x + 1, d.y - 1].Blocked == false
                               && aDots[d.x + 1, d.y].Own == enemy_own && aDots[d.x + 1, d.y].Blocked == false
                               && aDots[d.x + 1, d.y + 1].Own == 0 && aDots[d.x + 1, d.y + 1].Blocked == false
                               && aDots[d.x + 2, d.y + 1].Own == 0 && aDots[d.x + 2, d.y + 1].Blocked == false
                               && aDots[d.x + 2, d.y + 2].Own == enemy_own && aDots[d.x + 2, d.y + 2].Blocked == false
                               && aDots[d.x + 2, d.y].Own == 0 && aDots[d.x + 2, d.y].Blocked == false
                               && aDots[d.x + 2, d.y - 1].Own == 0 && aDots[d.x + 2, d.y - 1].Blocked == false
                           select d;
            if (pat384_2.Count() > 0)
            {
                result_dot = new Dot(pat384_2.First().x + 1, pat384_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat384_2_3 = from Dot d in aDots
                             where d.Own == Owner
                                 && aDots[d.x - 1, d.y + 1].Own == Owner && aDots[d.x - 1, d.y + 1].Blocked == false
                                 && aDots[d.x, d.y + 1].Own == enemy_own && aDots[d.x, d.y + 1].Blocked == false
                                 && aDots[d.x + 1, d.y + 1].Own == 0 && aDots[d.x + 1, d.y + 1].Blocked == false
                                 && aDots[d.x + 1, d.y + 2].Own == 0 && aDots[d.x + 1, d.y + 2].Blocked == false
                                 && aDots[d.x + 2, d.y + 2].Own == enemy_own && aDots[d.x + 2, d.y + 2].Blocked == false
                                 && aDots[d.x, d.y + 2].Own == 0 && aDots[d.x, d.y + 2].Blocked == false
                                 && aDots[d.x - 1, d.y + 2].Own == 0 && aDots[d.x - 1, d.y + 2].Blocked == false
                             select d;
            if (pat384_2_3.Count() > 0)
            {
                result_dot = new Dot(pat384_2_3.First().x + 1, pat384_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat384_2_3_4 = from Dot d in aDots
                               where d.Own == Owner
                                   && aDots[d.x + 1, d.y - 1].Own == Owner && aDots[d.x + 1, d.y - 1].Blocked == false
                                   && aDots[d.x, d.y - 1].Own == enemy_own && aDots[d.x, d.y - 1].Blocked == false
                                   && aDots[d.x - 1, d.y - 1].Own == 0 && aDots[d.x - 1, d.y - 1].Blocked == false
                                   && aDots[d.x - 1, d.y - 2].Own == 0 && aDots[d.x - 1, d.y - 2].Blocked == false
                                   && aDots[d.x - 2, d.y - 2].Own == enemy_own && aDots[d.x - 2, d.y - 2].Blocked == false
                                   && aDots[d.x, d.y - 2].Own == 0 && aDots[d.x, d.y - 2].Blocked == false
                                   && aDots[d.x + 1, d.y - 2].Own == 0 && aDots[d.x + 1, d.y - 2].Blocked == false
                               select d;
            if (pat384_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat384_2_3_4.First().x - 1, pat384_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 15;
            var pat15 = from Dot d in get_non_blocked
                        where d.Own == Owner && d.x + 2 == iBoardSize
                            && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                        select d;
            if (pat15.Count() > 0)
            {
                result_dot = new Dot(pat15.First().x + 1, pat15.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat15_2 = from Dot d in get_non_blocked
                          where d.Own == Owner && d.x - 1 == 0
                              && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                          select d;
            if (pat15_2.Count() > 0)
            {
                result_dot = new Dot(pat15_2.First().x - 1, pat15_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat15_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner && d.y - 1 == 0
                                && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            select d;
            if (pat15_2_3.Count() > 0)
            {
                result_dot = new Dot(pat15_2_3.First().x, pat15_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat15_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner && d.y + 2 == iBoardSize
                                  && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              select d;
            if (pat15_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat15_2_3_4.First().x, pat15_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            iNumberPattern = 437;
            var pat437 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat437.Count() > 0)
            {
                result_dot = new Dot(pat437.First().x - 1, pat437.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat437_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat437_2.Count() > 0)
            {
                result_dot = new Dot(pat437_2.First().x + 1, pat437_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat437_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat437_2_3.Count() > 0)
            {
                result_dot = new Dot(pat437_2_3.First().x, pat437_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat437_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat437_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat437_2_3_4.First().x, pat437_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 669;
            var pat669 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 3].Own == enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                         select d;
            if (pat669.Count() > 0)
            {
                result_dot = new Dot(pat669.First().x + 1, pat669.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat669_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 3].Own == enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                           select d;
            if (pat669_2.Count() > 0)
            {
                result_dot = new Dot(pat669_2.First().x - 1, pat669_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat669_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x - 3, d.y].Own == enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                             select d;
            if (pat669_2_3.Count() > 0)
            {
                result_dot = new Dot(pat669_2_3.First().x - 1, pat669_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat669_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x + 3, d.y].Own == enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                               select d;
            if (pat669_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat669_2_3_4.First().x + 1, pat669_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 434;
            var pat434 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                         select d;
            if (pat434.Count() > 0)
            {
                result_dot = new Dot(pat434.First().x + 1, pat434.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat434_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                           select d;
            if (pat434_2.Count() > 0)
            {
                result_dot = new Dot(pat434_2.First().x - 1, pat434_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat434_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                             select d;
            if (pat434_2_3.Count() > 0)
            {
                result_dot = new Dot(pat434_2_3.First().x - 1, pat434_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat434_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                               select d;
            if (pat434_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat434_2_3_4.First().x + 1, pat434_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 63;
            var pat63 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                            && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                            && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                            && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                            && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                            && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                            && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                        select d;
            if (pat63.Count() > 0)
            {
                result_dot = new Dot(pat63.First().x - 1, pat63.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat63_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                              && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                          select d;
            if (pat63_2.Count() > 0)
            {
                result_dot = new Dot(pat63_2.First().x + 1, pat63_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat63_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                                && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                            select d;
            if (pat63_2_3.Count() > 0)
            {
                result_dot = new Dot(pat63_2_3.First().x - 1, pat63_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat63_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                                  && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              select d;
            if (pat63_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat63_2_3_4.First().x + 1, pat63_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 1918;
            var pat1918 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                          select d;
            if (pat1918.Count() > 0)
            {
                result_dot = new Dot(pat1918.First().x + 1, pat1918.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1918_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            select d;
            if (pat1918_2.Count() > 0)
            {
                result_dot = new Dot(pat1918_2.First().x - 1, pat1918_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1918_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              select d;
            if (pat1918_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1918_2_3.First().x, pat1918_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1918_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                    && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                    && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                select d;
            if (pat1918_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1918_2_3_4.First().x, pat1918_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 239;
            var pat239 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == Owner && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                         select d;
            if (pat239.Count() > 0)
            {
                result_dot = new Dot(pat239.First().x - 1, pat239.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat239_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == Owner && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                             select d;
            if (pat239_2_3.Count() > 0)
            {
                result_dot = new Dot(pat239_2_3.First().x - 1, pat239_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //------------------------------------------------------------------
            iNumberPattern = 144;
            var pat144 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            if (pat144.Count() > 0)
            {
                result_dot = new Dot(pat144.First().x - 1, pat144.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat144_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                           select d;
            if (pat144_2.Count() > 0)
            {
                result_dot = new Dot(pat144_2.First().x + 1, pat144_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat144_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             select d;
            if (pat144_2_3.Count() > 0)
            {
                result_dot = new Dot(pat144_2_3.First().x - 1, pat144_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat144_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               select d;
            if (pat144_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat144_2_3_4.First().x + 1, pat144_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 723;
            var pat723 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat723.Count() > 0)
            {
                result_dot = new Dot(pat723.First().x - 1, pat723.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat723_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 2, d.y + 2].Own == Owner && _aDots[d.x + 2, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat723_2.Count() > 0)
            {
                result_dot = new Dot(pat723_2.First().x + 1, pat723_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat723_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 2].Own == Owner && _aDots[d.x + 2, d.y + 2].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat723_2_3.Count() > 0)
            {
                result_dot = new Dot(pat723_2_3.First().x + 1, pat723_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat723_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat723_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat723_2_3_4.First().x - 1, pat723_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 652;
            var pat652 = from Dot d in get_non_blocked
                         where d.Own == Owner
&& _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
&& _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
&& _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat652.Count() > 0)
            {
                result_dot = new Dot(pat652.First().x - 1, pat652.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat652_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
&& _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
&& _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat652_2.Count() > 0)
            {
                result_dot = new Dot(pat652_2.First().x + 1, pat652_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat652_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
&& _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
&& _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
&& _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
&& _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
&& _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
&& _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat652_2_3.Count() > 0)
            {
                result_dot = new Dot(pat652_2_3.First().x + 1, pat652_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat652_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
&& _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
&& _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
&& _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
&& _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
&& _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
&& _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
&& _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
&& _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
&& _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
&& _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
&& _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat652_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat652_2_3_4.First().x - 1, pat652_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 367;
            var pat367 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat367.Count() > 0)
            {
                result_dot = new Dot(pat367.First().x, pat367.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat367_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                           select d;
            if (pat367_2.Count() > 0)
            {
                result_dot = new Dot(pat367_2.First().x, pat367_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat367_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             select d;
            if (pat367_2_3.Count() > 0)
            {
                result_dot = new Dot(pat367_2_3.First().x + 1, pat367_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat367_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               select d;
            if (pat367_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat367_2_3_4.First().x - 1, pat367_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 117;
            var pat117 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x + 2, d.y + 2].Own == Owner && _aDots[d.x + 2, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                         select d;
            if (pat117.Count() > 0)
            {
                result_dot = new Dot(pat117.First().x - 1, pat117.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat117_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                           select d;
            if (pat117_2.Count() > 0)
            {
                result_dot = new Dot(pat117_2.First().x + 1, pat117_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat117_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y - 2].Own == Owner && _aDots[d.x - 2, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             select d;
            if (pat117_2_3.Count() > 0)
            {
                result_dot = new Dot(pat117_2_3.First().x - 1, pat117_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat117_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y + 2].Own == Owner && _aDots[d.x + 2, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               select d;
            if (pat117_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat117_2_3_4.First().x + 1, pat117_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 1723;
            var pat1723 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                          select d;
            if (pat1723.Count() > 0)
            {
                result_dot = new Dot(pat1723.First().x, pat1723.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1723_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                            select d;
            if (pat1723_2.Count() > 0)
            {
                result_dot = new Dot(pat1723_2.First().x, pat1723_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1723_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                  && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              select d;
            if (pat1723_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1723_2_3.First().x - 1, pat1723_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1723_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                    && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                    && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                    && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                    && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                    && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                select d;
            if (pat1723_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1723_2_3_4.First().x + 1, pat1723_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 991;
            var pat991 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own != enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own != enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y].Own != enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                         select d;
            if (pat991.Count() > 0)
            {
                result_dot = new Dot(pat991.First().x + 1, pat991.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat991_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == Owner && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own != enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own != enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 3, d.y].Own != enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                           select d;
            if (pat991_2.Count() > 0)
            {
                result_dot = new Dot(pat991_2.First().x - 1, pat991_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat991_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == Owner && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own != enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own != enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 3].Own != enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                             select d;
            if (pat991_2_3.Count() > 0)
            {
                result_dot = new Dot(pat991_2_3.First().x - 1, pat991_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat991_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == Owner && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own != enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own != enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 3].Own != enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                               select d;
            if (pat991_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat991_2_3_4.First().x + 1, pat991_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 329;
            var pat329 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                         select d;
            if (pat329.Count() > 0)
            {
                result_dot = new Dot(pat329.First().x + 1, pat329.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat329_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                           select d;
            if (pat329_2.Count() > 0)
            {
                result_dot = new Dot(pat329_2.First().x - 1, pat329_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat329_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             select d;
            if (pat329_2_3.Count() > 0)
            {
                result_dot = new Dot(pat329_2_3.First().x + 1, pat329_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat329_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               select d;
            if (pat329_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat329_2_3_4.First().x - 1, pat329_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 1472;
            var pat1472 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                          select d;
            if (pat1472.Count() > 0)
            {
                result_dot = new Dot(pat1472.First().x - 1, pat1472.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1472_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                            select d;
            if (pat1472_2.Count() > 0)
            {
                result_dot = new Dot(pat1472_2.First().x + 1, pat1472_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1472_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                              select d;
            if (pat1472_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1472_2_3.First().x - 1, pat1472_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1472_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                    && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                    && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                    && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                select d;
            if (pat1472_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1472_2_3_4.First().x + 1, pat1472_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            ////============================================================================================================== 
            iNumberPattern = 1970;
            var pat1970 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                          select d;
            if (pat1970.Count() > 0)
            {
                result_dot = new Dot(pat1970.First().x + 1, pat1970.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1970_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == Owner && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                            select d;
            if (pat1970_2.Count() > 0)
            {
                result_dot = new Dot(pat1970_2.First().x - 1, pat1970_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1970_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == Owner && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                  && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                              select d;
            if (pat1970_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1970_2_3.First().x + 1, pat1970_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1970_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                    && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x, d.y + 2].Own == Owner && _aDots[d.x, d.y + 2].Blocked == false
                                    && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                    && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                    && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                    && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                    && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                select d;
            if (pat1970_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1970_2_3_4.First().x - 1, pat1970_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 577;
            var pat577 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 3].Own == Owner && _aDots[d.x - 1, d.y - 3].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x - 2, d.y - 3].Own == 0 && _aDots[d.x - 2, d.y - 3].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                         select d;
            if (pat577.Count() > 0)
            {
                result_dot = new Dot(pat577.First().x - 1, pat577.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat577_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 3].Own == Owner && _aDots[d.x + 1, d.y + 3].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x + 2, d.y + 3].Own == 0 && _aDots[d.x + 2, d.y + 3].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                           select d;
            if (pat577_2.Count() > 0)
            {
                result_dot = new Dot(pat577_2.First().x + 1, pat577_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat577_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 3, d.y + 1].Own == Owner && _aDots[d.x + 3, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 3, d.y + 2].Own == 0 && _aDots[d.x + 3, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                             select d;
            if (pat577_2_3.Count() > 0)
            {
                result_dot = new Dot(pat577_2_3.First().x + 1, pat577_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat577_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 3, d.y - 1].Own == Owner && _aDots[d.x - 3, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 3, d.y - 2].Own == 0 && _aDots[d.x - 3, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                               select d;
            if (pat577_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat577_2_3_4.First().x - 1, pat577_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 27;
            var pat27 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                            && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                            && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                        select d;
            if (pat27.Count() > 0)
            {
                result_dot = new Dot(pat27.First().x - 2, pat27.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat27_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                          select d;
            if (pat27_2.Count() > 0)
            {
                result_dot = new Dot(pat27_2.First().x + 2, pat27_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat27_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                            select d;
            if (pat27_2_3.Count() > 0)
            {
                result_dot = new Dot(pat27_2_3.First().x, pat27_2_3.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat27_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                              select d;
            if (pat27_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat27_2_3_4.First().x, pat27_2_3_4.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 775;
            var pat775 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x - 3, d.y].Own == Owner && _aDots[d.x - 3, d.y].Blocked == false
                             && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                             && _aDots[d.x - 3, d.y - 2].Own == Owner && _aDots[d.x - 3, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                             && _aDots[d.x - 4, d.y - 1].Own == 0 && _aDots[d.x - 4, d.y - 1].Blocked == false
                             && _aDots[d.x - 4, d.y + 1].Own == 0 && _aDots[d.x - 4, d.y + 1].Blocked == false
                             && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                             && _aDots[d.x - 4, d.y].Own == 0 && _aDots[d.x - 4, d.y].Blocked == false
                         select d;
            if (pat775.Count() > 0)
            {
                result_dot = new Dot(pat775.First().x - 1, pat775.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat775_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x + 3, d.y].Own == Owner && _aDots[d.x + 3, d.y].Blocked == false
                               && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                               && _aDots[d.x + 3, d.y + 2].Own == Owner && _aDots[d.x + 3, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                               && _aDots[d.x + 4, d.y + 1].Own == 0 && _aDots[d.x + 4, d.y + 1].Blocked == false
                               && _aDots[d.x + 4, d.y - 1].Own == 0 && _aDots[d.x + 4, d.y - 1].Blocked == false
                               && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                               && _aDots[d.x + 4, d.y].Own == 0 && _aDots[d.x + 4, d.y].Blocked == false
                           select d;
            if (pat775_2.Count() > 0)
            {
                result_dot = new Dot(pat775_2.First().x + 1, pat775_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat775_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y + 3].Own == Owner && _aDots[d.x, d.y + 3].Blocked == false
                                 && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                                 && _aDots[d.x + 2, d.y + 3].Own == Owner && _aDots[d.x + 2, d.y + 3].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 4].Own == 0 && _aDots[d.x + 1, d.y + 4].Blocked == false
                                 && _aDots[d.x - 1, d.y + 4].Own == 0 && _aDots[d.x - 1, d.y + 4].Blocked == false
                                 && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                                 && _aDots[d.x, d.y + 4].Own == 0 && _aDots[d.x, d.y + 4].Blocked == false
                             select d;
            if (pat775_2_3.Count() > 0)
            {
                result_dot = new Dot(pat775_2_3.First().x, pat775_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat775_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y - 3].Own == Owner && _aDots[d.x, d.y - 3].Blocked == false
                                   && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                                   && _aDots[d.x - 2, d.y - 3].Own == Owner && _aDots[d.x - 2, d.y - 3].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 4].Own == 0 && _aDots[d.x - 1, d.y - 4].Blocked == false
                                   && _aDots[d.x + 1, d.y - 4].Own == 0 && _aDots[d.x + 1, d.y - 4].Blocked == false
                                   && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                                   && _aDots[d.x, d.y - 4].Own == 0 && _aDots[d.x, d.y - 4].Blocked == false
                               select d;
            if (pat775_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat775_2_3_4.First().x, pat775_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 848;
            var pat848 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x + 2, d.y].Own == Owner && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y].Own != enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                         select d;
            if (pat848.Count() > 0)
            {
                result_dot = new Dot(pat848.First().x + 1, pat848.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat848_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x - 2, d.y].Own == Owner && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x - 3, d.y].Own != enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                           select d;
            if (pat848_2.Count() > 0)
            {
                result_dot = new Dot(pat848_2.First().x - 1, pat848_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat848_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 2].Own == Owner && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y - 3].Own != enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                             select d;
            if (pat848_2_3.Count() > 0)
            {
                result_dot = new Dot(pat848_2_3.First().x - 1, pat848_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat848_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 2].Own == Owner && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y + 3].Own != enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                               select d;
            if (pat848_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat848_2_3_4.First().x + 1, pat848_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 444;
            var pat444 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 3].Own == enemy_own && _aDots[d.x + 1, d.y + 3].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat444.Count() > 0)
            {
                result_dot = new Dot(pat444.First().x - 1, pat444.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat444_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 3].Own == enemy_own && _aDots[d.x - 1, d.y - 3].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat444_2.Count() > 0)
            {
                result_dot = new Dot(pat444_2.First().x + 1, pat444_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat444_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x - 3, d.y - 1].Own == enemy_own && _aDots[d.x - 3, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat444_2_3.Count() > 0)
            {
                result_dot = new Dot(pat444_2_3.First().x - 1, pat444_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat444_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x + 3, d.y + 1].Own == enemy_own && _aDots[d.x + 3, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat444_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat444_2_3_4.First().x + 1, pat444_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 1242;
            var pat1242 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                              && _aDots[d.x + 1, d.y + 3].Own == enemy_own && _aDots[d.x + 1, d.y + 3].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                              && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                          select d;
            if (pat1242.Count() > 0)
            {
                result_dot = new Dot(pat1242.First().x + 1, pat1242.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1242_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                && _aDots[d.x - 1, d.y - 3].Own == enemy_own && _aDots[d.x - 1, d.y - 3].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                            select d;
            if (pat1242_2.Count() > 0)
            {
                result_dot = new Dot(pat1242_2.First().x - 1, pat1242_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1242_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                  && _aDots[d.x - 3, d.y - 1].Own == enemy_own && _aDots[d.x - 3, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                  && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                              select d;
            if (pat1242_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1242_2_3.First().x - 1, pat1242_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1242_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                    && _aDots[d.x + 3, d.y + 1].Own == enemy_own && _aDots[d.x + 3, d.y + 1].Blocked == false
                                    && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                                    && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                    && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                    && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                    && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                    && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                select d;
            if (pat1242_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1242_2_3_4.First().x + 1, pat1242_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 179;
            var pat179 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y - 2].Own == enemy_own && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                         select d;
            if (pat179.Count() > 0)
            {
                result_dot = new Dot(pat179.First().x + 1, pat179.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat179_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y + 2].Own == enemy_own && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                           select d;
            if (pat179_2.Count() > 0)
            {
                result_dot = new Dot(pat179_2.First().x - 1, pat179_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat179_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 2, d.y - 1].Own == enemy_own && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             select d;
            if (pat179_2_3.Count() > 0)
            {
                result_dot = new Dot(pat179_2_3.First().x, pat179_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat179_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 2, d.y + 1].Own == enemy_own && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               select d;
            if (pat179_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat179_2_3_4.First().x, pat179_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 82;
            var pat82 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                            && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                            && _aDots[d.x + 3, d.y - 1].Own == Owner && _aDots[d.x + 3, d.y - 1].Blocked == false
                            && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                            && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                        select d;
            if (pat82.Count() > 0)
            {
                result_dot = new Dot(pat82.First().x + 1, pat82.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat82_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                              && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                              && _aDots[d.x - 3, d.y + 1].Own == Owner && _aDots[d.x - 3, d.y + 1].Blocked == false
                              && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                          select d;
            if (pat82_2.Count() > 0)
            {
                result_dot = new Dot(pat82_2.First().x - 1, pat82_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat82_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                && _aDots[d.x + 1, d.y - 3].Own == Owner && _aDots[d.x + 1, d.y - 3].Blocked == false
                                && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                            select d;
            if (pat82_2_3.Count() > 0)
            {
                result_dot = new Dot(pat82_2_3.First().x, pat82_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat82_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y + 3].Own == Owner && _aDots[d.x - 1, d.y + 3].Blocked == false
                                  && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                              select d;
            if (pat82_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat82_2_3_4.First().x, pat82_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            //паттерн на конструкцию    + d+         
            //                          *     *   
            iNumberPattern = 4;
            var pat4_1 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false &&
                                                _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false &&
                                                _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false &&
                                                _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat4_1.Count() > 0)
            {

                result_dot = new Dot(pat4_1.First().x, pat4_1.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию      d+  +         
            //                          *     *   
            var pat4_2 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false &&
                                                _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false &&
                                                _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false &&
                                                _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat4_2.Count() > 0)
            {

                result_dot = new Dot(pat4_2.First().x, pat4_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию            
            //                          *     *   
            //                          + d+ 
            var pat4_3 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x - 1, d.y].Own == Owner && _aDots[d.x - 1, d.y].Blocked == false &&
                                                _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat4_3.Count() > 0)
            {

                result_dot = new Dot(pat4_3.First().x, pat4_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //паттерн на конструкцию            
            //                          *     *   
            //                            d+  +
            var pat4_4 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x + 1, d.y].Own == Owner && _aDots[d.x + 1, d.y].Blocked == false &&
                                                _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false &&
                                                _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                         select d;
            if (pat4_4.Count() > 0)
            {

                result_dot = new Dot(pat4_4.First().x, pat4_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //паттерн на конструкцию    *        
            //                             +  
            //                          * d+  
            var pat4_5 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false &&
                                                _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false &&
                                                _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false &&
                                                _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                         select d;
            if (pat4_5.Count() > 0)
            {

                result_dot = new Dot(pat4_5.First().x - 1, pat4_5.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //паттерн на конструкцию    *  +       
            //                            d+  
            //                          *   
            var pat4_6 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false &&
                                                _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false &&
                                                _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                         select d;
            if (pat4_6.Count() > 0)
            {

                result_dot = new Dot(pat4_6.First().x - 1, pat4_6.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //паттерн на конструкцию      +  *     
            //                           d+  
            //                               *
            var pat4_7 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x, d.y - 1].Own == Owner && _aDots[d.x, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false &&
                                                _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                         select d;
            if (pat4_7.Count() > 0)
            {

                result_dot = new Dot(pat4_7.First().x + 1, pat4_7.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            //паттерн на конструкцию         *     
            //                           d+  
            //                            +  *
            var pat4_8 = from Dot d in get_non_blocked
                         where d.Own == Owner && _aDots[d.x, d.y + 1].Own == Owner && _aDots[d.x, d.y + 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false &&
                                                _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false &&
                                                _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false &&
                                                _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                         select d;
            if (pat4_8.Count() > 0)
            {

                result_dot = new Dot(pat4_8.First().x + 1, pat4_8.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }

            iNumberPattern = 825;
            var pat825 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat825.Count() > 0)
            {
                result_dot = new Dot(pat825.First().x + 1, pat825.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat825_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                           select d;
            if (pat825_2.Count() > 0)
            {
                result_dot = new Dot(pat825_2.First().x - 1, pat825_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat825_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat825_2_3.Count() > 0)
            {
                result_dot = new Dot(pat825_2_3.First().x + 1, pat825_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat825_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               select d;
            if (pat825_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat825_2_3_4.First().x - 1, pat825_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 62;
            var pat62 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                            && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                            && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                            && _aDots[d.x - 1, d.y + 1].Own != Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                            && _aDots[d.x - 2, d.y + 1].Own != Owner && _aDots[d.x - 2, d.y + 1].Blocked == false
                            && _aDots[d.x - 2, d.y].Own != Owner && _aDots[d.x - 2, d.y].Blocked == false
                        select d;
            if (pat62.Count() > 0)
            {
                result_dot = new Dot(pat62.First().x - 1, pat62.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat62_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y - 1].Own != Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y - 1].Own != Owner && _aDots[d.x + 2, d.y - 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own != Owner && _aDots[d.x + 2, d.y].Blocked == false
                          select d;
            if (pat62_2.Count() > 0)
            {
                result_dot = new Dot(pat62_2.First().x + 1, pat62_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat62_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x + 1, d.y - 1].Own == Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 1].Own != Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                && _aDots[d.x - 1, d.y + 2].Own != Owner && _aDots[d.x - 1, d.y + 2].Blocked == false
                                && _aDots[d.x, d.y + 2].Own != Owner && _aDots[d.x, d.y + 2].Blocked == false
                            select d;
            if (pat62_2_3.Count() > 0)
            {
                result_dot = new Dot(pat62_2_3.First().x + 1, pat62_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat62_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x - 1, d.y + 1].Own == Owner && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y - 1].Own != Owner && _aDots[d.x + 1, d.y - 1].Blocked == false
                                  && _aDots[d.x + 1, d.y - 2].Own != Owner && _aDots[d.x + 1, d.y - 2].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own != Owner && _aDots[d.x, d.y - 2].Blocked == false
                              select d;
            if (pat62_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat62_2_3_4.First().x - 1, pat62_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 12;
            var pat12 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x + 2, d.y - 2].Own == Owner && _aDots[d.x + 2, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                            && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                            && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                            && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                            && _aDots[d.x + 2, d.y - 3].Own == 0 && _aDots[d.x + 2, d.y - 3].Blocked == false
                            && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                        select d;
            if (pat12.Count() > 0)
            {
                result_dot = new Dot(pat12.First().x, pat12.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat12_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x - 2, d.y + 2].Own == Owner && _aDots[d.x - 2, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                              && _aDots[d.x - 2, d.y + 3].Own == 0 && _aDots[d.x - 2, d.y + 3].Blocked == false
                              && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                          select d;
            if (pat12_2.Count() > 0)
            {
                result_dot = new Dot(pat12_2.First().x, pat12_2.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat12_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 2, d.y - 2].Own == Owner && _aDots[d.x + 2, d.y - 2].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                                && _aDots[d.x + 3, d.y - 2].Own == 0 && _aDots[d.x + 3, d.y - 2].Blocked == false
                                && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == enemy_own && _aDots[d.x + 1, d.y - 1].Blocked == false
                            select d;
            if (pat12_2_3.Count() > 0)
            {
                result_dot = new Dot(pat12_2_3.First().x + 2, pat12_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat12_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x - 2, d.y + 2].Own == Owner && _aDots[d.x - 2, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                                  && _aDots[d.x - 3, d.y + 2].Own == 0 && _aDots[d.x - 3, d.y + 2].Blocked == false
                                  && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                  && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == enemy_own && _aDots[d.x - 1, d.y + 1].Blocked == false
                              select d;
            if (pat12_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat12_2_3_4.First().x - 2, pat12_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 13;
            var pat13 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                            && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                            && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                            && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                            && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                        select d;
            if (pat13.Count() > 0)
            {
                result_dot = new Dot(pat13.First().x, pat13.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat13_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                              && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                              && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                              && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                              && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                          select d;
            if (pat13_2.Count() > 0)
            {
                result_dot = new Dot(pat13_2.First().x, pat13_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat13_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            select d;
            if (pat13_2_3.Count() > 0)
            {
                result_dot = new Dot(pat13_2_3.First().x - 1, pat13_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat13_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                  && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                  && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                  && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              select d;
            if (pat13_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat13_2_3_4.First().x + 1, pat13_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 14;
            var pat14 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                            && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                            && _aDots[d.x - 2, d.y - 1].Own == Owner && _aDots[d.x - 2, d.y - 1].Blocked == false
                            && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                        select d;
            if (pat14.Count() > 0)
            {
                result_dot = new Dot(pat14.First().x - 1, pat14.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat14_2 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                              && _aDots[d.x + 2, d.y + 1].Own == Owner && _aDots[d.x + 2, d.y + 1].Blocked == false
                              && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                          select d;
            if (pat14_2.Count() > 0)
            {
                result_dot = new Dot(pat14_2.First().x + 1, pat14_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat14_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 1, d.y + 1].Own == enemy_own && _aDots[d.x + 1, d.y + 1].Blocked == false
                                && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                && _aDots[d.x + 1, d.y + 2].Own == Owner && _aDots[d.x + 1, d.y + 2].Blocked == false
                                && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                            select d;
            if (pat14_2_3.Count() > 0)
            {
                result_dot = new Dot(pat14_2_3.First().x, pat14_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat14_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == enemy_own && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 2].Own == Owner && _aDots[d.x - 1, d.y - 2].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                              select d;
            if (pat14_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat14_2_3_4.First().x, pat14_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 688;
            var pat688 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 3, d.y].Own == enemy_own && _aDots[d.x + 3, d.y].Blocked == false
                             && _aDots[d.x + 4, d.y].Own == Owner && _aDots[d.x + 4, d.y].Blocked == false
                             && _aDots[d.x + 4, d.y + 1].Own == Owner && _aDots[d.x + 4, d.y + 1].Blocked == false
                             && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                             && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                             && _aDots[d.x + 3, d.y + 2].Own == 0 && _aDots[d.x + 3, d.y + 2].Blocked == false
                         select d;
            if (pat688.Count() > 0)
            {
                result_dot = new Dot(pat688.First().x + 1, pat688.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat688_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 3, d.y].Own == enemy_own && _aDots[d.x - 3, d.y].Blocked == false
                               && _aDots[d.x - 4, d.y].Own == Owner && _aDots[d.x - 4, d.y].Blocked == false
                               && _aDots[d.x - 4, d.y - 1].Own == Owner && _aDots[d.x - 4, d.y - 1].Blocked == false
                               && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                               && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                               && _aDots[d.x - 3, d.y - 2].Own == 0 && _aDots[d.x - 3, d.y - 2].Blocked == false
                           select d;
            if (pat688_2.Count() > 0)
            {
                result_dot = new Dot(pat688_2.First().x - 1, pat688_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat688_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 3].Own == enemy_own && _aDots[d.x, d.y - 3].Blocked == false
                                 && _aDots[d.x, d.y - 4].Own == Owner && _aDots[d.x, d.y - 4].Blocked == false
                                 && _aDots[d.x - 1, d.y - 4].Own == Owner && _aDots[d.x - 1, d.y - 4].Blocked == false
                                 && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                 && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                 && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                 && _aDots[d.x - 2, d.y - 3].Own == 0 && _aDots[d.x - 2, d.y - 3].Blocked == false
                             select d;
            if (pat688_2_3.Count() > 0)
            {
                result_dot = new Dot(pat688_2_3.First().x - 1, pat688_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat688_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 3].Own == enemy_own && _aDots[d.x, d.y + 3].Blocked == false
                                   && _aDots[d.x, d.y + 4].Own == Owner && _aDots[d.x, d.y + 4].Blocked == false
                                   && _aDots[d.x + 1, d.y + 4].Own == Owner && _aDots[d.x + 1, d.y + 4].Blocked == false
                                   && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                   && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                   && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                   && _aDots[d.x + 2, d.y + 3].Own == 0 && _aDots[d.x + 2, d.y + 3].Blocked == false
                               select d;
            if (pat688_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat688_2_3_4.First().x + 1, pat688_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 80;
            // тактический ход
            var pat80 = from Dot d in _aDots
                        where d.Own == Owner
                            && _aDots[d.x + 3, d.y - 3].Own == Owner && _aDots[d.x + 3, d.y - 3].Blocked == false
                            && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                            && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                            && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                            && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                            && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                            && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                            && _aDots[d.x + 2, d.y - 3].Own == 0 && _aDots[d.x + 2, d.y - 3].Blocked == false
                        select d;
            if (pat80.Count() > 0)
            {
                result_dot = new Dot(pat80.First().x + 1, pat80.First().y - 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat80_2 = from Dot d in _aDots
                          where d.Own == enemy_own
                              && _aDots[d.x - 3, d.y + 3].Own == enemy_own && _aDots[d.x - 3, d.y + 3].Blocked == false
                              && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                              && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                              && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                              && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                              && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                              && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                              && _aDots[d.x - 2, d.y + 3].Own == 0 && _aDots[d.x - 2, d.y + 3].Blocked == false
                          select d;
            if (pat80_2.Count() > 0)
            {
                result_dot = new Dot(pat80_2.First().x - 1, pat80_2.First().y + 2);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat80_2_3 = from Dot d in _aDots
                            where d.Own == enemy_own
                                && _aDots[d.x + 3, d.y - 3].Own == enemy_own && _aDots[d.x + 3, d.y - 3].Blocked == false
                                && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                && _aDots[d.x + 2, d.y - 2].Own == 0 && _aDots[d.x + 2, d.y - 2].Blocked == false
                                && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                                && _aDots[d.x + 3, d.y - 2].Own == 0 && _aDots[d.x + 3, d.y - 2].Blocked == false
                            select d;
            if (pat80_2_3.Count() > 0)
            {
                result_dot = new Dot(pat80_2_3.First().x + 2, pat80_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat80_2_3_4 = from Dot d in _aDots
                              where d.Own == enemy_own
                                  && _aDots[d.x - 3, d.y + 3].Own == enemy_own && _aDots[d.x - 3, d.y + 3].Blocked == false
                                  && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                  && _aDots[d.x - 2, d.y + 2].Own == 0 && _aDots[d.x - 2, d.y + 2].Blocked == false
                                  && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                  && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                  && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                                  && _aDots[d.x - 3, d.y + 2].Own == 0 && _aDots[d.x - 3, d.y + 2].Blocked == false
                              select d;
            if (pat80_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat80_2_3_4.First().x - 2, pat80_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            //============================================================================================================== 
            iNumberPattern = 1224;
            var pat1224 = from Dot d in _aDots
                          where d.Own == Owner
                              && _aDots[d.x + 2, d.y + 3].Own == Owner && _aDots[d.x + 2, d.y + 3].Blocked == false
                              && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                              && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                              && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                              && _aDots[d.x + 1, d.y + 3].Own != enemy_own && _aDots[d.x + 1, d.y + 3].Blocked == false
                              && _aDots[d.x + 1, d.y].Own != enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                          select d;
            if (pat1224.Count() > 0)
            {
                result_dot = new Dot(pat1224.First().x + 1, pat1224.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat1224_2 = from Dot d in _aDots
                            where d.Own == Owner
                                && _aDots[d.x - 2, d.y - 3].Own == Owner && _aDots[d.x - 2, d.y - 3].Blocked == false
                                && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                                && _aDots[d.x - 1, d.y - 3].Own != enemy_own && _aDots[d.x - 1, d.y - 3].Blocked == false
                                && _aDots[d.x - 1, d.y].Own != enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                            select d;
            if (pat1224_2.Count() > 0)
            {
                result_dot = new Dot(pat1224_2.First().x - 1, pat1224_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat1224_2_3 = from Dot d in _aDots
                              where d.Own == Owner
                                  && _aDots[d.x - 3, d.y - 2].Own == Owner && _aDots[d.x - 3, d.y - 2].Blocked == false
                                  && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                  && _aDots[d.x - 2, d.y - 2].Own == 0 && _aDots[d.x - 2, d.y - 2].Blocked == false
                                  && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                  && _aDots[d.x - 3, d.y - 1].Own != enemy_own && _aDots[d.x - 3, d.y - 1].Blocked == false
                                  && _aDots[d.x, d.y - 1].Own != enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                              select d;
            if (pat1224_2_3.Count() > 0)
            {
                result_dot = new Dot(pat1224_2_3.First().x - 1, pat1224_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1224_2_3_4 = from Dot d in _aDots
                                where d.Own == Owner
                                    && _aDots[d.x + 3, d.y + 2].Own == Owner && _aDots[d.x + 3, d.y + 2].Blocked == false
                                    && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                    && _aDots[d.x + 2, d.y + 2].Own == 0 && _aDots[d.x + 2, d.y + 2].Blocked == false
                                    && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                    && _aDots[d.x + 3, d.y + 1].Own != enemy_own && _aDots[d.x + 3, d.y + 1].Blocked == false
                                    && _aDots[d.x, d.y + 1].Own != enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                select d;
            if (pat1224_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat1224_2_3_4.First().x + 1, pat1224_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 330;
            var pat330 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x + 2, d.y - 5].Own == Owner && _aDots[d.x + 2, d.y - 5].Blocked == false
                             && _aDots[d.x + 1, d.y - 5].Own == 0 && _aDots[d.x + 1, d.y - 5].Blocked == false
                             && _aDots[d.x + 1, d.y - 4].Own == 0 && _aDots[d.x + 1, d.y - 4].Blocked == false
                             && _aDots[d.x, d.y - 4].Own == 0 && _aDots[d.x, d.y - 4].Blocked == false
                             && _aDots[d.x, d.y - 3].Own == 0 && _aDots[d.x, d.y - 3].Blocked == false
                             && _aDots[d.x + 1, d.y - 3].Own == 0 && _aDots[d.x + 1, d.y - 3].Blocked == false
                             && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 3].Own == 0 && _aDots[d.x - 1, d.y - 3].Blocked == false
                             && _aDots[d.x - 1, d.y - 2].Own == 0 && _aDots[d.x - 1, d.y - 2].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 5].Own == 0 && _aDots[d.x, d.y - 5].Blocked == false
                             && _aDots[d.x - 1, d.y - 4].Own == 0 && _aDots[d.x - 1, d.y - 4].Blocked == false
                             && _aDots[d.x + 1, d.y - 6].Own == 0 && _aDots[d.x + 1, d.y - 6].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                         select d;
            if (pat330.Count() > 0)
            {
                result_dot = new Dot(pat330.First().x, pat330.First().y - 4);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat330_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x - 2, d.y + 5].Own == Owner && _aDots[d.x - 2, d.y + 5].Blocked == false
                               && _aDots[d.x - 1, d.y + 5].Own == 0 && _aDots[d.x - 1, d.y + 5].Blocked == false
                               && _aDots[d.x - 1, d.y + 4].Own == 0 && _aDots[d.x - 1, d.y + 4].Blocked == false
                               && _aDots[d.x, d.y + 4].Own == 0 && _aDots[d.x, d.y + 4].Blocked == false
                               && _aDots[d.x, d.y + 3].Own == 0 && _aDots[d.x, d.y + 3].Blocked == false
                               && _aDots[d.x - 1, d.y + 3].Own == 0 && _aDots[d.x - 1, d.y + 3].Blocked == false
                               && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 3].Own == 0 && _aDots[d.x + 1, d.y + 3].Blocked == false
                               && _aDots[d.x + 1, d.y + 2].Own == 0 && _aDots[d.x + 1, d.y + 2].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 5].Own == 0 && _aDots[d.x, d.y + 5].Blocked == false
                               && _aDots[d.x + 1, d.y + 4].Own == 0 && _aDots[d.x + 1, d.y + 4].Blocked == false
                               && _aDots[d.x - 1, d.y + 6].Own == 0 && _aDots[d.x - 1, d.y + 6].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                           select d;
            if (pat330_2.Count() > 0)
            {
                result_dot = new Dot(pat330_2.First().x, pat330_2.First().y + 4);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat330_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 5, d.y - 2].Own == Owner && _aDots[d.x + 5, d.y - 2].Blocked == false
                                 && _aDots[d.x + 5, d.y - 1].Own == 0 && _aDots[d.x + 5, d.y - 1].Blocked == false
                                 && _aDots[d.x + 4, d.y - 1].Own == 0 && _aDots[d.x + 4, d.y - 1].Blocked == false
                                 && _aDots[d.x + 4, d.y].Own == 0 && _aDots[d.x + 4, d.y].Blocked == false
                                 && _aDots[d.x + 3, d.y].Own == 0 && _aDots[d.x + 3, d.y].Blocked == false
                                 && _aDots[d.x + 3, d.y - 1].Own == 0 && _aDots[d.x + 3, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 3, d.y + 1].Own == 0 && _aDots[d.x + 3, d.y + 1].Blocked == false
                                 && _aDots[d.x + 2, d.y + 1].Own == 0 && _aDots[d.x + 2, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 5, d.y].Own == 0 && _aDots[d.x + 5, d.y].Blocked == false
                                 && _aDots[d.x + 4, d.y + 1].Own == 0 && _aDots[d.x + 4, d.y + 1].Blocked == false
                                 && _aDots[d.x + 6, d.y - 1].Own == 0 && _aDots[d.x + 6, d.y - 1].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                             select d;
            if (pat330_2_3.Count() > 0)
            {
                result_dot = new Dot(pat330_2_3.First().x + 4, pat330_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat330_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 5, d.y + 2].Own == Owner && _aDots[d.x - 5, d.y + 2].Blocked == false
                                   && _aDots[d.x - 5, d.y + 1].Own == 0 && _aDots[d.x - 5, d.y + 1].Blocked == false
                                   && _aDots[d.x - 4, d.y + 1].Own == 0 && _aDots[d.x - 4, d.y + 1].Blocked == false
                                   && _aDots[d.x - 4, d.y].Own == 0 && _aDots[d.x - 4, d.y].Blocked == false
                                   && _aDots[d.x - 3, d.y].Own == 0 && _aDots[d.x - 3, d.y].Blocked == false
                                   && _aDots[d.x - 3, d.y + 1].Own == 0 && _aDots[d.x - 3, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 3, d.y - 1].Own == 0 && _aDots[d.x - 3, d.y - 1].Blocked == false
                                   && _aDots[d.x - 2, d.y - 1].Own == 0 && _aDots[d.x - 2, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 5, d.y].Own == 0 && _aDots[d.x - 5, d.y].Blocked == false
                                   && _aDots[d.x - 4, d.y - 1].Own == 0 && _aDots[d.x - 4, d.y - 1].Blocked == false
                                   && _aDots[d.x - 6, d.y + 1].Own == 0 && _aDots[d.x - 6, d.y + 1].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                               select d;
            if (pat330_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat330_2_3_4.First().x - 4, pat330_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 331;
            var pat331 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                             && _aDots[d.x - 2, d.y - 1].IndexRelation == _aDots[d.x + 2, d.y + 1].IndexRelation
                             && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                             && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat331.Count() > 0)
            {
                result_dot = new Dot(pat331.First().x + 1, pat331.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat331_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 2, d.y + 1].Own == enemy_own && _aDots[d.x + 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y - 1].Own == enemy_own && _aDots[d.x - 2, d.y - 1].Blocked == false
                               && _aDots[d.x + 2, d.y + 1].IndexRelation == _aDots[d.x - 2, d.y - 1].IndexRelation
                               && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                               && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                           select d;
            if (pat331_2.Count() > 0)
            {
                result_dot = new Dot(pat331_2.First().x - 1, pat331_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat331_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                 && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 2].IndexRelation == _aDots[d.x - 1, d.y - 2].IndexRelation
                                 && _aDots[d.x + 1, d.y + 1].Own == 0 && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y - 1].Own == 0 && _aDots[d.x + 1, d.y - 1].Blocked == false
                                 && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat331_2_3.Count() > 0)
            {
                result_dot = new Dot(pat331_2_3.First().x + 1, pat331_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat331_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x - 1, d.y - 2].Own == enemy_own && _aDots[d.x - 1, d.y - 2].Blocked == false
                                   && _aDots[d.x + 1, d.y + 2].Own == enemy_own && _aDots[d.x + 1, d.y + 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 2].IndexRelation == _aDots[d.x + 1, d.y + 2].IndexRelation
                                   && _aDots[d.x - 1, d.y - 1].Own == 0 && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y + 1].Own == 0 && _aDots[d.x - 1, d.y + 1].Blocked == false
                                   && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               select d;
            if (pat331_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat331_2_3_4.First().x - 1, pat331_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 312;
            var pat312 = from Dot d in _aDots
                         where d.Own == Owner
                             & _aDots[d.x - 1, d.y - 1].Own == enemy_own & _aDots[d.x - 1, d.y - 1].Blocked == false
                             & _aDots[d.x + 1, d.y + 2].Own == enemy_own & _aDots[d.x + 1, d.y + 2].Blocked == false
                             & _aDots[d.x, d.y + 1].Own != enemy_own & _aDots[d.x, d.y + 1].Blocked == false
                             & _aDots[d.x - 1, d.y].Own != enemy_own & _aDots[d.x - 1, d.y].Blocked == false
                             & _aDots[d.x, d.y - 1].Own == 0 & _aDots[d.x, d.y - 1].Blocked == false
                             & _aDots[d.x + 1, d.y - 1].Own == 0 & _aDots[d.x + 1, d.y - 1].Blocked == false
                             & _aDots[d.x + 1, d.y].Own == 0 & _aDots[d.x + 1, d.y].Blocked == false
                             & _aDots[d.x + 1, d.y + 1].Own == 0 & _aDots[d.x + 1, d.y + 1].Blocked == false
                             & _aDots[d.x, d.y - 2].Own == 0 & _aDots[d.x, d.y - 2].Blocked == false
                             & _aDots[d.x + 2, d.y].Own == 0 & _aDots[d.x + 2, d.y].Blocked == false
                             & _aDots[d.x + 2, d.y + 1].Own == 0 & _aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat312.Count() > 0)
            {
                result_dot = new Dot(pat312.First().x + 1, pat312.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat312_2 = from Dot d in _aDots
                           where d.Own == Owner
                               & _aDots[d.x + 1, d.y + 1].Own == enemy_own & _aDots[d.x + 1, d.y + 1].Blocked == false
                               & _aDots[d.x - 1, d.y - 2].Own == enemy_own & _aDots[d.x - 1, d.y - 2].Blocked == false
                               & _aDots[d.x, d.y - 1].Own != enemy_own & _aDots[d.x, d.y - 1].Blocked == false
                               & _aDots[d.x + 1, d.y].Own != enemy_own & _aDots[d.x + 1, d.y].Blocked == false
                               & _aDots[d.x, d.y + 1].Own == 0 & _aDots[d.x, d.y + 1].Blocked == false
                               & _aDots[d.x - 1, d.y + 1].Own == 0 & _aDots[d.x - 1, d.y + 1].Blocked == false
                               & _aDots[d.x - 1, d.y].Own == 0 & _aDots[d.x - 1, d.y].Blocked == false
                               & _aDots[d.x - 1, d.y - 1].Own == 0 & _aDots[d.x - 1, d.y - 1].Blocked == false
                               & _aDots[d.x, d.y + 2].Own == 0 & _aDots[d.x, d.y + 2].Blocked == false
                               & _aDots[d.x - 2, d.y].Own == 0 & _aDots[d.x - 2, d.y].Blocked == false
                               & _aDots[d.x - 2, d.y - 1].Own == 0 & _aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat312_2.Count() > 0)
            {
                result_dot = new Dot(pat312_2.First().x - 1, pat312_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat312_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 & _aDots[d.x + 1, d.y + 1].Own == enemy_own & _aDots[d.x + 1, d.y + 1].Blocked == false
                                 & _aDots[d.x - 2, d.y - 1].Own == enemy_own & _aDots[d.x - 2, d.y - 1].Blocked == false
                                 & _aDots[d.x - 1, d.y].Own != enemy_own & _aDots[d.x - 1, d.y].Blocked == false
                                 & _aDots[d.x, d.y + 1].Own != enemy_own & _aDots[d.x, d.y + 1].Blocked == false
                                 & _aDots[d.x + 1, d.y].Own == 0 & _aDots[d.x + 1, d.y].Blocked == false
                                 & _aDots[d.x + 1, d.y - 1].Own == 0 & _aDots[d.x + 1, d.y - 1].Blocked == false
                                 & _aDots[d.x, d.y - 1].Own == 0 & _aDots[d.x, d.y - 1].Blocked == false
                                 & _aDots[d.x - 1, d.y - 1].Own == 0 & _aDots[d.x - 1, d.y - 1].Blocked == false
                                 & _aDots[d.x + 2, d.y].Own == 0 & _aDots[d.x + 2, d.y].Blocked == false
                                 & _aDots[d.x, d.y - 2].Own == 0 & _aDots[d.x, d.y - 2].Blocked == false
                                 & _aDots[d.x - 1, d.y - 2].Own == 0 & _aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat312_2_3.Count() > 0)
            {
                result_dot = new Dot(pat312_2_3.First().x + 1, pat312_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat312_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   & _aDots[d.x - 1, d.y - 1].Own == enemy_own & _aDots[d.x - 1, d.y - 1].Blocked == false
                                   & _aDots[d.x + 2, d.y + 1].Own == enemy_own & _aDots[d.x + 2, d.y + 1].Blocked == false
                                   & _aDots[d.x + 1, d.y].Own != enemy_own & _aDots[d.x + 1, d.y].Blocked == false
                                   & _aDots[d.x, d.y - 1].Own != enemy_own & _aDots[d.x, d.y - 1].Blocked == false
                                   & _aDots[d.x - 1, d.y].Own == 0 & _aDots[d.x - 1, d.y].Blocked == false
                                   & _aDots[d.x - 1, d.y + 1].Own == 0 & _aDots[d.x - 1, d.y + 1].Blocked == false
                                   & _aDots[d.x, d.y + 1].Own == 0 & _aDots[d.x, d.y + 1].Blocked == false
                                   & _aDots[d.x + 1, d.y + 1].Own == 0 & _aDots[d.x + 1, d.y + 1].Blocked == false
                                   & _aDots[d.x - 2, d.y].Own == 0 & _aDots[d.x - 2, d.y].Blocked == false
                                   & _aDots[d.x, d.y + 2].Own == 0 & _aDots[d.x, d.y + 2].Blocked == false
                                   & _aDots[d.x + 1, d.y + 2].Own == 0 & _aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat312_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat312_2_3_4.First().x - 1, pat312_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 169;
            var pat169 = from Dot d in _aDots
                         where d.Own == Owner
                             & _aDots[d.x, d.y - 1].Own == enemy_own & _aDots[d.x, d.y - 1].Blocked == false
                             & _aDots[d.x, d.y + 3].Own == enemy_own & _aDots[d.x, d.y + 3].Blocked == false
                             & _aDots[d.x + 1, d.y - 1].Own == 0 & _aDots[d.x + 1, d.y - 1].Blocked == false
                             & _aDots[d.x + 1, d.y].Own == 0 & _aDots[d.x + 1, d.y].Blocked == false
                             & _aDots[d.x + 1, d.y + 1].Own == 0 & _aDots[d.x + 1, d.y + 1].Blocked == false
                             & _aDots[d.x + 1, d.y + 2].Own == 0 & _aDots[d.x + 1, d.y + 2].Blocked == false
                             & _aDots[d.x, d.y + 1].Own == 0 & _aDots[d.x, d.y + 1].Blocked == false
                             & _aDots[d.x, d.y + 2].Own == 0 & _aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat169.Count() > 0)
            {
                result_dot = new Dot(pat169.First().x + 1, pat169.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat169_2 = from Dot d in _aDots
                           where d.Own == Owner
                               & _aDots[d.x, d.y + 1].Own == enemy_own & _aDots[d.x, d.y + 1].Blocked == false
                               & _aDots[d.x, d.y - 3].Own == enemy_own & _aDots[d.x, d.y - 3].Blocked == false
                               & _aDots[d.x - 1, d.y + 1].Own == 0 & _aDots[d.x - 1, d.y + 1].Blocked == false
                               & _aDots[d.x - 1, d.y].Own == 0 & _aDots[d.x - 1, d.y].Blocked == false
                               & _aDots[d.x - 1, d.y - 1].Own == 0 & _aDots[d.x - 1, d.y - 1].Blocked == false
                               & _aDots[d.x - 1, d.y - 2].Own == 0 & _aDots[d.x - 1, d.y - 2].Blocked == false
                               & _aDots[d.x, d.y - 1].Own == 0 & _aDots[d.x, d.y - 1].Blocked == false
                               & _aDots[d.x, d.y - 2].Own == 0 & _aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat169_2.Count() > 0)
            {
                result_dot = new Dot(pat169_2.First().x - 1, pat169_2.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat169_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 & _aDots[d.x + 1, d.y].Own == enemy_own & _aDots[d.x + 1, d.y].Blocked == false
                                 & _aDots[d.x - 3, d.y].Own == enemy_own & _aDots[d.x - 3, d.y].Blocked == false
                                 & _aDots[d.x + 1, d.y - 1].Own == 0 & _aDots[d.x + 1, d.y - 1].Blocked == false
                                 & _aDots[d.x, d.y - 1].Own == 0 & _aDots[d.x, d.y - 1].Blocked == false
                                 & _aDots[d.x - 1, d.y - 1].Own == 0 & _aDots[d.x - 1, d.y - 1].Blocked == false
                                 & _aDots[d.x - 2, d.y - 1].Own == 0 & _aDots[d.x - 2, d.y - 1].Blocked == false
                                 & _aDots[d.x - 1, d.y].Own == 0 & _aDots[d.x - 1, d.y].Blocked == false
                                 & _aDots[d.x - 2, d.y].Own == 0 & _aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat169_2_3.Count() > 0)
            {
                result_dot = new Dot(pat169_2_3.First().x, pat169_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat169_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   & _aDots[d.x - 1, d.y].Own == enemy_own & _aDots[d.x - 1, d.y].Blocked == false
                                   & _aDots[d.x + 3, d.y].Own == enemy_own & _aDots[d.x + 3, d.y].Blocked == false
                                   & _aDots[d.x - 1, d.y + 1].Own == 0 & _aDots[d.x - 1, d.y + 1].Blocked == false
                                   & _aDots[d.x, d.y + 1].Own == 0 & _aDots[d.x, d.y + 1].Blocked == false
                                   & _aDots[d.x + 1, d.y + 1].Own == 0 & _aDots[d.x + 1, d.y + 1].Blocked == false
                                   & _aDots[d.x + 2, d.y + 1].Own == 0 & _aDots[d.x + 2, d.y + 1].Blocked == false
                                   & _aDots[d.x + 1, d.y].Own == 0 & _aDots[d.x + 1, d.y].Blocked == false
                                   & _aDots[d.x + 2, d.y].Own == 0 & _aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat169_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat169_2_3_4.First().x, pat169_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 30;
            var pat30 = from Dot d in _aDots
                        where d.Own == Owner
                            & _aDots[d.x + 1, d.y - 2].Own == enemy_own & _aDots[d.x + 1, d.y - 2].Blocked == false
                            & _aDots[d.x + 1, d.y - 1].Own == Owner & _aDots[d.x + 1, d.y - 1].Blocked == false
                            & _aDots[d.x, d.y - 1].Own == enemy_own & _aDots[d.x, d.y - 1].Blocked == false
                            & _aDots[d.x - 1, d.y - 1].Own == 0 & _aDots[d.x - 1, d.y - 1].Blocked == false
                            & _aDots[d.x - 1, d.y].Own == 0 & _aDots[d.x - 1, d.y].Blocked == false
                            & _aDots[d.x - 2, d.y].Own == enemy_own & _aDots[d.x - 2, d.y].Blocked == false
                        select d;
            if (pat30.Count() > 0)
            {
                result_dot = new Dot(pat30.First().x - 1, pat30.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat30_2 = from Dot d in _aDots
                          where d.Own == Owner
                              & _aDots[d.x - 1, d.y + 2].Own == enemy_own & _aDots[d.x - 1, d.y + 2].Blocked == false
                              & _aDots[d.x - 1, d.y + 1].Own == Owner & _aDots[d.x - 1, d.y + 1].Blocked == false
                              & _aDots[d.x, d.y + 1].Own == enemy_own & _aDots[d.x, d.y + 1].Blocked == false
                              & _aDots[d.x + 1, d.y + 1].Own == 0 & _aDots[d.x + 1, d.y + 1].Blocked == false
                              & _aDots[d.x + 1, d.y].Own == 0 & _aDots[d.x + 1, d.y].Blocked == false
                              & _aDots[d.x + 2, d.y].Own == enemy_own & _aDots[d.x + 2, d.y].Blocked == false
                          select d;
            if (pat30_2.Count() > 0)
            {
                result_dot = new Dot(pat30_2.First().x + 1, pat30_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat30_2_3 = from Dot d in _aDots
                            where d.Own == Owner
                                & _aDots[d.x + 2, d.y - 1].Own == enemy_own & _aDots[d.x + 2, d.y - 1].Blocked == false
                                & _aDots[d.x + 1, d.y - 1].Own == Owner & _aDots[d.x + 1, d.y - 1].Blocked == false
                                & _aDots[d.x + 1, d.y].Own == enemy_own & _aDots[d.x + 1, d.y].Blocked == false
                                & _aDots[d.x + 1, d.y + 1].Own == 0 & _aDots[d.x + 1, d.y + 1].Blocked == false
                                & _aDots[d.x, d.y + 1].Own == 0 & _aDots[d.x, d.y + 1].Blocked == false
                                & _aDots[d.x, d.y + 2].Own == enemy_own & _aDots[d.x, d.y + 2].Blocked == false
                            select d;
            if (pat30_2_3.Count() > 0)
            {
                result_dot = new Dot(pat30_2_3.First().x + 1, pat30_2_3.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat30_2_3_4 = from Dot d in _aDots
                              where d.Own == Owner
                                  & _aDots[d.x - 2, d.y + 1].Own == enemy_own & _aDots[d.x - 2, d.y + 1].Blocked == false
                                  & _aDots[d.x - 1, d.y + 1].Own == Owner & _aDots[d.x - 1, d.y + 1].Blocked == false
                                  & _aDots[d.x - 1, d.y].Own == enemy_own & _aDots[d.x - 1, d.y].Blocked == false
                                  & _aDots[d.x - 1, d.y - 1].Own == 0 & _aDots[d.x - 1, d.y - 1].Blocked == false
                                  & _aDots[d.x, d.y - 1].Own == 0 & _aDots[d.x, d.y - 1].Blocked == false
                                  & _aDots[d.x, d.y - 2].Own == enemy_own & _aDots[d.x, d.y - 2].Blocked == false
                              select d;
            if (pat30_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat30_2_3_4.First().x - 1, pat30_2_3_4.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 
            iNumberPattern = 740;
            var pat740 = from Dot d in _aDots
                         where d.Own == Owner
                             && _aDots[d.x - 3, d.y + 1].Own == enemy_own && _aDots[d.x - 3, d.y + 1].Blocked == false
                             && _aDots[d.x, d.y - 2].Own == enemy_own && _aDots[d.x, d.y - 2].Blocked == false
                             && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                             && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             && _aDots[d.x - 2, d.y + 1].Own == 0 && _aDots[d.x - 2, d.y + 1].Blocked == false
                             && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                             && _aDots[d.x - 2, d.y].Own == 0 && _aDots[d.x - 2, d.y].Blocked == false
                             && _aDots[d.x + 1, d.y].Own == enemy_own && _aDots[d.x + 1, d.y].Blocked == false
                             && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                         select d;
            if (pat740.Count() > 0)
            {
                result_dot = new Dot(pat740.First().x, pat740.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat740_2 = from Dot d in _aDots
                           where d.Own == Owner
                               && _aDots[d.x + 3, d.y - 1].Own == enemy_own && _aDots[d.x + 3, d.y - 1].Blocked == false
                               && _aDots[d.x, d.y + 2].Own == enemy_own && _aDots[d.x, d.y + 2].Blocked == false
                               && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                               && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               && _aDots[d.x + 2, d.y - 1].Own == 0 && _aDots[d.x + 2, d.y - 1].Blocked == false
                               && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                               && _aDots[d.x + 2, d.y].Own == 0 && _aDots[d.x + 2, d.y].Blocked == false
                               && _aDots[d.x - 1, d.y].Own == enemy_own && _aDots[d.x - 1, d.y].Blocked == false
                               && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                           select d;
            if (pat740_2.Count() > 0)
            {
                result_dot = new Dot(pat740_2.First().x, pat740_2.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat740_2_3 = from Dot d in _aDots
                             where d.Own == Owner
                                 && _aDots[d.x - 1, d.y + 3].Own == enemy_own && _aDots[d.x - 1, d.y + 3].Blocked == false
                                 && _aDots[d.x + 2, d.y].Own == enemy_own && _aDots[d.x + 2, d.y].Blocked == false
                                 && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                                 && _aDots[d.x, d.y + 1].Own == 0 && _aDots[d.x, d.y + 1].Blocked == false
                                 && _aDots[d.x - 1, d.y + 2].Own == 0 && _aDots[d.x - 1, d.y + 2].Blocked == false
                                 && _aDots[d.x + 1, d.y + 1].Own == Owner && _aDots[d.x + 1, d.y + 1].Blocked == false
                                 && _aDots[d.x, d.y + 2].Own == 0 && _aDots[d.x, d.y + 2].Blocked == false
                                 && _aDots[d.x, d.y - 1].Own == enemy_own && _aDots[d.x, d.y - 1].Blocked == false
                                 && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                             select d;
            if (pat740_2_3.Count() > 0)
            {
                result_dot = new Dot(pat740_2_3.First().x - 1, pat740_2_3.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat740_2_3_4 = from Dot d in _aDots
                               where d.Own == Owner
                                   && _aDots[d.x + 1, d.y - 3].Own == enemy_own && _aDots[d.x + 1, d.y - 3].Blocked == false
                                   && _aDots[d.x - 2, d.y].Own == enemy_own && _aDots[d.x - 2, d.y].Blocked == false
                                   && _aDots[d.x - 1, d.y].Own == 0 && _aDots[d.x - 1, d.y].Blocked == false
                                   && _aDots[d.x, d.y - 1].Own == 0 && _aDots[d.x, d.y - 1].Blocked == false
                                   && _aDots[d.x + 1, d.y - 2].Own == 0 && _aDots[d.x + 1, d.y - 2].Blocked == false
                                   && _aDots[d.x - 1, d.y - 1].Own == Owner && _aDots[d.x - 1, d.y - 1].Blocked == false
                                   && _aDots[d.x, d.y - 2].Own == 0 && _aDots[d.x, d.y - 2].Blocked == false
                                   && _aDots[d.x, d.y + 1].Own == enemy_own && _aDots[d.x, d.y + 1].Blocked == false
                                   && _aDots[d.x + 1, d.y].Own == 0 && _aDots[d.x + 1, d.y].Blocked == false
                               select d;
            if (pat740_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat740_2_3_4.First().x + 1, pat740_2_3_4.First().y);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

            iNumberPattern = 919;
            var pat919 = from Dot d in aDots
                         where d.Own == Owner
                             && aDots[d.x + 2, d.y - 3].Own == 0 && aDots[d.x + 2, d.y - 3].Blocked == false
                             && aDots[d.x + 2, d.y - 2].Own == 0 && aDots[d.x + 2, d.y - 2].Blocked == false
                             && aDots[d.x + 2, d.y - 1].Own == 0 && aDots[d.x + 2, d.y - 1].Blocked == false
                             && aDots[d.x + 1, d.y - 2].Own == 0 && aDots[d.x + 1, d.y - 2].Blocked == false
                             && aDots[d.x + 1, d.y].Own == 0 && aDots[d.x + 1, d.y].Blocked == false
                             && aDots[d.x - 1, d.y].Own == 0 && aDots[d.x - 1, d.y].Blocked == false
                             && aDots[d.x - 2, d.y].Own == 0 && aDots[d.x - 2, d.y].Blocked == false
                             && aDots[d.x - 1, d.y - 1].Own == 0 && aDots[d.x - 1, d.y - 1].Blocked == false
                             && aDots[d.x, d.y - 1].Own == 0 && aDots[d.x, d.y - 1].Blocked == false
                             && aDots[d.x, d.y - 2].Own == enemy_own && aDots[d.x, d.y - 2].Blocked == false
                             && aDots[d.x - 1, d.y - 2].Own == enemy_own && aDots[d.x - 1, d.y - 2].Blocked == false
                             && aDots[d.x + 1, d.y - 1].Own == enemy_own && aDots[d.x + 1, d.y - 1].Blocked == false
                             && aDots[d.x + 1, d.y - 3].Own == enemy_own && aDots[d.x + 1, d.y - 3].Blocked == false
                             && aDots[d.x, d.y - 3].Own == enemy_own && aDots[d.x, d.y - 3].Blocked == false
                             && aDots[d.x - 1, d.y - 3].Own == enemy_own && aDots[d.x - 1, d.y - 3].Blocked == false
                             && aDots[d.x - 2, d.y - 2].Own == enemy_own && aDots[d.x - 2, d.y - 2].Blocked == false
                             && aDots[d.x - 2, d.y - 1].Own == enemy_own && aDots[d.x - 2, d.y - 1].Blocked == false
                             && aDots[d.x - 2, d.y - 3].Own != enemy_own && aDots[d.x - 2, d.y - 3].Blocked == false
                         select d;
            if (pat919.Count() > 0)
            {
                result_dot = new Dot(pat919.First().x + 1, pat919.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //180 Rotate=========================================================================================================== 
            var pat919_2 = from Dot d in aDots
                           where d.Own == Owner
                               && aDots[d.x - 2, d.y + 3].Own == 0 && aDots[d.x - 2, d.y + 3].Blocked == false
                               && aDots[d.x - 2, d.y + 2].Own == 0 && aDots[d.x - 2, d.y + 2].Blocked == false
                               && aDots[d.x - 2, d.y + 1].Own == 0 && aDots[d.x - 2, d.y + 1].Blocked == false
                               && aDots[d.x - 1, d.y + 2].Own == 0 && aDots[d.x - 1, d.y + 2].Blocked == false
                               && aDots[d.x - 1, d.y].Own == 0 && aDots[d.x - 1, d.y].Blocked == false
                               && aDots[d.x + 1, d.y].Own == 0 && aDots[d.x + 1, d.y].Blocked == false
                               && aDots[d.x + 2, d.y].Own == 0 && aDots[d.x + 2, d.y].Blocked == false
                               && aDots[d.x + 1, d.y + 1].Own == 0 && aDots[d.x + 1, d.y + 1].Blocked == false
                               && aDots[d.x, d.y + 1].Own == 0 && aDots[d.x, d.y + 1].Blocked == false
                               && aDots[d.x, d.y + 2].Own == enemy_own && aDots[d.x, d.y + 2].Blocked == false
                               && aDots[d.x + 1, d.y + 2].Own == enemy_own && aDots[d.x + 1, d.y + 2].Blocked == false
                               && aDots[d.x - 1, d.y + 1].Own == enemy_own && aDots[d.x - 1, d.y + 1].Blocked == false
                               && aDots[d.x - 1, d.y + 3].Own == enemy_own && aDots[d.x - 1, d.y + 3].Blocked == false
                               && aDots[d.x, d.y + 3].Own == enemy_own && aDots[d.x, d.y + 3].Blocked == false
                               && aDots[d.x + 1, d.y + 3].Own == enemy_own && aDots[d.x + 1, d.y + 3].Blocked == false
                               && aDots[d.x + 2, d.y + 2].Own == enemy_own && aDots[d.x + 2, d.y + 2].Blocked == false
                               && aDots[d.x + 2, d.y + 1].Own == enemy_own && aDots[d.x + 2, d.y + 1].Blocked == false
                               && aDots[d.x + 2, d.y + 3].Own != enemy_own && aDots[d.x + 2, d.y + 3].Blocked == false
                           select d;
            if (pat919_2.Count() > 0)
            {
                result_dot = new Dot(pat919_2.First().x - 1, pat919_2.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90----------------------------------- 
            var pat919_2_3 = from Dot d in aDots
                             where d.Own == Owner
                                 && aDots[d.x + 3, d.y - 2].Own == 0 && aDots[d.x + 3, d.y - 2].Blocked == false
                                 && aDots[d.x + 2, d.y - 2].Own == 0 && aDots[d.x + 2, d.y - 2].Blocked == false
                                 && aDots[d.x + 1, d.y - 2].Own == 0 && aDots[d.x + 1, d.y - 2].Blocked == false
                                 && aDots[d.x + 2, d.y - 1].Own == 0 && aDots[d.x + 2, d.y - 1].Blocked == false
                                 && aDots[d.x, d.y - 1].Own == 0 && aDots[d.x, d.y - 1].Blocked == false
                                 && aDots[d.x, d.y + 1].Own == 0 && aDots[d.x, d.y + 1].Blocked == false
                                 && aDots[d.x, d.y + 2].Own == 0 && aDots[d.x, d.y + 2].Blocked == false
                                 && aDots[d.x + 1, d.y + 1].Own == 0 && aDots[d.x + 1, d.y + 1].Blocked == false
                                 && aDots[d.x + 1, d.y].Own == 0 && aDots[d.x + 1, d.y].Blocked == false
                                 && aDots[d.x + 2, d.y].Own == enemy_own && aDots[d.x + 2, d.y].Blocked == false
                                 && aDots[d.x + 2, d.y + 1].Own == enemy_own && aDots[d.x + 2, d.y + 1].Blocked == false
                                 && aDots[d.x + 1, d.y - 1].Own == enemy_own && aDots[d.x + 1, d.y - 1].Blocked == false
                                 && aDots[d.x + 3, d.y - 1].Own == enemy_own && aDots[d.x + 3, d.y - 1].Blocked == false
                                 && aDots[d.x + 3, d.y].Own == enemy_own && aDots[d.x + 3, d.y].Blocked == false
                                 && aDots[d.x + 3, d.y + 1].Own == enemy_own && aDots[d.x + 3, d.y + 1].Blocked == false
                                 && aDots[d.x + 2, d.y + 2].Own == enemy_own && aDots[d.x + 2, d.y + 2].Blocked == false
                                 && aDots[d.x + 1, d.y + 2].Own == enemy_own && aDots[d.x + 1, d.y + 2].Blocked == false
                                 && aDots[d.x + 3, d.y + 2].Own != enemy_own && aDots[d.x + 3, d.y + 2].Blocked == false
                             select d;
            if (pat919_2_3.Count() > 0)
            {
                result_dot = new Dot(pat919_2_3.First().x + 1, pat919_2_3.First().y - 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat919_2_3_4 = from Dot d in aDots
                               where d.Own == Owner
                                   && aDots[d.x - 3, d.y + 2].Own == 0 && aDots[d.x - 3, d.y + 2].Blocked == false
                                   && aDots[d.x - 2, d.y + 2].Own == 0 && aDots[d.x - 2, d.y + 2].Blocked == false
                                   && aDots[d.x - 1, d.y + 2].Own == 0 && aDots[d.x - 1, d.y + 2].Blocked == false
                                   && aDots[d.x - 2, d.y + 1].Own == 0 && aDots[d.x - 2, d.y + 1].Blocked == false
                                   && aDots[d.x, d.y + 1].Own == 0 && aDots[d.x, d.y + 1].Blocked == false
                                   && aDots[d.x, d.y - 1].Own == 0 && aDots[d.x, d.y - 1].Blocked == false
                                   && aDots[d.x, d.y - 2].Own == 0 && aDots[d.x, d.y - 2].Blocked == false
                                   && aDots[d.x - 1, d.y - 1].Own == 0 && aDots[d.x - 1, d.y - 1].Blocked == false
                                   && aDots[d.x - 1, d.y].Own == 0 && aDots[d.x - 1, d.y].Blocked == false
                                   && aDots[d.x - 2, d.y].Own == enemy_own && aDots[d.x - 2, d.y].Blocked == false
                                   && aDots[d.x - 2, d.y - 1].Own == enemy_own && aDots[d.x - 2, d.y - 1].Blocked == false
                                   && aDots[d.x - 1, d.y + 1].Own == enemy_own && aDots[d.x - 1, d.y + 1].Blocked == false
                                   && aDots[d.x - 3, d.y + 1].Own == enemy_own && aDots[d.x - 3, d.y + 1].Blocked == false
                                   && aDots[d.x - 3, d.y].Own == enemy_own && aDots[d.x - 3, d.y].Blocked == false
                                   && aDots[d.x - 3, d.y - 1].Own == enemy_own && aDots[d.x - 3, d.y - 1].Blocked == false
                                   && aDots[d.x - 2, d.y - 2].Own == enemy_own && aDots[d.x - 2, d.y - 2].Blocked == false
                                   && aDots[d.x - 1, d.y - 2].Own == enemy_own && aDots[d.x - 1, d.y - 2].Blocked == false
                                   && aDots[d.x - 3, d.y - 2].Own != enemy_own && aDots[d.x - 3, d.y - 2].Blocked == false
                               select d;
            if (pat919_2_3_4.Count() > 0)
            {
                result_dot = new Dot(pat919_2_3_4.First().x - 1, pat919_2_3_4.First().y + 1);
                result_dot.iNumberPattern = iNumberPattern;
                return result_dot;
            }
            //============================================================================================================== 

iNumberPattern = 831; 
var pat831 = from Dot d in aDots where d.Own == Owner 
 && aDots[d.x+4, d.y-2].Own == Owner && aDots[d.x+4, d.y-2].Blocked == false 
 && aDots[d.x+1, d.y].Own  == 0 && aDots[d.x+1, d.y].Blocked == false 
 && aDots[d.x+2, d.y].Own  == 0 && aDots[d.x+2, d.y].Blocked == false 
 && aDots[d.x+3, d.y].Own  == 0 && aDots[d.x+3, d.y].Blocked == false 
 && aDots[d.x+4, d.y].Own  == 0 && aDots[d.x+4, d.y].Blocked == false 
 && aDots[d.x+4, d.y-1].Own  == 0 && aDots[d.x+4, d.y-1].Blocked == false 
 && aDots[d.x+3, d.y-1].Own  == 0 && aDots[d.x+3, d.y-1].Blocked == false 
 && aDots[d.x+1, d.y+1].Own  == 0 && aDots[d.x+1, d.y+1].Blocked == false 
 && aDots[d.x+2, d.y+1].Own  == 0 && aDots[d.x+2, d.y+1].Blocked == false 
 && aDots[d.x+3, d.y+1].Own  == 0 && aDots[d.x+3, d.y+1].Blocked == false 
 && aDots[d.x+4, d.y+1].Own  == 0 && aDots[d.x+4, d.y+1].Blocked == false 
 && aDots[d.x+5, d.y-1].Own  == 0 && aDots[d.x+5, d.y-1].Blocked == false 
 && aDots[d.x+5, d.y].Own  == 0 && aDots[d.x+5, d.y].Blocked == false 
select d; 
 if (pat831.Count() > 0) return new Dot(pat831.First().x+3,pat831.First().y);

//180 Rotate=========================================================================================================== 
var pat831_2 = from Dot d in aDots where d.Own == Owner 
 && aDots[d.x-4, d.y+2].Own == Owner && aDots[d.x-4, d.y+2].Blocked == false 
 && aDots[d.x-1, d.y].Own  == 0 && aDots[d.x-1, d.y].Blocked == false 
 && aDots[d.x-2, d.y].Own  == 0 && aDots[d.x-2, d.y].Blocked == false 
 && aDots[d.x-3, d.y].Own  == 0 && aDots[d.x-3, d.y].Blocked == false 
 && aDots[d.x-4, d.y].Own  == 0 && aDots[d.x-4, d.y].Blocked == false 
 && aDots[d.x-4, d.y+1].Own  == 0 && aDots[d.x-4, d.y+1].Blocked == false 
 && aDots[d.x-3, d.y+1].Own  == 0 && aDots[d.x-3, d.y+1].Blocked == false 
 && aDots[d.x-1, d.y-1].Own  == 0 && aDots[d.x-1, d.y-1].Blocked == false 
 && aDots[d.x-2, d.y-1].Own  == 0 && aDots[d.x-2, d.y-1].Blocked == false 
 && aDots[d.x-3, d.y-1].Own  == 0 && aDots[d.x-3, d.y-1].Blocked == false 
 && aDots[d.x-4, d.y-1].Own  == 0 && aDots[d.x-4, d.y-1].Blocked == false 
 && aDots[d.x-5, d.y+1].Own  == 0 && aDots[d.x-5, d.y+1].Blocked == false 
 && aDots[d.x-5, d.y].Own  == 0 && aDots[d.x-5, d.y].Blocked == false 
select d; 
 if (pat831_2.Count() > 0) 
 {
    result_dot = new Dot(pat831_2.First().x-3,pat831_2.First().y); 
    result_dot.iNumberPattern = iNumberPattern; 
    return result_dot;
    }
//--------------Rotate on 90----------------------------------- 
var pat831_2_3 = from Dot d in aDots where d.Own == Owner 
 && aDots[d.x+2, d.y-4].Own == Owner && aDots[d.x+2, d.y-4].Blocked == false 
 && aDots[d.x, d.y-1].Own  == 0 && aDots[d.x, d.y-1].Blocked == false 
 && aDots[d.x, d.y-2].Own  == 0 && aDots[d.x, d.y-2].Blocked == false 
 && aDots[d.x, d.y-3].Own  == 0 && aDots[d.x, d.y-3].Blocked == false 
 && aDots[d.x, d.y-4].Own  == 0 && aDots[d.x, d.y-4].Blocked == false 
 && aDots[d.x+1, d.y-4].Own  == 0 && aDots[d.x+1, d.y-4].Blocked == false 
 && aDots[d.x+1, d.y-3].Own  == 0 && aDots[d.x+1, d.y-3].Blocked == false 
 && aDots[d.x-1, d.y-1].Own  == 0 && aDots[d.x-1, d.y-1].Blocked == false 
 && aDots[d.x-1, d.y-2].Own  == 0 && aDots[d.x-1, d.y-2].Blocked == false 
 && aDots[d.x-1, d.y-3].Own  == 0 && aDots[d.x-1, d.y-3].Blocked == false 
 && aDots[d.x-1, d.y-4].Own  == 0 && aDots[d.x-1, d.y-4].Blocked == false 
 && aDots[d.x+1, d.y-5].Own  == 0 && aDots[d.x+1, d.y-5].Blocked == false 
 && aDots[d.x, d.y-5].Own  == 0 && aDots[d.x, d.y-5].Blocked == false 
select d; 
 if (pat831_2_3.Count() > 0) 
 {result_dot = new Dot(pat831_2_3.First().x,pat831_2_3.First().y-3); 
result_dot.iNumberPattern = iNumberPattern; 
return result_dot;}
//--------------Rotate on 90 - 2----------------------------------- 
var pat831_2_3_4 = from Dot d in aDots where d.Own == Owner 
 && aDots[d.x-2, d.y+4].Own == Owner && aDots[d.x-2, d.y+4].Blocked == false 
 && aDots[d.x, d.y+1].Own  == 0 && aDots[d.x, d.y+1].Blocked == false 
 && aDots[d.x, d.y+2].Own  == 0 && aDots[d.x, d.y+2].Blocked == false 
 && aDots[d.x, d.y+3].Own  == 0 && aDots[d.x, d.y+3].Blocked == false 
 && aDots[d.x, d.y+4].Own  == 0 && aDots[d.x, d.y+4].Blocked == false 
 && aDots[d.x-1, d.y+4].Own  == 0 && aDots[d.x-1, d.y+4].Blocked == false 
 && aDots[d.x-1, d.y+3].Own  == 0 && aDots[d.x-1, d.y+3].Blocked == false 
 && aDots[d.x+1, d.y+1].Own  == 0 && aDots[d.x+1, d.y+1].Blocked == false 
 && aDots[d.x+1, d.y+2].Own  == 0 && aDots[d.x+1, d.y+2].Blocked == false 
 && aDots[d.x+1, d.y+3].Own  == 0 && aDots[d.x+1, d.y+3].Blocked == false 
 && aDots[d.x+1, d.y+4].Own  == 0 && aDots[d.x+1, d.y+4].Blocked == false 
 && aDots[d.x-1, d.y+5].Own  == 0 && aDots[d.x-1, d.y+5].Blocked == false 
 && aDots[d.x, d.y+5].Own  == 0 && aDots[d.x, d.y+5].Blocked == false 
select d; 
 if (pat831_2_3_4.Count() > 0) 
 {result_dot = new Dot(pat831_2_3_4.First().x,pat831_2_3_4.First().y+3); 
result_dot.iNumberPattern = iNumberPattern; 
return result_dot;}
//============================================================================================================== 
 iNumberPattern = 271;
 var pat271 = from Dot d in aDots
             where d.Own == Owner
                 && aDots[d.x + 1, d.y + 2].Own == enemy_own && aDots[d.x + 1, d.y + 2].Blocked == false
                 && aDots[d.x + 1, d.y + 1].Own == Owner && aDots[d.x + 1, d.y + 1].Blocked == false
                 && aDots[d.x, d.y + 1].Own == enemy_own && aDots[d.x, d.y + 1].Blocked == false
                 && aDots[d.x - 1, d.y].Own == enemy_own && aDots[d.x - 1, d.y].Blocked == false
                 && aDots[d.x - 1, d.y - 1].Own == 0 && aDots[d.x - 1, d.y - 1].Blocked == false
                 && aDots[d.x - 2, d.y - 1].Own == 0 && aDots[d.x - 2, d.y - 1].Blocked == false
                 && aDots[d.x - 2, d.y].Own == 0 && aDots[d.x - 2, d.y].Blocked == false
                 && aDots[d.x - 1, d.y + 1].Own == 0 && aDots[d.x - 1, d.y + 1].Blocked == false
                 && aDots[d.x, d.y + 2].Own == 0 && aDots[d.x, d.y + 2].Blocked == false
                 && aDots[d.x - 2, d.y - 2].Own == enemy_own && aDots[d.x - 2, d.y - 2].Blocked == false
                 && aDots[d.x - 1, d.y + 2].Own != enemy_own && aDots[d.x - 1, d.y + 2].Blocked == false
                 && aDots[d.x - 2, d.y + 1].Own != enemy_own && aDots[d.x - 2, d.y + 1].Blocked == false
             select d;
 if (pat271.Count() > 0) 
 {
     result_dot = new Dot(pat271.First().x - 1, pat271.First().y + 1); ;
    result_dot.iNumberPattern = iNumberPattern;
    return result_dot;

 }
 //180 Rotate=========================================================================================================== 
 var pat271_2 = from Dot d in aDots
               where d.Own == Owner
                   && aDots[d.x - 1, d.y - 2].Own == enemy_own && aDots[d.x - 1, d.y - 2].Blocked == false
                   && aDots[d.x - 1, d.y - 1].Own == Owner && aDots[d.x - 1, d.y - 1].Blocked == false
                   && aDots[d.x, d.y - 1].Own == enemy_own && aDots[d.x, d.y - 1].Blocked == false
                   && aDots[d.x + 1, d.y].Own == enemy_own && aDots[d.x + 1, d.y].Blocked == false
                   && aDots[d.x + 1, d.y + 1].Own == 0 && aDots[d.x + 1, d.y + 1].Blocked == false
                   && aDots[d.x + 2, d.y + 1].Own == 0 && aDots[d.x + 2, d.y + 1].Blocked == false
                   && aDots[d.x + 2, d.y].Own == 0 && aDots[d.x + 2, d.y].Blocked == false
                   && aDots[d.x + 1, d.y - 1].Own == 0 && aDots[d.x + 1, d.y - 1].Blocked == false
                   && aDots[d.x, d.y - 2].Own == 0 && aDots[d.x, d.y - 2].Blocked == false
                   && aDots[d.x + 2, d.y + 2].Own == enemy_own && aDots[d.x + 2, d.y + 2].Blocked == false
                   && aDots[d.x + 1, d.y - 2].Own != enemy_own && aDots[d.x + 1, d.y - 2].Blocked == false
                   && aDots[d.x + 2, d.y - 1].Own != enemy_own && aDots[d.x + 2, d.y - 1].Blocked == false
               select d;
 if (pat271_2.Count() > 0)
 {
     result_dot = new Dot(pat271_2.First().x + 1, pat271_2.First().y - 1);
     result_dot.iNumberPattern = iNumberPattern;
     return result_dot;
 }
 //--------------Rotate on 90----------------------------------- 
 var pat271_2_3 = from Dot d in aDots
                 where d.Own == Owner
                     && aDots[d.x - 2, d.y - 1].Own == enemy_own && aDots[d.x - 2, d.y - 1].Blocked == false
                     && aDots[d.x - 1, d.y - 1].Own == Owner && aDots[d.x - 1, d.y - 1].Blocked == false
                     && aDots[d.x - 1, d.y].Own == enemy_own && aDots[d.x - 1, d.y].Blocked == false
                     && aDots[d.x, d.y + 1].Own == enemy_own && aDots[d.x, d.y + 1].Blocked == false
                     && aDots[d.x + 1, d.y + 1].Own == 0 && aDots[d.x + 1, d.y + 1].Blocked == false
                     && aDots[d.x + 1, d.y + 2].Own == 0 && aDots[d.x + 1, d.y + 2].Blocked == false
                     && aDots[d.x, d.y + 2].Own == 0 && aDots[d.x, d.y + 2].Blocked == false
                     && aDots[d.x - 1, d.y + 1].Own == 0 && aDots[d.x - 1, d.y + 1].Blocked == false
                     && aDots[d.x - 2, d.y].Own == 0 && aDots[d.x - 2, d.y].Blocked == false
                     && aDots[d.x + 2, d.y + 2].Own == enemy_own && aDots[d.x + 2, d.y + 2].Blocked == false
                     && aDots[d.x - 2, d.y + 1].Own != enemy_own && aDots[d.x - 2, d.y + 1].Blocked == false
                     && aDots[d.x - 1, d.y + 2].Own != enemy_own && aDots[d.x - 1, d.y + 2].Blocked == false
                 select d;
 if (pat271_2_3.Count() > 0)
 {
     result_dot = new Dot(pat271_2_3.First().x - 1, pat271_2_3.First().y + 1);
     result_dot.iNumberPattern = iNumberPattern;
     return result_dot;
 }
 //--------------Rotate on 90 - 2----------------------------------- 
 var pat271_2_3_4 = from Dot d in aDots
                   where d.Own == Owner
                       && aDots[d.x + 2, d.y + 1].Own == enemy_own && aDots[d.x + 2, d.y + 1].Blocked == false
                       && aDots[d.x + 1, d.y + 1].Own == Owner && aDots[d.x + 1, d.y + 1].Blocked == false
                       && aDots[d.x + 1, d.y].Own == enemy_own && aDots[d.x + 1, d.y].Blocked == false
                       && aDots[d.x, d.y - 1].Own == enemy_own && aDots[d.x, d.y - 1].Blocked == false
                       && aDots[d.x - 1, d.y - 1].Own == 0 && aDots[d.x - 1, d.y - 1].Blocked == false
                       && aDots[d.x - 1, d.y - 2].Own == 0 && aDots[d.x - 1, d.y - 2].Blocked == false
                       && aDots[d.x, d.y - 2].Own == 0 && aDots[d.x, d.y - 2].Blocked == false
                       && aDots[d.x + 1, d.y - 1].Own == 0 && aDots[d.x + 1, d.y - 1].Blocked == false
                       && aDots[d.x + 2, d.y].Own == 0 && aDots[d.x + 2, d.y].Blocked == false
                       && aDots[d.x - 2, d.y - 2].Own == enemy_own && aDots[d.x - 2, d.y - 2].Blocked == false
                       && aDots[d.x + 2, d.y - 1].Own != enemy_own && aDots[d.x + 2, d.y - 1].Blocked == false
                       && aDots[d.x + 1, d.y - 2].Own != enemy_own && aDots[d.x + 1, d.y - 2].Blocked == false
                   select d;
 if (pat271_2_3_4.Count() > 0)
 {
     result_dot = new Dot(pat271_2_3_4.First().x + 1, pat271_2_3_4.First().y - 1);
     result_dot.iNumberPattern = iNumberPattern;
     return result_dot;
 }
 //============================================================================================================== 



            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль

        }
    }
}