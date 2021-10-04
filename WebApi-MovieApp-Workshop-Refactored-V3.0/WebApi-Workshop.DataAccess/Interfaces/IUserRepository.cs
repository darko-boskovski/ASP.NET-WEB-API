using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Domain.Models;

namespace WebApi_Workshop.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
        User LoginUser(string username, string password);
    }
}
