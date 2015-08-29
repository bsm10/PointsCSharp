using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                if (aDots[p.x, p.y + 2].Own == PLAYER_NONE) return new Dot(p.x, p.y + 2);
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
                if (aDots[p.x + 2, p.y].Own == PLAYER_NONE) return new Dot(p.x + 2, p.y);
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
                if (aDots[p.x - 2, p.y].Own == PLAYER_NONE) return new Dot(p.x - 2, p.y);
            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
                if (aDots[p.x, p.y + 2].Own == PLAYER_NONE) return new Dot(p.x, p.y + 2);
            }

            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //                           *     +  ставить сюда 
            var pat3 = from Dot d in get_non_blocked
                       where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                              aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                              aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                              aDots[d.x + 1, d.y + 1].Own == 0
                       select d;
            if (pat3.Count() > 0) return new Dot(pat3.First().x + 1, pat3.First().y + 1);
            // паттерн на конструкцию    *     *      точка окружена через две точки
            //                             d+    
            //               cтавить сюда+     *   
            var pat3_1 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y + 1].Own == 0 &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat3_1.Count() > 0) return new Dot(pat3_1.First().x + 1, pat3_1.First().y + 1);
            // паттерн на конструкцию    +     *      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_2 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y - 1].Own == 0 &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat3_2.Count() > 0) return new Dot(pat3_2.First().x + 1, pat3_2.First().y + 1);
            // паттерн на конструкцию    *     +      точка окружена через две точки
            //                             d+    
            //                           *     *   
            var pat3_3 = from Dot d in get_non_blocked
                         where d.Own == Owner & aDots[d.x + 1, d.y - 1].Own == 0 &
                                                aDots[d.x - 1, d.y - 1].Own == enemy_own &
                                                aDots[d.x - 1, d.y + 1].Own == enemy_own &
                                                aDots[d.x + 1, d.y + 1].Own == enemy_own
                         select d;
            if (pat3_3.Count() > 0) return new Dot(pat3_3.First().x + 1, pat3_3.First().y + 1);





            //если никаких паттернов не найдено возвращаем нуль
            return null;
        }
    }
}
