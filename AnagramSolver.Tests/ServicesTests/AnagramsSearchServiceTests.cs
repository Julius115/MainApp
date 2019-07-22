using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.Tests.ServicesTests
{
    class AnagramsSearchServiceTests
    {
        private ICachedWords _cachedWords;
        private IAnagramSolver _anagramSolver;
        private ILogger _logger;
        private IRequestWordContract _requestWordContract;
        private AnagramSolver.EF.CodeFirst.Contracts.IUserRepository _userContract;
        private IUserManagingService _userManagingService;

        private AnagramsSearchService _anagramsSearchService;

        private string ip = "127.0.0.1";

        [SetUp]
        public void Setup()
        {
            _cachedWords = Substitute.For<ICachedWords>();
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _logger = Substitute.For<ILogger>();
            _requestWordContract = Substitute.For<IRequestWordContract>();
            _userContract = Substitute.For<AnagramSolver.EF.CodeFirst.Contracts.IUserRepository>();
            _userManagingService = Substitute.For<IUserManagingService>();
            
            _anagramsSearchService = new AnagramsSearchService(_cachedWords, _anagramSolver,_logger, _requestWordContract, _userContract, _userManagingService);
        }
        
        [Test]
        public void GetAnagrams_IfUserWithIpIsRegistered_ShouldNotSetUserAgain()
        {
            User user = new User() { Id = 1, UserIp = ip, SearchesLeft = 0 };

            _userManagingService.CheckIfRegistered(ip).Returns(true);

            var result = _anagramsSearchService.GetAnagrams("test", ip);

            _userContract.DidNotReceive().SetUser(ip);
        }

        [Test]
        public void GetAnagrams_IfUserWithIpIsNotRegistered_ShouldSetUserToDb()
        {
            User user = new User() { Id = 1, UserIp = ip, SearchesLeft = 0 };

            _userManagingService.CheckIfRegistered(ip).Returns(false);

            var result = _anagramsSearchService.GetAnagrams("test", ip);

            _userContract.Received().SetUser(ip);
        }

        [Test]
        public void GetAnagrams_IfUserIsNotValidToSearch_ShouldSetBoolOfModelFalseAndReturn()
        {
            _userManagingService.CheckIfValidToSearch(ip).Returns(false);

            var result = _anagramsSearchService.GetAnagrams("test", ip);

            result.isValidToSearch.ShouldBe(false);
        }

        [Test]
        public void GetAnagrams_IfUserIsValidToSearch_ShouldSetBoolOfModelTrueAndReturn()
        {
            _userManagingService.CheckIfValidToSearch(ip).Returns(true);

            var result = _anagramsSearchService.GetAnagrams("test", ip);

            result.isValidToSearch.ShouldBe(true);
        }

        [Test]
        public void GetAnagrams_IfWordIsCached_ShouldSetModelAnagramsFromCache()
        {
            List<string> anagrams = new List<string> { "sula", "alus" };
            _cachedWords.CheckIfCached("test").Returns(true);
            _cachedWords.GetCachedAnagrams("test").Returns(anagrams);
            _userManagingService.CheckIfValidToSearch(ip).Returns(true);

            var result = _anagramsSearchService.GetAnagrams("test", ip);

            _logger.Received().Log("test", ip);
            result.Anagrams.ShouldBe(anagrams);

            _anagramSolver.DidNotReceive().GetAnagrams("test");
        }

        [Test]
        public void GetAnagrams_IfWordIsNotCached_ShouldSetAnagramsToCache()
        {
            List<string> anagrams = new List<string> { "sula", "alus" };
            _cachedWords.CheckIfCached("test").Returns(false);
            //_cachedWords.GetCachedAnagrams("test").Returns(anagrams);
            _userManagingService.CheckIfValidToSearch(ip).Returns(true);
            _anagramSolver.GetAnagrams("test").Returns(anagrams);

            var result = _anagramsSearchService.GetAnagrams("test", ip);

            _cachedWords.DidNotReceive().GetCachedAnagrams("test"); 
            _logger.Received().Log("test", ip);
            result.Anagrams.ShouldBe(anagrams);

            _anagramSolver.Received().GetAnagrams("test");
            _cachedWords.Received().SetCachedAnagrams(result.Anagrams, "test");
        }

    }
}
