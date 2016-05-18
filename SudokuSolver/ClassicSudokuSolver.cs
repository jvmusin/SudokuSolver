using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class ClassicSudokuSolver : ISolver
    {
        public IEnumerable<IGameField> GetAllSolutions(IGameField startState)
        {
            return FindSolutions(startState);
        }

        public IGameField GetSolution(IGameField startState)
        {
            var solution = GetAllSolutions(startState).Take(1).ToList();
            return solution.Any() ? solution.First() : null;
        }

        private static IEnumerable<IGameField> FindSolutions(IGameField field)
        {
            if (field.Filled)
            {
                yield return field;
                yield break;
            }

            foreach (var position in field.EnumerateCellPositions())
            {
                var availableNumbers = GetAvailableNumbers(field, position);
                var allSolutions = availableNumbers
                    .Select(newNumber => field.SetElementAt(position, newNumber))
                    .SelectMany(FindSolutions).ToList();

                foreach (var solution in allSolutions)
                    yield return solution;
            }
        }

        private static IEnumerable<int> GetAvailableNumbers(IGameField field, CellPosition position)
        {
            if (field.GetElementAt(position) != 0)
                return Enumerable.Empty<int>();

            var row = position.Row;
            var column = position.Column;

            var topLeftRow = row / 3;
            var topLeftColumn = column / 3;

            var numbersInSquare = GetNumbersInSquare(field, topLeftRow, topLeftColumn).ToList();
            var numbersInRow = field.GetRow(row).ToList();
            var numersInColumn = field.GetColumn(column).ToList();

            var result = Enumerable.Range(1, 9)
                .Except(numbersInSquare)
                .Except(numbersInRow)
                .Except(numersInColumn)
                .ToList();

            return result;
        }

        private static IEnumerable<int> GetNumbersInSquare(IGameField field, int topLeftRow, int topLeftColumn)
        {
            foreach (var row in Enumerable.Range(topLeftRow, 3))
                foreach (var column in Enumerable.Range(topLeftColumn, 3))
                {
                    var value = field.GetElementAt(row, column);
                    if (value != 0)
                        yield return value;
                }
        }
    }
}
