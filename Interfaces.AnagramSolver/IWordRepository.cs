using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        List<string> GetWordsDictionary();
        List<string> GetWords(int skip, int take);
        void AddWord(string input);
    }
}
