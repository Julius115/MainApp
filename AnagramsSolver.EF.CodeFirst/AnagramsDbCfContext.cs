using AnagramSolver.EF.CodeFirst.Models;
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWord>()
                .HasOne(c => c.RequestWord)
                .WithMany(r => r.CachedWords)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RequestWord>()
                .HasMany(r => r.UserLogs)
                .WithOne(u => u.RequestWord)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
