using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IWordSearch
    {
        SearchInfoModel GetSearchInfo(string word, DateTime date);
        List<SearchHistoryInfoModel> GetSearchHistory();
        List<string> GetWordsContainingPart(string input);
    }
}
