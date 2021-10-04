using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi_Workshop.DataAccess.Interfaces;
using WebApi_Workshop.Domain.Models;

namespace WebApi_Workshop.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private MoviesAppDbContext _moviesAppDbContext;

        public UserRepository(MoviesAppDbContext moviesAppDbContext)
        {
            _moviesAppDbContext = moviesAppDbContext;
        }


        public void Add(User entity)
        {
            _moviesAppDbContext.Users.Add(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _moviesAppDbContext.Users.Remove(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _moviesAppDbContext
                .Users
                .Include(x => x.Movies)
                .ToList();


        }

        public User GetById(int id)
        {
            return _moviesAppDbContext
                .Users
                .Include(x => x.Movies)
                .FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _moviesAppDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string password)
        {
            return _moviesAppDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower()
        && x.Password == password);
        }

        public void Update(User entity)
        {
            _moviesAppDbContext.Users.Update(entity);
            _moviesAppDbContext.SaveChanges();
        }
    }
}
