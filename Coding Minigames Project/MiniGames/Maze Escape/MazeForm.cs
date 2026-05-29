using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    // MazeForm is a maze-escape mini-game where the player navigates a randomly generated grid of walls
    // and paths using arrow keys.
    // The grid is built from Panel tiles, each randomly assigned as a wall or a path.
    // Pink tiles pop up a MazeQuestionForm trivia dialog when stepped on.
    // A key is obtaioned when the player answers the question correctly
    // The player must collect 3 keys and reach the gold finish tile to win.
    public class MazeForm : Form
    {
        const int TileSize = 25;
        const int SidebarWidth = 200;
        const int WallChance = 30;
        const int RequiredKeys = 3;

        int rows, cols, keys;
        Panel[,] tiles;

        int playerRow, playerCol, pixelX, pixelY;
        int targetX, targetY, pendingRow, pendingCol;
        bool isMoving;

        int finishRow, finishCol;
        List<(int row, int col, bool solved)> doors = new List<(int, int, bool)>();

        Panel gamePanel, playerPanel;
        Label lblKeys;
        Timer moveTimer;
        Random rnd = new Random();

        static readonly Color WallColor = Color.FromArgb(83, 74, 183);
        static readonly Color PathColor = Color.Black;

        public MazeForm()
        {
            Text = "Maze Escape";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            DoubleBuffered = true;

            BuildLayout();

            moveTimer = new Timer { Interval = 10 };
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();

            Shown += (s, e) => NewGame();
        }

        void BuildLayout()
        {
            var sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = SidebarWidth,
                BackColor = Color.FromArgb(40, 40, 50),
                Padding = new Padding(10)
            };

            var lblInfo = new Label
            {
                Text = "Obtain 3 keys by answering\nquestion doors to escape\nthe maze.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 170, Height = 80,
                Top = 20, Left = 10
            };

            var lblLegend = new Label
            {
                Text = "LEGEND:\n\nGreen  – Player\nPink   – Question Door\nYellow – Finish",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Top = 110, Left = 10
            };

            lblKeys = new Label
            {
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Top = 260, Left = 10
            };

            var btnReset = MakeSidebarButton("⟳ Reset Maze", Color.FromArgb(60, 120, 80));
            btnReset.Click += (s, e) => { isMoving = false; NewGame(); };

            var btnMenu = MakeSidebarButton("⌂ Main Menu", Color.FromArgb(120, 60, 60));
            btnMenu.Click += (s, e) => ReturnToMenu();

            sidebar.Controls.AddRange(new Control[] { lblInfo, lblLegend, lblKeys, btnReset, btnMenu });

            gamePanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Black };

            Controls.AddRange(new Control[] { gamePanel, sidebar });
        }

        Button MakeSidebarButton(string text, Color bg)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Bottom,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = bg,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        void NewGame()
        {
            if (tiles != null)
                foreach (var t in tiles)
                    if (t != null) { t.Paint -= PaintDoor; t.Paint -= PaintRoundedTile; }

            gamePanel.Controls.Clear();
            doors.Clear();
            keys = 0;
            UpdateKeysUI();

            var area = gamePanel.ClientSize;
            cols = Math.Max(1, area.Width / TileSize);
            rows = Math.Max(1, area.Height / TileSize);
            tiles = new Panel[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    bool isWall = rnd.Next(100) < WallChance;
                    var p = new Panel
                    {
                        Width = TileSize, Height = TileSize,
                        Left = c * TileSize, Top = r * TileSize,
                        BorderStyle = BorderStyle.None,
                        BackColor = isWall ? WallColor : PathColor
                    };

                    if (isWall) { p.Paint += PaintRoundedTile; }

                    tiles[r, c] = p;
                    gamePanel.Controls.Add(p);
                }
            }

            playerPanel = new Panel { Size = new Size(TileSize, TileSize), BackColor = Color.Transparent };
            playerPanel.Paint += PaintPlayer;
            gamePanel.Controls.Add(playerPanel);
            playerPanel.BringToFront();

            PlaceOnPath(out playerRow, out playerCol);
            PlaceFinish(out finishRow, out finishCol);

            for (int i = 0; i < RequiredKeys; i++)
            {
                int dr, dc;
                PlaceOnPath(out dr, out dc);
                tiles[dr, dc].Paint += PaintDoor;
                tiles[dr, dc].Invalidate();
                doors.Add((dr, dc, false));
            }

            pixelX = playerCol * TileSize;
            pixelY = playerRow * TileSize;
            playerPanel.Location = new Point(pixelX, pixelY);

            Focus();
        }

        void PlaceOnPath(out int row, out int col)
        {
            do { row = rnd.Next(rows); col = rnd.Next(cols); }
            while (tiles[row, col].BackColor != PathColor);
        }

        void PlaceFinish(out int row, out int col)
        {
            PlaceOnPath(out row, out col);
            tiles[row, col].BackColor = Color.Gold;
            tiles[row, col].Paint += PaintRoundedTile;
            tiles[row, col].Invalidate();
        }

        void PaintPlayer(object s, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = RoundedRect(0, 0, playerPanel.Width - 1, playerPanel.Height - 1, 6))
            {
                e.Graphics.FillPath(Brushes.Lime, path);
                using (var pen = new Pen(Color.White, 3) { Alignment = PenAlignment.Inset })
                    e.Graphics.DrawPath(pen, path);
            }
        }

        void PaintDoor(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(Color.Magenta))
                e.Graphics.FillEllipse(brush, 2, 2, p.Width - 5, p.Height - 5);
        }

        void PaintRoundedTile(object s, PaintEventArgs e)
        {
            var p = (Panel)s;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(gamePanel.BackColor);
            using (var path = RoundedRect(0, 0, p.Width - 1, p.Height - 1, 6))
            using (var brush = new SolidBrush(p.BackColor))
                e.Graphics.FillPath(brush, path);
        }

        static GraphicsPath RoundedRect(int x, int y, int w, int h, int r)
        {
            int d = r * 2;
            var gp = new GraphicsPath();
            gp.AddArc(x, y, d, d, 180, 90);
            gp.AddArc(x + w - d, y, d, d, 270, 90);
            gp.AddArc(x + w - d, y + h - d, d, d, 0, 90);
            gp.AddArc(x, y + h - d, d, d, 90, 90);
            gp.CloseFigure();
            return gp;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (isMoving) return base.ProcessCmdKey(ref msg, keyData);

            int r = playerRow, c = playerCol;
            bool handled = false;

            switch (keyData)
            {
                case Keys.Up:    r--; handled = true; break;
                case Keys.Down:  r++; handled = true; break;
                case Keys.Left:  c--; handled = true; break;
                case Keys.Right: c++; handled = true; break;
            }

            if (handled && TryMove(r, c)) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        bool TryMove(int newRow, int newCol)
        {
            if (isMoving) return false;
            if (newRow < 0 || newCol < 0 || newRow >= rows || newCol >= cols) return false;
            if (tiles[newRow, newCol].BackColor == WallColor) return false;

            pendingRow = newRow;
            pendingCol = newCol;
            targetX = newCol * TileSize;
            targetY = newRow * TileSize;
            isMoving = true;
            return true;
        }

        void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (!isMoving) return;

            const int speed = 5;
            int dx = targetX - pixelX;
            int dy = targetY - pixelY;

            if (Math.Abs(dx) < speed && Math.Abs(dy) < speed)
            {
                pixelX = targetX;
                pixelY = targetY;
                playerRow = pendingRow;
                playerCol = pendingCol;
                isMoving = false;
                playerPanel.Location = new Point(pixelX, pixelY);
                CheckTile();
                return;
            }

            pixelX += Math.Sign(dx) * speed;
            pixelY += Math.Sign(dy) * speed;
            playerPanel.Location = new Point(pixelX, pixelY);
        }

        void CheckTile()
        {
            int idx = doors.FindIndex(d => d.row == playerRow && d.col == playerCol && !d.solved);
            if (idx != -1) AskQuestion(idx);

            if (playerRow == finishRow && playerCol == finishCol)
            {
                if (keys >= RequiredKeys)
                {
                    MessageBox.Show("You Win! Generating a new maze...");
                    isMoving = false;
                    NewGame();
                }
                else
                {
                    MessageBox.Show($"You need {RequiredKeys} keys to escape!");
                }
            }
        }

        void AskQuestion(int doorIndex)
        {
            var (r, c, _) = doors[doorIndex];
            using (var qf = new MazeQuestionForm())
            {
                qf.ShowDialog();
                if (qf.IsCorrect)
                {
                    keys++;
                    UpdateKeysUI();
                    doors[doorIndex] = (r, c, true);

                    tiles[r, c].Paint -= PaintDoor;
                    tiles[r, c].BackColor = PathColor;
                    tiles[r, c].Invalidate();

                    MessageBox.Show("Key obtained!");
                }
            }
        }

        void UpdateKeysUI() => lblKeys.Text = $"Keys: {keys}/{RequiredKeys}";

        void ReturnToMenu()
        {
            var mainMenu = Application.OpenForms.OfType<MainMenu>().FirstOrDefault();
            mainMenu?.Show();
            Close();
            foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
                if (f != mainMenu) f.Close();
        }
    }
}
