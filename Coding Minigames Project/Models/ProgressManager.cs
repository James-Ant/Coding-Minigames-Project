using System;
using System.Collections.Generic;

namespace Coding_Minigames_Project
{
    public static class ProgressManager
    {
        private static HashSet<string> completedLevels = new HashSet<string>();

        public static void MarkAsCompleted(string worldName, string levelName)
        {
            completedLevels.Add($"{worldName}_{levelName}");
        }

        public static bool IsCompleted(string worldName, string levelName)
        {
            return completedLevels.Contains($"{worldName}_{levelName}");
        }
    }
}
