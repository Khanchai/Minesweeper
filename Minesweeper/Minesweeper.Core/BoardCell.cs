using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Core
{
    public class Board // can has fields, ctor, methods, properties, types, delegate
    {
        public Cell[,] cells;

        // ctor
        public Board(int width, int height)
        {
            cells = new Cell[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = new Cell();
                }
        }

        // method
        public void Touch(int x, int y)
        {
            GetCell(x, y).IsOpened = true;
        }

        // method
        public Cell GetCell(int x, int y)
        {
            return cells[x, y];
        }

        // this is for unittest
        public void SetBomb(int x, int y)
        {
            GetCell(x, y).IsBomb = true;
        }

        public int GetCountOfAroundBombs(int x, int y)// x1,y1
        {
            var countBomb = 0;
            //  is Cell(x - 1, y -1) bomb
            var isBomb1 = GetCell(x - 1, y - 1).IsBomb;
            var isBomb2 = GetCell(x, y - 1).IsBomb;
            var isBomb3 = GetCell(x + 1, y - 1).IsBomb;
            var isBomb4 = GetCell(x + 1, y).IsBomb;
            var isBomb5 = GetCell(x + 1, y + 1).IsBomb;
            var isBomb6 = GetCell(x, y + 1).IsBomb;
            var isBomb7 = GetCell(x - 1, y + 1).IsBomb;
            var isBomb8 = GetCell(x - 1, y).IsBomb;

            if (isBomb1)
            {
                countBomb++;
            }
            if (isBomb2)
            {
                countBomb++;
            }
            if (isBomb3)
            {
                countBomb++;
            }
            if (isBomb4)
            {
                countBomb++;
            }
            if (isBomb5)
            {
                countBomb++;
            }
            if (isBomb6)
            {
                countBomb++;
            }
            if (isBomb7)
            {
                countBomb++;
            }
            if (isBomb8)
            {
                countBomb++;
            }

            return countBomb;
        }
    }

    public class Cell
    {
        public bool IsOpened { get; set; }
        public bool IsBomb { get; set; }
    }
}
