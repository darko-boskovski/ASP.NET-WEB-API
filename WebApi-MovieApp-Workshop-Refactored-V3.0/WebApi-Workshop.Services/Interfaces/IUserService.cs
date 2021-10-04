using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Models;
using WebApi_Workshop.Models.Users;

namespace WebApi_Workshop.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        void AddUser(UserModel userModel);
        void UpdateUser(UserModel userModel);
        void DeleteUser(int id);
        void Register(RegisterUserModel registerUserModel);
        string Login(LoginUserModel loginUserModel);
    }
}
