using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.Services
{
    public class CachedWordsService
    {
        private readonly ICachedWords _cachedWords;
        private readonly IAnagramSolver _anagramSolver;

        public CachedWordsService(ICachedWords cachedWords, IAnagramSolver anagramSolver)
        {
            _cachedWords = cachedWords;
            _anagramSolver = anagramSolver;
        }

        public List<string> CacheWords(string requestWord)
        {
            if (_cachedWords.CheckIfCached(requestWord))
            {
                return _cachedWords.GetCachedAnagrams(requestWord);
            }

            List<string> anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();
            _cachedWords.SetCachedAnagrams(anagrams, requestWord);
            return anagrams;
        }
    }
}
