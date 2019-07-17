using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFUserRepository : IUserContract
    {
        private readonly AnagramsDbCfContext _em;
        public EFCFUserRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }
        public void SetUser(string userIp)
        {
            User user = new User();
            user.UserIp = userIp;
            _em.Users.Add(user);
            _em.SaveChanges();
        }

        public bool CheckIfRegistered(string userIp)
        {
            return (_em.Users.Where(u => u.UserIp == userIp).Count() > 0);
        }

        public bool CheckIfValidToSearch(string userIp)
        {
            if (_em.Users.Where(u => u.UserIp == userIp).FirstOrDefault().SearchesLeft == 0)
            {
                return false;
            }

            User user = _em.Users.Where(u => u.UserIp == userIp).FirstOrDefault();
            user.SearchesLeft--;
            _em.SaveChanges();

            return true;
        }

        public void GiveAdditionalSearch(string userIp)
        {
            User user = _em.Users.Where(u => u.UserIp == userIp).FirstOrDefault();
            user.SearchesLeft++;
            _em.SaveChanges();

        }
    }
}
