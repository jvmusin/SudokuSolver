using System;
using System.Linq;

namespace SudokuSolver
{
    public class GameField : IGameField
    {
        public int Height => state.Length;
        public int Width => state[0].Length;

        private readonly int[][] state;

        public int GetElementAt(int row, int column) => state[row][column];

        public IGameField SetElementAt(int row, int column, int value)
        {
            var result = new GameField(this);
            result.state[row][column] = value;
            return result;
        }

        public bool Filled => !state.Any(x => x.Contains(0));
        
        public GameField(int height, int width)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException("Size is not correct");

            state = Enumerable
                .Range(0, height)
                .Select(row => new int[width])
                .ToArray();
        }

        public GameField(IGameField source) : this(source.Height, source.Width)
        {
            foreach (var position in source.EnumerateCellPositions())
            {
                var row = position.Row;
                var column = position.Column;
                state[row][column] = source.GetElementAt(row, column);
            }
        }

        public override string ToString()
        {
            var rows = Enumerable.Range(0, Height)
                .Select(row => string.Join(" ", this.GetRow(row)));
            return string.Join("\n", rows);
        }

        #region Equals and HashCode

        protected bool Equals(IGameField other)
        {
            if (Height != other.Height || Width != other.Width)
                return false;
            var haveDifferentCells = (
                from row in Enumerable.Range(0, Height)
                from column in Enumerable.Range(0, Width)
                where state[row][column] != other.GetElementAt(row, column)
                select true).Any();
            return !haveDifferentCells;
        }

        public override bool Equals(object obj)
        {
            var other = obj as IGameField;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
