using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFUserLogRepository :ILogger
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFUserLogRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }

        public void Log(string requestWord, string userIp)
        {
            UserLog userLog = new UserLog();
            userLog.RequestWordId = _em.RequestWords.Where(r => r.Word == requestWord).Select(r => r.Id).FirstOrDefault();
            userLog.UserIp = userIp;
            userLog.RequestDate = DateTime.Now;

            _em.UserLogs.Add(userLog);
            _em.SaveChanges();
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
                }).First();

            return searchInfoModel;
        }

        public List<SearchHistoryInfoModel> GetSearchHistory()
        {
            List<SearchHistoryInfoModel> searchHistoryInfoModels = _em.UserLogs.Select(u => new SearchHistoryInfoModel() { Ip = u.UserIp, RequestDate = u.RequestDate, RequestWord = u.RequestWord.Word }).ToList();

            return searchHistoryInfoModels;
        }
    }
}
