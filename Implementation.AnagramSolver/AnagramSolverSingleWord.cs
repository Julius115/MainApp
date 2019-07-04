using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolverSingleWord : IAnagramSolver
    {
        private Dictionary<string, int> _words;

        public AnagramSolverSingleWord ()
        {
            
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader("zodynas.txt"))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split();

                    if (!wordsList.ContainsKey(words[0]))
                    {
                        wordsList.Add(words[0], Int32.Parse(words[words.Length - 1]));
                    }
                    if (!wordsList.ContainsKey(words[words.Length - 2]))
                    {
                        wordsList.Add(words[words.Length - 2], Int32.Parse(words[words.Length - 1]));

                    }

                }
            }

            _words = wordsList;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            List<string> anagrams = new List<string>();

            List<string> _input = myWords.Split().ToList();

            //foreach (string s in _input)
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
