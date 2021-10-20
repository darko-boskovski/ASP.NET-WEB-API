using Lotto_3000_App.Models;
using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Services.Interfaces
{
    public interface IUserService
    {
        string Login(LoginModel loginUserModel);
        void Register(RegisterModel model);
        void AddUser(UserModel userModel);
        void UpdateUser(UserModel userModel);
        void DeleteUser(int id);

    }
}
