using AnagramSolver.Contracts;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic.Tests
{
    [TestFixture]
    class AnagramSolverSingleWordTests
    {
        private IAnagramSolver _anagramSolver;
        private IWordRepository _wordRepository;

        private string fileName;
        private string fileText;

        [SetUp]
        public void Setup()
        {
            
            fileName = "units.txt";
            fileText = "salu a sula 1";

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
            fileText = "sala a sala 1";

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
            var result = _anagramSolver.GetAnagrams("alus");

            Assert.That(result.Count == 2);
            Assert.That(result.Contains("salu"));
            Assert.That(result.Contains("sula"));
        }

        [Test]
        public void GetAnagrams_DoesNotContainAnagram_ReturnsEmptyList()
        {
            var result = _anagramSolver.GetAnagrams("las");

            Assert.That(_wordRepository.GetWordsDictionary().Count > 0);
            Assert.That(result.Count == 0);
        }
    }
}
