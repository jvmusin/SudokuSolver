using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var testFileName = "Samples/ClassicSudokuSample1.txt";
            var field = GameFieldFromLines(File.ReadLines(testFileName));
            var solver = new ClassicSudokuSolver();
            var solutions = solver.GetAllSolutions(field);
            foreach (var solution in solutions)
            {
                Console.WriteLine(solution);
                Console.WriteLine();
                Console.WriteLine("-----------------------------");
                Console.WriteLine();
            }
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
    }
}
