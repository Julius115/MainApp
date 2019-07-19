using AnagramSolver.EF.CodeFirst.Contracts;
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

        public User GetUser(string userIp)
        {
            return (_em.Users.Where(u => u.UserIp == userIp).FirstOrDefault());
        }

        public void RemoveOneSearch(string userIp)
        {
            User user = _em.Users.Where(u => u.UserIp == userIp).FirstOrDefault();
            user.SearchesLeft--;
            _em.SaveChanges();
        }

        public void GiveAdditionalSearch(User user)
        {
            user.SearchesLeft++;
            _em.SaveChanges();
        }
    }
}
