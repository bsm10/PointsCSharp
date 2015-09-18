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
        private Dot CheckPattern(int Owner)
        {
            int enemy_own = Owner == 1 ? 2 : 1;
            var get_non_blocked = from Dot d in aDots where d.Blocked == false select d; //получить коллекцию незаблокированных точек

            //паттерн на диагональное расположение точек   d*                   
            //                                              +  *    
            //                                              *  +  * 
            var pat1 = from Dot d in get_non_blocked
                       where d.Own == Owner & aDots[d.x + 1, d.y + 1].Own == Owner & 
                       aDots[d.x + 2, d.y + 2].Own == Owner & aDots[d.x, d.y + 1].Own == enemy_own
                       & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 2].Own == enemy_own
                       & aDots[d.x + 1, d.y + 3].Own == 0 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x, d.y + 3].Own == 0
                       select d;
            foreach (Dot p in pat1)
            {
                if (aDots[p.x, p.y + 2].Own == PLAYER_NONE){
#if DEBUG
                    sPattern="pat1";
#endif
                    return new Dot(p.x, p.y + 2);
                    
                }
            }
            //**********************************************************************************        

            //паттерн на диагональное расположение точек    d*  +  *   
            //                                                 *  +  
            //                                                    * 
            var pat1_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 2].Own == Owner &
                           aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 2, d.y + 1].Own == enemy_own &
                           aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 3, d.y].Own == 0 & aDots[d.x + 3, d.y + 1].Own == 0

                         select d;
            foreach (Dot p in pat1_1)
            {
                if (aDots[p.x + 2, p.y].Own == PLAYER_NONE)
                {
#if DEBUG
                    sPattern = "pat1_1";
#endif
                    return new Dot(p.x + 2, p.y);
                }
            }

            //паттерн на диагональное расположение точек    *  +  *d   
            //                                              +  *    
            //                                              *       
            //то же но в обратную сторону
            var pat2 = from Dot d in get_non_blocked
                       where d.Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 2].Own == Owner &
                       aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 2, d.y + 1].Own == enemy_own & aDots[d.x - 3, d.y].Own == 0 &
                       aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 3, d.y + 1].Own == 0
                       select d;
            foreach (Dot p in pat2)
            {
                if (aDots[p.x - 2, p.y].Own == PLAYER_NONE){
#if DEBUG
                    sPattern="pat2";
#endif
                    return new Dot(p.x - 2, p.y);
                }
            }
            //паттерн на диагональное расположение точек         *d   
            //                                                *  +  
            //                                             *  +  *   

            var pat2_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 2].Own == Owner &
                       aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 1].Own == 0 &
                       aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x, d.y + 3].Own == 0 & aDots[d.x - 1, d.y + 3].Own == 0
                         select d;
            foreach (Dot p in pat2_1)
            {
                if (aDots[p.x, p.y + 2].Own == PLAYER_NONE){
#if DEBUG
                    sPattern = "pat2_1";
#endif
                    return new Dot(p.x, p.y + 2);
                }
            }
            //===========ВИЛОЧКА=================================================================================================== 
            var pat465 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y].Own == Owner & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 3, d.y].Own == 0
                         select d;
            if (pat465.Count() > 0) {
#if DEBUG
                    sPattern="pat465";
#endif
                    return new Dot(pat465.First().x - 1, pat465.First().y - 1);
               }
            //--------------Rotate on 180----------------------------------- 
            var pat465_2 = from Dot d in get_non_blocked
                           where d.Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y].Own == Owner & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 3, d.y].Own == 0
                           select d;
            if (pat465_2.Count() > 0){
#if DEBUG
                    sPattern="pat465_2";
#endif
                    return new Dot(pat465_2.First().x + 1, pat465_2.First().y + 1);
            }
            //============================================================================================================== 
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //                           *     +  ставить сюда 
            var pat3 = from Dot d in get_non_blocked
                       where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                              aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                              aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                              aDots[d.x + 1, d.y + 1].Own == 0 &
                                              aDots[d.x + 1, d.y].Own == 0 &
                                              aDots[d.x, d.y + 1].Own == 0
                                              select d;
            if (pat3.Count() > 0) return new Dot(pat3.First().x + 1, pat3.First().y + 1);
            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //               cтавить сюда+     *   
            var pat3_1_1 = from Dot d in get_non_blocked
                           where aDots[d.x + 1, d.y - 1].Own == enemy_own & 
                                 aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                 aDots[d.x + 1, d.y + 1].Own == enemy_own &
                                 aDots[d.x - 1, d.y].Own == 0 &
                                 aDots[d.x, d.y + 1].Own == 0 &
                                 aDots[d.x-1, d.y + 1].Own == 0 &
                                 aDots[d.x, d.y].Own == Owner
                               select d;
            if (pat3_1_1.Count() > 0) return new Dot(pat3_1_1.First().x - 1, pat3_1_1.First().y + 1);
            // паттерн на конструкцию    +     *      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_2 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y - 1].Own == 0 &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own
                                              & aDots[d.x - 1, d.y].Own != Owner &
                                              aDots[d.x + 1, d.y].Own != Owner &
                                              aDots[d.x, d.y + 1].Own != Owner &
                                              aDots[d.x, d.y - 1].Own != Owner

                         select d;
            if (pat3_2.Count() > 0) return new Dot(pat3_2.First().x - 1, pat3_2.First().y - 1);
            // паттерн на конструкцию    *     +      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_3 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0 &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own
                                              & aDots[d.x - 1, d.y].Own != Owner &
                                              aDots[d.x + 1, d.y].Own != Owner &
                                              aDots[d.x, d.y + 1].Own != Owner &
                                              aDots[d.x, d.y - 1].Own != Owner

                         select d;
            if (pat3_3.Count() > 0) return new Dot(pat3_3.First().x + 1, pat3_3.First().y - 1);

             //паттерн на конструкцию    *     +      точка окружена через две точки
             //                            d+    
             //                          *     *   
            var pat3_4 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0 &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own
                                              & aDots[d.x - 1, d.y].Own != Owner &
                                              aDots[d.x + 1, d.y].Own != Owner &
                                              aDots[d.x, d.y + 1].Own != Owner &
                                              aDots[d.x, d.y - 1].Own != Owner

                         select d;
            if (pat3_4.Count() > 0) return new Dot(pat3_4.First().x + 1, pat3_4.First().y - 1);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию    + d+         
            //                          *     *   
            var pat4_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == Owner &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 2].Own == 0 &
                                                aDots[d.x + 1, d.y].Own == 0 
                         select d;
            if (pat4_1.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_1";
#endif
            return new Dot(pat4_1.First().x , pat4_1.First().y + 1);
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию      d+  +         
            //                          *     *   
            var pat4_2 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == 0 &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 2].Own == 0 &
                                                aDots[d.x + 1, d.y].Own == Owner
                         select d;
            if (pat4_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_2";
#endif
            return new Dot(pat4_2.First().x, pat4_2.First().y + 1);
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию            
            //                          *     *   
            //                          + d+ 
            var pat4_3 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == Owner &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 2].Own == 0 &
                                                aDots[d.x + 1, d.y].Own == 0
                         select d;
            if (pat4_3.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat4_3";
#endif
            return new Dot(pat4_3.First().x, pat4_3.First().y - 1);
            }
            //паттерн на конструкцию            
            //                          *     *   
            //                            d+  +
            var pat4_4 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y].Own == Owner &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 2].Own == 0 &
                                                aDots[d.x - 1, d.y].Own == 0
                         select d;
            if (pat4_4.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_4";
#endif
            return new Dot(pat4_4.First().x, pat4_4.First().y - 1);
            }

            //паттерн на конструкцию    *        
            //                             +  
            //                          * d+  
            var pat4_5 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x , d.y-1].Own == Owner &
                                                aDots[d.x - 1, d.y].Own == enemy_own &
                                                aDots[d.x - 1, d.y - 1].Own == 0 &
                                                aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Own == 0 &
                                                aDots[d.x - 2, d.y - 1].Own == 0
                         select d;
            if (pat4_5.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_5";
#endif
            return new Dot(pat4_5.First().x-1, pat4_5.First().y - 1);
            }
            //паттерн на конструкцию    *  +       
            //                            d+  
            //                          *   
            var pat4_6 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y - 1].Own == Owner &
                                                aDots[d.x - 1, d.y-1].Own == enemy_own &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y].Own == 0 & aDots[d.x, d.y + 2].Own == 0 &
                                                aDots[d.x - 2, d.y].Own == 0
                         select d;
            if (pat4_6.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_6";
#endif
            return new Dot(pat4_6.First().x-1, pat4_6.First().y);
            }

            //паттерн на конструкцию      +  *     
            //                           d+  
            //                               *
            var pat4_7 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y - 1].Own == Owner &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x, d.y+1].Own == 0 & aDots[d.x+1, d.y].Own == 0 &
                                                aDots[d.x + 2, d.y].Own == 0
                         select d;
            if (pat4_7.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_7";
#endif
            return new Dot(pat4_7.First().x + 1, pat4_7.First().y);
            }

            //паттерн на конструкцию         *     
            //                           d+  
            //                            +  *
            var pat4_8 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y + 1].Own == Owner &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x + 1, d.y].Own == 0 &
                                                aDots[d.x + 2, d.y].Own == 0
                         select d;
            if (pat4_8.Count() > 0)
            {
#if DEBUG
                    sPattern="pat4_8";
#endif
            return new Dot(pat4_8.First().x + 1, pat4_8.First().y);
            }

            //паттерн на конструкцию   *     *  +    
            //                           d+  *  +
            //                             
            var pat5_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x+2, d.y].Own == Owner &
                                                aDots[d.x + 1, d.y].Own == enemy_own &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x + 2, d.y - 1].Own == Owner
                         select d;
            if (pat5_1.Count() > 0)
            {
#if DEBUG
                    sPattern="pat5_1";
#endif
            return new Dot(pat5_1.First().x, pat5_1.First().y-1);
            }

            //     *
            //    
            //  m  +d  +  
            //     *   *     ход для недопущения двухклеточного расстояния
            var pat6_1 = from Dot d in get_non_blocked
                         where aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 1].Own == 0 &
                                aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x, d.y].Own == Owner &
                                  aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Own == 0 &
                                  aDots[d.x, d.y].Own == Owner 
                                  select d;
            foreach (Dot p in pat6_1)
            {
                if (aDots[p.x - 1, p.y].Own == PLAYER_NONE) 
                {
#if DEBUG
                    sPattern="pat6_1";
#endif
                    return new Dot(p.x - 1, p.y);
                }
            }

            var pat6_2 = from Dot d in get_non_blocked where aDots[d.x - 1, d.y].Own == enemy_own &
                            aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x, d.y].Own == Owner & aDots[d.x - 1, d.y - 1].Own == 0 &
                            aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x, d.y].Own == Owner 
                            select d;
            foreach (Dot p in pat6_2)
            {
                if (aDots[p.x, p.y - 1].Own == PLAYER_NONE)
                {
#if DEBUG
                    sPattern = "pat6_2";
#endif

                    return new Dot(p.x, p.y - 1);
                }
            }

            var pat6_3 = from Dot d in get_non_blocked where aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y + 1].Own == enemy_own &
                                                             aDots[d.x, d.y].Own == Owner & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y + 1].Own == 0 &
                                                                aDots[d.x, d.y].Own == Owner            
                                                                select d;
            foreach (Dot p in pat6_3)
            {
                if (aDots[p.x + 1, p.y].Own == PLAYER_NONE) 
                {
#if DEBUG
                    sPattern = "pat6_3";
#endif
                    return new Dot(p.x + 1, p.y);
                }
            }

            var pat6_4 = from Dot d in get_non_blocked where aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Own == 0 &
                aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x, d.y].Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y].Own == enemy_own &
                aDots[d.x, d.y].Own == Owner 
                select d;
            foreach (Dot p in pat6_4)
            {
                if (aDots[p.x, p.y - 1].Own == PLAYER_NONE)
                {
#if DEBUG
                    sPattern = "pat6_4";
#endif
                    return new Dot(p.x, p.y - 1);
                }
            }

            var pat6_5 = from Dot d in get_non_blocked
                         where aDots[d.x - 2, d.y].Own == enemy_own &
                             aDots[d.x, d.y].Own == Owner &
                             aDots[d.x + 1, d.y].Own == enemy_own &
                             aDots[d.x, d.y - 1].Own == Owner &
                             aDots[d.x - 1, d.y].Own == 0 &
                             aDots[d.x - 1, d.y + 1].Own == 0 &
                             aDots[d.x, d.y].Own == Owner

                         select d;
            foreach (Dot p in pat6_5)
            {
                if (aDots[p.x, p.y + 1].Own == PLAYER_NONE)
                {
#if DEBUG
                    sPattern = "pat6_5";
#endif
                    return new Dot(p.x, p.y + 1);
                }
            }


            //============================================================================================================== 
            var pat549 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x + 1, d.y].Own == Owner
                                                            & aDots[d.x + 1, d.y + 1].Own == enemy_own
                                                            & aDots[d.x, d.y + 1].Own == enemy_own
                                                            & aDots[d.x, d.y + 3].Own == Owner
                                                            & aDots[d.x - 1, d.y].Own == 0
                                                            & aDots[d.x - 1, d.y + 1].Own == 0
                                                            & aDots[d.x, d.y + 2].Own == 0
                                                            & aDots[d.x - 1, d.y + 2].Own == 0
                                                            & aDots[d.x - 1, d.y + 3].Own == 0
                                                             select d;
            if (pat549.Count() > 0) 
            {
#if DEBUG
                sPattern = "pat549";
#endif
                return new Dot(pat549.First().x - 1, pat549.First().y + 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat549_2 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x - 1, d.y].Own == Owner
                                                            & aDots[d.x - 1, d.y - 1].Own == enemy_own
                                                            & aDots[d.x, d.y - 1].Own == enemy_own
                                                            & aDots[d.x, d.y - 3].Own == Owner
                                                            & aDots[d.x + 1, d.y].Own == 0
                                                            & aDots[d.x + 1, d.y - 1].Own == 0
                                                            & aDots[d.x, d.y - 2].Own == 0
                                                            & aDots[d.x + 1, d.y - 2].Own == 0
                                                            & aDots[d.x + 1, d.y - 3].Own == 0
                                                                                       select d;
            if (pat549_2.Count() > 0)
            {
#if DEBUG
                sPattern = "pat549_2";
#endif

                return new Dot(pat549_2.First().x + 1, pat549_2.First().y - 1);
            }
            //============================================================================================================== 
            var pat446 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x, d.y - 1].Own == enemy_own
                                                            & aDots[d.x, d.y - 3].Own == Owner
                                                            & aDots[d.x, d.y - 2].Own == 0
                                                            & aDots[d.x - 1, d.y - 3].Own == 0
                                                            & aDots[d.x - 1, d.y - 2].Own == 0
                                                            & aDots[d.x - 1, d.y - 1].Own == 0
                                                            & aDots[d.x - 1, d.y].Own == 0
                                                             select d;
            if (pat446.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat446";
#endif
            return new Dot(pat446.First().x - 1, pat446.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat446_2 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x, d.y + 1].Own == enemy_own
                                                            & aDots[d.x, d.y + 3].Own == Owner
                                                            & aDots[d.x, d.y + 2].Own == 0
                                                            & aDots[d.x + 1, d.y + 3].Own == 0
                                                            & aDots[d.x + 1, d.y + 2].Own == 0
                                                            & aDots[d.x + 1, d.y + 1].Own == 0
                                                            & aDots[d.x + 1, d.y].Own == 0
                                                           select d;
            if (pat446_2.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat446_2";
#endif
            return new Dot(pat446_2.First().x + 1, pat446_2.First().y + 1);
            }
            //============================================================================================================== 
            var pat798 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x, d.y - 1].Own == enemy_own
                                                            & aDots[d.x + 1, d.y].Own == enemy_own
                                                            & aDots[d.x + 1, d.y - 1].Own == 0
                                                            & aDots[d.x + 2, d.y].Own == enemy_own
                                                            & aDots[d.x + 2, d.y - 1].Own == enemy_own
                                                            & aDots[d.x + 2, d.y - 2].Own == Owner
                                                            & aDots[d.x + 1, d.y - 2].Own == Owner
                                                            & aDots[d.x, d.y - 2].Own == 0
                                                            select d;
            if (pat798.Count() > 0)
            {
#if DEBUG
                 sPattern="pat798";
#endif
                 return new Dot(pat798.First().x + 1, pat798.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat798_2 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x, d.y + 1].Own == enemy_own
                                                            & aDots[d.x - 1, d.y].Own == enemy_own
                                                            & aDots[d.x - 1, d.y + 1].Own == 0
                                                            & aDots[d.x - 2, d.y].Own == enemy_own
                                                            & aDots[d.x - 2, d.y + 1].Own == enemy_own
                                                            & aDots[d.x - 2, d.y + 2].Own == Owner
                                                            & aDots[d.x - 1, d.y + 2].Own == Owner
                                                            & aDots[d.x, d.y + 2].Own == 0
                                                            select d;
            if (pat798_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat798_2";
#endif
                     return new Dot(pat798_2.First().x - 1, pat798_2.First().y + 1);
            }
            //============================================================================================================== 
            var pat957 = from Dot d in get_non_blocked where d.Own == Owner
                                & aDots[d.x - 1, d.y - 1].Own == enemy_own
                                & aDots[d.x, d.y - 1].Own == 0
                                & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                & aDots[d.x, d.y - 2].Own == enemy_own
                                & aDots[d.x - 1, d.y - 2].Own == Owner
                                & aDots[d.x - 1, d.y].Own == 0
                                & aDots[d.x + 1, d.y].Own == 0
                         select d;
            if (pat957.Count() > 0)
            {
#if DEBUG
                    sPattern="pat957";
#endif
             return new Dot(pat957.First().x, pat957.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat957_2 = from Dot d in get_non_blocked where d.Own == Owner
                                & aDots[d.x + 1, d.y + 1].Own == enemy_own
                                & aDots[d.x, d.y + 1].Own == 0
                                & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                & aDots[d.x, d.y + 2].Own == enemy_own
                                & aDots[d.x + 1, d.y + 2].Own == Owner
                                & aDots[d.x + 1, d.y].Own == 0
                                & aDots[d.x - 1, d.y].Own == 0
                           select d;
            if (pat957_2.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat957_2";
#endif
            return new Dot(pat957_2.First().x, pat957_2.First().y + 1);
            }
            //============================================================================================================== 
            var pat40 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Own == enemy_own
            & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y - 1].Own == 0
            & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y + 1].Own == 0
            & aDots[d.x + 1, d.y + 2].Own == 0 
                        select d;
            if (pat40.Count() > 0)
             {
#if DEBUG
                    sPattern="pat40";
#endif
             return new Dot(pat40.First().x, pat40.First().y + 1);
             }
            //--------------Rotate on 180----------------------------------- 
            var pat40_2 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Own == enemy_own
            & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y + 1].Own == 0
            & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y - 1].Own == 0
            & aDots[d.x - 1, d.y - 2].Own == 0
                          select d;
            if (pat40_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat40_2";
#endif
             return new Dot(pat40_2.First().x, pat40_2.First().y - 1);
             }
            //============================================================================================================== 
            var pat868 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y].Own == enemy_own
            & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 2, d.y].Own == 0
            & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner
            & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x, d.y - 2].Own == 0
                         select d;
            if (pat868.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat868";
#endif
            return new Dot(pat868.First().x + 1, pat868.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat868_2 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y].Own == enemy_own
            & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y - 1].Own == Owner
            & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x, d.y + 2].Own == 0
                           select d;
            if (pat868_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat868_2";
#endif
             return new Dot(pat868_2.First().x - 1, pat868_2.First().y + 1);
             }
            //============================================================================================================== 

            //============================================================================================================== 
            var pat419 = from Dot d in get_non_blocked where d.Own == Owner
                                                        & aDots[d.x - 1, d.y].Own == enemy_own
                                                        & aDots[d.x, d.y - 1].Own == enemy_own
                                                        & aDots[d.x + 1, d.y].Own == Owner
                                                        & aDots[d.x + 2, d.y].Own == Owner
                                                        & aDots[d.x + 3, d.y].Own == enemy_own
                                                        & aDots[d.x + 1, d.y - 1].Own == 0
                                                        & aDots[d.x + 2, d.y - 1].Own == 0
                                                        & aDots[d.x + 3, d.y - 1].Own == 0
            select d;
            if (pat419.Count() > 0)
            {
#if DEBUG
                    sPattern="pat419";
#endif
            return new Dot(pat419.First().x + 2, pat419.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat419_2 = from Dot d in get_non_blocked where d.Own == Owner
                                                        & aDots[d.x + 1, d.y].Own == enemy_own
                                                        & aDots[d.x, d.y + 1].Own == enemy_own
                                                        & aDots[d.x - 1, d.y].Own == Owner
                                                        & aDots[d.x - 2, d.y].Own == Owner
                                                        & aDots[d.x - 3, d.y].Own == enemy_own
                                                        & aDots[d.x - 1, d.y + 1].Own == 0
                                                        & aDots[d.x - 2, d.y + 1].Own == 0
                                                        & aDots[d.x - 3, d.y + 1].Own == 0
            select d;
            if (pat419_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat419_2";
#endif
            return new Dot(pat419_2.First().x - 2, pat419_2.First().y + 1);
            }
            //============================================================================================================== 
            var pat353 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Own == enemy_own
                            & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Own == 0
                            & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y - 1].Own == 0
                         select d;
            if (pat353.Count() > 0)
            {
#if DEBUG
                    sPattern="pat353";
#endif
            return new Dot(pat353.First().x + 1, pat353.First().y);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat353_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y + 1].Own == 0
                           select d;
            if (pat353_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat353_2";
#endif
            return new Dot(pat353_2.First().x - 1, pat353_2.First().y);
            }
            //============================================================================================================== 
            var pat579 = from Dot d in get_non_blocked
                         where d.Own == Owner
                        & aDots[d.x + 1, d.y - 1].Own == enemy_own
                        & aDots[d.x + 2, d.y].Own == enemy_own
                        & aDots[d.x + 1, d.y + 1].Own == enemy_own
                        & aDots[d.x + 1, d.y].Own == 0
                        & aDots[d.x + 2, d.y - 1].Own == Owner
                         select d;
            if (pat579.Count() > 0)
            {
#if DEBUG
                    sPattern="pat579";
#endif
            return new Dot(pat579.First().x + 1, pat579.First().y);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat579_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                            & aDots[d.x - 1, d.y + 1].Own == enemy_own
                            & aDots[d.x - 2, d.y].Own == enemy_own
                            & aDots[d.x - 1, d.y - 1].Own == enemy_own
                            & aDots[d.x - 1, d.y].Own == 0
                            & aDots[d.x - 2, d.y + 1].Own == Owner
                           select d;
            if (pat579_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat579_2";
#endif
            return new Dot(pat579_2.First().x - 1, pat579_2.First().y);
            }
            //============================================================================================================== 
            //============================================================================================================== 
            var pat61 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & aDots[d.x, d.y + 1].Own == enemy_own
                            & aDots[d.x, d.y - 2].Own == enemy_own
                            & aDots[d.x - 1, d.y].Own == enemy_own
                            & aDots[d.x + 1, d.y + 1].Own == 0
                            & aDots[d.x + 1, d.y].Own == 0
                            & aDots[d.x + 1, d.y - 1].Own == 0
                            & aDots[d.x + 1, d.y - 2].Own == 0
                            & aDots[d.x, d.y - 1].Own == Owner
                            & aDots[d.x + 2, d.y].Own == 0
                            & aDots[d.x + 2, d.y - 1].Own == 0
                            & aDots[d.x + 2, d.y - 2].Own == 0
                            & aDots[d.x + 2, d.y + 1].Own == 0
                            & aDots[d.x + 1, d.y - 3].Own == 0
                            & aDots[d.x, d.y - 3].Own == 0
                            & aDots[d.x, d.y + 2].Own == 0
                            & aDots[d.x - 1, d.y + 1].Own == 0
                            & aDots[d.x + 1, d.y + 2].Own == 0
                        select d;
            if (pat61.Count() > 0)
            {
#if DEBUG
                    sPattern="pat61";
#endif
            return new Dot(pat61.First().x + 2, pat61.First().y);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat61_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x, d.y - 1].Own == enemy_own
                              & aDots[d.x, d.y + 2].Own == enemy_own
                              & aDots[d.x + 1, d.y].Own == enemy_own
                              & aDots[d.x - 1, d.y - 1].Own == 0
                              & aDots[d.x - 1, d.y].Own == 0
                              & aDots[d.x - 1, d.y + 1].Own == 0
                              & aDots[d.x - 1, d.y + 2].Own == 0
                              & aDots[d.x, d.y + 1].Own == Owner
                              & aDots[d.x - 2, d.y].Own == 0
                              & aDots[d.x - 2, d.y + 1].Own == 0
                              & aDots[d.x - 2, d.y + 2].Own == 0
                              & aDots[d.x - 2, d.y - 1].Own == 0
                              & aDots[d.x - 1, d.y + 3].Own == 0
                              & aDots[d.x, d.y + 3].Own == 0
                              & aDots[d.x, d.y - 2].Own == 0
                              & aDots[d.x + 1, d.y - 1].Own == 0
                              & aDots[d.x - 1, d.y - 2].Own == 0
                          select d;
            if (pat61_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat61_2";
#endif
            return new Dot(pat61_2.First().x - 2, pat61_2.First().y);
            }
            //============================================================================================================== 
            var pat831 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x - 1, d.y + 1].Own == Owner
                             & aDots[d.x - 2, d.y + 1].Own == enemy_own
                             & aDots[d.x - 1, d.y].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 2, d.y].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 2].Own == 0
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 2].Own == 0
                             & aDots[d.x - 1, d.y - 2].Own == 0
                             & aDots[d.x - 1, d.y - 1].Own == 0
                             & aDots[d.x - 2, d.y].Own == 0
                         select d;
            if (pat831.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat831";
#endif
            return new Dot(pat831.First().x - 1, pat831.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat831_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x + 1, d.y - 1].Own == Owner
                               & aDots[d.x + 2, d.y - 1].Own == enemy_own
                               & aDots[d.x + 1, d.y].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 2, d.y].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 2].Own == 0
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 2].Own == 0
                               & aDots[d.x + 1, d.y + 2].Own == 0
                               & aDots[d.x + 1, d.y + 1].Own == 0
                               & aDots[d.x + 2, d.y].Own == 0
                           select d;
            if (pat831_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat831_2";
#endif
            return new Dot(pat831_2.First().x + 1, pat831_2.First().y + 1);
            }
            //============================================================================================================== 
            //============================================================================================================== 
            var pat667 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 3, d.y].Own == Owner
                             & aDots[d.x + 2, d.y + 1].Own == Owner
                             & aDots[d.x + 2, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x, d.y + 1].Own == Owner
                             & aDots[d.x + 3, d.y - 1].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x - 1, d.y - 1].Own == 0
                             & aDots[d.x - 1, d.y].Own == enemy_own
                             & aDots[d.x - 1, d.y + 1].Own == Owner
                             & aDots[d.x - 2, d.y].Own == 0
                             & aDots[d.x, d.y - 2].Own == 0
                             & aDots[d.x + 1, d.y - 2].Own == 0
                             & aDots[d.x + 2, d.y - 2].Own == 0
                         select d;
            if (pat667.Count() > 0) 
            {
#if DEBUG
                    sPattern="pat667";
#endif
            return new Dot(pat667.First().x + 1, pat667.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat667_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 3, d.y].Own == Owner
                               & aDots[d.x - 2, d.y - 1].Own == Owner
                               & aDots[d.x - 2, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x, d.y - 1].Own == Owner
                               & aDots[d.x - 3, d.y + 1].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x + 1, d.y + 1].Own == 0
                               & aDots[d.x + 1, d.y].Own == enemy_own
                               & aDots[d.x + 1, d.y - 1].Own == Owner
                               & aDots[d.x + 2, d.y].Own == 0
                               & aDots[d.x, d.y + 2].Own == 0
                               & aDots[d.x - 1, d.y + 2].Own == 0
                               & aDots[d.x - 2, d.y + 2].Own == 0
                           select d;
            if (pat667_2.Count() > 0)
            {
#if DEBUG
                    sPattern="pat667_2";
#endif
            return new Dot(pat667_2.First().x - 1, pat667_2.First().y + 1);
            }
            //============================================================================================================== 







            return null;//если никаких паттернов не найдено возвращаем нуль
        }

        public string path_pat = Application.CommonAppDataPath + @"\patterns.dat";
        public void SavePattern()
        {
            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path_savegame, FileMode.Create)))
                {

                    for (int i = 0; i < list_moves.Count; i++)
                    {
                        writer.Write((byte)list_moves[i].x);
                        writer.Write((byte)list_moves[i].y);
                        writer.Write((byte)list_moves[i].Own);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
