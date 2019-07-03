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
        [Test]
        public void GetAnagrams_ContainsAnagram_ExpectedBehavior()
        {
            Dictionary<string, int> words = new Dictionary<string, int>() { { "sula", 1 }, { "sala", 1 } };
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(words);
            List<string> result = anagramSolver.GetAnagrams("alas").ToList();

            Assert.That(result.Count == 1);
            Assert.That(result[0] == "sala");
        }

        [Test]
        public void GetAnagrams_ContainsTwoAnagrams_ExpectedBehavior()
        {
            Dictionary<string, int> words = new Dictionary<string, int>() { { "sula", 1 }, { "usla", 1 } };
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(words);
            List<string> result = anagramSolver.GetAnagrams("sula").ToList();

            Assert.That(result.Count == 2);
        }

        [Test]
        public void GetAnagrams_DoesNotContainAnagram_ExpectedBehavior()
        {
            Dictionary<string, int> words = new Dictionary<string, int>() { { "sula", 1 }, { "sala", 1 } };
            AnagramSolverSingleWord anagramSolver = new AnagramSolverSingleWord(words);
            List<string> result = anagramSolver.GetAnagrams("las").ToList();

            Assert.That(result.Count == 0);
        }
    }
}
