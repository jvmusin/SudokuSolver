using System.Collections.Generic;

namespace SudokuSolver
{
    public interface IGameField
    {
        int Height { get; }
        int Width { get; }

        int GetElementAt(int row, int column);
        int GetElementAt(CellPosition position);
        IGameField SetElementAt(int row, int column, int value);
        IGameField SetElementAt(CellPosition position, int value);

        IEnumerable<int> GetRow(int row);
        IEnumerable<int> GetColumn(int column);

        bool Filled { get; }
    }
}
