using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFLoggerRepository :ILogger
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFLoggerRepository(AnagramsDbCfContext dbContext)
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
    }
}
