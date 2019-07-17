using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IUserContract
    {
        void SetUser(string userIp);

        bool CheckIfRegistered(string userIp);

        bool CheckIfValidToSearch(string userIp);

        void GiveAdditionalSearch(string userIp);
    }
}
