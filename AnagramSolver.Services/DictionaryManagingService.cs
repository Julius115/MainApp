using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.Services
{
    public class DictionaryManagingService
    {
        private readonly IUserContract _userContract;
        private readonly IWordRepository _wordRepository;
        public DictionaryManagingService(IUserContract userContract, IWordRepository wordRepository)
        {
            _userContract = userContract;
            _wordRepository = wordRepository;
        }

        public void AddWord(string inputWord, string userIp)
        {
            //List<string> inputList = inputWord.Split().ToList();

            if (!_wordRepository.GetWordsDictionary().Contains(inputWord))
            {
                _wordRepository.AddWord(inputWord);
                _userContract.GiveAdditionalSearch(userIp);
                //using (StreamWriter sw = new StreamWriter("zodynas.txt", true))
                //{
                //    sw.WriteLine(input);
                //}
            }
        }
    }
}
