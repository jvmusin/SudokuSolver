using System.Collections.Generic;

namespace SudokuSolver
{
    public interface ISudokuSolver
    {
        IEnumerable<SudokuGameField> GetAllSolutions(SudokuGameField startState);
        SudokuGameField GetSolution(SudokuGameField startState);
    }
}
