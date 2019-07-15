using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnagramSolver.EF.CodeFirst
{
    public class AnagramsDbCfContext : DbContext
    {
        public AnagramsDbCfContext(DbContextOptions<AnagramsDbCfContext> options) : base(options) { }

        public DbSet<Word> Words { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        public DbSet<CachedWord> CachedWords { get; set; }

        public class Word
        {
            public Word()
            {
                CachedWords = new HashSet<CachedWord>();
            }
           
            public int Id { get; set; }
            public string WordValue { get; set; }

            public ICollection<CachedWord> CachedWords { get; set; }
        }

        public class UserLog
        {
            public int Id { get; set; }
            public string UserIp { get; set; }
            public string RequestWord { get; set; }
            public DateTime RequestDate { get; set; }

        }

        public class CachedWord
        {
            public int Id { get; set; }
            public string RequestWord { get; set; }
            
            public int ResponseWord { get; set; }


            public virtual Word ResponseWordNavigation { get; set; }
        }

    }
}
