using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;
using WeatherApp.Models;

namespace WeatherApp.Mappers
{
    public static class WeatherDataMapper
    {
        public static WeatherData ToWeatherData(this WeatherDataModel weatherDataModel)
        {
            return new WeatherData
            {
              
                Date = weatherDataModel.Date.Date,
                TemperatureC = weatherDataModel.TemperatureC,
                TemperatureMinC = weatherDataModel.TemperatureMinC,
                TemperatureMaxC = weatherDataModel.TemperatureMaxC,
                WeatherDescription = weatherDataModel.WeatherDescription,
                Language = weatherDataModel.Language,
                Precipitation = weatherDataModel.Precipitation,
                WindSpeed = weatherDataModel.WindSpeed,
                Icon = weatherDataModel.Icon,
                WeekDayId = weatherDataModel.WeekDayId,
                WeekDay = weatherDataModel.WeekDay,
                CityId = weatherDataModel.CityId,
                City = weatherDataModel.City

            };
        }
        public static WeatherDataModel ToWeatherDataModel(this WeatherData weatherData)
        {
            return new WeatherDataModel
            {
                Id = weatherData.Id,
                Date = weatherData.Date.Date,
                TemperatureC = weatherData.TemperatureC,
                TemperatureMinC = weatherData.TemperatureMinC,
                TemperatureMaxC = weatherData.TemperatureMaxC,
                WeatherDescription = weatherData.WeatherDescription,
                Language = weatherData.Language,
                Precipitation = weatherData.Precipitation,
                WindSpeed = weatherData.WindSpeed,
                Icon = weatherData.Icon,
                WeekDayId = weatherData.WeekDayId,
                WeekDay = weatherData.WeekDay,
                CityId = weatherData.CityId,
                City = weatherData.City

            };
        }

    }
}
