using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    public class SlotControl : Panel
    {
        public string ExpectedTag { get; set; }
        public bool IsFilled { get; set; } = false;

        public System.Action OnFilled { get; set; }

        private Label hintLabel;

        public SlotControl(string expectedTag)
        {
            ExpectedTag = expectedTag;

            Size = new Size(160, 80);
            BackColor = Color.FromArgb(40, 40, 40);
            BorderStyle = BorderStyle.FixedSingle;

            hintLabel = new Label
            {
                Text = "Drop here",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(120, 120, 120),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10)
            };

            Controls.Add(hintLabel);
        }

        public bool TryAccept(DraggableBlock block)
        {
            if (IsFilled) return false;

            if (block.Data != null && block.Data.Tag == ExpectedTag)
            {
                Accept(block);
                return true;
            }
            else
            {
                Reject();
                return false;
            }
        }

        private void Accept(DraggableBlock block)
        {
            IsFilled = true;
            block.IsAccepted = true;

            block.Location = new Point(
                Left + (Width - block.Width) / 2,
                Top + (Height - block.Height) / 2
            );

            BackColor = Color.FromArgb(15, 80, 50);
            hintLabel.Text = block.Data.DisplayText;
            hintLabel.ForeColor = Color.White;

            OnFilled?.Invoke();
        }

        private void Reject()
        {
            BackColor = Color.FromArgb(100, 30, 30);

            var timer = new Timer { Interval = 300 };
            timer.Tick += (s, e) =>
            {
                BackColor = Color.FromArgb(40, 40, 40);
                timer.Stop();
            };
            timer.Start();
        }
    }
}
