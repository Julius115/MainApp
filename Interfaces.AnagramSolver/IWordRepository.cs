using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        Dictionary<string,int> GetWordsDictionary();
    }
}
