using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public static class GameFieldExtensions
    {
        public static IGameField Fill(this IGameField field, Func<int, int, int> numerator)
        {
            foreach (var position in field.EnumerateCellPositions())
            {
                var row = position.Row;
                var column = position.Column;
                var number = numerator(row, column);
                field = field.SetElementAt(row, column, number);
            }
            return field;
        }

        public static IEnumerable<CellPosition> EnumerateCellPositions(this IGameField field)
        {
            return
                from row in Enumerable.Range(0, field.Height)
                from column in Enumerable.Range(0, field.Width)
                select new CellPosition(row, column);
        }
    }
}
