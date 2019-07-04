using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IAnagramSolver
    {
        IList<string> GetAnagrams(string myWords);
    }
}
