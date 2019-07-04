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
        private readonly IWordRepository _wordRepository;

        public HomeController(IAnagramSolver anagramSolver, IWordRepository wordRepository)
        {
            _anagramSolver = anagramSolver;
            _wordRepository = wordRepository;
        }
        public IActionResult Index()
        {
            return View("Empty");
        }

        [Route("Home/Index/{word?}")]
        public IActionResult Index(string word)
        {
            return View(_anagramSolver.GetAnagrams(word).ToList());
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

            return View();
        }

        public IActionResult Form(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return View();
            }

            return View(_anagramSolver.GetAnagrams(input).ToList());
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
