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
        private Panel topBar;
        private Panel hintPanel;
        private Label descriptionLabel;
        private Label hintLabel;
        private Button hintButton;
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

            // top bar
            topBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(40, 40, 40),
                Padding = new Padding(16, 0, 16, 0)
            };

            descriptionLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 12),
                Text = ""
            };

            hintButton = new Button
            {
                Text = "💡 Hint",
                Dock = DockStyle.Right,
                Width = 90,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(83, 74, 183),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = false
            };
            hintButton.FlatAppearance.BorderSize = 0;
            hintButton.Click += OnHintClicked;

            topBar.Controls.Add(descriptionLabel);
            topBar.Controls.Add(hintButton);

            // hint panel
            hintPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(60, 50, 120),
                Padding = new Padding(16, 0, 16, 0),
                Visible = false
            };

            hintLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(220, 210, 255),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Text = ""
            };

            hintPanel.Controls.Add(hintLabel);

            // game panel
            gamePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // order matters for DockStyle
            Controls.Add(gamePanel);
            Controls.Add(hintPanel);
            Controls.Add(topBar);

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

            var level = levels[currentLevelIndex];

            descriptionLabel.Text = level.Description != ""
                ? level.Description
                : level.LevelName;

            hintLabel.Text = level.Hint;
            hintPanel.Visible = false;
            hintButton.Visible = level.Hint != "";
            gamePanel.BackColor = level.BackgroundColor;

            levelManager.LoadLevel(level, gamePanel);
            Text = $"Coding Minigames — {level.LevelName}";
        }

        private void OnHintClicked(object sender, EventArgs e)
        {
            hintPanel.Visible = !hintPanel.Visible;
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
