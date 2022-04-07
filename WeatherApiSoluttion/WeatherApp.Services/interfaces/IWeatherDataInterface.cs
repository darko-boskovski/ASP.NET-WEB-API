using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;
using WeatherApp.Models;

namespace WeatherApp.Services.interfaces
{
    public interface IWeatherDataInterface
    {
        List<WeatherDataModel> GetAll();
        WeatherDataModel GetById(int id);
        WeatherDataModel GetByName(string name);
        WeatherDataModel GetByDate(DateTime date, City city);
        void Add(WeatherDataModel entity);
        void Delete(int id);
        void Update(WeatherDataModel entity);
        WeatherDataModel fetchCurrentWeatherData();
        List<WeatherDataModel> fetchWeatherData(string cityName, string language, string date);
    }
}
