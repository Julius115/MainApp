using AnagramSolver.Contracts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic.Tests
{
    [TestFixture]
    class AnagramSolverSingleWordTests
    {
        private IAnagramSolver _anagramSolver;
        private IWordRepository _wordRepository;

        private string fileName;
        private string fileText;
        private string fileTextDuplicates;

        //public AnagramSolverSingleWordTests(IAnagramSolver anagramSolver, IWordRepository wordRepository)
        //{
        //    _anagramSolver = anagramSolver;
        //    _wordRepository = wordRepository;
        //}

        private Dictionary<string, int> words;


        [SetUp]
        public void Setup()
        {
            
            fileName = "units.txt";
            fileText = "salu a sula 1";
            fileTextDuplicates = "lasa a lasa 1\n" +
                                 "lasa a lasa 3";
            //_wordRepository = new FileWordRepository("zodynas.txt");
            words = new Dictionary<string, int>() { { "sula", 1 }, { "sala", 1 } };

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(fileText);
            }

            _wordRepository = new FileWordRepository(@"C:\Users\Julius\source\repos\MainApp\Implementation.AnagramSolver.Tests\bin\Debug\netcoreapp2.1\" + fileName);
            _anagramSolver = new AnagramSolverSingleWord(_wordRepository);

        }

        [Test]
        public void GetAnagrams_ContainsAnagram_ReturnsOneAnagram()
        {

            fileName = "units.txt";
            fileText = "sala a sala 1";
            fileTextDuplicates = "lasa a lasa 1\n" +
                                 "lasa a lasa 3";
            //_wordRepository = new FileWordRepository("zodynas.txt");
            words = new Dictionary<string, int>() { { "sula", 1 }, { "sala", 1 } };

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(fileText);
            }

            _wordRepository = new FileWordRepository(@"C:\Users\Julius\source\repos\MainApp\Implementation.AnagramSolver.Tests\bin\Debug\netcoreapp2.1\" + fileName);
            _anagramSolver = new AnagramSolverSingleWord(_wordRepository);

            var result = _anagramSolver.GetAnagrams("alas");
            

            Assert.That(result.Count == 1);
            Assert.That(result.First() == "sala");
        }

        [Test]
        public void GetAnagrams_ContainsMultipleAnagrams_ReturnsMultipleAnagrams()
        {
            //var words = new Dictionary<string, int>() { { "sula", 1 }, { "salu", 1 } };3
            //IWordRepository wordRepository = new WordDictionaryStorage();
            //var anagramSolver = new AnagramSolverSingleWord(words);
            

            
            //FileWordRepository FileReader = new FileWordRepository(fileName);
            //var result = FileReader.GetWordsDictionary();
            var result = _anagramSolver.GetAnagrams("alus");


            //FileWordRepository FileReader = new FileWordRepository(fileName);

            //IWordRepository _wordRepository = new FileWordRepository(fileName);
            //_anagramSolver = new AnagramSolverSingleWord(_wordRepository);

            //FileWordRepository FileReader = new FileWordRepository(fileName);
            //var result = _anagramSolver.GetAnagrams("alus");
            
            //var result = _anagramSolver.GetAnagrams("alus");

            Assert.That(result.Count == 2);
            Assert.That(result.Contains("salu"));
            Assert.That(result.Contains("sula"));
            //Assert.That(result.Contains("salu"));
        }

        [Test]
        public void GetAnagrams_DoesNotContainAnagram_ReturnsEmptyList()
        {
            //var anagramSolver = new AnagramSolverSingleWord(words);
            var result = _anagramSolver.GetAnagrams("las");

            Assert.That(result.Count == 0);
        }
    }
}
