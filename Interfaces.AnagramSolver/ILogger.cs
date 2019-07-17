using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface ILogger
    {
        void Log(string requestWord, string userIp);
        SearchInfoModel GetSearchInfo(string word, DateTime date);
        List<SearchHistoryInfoModel> GetSearchHistory();
    }
}
