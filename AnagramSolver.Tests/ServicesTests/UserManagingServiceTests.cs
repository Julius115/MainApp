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
        private IUserContract _userContract;
        private UserManagingService _userManagingService;

        [SetUp]
        public void Setup()
        {
            _userContract = Substitute.For<IUserContract>();
            _userManagingService = new UserManagingService(_userContract);
        }

        [Test]
        public void CheckIfRegistered_GivenIpOfRegisteredUser_ShouldReturnTrue()
        {
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _userManagingService.CheckIfRegistered("127.0.0.1");

            result.ShouldNotBeNull();
            result.ShouldBe(true);
        }

        [Test]
        public void CheckIfRegistered_GivenIpOfNotRegisteredUser_ShouldReturnFalse()
        {
            _userContract.GetUser("111.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _userManagingService.CheckIfRegistered("127.0.0.1");

            result.ShouldNotBeNull();
            result.ShouldBe(false);
        }

        [Test]
        public void CheckIfValidToSearch_GivenIpOfUserWhoHasSearches_ShouldReturnTrue()
        {
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _userManagingService.CheckIfValidToSearch("127.0.0.1");
            result.ShouldBe(true);
        }

        [Test]
        public void CheckIfValidToSearch_GivenIpOfUserWhoHasNoSearches_ShouldReturnFalse()
        {
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 0});

            var result = _userManagingService.CheckIfValidToSearch("127.0.0.1");
            result.ShouldBe(false);
        }

        [Test]
        public void GiveUserAdditionalSearch_GivenIpOfRegisteredUser_ShouldNotThrowException()
        {
            _userContract.GetUser("0").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 0 });
            Action action = () => _userManagingService.GiveUserAdditionalSearch("0");

            action.ShouldNotThrow();
        }

        [Test]
        public void GiveUserAdditionalSearch_GivenIpOfNotRegisteredUser_ShouldThrowException()
        {
            _userContract.GetUser("0").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 0 });
            Action action = () => _userManagingService.GiveUserAdditionalSearch("1");

            action.ShouldThrow<Exception>();
        }

    }
}
