using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Shared.Exceptions
{
    public class CityException : Exception
    {
        public CityException(string message) : base(message)
        {

        }
    }
}
