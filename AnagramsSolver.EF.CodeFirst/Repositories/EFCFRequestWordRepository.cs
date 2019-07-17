using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFRequestWordRepository : IRequestWordContract
    {
        private readonly AnagramsDbCfContext _em;

        public EFCFRequestWordRepository(AnagramsDbCfContext dbContext)
        {
            _em = dbContext;
        }

        public void SetRequestWord(string requestWord)
        {
            if (_em.RequestWords.Where(r => r.Word == requestWord).Count() == 0)
            {
                RequestWord requestWordObject = new RequestWord();
                requestWordObject.Word = requestWord;
                _em.RequestWords.Add(requestWordObject);
                _em.SaveChanges();
            }
        }
    }
}
