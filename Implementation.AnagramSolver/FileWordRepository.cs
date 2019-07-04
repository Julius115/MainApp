using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnagramSolver.BusinessLogic
{
    public class FileWordRepository : IWordRepository
    {
        public string _fileName;

        public FileWordRepository(string fileName)
        {
            _fileName = fileName;
        }
        public Dictionary<string, int> GetWordsDictionary()
        {
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(_fileName))
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
            return wordsList;
        }
    }
}
