using AnagramSolver.Contracts;

namespace AnagramSolver.Services
{
    public class DictionaryManagingService
    {
        private readonly IWordRepository _wordRepository;
        private readonly UserManagingService _userManagingService;
        public DictionaryManagingService(IWordRepository wordRepository, UserManagingService userManagingService)
        {
            _wordRepository = wordRepository;
            _userManagingService = userManagingService;
        }

        public void AddWord(string inputWord, string userIp)
        {
            if (!_wordRepository.GetWordsDictionary().Contains(inputWord))
            {
                _wordRepository.AddWord(inputWord);
                _userManagingService.GiveUserAdditionalSearch(userIp);
            }
        }

        public WordEditInfoModel EditWord(WordEditInfoModel wordEditInfoModel, string userIp)
        {
            bool isEditSuccessful = _wordRepository.EditWord(wordEditInfoModel.OriginalWord, wordEditInfoModel.NewWord);

            if (isEditSuccessful)
            {
                wordEditInfoModel.EditStatus = WordEditStatus.EditSuccessful;
                wordEditInfoModel.OriginalWord = wordEditInfoModel.NewWord;
                wordEditInfoModel.NewWord = null;

                _userManagingService.GiveUserAdditionalSearch(userIp);
            }
            else
            {
                wordEditInfoModel.EditStatus = WordEditStatus.EditUnsuccesful;
                wordEditInfoModel.NewWord = null;
            }

            return wordEditInfoModel;
        }

        public WordEditInfoModel DeleteWord(WordEditInfoModel wordEditInfoModel, string userIp)
        {
            bool hasCreditsToDelete = _userManagingService.CheckIfValidToSearch(userIp);

            if (!hasCreditsToDelete)
            {
                wordEditInfoModel.EditStatus = WordEditStatus.DeleteDenied;
                return wordEditInfoModel;
            }

            _wordRepository.DeleteWord(wordEditInfoModel.OriginalWord);
            wordEditInfoModel.EditStatus = WordEditStatus.DeleteSuccesful;

            return wordEditInfoModel;
        }
    }
}
