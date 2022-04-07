using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;
using WeatherApp.Mappers;
using WeatherApp.Models;
using WeatherApp.Services.interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Services.Implementations
{
    public class CityService : ICityInterface
    {
        private IRepository<City> _cityRepository;
        private IRepository<WeekDay> _weekDayRepository;
        private IRepository<WeatherData> _weatherDataRepository;

        public CityService(IRepository<City> cityRepository, IRepository<WeekDay> weekDayRepository, IRepository<WeatherData> weatherDataRepository)
        {
            _cityRepository = cityRepository;
            _weekDayRepository = weekDayRepository;
            _weatherDataRepository = weatherDataRepository;
        }


        public City AddCity(CityModel cityModel)
        {
            if (string.IsNullOrEmpty(cityModel.Name))
            {
                throw new CityException("The property Name for city is required");
            }
            if (cityModel.Name.Length > 100)
            {
                throw new CityException("The property Name can't contain more then 100 characters");
            }
            if (cityModel.Id != 0)
            {
                throw new CityException("Id must not be set!");
            }
            

            City cityForDb = cityModel.ToCity();
            return _cityRepository.Add(cityForDb);
        }

        public void DeleteCity(int id)
        {
            City cityDb = _cityRepository.GetById(id);
            if (cityDb == null)
            {
                throw new NotFoundException($"City with id {id} was not found");
            }
            _cityRepository.Delete(cityDb);
        }

        public List<CityModel> GetAllCities()
        {
            List<City> cityDb = _cityRepository.GetAll();
            List<CityModel> cityModels = new List<CityModel>();
            foreach (City city in cityDb)
            {
                cityModels.Add(city.ToCityModel());
            }
            return cityModels;
        }

        public CityModel GetCityById(int id)
        {
            City cityDb = _cityRepository.GetById(id);
            if (cityDb == null)
            {
                throw new NotFoundException($"City with id {id} was not found");
            }

            return cityDb.ToCityModel();
        }

        public CityModel GetCityByName(string name)
        {
            City cityDb = _cityRepository.GetByName(name);
            if (cityDb == null)
            {
                throw new NotFoundException($"City with name {name} was not found");
            }

            return cityDb.ToCityModel();
        }

        public void UpdateCity(CityModel cityModel)
        {
            City cityDb = _cityRepository.GetById(cityModel.Id);
            if (cityDb == null)
            {
                throw new NotFoundException($"City with id {cityModel.Id} was not found!");
            }
            
            if (string.IsNullOrEmpty(cityModel.Name))
            {
                throw new CityException("The property Name for city is required");
            }
            if (cityModel.Name.Length > 100)
            {
                throw new CityException("The property Name can not contain more than 100 characters");
            }

            cityDb = cityModel.ToCity();
          
            _cityRepository.Update(cityDb);
        }
    }
}
