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

        public int GetCountOfAroundBombs(int x, int y)
        {
            var countBomb = 0;
            for (int i = 0; i <= x+1; i++)
                for (int j = 0; j <= y + 1; j++)
                {
                    if (GetCell(i, j).IsBomb && !(i == x && j == y))
                    {
                        countBomb++;
                    }
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
