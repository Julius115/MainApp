using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFWordSearchRepository : IWordSearch
    {
        AnagramsDBContext em = new AnagramsDBContext();

        public SearchInfoModel GetSearchInfo(string word, DateTime date)
        {
            SearchInfoModel searchInfoModel = em.UserLog.Where(u => u.RequestWord == word && u.RequestDate == date)
                .Select(u => new SearchInfoModel
                {
                    UserIp = u.UserIp,
                    RequestDate = u.RequestDate,
                    RequestWord = u.RequestWord,
                    Anagrams = em.CachedWords.Where(x => x.RequestWord == u.RequestWord)
                                                .Select(x => x.ResponseWordNavigation.Word).ToList()
                }).First();

            return searchInfoModel;
        }

        public List<string> GetWordsContainingPart(string input)
        {
            List<string> wordsResult = em.Words.Where(w => w.Word.Contains(input)).Select(w => w.Word).ToList();

            return wordsResult;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistoryInfoModels = em.UserLog.Select(u => new SearchHistoryInfoModel() { Ip = u.UserIp, RequestDate = u.RequestDate, RequestWord = u.RequestWord }).ToList();

            return searchHistoryInfoModels;
        }
    }
}
