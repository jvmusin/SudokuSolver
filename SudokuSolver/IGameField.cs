namespace SudokuSolver
{
    public interface IGameField
    {
        int Height { get; }
        int Width { get; }

        int GetElementAt(int row, int column);
        IGameField SetElementAt(int row, int column, int value);

        bool Filled { get; }
    }
}
