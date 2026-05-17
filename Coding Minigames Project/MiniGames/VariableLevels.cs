using System;
using System.Collections.Generic;

namespace Coding_Minigames_Project
{
    public static class VariableLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            // --- GENERAL VARIABLES (3) ---

            var level1 = new LevelData("The Box", "Learn how to store a number in a variable.", "Drag the number into the box.");
            level1.SlotLabels.Add("Score =");
            level1.AvailableBlocks.Add(new CodeBlock("10", "ans-10", BlockCategory.Variable, "The number 10"));
            level1.AvailableBlocks.Add(new CodeBlock("\"10\"", "ans-str10", BlockCategory.Variable, "Wait, this is text!"));
            level1.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable, "This is true/false, not a score!"));
            level1.ExpectedTags.Add("ans-10");
            levels.Add(level1);

            var level2 = new LevelData("The Nametag", "Store a piece of text (a string) representing a name.", "Make sure to use quotes for text!");
            level2.SlotLabels.Add("Player Name =");
            level2.AvailableBlocks.Add(new CodeBlock("\"Hero\"", "ans-hero", BlockCategory.Variable, "The name 'Hero'"));
            level2.AvailableBlocks.Add(new CodeBlock("Hero", "ans-noquote", BlockCategory.Variable, "Missing quotes!"));
            level2.AvailableBlocks.Add(new CodeBlock("100", "ans-num", BlockCategory.Variable, "A number?"));
            level2.ExpectedTags.Add("ans-hero");
            levels.Add(level2);

            var level3 = new LevelData("The Light Switch", "Variables can also store a simple true or false.", "A switch can be On (true) or Off (false).");
            level3.SlotLabels.Add("Is On =");
            level3.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable, "Turn it on!"));
            level3.AvailableBlocks.Add(new CodeBlock("\"true\"", "ans-strtrue", BlockCategory.Variable, "That's just the word 'true'"));
            level3.AvailableBlocks.Add(new CodeBlock("1", "ans-one", BlockCategory.Variable, "In C#, use true or false."));
            level3.ExpectedTags.Add("ans-true");
            levels.Add(level3);

            // --- STRING VARIABLES (3) ---

            var level4 = new LevelData("Pet Collar", "Strings are text surrounded by double quotes.", "What should the dog's name be?");
            level4.SlotLabels.Add("Dog's Name =");
            level4.AvailableBlocks.Add(new CodeBlock("\"Rex\"", "ans-rex", BlockCategory.Variable, "Good boy!"));
            level4.AvailableBlocks.Add(new CodeBlock("Rex", "ans-bad", BlockCategory.Variable, "Forgot the quotes."));
            level4.AvailableBlocks.Add(new CodeBlock("false", "ans-bool", BlockCategory.Variable, "Dogs aren't booleans."));
            level4.ExpectedTags.Add("ans-rex");
            levels.Add(level4);

            var level5 = new LevelData("Secret Door", "Enter the string password to open the door.", "Passwords are just strings.");
            level5.SlotLabels.Add("Password =");
            level5.AvailableBlocks.Add(new CodeBlock("\"OpenSesame\"", "ans-pass", BlockCategory.Variable, "The secret passcode"));
            level5.AvailableBlocks.Add(new CodeBlock("1234", "ans-num", BlockCategory.Variable, "Too simple of a number password"));
            level5.AvailableBlocks.Add(new CodeBlock("true", "ans-bool", BlockCategory.Variable, "Not a valid password"));
            level5.ExpectedTags.Add("ans-pass");
            levels.Add(level5);

            var level6 = new LevelData("Welcome Sign", "Update the sign's message.", "Text goes in quotes.");
            level6.SlotLabels.Add("Message =");
            level6.AvailableBlocks.Add(new CodeBlock("\"Hello\"", "ans-hello", BlockCategory.Variable, "A friendly greeting"));
            level6.AvailableBlocks.Add(new CodeBlock("\"Error\"", "ans-err", BlockCategory.Variable, "Don't be rude"));
            level6.AvailableBlocks.Add(new CodeBlock("404", "ans-404", BlockCategory.Variable, "Not found"));
            level6.ExpectedTags.Add("ans-hello");
            levels.Add(level6);

            // --- INT VARIABLES (3) ---

            var level7 = new LevelData("Apple Basket", "Integers (ints) are whole numbers without decimals.", "How many apples?");
            level7.SlotLabels.Add("Apples =");
            level7.AvailableBlocks.Add(new CodeBlock("5", "ans-5", BlockCategory.Variable, "Five whole apples"));
            level7.AvailableBlocks.Add(new CodeBlock("5.5", "ans-float", BlockCategory.Variable, "Half an apple? Ints are whole numbers!"));
            level7.AvailableBlocks.Add(new CodeBlock("\"5\"", "ans-str", BlockCategory.Variable, "That's a string, not a number"));
            level7.ExpectedTags.Add("ans-5");
            levels.Add(level7);

            var level8 = new LevelData("Speed Limit", "Set the maximum speed.", "Speed limit should be an integer.");
            level8.SlotLabels.Add("Speed =");
            level8.AvailableBlocks.Add(new CodeBlock("60", "ans-60", BlockCategory.Variable, "Standard limit"));
            level8.AvailableBlocks.Add(new CodeBlock("\"Fast\"", "ans-str", BlockCategory.Variable, "Needs a number"));
            level8.AvailableBlocks.Add(new CodeBlock("true", "ans-bool", BlockCategory.Variable, "You can't go 'true' mph"));
            level8.ExpectedTags.Add("ans-60");
            levels.Add(level8);

            var level9 = new LevelData("Health Bar", "Your player needs health (HP).", "Set your HP to max!");
            level9.SlotLabels.Add("HP =");
            level9.AvailableBlocks.Add(new CodeBlock("100", "ans-100", BlockCategory.Variable, "Full health"));
            level9.AvailableBlocks.Add(new CodeBlock("0", "ans-0", BlockCategory.Variable, "You'd be dead!"));
            level9.AvailableBlocks.Add(new CodeBlock("-10", "ans-neg", BlockCategory.Variable, "Negative health?"));
            level9.ExpectedTags.Add("ans-100");
            levels.Add(level9);

            // --- BOOL VARIABLES (3) ---

            var level10 = new LevelData("The Door", "Booleans are used to answer Yes or No questions.", "Is the door closed?");
            level10.SlotLabels.Add("Door is closed =");
            level10.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable, "Yes, it is closed"));
            level10.AvailableBlocks.Add(new CodeBlock("\"true\"", "ans-str", BlockCategory.Variable, "Quotes make it a string"));
            level10.AvailableBlocks.Add(new CodeBlock("1", "ans-1", BlockCategory.Variable, "Use true/false"));
            level10.ExpectedTags.Add("ans-true");
            levels.Add(level10);

            var level11 = new LevelData("The Oven", "Another boolean question. Is the oven hot right now?", "It's off, so it's false.");
            level11.SlotLabels.Add("Oven is hot =");
            level11.AvailableBlocks.Add(new CodeBlock("false", "ans-false", BlockCategory.Variable, "No, it is cool"));
            level11.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable, "Careful!"));
            level11.AvailableBlocks.Add(new CodeBlock("\"hot\"", "ans-str", BlockCategory.Variable, "That's a string"));
            level11.ExpectedTags.Add("ans-false");
            levels.Add(level11);

            var level12 = new LevelData("Game Over Screen", "Did the player lose?", "If they ran out of HP, it's Game Over!");
            level12.SlotLabels.Add("Game Over =");
            level12.AvailableBlocks.Add(new CodeBlock("true", "ans-true", BlockCategory.Variable, "Game Over, man!"));
            level12.AvailableBlocks.Add(new CodeBlock("false", "ans-false", BlockCategory.Variable, "Keep playing"));
            level12.AvailableBlocks.Add(new CodeBlock("0", "ans-0", BlockCategory.Variable, "Needs a boolean"));
            level12.ExpectedTags.Add("ans-true");
            levels.Add(level12);

            return levels;
        }
    }
}
