using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess
{
    public class WeatherAppDbContext : DbContext
    {
        public WeatherAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>()
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<City>()
                .Property(x => x.Long)
                .HasMaxLength(10);
            modelBuilder.Entity<City>()
                .Property(x => x.Lat)
                .HasMaxLength(10);


            modelBuilder.Entity<WeekDay>()
                .Property(x => x.MkName)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<WeekDay>()
                .Property(x => x.EngName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<WeatherData>()
                .Property(x => x.Date)
                .HasMaxLength(100);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.TemperatureC)
                .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.TemperatureMaxC)
                .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.TemperatureMinC)
                .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.Language)
                .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
              .Property(x => x.Icon)
              .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.WeatherDescription)
                 .HasMaxLength(100);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.Precipitation)
                .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
                .Property(x => x.WindSpeed)
                .HasMaxLength(10);
            modelBuilder.Entity<WeatherData>()
                .HasOne(x => x.WeekDay)
                .WithMany(x => x.WeatherData)
                .HasForeignKey(x => x.WeekDayId);
            modelBuilder.Entity<WeatherData>()
                .HasOne(x => x.City)
                .WithMany(x => x.WeatherData)
                .HasForeignKey(x => x.CityId);




            modelBuilder.Entity<City>()
                .HasData(
                    new City()
                    {
                        Id = 1,
                        Name = "Skopje",
                        Long = "21.5122",
                        Lat = "42.002",
                    
                    });

            modelBuilder.Entity<WeekDay>()
                .HasData(
                     new WeekDay()
                     {                    
                         Id = 1,
                         EngName = "Monday",
                         MkName = "Понеделник"    
                         
                     },
                     new WeekDay()
                     {
                         Id = 2,
                         EngName = "Tuesday",
                         MkName = "Вторник"

                     },
                     new WeekDay()
                     {
                         Id = 3,
                         EngName = "Wednesday",
                         MkName = "Среда"

                     },
                     new WeekDay()
                     {
                         Id = 4,
                         EngName = "Thursday",
                         MkName = "Четврток"

                     },
                     new WeekDay()
                     {
                         Id = 5,
                         EngName = "Friday",
                         MkName = "Петок"

                     },
                     new WeekDay()
                     {
                         Id = 6,
                         EngName = "Saturday",
                         MkName = "Сабота"

                     },
                     new WeekDay()
                     {
                         Id = 7,
                         EngName = "Sunday",
                         MkName = "Недела"

                     });

            modelBuilder.Entity<WeatherData>()
                .HasData(
                     new WeatherData()
                     {
                         Id = 1,
                         CityId = 1,
                         WeekDayId = 1,
                         Date = DateTime.Now,
                         TemperatureC = 13,
                         TemperatureMinC = 5,
                         TemperatureMaxC = 13,
                         Language = "eng",
                         WeatherDescription = "Light Rain",
                         Icon = "09d",
                         Precipitation = "48",
                         WindSpeed = "5.93m/s"
           

                     });




        }
    }
}
