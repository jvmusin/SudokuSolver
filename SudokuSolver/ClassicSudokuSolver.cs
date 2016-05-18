using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class ClassicSudokuSolver : ISolver
    {
        public int BlockHeight { get; protected set; }
        public int BlockWidth { get; protected set; }
        public int MaxNumber => BlockHeight * BlockWidth;

        public ClassicSudokuSolver(int blockHeight, int blockWidth)
        {
            BlockHeight = blockHeight;
            BlockWidth = blockWidth;
        }

        public IEnumerable<IGameField> GetAllSolutions(IGameField startState)
        {
            return FindSolutions(startState);
        }

        public IGameField GetSolution(IGameField startState)
        {
            var solution = GetAllSolutions(startState).Take(1).ToList();
            return solution.Any() ? solution.First() : null;
        }

        private IEnumerable<IGameField> FindSolutions(IGameField field)
        {
            if (field.Filled)
            {
                yield return field;
                yield break;
            }

            foreach (var position in field.EnumerateCellPositions())
            {
                if (field.GetElementAt(position) != 0)
                    continue;

                var availableNumbers = GetAvailableNumbers(field, position);
                var solutions = availableNumbers
                    .Select(newNumber => field.SetElementAt(position, newNumber))
                    .SelectMany(FindSolutions);

                foreach (var solution in solutions)
                    yield return solution;
                yield break;
            }
        }

        private IEnumerable<int> GetAvailableNumbers(IGameField field, CellPosition position)
        {
            var numbersInSquare = GetNumbersInBlock(field, position);
            var numbersInRow = field.GetRow(position.Row);
            var numbersInColumn = field.GetColumn(position.Column);

            return Enumerable.Range(1, MaxNumber)
                .Except(numbersInSquare)
                .Except(numbersInRow)
                .Except(numbersInColumn);
        }

        private IEnumerable<int> GetNumbersInBlock(IGameField field, CellPosition position)
        {
            var topLeftRow = position.Row / BlockHeight;
            var topLeftColumn = position.Column / BlockWidth;
            foreach (var row in Enumerable.Range(topLeftRow, BlockHeight))
                foreach (var column in Enumerable.Range(topLeftColumn, BlockWidth))
                {
                    var value = field.GetElementAt(row, column);
                    if (value != 0)
                        yield return value;
                }
        }
    }
}
