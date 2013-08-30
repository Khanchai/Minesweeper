using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Core
{
    public class MinesweeperGame
    {
        //        Mines 
        //        Opened
        //        Marked
        //        Values
        //        Width 
        //        Height
        

        public static void Broad()
        {
            
        }

        public static bool CreateCells(int width, int height)
        {
            var randoms = new Random();
            randoms.Next(width,height);

            if (width == 0 && height == 0) return false;

            return true;
        }

        public void CreateMine()
        {

        }

    }
    
}
