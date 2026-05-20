using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    // LevelSelectForm shows a grid of level buttons for a single programming topic
    public class LevelSelectForm : Form
    {
        private Label headerLabel;
        private List<Button> levelCards = new List<Button>();
        private string worldName;
        private List<LevelData> levels;

        public LevelSelectForm(string worldName, List<LevelData> levels)
        {
            this.worldName = worldName;
            this.levels = levels;
            InitializeForm();
            CreateLevelButtons();

            this.VisibleChanged += (s, e) => { if (this.Visible) RefreshLevelButtons(); };
            this.Resize += (s, e) => UpdateLayout();
            UpdateLayout();
        }



        private void InitializeForm()
        {
            this.Text = "Select Level";
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(20, 20, 40);
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.F11) ToggleFullscreen();
            };
        }

        private void ToggleFullscreen()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Size = new Size(900, 600);
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void CreateLevelButtons()
        {
            headerLabel = new Label
            {
                Text = $"{worldName} Levels",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            this.Controls.Add(headerLabel);

            RefreshLevelButtons();

            Button btnBack = new Button
            {
                Text = "← Back to Worlds",
                Size = new Size(150, 40),
                Location = new Point(20, 45),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);
        }

        private void RefreshLevelButtons()
        {
            foreach (var btn in levelCards)
            {
                this.Controls.Remove(btn);
            }
            levelCards.Clear();

            for (int i = 0; i < levels.Count; i++)
            {
                AddLevelCard(i, levels[i]);
            }
            UpdateLayout();
        }

        private void AddLevelCard(int index, LevelData level)
        {
            Button card = new Button
            {
                Text = $"{index + 1}\n{level.LevelName}",
                Size = new Size(200, 200),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(30, 30, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            if (ProgressManager.IsCompleted(worldName, level.LevelName))
            {
                card.BackColor = Color.FromArgb(25, 25, 45);
                card.ForeColor = Color.FromArgb(120, 120, 160);
                card.Text += "\n(COMPLETED)";
            }

            card.FlatAppearance.BorderSize = 2;
            card.FlatAppearance.BorderColor = Color.FromArgb(83, 74, 183);

            card.Click += (s, e) =>
            {
                var game = new MainForm(worldName, levels, index);
                game.Show();
                this.Hide();
                game.FormClosed += (s2, args) => this.Show();
            };

            levelCards.Add(card);
            this.Controls.Add(card);
        }

        private void UpdateLayout()
        {
            if (headerLabel == null || levelCards.Count == 0) return;

            int centerX = this.ClientSize.Width / 2;
            
            headerLabel.Size = new Size(this.ClientSize.Width, 80);
            headerLabel.Location = new Point(0, (int)(this.ClientSize.Height * 0.15));

            int spacing = 30;

            int cardsPerRow = Math.Max(1, (this.ClientSize.Width - spacing) / (levelCards[0].Width + spacing));
            if (cardsPerRow > levelCards.Count) cardsPerRow = levelCards.Count;
            
            int totalWidth = (cardsPerRow * levelCards[0].Width) + ((cardsPerRow - 1) * spacing);
            int startX = centerX - (totalWidth / 2);
            int startY = headerLabel.Bottom + 60;

            for (int i = 0; i < levelCards.Count; i++)
            {
                int row = i / cardsPerRow;
                int col = i % cardsPerRow;
                
                int x = startX + (col * (levelCards[i].Width + spacing));
                int y = startY + (row * (levelCards[i].Height + spacing));
                
                levelCards[i].Location = new Point(x, y);
            }
        }


    }
}
