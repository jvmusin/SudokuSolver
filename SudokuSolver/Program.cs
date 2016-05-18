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
            var solver = new ClassicSudokuSolver(3, 3);
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

            return new GameField(height, width, (row, column) => fieldData[row][column]);
        }
    }
}
