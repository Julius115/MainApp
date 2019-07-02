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
        //private List<string> _input;
        public AnagramSolverSingleWord (Dictionary<string, int> words)
        {
            _words = words;
            //_input = input;
        }

        public IList<string> SolveAnagrams(string myWords)
        {
            List<string> anagrams = new List<string>();

            List<string> _input = myWords.Split().ToList();

            foreach (string s in _input)
            {
                foreach (KeyValuePair<string, int> k in _words)
                {
                    if (String.Concat(s.OrderBy(c => c)).Equals(String.Concat(k.Key.OrderBy(c => c))) && !anagrams.Contains(k.Key))
                    {
                        anagrams.Add(k.Key);
                    }
                }
            }

            return anagrams;
        }
    }
}
