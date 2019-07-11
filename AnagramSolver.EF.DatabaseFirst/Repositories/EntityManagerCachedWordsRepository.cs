using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{

    public class EntityManagerCachedWordsRepository : ICachedWords
    {
        private readonly IAnagramSolver _anagramSolver;
        AnagramsDBContext em = new AnagramsDBContext();

        private List<string> anagrams = new List<string>();

        public EntityManagerCachedWordsRepository(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
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
            return ((em.CachedWords.Where(w => w.RequestWord == requestWord)).Count() > 0);
        }

        public void SetCachedAnagrams(string requestWord)
        {
            anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();

            Words word = new Words();

            foreach (string anagram in anagrams)
            {
                CachedWords cachedWord = new CachedWords();
                cachedWord.RequestWord = requestWord;
                cachedWord.ResponseWord = em.Words.Where(w => w.Word == anagram).Single().Id;
                em.CachedWords.Add(cachedWord);
            }

            em.SaveChanges();
        }

        public List<string> GetCachedAnagrams(string requestWord)
        {
            List<string> q = (from w in em.Words
                    from c in em.CachedWords
                    where c.ResponseWord == w.Id
                    && c.RequestWord == requestWord
                    select w.Word.ToString()).ToList();

            return q;
        }
    }

}
