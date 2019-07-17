using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public class AnagramsSearchInfoModel
    {
        public List<string> Anagrams { get; set; } = new List<string>();
        public bool isValidToSearch;
    }
}
