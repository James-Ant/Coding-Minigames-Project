using System;
using System.Collections.Generic;

namespace Coding_Minigames_Project
{
    public static class ConditionalsLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            // --- IF STATEMENTS (4) ---

            var level1 = new LevelData("Rainy Day", "If it's raining outside, what should you do?", "Choose the right item for the weather.");
            level1.SlotLabels.Add("If it's raining =");
            level1.AvailableBlocks.Add(new CodeBlock("Take Umbrella", "ans-umb", BlockCategory.Conditional, "Keeps you dry!"));
            level1.AvailableBlocks.Add(new CodeBlock("Wear Sunglasses", "ans-sun", BlockCategory.Conditional, "Not much sun today..."));
            level1.AvailableBlocks.Add(new CodeBlock("Go Swimming", "ans-swim", BlockCategory.Conditional, "Probably too cold for that."));
            level1.ExpectedTags.Add("ans-umb");
            levels.Add(level1);

            var level2 = new LevelData("Low Battery", "Your phone is about to die!", "What action prevents a dead phone?");
            level2.SlotLabels.Add("If battery < 20% =");
            level2.AvailableBlocks.Add(new CodeBlock("Charge Phone", "ans-charge", BlockCategory.Conditional, "Plug it in!"));
            level2.AvailableBlocks.Add(new CodeBlock("Play Games", "ans-play", BlockCategory.Conditional, "That will kill it faster."));
            level2.AvailableBlocks.Add(new CodeBlock("Turn on Flashlight", "ans-flash", BlockCategory.Conditional, "A waste of battery."));
            level2.ExpectedTags.Add("ans-charge");
            levels.Add(level2);

            var level3 = new LevelData("Thirsty", "Your body needs water.", "Drink something healthy.");
            level3.SlotLabels.Add("If you are thirsty =");
            level3.AvailableBlocks.Add(new CodeBlock("Drink Water", "ans-water", BlockCategory.Conditional, "Stay hydrated!"));
            level3.AvailableBlocks.Add(new CodeBlock("Eat Chips", "ans-chips", BlockCategory.Conditional, "That makes you more thirsty."));
            level3.AvailableBlocks.Add(new CodeBlock("Run 5 Miles", "ans-run", BlockCategory.Conditional, "Terrible idea."));
            level3.ExpectedTags.Add("ans-water");
            levels.Add(level3);

            var level4 = new LevelData("Tired", "It's been a long day.", "Time to rest.");
            level4.SlotLabels.Add("If you are tired =");
            level4.AvailableBlocks.Add(new CodeBlock("Go to Sleep", "ans-sleep", BlockCategory.Conditional, "Goodnight!"));
            level4.AvailableBlocks.Add(new CodeBlock("Drink Coffee", "ans-coffee", BlockCategory.Conditional, "You'll be up all night."));
            level4.AvailableBlocks.Add(new CodeBlock("Watch TV", "ans-tv", BlockCategory.Conditional, "Screen time keeps you awake."));
            level4.ExpectedTags.Add("ans-sleep");
            levels.Add(level4);

            // --- IF / ELSE STATEMENTS (4) ---

            var level5 = new LevelData("Traffic Light", "Handle both the Green and Red light states.", "Green means go, red means stop.");
            level5.SlotLabels.Add("If light is green =");
            level5.SlotLabels.Add("Else =");
            level5.AvailableBlocks.Add(new CodeBlock("Go", "ans-go", BlockCategory.Conditional, "Drive forward"));
            level5.AvailableBlocks.Add(new CodeBlock("Stop", "ans-stop", BlockCategory.Conditional, "Hit the brakes"));
            level5.AvailableBlocks.Add(new CodeBlock("Honk Horn", "ans-honk", BlockCategory.Conditional, "Rude!"));
            level5.ExpectedTags.Add("ans-go");
            level5.ExpectedTags.Add("ans-stop");
            levels.Add(level5);

            var level6 = new LevelData("Password Check", "What happens if the password is correct or incorrect?", "Grant or deny access.");
            level6.SlotLabels.Add("If correct =");
            level6.SlotLabels.Add("Else =");
            level6.AvailableBlocks.Add(new CodeBlock("Login", "ans-login", BlockCategory.Conditional, "Welcome back!"));
            level6.AvailableBlocks.Add(new CodeBlock("Access Denied", "ans-deny", BlockCategory.Conditional, "Wrong password."));
            level6.AvailableBlocks.Add(new CodeBlock("Delete Account", "ans-del", BlockCategory.Conditional, "A bit extreme."));
            level6.ExpectedTags.Add("ans-login");
            level6.ExpectedTags.Add("ans-deny");
            levels.Add(level6);

            var level7 = new LevelData("Store Hours", "Are they open for business?", "Enter if open, leave if closed.");
            level7.SlotLabels.Add("If open =");
            level7.SlotLabels.Add("Else =");
            level7.AvailableBlocks.Add(new CodeBlock("Enter", "ans-enter", BlockCategory.Conditional, "Go inside."));
            level7.AvailableBlocks.Add(new CodeBlock("Come Back Later", "ans-later", BlockCategory.Conditional, "Try again tomorrow."));
            level7.AvailableBlocks.Add(new CodeBlock("Break Window", "ans-break", BlockCategory.Conditional, "That's illegal!"));
            level7.ExpectedTags.Add("ans-enter");
            level7.ExpectedTags.Add("ans-later");
            levels.Add(level7);

            var level8 = new LevelData("Weather", "Dress appropriately for the temperature.", "Coat for cold, t-shirt for warm.");
            level8.SlotLabels.Add("If cold =");
            level8.SlotLabels.Add("Else =");
            level8.AvailableBlocks.Add(new CodeBlock("Wear Coat", "ans-coat", BlockCategory.Conditional, "Stay warm."));
            level8.AvailableBlocks.Add(new CodeBlock("Wear T-Shirt", "ans-shirt", BlockCategory.Conditional, "Enjoy the sun."));
            level8.AvailableBlocks.Add(new CodeBlock("Wear Swimsuit", "ans-swim", BlockCategory.Conditional, "Maybe at the beach."));
            level8.ExpectedTags.Add("ans-coat");
            level8.ExpectedTags.Add("ans-shirt");
            levels.Add(level8);

            // --- IF / ELSE IF / ELSE STATEMENTS (4) ---

            var level9 = new LevelData("School Grades", "Assign a letter grade based on the score.", "A for > 90, B for > 80, C for the rest.");
            level9.SlotLabels.Add("If > 90 =");
            level9.SlotLabels.Add("Else if > 80 =");
            level9.SlotLabels.Add("Else =");
            level9.AvailableBlocks.Add(new CodeBlock("A", "ans-a", BlockCategory.Conditional, "Excellent!"));
            level9.AvailableBlocks.Add(new CodeBlock("B", "ans-b", BlockCategory.Conditional, "Good job."));
            level9.AvailableBlocks.Add(new CodeBlock("C", "ans-c", BlockCategory.Conditional, "Needs improvement."));
            level9.AvailableBlocks.Add(new CodeBlock("F", "ans-f", BlockCategory.Conditional, "Failing."));
            level9.ExpectedTags.Add("ans-a");
            level9.ExpectedTags.Add("ans-b");
            level9.ExpectedTags.Add("ans-c");
            levels.Add(level9);

            var level10 = new LevelData("Movie Ratings", "Determine what rating the movie gets based on age appropriateness.", "PG for < 13, PG-13 for < 17, otherwise R.");
            level10.SlotLabels.Add("If < 13 =");
            level10.SlotLabels.Add("Else if < 17 =");
            level10.SlotLabels.Add("Else =");
            level10.AvailableBlocks.Add(new CodeBlock("PG", "ans-pg", BlockCategory.Conditional, "Parental Guidance"));
            level10.AvailableBlocks.Add(new CodeBlock("PG-13", "ans-pg13", BlockCategory.Conditional, "Teens"));
            level10.AvailableBlocks.Add(new CodeBlock("R", "ans-r", BlockCategory.Conditional, "Restricted"));
            level10.AvailableBlocks.Add(new CodeBlock("G", "ans-g", BlockCategory.Conditional, "General Audience"));
            level10.ExpectedTags.Add("ans-pg");
            level10.ExpectedTags.Add("ans-pg13");
            level10.ExpectedTags.Add("ans-r");
            levels.Add(level10);

            var level11 = new LevelData("Game Difficulty", "Match the difficulty to the player's skill level.", "Beginner -> Easy, Veteran -> Hard, otherwise Medium.");
            level11.SlotLabels.Add("If beginner =");
            level11.SlotLabels.Add("Else if veteran =");
            level11.SlotLabels.Add("Else =");
            level11.AvailableBlocks.Add(new CodeBlock("Easy", "ans-easy", BlockCategory.Conditional, "A gentle start."));
            level11.AvailableBlocks.Add(new CodeBlock("Hard", "ans-hard", BlockCategory.Conditional, "A real challenge."));
            level11.AvailableBlocks.Add(new CodeBlock("Medium", "ans-med", BlockCategory.Conditional, "A balanced experience."));
            level11.ExpectedTags.Add("ans-easy");
            level11.ExpectedTags.Add("ans-hard");
            level11.ExpectedTags.Add("ans-med");
            levels.Add(level11);

            var level12 = new LevelData("Meal Time", "What to eat based on the time of day.", "Morning -> Breakfast, Noon -> Lunch, otherwise Dinner.");
            level12.SlotLabels.Add("If morning =");
            level12.SlotLabels.Add("Else if noon =");
            level12.SlotLabels.Add("Else =");
            level12.AvailableBlocks.Add(new CodeBlock("Breakfast", "ans-brk", BlockCategory.Conditional, "Most important meal!"));
            level12.AvailableBlocks.Add(new CodeBlock("Lunch", "ans-lun", BlockCategory.Conditional, "Midday refuel."));
            level12.AvailableBlocks.Add(new CodeBlock("Dinner", "ans-din", BlockCategory.Conditional, "Evening feast."));
            level12.ExpectedTags.Add("ans-brk");
            level12.ExpectedTags.Add("ans-lun");
            level12.ExpectedTags.Add("ans-din");
            levels.Add(level12);

            return levels;
        }
    }
}
