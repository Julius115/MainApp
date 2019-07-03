using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverSingleWordTests
    {
        private Dictionary<string, int> Words;

        [SetUp]
        public void Setup()
        {
            Words = new Dictionary<string, int>() { { "sula", 1 }, { "sala", 1 } };
        }

        [Test]
        public void GetAnagrams_ContainsAnagram_ExpectedBehavior()
        {
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(Words);
            List<string> result = anagramSolver.GetAnagrams("alas").ToList();

            Assert.That(result.Count == 1);
            Assert.That(result[0] == "sala");
        }

        [Test]
        public void GetAnagrams_ContainsTwoAnagrams_ExpectedBehavior()
        {
            Words = new Dictionary<string, int>() { { "sula", 1 }, { "salu", 1 } };
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(Words);
            List<string> result = anagramSolver.GetAnagrams("sula").ToList();

            Assert.That(result.Count == 2);
        }

        [Test]
        public void GetAnagrams_DoesNotContainAnagram_ExpectedBehavior()
        {
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(Words);
            List<string> result = anagramSolver.GetAnagrams("las").ToList();

            Assert.That(result.Count == 0);
        }
    }
}
