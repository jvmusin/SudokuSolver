using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SudokuSolver.Tests
{
    public abstract class TestBase
    {
        protected readonly Random rnd = new Random();

        [SetUp]
        public virtual void SetUp()
        {
        }

        protected static IGameField GameFieldFromLines(IEnumerable<string> lines)
        {
            var fieldData = lines
                .Select(line => line.Split(' ').Select(int.Parse).ToList())
                .ToList();
            var height = fieldData.Count;
            var width = fieldData[0].Count;
            IGameField field = new GameField(height, width);

            foreach (var row in Enumerable.Range(0, height))
                foreach (var column in Enumerable.Range(0, width))
                    field = field.SetElementAt(row, column, fieldData[row][column]);

            return field;
        }

        protected Func<int, int, int> GetByRowEnumerator(IGameField field)
            => (row, column) => row * field.Width + column;
    }
}
