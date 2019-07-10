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

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IWordRepository _wordRepository;

        public HomeController(IAnagramSolver anagramSolver, IWordRepository wordRepository)
        {
            _anagramSolver = anagramSolver;
            _wordRepository = wordRepository;
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
            //-------------------------------------------

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "SELECT Count(Id) FROM CachedWords WHERE RequestWord = @WORD";
                SqlCommand cmda = new SqlCommand(SQLstr, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = ("@WORD");
                param.Value = id;
                cmda.Parameters.Add(param);

                if ((int)cmda.ExecuteScalar() > 0)
                {
                    SQLstr = "SELECT b.Word FROM Words AS b INNER JOIN CachedWords as a ON (b.Id = a.ResponseWord and a.RequestWord = @WORD)";
                    cmda = new SqlCommand(SQLstr, conn);

                    SqlParameter paramas = new SqlParameter();
                    paramas.ParameterName = ("@WORD");
                    paramas.Value = id;
                    cmda.Parameters.Add(paramas);

                    SqlDataReader reader;
                    reader = cmda.ExecuteReader();

                    anagrams = new List<string>();

                    while (reader.Read())
                    {
                        anagrams.Add(reader.GetString(0));
                    }

                    ViewBag.Cached = true;

                    Log(id);
                    return View(anagrams);
                }

                anagrams = _anagramSolver.GetAnagrams(id).ToList();

                foreach (string anagram in anagrams)
                {
                    SQLstr = "INSERT INTO CachedWords (RequestWord, ResponseWord) VALUES" +
                                "( @WORD , (SELECT Id FROM Words WHERE Word = @WORDREQUEST)); ";

                    SqlCommand cmda1 = new SqlCommand(SQLstr, conn);

                    List<SqlParameter> prm = new List<SqlParameter>()
                         {
                             new SqlParameter("@WORD", SqlDbType.NVarChar) {Value = id},
                             new SqlParameter("@WORDREQUEST", SqlDbType.NVarChar) {Value = anagram},
                         };
                    cmda1.Parameters.AddRange(prm.ToArray());

                    cmda1.ExecuteNonQuery();
                }
            }

            Log(id);
            return View(_anagramSolver.GetAnagrams(id).ToList());
        }

        public IActionResult SearchInfo(string word)
        {
            SearchInfoViewModel searchInfo = new SearchInfoViewModel();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "SELECT u.UserIp, u.RequestDate , u.RequestWord,  w.Word "+
                "FROM UserLog AS u " +
                "INNER JOIN CachedWords AS c " +
                "ON u.RequestWord = c.RequestWord AND u.RequestWord = @WORD " +
                "INNER JOIN Words AS w " +
                "ON w.Id = c.ResponseWord";
                SqlCommand cmda = new SqlCommand(SQLstr, conn);

                SqlParameter paramas = new SqlParameter();
                paramas.ParameterName = ("@WORD");
                paramas.Value = word;
                cmda.Parameters.Add(paramas);

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
                //anagrams = new List<string>();
                while (reader.Read())
                {
                    searchInfo.Anagrams.Add(reader.GetString(3));
                }
                //while (reader.Read())
                //{
                //    anagrams.Add(reader.GetString(0));
                //}


            }
            return View(searchInfo);
        }

        public IActionResult SearchHistory()
        {
            //WordSearchViewModel wordSearch = new WordSearchViewModel();

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
                    //wordsSql
                    wordsSql.Add(new SearchHistoryViewModel() { Ip = reader.GetString(0), RequestWord = reader.GetString(1), RequestDate = reader.GetDateTime(2) });

                }

                //wordSearch.WordsToDisplay = wordsSql;
            }

            return View(wordsSql);
        }

        public void Log(string inputWord)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                string SQLstr = "INSERT INTO UserLog (UserIp, RequestWord, RequestDate) VALUES" +
                                "( @USERIP , @REQUESTWORD, @REQUESTDATE); ";

                SqlCommand cmda = new SqlCommand(SQLstr, conn);
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = ("@USERIP");
                //param.Value = 
                //cmda.Parameters.Add(param);
                //
                //SqlCommand cmda1 = new SqlCommand(SQLstr, conn);

                List<SqlParameter> prm = new List<SqlParameter>()
                         {
                             new SqlParameter("@USERIP", SqlDbType.NVarChar) {Value = HttpContext.Connection.LocalIpAddress.ToString()},
                             new SqlParameter("@REQUESTWORD", SqlDbType.NVarChar) {Value = inputWord},
                             new SqlParameter("@REQUESTDATE", SqlDbType.DateTime) {Value = DateTime.Now }
                         };
                cmda.Parameters.AddRange(prm.ToArray());
                cmda.ExecuteNonQuery();
            }
        }

        public IActionResult ClearTable(string tableName)
        {
            if (String.IsNullOrEmpty(tableName))
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();

                    string SQLstr = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'AnagramsDB'";
                    SqlCommand cmda = new SqlCommand(SQLstr, conn);

                    SqlDataReader reader = cmda.ExecuteReader();
                    List<string> anagrams = new List<string>();

                    while (reader.Read())
                    {
                        anagrams.Add(reader.GetString(0));
                    }

                    return View(anagrams);
                }
            }

            using (var conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (var command = new SqlCommand("ClearTableData", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@TABLENAME", tableName);
                command.ExecuteNonQuery();
            }

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
