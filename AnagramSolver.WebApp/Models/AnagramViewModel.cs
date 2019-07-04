using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public List<string> InputWords { get; set; }

        public AnagramViewModel()
        {
            InputWords = new List<string>();
        }
    }
}
