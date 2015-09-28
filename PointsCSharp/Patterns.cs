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
                       where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked==false &
                                              aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                              aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false &
                                              aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false &
                                              aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false &
                                              aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked==false 
                                              select d;
            if (pat3.Count() > 0) return new Dot(pat3.First().x + 1, pat3.First().y + 1);
            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //               cтавить сюда+     *   
            var pat3_1_1 = from Dot d in get_non_blocked
                           where aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                 aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                 aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false &
                                 aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false &
                                 aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false &
                                 aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false &
                                 aDots[d.x, d.y].Own == Owner & aDots[d.x, d.y ].Blocked==false 
                               select d;
            if (pat3_1_1.Count() > 0) return new Dot(pat3_1_1.First().x - 1, pat3_1_1.First().y + 1);
            // паттерн на конструкцию    +     *      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_2 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own &  aDots[d.x + 1, d.y + 1].Blocked==false &
                                                aDots[d.x - 1, d.y].Own != Owner & aDots[d.x - 1, d.y ].Blocked == false &
                                              aDots[d.x + 1, d.y].Own != Owner & aDots[d.x + 1, d.y ].Blocked == false &
                                              aDots[d.x, d.y + 1].Own != Owner & aDots[d.x , d.y + 1].Blocked == false &
                                              aDots[d.x, d.y - 1].Own != Owner &  aDots[d.x , d.y - 1].Blocked==false 

                         select d;
            if (pat3_2.Count() > 0) return new Dot(pat3_2.First().x - 1, pat3_2.First().y - 1);
            // паттерн на конструкцию    *     +      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_3 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1 ].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked==false &
                                                aDots[d.x - 1, d.y].Own != Owner & aDots[d.x - 1, d.y].Blocked == false &
                                              aDots[d.x + 1, d.y].Own != Owner & aDots[d.x + 1, d.y].Blocked == false &
                                              aDots[d.x, d.y + 1].Own != Owner & aDots[d.x, d.y + 1].Blocked == false &
                                              aDots[d.x, d.y - 1].Own != Owner &  aDots[d.x , d.y - 1].Blocked==false 

                         select d;
            if (pat3_3.Count() > 0) return new Dot(pat3_3.First().x + 1, pat3_3.First().y - 1);

             //паттерн на конструкцию    *     +      точка окружена через две точки
             //                            d+    
             //                          *     *   
            var pat3_4 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked==false &
                                                aDots[d.x - 1, d.y].Own != Owner & aDots[d.x - 1, d.y].Blocked == false &
                                                aDots[d.x + 1, d.y].Own != Owner & aDots[d.x + 1, d.y].Blocked == false &
                                                aDots[d.x, d.y + 1].Own != Owner & aDots[d.x, d.y + 1].Blocked == false &
                                                aDots[d.x, d.y - 1].Own != Owner &  aDots[d.x , d.y - 1].Blocked==false

                         select d;
            if (pat3_4.Count() > 0) return new Dot(pat3_4.First().x + 1, pat3_4.First().y - 1);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию    + d+         
            //                          *     *   
            iNumberPattern = 4;
            var pat4_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y ].Blocked == false &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false &
                                                aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false &
                                                aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked==false 
                         select d;
            if (pat4_1.Count() > 0)
            {
            return new Dot(pat4_1.First().x , pat4_1.First().y + 1);
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //паттерн на конструкцию      d+  +         
            //                          *     *   
            var pat4_2 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false &
                                                aDots[d.x, d.y + 2].Own == 0 & aDots[d.x , d.y + 2].Blocked == false &
                                                aDots[d.x + 1, d.y].Own == Owner &  aDots[d.x + 1, d.y ].Blocked==false
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
                         where d.Own == Owner & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x , d.y - 1].Blocked == false &
                                                aDots[d.x, d.y - 2].Own == 0 & aDots[d.x , d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y ].Blocked==false 
                         select d;
            if (pat4_3.Count() > 0) 
            {
            return new Dot(pat4_3.First().x, pat4_3.First().y - 1);
            }
            //паттерн на конструкцию            
            //                          *     *   
            //                            d+  +
            var pat4_4 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false &
                                                aDots[d.x, d.y - 2].Own == 0 & aDots[d.x , d.y - 2].Blocked == false &
                                                aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y ].Blocked==false 
                         select d;
            if (pat4_4.Count() > 0)
            {
            return new Dot(pat4_4.First().x, pat4_4.First().y - 1);
            }

            //паттерн на конструкцию    *        
            //                             +  
            //                          * d+  
            var pat4_5 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x , d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y ].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false &
                                                aDots[d.x, d.y - 2].Own == 0 & aDots[d.x , d.y - 2].Blocked == false &
                                                aDots[d.x - 2, d.y - 1].Own == 0 &  aDots[d.x - 2, d.y - 1].Blocked==false 
                         select d;
            if (pat4_5.Count() > 0)
            {
            return new Dot(pat4_5.First().x-1, pat4_5.First().y - 1);
            }
            //паттерн на конструкцию    *  +       
            //                            d+  
            //                          *   
            var pat4_6 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x , d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false &
                                                aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y ].Blocked == false &
                                                aDots[d.x, d.y + 2].Own == 0 & aDots[d.x , d.y + 2].Blocked == false &
                                                aDots[d.x - 2, d.y].Own == 0 &  aDots[d.x - 2, d.y].Blocked==false 
                         select d;
            if (pat4_6.Count() > 0)
            {
            return new Dot(pat4_6.First().x-1, pat4_6.First().y);
            }

            //паттерн на конструкцию      +  *     
            //                           d+  
            //                               *
            var pat4_7 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y ].Blocked == false &
                                                aDots[d.x + 2, d.y].Own == 0 &  aDots[d.x + 2, d.y].Blocked==false 
                         select d;
            if (pat4_7.Count() > 0)
            {
            return new Dot(pat4_7.First().x + 1, pat4_7.First().y);
            }

            //паттерн на конструкцию         *     
            //                           d+  
            //                            +  *
            var pat4_8 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x , d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false &
                                                aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false &
                                                aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y ].Blocked == false &
                                                aDots[d.x + 2, d.y].Own == 0 &  aDots[d.x + 2, d.y ].Blocked==false 
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
                         where d.Own == Owner & aDots[d.x + 2, d.y].Own == Owner & aDots[d.x + 2, d.y].Blocked == false &
                                                aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y ].Blocked == false &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false &
                                                aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false &
                                                aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false &
                                                aDots[d.x + 2, d.y - 1].Own == Owner &  aDots[d.x + 2, d.y - 1].Blocked==false 
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
& aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked==false
& aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y+1].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y+1].Blocked == false
& aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x , d.y+1].Blocked == false
& aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
& aDots[d.x + 2, d.y + 2].Own == 0 & aDots[d.x + 2, d.y+2].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y+1].Blocked == false 
                         select d;
            if (pat901.Count() > 0) return new Dot(pat901.First().x + 1, pat901.First().y + 1);
            //--------------Rotate on 180----------------------------------- 
            var pat901_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y-1].Blocked == false
& aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x -2, d.y-1].Blocked == false
& aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y-1].Blocked == false
& aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
& aDots[d.x - 2, d.y - 2].Own == 0 & aDots[d.x - 2, d.y - 2].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false 
                           select d;
            if (pat901_2.Count() > 0) return new Dot(pat901_2.First().x - 1, pat901_2.First().y - 1);
            //============================================================================================================== 
            // d+ m+  
            //  *  *  + 
            iNumberPattern = 604;
            var pat604 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y+1].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y+1].Blocked == false 
                         select d;
            if (pat604.Count() > 0) return new Dot(pat604.First().x + 1, pat604.First().y);
            //--------------Rotate on 180----------------------------------- 
            var pat604_2 = from Dot d in get_non_blocked where d.Own == Owner
& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
& aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1 ].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false 
                           select d;
            if (pat604_2.Count() > 0) return new Dot(pat604_2.First().x - 1, pat604_2.First().y);
            //============================================================================================================== 

            iNumberPattern = 549;
            //============================================================================================================== 
            var pat549 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                                                            & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y+1].Blocked == false
                                                            & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x , d.y + 1].Blocked == false
                                                            & aDots[d.x, d.y + 3].Own == Owner & aDots[d.x , d.y+3].Blocked == false
                                                            & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                                            & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y+1].Blocked == false
                                                            & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y+2].Blocked == false
                                                            & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y+2].Blocked == false
                                                            & aDots[d.x - 1, d.y + 3].Own == 0 & aDots[d.x - 1, d.y+3].Blocked == false 
                                                             select d;
            if (pat549.Count() > 0) 
            {
                return new Dot(pat549.First().x - 1, pat549.First().y + 1);
            }
            //--------------Rotate on 180----------------------------------- 
            var pat549_2 = from Dot d in get_non_blocked where d.Own == Owner
                                                            & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                                                            & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y-1].Blocked == false
                                                            & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x , d.y-1].Blocked == false
                                                            & aDots[d.x, d.y - 3].Own == Owner & aDots[d.x , d.y-3].Blocked == false
                                                            & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                                            & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y-1].Blocked == false
                                                            & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x , d.y-2].Blocked == false
                                                            & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y-2].Blocked == false
                                                            & aDots[d.x + 1, d.y - 3].Own == 0 & aDots[d.x + 1, d.y-3].Blocked == false 
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
            iNumberPattern = 334;
            var pat334 = from Dot d in get_non_blocked
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
            if (pat334.Count() > 0) return new Dot(pat334.First().x - 2, pat334.First().y - 1);
            //--------------Rotate on 180----------------------------------- 
            var pat334_2 = from Dot d in get_non_blocked
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
            if (pat334_2.Count() > 0) return new Dot(pat334_2.First().x + 2, pat334_2.First().y + 1);
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
            //iNumberPattern = 364;
            //var pat364 = from Dot d in get_non_blocked
            //             where d.Own == Owner
            //                 & aDots[d.x, d.y + 1].Own == enemy_own
            //                 & aDots[d.x, d.y + 3].Own == Owner
            //                 & aDots[d.x + 1, d.y + 2].Own == Owner
            //                 & aDots[d.x + 1, d.y + 1].Own == Owner
            //                 & aDots[d.x, d.y - 1].Own != enemy_own
            //                 & aDots[d.x, d.y + 4].Own != enemy_own
            //                 & aDots[d.x - 1, d.y].Own == 0
            //                 & aDots[d.x - 1, d.y + 1].Own == 0
            //                 & aDots[d.x - 1, d.y + 2].Own == 0
            //                 & aDots[d.x - 1, d.y + 3].Own == 0
            //                 & aDots[d.x - 2, d.y + 1].Own == 0
            //                 & aDots[d.x - 2, d.y + 2].Own == 0
            //             select d;
            //if (pat364.Count() > 0) return new Dot(pat364.First().x - 2, pat364.First().y + 1);
            ////180 Rotate=========================================================================================================== 
            //var pat364_2 = from Dot d in get_non_blocked
            //               where d.Own == Owner
            //                   & aDots[d.x, d.y - 1].Own == enemy_own
            //                   & aDots[d.x, d.y - 3].Own == Owner
            //                   & aDots[d.x - 1, d.y - 2].Own == Owner
            //                   & aDots[d.x - 1, d.y - 1].Own == Owner
            //                   & aDots[d.x, d.y + 1].Own != enemy_own
            //                   & aDots[d.x, d.y - 4].Own != enemy_own
            //                   & aDots[d.x + 1, d.y].Own == 0
            //                   & aDots[d.x + 1, d.y - 1].Own == 0
            //                   & aDots[d.x + 1, d.y - 2].Own == 0
            //                   & aDots[d.x + 1, d.y - 3].Own == 0
            //                   & aDots[d.x + 2, d.y - 1].Own == 0
            //                   & aDots[d.x + 2, d.y - 2].Own == 0
            //               select d;
            //if (pat364_2.Count() > 0) return new Dot(pat364_2.First().x + 2, pat364_2.First().y - 1);
            ////--------------Rotate on 90----------------------------------- 
            //var pat364_2_3 = from Dot d in get_non_blocked
            //                 where d.Own == Owner
            //                     & aDots[d.x - 1, d.y].Own == enemy_own
            //                     & aDots[d.x - 3, d.y].Own == Owner
            //                     & aDots[d.x - 2, d.y - 1].Own == Owner
            //                     & aDots[d.x - 1, d.y - 1].Own == Owner
            //                     & aDots[d.x + 1, d.y].Own != enemy_own
            //                     & aDots[d.x - 4, d.y].Own != enemy_own
            //                     & aDots[d.x, d.y + 1].Own == 0
            //                     & aDots[d.x - 1, d.y + 1].Own == 0
            //                     & aDots[d.x - 2, d.y + 1].Own == 0
            //                     & aDots[d.x - 3, d.y + 1].Own == 0
            //                     & aDots[d.x - 1, d.y + 2].Own == 0
            //                     & aDots[d.x - 2, d.y + 2].Own == 0
            //                 select d;
            //if (pat364_2_3.Count() > 0) return new Dot(pat364_2_3.First().x - 1, pat364_2_3.First().y + 2);
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat364_2_3_4 = from Dot d in get_non_blocked
            //                   where d.Own == Owner
            //                       & aDots[d.x + 1, d.y].Own == enemy_own
            //                       & aDots[d.x + 3, d.y].Own == Owner
            //                       & aDots[d.x + 2, d.y + 1].Own == Owner
            //                       & aDots[d.x + 1, d.y + 1].Own == Owner
            //                       & aDots[d.x - 1, d.y].Own != enemy_own
            //                       & aDots[d.x + 4, d.y].Own != enemy_own
            //                       & aDots[d.x, d.y - 1].Own == 0
            //                       & aDots[d.x + 1, d.y - 1].Own == 0
            //                       & aDots[d.x + 2, d.y - 1].Own == 0
            //                       & aDots[d.x + 3, d.y - 1].Own == 0
            //                       & aDots[d.x + 1, d.y - 2].Own == 0
            //                       & aDots[d.x + 2, d.y - 2].Own == 0
            //                   select d;
            //if (pat364_2_3_4.Count() > 0) return new Dot(pat364_2_3_4.First().x + 1, pat364_2_3_4.First().y - 2);
            //============================================================================================================== 
            iNumberPattern = 279;
            var pat279 = from Dot d in get_non_blocked
                         where d.Own == Owner & d.x>1 & d.x < iBoardSize-1
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                         select d;
            if (pat279.Count() > 0) return new Dot(pat279.First().x - 1, pat279.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat279_2 = from Dot d in get_non_blocked
                           where d.Own == Owner & d.x > 1 & d.x < iBoardSize - 1
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                           select d;
            if (pat279_2.Count() > 0) return new Dot(pat279_2.First().x + 1, pat279_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat279_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner & d.x > 1 & d.x < iBoardSize - 1
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             select d;
            if (pat279_2_3.Count() > 0) return new Dot(pat279_2_3.First().x - 1, pat279_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat279_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner & d.x > 1 & d.x < iBoardSize - 1
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               select d;
            if (pat279_2_3_4.Count() > 0) return new Dot(pat279_2_3_4.First().x + 1, pat279_2_3_4.First().y - 1);
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

            iNumberPattern = 472;
            var pat472 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x, d.y + 1].Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y + 2].Own == 0
                             & aDots[d.x, d.y + 2].Own == 0
                             & aDots[d.x, d.y + 3].Own == enemy_own
                             & aDots[d.x + 1, d.y + 3].Own == 0
                             & aDots[d.x + 2, d.y + 1].Own == 0
                             & aDots[d.x + 2, d.y + 2].Own == 0
                         select d;
            if (pat472.Count() > 0) return new Dot(pat472.First().x + 1, pat472.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat472_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x, d.y - 1].Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y - 2].Own == 0
                               & aDots[d.x, d.y - 2].Own == 0
                               & aDots[d.x, d.y - 3].Own == enemy_own
                               & aDots[d.x - 1, d.y - 3].Own == 0
                               & aDots[d.x - 2, d.y - 1].Own == 0
                               & aDots[d.x - 2, d.y - 2].Own == 0
                           select d;
            if (pat472_2.Count() > 0) return new Dot(pat472_2.First().x - 1, pat472_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat472_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y].Own == enemy_own
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y].Own == Owner
                                 & aDots[d.x - 1, d.y - 1].Own == 0
                                 & aDots[d.x - 2, d.y - 1].Own == 0
                                 & aDots[d.x - 2, d.y].Own == 0
                                 & aDots[d.x - 3, d.y].Own == enemy_own
                                 & aDots[d.x - 3, d.y - 1].Own == 0
                                 & aDots[d.x - 1, d.y - 2].Own == 0
                                 & aDots[d.x - 2, d.y - 2].Own == 0
                             select d;
            if (pat472_2_3.Count() > 0) return new Dot(pat472_2_3.First().x - 1, pat472_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat472_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y].Own == enemy_own
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y].Own == Owner
                                   & aDots[d.x + 1, d.y + 1].Own == 0
                                   & aDots[d.x + 2, d.y + 1].Own == 0
                                   & aDots[d.x + 2, d.y].Own == 0
                                   & aDots[d.x + 3, d.y].Own == enemy_own
                                   & aDots[d.x + 3, d.y + 1].Own == 0
                                   & aDots[d.x + 1, d.y + 2].Own == 0
                                   & aDots[d.x + 2, d.y + 2].Own == 0
                               select d;
            if (pat472_2_3_4.Count() > 0) return new Dot(pat472_2_3_4.First().x + 1, pat472_2_3_4.First().y + 1);
            ////============================================================================================================== 
            ////      *
            ////  d*    m*
            ////      * 
            //iNumberPattern = 385;
            //var pat385 = from Dot d in get_non_blocked
            //             where d.Own == Owner
            //                 & aDots[d.x + 1, d.y - 1].Own == Owner
            //                 & aDots[d.x + 1, d.y + 1].Own == Owner
            //                 & aDots[d.x + 1, d.y].Own == 0
            //                 & aDots[d.x + 2, d.y].Own == 0
            //             select d;
            //if (pat385.Count() > 0) return new Dot(pat385.First().x + 2, pat385.First().y);
            ////180 Rotate=========================================================================================================== 
            //var pat385_2 = from Dot d in get_non_blocked
            //               where d.Own == Owner
            //                   & aDots[d.x - 1, d.y + 1].Own == Owner
            //                   & aDots[d.x - 1, d.y - 1].Own == Owner
            //                   & aDots[d.x - 1, d.y].Own == 0
            //                   & aDots[d.x - 2, d.y].Own == 0
            //               select d;
            //if (pat385_2.Count() > 0) return new Dot(pat385_2.First().x - 2, pat385_2.First().y);
            ////--------------Rotate on 90----------------------------------- 
            //var pat385_2_3 = from Dot d in get_non_blocked
            //                 where d.Own == Owner
            //                     & aDots[d.x + 1, d.y - 1].Own == Owner
            //                     & aDots[d.x - 1, d.y - 1].Own == Owner
            //                     & aDots[d.x, d.y - 1].Own == 0
            //                     & aDots[d.x, d.y - 2].Own == 0
            //                 select d;
            //if (pat385_2_3.Count() > 0) return new Dot(pat385_2_3.First().x, pat385_2_3.First().y - 2);
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat385_2_3_4 = from Dot d in get_non_blocked
            //                   where d.Own == Owner
            //                       & aDots[d.x - 1, d.y + 1].Own == Owner
            //                       & aDots[d.x + 1, d.y + 1].Own == Owner
            //                       & aDots[d.x, d.y + 1].Own == 0
            //                       & aDots[d.x, d.y + 2].Own == 0
            //                   select d;
            //if (pat385_2_3_4.Count() > 0) return new Dot(pat385_2_3_4.First().x, pat385_2_3_4.First().y + 2);
            ////============================================================================================================== 
            iNumberPattern = 295;
            var pat295 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 2, d.y + 1].Own == enemy_own
                             & aDots[d.x + 3, d.y].Own == enemy_own
                             & aDots[d.x + 2, d.y].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x + 2, d.y - 2].Own == 0
                             & aDots[d.x + 3, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 1].Own != enemy_own
                             & aDots[d.x + 1, d.y - 2].Own != enemy_own
                             & aDots[d.x + 3, d.y + 1].Own != enemy_own
                         select d;
            if (pat295.Count() > 0) return new Dot(pat295.First().x + 2, pat295.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat295_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 2, d.y - 1].Own == enemy_own
                               & aDots[d.x - 3, d.y].Own == enemy_own
                               & aDots[d.x - 2, d.y].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x - 2, d.y + 2].Own == 0
                               & aDots[d.x - 3, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 1].Own != enemy_own
                               & aDots[d.x - 1, d.y + 2].Own != enemy_own
                               & aDots[d.x - 3, d.y - 1].Own != enemy_own
                           select d;
            if (pat295_2.Count() > 0) return new Dot(pat295_2.First().x - 2, pat295_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat295_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y - 1].Own == Owner
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y - 2].Own == enemy_own
                                 & aDots[d.x, d.y - 3].Own == enemy_own
                                 & aDots[d.x, d.y - 2].Own == 0
                                 & aDots[d.x + 1, d.y - 2].Own == 0
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x + 2, d.y - 2].Own == 0
                                 & aDots[d.x + 1, d.y - 3].Own == 0
                                 & aDots[d.x + 1, d.y].Own != enemy_own
                                 & aDots[d.x + 2, d.y - 1].Own != enemy_own
                                 & aDots[d.x - 1, d.y - 3].Own != enemy_own
                             select d;
            if (pat295_2_3.Count() > 0) return new Dot(pat295_2_3.First().x + 1, pat295_2_3.First().y - 2);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat295_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y + 1].Own == Owner
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y + 2].Own == enemy_own
                                   & aDots[d.x, d.y + 3].Own == enemy_own
                                   & aDots[d.x, d.y + 2].Own == 0
                                   & aDots[d.x - 1, d.y + 2].Own == 0
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x - 2, d.y + 2].Own == 0
                                   & aDots[d.x - 1, d.y + 3].Own == 0
                                   & aDots[d.x - 1, d.y].Own != enemy_own
                                   & aDots[d.x - 2, d.y + 1].Own != enemy_own
                                   & aDots[d.x + 1, d.y + 3].Own != enemy_own
                               select d;
            if (pat295_2_3_4.Count() > 0) return new Dot(pat295_2_3_4.First().x - 1, pat295_2_3_4.First().y + 2);
            //============================================================================================================== 

            iNumberPattern = 688;
            var pat688 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 2, d.y - 1].Own == Owner
                             & aDots[d.x + 2, d.y - 2].Own == Owner
                             & aDots[d.x + 1, d.y - 3].Own == Owner
                             & aDots[d.x + 1, d.y].Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 2].Own == 0
                             & aDots[d.x - 1, d.y - 2].Own == 0
                             & aDots[d.x - 1, d.y - 1].Own == 0
                         select d;
            if (pat688.Count() > 0) return new Dot(pat688.First().x, pat688.First().y - 2);
            //180 Rotate=========================================================================================================== 
            var pat688_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 2, d.y + 1].Own == Owner
                               & aDots[d.x - 2, d.y + 2].Own == Owner
                               & aDots[d.x - 1, d.y + 3].Own == Owner
                               & aDots[d.x - 1, d.y].Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 2].Own == 0
                               & aDots[d.x + 1, d.y + 2].Own == 0
                               & aDots[d.x + 1, d.y + 1].Own == 0
                           select d;
            if (pat688_2.Count() > 0) return new Dot(pat688_2.First().x, pat688_2.First().y + 2);
            //--------------Rotate on 90----------------------------------- 
            var pat688_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y - 2].Own == Owner
                                 & aDots[d.x + 2, d.y - 2].Own == Owner
                                 & aDots[d.x + 3, d.y - 1].Own == Owner
                                 & aDots[d.x, d.y - 1].Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y].Own == 0
                                 & aDots[d.x + 2, d.y].Own == 0
                                 & aDots[d.x + 2, d.y + 1].Own == 0
                                 & aDots[d.x + 1, d.y + 1].Own == 0
                             select d;
            if (pat688_2_3.Count() > 0) return new Dot(pat688_2_3.First().x + 2, pat688_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat688_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y + 2].Own == Owner
                                   & aDots[d.x - 2, d.y + 2].Own == Owner
                                   & aDots[d.x - 3, d.y + 1].Own == Owner
                                   & aDots[d.x, d.y + 1].Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y].Own == 0
                                   & aDots[d.x - 2, d.y].Own == 0
                                   & aDots[d.x - 2, d.y - 1].Own == 0
                                   & aDots[d.x - 1, d.y - 1].Own == 0
                               select d;
            if (pat688_2_3_4.Count() > 0) return new Dot(pat688_2_3_4.First().x - 2, pat688_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 982;
            var pat982 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y + 2].Own == enemy_own
                             & aDots[d.x - 1, d.y + 2].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == 0
                             & aDots[d.x, d.y + 1].Own == enemy_own
                             & aDots[d.x, d.y + 2].Own == 0
                             & aDots[d.x + 1, d.y + 1].Own == Owner
                             & aDots[d.x + 1, d.y + 2].Own == 0
                         select d;
            if (pat982.Count() > 0) return new Dot(pat982.First().x - 1, pat982.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat982_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y - 2].Own == enemy_own
                               & aDots[d.x + 1, d.y - 2].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == 0
                               & aDots[d.x, d.y - 1].Own == enemy_own
                               & aDots[d.x, d.y - 2].Own == 0
                               & aDots[d.x - 1, d.y - 1].Own == Owner
                               & aDots[d.x - 1, d.y - 2].Own == 0
                           select d;
            if (pat982_2.Count() > 0) return new Dot(pat982_2.First().x + 1, pat982_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat982_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 2, d.y + 2].Own == enemy_own
                                 & aDots[d.x - 2, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == 0
                                 & aDots[d.x - 1, d.y].Own == enemy_own
                                 & aDots[d.x - 2, d.y].Own == 0
                                 & aDots[d.x - 1, d.y - 1].Own == Owner
                                 & aDots[d.x - 2, d.y - 1].Own == 0
                             select d;
            if (pat982_2_3.Count() > 0) return new Dot(pat982_2_3.First().x - 1, pat982_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat982_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 2, d.y - 2].Own == enemy_own
                                   & aDots[d.x + 2, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == 0
                                   & aDots[d.x + 1, d.y].Own == enemy_own
                                   & aDots[d.x + 2, d.y].Own == 0
                                   & aDots[d.x + 1, d.y + 1].Own == Owner
                                   & aDots[d.x + 2, d.y + 1].Own == 0
                               select d;
            if (pat982_2_3_4.Count() > 0) return new Dot(pat982_2_3_4.First().x + 1, pat982_2_3_4.First().y - 1);
            ////============================================================================================================== 
            //iNumberPattern = 983;
            //var pat983 = from Dot d in get_non_blocked
            //             where d.Own == Owner
            //                 & aDots[d.x, d.y + 1].Own == enemy_own
            //                 & aDots[d.x, d.y + 2].Own == 0
            //                 & aDots[d.x, d.y + 3].Own == Owner
            //                 & aDots[d.x - 1, d.y + 1].Own == 0
            //                 & aDots[d.x - 1, d.y + 2].Own == 0
            //                 & aDots[d.x - 1, d.y].Own == 0
            //                 & aDots[d.x - 1, d.y + 3].Own == 0
            //             select d;
            //if (pat983.Count() > 0) return new Dot(pat983.First().x - 1, pat983.First().y + 1);
            ////180 Rotate=========================================================================================================== 
            //var pat983_2 = from Dot d in get_non_blocked
            //               where d.Own == Owner
            //                   & aDots[d.x, d.y - 1].Own == enemy_own
            //                   & aDots[d.x, d.y - 2].Own == 0
            //                   & aDots[d.x, d.y - 3].Own == Owner
            //                   & aDots[d.x + 1, d.y - 1].Own == 0
            //                   & aDots[d.x + 1, d.y - 2].Own == 0
            //                   & aDots[d.x + 1, d.y].Own == 0
            //                   & aDots[d.x + 1, d.y - 3].Own == 0
            //               select d;
            //if (pat983_2.Count() > 0) return new Dot(pat983_2.First().x + 1, pat983_2.First().y - 1);
            ////--------------Rotate on 90----------------------------------- 
            //var pat983_2_3 = from Dot d in get_non_blocked
            //                 where d.Own == Owner
            //                     & aDots[d.x - 1, d.y].Own == enemy_own
            //                     & aDots[d.x - 2, d.y].Own == 0
            //                     & aDots[d.x - 3, d.y].Own == Owner
            //                     & aDots[d.x - 1, d.y + 1].Own == 0
            //                     & aDots[d.x - 2, d.y + 1].Own == 0
            //                     & aDots[d.x, d.y + 1].Own == 0
            //                     & aDots[d.x - 3, d.y + 1].Own == 0
            //                 select d;
            //if (pat983_2_3.Count() > 0) return new Dot(pat983_2_3.First().x - 1, pat983_2_3.First().y + 1);
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat983_2_3_4 = from Dot d in get_non_blocked
            //                   where d.Own == Owner
            //                       & aDots[d.x + 1, d.y].Own == enemy_own
            //                       & aDots[d.x + 2, d.y].Own == 0
            //                       & aDots[d.x + 3, d.y].Own == Owner
            //                       & aDots[d.x + 1, d.y - 1].Own == 0
            //                       & aDots[d.x + 2, d.y - 1].Own == 0
            //                       & aDots[d.x, d.y - 1].Own == 0
            //                       & aDots[d.x + 3, d.y - 1].Own == 0
            //                   select d;
            //if (pat983_2_3_4.Count() > 0) return new Dot(pat983_2_3_4.First().x + 1, pat983_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 984;
            var pat984 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 2, d.y].Own == 0
                             & aDots[d.x + 2, d.y - 1].Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 1].Own == 0
                             & aDots[d.x, d.y + 1].Own == 0
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x + 3, d.y - 1].Own == 0
                         select d;
            if (pat984.Count() > 0) return new Dot(pat984.First().x + 1, pat984.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat984_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 2, d.y].Own == 0
                               & aDots[d.x - 2, d.y + 1].Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 1].Own == 0
                               & aDots[d.x, d.y - 1].Own == 0
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x - 3, d.y + 1].Own == 0
                           select d;
            if (pat984_2.Count() > 0) return new Dot(pat984_2.First().x - 1, pat984_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat984_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x, d.y - 2].Own == 0
                                 & aDots[d.x + 1, d.y - 2].Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x + 1, d.y].Own == 0
                                 & aDots[d.x - 1, d.y].Own == 0
                                 & aDots[d.x, d.y + 1].Own == 0
                                 & aDots[d.x + 1, d.y - 3].Own == 0
                             select d;
            if (pat984_2_3.Count() > 0) return new Dot(pat984_2_3.First().x + 1, pat984_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat984_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x, d.y + 2].Own == 0
                                   & aDots[d.x - 1, d.y + 2].Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x - 1, d.y].Own == 0
                                   & aDots[d.x + 1, d.y].Own == 0
                                   & aDots[d.x, d.y - 1].Own == 0
                                   & aDots[d.x - 1, d.y + 3].Own == 0
                               select d;
            if (pat295_2_3_4.Count() > 0) return new Dot(pat295_2_3_4.First().x - 1, pat295_2_3_4.First().y + 1);
            //============================================================================================================== 

            iNumberPattern = 30;
            var pat30 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & aDots[d.x, d.y + 1].Own == enemy_own
                            & aDots[d.x - 1, d.y].Own == 0
                            & aDots[d.x - 1, d.y - 1].Own == 0
                            & aDots[d.x - 2, d.y - 1].Own == 0
                            & aDots[d.x - 2, d.y].Own == 0
                            & aDots[d.x - 1, d.y + 1].Own == 0
                            & aDots[d.x - 2, d.y + 1].Own == 0
                            & aDots[d.x - 2, d.y - 2].Own == enemy_own
                            & aDots[d.x - 1, d.y - 2].Own != enemy_own
                            & aDots[d.x, d.y - 1].Own != enemy_own
                        select d;
            if (pat30.Count() > 0) return new Dot(pat30.First().x - 1, pat30.First().y);
            //180 Rotate=========================================================================================================== 
            var pat30_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x, d.y - 1].Own == enemy_own
                              & aDots[d.x + 1, d.y].Own == 0
                              & aDots[d.x + 1, d.y + 1].Own == 0
                              & aDots[d.x + 2, d.y + 1].Own == 0
                              & aDots[d.x + 2, d.y].Own == 0
                              & aDots[d.x + 1, d.y - 1].Own == 0
                              & aDots[d.x + 2, d.y - 1].Own == 0
                              & aDots[d.x + 2, d.y + 2].Own == enemy_own
                              & aDots[d.x + 1, d.y + 2].Own != enemy_own
                              & aDots[d.x, d.y + 1].Own != enemy_own
                          select d;
            if (pat30_2.Count() > 0) return new Dot(pat30_2.First().x + 1, pat30_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat30_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & aDots[d.x - 1, d.y].Own == enemy_own
                                & aDots[d.x, d.y + 1].Own == 0
                                & aDots[d.x + 1, d.y + 1].Own == 0
                                & aDots[d.x + 1, d.y + 2].Own == 0
                                & aDots[d.x, d.y + 2].Own == 0
                                & aDots[d.x - 1, d.y + 1].Own == 0
                                & aDots[d.x - 1, d.y + 2].Own == 0
                                & aDots[d.x + 2, d.y + 2].Own == enemy_own
                                & aDots[d.x + 2, d.y + 1].Own != enemy_own
                                & aDots[d.x + 1, d.y].Own != enemy_own
                            select d;
            if (pat30_2_3.Count() > 0) return new Dot(pat30_2_3.First().x, pat30_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat30_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & aDots[d.x + 1, d.y].Own == enemy_own
                                  & aDots[d.x, d.y - 1].Own == 0
                                  & aDots[d.x - 1, d.y - 1].Own == 0
                                  & aDots[d.x - 1, d.y - 2].Own == 0
                                  & aDots[d.x, d.y - 2].Own == 0
                                  & aDots[d.x + 1, d.y - 1].Own == 0
                                  & aDots[d.x + 1, d.y - 2].Own == 0
                                  & aDots[d.x - 2, d.y - 2].Own == enemy_own
                                  & aDots[d.x - 2, d.y - 1].Own != enemy_own
                                  & aDots[d.x - 1, d.y].Own != enemy_own
                              select d;
            if (pat30_2_3_4.Count() > 0) return new Dot(pat30_2_3_4.First().x, pat30_2_3_4.First().y - 1);
            iNumberPattern = 758;
            var pat758 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
& aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
& aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
& aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                         select d;
            if (pat758.Count() > 0) return new Dot(pat758.First().x - 1, pat758.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat758_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
& aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
& aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                           select d;
            if (pat758_2.Count() > 0) return new Dot(pat758_2.First().x + 1, pat758_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat758_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
& aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                             select d;
            if (pat758_2_3.Count() > 0) return new Dot(pat758_2_3.First().x + 1, pat758_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat758_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
& aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
& aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                               select d;
            if (pat758_2_3_4.Count() > 0) return new Dot(pat758_2_3_4.First().x - 1, pat758_2_3_4.First().y - 1);
            //============================================================================================================== 

            //iNumberPattern = 758;
            //var pat758 = from Dot d in get_non_blocked
            //             where d.Own == Owner
            //                 & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
            //                 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
            //                 & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
            //                 & aDots[d.x + 2, d.y + 1].Own == enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
            //                 & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
            //                 & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
            //                 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
            //             select d;
            //if (pat758.Count() > 0) return new Dot(pat758.First().x + 1, pat758.First().y + 1);
            ////180 Rotate=========================================================================================================== 
            //var pat758_2 = from Dot d in get_non_blocked
            //               where d.Own == Owner
            //                   & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
            //                   & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
            //                   & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
            //                   & aDots[d.x - 2, d.y - 1].Own == enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
            //                   & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
            //                   & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
            //                   & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
            //               select d;
            //if (pat758_2.Count() > 0) return new Dot(pat758_2.First().x - 1, pat758_2.First().y - 1);
            ////--------------Rotate on 90----------------------------------- 
            //var pat758_2_3 = from Dot d in get_non_blocked
            //                 where d.Own == Owner
            //                     & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
            //                     & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
            //                     & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
            //                     & aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
            //                     & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
            //                     & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
            //                     & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
            //                 select d;
            //if (pat758_2_3.Count() > 0) return new Dot(pat758_2_3.First().x - 1, pat758_2_3.First().y - 1);
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat758_2_3_4 = from Dot d in get_non_blocked
            //                   where d.Own == Owner
            //                       & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
            //                       & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
            //                       & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
            //                       & aDots[d.x + 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
            //                       & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
            //                       & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
            //                       & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
            //                   select d;
            //if (pat758_2_3_4.Count() > 0) return new Dot(pat758_2_3_4.First().x + 1, pat758_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 524;
            var pat524 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own
                             & aDots[d.x - 1, d.y].Own != enemy_own
                             & aDots[d.x, d.y - 1].Own == 0
                             & aDots[d.x - 1, d.y - 1].Own == 0
                             & aDots[d.x - 2, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 2].Own == 0
                         select d;
            if (pat524.Count() > 0) return new Dot(pat524.First().x - 1, pat524.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat524_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own
                               & aDots[d.x + 1, d.y].Own != enemy_own
                               & aDots[d.x, d.y + 1].Own == 0
                               & aDots[d.x + 1, d.y + 1].Own == 0
                               & aDots[d.x + 2, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 2].Own == 0
                           select d;
            if (pat524_2.Count() > 0) return new Dot(pat524_2.First().x + 1, pat524_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat524_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 2].Own == enemy_own
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x, d.y + 1].Own != enemy_own
                                 & aDots[d.x + 1, d.y].Own == 0
                                 & aDots[d.x + 1, d.y + 1].Own == 0
                                 & aDots[d.x + 1, d.y + 2].Own == 0
                                 & aDots[d.x + 2, d.y].Own == 0
                             select d;
            if (pat524_2_3.Count() > 0) return new Dot(pat524_2_3.First().x + 1, pat524_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat524_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 2].Own == enemy_own
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x, d.y - 1].Own != enemy_own
                                   & aDots[d.x - 1, d.y].Own == 0
                                   & aDots[d.x - 1, d.y - 1].Own == 0
                                   & aDots[d.x - 1, d.y - 2].Own == 0
                                   & aDots[d.x - 2, d.y].Own == 0
                               select d;
            if (pat524_2_3_4.Count() > 0) return new Dot(pat524_2_3_4.First().x - 1, pat524_2_3_4.First().y - 1);
            //============================================================================================================== 
            // *  any  m
            //any  d  any
            // *  any  *
            iNumberPattern = 792;
            var pat792 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == enemy_own
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y].Own != enemy_own
                             & aDots[d.x, d.y + 1].Own != enemy_own
                             & aDots[d.x - 1, d.y].Own != enemy_own
                             & aDots[d.x, d.y - 1].Own != enemy_own
                         select d;
            if (pat792.Count() > 0) return new Dot(pat792.First().x + 1, pat792.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat792_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == enemy_own
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y].Own != enemy_own
                               & aDots[d.x, d.y - 1].Own != enemy_own
                               & aDots[d.x + 1, d.y].Own != enemy_own
                               & aDots[d.x, d.y + 1].Own != enemy_own
                           select d;
            if (pat792_2.Count() > 0) return new Dot(pat792_2.First().x - 1, pat792_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat792_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own
                                 & aDots[d.x, d.y - 1].Own != enemy_own
                                 & aDots[d.x - 1, d.y].Own != enemy_own
                                 & aDots[d.x, d.y + 1].Own != enemy_own
                                 & aDots[d.x + 1, d.y].Own != enemy_own
                             select d;
            if (pat792_2_3.Count() > 0) return new Dot(pat792_2_3.First().x + 1, pat792_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat792_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own
                                   & aDots[d.x, d.y + 1].Own != enemy_own
                                   & aDots[d.x + 1, d.y].Own != enemy_own
                                   & aDots[d.x, d.y - 1].Own != enemy_own
                                   & aDots[d.x - 1, d.y].Own != enemy_own
                               select d;
            if (pat792_2_3_4.Count() > 0) return new Dot(pat792_2_3_4.First().x - 1, pat792_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 670;
            var pat670 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y].Own == enemy_own
                             & aDots[d.x - 2, d.y].Own == enemy_own
                             & aDots[d.x - 1, d.y].Own == 0
                             & aDots[d.x - 1, d.y - 1].Own == 0
                             & aDots[d.x, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 1].Own == 0
                         select d;
            if (pat670.Count() > 0) return new Dot(pat670.First().x - 1, pat670.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat670_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y].Own == enemy_own
                               & aDots[d.x + 2, d.y].Own == enemy_own
                               & aDots[d.x + 1, d.y].Own == 0
                               & aDots[d.x + 1, d.y + 1].Own == 0
                               & aDots[d.x, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 1].Own == 0
                           select d;
            if (pat670_2.Count() > 0) return new Dot(pat670_2.First().x + 1, pat670_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat670_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y - 1].Own == enemy_own
                                 & aDots[d.x, d.y + 2].Own == enemy_own
                                 & aDots[d.x, d.y + 1].Own == 0
                                 & aDots[d.x + 1, d.y + 1].Own == 0
                                 & aDots[d.x + 1, d.y].Own == 0
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                             select d;
            if (pat670_2_3.Count() > 0) return new Dot(pat670_2_3.First().x + 1, pat670_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat670_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y + 1].Own == enemy_own
                                   & aDots[d.x, d.y - 2].Own == enemy_own
                                   & aDots[d.x, d.y - 1].Own == 0
                                   & aDots[d.x - 1, d.y - 1].Own == 0
                                   & aDots[d.x - 1, d.y].Own == 0
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                               select d;
            if (pat670_2_3_4.Count() > 0) return new Dot(pat670_2_3_4.First().x - 1, pat670_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 276;
            var pat276 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x , d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y ].Blocked == false
                         select d;
            if (pat276.Count() > 0) return new Dot(pat276.First().x + 1, pat276.First().y);
            //180 Rotate=========================================================================================================== 
            var pat276_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked==false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x , d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y ].Blocked == false
                               & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                           select d;

            if (pat276_2.Count() > 0) return new Dot(pat276_2.First().x - 1, pat276_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat276_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x , d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x , d.y - 2].Blocked == false
                             select d;
            if (pat276_2_3.Count() > 0) return new Dot(pat276_2_3.First().x, pat276_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat276_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y ].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x , d.y + 2].Blocked == false
                               select d;
            if (pat276_2_3_4.Count() > 0) return new Dot(pat276_2_3_4.First().x, pat276_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 382;
            var pat382 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x + 2, d.y + 1].Own == enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat382.Count() > 0) return new Dot(pat382.First().x, pat382.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat382_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x - 2, d.y - 1].Own == enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat382_2.Count() > 0) return new Dot(pat382_2.First().x, pat382_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat382_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat382_2_3.Count() > 0) return new Dot(pat382_2_3.First().x - 1, pat382_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat382_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat382_2_3_4.Count() > 0) return new Dot(pat382_2_3_4.First().x + 1, pat382_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 475;
            var pat475 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y].Own == Owner
                             & aDots[d.x + 2, d.y - 1].Own == Owner
                             & aDots[d.x + 2, d.y - 2].Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x, d.y - 2].Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 2].Own == 0
                         select d;
            if (pat475.Count() > 0) return new Dot(pat475.First().x - 1, pat475.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat475_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y].Own == Owner
                               & aDots[d.x - 2, d.y + 1].Own == Owner
                               & aDots[d.x - 2, d.y + 2].Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x, d.y + 2].Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 2].Own == 0
                           select d;
            if (pat475_2.Count() > 0) return new Dot(pat475_2.First().x + 1, pat475_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat475_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y - 1].Own == Owner
                                 & aDots[d.x + 1, d.y - 2].Own == Owner
                                 & aDots[d.x + 2, d.y - 2].Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y].Own == enemy_own
                                 & aDots[d.x + 2, d.y].Own == Owner
                                 & aDots[d.x + 1, d.y + 1].Own == 0
                                 & aDots[d.x + 2, d.y - 1].Own == 0
                             select d;
            if (pat475_2_3.Count() > 0) return new Dot(pat475_2_3.First().x + 1, pat475_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat475_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y + 1].Own == Owner
                                   & aDots[d.x - 1, d.y + 2].Own == Owner
                                   & aDots[d.x - 2, d.y + 2].Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y].Own == enemy_own
                                   & aDots[d.x - 2, d.y].Own == Owner
                                   & aDots[d.x - 1, d.y - 1].Own == 0
                                   & aDots[d.x - 2, d.y + 1].Own == 0
                               select d;
            if (pat475_2_3_4.Count() > 0) return new Dot(pat475_2_3_4.First().x - 1, pat475_2_3_4.First().y - 1);
            //============================================================================================================== 
            //iNumberPattern = 723;
            //var pat723 = from Dot d in get_non_blocked
            //             where d.Own == Owner
            //                 & aDots[d.x - 1, d.y].Own == enemy_own
            //                 & aDots[d.x, d.y - 1].Own == enemy_own
            //                 & aDots[d.x + 1, d.y].Own == 0
            //                 & aDots[d.x + 1, d.y + 1].Own == 0
            //                 & aDots[d.x, d.y + 1].Own == 0
            //             select d;
            //if (pat723.Count() > 0) return new Dot(pat723.First().x + 1, pat723.First().y + 1);
            ////180 Rotate=========================================================================================================== 
            //var pat723_2 = from Dot d in get_non_blocked
            //               where d.Own == Owner
            //                   & aDots[d.x + 1, d.y].Own == enemy_own
            //                   & aDots[d.x, d.y + 1].Own == enemy_own
            //                   & aDots[d.x - 1, d.y].Own == 0
            //                   & aDots[d.x - 1, d.y - 1].Own == 0
            //                   & aDots[d.x, d.y - 1].Own == 0
            //               select d;
            //if (pat723_2.Count() > 0) return new Dot(pat723_2.First().x - 1, pat723_2.First().y - 1);
            ////--------------Rotate on 90----------------------------------- 
            //var pat723_2_3 = from Dot d in get_non_blocked
            //                 where d.Own == Owner
            //                     & aDots[d.x, d.y + 1].Own == enemy_own
            //                     & aDots[d.x + 1, d.y].Own == enemy_own
            //                     & aDots[d.x, d.y - 1].Own == 0
            //                     & aDots[d.x - 1, d.y - 1].Own == 0
            //                     & aDots[d.x - 1, d.y].Own == 0
            //                 select d;
            //if (pat723_2_3.Count() > 0) return new Dot(pat723_2_3.First().x - 1, pat723_2_3.First().y - 1);
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat723_2_3_4 = from Dot d in get_non_blocked
            //                   where d.Own == Owner
            //                       & aDots[d.x, d.y - 1].Own == enemy_own
            //                       & aDots[d.x - 1, d.y].Own == enemy_own
            //                       & aDots[d.x, d.y + 1].Own == 0
            //                       & aDots[d.x + 1, d.y + 1].Own == 0
            //                       & aDots[d.x + 1, d.y].Own == 0
            //                   select d;
            //if (pat723_2_3_4.Count() > 0) return new Dot(pat723_2_3_4.First().x + 1, pat723_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 865;
            var pat865 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own
                             & aDots[d.x - 1, d.y].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == enemy_own
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own
                             & aDots[d.x + 2, d.y - 1].Own == enemy_own
                             & aDots[d.x + 2, d.y].Own == 0
                             & aDots[d.x + 2, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y + 1].Own == 0
                             & aDots[d.x, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y].Own == Owner
                         select d;
            if (pat865.Count() > 0) return new Dot(pat865.First().x + 2, pat865.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat865_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own
                               & aDots[d.x + 1, d.y].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == enemy_own
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own
                               & aDots[d.x - 2, d.y + 1].Own == enemy_own
                               & aDots[d.x - 2, d.y].Own == 0
                               & aDots[d.x - 2, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y - 1].Own == 0
                               & aDots[d.x, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y].Own == Owner
                           select d;
            if (pat865_2.Count() > 0) return new Dot(pat865_2.First().x - 2, pat865_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat865_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                 & aDots[d.x, d.y + 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                 & aDots[d.x + 1, d.y - 2].Own == enemy_own
                                 & aDots[d.x, d.y - 2].Own == 0
                                 & aDots[d.x - 1, d.y - 2].Own == 0
                                 & aDots[d.x - 1, d.y - 1].Own == 0
                                 & aDots[d.x - 1, d.y].Own == 0
                                 & aDots[d.x, d.y - 1].Own == Owner
                             select d;
            if (pat865_2_3.Count() > 0) return new Dot(pat865_2_3.First().x - 1, pat865_2_3.First().y - 2);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat865_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y - 1].Own == enemy_own
                                   & aDots[d.x, d.y - 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own
                                   & aDots[d.x - 1, d.y + 2].Own == enemy_own
                                   & aDots[d.x, d.y + 2].Own == 0
                                   & aDots[d.x + 1, d.y + 2].Own == 0
                                   & aDots[d.x + 1, d.y + 1].Own == 0
                                   & aDots[d.x + 1, d.y].Own == 0
                                   & aDots[d.x, d.y + 1].Own == Owner
                               select d;
            if (pat865_2_3_4.Count() > 0) return new Dot(pat865_2_3_4.First().x + 1, pat865_2_3_4.First().y + 2);
            iNumberPattern = 385;
            var pat385 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 2, d.y + 1].Own == enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat385.Count() > 0) return new Dot(pat385.First().x - 1, pat385.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat385_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 2, d.y - 1].Own == enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                               & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat385_2.Count() > 0) return new Dot(pat385_2.First().x + 1, pat385_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat385_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat385_2_3.Count() > 0) return new Dot(pat385_2_3.First().x - 1, pat385_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat385_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat385_2_3_4.Count() > 0) return new Dot(pat385_2_3_4.First().x + 1, pat385_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 386;
            var pat386 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x, d.y - 3].Own == Owner & aDots[d.x , d.y - 3].Blocked == false
                             & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x +1, d.y ].Blocked == false
                             & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x , d.y - 2].Blocked == false
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                             & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                         select d;
            if (pat386.Count() > 0) return new Dot(pat386.First().x + 1, pat386.First().y - 2);
            //180 Rotate=========================================================================================================== 
            var pat386_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x, d.y + 3].Own == Owner & aDots[d.x , d.y +3].Blocked == false
                               & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                               & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x , d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                               & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 2, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 2].Blocked == false
                               & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                           select d;
            if (pat386_2.Count() > 0) return new Dot(pat386_2.First().x - 1, pat386_2.First().y + 2);
            //--------------Rotate on 90----------------------------------- 
            var pat386_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 3, d.y].Own == Owner & aDots[d.x + 3, d.y].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x , d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                                 & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                             select d;
            if (pat386_2_3.Count() > 0) return new Dot(pat386_2_3.First().x + 2, pat386_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat386_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 3, d.y].Own == Owner & aDots[d.x - 3, d.y].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x , d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x-2, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 2].Blocked == false
                                   & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                               select d;
            if (pat386_2_3_4.Count() > 0) return new Dot(pat386_2_3_4.First().x - 2, pat386_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 879;
            var pat879 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 3, d.y - 3].Own == Owner & aDots[d.x - 3, d.y - 3].Blocked == false
& aDots[d.x - 3, d.y - 2].Own == Owner & aDots[d.x - 3, d.y - 2].Blocked == false
& aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x - 2, d.y - 2].Own == enemy_own & aDots[d.x - 2, d.y - 2].Blocked == false
& aDots[d.x - 2, d.y - 3].Own == 0 & aDots[d.x - 2, d.y - 3].Blocked == false
& aDots[d.x - 1, d.y - 3].Own == 0 & aDots[d.x - 1, d.y - 3].Blocked == false
& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
& aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
& aDots[d.x, d.y - 3].Own == 0 & aDots[d.x, d.y - 3].Blocked == false
& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat879.Count() > 0) return new Dot(pat879.First().x, pat879.First().y - 3);
            //180 Rotate=========================================================================================================== 
            var pat879_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 3, d.y + 3].Own == Owner & aDots[d.x + 3, d.y + 3].Blocked == false
& aDots[d.x + 3, d.y + 2].Own == Owner & aDots[d.x + 3, d.y + 2].Blocked == false
& aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x + 2, d.y + 2].Own == enemy_own & aDots[d.x + 2, d.y + 2].Blocked == false
& aDots[d.x + 2, d.y + 3].Own == 0 & aDots[d.x + 2, d.y + 3].Blocked == false
& aDots[d.x + 1, d.y + 3].Own == 0 & aDots[d.x + 1, d.y + 3].Blocked == false
& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
& aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
& aDots[d.x, d.y + 3].Own == 0 & aDots[d.x, d.y + 3].Blocked == false
& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat879_2.Count() > 0) return new Dot(pat879_2.First().x, pat879_2.First().y + 3);
            //--------------Rotate on 90----------------------------------- 
            var pat879_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 3, d.y + 3].Own == Owner & aDots[d.x + 3, d.y + 3].Blocked == false
& aDots[d.x + 2, d.y + 3].Own == Owner & aDots[d.x + 2, d.y + 3].Blocked == false
& aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
& aDots[d.x + 2, d.y + 2].Own == enemy_own & aDots[d.x + 2, d.y + 2].Blocked == false
& aDots[d.x + 3, d.y + 2].Own == 0 & aDots[d.x + 3, d.y + 2].Blocked == false
& aDots[d.x + 3, d.y + 1].Own == 0 & aDots[d.x + 3, d.y + 1].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
& aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
& aDots[d.x + 3, d.y].Own == 0 & aDots[d.x + 3, d.y].Blocked == false
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat879_2_3.Count() > 0) return new Dot(pat879_2_3.First().x + 3, pat879_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat879_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                & aDots[d.x - 3, d.y - 3].Own == Owner & aDots[d.x - 3, d.y - 3].Blocked == false
                                & aDots[d.x - 2, d.y - 3].Own == Owner & aDots[d.x - 2, d.y - 3].Blocked == false
                                & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                                & aDots[d.x - 2, d.y - 2].Own == enemy_own & aDots[d.x - 2, d.y - 2].Blocked == false
                                & aDots[d.x - 3, d.y - 2].Own == 0 & aDots[d.x - 3, d.y - 2].Blocked == false
                                & aDots[d.x - 3, d.y - 1].Own == 0 & aDots[d.x - 3, d.y - 1].Blocked == false
                                & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                                & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                & aDots[d.x - 3, d.y].Own == 0 & aDots[d.x - 3, d.y].Blocked == false
                                & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat879_2_3_4.Count() > 0) return new Dot(pat879_2_3_4.First().x - 3, pat879_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 241;
            var pat241 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 3, d.y].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
                         select d;
            if (pat241.Count() > 0) return new Dot(pat241.First().x - 1, pat241.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat241_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 3, d.y].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
                           select d;
            if (pat241_2.Count() > 0) return new Dot(pat241_2.First().x + 1, pat241_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat241_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x, d.y + 3].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
                             select d;
            if (pat241_2_3.Count() > 0) return new Dot(pat241_2_3.First().x + 1, pat241_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat241_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x, d.y - 3].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
                               select d;
            if (pat241_2_3_4.Count() > 0) return new Dot(pat241_2_3_4.First().x - 1, pat241_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 659;
            var pat659 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x, d.y - 3].Own == Owner
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 2].Own == Owner
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 3].Own == 0
& aDots[d.x + 1, d.y].Own == 0
                         select d;
            if (pat659.Count() > 0) return new Dot(pat659.First().x + 1, pat659.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat659_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y + 3].Own == Owner
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 2].Own == Owner
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 3].Own == 0
& aDots[d.x - 1, d.y].Own == 0
                           select d;
            if (pat659_2.Count() > 0) return new Dot(pat659_2.First().x - 1, pat659_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat659_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 3, d.y].Own == Owner
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 2, d.y + 1].Own == Owner
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 3, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
                             select d;
            if (pat659_2_3.Count() > 0) return new Dot(pat659_2_3.First().x + 1, pat659_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat659_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 3, d.y].Own == Owner
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 2, d.y - 1].Own == Owner
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 3, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
                               select d;
            if (pat659_2_3_4.Count() > 0) return new Dot(pat659_2_3_4.First().x - 1, pat659_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 680;
            var pat680 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 2].Own == enemy_own
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
                         select d;
            if (pat680.Count() > 0) return new Dot(pat680.First().x + 1, pat680.First().y);
            //180 Rotate=========================================================================================================== 
            var pat680_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 2].Own == enemy_own
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
                           select d;
            if (pat680_2.Count() > 0) return new Dot(pat680_2.First().x - 1, pat680_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat680_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 2, d.y - 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
                             select d;
            if (pat680_2_3.Count() > 0) return new Dot(pat680_2_3.First().x, pat680_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat680_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 2, d.y + 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
                               select d;
            if (pat680_2_3_4.Count() > 0) return new Dot(pat680_2_3_4.First().x, pat680_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 272;
            var pat272 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x, d.y + 1].Own != enemy_own
& aDots[d.x - 1, d.y].Own != enemy_own
                         select d;
            if (pat272.Count() > 0) return new Dot(pat272.First().x + 1, pat272.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat272_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x, d.y - 1].Own != enemy_own
& aDots[d.x + 1, d.y].Own != enemy_own
                           select d;
            if (pat272_2.Count() > 0) return new Dot(pat272_2.First().x - 1, pat272_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat272_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own != enemy_own
& aDots[d.x, d.y + 1].Own != enemy_own
                             select d;
            if (pat272_2_3.Count() > 0) return new Dot(pat272_2_3.First().x + 1, pat272_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat272_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own != enemy_own
& aDots[d.x, d.y - 1].Own != enemy_own
                               select d;
            if (pat272_2_3_4.Count() > 0) return new Dot(pat272_2_3_4.First().x - 1, pat272_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 220;
            var pat220 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x, d.y + 2].Own == Owner
& aDots[d.x, d.y + 3].Own == enemy_own
& aDots[d.x - 1, d.y + 2].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
                         select d;
            if (pat220.Count() > 0) return new Dot(pat220.First().x, pat220.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat220_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x, d.y - 2].Own == Owner
& aDots[d.x, d.y - 3].Own == enemy_own
& aDots[d.x + 1, d.y - 2].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
                           select d;
            if (pat220_2.Count() > 0) return new Dot(pat220_2.First().x, pat220_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat220_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 2, d.y].Own == Owner
& aDots[d.x - 3, d.y].Own == enemy_own
& aDots[d.x - 2, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
                             select d;
            if (pat220_2_3.Count() > 0) return new Dot(pat220_2_3.First().x - 1, pat220_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat220_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 2, d.y].Own == Owner
& aDots[d.x + 3, d.y].Own == enemy_own
& aDots[d.x + 2, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
                               select d;
            if (pat220_2_3_4.Count() > 0) return new Dot(pat220_2_3_4.First().x + 1, pat220_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 962;
            var pat962 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x, d.y - 3].Own == enemy_own
& aDots[d.x - 1, d.y - 2].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
                         select d;
            if (pat962.Count() > 0) return new Dot(pat962.First().x + 1, pat962.First().y);
            //180 Rotate=========================================================================================================== 
            var pat962_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y + 3].Own == enemy_own
& aDots[d.x + 1, d.y + 2].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
                           select d;
            if (pat962_2.Count() > 0) return new Dot(pat962_2.First().x - 1, pat962_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat962_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 3, d.y].Own == enemy_own
& aDots[d.x + 2, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
                             select d;
            if (pat962_2_3.Count() > 0) return new Dot(pat962_2_3.First().x, pat962_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat962_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 3, d.y].Own == enemy_own
& aDots[d.x - 2, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
                               select d;
            if (pat962_2_3_4.Count() > 0) return new Dot(pat962_2_3_4.First().x, pat962_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 823;
            var pat823 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked==false
& aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
& aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat823.Count() > 0) return new Dot(pat823.First().x - 1, pat823.First().y);
            //180 Rotate=========================================================================================================== 
            var pat823_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
& aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat823_2.Count() > 0) return new Dot(pat823_2.First().x + 1, pat823_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat823_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
& aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat823_2_3.Count() > 0) return new Dot(pat823_2_3.First().x, pat823_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat823_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
& aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat823_2_3_4.Count() > 0) return new Dot(pat823_2_3_4.First().x, pat823_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 753;
            var pat753 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 2].Own == enemy_own
& aDots[d.x - 1, d.y + 3].Own == enemy_own
& aDots[d.x - 1, d.y + 4].Own == Owner
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x, d.y + 3].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x + 1, d.y + 3].Own == 0
                         select d;
            if (pat753.Count() > 0) return new Dot(pat753.First().x, pat753.First().y + 3);
            //180 Rotate=========================================================================================================== 
            var pat753_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 2].Own == enemy_own
& aDots[d.x + 1, d.y - 3].Own == enemy_own
& aDots[d.x + 1, d.y - 4].Own == Owner
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x, d.y - 3].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x - 1, d.y - 3].Own == 0
                           select d;
            if (pat753_2.Count() > 0) return new Dot(pat753_2.First().x, pat753_2.First().y - 3);
            //--------------Rotate on 90----------------------------------- 
            var pat753_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x - 2, d.y + 1].Own == enemy_own
& aDots[d.x - 3, d.y + 1].Own == enemy_own
& aDots[d.x - 4, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 3, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 3, d.y - 1].Own == 0
                             select d;
            if (pat753_2_3.Count() > 0) return new Dot(pat753_2_3.First().x - 3, pat753_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat753_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x + 2, d.y - 1].Own == enemy_own
& aDots[d.x + 3, d.y - 1].Own == enemy_own
& aDots[d.x + 4, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 3, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 3, d.y + 1].Own == 0
                               select d;
            if (pat753_2_3_4.Count() > 0) return new Dot(pat753_2_3_4.First().x + 3, pat753_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 673;
            var pat673 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 3, d.y].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
                         select d;
            if (pat673.Count() > 0) return new Dot(pat673.First().x - 2, pat673.First().y);
            //180 Rotate=========================================================================================================== 
            var pat673_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 3, d.y].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
                           select d;
            if (pat673_2.Count() > 0) return new Dot(pat673_2.First().x + 2, pat673_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat673_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 3].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
                             select d;
            if (pat673_2_3.Count() > 0) return new Dot(pat673_2_3.First().x, pat673_2_3.First().y + 2);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat673_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 3].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
                               select d;
            if (pat673_2_3_4.Count() > 0) return new Dot(pat673_2_3_4.First().x, pat673_2_3_4.First().y - 2);
            //============================================================================================================== 
            iNumberPattern = 389;
            var pat389 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == Owner
                         select d;
            if (pat389.Count() > 0) return new Dot(pat389.First().x - 1, pat389.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat389_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own == Owner
                           select d;
            if (pat389_2.Count() > 0) return new Dot(pat389_2.First().x + 1, pat389_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat389_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x, d.y - 1].Own == Owner
                             select d;
            if (pat389_2_3.Count() > 0) return new Dot(pat389_2_3.First().x - 1, pat389_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat389_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x, d.y + 1].Own == Owner
                               select d;
            if (pat389_2_3_4.Count() > 0) return new Dot(pat389_2_3_4.First().x + 1, pat389_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 395;
            var pat395 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x, d.y - 2].Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y - 2].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
                         select d;
            if (pat395.Count() > 0) return new Dot(pat395.First().x + 1, pat395.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat395_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y + 2].Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y + 2].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
                           select d;
            if (pat395_2.Count() > 0) return new Dot(pat395_2.First().x - 1, pat395_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat395_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 2, d.y].Own == Owner
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x + 2, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
                             select d;
            if (pat395_2_3.Count() > 0) return new Dot(pat395_2_3.First().x - 1, pat395_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat395_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 2, d.y].Own == Owner
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x - 2, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
                               select d;
            if (pat395_2_3_4.Count() > 0) return new Dot(pat395_2_3_4.First().x + 1, pat395_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 933;
            var pat933 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 2, d.y].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y - 1].Own != enemy_own
                         select d;
            if (pat933.Count() > 0) return new Dot(pat933.First().x, pat933.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat933_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 2, d.y].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y + 1].Own != enemy_own
                           select d;
            if (pat933_2.Count() > 0) return new Dot(pat933_2.First().x, pat933_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat933_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x, d.y - 2].Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own != enemy_own
                             select d;
            if (pat933_2_3.Count() > 0) return new Dot(pat933_2_3.First().x - 1, pat933_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat933_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x, d.y + 2].Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own != enemy_own
                               select d;
            if (pat933_2_3_4.Count() > 0) return new Dot(pat933_2_3_4.First().x + 1, pat933_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 517;
            var pat517 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x, d.y - 1].Own == Owner & aDots[d.x , d.y - 1].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y ].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y ].Blocked == false
& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat517.Count() > 0) return new Dot(pat517.First().x + 1, pat517.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat517_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x, d.y + 1].Own == Owner & aDots[d.x , d.y + 1].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x , d.y - 1].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y ].Blocked == false
& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
& aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat517_2.Count() > 0) return new Dot(pat517_2.First().x - 1, pat517_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat517_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y ].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x , d.y - 1].Blocked == false
& aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y ].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x , d.y + 1].Blocked == false
& aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
& aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat517_2_3.Count() > 0) return new Dot(pat517_2_3.First().x - 1, pat517_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat517_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y ].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y+1].Blocked == false
& aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
& aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x+2, d.y + 1].Blocked == false
& aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y +2].Blocked == false
                               select d;
            if (pat517_2_3_4.Count() > 0) return new Dot(pat517_2_3_4.First().x + 1, pat517_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 735;
            var pat735 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
                         select d;
            if (pat735.Count() > 0) return new Dot(pat735.First().x + 1, pat735.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat735_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
                           select d;
            if (pat735_2.Count() > 0) return new Dot(pat735_2.First().x - 1, pat735_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat735_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 1, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 1, d.y - 2].Own == 0
                             select d;
            if (pat735_2_3.Count() > 0) return new Dot(pat735_2_3.First().x + 1, pat735_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat735_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 1, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 1, d.y + 2].Own == 0
                               select d;
            if (pat735_2_3_4.Count() > 0) return new Dot(pat735_2_3_4.First().x - 1, pat735_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 699;
            var pat699 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked==false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
& aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat699.Count() > 0) return new Dot(pat699.First().x, pat699.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat699_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
& aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat699_2.Count() > 0) return new Dot(pat699_2.First().x, pat699_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat699_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
& aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat699_2_3.Count() > 0) return new Dot(pat699_2_3.First().x - 1, pat699_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat699_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
& aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat699_2_3_4.Count() > 0) return new Dot(pat699_2_3_4.First().x + 1, pat699_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 296;
            var pat296 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x - 2, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 2].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                         select d;
            if (pat296.Count() > 0) return new Dot(pat296.First().x - 1, pat296.First().y);
            //180 Rotate=========================================================================================================== 
            var pat296_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                               & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                           select d;
            if (pat296_2.Count() > 0) return new Dot(pat296_2.First().x + 1, pat296_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat296_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 2, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 2].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             select d;
            if (pat296_2_3.Count() > 0) return new Dot(pat296_2_3.First().x, pat296_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat296_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                               select d;
            if (pat296_2_3_4.Count() > 0) return new Dot(pat296_2_3_4.First().x, pat296_2_3_4.First().y - 1);
            //============================================================================================================== 

//            iNumberPattern = 86;
//            var pat86 = from Dot d in get_non_blocked
//                        where d.Own == Owner
//& aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
//& aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
//& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false
//                        select d;
//            if (pat86.Count() > 0) return new Dot(pat86.First().x, pat86.First().y + 1);
//            //180 Rotate=========================================================================================================== 
//            var pat86_2 = from Dot d in get_non_blocked
//                          where d.Own == Owner
//& aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
//& aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
//& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x , d.y - 1].Blocked == false
//                          select d;
//            if (pat86_2.Count() > 0) return new Dot(pat86_2.First().x, pat86_2.First().y - 1);
//            //--------------Rotate on 90----------------------------------- 
//            var pat86_2_3 = from Dot d in get_non_blocked
//                            where d.Own == Owner
//& aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
//& aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
//& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y ].Blocked == false
//                            select d;
//            if (pat86_2_3.Count() > 0) return new Dot(pat86_2_3.First().x - 1, pat86_2_3.First().y);
//            //--------------Rotate on 90 - 2----------------------------------- 
//            var pat86_2_3_4 = from Dot d in get_non_blocked
//                              where d.Own == Owner
//& aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
//& aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
//& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
//                              select d;
//            if (pat86_2_3_4.Count() > 0) return new Dot(pat86_2_3_4.First().x + 1, pat86_2_3_4.First().y);
////            //============================================================================================================== 
//            iNumberPattern = 168;
//            var pat168 = from Dot d in get_non_blocked
//                         where d.Own == Owner
//& aDots[d.x, d.y - 1].Own == 0 & aDots[d.x , d.y - 1].Blocked == false
//& aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y -2].Blocked == false
//& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
//                         select d;
//            if (pat168.Count() > 0) return new Dot(pat168.First().x, pat168.First().y - 1);
//            //180 Rotate=========================================================================================================== 
//            var pat168_2 = from Dot d in get_non_blocked
//                           where d.Own == Owner
//& aDots[d.x, d.y + 1].Own == 0 & aDots[d.x , d.y + 1].Blocked == false
//& aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
//& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
//                           select d;
//            if (pat168_2.Count() > 0) return new Dot(pat168_2.First().x, pat168_2.First().y + 1);
//            //--------------Rotate on 90----------------------------------- 
//            var pat168_2_3 = from Dot d in get_non_blocked
//                             where d.Own == Owner
//& aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y ].Blocked == false
//& aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
//& aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
//                             select d;
//            if (pat168_2_3.Count() > 0) return new Dot(pat168_2_3.First().x + 1, pat168_2_3.First().y);
//            //--------------Rotate on 90 - 2----------------------------------- 
//            var pat168_2_3_4 = from Dot d in get_non_blocked
//                               where d.Own == Owner
//& aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
//& aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
//& aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
//                               select d;
//            if (pat168_2_3_4.Count() > 0) return new Dot(pat168_2_3_4.First().x - 1, pat168_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 22;
            var pat22 = from Dot d in get_non_blocked
                        where d.Own == Owner
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
                        select d;
            if (pat22.Count() > 0) return new Dot(pat22.First().x + 2, pat22.First().y);
            //180 Rotate=========================================================================================================== 
            var pat22_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
& aDots[d.x, d.y + 2].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == enemy_own
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
                          select d;
            if (pat22_2.Count() > 0) return new Dot(pat22_2.First().x - 2, pat22_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat22_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x, d.y - 1].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x - 1, d.y - 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
                            select d;
            if (pat22_2_3.Count() > 0) return new Dot(pat22_2_3.First().x, pat22_2_3.First().y - 2);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat22_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
& aDots[d.x - 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 1].Own == 0
& aDots[d.x - 1, d.y].Own == 0
& aDots[d.x, d.y + 1].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x + 1, d.y + 1].Own == 0
& aDots[d.x + 1, d.y].Own == 0
& aDots[d.x + 2, d.y].Own == enemy_own
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
                              select d;
            if (pat22_2_3_4.Count() > 0) return new Dot(pat22_2_3_4.First().x, pat22_2_3_4.First().y + 2);
            //============================================================================================================== 
            iNumberPattern = 597;
            var pat597 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 3].Own == Owner & aDots[d.x, d.y + 3].Blocked == false
                             & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 3].Own == 0 & aDots[d.x + 1, d.y + 3].Blocked == false
                             & aDots[d.x - 1, d.y + 3].Own == 0 & aDots[d.x - 1, d.y + 3].Blocked == false
                             & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 2, d.y + 1].Own != enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat597.Count() > 0) return new Dot(pat597.First().x + 1, pat597.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat597_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 3].Own == Owner & aDots[d.x, d.y - 3].Blocked == false
                               & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 3].Own == 0 & aDots[d.x - 1, d.y - 3].Blocked == false
                               & aDots[d.x + 1, d.y - 3].Own == 0 & aDots[d.x + 1, d.y - 3].Blocked == false
                               & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 2, d.y - 1].Own != enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat597_2.Count() > 0) return new Dot(pat597_2.First().x - 1, pat597_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat597_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                 & aDots[d.x - 3, d.y].Own == Owner & aDots[d.x - 3, d.y].Blocked == false
                                 & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 3, d.y - 1].Own == 0 & aDots[d.x - 3, d.y - 1].Blocked == false
                                 & aDots[d.x - 3, d.y + 1].Own == 0 & aDots[d.x - 3, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 2].Own != enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat597_2_3.Count() > 0) return new Dot(pat597_2_3.First().x - 1, pat597_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat597_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x + 3, d.y].Own == Owner & aDots[d.x + 3, d.y].Blocked == false
                                   & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 3, d.y + 1].Own == 0 & aDots[d.x + 3, d.y + 1].Blocked == false
                                   & aDots[d.x + 3, d.y - 1].Own == 0 & aDots[d.x + 3, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 2].Own != enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat597_2_3_4.Count() > 0) return new Dot(pat597_2_3_4.First().x + 1, pat597_2_3_4.First().y + 1);
            //============================================================================================================== 
            //============================================================================================================== 

            iNumberPattern = 938;
            var pat938 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y].Own == Owner
                             & aDots[d.x + 1, d.y].Own == 0
                             & aDots[d.x + 2, d.y].Own == enemy_own
                             & aDots[d.x + 2, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y + 1].Own == 0
                             & aDots[d.x, d.y + 1].Own == 0
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat938.Count() > 0) return new Dot(pat938.First().x + 1, pat938.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat938_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y].Own == Owner
                               & aDots[d.x - 1, d.y].Own == 0
                               & aDots[d.x - 2, d.y].Own == enemy_own
                               & aDots[d.x - 2, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y - 1].Own == 0
                               & aDots[d.x, d.y - 1].Own == 0
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own
                           select d;
            if (pat938_2.Count() > 0) return new Dot(pat938_2.First().x - 1, pat938_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat938_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 1].Own == Owner
                                 & aDots[d.x, d.y - 1].Own == 0
                                 & aDots[d.x, d.y - 2].Own == enemy_own
                                 & aDots[d.x - 1, d.y - 2].Own == 0
                                 & aDots[d.x - 1, d.y - 1].Own == 0
                                 & aDots[d.x - 1, d.y].Own == 0
                                 & aDots[d.x - 1, d.y + 1].Own == enemy_own
                             select d;

            if (pat938_2_3.Count() > 0) return new Dot(pat938_2_3.First().x - 1, pat938_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat938_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 1].Own == Owner
                                   & aDots[d.x, d.y + 1].Own == 0
                                   & aDots[d.x, d.y + 2].Own == enemy_own
                                   & aDots[d.x + 1, d.y + 2].Own == 0
                                   & aDots[d.x + 1, d.y + 1].Own == 0
                                   & aDots[d.x + 1, d.y].Own == 0
                                   & aDots[d.x + 1, d.y - 1].Own == enemy_own
                               select d;
            if (pat938_2_3_4.Count() > 0) return new Dot(pat938_2_3_4.First().x + 1, pat938_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 44;
            var pat44 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                            & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                            & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                            & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                            & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
                            & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                            & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                            & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                        select d;
            if (pat44.Count() > 0) return new Dot(pat44.First().x + 1, pat44.First().y);
            //180 Rotate=========================================================================================================== 
            var pat44_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                              & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                              & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                              & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                              & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                              & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                              & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                              & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                              & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                              & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                          select d;
            if (pat44_2.Count() > 0) return new Dot(pat44_2.First().x - 1, pat44_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat44_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                                & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                                & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                                & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                            select d;
            if (pat44_2_3.Count() > 0) return new Dot(pat44_2_3.First().x, pat44_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat44_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                  & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                  & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                  & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                  & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                                  & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                  & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                                  & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                                  & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                  & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                              select d;
            if (pat44_2_3_4.Count() > 0) return new Dot(pat44_2_3_4.First().x, pat44_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 134;
            var pat134 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                             & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                             & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                             & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                         select d;
            if (pat134.Count() > 0) return new Dot(pat134.First().x, pat134.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat134_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                               & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                               & aDots[d.x - 2, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 2].Blocked == false
                               & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                           select d;
            if (pat134_2.Count() > 0) return new Dot(pat134_2.First().x, pat134_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat134_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y - 2].Own == 0 & aDots[d.x + 2, d.y - 2].Blocked == false
                                 & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                             select d;
            if (pat134_2_3.Count() > 0) return new Dot(pat134_2_3.First().x + 1, pat134_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat134_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                   & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y + 2].Own == 0 & aDots[d.x - 2, d.y + 2].Blocked == false
                                   & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                               select d;
            if (pat134_2_3_4.Count() > 0) return new Dot(pat134_2_3_4.First().x - 1, pat134_2_3_4.First().y);
            //============================================================================================================== 

            iNumberPattern = 671;
            var pat671 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat671.Count() > 0) return new Dot(pat671.First().x - 1, pat671.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat671_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat671_2.Count() > 0) return new Dot(pat671_2.First().x + 1, pat671_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat671_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat671_2_3.Count() > 0) return new Dot(pat671_2_3.First().x - 1, pat671_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat671_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat671_2_3_4.Count() > 0) return new Dot(pat671_2_3_4.First().x + 1, pat671_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 726;
            var pat726 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 2, d.y - 1].Own == enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                         select d;
            if (pat726.Count() > 0) return new Dot(pat726.First().x + 1, pat726.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat726_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 2, d.y + 1].Own == enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                           select d;
            if (pat726_2.Count() > 0) return new Dot(pat726_2.First().x - 1, pat726_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat726_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                             select d;
            if (pat726_2_3.Count() > 0) return new Dot(pat726_2_3.First().x + 1, pat726_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat726_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                   & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                               select d;
            if (pat726_2_3_4.Count() > 0) return new Dot(pat726_2_3_4.First().x - 1, pat726_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 793;
            var pat793 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat793.Count() > 0) return new Dot(pat793.First().x, pat793.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat793_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                               & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                           select d;
            if (pat793_2.Count() > 0) return new Dot(pat793_2.First().x, pat793_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat793_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat793_2_3.Count() > 0) return new Dot(pat793_2_3.First().x - 1, pat793_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat793_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                               select d;
            if (pat793_2_3_4.Count() > 0) return new Dot(pat793_2_3_4.First().x + 1, pat793_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 689;
            var pat689 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                         select d;
            if (pat689.Count() > 0) return new Dot(pat689.First().x + 1, pat689.First().y);
            //180 Rotate=========================================================================================================== 
            var pat689_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                           select d;
            if (pat689_2.Count() > 0) return new Dot(pat689_2.First().x - 1, pat689_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat689_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             select d;
            if (pat689_2_3.Count() > 0) return new Dot(pat689_2_3.First().x, pat689_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat689_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                               select d;
            if (pat689_2_3_4.Count() > 0) return new Dot(pat689_2_3_4.First().x, pat689_2_3_4.First().y + 1);
            //============================================================================================================== 

            iNumberPattern = 827;
            var pat827 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat827.Count() > 0) return new Dot(pat827.First().x, pat827.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat827_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                           select d;
            if (pat827_2.Count() > 0) return new Dot(pat827_2.First().x, pat827_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat827_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                             select d;
            if (pat827_2_3.Count() > 0) return new Dot(pat827_2_3.First().x + 1, pat827_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat827_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                               select d;
            if (pat827_2_3_4.Count() > 0) return new Dot(pat827_2_3_4.First().x - 1, pat827_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 974;
            var pat974 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                             & aDots[d.x, d.y - 3].Own == Owner & aDots[d.x, d.y - 3].Blocked == false
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                         select d;
            if (pat974.Count() > 0) return new Dot(pat974.First().x + 1, pat974.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat974_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                               & aDots[d.x, d.y + 3].Own == Owner & aDots[d.x, d.y + 3].Blocked == false
                               & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                           select d;
            if (pat974_2.Count() > 0) return new Dot(pat974_2.First().x - 1, pat974_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat974_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                 & aDots[d.x + 3, d.y].Own == Owner & aDots[d.x + 3, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             select d;
            if (pat974_2_3.Count() > 0) return new Dot(pat974_2_3.First().x + 1, pat974_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat974_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                   & aDots[d.x - 3, d.y].Own == Owner & aDots[d.x - 3, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                               select d;
            if (pat974_2_3_4.Count() > 0) return new Dot(pat974_2_3_4.First().x - 1, pat974_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 242;
            var pat242 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 2, d.y].Own != enemy_own & aDots[d.x + 2, d.y].Blocked == false
                             & aDots[d.x + 2, d.y + 1].Own != enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat242.Count() > 0) return new Dot(pat242.First().x + 1, pat242.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat242_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 2, d.y].Own != enemy_own & aDots[d.x - 2, d.y].Blocked == false
                               & aDots[d.x - 2, d.y - 1].Own != enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat242_2.Count() > 0) return new Dot(pat242_2.First().x - 1, pat242_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat242_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 2, d.y - 1].Own == enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 2].Own != enemy_own & aDots[d.x, d.y - 2].Blocked == false
                                 & aDots[d.x - 1, d.y - 2].Own != enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat242_2_3.Count() > 0) return new Dot(pat242_2_3.First().x - 1, pat242_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat242_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 2, d.y + 1].Own == enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 2].Own != enemy_own & aDots[d.x, d.y + 2].Blocked == false
                                   & aDots[d.x + 1, d.y + 2].Own != enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat242_2_3_4.Count() > 0) return new Dot(pat242_2_3_4.First().x + 1, pat242_2_3_4.First().y + 1);
            //============================================================================================================== 










            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль
        }
        private Dot CheckPattern_vilochka(int Owner)
        {
            int enemy_own = Owner == 1 ? 2 : 1;
            var get_non_blocked = from Dot d in aDots where d.Blocked == false select d; //получить коллекцию незаблокированных точек
            //паттерн на диагональное расположение точек         *d   
            //                                                *  +  
            //                                             *  +  *               
            iNumberPattern = 1;
            var pat1 = from Dot d in get_non_blocked
                       where d.Own == Owner
                           & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                           & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                           & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                           & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                           & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                           & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                           & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                           //& aDots[d.x - 2, d.y + 1].Own != enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                           //& aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                           //& aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                           //& aDots[d.x + 1, d.y - 2].Own != enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                           & aDots[d.x + 2, d.y + 1].Own != enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                           & aDots[d.x + 1, d.y + 2].Own != enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                       select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x + 1, pat1.First().y + 1);
            //180 Rotate=========================================================================================================== 
            //паттерн на диагональное расположение точек    *  +  *d   
            //                                              +  *    
            //                                              *       
            var pat1_2 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             //& aDots[d.x + 2, d.y - 1].Own != enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
                             //& aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             //& aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             //& aDots[d.x - 1, d.y + 2].Own != enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x - 2, d.y - 1].Own != enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 2].Own != enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                         select d;
            if (pat1_2.Count() > 0) return new Dot(pat1_2.First().x - 1, pat1_2.First().y - 1);
            //============================================================================================================== 
            //паттерн на диагональное расположение точек    *                   
            //                                              +  d    
            //                                              m  +  * 
            var pat1_3 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                            & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                            & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                            & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                            & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                            & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                            & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                            //& aDots[d.x + 2, d.y + 1].Own != enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                            //& aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                            //& aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                            //& aDots[d.x - 1, d.y - 2].Own != enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                            & aDots[d.x - 1, d.y + 2].Own != enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                            & aDots[d.x - 2, d.y + 1].Own != enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                        select d;
            if (pat1_3.Count() > 0) return new Dot(pat1_3.First().x - 1, pat1_3.First().y + 1);
            //180 Rotate=========================================================================================================== 
            //паттерн на диагональное расположение точек    *  +  m   
            //                                                 d  +  
            //                                                    * 
            var pat1_4 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                              & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                              & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                              & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                              & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                              & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                              & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                              //& aDots[d.x - 2, d.y - 1].Own != enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                              //& aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                              //& aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                              //& aDots[d.x + 1, d.y + 2].Own != enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                              & aDots[d.x + 1, d.y - 2].Own != enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                              & aDots[d.x + 2, d.y - 1].Own != enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
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
                             & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == Owner & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                             & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                         select d;
            if (pat938.Count() > 0) return new Dot(pat938.First().x + 1, pat938.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat938_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                               & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == Owner & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                               & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                           select d;
            if (pat938_2.Count() > 0) return new Dot(pat938_2.First().x - 1, pat938_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat938_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == Owner & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                             select d;
            if (pat938_2_3.Count() > 0) return new Dot(pat938_2_3.First().x - 1, pat938_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat938_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == Owner & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                               select d;
            if (pat938_2_3_4.Count() > 0) return new Dot(pat938_2_3_4.First().x + 1, pat938_2_3_4.First().y + 1);
            //============================================================================================================== 

            //iNumberPattern = 364;
            //var pat364 = from Dot d in get_non_blocked
            //             where d.Own == Owner
            //                 & aDots[d.x - 1, d.y - 1].Own == Owner
            //                 & aDots[d.x + 1, d.y + 1].Own == Owner
            //                 & aDots[d.x, d.y - 1].Own == enemy_own
            //                 & aDots[d.x + 1, d.y].Own == enemy_own
            //                 & aDots[d.x + 1, d.y - 1].Own == 0
            //                 & aDots[d.x, d.y - 2].Own != enemy_own
            //                 & aDots[d.x + 1, d.y - 2].Own != enemy_own
            //                 & aDots[d.x + 2, d.y - 1].Own != enemy_own
            //                 & aDots[d.x + 2, d.y].Own != enemy_own
            //             select d;
            //if (pat364.Count() > 0) return new Dot(pat364.First().x + 1, pat364.First().y - 1);
            ////180 Rotate=========================================================================================================== 
            //var pat364_2 = from Dot d in get_non_blocked
            //               where d.Own == Owner
            //                   & aDots[d.x + 1, d.y + 1].Own == Owner
            //                   & aDots[d.x - 1, d.y - 1].Own == Owner
            //                   & aDots[d.x, d.y + 1].Own == enemy_own
            //                   & aDots[d.x - 1, d.y].Own == enemy_own
            //                   & aDots[d.x - 1, d.y + 1].Own == 0
            //                   & aDots[d.x, d.y + 2].Own != enemy_own
            //                   & aDots[d.x - 1, d.y + 2].Own != enemy_own
            //                   & aDots[d.x - 2, d.y + 1].Own != enemy_own
            //                   & aDots[d.x - 2, d.y].Own != enemy_own
            //               select d;
            //if (pat364_2.Count() > 0) return new Dot(pat364_2.First().x - 1, pat364_2.First().y + 1);
            ////--------------Rotate on 90----------------------------------- 
            //var pat364_2_3 = from Dot d in get_non_blocked
            //                 where d.Own == Owner
            //                     & aDots[d.x + 1, d.y + 1].Own == Owner
            //                     & aDots[d.x - 1, d.y - 1].Own == Owner
            //                     & aDots[d.x + 1, d.y].Own == enemy_own
            //                     & aDots[d.x, d.y - 1].Own == enemy_own
            //                     & aDots[d.x + 1, d.y - 1].Own == 0
            //                     & aDots[d.x + 2, d.y].Own != enemy_own
            //                     & aDots[d.x + 2, d.y - 1].Own != enemy_own
            //                     & aDots[d.x + 1, d.y - 2].Own != enemy_own
            //                     & aDots[d.x, d.y - 2].Own != enemy_own
            //                 select d;
            //if (pat364_2_3.Count() > 0) return new Dot(pat364_2_3.First().x + 1, pat364_2_3.First().y - 1);
            ////--------------Rotate on 90 - 2----------------------------------- 
            //var pat364_2_3_4 = from Dot d in get_non_blocked
            //                   where d.Own == Owner
            //                       & aDots[d.x - 1, d.y - 1].Own == Owner
            //                       & aDots[d.x + 1, d.y + 1].Own == Owner
            //                       & aDots[d.x - 1, d.y].Own == enemy_own
            //                       & aDots[d.x, d.y + 1].Own == enemy_own
            //                       & aDots[d.x - 1, d.y + 1].Own == 0
            //                       & aDots[d.x - 2, d.y].Own != enemy_own
            //                       & aDots[d.x - 2, d.y + 1].Own != enemy_own
            //                       & aDots[d.x - 1, d.y + 2].Own != enemy_own
            //                       & aDots[d.x, d.y + 2].Own != enemy_own
            //                   select d;
            //if (pat364_2_3_4.Count() > 0) return new Dot(pat364_2_3_4.First().x - 1, pat364_2_3_4.First().y + 1);
            ////============================================================================================================== 

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
            iNumberPattern = 30;
            var pat30 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & aDots[d.x - 1, d.y + 1].Own == Owner
                            & aDots[d.x - 1, d.y].Own == enemy_own
                            & aDots[d.x, d.y - 1].Own == enemy_own
                            & aDots[d.x, d.y - 2].Own == Owner
                            & aDots[d.x + 1, d.y - 1].Own == 0
                            & aDots[d.x - 1, d.y - 1].Own == 0
                            & aDots[d.x - 1, d.y - 2].Own == 0
                            & aDots[d.x - 2, d.y - 1].Own == 0
                            & aDots[d.x - 2, d.y].Own == 0
                        select d;
            if (pat30.Count() > 0) return new Dot(pat30.First().x - 1, pat30.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat30_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x + 1, d.y - 1].Own == Owner
                              & aDots[d.x + 1, d.y].Own == enemy_own
                              & aDots[d.x, d.y + 1].Own == enemy_own
                              & aDots[d.x, d.y + 2].Own == Owner
                              & aDots[d.x - 1, d.y + 1].Own == 0
                              & aDots[d.x + 1, d.y + 1].Own == 0
                              & aDots[d.x + 1, d.y + 2].Own == 0
                              & aDots[d.x + 2, d.y + 1].Own == 0
                              & aDots[d.x + 2, d.y].Own == 0
                          select d;
            if (pat30_2.Count() > 0) return new Dot(pat30_2.First().x + 1, pat30_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat30_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & aDots[d.x - 1, d.y + 1].Own == Owner
                                & aDots[d.x, d.y + 1].Own == enemy_own
                                & aDots[d.x + 1, d.y].Own == enemy_own
                                & aDots[d.x + 2, d.y].Own == Owner
                                & aDots[d.x + 1, d.y - 1].Own == 0
                                & aDots[d.x + 1, d.y + 1].Own == 0
                                & aDots[d.x + 2, d.y + 1].Own == 0
                                & aDots[d.x + 1, d.y + 2].Own == 0
                                & aDots[d.x, d.y + 2].Own == 0
                            select d;
            if (pat30_2_3.Count() > 0) return new Dot(pat30_2_3.First().x + 1, pat30_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat30_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & aDots[d.x + 1, d.y - 1].Own == Owner
                                  & aDots[d.x, d.y - 1].Own == enemy_own
                                  & aDots[d.x - 1, d.y].Own == enemy_own
                                  & aDots[d.x - 2, d.y].Own == Owner
                                  & aDots[d.x - 1, d.y + 1].Own == 0
                                  & aDots[d.x - 1, d.y - 1].Own == 0
                                  & aDots[d.x - 2, d.y - 1].Own == 0
                                  & aDots[d.x - 1, d.y - 2].Own == 0
                                  & aDots[d.x, d.y - 2].Own == 0
                              select d;
            if (pat30_2_3_4.Count() > 0) return new Dot(pat30_2_3_4.First().x - 1, pat30_2_3_4.First().y - 1);
            //============================================================================================================== 
            iNumberPattern = 295;
            var pat295 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y].Own == enemy_own
                             & aDots[d.x, d.y + 1].Own == enemy_own
                             & aDots[d.x, d.y + 2].Own == enemy_own
                             & aDots[d.x + 1, d.y + 3].Own == enemy_own
                             & aDots[d.x + 2, d.y + 2].Own == enemy_own
                             & aDots[d.x, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y - 1].Own == 0
                             & aDots[d.x + 1, d.y].Own == 0
                             & aDots[d.x + 2, d.y].Own == 0
                             & aDots[d.x + 2, d.y + 1].Own == 0
                             & aDots[d.x + 1, d.y + 1].Own != enemy_own
                             & aDots[d.x + 1, d.y + 2].Own != enemy_own
                         select d;
            if (pat295.Count() > 0) return new Dot(pat295.First().x + 1, pat295.First().y);
            //180 Rotate=========================================================================================================== 
            var pat295_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y].Own == enemy_own
                               & aDots[d.x, d.y - 1].Own == enemy_own
                               & aDots[d.x, d.y - 2].Own == enemy_own
                               & aDots[d.x - 1, d.y - 3].Own == enemy_own
                               & aDots[d.x - 2, d.y - 2].Own == enemy_own
                               & aDots[d.x, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y + 1].Own == 0
                               & aDots[d.x - 1, d.y].Own == 0
                               & aDots[d.x - 2, d.y].Own == 0
                               & aDots[d.x - 2, d.y - 1].Own == 0
                               & aDots[d.x - 1, d.y - 1].Own != enemy_own
                               & aDots[d.x - 1, d.y - 2].Own != enemy_own
                           select d;
            if (pat295_2.Count() > 0) return new Dot(pat295_2.First().x - 1, pat295_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat295_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 1].Own == enemy_own
                                 & aDots[d.x - 1, d.y].Own == enemy_own
                                 & aDots[d.x - 2, d.y].Own == enemy_own
                                 & aDots[d.x - 3, d.y - 1].Own == enemy_own
                                 & aDots[d.x - 2, d.y - 2].Own == enemy_own
                                 & aDots[d.x + 1, d.y].Own == 0
                                 & aDots[d.x + 1, d.y - 1].Own == 0
                                 & aDots[d.x, d.y - 1].Own == 0
                                 & aDots[d.x, d.y - 2].Own == 0
                                 & aDots[d.x - 1, d.y - 2].Own == 0
                                 & aDots[d.x - 1, d.y - 1].Own != enemy_own
                                 & aDots[d.x - 2, d.y - 1].Own != enemy_own
                             select d;
            if (pat295_2_3.Count() > 0) return new Dot(pat295_2_3.First().x, pat295_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat295_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 1].Own == enemy_own
                                   & aDots[d.x + 1, d.y].Own == enemy_own
                                   & aDots[d.x + 2, d.y].Own == enemy_own
                                   & aDots[d.x + 3, d.y + 1].Own == enemy_own
                                   & aDots[d.x + 2, d.y + 2].Own == enemy_own
                                   & aDots[d.x - 1, d.y].Own == 0
                                   & aDots[d.x - 1, d.y + 1].Own == 0
                                   & aDots[d.x, d.y + 1].Own == 0
                                   & aDots[d.x, d.y + 2].Own == 0
                                   & aDots[d.x + 1, d.y + 2].Own == 0
                                   & aDots[d.x + 1, d.y + 1].Own != enemy_own
                                   & aDots[d.x + 2, d.y + 1].Own != enemy_own
                               select d;
            if (pat295_2_3_4.Count() > 0) return new Dot(pat295_2_3_4.First().x, pat295_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 968;
            var pat968 = from Dot d in get_non_blocked
                         where d.Own == Owner
& aDots[d.x + 1, d.y - 2].Own == Owner
& aDots[d.x + 2, d.y - 1].Own == 0
& aDots[d.x + 2, d.y].Own == 0
& aDots[d.x + 2, d.y + 1].Own == 0
& aDots[d.x + 1, d.y + 2].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == 0
                         select d;
            if (pat968.Count() > 0) return new Dot(pat968.First().x + 1, pat968.First().y);
            //180 Rotate=========================================================================================================== 
            var pat968_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
& aDots[d.x - 1, d.y + 2].Own == Owner
& aDots[d.x - 2, d.y + 1].Own == 0
& aDots[d.x - 2, d.y].Own == 0
& aDots[d.x - 2, d.y - 1].Own == 0
& aDots[d.x - 1, d.y - 2].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == Owner
& aDots[d.x, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == 0
                           select d;
            if (pat968_2.Count() > 0) return new Dot(pat968_2.First().x - 1, pat968_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat968_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
& aDots[d.x + 2, d.y - 1].Own == Owner
& aDots[d.x + 1, d.y - 2].Own == 0
& aDots[d.x, d.y - 2].Own == 0
& aDots[d.x - 1, d.y - 2].Own == 0
& aDots[d.x - 2, d.y - 1].Own == Owner
& aDots[d.x - 1, d.y - 1].Own == enemy_own
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x + 1, d.y - 1].Own == enemy_own
& aDots[d.x, d.y - 1].Own == 0
                             select d;
            if (pat968_2_3.Count() > 0) return new Dot(pat968_2_3.First().x, pat968_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat968_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
& aDots[d.x - 2, d.y + 1].Own == Owner
& aDots[d.x - 1, d.y + 2].Own == 0
& aDots[d.x, d.y + 2].Own == 0
& aDots[d.x + 1, d.y + 2].Own == 0
& aDots[d.x + 2, d.y + 1].Own == Owner
& aDots[d.x + 1, d.y + 1].Own == enemy_own
& aDots[d.x + 1, d.y].Own == Owner
& aDots[d.x - 1, d.y].Own == Owner
& aDots[d.x - 1, d.y + 1].Own == enemy_own
& aDots[d.x, d.y + 1].Own == 0
                               select d;
            if (pat968_2_3_4.Count() > 0) return new Dot(pat968_2_3_4.First().x, pat968_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 880;
            var pat880 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y + 2].Own == enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                             & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x - 2, d.y + 1].Own == enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own == enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own != enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                         select d;
            if (pat880.Count() > 0) return new Dot(pat880.First().x + 1, pat880.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat880_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x - 1, d.y - 2].Own == enemy_own & aDots[d.x - 1, d.y - 2].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                               & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                               & aDots[d.x + 2, d.y - 1].Own == enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own == enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                               & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own != enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                           select d;
            if (pat880_2.Count() > 0) return new Dot(pat880_2.First().x - 1, pat880_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat880_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 2, d.y - 1].Own == enemy_own & aDots[d.x - 2, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == enemy_own & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                                 & aDots[d.x - 1, d.y + 2].Own == enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own == enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own != enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                             select d;
            if (pat880_2_3.Count() > 0) return new Dot(pat880_2_3.First().x + 1, pat880_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat880_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 2, d.y + 1].Own == enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == enemy_own & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                                   & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
                                   & aDots[d.x + 1, d.y - 2].Own == enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own == enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own != enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                               select d;
            if (pat880_2_3_4.Count() > 0) return new Dot(pat880_2_3_4.First().x - 1, pat880_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 775;
            var pat775 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y].Own == Owner & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own != enemy_own & aDots[d.x + 1, d.y].Blocked == false
                         select d;
            if (pat775.Count() > 0) return new Dot(pat775.First().x - 1, pat775.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat775_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y].Own == Owner & aDots[d.x + 2, d.y].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                               & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own != enemy_own & aDots[d.x - 1, d.y].Blocked == false
                           select d;
            if (pat775_2.Count() > 0) return new Dot(pat775_2.First().x + 1, pat775_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat775_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 2].Own == Owner & aDots[d.x, d.y + 2].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x, d.y - 1].Own != enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             select d;
            if (pat775_2_3.Count() > 0) return new Dot(pat775_2_3.First().x - 1, pat775_2_3.First().y + 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat775_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 2].Own == Owner & aDots[d.x, d.y - 2].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x, d.y + 1].Own != enemy_own & aDots[d.x, d.y + 1].Blocked == false
                               select d;
            if (pat775_2_3_4.Count() > 0) return new Dot(pat775_2_3_4.First().x + 1, pat775_2_3_4.First().y - 1);
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
