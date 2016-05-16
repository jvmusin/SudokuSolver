using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class GameField_Should : TestBase
    {
        private GameField field;
        private static readonly Func<IGameField, int, int, int> Indexer 
            = (field0, x, y) => y*field0.Width + x;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            field = new GameField(5, 3);
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
            IGameField field = this.field;

            Fill(ref field, Indexer);

            foreach (var x in Enumerable.Range(0, field.Width))
                foreach (var y in Enumerable.Range(0, field.Height))
                    field.GetElementAt(x, y).Should().Be(Indexer(field, x, y));
        }

        [Test]
        public void ReturnRealValuesOnGetRow()
        {
            IGameField field = this.field;

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
            IGameField field = this.field;

            Fill(ref field, Indexer);

            foreach (var x in Enumerable.Range(0, field.Width))
            {
                var column = field.GetColumn(x).ToList();
                for (var y = 0; y < column.Count; y++)
                    column[y].Should().Be(Indexer(field, x, y));
            }
        }

        [Test]
        public void HaveZeroCellValues_ByDefault()
        {
            Enumerable.Range(0, field.Width)
                .SelectMany(y => field.GetColumn(y))
                .ShouldAllBeEquivalentTo(0);
        }
    }
}
