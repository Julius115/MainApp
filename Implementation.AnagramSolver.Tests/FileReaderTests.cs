using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class FileReaderTests
    {
        private string fileName;
        private string fileText;
        private string fileTextDuplicates;
        private string input;
        
        [SetUp]
        public void Setup()
        {
            fileName = "C:\\Users\\Julius\\Downloads\\unit.txt";
            fileText = "lasa a sula 1\n" +
                       "saul a salu 1";
            fileTextDuplicates = "lasa a lasa 1\n" +
                                 "lasa a lasa 3";
            input = "alus";
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

            FileReader FileReader = new FileReader(fileName);
            var result = FileReader.GetWordsDictionary();

            Assert.That(result.Count == 4);
            Assert.That(result.ContainsKey("lasa"));
            Assert.That(result.ContainsKey("sula"));
            Assert.That(result.ContainsKey("salu"));
        }

        [Test]
        public void GetWordsDictionary_ValidFileName_WithDuplicates_ReturnsDictionaryWithoutDuplicates()
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(fileTextDuplicates);
            }

            FileReader FileReader = new FileReader(fileName);

            var result = FileReader.GetWordsDictionary();

            Assert.That(result.Count == 1);
            Assert.That(result.ContainsKey("lasa"));
        }
    }
}
