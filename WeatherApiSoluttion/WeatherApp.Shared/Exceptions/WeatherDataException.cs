using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Shared.Exceptions
{
    public class WeatherDataException : Exception
    {
        public WeatherDataException(string message) : base(message)
        {

        }
    }
}
