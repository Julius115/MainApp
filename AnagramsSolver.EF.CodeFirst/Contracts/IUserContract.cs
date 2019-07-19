using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IUserContract
    {
        void SetUser(string userIp);

        User GetUser(string userIp);

        void GiveAdditionalSearch(User user);

        void RemoveOneSearch(string userIp);
    }
}
