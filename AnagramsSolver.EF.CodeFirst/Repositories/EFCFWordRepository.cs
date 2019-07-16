using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFWordRepository : IWordRepository
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFWordRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }
        public void AddWord(string input)
        {
            //Word word = new Word();
            //word.WordValue = input;
            //
            //_em.Words.Add(word);
            //_em.SaveChanges();
        }

        public List<string> GetWords(int skip, int take)
        {
            return _em.DictionaryWords.OrderBy(x => x.Word).Select(x => x.Word).Skip(skip * take).Take(take).ToList();
        }

        public List<string> GetWordsDictionary()
        {
            return _em.DictionaryWords.OrderBy(x => x.Word).Select(x => x.Word).ToList();
        }
    }
}
