using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IUserManagingService
    {
        bool CheckIfRegistered(string userIp);
        bool CheckIfValidToSearch(string userIp);
        void GiveUserAdditionalSearch(string userIp);

    }
}
