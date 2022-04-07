using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Implementations;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;
using WeatherApp.Services.interfaces;
using WeatherApp.Services.Implementations;

namespace WeatherApp.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WeatherAppDbContext>(x => x.UseSqlServer(connectionString));
        }

        public static void InjectRepository(IServiceCollection service)
        {
            service.AddTransient<IRepository<WeekDay>, WeekDayRepository>();
            service.AddTransient<IRepository<City>, CityRepository>();
            service.AddTransient<IRepository<WeatherData>, WeatherDataRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ICityInterface, CityService>();
            services.AddTransient<IWeekDayInterface, WeekDayService>();
            services.AddTransient<IWeatherDataInterface, WeatherDataService>();
        }

    }
}
