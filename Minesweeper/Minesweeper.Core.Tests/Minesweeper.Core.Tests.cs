using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Minesweeper.Core.Tests
{
    [TestFixture]
    public class DrawMinesweeper
    {
        [Test]
        public void BoardTests ()
        {
            
        }
        [Test]
        public void DrawCellsTests()
        {
            Assert.That(MinesweeperGame.CreateCells(10, 10),Is.True);
            Assert.That(MinesweeperGame.CreateCells(0, 0),Is.False);
        }

        public void ChooseBombTests()
        {
            var mine = new CreateMine();
            mine.Over();
            Assert.AreEqual("Game Over", mine.ToString());
        }

        public void ChooseEmptySpaceTests()
        {
            var emptySpace = new EmptySpace();
            
        }

        public void Choose
    }

    public class Space
    {

    }
}
