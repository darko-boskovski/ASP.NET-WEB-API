using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Models;

namespace WebApi_Workshop.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        void AddUser(UserModel userModel);
        void UpdateUser(UserModel userModel);
        void DeleteUser(int id);
    }
}
