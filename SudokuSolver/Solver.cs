using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class Solver : ISolver
    {
        public IEnumerable<IGameField> GetAllSolutions(IGameField startState)
        {
            return Enumerable.Empty<IGameField>();
        }

        public IGameField GetSolution(IGameField startState)
        {
            var solution = GetAllSolutions(startState).Take(1).ToList();
            return solution.Any() ? solution.First() : null;
        }
    }
}
