
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AnagramSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int minLength = int.Parse(ConfigurationManager.AppSettings["minLength"]);
            int maxWords = int.Parse(ConfigurationManager.AppSettings["maxWords"]);

            IWordRepository reader = new FileWordRepository("zodynas.txt");

            Dictionary<string, int> wordsList = reader.GetWordsDictionary();

            IAnagramSolver solver = new AnagramSolverSingleWord(wordsList);
            var anagrams = solver.GetAnagrams("sula liaideta");

            foreach (string s in anagrams)
            {
                System.Console.WriteLine(s);
            }
            
            //Dictionary<string, int> wordsList = new Dictionary<string, int>() { { "labas", 1 }, { "l", 1 }, { "aba", 1 }, { "s", 1 }, { "ba", 1 } };
            //IAnagramSolver solver = new AnagramSolverRecursive(wordsList);
            //List<string> anagrams = solver.GetAnagrams("lbas ba").ToList();
        }

    }
}
