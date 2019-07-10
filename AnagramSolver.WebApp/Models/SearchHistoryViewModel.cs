using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Models
{
    public class SearchHistoryViewModel
    {
        public string Ip { get; set; }
        public string RequestWord { get; set; }
        public DateTime RequestDate { get; set; }

    }
}
