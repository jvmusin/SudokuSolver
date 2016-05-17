using System;
using System.Linq;

namespace SudokuSolver
{
    public static class GameFieldExtensions
    {
        public static IGameField Fill(this IGameField field, Func<int, int, int> numerator)
        {
            foreach (var row in Enumerable.Range(0, field.Height))
                foreach (var column in Enumerable.Range(0, field.Width))
                    field = field.SetElementAt(row, column, numerator(row, column));
            return field;
        }
    }
}
