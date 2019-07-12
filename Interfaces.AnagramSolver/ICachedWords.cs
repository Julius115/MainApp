using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface ICachedWords
    {
        //List<string> CacheWords(string requestWord);
        bool CheckIfCached(string requestWord);

        void SetCachedAnagrams(List<string> anagrams, string requestWord);

        List<string> GetCachedAnagrams(string requestWord);

    }
}
