using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_3000_App.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly LottoDbContext _lottoDbContext;
        public UserRepository(LottoDbContext lottoDbContext)
        {
            _lottoDbContext = lottoDbContext;
        }

        public void Add(User entity)
        {
            _lottoDbContext.Users.Add(entity);
            _lottoDbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _lottoDbContext.Users.Remove(entity);
            _lottoDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _lottoDbContext
                .Users
                .Include(x => x.Tickets)
                .ToList();
        }

        public User GetById(int id)
        {
            return _lottoDbContext
                .Users
                .Include(x => x.Tickets)
                .FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _lottoDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string password)
        {
            return _lottoDbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }

        public void Update(User entity)
        {
            _lottoDbContext.Users.Update(entity);
            _lottoDbContext.SaveChanges();
        }
    }
}
