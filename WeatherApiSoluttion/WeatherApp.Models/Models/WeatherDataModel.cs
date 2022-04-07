using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;

namespace WeatherApp.Models
{
    public class WeatherDataModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureMinC { get; set; }

        public int TemperatureMaxC { get; set; }
        public string Language { get; set; }
        public string WeatherDescription { get; set; }

        public string Precipitation { get; set; }

        public string WindSpeed { get; set; }

        public int WeekDayId { get; set; }

        public string Icon { get; set; }

        public WeekDay WeekDay { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        //public WeatherDataModel()
        //{
        //    Date = DateTime.Now;

        //}
    }
}
