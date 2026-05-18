using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Coding_Minigames_Project.MiniGames.Maze_Escape
{
    public partial class MazeForm : Form
    {
        // CONFIG
        int tileSize = 25;
        int rows;
        int cols;

        const int sidebarWidth = 200;

        // GAME OBJECTS
        Panel[,] tiles;
        MazeGenerator generator;

        MazePlayer player;

        Panel gamePanel;
        Panel sidebarPanel; //smh
        Button btnMainMenu;
        Button btnReset;

        Panel playerPanel;

        // FINISH + KEYS
        int finishRow;
        int finishCol;

        int keys = 0;
        int requiredKeys = 3;

        List<(int row, int col, bool solved)> questionDoors =
            new List<(int row, int col, bool solved)>();

        Random rnd = new Random();

        // SMOOTH MOVEMENT
        Timer moveTimer = new Timer();
        int moveSpeed = 5;

        public MazeForm()
        {
            InitializeComponent();

            this.Text = "Maze Escape";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            this.DoubleBuffered = true;

            generator = new MazeGenerator();
            player = new MazePlayer();

            CreateLayout();

            moveTimer.Interval = 10;
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();

            // ONLY run GenerateGame here once the form is visible on screen
            this.Shown += (s, e) =>
            {
                GenerateGame();
            };
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

            // 1. MAIN MENU BUTTON (Added first, goes to the very bottom)
            btnMainMenu = new Button
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

            btnMainMenu.Click += (s, e) =>
            {
                var mainMenu = Application.OpenForms.OfType<MainMenu>().FirstOrDefault();
                if (mainMenu != null)
                    mainMenu.Show();

                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (form != mainMenu && form != this)
                        form.Hide();
                }
                this.Close();
            };

            // 2. RESET BUTTON (Added second, stacks cleanly ABOVE Main Menu)
            btnReset = new Button
            {
                Text = "⟳ Reset Maze",
                Dock = DockStyle.Bottom,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(60, 120, 80), // Nice green color
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 10) // Puts a little breathing room between buttons
            };
            btnReset.FlatAppearance.BorderSize = 0;

            // The magic click event
            btnReset.Click += (s, e) =>
            {
                // Stop the player movement if they were mid-animation
                player.IsMoving = false;

                // Wipe everything and rebuild!
                GenerateGame();
            };

            // Add them to the sidebar panel
            sidebarPanel.Controls.Add(btnReset);
            sidebarPanel.Controls.Add(btnMainMenu);

            gamePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black
            };

            Controls.Add(gamePanel);
            Controls.Add(sidebarPanel);
        }

        // =========================
        // GAME SETUP
        // =========================
        void GenerateGame()
        {
            // FIX #1: Completely clear out any old maze elements 
            gamePanel.Controls.Clear();
            questionDoors.Clear();
            keys = 0;

            var area = gamePanel.ClientSize;

            cols = Math.Max(1, area.Width / tileSize);
            rows = Math.Max(1, area.Height / tileSize);

            tiles = generator.Generate(rows, cols, tileSize, gamePanel);

            // PLAYER PANEL (visual object)
            playerPanel = new Panel
            {
                Size = new Size(tileSize, tileSize),
                BackColor = Color.Lime
            };

            gamePanel.Controls.Add(playerPanel);

            // FIX #2: Explicitly force the moving player tile to layer over everything else
            playerPanel.BringToFront();

            // PLAYER SPAWN
            while (true)
            {
                int r = rnd.Next(rows);
                int c = rnd.Next(cols);

                if (tiles[r, c].BackColor == Color.Black)
                {
                    player.Row = r;
                    player.Col = c;
                    break;
                }
            }

            // FINISH
            while (true)
            {
                int r = rnd.Next(rows);
                int c = rnd.Next(cols);

                if (tiles[r, c].BackColor == Color.Black &&
                    (r != player.Row || c != player.Col))
                {
                    finishRow = r;
                    finishCol = c;
                    break;
                }
            }

            tiles[finishRow, finishCol].BackColor = Color.Gold;

            // QUESTION TILES
            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    int r = rnd.Next(rows);
                    int c = rnd.Next(cols);

                    if (tiles[r, c].BackColor == Color.Black &&
                        (r != player.Row || c != player.Col) &&
                        (r != finishRow || c != finishCol))
                    {
                        tiles[r, c].BackColor = Color.Magenta;
                        questionDoors.Add((r, c, false));
                        break;
                    }
                }
            }

            // INIT PLAYER POSITION
            player.PixelX = player.Col * tileSize;
            player.PixelY = player.Row * tileSize;

            playerPanel.Location = new Point(player.PixelX, player.PixelY);

            UpdateTitle();

            // FIX #3: Demand keyboard focus right onto the game area
            gamePanel.Select();
            gamePanel.Focus();
        }

        // =========================
        // INPUT HANDLER
        // =========================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (player.IsMoving) return base.ProcessCmdKey(ref msg, keyData);

            int newRow = player.Row;
            int newCol = player.Col;
            bool handled = false;

            if (keyData == Keys.Up) { newRow--; handled = true; }
            else if (keyData == Keys.Down) { newRow++; handled = true; }
            else if (keyData == Keys.Left) { newCol--; handled = true; }
            else if (keyData == Keys.Right) { newCol++; handled = true; }

            if (handled)
            {
                player.Move(newRow, newCol, tiles, tileSize);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =========================
        // SMOOTH MOVEMENT
        // =========================
        void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (!player.IsMoving) return;

            int dx = player.TargetX - player.PixelX;
            int dy = player.TargetY - player.PixelY;

            if (Math.Abs(dx) < moveSpeed && Math.Abs(dy) < moveSpeed)
            {
                player.PixelX = player.TargetX;
                player.PixelY = player.TargetY;

                // Sync the logical coordinates upon completion
                player.Row = player.PendingRow;
                player.Col = player.PendingCol;

                player.IsMoving = false;

                playerPanel.Location = new Point(player.PixelX, player.PixelY);

                CheckTileEvent();
                return;
            }

            if (dx != 0)
                player.PixelX += Math.Sign(dx) * moveSpeed;

            if (dy != 0)
                player.PixelY += Math.Sign(dy) * moveSpeed;

            playerPanel.Location = new Point(player.PixelX, player.PixelY);
        }

        // =========================
        // TILE LOGIC
        // =========================
        void CheckTileEvent()
        {
            var door = questionDoors.FirstOrDefault(d =>
                d.row == player.Row &&
                d.col == player.Col &&
                !d.solved);

            if (door != default)
            {
                AskQuestion(door.row, door.col);
            }

            if (player.Row == finishRow && player.Col == finishCol)
            {
                if (keys >= requiredKeys)
                    MessageBox.Show("You Win!");
                else
                    MessageBox.Show($"Need {requiredKeys} keys!");
            }
        }

        // =========================
        // QUESTION
        // =========================
        void AskQuestion(int r, int c)
        {
            QuestionForm qf = new QuestionForm();
            qf.ShowDialog();

            if (qf.isCorrect)
            {
                for (int i = 0; i < questionDoors.Count; i++)
                {
                    if (questionDoors[i].row == r && questionDoors[i].col == c)
                    {
                        questionDoors[i] = (r, c, true);
                        break;
                    }
                }

                tiles[r, c].BackColor = Color.Black;
                keys++;

                MessageBox.Show("You got a key!");
                UpdateTitle();
            }
        }

        void UpdateTitle()
        {
            this.Text = $"Maze Escape | Keys: {keys}/{requiredKeys}";
        }
    }
}