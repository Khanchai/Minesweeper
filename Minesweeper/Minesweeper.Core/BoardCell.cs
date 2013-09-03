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
        Cell[,] cells;

        public GameState GameState { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        // ctor
        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            GameState = GameState.Continue;

            cells = new Cell[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = new Cell() { X = i, Y = j };
                }
        }

        public void TouchAroundSafetyCells(int x, int y)
        {
            if (GetCountOfAroundBombs(x, y) == 0)
            {
                foreach (var aroundCell in GetAroundCells(x, y))
                {
                    if ((!GetCell(aroundCell.X, aroundCell.Y).IsOpened) && (IsValidCell(aroundCell.X, aroundCell.Y)))
                    {
                        GetCell(aroundCell.X, aroundCell.Y).IsOpened = true;
                        TouchAroundSafetyCells(aroundCell.X, aroundCell.Y);
                    }
                }
            }
        }

        public Cell[] GetAroundCells(int x, int y)
        {
            var listCell = new List<Cell>();
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i != x || j != y)
                    {
                        if (IsValidCell(i, j))
                        {
                            listCell.Add(GetCell(i, j));
                        }
                    }
                }
            }
            return listCell.ToArray();
        }

        private bool IsValidCell(int x, int y)
        {
            return (x >= 0 && x < Width) && (y >= 0 && y < Height);
        }

        // method
        public void Touch(int x, int y)
        {
          TouchAroundSafetyCells(x, y);

          GameState = GetCell(x, y).IsBomb ? GameState.GameOver : GameState.Continue;
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
            for (int i = x-1; i <= x + 1; ++i)
                for (int j = y-1; j <= y + 1; ++j)
                {
                    if (IsValidCell(i,j) && GetCell(i, j).IsBomb && !(i == x && j == y))
                    {
                        countBomb++;
                    }
                }
            return countBomb;
        }
    }

    public enum GameState { GameOver, Continue, GameClear }

    public class Cell
    {
        public bool IsOpened { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
    }
}
