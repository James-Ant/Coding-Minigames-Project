using System;
using System.Drawing;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    // MainMenu is the first screen the player sees when the app launches. It extends Form and displays
    // the game title and three buttons. Play, Music toggle, and Quit.
    public class MainMenu : Form
    {
        private Label titleLabel;
        private Button btnStart;
        private Button btnMusic;
        private Button btnQuit;

        public MainMenu()
        {
            InitializeForm();
            CreateTitle();
            CreateButtons();

            this.Resize += (s, e) => UpdateLayout();
            UpdateLayout();

            string musicPath = System.IO.Path.Combine(Application.StartupPath, "Resources", "denis-pavlov-music-game-music-puzzle-strategy-arcade-technology-301226.mp3");
            MusicManager.Play(musicPath);
        }



        private void InitializeForm()
        {
            this.Text = "Coding Minigames";
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

        private Label subtitleLabel;

        private void CreateTitle()
        {
            titleLabel = new Label
            {
                Text = "Coding Minigames",
                Font = new Font("Segoe UI", 48, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            subtitleLabel = new Label
            {
                Text = "Learn to code by solving puzzles",
                Font = new Font("Segoe UI", 18, FontStyle.Regular),
                ForeColor = Color.FromArgb(160, 160, 200),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
        }

        private void CreateButtons()
        {
            btnStart = CreateButton("▶  Play", Color.FromArgb(83, 74, 183));
            btnMusic = CreateButton(MusicManager.IsMusicEnabled ? "🎵  Music: ON" : "🎵  Music: OFF", Color.FromArgb(40, 120, 80));
            btnQuit = CreateButton("✕  Quit", Color.FromArgb(120, 40, 40));

            btnStart.Click += (s, e) =>
            {
                var worldSelect = new WorldSelectForm();
                worldSelect.Show();
                this.Hide();
                worldSelect.FormClosed += (s2, args) => this.Show();
            };

            btnMusic.Click += (s, e) =>
            {
                MusicManager.ToggleMusic();
                btnMusic.Text = MusicManager.IsMusicEnabled ? "🎵  Music: ON" : "🎵  Music: OFF";
            };

            btnQuit.Click += (s, e) => Application.Exit();

            Controls.Add(btnStart);
            Controls.Add(btnMusic);
            Controls.Add(btnQuit);
        }

        private void UpdateLayout()
        {
            if (titleLabel == null || btnStart == null) return;

            int centerX = this.ClientSize.Width / 2;

            titleLabel.Size = new Size(this.ClientSize.Width, 120);
            titleLabel.Location = new Point(0, (int)(this.ClientSize.Height * 0.2));

            subtitleLabel.Size = new Size(this.ClientSize.Width, 40);
            subtitleLabel.Location = new Point(0, titleLabel.Bottom + 10);

            btnStart.Location = new Point(centerX - btnStart.Width / 2, subtitleLabel.Bottom + 60);
            btnMusic.Location = new Point(centerX - btnMusic.Width / 2, btnStart.Bottom + 20);
            btnQuit.Location = new Point(centerX - btnQuit.Width / 2, btnMusic.Bottom + 20);
        }

        private Button CreateButton(string text, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(240, 65),
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = Lighten(color);
            btn.MouseLeave += (s, e) => btn.BackColor = color;

            return btn;
        }

        private Color Lighten(Color color)
        {
            return Color.FromArgb(
                Math.Min(255, color.R + 30),
                Math.Min(255, color.G + 30),
                Math.Min(255, color.B + 30)
            );
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
                m.Result = (IntPtr)0x2;
        }
    }
}