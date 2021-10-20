using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lotto_3000_App.DataAccess
{
    public class LottoDbContext : DbContext
    {

        public LottoDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Winner> Winners { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var splitStringConverter = new ValueConverter<IEnumerable<string>, string>(v => string.Join(";", v), v => v.Split(new[] { ';' }));


            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(
                Encoding.ASCII.GetBytes("admin123"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .Property(nameof(Ticket.TicketCombination))
                .HasConversion(splitStringConverter);
            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<Session>()
               .Property(x => x.TimeCreated);
            modelBuilder.Entity<Session>()
                .Property(nameof(Session.WinningCombination))
                .HasConversion(splitStringConverter);


            modelBuilder.Entity<User>()
                .Property(x => x.Firstname)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(x => x.Lastname)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
             .Property(x => x.Role)
             .HasMaxLength(10);
            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Winner>()
               .Property(x => x.Fullname)
              .HasMaxLength(50);
            modelBuilder.Entity<Winner>()
                .Property(nameof(Winner.TicketCombination))
                .HasConversion(splitStringConverter);
            modelBuilder.Entity<Winner>()
                .HasOne(x => x.Session)
                .WithMany(x => x.Winners)
                .HasForeignKey(x => x.SessionId);

            modelBuilder.Entity<User>()
                .HasData(
                 new User()
                   {
                       Id = 1,
                       Firstname = "Darko",
                       Lastname = "Boskovski",
                       Username = "admin",
                       Role = 1,
                       Password = hashedPassword,
                       Tickets = new List<Ticket>()

                   });

            //modelBuilder.Entity<Session>()
            //   .HasData(
            //   new Session()
            //   {
            //       Id = 1,
            //       TimeCreated = DateTime.Now,
            //       NotActive = true,
            //       Winners = new List<Winner>(),
            //       WinningCombination = new List<string> { "1", "2", "3", "4", "5", "6", "7" },


            //   });

            //_ = modelBuilder.Entity<Ticket>()
            //.HasData(
            // new Ticket()
            // {
            //     Id = 1,
            //     TicketCombination = new List<string> { "1", "2", "3", "4", "5", "6", "7" },
            //     SessionId = 1,
            //     UserId = 1

            // });

            //modelBuilder.Entity<Winner>()
            //    .HasData(
            //    new Winner()
            //    {
            //        Id = 1,
            //        TicketCombination = new List<string> { "1", "2", "3", "4", "5", "6", "7" },
            //        SessionId = 1,
            //        Fullname = "Darko Boskovski",
            //        Prize = 1

            //    });



        }


    }

}
