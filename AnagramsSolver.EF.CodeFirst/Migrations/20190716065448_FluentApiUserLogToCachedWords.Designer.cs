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
    [Migration("20190716065448_FluentApiUserLogToCachedWords")]
    partial class FluentApiUserLogToCachedWords
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

                    b.Property<string>("RequestWord");

                    b.Property<int>("WordId");

                    b.HasKey("Id");

                    b.HasIndex("RequestWord");

                    b.HasIndex("WordId");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+UserLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RequestDate");

                    b.Property<string>("RequestWord")
                        .IsRequired();

                    b.Property<string>("UserIp");

                    b.HasKey("Id");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WordValue");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+CachedWord", b =>
                {
                    b.HasOne("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+UserLog", "UserLog")
                        .WithMany("CachedWords")
                        .HasForeignKey("RequestWord")
                        .HasPrincipalKey("RequestWord");

                    b.HasOne("AnagramSolver.EF.CodeFirst.AnagramsDbCfContext+Word", "Word")
                        .WithMany("CachedWords")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
