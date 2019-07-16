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
            //SearchInfoModel searchInfoModel = _em.UserLogs.Where(u => u.RequestWord == word && u.RequestDate == date)
            //    .Select(u => new SearchInfoModel
            //    {
            //        UserIp = u.UserIp,
            //        RequestDate = u.RequestDate,
            //        RequestWord = u.RequestWord,
            //        Anagrams = u.CachedWords.Select(x => x.Word.WordValue).ToList()
            //    }).First();
            //
            //return searchInfoModel;

            //SearchInfoModel searchInfoModel = _em.UserLogs.Where(u => u.RequestWord.Word == word && u.RequestDate == date)
            //    .Select(u => new SearchInfoModel
            //    {
            //        UserIp = u.UserIp,
            //        RequestDate = u.RequestDate,
            //        RequestWord = u.RequestWord.Word,
            //        //Anagrams = u.Ca
            //    });
            //SearchInfoModel searchInfoModel = _em.RequestWords.Where(r => r.Word == word)
            return null;
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
