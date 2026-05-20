using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    // MainForm is the main gameplay screen where the player solves coding puzzles by dragging blocks
    // into slots. It extends Form and receives a world name, list of levels, and a starting level index.
    // The form has a left sidebar containing a Show Hint ,a Level Select, and a Main Menu. 
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

            gamePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
            };

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
                ShowGameCompleteOverlay();
                return;
            }

            var level = worldLevels[currentLevelIndex];

            Text = $"Coding Minigames — {level.LevelName}";



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
                ShowGameCompleteOverlay();
                return;
            }

            ShowLevelCompleteOverlay();
        }

        private void ShowLevelCompleteOverlay()
        {
            Panel overlay = new Panel
            {
                Size = new Size(450, 260),
                BackColor = Color.FromArgb(35, 35, 45),
                BorderStyle = BorderStyle.None
            };
            overlay.Location = new Point((gamePanel.Width - overlay.Width) / 2, (gamePanel.Height - overlay.Height) / 2);
            
            overlay.Padding = new Padding(2);
            overlay.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, overlay.ClientRectangle, Color.FromArgb(52, 152, 219), ButtonBorderStyle.Solid);
            };

            Label title = new Label
            {
                Text = "LEVEL COMPLETE!",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.BottomCenter,
                Height = 90
            };
            
            Label msg = new Label
            {
                Text = "Great job! Ready for the next challenge?",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 200, 200),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 60
            };

            Button btnNext = new Button
            {
                Text = "Next Level ➔",
                Size = new Size(180, 50),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Location = new Point((overlay.Width - btnNext.Width) / 2, 170);
            
            btnNext.Click += (s, e) => {
                gamePanel.Controls.Remove(overlay);
                overlay.Dispose();
                currentLevelIndex++;
                LoadCurrentLevel();
            };

            overlay.Controls.Add(btnNext);
            overlay.Controls.Add(msg);
            overlay.Controls.Add(title);
            
            gamePanel.Controls.Add(overlay);
            overlay.BringToFront();
        }

        private void ShowGameCompleteOverlay()
        {
            Panel overlay = new Panel
            {
                Size = new Size(450, 260),
                BackColor = Color.FromArgb(35, 35, 45)
            };
            overlay.Location = new Point((gamePanel.Width - overlay.Width) / 2, (gamePanel.Height - overlay.Height) / 2);
            
            overlay.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, overlay.ClientRectangle, Color.FromArgb(241, 196, 15), ButtonBorderStyle.Solid);
            };

            Label title = new Label
            {
                Text = "WORLD CLEARED!",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.BottomCenter,
                Height = 90
            };
            
            Label msg = new Label
            {
                Text = "You completed all levels!\nYou're a coding legend.",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 200, 200),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 60
            };

            Button btnFinish = new Button
            {
                Text = "Finish",
                Size = new Size(180, 50),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnFinish.FlatAppearance.BorderSize = 0;
            btnFinish.Location = new Point((overlay.Width - btnFinish.Width) / 2, 170);
            
            btnFinish.Click += (s, e) => {
                gamePanel.Controls.Remove(overlay);
                overlay.Dispose();
                Close();
            };

            overlay.Controls.Add(btnFinish);
            overlay.Controls.Add(msg);
            overlay.Controls.Add(title);
            
            gamePanel.Controls.Add(overlay);
            overlay.BringToFront();
        }
    }
}
