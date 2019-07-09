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
