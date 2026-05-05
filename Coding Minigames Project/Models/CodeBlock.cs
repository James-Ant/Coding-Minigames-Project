using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Minigames_Project
{
    public class CodeBlock
    {
        public string DisplayText { get; set; }
        public string Tooltip { get; set; }
        public BlockCategory Category { get; set; }
        public string Tag { get; set; }

        public System.Drawing.Color? ColorOverride { get; set; } = null;
        public System.Drawing.Size? SizeOverride { get; set; } = null;

        public CodeBlock(string displayText, string tag, BlockCategory category, string tooltip = "")
        {
            DisplayText = displayText;
            Tag = tag;
            Category = category;
            Tooltip = tooltip;
        }
    }
}
