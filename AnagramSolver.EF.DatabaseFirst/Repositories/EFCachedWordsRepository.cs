using AnagramSolver.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            //List<string> q = (from w in _em.Words
            //        from c in _em.CachedWords
            //        where c.ResponseWord == w.Id
            //        && c.RequestWord == requestWord
            //        select w.Word.ToString()).ToList();

            //List<string> a = _em.Words.Where(x => x.Id == _em.CachedWords.Select(c => c.ResponseWord)))
            //var a = _em.Words.Where(w => w.Id == _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(c => c.ResponseWord).ToList())

            //var a = _em.Words.Where(w => w.Id == _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(c => c.ResponseWord).First()).Select(w => w.Word);
            //var a = _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(u => u.ResponseWord == ))
            //var a = _em.Words.Select(w => w.Word).Where()
            //var a = _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(_em.Words.Where(a => a.Id == ))

            //var a = _em.Words.Where(w => w.Id == _em.CachedWords.Where(c => c.RequestWord == requestWord))
            var anagrams = _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(x => x.ResponseWordNavigation.Word).ToList();
            //var b = _em.Words.Select(a => a.Word == _em.CachedWords.Where(c => c.RequestWord == requestWord).Select(c => c.ResponseWord));
            return anagrams;
        }
    }
}
