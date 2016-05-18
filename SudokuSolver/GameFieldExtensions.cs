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
        
        public static int GetElementAt(this IGameField field, CellPosition position)
        {
            return field.GetElementAt(position.Row, position.Column);
        }
        
        public static IGameField SetElementAt(this IGameField field, CellPosition position, int value)
        {
            return field.SetElementAt(position.Row, position.Column, value);
        }

        public static IEnumerable<int> GetRow(this IGameField field, int row)
        {
            return Enumerable.Range(0, field.Width)
                .Select(column => field.GetElementAt(row, column));
        }

        public static IEnumerable<int> GetColumn(this IGameField field, int column)
        {
            return Enumerable.Range(0, field.Height)
                .Select(row => field.GetElementAt(row, column));
        }
    }
}
