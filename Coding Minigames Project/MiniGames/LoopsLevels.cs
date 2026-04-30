using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Minigames_Project
{
    public static class LoopsLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            // Level 1: Count it Out
            var level1 = new LevelData("Count it Out");
            level1.AvailableBlocks.Add(new CodeBlock("for (i = 0; i < 3; i++)", "for-3", BlockCategory.Loop, "Repeat 3 times"));
            level1.AvailableBlocks.Add(new CodeBlock("for (i = 0; i < 5; i++)", "for-wrong1", BlockCategory.Loop, "Too many!"));
            level1.AvailableBlocks.Add(new CodeBlock("for (i = 0; i > 3; i++)", "for-wrong2", BlockCategory.Loop, "Wrong condition!"));
            level1.ExpectedTags.Add("for-3");
            levels.Add(level1);

            // Level 2: While it Runs
            var level2 = new LevelData("While it Runs");
            level2.AvailableBlocks.Add(new CodeBlock("while (fuel > 0)", "while-fuel", BlockCategory.Loop, "Run until empty"));
            level2.AvailableBlocks.Add(new CodeBlock("while (fuel < 0)", "while-wrong1", BlockCategory.Loop, "Already empty!"));
            level2.AvailableBlocks.Add(new CodeBlock("while (fuel == 0)", "while-wrong2", BlockCategory.Loop, "Never runs!"));
            level2.ExpectedTags.Add("while-fuel");
            levels.Add(level2);

            // Level 3: Nested Loops
            var level3 = new LevelData("Nested Loops");
            level3.AvailableBlocks.Add(new CodeBlock("for (row = 0; row < 3; row++)", "for-rows", BlockCategory.Loop));
            level3.AvailableBlocks.Add(new CodeBlock("for (col = 0; col < 3; col++)", "for-cols", BlockCategory.Loop));
            level3.AvailableBlocks.Add(new CodeBlock("for (row = 0; row < 9; row++)", "for-wrong1", BlockCategory.Loop));
            level3.AvailableBlocks.Add(new CodeBlock("while (row < 3)", "while-wrong", BlockCategory.Loop));
            level3.ExpectedTags.Add("for-rows");
            level3.ExpectedTags.Add("for-cols");
            levels.Add(level3);

            return levels;
        }
    }
}
