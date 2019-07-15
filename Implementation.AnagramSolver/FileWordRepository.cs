using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class FileWordRepository : IWordRepository
    {
        private List<string> _dictionary;

        public FileWordRepository(string fileName)
        {
            List<string> wordsList = new List<string>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split();

                    if (!wordsList.Contains(words[0]))
                    {
                        wordsList.Add(words[0]);
                    }
                    if (!wordsList.Contains(words[words.Length - 2]))
                    {
                        wordsList.Add(words[words.Length - 2]);
                    }
                }
            }

            _dictionary = wordsList;
        }

        public List<string> GetWords(int skip, int take)
        {
            return _dictionary.OrderBy(x => x).Skip(skip * take).Take(take).ToList();
        }

        public void AddWord(string input)
        {

            var inputWords = input.Split();

            KeyValuePair<string, int> keyValue;

            if (!_dictionary.Contains(inputWords[0]))
            {
                _dictionary.Add(inputWords[0]);
            }
            if (!_dictionary.Contains(inputWords[inputWords.Length - 2]))
            {
                _dictionary.Add(inputWords[inputWords.Length - 2]);
            }
        }

        public List<string> GetWordsDictionary()
        {
            return _dictionary;
        }
    }
}
