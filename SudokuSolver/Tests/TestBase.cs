using System;
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
    }
}
