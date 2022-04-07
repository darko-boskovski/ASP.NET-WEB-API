using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Shared;

namespace WeatherApp.Domain.Models
{
    public class WeekDay : BaseEntity
    {
        public string MkName { get; set; }
        public string EngName { get; set; }

        [JsonIgnore]
        public List<WeatherData> WeatherData { get; set; }


    }
}
