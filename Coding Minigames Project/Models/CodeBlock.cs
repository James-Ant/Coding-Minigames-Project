namespace Coding_Minigames_Project
{
    // CodeBlock represents a single draggable puzzle piece in the game. It contains the DisplayText
    // shown on the block (e.g. "int x = 5"), a BlockCategory that determines the block's color
    //and  Tag which is a unique ID used to check if the block was dropped into the correct slot .
    public class CodeBlock
    {
        public string DisplayText { get; set; }

        public BlockCategory Category { get; set; }

        public string Tag { get; set; }

        public System.Drawing.Color? ColorOverride { get; set; } = null;

        public System.Drawing.Size? SizeOverride { get; set; } = null;

        public CodeBlock(string displayText, string tag, BlockCategory category)
        {
            DisplayText = displayText;
            Tag = tag;
            Category = category;
        }
    }
}
