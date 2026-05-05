using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    public class MainMenu : Form
    {
        private Label titleLabel;
        private Button btnStart;
        private Button btnQuit;

        public MainMenu()
        {
            InitializeForm();
            CreateTitle();
            CreateButtons();
        }

        private void InitializeForm()
        {
            this.Text = "Coding Minigames";
            this.Size = new Size(900, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 40);
        }

        private void CreateTitle()
        {
            titleLabel = new Label
            {
                Text = "Coding Minigames",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(900, 120),
                Location = new Point(0, 140)
            };

            var subtitleLabel = new Label
            {
                Text = "Learn to code by solving puzzles",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(160, 160, 200),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(900, 40),
                Location = new Point(0, 260)
            };

            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
        }

        private void CreateButtons()
        {
            btnStart = CreateButton("▶  Play", new Point(350, 340), Color.FromArgb(83, 74, 183));
            btnQuit = CreateButton("✕  Quit", new Point(350, 420), Color.FromArgb(120, 40, 40));

            btnStart.Click += (s, e) =>
            {
                var worldSelect = new WorldSelectForm();
                worldSelect.Show();
                this.Hide();
                worldSelect.FormClosed += (s2, args) => this.Show();
            };

            btnQuit.Click += (s, e) => Application.Exit();

            Controls.Add(btnStart);
            Controls.Add(btnQuit);
        }

        private Button CreateButton(string text, Point location, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(200, 55),
                Location = location,
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