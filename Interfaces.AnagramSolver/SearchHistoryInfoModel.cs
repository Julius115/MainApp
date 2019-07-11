using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public class SearchHistoryInfoModel
    {
        public string Ip { get; set; }
        public string RequestWord { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
