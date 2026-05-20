using System.Collections.Generic;

namespace Coding_Minigames_Project
{
    // SortingLevels is a static class that defines all 8 puzzle levels for the "Sorting" minigame topic.
    // These are more advanced levels where each item has multiple attribute and the player must sort
    // them into the correct condition-based slots.
    // Each level is a LevelData with a title, description, hint, slot labels showing the conditions,
    // available CodeBlock answer blocks with multi-line display text describing item attributes
    public static class SortingLevels
    {
        public static List<LevelData> GetLevels()
        {
            var levels = new List<LevelData>();

            var level1 = new LevelData("Shipping Sorter", "Sort the packages based on their exact attributes.", "Match the item properties to the condition.");
            level1.SlotLabels.Add("weight > 10 =");
            level1.SlotLabels.Add("fragile == true =");
            level1.SlotLabels.Add("weight < 5 && fragile == false =");

            var b1_1 = new CodeBlock("Bowling Ball\nWeight: 15\nFragile: No", "ans-heavy", BlockCategory.Variable);
            b1_1.SizeOverride = new System.Drawing.Size(160, 80);
            var b1_2 = new CodeBlock("Wine Glass\nWeight: 1\nFragile: Yes", "ans-fragile", BlockCategory.Variable);
            b1_2.SizeOverride = new System.Drawing.Size(160, 80);
            var b1_3 = new CodeBlock("T-Shirt\nWeight: 1\nFragile: No", "ans-normal", BlockCategory.Variable);
            b1_3.SizeOverride = new System.Drawing.Size(160, 80);

            level1.AvailableBlocks.Add(b1_1);
            level1.AvailableBlocks.Add(b1_2);
            level1.AvailableBlocks.Add(b1_3);
            level1.ExpectedTags.Add("ans-heavy");
            level1.ExpectedTags.Add("ans-fragile");
            level1.ExpectedTags.Add("ans-normal");
            levels.Add(level1);

            var level2 = new LevelData("Access Control", "Grant the correct access level by evaluating conditions.", "Check both age and subscription status.");
            level2.SlotLabels.Add("age < 13 =");
            level2.SlotLabels.Add("sub == \"Pro\" =");
            level2.SlotLabels.Add("age >= 13 && sub == \"Free\" =");

            var b2_1 = new CodeBlock("Jimmy\nAge: 12\nSub: Free", "ans-child", BlockCategory.Variable);
            b2_1.SizeOverride = new System.Drawing.Size(160, 80);
            var b2_2 = new CodeBlock("Alice\nAge: 25\nSub: Pro", "ans-pro", BlockCategory.Variable);
            b2_2.SizeOverride = new System.Drawing.Size(160, 80);
            var b2_3 = new CodeBlock("Bob\nAge: 30\nSub: Free", "ans-free", BlockCategory.Variable);
            b2_3.SizeOverride = new System.Drawing.Size(160, 80);

            level2.AvailableBlocks.Add(b2_1);
            level2.AvailableBlocks.Add(b2_2);
            level2.AvailableBlocks.Add(b2_3);
            level2.ExpectedTags.Add("ans-child");
            level2.ExpectedTags.Add("ans-pro");
            level2.ExpectedTags.Add("ans-free");
            levels.Add(level2);

            var level3 = new LevelData("Matter State", "Sort these H2O samples by their physical state conditions.", "Water freezes at 0 and boils at 100.");
            level3.SlotLabels.Add("temp <= 0 =");
            level3.SlotLabels.Add("temp >= 100 =");
            level3.SlotLabels.Add("temp > 0 && temp < 100 =");

            var b3_1 = new CodeBlock("Ice Block\nTemp: -5°C", "ans-solid", BlockCategory.Variable);
            b3_1.SizeOverride = new System.Drawing.Size(150, 70);
            var b3_2 = new CodeBlock("Steam\nTemp: 110°C", "ans-gas", BlockCategory.Variable);
            b3_2.SizeOverride = new System.Drawing.Size(150, 70);
            var b3_3 = new CodeBlock("Tap Water\nTemp: 20°C", "ans-liquid", BlockCategory.Variable);
            b3_3.SizeOverride = new System.Drawing.Size(150, 70);

            level3.AvailableBlocks.Add(b3_1);
            level3.AvailableBlocks.Add(b3_2);
            level3.AvailableBlocks.Add(b3_3);
            level3.ExpectedTags.Add("ans-solid");
            level3.ExpectedTags.Add("ans-gas");
            level3.ExpectedTags.Add("ans-liquid");
            levels.Add(level3);

            var level4 = new LevelData("Firewall Router", "Route the incoming network packets.", "Look at both the port and the protocol.");
            level4.SlotLabels.Add("port == 443 =");
            level4.SlotLabels.Add("proto == \"UDP\" =");
            level4.SlotLabels.Add("port == 9999 =");

            var b4_1 = new CodeBlock("Web Traffic\nPort: 443\nProto: TCP", "ans-web", BlockCategory.Variable);
            b4_1.SizeOverride = new System.Drawing.Size(170, 80);
            var b4_2 = new CodeBlock("Game Sync\nPort: 27015\nProto: UDP", "ans-game", BlockCategory.Variable);
            b4_2.SizeOverride = new System.Drawing.Size(170, 80);
            var b4_3 = new CodeBlock("Unknown\nPort: 9999\nProto: TCP", "ans-drop", BlockCategory.Variable);
            b4_3.SizeOverride = new System.Drawing.Size(170, 80);

            level4.AvailableBlocks.Add(b4_1);
            level4.AvailableBlocks.Add(b4_2);
            level4.AvailableBlocks.Add(b4_3);
            level4.ExpectedTags.Add("ans-web");
            level4.ExpectedTags.Add("ans-game");
            level4.ExpectedTags.Add("ans-drop");
            levels.Add(level4);

            var level5 = new LevelData("File System", "Sort files into correct directories.", "Check file extension and size in MB.");
            level5.SlotLabels.Add("ext == \".exe\" =");
            level5.SlotLabels.Add("size > 1000 =");
            level5.SlotLabels.Add("ext == \".txt\" =");

            var b5_1 = new CodeBlock("Game Installer\nExt: .exe\nSize: 50", "ans-exe", BlockCategory.Variable);
            b5_1.SizeOverride = new System.Drawing.Size(170, 80);
            var b5_2 = new CodeBlock("Holiday Movie\nExt: .mp4\nSize: 2000", "ans-large", BlockCategory.Variable);
            b5_2.SizeOverride = new System.Drawing.Size(170, 80);
            var b5_3 = new CodeBlock("Shopping List\nExt: .txt\nSize: 1", "ans-text", BlockCategory.Variable);
            b5_3.SizeOverride = new System.Drawing.Size(170, 80);

            level5.AvailableBlocks.Add(b5_1);
            level5.AvailableBlocks.Add(b5_2);
            level5.AvailableBlocks.Add(b5_3);
            level5.ExpectedTags.Add("ans-exe");
            level5.ExpectedTags.Add("ans-large");
            level5.ExpectedTags.Add("ans-text");
            levels.Add(level5);

            var level6 = new LevelData("E-commerce Orders", "Sort customer orders based on price and subscription.", "Pay attention to compound conditions.");
            level6.SlotLabels.Add("price > 1000 =");
            level6.SlotLabels.Add("prime == true && price < 20 =");
            level6.SlotLabels.Add("prime == false && price < 50 =");

            var b6_1 = new CodeBlock("Gaming Laptop\nPrice: 1500\nPrime: Yes", "ans-expensive", BlockCategory.Variable);
            b6_1.SizeOverride = new System.Drawing.Size(180, 80);
            var b6_2 = new CodeBlock("Notebook\nPrice: 15\nPrime: Yes", "ans-cheap-prime", BlockCategory.Variable);
            b6_2.SizeOverride = new System.Drawing.Size(180, 80);
            var b6_3 = new CodeBlock("Coffee Mug\nPrice: 10\nPrime: No", "ans-cheap-noprime", BlockCategory.Variable);
            b6_3.SizeOverride = new System.Drawing.Size(180, 80);

            level6.AvailableBlocks.Add(b6_1);
            level6.AvailableBlocks.Add(b6_2);
            level6.AvailableBlocks.Add(b6_3);
            level6.ExpectedTags.Add("ans-expensive");
            level6.ExpectedTags.Add("ans-cheap-prime");
            level6.ExpectedTags.Add("ans-cheap-noprime");
            levels.Add(level6);

            var level7 = new LevelData("RPG Inventory", "Organize your loot by type and rarity.", "Match the exact string values.");
            level7.SlotLabels.Add("type == \"Weapon\" && rarity == \"Epic\" =");
            level7.SlotLabels.Add("type == \"Potion\" =");
            level7.SlotLabels.Add("rarity == \"Legendary\" =");

            var b7_1 = new CodeBlock("Frost Sword\nType: Weapon\nRarity: Epic", "ans-epicwep", BlockCategory.Variable);
            b7_1.SizeOverride = new System.Drawing.Size(190, 80);
            var b7_2 = new CodeBlock("Health Flask\nType: Potion\nRarity: Common", "ans-potion", BlockCategory.Variable);
            b7_2.SizeOverride = new System.Drawing.Size(190, 80);
            var b7_3 = new CodeBlock("Dragon Ring\nType: Armor\nRarity: Legendary", "ans-legendary", BlockCategory.Variable);
            b7_3.SizeOverride = new System.Drawing.Size(190, 80);

            level7.AvailableBlocks.Add(b7_1);
            level7.AvailableBlocks.Add(b7_2);
            level7.AvailableBlocks.Add(b7_3);
            level7.ExpectedTags.Add("ans-epicwep");
            level7.ExpectedTags.Add("ans-potion");
            level7.ExpectedTags.Add("ans-legendary");
            levels.Add(level7);

            var level8 = new LevelData("Smart Home", "Route commands to the appropriate devices.", "Check the device type and its current status.");
            level8.SlotLabels.Add("device == \"Light\" && status == \"On\" =");
            level8.SlotLabels.Add("device == \"Thermostat\" =");
            level8.SlotLabels.Add("status == \"Error\" =");

            var b8_1 = new CodeBlock("Living Room\nDevice: Light\nStatus: On", "ans-lighton", BlockCategory.Variable);
            b8_1.SizeOverride = new System.Drawing.Size(190, 80);
            var b8_2 = new CodeBlock("Hallway\nDevice: Thermostat\nStatus: Off", "ans-thermo", BlockCategory.Variable);
            b8_2.SizeOverride = new System.Drawing.Size(190, 80);
            var b8_3 = new CodeBlock("Garage Door\nDevice: Motor\nStatus: Error", "ans-error", BlockCategory.Variable);
            b8_3.SizeOverride = new System.Drawing.Size(190, 80);

            level8.AvailableBlocks.Add(b8_1);
            level8.AvailableBlocks.Add(b8_2);
            level8.AvailableBlocks.Add(b8_3);
            level8.ExpectedTags.Add("ans-lighton");
            level8.ExpectedTags.Add("ans-thermo");
            level8.ExpectedTags.Add("ans-error");
            levels.Add(level8);

            return levels;
        }
    }
}
