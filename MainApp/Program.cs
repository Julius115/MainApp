using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Configuration;

namespace AnagramSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int minLength = int.Parse(ConfigurationManager.AppSettings["minLength"]);
            int maxWords = int.Parse(ConfigurationManager.AppSettings["maxWords"]);

            IWordRepository reader = new FileWordRepository("zodynas.txt");

            List<string> wordsList = reader.GetWordsDictionary();

            IAnagramSolver solver = new AnagramSolverSingleWord(reader);
            var anagrams = solver.GetAnagrams("sula liaideta");

            foreach (string s in anagrams)
            {
                System.Console.WriteLine(s);
            }
        }
    }
}
