using Coding_Minigames_Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    public class MainForm : Form
    {
        private Panel gamePanel;
        private Button btnHint;
        private Label hintLabel;
        private GameLevelManager levelManager;
        private string worldName;
        private int currentLevelIndex = 0;
        private List<LevelData> worldLevels;

        public MainForm(string worldName, List<LevelData> worldLevels, int startIndex = 0)
        {
            this.worldName = worldName;
            this.worldLevels = worldLevels;
            currentLevelIndex = startIndex;
            InitializeForm();
            LoadCurrentLevel();
        }

        private void InitializeForm()
        {
            Text = "Coding Minigames";
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(30, 30, 30);
            KeyPreview = true;
            KeyDown += (s, e) => {
                if (e.KeyCode == Keys.F11) ToggleFullscreen();
            };
            this.Resize += (s, e) => {
                if (levelManager != null && gamePanel != null)
                    levelManager.Relayout(gamePanel);
            };

            // game panel
            gamePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // sidebar panel
            Panel sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.FromArgb(40, 40, 50),
                Padding = new Padding(10)
            };

            Button btnMainMenu = new Button
            {
                Text = "⌂ Main Menu",
                Dock = DockStyle.Bottom,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(120, 60, 60),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnMainMenu.FlatAppearance.BorderSize = 0;
            btnMainMenu.Click += (s, e) => {
                var mainMenu = Application.OpenForms.OfType<MainMenu>().FirstOrDefault();
                if (mainMenu != null) mainMenu.Show();
                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (form != mainMenu && form != this) form.Hide();
                }
                this.Close();
            };

            Panel spacerPanel = new Panel { Dock = DockStyle.Bottom, Height = 10 };

            Button btnLevelSelect = new Button
            {
                Text = "← Level Select",
                Dock = DockStyle.Bottom,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(60, 60, 80),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLevelSelect.FlatAppearance.BorderSize = 0;
            btnLevelSelect.Click += (s, e) => this.Close();

            btnHint = new Button
            {
                Text = "💡 Show Hint",
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(60, 100, 180),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnHint.FlatAppearance.BorderSize = 0;

            hintLabel = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 150,
                TextAlign = ContentAlignment.TopLeft,
                ForeColor = Color.FromArgb(220, 210, 255),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Text = "",
                Visible = false,
                Padding = new Padding(0, 15, 0, 0)
            };

            btnHint.Click += (s, e) => {
                hintLabel.Visible = !hintLabel.Visible;
                btnHint.Text = hintLabel.Visible ? "💡 Hide Hint" : "💡 Show Hint";
            };

            sidebarPanel.Controls.Add(hintLabel);
            sidebarPanel.Controls.Add(btnHint);
            sidebarPanel.Controls.Add(btnLevelSelect);
            sidebarPanel.Controls.Add(spacerPanel);
            sidebarPanel.Controls.Add(btnMainMenu);

            Controls.Add(gamePanel);
            Controls.Add(sidebarPanel);
            
            // Add a hint button to the game panel or handle it via shortcut
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.H) btnHint.PerformClick();
            };

            levelManager = new GameLevelManager();
            levelManager.OnLevelComplete = OnLevelComplete;
        }

        private void ToggleFullscreen()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Size = new Size(1000, 700);
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void LoadCurrentLevel()
        {
            if (currentLevelIndex >= worldLevels.Count)
            {
                ShowGameComplete();
                return;
            }

            var level = worldLevels[currentLevelIndex];

            Text = level.Description != ""
                ? $"Coding Minigames — {level.LevelName}: {level.Description}"
                : $"Coding Minigames — {level.LevelName}";

            hintLabel.Text = level.Hint;
            hintLabel.Visible = false;
            if (btnHint != null) btnHint.Text = "💡 Show Hint";
            gamePanel.BackColor = level.BackgroundColor;

            levelManager.LoadLevel(level, gamePanel);
        }



        private void OnLevelComplete()
        {
            var currentLevel = worldLevels[currentLevelIndex];
            ProgressManager.MarkAsCompleted(worldName, currentLevel.LevelName);

            if (currentLevelIndex + 1 >= worldLevels.Count)
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
