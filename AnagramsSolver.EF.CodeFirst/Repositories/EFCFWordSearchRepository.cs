using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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
            SearchInfoModel searchInfoModel = _em.UserLogs.Where(u => u.RequestWord.Word == word && (u.RequestDate.ToString("yyyy-MM-dd HH:mm:ss.fff") == date.ToString("yyyy-MM-dd HH:mm:ss.fff")))
                .Select(u => new SearchInfoModel
                {
                    UserIp = u.UserIp,
                    RequestDate = u.RequestDate,
                    RequestWord = u.RequestWord.Word,
                    Anagrams = u.RequestWord.CachedWords.Select(c => c.DictionaryWord.Word).ToList()
                }).First() ;

            return searchInfoModel;
        }

        public List<string> GetWordsContainingPart(string searchPhrase)
        {
            List<string> searchResults = _em.DictionaryWords.Where(d => d.Word.Contains(searchPhrase)).Select(d => d.Word).ToList();

            return searchResults;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistoryInfoModels = _em.UserLogs.Select(u => new SearchHistoryInfoModel() { Ip = u.UserIp, RequestDate = u.RequestDate, RequestWord = u.RequestWord.Word }).ToList();

            return searchHistoryInfoModels;
        }
    }
}
