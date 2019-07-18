using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IWordRepository
    {
        List<string> GetWordsDictionary();
        List<string> GetWords(int skip, int take);
        void AddWord(string input);
        List<string> GetWordsContainingPart(string searchPhrase);
        bool EditWord(string originalWord, string newWord);
        void DeleteWord(string wordToDelete);
    }
}
