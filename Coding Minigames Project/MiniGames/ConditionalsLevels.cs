using System;
using System.Collections.Generic;
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

            // Level 1: Simple Gate
            var level1 = new LevelData("Simple Gate");
            level1.AvailableBlocks.Add(new CodeBlock("if (health > 0)", "if-health", BlockCategory.Conditional, "Open if alive"));
            level1.AvailableBlocks.Add(new CodeBlock("if (health < 0)", "if-wrong1", BlockCategory.Conditional, "Wrong direction!"));
            level1.AvailableBlocks.Add(new CodeBlock("if (health == 0)", "if-wrong2", BlockCategory.Conditional, "Only when dead!"));
            level1.ExpectedTags.Add("if-health");
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
