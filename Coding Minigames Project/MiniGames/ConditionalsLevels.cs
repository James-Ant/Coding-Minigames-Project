using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Minigames_Project
{
    public static class ConditionalsLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            // Level 1: Simple Gate (Revamped)
            var level1 = new LevelData("Vital Signs", "Check if the player is still in the game.");
            level1.BackgroundColor = Color.FromArgb(25, 25, 35); // Dark Slate[cite: 1]

            // Position the target slot high up
            level1.SlotPositions.Add(new Point(300, 150));
            level1.SlotSizes.Add(new Size(200, 60)); // Slightly larger slot[cite: 1]
            level1.ExpectedTags.Add("if-health");

            // Spread out the choices
            level1.AvailableBlocks.Add(new CodeBlock("if (health > 0)", "if-health", BlockCategory.Conditional, "The player is alive"));
            level1.AvailableBlocks.Add(new CodeBlock("if (health < 0)", "if-wrong1", BlockCategory.Conditional, "The player is already dead!"));
            level1.AvailableBlocks.Add(new CodeBlock("if (health == 0)", "if-wrong2", BlockCategory.Conditional, "Just barely gone!"));

            level1.BlockPositions.Add(new Point(100, 450));
            level1.BlockPositions.Add(new Point(350, 450));
            level1.BlockPositions.Add(new Point(600, 450));

            levels.Add(level1);

            // Level 2: Two Paths
            var level2 = new LevelData("Two Paths");
            level2.AvailableBlocks.Add(new CodeBlock("if (score > 50)", "if-score", BlockCategory.Conditional));
            level2.AvailableBlocks.Add(new CodeBlock("else", "else-block", BlockCategory.Conditional));
            level2.AvailableBlocks.Add(new CodeBlock("if (score < 50)", "if-wrong", BlockCategory.Conditional));
            level2.AvailableBlocks.Add(new CodeBlock("else if", "elseif-wrong", BlockCategory.Conditional));
            level2.ExpectedTags.Add("if-score");
            level2.ExpectedTags.Add("else-block");
            levels.Add(level2);

            // Level 3: Chained Logic
            var level3 = new LevelData("Chained Logic");
            level3.AvailableBlocks.Add(new CodeBlock("if (rank == 1)", "if-rank1", BlockCategory.Conditional));
            level3.AvailableBlocks.Add(new CodeBlock("else if (rank == 2)", "elseif-rank2", BlockCategory.Conditional));
            level3.AvailableBlocks.Add(new CodeBlock("else", "else-block", BlockCategory.Conditional));
            level3.AvailableBlocks.Add(new CodeBlock("if (rank == 2)", "if-wrong", BlockCategory.Conditional));
            level3.ExpectedTags.Add("if-rank1");
            level3.ExpectedTags.Add("elseif-rank2");
            level3.ExpectedTags.Add("else-block");
            levels.Add(level3);

            return levels;
        }
    }
}
