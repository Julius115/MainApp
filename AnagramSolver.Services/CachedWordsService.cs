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
        private readonly ILogger _logger;

        public CachedWordsService(ICachedWords cachedWords, IAnagramSolver anagramSolver, ILogger logger)
        {
            _cachedWords = cachedWords;
            _anagramSolver = anagramSolver;
            _logger = logger;
        }

        public List<string> CacheWords(string requestWord, string userIp)
        {
            if (_cachedWords.CheckIfCached(requestWord))
            {
                _logger.Log(requestWord, userIp);
                return _cachedWords.GetCachedAnagrams(requestWord);
            }

            List<string> anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();
            _cachedWords.SetCachedAnagrams(anagrams, requestWord);
            _logger.Log(requestWord, userIp);

            return anagrams;
        }
    }
}
