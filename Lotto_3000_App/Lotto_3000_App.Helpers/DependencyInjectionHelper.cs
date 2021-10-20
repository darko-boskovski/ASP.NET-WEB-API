

using Lotto_3000_App.DataAccess;
using Lotto_3000_App.DataAccess.Implementations;
using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Lotto_3000_App.Services.Implementations;
using Lotto_3000_App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lotto_3000_App.Helpers
{
    public class DependencyInjectionHelper
    {

      
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LottoDbContext>(x => x.UseSqlServer(connectionString));
        }
        public static void InjectRepository(IServiceCollection service)
        {
            service.AddTransient<IRepository<Winner>, WinnerRepository>();
            service.AddTransient<IRepository<Ticket>, TicketRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IRepository<Session>, SessionRepository>();
          
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IDrawService, DrawService>();
            services.AddTransient<IWinnersService, WinnersService>();
            services.AddTransient<IUserService, UserService>();
        }
     
    }
}
