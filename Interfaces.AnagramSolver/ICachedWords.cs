using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface ICachedWords
    {
        List<string> CacheWords(string requestWord);
    }
}
