using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Tests.ServicesTests
{
    class UserManagingServiceTests
    {
        private IUserRepository _userContract;
        private UserManagingService _userManagingService;

        [SetUp]
        public void Setup()
        {
            _userContract = Substitute.For<IUserRepository>();
            _userManagingService = new UserManagingService(_userContract);
        }

        [Test]
        public void CheckIfRegistered_GivenIpOfRegisteredUser_ShouldReturnTrue()
        {
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });
            var result = _userManagingService.CheckIfRegistered("127.0.0.1");

            _userContract.Received().GetUser("127.0.0.1");

            result.ShouldBe(true);
        }

        [Test]
        public void CheckIfRegistered_GivenIpOfNotRegisteredUser_ShouldReturnFalse()
        {
            _userContract.GetUser("127.0.0.1").Returns((User)null);
            var result = _userManagingService.CheckIfRegistered("127.0.0.1");

            _userContract.Received().GetUser("127.0.0.1");

            result.ShouldBe(false);
        }

        [Test]
        public void CheckIfValidToSearch_GivenIpOfUserWhoHasSearches_ShouldReturnTrue()
        {
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _userManagingService.CheckIfValidToSearch("127.0.0.1");

            _userContract.Received().GetUser("127.0.0.1");
            _userContract.Received().RemoveOneSearch("127.0.0.1");

            result.ShouldBe(true);
        }

        [Test]
        public void CheckIfValidToSearch_GivenIpOfUserWhoHasNoSearches_ShouldReturnFalse()
        {
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 0});

            var result = _userManagingService.CheckIfValidToSearch("127.0.0.1");

            _userContract.Received().GetUser("127.0.0.1");
            _userContract.DidNotReceive().RemoveOneSearch("127.0.0.1");

            result.ShouldBe(false);
        }

        [Test]
        public void GiveUserAdditionalSearch_GivenIpOfRegisteredUser_ShouldNotThrowException()
        {
            User user = new User() { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 0 };

            _userContract.GetUser("127.0.0.1").Returns(user);

            Action action = () => _userManagingService.GiveUserAdditionalSearch("127.0.0.1");

            action.ShouldNotThrow();

            _userContract.Received().GetUser("127.0.0.1");
            _userContract.Received().GiveAdditionalSearch(user);
        }

        [Test]
        public void GiveUserAdditionalSearch_GivenIpOfNotRegisteredUser_ShouldThrowException()
        {
            User user = null;
            _userContract.GetUser("1").Returns(user);
            Action action = () => _userManagingService.GiveUserAdditionalSearch("1");

            action.ShouldThrow<Exception>();

            _userContract.DidNotReceive().GiveAdditionalSearch(user);
        }

    }
}
