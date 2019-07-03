using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverSingleWordTests
    {
        private string FileName;
        private string InvalidFileName;
        private FileReader FileReader;

        [SetUp]
        public void Setup()
        {
            FileName = "C:\\Users\\Julius\\Downloads\\zodynas.txt";
            InvalidFileName = "C:\\Users\\test.txt";
            FileReader = new FileReader(FileName);
        }

       
        [Test]
        public void GetWordsDictionary_ValidFileName_ExpectedBehavior()
        {
            var result = FileReader.GetWordsDictionary();

            Assert.That(result.Count > 0);
        }

        [Test]
        public void GetWordsDictionary_InvalidFileName_Exception()
        {
            FileReader._fileName = InvalidFileName;
            Assert.Throws<FileNotFoundException>(() => FileReader.GetWordsDictionary());
        }
    }
}
