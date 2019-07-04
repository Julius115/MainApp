using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class WordRepositoryController : Controller
    {
        //private AnagramViewModel anagramViewModel = new AnagramViewModel();
        private Dictionary<string, int> dictionary = new Dictionary<string, int>();
        public IActionResult ReadDictionary(int page = 0)
        {
            ViewBag.Page = page;

            IWordRepository reader = new FileWordRepository("zodynas.txt");
            dictionary = reader.GetWordsDictionary();
            //TODO: change AnagramViewModel to new ViewModel
            AnagramViewModel.InputWords = dictionary.Keys.ToList();


            AnagramViewModel.InputWords = dictionary.Keys.ToList().Skip(page * 100).Take(100).ToList();


            return View(AnagramViewModel.InputWords);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}