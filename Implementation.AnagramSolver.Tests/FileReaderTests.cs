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
        private string FileName;
        private string InvalidFileName;

        [SetUp]
        public void Setup()
        {
            FileName = "C:\\Users\\Julius\\Downloads\\zodynas.txt";
            InvalidFileName = "C:\\Users\\test.txt";
        }
       
        [Test]
        public void GetWordsDictionary_ValidFileName_ExpectedBehavior()
        {
            FileReader FileReader = new FileReader(FileName);
            var result = FileReader.GetWordsDictionary();

            Assert.That(result.Count > 0);
        }

        [Test]
        public void GetWordsDictionary_InvalidFileName_Exception()
        {
            FileReader FileReader = new FileReader(InvalidFileName);

            Assert.Throws<FileNotFoundException>(() => FileReader.GetWordsDictionary());
        }
    }
}
