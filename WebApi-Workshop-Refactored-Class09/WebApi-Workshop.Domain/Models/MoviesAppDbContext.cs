using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Domain.Models
{
    public class MoviesAppDbContext : DbContext
    {
        public MoviesAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(300);
            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .HasMaxLength(10);
            modelBuilder.Entity<Movie>()
                .HasOne(x => x.User)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.UserId);



            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
               .Property(x => x.Username)
               .HasMaxLength(50)
               .IsRequired();
            modelBuilder.Entity<User>()
               .Property(x => x.Address)
                .HasMaxLength(150);
            modelBuilder.Entity<User>()
               .Ignore(x => x.Age);

            modelBuilder.Entity<User>()
               .HasData(
               new User()
               {
                   Id = 1,
                   FirstName = "Pero",
                   LastName = "Blazevski",
                   Username = "Pero123",
                   Address = "Street 01",


               },
               new User()
               {
                   Id = 2,
                   FirstName = "Blazo",
                   LastName = "Ristovski",
                   Username = "Blazo123",
                   Address = "Street 02",

               },
                new User()
                {
                    Id = 4,
                    FirstName = "User4",
                    LastName = "4ski",
                    Username = "4-123",
                    Address = "Street 04",

                },
            new User()
            {
                Id = 5,
                FirstName = "User5",
                LastName = "5ski",
                Username = "1235",
                Address = "Street 05",

            },
            new User()
            {
                Id = 3,
                FirstName = "Risto",
                LastName = "Petkovski",
                Username = "Risto123",
                Address = "Street 03",

            });

            modelBuilder.Entity<Movie>()
           .HasData(
                 new Movie()
                 {
                     Id = 1,
                     Title = "Memento",
                     Description = "A man with short-term memory loss attempts to track down his wife's murderer.",
                     Year = 2000,
                     Genre = Genre.Mystery,
                     UserId = 1,

                 },
               new Movie()
               {
                   Id = 2,
                   Title = "Pulp Fiction",
                   Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                   Year = 1994,
                   Genre = Genre.Crime,
                   UserId = 2,
               },
                  new Movie()
                  {
                      Id = 3,
                      Title = "Oldboy",
                      Description = "After being kidnapped and imprisoned for fifteen years, Oh Dae-Su is released, only to find that he must find his captor in five days.",
                      Year = 2003,
                      Genre = Genre.Mystery,
                      UserId = 3,
                  });



        }

    }
}
