﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Minesweeper.Core.Tests
{
    [TestFixture]
    public class BoadTest
    {
        [Test]
        public void TestTouchCellOfBoard()
        {
            var board = new Board(10, 10);
            board.Touch(0, 0);

            Assert.That(board.GetCell(0, 0).IsOpened, Is.False);
        }

        [Test]
        public void TestTouchTwoCellOfBoard()
        {
            var board = new Board(10, 10);
            board.Touch(1, 1);

            Assert.That(board.GetCell(0, 0).IsOpened, Is.False);
            Assert.That(board.GetCell(1, 1).IsOpened, Is.True);
        }

        [Test]
        public void TestGetCoutOfAroundBombs()
        {
            var board = new Board(10, 10);
            board.SetBomb(0, 0);
            board.SetBomb(2, 0);

            Assert.That(board.GetCountOfAroundBombs(1, 1), Is.EqualTo(2));
        }

        [Test]
        public void TestGetCoutOfAroundBombs2()
        {
            var board = new Board(10, 10);
            board.SetBomb(0, 0);
            board.SetBomb(1, 0);
            board.SetBomb(2, 0);
            board.SetBomb(2, 1);
            board.SetBomb(2, 2);
            board.SetBomb(1, 2);
            board.SetBomb(0, 2);
            board.SetBomb(0, 1);

            Assert.That(board.GetCountOfAroundBombs(1, 1), Is.EqualTo(8));
        }
    }
}