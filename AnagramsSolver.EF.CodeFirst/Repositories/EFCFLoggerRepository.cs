using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using static AnagramSolver.EF.CodeFirst.AnagramsDbCfContext;

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
            userLog.RequestWord = requestWord;
            userLog.UserIp = userIp;
            userLog.RequestDate = DateTime.Now;

            _em.UserLogs.Add(userLog);
            _em.SaveChanges();
        }
    }
}
