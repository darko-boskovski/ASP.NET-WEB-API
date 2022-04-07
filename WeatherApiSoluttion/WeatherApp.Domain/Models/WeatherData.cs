using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Models
{
    public class WeatherData : BaseEntity
    {
        public WeatherData() { }

        public WeatherData(City city, DateTime date, int tempC, int tempMinC, int tempMaxC, string weatherDescription, string precipitation, string windspeed, WeekDay weekday, string icon)
        {
            City = city;
            Date = date;
            TemperatureC = tempC;
            TemperatureMinC = tempMinC;
            TemperatureMaxC = tempMaxC;
            WeatherDescription = weatherDescription;
            Precipitation = precipitation;
            WindSpeed = windspeed;
            WeekDay = weekday;
            Icon = icon;

        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureMinC { get; set; }

        public int TemperatureMaxC { get; set; }
        public  string Language { get; set; }
        public string WeatherDescription { get; set; }

        public string Precipitation { get; set; }

        public string WindSpeed { get; set; }

        public string Icon { get; set; }

        public int WeekDayId { get; set; }

        public WeekDay WeekDay { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }


    }
}
