using OpenTK.Mathematics;

namespace DemoOpenTK
{
    internal static class MatrixHelper
    {

        public static bool InRange(Vector2i cell, int minValue, int maxValue)
            => cell.X <= maxValue && cell.X >= minValue && cell.Y <= maxValue && cell.Y >= minValue;

        public static bool TryFindRandomFreeCell(bool[,] occupiedCells, out Vector2i freeCell)
        {
            Random random = new();
            int size = occupiedCells.GetLength(0);

            int x = random.Next(0, size);
            int y = random.Next(0, size);

            bool[,] viewedCells = new bool[size, size];
            Stack<Vector2i> stack = new();
            stack.Push(new Vector2i(x, y));

            while (stack.Any())
            {
                Vector2i cell = stack.Pop();
                if (!occupiedCells[cell.X, cell.Y])
                {
                    freeCell = cell;
                    return true;
                }

                viewedCells[cell.X, cell.Y] = true;


                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Vector2i newCell = new(cell.X + i, cell.Y + j);
                        if (!InRange(newCell, 0, size - 1))
                            continue;

                        if (viewedCells[newCell.X, newCell.Y])
                            continue;

                        stack.Push(newCell);
                    }
                }
            }

            freeCell = new Vector2i(-1, -1);
            return false;
        }
    }
}
