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
        private readonly IWordRepository _wordRepository;

        public WordRepositoryController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult ReadDictionary(int page = 0)
        {
            WordRepositoryViewModel wordRepository = new WordRepositoryViewModel(_wordRepository.GetWords(page, 100), page);

            return View(wordRepository);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}