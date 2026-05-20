using Coding_Minigames_Project.MiniGames.Maze_Escape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    // WorldSelectForm is the topic/world selection screen where the player picks a programming topic
    // to practice. It extends Form and displays card buttons for each topic.
    // Variables ,Conditionals, Loops, Sorting, and a special Maze Escape standalone game.
    public class WorldSelectForm : Form
    {
        private Label headerLabel;
        private List<Button> worldCards = new List<Button>();

        public WorldSelectForm()
        {
            InitializeForm();
            CreateWorldButtons();
            this.Resize += (s, e) => UpdateLayout();
            UpdateLayout();
        }



        private void InitializeForm()
        {
            this.Text = "Select Your World";
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

        private void CreateWorldButtons()
        {
            headerLabel = new Label
            {
                Text = "Choose a Programming Topic",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            this.Controls.Add(headerLabel);

            AddWorldCard("VARIABLES", VariableLevels.GetLevels(), Color.FromArgb(46, 204, 113));
            AddWorldCard("CONDITIONALS", ConditionalsLevels.GetLevels(), Color.FromArgb(52, 152, 219));
            AddWorldCard("LOOPS", LoopsLevels.GetLevels(), Color.FromArgb(155, 89, 182));
            AddWorldCard("SORTING", SortingLevels.GetLevels(), Color.FromArgb(241, 196, 15));

            Button mazeCard = new Button
            {
                Text = "MAZE ESCAPE",
                Size = new Size(250, 320),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(30, 30, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            mazeCard.FlatAppearance.BorderSize = 2;
            mazeCard.FlatAppearance.BorderColor = Color.FromArgb(231, 76, 60);
            mazeCard.Click += (s, e) =>
            {
                var maze = new MazeForm();
                maze.Show();
                this.Hide();
                maze.FormClosed += (s2, args) => this.Show();
            };
            worldCards.Add(mazeCard);
            this.Controls.Add(mazeCard);

            Button btnBack = new Button
            {
                Text = "← Back to Menu",
                Size = new Size(150, 40),
                Location = new Point(20, 45),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);
        }

        private void AddWorldCard(string name, List<LevelData> levels, Color accentColor)
        {
            Button card = new Button
            {
                Text = name,
                Size = new Size(250, 320),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(30, 30, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            card.FlatAppearance.BorderSize = 2;
            card.FlatAppearance.BorderColor = accentColor;

            card.Click += (s, e) =>
            {
                var levelSelect = new LevelSelectForm(name, levels);
                levelSelect.Show();
                this.Hide();
                levelSelect.FormClosed += (s2, args) => this.Show();
            };

            worldCards.Add(card);
            this.Controls.Add(card);
        }

        private void UpdateLayout()
        {
            if (headerLabel == null || worldCards.Count == 0) return;

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            headerLabel.Size = new Size(this.ClientSize.Width, 80);
            headerLabel.Location = new Point(0, (int)(this.ClientSize.Height * 0.15));

            int spacing = 40;
            int totalWidth = (worldCards.Count * worldCards[0].Width) + ((worldCards.Count - 1) * spacing);
            int startX = centerX - (totalWidth / 2);
            int startY = headerLabel.Bottom + 60;

            for (int i = 0; i < worldCards.Count; i++)
            {
                worldCards[i].Location = new Point(startX + (i * (worldCards[i].Width + spacing)), startY);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WorldSelectForm
            // 
            this.ClientSize = new System.Drawing.Size(1411, 646);
            this.Name = "WorldSelectForm";
            this.ResumeLayout(false);

        }
    }
}
