using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class EFWordRepository : IWordRepository
    {
        private Dictionary<string, int> _dictionary;
        private readonly AnagramsDBContext em = new AnagramsDBContext();

        public EFWordRepository()
        {
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            var result = em.Words;

            foreach (var w in result)
            {
                if (!wordsList.ContainsKey(w.Word))
                {
                    wordsList.Add(w.Word, 1);
                }
            }

            _dictionary = wordsList;
        }
        public void AddWord(string input)
        {
            Words word = new Words();
            word.Word = input;

            em.Words.Add(word);
            em.SaveChanges();

        }

        public List<string> GetWords(int skip, int take)
        {
            return _dictionary.Keys.Skip(skip).Take(skip * take).ToList(); ;

        }

        public Dictionary<string, int> GetWordsDictionary()
        {
            return _dictionary;
        }
    }
}
