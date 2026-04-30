using Coding_Minigames_Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    public class DraggableBlock : Panel
    {
        public CodeBlock Data { get; set; }
        public bool IsAccepted { get; set; } = false;
        public Point StartPosition => startPosition;
        public Action OnReleased { get; set; }

        private Point startPosition;
        private Point dragOffset;
        private bool isDragging = false;
        private Label label;

        public DraggableBlock(CodeBlock data)
        {
            Data = data;

            Size = new Size(140, 60);
            BackColor = GetCategoryColor(data.Category);
            Cursor = Cursors.Hand;

            label = new Label
            {
                Text = data.DisplayText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Consolas", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            Controls.Add(label);

            // wire both panel and label so clicking the text works too
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            label.MouseDown += OnMouseDown;
            label.MouseMove += OnMouseMove;
            label.MouseUp += OnMouseUp;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (IsAccepted) return;
            isDragging = true;
            dragOffset = e.Location;
            BringToFront();
            Capture = true; // receive all mouse events until release
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;
            Left = Left + e.X - dragOffset.X;
            Top = Top + e.Y - dragOffset.Y;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;
            isDragging = false;
            Capture = false;
            OnReleased?.Invoke(); // tell the manager this block was released
        }

        public void SetStartPosition(Point position)
        {
            startPosition = position;
            Location = position;
        }

        private Color GetCategoryColor(BlockCategory category)
        {
            switch (category)
            {
                case BlockCategory.Variable: return Color.FromArgb(83, 74, 183);
                case BlockCategory.Conditional: return Color.FromArgb(15, 110, 86);
                case BlockCategory.Loop: return Color.FromArgb(99, 56, 6);
                default: return Color.Gray;
            }
        }
    }
}