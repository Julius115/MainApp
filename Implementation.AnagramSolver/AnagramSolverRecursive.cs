using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class AnagramSolverRecursive : IAnagramSolver
    {
        private Dictionary<string, int> _words;
        

        public AnagramSolverRecursive(Dictionary<string, int> words)
        {
            _words = words;
        }
        public IList<string> GetAnagrams(string myWords)
        {
            throw new NotImplementedException();
        }
    }
}
