using System.Collections.Generic;

namespace SudokuSolver
{
    public interface IGameField
    {
        int Height { get; }
        int Width { get; }

        int GetElementAt(int x, int y);
        IGameField SetElementAt(int x, int y, int value);

        IEnumerable<int> GetRow(int rowIndex);
        IEnumerable<int> GetColumn(int columnIndex);  

        bool IsFilled { get; }
    }
}
