using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class GameForm : Form
    {
        private readonly int CellSize;
        private readonly int MazeSize;
        private int playerX = 1;
        private int playerY = 0;
        private int[,] array2;
        public GameForm(int[,] array2)
        {
            InitializeComponent();
            this.array2 = array2;
            CellSize = this.array2.GetLength(0) * 2;
            MazeSize = this.array2.GetLength(1);
            InitializeMaze();
        }

        private void InitializeMaze()
        {
            this.ClientSize = new Size(MazeSize * CellSize, MazeSize * CellSize);
            this.Text = "Maze Game";
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < array2.GetLength(0); i++)
            {
                for(int j = 0; j < array2.GetLength(1); j++)
                {
                    Brush brush = array2[i, j] == 1? Brushes.Black : Brushes.White;
                    g.FillRectangle(brush, j * CellSize, i * CellSize, CellSize, CellSize);
                }
            }
            g.FillRectangle(Brushes.Red, playerX * CellSize, playerY * CellSize, CellSize, CellSize);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = playerX;
            int newY = playerY;

            switch(e.KeyCode) 
            {
                case Keys.Up: { newY = Math.Max(0, playerY - 1); break; }
                case Keys.Down: { newY = Math.Min(MazeSize - 1, playerY + 1); break; }
                case Keys.Left: { newY = Math.Max(0, playerX - 1); break; }
                case Keys.Right: { newX = Math.Min(MazeSize - 1, playerX + 1); break; }
            }
            if (array2[newX, newY] == 0)
            {
                playerX = newX;
                playerY = newY;
                Invalidate();
            }    
            if(playerX==array2.GetLength(1)-1 || playerY==array2.GetLength(0)-1) 
            {
                MessageBox.Show("Game over");
                this.Close();
            }
        }
    }
}
