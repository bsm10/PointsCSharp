using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotsGame
{
    public partial class Game
    {
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
                           & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                           & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                           & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                           & aDots[d.x + 2, d.y + 1].Own != enemy_own & aDots[d.x + 2, d.y + 1].Blocked == false
                           & aDots[d.x + 1, d.y + 2].Own != enemy_own & aDots[d.x + 1, d.y + 2].Blocked == false
                       select d;
            if (pat1.Count() > 0) return new Dot(pat1.First().x + 1, pat1.First().y + 1);
            //паттерн на диагональное расположение точек    *  +  *d   
            //                                              +  *    
            //                                              *       

            var pat1_2 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x + 1, d.y - 1].Own == Owner & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == Owner & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
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
            iNumberPattern = 685;
            var pat685 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 2, d.y + 3].Own == Owner & aDots[d.x - 2, d.y + 3].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 3].Own == Owner & aDots[d.x, d.y + 3].Blocked == false
                             & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                             & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x - 3, d.y + 2].Own == 0 & aDots[d.x - 3, d.y + 2].Blocked == false
                             & aDots[d.x - 3, d.y + 1].Own == 0 & aDots[d.x - 3, d.y + 1].Blocked == false
                             & aDots[d.x - 2, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y + 2].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                         select d;
            if (pat685.Count() > 0) return new Dot(pat685.First().x - 2, pat685.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat685_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 2, d.y - 3].Own == Owner & aDots[d.x + 2, d.y - 3].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 3].Own == Owner & aDots[d.x, d.y - 3].Blocked == false
                               & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                               & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x + 3, d.y - 2].Own == 0 & aDots[d.x + 3, d.y - 2].Blocked == false
                               & aDots[d.x + 3, d.y - 1].Own == 0 & aDots[d.x + 3, d.y - 1].Blocked == false
                               & aDots[d.x + 2, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y - 2].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                           select d;
            if (pat685_2.Count() > 0) return new Dot(pat685_2.First().x + 2, pat685_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat685_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x - 3, d.y + 2].Own == Owner & aDots[d.x - 3, d.y + 2].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                                 & aDots[d.x - 3, d.y].Own == Owner & aDots[d.x - 3, d.y].Blocked == false
                                 & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                 & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x - 2, d.y + 3].Own == 0 & aDots[d.x - 2, d.y + 3].Blocked == false
                                 & aDots[d.x - 1, d.y + 3].Own == 0 & aDots[d.x - 1, d.y + 3].Blocked == false
                                 & aDots[d.x - 2, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y + 2].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own == enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             select d;
            if (pat685_2_3.Count() > 0) return new Dot(pat685_2_3.First().x - 1, pat685_2_3.First().y + 2);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat685_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x + 3, d.y - 2].Own == Owner & aDots[d.x + 3, d.y - 2].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                                   & aDots[d.x + 3, d.y].Own == Owner & aDots[d.x + 3, d.y].Blocked == false
                                   & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                   & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x + 2, d.y - 3].Own == 0 & aDots[d.x + 2, d.y - 3].Blocked == false
                                   & aDots[d.x + 1, d.y - 3].Own == 0 & aDots[d.x + 1, d.y - 3].Blocked == false
                                   & aDots[d.x + 2, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y - 2].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own == enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               select d;
            if (pat685_2_3_4.Count() > 0) return new Dot(pat685_2_3_4.First().x + 1, pat685_2_3_4.First().y - 2);
            //============================================================================================================== 
            iNumberPattern = 632;
            var pat632 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x - 2, d.y + 1].Own == enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 1].Own != enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                             & aDots[d.x - 2, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y + 2].Blocked == false
                             & aDots[d.x - 1, d.y + 3].Own == enemy_own & aDots[d.x - 1, d.y + 3].Blocked == false
                             & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x - 1, d.y + 2].Own != enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                         select d;
            if (pat632.Count() > 0) return new Dot(pat632.First().x, pat632.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat632_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x + 2, d.y - 1].Own == enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 1].Own != enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                               & aDots[d.x + 2, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y - 2].Blocked == false
                               & aDots[d.x + 1, d.y - 3].Own == enemy_own & aDots[d.x + 1, d.y - 3].Blocked == false
                               & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x + 1, d.y - 2].Own != enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                           select d;
            if (pat632_2.Count() > 0) return new Dot(pat632_2.First().x, pat632_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat632_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x - 1, d.y + 2].Own == enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                                 & aDots[d.x - 1, d.y + 1].Own != enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                 & aDots[d.x - 2, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y + 2].Blocked == false
                                 & aDots[d.x - 3, d.y + 1].Own == enemy_own & aDots[d.x - 3, d.y + 1].Blocked == false
                                 & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 2, d.y + 1].Own != enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                             select d;
            if (pat632_2_3.Count() > 0) return new Dot(pat632_2_3.First().x - 1, pat632_2_3.First().y);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat632_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x + 1, d.y - 2].Own == enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                                   & aDots[d.x + 1, d.y - 1].Own != enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                   & aDots[d.x + 2, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y - 2].Blocked == false
                                   & aDots[d.x + 3, d.y - 1].Own == enemy_own & aDots[d.x + 3, d.y - 1].Blocked == false
                                   & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 2, d.y - 1].Own != enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
                               select d;
            if (pat632_2_3_4.Count() > 0) return new Dot(pat632_2_3_4.First().x + 1, pat632_2_3_4.First().y);
            //============================================================================================================== 
            iNumberPattern = 1938;
            var pat1938 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x, d.y - 3].Own == Owner & aDots[d.x, d.y - 3].Blocked == false
                              & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                              & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                              & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                              & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                              & aDots[d.x, d.y - 2].Own != Owner & aDots[d.x, d.y - 2].Blocked == false
                              & aDots[d.x, d.y - 1].Own != Owner & aDots[d.x, d.y - 1].Blocked == false
                              & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                              & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                              & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                              & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                              & aDots[d.x + 1, d.y - 3].Own == 0 & aDots[d.x + 1, d.y - 3].Blocked == false
                              & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                          select d;
            if (pat1938.Count() > 0) return new Dot(pat1938.First().x + 1, pat1938.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat1938_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & aDots[d.x, d.y + 3].Own == Owner & aDots[d.x, d.y + 3].Blocked == false
                                & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                                & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                & aDots[d.x, d.y + 2].Own != Owner & aDots[d.x, d.y + 2].Blocked == false
                                & aDots[d.x, d.y + 1].Own != Owner & aDots[d.x, d.y + 1].Blocked == false
                                & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                                & aDots[d.x - 1, d.y + 3].Own == 0 & aDots[d.x - 1, d.y + 3].Blocked == false
                                & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                            select d;
            if (pat1938_2.Count() > 0) return new Dot(pat1938_2.First().x - 1, pat1938_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat1938_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & aDots[d.x + 3, d.y].Own == Owner & aDots[d.x + 3, d.y].Blocked == false
                                  & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                                  & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                  & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                  & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                  & aDots[d.x + 2, d.y].Own != Owner & aDots[d.x + 2, d.y].Blocked == false
                                  & aDots[d.x + 1, d.y].Own != Owner & aDots[d.x + 1, d.y].Blocked == false
                                  & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                  & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                  & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                  & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                                  & aDots[d.x + 3, d.y - 1].Own == 0 & aDots[d.x + 3, d.y - 1].Blocked == false
                                  & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                              select d;
            if (pat1938_2_3.Count() > 0) return new Dot(pat1938_2_3.First().x + 1, pat1938_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1938_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    & aDots[d.x - 3, d.y].Own == Owner & aDots[d.x - 3, d.y].Blocked == false
                                    & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                                    & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                    & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                    & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                    & aDots[d.x - 2, d.y].Own != Owner & aDots[d.x - 2, d.y].Blocked == false
                                    & aDots[d.x - 1, d.y].Own != Owner & aDots[d.x - 1, d.y].Blocked == false
                                    & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                    & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                    & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                    & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                                    & aDots[d.x - 3, d.y + 1].Own == 0 & aDots[d.x - 3, d.y + 1].Blocked == false
                                    & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                select d;
            if (pat1938_2_3_4.Count() > 0) return new Dot(pat1938_2_3_4.First().x - 1, pat1938_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 1775;
            var pat1775 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                              & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                              & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                              & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                              & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                              & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                              & aDots[d.x, d.y - 2].Own == Owner & aDots[d.x, d.y - 2].Blocked == false
                              & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                              & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                              & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                              & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                          select d;
            if (pat1775.Count() > 0) return new Dot(pat1775.First().x + 1, pat1775.First().y - 1);
            //180 Rotate=========================================================================================================== 
            var pat1775_2 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                & aDots[d.x, d.y + 2].Own == Owner & aDots[d.x, d.y + 2].Blocked == false
                                & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                                & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                                & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                            select d;
            if (pat1775_2.Count() > 0) return new Dot(pat1775_2.First().x - 1, pat1775_2.First().y + 1);
            //--------------Rotate on 90----------------------------------- 
            var pat1775_2_3 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & aDots[d.x - 1, d.y - 1].Own == Owner & aDots[d.x - 1, d.y - 1].Blocked == false
                                  & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                  & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                  & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                  & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                  & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                                  & aDots[d.x + 2, d.y].Own == Owner & aDots[d.x + 2, d.y].Blocked == false
                                  & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                                  & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                                  & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                  & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                              select d;
            if (pat1775_2_3.Count() > 0) return new Dot(pat1775_2_3.First().x + 1, pat1775_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat1775_2_3_4 = from Dot d in get_non_blocked
                                where d.Own == Owner
                                    & aDots[d.x + 1, d.y + 1].Own == Owner & aDots[d.x + 1, d.y + 1].Blocked == false
                                    & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                    & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                    & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                    & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                    & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                                    & aDots[d.x - 2, d.y].Own == Owner & aDots[d.x - 2, d.y].Blocked == false
                                    & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                                    & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                                    & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                    & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                select d;
            if (pat1775_2_3_4.Count() > 0) return new Dot(pat1775_2_3_4.First().x - 1, pat1775_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 274;
            var pat274 = from Dot d in get_non_blocked
                         where d.Own == Owner
                             & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                             & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                             & aDots[d.x, d.y - 2].Own == enemy_own & aDots[d.x, d.y - 2].Blocked == false
                             & aDots[d.x, d.y - 3].Own == enemy_own & aDots[d.x, d.y - 3].Blocked == false
                             & aDots[d.x + 1, d.y - 4].Own == enemy_own & aDots[d.x + 1, d.y - 4].Blocked == false
                             & aDots[d.x + 2, d.y - 3].Own == enemy_own & aDots[d.x + 2, d.y - 3].Blocked == false
                             & aDots[d.x + 2, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y - 2].Blocked == false
                             & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                             & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                             & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                             & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                             & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                             & aDots[d.x + 1, d.y - 1].Own != enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                             & aDots[d.x + 1, d.y - 2].Own != enemy_own & aDots[d.x + 1, d.y - 2].Blocked == false
                             & aDots[d.x + 1, d.y - 3].Own != enemy_own & aDots[d.x + 1, d.y - 3].Blocked == false
                         select d;
            if (pat274.Count() > 0) return new Dot(pat274.First().x + 1, pat274.First().y);
            //180 Rotate=========================================================================================================== 
            var pat274_2 = from Dot d in get_non_blocked
                           where d.Own == Owner
                               & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                               & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                               & aDots[d.x, d.y + 2].Own == enemy_own & aDots[d.x, d.y + 2].Blocked == false
                               & aDots[d.x, d.y + 3].Own == enemy_own & aDots[d.x, d.y + 3].Blocked == false
                               & aDots[d.x - 1, d.y + 4].Own == enemy_own & aDots[d.x - 1, d.y + 4].Blocked == false
                               & aDots[d.x - 2, d.y + 3].Own == enemy_own & aDots[d.x - 2, d.y + 3].Blocked == false
                               & aDots[d.x - 2, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y + 2].Blocked == false
                               & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                               & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                               & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                               & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                               & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                               & aDots[d.x - 1, d.y + 1].Own != enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                               & aDots[d.x - 1, d.y + 2].Own != enemy_own & aDots[d.x - 1, d.y + 2].Blocked == false
                               & aDots[d.x - 1, d.y + 3].Own != enemy_own & aDots[d.x - 1, d.y + 3].Blocked == false
                           select d;
            if (pat274_2.Count() > 0) return new Dot(pat274_2.First().x - 1, pat274_2.First().y);
            //--------------Rotate on 90----------------------------------- 
            var pat274_2_3 = from Dot d in get_non_blocked
                             where d.Own == Owner
                                 & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                 & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                 & aDots[d.x + 2, d.y].Own == enemy_own & aDots[d.x + 2, d.y].Blocked == false
                                 & aDots[d.x + 3, d.y].Own == enemy_own & aDots[d.x + 3, d.y].Blocked == false
                                 & aDots[d.x + 4, d.y - 1].Own == enemy_own & aDots[d.x + 4, d.y - 1].Blocked == false
                                 & aDots[d.x + 3, d.y - 2].Own == enemy_own & aDots[d.x + 3, d.y - 2].Blocked == false
                                 & aDots[d.x + 2, d.y - 2].Own == enemy_own & aDots[d.x + 2, d.y - 2].Blocked == false
                                 & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                 & aDots[d.x, d.y - 1].Own == 0 & aDots[d.x, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                                 & aDots[d.x - 1, d.y].Own == 0 & aDots[d.x - 1, d.y].Blocked == false
                                 & aDots[d.x + 1, d.y - 1].Own != enemy_own & aDots[d.x + 1, d.y - 1].Blocked == false
                                 & aDots[d.x + 2, d.y - 1].Own != enemy_own & aDots[d.x + 2, d.y - 1].Blocked == false
                                 & aDots[d.x + 3, d.y - 1].Own != enemy_own & aDots[d.x + 3, d.y - 1].Blocked == false
                             select d;
            if (pat274_2_3.Count() > 0) return new Dot(pat274_2_3.First().x, pat274_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat274_2_3_4 = from Dot d in get_non_blocked
                               where d.Own == Owner
                                   & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                   & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                   & aDots[d.x - 2, d.y].Own == enemy_own & aDots[d.x - 2, d.y].Blocked == false
                                   & aDots[d.x - 3, d.y].Own == enemy_own & aDots[d.x - 3, d.y].Blocked == false
                                   & aDots[d.x - 4, d.y + 1].Own == enemy_own & aDots[d.x - 4, d.y + 1].Blocked == false
                                   & aDots[d.x - 3, d.y + 2].Own == enemy_own & aDots[d.x - 3, d.y + 2].Blocked == false
                                   & aDots[d.x - 2, d.y + 2].Own == enemy_own & aDots[d.x - 2, d.y + 2].Blocked == false
                                   & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                   & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                   & aDots[d.x, d.y + 1].Own == 0 & aDots[d.x, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                                   & aDots[d.x + 1, d.y].Own == 0 & aDots[d.x + 1, d.y].Blocked == false
                                   & aDots[d.x - 1, d.y + 1].Own != enemy_own & aDots[d.x - 1, d.y + 1].Blocked == false
                                   & aDots[d.x - 2, d.y + 1].Own != enemy_own & aDots[d.x - 2, d.y + 1].Blocked == false
                                   & aDots[d.x - 3, d.y + 1].Own != enemy_own & aDots[d.x - 3, d.y + 1].Blocked == false
                               select d;
            if (pat274_2_3_4.Count() > 0) return new Dot(pat274_2_3_4.First().x, pat274_2_3_4.First().y + 1);
            //============================================================================================================== 
            iNumberPattern = 44;
            var pat44 = from Dot d in get_non_blocked
                        where d.Own == Owner
                            & aDots[d.x - 1, d.y + 2].Own == Owner & aDots[d.x - 1, d.y + 2].Blocked == false
                            & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                            & aDots[d.x + 2, d.y - 1].Own == Owner & aDots[d.x + 2, d.y - 1].Blocked == false
                            & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                            & aDots[d.x + 1, d.y + 1].Own == 0 & aDots[d.x + 1, d.y + 1].Blocked == false
                            & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                            & aDots[d.x + 2, d.y + 1].Own == 0 & aDots[d.x + 2, d.y + 1].Blocked == false
                            & aDots[d.x + 1, d.y + 2].Own == 0 & aDots[d.x + 1, d.y + 2].Blocked == false
                        select d;
            if (pat44.Count() > 0) return new Dot(pat44.First().x + 1, pat44.First().y + 1);
            //180 Rotate=========================================================================================================== 
            var pat44_2 = from Dot d in get_non_blocked
                          where d.Own == Owner
                              & aDots[d.x + 1, d.y - 2].Own == Owner & aDots[d.x + 1, d.y - 2].Blocked == false
                              & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                              & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                              & aDots[d.x - 2, d.y + 1].Own == Owner & aDots[d.x - 2, d.y + 1].Blocked == false
                              & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                              & aDots[d.x - 1, d.y - 1].Own == 0 & aDots[d.x - 1, d.y - 1].Blocked == false
                              & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                              & aDots[d.x - 2, d.y - 1].Own == 0 & aDots[d.x - 2, d.y - 1].Blocked == false
                              & aDots[d.x - 1, d.y - 2].Own == 0 & aDots[d.x - 1, d.y - 2].Blocked == false
                          select d;
            if (pat44_2.Count() > 0) return new Dot(pat44_2.First().x - 1, pat44_2.First().y - 1);
            //--------------Rotate on 90----------------------------------- 
            var pat44_2_3 = from Dot d in get_non_blocked
                            where d.Own == Owner
                                & aDots[d.x + 2, d.y + 1].Own == Owner & aDots[d.x + 2, d.y + 1].Blocked == false
                                & aDots[d.x + 1, d.y].Own == enemy_own & aDots[d.x + 1, d.y].Blocked == false
                                & aDots[d.x, d.y - 1].Own == enemy_own & aDots[d.x, d.y - 1].Blocked == false
                                & aDots[d.x - 1, d.y - 2].Own == Owner & aDots[d.x - 1, d.y - 2].Blocked == false
                                & aDots[d.x + 2, d.y].Own == 0 & aDots[d.x + 2, d.y].Blocked == false
                                & aDots[d.x + 1, d.y - 1].Own == 0 & aDots[d.x + 1, d.y - 1].Blocked == false
                                & aDots[d.x, d.y - 2].Own == 0 & aDots[d.x, d.y - 2].Blocked == false
                                & aDots[d.x + 1, d.y - 2].Own == 0 & aDots[d.x + 1, d.y - 2].Blocked == false
                                & aDots[d.x + 2, d.y - 1].Own == 0 & aDots[d.x + 2, d.y - 1].Blocked == false
                            select d;
            if (pat44_2_3.Count() > 0) return new Dot(pat44_2_3.First().x + 1, pat44_2_3.First().y - 1);
            //--------------Rotate on 90 - 2----------------------------------- 
            var pat44_2_3_4 = from Dot d in get_non_blocked
                              where d.Own == Owner
                                  & aDots[d.x - 2, d.y - 1].Own == Owner & aDots[d.x - 2, d.y - 1].Blocked == false
                                  & aDots[d.x - 1, d.y].Own == enemy_own & aDots[d.x - 1, d.y].Blocked == false
                                  & aDots[d.x, d.y + 1].Own == enemy_own & aDots[d.x, d.y + 1].Blocked == false
                                  & aDots[d.x + 1, d.y + 2].Own == Owner & aDots[d.x + 1, d.y + 2].Blocked == false
                                  & aDots[d.x - 2, d.y].Own == 0 & aDots[d.x - 2, d.y].Blocked == false
                                  & aDots[d.x - 1, d.y + 1].Own == 0 & aDots[d.x - 1, d.y + 1].Blocked == false
                                  & aDots[d.x, d.y + 2].Own == 0 & aDots[d.x, d.y + 2].Blocked == false
                                  & aDots[d.x - 1, d.y + 2].Own == 0 & aDots[d.x - 1, d.y + 2].Blocked == false
                                  & aDots[d.x - 2, d.y + 1].Own == 0 & aDots[d.x - 2, d.y + 1].Blocked == false
                              select d;
            if (pat44_2_3_4.Count() > 0) return new Dot(pat44_2_3_4.First().x - 1, pat44_2_3_4.First().y + 1);
            //============================================================================================================== 



            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль

        }

    }
}
