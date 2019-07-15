using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverSingleWord : IAnagramSolver
    {
        private List<string> _words;
        private readonly IWordRepository _wordRepository;

        public AnagramSolverSingleWord (IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            _words = _wordRepository.GetWordsDictionary();
        }

        public IList<string> GetAnagrams(string myWords)
        {
            List<string> anagrams = new List<string>();

            List<string> _input = myWords.Split().ToList();

            foreach (string s in _input)
            {
                foreach (string word in _words)
                {
                    if (String.Concat(s.OrderBy(c => c)).Equals(String.Concat(word.OrderBy(c => c))) && !anagrams.Contains(word))
                    {
                        anagrams.Add(word);
                    }
                }
            }

            return anagrams;
        }
    }
}
