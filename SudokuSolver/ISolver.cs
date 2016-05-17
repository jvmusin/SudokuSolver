using System.Collections.Generic;

namespace SudokuSolver
{
    public interface ISolver
    {
        IEnumerable<IGameField> GetAllSolutions(IGameField startState);
        IGameField GetSolution(IGameField startState);
    }
}
