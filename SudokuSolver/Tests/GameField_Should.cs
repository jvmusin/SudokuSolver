using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class GameField_Should : TestBase
    {
        private IGameField field;
        private Func<int, int,int> byRowEnumerator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            field = new GameField(5, 6);
            byRowEnumerator = GetRowEnumerator(field);
        }

        [Test]
        public void SaySizeCorrectly_WhenSizeIsCorrect()
        {
            var width = 6;
            var height = 5;
            field = new GameField(width, height);

            field.Width.Should().Be(width);
            field.Height.Should().Be(height);
        }

        [Test]
        public void Fail_WhenSizeIsNotCorrect(
            [Values(-5, 0, 5)] int width,
            [Values(-6, 0, 6)] int height)
        {
            if (width > 0 && height > 0)
                return;
            Action create = () => new GameField(width, height);
            create.ShouldThrow<Exception>();
        }

        [Test]
        public void RememberCellValuesCorrectly()
        {
            Fill(ref field, byRowEnumerator);

            foreach (var x in Enumerable.Range(0, field.Width))
                foreach (var y in Enumerable.Range(0, field.Height))
                    field.GetElementAt(x, y).Should().Be(byRowEnumerator(x, y));
        }

        [Test]
        public void ReturnRealValuesOnGetRow()
        {
            Fill(ref field, byRowEnumerator);

            foreach (var y in Enumerable.Range(0, field.Height))
            {
                var row = field.GetRow(y).ToList();
                row.Count.Should().Be(field.Width);
                foreach (var x in Enumerable.Range(0, field.Width))
                    row[x].Should().Be(byRowEnumerator(x, y));
            }
        }

        [Test]
        public void ReturnRealValuesOnGetColumn()
        {
            Fill(ref field, byRowEnumerator);

            foreach (var x in Enumerable.Range(0, field.Width))
            {
                var column = field.GetColumn(x).ToList();
                column.Count.Should().Be(field.Height);
                foreach (var y in Enumerable.Range(0, field.Height))
                    column[y].Should().Be(byRowEnumerator(x, y));
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
        public void SayThatItsNotFilled_WhenJustCreated()
        {
            field.Filled.Should().BeFalse();
        }

        [Test]
        public void SayThatItsNotFilled_WhenThereAreSomeZeroCells()
        {
            Fill(ref field, byRowEnumerator);
            field = field
                .SetElementAt(2, 3, 0)
                .SetElementAt(0, 0, 0);
            field.Filled.Should().BeFalse();
        }

        [Test]
        public void SayThatItsFilled_WhenThereAreNoZeroCells()
        {
            Fill(ref field, byRowEnumerator);
            field = field.SetElementAt(0, 0, 1);
            field.Filled.Should().BeTrue();
        }
    }
}
