using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class DataController : Controller
    {
        private readonly IWordRepository _wordRepository;

        public DataController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DownloadDictionary()
        {
            var fileName = "zodynasDownload.txt";
            var contentType = "text/plain";

            var myStringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, int> pair in _wordRepository.GetWordsDictionary())
            {
                myStringBuilder.AppendFormat("{0} {1}\n", pair.Key, pair.Value);
            }

            var enc = new UTF8Encoding();
            var bytes = enc.GetBytes(myStringBuilder.ToString());

            return File(bytes, contentType, fileName);
        }
    }
}