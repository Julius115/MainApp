using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class FileWordRepository : IWordRepository
    {
        private Dictionary<string, int> _dictionary;

        public FileWordRepository(string fileName)
        {
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(fileName))
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

            _dictionary = wordsList;
        }

        public List<string> GetWords(int skip, int take)
        {
            return _dictionary.Keys.ToList().Skip(skip * take).Take(take).ToList();
        }

        public void AddWord(KeyValuePair<string, int> keyValue)
        {
            ((ICollection<KeyValuePair<string, int>>)_dictionary).Add(keyValue);
        }

        public Dictionary<string, int> GetWordsDictionary()
        {
            return _dictionary;
        }
    }
}
