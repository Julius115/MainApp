using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Models
{
    public class SearchInfoViewModel
    {
        public string UserIp { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestWord { get; set; }
        public List<string> Anagrams { get; set; } = new List<string>();
    }
}
