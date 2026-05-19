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
        Panel sidebarPanel;

        Button btnMainMenu;
        Button btnReset;

        Label lblInstruction;
        Label lblLegend;
        Label lblKeys;

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

            Text = "Maze Escape";
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            DoubleBuffered = true;

            generator = new MazeGenerator();
            player = new MazePlayer();

            CreateLayout();

            moveTimer.Interval = 10;
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

            // INSTRUCTION
            lblInstruction = new Label
            {
                Text = "Obtain 3 keys by answering question doors to escape the maze.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 170,
                Height = 80,
                Top = 20,
                Left = 10
            };

            // LEGEND
            lblLegend = new Label
            {
                Text =
                    "LEGEND:\n\n" +
                    "🟩 Player\n" +
                    "🟪 Question Door\n" +
                    "🟨 Finish",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Top = 110,
                Left = 10
            };

            // KEYS
            lblKeys = new Label
            {
                Text = $"Keys: {keys}/{requiredKeys}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Top = 250,
                Left = 10
            };

            // RESET
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

            btnReset.Click += (s, e) =>
            {
                player.IsMoving = false;
                GenerateGame();
            };

            // MAIN MENU
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

            btnMainMenu.Click += (s, e) =>
            {
                var mainMenu = Application.OpenForms.OfType<MainMenu>().FirstOrDefault();

                if (mainMenu != null)
                    mainMenu.Show();

                foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (f != mainMenu && f != this)
                        f.Hide();
                }

                Close();
            };

            sidebarPanel.Controls.Add(lblInstruction);
            sidebarPanel.Controls.Add(lblLegend);
            sidebarPanel.Controls.Add(lblKeys);
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
            gamePanel.Controls.Clear();
            questionDoors.Clear();
            keys = 0;

            UpdateKeysUI();

            var area = gamePanel.ClientSize;

            cols = Math.Max(1, area.Width / tileSize);
            rows = Math.Max(1, area.Height / tileSize);

            tiles = generator.Generate(rows, cols, tileSize, gamePanel);

            // PLAYER
            playerPanel = new Panel
            {
                Size = new Size(tileSize, tileSize),
                BackColor = Color.Lime
            };

            gamePanel.Controls.Add(playerPanel);
            playerPanel.BringToFront();

            // SPAWN
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

            // QUESTIONS
            for (int i = 0; i < 3; i++)
            {
                while (true)
                {
                    int r = rnd.Next(rows);
                    int c = rnd.Next(cols);

                    if (tiles[r, c].BackColor == Color.Black)
                    {
                        tiles[r, c].BackColor = Color.Magenta;
                        questionDoors.Add((r, c, false));
                        break;
                    }
                }
            }

            player.PixelX = player.Col * tileSize;
            player.PixelY = player.Row * tileSize;

            playerPanel.Location = new Point(player.PixelX, player.PixelY);

            Focus();
        }

        // =========================
        // INPUT
        // =========================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (player.IsMoving)
                return base.ProcessCmdKey(ref msg, keyData);

            int r = player.Row;
            int c = player.Col;

            if (keyData == Keys.Up) r--;
            else if (keyData == Keys.Down) r++;
            else if (keyData == Keys.Left) c--;
            else if (keyData == Keys.Right) c++;

            player.Move(r, c, tiles, tileSize);
            return true;
        }

        // =========================
        // MOVEMENT
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
        // TILE CHECK
        // =========================
        void CheckTileEvent()
        {
            var door = questionDoors.FirstOrDefault(d =>
                d.row == player.Row &&
                d.col == player.Col &&
                !d.solved);

            if (door != default)
                AskQuestion(door.row, door.col);

            if (player.Row == finishRow && player.Col == finishCol)
            {
                if (keys >= requiredKeys)
                    MessageBox.Show("You Win!");
                else
                    MessageBox.Show("Need 3 keys!");
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
                keys++;
                UpdateKeysUI();

                tiles[r, c].BackColor = Color.Black;

                MessageBox.Show("Key obtained!");
            }
        }

        void UpdateKeysUI()
        {
            lblKeys.Text = $"Keys: {keys}/{requiredKeys}";
        }
    }
}