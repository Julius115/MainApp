﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.IO;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }
        public IActionResult Index()
        {
            return View("Empty");
        }

        [Route("Home/Index/{word?}")]
        public IActionResult Index(string word)
        {
            //IWordRepository reader = new FileWordRepository("zodynas.txt");
            //var wordsDictionary = reader.GetWordsDictionary();
            //IAnagramSolver solver = new AnagramSolverSingleWord(wordsDictionary);

            var anagramViewModel = new AnagramViewModel();

            anagramViewModel.InputWords = _anagramSolver.GetAnagrams(word).ToList();

            return View(anagramViewModel.InputWords);
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

            IWordRepository reader = new FileWordRepository("zodynas.txt");
            var wordsDictionary = reader.GetWordsDictionary();

            if (!wordsDictionary.Keys.Contains(inputList.First()))
            {
                using (StreamWriter sw = new StreamWriter("zodynas.txt", true))
                {
                    sw.WriteLine(input);
                }
            }

            return View();
        }

        public IActionResult Form(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return View();
            }

            //IWordRepository reader = new FileWordRepository("zodynas.txt");
            //var wordsDictionary = reader.GetWordsDictionary();
            IAnagramSolver solver = new AnagramSolverSingleWord(WordsDictionaryModel.WordsDictionary);

            //var anagramViewModel = new AnagramViewModel();
            AnagramViewModel.InputWords = solver.GetAnagrams(input).ToList();

            return View(AnagramViewModel.InputWords);
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
