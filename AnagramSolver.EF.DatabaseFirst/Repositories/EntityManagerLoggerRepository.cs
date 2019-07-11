using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EntityManagerLoggerRepository : ILogger
    {
        AnagramsDBContext em = new AnagramsDBContext();

        public void Log(string requestWord, string userIp)
        {
            UserLog userLog = new UserLog();
            userLog.RequestWord = requestWord;
            userLog.UserIp = userIp;
            userLog.RequestDate = DateTime.Now;

            em.UserLog.Add(userLog);
            em.SaveChanges();
        }
    }
}
