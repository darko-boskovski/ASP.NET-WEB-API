using Lotto_3000_App.Domain.Models;
using Lotto_3000_App.Models.Users;
using Lotto_3000_App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserModel userModel)
        {
            return new User
            {
                Id = userModel.Id,
                Firstname = userModel.Firstname,
                Lastname = userModel.Lastname,
                Username = userModel.Username,
                Role = (int)userModel.Role

            };
        }

        public static UserModel ToUserModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Role = (Role)user.Role

            };
        }

    }
}
