using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Minesweeper.Core.Tests
{
    [TestFixture]
    public class BoadTest
    {
        [Test]
        public void TestTouchCellOfBoard0()
        {
            var board = new Board(3, 3);
            board.Touch(0, 0);

            Assert.That(board.GetCell(0, 0).IsOpened, Is.True);
        }

        [Test]
        public void TestTouchCellOfBoard()
        {
            var board = new Board(10, 10);
            board.Touch(0, 0);

            Assert.That(board.GetCell(0, 0).IsOpened, Is.True);
        }

        [Test]
        public void TestTouchTwoCellOfBoard()
        {
            var board = new Board(10, 10);
            board.Touch(1, 1);

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

        [Test]
        public void TestGetCoutOfAroundBombs3()
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
            board.SetBomb(1, 1);

            Assert.That(board.GetCountOfAroundBombs(1, 1), Is.EqualTo(8));
        }

        [Test]
        public void TestGetCoutOfAroundBombs4()
        {
            var board = new Board(10, 10);
            board.SetBomb(1, 0);
            board.SetBomb(0, 1);

            Assert.That(board.GetCountOfAroundBombs(0, 0), Is.EqualTo(2));
        }

  

        // doing
        [Test]
        public void TestGetCoutOfAroundBombs5()
        {
            var board = new Board(5, 5);
            board.SetBomb(4, 4);
            board.SetBomb(2, 3);

            Assert.That(board.GetCountOfAroundBombs(4, 2), Is.EqualTo(0));
        }

        [Test]
        public void TestGetCoutOfAroundBombs6()
        {
            var board = new Board(3, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != 1 || j != 2)
                        board.SetBomb(i, j);
                }
            }
            Assert.That(board.GetCountOfAroundBombs(1, 2), Is.EqualTo(5));
        }

        [Test]
        public void WhenTouchBomb_GameOver()
        {
            var board = new Board(10, 10);
            board.SetBomb(0, 0);

            board.Touch(0, 0);
            Assert.That(board.State, Is.EqualTo(GameState.GameOver));
        }

        [Test]
        public void WhenTouchBomb_GameOver2()
        {
            var board = new Board(10, 10);
            board.SetBomb(1, 1);

            board.Touch(1, 1);
            Assert.That(board.State, Is.EqualTo(GameState.GameOver));
        }

        [Test]
        public void WhenTouchBomb_GameOver3()
        {
            var board = new Board(10, 10);
            board.SetBomb(1, 1);

            board.Touch(1, 2);
            Assert.That(board.State, Is.Not.EqualTo(GameState.GameOver));
        }

        [Test]
        public void WhenTouchZero_TheresNoBombs()
        {
            var board = new Board(3, 3);

            board.Touch(0, 0);
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    Assert.That(board.GetCell(x, y).IsOpened, Is.True);
                }
        }

        [Test]
        public void WhenTouchZero_TheresOneBombs()
        {
            var board = new Board(3, 3);
            board.SetBomb(2, 2);
            board.Touch(0, 0);

            for (int x = 0; x < 3; ++x)
                for (int y = 0; y < 3; ++y)
                {
                    if (x == 2 && y == 2)
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.False);
                    }
                    else
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.True);
                    }
                }
        }

        [Test]
        public void WhenTouchZero_TheresOneBombs2()
        {
            var board = new Board(5, 5);
            board.SetBomb(4, 4);
            board.Touch(0, 0);

            for (int x = 0; x < 5; ++x)
                for (int y = 0; y < 5; ++y)
                {
                    if (x == 4 && y == 4)
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.False);
                    }
                    else
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.True);
                    }
                }
        }

        [Test]
        public void TouchSafetyCell()
        {
            var board = new Board(3, 3);
            board.SetBomb(2, 2);
            board.Touch(1, 0);

            for (int x = 0; x < 3; ++x)
                for (int y = 0; y < 3; ++y)
                {
                    if (x == 2 && y == 2)
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.False);
                    }
                    else
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.True);
                    }
                }

        }

        [Test]
        public void WhenTouchZero_TheresOneBombs3()
        {
            var board = new Board(5, 5);
            board.SetBomb(4, 4);
            board.SetBomb(2, 3);

            board.Touch(0, 0);

            var notOpendCellIndexes = new []{ Tuple.Create(2, 3), Tuple.Create(2, 4), Tuple.Create(3, 4), Tuple.Create(4, 4) };

            for (int x = 0; x < 5; ++x)
                for (int y = 0; y < 5; ++y)
                {
                    if (notOpendCellIndexes.Any(idx => idx.Item1 == x && idx.Item2 == y))
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.False);
                    }
                    else
                    {
                        Assert.That(board.GetCell(x, y).IsOpened, Is.True);
                    }
                }
        }

        [Test]
        public void SetRandomBomb1()
        {
            var board = new Board(3, 3);
            board.SetBombs(5);

            Assert.That(5, Is.EqualTo(board.GetBombsCount()));
        }

        [Test]
        public void SetRandomBomb2()
        {
            var board = new Board(5, 5);
            board.SetBombs(10);

            Assert.That(10, Is.EqualTo(board.GetBombsCount()));
        }
    }
}
