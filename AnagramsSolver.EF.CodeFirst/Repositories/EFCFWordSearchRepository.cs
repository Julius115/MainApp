using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFWordSearchRepository :IWordSearch
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFWordSearchRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }

        public SearchInfoModel GetSearchInfo(string word, DateTime date)
        {
            SearchInfoModel searchInfoModel = _em.UserLogs.Where(u => u.RequestWord == word)
                .Select(u => new SearchInfoModel
                {
                    UserIp = u.UserIp,
                    RequestDate = u.RequestDate,
                    RequestWord = u.RequestWord,
                    //Anagrams = u. .Select(x => x.Word.WordValue).ToList()
                }).First();

            return searchInfoModel;
        }

        public List<string> GetWordsContainingPart(string input)
        {
            List<string> wordsResult = _em.Words.Where(w => w.WordValue.Contains(input)).Select(w => w.WordValue).ToList();

            return wordsResult;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistoryInfoModels = _em.UserLogs.Select(u => new SearchHistoryInfoModel() { Ip = u.UserIp, RequestDate = u.RequestDate, RequestWord = u.RequestWord }).ToList();

            return searchHistoryInfoModels;
        }
    }
}
