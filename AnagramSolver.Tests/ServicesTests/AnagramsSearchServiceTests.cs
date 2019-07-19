using AnagramSolver.Contracts;
using AnagramSolver.Services;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.Tests.ServicesTests
{
    class AnagramsSearchServiceTests
    {
        //private ICachedWords _cachedWords;
        //private IAnagramSolver _anagramSolver;
        //private ILogger _logger;
        //private IRequestWordContract _requestWordContract;
        //private IUserContract _userContract;
        //
        //private AnagramsSearchService _anagramsSearchService;
        //
        //
        //[SetUp]
        //public void Setup()
        //{
        //    _cachedWords = Substitute.For<ICachedWords>();
        //    _anagramSolver = Substitute.For<IAnagramSolver>();
        //    _logger = Substitute.For<ILogger>();
        //    _requestWordContract = Substitute.For<IRequestWordContract>();
        //    _userContract = Substitute.For<IUserContract>();
        //
        //    _anagramsSearchService = new AnagramsSearchService(_cachedWords, _anagramSolver, _logger, _requestWordContract, _userContract);
        //
        //}
        //
        //[Test]
        //public void GetAnagrams_ShouldGetAllAnagramsOfWord()
        //{
        //    //_anagramSolver.GetAnagrams("sula").Returns(new List<string>
        //    //{
        //    //    "alus", "sula"
        //    //});
        //
        //    _anagramSolver.GetAnagrams("sula").Returns(new List<string> { "alus" });
        //
        //    var result = _anagramsSearchService.GetAnagrams("sula", "1");
        //
        //    
        //    //result.First().
        //    
        //    //result.ShouldNotBeNull();
        //
        //    Assert.Pass();
        //}
    }
}
