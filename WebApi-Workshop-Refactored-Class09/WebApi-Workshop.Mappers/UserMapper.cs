using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Models;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserModel userModel)
        {
            return new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Username = userModel.Username,
                Address = userModel.Address,
                Age = userModel.Age
            };
        }

        public static UserModel ToUserModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Address = user.Address,
                Age = user.Age
            };
        }

        public static User MapUser(User user, UserModel userModel, Movie movie)
        {
            user.Id = userModel.Id;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Username = userModel.Username;
            user.Address = userModel.Address;
            user.Age = userModel.Age;

            return user;

        }
    }
}
