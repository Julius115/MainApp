using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramSolver.EF.DatabaseFirst
{
    public partial class AnagramsDBContext : DbContext
    {
        public AnagramsDBContext()
        {
        }

        public AnagramsDBContext(DbContextOptions<AnagramsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWords> CachedWords { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Words> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AnagramsDB; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWords>(entity =>
            {
                entity.Property(e => e.RequestWord).IsRequired();

                entity.HasOne(d => d.ResponseWordNavigation)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.ResponseWord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CachedWor__Respo__6EF57B66");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestWord)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.UserIp)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
