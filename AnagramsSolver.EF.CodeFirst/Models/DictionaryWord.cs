using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class DictionaryWord
    {
        public int Id { get; set; }
        public string Word { get; set; }

        public virtual ICollection<CachedWord> CachedWords { get; set; }
    }
}
