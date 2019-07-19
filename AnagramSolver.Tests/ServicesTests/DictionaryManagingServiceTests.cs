using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace AnagramSolver.Tests.ServicesTests
{
    class DictionaryManagingServiceTests
    {
        private IWordRepository _wordRepository;
        private UserManagingService _userManagingService;
        private DictionaryManagingService _dictionaryManagingService;
        private AnagramSolver.EF.CodeFirst.Contracts.IUserContract _userContract;

        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepository>();
            _userContract = Substitute.For<AnagramSolver.EF.CodeFirst.Contracts.IUserContract>();
            _userManagingService = Substitute.For<UserManagingService>(_userContract);

            _dictionaryManagingService = new DictionaryManagingService(_wordRepository, _userManagingService);
        }

        [Test]
        public void EditWord_GivenWordEditModelWithUniqueNewWord_ShouldChangeOriginalWordToNewWord()
        {
            WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };

            _wordRepository.EditWord(wordEditInfoModel.OriginalWord, wordEditInfoModel.NewWord).Returns(true);
            _userContract.GetUser("10").Returns(new User { Id = 10, UserIp = "10", SearchesLeft = 10 });

            var result = _dictionaryManagingService.EditWord(wordEditInfoModel, "10");

            result.NewWord.ShouldBeNull();
            result.OriginalWord.ShouldBe("alus");
            result.EditStatus.ShouldBe(WordEditStatus.EditSuccessful);
        }

        [Test]
        public void EditWord_GivenWordEditModelWithAlreadyExistingNewWord_ShouldNotChangeWord()
        {
            WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };

            _wordRepository.EditWord(wordEditInfoModel.OriginalWord, wordEditInfoModel.NewWord).Returns(false);

            var result = _dictionaryManagingService.EditWord(wordEditInfoModel, "10");

            result.NewWord.ShouldBeNull();
            result.OriginalWord.ShouldBe("sula");
            result.EditStatus.ShouldBe(WordEditStatus.EditUnsuccesful);
        }

        [Test]
        public void DeleteWord_GivenIpOfUserWhoHasSearchCredits_ShouldSetEditStatusToDeleteSuccesful()
        {
            
        }

        [Test]
        public void DeleteWord_GivenIpOfUserWhoHasNoSearchCredits_ShouldReturnModelWithEditStatusDeleteDenied()
        {
            //WordEditInfoModel wordEditInfoModel = new WordEditInfoModel { OriginalWord = "sula", NewWord = "alus" };
            //
            //_userContract.GetUser("10").Returns(new User { Id = 10, UserIp = "10", SearchesLeft = 10 });
            //
            //_userContract.RemoveOneSearch("10");
            //
            //_userManagingService.CheckIfValidToSearch("10").Returns(false);
            //
            //var result = _dictionaryManagingService.DeleteWord(wordEditInfoModel, "10");
            //
            //result.EditStatus.ShouldBe(WordEditStatus.DeleteDenied);
        }
    }
}
