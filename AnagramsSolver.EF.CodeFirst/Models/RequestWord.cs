using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class RequestWord
    {
        public int Id { get; set; }
        public string Word { get; set; }

        public virtual ICollection<UserLog> UserLogs { get; set; }
        public virtual ICollection<CachedWord> CachedWords { get; set; }
    }
}
