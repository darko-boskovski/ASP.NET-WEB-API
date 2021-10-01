using System;
using System.Collections.Generic;
using System.Text;
using WebApi_Workshop.DataAccess;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Mappers;
using WebApi_Workshop.Models;
using WebApi_Workshop.Services.Interfaces;
using WebApi_Workshop.Shared.Exceptions;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<Movie> _movieRepository;
        private IRepository<User> _userRepository;

        public UserService(IRepository<Movie> movieRepository, IRepository<User> userRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public void AddUser(UserModel userModel)
        {

            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new MovieException("The property UserName for user is required");
            }
            if (userModel.Username.Length > 100)
            {
                throw new MovieException("The property Username can't contain more then 100 characters");
            }
            if (userModel.Username.Length > 50)
            {
                throw new MovieException("The property UserName can't contain more then 10 characters");
            }
            if (userModel.Id != 0)
            {
                throw new MovieException("Id must not be set!");
            }

            User userForDb = userModel.ToUser();
            _userRepository.Add(userForDb);
        }

        public void DeleteUser(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {id} was not found");
            }
            _userRepository.Delete(userDb);
        }

        public List<UserModel> GetAllUsers()
        {
            List<User> usersDb = _userRepository.GetAll();
            List<UserModel> userModels = new List<UserModel>();
            foreach (User user in usersDb)
            {
                userModels.Add(user.ToUserModel());
            }
            return userModels;
        }

        public UserModel GetUserById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {id} was not found");
            }

            return userDb.ToUserModel();
        }

        public void UpdateUser(UserModel userModel)
        {
            User userDb = _userRepository.GetById(userModel.Id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {userModel.Id} was not found!");
            }
            Movie movieDb = _movieRepository.GetById(userModel.Id);
            if (movieDb == null)
            {
                throw new NotFoundException($"The movie with id {userModel.Id} was not found");
            }
            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new MovieException("The property Username for Movie is required");
            }
            if (userModel.Username.Length > 50)
            {
                throw new MovieException("The property Username can not contain more than 50 characters");
            }
            if (!string.IsNullOrEmpty(userModel.FirstName) && userModel.FirstName.Length > 50)
            {
                throw new MovieException("The property FirstName can not contain more than 30 characters");
            }

            _userRepository.Update(UserMapper.MapUser(userDb, userModel,movieDb));
        }
    }
}
