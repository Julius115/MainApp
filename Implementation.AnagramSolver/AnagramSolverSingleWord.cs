using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class AnagramSolverSingleWord : IAnagramSolver
    {
        private Dictionary<string, int> _words;
        public AnagramSolverSingleWord (Dictionary<string, int> words)
        {
            _words = words;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            List<string> anagrams = new List<string>();

            List<string> _input = myWords.Split().ToList();

            foreach (string s in _input)
            {
                foreach (var keyValue in _words)
                {
                    if (String.Concat(s.OrderBy(c => c)).Equals(String.Concat(keyValue.Key.OrderBy(c => c))) && !anagrams.Contains(keyValue.Key))
                    {
                        anagrams.Add(keyValue.Key);
                    }
                }
            }

            return anagrams;
        }
    }
}
