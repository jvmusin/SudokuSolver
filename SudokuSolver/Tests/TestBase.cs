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

        protected Func<int, int, int> GetByRowEnumerator(IGameField field)
            => (row, column) => row * field.Width + column;
    }
}
