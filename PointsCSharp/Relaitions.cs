using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotsGame
{
    class Relaitions
    {
        public Dot d1, d2;
        public int Own;
        public enum Owner : int { None = 0, Player1 = 1, Player2 = 2 }
        public Relaitions(Dot d1,Dot d2)//конструктор класса
        {
            d1.InRelation =true;
            d2.InRelation = true;
            this.d1  = d1;
            this.d2  = d2;
            //Own = (int)OwnerDot;
        }

        //public bool CheckRegion(Dot[] ArrayDots)
        //{
        //    int i;
        //    int chkX = 0, chkY = 0;
        //    for (i = 0; i <= ArrayDots.Length; i++)
        //    {
        //        if (i < ArrayDots.Length)
        //        {
        //            chkX += (ArrayDots[i].x - ArrayDots[i + 1].x);
        //            chkY += (ArrayDots[i].x - ArrayDots[i + 1].y);
        //        }
        //        else
        //        {
        //            chkX += (ArrayDots[i].x - ArrayDots[0].x);
        //            chkY += (ArrayDots[i].y - ArrayDots[0].y);
        //        }
        //    }
        //    if (chkX == 0 & chkY == 0)
        //    {
        //        return true;
        //    }
        //    else { return false; }
        //}
    }
}
