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
            myWords = myWords.Replace(" ", String.Empty);
            var inputCharsDictionary = new Dictionary<char, int>();
            for (int i = 0; i < myWords.Length; i++)
            {
                if (!inputCharsDictionary.ContainsKey(myWords[i]))
                {
                    inputCharsDictionary.Add(myWords[i], 1);
                }
                else
                {
                    inputCharsDictionary[myWords[i]]++;
                }
            }

            for (int i = 0; i < _words.Count; i++)
            {
                GetAnagramsRecursively(inputCharsDictionary, i ,new List<int>() );
            }

            return new List<string>();
        }

        public List<string> GetAnagramsRecursively(Dictionary<char, int> inputChars, int lastIndex, List<int> listOfIndexes)
        {
            
            

            return new List<string>();
        }
    }
}
