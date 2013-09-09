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

            GameState = GameState.Gaming;

            cells = new Cell[width, height];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
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

        public void TouchNoBomb(int x, int y,bool bomb)
        {
            if (GetCountOfAroundBombs(x, y) == 0)
            {
                foreach (var cell in GetAroundCells(x, y).Where(cellx => !GetCell(cellx.X, cellx.Y).IsOpened && IsValidCell(cellx.X, cellx.Y)))
                {
                    GetCell(cell.X, cell.Y).IsOpened = true;
                    TouchNoBomb(cell.X, cell.Y, false);
                }
            }
        }

        public Cell[] GetAroundCells(int x, int y)
        {
            var listCell = new List<Cell>();
            for (var i = x - 1; i < x + 2; i++)
            {
                for (var j = y - 1; j < y + 2; j++)
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
            if(GameState == GameState.GameOver) return;
            if(Width <0 || Width <= x || Height < 0 || Height <= y) return;

            if (GameState == GameState.StartGame)
            {
                GameState = GameState.Gaming;
            }
            var cell = cells[x, y];

            if (cell.IsOpened || cell.IsFlag) return;
            cell.IsOpened = true;
            TouchAroundSafetyCells(x, y);
            TouchNoBomb(x,y,true);

            GameState = GetCell(x, y).IsBomb ? GameState.GameOver : GameState.Gaming;
        }

        // method
        public Cell GetCell(int x, int y)
        {
            return cells[x, y];
        }

        public void SetBomb(int x, int y)
        {
            GetCell(x, y).IsBomb = true;
        }

        public int GetBombsCount()
        {
            var count = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (cells[i, j].IsBomb) count++;
                }
            }
            return count;
        }

        public void SetBombs(int numBomb)
        {
            var random = new Random();
//            for (int i = 0; i < Width; i++)
//            {
//                for (int j = 0; j < Height; j++)
//                {
//                    if(x == i && y == j) continue;
//                    GetCell(x, y).IsBomb = random.Next(0, 8) == 1;
//                }
//            }
            var bombCount = 0;
            while (bombCount < numBomb)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                if (!GetCell(x, y).IsBomb)
                {
                    SetBomb(x, y);
                    bombCount++;
                }
            }
        }

        public int GetCountOfAroundBombs(int x, int y)
        {
            var countBomb = 0;
            for (var i = x - 1; i < x + 2; ++i)
                for (var j = y - 1; j < y + 2; ++j)
                {
                    if (IsValidCell(i, j) && GetCell(i, j).IsBomb && !(i == x && j == y))
                    {
                        countBomb++;
                    }
                }
            return countBomb;
        }
        internal void Flag(int x, int y)
        {
            if (IsValidCell(x, y)) GetCell(x, y).IsFlag ^= true;
        }
    }

    public enum GameState { StartGame, GameOver, Gaming, GameClear }

    public class Cell
    {
        public bool IsOpened { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

    }
}
