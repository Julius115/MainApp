using System;

namespace AnagramSolver.EF.CodeFirst.Models
{
    public class UserLog
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestWordId { get; set; }
        public int UserId { get; set; }

        public virtual RequestWord RequestWord { get; set; }
        public virtual User User { get; set; }
    }
}
