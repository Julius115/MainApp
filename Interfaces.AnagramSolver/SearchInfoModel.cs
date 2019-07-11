using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public class SearchInfoModel
    {
        public string UserIp { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestWord { get; set; }
        public List<string> Anagrams { get; set; } = new List<string>();
    }
}
