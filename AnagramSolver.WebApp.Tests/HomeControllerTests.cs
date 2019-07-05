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
        private readonly IAnagramSolver _anagramSolver;
        private readonly IWordRepository _wordRepository;

        [Test]
        public void Form_ValidWord_ReturnsAnagrams()
        {

        }

        
    }
}
