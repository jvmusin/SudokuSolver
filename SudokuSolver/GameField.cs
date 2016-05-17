using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class GameField : IGameField
    {
        public int Height => state.Length;
        public int Width => state[0].Length;

        private readonly int[][] state;

        public int GetElementAt(int row, int column) => state[row][column];

        public int GetElementAt(CellPosition position)
        {
            return GetElementAt(position.Row, position.Column);
        }

        public IGameField SetElementAt(int row, int column, int value)
        {
            var result = new GameField(this);
            result.state[row][column] = value;
            return result;
        }

        public IGameField SetElementAt(CellPosition position, int value)
        {
            return SetElementAt(position.Row, position.Column, value);
        }

        public bool Filled => !state.Any(x => x.Contains(0));

        public IEnumerable<int> GetRow(int row)
        {
            return Enumerable.Range(0, Width)
                .Select(column => state[row][column]);
        }

        public IEnumerable<int> GetColumn(int column)
        {
            return Enumerable.Range(0, Height)
                .Select(row => state[row][column]);
        }

        public GameField(int height, int width)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException("Size is not correct");

            state = Enumerable
                .Range(0, height)
                .Select(x => new int[width])
                .ToArray();
        }

        public GameField(IGameField source) : this(source.Height, source.Width)
        {
            foreach (var row in Enumerable.Range(0, Height))
                foreach (var column in Enumerable.Range(0, Width))
                    state[row][column] = source.GetElementAt(row, column);
        }

        public override string ToString()
        {
            var rows = Enumerable.Range(0, Height)
                .Select(row => string.Join(" ", GetRow(row)));
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
