using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.BusinessLogic;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                // return View("Empty");
                return new EmptyResult();
            }

            FileWordRepository fileWordRepository = new FileWordRepository("zodynas.txt");
            Dictionary<string, int> dictionary = fileWordRepository.GetWordsDictionary();

            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(dictionary);

            AnagramViewModel anagramViewModel = new AnagramViewModel();
            anagramViewModel.InputWords = anagramSolver.GetAnagrams(word).ToList();

            return View(anagramViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
