using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class EFWordRepository : IWordRepository
    {
        private readonly AnagramsDBContext _em;

        public EFWordRepository(AnagramsDBContext dbContext)
        {
            _em = dbContext;
        }
        public void AddWord(string input)
        {
            Words word = new Words();
            word.Word = input;

            _em.Words.Add(word);
            _em.SaveChanges();
        }

        public bool DeleteWord(string wordToDelete)
        {
            throw new System.NotImplementedException();
        }

        public bool EditWord(string originalWord, string newWord)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetWords(int skip, int take)
        {
            return _em.Words.OrderBy(x => x.Word).Select(x => x.Word).Skip(skip).Take(take).ToList();
        }

        public List<string> GetWordsContainingPart(string searchPhrase)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetWordsDictionary()
        {
            List<string> wordsList = _em.Words.OrderBy(x => x.Word).Select(x => x.Word).ToList();

            return wordsList;
        }

        void IWordRepository.DeleteWord(string wordToDelete)
        {
            throw new System.NotImplementedException();
        }
    }
}
