using System.Collections.Generic;

namespace Coding_Minigames_Project
{
    // VariableLevels is a static class that defines all 12 puzzle levels for the "Variables" minigame
    // topic. Levels are grouped into four sections of 3 levels each: General Variables, String Variables,
    // Int Variables, and Bool Variables. Eachlevel is a LevelData with a title, description, hint,
    // slot labels, available CodeBlock answer blocks, and expected tags defining the correct answers.

    public static class VariableLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            var level1 = new LevelData("The Box", "Learn how to store a number in a variable.", "Drag the number into the box.");
            level1.SlotLabels.Add("Score =");
            level1.AvailableBlocks.Add(new CodeBlock("10", "ans-10", BlockCategory.Variable));
            level1.AvailableBlocks.Add(new CodeBlock("\"10\"", "ans-str10", BlockCategory.Variable));
            level1.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable));
            level1.ExpectedTags.Add("ans-10");
            levels.Add(level1);

            var level2 = new LevelData("The Nametag", "Store a piece of text (a string) representing a name.", "Make sure to use quotes for text!");
            level2.SlotLabels.Add("Player Name =");
            level2.AvailableBlocks.Add(new CodeBlock("\"Hero\"", "ans-hero", BlockCategory.Variable));
            level2.AvailableBlocks.Add(new CodeBlock("Hero", "ans-noquote", BlockCategory.Variable));
            level2.AvailableBlocks.Add(new CodeBlock("100", "ans-num", BlockCategory.Variable));
            level2.ExpectedTags.Add("ans-hero");
            levels.Add(level2);

            var level3 = new LevelData("The Light Switch", "Variables can also store a simple true or false.", "A switch can be On (true) or Off (false).");
            level3.SlotLabels.Add("Is On =");
            level3.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable));
            level3.AvailableBlocks.Add(new CodeBlock("\"true\"", "ans-strtrue", BlockCategory.Variable));
            level3.AvailableBlocks.Add(new CodeBlock("1", "ans-one", BlockCategory.Variable));
            level3.ExpectedTags.Add("ans-true");
            levels.Add(level3);

            var level4 = new LevelData("Pet Collar", "Strings are text surrounded by double quotes.", "What should the dog's name be?");
            level4.SlotLabels.Add("Dog's Name =");
            level4.AvailableBlocks.Add(new CodeBlock("\"Rex\"", "ans-rex", BlockCategory.Variable));
            level4.AvailableBlocks.Add(new CodeBlock("Rex", "ans-bad", BlockCategory.Variable));
            level4.AvailableBlocks.Add(new CodeBlock("false", "ans-bool", BlockCategory.Variable));
            level4.ExpectedTags.Add("ans-rex");
            levels.Add(level4);

            var level5 = new LevelData("Secret Door", "Enter the string password to open the door.", "Passwords are just strings.");
            level5.SlotLabels.Add("Password =");
            level5.AvailableBlocks.Add(new CodeBlock("\"OpenSesame\"", "ans-pass", BlockCategory.Variable));
            level5.AvailableBlocks.Add(new CodeBlock("1234", "ans-num", BlockCategory.Variable));
            level5.AvailableBlocks.Add(new CodeBlock("true", "ans-bool", BlockCategory.Variable));
            level5.ExpectedTags.Add("ans-pass");
            levels.Add(level5);

            var level6 = new LevelData("Welcome Sign", "Update the sign's message.", "Text goes in quotes.");
            level6.SlotLabels.Add("Message =");
            level6.AvailableBlocks.Add(new CodeBlock("\"Hello\"", "ans-hello", BlockCategory.Variable));
            level6.AvailableBlocks.Add(new CodeBlock("\"Error\"", "ans-err", BlockCategory.Variable));
            level6.AvailableBlocks.Add(new CodeBlock("404", "ans-404", BlockCategory.Variable));
            level6.ExpectedTags.Add("ans-hello");
            levels.Add(level6);

            var level7 = new LevelData("Apple Basket", "Integers (ints) are whole numbers without decimals.", "How many apples?");
            level7.SlotLabels.Add("Apples =");
            level7.AvailableBlocks.Add(new CodeBlock("5", "ans-5", BlockCategory.Variable));
            level7.AvailableBlocks.Add(new CodeBlock("5.5", "ans-float", BlockCategory.Variable));
            level7.AvailableBlocks.Add(new CodeBlock("\"5\"", "ans-str", BlockCategory.Variable));
            level7.ExpectedTags.Add("ans-5");
            levels.Add(level7);

            var level8 = new LevelData("Speed Limit", "Set the maximum speed.", "Speed limit should be an integer.");
            level8.SlotLabels.Add("Speed =");
            level8.AvailableBlocks.Add(new CodeBlock("60", "ans-60", BlockCategory.Variable));
            level8.AvailableBlocks.Add(new CodeBlock("\"Fast\"", "ans-str", BlockCategory.Variable));
            level8.AvailableBlocks.Add(new CodeBlock("true", "ans-bool", BlockCategory.Variable));
            level8.ExpectedTags.Add("ans-60");
            levels.Add(level8);

            var level9 = new LevelData("Health Bar", "Your player needs health (HP).", "The bigger the better!");
            level9.SlotLabels.Add("HP =");
            level9.AvailableBlocks.Add(new CodeBlock("100", "ans-100", BlockCategory.Variable));
            level9.AvailableBlocks.Add(new CodeBlock("hundred", "ans-0", BlockCategory.Variable));
            level9.AvailableBlocks.Add(new CodeBlock("-10", "ans-neg", BlockCategory.Variable));
            level9.ExpectedTags.Add("ans-100");
            levels.Add(level9);

            var level10 = new LevelData("The Door", "Booleans are used to answer Yes or No questions.", "Is the door closed?");
            level10.SlotLabels.Add("Door is closed =");
            level10.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable));
            level10.AvailableBlocks.Add(new CodeBlock("\"true\"", "ans-str", BlockCategory.Variable));
            level10.AvailableBlocks.Add(new CodeBlock("1", "ans-1", BlockCategory.Variable));
            level10.ExpectedTags.Add("ans-true");
            levels.Add(level10);

            var level11 = new LevelData("The Oven", "Another boolean question. Is the oven hot right now?", "It's off, so it's false.");
            level11.SlotLabels.Add("Oven is hot =");
            level11.AvailableBlocks.Add(new CodeBlock("false", "ans-false", BlockCategory.Variable));
            level11.AvailableBlocks.Add(new CodeBlock("no", "ans-no", BlockCategory.Variable));
            level11.AvailableBlocks.Add(new CodeBlock("\"hot\"", "ans-str", BlockCategory.Variable));
            level11.ExpectedTags.Add("ans-false");
            levels.Add(level11);

            var level12 = new LevelData("Game Over Screen", "The player died!", "If they ran out of HP, it's Game Over!");
            level12.SlotLabels.Add("Game Over =");
            level12.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable));
            level12.AvailableBlocks.Add(new CodeBlock("false", "ans-false", BlockCategory.Variable));
            level12.AvailableBlocks.Add(new CodeBlock("0", "ans-0", BlockCategory.Variable));
            level12.ExpectedTags.Add("ans-true");
            levels.Add(level12);

            return levels;
        }
    }
}
