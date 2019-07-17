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
        private readonly IRequestWordContract _requestWordContract;

        public CachedWordsService(ICachedWords cachedWords, IAnagramSolver anagramSolver, ILogger logger, IRequestWordContract requestWordContract)
        {
            _cachedWords = cachedWords;
            _anagramSolver = anagramSolver;
            _logger = logger;
            _requestWordContract = requestWordContract;
        }

        public List<string> CacheWords(string requestWord, string userIp)
        {
            if (_cachedWords.CheckIfCached(requestWord))
            {
                _logger.Log(requestWord, userIp);
                return _cachedWords.GetCachedAnagrams(requestWord);
            }

            List<string> anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();
            _requestWordContract.SetRequestWord(requestWord);
            _cachedWords.SetCachedAnagrams(anagrams, requestWord);
            _logger.Log(requestWord, userIp);

            return anagrams;
        }
    }
}
