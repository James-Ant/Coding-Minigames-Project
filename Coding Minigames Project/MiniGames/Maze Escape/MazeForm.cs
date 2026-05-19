using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    public partial class MazeForm : Form
    {
        // =========================
        // PROPERTIES & CONFIG
        // =========================
        int tileSize = 25;
        int rows, cols;
        const int sidebarWidth = 200;

        Panel[,] tiles;
        MazeGenerator generator = new MazeGenerator();
        MazePlayer player = new MazePlayer();

        // =========================
        // UI CONTROLS
        // =========================
        Panel gamePanel, sidebarPanel;
        Button btnMainMenu, btnReset;
        Label lblInstruction, lblLegend, lblKeys;
        Panel playerPanel;

        // =========================
        // GAME STATE
        // =========================
        int finishRow, finishCol, keys = 0;
        const int requiredKeys = 3;

        List<(int row, int col, bool solved)> questionDoors = new List<(int row, int col, bool solved)>();
        Random rnd = new Random();
        Timer moveTimer = new Timer { Interval = 10 }; // Interpolation loop timer
        int moveSpeed = 5;

        // =========================
        // CONSTRUCTOR
        // =========================
        public MazeForm()
        {
            InitializeComponent();
            Text = "Maze Escape";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true; // Intercept keyboard input before controls
            DoubleBuffered = true; // Prevents screen flickering

            CreateLayout();

            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();

            Shown += (s, e) => GenerateGame();
        }

        // =========================
        // LAYOUT
        // =========================
        void CreateLayout()
        {
            sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = sidebarWidth,
                BackColor = Color.FromArgb(40, 40, 50),
                Padding = new Padding(10)
            };

            lblInstruction = new Label
            {
                Text = "Obtain 3 keys by answering question doors to escape the maze. There are hidden doors scattered around.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 170,
                Height = 100,
                Top = 20,
                Left = 10
            };

            lblLegend = new Label
            {
                Text = "LEGEND:\n\nGreen - Player\nPink - Question Door\nYellow - Finish",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Top = 140,
                Left = 10
            };

            lblKeys = new Label
            {
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Top = 260,
                Left = 10
            };

            btnReset = new Button
            {
                Text = "⟳ Reset Maze",
                Dock = DockStyle.Bottom,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(60, 120, 80),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Click += (s, e) => { player.IsMoving = false; GenerateGame(); };

            btnMainMenu = new Button
            {
                Text = "⌂ Main Menu",
                Dock = DockStyle.Bottom,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(120, 60, 60),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnMainMenu.FlatAppearance.BorderSize = 0;
            btnMainMenu.Click += (s, e) => { CloseFormAndReturn(); };

            sidebarPanel.Controls.AddRange(new Control[] { lblInstruction, lblLegend, lblKeys, btnReset, btnMainMenu });

            gamePanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Black };
            Controls.AddRange(new Control[] { gamePanel, sidebarPanel });
        }

        // =========================
        // GAME SETUP
        // =========================
        void GenerateGame()
        {
            // Clean up old paint handlers to prevent memory leaks
            if (tiles != null)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (tiles[r, c] != null)
                        {
                            tiles[r, c].Paint -= DoorPanel_Paint;
                            tiles[r, c].Paint -= WallPanel_Paint;
                        }
                    }
                }
            }

            gamePanel.Controls.Clear();
            questionDoors.Clear();
            keys = 0;
            UpdateKeysUI();

            var area = gamePanel.ClientSize;
            cols = Math.Max(1, area.Width / tileSize);
            rows = Math.Max(1, area.Height / tileSize);
            tiles = generator.Generate(rows, cols, tileSize, gamePanel);

            // Hook up curved paint handler to solid wall tiles
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (tiles[r, c].BackColor != Color.Black)
                    {
                        tiles[r, c].Paint += WallPanel_Paint;
                        tiles[r, c].Invalidate();
                    }
                }
            }

            // Setup Player Visual Token
            playerPanel = new Panel { Size = new Size(tileSize, tileSize), BackColor = Color.Transparent };
            playerPanel.Paint += PlayerPanel_Paint;

            gamePanel.Controls.Add(playerPanel);
            playerPanel.BringToFront();

            // Spawn Entities
            SetRandomPlacement(ref player.Row, ref player.Col, Color.Black, isDoor: false, isFinish: false);
            SetRandomPlacement(ref finishRow, ref finishCol, Color.Gold, isDoor: false, isFinish: true);

            for (int i = 0; i < requiredKeys; i++)
            {
                int r = 0, c = 0;
                SetRandomPlacement(ref r, ref c, Color.Magenta, isDoor: true, isFinish: false);
                questionDoors.Add((r, c, false));
            }

            player.PixelX = player.Col * tileSize;
            player.PixelY = player.Row * tileSize;
            playerPanel.Location = new Point(player.PixelX, player.PixelY);

            Focus();
        }

        void SetRandomPlacement(ref int targetRow, ref int targetCol, Color tileColor, bool isDoor, bool isFinish)
        {
            while (true)
            {
                int r = rnd.Next(rows);
                int c = rnd.Next(cols);
                if (tiles[r, c].BackColor == Color.Black)
                {
                    targetRow = r;
                    targetCol = c;

                    if (isDoor)
                    {
                        tiles[r, c].Paint -= WallPanel_Paint;
                        tiles[r, c].Paint += DoorPanel_Paint;
                        tiles[r, c].Invalidate();
                    }
                    else if (isFinish)
                    {
                        tiles[r, c].BackColor = tileColor;
                        tiles[r, c].Paint += WallPanel_Paint;
                        tiles[r, c].Invalidate();
                    }
                    else
                    {
                        tiles[r, c].Paint -= WallPanel_Paint;
                        tiles[r, c].BackColor = tileColor;
                        tiles[r, c].Invalidate();
                    }
                    break;
                }
            }
        }

        // =========================
        // GDI+ CUSTOM DRAWING
        // =========================
        private void PlayerPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 6;
            int diameter = radius * 2;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, diameter, diameter, 180, 90);
                path.AddArc(playerPanel.Width - diameter - 1, 0, diameter, diameter, 270, 90);
                path.AddArc(playerPanel.Width - diameter - 1, playerPanel.Height - diameter - 1, diameter, diameter, 0, 90);
                path.AddArc(0, playerPanel.Height - diameter - 1, diameter, diameter, 90, 90);
                path.CloseFigure();

                using (Brush brush = new SolidBrush(Color.Lime))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.White, 3) { Alignment = PenAlignment.Inset })
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void DoorPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush brush = new SolidBrush(Color.Magenta))
            {
                e.Graphics.FillEllipse(brush, 2, 2, p.Width - 5, p.Height - 5);
            }
        }

        private void WallPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 6;
            int diameter = radius * 2;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, diameter, diameter, 180, 90);
                path.AddArc(p.Width - diameter - 1, 0, diameter, diameter, 270, 90);
                path.AddArc(p.Width - diameter - 1, p.Height - diameter - 1, diameter, diameter, 0, 90);
                path.AddArc(0, p.Height - diameter - 1, diameter, diameter, 90, 90);
                path.CloseFigure();

                e.Graphics.Clear(gamePanel.BackColor);

                using (Brush brush = new SolidBrush(p.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        // =========================
        // INPUT HANDLERS
        // =========================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (player.IsMoving) return base.ProcessCmdKey(ref msg, keyData);

            int r = player.Row, c = player.Col;
            bool handled = false;

            if (keyData == Keys.Up) { r--; handled = true; }
            else if (keyData == Keys.Down) { r++; handled = true; }
            else if (keyData == Keys.Left) { c--; handled = true; }
            else if (keyData == Keys.Right) { c++; handled = true; }

            if (handled)
            {
                player.Move(r, c, tiles, tileSize);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =========================
        // MOVEMENT LOOP (10ms)
        // =========================
        void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (!player.IsMoving) return;

            int dx = player.TargetX - player.PixelX;
            int dy = player.TargetY - player.PixelY;

            // Pixel destination snapping
            if (Math.Abs(dx) < moveSpeed && Math.Abs(dy) < moveSpeed)
            {
                player.PixelX = player.TargetX;
                player.PixelY = player.TargetY;
                player.Row = player.PendingRow;
                player.Col = player.PendingCol;
                player.IsMoving = false;

                playerPanel.Location = new Point(player.PixelX, player.PixelY);
                CheckTileEvent();
                return;
            }

            player.PixelX += Math.Sign(dx) * moveSpeed;
            player.PixelY += Math.Sign(dy) * moveSpeed;
            playerPanel.Location = new Point(player.PixelX, player.PixelY);
        }

        // =========================
        // GAME RULES & EVALUATION
        // =========================
        void CheckTileEvent()
        {
            var door = questionDoors.FirstOrDefault(d => d.row == player.Row && d.col == player.Col && !d.solved);
            if (door != default) AskQuestion(door.row, door.col);

            if (player.Row == finishRow && player.Col == finishCol)
            {
                if (keys >= requiredKeys)
                {
                    MessageBox.Show("You Win! Generating a new maze...");
                    player.IsMoving = false;
                    GenerateGame();
                }
                else
                {
                    MessageBox.Show($"Need {requiredKeys} keys!");
                }
            }
        }

        void AskQuestion(int r, int c)
        {
            using (QuestionForm qf = new QuestionForm())
            {
                qf.ShowDialog();
                if (qf.isCorrect)
                {
                    keys++;
                    UpdateKeysUI();

                    // Track door state as solved to prevent re-trigger loop
                    int index = questionDoors.FindIndex(d => d.row == r && d.col == c);
                    if (index != -1) questionDoors[index] = (r, c, true);

                    tiles[r, c].Paint -= DoorPanel_Paint;
                    tiles[r, c].BackColor = Color.Black;
                    tiles[r, c].Invalidate();

                    MessageBox.Show("Key obtained!");
                }
            }
        }

        void UpdateKeysUI() => lblKeys.Text = $"Keys: {keys}/{requiredKeys}";

        void CloseFormAndReturn()
        {
            var mainMenu = Application.OpenForms.OfType<MainMenu>().FirstOrDefault();
            mainMenu?.Show();

            foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
            {
                if (f != mainMenu && f != this) f.Hide();
            }
            Close();
        }
    }
}