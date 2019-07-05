using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Models
{
    public class WordRepositoryViewModel
    {
        public List<string> Words { get; set; }
        public int PageNumber { get; set; }

        public WordRepositoryViewModel(List<string> words, int pageNumber)
        {
            Words = words;
            PageNumber = pageNumber;
        }
    }
}
