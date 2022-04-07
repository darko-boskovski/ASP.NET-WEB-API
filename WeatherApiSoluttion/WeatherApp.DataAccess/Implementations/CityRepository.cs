using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess.Implementations
{
    public class CityRepository : IRepository<City>
    {
        private WeatherAppDbContext _weatherAppDbContext;

        public CityRepository (WeatherAppDbContext weatherAppDbContext)
        {
            _weatherAppDbContext = weatherAppDbContext;
        }

        public City Add(City entity)
        {
            _weatherAppDbContext.Cities.Add(entity);
            _weatherAppDbContext.SaveChanges();
            return entity;
        }

        public void Delete(City entity)
        {
            _weatherAppDbContext.Cities.Remove(entity);
            _weatherAppDbContext.SaveChanges();
        }

        public List<City> GetAll()
        {
            return _weatherAppDbContext.Cities
                 .ToList();
        }

        public City GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public City GetByDate(DateTime date, City city)
        {
            throw new NotImplementedException();
        }

        public City GetById(int id)
        {
            return _weatherAppDbContext.Cities
               .FirstOrDefault(x => x.Id == id);
        }

        public City GetByName(string name)
        {
            return _weatherAppDbContext.Cities
                 .FirstOrDefault(x =>x.Name == name);
        }

        public void Update(City entity)
        {
            _weatherAppDbContext.Cities.Update(entity);
            _weatherAppDbContext.SaveChanges();
        }
    }
}
