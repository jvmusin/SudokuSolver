using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class GameField_Should : TestBase
    {
        private IGameField field;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            field = new GameField(5, 6);
        }

        [Test]
        public void SayItsRealSize()
        {
            var width = 15;
            var height = 10;
            field = new GameField(width, height);

            field.Height.Should().Be(height);
            field.Width.Should().Be(width);
        }

        [Test]
        public void RememberCellValuesCorrectly()
        {
            Fill(ref field, Indexer);

            foreach (var x in Enumerable.Range(0, field.Width))
                foreach (var y in Enumerable.Range(0, field.Height))
                    field.GetElementAt(x, y).Should().Be(Indexer(field, x, y));
        }

        [Test]
        public void ReturnRealValuesOnGetRow()
        {
            Fill(ref field, Indexer);

            foreach (var y in Enumerable.Range(0, field.Height))
            {
                var row = field.GetRow(y).ToList();
                for (var x = 0; x < row.Count; x++)
                    row[x].Should().Be(Indexer(field, x, y));
            }
        }

        [Test]
        public void ReturnRealValuesOnGetColumn()
        {
            Fill(ref field, Indexer);

            foreach (var x in Enumerable.Range(0, field.Width))
            {
                var column = field.GetColumn(x).ToList();
                for (var y = 0; y < column.Count; y++)
                    column[y].Should().Be(Indexer(field, x, y));
            }
        }

        [Test]
        public void HaveZeroValues_ByDefault()
        {
            Enumerable.Range(0, field.Width)
                .SelectMany(y => field.GetColumn(y))
                .ShouldAllBeEquivalentTo(0);
        }

        [Test]
        public void SayThatItsNotFilled_WhenThereAreSomeZeroCells()
        {
            Fill(ref field, Indexer);
            field = field
                .SetElementAt(2, 3, 0)
                .SetElementAt(0, 0, 0);
            field.Filled.Should().BeFalse();
        }

        [Test]
        public void SayThatItsFilled_WhenThereAreNoZeroCells()
        {
            Fill(ref field, Indexer);
            field = field.SetElementAt(0, 0, 1);
            field.Filled.Should().BeTrue();
        }
    }
}
