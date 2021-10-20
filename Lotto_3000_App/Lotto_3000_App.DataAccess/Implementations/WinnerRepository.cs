using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_3000_App.DataAccess.Implementations
{
    public class WinnerRepository : IRepository<Winner>
    {
        private readonly LottoDbContext _lottoDbContext;
        public WinnerRepository(LottoDbContext lottoDbContext)
        {
            _lottoDbContext = lottoDbContext;
        }

        public void Add(Winner entity)
        {
            _lottoDbContext.Winners.Add(entity);
            _lottoDbContext.SaveChanges();
        }

        public void Delete(Winner entity)
        {
            _lottoDbContext.Winners.Remove(entity);
            _lottoDbContext.SaveChanges();
        }

        public List<Winner> GetAll()
        {
            return _lottoDbContext
                .Winners
                .Include(x => x.Session)
                .ToList();
        }

        public Winner GetById(int id)
        {
            return _lottoDbContext
                   .Winners
                   .Include(x => x.Session)
                   .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Winner entity)
        {
            _lottoDbContext.Winners.Update(entity);
            _lottoDbContext.SaveChanges();
        }
    }
}
