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
    public class WeatherDataRepository : IRepository<WeatherData>
    {
      
        private WeatherAppDbContext _weatherAppDbContext;

        public WeatherDataRepository(WeatherAppDbContext weatherAppDbContext)
        {
            _weatherAppDbContext = weatherAppDbContext;
        }


        public WeatherData Add(WeatherData entity)
        {
            _weatherAppDbContext.WeatherData.Add(entity);
            _weatherAppDbContext.SaveChanges();
            return entity;
        }

        public void Delete(WeatherData entity)
        {
            _weatherAppDbContext.WeatherData.Remove(entity);
            _weatherAppDbContext.SaveChanges();
        }

        public List<WeatherData> GetAll()
        {
            return _weatherAppDbContext.WeatherData
                .Include(x => x.City)
                .Include(x => x.WeekDay)
                .OrderByDescending(x=>x.CityId)
                .ToList();
        }

        public WeatherData GetById(int id)
        {
            return _weatherAppDbContext.WeatherData
               .Include(x => x.City)
               .Include(x => x.WeekDay)
               .FirstOrDefault(x => x.Id == id);
        }

        public WeatherData GetByDate(DateTime date, City city)
        {
         
            WeatherData data = _weatherAppDbContext.WeatherData
                .Include(x => x.City)
                .Include(x => x.WeekDay)
                .OrderByDescending(x=>x.Date)
                .FirstOrDefault(x => x.Date == date.Date && x.City.Name == city.Name);

            return data;
        }

        public void Update(WeatherData entity)
        {
            _weatherAppDbContext.WeatherData.Update(entity);
            _weatherAppDbContext.SaveChanges();
        }

        public WeatherData GetByName(string name)
        {
            return _weatherAppDbContext.WeatherData
                .Include(x => x.City)
                .FirstOrDefault(x => x.City.Name == name);
        }

    }
}
