using Coding_Minigames_Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class WorldSelectForm : Form
{
    public WorldSelectForm()
    {
        InitializeForm();
        CreateWorldButtons();
    }

    private void InitializeForm()
    {
        this.Text = "Select Your World";
        this.Size = new Size(900, 600);
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(20, 20, 40); // Matching your MainMenu
    }

    private void CreateWorldButtons()
    {
        // Title for the selection screen
        Label header = new Label
        {
            Text = "Choose a Programming Topic",
            Font = new Font("Segoe UI", 24, FontStyle.Bold),
            ForeColor = Color.White,
            Size = new Size(900, 60),
            Location = new Point(0, 50),
            TextAlign = ContentAlignment.MiddleCenter
        };
        this.Controls.Add(header);

        // Define our Worlds: Name, Level List, and Color
        // These reference your existing logic classes
        AddWorldCard("VARIABLES", VariableLevels.GetLevels(), new Point(100, 180), Color.FromArgb(46, 204, 113));
        AddWorldCard("CONDITIONALS", ConditionalsLevels.GetLevels(), new Point(350, 180), Color.FromArgb(52, 152, 219));
        AddWorldCard("LOOPS", LoopsLevels.GetLevels(), new Point(600, 180), Color.FromArgb(155, 89, 182));

        // Back to Menu Button
        Button btnBack = new Button
        {
            Text = "← Back to Menu",
            Size = new Size(150, 40),
            Location = new Point(20, 20),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnBack.FlatAppearance.BorderSize = 0;
        btnBack.Click += (s, e) => this.Close(); // Closing shows the MainMenu again
        this.Controls.Add(btnBack);
    }

    private void AddWorldCard(string name, List<LevelData> levels, Point location, Color accentColor)
    {
        Button card = new Button
        {
            Text = name,
            Size = new Size(200, 250),
            Location = location,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(30, 30, 60),
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        card.FlatAppearance.BorderSize = 2;
        card.FlatAppearance.BorderColor = accentColor;

        card.Click += (s, e) =>
        {
            // Pass the specific world's levels to MainForm
            var game = new MainForm(levels);
            game.Show();
            this.Hide();
            game.FormClosed += (s2, args) => this.Show();
        };

        this.Controls.Add(card);
    }

    // Allow dragging the form
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == 0x84) m.Result = (IntPtr)0x2;
    }
}
