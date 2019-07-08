using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        Dictionary<string,int> GetWordsDictionary();
        List<string> GetWords(int skip, int take);
        void AddWord(string input);
    }
}
