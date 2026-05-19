using System;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    public class MazeGenerator
    {
        Random rnd = new Random();

        public Panel[,] Generate(int rows, int cols, int tileSize, Control parent)
        {
            Panel[,] tiles = new Panel[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Panel p = new Panel
                    {
                        Width = tileSize,
                        Height = tileSize,
                        Left = c * tileSize,
                        Top = r * tileSize,
                        BorderStyle = BorderStyle.None
                    };

                    // BASIC MAZE GENERATION
                    // 30% walls, 70% paths
                    int chance = rnd.Next(100);

                    if (chance < 30)
                        p.BackColor = Color.FromArgb(83, 74, 183);   // WALL
                    else
                        p.BackColor = Color.Black;  // PATH

                    tiles[r, c] = p;
                    parent.Controls.Add(p);
                }
            }

            return tiles;
        }
    }
}