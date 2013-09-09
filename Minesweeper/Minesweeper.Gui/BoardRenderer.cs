using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Minesweeper.Core;

namespace Minesweeper.Gui
{
       public static class BoardRenderer
       {
       public static void Render(Graphics graphics, Board board)
        {
            for(int x = 0; x < board.Width; ++x)
                for (int y = 0; y < board.Height; ++y)
                {
                    var cellRect = new Rectangle(
                        BoardMetrics.Offset + x * BoardMetrics.CellWidth, 
                        BoardMetrics.Offset + y * BoardMetrics.CellHeight, 
                        BoardMetrics.CellWidth, 
                        BoardMetrics.CellHeight);

                    var isOpen = board.GetCell(x, y).IsOpened;
                    var isBomb = board.GetCell(x, y).IsBomb;
                    var countOfBombs = board.GetCountOfAroundBombs(x, y);
                    ButtonRenderer.DrawButton(graphics, cellRect, isOpen ? PushButtonState.Normal : PushButtonState.Pressed);
                    var font = new Font(FontFamily.GenericSerif, 15f);

                    if (isOpen)
                    {
                        if (isBomb)
                        {
                            graphics.FillEllipse(Brushes.Red, cellRect);
                        }
                        else
                        {
                            if (countOfBombs != 0)
                                TextRenderer.DrawText(graphics, countOfBombs.ToString(), font, cellRect, Color.Black);
                        }
                    }
                    else
                    {
                        if(board.GetCell(x,y).IsFlag)
                            TextRenderer.DrawText(graphics, "Flag", font, cellRect, Color.Red);
                    }
                }

            if (board.State == GameState.GameOver)
            {
                var scene = new Rectangle(BoardMetrics.Offset, BoardMetrics.Offset, BoardMetrics.CellWidth * board.Width, BoardMetrics.CellHeight * board.Height);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Red)), scene);
                var font = new Font(FontFamily.GenericSerif, 30f);
                TextRenderer.DrawText(graphics, "Game Over!", font, scene, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            if (board.State == GameState.GameClear)
            {
                var scene = new Rectangle(BoardMetrics.Offset, BoardMetrics.Offset, BoardMetrics.CellWidth * board.Width, BoardMetrics.CellHeight * board.Height);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Blue)), scene);
                var font = new Font(FontFamily.GenericSerif, 30f);
                TextRenderer.DrawText(graphics, "Clear!", font, scene, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}