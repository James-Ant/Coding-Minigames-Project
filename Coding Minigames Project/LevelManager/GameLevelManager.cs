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

        public Action OnLevelComplete { get; set; }

        public void LoadLevel(LevelData level, Panel gamePanel)
        {
            currentLevel = level;
            slots.Clear();
            blocks.Clear();
            gamePanel.Controls.Clear();

            SpawnSlots(level, gamePanel);
            SpawnBlocks(level, gamePanel);
        }

        private void SpawnSlots(LevelData level, Panel gamePanel)
        {
            bool hasCustomPositions = level.SlotPositions != null &&
                                      level.SlotPositions.Count == level.ExpectedTags.Count;

            int totalWidth = level.ExpectedTags.Count * 180;
            int startX = (gamePanel.Width - totalWidth) / 2;
            int y = (gamePanel.Height / 2) - 40;

            for (int i = 0; i < level.ExpectedTags.Count; i++)
            {
                var slot = new SlotControl(level.ExpectedTags[i]);

                if (hasCustomPositions)
                {
                    slot.Left = level.SlotPositions[i].X;
                    slot.Top = level.SlotPositions[i].Y;
                }
                else
                {
                    slot.Left = startX + (i * 180);
                    slot.Top = y;
                }

                if (level.SlotSizes != null && level.SlotSizes.Count > i)
                    slot.Size = level.SlotSizes[i];

                slot.OnFilled = CheckWinCondition;
                slots.Add(slot);
                gamePanel.Controls.Add(slot);
            }
        }


        private void SpawnBlocks(LevelData level, Panel gamePanel)
        {
            bool hasCustomPositions = level.BlockPositions != null &&
                                      level.BlockPositions.Count == level.AvailableBlocks.Count;

            int totalWidth = level.AvailableBlocks.Count * 160;
            int startX = (gamePanel.Width - totalWidth) / 2;
            int y = gamePanel.Height - 100;

            for (int i = 0; i < level.AvailableBlocks.Count; i++)
            {
                var block = new DraggableBlock(level.AvailableBlocks[i]);

                if (hasCustomPositions)
                {
                    block.SetStartPosition(level.BlockPositions[i]);
                }
                else
                {
                    block.SetStartPosition(new Point(startX + (i * 160), y));
                }

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
