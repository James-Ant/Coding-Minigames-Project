using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    /// <summary>
    /// Represents an interactive door tile within the maze grid that triggers a trivia question.
    /// </summary>
    public class MazeDoor
    {
        // Reference to the physical UI panel on the game grid
        public Panel Tile { get; set; }

        // Flags to manage the door's state and behavior
        public bool HasQuestion { get; set; }
        public bool AlreadyUsed { get; set; }

        /// <summary>
        /// Initializes a new door linked to a specific map panel tile.
        /// </summary>
        public MazeDoor(Panel tile)
        {
            Tile = tile;
            HasQuestion = true;   // Default to active state
            AlreadyUsed = false;  // Starts as unvisited/unlocked
        }
    }
}