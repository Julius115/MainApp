using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Contracts;
using System.IO;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using AnagramSolver.BusinessLogic;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IWordRepository _wordRepository;

        private readonly ICachedWords _cachedWords;
        private readonly ILogger _logger;
        private readonly IDatabaseManager _databaseManager;

        public HomeController(IAnagramSolver anagramSolver, IWordRepository wordRepository)
        {
            _anagramSolver = anagramSolver;
            _wordRepository = wordRepository;

            _cachedWords = new CachedWordsRepository(_anagramSolver);
            _logger = new LoggerRepository();
            _databaseManager = new DatabaseManagerRepository();
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

            _logger.Log(id, HttpContext.Connection.LocalIpAddress.ToString());

            return View(_cachedWords.CacheWords(id));
        }

        public IActionResult SearchInfo(string word, DateTime date)
        {
            SearchInfoViewModel searchInfo = new SearchInfoViewModel();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "SELECT u.UserIp, u.RequestDate , u.RequestWord,  w.Word " +
                "FROM UserLog AS u " +
                "INNER JOIN CachedWords AS c " +
                "ON u.RequestWord = c.RequestWord AND u.RequestWord = @WORD AND u.RequestDate = @DATE " +
                "INNER JOIN Words AS w " +
                "ON w.Id = c.ResponseWord";
                SqlCommand cmda = new SqlCommand(SQLstr, conn);

                SqlParameter paramas = new SqlParameter();

                List<SqlParameter> prm = new List<SqlParameter>()
                         {
                             new SqlParameter("@WORD", SqlDbType.NVarChar) {Value = word},
                             new SqlParameter("@DATE", SqlDbType.DateTime) {Value = date},
                         };
                cmda.Parameters.AddRange(prm.ToArray());

                SqlDataReader reader;
                reader = cmda.ExecuteReader();

                if (reader != null)
                {
                    reader.Read();
                    searchInfo.UserIp = reader.GetString(0);
                    searchInfo.RequestDate = reader.GetDateTime(1);
                    searchInfo.RequestWord = reader.GetString(2);
                    searchInfo.Anagrams.Add(reader.GetString(3));
                }

                while (reader.Read())
                {
                    searchInfo.Anagrams.Add(reader.GetString(3));
                }
            }

            return View(searchInfo);
        }

        public IActionResult SearchHistory()
        {
            List<SearchHistoryViewModel> wordsSql = new List<SearchHistoryViewModel>();

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "SELECT UserIp, RequestWord, RequestDate FROM UserLog";
                SqlCommand cmd = new SqlCommand(SQLstr, conn);

                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    wordsSql.Add(new SearchHistoryViewModel() { Ip = reader.GetString(0), RequestWord = reader.GetString(1), RequestDate = reader.GetDateTime(2) });

                }
            }

            return View(wordsSql);
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

            if (!_wordRepository.GetWordsDictionary().Keys.Contains(inputList.First()))
            {
                using (StreamWriter sw = new StreamWriter("zodynas.txt", true))
                {
                    sw.WriteLine(input);
                }
            }

            _wordRepository.AddWord(input);

            return View();
        }

        public IActionResult WordSearch(string id)
        {

            if (String.IsNullOrEmpty(id))
            {
                return View();
            }

            WordSearchViewModel wordSearch = new WordSearchViewModel();

            List<string> wordsSql = new List<string>();

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "SELECT Word FROM Words WHERE Word LIKE '%' + @WORD + '%'";
                SqlCommand cmd = new SqlCommand(SQLstr, conn);

                SqlParameter param = new SqlParameter();
                param.ParameterName = ("@WORD");
                param.Value = id;
                cmd.Parameters.Add(param);

                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    wordsSql.Add(reader.GetString(0));
                }

                wordSearch.WordsToDisplay = wordsSql;
            }

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
