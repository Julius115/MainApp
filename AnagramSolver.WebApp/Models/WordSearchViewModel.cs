using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Models
{
    public class WordSearchViewModel
    {
        public int PageNumber { get; set; } = 0;
        public List<string> WordsToDisplay { get; set; } = new List<string>();
        public string SearchedWord { get; set; }
    }
}
