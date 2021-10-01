using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.DataAccess;
using WebApi_Workshop.DataAccess.Implementations;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.MovieApi
{
    public class DependencyInjectionHelper
    {
        public static void IjectDBContext(IServiceCollection services, string ConnectionString)
        {
            services.AddDbContext<MoviesAppDbContext>(x => x.UseSqlServer(ConnectionString));
        }
        public static void InjectRepository(IServiceCollection service)
        {
            service.AddTransient<IRepository<Movie>, MovieRepository>();
            service.AddTransient<IRepository<User>, UserRepository>();
        }
    }
}
