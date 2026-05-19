using System.Drawing;
using System.Windows.Forms;

public class MazePlayer
{
    public int Row;
    public int Col;

    public int PixelX;
    public int PixelY;

    public int TargetX;
    public int TargetY;

    public bool IsMoving = false;

    // Temporary storage for where the player is headed
    public int PendingRow;
    public int PendingCol;

    public bool Move(int newRow, int newCol, Panel[,] tiles, int tileSize)
    {
        if (IsMoving) return false;

        // Boundary checks
        if (newRow < 0 || newCol < 0 ||
            newRow >= tiles.GetLength(0) ||
            newCol >= tiles.GetLength(1))
            return false;

        // Wall check 
        if (tiles[newRow, newCol].BackColor == Color.FromArgb(83, 74, 183))
            return false;

        // Save the destination temporarily instead of updating immediately
        PendingRow = newRow;
        PendingCol = newCol;

        TargetX = newCol * tileSize;
        TargetY = newRow * tileSize;

        IsMoving = true;

        return true;
    }
}