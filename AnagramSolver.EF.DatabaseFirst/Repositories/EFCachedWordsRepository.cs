using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFCachedWordsRepository : ICachedWords
    {
        private readonly AnagramsDBContext _em;

        public EFCachedWordsRepository(AnagramsDBContext dbContext)
        {
            _em = dbContext;
        }

        public bool CheckIfCached(string requestWord)
        {
            return ((_em.CachedWords.Where(w => w.RequestWord == requestWord)).Count() > 0);
        }

        public void SetCachedAnagrams(List<string> anagrams, string requestWord)
        {
            Words word = new Words();

            foreach (string anagram in anagrams)
            {
                CachedWords cachedWord = new CachedWords();
                cachedWord.RequestWord = requestWord;
                cachedWord.ResponseWord = _em.Words.Where(w => w.Word == anagram).Single().Id;
                _em.CachedWords.Add(cachedWord);
            }

            _em.SaveChanges();
        }

        public List<string> GetCachedAnagrams(string requestWord)
        {
            var anagrams = _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(x => x.ResponseWordNavigation.Word).ToList();

            return anagrams;
        }
    }
}
