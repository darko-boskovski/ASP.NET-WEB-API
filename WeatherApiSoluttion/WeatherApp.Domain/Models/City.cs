using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public string Long { get; set; }

        public string Lat { get; set; }

        [JsonIgnore]
        public List<WeatherData> WeatherData { get; set; }
    }

}
