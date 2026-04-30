using Coding_Minigames_Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Coding_Minigames_Project
{
    public class MainForm : Form
    {
        private Panel gamePanel;
        private GameLevelManager levelManager;
        private int currentLevelIndex = 0;
        private List<LevelData> levels;

        public MainForm(List<LevelData> worldLevels)
        {
            levels = worldLevels;
            InitializeForm();
            LoadCurrentLevel();
        }

        private void InitializeForm()
        {
            Text = "Coding Minigames";
            Size = new Size(900, 600);
            BackColor = Color.FromArgb(30, 30, 30);
            StartPosition = FormStartPosition.CenterScreen;

            gamePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            Controls.Add(gamePanel);

            levelManager = new GameLevelManager();
            levelManager.OnLevelComplete = OnLevelComplete;
        }

        private void LoadCurrentLevel()
        {
            if (currentLevelIndex >= levels.Count)
            {
                ShowGameComplete();
                return;
            }

            levelManager.LoadLevel(levels[currentLevelIndex], gamePanel);
            Text = $"Coding Minigames — {levels[currentLevelIndex].LevelName}";
        }

        private void OnLevelComplete()
        {
            if (currentLevelIndex + 1 >= levels.Count)
            {
                ShowGameComplete();
                return;
            }

            var result = MessageBox.Show(
                $"Level Complete!\n\nReady for the next one?",
                "Nice work!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.None
            );

            if (result == DialogResult.Yes)
            {
                currentLevelIndex++;
                LoadCurrentLevel();
            }
        }

        private void ShowGameComplete()
        {
            MessageBox.Show(
                "You completed all levels!\n\nYou're a coding legend.",
                "Game Complete!",
                MessageBoxButtons.OK
            );

            Close();
        }
    }
}
