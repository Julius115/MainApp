using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFLoggerRepository : ILogger
    {
        private readonly AnagramsDBContext _em;

        public EFLoggerRepository(AnagramsDBContext dbContext)
        {
            _em = dbContext;
        }

        public void Log(string requestWord, string userIp)
        {
            UserLog userLog = new UserLog();
            userLog.RequestWord = requestWord;
            userLog.UserIp = userIp;
            userLog.RequestDate = DateTime.Now;

            _em.UserLog.Add(userLog);
            _em.SaveChanges();
        }
    }
}
