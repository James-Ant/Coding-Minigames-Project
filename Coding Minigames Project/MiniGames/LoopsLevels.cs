using System;
using System.Collections.Generic;

namespace Coding_Minigames_Project
{
    public static class LoopsLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            // --- FOR LOOPS (6) ---

            var level1 = new LevelData("Taking Steps", "Walk across the room.", "How many steps to take?");
            level1.SlotLabels.Add("Action:");
            level1.SlotLabels.Add("Count:");
            level1.AvailableBlocks.Add(new CodeBlock("Take Step", "ans-act", BlockCategory.Loop, "Move forward"));
            level1.AvailableBlocks.Add(new CodeBlock("5", "ans-count", BlockCategory.Loop, "Number of steps"));
            level1.AvailableBlocks.Add(new CodeBlock("Jump", "ans-wrong1", BlockCategory.Loop, "Wrong action"));
            level1.ExpectedTags.Add("ans-act");
            level1.ExpectedTags.Add("ans-count");
            levels.Add(level1);

            var level2 = new LevelData("Watering Plants", "Water all the plants in the garden.", "3 plants need water.");
            level2.SlotLabels.Add("Action:");
            level2.SlotLabels.Add("Count:");
            level2.AvailableBlocks.Add(new CodeBlock("Water Plant", "ans-act", BlockCategory.Loop, "Give water"));
            level2.AvailableBlocks.Add(new CodeBlock("3", "ans-count", BlockCategory.Loop, "3 plants"));
            level2.AvailableBlocks.Add(new CodeBlock("Pick Flower", "ans-wrong1", BlockCategory.Loop, "Don't pick them!"));
            level2.ExpectedTags.Add("ans-act");
            level2.ExpectedTags.Add("ans-count");
            levels.Add(level2);

            var level3 = new LevelData("Baking Cookies", "Bake a dozen cookies.", "A dozen is 12.");
            level3.SlotLabels.Add("Action:");
            level3.SlotLabels.Add("Count:");
            level3.AvailableBlocks.Add(new CodeBlock("Bake Cookie", "ans-act", BlockCategory.Loop, "Put in oven"));
            level3.AvailableBlocks.Add(new CodeBlock("12", "ans-count", BlockCategory.Loop, "A dozen"));
            level3.AvailableBlocks.Add(new CodeBlock("Eat Cookie", "ans-wrong1", BlockCategory.Loop, "Wait until they are baked!"));
            level3.ExpectedTags.Add("ans-act");
            level3.ExpectedTags.Add("ans-count");
            levels.Add(level3);

            var level4 = new LevelData("Washing Dishes", "Clean up after dinner.", "There are 8 dirty plates.");
            level4.SlotLabels.Add("Action:");
            level4.SlotLabels.Add("Count:");
            level4.AvailableBlocks.Add(new CodeBlock("Wash Dish", "ans-act", BlockCategory.Loop, "Scrub scrub"));
            level4.AvailableBlocks.Add(new CodeBlock("8", "ans-count", BlockCategory.Loop, "8 plates"));
            level4.AvailableBlocks.Add(new CodeBlock("Break Dish", "ans-wrong1", BlockCategory.Loop, "Oops!"));
            level4.ExpectedTags.Add("ans-act");
            level4.ExpectedTags.Add("ans-count");
            levels.Add(level4);

            var level5 = new LevelData("Brushing Teeth", "Keep your teeth clean.", "Brush 20 times for good measure.");
            level5.SlotLabels.Add("Action:");
            level5.SlotLabels.Add("Count:");
            level5.AvailableBlocks.Add(new CodeBlock("Brush", "ans-act", BlockCategory.Loop, "Up and down"));
            level5.AvailableBlocks.Add(new CodeBlock("20", "ans-count", BlockCategory.Loop, "20 times"));
            level5.AvailableBlocks.Add(new CodeBlock("Floss", "ans-wrong1", BlockCategory.Loop, "Do that later"));
            level5.ExpectedTags.Add("ans-act");
            level5.ExpectedTags.Add("ans-count");
            levels.Add(level5);

            var level6 = new LevelData("Jumping Jacks", "Get some exercise.", "Do 15 jumping jacks.");
            level6.SlotLabels.Add("Action:");
            level6.SlotLabels.Add("Count:");
            level6.AvailableBlocks.Add(new CodeBlock("Jump", "ans-act", BlockCategory.Loop, "Star shape"));
            level6.AvailableBlocks.Add(new CodeBlock("15", "ans-count", BlockCategory.Loop, "15 reps"));
            level6.AvailableBlocks.Add(new CodeBlock("Sit", "ans-wrong1", BlockCategory.Loop, "No resting yet!"));
            level6.ExpectedTags.Add("ans-act");
            level6.ExpectedTags.Add("ans-count");
            levels.Add(level6);

            // --- WHILE LOOPS (6) ---

            var level7 = new LevelData("Eating", "Have dinner until you are satisfied.", "Stop when you're full.");
            level7.SlotLabels.Add("Action:");
            level7.SlotLabels.Add("Until:");
            level7.AvailableBlocks.Add(new CodeBlock("Eat", "ans-act", BlockCategory.Loop, "Chomp"));
            level7.AvailableBlocks.Add(new CodeBlock("Full", "ans-cond", BlockCategory.Loop, "Stomach is full"));
            level7.AvailableBlocks.Add(new CodeBlock("Starving", "ans-wrong1", BlockCategory.Loop, "You're already eating"));
            level7.ExpectedTags.Add("ans-act");
            level7.ExpectedTags.Add("ans-cond");
            levels.Add(level7);

            var level8 = new LevelData("Driving", "Road trip!", "Drive until you reach your destination.");
            level8.SlotLabels.Add("Action:");
            level8.SlotLabels.Add("Until:");
            level8.AvailableBlocks.Add(new CodeBlock("Drive", "ans-act", BlockCategory.Loop, "Vroom"));
            level8.AvailableBlocks.Add(new CodeBlock("Arrive", "ans-cond", BlockCategory.Loop, "Destination reached"));
            level8.AvailableBlocks.Add(new CodeBlock("Lost", "ans-wrong1", BlockCategory.Loop, "Use a map!"));
            level8.ExpectedTags.Add("ans-act");
            level8.ExpectedTags.Add("ans-cond");
            levels.Add(level8);

            var level9 = new LevelData("Cleaning", "The floor is really dirty.", "Keep scrubbing until it shines.");
            level9.SlotLabels.Add("Action:");
            level9.SlotLabels.Add("Until:");
            level9.AvailableBlocks.Add(new CodeBlock("Scrub", "ans-act", BlockCategory.Loop, "Clean hard"));
            level9.AvailableBlocks.Add(new CodeBlock("Clean", "ans-cond", BlockCategory.Loop, "Spotless!"));
            level9.AvailableBlocks.Add(new CodeBlock("Messy", "ans-wrong1", BlockCategory.Loop, "Still dirty"));
            level9.ExpectedTags.Add("ans-act");
            level9.ExpectedTags.Add("ans-cond");
            levels.Add(level9);

            var level10 = new LevelData("Studying", "Prepare for the test.", "Read until you get it.");
            level10.SlotLabels.Add("Action:");
            level10.SlotLabels.Add("Until:");
            level10.AvailableBlocks.Add(new CodeBlock("Read", "ans-act", BlockCategory.Loop, "Study notes"));
            level10.AvailableBlocks.Add(new CodeBlock("Understand", "ans-cond", BlockCategory.Loop, "Aha moment"));
            level10.AvailableBlocks.Add(new CodeBlock("Confused", "ans-wrong1", BlockCategory.Loop, "Keep reading!"));
            level10.ExpectedTags.Add("ans-act");
            level10.ExpectedTags.Add("ans-cond");
            levels.Add(level10);

            var level11 = new LevelData("Running", "Run the race.", "Keep going until the end.");
            level11.SlotLabels.Add("Action:");
            level11.SlotLabels.Add("Until:");
            level11.AvailableBlocks.Add(new CodeBlock("Run", "ans-act", BlockCategory.Loop, "Jog"));
            level11.AvailableBlocks.Add(new CodeBlock("Finish Line", "ans-cond", BlockCategory.Loop, "You did it!"));
            level11.AvailableBlocks.Add(new CodeBlock("Start Line", "ans-wrong1", BlockCategory.Loop, "You just left here."));
            level11.ExpectedTags.Add("ans-act");
            level11.ExpectedTags.Add("ans-cond");
            levels.Add(level11);

            var level12 = new LevelData("Charging", "Phone is dead again.", "Charge until the battery is full.");
            level12.SlotLabels.Add("Action:");
            level12.SlotLabels.Add("Until:");
            level12.AvailableBlocks.Add(new CodeBlock("Charge", "ans-act", BlockCategory.Loop, "Plugged in"));
            level12.AvailableBlocks.Add(new CodeBlock("100%", "ans-cond", BlockCategory.Loop, "Fully charged"));
            level12.AvailableBlocks.Add(new CodeBlock("1%", "ans-wrong1", BlockCategory.Loop, "Still dead"));
            level12.ExpectedTags.Add("ans-act");
            level12.ExpectedTags.Add("ans-cond");
            levels.Add(level12);

            return levels;
        }
    }
}
