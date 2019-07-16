using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst
{
    public class AnagramsDbCfContext : DbContext
    {
        public AnagramsDbCfContext(DbContextOptions<AnagramsDbCfContext> options) : base(options) { }
        public DbSet<RequestWord> RequestWords { get; set; }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<DictionaryWord> DictionaryWords { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        public class RequestWord
        {
            public int Id { get; set; }
            public string Word { get; set; }

            public virtual ICollection<UserLog> UserLogs { get; set; }
            public virtual ICollection<CachedWord> CachedWords { get; set; }
        }

        public class CachedWord
        {
            public int Id { get; set; }
            public int RequestWordId { get; set; }
            public int DictionaryWordId { get; set; }

            public virtual DictionaryWord DictionaryWord { get; set; }
            public virtual RequestWord RequestWord { get; set; }
        }

        public class DictionaryWord
        {
            public int Id { get; set; }
            public string Word { get; set; }

            public virtual ICollection<CachedWord> CachedWords { get; set; }
        }

        public class UserLog
        {
            public int Id { get; set; }
            public string UserIp { get; set; }
            public DateTime RequestDate { get; set; }
            public int RequestWordId { get; set; }

            public virtual RequestWord RequestWord { get; set; }
        }
    }
}
