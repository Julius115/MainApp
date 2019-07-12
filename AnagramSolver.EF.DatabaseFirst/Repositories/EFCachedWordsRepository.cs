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
        private List<string> anagrams = new List<string>();


        private readonly IAnagramSolver _anagramSolver;
        private readonly AnagramsDBContext _em;

        public EFCachedWordsRepository(IAnagramSolver anagramSolver, AnagramsDBContext dbContext)
        {
            _anagramSolver = anagramSolver;
            _em = dbContext;
        }
        
        public List<string> CacheWords(string requestWord)
        {
            if (CheckIfCached(requestWord))
            {
                return GetCachedAnagrams(requestWord);
            }

            SetCachedAnagrams(requestWord);
            return anagrams;
        }

        public bool CheckIfCached(string requestWord)
        {
            return ((_em.CachedWords.Where(w => w.RequestWord == requestWord)).Count() > 0);
        }

        public void SetCachedAnagrams(string requestWord)
        {
            anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();

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
            List<string> q = (from w in _em.Words
                    from c in _em.CachedWords
                    where c.ResponseWord == w.Id
                    && c.RequestWord == requestWord
                    select w.Word.ToString()).ToList();

            return q;
        }
    }

}
