using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesAPI.DataAccess;
using SEDC.NotesAPI.DataAccess.AdoNetNoteRepository;
using SEDC.NotesAPI.DataAccess.DapperNoteRepository;
using SEDC.NotesAPI.Domain.Models;
using SEDC.NotesAPI.Services.Implementations;
using SEDC.NotesAPI.Services.Interfaces;

namespace SEDC.NotesAPI.Helpers
{
    public class DependencyInjectionHelper
    {

        public static void InjectAdoRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteAdoNetRepository(connectionString));
            services.AddTransient<IRepository<User>>(x => new UserAdoNetRepository(connectionString));
        }
        public static void InjectDapperRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteDapperRepository(connectionString));
            services.AddTransient<IRepository<User>>(x => new UserDapperRepository(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
