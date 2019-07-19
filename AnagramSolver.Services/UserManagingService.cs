using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Services
{
    public class UserManagingService
    {
        private readonly IUserContract _userContract;

        public UserManagingService (IUserContract userContract)
        {
            _userContract = userContract;
        }

        public bool CheckIfRegistered(string userIp)
        {
            return _userContract.GetUser(userIp) != null;
        }

        public bool CheckIfValidToSearch(string userIp)
        {
            if (_userContract.GetUser(userIp).SearchesLeft == 0)
                return false;

            _userContract.RemoveOneSearch(userIp);

            return true;
        }

        public void GiveUserAdditionalSearch(string userIp)
        {
            User user = _userContract.GetUser(userIp);

            if (user == null)
            {
                throw new Exception(message: $"User with ip: {userIp} doesn't exist");
            }

            _userContract.GiveAdditionalSearch(user);
        }
    }
}
