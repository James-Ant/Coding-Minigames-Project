using System.Collections.Generic;
using System.Drawing;

namespace Coding_Minigames_Project
{
    // LevelData holds all the configuration needed to set up one puzzle level in the drag-and-drop
    // minigames.
    public class LevelData
    {
        public string LevelName { get; set; }

        public string Description { get; set; }

        public string Hint { get; set; }

        public List<CodeBlock> AvailableBlocks { get; set; }

        public List<string> ExpectedTags { get; set; }

        public Color BackgroundColor { get; set; } = Color.FromArgb(30, 30, 30);

        public List<Point> BlockPositions { get; set; }

        public List<Point> SlotPositions { get; set; }

        public List<Size> SlotSizes { get; set; }

        public List<string> SlotLabels { get; set; }

        public Image LevelImage { get; set; }

        public LevelData(string levelName, string description = "", string hint = "")
        {
            LevelName = levelName;
            Description = description;
            Hint = hint;
            AvailableBlocks = new List<CodeBlock>();
            ExpectedTags = new List<string>();
            BlockPositions = new List<Point>();
            SlotPositions = new List<Point>();
            SlotSizes = new List<Size>();
            SlotLabels = new List<string>();
        }
    }
}
