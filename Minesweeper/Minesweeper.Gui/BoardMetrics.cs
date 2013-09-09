using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Gui
{
    class BoardMetrics
    {
        public const int CellWidth = 50;
        public const int CellHeight = 50;
        public const int Offset = 50;

        public static Tuple<int, int> HitTestCell(Point point)
        {
            if (point.X < Offset && point.Y < Offset) return null;

            var xOfCell = (point.X - Offset) / CellWidth;
            var yOfCell = (point.Y - Offset) / CellHeight;

            return Tuple.Create(xOfCell, yOfCell);
        }
    }
}
