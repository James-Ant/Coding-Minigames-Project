using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Coding_Minigames_Project
{
    // MusicManager is a static class that handles background music playback for the game
    public static class MusicManager
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

        public static bool IsMusicEnabled { get; set; } = true;

        private static bool isPlaying = false;

        private static string currentFile;

        public static void Play(string fileName)
        {
            if (!File.Exists(fileName)) return;

            currentFile = fileName;
            if (IsMusicEnabled)
            {
                StartPlaying();
            }
        }

        private static void StartPlaying()
        {
            if (isPlaying) return;
            mciSendString($"open \"{currentFile}\" type mpegvideo alias bgm", null, 0, IntPtr.Zero);
            mciSendString("play bgm repeat", null, 0, IntPtr.Zero);
            isPlaying = true;
        }

        public static void Stop()
        {
            mciSendString("stop bgm", null, 0, IntPtr.Zero);
            mciSendString("close bgm", null, 0, IntPtr.Zero);
            isPlaying = false;
        }

        public static void ToggleMusic()
        {
            IsMusicEnabled = !IsMusicEnabled;
            if (IsMusicEnabled)
            {
                if (!string.IsNullOrEmpty(currentFile))
                {
                    StartPlaying();
                }
            }
            else
            {
                Stop();
            }
        }
    }
}
