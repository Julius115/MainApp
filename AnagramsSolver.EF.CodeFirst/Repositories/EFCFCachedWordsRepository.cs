using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AnagramSolver.EF.CodeFirst.AnagramsDbCfContext;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFCachedWordsRepository : ICachedWords
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFCachedWordsRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }

        public bool CheckIfCached(string requestWord)
        {
            return ((_em.CachedWords.Where(w => w.RequestWord == requestWord)).Count() > 0);
        }

        public void SetCachedAnagrams(List<string> anagrams, string requestWord)
        {
            Word word = new Word();

            foreach (string anagram in anagrams)
            {
                CachedWord cachedWord = new CachedWord();
                cachedWord.RequestWord = requestWord;
                //cachedWord.ResponseWord = _em.Words.Where(w => w.WordValue == anagram).Single().Id;
                _em.CachedWords.Add(cachedWord);
            }

            _em.SaveChanges();
        }

        public List<string> GetCachedAnagrams(string requestWord)
        {
            //var anagrams = _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(x => x.ResponseWordNavigation.WordValue).ToList();

            return null;
        }
    }
}
