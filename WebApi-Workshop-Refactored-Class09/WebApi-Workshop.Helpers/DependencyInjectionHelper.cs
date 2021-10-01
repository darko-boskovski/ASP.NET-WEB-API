using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.DataAccess;
using WebApi_Workshop.DataAccess.Implementations;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Services.Implementations;
using WebApi_Workshop.Services.Interfaces;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesAppDbContext>(x => x.UseSqlServer(connectionString));
        }
        public static void InjectRepository(IServiceCollection service)
        {
            service.AddTransient<IRepository<Movie>, MovieRepository>();
            service.AddTransient<IRepository<User>, UserRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IUserService, UserService>();
        }

   
    }
}
