using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Minigames_Project
{
    public static class VariableLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            var level1 = new LevelData("Store the Value");
            level1.AvailableBlocks.Add(new CodeBlock("int x = 5", "int-assign", BlockCategory.Variable, "Store the number 5"));
            level1.AvailableBlocks.Add(new CodeBlock("string x = 5", "string-assign", BlockCategory.Variable, "Wrong type!"));
            level1.AvailableBlocks.Add(new CodeBlock("int x = 0", "int-wrong", BlockCategory.Variable, "Wrong value!"));
            level1.ExpectedTags.Add("int-assign");
            levels.Add(level1);

            var level2 = new LevelData("Right Type, Wrong Type");
            level2.AvailableBlocks.Add(new CodeBlock("int score = 10", "int-score", BlockCategory.Variable));
            level2.AvailableBlocks.Add(new CodeBlock("string name = \"Al\"", "string-name", BlockCategory.Variable));
            level2.AvailableBlocks.Add(new CodeBlock("bool isAlive = true", "bool-alive", BlockCategory.Variable));
            level2.AvailableBlocks.Add(new CodeBlock("int name = \"Al\"", "int-wrong", BlockCategory.Variable));
            level2.AvailableBlocks.Add(new CodeBlock("string score = 10", "string-wrong", BlockCategory.Variable));
            level2.ExpectedTags.Add("int-score");
            level2.ExpectedTags.Add("string-name");
            level2.ExpectedTags.Add("bool-alive");
            levels.Add(level2);

            var level3 = new LevelData("Reassign");
            level3.AvailableBlocks.Add(new CodeBlock("x = 20", "reassign-correct", BlockCategory.Variable, "Update x to 20"));
            level3.AvailableBlocks.Add(new CodeBlock("x = 10", "reassign-wrong1", BlockCategory.Variable, "That's the old value!"));
            level3.AvailableBlocks.Add(new CodeBlock("int x = 20", "reassign-wrong2", BlockCategory.Variable, "Can't redeclare!"));
            level3.ExpectedTags.Add("reassign-correct");
            levels.Add(level3);

            return levels;
        }
    }
}
