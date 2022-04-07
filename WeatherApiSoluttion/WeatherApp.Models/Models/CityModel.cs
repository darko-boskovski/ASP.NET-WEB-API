using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;

namespace WeatherApp.Models
{
    public class CityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Long { get; set; }

        public string  Lat { get; set; }
        public List<WeatherData> WeatherData { get; set; }
    }
}
