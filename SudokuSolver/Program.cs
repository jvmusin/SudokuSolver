using System;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(new GameField(5, 5).SetElementAt(0, 0, 100500).SetElementAt(4, 4, 100500));
//            Console.WriteLine(123);
        }
    }
}
