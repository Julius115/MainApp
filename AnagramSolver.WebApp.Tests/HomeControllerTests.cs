using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.WebApp.Tests
{
    [TestFixture]
    class HomeControllerTests
    {
        private IAnagramSolver _anagramSolver;
        private IWordRepository _wordRepository;

        [SetUp]
        public void Setup()
        {
            _wordRepository = new FileWordRepository("zodynas.txt");
            _anagramSolver = new AnagramSolverSingleWord(_wordRepository);
        }

        [Test]
        public void Form_ValidWord_ReturnsAnagrams()
        {

        }
    }
}
