using System;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(new GameField(5, 6).SetElementAt(0, 0, 1).SetElementAt(4, 5, 2));
//            Console.WriteLine(123);
        }
    }
}
