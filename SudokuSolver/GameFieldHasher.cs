using System.Linq;

namespace SudokuSolver
{
    public class GameFieldHasher : IGameFieldHasher
    {
        public int HashBase { get; }

        public GameFieldHasher(int hashBase)
        {
            HashBase = hashBase;
        }

        public int Hash(IGameField field)
        {
            var hash = 0;

            foreach (var x in Enumerable.Range(0, field.Width))
                foreach (var y in Enumerable.Range(0, field.Height))
                    unchecked
                    {
                        hash = hash*HashBase + field.GetElementAt(x, y);
                    }

            return hash;
        }
    }
}
