
namespace AnagramSolver.EF.CodeFirst.Models
{
    public class CachedWord
    {
        public int Id { get; set; }
        public int RequestWordId { get; set; }
        public int? DictionaryWordId { get; set; }

        public virtual DictionaryWord DictionaryWord { get; set; }
        public virtual RequestWord RequestWord { get; set; }
    }
}
