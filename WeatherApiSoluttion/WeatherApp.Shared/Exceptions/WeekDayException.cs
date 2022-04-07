using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Shared.Exceptions
{
    public class WeekDayException : Exception
    {
        public WeekDayException(string message) : base(message)
        {

        }
    }
}
