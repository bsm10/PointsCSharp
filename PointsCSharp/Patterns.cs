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
            iNumberPattern = 1;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //                           *     +  ставить сюда 
            iNumberPattern = 3;
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
            iNumberPattern = 4;
            var pat4_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == Owner &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 2].Own == 0 &
                                                aDots[d.x + 1, d.y].Own == 0 
                         select d;
            if (pat4_1.Count() > 0)
            {
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
            return new Dot(pat4_8.First().x + 1, pat4_8.First().y);
            }

            //паттерн на конструкцию   *     *  +    
            //                           d+  *  +
            //                             
            iNumberPattern = 5;
            var pat5_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x+2, d.y].Own == Owner &
                                                aDots[d.x + 1, d.y].Own == enemy_own &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x + 2, d.y - 1].Own == Owner
                         select d;
            if (pat5_1.Count() > 0)
            {
            return new Dot(pat5_1.First().x, pat5_1.First().y-1);
            }
            //============================================================================================================== 
            // d+  * 
            //  * m+  + 
            iNumberPattern = 901;
            var pat901 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 2, d.y + 2].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
                         select d;
            if (pat901.Count() > 0) return new Dot(pat901.First().x + 1, pat901.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat901_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 2, d.y - 2].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
                           select d;
            if (pat901_2.Count() > 0) return new Dot(pat901_2.First().x - 1, pat901_2.First().y - 1);
            //============================================================================================================== 
            // d+ m+  
            //  *  *  + 
            iNumberPattern = 604;
            var pat604 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 2, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat604.Count() > 0) return new Dot(pat604.First().x + 1, pat604.First().y);
            //--------------Rotate on 180----------------------------------- 
            var pat604_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 2, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
                           select d;
            if (pat604_2.Count() > 0) return new Dot(pat604_2.First().x - 1, pat604_2.First().y);
            //============================================================================================================== 

            iNumberPattern = 549;
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
                return new Dot(pat549_2.First().x + 1, pat549_2.First().y - 1);
            }
            //============================================================================================================== 
            iNumberPattern = 446;
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
            return new Dot(pat446_2.First().x + 1, pat446_2.First().y + 1);
            }
            //============================================================================================================== 
            iNumberPattern = 798;
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
               return new Dot(pat798_2.First().x - 1, pat798_2.First().y + 1);
            }
            //============================================================================================================== 
            iNumberPattern = 957;
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
            return new Dot(pat957_2.First().x, pat957_2.First().y + 1);
            }
            //============================================================================================================== 
            iNumberPattern = 40;
            var pat40 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Own == enemy_own
            & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y - 1].Own == 0
            & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y + 1].Own == 0
            & aDots[d.x + 1, d.y + 2].Own == 0 
                        select d;
            if (pat40.Count() > 0)
             {
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
             return new Dot(pat40_2.First().x, pat40_2.First().y - 1);
             }
            //============================================================================================================== 
            iNumberPattern = 868;
            var pat868 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y].Own == enemy_own
            & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 2, d.y].Own == 0
            & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner
            & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x, d.y - 2].Own == 0
                         select d;
            if (pat868.Count() > 0) 
            {
            return new Dot(pat868.First().x + 1, pat868.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat868_2 = from Dot d in get_non_blocked where d.Own == Owner & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y].Own == enemy_own
            & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y - 1].Own == Owner
            & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x, d.y + 2].Own == 0
                           select d;
            if (pat868_2.Count() > 0)
            {
             return new Dot(pat868_2.First().x - 1, pat868_2.First().y + 1);
             }
            //============================================================================================================== 
            iNumberPattern = 419;
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
            return new Dot(pat419_2.First().x - 2, pat419_2.First().y + 1);
            }
            //============================================================================================================== 
            iNumberPattern = 353;
            var pat353 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Own == enemy_own
                            & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Own == 0
                            & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y - 1].Own == 0
                         select d;
            if (pat353.Count() > 0)
            {
            return new Dot(pat353.First().x + 1, pat353.First().y);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat353_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y + 1].Own == 0
                           select d;
            if (pat353_2.Count() > 0)
            {
            return new Dot(pat353_2.First().x - 1, pat353_2.First().y);
            }
            //============================================================================================================== 
            iNumberPattern = 579;
            var pat579 = from Dot d in get_non_blocked where d.Own == Owner
                        & aDots[d.x + 1, d.y - 1].Own == enemy_own
                        & aDots[d.x + 2, d.y].Own == enemy_own
                        & aDots[d.x + 1, d.y + 1].Own == enemy_own
                        & aDots[d.x + 1, d.y].Own == 0
                        & aDots[d.x + 2, d.y - 1].Own == Owner
                         select d;
            if (pat579.Count() > 0)
            {
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
            return new Dot(pat579_2.First().x - 1, pat579_2.First().y);
            }
            //============================================================================================================== 
            iNumberPattern = 61;
            var pat61 = from Dot d in get_non_blocked where d.Own == Owner
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
            return new Dot(pat61.First().x + 2, pat61.First().y);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat61_2 = from Dot d in get_non_blocked where d.Own == Owner
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
            return new Dot(pat61_2.First().x - 2, pat61_2.First().y);
            }
            //============================================================================================================== 
            iNumberPattern = 831;
            var pat831 = from Dot d in get_non_blocked where d.Own == Owner
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
            return new Dot(pat831.First().x - 1, pat831.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat831_2 = from Dot d in get_non_blocked where d.Own == Owner
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
            return new Dot(pat831_2.First().x + 1, pat831_2.First().y + 1);
            }
            //============================================================================================================== 
            iNumberPattern = 667;
            var pat667 = from Dot d in get_non_blocked where d.Own == Owner
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
            return new Dot(pat667.First().x + 1, pat667.First().y - 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat667_2 = from Dot d in get_non_blocked where d.Own == Owner
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
            return new Dot(pat667_2.First().x - 1, pat667_2.First().y + 1);
            }
            //============================================================================================================== 
            iNumberPattern = 163;
            var pat163 = from Dot d in get_non_blocked where d.Own == Owner
                        & aDots[d.x + 1, d.y - 1].Own == enemy_own
                        & aDots[d.x - 1, d.y - 1].Own == enemy_own
                        & aDots[d.x, d.y - 2].Own == Owner
                        & aDots[d.x, d.y - 1].Own == 0
                        & aDots[d.x - 1, d.y].Own == Owner
                        & aDots[d.x + 1, d.y].Own == 0
                        & aDots[d.x + 1, d.y + 1].Own == enemy_own
                        & aDots[d.x + 1, d.y - 2].Own == 0
                        & aDots[d.x - 1, d.y - 2].Own == 0
                         select d;
            if (pat163.Count() > 0) return new Dot(pat163.First().x, pat163.First().y - 1);
            //--------------Rotate on 180----------------------------------- 
            var pat163_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                            & aDots[d.x - 1, d.y + 1].Own == enemy_own
                            & aDots[d.x + 1, d.y + 1].Own == enemy_own
                            & aDots[d.x, d.y + 2].Own == Owner
                            & aDots[d.x, d.y + 1].Own == 0
                            & aDots[d.x + 1, d.y].Own == Owner
                            & aDots[d.x - 1, d.y].Own == 0
                            & aDots[d.x - 1, d.y - 1].Own == enemy_own
                            & aDots[d.x - 1, d.y + 2].Own == 0
                            & aDots[d.x + 1, d.y + 2].Own == 0
                           select d;
            if (pat163_2.Count() > 0) return new Dot(pat163_2.First().x, pat163_2.First().y + 1);

            //============================================================================================================== 
            iNumberPattern = 169;
            var pat169 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 1, d.y + 2].Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 1].Own == enemy_own
                         select d;
            if (pat169.Count() > 0) return new Dot(pat169.First().x - 1, pat169.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat169_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y - 2].Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 1].Own == enemy_own
                           select d;
            if (pat169_2.Count() > 0) return new Dot(pat169_2.First().x + 1, pat169_2.First().y - 1);

            //============================================================================================================== 
            //     +  +
            // d+  * 
            //  +     +m
            iNumberPattern = 581;
            var pat581 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 2, d.y - 1].Own == Owner
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 3, d.y].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
                         select d;
            if (pat581.Count() > 0) return new Dot(pat581.First().x + 2, pat581.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat581_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 2, d.y + 1].Own == Owner
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 3, d.y].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
                           select d;
            if (pat581_2.Count() > 0) return new Dot(pat581_2.First().x - 2, pat581_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 64;
            var pat64 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 3, d.y + 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 2].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
                        select d;
            if (pat64.Count() > 0) return new Dot(pat64.First().x - 1, pat64.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat64_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 3, d.y - 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 2].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
                          select d;
            if (pat64_2.Count() > 0) return new Dot(pat64_2.First().x + 1, pat64_2.First().y - 1);
            //============================================================================================================== 
            //============================================================================================================== 
            iNumberPattern = 666;
            var pat666 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 2, d.y].Own == 0
                         select d;
            if (pat666.Count() > 0) return new Dot(pat666.First().x - 1, pat666.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat666_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 2, d.y].Own == 0
                           select d;
            if (pat666_2.Count() > 0) return new Dot(pat666_2.First().x + 1, pat666_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 333;
            var pat333 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y - 3].Own == Owner
                             & aDots[d.x - 1, d.y - 2].Own == Owner
                             & aDots[d.x, d.y - 2].Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own
                             & aDots[d.x - 2, d.y - 2].Own == enemy_own
                             & aDots[d.x - 3, d.y - 1].Own == 0
                             & aDots[d.x - 2, d.y].Own == 0
                             & aDots[d.x - 2, d.y - 1].Own == 0
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x, d.y - 1].Own != enemy_own
                             & aDots[d.x + 1, d.y].Own == 0
                         select d;
            if (pat333.Count() > 0) return new Dot(pat333.First().x - 2, pat333.First().y - 1);
            //--------------Rotate on 180----------------------------------- 
            var pat333_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y + 3].Own == Owner
                               & aDots[d.x + 1, d.y + 2].Own == Owner
                               & aDots[d.x, d.y + 2].Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own
                               & aDots[d.x + 2, d.y + 2].Own == enemy_own
                               & aDots[d.x + 3, d.y + 1].Own == 0
                               & aDots[d.x + 2, d.y].Own == 0
                               & aDots[d.x + 2, d.y + 1].Own == 0
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x, d.y + 1].Own != enemy_own
                           select d;
            if (pat333_2.Count() > 0) return new Dot(pat333_2.First().x + 2, pat333_2.First().y + 1);
            //============================================================================================================== 
            //============================================================================================================== 
            iNumberPattern = 42;
            var pat42 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 2, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 3, d.y - 1].Own == Owner
                        select d;
            if (pat42.Count() > 0) return new Dot(pat42.First().x - 1, pat42.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat42_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x + 2, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 3, d.y + 1].Own == Owner
                          select d;
            if (pat42_2.Count() > 0) return new Dot(pat42_2.First().x + 1, pat42_2.First().y - 1);

            //============================================================================================================== 
            iNumberPattern = 610;
            var pat610 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 3, d.y].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 2, d.y + 2].Own == 0
                         select d;
            if (pat610.Count() > 0) return new Dot(pat610.First().x - 2, pat610.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat610_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x + 3, d.y].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 2, d.y - 2].Own == 0
                           select d;
            if (pat610_2.Count() > 0) return new Dot(pat610_2.First().x + 2, pat610_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 906;
            var pat906 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x + 3, d.y - 1].Own == Owner
& aDots[d.x + 3, d.y].Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 3, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x + 4, d.y].Own == 0
                         select d;
            if (pat906.Count() > 0) return new Dot(pat906.First().x + 1, pat906.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat906_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 3, d.y + 1].Own == Owner
& aDots[d.x - 3, d.y].Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 3, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x - 4, d.y].Own == 0
                           select d;
            if (pat906_2.Count() > 0) return new Dot(pat906_2.First().x - 1, pat906_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 143;
            var pat143 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y - 3].Own == enemy_own
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x, d.y - 2].Own == Owner
& aDots[d.x, d.y + 2].Own == 0
                         select d;
            if (pat143.Count() > 0) return new Dot(pat143.First().x + 1, pat143.First().y);
            //--------------Rotate on 180----------------------------------- 
            var pat143_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y + 3].Own == enemy_own
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x, d.y + 2].Own == Owner
& aDots[d.x, d.y - 2].Own == 0
                           select d;
            if (pat143_2.Count() > 0) return new Dot(pat143_2.First().x - 1, pat143_2.First().y);
            //============================================================================================================== 
            iNumberPattern = 794;
            var pat794 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat794.Count() > 0) return new Dot(pat794.First().x, pat794.First().y - 1);
            //--------------Rotate on 180----------------------------------- 
            var pat794_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
                           select d;
            if (pat794_2.Count() > 0) return new Dot(pat794_2.First().x, pat794_2.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 796;
            var pat796 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 2, d.y - 2].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 2, d.y + 2].Own == Owner
& aDots[d.x - 2, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 2, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y - 3].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
                         select d;
            if (pat796.Count() > 0) return new Dot(pat796.First().x, pat796.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat796_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 2, d.y + 2].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 2, d.y - 2].Own == Owner
& aDots[d.x + 2, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 2, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y + 3].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
                           select d;
            if (pat796_2.Count() > 0) return new Dot(pat796_2.First().x, pat796_2.First().y - 1);
            //============================================================================================================== 

            iNumberPattern = 685;
            var pat685 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x, d.y + 1].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own
                             & aDots[d.x - 1, d.y + 2].Own == Owner
                             & aDots[d.x, d.y + 2].Own == 0
                             & aDots[d.x - 2, d.y + 1].Own == 0
                             & aDots[d.x - 2, d.y + 2].Own == 0
                             & aDots[d.x + 1, d.y].Own == 0
                             & aDots[d.x + 1, d.y + 1].Own == 0
                         select d;
            if (pat685.Count() > 0) return new Dot(pat685.First().x, pat685.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat685_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x, d.y - 1].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own
                               & aDots[d.x + 1, d.y - 2].Own == Owner
                               & aDots[d.x, d.y - 2].Own == 0
                               & aDots[d.x + 2, d.y - 1].Own == 0
                               & aDots[d.x + 2, d.y - 2].Own == 0
                               & aDots[d.x - 1, d.y].Own == 0
                               & aDots[d.x - 1, d.y - 1].Own == 0
                           select d;
            if (pat685_2.Count() > 0) return new Dot(pat685_2.First().x, pat685_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 921;
            var pat921 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == Owner
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x + 2, d.y + 1].Own == Owner
                             & aDots[d.x + 2, d.y].Own == enemy_own
                             & aDots[d.x + 3, d.y].Own == Owner
                             & aDots[d.x + 4, d.y].Own == 0
                             & aDots[d.x + 3, d.y - 1].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 2].Own == 0
                             & aDots[d.x + 1, d.y - 2].Own == 0
                             & aDots[d.x - 1, d.y - 2].Own == 0
                         select d;
            if (pat921.Count() > 0) return new Dot(pat921.First().x + 1, pat921.First().y - 1);
            //--------------Rotate on 180----------------------------------- 
            var pat921_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == Owner
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x - 2, d.y - 1].Own == Owner
                               & aDots[d.x - 2, d.y].Own == enemy_own
                               & aDots[d.x - 3, d.y].Own == Owner
                               & aDots[d.x - 4, d.y].Own == 0
                               & aDots[d.x - 3, d.y + 1].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 2].Own == 0
                               & aDots[d.x - 1, d.y + 2].Own == 0
                               & aDots[d.x + 1, d.y + 2].Own == 0
                           select d;
            if (pat921_2.Count() > 0) return new Dot(pat921_2.First().x - 1, pat921_2.First().y + 1);
//============================================================================================================== 
iNumberPattern = 970; 
var pat970 = from Dot d in get_non_blocked where d.Own == Owner 
 & aDots[d.x+1, d.y].Own == enemy_own 
 & aDots[d.x-3, d.y].Own == enemy_own 
 & aDots[d.x-2, d.y+1].Own == enemy_own 
 & aDots[d.x-1, d.y+1].Own  == 0 
 & aDots[d.x, d.y+1].Own  == 0 
 & aDots[d.x-1, d.y].Own  != enemy_own 
 & aDots[d.x-2, d.y].Own  != enemy_own 
 & aDots[d.x+1, d.y+1].Own  == 0 
select d; 
 if (pat970.Count() > 0) return new Dot(pat970.First().x,pat970.First().y+1);
//--------------Rotate on 180----------------------------------- 
var pat970_2 = from Dot d in get_non_blocked where d.Own == Owner 
 & aDots[d.x-1, d.y].Own == enemy_own 
 & aDots[d.x+3, d.y].Own == enemy_own 
 & aDots[d.x+2, d.y-1].Own == enemy_own 
 & aDots[d.x+1, d.y-1].Own == 0 
 & aDots[d.x, d.y-1].Own == 0 
 & aDots[d.x+1, d.y].Own != enemy_own 
 & aDots[d.x+2, d.y].Own != enemy_own 
select d; 
 if (pat970_2.Count() > 0) return new Dot(pat970_2.First().x,pat970_2.First().y-1);
 //============================================================================================================== 
 iNumberPattern = 973;
 var pat973 = from Dot d in get_non_blocked
              where d.Own == Owner
                  & aDots[d.x - 3, d.y - 2].Own == Owner
                  & aDots[d.x - 2, d.y - 1].Own == Owner
                  & aDots[d.x - 3, d.y - 1].Own == enemy_own
                  & aDots[d.x - 2, d.y].Own == enemy_own
                  & aDots[d.x - 1, d.y - 1].Own == Owner
                  & aDots[d.x, d.y + 1].Own == 0
                  & aDots[d.x - 1, d.y].Own == 0
                  & aDots[d.x - 1, d.y + 1].Own == 0
                  & aDots[d.x - 2, d.y + 1].Own == 0
                  & aDots[d.x - 3, d.y].Own == 0
                  & aDots[d.x - 3, d.y + 1].Own == 0
                  & aDots[d.x - 4, d.y].Own == 0
                  & aDots[d.x - 4, d.y - 1].Own == 0
              select d;
 if (pat973.Count() > 0) return new Dot(pat973.First().x - 1, pat973.First().y + 1);
 //--------------Rotate on 180----------------------------------- 
 var pat973_2 = from Dot d in get_non_blocked
                where d.Own == Owner
                    & aDots[d.x + 3, d.y + 2].Own == Owner
                    & aDots[d.x + 2, d.y + 1].Own == Owner
                    & aDots[d.x + 3, d.y + 1].Own == enemy_own
                    & aDots[d.x + 2, d.y].Own == enemy_own
                    & aDots[d.x + 1, d.y + 1].Own == Owner
                    & aDots[d.x, d.y - 1].Own == 0
                    & aDots[d.x + 1, d.y].Own == 0
                    & aDots[d.x + 1, d.y - 1].Own == 0
                    & aDots[d.x + 2, d.y - 1].Own == 0
                    & aDots[d.x + 3, d.y].Own == 0
                    & aDots[d.x + 3, d.y - 1].Own == 0
                    & aDots[d.x + 4, d.y].Own == 0
                select d;
 if (pat973_2.Count() > 0) return new Dot(pat973_2.First().x + 1, pat973_2.First().y - 1);
            //============================================================================================================== 
            //============================================================================================================== 
            iNumberPattern = 448;
            var pat448 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 2, d.y - 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own != enemy_own
& aDots[d.x - 1, d.y].Own != enemy_own
                         select d;
            if (pat448.Count() > 0) return new Dot(pat448.First().x - 1, pat448.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat448_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 2, d.y + 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own != enemy_own
                           select d;
            if (pat448_2.Count() > 0) return new Dot(pat448_2.First().x + 1, pat448_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 364;
            var pat364 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x, d.y + 1].Own == enemy_own
                             & aDots[d.x, d.y + 3].Own == Owner
                             & aDots[d.x + 1, d.y + 2].Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x, d.y - 1].Own != enemy_own
                             & aDots[d.x, d.y + 4].Own != enemy_own
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == 0
                             & aDots[d.x - 1, d.y + 2].Own == 0
                             & aDots[d.x - 1, d.y + 3].Own == 0
                             & aDots[d.x - 2, d.y + 1].Own == 0
                             & aDots[d.x - 2, d.y + 2].Own == 0
                         select d;
            if (pat364.Count() > 0) return new Dot(pat364.First().x - 2, pat364.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat364_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x, d.y - 1].Own == enemy_own
                               & aDots[d.x, d.y - 3].Own == Owner
                               & aDots[d.x - 1, d.y - 2].Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x, d.y + 1].Own != enemy_own
                               & aDots[d.x, d.y - 4].Own != enemy_own
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == 0
                               & aDots[d.x + 1, d.y - 2].Own == 0
                               & aDots[d.x + 1, d.y - 3].Own == 0
                               & aDots[d.x + 2, d.y - 1].Own == 0
                               & aDots[d.x + 2, d.y - 2].Own == 0
                           select d;
            if (pat364_2.Count() > 0) return new Dot(pat364_2.First().x + 2, pat364_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat364_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y].Own == enemy_own
                                 & aDots[d.x - 3, d.y].Own == Owner
                                 & aDots[d.x - 2, d.y - 1].Own == Owner
                                 & aDots[d.x - 1, d.y - 1].Own == Owner
                                 & aDots[d.x + 1, d.y].Own != enemy_own
                                 & aDots[d.x - 4, d.y].Own != enemy_own
                                 & aDots[d.x, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == 0
                                 & aDots[d.x - 2, d.y + 1].Own == 0
                                 & aDots[d.x - 3, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y + 2].Own == 0
                                 & aDots[d.x - 2, d.y + 2].Own == 0
                             select d;
            if (pat364_2_3.Count() > 0) return new Dot(pat364_2_3.First().x - 1, pat364_2_3.First().y + 2);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat364_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y].Own == enemy_own
                                   & aDots[d.x + 3, d.y].Own == Owner
                                   & aDots[d.x + 2, d.y + 1].Own == Owner
                                   & aDots[d.x + 1, d.y + 1].Own == Owner
                                   & aDots[d.x - 1, d.y].Own != enemy_own
                                   & aDots[d.x + 4, d.y].Own != enemy_own
                                   & aDots[d.x, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == 0
                                   & aDots[d.x + 2, d.y - 1].Own == 0
                                   & aDots[d.x + 3, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y - 2].Own == 0
                                   & aDots[d.x + 2, d.y - 2].Own == 0
                               select d;
            if (pat364_2_3_4.Count() > 0) return new Dot(pat364_2_3_4.First().x + 1, pat364_2_3_4.First().y - 2);
            //============================================================================================================== 
            iNumberPattern = 615;
            var pat615 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y + 1].Own == enemy_own
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == 0
                             & aDots[d.x, d.y + 1].Own == 0
                             & aDots[d.x, d.y + 2].Own == 0
                             & aDots[d.x - 2, d.y].Own == 0
                         select d;
            if (pat615.Count() > 0) return new Dot(pat615.First().x - 1, pat615.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat615_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y - 1].Own == enemy_own
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == 0
                               & aDots[d.x, d.y - 1].Own == 0
                               & aDots[d.x, d.y - 2].Own == 0
                               & aDots[d.x + 2, d.y].Own == 0
                           select d;
            if (pat615_2.Count() > 0) return new Dot(pat615_2.First().x + 1, pat615_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat615_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y].Own == enemy_own
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y].Own == 0
                                 & aDots[d.x - 2, d.y].Own == 0
                                 & aDots[d.x, d.y + 2].Own == 0
                             select d;
            if (pat615_2_3.Count() > 0) return new Dot(pat615_2_3.First().x - 1, pat615_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat615_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y].Own == enemy_own
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y].Own == 0
                                   & aDots[d.x + 2, d.y].Own == 0
                                   & aDots[d.x, d.y - 2].Own == 0
                               select d;
            if (pat615_2_3_4.Count() > 0) return new Dot(pat615_2_3_4.First().x + 1, pat615_2_3_4.First().y - 1);
            //============================================================================================================== 

            iNumberPattern = 528;
            var pat528 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y].Own == enemy_own
                             & aDots[d.x, d.y + 1].Own == enemy_own
                             & aDots[d.x + 1, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y].Own == 0
                             & aDots[d.x + 2, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 1].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == Owner
                             & aDots[d.x, d.y + 2].Own == 0
                         select d;
            if (pat528.Count() > 0) return new Dot(pat528.First().x + 1, pat528.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat528_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y].Own == enemy_own
                               & aDots[d.x, d.y - 1].Own == enemy_own
                               & aDots[d.x - 1, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y].Own == 0
                               & aDots[d.x - 2, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 1].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == Owner
                               & aDots[d.x, d.y - 2].Own == 0
                           select d;
            if (pat528_2.Count() > 0) return new Dot(pat528_2.First().x - 1, pat528_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat528_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y].Own == enemy_own
                                 & aDots[d.x - 1, d.y - 1].Own == 0
                                 & aDots[d.x, d.y - 1].Own == 0
                                 & aDots[d.x, d.y - 2].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x + 1, d.y].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == Owner
                                 & aDots[d.x - 2, d.y].Own == 0
                             select d;
            if (pat528_2_3.Count() > 0) return new Dot(pat528_2_3.First().x - 1, pat528_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat528_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y].Own == enemy_own
                                   & aDots[d.x + 1, d.y + 1].Own == 0
                                   & aDots[d.x, d.y + 1].Own == 0
                                   & aDots[d.x, d.y + 2].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x - 1, d.y].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == Owner
                                   & aDots[d.x + 2, d.y].Own == 0
                               select d;
            if (pat528_2_3_4.Count() > 0) return new Dot(pat528_2_3_4.First().x + 1, pat528_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 490;
            var pat490 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == Owner
                             & aDots[d.x + 1, d.y].Own == Owner
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own == enemy_own
                             & aDots[d.x, d.y - 2].Own == 0
                             & aDots[d.x + 1, d.y - 2].Own == 0
                             & aDots[d.x + 2, d.y - 2].Own == 0
                         select d;
            if (pat490.Count() > 0) return new Dot(pat490.First().x + 1, pat490.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat490_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == Owner
                               & aDots[d.x - 1, d.y].Own == Owner
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own == enemy_own
                               & aDots[d.x, d.y + 2].Own == 0
                               & aDots[d.x - 1, d.y + 2].Own == 0
                               & aDots[d.x - 2, d.y + 2].Own == 0
                           select d;
            if (pat490_2.Count() > 0) return new Dot(pat490_2.First().x - 1, pat490_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat490_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y + 1].Own == Owner
                                 & aDots[d.x, d.y - 1].Own == Owner
                                 & aDots[d.x + 1, d.y].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x + 1, d.y - 2].Own == enemy_own
                                 & aDots[d.x + 2, d.y].Own == 0
                                 & aDots[d.x + 2, d.y - 1].Own == 0
                                 & aDots[d.x + 2, d.y - 2].Own == 0
                             select d;
            if (pat490_2_3.Count() > 0) return new Dot(pat490_2_3.First().x + 1, pat490_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat490_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y - 1].Own == Owner
                                   & aDots[d.x, d.y + 1].Own == Owner
                                   & aDots[d.x - 1, d.y].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x - 1, d.y + 2].Own == enemy_own
                                   & aDots[d.x - 2, d.y].Own == 0
                                   & aDots[d.x - 2, d.y + 1].Own == 0
                                   & aDots[d.x - 2, d.y + 2].Own == 0
                               select d;
            if (pat490_2_3_4.Count() > 0) return new Dot(pat490_2_3_4.First().x - 1, pat490_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 378;
            var pat378 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 2, d.y - 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 2, d.y].Own == 0
                         select d;
            if (pat378.Count() > 0) return new Dot(pat378.First().x + 1, pat378.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat378_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 2, d.y + 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 2, d.y].Own == 0
                           select d;
            if (pat378_2.Count() > 0) return new Dot(pat378_2.First().x - 1, pat378_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat378_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y + 2].Own == enemy_own
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
                             select d;
            if (pat378_2_3.Count() > 0) return new Dot(pat378_2_3.First().x + 1, pat378_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat378_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y - 2].Own == enemy_own
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
                               select d;
            if (pat378_2_3_4.Count() > 0) return new Dot(pat378_2_3_4.First().x - 1, pat378_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 883;
            var pat883 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y].Own == enemy_own
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == Owner
                             & aDots[d.x, d.y - 2].Own == enemy_own
                             & aDots[d.x - 2, d.y + 1].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == 0
                             & aDots[d.x, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y + 1].Own == 0
                         select d;
            if (pat883.Count() > 0) return new Dot(pat883.First().x - 1, pat883.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat883_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y].Own == enemy_own
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == Owner
                               & aDots[d.x, d.y + 2].Own == enemy_own
                               & aDots[d.x + 2, d.y - 1].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == 0
                               & aDots[d.x, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y - 1].Own == 0
                           select d;
            if (pat883_2.Count() > 0) return new Dot(pat883_2.First().x + 1, pat883_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat883_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 2].Own == enemy_own
                                 & aDots[d.x, d.y + 1].Own == 0
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y].Own == Owner
                                 & aDots[d.x + 2, d.y].Own == enemy_own
                                 & aDots[d.x - 1, d.y + 2].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y].Own == 0
                                 & aDots[d.x - 1, d.y - 1].Own == 0
                             select d;
            if (pat883_2_3.Count() > 0) return new Dot(pat883_2_3.First().x - 1, pat883_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat883_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 2].Own == enemy_own
                                   & aDots[d.x, d.y - 1].Own == 0
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y].Own == Owner
                                   & aDots[d.x - 2, d.y].Own == enemy_own
                                   & aDots[d.x + 1, d.y - 2].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y].Own == 0
                                   & aDots[d.x + 1, d.y + 1].Own == 0
                               select d;
            if (pat883_2_3_4.Count() > 0) return new Dot(pat883_2_3_4.First().x + 1, pat883_2_3_4.First().y - 1);
            //============================================================================================================== 







            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль
        }
        private Dot CheckPattern_vilochka(int Owner)
        {
            int enemy_own = Owner == 1 ? 2 : 1;
            var get_non_blocked = from Dot d in aDots where d.Blocked == false select d; //получить коллекцию незаблокированных точек
            iNumberPattern = 1;
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
                    return new Dot(p.x + 2, p.y);
                }
            }

            //паттерн на диагональное расположение точек    *  +  *d   
            //                                              +  *    
            //                                              *       
            //то же но в обратную сторону
            iNumberPattern = 2;
            var pat2 = from Dot d in get_non_blocked
                       where d.Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 2].Own == Owner &
                       aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 2, d.y + 1].Own == enemy_own & aDots[d.x - 3, d.y].Own == 0 &
                       aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 3, d.y + 1].Own == 0
                       select d;
            foreach (Dot p in pat2)
            {
                if (aDots[p.x - 2, p.y].Own == PLAYER_NONE){
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
                    return new Dot(p.x, p.y + 2);
                }
            }
            iNumberPattern = 364;
            var pat364 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 2].Own != enemy_own
                             & aDots[d.x + 1, d.y - 2].Own != enemy_own
                             & aDots[d.x + 2, d.y - 1].Own != enemy_own
                             & aDots[d.x + 2, d.y].Own != enemy_own
                         select d;
            if (pat364.Count() > 0) return new Dot(pat364.First().x + 1, pat364.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat364_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 2].Own != enemy_own
                               & aDots[d.x - 1, d.y + 2].Own != enemy_own
                               & aDots[d.x - 2, d.y + 1].Own != enemy_own
                               & aDots[d.x - 2, d.y].Own != enemy_own
                           select d;
            if (pat364_2.Count() > 0) return new Dot(pat364_2.First().x - 1, pat364_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat364_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y + 1].Own == Owner
                                 & aDots[d.x - 1, d.y - 1].Own == Owner
                                 & aDots[d.x + 1, d.y].Own == enemy_own
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x + 2, d.y].Own != enemy_own
                                 & aDots[d.x + 2, d.y - 1].Own != enemy_own
                                 & aDots[d.x + 1, d.y - 2].Own != enemy_own
                                 & aDots[d.x, d.y - 2].Own != enemy_own
                             select d;
            if (pat364_2_3.Count() > 0) return new Dot(pat364_2_3.First().x + 1, pat364_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat364_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y - 1].Own == Owner
                                   & aDots[d.x + 1, d.y + 1].Own == Owner
                                   & aDots[d.x - 1, d.y].Own == enemy_own
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x - 2, d.y].Own != enemy_own
                                   & aDots[d.x - 2, d.y + 1].Own != enemy_own
                                   & aDots[d.x - 1, d.y + 2].Own != enemy_own
                                   & aDots[d.x, d.y + 2].Own != enemy_own
                               select d;
            if (pat364_2_3_4.Count() > 0) return new Dot(pat364_2_3_4.First().x - 1, pat364_2_3_4.First().y + 1);
            //============================================================================================================== 

            //===========ВИЛОЧКА=================================================================================================== 
            //     *   
            //     +  
            //  +  *
            //  *
            iNumberPattern = 670;
            var pat670 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x, d.y + 2].Own == Owner
                             & aDots[d.x, d.y + 1].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == Owner
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y + 1].Own == 0
                             & aDots[d.x + 2, d.y].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own != enemy_own
                             & aDots[d.x, d.y - 1].Own != enemy_own
                             & aDots[d.x + 2, d.y + 1].Own != enemy_own
                             & aDots[d.x + 1, d.y + 2].Own != enemy_own
                             & aDots[d.x - 1, d.y].Own != enemy_own
                         select d;
            if (pat670.Count() > 0) return new Dot(pat670.First().x + 1, pat670.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat670_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x, d.y - 2].Own == Owner
                               & aDots[d.x, d.y - 1].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == Owner
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y - 1].Own == 0
                               & aDots[d.x - 2, d.y].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own != enemy_own
                               & aDots[d.x, d.y + 1].Own != enemy_own
                               & aDots[d.x - 2, d.y - 1].Own != enemy_own
                               & aDots[d.x - 1, d.y - 2].Own != enemy_own
                               & aDots[d.x + 1, d.y].Own != enemy_own
                           select d;
            if (pat670_2.Count() > 0) return new Dot(pat670_2.First().x - 1, pat670_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat670_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 2, d.y].Own == Owner
                                 & aDots[d.x - 1, d.y].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == Owner
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y - 1].Own == 0
                                 & aDots[d.x, d.y - 2].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == 0
                                 & aDots[d.x + 1, d.y - 2].Own != enemy_own
                                 & aDots[d.x + 1, d.y].Own != enemy_own
                                 & aDots[d.x - 1, d.y - 2].Own != enemy_own
                                 & aDots[d.x - 2, d.y - 1].Own != enemy_own
                                 & aDots[d.x, d.y + 1].Own != enemy_own
                             select d;
            if (pat670_2_3.Count() > 0) return new Dot(pat670_2_3.First().x - 1, pat670_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat670_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 2, d.y].Own == Owner
                                   & aDots[d.x + 1, d.y].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == Owner
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y + 1].Own == 0
                                   & aDots[d.x, d.y + 2].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == 0
                                   & aDots[d.x - 1, d.y + 2].Own != enemy_own
                                   & aDots[d.x - 1, d.y].Own != enemy_own
                                   & aDots[d.x + 1, d.y + 2].Own != enemy_own
                                   & aDots[d.x + 2, d.y + 1].Own != enemy_own
                                   & aDots[d.x, d.y - 1].Own != enemy_own
                               select d;
            if (pat670_2_3_4.Count() > 0) return new Dot(pat670_2_3_4.First().x + 1, pat670_2_3_4.First().y + 1);
            //============================================================================================================== 

//            iNumberPattern = 465;
//            var pat465 = from Dot d in get_non_blocked
//                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y + 1].Own == 0
//& aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 1].Own == 0
//& aDots[d.x - 2, d.y].Own == Owner & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x - 2, d.y - 1].Own == 0
//& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 3, d.y].Own == 0
//                         select d;
//            if (pat465.Count() > 0) {
//                    return new Dot(pat465.First().x - 1, pat465.First().y - 1);
//               }
//            //--------------Rotate on 180----------------------------------- 
//            var pat465_2 = from Dot d in get_non_blocked
//                           where d.Own == Owner & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0
//& aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 1].Own == 0
//& aDots[d.x + 2, d.y].Own == Owner & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x + 2, d.y + 1].Own == 0
//& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 3, d.y].Own == 0
//                           select d;
//            if (pat465_2.Count() > 0){
//                    return new Dot(pat465_2.First().x + 1, pat465_2.First().y + 1);
//            }
//            //==================ВИЛОЧКА 2=================================================================================== 
//            iNumberPattern = 883;
//            var pat883 = from Dot d in get_non_blocked
//                         where d.Own == Owner
//                             & aDots[d.x, d.y - 1].Own == enemy_own
//                             & aDots[d.x + 1, d.y].Own == enemy_own
//                             & aDots[d.x, d.y - 2].Own == Owner
//                             & aDots[d.x + 1, d.y + 1].Own == Owner
//                             & aDots[d.x + 1, d.y - 1].Own == 0
//                             & aDots[d.x + 2, d.y].Own == 0
//                             & aDots[d.x - 1, d.y - 1].Own == 0
//                             & aDots[d.x + 2, d.y - 1].Own == 0
//                             & aDots[d.x + 1, d.y - 2].Own == 0
//                         select d;
//            if (pat883.Count() > 0) return new Dot(pat883.First().x + 1, pat883.First().y - 1);
//            //--------------Rotate on 180----------------------------------- 
//            var pat883_2 = from Dot d in get_non_blocked
//                           where d.Own == Owner
//                               & aDots[d.x, d.y + 1].Own == enemy_own
//                               & aDots[d.x - 1, d.y].Own == enemy_own
//                               & aDots[d.x, d.y + 2].Own == Owner
//                               & aDots[d.x - 1, d.y - 1].Own == Owner
//                               & aDots[d.x - 1, d.y + 1].Own == 0
//                               & aDots[d.x - 2, d.y].Own == 0
//                               & aDots[d.x + 1, d.y + 1].Own == 0
//                               & aDots[d.x - 2, d.y + 1].Own == 0
//                           select d;
//            if (pat883_2.Count() > 0) return new Dot(pat883_2.First().x - 1, pat883_2.First().y + 1);

            //============================================================================================================== 
            iNumberPattern = 387;
            var pat387 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 2, d.y - 1].Own != enemy_own
& aDots[d.x - 1, d.y + 2].Own != enemy_own
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
                         select d;
            if (pat387.Count() > 0) return new Dot(pat387.First().x + 1, pat387.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat387_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 2, d.y + 1].Own != enemy_own
& aDots[d.x + 1, d.y - 2].Own != enemy_own
& aDots[d.x - 2, d.y - 1].Own == 0
                           select d;
            if (pat387_2.Count() > 0) return new Dot(pat387_2.First().x - 1, pat387_2.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 865;
            var pat865 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y + 2].Own == enemy_own
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x, d.y + 1].Own != enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
                         select d;
            if (pat865.Count() > 0) return new Dot(pat865.First().x - 1, pat865.First().y);
            //--------------Rotate on 180----------------------------------- 
            var pat865_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y - 2].Own == enemy_own
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x, d.y - 1].Own != enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
                           select d;
            if (pat865_2.Count() > 0) return new Dot(pat865_2.First().x + 1, pat865_2.First().y);
            //============================================================================================================== 


            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
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
