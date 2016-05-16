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

        protected static GameField FromLines(int width, int height, IEnumerable<string> lines)
        {
            var field = new GameField(width, height);
            var row = 0;
            foreach (var rowElements in lines.Select(line => line.Split(' ').Select(int.Parse)))
            {
                var column = 0;
                foreach (var element in rowElements)
                    field.SetElementAt(column++, row, element);
                row++;
            }
            return field;
        }

        protected static void Fill(ref IGameField field, Func<IGameField, int, int, int> getNumber)
        {
            foreach (var x in Enumerable.Range(0, field.Width))
                foreach (var y in Enumerable.Range(0, field.Height))
                    field = field.SetElementAt(x, y, getNumber(field, x, y));
        }
    }
}
