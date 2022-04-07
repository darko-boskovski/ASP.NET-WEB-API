using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;

namespace WeatherApp.Models
{
    public class WeekDayModel
    {
        public int Id { get; set; }
        public string MkName { get; set; }
        public string EngName { get; set; }
        public List<WeatherData> WeatherData { get; set; }
    }
}
