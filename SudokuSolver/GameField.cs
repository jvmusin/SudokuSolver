using System;
using System.Linq;

namespace SudokuSolver
{
    public class GameField : IGameField
    {
        public int Height => state.Length;
        public int Width => Height == 0 ? 0 : state[0].Length;

        private readonly int[][] state;

        public int GetElementAt(int x, int y) => state[x][y];

        public IGameField SetElementAt(int x, int y, int value)
        {
            var result = new GameField(this);
            result.state[x][y] = value;
            return result;
        }

        public bool IsFilled => !state.Any(x => x.Contains(0));

        public GameField(int width, int height)
        {
            state = Enumerable
                .Range(0, width)
                .Select(x => new int[height])
                .ToArray();
        }

        public GameField(IGameField source) : this(source.Width, source.Height)
        {
            foreach (var x in Enumerable.Range(0, Width))
                foreach (var y in Enumerable.Range(0, Height))
                    state[x][y] = source.GetElementAt(x, y);
        }

        protected bool Equals(IGameField other)
        {
            if (Height != other.Height || Width != other.Width)
                return false;
            var haveDifferentCells = (
                from x in Enumerable.Range(0, Width)
                from y in Enumerable.Range(0, Height)
                where state[x][y] != other.GetElementAt(x, y)
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
    }
}
