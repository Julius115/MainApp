using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MainApp
{
    class View
    {
        public Dictionary<string, int> ReadWordsFromFile(string fileName)
        {
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split();
                    if (!wordsList.ContainsKey(words[words.Length -2]))
                    {
                        wordsList.Add(words[words.Length -2], Int32.Parse(words[words.Length -1]));

                    }
                }
            }
            return wordsList;
        }
    }
}
