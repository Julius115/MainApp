using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Contracts;
using System.IO;
using Microsoft.AspNetCore.Http;
using AnagramSolver.Services;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IWordRepository _wordRepository;
        private readonly ILogger _logger;
        private readonly IDatabaseManager _databaseManager;
        private readonly IWordSearch _wordSearchRepository;
        private readonly CachedWordsService _cachedWordsService;

        public HomeController(IAnagramSolver anagramSolver, IWordRepository wordRepository, IDatabaseManager databaseManager, ILogger logger, IWordSearch wordSearch, CachedWordsService cachedWordsService)
        {
            _anagramSolver = anagramSolver;
            _wordRepository = wordRepository;
            _databaseManager = databaseManager;
            _logger = logger;
            _wordSearchRepository = wordSearch;
            _cachedWordsService = cachedWordsService;
        }

        [HttpGet]
        public List<string> GetAnagrams(string id)
        {
            return _anagramSolver.GetAnagrams(id).ToList();
        }

        public IActionResult Index(string id = null)
        {
            List<string> anagrams = new List<string>();

            if (String.IsNullOrEmpty(id))
            {
                foreach (var cookie in Request.Cookies)
                {
                    if (cookie.Key == "inputWord")
                    {
                        return View(_anagramSolver.GetAnagrams(cookie.Value).ToList());
                    }
                }
                return View();
            }
            else
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Append("inputWord", id, options);
            }

            return View(_cachedWordsService.CacheWords(id, HttpContext.Connection.LocalIpAddress.ToString()));
        }

        public IActionResult SearchInfo(string word, DateTime date)
        {
            SearchInfoModel searchInfoResult = _wordSearchRepository.GetSearchInfo(word, date);

            SearchInfoViewModel searchInfo = new SearchInfoViewModel();

            searchInfo.UserIp = searchInfoResult.UserIp;
            searchInfo.RequestWord = searchInfoResult.RequestWord;
            searchInfo.RequestDate = searchInfoResult.RequestDate;

            foreach (string s in searchInfoResult.Anagrams)
            {
                searchInfo.Anagrams.Add(s);
            }

            return View(searchInfo);
        }

        public IActionResult SearchHistory()
        {
            List<SearchHistoryViewModel> searchHistory = new List<SearchHistoryViewModel>();

            List<SearchHistoryInfoModel> searchHistoryResult = _wordSearchRepository.GetSearchHistory();

            foreach (SearchHistoryInfoModel s in searchHistoryResult)
            {
                searchHistory.Add(new SearchHistoryViewModel() { Ip = s.Ip, RequestDate = s.RequestDate, RequestWord = s.RequestWord });
            }

            return View(searchHistory);
        }

        public IActionResult ClearTable(string tableName)
        {
            if (String.IsNullOrEmpty(tableName))
            {
                return View(_databaseManager.GetTablesNames(tableName));
            }

            _databaseManager.DeleteTableData(tableName);

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult WriteToFile(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return View();
            }

            List<string> inputList = input.Split().ToList();

            if (!_wordRepository.GetWordsDictionary().Contains(inputList.First()))
            {
                using (StreamWriter sw = new StreamWriter("zodynas.txt", true))
                {
                    sw.WriteLine(input);
                }
            }

            _wordRepository.AddWord(inputList.First());

            return View();
        }

        public IActionResult WordSearch(string id)
        {
            
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }

            WordSearchViewModel wordSearch = new WordSearchViewModel();

            wordSearch.SearchedWord = id;
            wordSearch.WordsToDisplay = _wordSearchRepository.GetWordsContainingPart(id);

            return View(wordSearch);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
