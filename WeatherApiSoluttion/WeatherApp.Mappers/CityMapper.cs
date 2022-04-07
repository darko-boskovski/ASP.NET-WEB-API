using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;
using WeatherApp.Models;

namespace WeatherApp.Mappers
{
    public static class CityMapper
    {
        public static City ToCity(this CityModel cityModel)
        {
            return new City
            {
                Id = cityModel.Id,
                Name = cityModel.Name,
                Long = cityModel.Long,
                Lat = cityModel.Lat,
                WeatherData = cityModel.WeatherData
            };
        }

        public static CityModel ToCityModel(this City city)
        {
            return new CityModel
            {
                Id = city.Id,
                Name = city.Name,
                Long = city.Long,
                Lat = city.Lat,
                WeatherData = city.WeatherData
            };
        }
    }
}
