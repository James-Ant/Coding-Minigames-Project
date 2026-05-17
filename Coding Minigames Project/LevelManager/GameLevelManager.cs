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
    public class GameLevelManager
    {
        private LevelData currentLevel;
        private List<SlotControl> slots = new List<SlotControl>();
        private List<DraggableBlock> blocks = new List<DraggableBlock>();
        private List<Label> slotLabels = new List<Label>();
        private PictureBox levelPictureBox;

        public Action OnLevelComplete { get; set; }

        public void LoadLevel(LevelData level, Panel gamePanel)
        {
            currentLevel = level;
            slots.Clear();
            blocks.Clear();
            slotLabels.Clear();
            gamePanel.Controls.Clear();

            SpawnImage(level, gamePanel);
            SpawnSlots(level, gamePanel);
            SpawnBlocks(level, gamePanel);
            Relayout(gamePanel);
        }

        public void Relayout(Panel gamePanel)
        {
            if (currentLevel == null) return;

            UpdateSlotPositions(gamePanel);
            UpdateBlockPositions(gamePanel);
        }

        private void UpdateSlotPositions(Panel gamePanel)
        {
            bool hasCustomPositions = currentLevel.SlotPositions != null &&
                                      currentLevel.SlotPositions.Count == currentLevel.ExpectedTags.Count;

            int spacing = 30; // standard spacing between slots
            int y = (gamePanel.Height / 2) - 40;

            if (levelPictureBox != null)
            {
                levelPictureBox.Location = new Point((gamePanel.Width - levelPictureBox.Width) / 2, y - levelPictureBox.Height - 40);
            }

            if (hasCustomPositions)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    var slot = slots[i];
                    slot.Left = currentLevel.SlotPositions[i].X;
                    slot.Top = currentLevel.SlotPositions[i].Y;

                    var lbl = slotLabels.Count > i ? slotLabels[i] : null;
                    if (lbl != null)
                    {
                        lbl.Left = slot.Left - lbl.Width - 10;
                        lbl.Top = slot.Top + (slot.Height - lbl.Height) / 2;
                    }
                }
            }
            else
            {
                // Calculate total width of all elements
                int totalWidth = 0;
                for (int i = 0; i < slots.Count; i++)
                {
                    var lbl = slotLabels.Count > i ? slotLabels[i] : null;
                    if (lbl != null) totalWidth += lbl.Width + 10;
                    totalWidth += slots[i].Width;
                    if (i < slots.Count - 1) totalWidth += spacing;
                }

                bool stackVertically = slots.Count > 2 || totalWidth > gamePanel.Width - 100;

                if (stackVertically)
                {
                    int totalHeight = (slots.Count * slots[0].Height) + ((slots.Count - 1) * spacing);
                    int startY = (gamePanel.Height - totalHeight) / 2;

                    for (int i = 0; i < slots.Count; i++)
                    {
                        var slot = slots[i];
                        var lbl = slotLabels.Count > i ? slotLabels[i] : null;

                        int rowWidth = slot.Width;
                        if (lbl != null) rowWidth += lbl.Width + 10;

                        int currentX = (gamePanel.Width - rowWidth) / 2;

                        if (lbl != null)
                        {
                            lbl.Left = currentX;
                            lbl.Top = startY + (slot.Height - lbl.Height) / 2;
                            currentX += lbl.Width + 10;
                        }

                        slot.Left = currentX;
                        slot.Top = startY;
                        startY += slot.Height + spacing;
                    }
                }
                else
                {
                    int currentX = (gamePanel.Width - totalWidth) / 2;

                    for (int i = 0; i < slots.Count; i++)
                    {
                        var slot = slots[i];
                        var lbl = slotLabels.Count > i ? slotLabels[i] : null;

                        if (lbl != null)
                        {
                            lbl.Left = currentX;
                            lbl.Top = y + (slot.Height - lbl.Height) / 2;
                            currentX += lbl.Width + 10;
                        }

                        slot.Left = currentX;
                        slot.Top = y;
                        currentX += slot.Width + spacing;
                    }
                }
            }
        }

        private void UpdateBlockPositions(Panel gamePanel)
        {
            bool hasCustomPositions = currentLevel.BlockPositions != null &&
                                      currentLevel.BlockPositions.Count == currentLevel.AvailableBlocks.Count;

            int totalWidth = currentLevel.AvailableBlocks.Count * 160;
            int startX = (gamePanel.Width - totalWidth) / 2;
            int y = gamePanel.Height - 120; // Adjusted for padding

            for (int i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                Point newStart;

                if (hasCustomPositions)
                {
                    newStart = currentLevel.BlockPositions[i];
                }
                else
                {
                    newStart = new Point(startX + (i * 160), y);
                }

                block.SetStartPosition(newStart);

                if (!block.IsAccepted)
                {
                    block.Left = newStart.X;
                    block.Top = newStart.Y;
                }
                else
                {
                    // If it is accepted, snap it to its current slot (which might have moved)
                    SlotControl currentSlot = slots.FirstOrDefault(s => s.AcceptedBlock == block);
                    if (currentSlot != null)
                    {
                        SnapBlockToSlot(block, currentSlot);
                    }
                }
            }
        }

        private void SpawnImage(LevelData level, Panel gamePanel)
        {
            if (level.LevelImage != null)
            {
                levelPictureBox = new PictureBox
                {
                    Image = level.LevelImage,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(300, 200),
                    BackColor = Color.Transparent
                };
                gamePanel.Controls.Add(levelPictureBox);
            }
            else
            {
                levelPictureBox = null;
            }
        }

        private void SpawnSlots(LevelData level, Panel gamePanel)
        {
            for (int i = 0; i < level.ExpectedTags.Count; i++)
            {
                if (level.SlotLabels != null && level.SlotLabels.Count > i)
                {
                    var lbl = new Label
                    {
                        Text = level.SlotLabels[i],
                        Font = new Font("Segoe UI", 24, FontStyle.Bold),
                        ForeColor = Color.White,
                        AutoSize = true,
                        BackColor = Color.Transparent,
                        TextAlign = ContentAlignment.MiddleRight
                    };
                    slotLabels.Add(lbl);
                    gamePanel.Controls.Add(lbl);
                }
                else
                {
                    slotLabels.Add(null);
                }

                var slot = new SlotControl(level.ExpectedTags[i]);

                if (level.SlotSizes != null && level.SlotSizes.Count > i)
                    slot.Size = level.SlotSizes[i];

                slot.OnFilled = CheckWinCondition;
                slots.Add(slot);
                gamePanel.Controls.Add(slot);
            }
        }


        private void SpawnBlocks(LevelData level, Panel gamePanel)
        {
            for (int i = 0; i < level.AvailableBlocks.Count; i++)
            {
                var block = new DraggableBlock(level.AvailableBlocks[i]);

                if (level.AvailableBlocks[i].ColorOverride.HasValue)
                    block.BackColor = level.AvailableBlocks[i].ColorOverride.Value;

                if (level.AvailableBlocks[i].SizeOverride.HasValue)
                    block.Size = level.AvailableBlocks[i].SizeOverride.Value;

                var captured = block;
                captured.OnReleased = () => HandleBlockReleased(captured);

                blocks.Add(block);
                gamePanel.Controls.Add(block);
                block.BringToFront();
            }
        }

        private void HandleBlockReleased(DraggableBlock block)
        {
            if (block.IsAccepted) return;

            SlotControl bestSlot = FindBestSlot(block);

            if (bestSlot != null)
            {
                SnapBlockToSlot(block, bestSlot);
                bestSlot.TryAccept(block);
                CheckWinCondition();
            }
            else
            {
                ReturnBlockToStart(block);
            }
        }

        private SlotControl FindBestSlot(DraggableBlock block)
        {
            SlotControl bestSlot = null;
            double closestDistance = double.MaxValue;

            foreach (var slot in slots)
            {
                if (slot.IsFilled) continue;

                if (IsOverlapping(block, slot))
                {
                    double distance = GetDistance(block, slot);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestSlot = slot;
                    }
                }
            }

            return bestSlot;
        }

        private void SnapBlockToSlot(DraggableBlock block, SlotControl slot)
        {
            // calculate exact center of slot then offset by half block size
            int snapX = slot.Left + (slot.Width - block.Width) / 2;
            int snapY = slot.Top + (slot.Height - block.Height) / 2;

            block.Left = snapX;
            block.Top = snapY;
        }

        private void ReturnBlockToStart(DraggableBlock block)
        {
            block.Left = block.StartPosition.X;
            block.Top = block.StartPosition.Y;
        }

        private bool IsOverlapping(Control a, Control b)
        {
            return a.Bounds.IntersectsWith(b.Bounds);
        }

        private double GetDistance(Control a, Control b)
        {
            var centerA = new Point(a.Left + a.Width / 2, a.Top + a.Height / 2);
            var centerB = new Point(b.Left + b.Width / 2, b.Top + b.Height / 2);
            return Math.Sqrt(Math.Pow(centerA.X - centerB.X, 2) + Math.Pow(centerA.Y - centerB.Y, 2));
        }

        private void CheckWinCondition()
        {
            bool allFilled = slots.All(s => s.IsFilled);
            if (allFilled)
            {
                OnLevelComplete?.Invoke();
            }
        }
    }
}
