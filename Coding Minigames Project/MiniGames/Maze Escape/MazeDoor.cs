using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    public class MazeDoor
    {
        public Panel Tile { get; set; }
        public bool HasQuestion { get; set; }
        public bool AlreadyUsed { get; set; }

        public MazeDoor(Panel tile)
        {
            Tile = tile;
            HasQuestion = true;
            AlreadyUsed = false;
        }
    }
}
