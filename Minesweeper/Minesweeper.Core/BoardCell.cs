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

        public GameState State { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        // ctor
        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            State = GameState.GameStart;

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
            if (State == GameState.GameOver) return;
            if (Width < 0 || Width <= x || Height < 0 || Height <= y) return;

            if (State == GameState.GameStart)
            {
                SetBombs(1);
                State = GameState.Gaming;
            }
            var cell = cells[x, y];

            if (cell.IsOpened || cell.IsFlag) return;

            cell.IsOpened = true;

            TouchAroundSafetyCells(x, y);

            if (cell.IsBomb)
            {
                GameOver();
                return;
            }

            if (IsClear())
            {
                State = GameState.GameClear;
            }
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

        public void SetBombs(int numBomb)
        {
            var random = new Random();
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

        public int GetBombsCount()
        {
            var count = 0;
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (cells[i, j].IsBomb) count++;
                }
            }
            return count;
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

        public bool IsClear()
        {
            var result = true;
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    result &= GetCell(i, j).IsBomb || GetCell(i, j).IsOpened;
                }
            }
            return result;
        }

        public void Flag(int x, int y)
        {
            if (IsValidCell(x, y)) GetCell(x, y).IsFlag ^= true;
        }

        public void GameOver()
        {
            State = GameState.GameOver;
        }

        public void Continue()
        {
            cells = new Cell[Width, Height];
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    cells.SetValue(new Cell() { X = i, Y = j, IsBomb = false }, i, j);
                }
            }
            State = GameState.GameStart;

        }
    }

    public enum GameState
    {
        GameStart,
        GameOver,
        Gaming,
        GameClear
    }

    public class Cell
    {
        public bool IsOpened { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

    }
}
