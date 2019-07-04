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
        private Dictionary<string, int> words;

        [SetUp]
        public void Setup()
        {
            words = new Dictionary<string, int>() { { "sula", 1 }, { "sala", 1 } };
        }

        [Test]
        public void GetAnagrams_ContainsAnagram_ReturnsOneAnagram()
        {
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(words);
            var result = anagramSolver.GetAnagrams("alas");

            Assert.That(result.Count == 1);
            Assert.That(result.First() == "sala");
        }

        [Test]
        public void GetAnagrams_ContainsMultipleAnagrams_ReturnsMultipleAnagrams()
        {
            var words = new Dictionary<string, int>() { { "sula", 1 }, { "salu", 1 } };
            var anagramSolver = new AnagramSolverSingleWord(words);
            var result = anagramSolver.GetAnagrams("sula");

            Assert.That(result.Count == 2);
            Assert.That(result.Contains("sula"));
            Assert.That(result.Contains("salu"));
        }

        [Test]
        public void GetAnagrams_DoesNotContainAnagram_ReturnsEmptyList()
        {
            var anagramSolver = new AnagramSolverSingleWord(words);
            var result = anagramSolver.GetAnagrams("las");

            Assert.That(result.Count == 0);
        }
    }
}
