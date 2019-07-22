using AnagramSolver.Contracts;
using System.Linq;

namespace AnagramSolver.Services
{
    public class AnagramsSearchService
    {
        private readonly ICachedWords _cachedWords;
        private readonly IAnagramSolver _anagramSolver;
        private readonly ILogger _logger;
        private readonly IRequestWordContract _requestWordContract;
        private readonly AnagramSolver.EF.CodeFirst.Contracts.IUserRepository _userContract;
        private readonly IUserManagingService _userManagingService;


        // TODO: add implementation to interfaces to get id etc..
        public AnagramsSearchService(ICachedWords cachedWords, IAnagramSolver anagramSolver, ILogger logger, IRequestWordContract requestWordContract, AnagramSolver.EF.CodeFirst.Contracts.IUserRepository userContract,IUserManagingService userManagingService)
        {
            _cachedWords = cachedWords;
            _anagramSolver = anagramSolver;
            _logger = logger;
            _requestWordContract = requestWordContract;
            _userContract = userContract;
            _userManagingService = userManagingService;
        }

        public AnagramsSearchInfoModel GetAnagrams(string requestWord, string userIp)
        {
            var anagramsSearchInfoModel = new AnagramsSearchInfoModel();

            if (!_userManagingService.CheckIfRegistered(userIp))
            {
                _userContract.SetUser(userIp);
            }

            anagramsSearchInfoModel.isValidToSearch = _userManagingService.CheckIfValidToSearch(userIp);

            if (!anagramsSearchInfoModel.isValidToSearch)
            {
                return anagramsSearchInfoModel;
            }

            if (_cachedWords.CheckIfCached(requestWord))
            {
                _logger.Log(requestWord, userIp);
                anagramsSearchInfoModel.Anagrams = _cachedWords.GetCachedAnagrams(requestWord);

                return anagramsSearchInfoModel;
            }

            anagramsSearchInfoModel.Anagrams = _anagramSolver.GetAnagrams(requestWord).ToList();
            _requestWordContract.SetRequestWord(requestWord);
            _cachedWords.SetCachedAnagrams(anagramsSearchInfoModel.Anagrams, requestWord);
            _logger.Log(requestWord, userIp);

            return anagramsSearchInfoModel;
        }
    }
}
