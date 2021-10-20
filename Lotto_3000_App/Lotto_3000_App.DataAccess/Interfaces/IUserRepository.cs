using Lotto_3000_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
        User LoginUser(string username, string password);
    }
}
