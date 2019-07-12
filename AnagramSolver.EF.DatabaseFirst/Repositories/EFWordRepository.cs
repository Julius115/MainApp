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
        private readonly AnagramsDBContext _em;

        public EFWordRepository(AnagramsDBContext dbContext)
        {
            _em = dbContext;
            
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            var result = _em.Words;

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

            _em.Words.Add(word);
            _em.SaveChanges();

        }

        public List<string> GetWords(int skip, int take)
        {
            return _dictionary.Keys.Skip(skip).Take(take).ToList(); ;
        }

        public Dictionary<string, int> GetWordsDictionary()
        {
            return _dictionary;
        }
    }
}
