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
            ViewBag.Cached = false;

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

            var a = _anagramSolver.GetAnagrams(id).ToList();
            //-------------------------------------------

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();

                //string SQLstr = "SELECT Word FROM Words WHERE Word LIKE '%' + @WORD + '%'";
                string SQLs = "SELECT Id FROM Words WHERE Word = @ID";
                SqlCommand cmda = new SqlCommand(SQLs, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = ("@ID");
                param.Value = id;
                cmda.Parameters.Add(param);

                int aa = Convert.ToInt32(cmda.ExecuteScalar());
                //int aa = (int)cmda.ExecuteScalar();
                //object searchedWordId = aa.GetSqlValue(0);

                SQLs = "SELECT Count(Id) FROM CachedWords WHERE RequestWord = @ID";
                cmda = new SqlCommand(SQLs, conn);
                param = new SqlParameter();
                param.ParameterName = ("@ID");
                param.Value = aa;
                cmda.Parameters.Add(param);
                cmda.ExecuteNonQuery();

                var test = (int)cmda.ExecuteScalar();
                cmda.Parameters.RemoveAt("@ID");

                // test == 0 If no CachedWords of searched word
                if (test == 0)
                {
                    // no such cached word
                    List<string> anagrams = _anagramSolver.GetAnagrams(id).ToList();

                    
                    

                    foreach (string s in anagrams)
                    {
                        SQLs = "INSERT INTO CachedWords (RequestWord, ResponseWord) VALUES" +
                                "( @IDREQUEST , (SELECT Id FROM Words WHERE Word = @IDRESULT)); ";

                        //cmda = new SqlCommand(SQLs, conn);
                        //param = new SqlParameter();
                        //param.ParameterName = ("@IDREQUEST");
                        //param.Value = aa;
                        //cmda.Parameters.Add(param);
                        //
                        //param.ParameterName = ("@IDRESULT");
                        //param.Value = s;
                        //cmda.Parameters.Add(param);

                        SqlCommand cmda1 = new SqlCommand(SQLs, conn);
                        SqlParameter param1 = new SqlParameter();


                        List<SqlParameter> prm = new List<SqlParameter>()
                         {
                             new SqlParameter("@IDREQUEST", SqlDbType.Int) {Value = aa},
                             new SqlParameter("@IDRESULT", SqlDbType.NVarChar) {Value = s},
                         };
                        cmda1.Parameters.AddRange(prm.ToArray());

                        cmda1.ExecuteNonQuery();
                    }

                }
                else
                {
                    //TODO: Get anagrams from CachedWords

                    string SQLstr = "SELECT b.Word FROM Words AS b INNER JOIN CachedWords as a ON (b.Id = a.ResponseWord and b.Id = @ID)";
                    SqlCommand cmd = new SqlCommand(SQLstr, conn);

                    SqlParameter paramas = new SqlParameter();
                    paramas.ParameterName = ("@ID");
                    paramas.Value = aa;
                    cmd.Parameters.Add(paramas);

                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();

                    List<string> anagrams = new List<string>();

                    while (reader.Read())
                    {
                        //wordsSql.Add(reader.GetString(0));
                        anagrams.Add(reader.GetString(0));
                    }

                    //wordSearch.WordsToDisplay = wordsSql;
                    //cmda.Parameters.Add(param);
                    //
                    //param.ParameterName = ("@IDRESULT");
                    //param.Value = s;
                    //cmda.Parameters.Add(param);
                    ViewBag.Cached = true;

                    return View(anagrams);

                }


                //string SQLstr = "SELECT Word FROM Words WHERE Word LIKE '%' + @WORD + '%'";
                //SqlCommand cmd = new SqlCommand(SQLstr, conn);

                //SqlParameter param = new SqlParameter();
                //param.ParameterName = ("@WORD");
                //param.Value = id;
                //cmd.Parameters.Add(param);

                //SqlDataReader reader;
                //reader = cmd.ExecuteReader();

                //while (reader.Read())
                //{
                //    wordsSql.Add(reader.GetString(0));
                //}

                //wordSearch.WordsToDisplay = wordsSql;
            }

            //-------------------------------------------
            return View(_anagramSolver.GetAnagrams(id).ToList());
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
