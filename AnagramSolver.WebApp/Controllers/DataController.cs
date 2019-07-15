using System.Text;
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

            foreach (string word in _wordRepository.GetWordsDictionary())
            {
                myStringBuilder.AppendFormat("{0}\r\n", word);
            }

            var enc = new UTF8Encoding();
            var bytes = enc.GetBytes(myStringBuilder.ToString());

            return File(bytes, contentType, fileName);
        }
    }
}