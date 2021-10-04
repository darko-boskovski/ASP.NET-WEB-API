﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi_Workshop.Domain.Models;

namespace WebApi_Workshop.DataAccess.Migrations
{
    [DbContext(typeof(MoviesAppDbContext))]
    partial class MoviesAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi_Workshop.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("FavoriteGenre")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Street 01",
                            FavoriteGenre = 0,
                            FirstName = "Pero",
                            LastName = "Blazevski",
                            Username = "Pero123"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Street 02",
                            FavoriteGenre = 0,
                            FirstName = "Blazo",
                            LastName = "Ristovski",
                            Username = "Blazo123"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Street 04",
                            FavoriteGenre = 0,
                            FirstName = "User4",
                            LastName = "4ski",
                            Username = "4-123"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Street 05",
                            FavoriteGenre = 0,
                            FirstName = "User5",
                            LastName = "5ski",
                            Username = "1235"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Street 03",
                            FavoriteGenre = 0,
                            FirstName = "Risto",
                            LastName = "Petkovski",
                            Username = "Risto123"
                        });
                });

            modelBuilder.Entity("WebApi_Workshop_Class05.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A man with short-term memory loss attempts to track down his wife's murderer.",
                            Genre = 5,
                            Title = "Memento",
                            UserId = 1,
                            Year = 2000
                        },
                        new
                        {
                            Id = 2,
                            Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                            Genre = 7,
                            Title = "Pulp Fiction",
                            UserId = 2,
                            Year = 1994
                        },
                        new
                        {
                            Id = 3,
                            Description = "After being kidnapped and imprisoned for fifteen years, Oh Dae-Su is released, only to find that he must find his captor in five days.",
                            Genre = 5,
                            Title = "Oldboy",
                            UserId = 3,
                            Year = 2003
                        });
                });

            modelBuilder.Entity("WebApi_Workshop_Class05.Models.Movie", b =>
                {
                    b.HasOne("WebApi_Workshop.Domain.Models.User", "User")
                        .WithMany("Movies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_Workshop.Domain.Models.User", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}