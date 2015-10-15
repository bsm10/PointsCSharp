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



            //=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*
            return null;//если никаких паттернов не найдено возвращаем нуль
        }
    }
}
