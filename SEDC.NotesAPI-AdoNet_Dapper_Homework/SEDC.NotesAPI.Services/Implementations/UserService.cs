using SEDC.NotesAPI.DataAccess;
using SEDC.NotesAPI.Domain.Models;
using SEDC.NotesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);

        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
