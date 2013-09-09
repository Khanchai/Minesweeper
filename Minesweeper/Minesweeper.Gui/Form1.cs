using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Core;

namespace Minesweeper.Gui
{
    public partial class MineSweeper : Form
    {
        Board board = new Board(10, 10);
        
        public MineSweeper()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (board.State == GameState.GameOver || board.State == GameState.GameClear)
            {
                board.Continue();
            }
            else
            {
                var hit = BoardMetrics.HitTestCell(e.Location);

                if (hit == null) return;

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    board.Touch(hit.Item1, hit.Item2);
                }
                else if(e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    board.Flag(hit.Item1, hit.Item2);
                }
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BoardRenderer.Render(e.Graphics, board);
            base.OnPaint(e);
        }
    }
}
