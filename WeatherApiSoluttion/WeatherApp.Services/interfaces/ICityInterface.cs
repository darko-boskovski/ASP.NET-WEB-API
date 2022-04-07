using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;
using WeatherApp.Models;

namespace WeatherApp.Services.interfaces
{
    public interface ICityInterface
    {
        List<CityModel> GetAllCities();
        CityModel GetCityById(int id);
        CityModel GetCityByName(string name);
        City AddCity(CityModel cityModel);
        void UpdateCity(CityModel cityModel);
        void DeleteCity(int id);
    }
}
