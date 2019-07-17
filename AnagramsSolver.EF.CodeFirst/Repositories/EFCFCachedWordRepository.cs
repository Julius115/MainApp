using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFCachedWordRepository : ICachedWords
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFCachedWordRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }

        public bool CheckIfCached(string requestWord)
        {
            return ((_em.CachedWords.Where(c => c.RequestWord.Word == requestWord)).Count() > 0);
        }

        public void SetCachedAnagrams(List<string> anagrams, string requestWord)
        {
            foreach (string anagram in anagrams)
            {
                CachedWord cachedWord = new CachedWord();
                cachedWord.RequestWordId = _em.RequestWords.Where(r => r.Word == requestWord).Select(r => r.Id).FirstOrDefault();
                cachedWord.DictionaryWordId = _em.DictionaryWords.Where(d => d.Word == anagram).Select(d => d.Id).FirstOrDefault();
                _em.CachedWords.Add(cachedWord);
            }

            _em.SaveChanges();
        }

        public List<string> GetCachedAnagrams(string requestWord)
        {
            List<string> anagrams = _em.CachedWords.Where(c => c.RequestWord.Word == requestWord).Select(c => c.DictionaryWord.Word).ToList();

            return anagrams;
        }
    }
}
