﻿// <auto-generated />
using System;
using AnagramSolver.EF.CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    [DbContext(typeof(AnagramsDbCfContext))]
    [Migration("20190716123326_initia10")]
    partial class initia10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+CachedWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DictionaryWordId");

                    b.Property<int>("RequestWordId");

                    b.HasKey("Id");

                    b.HasIndex("DictionaryWordId");

                    b.HasIndex("RequestWordId");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+DictionaryWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("DictionaryWords");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+RequestWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserLogId");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.HasIndex("UserLogId");

                    b.ToTable("RequestWords");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+UserLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RequestDate");

                    b.Property<string>("RequestWordId");

                    b.Property<string>("UserIp");

                    b.HasKey("Id");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+CachedWord", b =>
                {
                    b.HasOne("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+DictionaryWord", "DictionaryWord")
                        .WithMany("CachedWords")
                        .HasForeignKey("DictionaryWordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+RequestWord", "RequestWord")
                        .WithMany("CachedWords")
                        .HasForeignKey("RequestWordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+RequestWord", b =>
                {
                    b.HasOne("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+UserLog", "UserLog")
                        .WithMany("RequestWords")
                        .HasForeignKey("UserLogId");
                });
#pragma warning restore 612, 618
        }
    }
}
