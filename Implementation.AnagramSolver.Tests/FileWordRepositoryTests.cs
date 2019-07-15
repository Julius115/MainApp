using AnagramSolver.Contracts;
using NUnit.Framework;
using System.IO;

namespace AnagramSolver.BusinessLogic.Tests
{
    [TestFixture]
    class FileWordRepositoryTests
    {
        private string fileName;
        private string fileText;
        private string fileTextDuplicates;

        private IWordRepository _wordRepository;

        [SetUp]
        public void Setup()
        {
            fileName = "unit.txt";
            fileText = "lasa a sula 1\n" +
                       "saul a salu 1";
            fileTextDuplicates = "lasa a lasa 1\n" +
                                 "lasa a lasa 3";
        }

        [TearDown]
        public void Dispose()
        {
            File.Delete(fileName);
        }
       
        [Test]
        public void GetWordsDictionary_ValidFileName_ReturnsDictionary()
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(fileText);
            }

            _wordRepository = new FileWordRepository(@"C:\Users\Julius\source\repos\MainApp\Implementation.AnagramSolver.Tests\bin\Debug\netcoreapp2.1\" + fileName);
            var result = _wordRepository.GetWordsDictionary();

            Assert.That(result.Count == 4);
            Assert.That(result.Contains("lasa"));
            Assert.That(result.Contains("sula"));
            Assert.That(result.Contains("salu"));
        }

        [Test]
        public void GetWordsDictionary_ValidFileName_WithDuplicates_ReturnsDictionaryWithoutDuplicates()
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(fileTextDuplicates);
            }

            _wordRepository = new FileWordRepository(@"C:\Users\Julius\source\repos\MainApp\Implementation.AnagramSolver.Tests\bin\Debug\netcoreapp2.1\" + fileName);

            var result = _wordRepository.GetWordsDictionary();

            Assert.That(result.Count == 1);
            Assert.That(result.Contains("lasa"));
        }
    }
}
