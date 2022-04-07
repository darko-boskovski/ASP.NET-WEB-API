using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess.Implementations
{
    public class WeekDayRepository : IRepository<WeekDay>
    {
        private WeatherAppDbContext _weatherAppDbContext;

        public WeekDayRepository(WeatherAppDbContext weatherAppDbContext)
        {
            _weatherAppDbContext = weatherAppDbContext;
        }

        public WeekDay Add(WeekDay entity)
        {
            _weatherAppDbContext.WeekDays.Add(entity);
            _weatherAppDbContext.SaveChanges();

            return entity;
        }

        public void Delete(WeekDay entity)
        {
            _weatherAppDbContext.WeekDays.Remove(entity);
            _weatherAppDbContext.SaveChanges();
        }

        public List<WeekDay> GetAll()
        {
            return _weatherAppDbContext.WeekDays
                 .ToList();
        }

        public WeekDay GetByDate(DateTime date)
        {
            return _weatherAppDbContext.WeekDays
               .FirstOrDefault(x => x.WeatherData.Select(x=>x.Date).FirstOrDefault() == date);
        }

        public WeekDay GetByDate(DateTime date, City city)
        {
            throw new NotImplementedException();
        }

        public WeekDay GetById(int id)
        {
            return _weatherAppDbContext.WeekDays
               .FirstOrDefault(x => x.Id == id);
        }

        public WeekDay GetByName(string name)
        {
            return _weatherAppDbContext.WeekDays
                 .FirstOrDefault(x => x.MkName == name);
        }

        public void Update(WeekDay entity)
        {
            _weatherAppDbContext.WeekDays.Update(entity);
            _weatherAppDbContext.SaveChanges();
        }
    }
}
