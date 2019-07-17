using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFDictionaryWordRepository : IWordRepository
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFDictionaryWordRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }
        public void AddWord(string input)
        {
            DictionaryWord word = new DictionaryWord();
            word.Word = input;

            _em.DictionaryWords.Add(word);
            _em.SaveChanges();
        }

        public List<string> GetWords(int skip, int take)
        {
            return _em.DictionaryWords.OrderBy(x => x.Word).Select(x => x.Word).Skip(skip * take).Take(take).ToList();
        }

        public List<string> GetWordsDictionary()
        {
            return _em.DictionaryWords.OrderBy(x => x.Word).Select(x => x.Word).ToList();
        }

        public List<string> GetWordsContainingPart(string searchPhrase)
        {
            List<string> searchResults = _em.DictionaryWords.Where(d => d.Word.Contains(searchPhrase)).Select(d => d.Word).ToList();

            return searchResults;
        }
    }
}
