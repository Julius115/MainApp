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

        //public DbSet<Word> Words { get; set; }
        //public DbSet<UserLog> UserLogs { get; set; }
        //
        //public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<RequestWord> RequestWords { get; set; }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<DictionaryWord> DictionaryWords { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            //modelBuilder.Entity<CachedWord>()
            //    .HasOne(c => c.Word)
            //    .WithMany(u => u.)

            //modelBuilder.Entity<CachedWord>()
            //    .HasOne(c => c.UserLog)
            //    .WithMany(u => u.CachedWords)
            //    .HasForeignKey(c => c.RequestWord)
            //    .HasPrincipalKey(u => u.RequestWord);

            //modelBuilder.Entity<UserLog>()
            //    .HasOne(c => c.CachedWord)
            //    .WithMany(u => u.UserLogs)
            //    .HasForeignKey(c => c.RequestWord)
            //    .HasPrincipalKey(u => u.RequestWord);

            //modelBuilder.Entity<CachedWord>()
            //    .HasMany(c => c.UserLogs)
            //    .WithOne(u => u.CachedWord)
            //    .HasForeignKey(c => c.RequestWord)
            //    .HasPrincipalKey(u => u.RequestWord);
        }

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
            //public virtual ICollection<CachedWord> CachedWords { get; set; }
            //public virtual ICollection<RequestWord> RequestWords { get; set; }

        }


        //public class Word
        //{
        //    public Word()
        //    {
        //        CachedWords = new HashSet<CachedWord>();
        //    }
        //   
        //    public int Id { get; set; }
        //    public string WordValue { get; set; }
        //
        //    public ICollection<CachedWord> CachedWords { get; set; }
        //}
        //
        //public class UserLog
        //{
        //    public int Id { get; set; }
        //    public string UserIp { get; set; }
        //
        //    //public virtual CachedWord RequestWord { get; set; }
        //
        //    public string RequestWord { get; set; }
        //    public DateTime RequestDate { get; set; }
        //
        //    //public int CachedWordId { get; set; }
        //    public virtual ICollection<CachedWord> CachedWords { get; set; }
        //    
        //    //public virtual CachedWord CachedWord { get; set; }
        //    
        //    
        //    //public virtual CachedWord CachedWord { get; set; }
        //    //[ForeignKey("RequestWord")]
        //    //public virtual CachedWord CachedWord{ get; set;}
        //    
        //}
        //
        //public class CachedWord
        //{
        //    public int Id { get; set; }
        //
        //    public string RequestWord { get; set; }
        //    
        //    public int WordId { get; set; }
        //
        //    public virtual Word Word { get; set; }
        //
        //    public virtual UserLog UserLog { get; set; }
        //    //public virtual ICollection<UserLog> UserLogs { get; set; }
        //    
        //    //public virtual UserLog UserLog { get; set; }
        //}

    }
}
