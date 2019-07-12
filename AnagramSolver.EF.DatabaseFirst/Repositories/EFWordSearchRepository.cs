using AnagramSolver.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFWordSearchRepository : IWordSearch
    {
        private readonly AnagramsDBContext _em;

        public EFWordSearchRepository (AnagramsDBContext dbContext)
        {
            _em = dbContext;
        }

        public SearchInfoModel GetSearchInfo(string word, DateTime date)
        {
            SearchInfoModel searchInfoModel = _em.UserLog.Where(u => u.RequestWord == word && u.RequestDate == date)
                .Select(u => new SearchInfoModel
                {
                    UserIp = u.UserIp,
                    RequestDate = u.RequestDate,
                    RequestWord = u.RequestWord,
                    Anagrams = _em.CachedWords.Where(x => x.RequestWord == u.RequestWord)
                                                .Select(x => x.ResponseWordNavigation.Word).ToList()
                }).First();

            return searchInfoModel;
        }

        public List<string> GetWordsContainingPart(string input)
        {
            List<string> wordsResult = _em.Words.Where(w => w.Word.Contains(input)).Select(w => w.Word).ToList();

            return wordsResult;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistoryInfoModels = _em.UserLog.Select(u => new SearchHistoryInfoModel() { Ip = u.UserIp, RequestDate = u.RequestDate, RequestWord = u.RequestWord }).ToList();

            return searchHistoryInfoModels;
        }
    }
}
