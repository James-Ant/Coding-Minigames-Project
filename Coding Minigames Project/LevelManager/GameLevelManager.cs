using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Coding_Minigames_Project
{
    // GameLevelManager is the core engine of the drag-and-drop puzzle game.
    // It manages loading levels, creating UI elements laying them out on a panel,
    // handling drag-and-drop logic, and checking win conditions.
 
    public class GameLevelManager
    {
        private LevelData currentLevel;
        private List<SlotControl> slots = new List<SlotControl>();
        private List<DraggableBlock> blocks = new List<DraggableBlock>();
        private List<Label> slotLabels = new List<Label>();

        private Panel levelDescPanel;

        public Action OnLevelComplete { get; set; }

        public void LoadLevel(LevelData level, Panel gamePanel)
        {
            currentLevel = level;
            slots.Clear();
            blocks.Clear();
            slotLabels.Clear();
            gamePanel.Controls.Clear();
            levelDescPanel = null;

            // Spawn the description panel textbox first inside the level itself
            SpawnDescriptionPanel(level, gamePanel);

            SpawnSlots(level, gamePanel);
            SpawnBlocks(level, gamePanel);
            Relayout(gamePanel);
        }

        public void Relayout(Panel gamePanel)
        {
            if (currentLevel == null) return;

            if (levelDescPanel != null)
            {
                levelDescPanel.Width = Math.Min(800, gamePanel.Width - 40);
                levelDescPanel.Left = (gamePanel.Width - levelDescPanel.Width) / 2;
                levelDescPanel.Top = 15;
            }

            UpdateSlotPositions(gamePanel);
            UpdateBlockPositions(gamePanel);
        }

        private void UpdateSlotPositions(Panel gamePanel)
        {
            bool hasCustomPositions = currentLevel.SlotPositions != null &&
                                      currentLevel.SlotPositions.Count == currentLevel.ExpectedTags.Count;

            int topBoundary = (levelDescPanel != null) ? (levelDescPanel.Top + levelDescPanel.Height + 15) : 15;
            int remainingHeight = gamePanel.Height - topBoundary;
            int spacing = 30;

            int slotHeight = (slots.Count > 0) ? slots[0].Height : 80;
            int y = topBoundary + (remainingHeight / 2) - (slotHeight / 2);



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
                    int totalHeight = (slots.Count * slotHeight) + ((slots.Count - 1) * spacing);
                    int startY = topBoundary + (remainingHeight - totalHeight) / 2;

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

        private void SpawnDescriptionPanel(LevelData level, Panel gamePanel)
        {
            if (string.IsNullOrEmpty(level.LevelName) && string.IsNullOrEmpty(level.Description))
                return;

            levelDescPanel = new Panel
            {
                BackColor = Color.FromArgb(38, 38, 48),
                Padding = new Padding(15, 10, 15, 10),
                Height = 110
            };

            levelDescPanel.Paint += (s, e) => {
                using (var pen = new Pen(Color.FromArgb(70, 70, 95), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, levelDescPanel.Width - 1, levelDescPanel.Height - 1);
                }
            };

            Label titleLabel = new Label
            {
                Text = level.LevelName,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Dock = DockStyle.Top,
                Height = 28,
                BackColor = Color.Transparent
            };

            Label descLabel = new Label
            {
                Text = level.Description,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 220, 220),
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            levelDescPanel.Controls.Add(descLabel);
            levelDescPanel.Controls.Add(titleLabel);

            gamePanel.Controls.Add(levelDescPanel);
            levelDescPanel.BringToFront();
        }

        private void UpdateBlockPositions(Panel gamePanel)
        {
            bool hasCustomPositions = currentLevel.BlockPositions != null &&
                                      currentLevel.BlockPositions.Count == currentLevel.AvailableBlocks.Count;

            int totalWidth = currentLevel.AvailableBlocks.Count * 160;
            int startX = (gamePanel.Width - totalWidth) / 2;
            int y = gamePanel.Height - 120;

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
                    SlotControl currentSlot = slots.FirstOrDefault(s => s.AcceptedBlock == block);
                    if (currentSlot != null)
                    {
                        SnapBlockToSlot(block, currentSlot);
                    }
                }
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
