using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Minigames_Project
{
    public class LevelData
    {
        public string LevelName { get; set; }
        public List<CodeBlock> AvailableBlocks { get; set; }
        public List<string> ExpectedTags { get; set; }

        public LevelData(string levelName)
        {
            LevelName = levelName;
            AvailableBlocks = new List<CodeBlock>();
            ExpectedTags = new List<string>();
        }
    }
}
