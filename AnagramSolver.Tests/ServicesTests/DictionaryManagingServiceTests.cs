using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace AnagramSolver.Tests.ServicesTests
{
    class DictionaryManagingServiceTests
    {
        private IWordRepository _wordRepository;
        private UserManagingService _userManagingService;
        private DictionaryManagingService _dictionaryManagingService;
        private AnagramSolver.EF.CodeFirst.Contracts.IUserRepository _userContract;

        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepository>();
            _userContract = Substitute.For<AnagramSolver.EF.CodeFirst.Contracts.IUserRepository>();
            _userManagingService = Substitute.For<UserManagingService>(_userContract);

            _dictionaryManagingService = new DictionaryManagingService(_wordRepository, _userManagingService);
        }

        

        [Test]
        public void AddWord_IfWordRepositoryDoesNotContainSpecifiedWord_ShouldNotCallAddWordAndGiveUserAdditionalSearch()
        {
            _wordRepository.GetWordsDictionary().Returns( new List<string> {"test", "vienas" });
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10});

            _dictionaryManagingService.AddWord("test", "127.0.0.1");

            _wordRepository.DidNotReceive().AddWord("test");
            _userManagingService.DidNotReceive().GiveUserAdditionalSearch("127.0.0.1");
        }

        [Test]
        public void AddWord_IfWordRepositoryContainsSpecifiedWord_ShouldCallAddWordAndGiveUserAdditionalSearch()
        {
            _wordRepository.GetWordsDictionary().Returns(new List<string> { "vienas" });
            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            _dictionaryManagingService.AddWord("test", "127.0.0.1");

            _wordRepository.Received().AddWord("test");
            _userManagingService.Received().GiveUserAdditionalSearch("127.0.0.1");
        }

        [Test]
        public void EditWord_GivenWordEditModelWithUniqueNewWord_ShouldChangeOriginalWordToNewWord()
        {
            WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };

            _wordRepository.EditWord(wordEditInfoModel.OriginalWord, wordEditInfoModel.NewWord).Returns(true);
            _userContract.GetUser("10").Returns(new User { Id = 10, UserIp = "10", SearchesLeft = 10 });

            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _dictionaryManagingService.EditWord(wordEditInfoModel, "10");

            _userManagingService.Received().GiveUserAdditionalSearch("127.0.0.1");

            result.NewWord.ShouldBeNull();
            result.OriginalWord.ShouldBe("alus");
            result.EditStatus.ShouldBe(WordEditStatus.EditSuccessful);
        }

        [Test]
        public void EditWord_GivenWordEditModelWithAlreadyExistingNewWord_ShouldNotChangeWord()
        {
            WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };
            _wordRepository.EditWord(wordEditInfoModel.OriginalWord, wordEditInfoModel.NewWord).Returns(false);

            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _dictionaryManagingService.EditWord(wordEditInfoModel, wordEditInfoModel.NewWord);

            result.NewWord.ShouldBeNull();
            result.OriginalWord.ShouldBe("sula");
            result.EditStatus.ShouldBe(WordEditStatus.EditUnsuccesful);

            _userManagingService.DidNotReceive().GiveUserAdditionalSearch("127.0.0.1");
        }

        [Test]
        public void DeleteWord_GivenIpOfUserWhoHasSearchCredits_ShouldSetEditStatusToDeleteSuccesful()
        {
            WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };

            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 10 });

            var result = _dictionaryManagingService.DeleteWord(wordEditInfoModel, "127.0.0.1");

            _wordRepository.Received().DeleteWord(wordEditInfoModel.OriginalWord);
            result.EditStatus.ShouldBe(WordEditStatus.DeleteSuccesful);
        }

        [Test]
        public void DeleteWord_GivenIpOfUserWhoHasNoSearchCredits_ShouldReturnModelWithEditStatusDeleteDenied()
        {
            WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };

            _userContract.GetUser("127.0.0.1").Returns(new User { Id = 1, UserIp = "127.0.0.1", SearchesLeft = 0 });

            var result = _dictionaryManagingService.DeleteWord(wordEditInfoModel, "127.0.0.1");

            _wordRepository.DidNotReceive().DeleteWord(wordEditInfoModel.OriginalWord);
            result.EditStatus.ShouldBe(WordEditStatus.DeleteDenied);
        }
    }
}
